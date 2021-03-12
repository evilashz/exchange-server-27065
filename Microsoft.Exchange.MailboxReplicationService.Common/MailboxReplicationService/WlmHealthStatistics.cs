using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000286 RID: 646
	[XmlType(TypeName = "WlmHealthStats")]
	[Serializable]
	public sealed class WlmHealthStatistics : XMLSerializableBase
	{
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x000439F8 File Offset: 0x00041BF8
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x00043A00 File Offset: 0x00041C00
		[XmlElement(ElementName = "A5M")]
		public WlmHealthStatistics.HealthAverages Avg5Min { get; set; }

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x00043A09 File Offset: 0x00041C09
		// (set) Token: 0x06001FCE RID: 8142 RVA: 0x00043A11 File Offset: 0x00041C11
		[XmlElement(ElementName = "A1H")]
		public WlmHealthStatistics.HealthAverages Avg1Hour { get; set; }

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x00043A1A File Offset: 0x00041C1A
		// (set) Token: 0x06001FD0 RID: 8144 RVA: 0x00043A22 File Offset: 0x00041C22
		[XmlElement(ElementName = "A1D")]
		public WlmHealthStatistics.HealthAverages Avg1Day { get; set; }

		// Token: 0x06001FD1 RID: 8145 RVA: 0x00043A2B File Offset: 0x00041C2B
		public override string ToString()
		{
			return string.Format("A5M:[{0}]; A1H:[{1}]; A1D:[{2}]", this.Avg5Min.ToString(), this.Avg1Hour.ToString(), this.Avg1Day.ToString());
		}

		// Token: 0x02000287 RID: 647
		[Serializable]
		public sealed class HealthAverages : XMLSerializableBase
		{
			// Token: 0x17000C1B RID: 3099
			// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x00043A60 File Offset: 0x00041C60
			// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x00043A68 File Offset: 0x00041C68
			[XmlAttribute(AttributeName = "G")]
			public int Underloaded { get; set; }

			// Token: 0x17000C1C RID: 3100
			// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x00043A71 File Offset: 0x00041C71
			// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x00043A79 File Offset: 0x00041C79
			[XmlAttribute(AttributeName = "Y")]
			public int Full { get; set; }

			// Token: 0x17000C1D RID: 3101
			// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00043A82 File Offset: 0x00041C82
			// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x00043A8A File Offset: 0x00041C8A
			[XmlAttribute(AttributeName = "R")]
			public int Overloaded { get; set; }

			// Token: 0x17000C1E RID: 3102
			// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x00043A93 File Offset: 0x00041C93
			// (set) Token: 0x06001FDA RID: 8154 RVA: 0x00043A9B File Offset: 0x00041C9B
			[XmlAttribute(AttributeName = "B")]
			public int Critical { get; set; }

			// Token: 0x17000C1F RID: 3103
			// (get) Token: 0x06001FDB RID: 8155 RVA: 0x00043AA4 File Offset: 0x00041CA4
			// (set) Token: 0x06001FDC RID: 8156 RVA: 0x00043AAC File Offset: 0x00041CAC
			[XmlAttribute(AttributeName = "U")]
			public int Unknown { get; set; }

			// Token: 0x17000C20 RID: 3104
			// (get) Token: 0x06001FDD RID: 8157 RVA: 0x00043AB5 File Offset: 0x00041CB5
			// (set) Token: 0x06001FDE RID: 8158 RVA: 0x00043ABD File Offset: 0x00041CBD
			[XmlAttribute(AttributeName = "Capacity")]
			public float AverageCapacity { get; set; }
		}
	}
}
