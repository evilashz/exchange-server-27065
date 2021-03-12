using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D99 RID: 3481
	[MessageContract(IsWrapped = false)]
	public class PostModernGroupItemSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400312B RID: 12587
		[MessageBodyMember(Name = "PostModernGroupItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public PostModernGroupItemResponse Body;
	}
}
