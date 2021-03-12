using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000152 RID: 338
	internal sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x00024161 File Offset: 0x00022361
		public SafeRegistryHandle() : base(true)
		{
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002416A File Offset: 0x0002236A
		public SafeRegistryHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0002417A File Offset: 0x0002237A
		public static SafeRegistryHandle LocalMachine
		{
			get
			{
				return new SafeRegistryHandle(new IntPtr(-2147483646), false);
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002418C File Offset: 0x0002238C
		protected override bool ReleaseHandle()
		{
			DiagnosticsNativeMethods.ErrorCode errorCode = DiagnosticsNativeMethods.RegCloseKey(this.handle);
			return errorCode == DiagnosticsNativeMethods.ErrorCode.Success;
		}
	}
}
