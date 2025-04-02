use DealGenieDB;

CREATE TABLE EnterpriseInfo (
    UnifiedSocialCreditCode CHAR(18) PRIMARY KEY,
    EnterpriseName NVARCHAR(255) NOT NULL,
    RegistrationStatus NVARCHAR(50) NOT NULL,
    LegalRepresentative NVARCHAR(100),
    RegisteredCapital NVARCHAR(50),
    EstablishmentDate DATE,
    EnterpriseAddress NVARCHAR(500),
    Province NVARCHAR(50),
    City NVARCHAR(50),
    DistrictCounty NVARCHAR(50),
    ValidPhoneNumber NVARCHAR(100),
    AdditionalPhoneNumbers NVARCHAR(MAX),
    Email NVARCHAR(255),
    EnterpriseType NVARCHAR(100),
    TaxpayerID NVARCHAR(50),
    RegistrationNumber NVARCHAR(50),
    OrganizationCode NVARCHAR(50),
    InsuredCount INT,
    InsuredYearReport DATE,
    NationalIndustryCategory NVARCHAR(100),
    NationalIndustryMajor NVARCHAR(100),
    NationalIndustryMedium NVARCHAR(100),
    NationalIndustryMinor NVARCHAR(100),
    QichachaIndustryCategory NVARCHAR(100),
    QichachaIndustryMajor NVARCHAR(100),
    QichachaIndustryMedium NVARCHAR(100),
    QichachaIndustryMinor NVARCHAR(100),
    EnterpriseScale NVARCHAR(50),
    EnglishName NVARCHAR(255),
    Website NVARCHAR(255),
    MailingAddress NVARCHAR(500),
    EnterpriseProfile NVARCHAR(MAX),
    BusinessScope NVARCHAR(MAX),
    -- 其他字段约束
    CONSTRAINT UQ_TaxpayerID UNIQUE (TaxpayerID), -- 纳税人识别号唯一
    CONSTRAINT UQ_RegistrationNumber UNIQUE (RegistrationNumber) -- 注册号唯一
);

-- 索引示例（根据查询需求可选）
CREATE INDEX IX_EnterpriseName ON EnterpriseInfo (EnterpriseName);
CREATE INDEX IX_EstablishmentDate ON EnterpriseInfo (EstablishmentDate);
CREATE INDEX IX_ProvinceCity ON EnterpriseInfo (Province, City);