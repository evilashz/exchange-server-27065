using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D6D RID: 3437
	[MessageContract(IsWrapped = false)]
	public class RemoveContactFromImListSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030FF RID: 12543
		[MessageBodyMember(Name = "RemoveContactFromImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveContactFromImListResponseMessage Body;
	}
}
