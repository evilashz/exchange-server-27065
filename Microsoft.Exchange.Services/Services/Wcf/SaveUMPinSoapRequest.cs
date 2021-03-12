using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D8C RID: 3468
	[MessageContract(IsWrapped = false)]
	public class SaveUMPinSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400311E RID: 12574
		[MessageBodyMember(Name = "SaveUMPin", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SaveUMPinRequest Body;
	}
}
