using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace AopWikiExporter.Mapping
{
    static class WikiReferenceExtensions
    {
        static readonly ConcurrentDictionary<Type, string> s_TagNamesByType = new ConcurrentDictionary<Type, string>();

        public static void AddToReferencesAndAssignToRoot<TXmlIdentifiable>(
            this IEnumerable<TXmlIdentifiable> collection,
            IList<IEnumerable<IXmlIdentifiable>> referenceLists,
            data root) where TXmlIdentifiable : IXmlIdentifiable
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (referenceLists == null) throw new ArgumentNullException(nameof(referenceLists));
            if (root == null) throw new ArgumentNullException(nameof(root));

            var array = collection.ToArray();
            referenceLists.Add(array.Cast<IXmlIdentifiable>());

            var property = root
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.PropertyType == typeof(TXmlIdentifiable).MakeArrayType());
            if (property == null)
            {
                throw new InvalidOperationException(
                    $"root element doesn't have a property of type {typeof(TXmlIdentifiable).Name}[]");
            }
            property.SetValue(root, array);
        }

        public static XmlElement ToXmlElement(this IXmlIdentifiable reference)
        {
            var document = new XmlDocument();
            var tagName = s_TagNamesByType.GetOrAdd(
                reference.GetType(),
                t => typeof(data).GetTypeInfo()
                         .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                         .FirstOrDefault(p => p.PropertyType == reference.GetType().MakeArrayType())
                         ?.GetCustomAttribute<XmlElementAttribute>()
                         ?.ElementName ?? throw new ArgumentException(
                         $"data root element doesn't contain a property of type {reference.GetType().Name}[]"));
            var element = document.CreateElement($"{tagName}-reference");
            var idAttribute = document.CreateAttribute("id");
            idAttribute.Value = reference.id;
            var aopWikiIdAttribute = document.CreateAttribute("aop-wiki-id");
            aopWikiIdAttribute.Value = reference.GetWikiId().ToString();
            element.Attributes.Append(idAttribute);
            element.Attributes.Append(aopWikiIdAttribute);
            return element;
        }
    }
}
