using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200027A RID: 634
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAttachmentDataProviderItemsRequestWrapper
	{
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00053A8B File Offset: 0x00051C8B
		// (set) Token: 0x06001746 RID: 5958 RVA: 0x00053A93 File Offset: 0x00051C93
		[DataMember(Name = "request")]
		public GetAttachmentDataProviderItemsRequest Request { get; set; }
	}
}
