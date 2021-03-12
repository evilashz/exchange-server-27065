using System;
using System.Configuration;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Servicelets.AuthAdmin
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuthAdminContext : AnchorContext
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002120 File Offset: 0x00000320
		internal AuthAdminContext(string applicationName) : base(OrganizationCapability.Management)
		{
			AnchorConfig config = AuthAdminContext.CreateConfigSchema();
			AuthAdminLogger logger = new AuthAdminLogger(applicationName, config, new ExEventLog(AuthAdminContext.ComponentGuid, "MSExchange AuthAdmin"));
			base.Initialize(applicationName, logger, config);
			this.IsMultiTenancyEnabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000217F File Offset: 0x0000037F
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002187 File Offset: 0x00000387
		internal bool IsMultiTenancyEnabled { get; private set; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002190 File Offset: 0x00000390
		public override CacheProcessorBase[] CreateCacheComponents(WaitHandle stopEvent)
		{
			return new CacheProcessorBase[]
			{
				new FirstOrgCacheScanner(this, stopEvent),
				new AuthAdminScheduler(this, stopEvent)
			};
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021B9 File Offset: 0x000003B9
		internal static AnchorConfig CreateConfigSchema()
		{
			return new AuthAdminContext.AuthAdminConfig();
		}

		// Token: 0x04000002 RID: 2
		internal const string TrustAnySSLCertificate = "TrustAnySSLCertificate";

		// Token: 0x04000003 RID: 3
		internal const string EventLogSourceName = "MSExchange AuthAdmin";

		// Token: 0x04000004 RID: 4
		private static readonly Guid ComponentGuid = new Guid("B03300F0-060B-4EC3-89BB-1180DD8FE1BF");

		// Token: 0x02000004 RID: 4
		private class AuthAdminConfig : AnchorConfig
		{
			// Token: 0x06000009 RID: 9 RVA: 0x000021D4 File Offset: 0x000003D4
			internal AuthAdminConfig() : base("AuthAdmin")
			{
				base.UpdateConfig<TimeSpan>("IdleRunDelay", TimeSpan.FromHours(12.0));
				base.UpdateConfig<TimeSpan>("ActiveRunDelay", TimeSpan.FromHours(12.0));
				base.UpdateConfig<TimeSpan>("TransientErrorRunDelay", TimeSpan.FromMinutes(15.0));
				base.UpdateConfig<int>("MaximumCacheEntrySchedulerRun", 1);
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000A RID: 10 RVA: 0x00002243 File Offset: 0x00000443
			// (set) Token: 0x0600000B RID: 11 RVA: 0x00002250 File Offset: 0x00000450
			[ConfigurationProperty("TrustAnySSLCertificate", DefaultValue = false)]
			public bool TrustAnySSLCertificate
			{
				get
				{
					return this.InternalGetConfig<bool>("TrustAnySSLCertificate");
				}
				set
				{
					this.InternalSetConfig<bool>(value, "TrustAnySSLCertificate");
				}
			}
		}
	}
}
