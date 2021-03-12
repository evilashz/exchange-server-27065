using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003D8 RID: 984
	[DataContract(Name = "AddMembersToUnifiedGroupResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddMembersToUnifiedGroupResponse : BaseJsonResponse
	{
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x00077747 File Offset: 0x00075947
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x0007774F File Offset: 0x0007594F
		[DataMember(Name = "ErrorState", IsRequired = false)]
		public UnifiedGroupResponseErrorState ErrorState { get; set; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x00077758 File Offset: 0x00075958
		// (set) Token: 0x06001F8E RID: 8078 RVA: 0x00077760 File Offset: 0x00075960
		[DataMember(Name = "Error", IsRequired = false)]
		public string Error { get; set; }
	}
}
