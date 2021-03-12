using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000282 RID: 642
	[XmlType(TypeName = "UserConfig")]
	[Serializable]
	public sealed class UserConfigXML : XMLSerializableBase
	{
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00089387 File Offset: 0x00087587
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x0008938F File Offset: 0x0008758F
		[XmlElement(ElementName = "UpgradeDetails")]
		public string UpgradeDetails { get; set; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00089398 File Offset: 0x00087598
		// (set) Token: 0x06001E8C RID: 7820 RVA: 0x000893A0 File Offset: 0x000875A0
		[XmlElement(ElementName = "UpgradeMessage")]
		public string UpgradeMessage { get; set; }

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x000893A9 File Offset: 0x000875A9
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x000893B1 File Offset: 0x000875B1
		[XmlElement(ElementName = "UpgradeStage")]
		public UpgradeStage? UpgradeStage { get; set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x000893BA File Offset: 0x000875BA
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x000893C2 File Offset: 0x000875C2
		[XmlElement(ElementName = "MailboxProvisioningConstraints")]
		public MailboxProvisioningConstraints MailboxProvisioningConstraints { get; set; }

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x000893CB File Offset: 0x000875CB
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x000893D3 File Offset: 0x000875D3
		[XmlElement(ElementName = "UpgradeStageTimeStamp")]
		public DateTime? UpgradeStageTimeStamp { get; set; }

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x000893DC File Offset: 0x000875DC
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x000893E4 File Offset: 0x000875E4
		[XmlElement(ElementName = "RelocationLastWriteTime")]
		public DateTime RelocationLastWriteTime { get; set; }

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x000893ED File Offset: 0x000875ED
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x000893F5 File Offset: 0x000875F5
		[XmlElement(ElementName = "ReleaseTrack")]
		public ReleaseTrack? ReleaseTrack { get; set; }

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x000893FE File Offset: 0x000875FE
		// (set) Token: 0x06001E98 RID: 7832 RVA: 0x00089406 File Offset: 0x00087606
		[XmlElement(ElementName = "RelocationShadowPropMetaData")]
		public PropertyMetaData[] RelocationShadowPropMetaData { get; set; }

		// Token: 0x06001E99 RID: 7833 RVA: 0x00089410 File Offset: 0x00087610
		public bool ShouldSerializeUpgradeStage()
		{
			return this.UpgradeStage != null;
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0008942C File Offset: 0x0008762C
		public bool ShouldSerializeReleaseTrack()
		{
			return this.ReleaseTrack != null;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00089448 File Offset: 0x00087648
		public bool ShouldSerializeUpgradeStageTimeStamp()
		{
			return this.UpgradeStageTimeStamp != null;
		}
	}
}
