using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D89 RID: 3465
	[MessageContract(IsWrapped = false)]
	public class ResetUMMailboxSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400311B RID: 12571
		[MessageBodyMember(Name = "ResetUMMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ResetUMMailboxResponseMessage Body;
	}
}
