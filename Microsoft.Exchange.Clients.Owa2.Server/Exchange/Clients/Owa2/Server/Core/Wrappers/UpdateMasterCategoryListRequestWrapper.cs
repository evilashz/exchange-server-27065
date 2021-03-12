using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002CE RID: 718
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateMasterCategoryListRequestWrapper
	{
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0005448A File Offset: 0x0005268A
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x00054492 File Offset: 0x00052692
		[DataMember(Name = "request")]
		public UpdateMasterCategoryListRequest Request { get; set; }
	}
}
