using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200002B RID: 43
	internal sealed class MemoryPrivateBytesMonitor : ResourceMonitor
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00004460 File Offset: 0x00002660
		public MemoryPrivateBytesMonitor(ResourceManagerConfiguration.ResourceMonitorConfiguration configuration) : base(Strings.PrivateBytesResource, configuration)
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004474 File Offset: 0x00002674
		internal static ulong TotalPhysicalMemory
		{
			get
			{
				if (MemoryPrivateBytesMonitor.totalPhysicalMemory == 0UL)
				{
					NativeMethods.MemoryStatusEx memoryStatusEx;
					if (NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
					{
						MemoryPrivateBytesMonitor.totalPhysicalMemory = memoryStatusEx.TotalPhys;
					}
					else
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						ExTraceGlobals.ResourceManagerTracer.TraceError<int>(0L, "Call to GlobalMemoryStatusEx failed with 0x{0:X}", lastWin32Error);
					}
				}
				return MemoryPrivateBytesMonitor.totalPhysicalMemory;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000044C0 File Offset: 0x000026C0
		private static ulong DefaultPrivateBytesLimit
		{
			get
			{
				if (MemoryPrivateBytesMonitor.defaultPrivateBytesLimit == 0UL)
				{
					bool flag = UIntPtr.Size >= 8;
					NativeMethods.MemoryStatusEx memoryStatusEx;
					if (NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
					{
						MemoryPrivateBytesMonitor.totalPhysicalMemory = memoryStatusEx.TotalPhys;
						ulong val;
						if (flag)
						{
							val = 1099511627776UL;
						}
						else if (memoryStatusEx.TotalVirtual > (ulong)-2147483648)
						{
							val = 1887436800UL;
						}
						else
						{
							val = 838860800UL;
						}
						MemoryPrivateBytesMonitor.defaultPrivateBytesLimit = Math.Min(memoryStatusEx.TotalPhys * 75UL / 100UL, val);
					}
					else
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						ExTraceGlobals.ResourceManagerTracer.TraceError<int>(0L, "Call to GlobalMemoryStatusEx failed with 0x{0:X}", lastWin32Error);
						MemoryPrivateBytesMonitor.defaultPrivateBytesLimit = (flag ? 1099511627776UL : 838860800UL);
					}
				}
				return MemoryPrivateBytesMonitor.defaultPrivateBytesLimit;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000457C File Offset: 0x0000277C
		public override void UpdateConfig()
		{
			if (this.Configuration.HighThreshold == 0 && MemoryPrivateBytesMonitor.TotalPhysicalMemory > 0UL)
			{
				base.HighPressureLimit = (int)(MemoryPrivateBytesMonitor.DefaultPrivateBytesLimit * 100UL / MemoryPrivateBytesMonitor.TotalPhysicalMemory);
				base.MediumPressureLimit = base.HighPressureLimit - 2;
				base.LowPressureLimit = base.MediumPressureLimit - 2;
				return;
			}
			base.UpdateConfig();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000045D8 File Offset: 0x000027D8
		protected override bool GetCurrentReading(out int currentReading)
		{
			currentReading = 0;
			if (MemoryPrivateBytesMonitor.TotalPhysicalMemory > 0UL)
			{
				using (SafeProcessHandle currentProcess = NativeMethods.GetCurrentProcess())
				{
					NativeMethods.ProcessMemoryCounterEx processMemoryCounterEx;
					if (NativeMethods.GetProcessMemoryInfo(currentProcess, out processMemoryCounterEx, NativeMethods.ProcessMemoryCounterEx.Size))
					{
						currentReading = (int)(processMemoryCounterEx.privateUsage.ToUInt64() * 100UL / MemoryPrivateBytesMonitor.TotalPhysicalMemory);
					}
					else
					{
						ExTraceGlobals.ResourceManagerTracer.TraceError<int>(0L, "Failed to GetProcessMemoryInfo Error: {0}", Marshal.GetLastWin32Error());
						currentReading = 0;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000067 RID: 103
		internal const ulong TERABYTE = 1099511627776UL;

		// Token: 0x04000068 RID: 104
		internal const ulong GIGABYTE = 1073741824UL;

		// Token: 0x04000069 RID: 105
		internal const ulong MEGABYTE = 1048576UL;

		// Token: 0x0400006A RID: 106
		private const ulong PrivateBytesLimit2GB = 838860800UL;

		// Token: 0x0400006B RID: 107
		private const ulong PrivateBytesLimit3GB = 1887436800UL;

		// Token: 0x0400006C RID: 108
		private const ulong PrivateBytesLimit64Bit = 1099511627776UL;

		// Token: 0x0400006D RID: 109
		private static ulong totalPhysicalMemory;

		// Token: 0x0400006E RID: 110
		private static ulong defaultPrivateBytesLimit;
	}
}
