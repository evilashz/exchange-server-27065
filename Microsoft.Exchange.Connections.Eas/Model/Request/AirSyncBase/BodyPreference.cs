using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase
{
	// Token: 0x0200009B RID: 155
	[XmlType(Namespace = "AirSyncBase", TypeName = "BodyPreference")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class BodyPreference
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00009F7F File Offset: 0x0000817F
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00009F87 File Offset: 0x00008187
		[XmlElement(ElementName = "Type")]
		public byte? Type { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00009F90 File Offset: 0x00008190
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00009F98 File Offset: 0x00008198
		[XmlElement(ElementName = "TruncationSize")]
		public uint? TruncationSize { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00009FA1 File Offset: 0x000081A1
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00009FA9 File Offset: 0x000081A9
		[XmlIgnore]
		public bool? AllOrNone { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00009FB4 File Offset: 0x000081B4
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00009FF2 File Offset: 0x000081F2
		[XmlElement(ElementName = "AllOrNone")]
		public string SerializableAllOrNone
		{
			get
			{
				if (this.AllOrNone == null)
				{
					return "0";
				}
				if (!this.AllOrNone.Value)
				{
					return "0";
				}
				return "1";
			}
			set
			{
				this.AllOrNone = new bool?(XmlConvert.ToBoolean(value));
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000A005 File Offset: 0x00008205
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000A00D File Offset: 0x0000820D
		[XmlElement(ElementName = "Restriction")]
		public string Restriction { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000A016 File Offset: 0x00008216
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000A01E File Offset: 0x0000821E
		[XmlElement(ElementName = "Preview")]
		public uint? Preview { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000A028 File Offset: 0x00008228
		[XmlIgnore]
		public bool TypeSpecified
		{
			get
			{
				return this.Type != null;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000A044 File Offset: 0x00008244
		[XmlIgnore]
		public bool TruncationSizeSpecified
		{
			get
			{
				return this.TruncationSize != null;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000A060 File Offset: 0x00008260
		[XmlIgnore]
		public bool SerializableAllOrNoneSpecified
		{
			get
			{
				return this.AllOrNone != null;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000A07C File Offset: 0x0000827C
		[XmlIgnore]
		public bool PreviewSpecified
		{
			get
			{
				return this.Preview != null;
			}
		}
	}
}
