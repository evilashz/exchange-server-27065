using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CE3 RID: 3299
	[MessageContract(IsWrapped = false)]
	public class SyncFolderItemsSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003075 RID: 12405
		[MessageBodyMember(Name = "SyncFolderItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SyncFolderItemsResponse Body;
	}
}
