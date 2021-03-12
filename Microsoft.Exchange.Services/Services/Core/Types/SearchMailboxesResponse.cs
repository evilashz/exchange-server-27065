using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200054B RID: 1355
	[DataContract(Name = "SearchMailboxesResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "SearchMailboxesResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SearchMailboxesResponse : BaseResponseMessage
	{
		// Token: 0x06002640 RID: 9792 RVA: 0x000A6460 File Offset: 0x000A4660
		public SearchMailboxesResponse() : base(ResponseType.SearchMailboxesResponseMessage)
		{
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000A648C File Offset: 0x000A468C
		internal void AddResponses(ServiceResult<SearchMailboxesResult>[] serviceResults)
		{
			ServiceResult<SearchMailboxesResult>.ProcessServiceResults(serviceResults, delegate(ServiceResult<SearchMailboxesResult> result)
			{
				if (result != null)
				{
					base.AddResponse(new SearchMailboxesResponseMessage(result.Code, result.Error, result.Value));
				}
			});
		}
	}
}
