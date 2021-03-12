using System;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x0200001A RID: 26
	internal class NativeMethodsWrapper : INativeMethodsWrapper
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000618F File Offset: 0x0000438F
		public bool GetDiskFreeSpaceEx(string directoryName, out ulong freeBytesAvailable, out ulong totalNumberOfBytes, out ulong totalNumberOfFreeBytes)
		{
			return NativeMethods.GetDiskFreeSpaceEx(directoryName, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000619C File Offset: 0x0000439C
		public bool GetSystemMemoryUsePercentage(out uint systemMemoryUsage)
		{
			NativeMethods.MemoryStatusEx memoryStatusEx;
			if (NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
			{
				systemMemoryUsage = memoryStatusEx.MemoryLoad;
				return true;
			}
			systemMemoryUsage = 0U;
			return false;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000061C4 File Offset: 0x000043C4
		public bool GetTotalSystemMemory(out ulong systemMemory)
		{
			NativeMethods.MemoryStatusEx memoryStatusEx;
			if (NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
			{
				systemMemory = memoryStatusEx.TotalPhys;
				return true;
			}
			systemMemory = 0UL;
			return false;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000061EC File Offset: 0x000043EC
		public bool GetTotalVirtualMemory(out ulong virtualMemory)
		{
			NativeMethods.MemoryStatusEx memoryStatusEx;
			if (NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
			{
				virtualMemory = memoryStatusEx.TotalVirtual;
				return true;
			}
			virtualMemory = 0UL;
			return false;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006214 File Offset: 0x00004414
		public bool GetProcessPrivateBytes(out ulong privateBytes)
		{
			bool result;
			using (SafeProcessHandle currentProcess = NativeMethods.GetCurrentProcess())
			{
				NativeMethods.ProcessMemoryCounterEx processMemoryCounterEx;
				if (NativeMethods.GetProcessMemoryInfo(currentProcess, out processMemoryCounterEx, NativeMethods.ProcessMemoryCounterEx.Size))
				{
					privateBytes = processMemoryCounterEx.privateUsage.ToUInt64();
					result = true;
				}
				else
				{
					privateBytes = 0UL;
					result = false;
				}
			}
			return result;
		}
	}
}
