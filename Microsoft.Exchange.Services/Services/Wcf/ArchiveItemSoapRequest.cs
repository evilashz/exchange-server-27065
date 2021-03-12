using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC6 RID: 3270
	[MessageContract(IsWrapped = false)]
	public class ArchiveItemSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003056 RID: 12374
		[MessageBodyMember(Name = "ArchiveItem", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ArchiveItemRequest Body;
	}
}
