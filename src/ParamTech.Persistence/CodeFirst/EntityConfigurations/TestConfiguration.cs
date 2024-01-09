using ParamTech.Domain;
namespace ParamTech.Persistence.CodeFirst.EntityConfigurations;
public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> entity)
    {
        entity.ToTable("TEST").HasKey(e => e.TestId);
        entity.Property(e => e.TestId).HasColumnName("TESTID").IsRequired();
        entity.Property(e => e.Createdate).HasColumnType("datetime").HasColumnName("CREATEDATE").IsRequired();
        entity.Property(e => e.Deletedate).HasColumnType("datetime").HasColumnName("DELETEDATE");
        entity.Property(e => e.Updatedate).HasColumnType("datetime").HasColumnName("UPDATEDATE");
        entity.Property(e => e.Name).HasMaxLength(500).IsUnicode(false).HasColumnName("NAME").IsRequired();
    }
}
