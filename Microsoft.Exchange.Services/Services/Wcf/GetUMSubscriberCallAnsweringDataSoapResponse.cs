using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D91 RID: 3473
	[MessageContract(IsWrapped = false)]
	public class GetUMSubscriberCallAnsweringDataSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003123 RID: 12579
		[MessageBodyMember(Name = "GetUMSubscriberCallAnsweringDataResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMSubscriberCallAnsweringDataResponseMessage Body;
	}
}
