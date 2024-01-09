using ParamTech.Domain;

namespace ParamTech.Persistence.CodeFirst.EntityConfigurations;
public class UsersConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> entity)
    {
        entity.ToTable("USERS").HasKey(e => e.Userid);
        entity.Property(e => e.Userid).HasColumnName("USERID").IsRequired();
        entity.Property(e => e.Createdate).HasColumnType("datetime").HasColumnName("CREATEDATE").IsRequired();
        entity.Property(e => e.Deletedate).HasColumnType("datetime").HasColumnName("DELETEDATE");
        entity.Property(e => e.Isok).HasColumnName("ISOK").IsRequired();
        entity.Property(e => e.Updatedate).HasColumnType("datetime").HasColumnName("UPDATEDATE");
        entity.Property(e => e.Useremailhash).HasMaxLength(500).IsUnicode(false).HasColumnName("USEREMAILHASH").IsRequired();
        entity.Property(e => e.Useremailsalt).HasMaxLength(500).IsUnicode(false).HasColumnName("USEREMAILSALT").IsRequired();
        entity.Property(e => e.Userfirstname).HasMaxLength(150).IsUnicode(false).HasColumnName("USERFIRSTNAME").IsRequired();
        entity.Property(e => e.Userlastname).HasMaxLength(150).IsUnicode(false).HasColumnName("USERLASTNAME").IsRequired();
    }
}
