using System;
using System.Collections.Generic;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    static class ScientificTermExtensions
    {
        public static IWikiReference<biologicaltermtype> MapToSchema(this CellTerm source)
        {
            return source.MapToSchema<CellTerm, biologicaltermtype>();
        }

        public static IWikiReference<biologicaltermtype> MapToSchema(this OrganTerm source)
        {
            return source.MapToSchema<OrganTerm, biologicaltermtype>();
        }

        public static IWikiReference<dataBiologicalaction> MapToSchema(this BiologicalAction source)
        {
            return source.MapToSchema<BiologicalAction, dataBiologicalaction>();
        }

        public static IWikiReference<dataBiologicalobject> MapToSchema(
            this BiologicalObject source,
            IReadOnlyDictionary<int, biologicalorganizationleveltype> biologicalOrganizationsByWikiId)
        {
            if (biologicalOrganizationsByWikiId == null)
            {
                throw new ArgumentNullException(nameof(biologicalOrganizationsByWikiId));
            }

            var destination = source.MapToSchema<BiologicalObject, dataBiologicalobject>();

            if (source.BiologicalOrganizationId.HasValue)
            {
                destination.Target.biologicalorganization =
                    biologicalOrganizationsByWikiId[source.BiologicalOrganizationId.Value];
            }

            return destination;
        }

        public static IWikiReference<dataBiologicalprocess> MapToSchema(this BiologicalProcess source)
        {
            return source.MapToSchema<BiologicalProcess, dataBiologicalprocess>();
        }

        public static IWikiReference<dataTaxonomy> MapToSchema(this TaxonTerm source)
        {
            return source.MapToSchema<TaxonTerm, dataTaxonomy>();
        }

        static IWikiReference<TBiologicalTermType> MapToSchema<TScientificTerm, TBiologicalTermType>(
            this TScientificTerm source)
            where TScientificTerm : class, IScientificTerm
            where TBiologicalTermType : biologicaltermtype, new()
        {
            var destination = new TBiologicalTermType
            {
                source = source.Source,
                sourceid = source.SourceId,
                name = source.Term,
                synonym = source.Synonym
            };

            if (destination is IXmlIdentifiable identifiable)
            {
                identifiable.id = Guid.NewGuid().ToString();
            }

            return new WikiReference<TBiologicalTermType>
            {
                AopWikiId = source.Id,
                Target = destination
            };
        }
    }
}