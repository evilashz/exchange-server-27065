using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001DF RID: 479
	[Serializable]
	public sealed class FolderToMailboxMapping : XMLSerializableBase
	{
		// Token: 0x060013F4 RID: 5108 RVA: 0x0002DDB0 File Offset: 0x0002BFB0
		private FolderToMailboxMapping()
		{
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
		public FolderToMailboxMapping(string folderName, Guid mailboxGuid)
		{
			this.FolderName = folderName;
			this.MailboxGuid = mailboxGuid;
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0002DDCE File Offset: 0x0002BFCE
		// (set) Token: 0x060013F7 RID: 5111 RVA: 0x0002DDD6 File Offset: 0x0002BFD6
		[XmlElement(ElementName = "FolderName")]
		public string FolderName { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0002DDDF File Offset: 0x0002BFDF
		// (set) Token: 0x060013F9 RID: 5113 RVA: 0x0002DDE7 File Offset: 0x0002BFE7
		[XmlElement(ElementName = "MailboxGuid")]
		public Guid MailboxGuid { get; set; }
	}
}
