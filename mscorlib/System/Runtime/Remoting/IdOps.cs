using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000785 RID: 1925
	internal struct IdOps
	{
		// Token: 0x0600544F RID: 21583 RVA: 0x0012AB8E File Offset: 0x00128D8E
		internal static bool bStrongIdentity(int flags)
		{
			return (flags & 2) != 0;
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x0012AB96 File Offset: 0x00128D96
		internal static bool bIsInitializing(int flags)
		{
			return (flags & 4) != 0;
		}

		// Token: 0x0400269E RID: 9886
		internal const int None = 0;

		// Token: 0x0400269F RID: 9887
		internal const int GenerateURI = 1;

		// Token: 0x040026A0 RID: 9888
		internal const int StrongIdentity = 2;

		// Token: 0x040026A1 RID: 9889
		internal const int IsInitializing = 4;
	}
}
