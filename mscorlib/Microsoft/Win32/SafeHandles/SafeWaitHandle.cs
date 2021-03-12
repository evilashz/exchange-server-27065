using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000021 RID: 33
	[SecurityCritical]
	[__DynamicallyInvokable]
	public sealed class SafeWaitHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00004823 File Offset: 0x00002A23
		private SafeWaitHandle() : base(true)
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000482C File Offset: 0x00002A2C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public SafeWaitHandle(IntPtr existingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000483C File Offset: 0x00002A3C
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
