using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200000F RID: 15
	internal class ToolhelpSnapshotSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000067 RID: 103 RVA: 0x0000351C File Offset: 0x0000171C
		public ToolhelpSnapshotSafeHandle() : base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003530 File Offset: 0x00001730
		protected override bool ReleaseHandle()
		{
			return DiagnosticsNativeMethods.CloseHandle(this.handle);
		}
	}
}
