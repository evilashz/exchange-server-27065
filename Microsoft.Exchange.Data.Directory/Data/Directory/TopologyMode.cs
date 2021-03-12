using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001AB RID: 427
	internal enum TopologyMode
	{
		// Token: 0x04000A6F RID: 2671
		ADTopologyService,
		// Token: 0x04000A70 RID: 2672
		[Obsolete("Removed and replaced by LdapTopology Provider")]
		DirectoryServices,
		// Token: 0x04000A71 RID: 2673
		Adam,
		// Token: 0x04000A72 RID: 2674
		Ldap
	}
}
