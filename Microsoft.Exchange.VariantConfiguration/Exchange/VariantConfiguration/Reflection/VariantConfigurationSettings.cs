using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000129 RID: 297
	public sealed class VariantConfigurationSettings
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x000221B8 File Offset: 0x000203B8
		internal VariantConfigurationSettings()
		{
			this.Add(new VariantConfigurationActiveMonitoringComponent());
			this.Add(new VariantConfigurationActiveSyncComponent());
			this.Add(new VariantConfigurationADComponent());
			this.Add(new VariantConfigurationAutodiscoverComponent());
			this.Add(new VariantConfigurationBoomerangComponent());
			this.Add(new VariantConfigurationCafeComponent());
			this.Add(new VariantConfigurationCalendarLoggingComponent());
			this.Add(new VariantConfigurationClientAccessRulesCommonComponent());
			this.Add(new VariantConfigurationCmdletInfraComponent());
			this.Add(new VariantConfigurationCompliancePolicyComponent());
			this.Add(new VariantConfigurationDataStorageComponent());
			this.Add(new VariantConfigurationDiagnosticsComponent());
			this.Add(new VariantConfigurationDiscoveryComponent());
			this.Add(new VariantConfigurationE4EComponent());
			this.Add(new VariantConfigurationEacComponent());
			this.Add(new VariantConfigurationEwsComponent());
			this.Add(new VariantConfigurationGlobalComponent());
			this.Add(new VariantConfigurationHighAvailabilityComponent());
			this.Add(new VariantConfigurationHolidayCalendarsComponent());
			this.Add(new VariantConfigurationHxComponent());
			this.Add(new VariantConfigurationImapComponent());
			this.Add(new VariantConfigurationInferenceComponent());
			this.Add(new VariantConfigurationIpaedComponent());
			this.Add(new VariantConfigurationMailboxAssistantsComponent());
			this.Add(new VariantConfigurationMailboxPlansComponent());
			this.Add(new VariantConfigurationMailboxTransportComponent());
			this.Add(new VariantConfigurationMalwareAgentComponent());
			this.Add(new VariantConfigurationMessageTrackingComponent());
			this.Add(new VariantConfigurationMexAgentsComponent());
			this.Add(new VariantConfigurationMrsComponent());
			this.Add(new VariantConfigurationNotificationBrokerServiceComponent());
			this.Add(new VariantConfigurationOABComponent());
			this.Add(new VariantConfigurationOfficeGraphComponent());
			this.Add(new VariantConfigurationOwaClientComponent());
			this.Add(new VariantConfigurationOwaClientServerComponent());
			this.Add(new VariantConfigurationOwaServerComponent());
			this.Add(new VariantConfigurationOwaDeploymentComponent());
			this.Add(new VariantConfigurationPopComponent());
			this.Add(new VariantConfigurationRpcClientAccessComponent());
			this.Add(new VariantConfigurationSearchComponent());
			this.Add(new VariantConfigurationSharedCacheComponent());
			this.Add(new VariantConfigurationSharedMailboxComponent());
			this.Add(new VariantConfigurationTestComponent());
			this.Add(new VariantConfigurationTest2Component());
			this.Add(new VariantConfigurationTransportComponent());
			this.Add(new VariantConfigurationUCCComponent());
			this.Add(new VariantConfigurationUMComponent());
			this.Add(new VariantConfigurationVariantConfigComponent());
			this.Add(new VariantConfigurationWorkingSetComponent());
			this.Add(new VariantConfigurationWorkloadManagementComponent());
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00022401 File Offset: 0x00020601
		public VariantConfigurationActiveMonitoringComponent ActiveMonitoring
		{
			get
			{
				return (VariantConfigurationActiveMonitoringComponent)this["ActiveMonitoring"];
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00022413 File Offset: 0x00020613
		public VariantConfigurationActiveSyncComponent ActiveSync
		{
			get
			{
				return (VariantConfigurationActiveSyncComponent)this["ActiveSync"];
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00022425 File Offset: 0x00020625
		public VariantConfigurationADComponent AD
		{
			get
			{
				return (VariantConfigurationADComponent)this["AD"];
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00022437 File Offset: 0x00020637
		public VariantConfigurationAutodiscoverComponent Autodiscover
		{
			get
			{
				return (VariantConfigurationAutodiscoverComponent)this["Autodiscover"];
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00022449 File Offset: 0x00020649
		public VariantConfigurationBoomerangComponent Boomerang
		{
			get
			{
				return (VariantConfigurationBoomerangComponent)this["Boomerang"];
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0002245B File Offset: 0x0002065B
		public VariantConfigurationCafeComponent Cafe
		{
			get
			{
				return (VariantConfigurationCafeComponent)this["Cafe"];
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0002246D File Offset: 0x0002066D
		public VariantConfigurationCalendarLoggingComponent CalendarLogging
		{
			get
			{
				return (VariantConfigurationCalendarLoggingComponent)this["CalendarLogging"];
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0002247F File Offset: 0x0002067F
		public VariantConfigurationClientAccessRulesCommonComponent ClientAccessRulesCommon
		{
			get
			{
				return (VariantConfigurationClientAccessRulesCommonComponent)this["ClientAccessRulesCommon"];
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00022491 File Offset: 0x00020691
		public VariantConfigurationCmdletInfraComponent CmdletInfra
		{
			get
			{
				return (VariantConfigurationCmdletInfraComponent)this["CmdletInfra"];
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x000224A3 File Offset: 0x000206A3
		public VariantConfigurationCompliancePolicyComponent CompliancePolicy
		{
			get
			{
				return (VariantConfigurationCompliancePolicyComponent)this["CompliancePolicy"];
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x000224B5 File Offset: 0x000206B5
		public VariantConfigurationDataStorageComponent DataStorage
		{
			get
			{
				return (VariantConfigurationDataStorageComponent)this["DataStorage"];
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x000224C7 File Offset: 0x000206C7
		public VariantConfigurationDiagnosticsComponent Diagnostics
		{
			get
			{
				return (VariantConfigurationDiagnosticsComponent)this["Diagnostics"];
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x000224D9 File Offset: 0x000206D9
		public VariantConfigurationDiscoveryComponent Discovery
		{
			get
			{
				return (VariantConfigurationDiscoveryComponent)this["Discovery"];
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x000224EB File Offset: 0x000206EB
		public VariantConfigurationE4EComponent E4E
		{
			get
			{
				return (VariantConfigurationE4EComponent)this["E4E"];
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000224FD File Offset: 0x000206FD
		public VariantConfigurationEacComponent Eac
		{
			get
			{
				return (VariantConfigurationEacComponent)this["Eac"];
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0002250F File Offset: 0x0002070F
		public VariantConfigurationEwsComponent Ews
		{
			get
			{
				return (VariantConfigurationEwsComponent)this["Ews"];
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00022521 File Offset: 0x00020721
		public VariantConfigurationGlobalComponent Global
		{
			get
			{
				return (VariantConfigurationGlobalComponent)this["Global"];
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00022533 File Offset: 0x00020733
		public VariantConfigurationHighAvailabilityComponent HighAvailability
		{
			get
			{
				return (VariantConfigurationHighAvailabilityComponent)this["HighAvailability"];
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00022545 File Offset: 0x00020745
		public VariantConfigurationHolidayCalendarsComponent HolidayCalendars
		{
			get
			{
				return (VariantConfigurationHolidayCalendarsComponent)this["HolidayCalendars"];
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00022557 File Offset: 0x00020757
		public VariantConfigurationHxComponent Hx
		{
			get
			{
				return (VariantConfigurationHxComponent)this["Hx"];
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00022569 File Offset: 0x00020769
		public VariantConfigurationImapComponent Imap
		{
			get
			{
				return (VariantConfigurationImapComponent)this["Imap"];
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0002257B File Offset: 0x0002077B
		public VariantConfigurationInferenceComponent Inference
		{
			get
			{
				return (VariantConfigurationInferenceComponent)this["Inference"];
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0002258D File Offset: 0x0002078D
		public VariantConfigurationIpaedComponent Ipaed
		{
			get
			{
				return (VariantConfigurationIpaedComponent)this["Ipaed"];
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0002259F File Offset: 0x0002079F
		public VariantConfigurationMailboxAssistantsComponent MailboxAssistants
		{
			get
			{
				return (VariantConfigurationMailboxAssistantsComponent)this["MailboxAssistants"];
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x000225B1 File Offset: 0x000207B1
		public VariantConfigurationMailboxPlansComponent MailboxPlans
		{
			get
			{
				return (VariantConfigurationMailboxPlansComponent)this["MailboxPlans"];
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x000225C3 File Offset: 0x000207C3
		public VariantConfigurationMailboxTransportComponent MailboxTransport
		{
			get
			{
				return (VariantConfigurationMailboxTransportComponent)this["MailboxTransport"];
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x000225D5 File Offset: 0x000207D5
		public VariantConfigurationMalwareAgentComponent MalwareAgent
		{
			get
			{
				return (VariantConfigurationMalwareAgentComponent)this["MalwareAgent"];
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x000225E7 File Offset: 0x000207E7
		public VariantConfigurationMessageTrackingComponent MessageTracking
		{
			get
			{
				return (VariantConfigurationMessageTrackingComponent)this["MessageTracking"];
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x000225F9 File Offset: 0x000207F9
		public VariantConfigurationMexAgentsComponent MexAgents
		{
			get
			{
				return (VariantConfigurationMexAgentsComponent)this["MexAgents"];
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0002260B File Offset: 0x0002080B
		public VariantConfigurationMrsComponent Mrs
		{
			get
			{
				return (VariantConfigurationMrsComponent)this["Mrs"];
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0002261D File Offset: 0x0002081D
		public VariantConfigurationNotificationBrokerServiceComponent NotificationBrokerService
		{
			get
			{
				return (VariantConfigurationNotificationBrokerServiceComponent)this["NotificationBrokerService"];
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0002262F File Offset: 0x0002082F
		public VariantConfigurationOABComponent OAB
		{
			get
			{
				return (VariantConfigurationOABComponent)this["OAB"];
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00022641 File Offset: 0x00020841
		public VariantConfigurationOfficeGraphComponent OfficeGraph
		{
			get
			{
				return (VariantConfigurationOfficeGraphComponent)this["OfficeGraph"];
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00022653 File Offset: 0x00020853
		public VariantConfigurationOwaClientComponent OwaClient
		{
			get
			{
				return (VariantConfigurationOwaClientComponent)this["OwaClient"];
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00022665 File Offset: 0x00020865
		public VariantConfigurationOwaClientServerComponent OwaClientServer
		{
			get
			{
				return (VariantConfigurationOwaClientServerComponent)this["OwaClientServer"];
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00022677 File Offset: 0x00020877
		public VariantConfigurationOwaServerComponent OwaServer
		{
			get
			{
				return (VariantConfigurationOwaServerComponent)this["OwaServer"];
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00022689 File Offset: 0x00020889
		public VariantConfigurationOwaDeploymentComponent OwaDeployment
		{
			get
			{
				return (VariantConfigurationOwaDeploymentComponent)this["OwaDeployment"];
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0002269B File Offset: 0x0002089B
		public VariantConfigurationPopComponent Pop
		{
			get
			{
				return (VariantConfigurationPopComponent)this["Pop"];
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x000226AD File Offset: 0x000208AD
		public VariantConfigurationRpcClientAccessComponent RpcClientAccess
		{
			get
			{
				return (VariantConfigurationRpcClientAccessComponent)this["RpcClientAccess"];
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x000226BF File Offset: 0x000208BF
		public VariantConfigurationSearchComponent Search
		{
			get
			{
				return (VariantConfigurationSearchComponent)this["Search"];
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x000226D1 File Offset: 0x000208D1
		public VariantConfigurationSharedCacheComponent SharedCache
		{
			get
			{
				return (VariantConfigurationSharedCacheComponent)this["SharedCache"];
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x000226E3 File Offset: 0x000208E3
		public VariantConfigurationSharedMailboxComponent SharedMailbox
		{
			get
			{
				return (VariantConfigurationSharedMailboxComponent)this["SharedMailbox"];
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x000226F5 File Offset: 0x000208F5
		public VariantConfigurationTestComponent Test
		{
			get
			{
				return (VariantConfigurationTestComponent)this["Test"];
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00022707 File Offset: 0x00020907
		public VariantConfigurationTest2Component Test2
		{
			get
			{
				return (VariantConfigurationTest2Component)this["Test2"];
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00022719 File Offset: 0x00020919
		public VariantConfigurationTransportComponent Transport
		{
			get
			{
				return (VariantConfigurationTransportComponent)this["Transport"];
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0002272B File Offset: 0x0002092B
		public VariantConfigurationUCCComponent UCC
		{
			get
			{
				return (VariantConfigurationUCCComponent)this["UCC"];
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0002273D File Offset: 0x0002093D
		public VariantConfigurationUMComponent UM
		{
			get
			{
				return (VariantConfigurationUMComponent)this["UM"];
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0002274F File Offset: 0x0002094F
		public VariantConfigurationVariantConfigComponent VariantConfig
		{
			get
			{
				return (VariantConfigurationVariantConfigComponent)this["VariantConfig"];
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00022761 File Offset: 0x00020961
		public VariantConfigurationWorkingSetComponent WorkingSet
		{
			get
			{
				return (VariantConfigurationWorkingSetComponent)this["WorkingSet"];
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x00022773 File Offset: 0x00020973
		public VariantConfigurationWorkloadManagementComponent WorkloadManagement
		{
			get
			{
				return (VariantConfigurationWorkloadManagementComponent)this["WorkloadManagement"];
			}
		}

		// Token: 0x17000B19 RID: 2841
		public VariantConfigurationComponent this[string name]
		{
			get
			{
				return this.components[name];
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000227BC File Offset: 0x000209BC
		public IEnumerable<string> GetComponents(bool includeInternal)
		{
			if (includeInternal)
			{
				return this.components.Keys;
			}
			return from component in this.components.Keys
			where this[component].GetSections(includeInternal).Any<string>()
			select component;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00022814 File Offset: 0x00020A14
		public bool Contains(string name, bool includeInternal)
		{
			return this.components.ContainsKey(name) && (includeInternal || this[name].GetSections(false).Any<string>());
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0002283D File Offset: 0x00020A3D
		private void Add(VariantConfigurationComponent component)
		{
			this.components.Add(component.ComponentName, component);
		}

		// Token: 0x04000486 RID: 1158
		private Dictionary<string, VariantConfigurationComponent> components = new Dictionary<string, VariantConfigurationComponent>(StringComparer.OrdinalIgnoreCase);
	}
}
