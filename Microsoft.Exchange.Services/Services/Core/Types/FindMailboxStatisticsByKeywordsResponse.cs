using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004DF RID: 1247
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "FindMailboxStatisticsByKeywordsResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FindMailboxStatisticsByKeywordsResponse : BaseResponseMessage
	{
		// Token: 0x06002479 RID: 9337 RVA: 0x000A4DFD File Offset: 0x000A2FFD
		public FindMailboxStatisticsByKeywordsResponse() : base(ResponseType.FindMailboxStatisticsByKeywordsResponseMessage)
		{
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000A4E3C File Offset: 0x000A303C
		internal void AddResponses(UserMailbox userMailbox, ServiceResult<KeywordStatisticsSearchResult>[] serviceResults)
		{
			ServiceResult<KeywordStatisticsSearchResult>.ProcessServiceResults(serviceResults, delegate(ServiceResult<KeywordStatisticsSearchResult> result)
			{
				if (result != null)
				{
					this.AddResponse(new FindMailboxStatisticsByKeywordsResponseMessage(result.Code, result.Error, userMailbox, result.Value));
				}
			});
		}
	}
}
