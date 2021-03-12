using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D62 RID: 3426
	[MessageContract(IsWrapped = false)]
	public class AddImGroupSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030F4 RID: 12532
		[MessageBodyMember(Name = "AddImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddImGroupRequest Body;
	}
}
