using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F6 RID: 2038
	[DataContract(Name = "MailboxQuery", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "MailboxQueryType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MailboxQuery
	{
		// Token: 0x06003B99 RID: 15257 RVA: 0x000D02FA File Offset: 0x000CE4FA
		public MailboxQuery()
		{
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x000D0302 File Offset: 0x000CE502
		public MailboxQuery(string query, MailboxSearchScope[] mailboxSearchScopes)
		{
			this.query = query;
			this.mailboxSearchScopes = mailboxSearchScopes;
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06003B9B RID: 15259 RVA: 0x000D0318 File Offset: 0x000CE518
		// (set) Token: 0x06003B9C RID: 15260 RVA: 0x000D0320 File Offset: 0x000CE520
		[XmlElement("Query")]
		[DataMember(Name = "Query", IsRequired = true)]
		public string Query
		{
			get
			{
				return this.query;
			}
			set
			{
				this.query = value;
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x000D0329 File Offset: 0x000CE529
		// (set) Token: 0x06003B9E RID: 15262 RVA: 0x000D0331 File Offset: 0x000CE531
		[DataMember(Name = "MailboxSearchScopes", IsRequired = true)]
		[XmlArray(ElementName = "MailboxSearchScopes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "MailboxSearchScope", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(MailboxSearchScope))]
		public MailboxSearchScope[] MailboxSearchScopes
		{
			get
			{
				return this.mailboxSearchScopes;
			}
			set
			{
				this.mailboxSearchScopes = value;
			}
		}

		// Token: 0x040020DC RID: 8412
		private string query;

		// Token: 0x040020DD RID: 8413
		private MailboxSearchScope[] mailboxSearchScopes;
	}
}
