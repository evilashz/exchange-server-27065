using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009B3 RID: 2483
	internal static class CPUUsage
	{
		// Token: 0x06007296 RID: 29334 RVA: 0x0017B602 File Offset: 0x00179802
		internal static bool CalculateCPUUsagePercentage(ref DateTime lastUpdatedTime, ref long lastCPU, out float cpuUsage)
		{
			return CPUUsage.InternalCalculateCPUUsagePercentage(IntPtr.Zero, ref lastUpdatedTime, ref lastCPU, out cpuUsage);
		}

		// Token: 0x06007297 RID: 29335 RVA: 0x0017B611 File Offset: 0x00179811
		internal static bool CalculateCPUUsagePercentage(IntPtr process, ref DateTime lastUpdatedTime, ref long lastCPU, out float cpuUsage)
		{
			if (process == IntPtr.Zero)
			{
				throw new ArgumentNullException("process");
			}
			return CPUUsage.InternalCalculateCPUUsagePercentage(process, ref lastUpdatedTime, ref lastCPU, out cpuUsage);
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x0017B634 File Offset: 0x00179834
		internal static bool GetCurrentCPU(ref long cpuTime)
		{
			return CPUUsage.InternalGetCurrentCPU(IntPtr.Zero, ref cpuTime);
		}

		// Token: 0x06007299 RID: 29337 RVA: 0x0017B641 File Offset: 0x00179841
		internal static bool GetCurrentCPU(IntPtr process, ref long cpuTime)
		{
			if (process == IntPtr.Zero)
			{
				throw new ArgumentNullException("process");
			}
			return CPUUsage.InternalGetCurrentCPU(process, ref cpuTime);
		}

		// Token: 0x0600729A RID: 29338 RVA: 0x0017B664 File Offset: 0x00179864
		private static bool InternalCalculateCPUUsagePercentage(IntPtr process, ref DateTime lastUpdatedTime, ref long lastCPU, out float cpuUsage)
		{
			cpuUsage = 0f;
			long num = 0L;
			if (!CPUUsage.InternalGetCurrentCPU(process, ref num))
			{
				return false;
			}
			DateTime utcNow = DateTime.UtcNow;
			long num2 = num - lastCPU;
			double totalSeconds = (utcNow - lastUpdatedTime).TotalSeconds;
			if (totalSeconds > 0.0)
			{
				cpuUsage = (float)((double)num2 * 1E-05 / (totalSeconds * (double)CPUUsage.processorCount));
				if (cpuUsage > 100f)
				{
					cpuUsage = 100f;
				}
				lastUpdatedTime = utcNow;
				lastCPU = num;
			}
			return true;
		}

		// Token: 0x0600729B RID: 29339 RVA: 0x0017B6E8 File Offset: 0x001798E8
		private static bool InternalGetCurrentCPU(IntPtr process, ref long cpuTime)
		{
			bool result = false;
			long num3;
			long num4;
			long num5;
			if (process != IntPtr.Zero)
			{
				long num;
				long num2;
				if (NativeMethods.GetProcessTimes(process, out num, out num2, out num3, out num4))
				{
					result = true;
					cpuTime = num3 + num4;
				}
				else
				{
					CPUUsage.Tracer.TraceError<uint>(0L, "[CPUUsage::InternalGetCurrentCPU] Calling GetProcessTimes failed with error code '{0}'.", NativeMethods.GetLastError());
				}
			}
			else if (NativeMethods.GetSystemTimes(out num5, out num3, out num4))
			{
				result = true;
				cpuTime = num3 + num4 - num5;
			}
			else
			{
				CPUUsage.Tracer.TraceError<uint>(0L, "[CPUUsage::InternalGetCurrentCPU] Calling GetSystemTimes failed with error code '{0}'.", NativeMethods.GetLastError());
			}
			return result;
		}

		// Token: 0x04004A3A RID: 19002
		private static readonly Trace Tracer = ExTraceGlobals.ResourceHealthManagerTracer;

		// Token: 0x04004A3B RID: 19003
		private static readonly int processorCount = Environment.ProcessorCount;
	}
}
