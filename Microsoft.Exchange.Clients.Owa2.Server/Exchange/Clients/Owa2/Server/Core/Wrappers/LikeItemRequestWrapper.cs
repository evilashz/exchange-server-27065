using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000298 RID: 664
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LikeItemRequestWrapper
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x00053E12 File Offset: 0x00052012
		// (set) Token: 0x060017B2 RID: 6066 RVA: 0x00053E1A File Offset: 0x0005201A
		[DataMember(Name = "request")]
		public LikeItemRequest Request { get; set; }
	}
}
