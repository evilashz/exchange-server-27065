using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200001A RID: 26
	internal static class Xpress
	{
		// Token: 0x060000B1 RID: 177
		[DllImport("huffman_xpress.dll")]
		private static extern int EcCompressEx(IntPtr pb, int cb, IntPtr pbOut, ref int cbOut);

		// Token: 0x060000B2 RID: 178
		[DllImport("huffman_xpress.dll")]
		private static extern int EcDecompressEx(IntPtr pb, int cb, IntPtr pbOut, ref int cbOut);

		// Token: 0x060000B3 RID: 179 RVA: 0x000039AC File Offset: 0x00001BAC
		public unsafe static bool Compress(byte[] inBuf, int inOffset, int inLength, byte[] outBuf, int outOffset, int maxOutLength, out int compressedSize)
		{
			Dependencies.AssertRtl(outBuf.Length >= outOffset + maxOutLength, "BufferOverflow: Offset({0}) + Len({1}) exceeds buffer length({2})", new object[]
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

		// Token: 0x060000B4 RID: 180 RVA: 0x00003A70 File Offset: 0x00001C70
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

		// Token: 0x0400004F RID: 79
		public const int MaxXpressBlockSize = 65536;

		// Token: 0x04000050 RID: 80
		public const int MinXpressBlockSize = 265;
	}
}
