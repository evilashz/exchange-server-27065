using System;
using Microsoft.Exchange.Flighting;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000F7 RID: 247
	public sealed class VariantConfigurationActiveMonitoringComponent : VariantConfigurationComponent
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x00018BFC File Offset: 0x00016DFC
		internal VariantConfigurationActiveMonitoringComponent() : base("ActiveMonitoring")
		{
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "ProcessIsolationResetIISAppPoolResponder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "WatsonResponder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "DirectoryAccessor", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "GetExchangeDiagnosticsInfoResponder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "PushNotificationsDiscoveryMbx", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "EscalateResponder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "CafeOfflineRespondersUseClientAccessArray", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "PopImapDiscoveryCommon", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "TraceLogResponder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "AllowBasicAuthForOutsideInMonitoringMailboxes", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "ActiveSyncDiscovery", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "ClearLsassCacheResponder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "ProcessIsolationRestartServiceResponder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "SubjectMaintenance", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "LocalEndpointManager", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "F1TraceResponder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "RpcProbe", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "PushNotificationsDiscoveryCafe", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "AutoDiscoverExternalUrlVerification", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveMonitoring.settings.ini", "PinMonitoringMailboxesToDatabases", typeof(ICmdletSettings), false));
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00018E94 File Offset: 0x00017094
		public VariantConfigurationSection ProcessIsolationResetIISAppPoolResponder
		{
			get
			{
				return base["ProcessIsolationResetIISAppPoolResponder"];
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00018EA1 File Offset: 0x000170A1
		public VariantConfigurationSection WatsonResponder
		{
			get
			{
				return base["WatsonResponder"];
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00018EAE File Offset: 0x000170AE
		public VariantConfigurationSection DirectoryAccessor
		{
			get
			{
				return base["DirectoryAccessor"];
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00018EBB File Offset: 0x000170BB
		public VariantConfigurationSection GetExchangeDiagnosticsInfoResponder
		{
			get
			{
				return base["GetExchangeDiagnosticsInfoResponder"];
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00018EC8 File Offset: 0x000170C8
		public VariantConfigurationSection PushNotificationsDiscoveryMbx
		{
			get
			{
				return base["PushNotificationsDiscoveryMbx"];
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00018ED5 File Offset: 0x000170D5
		public VariantConfigurationSection EscalateResponder
		{
			get
			{
				return base["EscalateResponder"];
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00018EE2 File Offset: 0x000170E2
		public VariantConfigurationSection CafeOfflineRespondersUseClientAccessArray
		{
			get
			{
				return base["CafeOfflineRespondersUseClientAccessArray"];
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00018EEF File Offset: 0x000170EF
		public VariantConfigurationSection PopImapDiscoveryCommon
		{
			get
			{
				return base["PopImapDiscoveryCommon"];
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00018EFC File Offset: 0x000170FC
		public VariantConfigurationSection TraceLogResponder
		{
			get
			{
				return base["TraceLogResponder"];
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00018F09 File Offset: 0x00017109
		public VariantConfigurationSection AllowBasicAuthForOutsideInMonitoringMailboxes
		{
			get
			{
				return base["AllowBasicAuthForOutsideInMonitoringMailboxes"];
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00018F16 File Offset: 0x00017116
		public VariantConfigurationSection ActiveSyncDiscovery
		{
			get
			{
				return base["ActiveSyncDiscovery"];
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00018F23 File Offset: 0x00017123
		public VariantConfigurationSection ClearLsassCacheResponder
		{
			get
			{
				return base["ClearLsassCacheResponder"];
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00018F30 File Offset: 0x00017130
		public VariantConfigurationSection ProcessIsolationRestartServiceResponder
		{
			get
			{
				return base["ProcessIsolationRestartServiceResponder"];
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00018F3D File Offset: 0x0001713D
		public VariantConfigurationSection SubjectMaintenance
		{
			get
			{
				return base["SubjectMaintenance"];
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00018F4A File Offset: 0x0001714A
		public VariantConfigurationSection LocalEndpointManager
		{
			get
			{
				return base["LocalEndpointManager"];
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00018F57 File Offset: 0x00017157
		public VariantConfigurationSection F1TraceResponder
		{
			get
			{
				return base["F1TraceResponder"];
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00018F64 File Offset: 0x00017164
		public VariantConfigurationSection RpcProbe
		{
			get
			{
				return base["RpcProbe"];
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00018F71 File Offset: 0x00017171
		public VariantConfigurationSection PushNotificationsDiscoveryCafe
		{
			get
			{
				return base["PushNotificationsDiscoveryCafe"];
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00018F7E File Offset: 0x0001717E
		public VariantConfigurationSection AutoDiscoverExternalUrlVerification
		{
			get
			{
				return base["AutoDiscoverExternalUrlVerification"];
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00018F8B File Offset: 0x0001718B
		public VariantConfigurationSection PinMonitoringMailboxesToDatabases
		{
			get
			{
				return base["PinMonitoringMailboxesToDatabases"];
			}
		}
	}
}
