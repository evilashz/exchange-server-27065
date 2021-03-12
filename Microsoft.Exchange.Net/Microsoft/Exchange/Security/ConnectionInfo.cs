using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C88 RID: 3208
	[StructLayout(LayoutKind.Sequential)]
	internal class ConnectionInfo
	{
		// Token: 0x060046DE RID: 18142 RVA: 0x000BE798 File Offset: 0x000BC998
		internal unsafe ConnectionInfo(byte[] memory)
		{
			fixed (IntPtr* ptr = memory)
			{
				IntPtr ptr2 = new IntPtr((void*)ptr);
				checked
				{
					this.Protocol = (int)((uint)Marshal.ReadInt32(ptr2));
					this.Cipher = (int)((uint)Marshal.ReadInt32(ptr2, 4));
					this.CipherStrength = (int)((uint)Marshal.ReadInt32(ptr2, 8));
					this.MACHashAlgorithm = (int)((uint)Marshal.ReadInt32(ptr2, 12));
					this.MACHashStrength = (int)((uint)Marshal.ReadInt32(ptr2, 16));
					this.KeyExchangeAlgorithm = (int)((uint)Marshal.ReadInt32(ptr2, 20));
					this.KeyExchangeStrength = (int)((uint)Marshal.ReadInt32(ptr2, 24));
				}
			}
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x000BE833 File Offset: 0x000BCA33
		private ConnectionInfo()
		{
		}

		// Token: 0x04003BC1 RID: 15297
		public readonly int Protocol;

		// Token: 0x04003BC2 RID: 15298
		public readonly int Cipher;

		// Token: 0x04003BC3 RID: 15299
		public readonly int CipherStrength;

		// Token: 0x04003BC4 RID: 15300
		public readonly int MACHashAlgorithm;

		// Token: 0x04003BC5 RID: 15301
		public readonly int MACHashStrength;

		// Token: 0x04003BC6 RID: 15302
		public readonly int KeyExchangeAlgorithm;

		// Token: 0x04003BC7 RID: 15303
		public readonly int KeyExchangeStrength;

		// Token: 0x04003BC8 RID: 15304
		public static readonly ConnectionInfo Empty = new ConnectionInfo();

		// Token: 0x04003BC9 RID: 15305
		public static readonly int Size = Marshal.SizeOf(typeof(ConnectionInfo));
	}
}
