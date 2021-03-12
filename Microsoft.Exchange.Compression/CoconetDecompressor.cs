using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000005 RID: 5
	internal class CoconetDecompressor : DisposeTrackableBase
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021D9 File Offset: 0x000003D9
		public CoconetDecompressor(long dictionarySize, CoconetCompressor.LzOption lzOpt)
		{
			this.hDecompressor = CoconetDecompressor.GetDecompressor((ulong)dictionarySize, (uint)lzOpt);
			if (this.hDecompressor == IntPtr.Zero)
			{
				throw new CompressionOutOfMemoryException();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002211 File Offset: 0x00000411
		public CoconetDecompressor() : this(16777216L, CoconetCompressor.LzOption.LzHuf)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002220 File Offset: 0x00000420
		public unsafe void Decompress(byte[] inBuf, int inOffset, int inLength, byte[] outBuf, int outOffset, int expectedDecompressedLength)
		{
			uint maxOutputSize = (uint)expectedDecompressedLength;
			fixed (byte* ptr = inBuf)
			{
				fixed (byte* ptr2 = outBuf)
				{
					int num = CoconetDecompressor.DecompressBufferCoconet(new IntPtr((void*)((byte*)ptr + inOffset)), (uint)inLength, new IntPtr((void*)((byte*)ptr2 + outOffset)), maxOutputSize, ref maxOutputSize, this.hDecompressor);
					if (num != 0)
					{
						throw new DecompressionException(num);
					}
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002298 File Offset: 0x00000498
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CoconetDecompressor>(this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022A0 File Offset: 0x000004A0
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				IntPtr value = this.hDecompressor;
				this.hDecompressor = IntPtr.Zero;
				if (value != IntPtr.Zero)
				{
					CoconetDecompressor.FreeDecompressor(value);
				}
			}
		}

		// Token: 0x0600000E RID: 14
		[DllImport("coconet.dll")]
		private static extern IntPtr GetDecompressor(ulong dictionarySize, uint lzOpt);

		// Token: 0x0600000F RID: 15
		[DllImport("coconet.dll")]
		private static extern void FreeDecompressor(IntPtr hDecompressor);

		// Token: 0x06000010 RID: 16
		[DllImport("coconet.dll")]
		private static extern int DecompressBufferCoconet(IntPtr pInput, uint inputLen, IntPtr pOutput, uint maxOutputSize, ref uint decompressedLen, IntPtr workSpace);

		// Token: 0x0400000F RID: 15
		private IntPtr hDecompressor = IntPtr.Zero;
	}
}
