using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200009D RID: 157
	[DataContract(Name = "DomainSettingError", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class DomainSettingError
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00017B1A File Offset: 0x00015D1A
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00017B22 File Offset: 0x00015D22
		[DataMember(IsRequired = true)]
		public string SettingName { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00017B2B File Offset: 0x00015D2B
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00017B33 File Offset: 0x00015D33
		[DataMember(IsRequired = true)]
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00017B3C File Offset: 0x00015D3C
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00017B44 File Offset: 0x00015D44
		[DataMember(IsRequired = true)]
		public string ErrorMessage { get; set; }
	}
}
