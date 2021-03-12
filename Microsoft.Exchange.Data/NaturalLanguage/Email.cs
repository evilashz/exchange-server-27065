using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200006F RID: 111
	public class Email : IPositionedExtraction
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000DD3A File Offset: 0x0000BF3A
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000DD42 File Offset: 0x0000BF42
		[XmlElement(ElementName = "EmailString")]
		public string EmailString { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000DD4B File Offset: 0x0000BF4B
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000DD53 File Offset: 0x0000BF53
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

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000DD64 File Offset: 0x0000BF64
		[XmlAttribute]
		[DefaultValue(EmailPosition.LatestReply)]
		public EmailPosition Position { get; set; }

		// Token: 0x0400016F RID: 367
		private int startIndex = -1;
	}
}
