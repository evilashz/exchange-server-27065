using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CCA RID: 3274
	[MessageContract(IsWrapped = false)]
	public class DeleteAttachmentSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400305A RID: 12378
		[MessageBodyMember(Name = "DeleteAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DeleteAttachmentRequest Body;
	}
}
