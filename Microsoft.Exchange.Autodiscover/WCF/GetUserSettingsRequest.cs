using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200008E RID: 142
	[DataContract(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class GetUserSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00016F44 File Offset: 0x00015144
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00016F4C File Offset: 0x0001514C
		[DataMember(Name = "Users", IsRequired = true, Order = 1)]
		public UserCollection Users { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00016F55 File Offset: 0x00015155
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00016F5D File Offset: 0x0001515D
		[DataMember(Name = "RequestedSettings", IsRequired = true, Order = 2)]
		public RequestedSettingCollection RequestedSettings { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00016F66 File Offset: 0x00015166
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00016F6E File Offset: 0x0001516E
		[DataMember(Name = "RequestedVersion", IsRequired = false, Order = 3)]
		public ExchangeVersion? RequestedVersion { get; set; }
	}
}
