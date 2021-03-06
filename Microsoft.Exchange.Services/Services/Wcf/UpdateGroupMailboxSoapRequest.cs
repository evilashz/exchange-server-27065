using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D96 RID: 3478
	[MessageContract(IsWrapped = false)]
	public class UpdateGroupMailboxSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003128 RID: 12584
		[MessageBodyMember(Name = "UpdateGroupMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateGroupMailboxRequest Body;
	}
}
