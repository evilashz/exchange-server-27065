using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000068 RID: 104
	public class Meeting : IPositionedExtraction
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000DB60 File Offset: 0x0000BD60
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000DB68 File Offset: 0x0000BD68
		public string MeetingString { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000DB71 File Offset: 0x0000BD71
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000DB79 File Offset: 0x0000BD79
		public EmailUser[] Attendees { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000DB82 File Offset: 0x0000BD82
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000DB8A File Offset: 0x0000BD8A
		[XmlAttribute]
		public string Location { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000DB93 File Offset: 0x0000BD93
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000DB9B File Offset: 0x0000BD9B
		[XmlAttribute]
		public string Subject { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		public DateTime? StartTime { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000DBB5 File Offset: 0x0000BDB5
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000DBBD File Offset: 0x0000BDBD
		public DateTime? EndTime { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000DBC6 File Offset: 0x0000BDC6
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000DBCE File Offset: 0x0000BDCE
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000DBD7 File Offset: 0x0000BDD7
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000DBDF File Offset: 0x0000BDDF
		[XmlAttribute]
		[DefaultValue(EmailPosition.LatestReply)]
		public EmailPosition Position { get; set; }

		// Token: 0x04000152 RID: 338
		private int startIndex = -1;
	}
}
