using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200006A RID: 106
	public class Address : IPositionedExtraction
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000DC10 File Offset: 0x0000BE10
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000DC18 File Offset: 0x0000BE18
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000DC21 File Offset: 0x0000BE21
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000DC29 File Offset: 0x0000BE29
		[XmlAttribute]
		[DefaultValue(EmailPosition.LatestReply)]
		public EmailPosition Position { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000DC32 File Offset: 0x0000BE32
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000DC3A File Offset: 0x0000BE3A
		[XmlText]
		public string AddressString { get; set; }

		// Token: 0x0400015B RID: 347
		private int startIndex = -1;
	}
}
