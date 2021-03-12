using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000283 RID: 643
	[XmlType(TypeName = "Resource")]
	public class ResourceDiagnosticInfoXML : XMLSerializableBase
	{
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x000435D7 File Offset: 0x000417D7
		// (set) Token: 0x06001FAB RID: 8107 RVA: 0x000435DF File Offset: 0x000417DF
		[XmlAttribute(AttributeName = "Name")]
		public string ResourceName { get; set; }

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x000435E8 File Offset: 0x000417E8
		// (set) Token: 0x06001FAD RID: 8109 RVA: 0x000435F0 File Offset: 0x000417F0
		[XmlAttribute(AttributeName = "Guid")]
		public Guid ResourceGuid { get; set; }

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x000435F9 File Offset: 0x000417F9
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x00043601 File Offset: 0x00041801
		[XmlAttribute(AttributeName = "Type")]
		public string ResourceType { get; set; }

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0004360A File Offset: 0x0004180A
		// (set) Token: 0x06001FB1 RID: 8113 RVA: 0x00043612 File Offset: 0x00041812
		[XmlAttribute(AttributeName = "StaticCapacity")]
		public int StaticCapacity { get; set; }

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0004361B File Offset: 0x0004181B
		// (set) Token: 0x06001FB3 RID: 8115 RVA: 0x00043623 File Offset: 0x00041823
		[XmlAttribute(AttributeName = "DynamicCapacity")]
		public int DynamicCapacity { get; set; }

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0004362C File Offset: 0x0004182C
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x00043634 File Offset: 0x00041834
		[XmlAttribute(AttributeName = "Utilization")]
		public int Utilization { get; set; }

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0004363D File Offset: 0x0004183D
		// (set) Token: 0x06001FB7 RID: 8119 RVA: 0x00043645 File Offset: 0x00041845
		[XmlAttribute(AttributeName = "IsUnhealthy")]
		public bool IsUnhealthy { get; set; }

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x0004364E File Offset: 0x0004184E
		// (set) Token: 0x06001FB9 RID: 8121 RVA: 0x00043656 File Offset: 0x00041856
		[XmlAttribute(AttributeName = "WlmWorkloadType")]
		public string WlmWorkloadType { get; set; }

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x0004365F File Offset: 0x0004185F
		// (set) Token: 0x06001FBB RID: 8123 RVA: 0x00043667 File Offset: 0x00041867
		[XmlAttribute(AttributeName = "BytesPerMin")]
		public uint TransferRatePerMin { get; set; }

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x00043670 File Offset: 0x00041870
		// (set) Token: 0x06001FBD RID: 8125 RVA: 0x00043678 File Offset: 0x00041878
		[XmlArray("WlmResources")]
		public List<WlmResourceHealthMonitorDiagnosticInfoXML> WlmResourceHealthMonitors { get; set; }
	}
}
