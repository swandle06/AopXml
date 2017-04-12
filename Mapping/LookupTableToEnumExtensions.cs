using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AopWikiExporter.Data;
using EnumsNET.Unsafe;

namespace AopWikiExporter.Mapping
{
    static class LookupTableToEnumExtensions
    {
        static readonly Regex s_NonAlphaCharacters = new Regex("[^A-Za-z0-9]");

        public static IReadOnlyDictionary<int, TEnum> MapToLookupTable<TLookup, TEnum>(
            this IQueryable<TLookup> lookupTerms)
            where TLookup : IAopWikiIdentifiable, ITerm
            where TEnum : struct
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
            {
                throw new ArgumentException($"{typeof(TEnum).Name} must be an enum type.");
            }

            return lookupTerms.ToList()
                .ToDictionary(
                    x => x.Id,
                    x => UnsafeEnums.GetMember<TEnum>(GetEnumMemberName(x.Term), true)?.Value
                         ?? throw new ArgumentException(
                             $"Could not find enum value {x.Term} in type {typeof(TEnum).Name}"));
        }

        static string GetEnumMemberName(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(term));
            }

            var output = term;
            output = output.Trim();
            if (char.IsDigit(term[0]))
            {
                output = "Item" + output;
            }

            output = s_NonAlphaCharacters.Replace(output, string.Empty);
            return output;
        }
    }
}