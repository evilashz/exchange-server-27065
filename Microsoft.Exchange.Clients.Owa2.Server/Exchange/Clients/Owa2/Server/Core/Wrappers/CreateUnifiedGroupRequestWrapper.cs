using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000270 RID: 624
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateUnifiedGroupRequestWrapper
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00053980 File Offset: 0x00051B80
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x00053988 File Offset: 0x00051B88
		[DataMember(Name = "request")]
		public CreateUnifiedGroupRequest Request { get; set; }
	}
}
