using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC9 RID: 3273
	[MessageContract(IsWrapped = false)]
	public class CreateAttachmentSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003059 RID: 12377
		[MessageBodyMember(Name = "CreateAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateAttachmentResponse Body;
	}
}
