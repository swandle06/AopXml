using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace AopWikiExporter.Mapping
{
    interface IWikiReference<out T>
    {
        int AopWikiId { get; }
        T Target { get; }
    }

    class WikiReference<T> : IWikiReference<T>
    {
        public int AopWikiId { get; set; }
        public T Target { get; set; }
    }

    static class WikiReferenceExtensions
    {
        static readonly ConcurrentDictionary<Type, string> s_TagNamesByType = new ConcurrentDictionary<Type, string>();

        public static IReadOnlyCollection<IWikiReference<TXmlIdentifiable>> AddToReferencesAndAssignToRoot<TXmlIdentifiable>(
            this IReadOnlyCollection<IWikiReference<TXmlIdentifiable>> collection,
            IList<IEnumerable<IWikiReference<IXmlIdentifiable>>> referenceLists,
            data root)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (referenceLists == null) throw new ArgumentNullException(nameof(referenceLists));
            if (root == null) throw new ArgumentNullException(nameof(root));

            referenceLists.Add(collection.Cast<IWikiReference<IXmlIdentifiable>>());

            var property = root
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.PropertyType == typeof(TXmlIdentifiable).MakeArrayType());
            if (property == null)
            {
                throw new InvalidOperationException(
                    $"root element doesn't have a property of type {typeof(TXmlIdentifiable).Name}[]");
            }
            property.SetValue(root, collection.Select(x => x.Target).ToArray());

            return collection;
        }

        public static XmlElement ToXmlElement(this IWikiReference<IXmlIdentifiable> reference)
        {
            var document = new XmlDocument();
            var tagName = s_TagNamesByType.GetOrAdd(
                reference.Target.GetType(),
                t => t.GetTypeInfo().GetCustomAttribute<XmlRootAttribute>()?.ElementName?.Length > 0
                    ? t.GetTypeInfo().GetCustomAttribute<XmlRootAttribute>().ElementName
                    : t.Name);
            var element = document.CreateElement($"{tagName}-reference");
            var idAttribute = document.CreateAttribute("id");
            idAttribute.Value = reference.Target.id;
            var aopWikiIdAttribute = document.CreateAttribute("aop-wiki-id");
            aopWikiIdAttribute.Value = reference.AopWikiId.ToString();
            element.Attributes.Append(idAttribute);
            element.Attributes.Append(aopWikiIdAttribute);
            return element;
        }
    }
}
