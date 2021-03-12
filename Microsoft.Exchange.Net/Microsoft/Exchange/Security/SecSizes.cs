using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C8A RID: 3210
	[StructLayout(LayoutKind.Sequential)]
	internal class SecSizes
	{
		// Token: 0x060046E3 RID: 18147 RVA: 0x000BE8D8 File Offset: 0x000BCAD8
		internal unsafe SecSizes(byte[] memory)
		{
			fixed (IntPtr* ptr = memory)
			{
				IntPtr ptr2 = new IntPtr((void*)ptr);
				checked
				{
					this.MaxToken = (int)((uint)Marshal.ReadInt32(ptr2));
					this.MaxSignature = (int)((uint)Marshal.ReadInt32(ptr2, 4));
					this.BlockSize = (int)((uint)Marshal.ReadInt32(ptr2, 8));
					this.SecurityTrailer = (int)((uint)Marshal.ReadInt32(ptr2, 12));
				}
			}
		}

		// Token: 0x04003BCD RID: 15309
		public readonly int MaxToken;

		// Token: 0x04003BCE RID: 15310
		public readonly int MaxSignature;

		// Token: 0x04003BCF RID: 15311
		public readonly int BlockSize;

		// Token: 0x04003BD0 RID: 15312
		public readonly int SecurityTrailer;

		// Token: 0x04003BD1 RID: 15313
		public static readonly int Size = Marshal.SizeOf(typeof(SecSizes));
	}
}
