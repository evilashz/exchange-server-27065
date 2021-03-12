using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D98 RID: 3480
	[MessageContract(IsWrapped = false)]
	public class PostModernGroupItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400312A RID: 12586
		[MessageBodyMember(Name = "PostModernGroupItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public PostModernGroupItemRequest Body;
	}
}
