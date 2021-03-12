using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000012 RID: 18
	internal class ConfigurationCache
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00004C12 File Offset: 0x00002E12
		public ConfigurationCache()
		{
			this.configCache = new CacheWithExpiration<OrganizationId, ConfigWrapper>(16, ConfigurationCache.cacheExpirationTime, null);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004C30 File Offset: 0x00002E30
		public ConfigWrapper FindOrCreate(OrganizationId organizationId, LogMessageDelegate logMessage)
		{
			DateTime utcNow = DateTime.UtcNow;
			ConfigWrapper configWrapper = null;
			if (!this.configCache.TryGetValue(organizationId, utcNow, out configWrapper))
			{
				ConfigurationCache.ConfigurationPolicy configurationPolicy = new ConfigurationCache.ConfigurationPolicy(organizationId);
				configWrapper = new ConfigWrapper(configurationPolicy, utcNow, logMessage);
				this.configCache.TryAdd(organizationId, utcNow, configWrapper);
			}
			return configWrapper;
		}

		// Token: 0x0400004A RID: 74
		private const int cacheSize = 16;

		// Token: 0x0400004B RID: 75
		private static readonly TimeSpan cacheExpirationTime = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400004C RID: 76
		private readonly CacheWithExpiration<OrganizationId, ConfigWrapper> configCache;

		// Token: 0x02000013 RID: 19
		private class ConfigurationPolicy : IConfigurationPolicy
		{
			// Token: 0x0600007C RID: 124 RVA: 0x00004C8B File Offset: 0x00002E8B
			public ConfigurationPolicy(OrganizationId organizationId)
			{
				this.organizationId = organizationId;
			}

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600007D RID: 125 RVA: 0x00004C9A File Offset: 0x00002E9A
			public bool RunningOnDataCenter
			{
				get
				{
					return AdminAuditLogHelper.RunningOnDataCenter;
				}
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600007E RID: 126 RVA: 0x00004CA1 File Offset: 0x00002EA1
			public OrganizationId OrganizationId
			{
				get
				{
					return this.organizationId;
				}
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x0600007F RID: 127 RVA: 0x00004CA9 File Offset: 0x00002EA9
			public IExchangePrincipal ExchangePrincipal
			{
				get
				{
					return this.exchangePrincipal;
				}
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00004CB4 File Offset: 0x00002EB4
			public ArbitrationMailboxStatus CheckArbitrationMailboxStatus(out Exception initialError)
			{
				ADUser aduser;
				return AdminAuditLogHelper.CheckArbitrationMailboxStatus(this.organizationId, out aduser, out this.exchangePrincipal, out initialError);
			}

			// Token: 0x06000081 RID: 129 RVA: 0x00004CD5 File Offset: 0x00002ED5
			public IAuditLog CreateLogger(ArbitrationMailboxStatus mailboxStatus)
			{
				if (mailboxStatus == ArbitrationMailboxStatus.FFO)
				{
					return AuditLoggerFactory.CreateForFFO(this.organizationId);
				}
				return AuditLoggerFactory.Create(this.exchangePrincipal, mailboxStatus);
			}

			// Token: 0x06000082 RID: 130 RVA: 0x00004CF4 File Offset: 0x00002EF4
			public IAdminAuditLogConfig GetAdminAuditLogConfig()
			{
				IConfigurationSession configSession = AdminAuditLogHelper.CreateSession(this.organizationId, null);
				AdminAuditLogConfig adminAuditLogConfig = AdminAuditLogHelper.GetAdminAuditLogConfig(configSession);
				if (adminAuditLogConfig != null)
				{
					return new ConfigurationCache.ConfigurationPolicy.AdminAuditLogConfigAdapter(adminAuditLogConfig);
				}
				return null;
			}

			// Token: 0x0400004D RID: 77
			private readonly OrganizationId organizationId;

			// Token: 0x0400004E RID: 78
			private ExchangePrincipal exchangePrincipal;

			// Token: 0x02000014 RID: 20
			private class AdminAuditLogConfigAdapter : IAdminAuditLogConfig
			{
				// Token: 0x06000083 RID: 131 RVA: 0x00004D20 File Offset: 0x00002F20
				public AdminAuditLogConfigAdapter(AdminAuditLogConfig adminAuditLogConfig)
				{
					this.adminAuditLogConfig = adminAuditLogConfig;
				}

				// Token: 0x17000028 RID: 40
				// (get) Token: 0x06000084 RID: 132 RVA: 0x00004D2F File Offset: 0x00002F2F
				public MultiValuedProperty<string> AdminAuditLogParameters
				{
					get
					{
						return this.adminAuditLogConfig.AdminAuditLogParameters;
					}
				}

				// Token: 0x17000029 RID: 41
				// (get) Token: 0x06000085 RID: 133 RVA: 0x00004D3C File Offset: 0x00002F3C
				public MultiValuedProperty<string> AdminAuditLogCmdlets
				{
					get
					{
						return this.adminAuditLogConfig.AdminAuditLogCmdlets;
					}
				}

				// Token: 0x1700002A RID: 42
				// (get) Token: 0x06000086 RID: 134 RVA: 0x00004D49 File Offset: 0x00002F49
				public MultiValuedProperty<string> AdminAuditLogExcludedCmdlets
				{
					get
					{
						return this.adminAuditLogConfig.AdminAuditLogExcludedCmdlets;
					}
				}

				// Token: 0x1700002B RID: 43
				// (get) Token: 0x06000087 RID: 135 RVA: 0x00004D56 File Offset: 0x00002F56
				public bool AdminAuditLogEnabled
				{
					get
					{
						return this.adminAuditLogConfig.AdminAuditLogEnabled;
					}
				}

				// Token: 0x1700002C RID: 44
				// (get) Token: 0x06000088 RID: 136 RVA: 0x00004D64 File Offset: 0x00002F64
				public bool IsValidAuditLogMailboxAddress
				{
					get
					{
						SmtpAddress adminAuditLogMailbox = this.adminAuditLogConfig.AdminAuditLogMailbox;
						return this.adminAuditLogConfig.AdminAuditLogMailbox.IsValidAddress;
					}
				}

				// Token: 0x1700002D RID: 45
				// (get) Token: 0x06000089 RID: 137 RVA: 0x00004D90 File Offset: 0x00002F90
				public bool TestCmdletLoggingEnabled
				{
					get
					{
						return this.adminAuditLogConfig.TestCmdletLoggingEnabled;
					}
				}

				// Token: 0x1700002E RID: 46
				// (get) Token: 0x0600008A RID: 138 RVA: 0x00004D9D File Offset: 0x00002F9D
				public AuditLogLevel LogLevel
				{
					get
					{
						return this.adminAuditLogConfig.LogLevel;
					}
				}

				// Token: 0x0400004F RID: 79
				private AdminAuditLogConfig adminAuditLogConfig;
			}
		}
	}
}
