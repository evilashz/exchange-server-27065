using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000022 RID: 34
	internal static class DiskIoControl
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00005A5C File Offset: 0x00003C5C
		public static int GetDiskReadLatency(string volumeName, ref DiskPerformanceStructure lastDiskPerf, out DateTime lastUpdatedTime)
		{
			DiskPerformanceStructure diskPerformance = DiskIoControl.GetDiskPerformance(volumeName);
			long num = diskPerformance.ReadTime - lastDiskPerf.ReadTime;
			int num2 = diskPerformance.ReadCount - lastDiskPerf.ReadCount;
			int result = 0;
			if (num < 0L || num2 < 0)
			{
				result = int.MaxValue;
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<string, long, int>(0L, "[DiskIoControl.GetDiskReadLatency] Volume: {0}. Set readLatency to int.MaxValue. Reported readTime: {1}, reported readCount: {2}", volumeName, num, num2);
			}
			else if (num2 != 0)
			{
				try
				{
					long value = num / checked(unchecked((long)num2) * 10000L);
					result = Convert.ToInt32(value);
				}
				catch (OverflowException arg)
				{
					result = int.MaxValue;
					ExTraceGlobals.ResourceHealthManagerTracer.TraceError<string, OverflowException>(0L, "[DiskIoControl.GetDiskReadLatency] Volume: {0}. Set readLatency to int.MaxValue. Error: {1}", volumeName, arg);
				}
			}
			lastDiskPerf = diskPerformance;
			lastUpdatedTime = DateTime.UtcNow;
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005B10 File Offset: 0x00003D10
		public static DiskPerformanceStructure GetDiskPerformance(string volumeName)
		{
			return DiskIoControl.GetDeviceIoControlReturnedStructures<DiskPerformanceStructure>("\\\\.\\PhysicalDrive" + DiskIoControl.GetDeviceIoControlReturnedStructures<DiskExtents>(volumeName.TrimEnd(new char[]
			{
				'\\'
			}), 5636096U).Extents.DiskNumber, 458784U);
		}

		// Token: 0x06000123 RID: 291
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000124 RID: 292
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern uint DeviceIoControl(SafeFileHandle hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, ref uint lpBytesReturned, IntPtr lpOverlapped);

		// Token: 0x06000125 RID: 293 RVA: 0x00005B60 File Offset: 0x00003D60
		private static T GetDeviceIoControlReturnedStructures<T>(string pathToDevice, uint ioControlCode) where T : struct
		{
			T result;
			using (SafeFileHandle safeFileHandle = DiskIoControl.CreateFile(pathToDevice, 0U, 3U, 0, 3U, 0U, 0))
			{
				if (safeFileHandle == null || safeFileHandle.IsInvalid)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				int num = Marshal.SizeOf(typeof(T));
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				uint num2 = 0U;
				if (DiskIoControl.DeviceIoControl(safeFileHandle, ioControlCode, IntPtr.Zero, 0U, intPtr, num, ref num2, IntPtr.Zero) == 0U)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				result = (T)((object)Marshal.PtrToStructure(intPtr, typeof(T)));
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x04000098 RID: 152
		private const int IoctlDiskPerformance = 458784;

		// Token: 0x04000099 RID: 153
		private const int IoctlVolumeGetVolumeDiskExtents = 5636096;

		// Token: 0x0400009A RID: 154
		private const uint DriveDesiredAccessNoAccess = 0U;

		// Token: 0x0400009B RID: 155
		private const uint DriveShareModeReadWrite = 3U;

		// Token: 0x0400009C RID: 156
		private const uint DriveDispositionOpenExisting = 3U;

		// Token: 0x0400009D RID: 157
		private const uint DriveFlagsAndAttributesNone = 0U;

		// Token: 0x0400009E RID: 158
		private const string DriveNameTemplate = "\\\\.\\PhysicalDrive";
	}
}
