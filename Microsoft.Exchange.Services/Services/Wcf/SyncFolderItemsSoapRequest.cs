using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CE2 RID: 3298
	[MessageContract(IsWrapped = false)]
	public class SyncFolderItemsSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003074 RID: 12404
		[MessageBodyMember(Name = "SyncFolderItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SyncFolderItemsRequest Body;
	}
}
