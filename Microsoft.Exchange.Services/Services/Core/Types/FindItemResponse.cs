using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004DC RID: 1244
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("FindItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class FindItemResponse : BaseResponseMessage
	{
		// Token: 0x06002467 RID: 9319 RVA: 0x000A4CF0 File Offset: 0x000A2EF0
		public FindItemResponse() : base(ResponseType.FindItemResponseMessage)
		{
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000A4CFA File Offset: 0x000A2EFA
		internal static FindItemResponse CreateResponse()
		{
			return new FindItemResponse();
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000A4D04 File Offset: 0x000A2F04
		internal static BaseResponseMessage CreateResponseForFindItem(ServiceResult<FindItemParentWrapper>[] serviceResults, FindItemResponse.CreateFindItemResponse createResponse, HighlightTermType[] highlightTerms, bool isSearchInProgress, FolderId searchFolderId)
		{
			FindItemResponse findItemResponse = createResponse();
			findItemResponse.highlightTerms = highlightTerms;
			findItemResponse.isSearchInProgress = isSearchInProgress;
			findItemResponse.searchFolderId = searchFolderId;
			ServiceResult<FindItemParentWrapper>.ProcessServiceResults(serviceResults, new ProcessServiceResult<FindItemParentWrapper>(findItemResponse.ProcessServiceResult));
			return findItemResponse;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000A4D41 File Offset: 0x000A2F41
		internal void ProcessServiceResult(ServiceResult<FindItemParentWrapper> result)
		{
			base.AddResponse(new FindItemResponseMessage(result.Code, result.Error, result.Value, this.highlightTerms, this.isSearchInProgress, this.searchFolderId));
		}

		// Token: 0x0400157A RID: 5498
		private HighlightTermType[] highlightTerms;

		// Token: 0x0400157B RID: 5499
		private bool isSearchInProgress;

		// Token: 0x0400157C RID: 5500
		private FolderId searchFolderId;

		// Token: 0x020004DD RID: 1245
		// (Invoke) Token: 0x0600246C RID: 9324
		internal delegate FindItemResponse CreateFindItemResponse();
	}
}
