using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020001D8 RID: 472
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostSecurityManagerOptions
	{
		// Token: 0x040009FD RID: 2557
		None = 0,
		// Token: 0x040009FE RID: 2558
		HostAppDomainEvidence = 1,
		// Token: 0x040009FF RID: 2559
		[Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		HostPolicyLevel = 2,
		// Token: 0x04000A00 RID: 2560
		HostAssemblyEvidence = 4,
		// Token: 0x04000A01 RID: 2561
		HostDetermineApplicationTrust = 8,
		// Token: 0x04000A02 RID: 2562
		HostResolvePolicy = 16,
		// Token: 0x04000A03 RID: 2563
		AllFlags = 31
	}
}
