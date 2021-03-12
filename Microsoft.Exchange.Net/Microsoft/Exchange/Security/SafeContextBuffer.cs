using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C7F RID: 3199
	internal sealed class SafeContextBuffer : DebugSafeHandle
	{
		// Token: 0x060046CA RID: 18122 RVA: 0x000BE494 File Offset: 0x000BC694
		internal SafeContextBuffer()
		{
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x000BE49C File Offset: 0x000BC69C
		internal SafeContextBuffer(IntPtr handle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x000BE4AB File Offset: 0x000BC6AB
		protected override bool ReleaseHandle()
		{
			return SspiNativeMethods.FreeContextBuffer(this.handle) == SecurityStatus.OK;
		}
	}
}
