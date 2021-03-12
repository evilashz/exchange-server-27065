using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000283 RID: 643
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetGroupInfoRequestWrapper
	{
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00053BC1 File Offset: 0x00051DC1
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x00053BC9 File Offset: 0x00051DC9
		[DataMember(Name = "getGroupInfoRequest")]
		public GetGroupInfoRequest GetGroupInfoRequest { get; set; }
	}
}
