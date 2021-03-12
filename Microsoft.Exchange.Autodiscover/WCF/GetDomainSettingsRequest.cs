using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000088 RID: 136
	[DataContract(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class GetDomainSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000160CD File Offset: 0x000142CD
		// (set) Token: 0x0600038C RID: 908 RVA: 0x000160D5 File Offset: 0x000142D5
		[DataMember(Name = "Domains", IsRequired = true, Order = 1)]
		public DomainCollection Domains { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600038D RID: 909 RVA: 0x000160DE File Offset: 0x000142DE
		// (set) Token: 0x0600038E RID: 910 RVA: 0x000160E6 File Offset: 0x000142E6
		[DataMember(Name = "RequestedSettings", IsRequired = true, Order = 2)]
		public RequestedSettingCollection RequestedSettings { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000160EF File Offset: 0x000142EF
		// (set) Token: 0x06000390 RID: 912 RVA: 0x000160F7 File Offset: 0x000142F7
		[DataMember(Name = "RequestedVersion", IsRequired = false, Order = 3)]
		public ExchangeVersion? RequestedVersion { get; set; }
	}
}
