using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F8 RID: 1272
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetEventsResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetEventsResponse : BaseInfoResponse
	{
		// Token: 0x060024EC RID: 9452 RVA: 0x000A54B0 File Offset: 0x000A36B0
		public GetEventsResponse() : base(ResponseType.GetEventsResponseMessage)
		{
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000A54BA File Offset: 0x000A36BA
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new GetEventsResponseMessage(code, error, value as EwsNotificationType);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000A54CE File Offset: 0x000A36CE
		internal override void ProcessServiceResult<TValue>(ServiceResult<TValue> result)
		{
			base.AddResponse(this.CreateResponseMessage<TValue>(result.Code, result.Error, result.Value));
		}
	}
}
