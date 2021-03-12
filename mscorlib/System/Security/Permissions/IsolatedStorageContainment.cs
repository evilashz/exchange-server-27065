using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002BD RID: 701
	[ComVisible(true)]
	[Serializable]
	public enum IsolatedStorageContainment
	{
		// Token: 0x04000E09 RID: 3593
		None,
		// Token: 0x04000E0A RID: 3594
		DomainIsolationByUser = 16,
		// Token: 0x04000E0B RID: 3595
		ApplicationIsolationByUser = 21,
		// Token: 0x04000E0C RID: 3596
		AssemblyIsolationByUser = 32,
		// Token: 0x04000E0D RID: 3597
		DomainIsolationByMachine = 48,
		// Token: 0x04000E0E RID: 3598
		AssemblyIsolationByMachine = 64,
		// Token: 0x04000E0F RID: 3599
		ApplicationIsolationByMachine = 69,
		// Token: 0x04000E10 RID: 3600
		DomainIsolationByRoamingUser = 80,
		// Token: 0x04000E11 RID: 3601
		AssemblyIsolationByRoamingUser = 96,
		// Token: 0x04000E12 RID: 3602
		ApplicationIsolationByRoamingUser = 101,
		// Token: 0x04000E13 RID: 3603
		AdministerIsolatedStorageByUser = 112,
		// Token: 0x04000E14 RID: 3604
		UnrestrictedIsolatedStorage = 240
	}
}
