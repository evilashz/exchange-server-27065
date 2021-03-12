using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005C4 RID: 1476
	internal sealed class LoaderAllocatorScout
	{
		// Token: 0x06004551 RID: 17745
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool Destroy(IntPtr nativeLoaderAllocator);

		// Token: 0x06004552 RID: 17746 RVA: 0x000FDCA0 File Offset: 0x000FBEA0
		[SecuritySafeCritical]
		~LoaderAllocatorScout()
		{
			if (!this.m_nativeLoaderAllocator.IsNull())
			{
				if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload() && !LoaderAllocatorScout.Destroy(this.m_nativeLoaderAllocator))
				{
					GC.ReRegisterForFinalize(this);
				}
			}
		}

		// Token: 0x04001C18 RID: 7192
		internal IntPtr m_nativeLoaderAllocator;
	}
}
