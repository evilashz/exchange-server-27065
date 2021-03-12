using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D44 RID: 3396
	[MessageContract(IsWrapped = false)]
	public class MarkAllItemsAsReadSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030D6 RID: 12502
		[MessageBodyMember(Name = "MarkAllItemsAsRead", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public MarkAllItemsAsReadRequest Body;
	}
}
