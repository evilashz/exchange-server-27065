using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMService
{
	// Token: 0x02000005 RID: 5
	internal abstract class MemoryMonitorHelperMethods
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002100 File Offset: 0x00000300
		internal static bool IsRecommendedMemoryPressureExceeded(IntPtr processHandle, double percentage, out double currentPercent)
		{
			bool result = false;
			currentPercent = 0.0;
			if (MemoryMonitorHelperMethods.TotalPhysicalMemory > 0UL)
			{
				MemoryNativeMethods.ProcessMemoryCounterEx processMemoryCounterEx = default(MemoryNativeMethods.ProcessMemoryCounterEx);
				processMemoryCounterEx.Init();
				if (MemoryNativeMethods.GetProcessMemoryInfo(processHandle, ref processMemoryCounterEx, processMemoryCounterEx.cb))
				{
					currentPercent = processMemoryCounterEx.privateUsage.ToUInt64() * 100UL / MemoryMonitorHelperMethods.TotalPhysicalMemory;
					MemoryMonitorHelperMethods.DebugTrace("private usage = {0}, currentPercent = {1}, givenPercent = {2}", new object[]
					{
						processMemoryCounterEx.privateUsage.ToUInt64(),
						currentPercent,
						percentage
					});
					if (currentPercent > percentage)
					{
						result = true;
					}
				}
				else
				{
					MemoryMonitorHelperMethods.ErrorTrace("Failed to GetProcessMemoryInfo Error: {0}", new object[]
					{
						Marshal.GetLastWin32Error()
					});
				}
			}
			return result;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021C4 File Offset: 0x000003C4
		private static ulong TotalPhysicalMemory
		{
			get
			{
				if (MemoryMonitorHelperMethods.totalPhysicalMemory == 0UL)
				{
					MemoryNativeMethods.MemoryStatusEx memoryStatusEx = default(MemoryNativeMethods.MemoryStatusEx);
					memoryStatusEx.Init();
					if (MemoryNativeMethods.GlobalMemoryStatusEx(ref memoryStatusEx))
					{
						MemoryMonitorHelperMethods.totalPhysicalMemory = memoryStatusEx.totalPhys;
					}
					else
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						MemoryMonitorHelperMethods.ErrorTrace("Call to GlobalMemoryStatusEx failed with 0x{0:X}", new object[]
						{
							lastWin32Error
						});
					}
				}
				return MemoryMonitorHelperMethods.totalPhysicalMemory;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002226 File Offset: 0x00000426
		private static void ErrorTrace(string formatString, params object[] formatObjects)
		{
			CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, formatString, formatObjects);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000223A File Offset: 0x0000043A
		private static void DebugTrace(string formatString, params object[] formatObjects)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, formatString, formatObjects);
		}

		// Token: 0x04000017 RID: 23
		private static ulong totalPhysicalMemory;
	}
}
