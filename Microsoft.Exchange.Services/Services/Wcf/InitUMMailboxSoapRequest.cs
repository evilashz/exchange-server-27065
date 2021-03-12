using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D86 RID: 3462
	[MessageContract(IsWrapped = false)]
	public class InitUMMailboxSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003118 RID: 12568
		[MessageBodyMember(Name = "InitUMMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public InitUMMailboxRequest Body;
	}
}
