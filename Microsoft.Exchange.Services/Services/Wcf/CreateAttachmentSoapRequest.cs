using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC8 RID: 3272
	[MessageContract(IsWrapped = false)]
	public class CreateAttachmentSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003058 RID: 12376
		[MessageBodyMember(Name = "CreateAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateAttachmentRequest Body;
	}
}
