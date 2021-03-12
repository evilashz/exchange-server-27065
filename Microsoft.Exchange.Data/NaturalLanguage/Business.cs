using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200006C RID: 108
	public class Business : IPositionedExtraction
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000DC94 File Offset: 0x0000BE94
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		[XmlElement(ElementName = "BusinessString")]
		public string BusinessString { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000DCA5 File Offset: 0x0000BEA5
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000DCAD File Offset: 0x0000BEAD
		[DefaultValue(-1)]
		[XmlAttribute]
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000DCB6 File Offset: 0x0000BEB6
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000DCBE File Offset: 0x0000BEBE
		[DefaultValue(EmailPosition.LatestReply)]
		[XmlAttribute]
		public EmailPosition Position { get; set; }

		// Token: 0x04000161 RID: 353
		private int startIndex = -1;
	}
}
