using LitobzorImport.Domain.Aggregates;
using LitobzorImport.Domain.References;
using LitobzorImport.Domain.Relations;
using Microsoft.EntityFrameworkCore;

namespace LitobzorImport.Domain
{
    public class LitobzorDataContext : DbContext
    {
        #region Initialization

        public LitobzorDataContext(DbContextOptions<LitobzorDataContext> options) : base(options)
        {
        }

        public LitobzorDataContext()
        {
        }

        #endregion

        #region Mapping

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LowLevelTerm>(entity =>
            {
                entity.HasKey(x => x.LltCode);

                entity.HasOne(x => x.PreferredTerm)
                    .WithMany(x => x.LowLevelTerms)
                    .HasForeignKey(x => x.PtCode);
            });

            modelBuilder.Entity<PreferredTerm>(entity =>
            {
                entity.HasKey(x => x.PtCode);

                entity.HasOne(x => x.MainSystemOrganClass)
                    .WithMany(x => x.PreferredTerms)
                    .HasForeignKey(x => x.PtSocCode);
            });

            modelBuilder.Entity<HighLevelTerm>(entity =>
            {
                entity.HasKey(x => x.HltCode);
            });

            modelBuilder.Entity<HighLevelGroupTerm>(entity =>
            {
                entity.HasKey(x => x.HlgtCode);
            });

            modelBuilder.Entity<SystemOrganClass>(entity =>
            {
                entity.HasKey(x => x.SocCode);
            });

            modelBuilder.Entity<HltPtRelation>(entity =>
            {
                entity.HasKey(x => new { x.HltCode, x.PtCode });

                entity.HasOne(x => x.HighLevelTerm)
                    .WithMany(x => x.HltPtRelations)
                    .HasForeignKey(x => x.HltCode);

                entity.HasOne(x => x.PreferredTerm)
                    .WithMany(x => x.HltPtRelations)
                    .HasForeignKey(x => x.PtCode);
            });

            modelBuilder.Entity<HlgtHltRelation>(entity =>
            {
                entity.HasKey(x => new { x.HlgtCode, x.HltCode });
                entity.HasOne(x => x.HighLevelGroupTerm)
                    .WithMany(x => x.HlgtHltRelations)
                    .HasForeignKey(x => x.HlgtCode);
                entity.HasOne(x => x.HighLevelTerm)
                    .WithMany(x => x.HlgtHltRelations)
                    .HasForeignKey(x => x.HltCode);
            });

            modelBuilder.Entity<SocHlgtRelation>(entity =>
            {
                entity.HasKey(x => new { x.SocCode, x.HlgtCode });

                entity.HasOne(x => x.HighLevelGroupTerm)
                    .WithMany(x => x.SocHlgtRelations)
                    .HasForeignKey(x => x.HlgtCode);

                entity.HasOne(x => x.SystemOrganClass)
                    .WithMany(x => x.SocHlgtRelations)
                    .HasForeignKey(x => x.SocCode);
            });

            modelBuilder.Entity<MedDraHierarchy>(entity =>
            {
                entity.HasKey(x => new { x.PtCode, x.HltCode, x.HlgtCode, x.SocCode, x.PtSocCode });
            });
        }

        #endregion
    }
}
