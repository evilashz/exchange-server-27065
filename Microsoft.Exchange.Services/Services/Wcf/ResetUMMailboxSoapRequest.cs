using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D88 RID: 3464
	[MessageContract(IsWrapped = false)]
	public class ResetUMMailboxSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400311A RID: 12570
		[MessageBodyMember(Name = "ResetUMMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ResetUMMailboxRequest Body;
	}
}
