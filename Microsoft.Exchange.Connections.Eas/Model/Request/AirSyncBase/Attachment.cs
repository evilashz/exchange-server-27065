using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase
{
	// Token: 0x02000098 RID: 152
	[XmlType(Namespace = "AirSyncBase", TypeName = "Attachment")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Attachment
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00009DF3 File Offset: 0x00007FF3
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00009DFB File Offset: 0x00007FFB
		[XmlElement(ElementName = "DisplayName")]
		public string DisplayName { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00009E04 File Offset: 0x00008004
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00009E0C File Offset: 0x0000800C
		[XmlElement(ElementName = "FileReference")]
		public string FileReference { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00009E15 File Offset: 0x00008015
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00009E1D File Offset: 0x0000801D
		[XmlElement(ElementName = "Method")]
		public byte? Method { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00009E26 File Offset: 0x00008026
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00009E2E File Offset: 0x0000802E
		[XmlElement(ElementName = "EstimatedDataSize")]
		public uint? EstimatedDataSize { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00009E37 File Offset: 0x00008037
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00009E3F File Offset: 0x0000803F
		[XmlElement(ElementName = "ContentId")]
		public string ContentId { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00009E48 File Offset: 0x00008048
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00009E50 File Offset: 0x00008050
		[XmlElement(ElementName = "ContentLocation")]
		public string ContentLocation { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00009E59 File Offset: 0x00008059
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00009E61 File Offset: 0x00008061
		[XmlElement(ElementName = "IsInline")]
		public bool? IsInline { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00009E6C File Offset: 0x0000806C
		[XmlIgnore]
		public bool MethodSpecified
		{
			get
			{
				return this.Method != null;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00009E88 File Offset: 0x00008088
		[XmlIgnore]
		public bool EstimatedDataSizeSpecified
		{
			get
			{
				return this.EstimatedDataSize != null;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00009EA4 File Offset: 0x000080A4
		[XmlIgnore]
		public bool IsInlineSpecified
		{
			get
			{
				return this.IsInline != null;
			}
		}
	}
}
