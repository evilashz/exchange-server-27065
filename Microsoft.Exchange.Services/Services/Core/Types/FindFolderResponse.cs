using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D9 RID: 1241
	[XmlType("FindFolderResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindFolderResponse : BaseResponseMessage
	{
		// Token: 0x0600245B RID: 9307 RVA: 0x000A4C6D File Offset: 0x000A2E6D
		public FindFolderResponse() : base(ResponseType.FindFolderResponseMessage)
		{
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000A4C77 File Offset: 0x000A2E77
		internal static FindFolderResponse CreateResponse()
		{
			return new FindFolderResponse();
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000A4C80 File Offset: 0x000A2E80
		internal static BaseResponseMessage CreateResponseForFindFolder(ServiceResult<FindFolderParentWrapper>[] serviceResults, FindFolderResponse.CreateFindFolderResponse createResponse)
		{
			FindFolderResponse findFolderResponse = createResponse();
			ServiceResult<FindFolderParentWrapper>.ProcessServiceResults(serviceResults, new ProcessServiceResult<FindFolderParentWrapper>(findFolderResponse.ProcessServiceResult));
			return findFolderResponse;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000A4CA7 File Offset: 0x000A2EA7
		internal void ProcessServiceResult(ServiceResult<FindFolderParentWrapper> result)
		{
			base.AddResponse(new FindFolderResponseMessage(result.Code, result.Error, result.Value));
		}

		// Token: 0x020004DA RID: 1242
		// (Invoke) Token: 0x06002460 RID: 9312
		internal delegate FindFolderResponse CreateFindFolderResponse();
	}
}
