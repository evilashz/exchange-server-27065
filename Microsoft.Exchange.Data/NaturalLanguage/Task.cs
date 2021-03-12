using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000067 RID: 103
	public class Task : IPositionedExtraction
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000DB0D File Offset: 0x0000BD0D
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000DB15 File Offset: 0x0000BD15
		public string TaskString { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000DB1E File Offset: 0x0000BD1E
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000DB26 File Offset: 0x0000BD26
		public EmailUser[] Assignees { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000DB2F File Offset: 0x0000BD2F
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000DB37 File Offset: 0x0000BD37
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000DB40 File Offset: 0x0000BD40
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000DB48 File Offset: 0x0000BD48
		[DefaultValue(EmailPosition.LatestReply)]
		[XmlAttribute]
		public EmailPosition Position { get; set; }

		// Token: 0x0400014E RID: 334
		private int startIndex = -1;
	}
}
