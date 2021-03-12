using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000425 RID: 1061
	[XmlType("GetAggregatedAccountRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAggregatedAccountRequest : BaseAggregatedAccountRequest
	{
		// Token: 0x06001F16 RID: 7958 RVA: 0x000A091C File Offset: 0x0009EB1C
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetAggregatedAccount(callContext, this);
		}
	}
}
