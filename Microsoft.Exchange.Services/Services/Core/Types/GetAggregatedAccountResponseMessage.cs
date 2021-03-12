using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E5 RID: 1253
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetAggregatedAccountResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetAggregatedAccountResponseMessage : ResponseMessage
	{
		// Token: 0x06002496 RID: 9366 RVA: 0x000A500E File Offset: 0x000A320E
		public GetAggregatedAccountResponseMessage()
		{
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000A5016 File Offset: 0x000A3216
		internal GetAggregatedAccountResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
