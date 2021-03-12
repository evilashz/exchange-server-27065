using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000183 RID: 387
	internal class NativeMethods
	{
		// Token: 0x06000B34 RID: 2868
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryPerformanceFrequency(out long freq);

		// Token: 0x06000B35 RID: 2869
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryPerformanceCounter(out long count);

		// Token: 0x06000B36 RID: 2870
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool QueryThreadCycleTime(SafeThreadHandle handle, out ulong ticks);

		// Token: 0x06000B37 RID: 2871
		[DllImport("kernel32.dll")]
		internal static extern SafeThreadHandle GetCurrentThread();

		// Token: 0x06000B38 RID: 2872
		[DllImport("kernel32.dll")]
		internal static extern uint GetCurrentProcessorNumber();

		// Token: 0x06000B39 RID: 2873
		[DllImport("powrprof.dll", SetLastError = true)]
		internal static extern NativeMethods.NTSTATUS CallNtPowerInformation(int InformationLevel, IntPtr lpInputBuffer, uint nInputBufferSize, IntPtr lpOutputBuffer, uint nOutputBufferSize);

		// Token: 0x02000184 RID: 388
		internal enum NTSTATUS : uint
		{
			// Token: 0x040007AC RID: 1964
			STATUS_SUCCESS,
			// Token: 0x040007AD RID: 1965
			STATUS_BUFFER_TOO_SMALL = 3221225507U,
			// Token: 0x040007AE RID: 1966
			STATUS_ACCESS_DENIED = 3221225506U
		}
	}
}
