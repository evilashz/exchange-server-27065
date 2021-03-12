using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D12 RID: 3346
	[MessageContract(IsWrapped = false)]
	public class UnpinTeamMailboxSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030A4 RID: 12452
		[MessageBodyMember(Name = "UnpinTeamMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UnpinTeamMailboxRequest Body;
	}
}
