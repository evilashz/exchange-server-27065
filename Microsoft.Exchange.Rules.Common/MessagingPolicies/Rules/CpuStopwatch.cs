using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200000B RID: 11
	internal sealed class CpuStopwatch
	{
		// Token: 0x06000043 RID: 67
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetThreadTimes(IntPtr hThread, out long lpCreationTime, out long lpExitTime, out long lpKernelTime, out long lpUserTime);

		// Token: 0x06000044 RID: 68
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetCurrentThread();

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002CE0 File Offset: 0x00000EE0
		internal long ElapsedMilliseconds
		{
			get
			{
				return this.elapsedMs;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CE8 File Offset: 0x00000EE8
		internal void Restart()
		{
			this.elapsedMs = 0L;
			this.Start();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CF8 File Offset: 0x00000EF8
		internal void Reset()
		{
			this.elapsedMs = 0L;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D04 File Offset: 0x00000F04
		internal void Start()
		{
			this.monitoredThread = CpuStopwatch.GetCurrentThread();
			if (this.monitoredThread == IntPtr.Zero)
			{
				throw new InvalidOperationException("CpuStopWatch: GetCurrentThread API failed to get current thread handle.");
			}
			long num;
			long num2;
			CpuStopwatch.GetThreadTimes(this.monitoredThread, out num, out num2, out this.kernelStartMark, out this.userStartMark);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D58 File Offset: 0x00000F58
		internal void Stop()
		{
			if (this.monitoredThread == IntPtr.Zero)
			{
				throw new InvalidOperationException("CpuStopWatch: Start was not called before Stop - current thread is not set.");
			}
			long num;
			long num2;
			long num3;
			long num4;
			CpuStopwatch.GetThreadTimes(this.monitoredThread, out num, out num2, out num3, out num4);
			this.elapsedMs = (num3 - this.kernelStartMark + (num4 - this.userStartMark)) / 10000L;
			this.kernelStartMark = num3;
			this.userStartMark = num4;
		}

		// Token: 0x0400001A RID: 26
		private long kernelStartMark;

		// Token: 0x0400001B RID: 27
		private long userStartMark;

		// Token: 0x0400001C RID: 28
		private IntPtr monitoredThread = IntPtr.Zero;

		// Token: 0x0400001D RID: 29
		private long elapsedMs;
	}
}
