using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Shared.Models;

#nullable disable

namespace TestCoreHosted.Server.Data
{
    public partial class CartographieContext : DbContext
    {
        public CartographieContext()
        {
        }

        public CartographieContext(DbContextOptions<CartographieContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppCout> AppCouts { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<DataBase> Databases { get; set; }
        public virtual DbSet<Db> Dbs { get; set; }
        public virtual DbSet<Documentation> Documentations { get; set; }
        public virtual DbSet<Domaine> Domaines { get; set; }
        public virtual DbSet<Environnement> Environnements { get; set; }
        public virtual DbSet<Metier> Metiers { get; set; }
        public virtual DbSet<OSystem> OSystems { get; set; }
        public virtual DbSet<Serveur> Serveurs { get; set; }
        public virtual DbSet<TypeO> TypeOs { get; set; }
        public virtual DbSet<VersionDb> VersionDbs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-2FQ5EBT;Database=Cartographie;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AppCout>(entity =>
            {
                entity.ToTable("AppCout");

                entity.Property(e => e.Cout).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IdApp).HasColumnName("Id_App");

                entity.Property(e => e.Licence)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NbreLicence).HasColumnName("Nbre_Licence");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AppCouts)
                    .HasForeignKey(d => d.IdApp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppCout_APPLICATIONS");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.AppId);

                entity.ToTable("APPLICATIONS");

                entity.Property(e => e.AppId).HasColumnName("APP_Id");

                entity.Property(e => e.Architecture)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Ba).IsUnicode(false);

                entity.Property(e => e.Bm)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Conformite)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ContactUser).HasColumnName("ContactUSer");

                entity.Property(e => e.Cout)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CoutPrice)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("COUT_PRICE");

                entity.Property(e => e.DemoDate)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Dependences).HasColumnType("text");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Documentation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DomaineId).HasColumnName("Domaine_Id");

                entity.Property(e => e.Editeur).IsUnicode(false);

                entity.Property(e => e.Escalade).HasColumnType("text");

                entity.Property(e => e.Etat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MigDate).HasColumnType("datetime");

                entity.Property(e => e.OnlineDate)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Perimetre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServeurId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Serveur_Id");

                entity.Property(e => e.SiteApp)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Site_app");

                entity.Property(e => e.Titre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.VersionApp)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Domaine)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.DomaineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPLICATIONS_DOMAINE");

                entity.HasOne(d => d.Metier)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.MetierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPLICATIONS_METIERS");
            });

            modelBuilder.Entity<DataBase>(entity =>
            {
                entity.HasKey(e => e.DataId);

                entity.ToTable("DATA_BASE");

                entity.Property(e => e.DataId).HasColumnName("Data_Id");

                entity.Property(e => e.AppId).HasColumnName("App_Id");

                entity.Property(e => e.CoutPrice)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("COUT_PRICE");

                entity.Property(e => e.DTitre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("D_Titre");

                entity.Property(e => e.Etat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MigDate).HasColumnType("datetime");

                entity.Property(e => e.ServeurId).HasColumnName("Serveur_Id");

                entity.Property(e => e.VersionDbId).HasColumnName("VersionDb_Id");

                entity.HasOne(d => d.Env)
                    .WithMany(p => p.Databases)
                    .HasForeignKey(d => d.EnvId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DATA_BASE_ENVIRONNEMENT");

                entity.HasOne(d => d.Serveur)
                    .WithMany(p => p.Databases)
                    .HasForeignKey(d => d.ServeurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DATA_BASE_SERVEUR");

                entity.HasOne(d => d.VersionDb)
                    .WithMany(p => p.Databases)
                    .HasForeignKey(d => d.VersionDbId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DATA_BASE_VERSION_DB");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.DataBases)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DATA_BASE_APPLICATIONS");
            });

            modelBuilder.Entity<Db>(entity =>
            {
                entity.ToTable("Db");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Documentation>(entity =>
            {
                entity.ToTable("DOCUMENTATION");

                entity.Property(e => e.AppId).HasColumnName("App_Id");

                entity.Property(e => e.ChargerLe).HasColumnType("datetime");

                entity.Property(e => e.NameFile)
                    .IsRequired();

                entity.Property(e => e.Titre)
                    .IsRequired();

                entity.HasOne(d => d.App)
                    .WithMany(p => p.Documentations)
                    .HasForeignKey(d => d.AppId)
                    .HasConstraintName("FK_DOCUMENTATION_APPLICATIONS");
            });

            modelBuilder.Entity<Domaine>(entity =>
            {
                entity.ToTable("DOMAINE");

                entity.Property(e => e.DTitle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("D_title");
            });

            modelBuilder.Entity<Environnement>(entity =>
            {
                entity.HasKey(e => e.EnvId);

                entity.ToTable("ENVIRONNEMENT");

                entity.Property(e => e.EnvType)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Env_Type");
            });

            modelBuilder.Entity<Metier>(entity =>
            {
                entity.ToTable("METIERS");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OSystem>(entity =>
            {
                entity.HasKey(e => e.OId);

                entity.ToTable("O_SYSTEMS");

                entity.Property(e => e.OId).HasColumnName("O_Id");

                entity.Property(e => e.VersionO)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Version_o");

                entity.HasOne(d => d.TypeO)
                    .WithMany(p => p.OSystems)
                    .HasForeignKey(d => d.Title)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_O_SYSTEMS_TYPE_OS");
            });

            modelBuilder.Entity<Serveur>(entity =>
            {
                entity.ToTable("SERVEUR");

                entity.Property(e => e.ServeurId).HasColumnName("Serveur_Id");

                entity.Property(e => e.Categorie)
                    .HasMaxLength(155)
                    .IsUnicode(false);

                entity.Property(e => e.CoutPrice)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("COUT_PRICE");

                entity.Property(e => e.EnvId).HasColumnName("Env_Id");

                entity.Property(e => e.Etat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MigDate).HasColumnType("datetime");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Noyau)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observation).HasColumnType("text");

                entity.Property(e => e.OsId).HasColumnName("OS_Id");

                entity.Property(e => e.Rack)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Salle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeServeur)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Type_Serveur");

                entity.Property(e => e.VersionOsId).HasColumnName("VersionOs_Id");

                entity.HasOne(d => d.Env)
                    .WithMany(p => p.Serveurs)
                    .HasForeignKey(d => d.EnvId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SERVEUR_ENVIRONNEMENT");

                entity.HasOne(d => d.Os)
                    .WithMany(p => p.Serveurs)
                    .HasForeignKey(d => d.OsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SERVEUR_TYPE_OS");

                entity.HasOne(d => d.VersionOs)
                    .WithMany(p => p.Serveurs)
                    .HasForeignKey(d => d.VersionOsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SERVEUR_O_SYSTEMS");
            });

            modelBuilder.Entity<TypeO>(entity =>
            {
                entity.HasKey(e => e.TypeOsId);

                entity.ToTable("TYPE_OS");

                entity.Property(e => e.TypeOsId).HasColumnName("TypeOs_Id");

                entity.Property(e => e.TitreOs)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Titre_os");
            });

            modelBuilder.Entity<VersionDb>(entity =>
            {
                entity.HasKey(e => e.VdbId);

                entity.ToTable("VERSION_DB");

                entity.Property(e => e.VdbId).HasColumnName("VDb_Id");

                entity.Property(e => e.DbId).HasColumnName("Db_Id");

                entity.Property(e => e.Noyau)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Titre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Db)
                    .WithMany(p => p.VersionDbs)
                    .HasForeignKey(d => d.DbId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VERSION_DB_Db");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
