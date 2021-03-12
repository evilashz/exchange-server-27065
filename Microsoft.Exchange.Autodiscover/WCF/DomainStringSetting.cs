using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200009F RID: 159
	[DataContract(Name = "DomainStringSetting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public sealed class DomainStringSetting : DomainSetting
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00017B5D File Offset: 0x00015D5D
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00017B65 File Offset: 0x00015D65
		[DataMember]
		public string Value { get; set; }
	}
}
