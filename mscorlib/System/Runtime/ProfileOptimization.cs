using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime
{
	// Token: 0x020006ED RID: 1773
	public static class ProfileOptimization
	{
		// Token: 0x06005015 RID: 20501
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void InternalSetProfileRoot(string directoryPath);

		// Token: 0x06005016 RID: 20502
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void InternalStartProfile(string profile, IntPtr ptrNativeAssemblyLoadContext);

		// Token: 0x06005017 RID: 20503 RVA: 0x00119C15 File Offset: 0x00117E15
		[SecurityCritical]
		public static void SetProfileRoot(string directoryPath)
		{
			ProfileOptimization.InternalSetProfileRoot(directoryPath);
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x00119C1D File Offset: 0x00117E1D
		[SecurityCritical]
		public static void StartProfile(string profile)
		{
			ProfileOptimization.InternalStartProfile(profile, IntPtr.Zero);
		}
	}
}
