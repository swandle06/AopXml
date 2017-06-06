using System.Collections.Generic;
using AopWikiExporter.Data;
using AopWikiExporter.Mapping;

namespace AopWikiExporter
{
    class EnumsByWikiId
    {
        public IReadOnlyDictionary<int, applicabilitytypeLifestageLifestage> LifeStages { get; private set; }
        public IReadOnlyDictionary<int, applicabilitytypeSexSex> Sexes { get; private set; }
        public IReadOnlyDictionary<int, biologicalorganizationleveltype> BiologicalOrganizations { get; private set; }
        public IReadOnlyDictionary<int, confidenceleveltype> ConfidenceLevels { get; private set; }
        public IReadOnlyDictionary<int, statusWikistatus> WikiStatuses { get; private set; }
        public IReadOnlyDictionary<int, statusOecdstatus> OecdStatuses { get; private set; }
        public IReadOnlyDictionary<int, statusSaaopstatus> SaaopStatuses { get; private set; }

        EnumsByWikiId() { }

        public static EnumsByWikiId CreateFrom(AopWikiDbContext context)
        {
            return new EnumsByWikiId
            {
                LifeStages = context
                    .LifeStageTerms
                    .MapToLookupTable<LifeStageTerm, applicabilitytypeLifestageLifestage>(),

                Sexes = context
                    .SexTerms
                    .MapToLookupTable<SexTerm, applicabilitytypeSexSex>(),

                BiologicalOrganizations = context
                    .BiologicalOrganizations
                    .MapToLookupTable<BiologicalOrganization, biologicalorganizationleveltype>(),

                ConfidenceLevels = context
                    .ConfidenceLevels
                    .MapToLookupTable<ConfidenceLevel, confidenceleveltype>(),

                WikiStatuses = context
                    .Statuses
                    .MapToLookupTable(
                        new Dictionary<string, statusWikistatus>
                        {
                            ["Open for adopton"] = statusWikistatus.Openforadoption
                        }),

                OecdStatuses = context
                    .OecdStatuses
                    .MapToLookupTable<OecdStatus, statusOecdstatus>(),

                SaaopStatuses = context
                    .SaaopStatuses
                    .MapToLookupTable<SaaopStatus, statusSaaopstatus>()
            };
        }
    }
}
