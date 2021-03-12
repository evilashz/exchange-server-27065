using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D4B RID: 3403
	[MessageContract(IsWrapped = false)]
	public class SetClientExtensionSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030DD RID: 12509
		[MessageBodyMember(Name = "SetClientExtensionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetClientExtensionResponse Body;
	}
}
