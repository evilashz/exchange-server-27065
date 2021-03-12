using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200054C RID: 1356
	[DataContract(Name = "SearchMailboxesResponseMessage", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "SearchMailboxesResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SearchMailboxesResponseMessage : ResponseMessage
	{
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x000A64A0 File Offset: 0x000A46A0
		// (set) Token: 0x06002644 RID: 9796 RVA: 0x000A64A8 File Offset: 0x000A46A8
		[XmlElement("SearchMailboxesResult")]
		[DataMember(Name = "SearchMailboxesResult", IsRequired = false)]
		public SearchMailboxesResult SearchMailboxesResult
		{
			get
			{
				return this.searchMailboxesResult;
			}
			set
			{
				this.searchMailboxesResult = value;
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000A64B1 File Offset: 0x000A46B1
		public SearchMailboxesResponseMessage()
		{
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000A64B9 File Offset: 0x000A46B9
		internal SearchMailboxesResponseMessage(ServiceResultCode code, ServiceError error, SearchMailboxesResult results) : base(code, error)
		{
			this.searchMailboxesResult = results;
		}

		// Token: 0x040018A1 RID: 6305
		private SearchMailboxesResult searchMailboxesResult;
	}
}
