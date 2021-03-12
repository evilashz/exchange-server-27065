using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA6 RID: 3238
	[MessageContract(IsWrapped = false)]
	public class CreateUnifiedMailboxSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003036 RID: 12342
		[MessageBodyMember(Name = "CreateUnifiedMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateUnifiedMailboxRequest Body;
	}
}
