using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C89 RID: 3209
	[StructLayout(LayoutKind.Sequential)]
	internal class EapKeyBlock
	{
		// Token: 0x060046E1 RID: 18145 RVA: 0x000BE85C File Offset: 0x000BCA5C
		internal EapKeyBlock(byte[] buffer)
		{
			this.rgbKeys = new byte[128];
			Buffer.BlockCopy(buffer, 0, this.rgbKeys, 0, this.rgbKeys.Length);
			this.rgbIVs = new byte[64];
			Buffer.BlockCopy(buffer, this.rgbKeys.Length, this.rgbIVs, 0, this.rgbIVs.Length);
		}

		// Token: 0x04003BCA RID: 15306
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public readonly byte[] rgbKeys;

		// Token: 0x04003BCB RID: 15307
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		public readonly byte[] rgbIVs;

		// Token: 0x04003BCC RID: 15308
		public static readonly int Size = Marshal.SizeOf(typeof(EapKeyBlock));
	}
}
