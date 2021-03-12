using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D8D RID: 3469
	[MessageContract(IsWrapped = false)]
	public class SaveUMPinSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400311F RID: 12575
		[MessageBodyMember(Name = "SaveUMPinResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SaveUMPinResponseMessage Body;
	}
}
