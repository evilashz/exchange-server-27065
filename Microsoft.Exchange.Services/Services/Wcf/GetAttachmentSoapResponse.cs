using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CCD RID: 3277
	[MessageContract(IsWrapped = false)]
	public class GetAttachmentSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400305D RID: 12381
		[MessageBodyMember(Name = "GetAttachmentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetAttachmentResponse Body;
	}
}
