using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000492 RID: 1170
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("UnsubscribeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UnsubscribeRequest : BasePullRequest
	{
		// Token: 0x060022E7 RID: 8935 RVA: 0x000A3630 File Offset: 0x000A1830
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new Unsubscribe(callContext, this);
		}
	}
}
