using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D90 RID: 3472
	[MessageContract(IsWrapped = false)]
	public class GetUMSubscriberCallAnsweringDataSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003122 RID: 12578
		[MessageBodyMember(Name = "GetUMSubscriberCallAnsweringData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMSubscriberCallAnsweringDataRequest Body;
	}
}
