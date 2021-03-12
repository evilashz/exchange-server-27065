using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C9A RID: 3226
	[MessageContract(IsWrapped = false)]
	public class ConvertIdSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400302A RID: 12330
		[MessageBodyMember(Name = "ConvertId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ConvertIdRequest Body;
	}
}
