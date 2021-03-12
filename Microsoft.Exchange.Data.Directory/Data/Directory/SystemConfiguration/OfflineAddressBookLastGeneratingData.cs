using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000517 RID: 1303
	[XmlType(TypeName = "OfflineAddressBookLastGeneratingData")]
	public class OfflineAddressBookLastGeneratingData : XMLSerializableBase
	{
		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x000DE966 File Offset: 0x000DCB66
		// (set) Token: 0x060039A4 RID: 14756 RVA: 0x000DE96E File Offset: 0x000DCB6E
		[XmlElement(ElementName = "MailboxGuid")]
		public Guid? MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
			set
			{
				this.mailboxGuid = value;
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x060039A5 RID: 14757 RVA: 0x000DE977 File Offset: 0x000DCB77
		// (set) Token: 0x060039A6 RID: 14758 RVA: 0x000DE97F File Offset: 0x000DCB7F
		[XmlElement(ElementName = "DatabaseGuid")]
		public Guid? DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
			set
			{
				this.databaseGuid = value;
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000DE988 File Offset: 0x000DCB88
		// (set) Token: 0x060039A8 RID: 14760 RVA: 0x000DE990 File Offset: 0x000DCB90
		[XmlElement(ElementName = "ServerFqdn")]
		public string ServerFqdn
		{
			get
			{
				return this.serverFqdn;
			}
			set
			{
				this.serverFqdn = value;
			}
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000DE99C File Offset: 0x000DCB9C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"MailboxGuid: ",
				(this.mailboxGuid != null) ? this.mailboxGuid.ToString() : "",
				"; DatabaseGuid: ",
				(this.databaseGuid != null) ? this.databaseGuid.ToString() : "",
				"; Server: ",
				this.serverFqdn
			});
		}

		// Token: 0x04002768 RID: 10088
		private Guid? mailboxGuid;

		// Token: 0x04002769 RID: 10089
		private Guid? databaseGuid;

		// Token: 0x0400276A RID: 10090
		private string serverFqdn;
	}
}
