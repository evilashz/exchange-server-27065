using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200006E RID: 110
	public class Phone : IPositionedExtraction
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000DCD6 File Offset: 0x0000BED6
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000DCDE File Offset: 0x0000BEDE
		[XmlElement(ElementName = "PhoneString")]
		public string PhoneString { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000DCE7 File Offset: 0x0000BEE7
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000DCEF File Offset: 0x0000BEEF
		[XmlElement(ElementName = "OriginalPhoneString")]
		public string OriginalPhoneString { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000DD00 File Offset: 0x0000BF00
		[XmlAttribute]
		[DefaultValue(-1)]
		public int StartIndex
		{
			get
			{
				return this.startIndex;
			}
			set
			{
				this.startIndex = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000DD09 File Offset: 0x0000BF09
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000DD11 File Offset: 0x0000BF11
		[DefaultValue(EmailPosition.LatestReply)]
		[XmlAttribute]
		public EmailPosition Position { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000DD1A File Offset: 0x0000BF1A
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000DD22 File Offset: 0x0000BF22
		[DefaultValue(PhoneType.Unspecified)]
		[XmlAttribute]
		public PhoneType Type { get; set; }

		// Token: 0x0400016A RID: 362
		private int startIndex = -1;
	}
}
