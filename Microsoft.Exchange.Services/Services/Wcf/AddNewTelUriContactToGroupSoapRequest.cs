using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D66 RID: 3430
	[MessageContract(IsWrapped = false)]
	public class AddNewTelUriContactToGroupSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030F8 RID: 12536
		[MessageBodyMember(Name = "AddNewTelUriContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddNewTelUriContactToGroupRequest Body;
	}
}
