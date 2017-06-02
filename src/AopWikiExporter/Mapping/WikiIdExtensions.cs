using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AopWikiExporter.Mapping
{
    static class WikiIdExtensions
    {
        static readonly ConditionalWeakTable<object, object> s_WikiIdsByObject =
            new ConditionalWeakTable<object, object>();

        public static T SetWikiId<T>(this T obj, int wikiId) where T : class
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            object existingBoxedWikiId = s_WikiIdsByObject.GetValue(obj, o => wikiId);
            if ((int)existingBoxedWikiId != wikiId)
            {
                throw new InvalidOperationException($"Attempting to set different WikiId {wikiId} for object {obj}");
            }

            return obj;
        }

        public static int GetWikiId(this object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (!s_WikiIdsByObject.TryGetValue(obj, out object wikiIdBoxed))
            {
                throw new KeyNotFoundException($"Could not get WikiId for object: {obj}");
            }
            return (int)wikiIdBoxed;
        }
    }
}