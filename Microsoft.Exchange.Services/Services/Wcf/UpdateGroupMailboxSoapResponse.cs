using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D97 RID: 3479
	[MessageContract(IsWrapped = false)]
	public class UpdateGroupMailboxSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003129 RID: 12585
		[MessageBodyMember(Name = "UpdateGroupMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateGroupMailboxResponse Body;
	}
}
