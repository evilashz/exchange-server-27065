using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200006B RID: 107
	public class Person : IPositionedExtraction
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000DC52 File Offset: 0x0000BE52
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000DC5A File Offset: 0x0000BE5A
		[XmlElement(ElementName = "PersonString")]
		public string PersonString { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000DC63 File Offset: 0x0000BE63
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000DC6B File Offset: 0x0000BE6B
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000DC74 File Offset: 0x0000BE74
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		[DefaultValue(EmailPosition.LatestReply)]
		[XmlAttribute]
		public EmailPosition Position { get; set; }

		// Token: 0x0400015E RID: 350
		private int startIndex = -1;
	}
}
