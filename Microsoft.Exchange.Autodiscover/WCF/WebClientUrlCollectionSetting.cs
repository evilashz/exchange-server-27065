using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000BC RID: 188
	[DataContract(Name = "WebClientUrlCollectionSetting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class WebClientUrlCollectionSetting : UserSetting
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00018342 File Offset: 0x00016542
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001834A File Offset: 0x0001654A
		[DataMember(Name = "WebClientUrls", IsRequired = true)]
		public WebClientUrlCollection WebClientUrls { get; set; }
	}
}
