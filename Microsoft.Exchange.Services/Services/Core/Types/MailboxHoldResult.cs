using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F1 RID: 2033
	[XmlType(TypeName = "MailboxHoldResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "MailboxHoldResult", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class MailboxHoldResult
	{
		// Token: 0x06003B7C RID: 15228 RVA: 0x000D01B7 File Offset: 0x000CE3B7
		public MailboxHoldResult()
		{
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000D01BF File Offset: 0x000CE3BF
		internal MailboxHoldResult(string holdId, string query, MailboxHoldStatus[] statuses)
		{
			this.holdId = holdId;
			this.query = query;
			this.statuses = statuses;
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003B7E RID: 15230 RVA: 0x000D01DC File Offset: 0x000CE3DC
		// (set) Token: 0x06003B7F RID: 15231 RVA: 0x000D01E4 File Offset: 0x000CE3E4
		[DataMember(Name = "HoldId", IsRequired = true)]
		[XmlElement("HoldId")]
		public string HoldId
		{
			get
			{
				return this.holdId;
			}
			set
			{
				this.holdId = value;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003B80 RID: 15232 RVA: 0x000D01ED File Offset: 0x000CE3ED
		// (set) Token: 0x06003B81 RID: 15233 RVA: 0x000D01F5 File Offset: 0x000CE3F5
		[XmlElement("Query")]
		[DataMember(Name = "Query", IsRequired = false)]
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

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x000D01FE File Offset: 0x000CE3FE
		// (set) Token: 0x06003B83 RID: 15235 RVA: 0x000D0206 File Offset: 0x000CE406
		[DataMember(Name = "Statuses", IsRequired = false)]
		[XmlArray(ElementName = "MailboxHoldStatuses", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "MailboxHoldStatus", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(MailboxHoldStatus))]
		public MailboxHoldStatus[] Statuses
		{
			get
			{
				return this.statuses;
			}
			set
			{
				this.statuses = value;
			}
		}

		// Token: 0x040020D0 RID: 8400
		private string holdId;

		// Token: 0x040020D1 RID: 8401
		private string query;

		// Token: 0x040020D2 RID: 8402
		private MailboxHoldStatus[] statuses;
	}
}
