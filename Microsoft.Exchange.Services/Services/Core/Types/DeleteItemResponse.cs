using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C9 RID: 1225
	[XmlType("DeleteItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteItemResponse : BaseResponseMessage
	{
		// Token: 0x06002414 RID: 9236 RVA: 0x000A475F File Offset: 0x000A295F
		public DeleteItemResponse() : base(ResponseType.DeleteItemResponseMessage)
		{
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000A478B File Offset: 0x000A298B
		internal void AddResponses(ServiceResult<DeleteItemResponseMessage>[] results)
		{
			ServiceResult<DeleteItemResponseMessage>.ProcessServiceResults(results, delegate(ServiceResult<DeleteItemResponseMessage> result)
			{
				base.AddResponse(result.Value ?? new DeleteItemResponseMessage(result.Code, result.Error));
			});
		}
	}
}
