using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000044 RID: 68
	[DataContract]
	internal sealed class ProxyServerInformation
	{
		// Token: 0x06000340 RID: 832 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public ProxyServerInformation()
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00005D00 File Offset: 0x00003F00
		public ProxyServerInformation(string fqdn, bool isProxyLocal)
		{
			this.ServerFqdn = fqdn;
			this.IsProxyLocal = isProxyLocal;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00005D16 File Offset: 0x00003F16
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00005D1E File Offset: 0x00003F1E
		[DataMember(Name = "IsProxyLocal")]
		public bool IsProxyLocal { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00005D27 File Offset: 0x00003F27
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00005D2F File Offset: 0x00003F2F
		[DataMember(Name = "ServerFqdn")]
		public string ServerFqdn { get; set; }
	}
}
