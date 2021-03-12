﻿using System;

namespace System.Security
{
	// Token: 0x020001CF RID: 463
	[Serializable]
	internal enum PermissionType
	{
		// Token: 0x040009DD RID: 2525
		SecurityUnmngdCodeAccess,
		// Token: 0x040009DE RID: 2526
		SecuritySkipVerification,
		// Token: 0x040009DF RID: 2527
		ReflectionTypeInfo,
		// Token: 0x040009E0 RID: 2528
		SecurityAssert,
		// Token: 0x040009E1 RID: 2529
		ReflectionMemberAccess,
		// Token: 0x040009E2 RID: 2530
		SecuritySerialization,
		// Token: 0x040009E3 RID: 2531
		ReflectionRestrictedMemberAccess,
		// Token: 0x040009E4 RID: 2532
		FullTrust,
		// Token: 0x040009E5 RID: 2533
		SecurityBindingRedirects,
		// Token: 0x040009E6 RID: 2534
		UIPermission,
		// Token: 0x040009E7 RID: 2535
		EnvironmentPermission,
		// Token: 0x040009E8 RID: 2536
		FileDialogPermission,
		// Token: 0x040009E9 RID: 2537
		FileIOPermission,
		// Token: 0x040009EA RID: 2538
		ReflectionPermission,
		// Token: 0x040009EB RID: 2539
		SecurityPermission,
		// Token: 0x040009EC RID: 2540
		SecurityControlEvidence = 16,
		// Token: 0x040009ED RID: 2541
		SecurityControlPrincipal
	}
}
