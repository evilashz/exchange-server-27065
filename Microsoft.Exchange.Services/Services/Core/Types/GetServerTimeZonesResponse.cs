using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000512 RID: 1298
	[XmlType("GetServerTimeZonesResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetServerTimeZonesResponse : BaseInfoResponse
	{
		// Token: 0x06002559 RID: 9561 RVA: 0x000A5989 File Offset: 0x000A3B89
		public GetServerTimeZonesResponse() : base(ResponseType.GetServerTimeZonesResponseMessage)
		{
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000A5993 File Offset: 0x000A3B93
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue timeZones)
		{
			return new GetServerTimeZonesResponseMessage(code, error, timeZones as GetServerTimeZoneResultType);
		}
	}
}
