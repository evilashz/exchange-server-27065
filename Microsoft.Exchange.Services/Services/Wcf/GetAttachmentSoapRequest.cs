using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CCC RID: 3276
	[MessageContract(IsWrapped = false)]
	public class GetAttachmentSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400305C RID: 12380
		[MessageBodyMember(Name = "GetAttachment", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetAttachmentRequest Body;
	}
}
