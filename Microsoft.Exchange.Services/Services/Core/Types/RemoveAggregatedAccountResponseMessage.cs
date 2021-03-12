using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200053C RID: 1340
	[XmlType("RemoveAggregatedAccountResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class RemoveAggregatedAccountResponseMessage : ResponseMessage
	{
		// Token: 0x06002620 RID: 9760 RVA: 0x000A6343 File Offset: 0x000A4543
		public RemoveAggregatedAccountResponseMessage()
		{
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000A634B File Offset: 0x000A454B
		internal RemoveAggregatedAccountResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
