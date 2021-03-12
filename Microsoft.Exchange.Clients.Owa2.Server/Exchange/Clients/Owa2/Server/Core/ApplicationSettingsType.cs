using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003B3 RID: 947
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ApplicationSettingsType
	{
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x0007663A File Offset: 0x0007483A
		// (set) Token: 0x06001E2B RID: 7723 RVA: 0x00076642 File Offset: 0x00074842
		[DataMember]
		public bool AnalyticsEnabled { get; set; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x0007664B File Offset: 0x0007484B
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x00076653 File Offset: 0x00074853
		[DataMember]
		public bool CoreAnalyticsEnabled { get; set; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0007665C File Offset: 0x0007485C
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x00076664 File Offset: 0x00074864
		[DataMember]
		public bool InferenceEnabled { get; set; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x0007666D File Offset: 0x0007486D
		// (set) Token: 0x06001E31 RID: 7729 RVA: 0x00076675 File Offset: 0x00074875
		[DataMember]
		public TraceLevel DefaultTraceLevel { get; set; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0007667E File Offset: 0x0007487E
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x00076686 File Offset: 0x00074886
		[DataMember]
		public bool ConsoleTracingEnabled { get; set; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x0007668F File Offset: 0x0007488F
		// (set) Token: 0x06001E35 RID: 7733 RVA: 0x00076697 File Offset: 0x00074897
		[DataMember]
		public PerfTraceLevel DefaultPerfTraceLevel { get; set; }

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000766A0 File Offset: 0x000748A0
		// (set) Token: 0x06001E37 RID: 7735 RVA: 0x000766A8 File Offset: 0x000748A8
		[DataMember]
		public JsMvvmPerfTraceLevel DefaultJsMvvmPerfTraceLevel { get; set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000766B1 File Offset: 0x000748B1
		// (set) Token: 0x06001E39 RID: 7737 RVA: 0x000766B9 File Offset: 0x000748B9
		[DataMember(EmitDefaultValue = false)]
		public string[] TraceInfoComponents { get; set; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x000766C2 File Offset: 0x000748C2
		// (set) Token: 0x06001E3B RID: 7739 RVA: 0x000766CA File Offset: 0x000748CA
		[DataMember(EmitDefaultValue = false)]
		public string[] TraceVerboseComponents { get; set; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x000766D3 File Offset: 0x000748D3
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x000766DB File Offset: 0x000748DB
		[DataMember(EmitDefaultValue = false)]
		public string[] TracePerfComponents { get; set; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x000766E4 File Offset: 0x000748E4
		// (set) Token: 0x06001E3F RID: 7743 RVA: 0x000766EC File Offset: 0x000748EC
		[DataMember(EmitDefaultValue = false)]
		public string[] TraceWarningComponents { get; set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x000766F5 File Offset: 0x000748F5
		// (set) Token: 0x06001E41 RID: 7745 RVA: 0x000766FD File Offset: 0x000748FD
		[DataMember]
		public bool WatsonEnabled { get; set; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x00076706 File Offset: 0x00074906
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x0007670E File Offset: 0x0007490E
		[DataMember]
		public bool ManualPerfTracerEnabled { get; set; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x00076717 File Offset: 0x00074917
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x0007671F File Offset: 0x0007491F
		[DataMember]
		public int InstrumentationSendIntervalSeconds { get; set; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x00076728 File Offset: 0x00074928
		// (set) Token: 0x06001E47 RID: 7751 RVA: 0x00076730 File Offset: 0x00074930
		[DataMember]
		public string StaticMapUrl { get; set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x00076739 File Offset: 0x00074939
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x00076741 File Offset: 0x00074941
		[DataMember]
		public string MapControlKey { get; set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0007674A File Offset: 0x0007494A
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x00076752 File Offset: 0x00074952
		[DataMember]
		public string DirectionsPageUrl { get; set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x0007675B File Offset: 0x0007495B
		// (set) Token: 0x06001E4D RID: 7757 RVA: 0x00076763 File Offset: 0x00074963
		[DataMember]
		public bool CheckForForgottenAttachmentsEnabled { get; set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x0007676C File Offset: 0x0007496C
		// (set) Token: 0x06001E4F RID: 7759 RVA: 0x00076774 File Offset: 0x00074974
		[DataMember]
		public bool ControlTasksQueueDisabled { get; set; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x0007677D File Offset: 0x0007497D
		// (set) Token: 0x06001E51 RID: 7761 RVA: 0x00076785 File Offset: 0x00074985
		[DataMember]
		public bool CloseWindowOnLogout { get; set; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x0007678E File Offset: 0x0007498E
		// (set) Token: 0x06001E53 RID: 7763 RVA: 0x00076796 File Offset: 0x00074996
		[DataMember]
		public bool IsLegacySignOut { get; set; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0007679F File Offset: 0x0007499F
		// (set) Token: 0x06001E55 RID: 7765 RVA: 0x000767A7 File Offset: 0x000749A7
		[DataMember]
		public WebBeaconFilterLevels FilterWebBeaconsAndHtmlForms { get; set; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000767B0 File Offset: 0x000749B0
		// (set) Token: 0x06001E57 RID: 7767 RVA: 0x000767B8 File Offset: 0x000749B8
		[DataMember]
		public int FindFolderCountLimit { get; set; }
	}
}
