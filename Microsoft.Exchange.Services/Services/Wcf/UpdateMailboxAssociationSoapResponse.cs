using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D95 RID: 3477
	[MessageContract(IsWrapped = false)]
	public class UpdateMailboxAssociationSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003127 RID: 12583
		[MessageBodyMember(Name = "UpdateMailboxAssociationResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateMailboxAssociationResponse Body;
	}
}
