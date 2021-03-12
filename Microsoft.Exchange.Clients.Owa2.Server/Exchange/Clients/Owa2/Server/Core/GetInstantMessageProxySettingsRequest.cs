using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003D0 RID: 976
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetInstantMessageProxySettingsRequest
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0007740C File Offset: 0x0007560C
		// (set) Token: 0x06001F45 RID: 8005 RVA: 0x00077414 File Offset: 0x00075614
		[DataMember(Name = "userPrincipalNames", IsRequired = true)]
		public string[] UserPrincipalNames { get; set; }
	}
}
