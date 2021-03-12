using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000286 RID: 646
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMasterCategoryListRequestWrapper
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00053C1D File Offset: 0x00051E1D
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x00053C25 File Offset: 0x00051E25
		[DataMember(Name = "request")]
		public GetMasterCategoryListRequest Request { get; set; }
	}
}
