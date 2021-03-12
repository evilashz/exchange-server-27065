using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000273 RID: 627
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteCalendarGroupRequestWrapper
	{
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x000539CB File Offset: 0x00051BCB
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x000539D3 File Offset: 0x00051BD3
		[DataMember(Name = "groupId")]
		public string GroupId { get; set; }
	}
}
