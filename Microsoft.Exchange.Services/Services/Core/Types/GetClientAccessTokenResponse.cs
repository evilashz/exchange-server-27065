using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004EB RID: 1259
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetClientAccessTokenResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetClientAccessTokenResponse : BaseInfoResponse
	{
		// Token: 0x060024AC RID: 9388 RVA: 0x000A511B File Offset: 0x000A331B
		public GetClientAccessTokenResponse() : base(ResponseType.GetClientAccessTokenResponseMessage)
		{
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000A5125 File Offset: 0x000A3325
		internal override void ProcessServiceResult<TValue>(ServiceResult<TValue> result)
		{
			base.AddResponse(this.CreateResponseMessage<TValue>(result.Code, result.Error, result.Value));
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000A5145 File Offset: 0x000A3345
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new GetClientAccessTokenResponseMessage(code, error, value as ClientAccessTokenResponseType);
		}
	}
}
