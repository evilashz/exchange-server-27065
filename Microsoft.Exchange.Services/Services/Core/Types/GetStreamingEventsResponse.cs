using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000517 RID: 1303
	[XmlType("GetStreamingEventsResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlRoot(ElementName = "GetStreamingEventsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetStreamingEventsResponse : BaseInfoResponse
	{
		// Token: 0x06002572 RID: 9586 RVA: 0x000A5AAA File Offset: 0x000A3CAA
		public GetStreamingEventsResponse() : base(ResponseType.GetStreamingEventsResponseMessage)
		{
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000A5AB4 File Offset: 0x000A3CB4
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new GetStreamingEventsResponseMessage(code, error);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000A5ABD File Offset: 0x000A3CBD
		internal override void ProcessServiceResult<TValue>(ServiceResult<TValue> result)
		{
			base.AddResponse(this.CreateResponseMessage<TValue>(result.Code, result.Error, result.Value));
		}
	}
}
