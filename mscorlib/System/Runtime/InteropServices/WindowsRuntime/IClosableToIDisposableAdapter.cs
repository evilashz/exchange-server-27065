using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CF RID: 2511
	[SecurityCritical]
	internal sealed class IClosableToIDisposableAdapter
	{
		// Token: 0x060063D3 RID: 25555 RVA: 0x001531A6 File Offset: 0x001513A6
		private IClosableToIDisposableAdapter()
		{
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x001531B0 File Offset: 0x001513B0
		[SecurityCritical]
		private void Dispose()
		{
			IClosable closable = JitHelpers.UnsafeCast<IClosable>(this);
			closable.Close();
		}
	}
}
