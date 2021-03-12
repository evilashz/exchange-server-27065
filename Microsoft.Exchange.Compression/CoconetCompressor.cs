using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000002 RID: 2
	internal class CoconetCompressor : DisposeTrackableBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal CoconetCompressor(long dictionarySize, int sampleRate, CoconetCompressor.LzOption lzOpt)
		{
			this.hCompressor = CoconetCompressor.GetCompressor((ulong)dictionarySize, (uint)sampleRate, (uint)lzOpt);
			if (this.hCompressor == IntPtr.Zero)
			{
				throw new CompressionOutOfMemoryException();
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002109 File Offset: 0x00000309
		internal CoconetCompressor() : this(16777216L, 8, CoconetCompressor.LzOption.LzHuf)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000211C File Offset: 0x0000031C
		internal unsafe void Compress(byte[] inBuf, int inOffset, int inLength, byte[] outBuf, int outOffset, int maxOutLength, out int compressedLength)
		{
			compressedLength = 0;
			uint num = 0U;
			fixed (byte* ptr = inBuf)
			{
				fixed (byte* ptr2 = outBuf)
				{
					int num2 = CoconetCompressor.CompressBufferCoconet(new IntPtr((void*)((byte*)ptr + inOffset)), (uint)inLength, new IntPtr((void*)((byte*)ptr2 + outOffset)), (uint)maxOutLength, ref num, this.hCompressor);
					if (num2 != 0)
					{
						throw new CompressionException(num2);
					}
				}
			}
			compressedLength = (int)num;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000219C File Offset: 0x0000039C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CoconetCompressor>(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021A4 File Offset: 0x000003A4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				IntPtr value = this.hCompressor;
				this.hCompressor = IntPtr.Zero;
				if (value != IntPtr.Zero)
				{
					CoconetCompressor.FreeCompressor(value);
				}
			}
		}

		// Token: 0x06000006 RID: 6
		[DllImport("coconet.dll")]
		private static extern IntPtr GetCompressor(ulong dictionarySize, uint sampleRate, uint lzOpt);

		// Token: 0x06000007 RID: 7
		[DllImport("coconet.dll")]
		private static extern void FreeCompressor(IntPtr hCompressor);

		// Token: 0x06000008 RID: 8
		[DllImport("coconet.dll")]
		private static extern int CompressBufferCoconet(IntPtr pInput, uint inputLen, IntPtr pOutput, uint maxOutputSize, ref uint compressedLen, IntPtr workSpace);

		// Token: 0x04000001 RID: 1
		public const long DefaultDictionarySize = 16777216L;

		// Token: 0x04000002 RID: 2
		public const int DefaultSampleRate = 8;

		// Token: 0x04000003 RID: 3
		private IntPtr hCompressor = IntPtr.Zero;

		// Token: 0x02000003 RID: 3
		public enum LzOption
		{
			// Token: 0x04000005 RID: 5
			LzNone = 1,
			// Token: 0x04000006 RID: 6
			LzLz,
			// Token: 0x04000007 RID: 7
			LzHuf,
			// Token: 0x04000008 RID: 8
			LzMax,
			// Token: 0x04000009 RID: 9
			LzHufMax,
			// Token: 0x0400000A RID: 10
			LzDefault = 3
		}

		// Token: 0x02000004 RID: 4
		public enum StatusCode
		{
			// Token: 0x0400000C RID: 12
			OK,
			// Token: 0x0400000D RID: 13
			STATUS_BUFFER_TOO_SMALL = 35,
			// Token: 0x0400000E RID: 14
			STATUS_BAD_COMPRESSION_BUFFER = 578
		}
	}
}
