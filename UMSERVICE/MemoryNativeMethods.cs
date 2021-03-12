using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.UM.UMService
{
	// Token: 0x02000002 RID: 2
	internal static class MemoryNativeMethods
	{
		// Token: 0x06000001 RID: 1
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GlobalMemoryStatusEx(ref MemoryNativeMethods.MemoryStatusEx memoryStatusEx);

		// Token: 0x06000002 RID: 2
		[DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetProcessMemoryInfo(IntPtr processHandle, ref MemoryNativeMethods.ProcessMemoryCounterEx counters, int size);

		// Token: 0x04000001 RID: 1
		private const string Kernel32 = "kernel32.dll";

		// Token: 0x04000002 RID: 2
		private const string PSAPI = "psapi.dll";

		// Token: 0x02000003 RID: 3
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct MemoryStatusEx
		{
			// Token: 0x06000003 RID: 3 RVA: 0x000020D0 File Offset: 0x000002D0
			public void Init()
			{
				this.length = Marshal.SizeOf(typeof(MemoryNativeMethods.MemoryStatusEx));
			}

			// Token: 0x04000003 RID: 3
			public int length;

			// Token: 0x04000004 RID: 4
			public int memoryLoad;

			// Token: 0x04000005 RID: 5
			public ulong totalPhys;

			// Token: 0x04000006 RID: 6
			public ulong availPhys;

			// Token: 0x04000007 RID: 7
			public ulong totalPageFile;

			// Token: 0x04000008 RID: 8
			public ulong availPageFile;

			// Token: 0x04000009 RID: 9
			public ulong totalVirtual;

			// Token: 0x0400000A RID: 10
			public ulong availVirtual;

			// Token: 0x0400000B RID: 11
			public ulong availExtendedVirtual;
		}

		// Token: 0x02000004 RID: 4
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct ProcessMemoryCounterEx
		{
			// Token: 0x06000004 RID: 4 RVA: 0x000020E7 File Offset: 0x000002E7
			public void Init()
			{
				this.cb = Marshal.SizeOf(typeof(MemoryNativeMethods.ProcessMemoryCounterEx));
			}

			// Token: 0x0400000C RID: 12
			public int cb;

			// Token: 0x0400000D RID: 13
			public uint pageFaultCount;

			// Token: 0x0400000E RID: 14
			public UIntPtr peakWorkingSetSize;

			// Token: 0x0400000F RID: 15
			public UIntPtr workingSetSize;

			// Token: 0x04000010 RID: 16
			public UIntPtr quotaPeakPagedPoolUsage;

			// Token: 0x04000011 RID: 17
			public UIntPtr quotaPagedPoolUsage;

			// Token: 0x04000012 RID: 18
			public UIntPtr quotaPeakNonPagedPoolUsage;

			// Token: 0x04000013 RID: 19
			public UIntPtr quotaNonPagedPoolUsage;

			// Token: 0x04000014 RID: 20
			public UIntPtr pagefileUsage;

			// Token: 0x04000015 RID: 21
			public UIntPtr peakPagefileUsage;

			// Token: 0x04000016 RID: 22
			public UIntPtr privateUsage;
		}
	}
}
