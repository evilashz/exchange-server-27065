using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200002C RID: 44
	internal sealed class MemoryTotalBytesMonitor : ResourceMonitor
	{
		// Token: 0x060000EB RID: 235 RVA: 0x0000465C File Offset: 0x0000285C
		public MemoryTotalBytesMonitor(ResourceManagerConfiguration.ResourceMonitorConfiguration configuration) : base(string.Empty, configuration)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000466A File Offset: 0x0000286A
		public override string ToString(ResourceUses resourceUses, int currentPressure)
		{
			return Strings.PhysicalMemoryUses(currentPressure, base.HighPressureLimit);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004680 File Offset: 0x00002880
		protected override bool GetCurrentReading(out int currentReading)
		{
			NativeMethods.MemoryStatusEx memoryStatusEx;
			if (NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
			{
				currentReading = (int)memoryStatusEx.MemoryLoad;
			}
			else
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				ExTraceGlobals.ResourceManagerTracer.TraceError<int>(0L, "Call to GlobalMemoryStatusEx failed with 0x{0:X}", lastWin32Error);
				currentReading = 0;
			}
			return true;
		}
	}
}
