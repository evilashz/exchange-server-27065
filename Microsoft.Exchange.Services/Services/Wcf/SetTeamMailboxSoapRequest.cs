using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D10 RID: 3344
	[MessageContract(IsWrapped = false)]
	public class SetTeamMailboxSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030A2 RID: 12450
		[MessageBodyMember(Name = "SetTeamMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetTeamMailboxRequest Body;
	}
}
