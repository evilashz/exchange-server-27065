using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA7 RID: 3239
	[MessageContract(IsWrapped = false)]
	public class CreateUnifiedMailboxSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003037 RID: 12343
		[MessageBodyMember(Name = "CreateUnifiedMailboxResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateUnifiedMailboxResponse Body;
	}
}
