using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000062 RID: 98
	internal static class NativeMemoryMethods
	{
		// Token: 0x06000423 RID: 1059
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GlobalMemoryStatusEx(ref NativeMemoryMethods.MemoryStatusEx memoryStatusEx);

		// Token: 0x04000134 RID: 308
		private const string Kernel32 = "kernel32.dll";

		// Token: 0x04000135 RID: 309
		private const string PSAPI = "psapi.dll";

		// Token: 0x02000063 RID: 99
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct MemoryStatusEx
		{
			// Token: 0x06000424 RID: 1060 RVA: 0x00014198 File Offset: 0x00012398
			public void Init()
			{
				this.Length = Marshal.SizeOf(typeof(NativeMemoryMethods.MemoryStatusEx));
			}

			// Token: 0x04000136 RID: 310
			public int Length;

			// Token: 0x04000137 RID: 311
			public int MemoryLoad;

			// Token: 0x04000138 RID: 312
			public ulong TotalPhys;

			// Token: 0x04000139 RID: 313
			public ulong AvailPhys;

			// Token: 0x0400013A RID: 314
			public ulong TotalPageFile;

			// Token: 0x0400013B RID: 315
			public ulong AvailPageFile;

			// Token: 0x0400013C RID: 316
			public ulong TotalVirtual;

			// Token: 0x0400013D RID: 317
			public ulong AvailVirtual;

			// Token: 0x0400013E RID: 318
			public ulong AvailExtendedVirtual;
		}
	}
}
