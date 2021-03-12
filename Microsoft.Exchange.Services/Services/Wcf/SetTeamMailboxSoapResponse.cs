using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D11 RID: 3345
	[MessageContract(IsWrapped = false)]
	public class SetTeamMailboxSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030A3 RID: 12451
		[MessageBodyMember(Name = "SetTeamMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetTeamMailboxResponseMessage Body;
	}
}
