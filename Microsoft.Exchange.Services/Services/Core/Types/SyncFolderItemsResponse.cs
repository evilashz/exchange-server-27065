using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200056A RID: 1386
	[XmlType("SyncFolderItemsResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncFolderItemsResponse : BaseInfoResponse
	{
		// Token: 0x060026C9 RID: 9929 RVA: 0x000A69F2 File Offset: 0x000A4BF2
		public SyncFolderItemsResponse() : base(ResponseType.SyncFolderItemsResponseMessage)
		{
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000A69FC File Offset: 0x000A4BFC
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new SyncFolderItemsResponseMessage(code, error, value as SyncFolderItemsChangesType);
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000A6A10 File Offset: 0x000A4C10
		internal override void ProcessServiceResult<TValue>(ServiceResult<TValue> result)
		{
			base.AddResponse(this.CreateResponseMessage<TValue>(result.Code, result.Error, result.Value));
		}
	}
}
