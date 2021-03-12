using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D45 RID: 3397
	[MessageContract(IsWrapped = false)]
	public class MarkAllItemsAsReadSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030D7 RID: 12503
		[MessageBodyMember(Name = "MarkAllItemsAsReadResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public MarkAllItemsAsReadResponse Body;
	}
}
