using System;
using System.Net;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000886 RID: 2182
	internal class AddDagServerWmiIpInformation
	{
		// Token: 0x06004BC5 RID: 19397 RVA: 0x0013AF02 File Offset: 0x00139102
		public AddDagServerWmiIpInformation(IPAddress ipAddress, uint netmask, bool dhcpEnabled)
		{
			this.m_ipAddress = ipAddress;
			this.m_netmask = netmask;
			this.m_fDhcpEnabled = dhcpEnabled;
		}

		// Token: 0x04002D54 RID: 11604
		public readonly IPAddress m_ipAddress;

		// Token: 0x04002D55 RID: 11605
		public readonly uint m_netmask;

		// Token: 0x04002D56 RID: 11606
		public readonly bool m_fDhcpEnabled;
	}
}
