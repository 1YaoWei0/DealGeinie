using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DealGeinieCrmService.Models
{
    [Table("EnterpriseInfo")]
    public class EnterpriseInfo
    {
        // 主键配置
        [Key]
        [StringLength(18)]
        [Column(TypeName = "CHAR")]
        public string UnifiedSocialCreditCode { get; set; }

        // 必填字段配置
        [Required]
        [StringLength(255)]
        public string EnterpriseName { get; set; }

        [Required]
        [StringLength(50)]
        public string RegistrationStatus { get; set; }

        // 可选字段配置
        [StringLength(100)]
        public string LegalRepresentative { get; set; }

        [StringLength(50)]
        public string RegisteredCapital { get; set; } // 建议后续拆分为数值+单位

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EstablishmentDate { get; set; }

        [StringLength(500)]
        public string EnterpriseAddress { get; set; }

        [StringLength(50)]
        public string Province { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string DistrictCounty { get; set; }

        [StringLength(100)]
        public string ValidPhoneNumber { get; set; }

        // 长文本字段
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string AdditionalPhoneNumbers { get; set; }

        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(100)]
        public string EnterpriseType { get; set; }

        // 唯一约束字段
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string TaxpayerID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [StringLength(50)]
        public string OrganizationCode { get; set; }

        public int? InsuredCount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "年报 yyyy")]
        public DateTime? InsuredYearReport { get; set; }

        // 行业分类字段
        [StringLength(100)]
        public string NationalIndustryCategory { get; set; }

        [StringLength(100)]
        public string NationalIndustryMajor { get; set; }

        [StringLength(100)]
        public string NationalIndustryMedium { get; set; }

        [StringLength(100)]
        public string NationalIndustryMinor { get; set; }

        // 企查查行业分类
        [StringLength(100)]
        public string QichachaIndustryCategory { get; set; }

        [StringLength(100)]
        public string QichachaIndustryMajor { get; set; }

        [StringLength(100)]
        public string QichachaIndustryMedium { get; set; }

        [StringLength(100)]
        public string QichachaIndustryMinor { get; set; }

        [StringLength(50)]
        public string EnterpriseScale { get; set; }

        [StringLength(255)]
        public string EnglishName { get; set; }

        [StringLength(255)]
        [Url]
        public string Website { get; set; }

        [StringLength(500)]
        public string MailingAddress { get; set; }

        // 长文本字段
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string EnterpriseProfile { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string BusinessScope { get; set; }
    }

    public class EnterpriseContext : DbContext
    {
        public EnterpriseContext() : base("name=EnterpriseConnectionString")
        {
            // 禁用初始化策略
            Database.SetInitializer<EnterpriseContext>(null);
        }

        public DbSet<EnterpriseInfo> Enterprises { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Fluent API 配置
            modelBuilder.Entity<EnterpriseInfo>()
                .Property(e => e.UnifiedSocialCreditCode)
                .IsFixedLength()  // 配置CHAR类型
                .HasMaxLength(18);

            // 配置唯一索引
            modelBuilder.Entity<EnterpriseInfo>()
                .HasIndex(e => e.TaxpayerID)
                .IsUnique();

            modelBuilder.Entity<EnterpriseInfo>()
                .HasIndex(e => e.RegistrationNumber)
                .IsUnique();

            // 配置默认字符串长度
            modelBuilder.Properties<string>()
                .Configure(c => c.HasMaxLength(255).IsUnicode(true));

            base.OnModelCreating(modelBuilder);
        }
    }
}