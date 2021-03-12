using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200009A RID: 154
	[DataContract(Name = "DomainResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class DomainResponse : AutodiscoverResponse
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x00017AA0 File Offset: 0x00015CA0
		public DomainResponse()
		{
			this.DomainSettings = new DomainSettingCollection();
			this.DomainSettingErrors = new DomainSettingErrorCollection();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00017ABE File Offset: 0x00015CBE
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00017AC6 File Offset: 0x00015CC6
		[DataMember(Name = "RedirectTarget", IsRequired = false)]
		public string RedirectTarget { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00017ACF File Offset: 0x00015CCF
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00017AD7 File Offset: 0x00015CD7
		[DataMember(Name = "DomainSettings", IsRequired = false)]
		public DomainSettingCollection DomainSettings { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00017AE0 File Offset: 0x00015CE0
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00017AE8 File Offset: 0x00015CE8
		[DataMember(Name = "DomainSettingErrors", IsRequired = false)]
		public DomainSettingErrorCollection DomainSettingErrors { get; set; }
	}
}
