using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200014D RID: 333
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ProxySettings
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002E13C File Offset: 0x0002C33C
		public ProxySettings()
		{
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002E144 File Offset: 0x0002C344
		public ProxySettings(string userPrincipalName, string[] proxyAddresses)
		{
			this.UserPrincipalName = userPrincipalName;
			this.ProxyAddresses = proxyAddresses;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002E15A File Offset: 0x0002C35A
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0002E162 File Offset: 0x0002C362
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string UserPrincipalName { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0002E16B File Offset: 0x0002C36B
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0002E173 File Offset: 0x0002C373
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string[] ProxyAddresses { get; set; }
	}
}
