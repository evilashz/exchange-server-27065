using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009E1 RID: 2529
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ConnectedAccountInfo
	{
		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06004761 RID: 18273 RVA: 0x00100385 File Offset: 0x000FE585
		// (set) Token: 0x06004762 RID: 18274 RVA: 0x0010038D File Offset: 0x000FE58D
		[DataMember(IsRequired = true, Order = 1)]
		public Guid SubscriptionGuid { get; set; }

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06004763 RID: 18275 RVA: 0x00100396 File Offset: 0x000FE596
		// (set) Token: 0x06004764 RID: 18276 RVA: 0x0010039E File Offset: 0x000FE59E
		[DataMember(IsRequired = true, Order = 2)]
		public string EmailAddress { get; set; }

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06004765 RID: 18277 RVA: 0x001003A7 File Offset: 0x000FE5A7
		// (set) Token: 0x06004766 RID: 18278 RVA: 0x001003AF File Offset: 0x000FE5AF
		[DataMember(IsRequired = true, Order = 3)]
		public string DisplayName { get; set; }
	}
}
