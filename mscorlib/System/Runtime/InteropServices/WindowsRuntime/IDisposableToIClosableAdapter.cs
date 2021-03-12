using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CE RID: 2510
	internal sealed class IDisposableToIClosableAdapter
	{
		// Token: 0x060063D1 RID: 25553 RVA: 0x00153183 File Offset: 0x00151383
		private IDisposableToIClosableAdapter()
		{
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x0015318C File Offset: 0x0015138C
		[SecurityCritical]
		public void Close()
		{
			IDisposable disposable = JitHelpers.UnsafeCast<IDisposable>(this);
			disposable.Dispose();
		}
	}
}
