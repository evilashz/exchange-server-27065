using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000071 RID: 113
	public class Url : IPositionedExtraction
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000DD84 File Offset: 0x0000BF84
		[XmlElement(ElementName = "UrlString")]
		public string UrlString { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000DD8D File Offset: 0x0000BF8D
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000DD95 File Offset: 0x0000BF95
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000DD9E File Offset: 0x0000BF9E
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000DDA6 File Offset: 0x0000BFA6
		[DefaultValue(EmailPosition.LatestReply)]
		[XmlAttribute]
		public EmailPosition Position { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000DDAF File Offset: 0x0000BFAF
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000DDB7 File Offset: 0x0000BFB7
		[XmlAttribute]
		[DefaultValue(UrlType.Unspecified)]
		public UrlType Type { get; set; }

		// Token: 0x04000176 RID: 374
		private int startIndex = -1;
	}
}
