using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200009B RID: 155
	[DataContract(Name = "DomainSetting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[KnownType(typeof(DomainStringSetting))]
	public abstract class DomainSetting
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x00017AF1 File Offset: 0x00015CF1
		public DomainSetting()
		{
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00017AF9 File Offset: 0x00015CF9
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x00017B01 File Offset: 0x00015D01
		[DataMember(IsRequired = true)]
		public string Name { get; set; }
	}
}
