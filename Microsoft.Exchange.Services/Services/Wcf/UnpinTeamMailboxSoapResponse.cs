using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D13 RID: 3347
	[MessageContract(IsWrapped = false)]
	public class UnpinTeamMailboxSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030A5 RID: 12453
		[MessageBodyMember(Name = "UnpinTeamMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UnpinTeamMailboxResponseMessage Body;
	}
}
