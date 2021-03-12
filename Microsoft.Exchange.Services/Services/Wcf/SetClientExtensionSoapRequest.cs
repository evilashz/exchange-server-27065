using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D4A RID: 3402
	[MessageContract(IsWrapped = false)]
	public class SetClientExtensionSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030DC RID: 12508
		[MessageBodyMember(Name = "SetClientExtension", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetClientExtensionRequest Body;
	}
}
