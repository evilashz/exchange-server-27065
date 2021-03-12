using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000BA RID: 186
	[DataContract(Name = "WebClientUrl", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class WebClientUrl
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00018310 File Offset: 0x00016510
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00018318 File Offset: 0x00016518
		[DataMember(Name = "AuthenticationMethods", IsRequired = true, Order = 1)]
		public string AuthenticationMethods { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00018321 File Offset: 0x00016521
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00018329 File Offset: 0x00016529
		[DataMember(Name = "Url", IsRequired = true, Order = 2)]
		public string Url { get; set; }
	}
}
