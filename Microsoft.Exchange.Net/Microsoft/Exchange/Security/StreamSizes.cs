using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C94 RID: 3220
	[StructLayout(LayoutKind.Sequential)]
	internal class StreamSizes
	{
		// Token: 0x060046F1 RID: 18161 RVA: 0x000BEC9C File Offset: 0x000BCE9C
		internal unsafe StreamSizes(byte[] memory)
		{
			fixed (IntPtr* ptr = memory)
			{
				IntPtr ptr2 = new IntPtr((void*)ptr);
				checked
				{
					this.Header = (int)((uint)Marshal.ReadInt32(ptr2));
					this.Trailer = (int)((uint)Marshal.ReadInt32(ptr2, 4));
					this.MaxMessage = (int)((uint)Marshal.ReadInt32(ptr2, 8));
					this.BufferCount = (int)((uint)Marshal.ReadInt32(ptr2, 12));
					this.BlockSize = (int)((uint)Marshal.ReadInt32(ptr2, 16));
				}
			}
		}

		// Token: 0x04003C1B RID: 15387
		public readonly int Header;

		// Token: 0x04003C1C RID: 15388
		public readonly int Trailer;

		// Token: 0x04003C1D RID: 15389
		public readonly int MaxMessage;

		// Token: 0x04003C1E RID: 15390
		public readonly int BufferCount;

		// Token: 0x04003C1F RID: 15391
		public readonly int BlockSize;

		// Token: 0x04003C20 RID: 15392
		public static readonly int Size = Marshal.SizeOf(typeof(StreamSizes));
	}
}
