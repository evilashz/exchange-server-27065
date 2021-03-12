using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D94 RID: 3476
	[MessageContract(IsWrapped = false)]
	public class UpdateMailboxAssociationSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003126 RID: 12582
		[MessageBodyMember(Name = "UpdateMailboxAssociation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateMailboxAssociationRequest Body;
	}
}
