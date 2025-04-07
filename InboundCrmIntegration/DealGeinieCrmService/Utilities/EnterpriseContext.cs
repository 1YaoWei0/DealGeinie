using DealGeinieCrmService.Models;
using System.Data.Entity;

namespace DealGeinieCrmService.Utilities
{
    public class EnterpriseContext : DbContext
    {
        // ʹ����������ַ�������
        public EnterpriseContext()
            : base("name=EnterpriseConnectionString") // ��ӦWeb.config�е�name
        {
            // ���master���ݿ��Ȩ������
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            // ���ó�ʼ�����ԣ��Ƽ���������ʹ�ã�
            Database.SetInitializer<EnterpriseContext>(null);

            // �����Ҫ�����ݿⲻ����ʱ������
            // Database.SetInitializer(new CreateDatabaseIfNotExists<EnterpriseContext>());
        }

        public DbSet<EnterpriseInfo> Enterprises { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ȷ��ʹ��dboģʽ��master���ݿ���Ҫ��
            modelBuilder.HasDefaultSchema("dbo");

            // ����Fluent API���ñ��ֲ���...
            modelBuilder.Entity<EnterpriseInfo>().Property(e => e.UnifiedSocialCreditCode).IsFixedLength().HasMaxLength(18);

            base.OnModelCreating(modelBuilder);
        }
    }
}