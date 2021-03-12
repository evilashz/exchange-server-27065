using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000164 RID: 356
	internal static class Xpress
	{
		// Token: 0x06000E40 RID: 3648
		[DllImport("huffman_xpress.dll")]
		private static extern int EcCompressEx(IntPtr pb, int cb, IntPtr pbOut, ref int cbOut);

		// Token: 0x06000E41 RID: 3649
		[DllImport("huffman_xpress.dll")]
		private static extern int EcDecompressEx(IntPtr pb, int cb, IntPtr pbOut, ref int cbOut);

		// Token: 0x06000E42 RID: 3650 RVA: 0x0003DADC File Offset: 0x0003BCDC
		public unsafe static bool Compress(byte[] inBuf, int inOffset, int inLength, byte[] outBuf, int outOffset, int maxOutLength, out int compressedSize)
		{
			DiagCore.RetailAssert(outBuf.Length >= outOffset + maxOutLength, "BufferOverflow: Offset({0}) + Len({1}) exceeds buffer length({2})", new object[]
			{
				outOffset,
				maxOutLength,
				outBuf.Length
			});
			compressedSize = maxOutLength;
			fixed (byte* ptr = inBuf)
			{
				fixed (byte* ptr2 = outBuf)
				{
					if (Xpress.EcCompressEx(new IntPtr((void*)((byte*)ptr + inOffset)), inLength, new IntPtr((void*)((byte*)ptr2 + outOffset)), ref compressedSize) == 0 && compressedSize < inLength)
					{
						return true;
					}
				}
			}
			compressedSize = inLength;
			Array.Copy(inBuf, inOffset, outBuf, outOffset, inLength);
			return false;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
		public unsafe static bool Decompress(byte[] inBuf, int inOffset, int inLength, byte[] outBuf, int outOffset, int expectedDecompressedLength)
		{
			if (expectedDecompressedLength == inLength)
			{
				Array.Copy(inBuf, inOffset, outBuf, outOffset, inLength);
				return true;
			}
			int num = expectedDecompressedLength;
			fixed (byte* ptr = inBuf)
			{
				fixed (byte* ptr2 = outBuf)
				{
					if (Xpress.EcDecompressEx(new IntPtr((void*)((byte*)ptr + inOffset)), inLength, new IntPtr((void*)((byte*)ptr2 + outOffset)), ref num) == 0)
					{
						bool result;
						if (num != expectedDecompressedLength)
						{
							result = false;
						}
						else
						{
							result = true;
						}
						return result;
					}
				}
			}
			return false;
		}

		// Token: 0x040005DD RID: 1501
		public const int MaxXpressBlockSize = 65536;

		// Token: 0x040005DE RID: 1502
		public const int MinXpressBlockSize = 265;
	}
}
