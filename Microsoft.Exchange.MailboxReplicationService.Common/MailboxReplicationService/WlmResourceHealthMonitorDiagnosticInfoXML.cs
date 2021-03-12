using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200028A RID: 650
	[XmlType(TypeName = "WlmResourceHealthMonitor")]
	public class WlmResourceHealthMonitorDiagnosticInfoXML : XMLSerializableBase
	{
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x00043CD0 File Offset: 0x00041ED0
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x00043CD8 File Offset: 0x00041ED8
		[XmlAttribute(AttributeName = "Key")]
		public string WlmResourceKey { get; set; }

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00043CE1 File Offset: 0x00041EE1
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x00043CE9 File Offset: 0x00041EE9
		[XmlAttribute(AttributeName = "State")]
		public string LoadState { get; set; }

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x00043CF2 File Offset: 0x00041EF2
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x00043CFA File Offset: 0x00041EFA
		[XmlAttribute(AttributeName = "Load")]
		public double LoadRatio { get; set; }

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x00043D03 File Offset: 0x00041F03
		// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x00043D0B File Offset: 0x00041F0B
		[XmlAttribute(AttributeName = "Metric")]
		public string Metric { get; set; }

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x00043D14 File Offset: 0x00041F14
		// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x00043D1C File Offset: 0x00041F1C
		[XmlAttribute(AttributeName = "DynamicCapacity")]
		public double DynamicCapacity { get; set; }

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x00043D25 File Offset: 0x00041F25
		// (set) Token: 0x06001FF9 RID: 8185 RVA: 0x00043D2D File Offset: 0x00041F2D
		[XmlAttribute(AttributeName = "IsDisabled")]
		public string IsDisabled { get; set; }

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x00043D36 File Offset: 0x00041F36
		// (set) Token: 0x06001FFB RID: 8187 RVA: 0x00043D3E File Offset: 0x00041F3E
		[XmlAttribute(AttributeName = "DynamicThrottlingDisabled")]
		public string DynamicThrottingDisabled { get; set; }

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x00043D47 File Offset: 0x00041F47
		// (set) Token: 0x06001FFD RID: 8189 RVA: 0x00043D4F File Offset: 0x00041F4F
		[XmlElement(ElementName = "HealthStats")]
		public WlmHealthStatistics WlmHealthStatistics { get; set; }
	}
}
