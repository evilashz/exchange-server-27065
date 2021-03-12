using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Ehf;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200001B RID: 27
	internal abstract class EhfTargetConnection : DatacenterTargetConnection
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00009305 File Offset: 0x00007505
		public EhfTargetConnection(int localServerVersion, EhfTargetServerConfig config, EnhancedTimeSpan syncInterval, EdgeSyncLogSession logSession) : base(localServerVersion, config, syncInterval, logSession, ExTraceGlobals.TargetConnectionTracer)
		{
			this.config = config;
			this.configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 104, ".ctor", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\EHF\\EhfTargetConnection.cs");
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00009341 File Offset: 0x00007541
		public EhfTargetConnection(int localServerVersion, EhfTargetServerConfig config, EdgeSyncLogSession logSession, EhfPerfCounterHandler perfCounterHandler, IProvisioningService provisioningService, IManagementService managementService, IAdminSyncService adminSyncService, EhfADAdapter adapter, EnhancedTimeSpan syncInterval) : this(localServerVersion, config, syncInterval, logSession)
		{
			this.provisioningService = new EhfProvisioningService(provisioningService, managementService, adminSyncService);
			this.adapter = adapter;
			this.perfCounterHandler = perfCounterHandler;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000936F File Offset: 0x0000756F
		public EhfProvisioningService ProvisioningService
		{
			get
			{
				return this.provisioningService;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00009377 File Offset: 0x00007577
		public EhfADAdapter ADAdapter
		{
			get
			{
				return this.adapter;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000937F File Offset: 0x0000757F
		public EhfTargetServerConfig Config
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00009387 File Offset: 0x00007587
		public EhfPerfCounterHandler PerfCounterHandler
		{
			get
			{
				return this.perfCounterHandler;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000938F File Offset: 0x0000758F
		protected override string LeaseFileName
		{
			get
			{
				return "ehf.lease";
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00009396 File Offset: 0x00007596
		protected override IConfigurationSession ConfigSession
		{
			get
			{
				return this.configSession;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000093A0 File Offset: 0x000075A0
		public static ADObjectId GetCookieContainerId(IConfigurationSession configSession)
		{
			ADObjectId orgContainerId = configSession.GetOrgContainerId();
			return orgContainerId.GetChildId("Transport Settings").GetChildId("EHF Sync Cookies");
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000093C9 File Offset: 0x000075C9
		public static ADObjectId GetPerimeterConfigObjectIdFromConfigUnitId(ADObjectId configUnitId)
		{
			return configUnitId.GetChildId("Transport Settings").GetChildId("Tenant Perimeter Settings");
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000093E0 File Offset: 0x000075E0
		public virtual void AbortSyncCycle(Exception cause)
		{
			if (cause is EdgeSyncCycleFailedException)
			{
				throw cause;
			}
			throw new EdgeSyncCycleFailedException(cause);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000093F4 File Offset: 0x000075F4
		public override bool OnSynchronizing()
		{
			if (this.perfCounterHandler == null)
			{
				this.perfCounterHandler = new EhfPerfCounterHandler();
			}
			if (this.provisioningService == null)
			{
				this.provisioningService = new EhfProvisioningService(this.config);
			}
			if (this.adapter == null)
			{
				this.adapter = new EhfADAdapter();
			}
			return true;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009441 File Offset: 0x00007641
		public override void OnConnectedToSource(Connection sourceConnection)
		{
			this.adapter.SetConnection(sourceConnection);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000944F File Offset: 0x0000764F
		public override SyncResult OnRenameEntry(ExSearchResultEntry entry)
		{
			return SyncResult.None;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009452 File Offset: 0x00007652
		public override void Dispose()
		{
			if (this.provisioningService != null)
			{
				this.provisioningService.Dispose();
				this.provisioningService = null;
			}
			base.Dispose();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009474 File Offset: 0x00007674
		public override bool TryReadCookie(out Dictionary<string, Cookie> cookies)
		{
			if (!base.TryReadCookie(out cookies))
			{
				return false;
			}
			ADObjectId adobjectId;
			if (!this.TryGetConfigurationNamingContext(out adobjectId))
			{
				base.DiagSession.LogAndTraceError("Failed to get Configuration Naming context. DomainController=<{0}>", new object[]
				{
					this.ConfigSession.DomainController
				});
				return false;
			}
			Cookie cookie;
			if (!cookies.TryGetValue(adobjectId.DistinguishedName, out cookie) || cookie == null)
			{
				base.DiagSession.LogAndTraceError("Could not find the config sync cookie. This is expected only if we are trying to do a fullSync or during initial sync cycles. Cookies=<{0}>", new object[]
				{
					Util.GetCookieInformationToLog(cookies)
				});
				return true;
			}
			if (string.IsNullOrEmpty(cookie.DomainController))
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "ConfigCookie DC is null or emtpy", new object[0]);
				return true;
			}
			if (cookie.DomainController.Equals(this.ConfigSession.DomainController, StringComparison.OrdinalIgnoreCase))
			{
				base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Edgesync is connecting to the configsync cookie DC. ConfigCookie=<{0}>; ADDriverDC=<{1}>", new object[]
				{
					cookie,
					this.ConfigSession.DomainController
				});
			}
			else
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(cookie.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 379, "TryReadCookie", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\EHF\\EhfTargetConnection.cs");
				Dictionary<string, Cookie> dictionary;
				if (base.TryReadCookie(tenantOrTopologyConfigurationSession, out dictionary))
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Medium, "Read cookies {0} from cookie Domain controller {1}.", new object[]
					{
						Util.GetCookieInformationToLog(dictionary),
						cookie.DomainController
					});
					cookies = dictionary;
				}
				else
				{
					base.DiagSession.LogAndTraceError("Failed to read cookie from config cookie DC <{0}>. Using the cookie from ADDriver DC <{1}>.", new object[]
					{
						cookie.DomainController,
						this.ConfigSession.DomainController
					});
				}
			}
			this.PointCookiesToConfigCookieDC(cookies, cookie.DomainController);
			return true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000961A File Offset: 0x0000781A
		protected override ADObjectId GetCookieContainerId()
		{
			if (EhfTargetConnection.cookieContainerId == null)
			{
				EhfTargetConnection.cookieContainerId = EhfTargetConnection.GetCookieContainerId(this.ConfigSession);
			}
			return EhfTargetConnection.cookieContainerId;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009658 File Offset: 0x00007858
		private bool TryGetConfigurationNamingContext(out ADObjectId configurationNamingContextId)
		{
			ADObjectId tempADObjectId = null;
			configurationNamingContextId = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				tempADObjectId = this.ConfigSession.ConfigurationNamingContext;
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				base.DiagSession.LogAndTraceException(adoperationResult.Exception, "Failed to read Configuration Naming Context from AD", new object[0]);
			}
			else
			{
				configurationNamingContextId = tempADObjectId;
			}
			return adoperationResult.Succeeded;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000096C4 File Offset: 0x000078C4
		private void PointCookiesToConfigCookieDC(Dictionary<string, Cookie> cookies, string configCookieDC)
		{
			foreach (Cookie cookie in cookies.Values)
			{
				if (cookie.DomainController != null && !cookie.DomainController.Equals(configCookieDC, StringComparison.OrdinalIgnoreCase))
				{
					base.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "CookieDC for {0} is not the same as the ConfigSync CookieDC. Setting the CookieDC to {1}. AllCookies = <{2}>", new object[]
					{
						cookie,
						configCookieDC,
						Util.GetCookieInformationToLog(cookies)
					});
					cookie.DomainController = configCookieDC;
				}
			}
		}

		// Token: 0x04000072 RID: 114
		public const string EhfLeaseFileName = "ehf.lease";

		// Token: 0x04000073 RID: 115
		private const string TransportSettingsContainerName = "Transport Settings";

		// Token: 0x04000074 RID: 116
		private const string PerimeterSettingsObjectName = "Tenant Perimeter Settings";

		// Token: 0x04000075 RID: 117
		private const string CookieContainerName = "EHF Sync Cookies";

		// Token: 0x04000076 RID: 118
		private static ADObjectId cookieContainerId;

		// Token: 0x04000077 RID: 119
		private EhfTargetServerConfig config;

		// Token: 0x04000078 RID: 120
		private EhfProvisioningService provisioningService;

		// Token: 0x04000079 RID: 121
		private EhfADAdapter adapter;

		// Token: 0x0400007A RID: 122
		private EhfPerfCounterHandler perfCounterHandler;

		// Token: 0x0400007B RID: 123
		private IConfigurationSession configSession;
	}
}
