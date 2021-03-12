using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC4 RID: 3268
	[MessageContract(IsWrapped = false)]
	public class CopyItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003054 RID: 12372
		[MessageBodyMember(Name = "CopyItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CopyItemRequest Body;
	}
}
