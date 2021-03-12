using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000014 RID: 20
	internal class DiskSpaceMonitor : ResourceMonitor
	{
		// Token: 0x0600007A RID: 122 RVA: 0x000030A0 File Offset: 0x000012A0
		public DiskSpaceMonitor(string displayName, string path, ResourceManagerConfiguration.ResourceMonitorConfiguration configuration, ulong minimumFreeSpace = 0UL) : base(displayName, configuration)
		{
			this.path = Path.GetDirectoryName(path);
			this.minimumFreeSpace = minimumFreeSpace;
			ulong num;
			this.TryGetDiskUsage(out num, out this.diskSize);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000030D8 File Offset: 0x000012D8
		public override void UpdateConfig()
		{
			if (this.minimumFreeSpace != 0UL && this.diskSize > 0UL)
			{
				base.HighPressureLimit = Math.Min((int)((this.diskSize - this.minimumFreeSpace) * 100UL / this.diskSize), this.Configuration.HighThreshold);
				base.MediumPressureLimit = Math.Min(base.HighPressureLimit - 2, this.Configuration.MediumThreshold);
				base.LowPressureLimit = Math.Min(base.MediumPressureLimit - 2, this.Configuration.NormalThreshold);
				return;
			}
			base.UpdateConfig();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000316C File Offset: 0x0000136C
		protected override bool GetCurrentReading(out int currentReading)
		{
			ulong num;
			ulong num2;
			if (this.TryGetDiskUsage(out num, out num2) && num2 > 0UL)
			{
				num += this.GetFreeBytesAvailableOffset();
				currentReading = (int)((num2 - num) * 100UL / num2);
				return true;
			}
			currentReading = 0;
			return false;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000031A8 File Offset: 0x000013A8
		protected bool TryGetDiskUsage(out ulong freeBytesAvailable, out ulong totalNumberOfBytes)
		{
			ulong num;
			if (!NativeMethods.GetDiskFreeSpaceEx(this.path, out freeBytesAvailable, out totalNumberOfBytes, out num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				ExTraceGlobals.ResourceManagerTracer.TraceError<int>(0L, "Call to GetDiskFreeInfoEx failed with 0x{0:X}", lastWin32Error);
				return false;
			}
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000031E1 File Offset: 0x000013E1
		protected virtual ulong GetFreeBytesAvailableOffset()
		{
			return 0UL;
		}

		// Token: 0x04000035 RID: 53
		private readonly string path;

		// Token: 0x04000036 RID: 54
		private readonly ulong minimumFreeSpace;

		// Token: 0x04000037 RID: 55
		private readonly ulong diskSize;
	}
}
