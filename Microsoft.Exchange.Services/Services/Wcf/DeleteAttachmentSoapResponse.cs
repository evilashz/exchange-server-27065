using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CCB RID: 3275
	[MessageContract(IsWrapped = false)]
	public class DeleteAttachmentSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400305B RID: 12379
		[MessageBodyMember(Name = "DeleteAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DeleteAttachmentResponse Body;
	}
}
