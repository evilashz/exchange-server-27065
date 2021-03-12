using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase
{
	// Token: 0x0200009A RID: 154
	[XmlType(Namespace = "AirSyncBase", TypeName = "Body")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Body
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00009EE0 File Offset: 0x000080E0
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00009EE8 File Offset: 0x000080E8
		[XmlElement(ElementName = "Type")]
		public byte? Type { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00009EF1 File Offset: 0x000080F1
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00009EF9 File Offset: 0x000080F9
		[XmlElement(ElementName = "Data")]
		public string Data { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00009F02 File Offset: 0x00008102
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00009F0A File Offset: 0x0000810A
		[XmlElement(ElementName = "EstimatedDataSize")]
		public uint? EstimatedDataSize { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00009F13 File Offset: 0x00008113
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00009F1B File Offset: 0x0000811B
		[XmlElement(ElementName = "Truncated")]
		public bool? Truncated { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00009F24 File Offset: 0x00008124
		[XmlIgnore]
		public bool TypeSpecified
		{
			get
			{
				return this.Type != null;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00009F40 File Offset: 0x00008140
		[XmlIgnore]
		public bool EstimatedDataSizeSpecified
		{
			get
			{
				return this.EstimatedDataSize != null;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00009F5C File Offset: 0x0000815C
		[XmlIgnore]
		public bool TruncatedSpecified
		{
			get
			{
				return this.Truncated != null;
			}
		}
	}
}
