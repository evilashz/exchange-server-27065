using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000086 RID: 134
	public class JobObject : DisposeTrackableBase
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x00011924 File Offset: 0x0000FB24
		public JobObject(string name, long memorySizeLimitInMb = 0L)
		{
			this.safeJobHandle = NativeMethods.CreateJobObject(IntPtr.Zero, name);
			if (this.safeJobHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				ExTraceGlobals.UtilsTracer.TraceError<string, int>((long)name.GetHashCode(), "{0}: Failed to create Job object. Win32 ErrorCode: {1}", name, lastWin32Error);
				throw new Win32Exception(lastWin32Error, "Failed to create Job object. Error code is {0}.");
			}
			NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION extendedLimits = default(NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION);
			extendedLimits.BasicLimitInformation.LimitFlags = 8192U;
			if (memorySizeLimitInMb > 0L)
			{
				extendedLimits.BasicLimitInformation.LimitFlags = (extendedLimits.BasicLimitInformation.LimitFlags | 256U);
				extendedLimits.ProcessMemoryLimit = new UIntPtr((uint)memorySizeLimitInMb * 1024U * 1024U);
			}
			this.safeJobHandle.SetExtendedLimits(extendedLimits);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000119DB File Offset: 0x0000FBDB
		public bool Add(Process process)
		{
			return process != null && this.safeJobHandle.Add(process);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000119EE File Offset: 0x0000FBEE
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.safeJobHandle != null)
			{
				this.safeJobHandle.Dispose();
				this.safeJobHandle = null;
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00011A0D File Offset: 0x0000FC0D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JobObject>(this);
		}

		// Token: 0x04000244 RID: 580
		private const int Win32ErrorAccessDenied = 5;

		// Token: 0x04000245 RID: 581
		private const int Win32ErrorNotSupported = 50;

		// Token: 0x04000246 RID: 582
		private SafeJobHandle safeJobHandle;

		// Token: 0x02000087 RID: 135
		[Flags]
		private enum JobObjectExtendedLimit : uint
		{
			// Token: 0x04000248 RID: 584
			LimitProcessMemory = 256U,
			// Token: 0x04000249 RID: 585
			LimitKillOnJobClose = 8192U
		}
	}
}
