using DealGeinieCrmService.Models;
using System.Data.Entity;

namespace DealGeinieCrmService.Utilities
{
    public class EnterpriseContext : DbContext
    {
        // 使用你的连接字符串名称
        public EnterpriseContext()
            : base("name=EnterpriseConnectionString") // 对应Web.config中的name
        {
            // 解决master数据库的权限问题
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            // 禁用初始化策略（推荐生产环境使用）
            Database.SetInitializer<EnterpriseContext>(null);

            // 如果需要在数据库不存在时创建：
            // Database.SetInitializer(new CreateDatabaseIfNotExists<EnterpriseContext>());
        }

        public DbSet<EnterpriseInfo> Enterprises { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 确保使用dbo模式（master数据库需要）
            modelBuilder.HasDefaultSchema("dbo");

            // 其他Fluent API配置保持不变...
            modelBuilder.Entity<EnterpriseInfo>().Property(e => e.UnifiedSocialCreditCode).IsFixedLength().HasMaxLength(18);

            base.OnModelCreating(modelBuilder);
        }
    }
}