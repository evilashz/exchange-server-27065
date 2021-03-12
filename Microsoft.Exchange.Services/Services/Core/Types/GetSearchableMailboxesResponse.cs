using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000511 RID: 1297
	[XmlType(TypeName = "GetSearchableMailboxesResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetSearchableMailboxesResponse : ResponseMessage
	{
		// Token: 0x06002553 RID: 9555 RVA: 0x000A58F9 File Offset: 0x000A3AF9
		public GetSearchableMailboxesResponse()
		{
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000A590C File Offset: 0x000A3B0C
		internal GetSearchableMailboxesResponse(ServiceResultCode code, ServiceError error, SearchableMailbox[] mailboxes, FailedSearchMailbox[] failedMailboxes) : base(code, error)
		{
			if (mailboxes != null && mailboxes.Length > 0)
			{
				this.searchableMailboxes.AddRange(mailboxes);
			}
			if (failedMailboxes != null && failedMailboxes.Length > 0)
			{
				this.FailedMailboxes = failedMailboxes;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000A5949 File Offset: 0x000A3B49
		// (set) Token: 0x06002556 RID: 9558 RVA: 0x000A5956 File Offset: 0x000A3B56
		[DataMember(Name = "SearchableMailboxes", IsRequired = false)]
		[XmlArray]
		[XmlArrayItem("SearchableMailbox", Type = typeof(SearchableMailbox), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public SearchableMailbox[] SearchableMailboxes
		{
			get
			{
				return this.searchableMailboxes.ToArray();
			}
			set
			{
				this.searchableMailboxes.Clear();
				if (value != null && value.Length > 0)
				{
					this.searchableMailboxes.AddRange(value);
				}
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000A5978 File Offset: 0x000A3B78
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x000A5980 File Offset: 0x000A3B80
		[XmlArray]
		[DataMember(Name = "FailedMailboxes", EmitDefaultValue = false, IsRequired = false)]
		[XmlArrayItem(ElementName = "FailedMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(FailedSearchMailbox))]
		public FailedSearchMailbox[] FailedMailboxes { get; set; }

		// Token: 0x040015B7 RID: 5559
		private List<SearchableMailbox> searchableMailboxes = new List<SearchableMailbox>();
	}
}
