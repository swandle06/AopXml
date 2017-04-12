using System;
using Microsoft.EntityFrameworkCore;

namespace AopWikiExporter.Data
{
    public partial class AopWikiDbContext : DbContext
    {
        readonly string _connectionString;

        public virtual DbSet<AopContributor> AopContributors { get; set; }
        public virtual DbSet<AopEvent> AopEvents { get; set; }
        public virtual DbSet<AopLifeStage> AopLifeStages { get; set; }
        public virtual DbSet<AopLog> AopLogs { get; set; }
        public virtual DbSet<AopRelationship> AopRelationships { get; set; }
        public virtual DbSet<AopSex> AopSexes { get; set; }
        public virtual DbSet<AopStressor> AopStressors { get; set; }
        public virtual DbSet<AopTaxon> AopTaxons { get; set; }
        public virtual DbSet<Aop> Aops { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<BiologicalAction> BiologicalActions { get; set; }
        public virtual DbSet<BiologicalObject> BiologicalObjects { get; set; }
        public virtual DbSet<BiologicalOrganization> BiologicalOrganizations { get; set; }
        public virtual DbSet<BiologicalProcess> BiologicalProcesses { get; set; }
        public virtual DbSet<CellTerm> CellTerms { get; set; }
        public virtual DbSet<ChemicalSynonym> ChemicalSynonyms { get; set; }
        public virtual DbSet<Chemical> Chemicals { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ConfidenceLevel> ConfidenceLevels { get; set; }
        public virtual DbSet<EventLifeStage> EventLifeStages { get; set; }
        public virtual DbSet<EventLog> EventLogs { get; set; }
        public virtual DbSet<EventSex> EventSexes { get; set; }
        public virtual DbSet<EventStressor> EventStressors { get; set; }
        public virtual DbSet<EventSubEvent> EventSubEvents { get; set; }
        public virtual DbSet<EventTaxon> EventTaxons { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Help> Helps { get; set; }
        public virtual DbSet<InfoLinkedMessage> InfoLinkedMessages { get; set; }
        public virtual DbSet<InfoLinkedPage> InfoLinkedPages { get; set; }
        public virtual DbSet<InfoMessage> InfoMessages { get; set; }
        public virtual DbSet<InfoPage> InfoPages { get; set; }
        public virtual DbSet<LifeStageTerm> LifeStageTerms { get; set; }
        public virtual DbSet<OecdStatus> OecdStatuses { get; set; }
        public virtual DbSet<OrganTerm> OrganTerms { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<RelationshipLifeStage> RelationshipLifeStages { get; set; }
        public virtual DbSet<RelationshipLog> RelationshipLogs { get; set; }
        public virtual DbSet<RelationshipSex> RelationshipSexes { get; set; }
        public virtual DbSet<RelationshipTaxon> RelationshipTaxons { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }
        public virtual DbSet<AopRole> Roles { get; set; }
        public virtual DbSet<SaaopStatus> SaaopStatuses { get; set; }
        public virtual DbSet<SchemaMigration> SchemaMigrations { get; set; }
        public virtual DbSet<SexTerm> SexTerms { get; set; }
        public virtual DbSet<Snapshot> Snapshots { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<StressorChemical> StressorChemicals { get; set; }
        public virtual DbSet<StressorLog> StressorLogs { get; set; }
        public virtual DbSet<Stressor> Stressors { get; set; }
        public virtual DbSet<SubEvent> SubEvents { get; set; }
        public virtual DbSet<TaxonTerm> TaxonTerms { get; set; }
        public virtual DbSet<TermRequest> TermRequests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Version> Versions { get; set; }
        public virtual DbSet<Watch> Watches { get; set; }

        public AopWikiDbContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(connectionString));

            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(this._connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AopContributor>(entity =>
            {
                entity.ToTable("aop_contributors");

                entity.HasIndex(e => e.AopId)
                    .HasName("index_aop_contributors_on_aop_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_aop_contributors_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Aop)
                    .WithMany(p => p.AopContributors)
                    .HasForeignKey(d => d.AopId)
                    .HasConstraintName("fk_rails_9c78537f5b");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AopContributors)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_4731edb8f6");
            });

            modelBuilder.Entity<AopEvent>(entity =>
            {
                entity.ToTable("aop_events");

                entity.HasIndex(e => e.AopId)
                    .HasName("index_aop_events_on_aop_id");

                entity.HasIndex(e => e.EssentialityId)
                    .HasName("fk_rails_ea7dbede77");

                entity.HasIndex(e => e.EventId)
                    .HasName("index_aop_events_on_event_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EssentialityId)
                    .HasColumnName("essentiality_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RowOrder)
                    .HasColumnName("row_order")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Essentiality)
                    .WithMany(p => p.AopEvents)
                    .HasForeignKey(d => d.EssentialityId)
                    .HasConstraintName("fk_rails_ea7dbede77");
            });

            modelBuilder.Entity<AopLifeStage>(entity =>
            {
                entity.ToTable("aop_life_stages");

                entity.HasIndex(e => e.AopId)
                    .HasName("index_aop_life_stages_on_aop_id");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("fk_rails_d0e660d7e9");

                entity.HasIndex(e => e.LifeStageTermId)
                    .HasName("index_aop_life_stages_on_life_stage_term_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LifeStageTermId)
                    .HasColumnName("life_stage_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Aop)
                    .WithMany(p => p.AopLifeStages)
                    .HasForeignKey(d => d.AopId)
                    .HasConstraintName("fk_rails_232977268a");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.AopLifeStages)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_d0e660d7e9");

                entity.HasOne(d => d.LifeStageTerm)
                    .WithMany(p => p.AopLifeStages)
                    .HasForeignKey(d => d.LifeStageTermId)
                    .HasConstraintName("fk_rails_563b584d72");
            });

            modelBuilder.Entity<AopLog>(entity =>
            {
                entity.ToTable("aop_logs");

                entity.HasIndex(e => e.AopId)
                    .HasName("index_aop_logs_on_aop_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_aop_logs_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Aop)
                    .WithMany(p => p.AopLogs)
                    .HasForeignKey(d => d.AopId)
                    .HasConstraintName("fk_rails_8dbe4c04ac");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AopLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_397b3a4aaa");
            });

            modelBuilder.Entity<AopRelationship>(entity =>
            {
                entity.ToTable("aop_relationships");

                entity.HasIndex(e => e.AopId)
                    .HasName("index_aop_relationships_on_aop_id");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("index_aop_relationships_on_evidence_id");

                entity.HasIndex(e => e.QuantitativeUnderstandingId)
                    .HasName("index_aop_relationships_on_quantitative_understanding_id");

                entity.HasIndex(e => e.RelationshipId)
                    .HasName("index_aop_relationships_on_relationship_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Directness)
                    .HasColumnName("directness")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuantitativeUnderstandingId)
                    .HasColumnName("quantitative_understanding_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RelationshipId)
                    .HasColumnName("relationship_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RowOrder)
                    .HasColumnName("row_order")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Aop)
                    .WithMany(p => p.AopRelationships)
                    .HasForeignKey(d => d.AopId)
                    .HasConstraintName("fk_rails_50e5f51323");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.AopRelationshipsEvidence)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_45fbbac924");

                entity.HasOne(d => d.QuantitativeUnderstanding)
                    .WithMany(p => p.AopRelationshipsQuantitativeUnderstanding)
                    .HasForeignKey(d => d.QuantitativeUnderstandingId)
                    .HasConstraintName("fk_rails_a93375efbc");

                entity.HasOne(d => d.Relationship)
                    .WithMany(p => p.AopRelationships)
                    .HasForeignKey(d => d.RelationshipId)
                    .HasConstraintName("fk_rails_331d7c334b");
            });

            modelBuilder.Entity<AopSex>(entity =>
            {
                entity.ToTable("aop_sexes");

                entity.HasIndex(e => e.AopId)
                    .HasName("index_aop_sexes_on_aop_id");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("fk_rails_a81b33bf59");

                entity.HasIndex(e => e.SexTermId)
                    .HasName("index_aop_sexes_on_sex_term_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SexTermId)
                    .HasColumnName("sex_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Aop)
                    .WithMany(p => p.AopSexes)
                    .HasForeignKey(d => d.AopId)
                    .HasConstraintName("fk_rails_fb70d75ff8");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.AopSexes)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_a81b33bf59");

                entity.HasOne(d => d.SexTerm)
                    .WithMany(p => p.AopSexes)
                    .HasForeignKey(d => d.SexTermId)
                    .HasConstraintName("fk_rails_294bdda722");
            });

            modelBuilder.Entity<AopStressor>(entity =>
            {
                entity.ToTable("aop_stressors");

                entity.HasIndex(e => e.AopId)
                    .HasName("index_aop_stressors_on_aop_id");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("index_aop_stressors_on_evidence_id");

                entity.HasIndex(e => e.StressorId)
                    .HasName("index_aop_stressors_on_stressor_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EvidenceText)
                    .HasColumnName("evidence_text")
                    .HasColumnType("text");

                entity.Property(e => e.StressorId)
                    .HasColumnName("stressor_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Aop)
                    .WithMany(p => p.AopStressors)
                    .HasForeignKey(d => d.AopId)
                    .HasConstraintName("fk_rails_323ab4836d");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.AopStressors)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_cfc58ba6e7");

                entity.HasOne(d => d.Stressor)
                    .WithMany(p => p.AopStressors)
                    .HasForeignKey(d => d.StressorId)
                    .HasConstraintName("fk_rails_44c5cd30a6");
            });

            modelBuilder.Entity<AopTaxon>(entity =>
            {
                entity.ToTable("aop_taxons");

                entity.HasIndex(e => e.AopId)
                    .HasName("index_aop_taxons_on_aop_id");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("index_aop_taxons_on_evidence_id");

                entity.HasIndex(e => e.TaxonTermId)
                    .HasName("index_aop_taxons_on_taxon_term_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaxonTermId)
                    .HasColumnName("taxon_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Aop)
                    .WithMany(p => p.AopTaxons)
                    .HasForeignKey(d => d.AopId)
                    .HasConstraintName("fk_rails_7739f1baeb");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.AopTaxons)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_f6f42badb0");

                entity.HasOne(d => d.TaxonTerm)
                    .WithMany(p => p.AopTaxons)
                    .HasForeignKey(d => d.TaxonTermId)
                    .HasConstraintName("fk_rails_e4694bbb26");
            });

            modelBuilder.Entity<Aop>(entity =>
            {
                entity.ToTable("aops");

                entity.HasIndex(e => e.CorrespondingAuthorId)
                    .HasName("index_aops_on_corresponding_author_id");

                entity.HasIndex(e => e.OecdStatusId)
                    .HasName("fk_rails_a74d4f10b8");

                entity.HasIndex(e => e.SaaopStatusId)
                    .HasName("index_aops_on_saaop_status_id");

                entity.HasIndex(e => e.StatusId)
                    .HasName("index_aops_on_status_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Abstract)
                    .HasColumnName("abstract")
                    .HasColumnType("text");

                entity.Property(e => e.ApplicabilityOfTheAop)
                    .HasColumnName("applicability_of_the_aop")
                    .HasColumnType("text");

                entity.Property(e => e.Authors)
                    .HasColumnName("authors")
                    .HasColumnType("text");

                entity.Property(e => e.Background)
                    .HasColumnName("background")
                    .HasColumnType("text");

                entity.Property(e => e.CorrespondingAuthorId)
                    .HasColumnName("corresponding_author_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.GraphicalRepresentationImageUid)
                    .HasColumnName("graphical_representation_image_uid")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.KeyEventEssentiality)
                    .HasColumnName("key_event_essentiality")
                    .HasColumnType("text");

                entity.Property(e => e.Legacy)
                    .HasColumnName("legacy")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.OecdProject)
                    .HasColumnName("oecd_project")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OecdStatusId)
                    .HasColumnName("oecd_status_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OptionalConsiderations)
                    .HasColumnName("optional_considerations")
                    .HasColumnType("text");

                entity.Property(e => e.OverallAssessment)
                    .HasColumnName("overall_assessment")
                    .HasColumnType("text");

                entity.Property(e => e.QuantitativeConsiderations)
                    .HasColumnName("quantitative_considerations")
                    .HasColumnType("text");

                entity.Property(e => e.References)
                    .HasColumnName("references")
                    .HasColumnType("text");

                entity.Property(e => e.SaaopStatusId)
                    .HasColumnName("saaop_status_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserDefinedAo)
                    .HasColumnName("user_defined_ao")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserDefinedMie)
                    .HasColumnName("user_defined_mie")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.WeightOfEvidenceSummary)
                    .HasColumnName("weight_of_evidence_summary")
                    .HasColumnType("text");

                entity.HasOne(d => d.CorrespondingAuthor)
                    .WithMany(p => p.Aops)
                    .HasForeignKey(d => d.CorrespondingAuthorId)
                    .HasConstraintName("fk_rails_effeba18b6");

                entity.HasOne(d => d.OecdStatus)
                    .WithMany(p => p.Aops)
                    .HasForeignKey(d => d.OecdStatusId)
                    .HasConstraintName("fk_rails_a74d4f10b8");

                entity.HasOne(d => d.SaaopStatus)
                    .WithMany(p => p.Aops)
                    .HasForeignKey(d => d.SaaopStatusId)
                    .HasConstraintName("fk_rails_ab1eade4c9");
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("assignments");

                entity.HasIndex(e => e.RoleId)
                    .HasName("index_assignments_on_role_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_assignments_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<BiologicalAction>(entity =>
            {
                entity.ToTable("biological_actions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SourceId)
                    .HasColumnName("source_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Term)
                    .HasColumnName("term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<BiologicalObject>(entity =>
            {
                entity.ToTable("biological_objects");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BiologicalOrganizationId)
                    .HasColumnName("biological_organization_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SourceId)
                    .HasColumnName("source_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Term)
                    .HasColumnName("term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<BiologicalOrganization>(entity =>
            {
                entity.ToTable("biological_organizations");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Term)
                    .IsRequired()
                    .HasColumnName("term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<BiologicalProcess>(entity =>
            {
                entity.ToTable("biological_processes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BiologicalOrganizationId)
                    .HasColumnName("biological_organization_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SourceId)
                    .HasColumnName("source_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Term)
                    .HasColumnName("term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CellTerm>(entity =>
            {
                entity.ToTable("cell_terms");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfficialName)
                    .HasColumnName("official_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SourceId)
                    .HasColumnName("source_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Synonym)
                    .HasColumnName("synonym")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ChemicalSynonym>(entity =>
            {
                entity.ToTable("chemical_synonyms");

                entity.HasIndex(e => e.ChemicalId)
                    .HasName("index_chemical_synonyms_on_chemical_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ChemicalId)
                    .HasColumnName("chemical_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Term)
                    .HasColumnName("term")
                    .HasColumnType("text");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Chemical)
                    .WithMany(p => p.ChemicalSynonyms)
                    .HasForeignKey(d => d.ChemicalId)
                    .HasConstraintName("fk_rails_ccd0c721d6");
            });

            modelBuilder.Entity<Chemical>(entity =>
            {
                entity.ToTable("chemicals");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Casrn)
                    .HasColumnName("casrn")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.DsstoxSubstanceId)
                    .HasColumnName("dsstox_substance_id")
                    .HasColumnType("text");

                entity.Property(e => e.IndigoInchiKey)
                    .HasColumnName("indigo_inchi_key")
                    .HasColumnType("text");

                entity.Property(e => e.JchemInchiKey)
                    .HasColumnName("jchem_inchi_key")
                    .HasColumnType("text");

                entity.Property(e => e.PreferredName)
                    .HasColumnName("preferred_name")
                    .HasColumnType("text");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments");

                entity.HasIndex(e => e.CommentableId)
                    .HasName("index_comments_on_commentable_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_comments_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AttachedFileUid)
                    .HasColumnName("attached_file_uid")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CommentableId)
                    .HasColumnName("commentable_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CommentableType)
                    .HasColumnName("commentable_type")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_03de2dc08c");
            });

            modelBuilder.Entity<ConfidenceLevel>(entity =>
            {
                entity.ToTable("confidence_levels");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Term)
                    .IsRequired()
                    .HasColumnName("term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<EventLifeStage>(entity =>
            {
                entity.ToTable("event_life_stages");

                entity.HasIndex(e => e.EventId)
                    .HasName("index_event_life_stages_on_event_id");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("index_event_life_stages_on_evidence_id");

                entity.HasIndex(e => e.LifeStageTermId)
                    .HasName("index_event_life_stages_on_life_stage_term_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LifeStageTermId)
                    .HasColumnName("life_stage_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventLifeStages)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("fk_rails_d90c2c4068");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.EventLifeStages)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_769257b1b6");

                entity.HasOne(d => d.LifeStageTerm)
                    .WithMany(p => p.EventLifeStages)
                    .HasForeignKey(d => d.LifeStageTermId)
                    .HasConstraintName("fk_rails_b4b1ffa27d");
            });

            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.ToTable("event_logs");

                entity.HasIndex(e => e.EventId)
                    .HasName("index_event_logs_on_event_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_event_logs_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventLogs)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("fk_rails_9957176d3e");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EventLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_61d856fcce");
            });

            modelBuilder.Entity<EventSex>(entity =>
            {
                entity.ToTable("event_sexes");

                entity.HasIndex(e => e.EventId)
                    .HasName("index_event_sexes_on_event_id");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("index_event_sexes_on_evidence_id");

                entity.HasIndex(e => e.SexTermId)
                    .HasName("index_event_sexes_on_sex_term_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SexTermId)
                    .HasColumnName("sex_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventSexes)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("fk_rails_bf099c0c8c");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.EventSexes)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_00a5ea5c54");

                entity.HasOne(d => d.SexTerm)
                    .WithMany(p => p.EventSexes)
                    .HasForeignKey(d => d.SexTermId)
                    .HasConstraintName("fk_rails_284fbbfde0");
            });

            modelBuilder.Entity<EventStressor>(entity =>
            {
                entity.ToTable("event_stressors");

                entity.HasIndex(e => e.EventId)
                    .HasName("index_event_stressors_on_event_id");

                entity.HasIndex(e => e.StressorId)
                    .HasName("index_event_stressors_on_stressor_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EvidenceText)
                    .HasColumnName("evidence_text")
                    .HasColumnType("text");

                entity.Property(e => e.StressorId)
                    .HasColumnName("stressor_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventStressors)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("fk_rails_bedaf5f667");

                entity.HasOne(d => d.Stressor)
                    .WithMany(p => p.EventStressors)
                    .HasForeignKey(d => d.StressorId)
                    .HasConstraintName("fk_rails_0a22f2f873");
            });

            modelBuilder.Entity<EventSubEvent>(entity =>
            {
                entity.ToTable("event_sub_events");

                entity.HasIndex(e => e.EventId)
                    .HasName("index_event_sub_events_on_event_id");

                entity.HasIndex(e => e.SubEventId)
                    .HasName("index_event_sub_events_on_sub_event_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SubEventId)
                    .HasColumnName("sub_event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventSubEvents)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("fk_rails_30cc170603");

                entity.HasOne(d => d.SubEvent)
                    .WithMany(p => p.EventSubEvents)
                    .HasForeignKey(d => d.SubEventId)
                    .HasConstraintName("fk_rails_9efce58d90");
            });

            modelBuilder.Entity<EventTaxon>(entity =>
            {
                entity.ToTable("event_taxons");

                entity.HasIndex(e => e.EventId)
                    .HasName("index_event_taxons_on_event_id");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("index_event_taxons_on_evidence_id");

                entity.HasIndex(e => e.TaxonTermId)
                    .HasName("index_event_taxons_on_taxon_term_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaxonTermId)
                    .HasColumnName("taxon_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventTaxons)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("fk_rails_bfe3a947a0");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.EventTaxons)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_a52dee79cb");

                entity.HasOne(d => d.TaxonTerm)
                    .WithMany(p => p.EventTaxons)
                    .HasForeignKey(d => d.TaxonTermId)
                    .HasConstraintName("fk_rails_e0ae6c9b75");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");

                entity.HasIndex(e => e.BiologicalOrganizationId)
                    .HasName("fk_rails_f6ca0cebd0");

                entity.HasIndex(e => e.CellTermId)
                    .HasName("index_events_on_cell_term_id");

                entity.HasIndex(e => e.OrganTermId)
                    .HasName("index_events_on_organ_term_id");

                entity.HasIndex(e => e.ShortName)
                    .HasName("index_events_on_short_name");

                entity.HasIndex(e => e.Title)
                    .HasName("index_events_on_title");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BiologicalOrganizationId)
                    .HasColumnName("biological_organization_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CellTermId)
                    .HasColumnName("cell_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Definition)
                    .HasColumnName("definition")
                    .HasColumnType("text");

                entity.Property(e => e.EvidenceForChemicalInitiation)
                    .HasColumnName("evidence_for_chemical_initiation")
                    .HasColumnType("text");

                entity.Property(e => e.ExamplesUsingAo)
                    .HasColumnName("examples_using_ao")
                    .HasColumnType("text");

                entity.Property(e => e.HowItWorks)
                    .HasColumnName("how_it_works")
                    .HasColumnType("text");

                entity.Property(e => e.MeasuredOrDetected)
                    .HasColumnName("measured_or_detected")
                    .HasColumnType("text");

                entity.Property(e => e.OrganTermId)
                    .HasColumnName("organ_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.References)
                    .HasColumnName("references")
                    .HasColumnType("text");

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SupportingTaxEvidence)
                    .HasColumnName("supporting_tax_evidence")
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.BiologicalOrganization)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.BiologicalOrganizationId)
                    .HasConstraintName("fk_rails_f6ca0cebd0");

                entity.HasOne(d => d.CellTerm)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CellTermId)
                    .HasConstraintName("fk_rails_f70d5f7025");

                entity.HasOne(d => d.OrganTerm)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.OrganTermId)
                    .HasConstraintName("fk_rails_64c6e36333");
            });

            modelBuilder.Entity<Help>(entity =>
            {
                entity.ToTable("helps");

                entity.HasIndex(e => e.InfoLinkedMessageId)
                    .HasName("index_helps_on_info_linked_message_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.InfoLinkedMessageId)
                    .HasColumnName("info_linked_message_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.ViewLabel)
                    .HasColumnName("view_label")
                    .HasColumnType("text");

                entity.HasOne(d => d.InfoLinkedMessage)
                    .WithMany(p => p.Helps)
                    .HasForeignKey(d => d.InfoLinkedMessageId)
                    .HasConstraintName("fk_rails_1b8c94a6d3");
            });

            modelBuilder.Entity<InfoLinkedMessage>(entity =>
            {
                entity.ToTable("info_linked_messages");

                entity.HasIndex(e => e.InfoLinkedPageId)
                    .HasName("index_info_linked_messages_on_info_linked_page_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.InfoLinkedPageId)
                    .HasColumnName("info_linked_page_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("text");

                entity.Property(e => e.MessageOrder)
                    .HasColumnName("message_order")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Section)
                    .HasColumnName("section")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Subsection)
                    .HasColumnName("subsection")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.InfoLinkedPage)
                    .WithMany(p => p.InfoLinkedMessages)
                    .HasForeignKey(d => d.InfoLinkedPageId)
                    .HasConstraintName("fk_rails_4e1bc9e6db");
            });

            modelBuilder.Entity<InfoLinkedPage>(entity =>
            {
                entity.ToTable("info_linked_pages");

                entity.HasIndex(e => e.InfoPageId)
                    .HasName("index_info_linked_pages_on_info_page_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.InfoPageId)
                    .HasColumnName("info_page_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.InfoPage)
                    .WithMany(p => p.InfoLinkedPages)
                    .HasForeignKey(d => d.InfoPageId)
                    .HasConstraintName("fk_rails_b2d60def4f");
            });

            modelBuilder.Entity<InfoMessage>(entity =>
            {
                entity.ToTable("info_messages");

                entity.HasIndex(e => e.InfoPageId)
                    .HasName("index_info_messages_on_info_page_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.InfoPageId)
                    .HasColumnName("info_page_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("text");

                entity.Property(e => e.MessageOrder)
                    .HasColumnName("message_order")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Section)
                    .HasColumnName("section")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Subsection)
                    .HasColumnName("subsection")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.InfoPage)
                    .WithMany(p => p.InfoMessages)
                    .HasForeignKey(d => d.InfoPageId)
                    .HasConstraintName("fk_rails_85f892d547");
            });

            modelBuilder.Entity<InfoPage>(entity =>
            {
                entity.ToTable("info_pages");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<LifeStageTerm>(entity =>
            {
                entity.ToTable("life_stage_terms");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Term)
                    .IsRequired()
                    .HasColumnName("term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<OecdStatus>(entity =>
            {
                entity.ToTable("oecd_statuses");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<OrganTerm>(entity =>
            {
                entity.ToTable("organ_terms");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfficialName)
                    .HasColumnName("official_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SourceId)
                    .HasColumnName("source_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Synonym)
                    .HasColumnName("synonym")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("pictures");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.ImageName)
                    .HasColumnName("image_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ImageTitle)
                    .HasColumnName("image_title")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ImageUid)
                    .HasColumnName("image_uid")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("profiles");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_profiles_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Affiliation)
                    .HasColumnName("affiliation")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TimeZone)
                    .HasColumnName("time_zone")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("Eastern Time (US & Canada)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<RelationshipLifeStage>(entity =>
            {
                entity.ToTable("relationship_life_stages");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("index_relationship_life_stages_on_evidence_id");

                entity.HasIndex(e => e.LifeStageTermId)
                    .HasName("index_relationship_life_stages_on_life_stage_term_id");

                entity.HasIndex(e => e.RelationshipId)
                    .HasName("index_relationship_life_stages_on_relationship_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LifeStageTermId)
                    .HasColumnName("life_stage_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RelationshipId)
                    .HasColumnName("relationship_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.RelationshipLifeStages)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_46a5edd25c");

                entity.HasOne(d => d.LifeStageTerm)
                    .WithMany(p => p.RelationshipLifeStages)
                    .HasForeignKey(d => d.LifeStageTermId)
                    .HasConstraintName("fk_rails_1a4b024905");

                entity.HasOne(d => d.Relationship)
                    .WithMany(p => p.RelationshipLifeStages)
                    .HasForeignKey(d => d.RelationshipId)
                    .HasConstraintName("fk_rails_a4b5c43758");
            });

            modelBuilder.Entity<RelationshipLog>(entity =>
            {
                entity.ToTable("relationship_logs");

                entity.HasIndex(e => e.RelationshipId)
                    .HasName("index_relationship_logs_on_relationship_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_relationship_logs_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.RelationshipId)
                    .HasColumnName("relationship_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Relationship)
                    .WithMany(p => p.RelationshipLogs)
                    .HasForeignKey(d => d.RelationshipId)
                    .HasConstraintName("fk_rails_cf73fdbd4a");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RelationshipLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_37a71b3613");
            });

            modelBuilder.Entity<RelationshipSex>(entity =>
            {
                entity.ToTable("relationship_sexes");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("index_relationship_sexes_on_evidence_id");

                entity.HasIndex(e => e.RelationshipId)
                    .HasName("index_relationship_sexes_on_relationship_id");

                entity.HasIndex(e => e.SexTermId)
                    .HasName("index_relationship_sexes_on_sex_term_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RelationshipId)
                    .HasColumnName("relationship_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SexTermId)
                    .HasColumnName("sex_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.RelationshipSexes)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_d703f38f9a");

                entity.HasOne(d => d.Relationship)
                    .WithMany(p => p.RelationshipSexes)
                    .HasForeignKey(d => d.RelationshipId)
                    .HasConstraintName("fk_rails_9fe420804e");

                entity.HasOne(d => d.SexTerm)
                    .WithMany(p => p.RelationshipSexes)
                    .HasForeignKey(d => d.SexTermId)
                    .HasConstraintName("fk_rails_aa1fb8f260");
            });

            modelBuilder.Entity<RelationshipTaxon>(entity =>
            {
                entity.ToTable("relationship_taxons");

                entity.HasIndex(e => e.EvidenceId)
                    .HasName("index_relationship_taxons_on_evidence_id");

                entity.HasIndex(e => e.RelationshipId)
                    .HasName("index_relationship_taxons_on_relationship_id");

                entity.HasIndex(e => e.TaxonTermId)
                    .HasName("index_relationship_taxons_on_taxon_term_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EvidenceId)
                    .HasColumnName("evidence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RelationshipId)
                    .HasColumnName("relationship_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaxonTermId)
                    .HasColumnName("taxon_term_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Evidence)
                    .WithMany(p => p.RelationshipTaxons)
                    .HasForeignKey(d => d.EvidenceId)
                    .HasConstraintName("fk_rails_53b2e4d118");
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.ToTable("relationships");

                entity.HasIndex(e => e.DownstreamEventId)
                    .HasName("index_relationships_on_downstream_event_id");

                entity.HasIndex(e => e.UpstreamEventId)
                    .HasName("index_relationships_on_upstream_event_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BiologicalPlausibility)
                    .HasColumnName("biological_plausibility")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.DownstreamEventId)
                    .HasColumnName("downstream_event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmpiricalSupport)
                    .HasColumnName("empirical_support")
                    .HasColumnType("text");

                entity.Property(e => e.HowItWorks)
                    .HasColumnName("how_it_works")
                    .HasColumnType("text");

                entity.Property(e => e.QuantitativeUnderstanding)
                    .HasColumnName("quantitative_understanding")
                    .HasColumnType("text");

                entity.Property(e => e.References)
                    .HasColumnName("references")
                    .HasColumnType("text");

                entity.Property(e => e.TaxonEvidence)
                    .HasColumnName("taxon_evidence")
                    .HasColumnType("text");

                entity.Property(e => e.Uncertainties)
                    .HasColumnName("uncertainties")
                    .HasColumnType("text");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpstreamEventId)
                    .HasColumnName("upstream_event_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WeightOfEvidence)
                    .HasColumnName("weight_of_evidence")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<AopRole>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SaaopStatus>(entity =>
            {
                entity.ToTable("saaop_statuses");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SchemaMigration>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("unique_schema_migrations");

                entity.ToTable("schema_migrations");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<SexTerm>(entity =>
            {
                entity.ToTable("sex_terms");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Term)
                    .IsRequired()
                    .HasColumnName("term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Snapshot>(entity =>
            {
                entity.ToTable("snapshots");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AopId)
                    .HasColumnName("aop_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Archive)
                    .HasColumnName("archive")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Creator)
                    .HasColumnName("creator")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HtmlFile)
                    .HasColumnName("html_file")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PdfFile)
                    .HasColumnName("pdf_file")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("statuses");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<StressorChemical>(entity =>
            {
                entity.ToTable("stressor_chemicals");

                entity.HasIndex(e => e.ChemicalId)
                    .HasName("index_stressor_chemicals_on_chemical_id");

                entity.HasIndex(e => e.StressorId)
                    .HasName("index_stressor_chemicals_on_stressor_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ChemicalId)
                    .HasColumnName("chemical_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.StressorId)
                    .HasColumnName("stressor_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserTerm)
                    .HasColumnName("user_term")
                    .HasColumnType("text");

                entity.HasOne(d => d.Chemical)
                    .WithMany(p => p.StressorChemicals)
                    .HasForeignKey(d => d.ChemicalId)
                    .HasConstraintName("fk_rails_d0226541cf");

                entity.HasOne(d => d.Stressor)
                    .WithMany(p => p.StressorChemicals)
                    .HasForeignKey(d => d.StressorId)
                    .HasConstraintName("fk_rails_5444ff339f");
            });

            modelBuilder.Entity<StressorLog>(entity =>
            {
                entity.ToTable("stressor_logs");

                entity.HasIndex(e => e.StressorId)
                    .HasName("index_stressor_logs_on_stressor_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_stressor_logs_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.StressorId)
                    .HasColumnName("stressor_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Stressor)
                    .WithMany(p => p.StressorLogs)
                    .HasForeignKey(d => d.StressorId)
                    .HasConstraintName("fk_rails_8cd8d2c989");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StressorLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_7f7efb772a");
            });

            modelBuilder.Entity<Stressor>(entity =>
            {
                entity.ToTable("stressors");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CharacterizationOfExposure)
                    .HasColumnName("characterization_of_exposure")
                    .HasColumnType("text");

                entity.Property(e => e.ChemicalDescription)
                    .HasColumnName("chemical_description")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.References)
                    .HasColumnName("references")
                    .HasColumnType("text");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SubEvent>(entity =>
            {
                entity.ToTable("sub_events");

                entity.HasIndex(e => e.BiologicalActionId)
                    .HasName("index_sub_events_on_biological_action_id");

                entity.HasIndex(e => e.BiologicalObjectId)
                    .HasName("index_sub_events_on_biological_object_id");

                entity.HasIndex(e => e.BiologicalProcessId)
                    .HasName("index_sub_events_on_biological_process_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BiologicalActionId)
                    .HasColumnName("biological_action_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BiologicalObjectId)
                    .HasColumnName("biological_object_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BiologicalProcessId)
                    .HasColumnName("biological_process_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.BiologicalAction)
                    .WithMany(p => p.SubEvents)
                    .HasForeignKey(d => d.BiologicalActionId)
                    .HasConstraintName("fk_rails_8c0a010623");

                entity.HasOne(d => d.BiologicalObject)
                    .WithMany(p => p.SubEvents)
                    .HasForeignKey(d => d.BiologicalObjectId)
                    .HasConstraintName("fk_rails_c3e79c4a74");

                entity.HasOne(d => d.BiologicalProcess)
                    .WithMany(p => p.SubEvents)
                    .HasForeignKey(d => d.BiologicalProcessId)
                    .HasConstraintName("fk_rails_97b06239d2");
            });

            modelBuilder.Entity<TaxonTerm>(entity =>
            {
                entity.ToTable("taxon_terms");

                //entity.HasIndex(e => e.Term)
                //    .HasName("index_taxon_terms_on_term");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.NcbiId)
                    .HasColumnName("ncbi_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ScientificTerm)
                    .HasColumnName("scientific_term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasColumnType("varchar(255)");

                //entity.Property(e => e.Term)
                //    .HasColumnName("term")
                //    .HasColumnType("varchar(255)");

                entity.Property(e => e.TermClass)
                    .HasColumnName("term_class")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TermRequest>(entity =>
            {
                entity.ToTable("term_requests");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_term_requests_on_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Justification)
                    .HasColumnName("justification")
                    .HasColumnType("text");

                entity.Property(e => e.ScientificTerm)
                    .HasColumnName("scientific_term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Term)
                    .HasColumnName("term")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TermType)
                    .HasColumnName("term_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TermRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_111d729290");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("index_users_on_email")
                    .IsUnique();

                entity.HasIndex(e => e.ResetPasswordToken)
                    .HasName("index_users_on_reset_password_token")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ConfirmationSentAt)
                    .HasColumnName("confirmation_sent_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.ConfirmationToken)
                    .HasColumnName("confirmation_token")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ConfirmedAt)
                    .HasColumnName("confirmed_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.CurrentSignInAt)
                    .HasColumnName("current_sign_in_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.CurrentSignInIp)
                    .HasColumnName("current_sign_in_ip")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EncryptedPassword)
                    .IsRequired()
                    .HasColumnName("encrypted_password")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LastSignInAt)
                    .HasColumnName("last_sign_in_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastSignInIp)
                    .HasColumnName("last_sign_in_ip")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.RememberCreatedAt)
                    .HasColumnName("remember_created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.ResetPasswordSentAt)
                    .HasColumnName("reset_password_sent_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.ResetPasswordToken)
                    .IsRequired()
                    .HasColumnName("reset_password_token")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SignInCount)
                    .HasColumnName("sign_in_count")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Subscribed)
                    .HasColumnName("subscribed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.UnconfirmedEmail)
                    .HasColumnName("unconfirmed_email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.ToTable("versions");

                entity.HasIndex(e => new { e.ItemType, e.ItemId })
                    .HasName("index_versions_on_item_type_and_item_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Event)
                    .IsRequired()
                    .HasColumnName("event")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ItemType)
                    .IsRequired()
                    .HasColumnName("item_type")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Object).HasColumnName("object");

                entity.Property(e => e.Whodunnit)
                    .HasColumnName("whodunnit")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Watch>(entity =>
            {
                entity.ToTable("watches");

                entity.HasIndex(e => e.UserId)
                    .HasName("index_watches_on_user_id");

                entity.HasIndex(e => new { e.WatchableType, e.WatchableId })
                    .HasName("index_watches_on_watchable_type_and_watchable_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WatchableId)
                    .HasColumnName("watchable_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WatchableType)
                    .HasColumnName("watchable_type")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Watches)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_rails_f9e5562894");
            });
        }
    }
}