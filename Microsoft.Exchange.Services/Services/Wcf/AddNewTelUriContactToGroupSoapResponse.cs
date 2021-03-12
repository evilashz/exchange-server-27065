using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D67 RID: 3431
	[MessageContract(IsWrapped = false)]
	public class AddNewTelUriContactToGroupSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030F9 RID: 12537
		[MessageBodyMember(Name = "AddNewTelUriContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddNewTelUriContactToGroupResponseMessage Body;
	}
}
