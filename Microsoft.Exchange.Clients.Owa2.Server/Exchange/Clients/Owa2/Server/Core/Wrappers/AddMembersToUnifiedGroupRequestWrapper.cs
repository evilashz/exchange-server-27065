using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000260 RID: 608
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddMembersToUnifiedGroupRequestWrapper
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00053724 File Offset: 0x00051924
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x0005372C File Offset: 0x0005192C
		[DataMember(Name = "request")]
		public AddMembersToUnifiedGroupRequest Request { get; set; }
	}
}
