using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D6C RID: 3436
	[MessageContract(IsWrapped = false)]
	public class RemoveContactFromImListSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030FE RID: 12542
		[MessageBodyMember(Name = "RemoveContactFromImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveContactFromImListRequest Body;
	}
}
