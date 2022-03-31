using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DevTest.DbModel
{
    public partial class DevTestContext : DbContext
    {
        public DevTestContext()
        {
        }

        public DevTestContext(DbContextOptions<DevTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<ClaimFkcode> ClaimFkcodes { get; set; }
        public virtual DbSet<Fkcode> Fkcodes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonEmail> PersonEmails { get; set; }
        public virtual DbSet<PersonPhone> PersonPhones { get; set; }
        public virtual DbSet<PersonRelated> PersonRelateds { get; set; }
        public virtual DbSet<TestDataAllRawDatum> TestDataAllRawData { get; set; }
        public virtual DbSet<TestDataDevOrg01> TestDataDevOrg01s { get; set; }
        public virtual DbSet<TestDataDevOrg02> TestDataDevOrg02s { get; set; }
        public virtual DbSet<TestDataDevOrg03> TestDataDevOrg03s { get; set; }
        public virtual DbSet<TestDataDevOrg04> TestDataDevOrg04s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=DevTest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasIndex(e => e.ExternalClaimId, "NC-Claims_ExternalClaimId");

                entity.HasIndex(e => e.ExternalPersonId, "NC-Claims_ExternalPersonId");

                entity.HasIndex(e => e.OrganizationId, "NC-Claims_OrganizationId");

                entity.Property(e => e.ChargedAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DateofServiceEnd).HasColumnType("datetime");

                entity.Property(e => e.DateofServiceStart).HasColumnType("datetime");

                entity.Property(e => e.Decased).HasMaxLength(25);

                entity.Property(e => e.ExternalClaimId).HasMaxLength(50);

                entity.Property(e => e.LineCode).HasMaxLength(25);

                entity.Property(e => e.OfficeCode).HasMaxLength(25);

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.Property(e => e.Policy).HasMaxLength(25);

                entity.Property(e => e.Policy2).HasMaxLength(25);

                entity.Property(e => e.Policy3).HasMaxLength(25);

                entity.Property(e => e.Policy4).HasMaxLength(25);

                entity.Property(e => e.Policy5).HasMaxLength(25);

                entity.Property(e => e.PolicyName).HasMaxLength(250);

                entity.Property(e => e.ServiceAddress).HasMaxLength(250);

                entity.Property(e => e.ServiceCity).HasMaxLength(250);

                entity.Property(e => e.ServiceName).HasMaxLength(250);

                entity.Property(e => e.ServiceState).HasMaxLength(2);

                entity.Property(e => e.ServiceType).HasMaxLength(2);

                entity.Property(e => e.ServiceZip).HasMaxLength(10);

                entity.Property(e => e.ServiceZip2).HasMaxLength(10);

                entity.Property(e => e.WorkCode).HasMaxLength(25);

                entity.HasOne(d => d.ExternalPerson)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.ExternalPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__External__3B75D760");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Claims__Organiza__3A81B327");
            });

            modelBuilder.Entity<ClaimFkcode>(entity =>
            {
                entity.ToTable("ClaimFKCodes");

                entity.HasIndex(e => e.ExternalClaimId, "NC-ClaimFKCodes_ExternalClaimId");

                entity.HasIndex(e => e.ExternalPersonId, "NC-ClaimFKCodes_ExternalPersonId");

                entity.HasIndex(e => e.FkcodeId, "NC-ClaimFKCodes_FKCodeId");

                entity.HasIndex(e => e.OrganizationId, "NC-ClaimFKCodes_OrganizationId");

                entity.Property(e => e.FkcodeId).HasColumnName("FKCodeId");

                entity.HasOne(d => d.ExternalClaim)
                    .WithMany(p => p.ClaimFkcodes)
                    .HasForeignKey(d => d.ExternalClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClaimFKCo__Exter__38996AB5");

                entity.HasOne(d => d.ExternalPerson)
                    .WithMany(p => p.ClaimFkcodes)
                    .HasForeignKey(d => d.ExternalPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClaimFKCo__Exter__37A5467C");

                entity.HasOne(d => d.Fkcode)
                    .WithMany(p => p.ClaimFkcodes)
                    .HasForeignKey(d => d.FkcodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClaimFKCo__FKCod__398D8EEE");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.ClaimFkcodes)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClaimFKCo__Organ__36B12243");
            });

            modelBuilder.Entity<Fkcode>(entity =>
            {
                entity.ToTable("FKCodes");

                entity.HasIndex(e => e.Code, "NC-FKCodes_Code");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization");

                entity.HasIndex(e => e.OrganizationId, "NC-Organization_OrganizationId");

                entity.Property(e => e.OrganizationId)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.HasIndex(e => e.ExternalPersonId, "NC-Person_ExternalPersonId");

                entity.HasIndex(e => e.OrganizationId, "NC-Person_OrganizationId");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Address2).HasMaxLength(250);

                entity.Property(e => e.BillingAddress).HasMaxLength(250);

                entity.Property(e => e.BillingCity).HasMaxLength(250);

                entity.Property(e => e.BillingName).HasMaxLength(250);

                entity.Property(e => e.BillingPostalCode).HasMaxLength(10);

                entity.Property(e => e.BillingPostalCode2).HasMaxLength(10);

                entity.Property(e => e.BillingState).HasMaxLength(2);

                entity.Property(e => e.City).HasMaxLength(250);

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.ExternalPersonId).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.Gender)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.Middle).HasMaxLength(250);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.PostalCode2).HasMaxLength(10);

                entity.Property(e => e.SocialSecurityNumber).HasMaxLength(25);

                entity.Property(e => e.State).HasMaxLength(2);

                entity.Property(e => e.SubscriberId).HasMaxLength(50);

                entity.Property(e => e.SubscriberNumber).HasMaxLength(50);

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Person__Organiza__3C69FB99");
            });

            modelBuilder.Entity<PersonEmail>(entity =>
            {
                entity.ToTable("PersonEmail");

                entity.HasIndex(e => e.ExternalPersonId, "NC-PersonEmail_ExternalPersonId");

                entity.HasIndex(e => e.OrganizationId, "NC-PersonEmail_OrganizationId");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.HasOne(d => d.ExternalPerson)
                    .WithMany(p => p.PersonEmails)
                    .HasForeignKey(d => d.ExternalPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonEma__Exter__3E52440B");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.PersonEmails)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonEma__Organ__3D5E1FD2");
            });

            modelBuilder.Entity<PersonPhone>(entity =>
            {
                entity.ToTable("PersonPhone");

                entity.HasIndex(e => e.ExternalPersonId, "NC-PersonPhone_ExternalPersonId");

                entity.HasIndex(e => e.OrganizationId, "NC-PersonPhone_OrganizationId");

                entity.Property(e => e.Phone).HasMaxLength(250);

                entity.HasOne(d => d.ExternalPerson)
                    .WithMany(p => p.PersonPhones)
                    .HasForeignKey(d => d.ExternalPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonPho__Exter__403A8C7D");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.PersonPhones)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonPho__Organ__3F466844");
            });

            modelBuilder.Entity<PersonRelated>(entity =>
            {
                entity.ToTable("PersonRelated");

                entity.HasIndex(e => e.ExternalPersonId, "NC-PersonRelated_ExternalPersonId");

                entity.HasIndex(e => e.OrganizationId, "NC-PersonRelated_OrganizationId");

                entity.Property(e => e.RelatedDateofBirth).HasColumnType("datetime");

                entity.Property(e => e.RelatedFirstName).HasMaxLength(250);

                entity.Property(e => e.RelatedGender)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.RelatedLastName).HasMaxLength(250);

                entity.Property(e => e.RelatedMiddle).HasMaxLength(250);

                entity.HasOne(d => d.ExternalPerson)
                    .WithMany(p => p.PersonRelateds)
                    .HasForeignKey(d => d.ExternalPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonRel__Exter__4222D4EF");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.PersonRelateds)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PersonRel__Organ__412EB0B6");
            });

            modelBuilder.Entity<TestDataAllRawDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TestData_AllRawData");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Address2).HasMaxLength(250);

                entity.Property(e => e.BillingAddress).HasMaxLength(250);

                entity.Property(e => e.BillingCity).HasMaxLength(250);

                entity.Property(e => e.BillingName).HasMaxLength(250);

                entity.Property(e => e.BillingPostalCode).HasMaxLength(10);

                entity.Property(e => e.BillingPostalCode2).HasMaxLength(10);

                entity.Property(e => e.BillingState).HasMaxLength(2);

                entity.Property(e => e.ChargedAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.City).HasMaxLength(250);

                entity.Property(e => e.CustomField67).HasMaxLength(4000);

                entity.Property(e => e.CustomField68).HasMaxLength(4000);

                entity.Property(e => e.CustomField69).HasMaxLength(4000);

                entity.Property(e => e.CustomField70).HasMaxLength(4000);

                entity.Property(e => e.CustomField71).HasMaxLength(4000);

                entity.Property(e => e.CustomField72).HasMaxLength(4000);

                entity.Property(e => e.CustomField73).HasMaxLength(4000);

                entity.Property(e => e.CustomField74).HasMaxLength(4000);

                entity.Property(e => e.CustomField75).HasMaxLength(4000);

                entity.Property(e => e.CustomField76).HasMaxLength(4000);

                entity.Property(e => e.CustomField77).HasMaxLength(4000);

                entity.Property(e => e.CustomField78).HasMaxLength(4000);

                entity.Property(e => e.CustomField79).HasMaxLength(4000);

                entity.Property(e => e.CustomField80).HasMaxLength(4000);

                entity.Property(e => e.CustomField81).HasMaxLength(4000);

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.DateofServiceEnd).HasColumnType("datetime");

                entity.Property(e => e.DateofServiceStart).HasColumnType("datetime");

                entity.Property(e => e.Decased).HasMaxLength(25);

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.ExternalClaimId).HasMaxLength(50);

                entity.Property(e => e.ExternalPersonId).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.FkcodeThree)
                    .HasMaxLength(25)
                    .HasColumnName("FKCodeThree");

                entity.Property(e => e.FkcodeThree2)
                    .HasMaxLength(25)
                    .HasColumnName("FKCodeThree2");

                entity.Property(e => e.FkcodeThree3)
                    .HasMaxLength(25)
                    .HasColumnName("FKCodeThree3");

                entity.Property(e => e.FkcodeThree4)
                    .HasMaxLength(25)
                    .HasColumnName("FKCodeThree4");

                entity.Property(e => e.FkcodeThree5)
                    .HasMaxLength(25)
                    .HasColumnName("FKCodeThree5");

                entity.Property(e => e.FkcodeThreeVersion).HasColumnName("FKCodeThreeVersion");

                entity.Property(e => e.Fkone)
                    .HasMaxLength(25)
                    .HasColumnName("FKOne");

                entity.Property(e => e.FkoneType)
                    .HasMaxLength(2)
                    .HasColumnName("FKOneType");

                entity.Property(e => e.Fktwo)
                    .HasMaxLength(25)
                    .HasColumnName("FKTwo");

                entity.Property(e => e.Gender)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.LineCode).HasMaxLength(25);

                entity.Property(e => e.Middle).HasMaxLength(250);

                entity.Property(e => e.OfficeCode).HasMaxLength(25);

                entity.Property(e => e.OrganizationId).HasMaxLength(10);

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.Property(e => e.Policy).HasMaxLength(25);

                entity.Property(e => e.Policy2).HasMaxLength(25);

                entity.Property(e => e.Policy3).HasMaxLength(25);

                entity.Property(e => e.Policy4).HasMaxLength(25);

                entity.Property(e => e.Policy5).HasMaxLength(25);

                entity.Property(e => e.PolicyName).HasMaxLength(250);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.PostalCode2).HasMaxLength(10);

                entity.Property(e => e.RelatedDateofBirth).HasColumnType("datetime");

                entity.Property(e => e.RelatedFirstName).HasMaxLength(250);

                entity.Property(e => e.RelatedGender)
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.RelatedLastName).HasMaxLength(250);

                entity.Property(e => e.RelatedMiddle).HasMaxLength(250);

                entity.Property(e => e.ServiceAddress).HasMaxLength(250);

                entity.Property(e => e.ServiceCity).HasMaxLength(250);

                entity.Property(e => e.ServiceName).HasMaxLength(250);

                entity.Property(e => e.ServiceState).HasMaxLength(2);

                entity.Property(e => e.ServiceType).HasMaxLength(2);

                entity.Property(e => e.ServiceZip).HasMaxLength(10);

                entity.Property(e => e.ServiceZip2).HasMaxLength(10);

                entity.Property(e => e.SocialSecurityNumber).HasMaxLength(25);

                entity.Property(e => e.State).HasMaxLength(2);

                entity.Property(e => e.SubscriberId).HasMaxLength(50);

                entity.Property(e => e.SubscriberNumber).HasMaxLength(50);

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.Property(e => e.WorkCode).HasMaxLength(25);
            });

            modelBuilder.Entity<TestDataDevOrg01>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TestData_DevOrg_01");

                entity.Property(e => e.Column0)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 0");

                entity.Property(e => e.Column1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 1");

                entity.Property(e => e.Column10)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 10");

                entity.Property(e => e.Column11)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 11");

                entity.Property(e => e.Column12)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 12");

                entity.Property(e => e.Column13)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 13");

                entity.Property(e => e.Column14)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 14");

                entity.Property(e => e.Column15)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 15");

                entity.Property(e => e.Column16)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 16");

                entity.Property(e => e.Column17)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 17");

                entity.Property(e => e.Column18)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 18");

                entity.Property(e => e.Column19)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 19");

                entity.Property(e => e.Column2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 2");

                entity.Property(e => e.Column20)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 20");

                entity.Property(e => e.Column21)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 21");

                entity.Property(e => e.Column22)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 22");

                entity.Property(e => e.Column23)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 23");

                entity.Property(e => e.Column24)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 24");

                entity.Property(e => e.Column25)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 25");

                entity.Property(e => e.Column26)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 26");

                entity.Property(e => e.Column27)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 27");

                entity.Property(e => e.Column28)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 28");

                entity.Property(e => e.Column29)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 29");

                entity.Property(e => e.Column3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 3");

                entity.Property(e => e.Column30)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 30");

                entity.Property(e => e.Column31)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 31");

                entity.Property(e => e.Column32)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 32");

                entity.Property(e => e.Column33)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 33");

                entity.Property(e => e.Column34)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 34");

                entity.Property(e => e.Column35)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 35");

                entity.Property(e => e.Column36)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 36");

                entity.Property(e => e.Column37)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 37");

                entity.Property(e => e.Column38)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 38");

                entity.Property(e => e.Column39)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 39");

                entity.Property(e => e.Column4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 4");

                entity.Property(e => e.Column40)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 40");

                entity.Property(e => e.Column41)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 41");

                entity.Property(e => e.Column42)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 42");

                entity.Property(e => e.Column43)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 43");

                entity.Property(e => e.Column44)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 44");

                entity.Property(e => e.Column45)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 45");

                entity.Property(e => e.Column46)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 46");

                entity.Property(e => e.Column47)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 47");

                entity.Property(e => e.Column48)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 48");

                entity.Property(e => e.Column49)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 49");

                entity.Property(e => e.Column5)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 5");

                entity.Property(e => e.Column50)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 50");

                entity.Property(e => e.Column51)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 51");

                entity.Property(e => e.Column52)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 52");

                entity.Property(e => e.Column53)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 53");

                entity.Property(e => e.Column54)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 54");

                entity.Property(e => e.Column55)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 55");

                entity.Property(e => e.Column56)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 56");

                entity.Property(e => e.Column57)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 57");

                entity.Property(e => e.Column58)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 58");

                entity.Property(e => e.Column59)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 59");

                entity.Property(e => e.Column6)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 6");

                entity.Property(e => e.Column60)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 60");

                entity.Property(e => e.Column61)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 61");

                entity.Property(e => e.Column62)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 62");

                entity.Property(e => e.Column63)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 63");

                entity.Property(e => e.Column64)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 64");

                entity.Property(e => e.Column65)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 65");

                entity.Property(e => e.Column66)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 66");

                entity.Property(e => e.Column67)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 67");

                entity.Property(e => e.Column68)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 68");

                entity.Property(e => e.Column69)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 69");

                entity.Property(e => e.Column7)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 7");

                entity.Property(e => e.Column70)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 70");

                entity.Property(e => e.Column71)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 71");

                entity.Property(e => e.Column72)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 72");

                entity.Property(e => e.Column73)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 73");

                entity.Property(e => e.Column74)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 74");

                entity.Property(e => e.Column75)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 75");

                entity.Property(e => e.Column76)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 76");

                entity.Property(e => e.Column77)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 77");

                entity.Property(e => e.Column78)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 78");

                entity.Property(e => e.Column79)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 79");

                entity.Property(e => e.Column8)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 8");

                entity.Property(e => e.Column80)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 80");

                entity.Property(e => e.Column81)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 81");

                entity.Property(e => e.Column9)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 9");
            });

            modelBuilder.Entity<TestDataDevOrg02>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TestData_DevOrg_02");

                entity.Property(e => e.Column0)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 0");

                entity.Property(e => e.Column1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 1");

                entity.Property(e => e.Column10)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 10");

                entity.Property(e => e.Column11)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 11");

                entity.Property(e => e.Column12)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 12");

                entity.Property(e => e.Column13)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 13");

                entity.Property(e => e.Column14)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 14");

                entity.Property(e => e.Column15)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 15");

                entity.Property(e => e.Column16)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 16");

                entity.Property(e => e.Column17)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 17");

                entity.Property(e => e.Column18)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 18");

                entity.Property(e => e.Column19)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 19");

                entity.Property(e => e.Column2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 2");

                entity.Property(e => e.Column20)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 20");

                entity.Property(e => e.Column21)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 21");

                entity.Property(e => e.Column22)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 22");

                entity.Property(e => e.Column23)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 23");

                entity.Property(e => e.Column24)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 24");

                entity.Property(e => e.Column25)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 25");

                entity.Property(e => e.Column26)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 26");

                entity.Property(e => e.Column27)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 27");

                entity.Property(e => e.Column28)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 28");

                entity.Property(e => e.Column29)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 29");

                entity.Property(e => e.Column3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 3");

                entity.Property(e => e.Column30)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 30");

                entity.Property(e => e.Column31)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 31");

                entity.Property(e => e.Column32)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 32");

                entity.Property(e => e.Column33)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 33");

                entity.Property(e => e.Column34)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 34");

                entity.Property(e => e.Column35)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 35");

                entity.Property(e => e.Column36)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 36");

                entity.Property(e => e.Column37)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 37");

                entity.Property(e => e.Column38)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 38");

                entity.Property(e => e.Column39)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 39");

                entity.Property(e => e.Column4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 4");

                entity.Property(e => e.Column40)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 40");

                entity.Property(e => e.Column41)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 41");

                entity.Property(e => e.Column42)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 42");

                entity.Property(e => e.Column43)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 43");

                entity.Property(e => e.Column44)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 44");

                entity.Property(e => e.Column45)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 45");

                entity.Property(e => e.Column46)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 46");

                entity.Property(e => e.Column47)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 47");

                entity.Property(e => e.Column48)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 48");

                entity.Property(e => e.Column49)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 49");

                entity.Property(e => e.Column5)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 5");

                entity.Property(e => e.Column50)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 50");

                entity.Property(e => e.Column51)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 51");

                entity.Property(e => e.Column52)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 52");

                entity.Property(e => e.Column53)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 53");

                entity.Property(e => e.Column54)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 54");

                entity.Property(e => e.Column55)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 55");

                entity.Property(e => e.Column56)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 56");

                entity.Property(e => e.Column57)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 57");

                entity.Property(e => e.Column58)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 58");

                entity.Property(e => e.Column59)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 59");

                entity.Property(e => e.Column6)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 6");

                entity.Property(e => e.Column60)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 60");

                entity.Property(e => e.Column61)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 61");

                entity.Property(e => e.Column62)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 62");

                entity.Property(e => e.Column63)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 63");

                entity.Property(e => e.Column64)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 64");

                entity.Property(e => e.Column65)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 65");

                entity.Property(e => e.Column66)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 66");

                entity.Property(e => e.Column67)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 67");

                entity.Property(e => e.Column68)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 68");

                entity.Property(e => e.Column69)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 69");

                entity.Property(e => e.Column7)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 7");

                entity.Property(e => e.Column70)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 70");

                entity.Property(e => e.Column71)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 71");

                entity.Property(e => e.Column72)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 72");

                entity.Property(e => e.Column73)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 73");

                entity.Property(e => e.Column74)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 74");

                entity.Property(e => e.Column75)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 75");

                entity.Property(e => e.Column76)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 76");

                entity.Property(e => e.Column77)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 77");

                entity.Property(e => e.Column78)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 78");

                entity.Property(e => e.Column79)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 79");

                entity.Property(e => e.Column8)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 8");

                entity.Property(e => e.Column80)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 80");

                entity.Property(e => e.Column81)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 81");

                entity.Property(e => e.Column9)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 9");
            });

            modelBuilder.Entity<TestDataDevOrg03>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TestData_DevOrg_03");

                entity.Property(e => e.Column0)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 0");

                entity.Property(e => e.Column1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 1");

                entity.Property(e => e.Column10)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 10");

                entity.Property(e => e.Column11)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 11");

                entity.Property(e => e.Column12)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 12");

                entity.Property(e => e.Column13)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 13");

                entity.Property(e => e.Column14)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 14");

                entity.Property(e => e.Column15)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 15");

                entity.Property(e => e.Column16)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 16");

                entity.Property(e => e.Column17)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 17");

                entity.Property(e => e.Column18)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 18");

                entity.Property(e => e.Column19)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 19");

                entity.Property(e => e.Column2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 2");

                entity.Property(e => e.Column20)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 20");

                entity.Property(e => e.Column21)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 21");

                entity.Property(e => e.Column22)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 22");

                entity.Property(e => e.Column23)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 23");

                entity.Property(e => e.Column24)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 24");

                entity.Property(e => e.Column25)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 25");

                entity.Property(e => e.Column26)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 26");

                entity.Property(e => e.Column27)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 27");

                entity.Property(e => e.Column28)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 28");

                entity.Property(e => e.Column29)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 29");

                entity.Property(e => e.Column3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 3");

                entity.Property(e => e.Column30)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 30");

                entity.Property(e => e.Column31)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 31");

                entity.Property(e => e.Column32)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 32");

                entity.Property(e => e.Column33)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 33");

                entity.Property(e => e.Column34)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 34");

                entity.Property(e => e.Column35)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 35");

                entity.Property(e => e.Column36)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 36");

                entity.Property(e => e.Column37)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 37");

                entity.Property(e => e.Column38)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 38");

                entity.Property(e => e.Column39)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 39");

                entity.Property(e => e.Column4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 4");

                entity.Property(e => e.Column40)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 40");

                entity.Property(e => e.Column41)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 41");

                entity.Property(e => e.Column42)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 42");

                entity.Property(e => e.Column43)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 43");

                entity.Property(e => e.Column44)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 44");

                entity.Property(e => e.Column45)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 45");

                entity.Property(e => e.Column46)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 46");

                entity.Property(e => e.Column47)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 47");

                entity.Property(e => e.Column48)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 48");

                entity.Property(e => e.Column49)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 49");

                entity.Property(e => e.Column5)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 5");

                entity.Property(e => e.Column50)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 50");

                entity.Property(e => e.Column51)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 51");

                entity.Property(e => e.Column52)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 52");

                entity.Property(e => e.Column53)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 53");

                entity.Property(e => e.Column54)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 54");

                entity.Property(e => e.Column55)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 55");

                entity.Property(e => e.Column56)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 56");

                entity.Property(e => e.Column57)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 57");

                entity.Property(e => e.Column58)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 58");

                entity.Property(e => e.Column59)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 59");

                entity.Property(e => e.Column6)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 6");

                entity.Property(e => e.Column60)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 60");

                entity.Property(e => e.Column61)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 61");

                entity.Property(e => e.Column62)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 62");

                entity.Property(e => e.Column63)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 63");

                entity.Property(e => e.Column64)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 64");

                entity.Property(e => e.Column65)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 65");

                entity.Property(e => e.Column66)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 66");

                entity.Property(e => e.Column67)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 67");

                entity.Property(e => e.Column68)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 68");

                entity.Property(e => e.Column69)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 69");

                entity.Property(e => e.Column7)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 7");

                entity.Property(e => e.Column70)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 70");

                entity.Property(e => e.Column71)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 71");

                entity.Property(e => e.Column72)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 72");

                entity.Property(e => e.Column73)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 73");

                entity.Property(e => e.Column74)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 74");

                entity.Property(e => e.Column75)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 75");

                entity.Property(e => e.Column76)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 76");

                entity.Property(e => e.Column77)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 77");

                entity.Property(e => e.Column78)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 78");

                entity.Property(e => e.Column79)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 79");

                entity.Property(e => e.Column8)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 8");

                entity.Property(e => e.Column80)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 80");

                entity.Property(e => e.Column81)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 81");

                entity.Property(e => e.Column9)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 9");
            });

            modelBuilder.Entity<TestDataDevOrg04>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TestData_DevOrg_04");

                entity.Property(e => e.Column0)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 0");

                entity.Property(e => e.Column1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 1");

                entity.Property(e => e.Column10)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 10");

                entity.Property(e => e.Column11)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 11");

                entity.Property(e => e.Column12)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 12");

                entity.Property(e => e.Column13)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 13");

                entity.Property(e => e.Column14)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 14");

                entity.Property(e => e.Column15)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 15");

                entity.Property(e => e.Column16)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 16");

                entity.Property(e => e.Column17)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 17");

                entity.Property(e => e.Column18)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 18");

                entity.Property(e => e.Column19)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 19");

                entity.Property(e => e.Column2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 2");

                entity.Property(e => e.Column20)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 20");

                entity.Property(e => e.Column21)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 21");

                entity.Property(e => e.Column22)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 22");

                entity.Property(e => e.Column23)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 23");

                entity.Property(e => e.Column24)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 24");

                entity.Property(e => e.Column25)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 25");

                entity.Property(e => e.Column26)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 26");

                entity.Property(e => e.Column27)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 27");

                entity.Property(e => e.Column28)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 28");

                entity.Property(e => e.Column29)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 29");

                entity.Property(e => e.Column3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 3");

                entity.Property(e => e.Column30)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 30");

                entity.Property(e => e.Column31)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 31");

                entity.Property(e => e.Column32)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 32");

                entity.Property(e => e.Column33)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 33");

                entity.Property(e => e.Column34)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 34");

                entity.Property(e => e.Column35)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 35");

                entity.Property(e => e.Column36)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 36");

                entity.Property(e => e.Column37)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 37");

                entity.Property(e => e.Column38)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 38");

                entity.Property(e => e.Column39)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 39");

                entity.Property(e => e.Column4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 4");

                entity.Property(e => e.Column40)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 40");

                entity.Property(e => e.Column41)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 41");

                entity.Property(e => e.Column42)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 42");

                entity.Property(e => e.Column43)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 43");

                entity.Property(e => e.Column44)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 44");

                entity.Property(e => e.Column45)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 45");

                entity.Property(e => e.Column46)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 46");

                entity.Property(e => e.Column47)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 47");

                entity.Property(e => e.Column48)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 48");

                entity.Property(e => e.Column49)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 49");

                entity.Property(e => e.Column5)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 5");

                entity.Property(e => e.Column50)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 50");

                entity.Property(e => e.Column51)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 51");

                entity.Property(e => e.Column52)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 52");

                entity.Property(e => e.Column53)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 53");

                entity.Property(e => e.Column54)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 54");

                entity.Property(e => e.Column55)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 55");

                entity.Property(e => e.Column56)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 56");

                entity.Property(e => e.Column57)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 57");

                entity.Property(e => e.Column58)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 58");

                entity.Property(e => e.Column59)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 59");

                entity.Property(e => e.Column6)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 6");

                entity.Property(e => e.Column60)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 60");

                entity.Property(e => e.Column61)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 61");

                entity.Property(e => e.Column62)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 62");

                entity.Property(e => e.Column63)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 63");

                entity.Property(e => e.Column64)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 64");

                entity.Property(e => e.Column65)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 65");

                entity.Property(e => e.Column66)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 66");

                entity.Property(e => e.Column67)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 67");

                entity.Property(e => e.Column68)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 68");

                entity.Property(e => e.Column69)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 69");

                entity.Property(e => e.Column7)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 7");

                entity.Property(e => e.Column70)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 70");

                entity.Property(e => e.Column71)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 71");

                entity.Property(e => e.Column72)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 72");

                entity.Property(e => e.Column73)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 73");

                entity.Property(e => e.Column74)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 74");

                entity.Property(e => e.Column75)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 75");

                entity.Property(e => e.Column76)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 76");

                entity.Property(e => e.Column77)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 77");

                entity.Property(e => e.Column78)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 78");

                entity.Property(e => e.Column79)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 79");

                entity.Property(e => e.Column8)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 8");

                entity.Property(e => e.Column80)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 80");

                entity.Property(e => e.Column81)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("Column 81");

                entity.Property(e => e.Column9)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Column 9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
