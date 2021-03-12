using System;
using System.ComponentModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000147 RID: 327
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DataCompression : DisposeTrackableBase
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x00038158 File Offset: 0x00036358
		public DataCompression(int uncompressedBufferSize, int compressedBufferSize)
		{
			this.uncompressedBuffer = new NativeBuffer(uncompressedBufferSize);
			this.compressedBuffer = new NativeBuffer(compressedBufferSize);
			DataCompression.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "DataCompression: allocated {0} bytes for uncompressedBuffer and {1} bytes for compressedBuffer.", uncompressedBufferSize, compressedBufferSize);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00038190 File Offset: 0x00036390
		public bool TryCompress(byte[] uncompressedData, out byte[] compressedData)
		{
			this.uncompressedBuffer.CopyIn(uncompressedData);
			int num2;
			uint num = LzxInterop.RawLzxCompressBuffer(this.uncompressedBuffer.Buffer, uncompressedData.Length, this.compressedBuffer.Buffer, this.compressedBuffer.Size, out num2);
			uint num3 = num;
			if (num3 == 0U)
			{
				DataCompression.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "DataCompression: compressed input of {0} bytes into {1} bytes", uncompressedData.Length, num2);
				compressedData = this.compressedBuffer.CopyOut(num2);
				return true;
			}
			if (num3 != 122U)
			{
				DataCompression.Tracer.TraceError<int, uint>((long)this.GetHashCode(), "DataCompression: RawLzxCompressBuffer failed to compress {0} bytes, error: {0}", uncompressedData.Length, num);
				throw new Win32Exception((int)num, "RawLzxCompressBuffer");
			}
			DataCompression.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "DataCompression: unable to compressed input of {0} bytes into {1} bytes because output buffer is not large enough.", uncompressedData.Length, num2);
			compressedData = null;
			return false;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00038250 File Offset: 0x00036450
		public byte[] Decompress(byte[] compressedData, int decompressedDataSize)
		{
			if (decompressedDataSize > this.uncompressedBuffer.Size)
			{
				throw new ArgumentException("decompressedDataSize > uncompressedBuffer.Size");
			}
			this.compressedBuffer.CopyIn(compressedData);
			uint num = LzxInterop.ApplyRawLzxPatchToBuffer(IntPtr.Zero, 0, this.compressedBuffer.Buffer, compressedData.Length, this.uncompressedBuffer.Buffer, decompressedDataSize);
			if (num != 0U)
			{
				DataCompression.Tracer.TraceError<int, uint>((long)this.GetHashCode(), "DataCompression: ApplyRawLzxPatchToBuffer failed to decompress {0} bytes, error: {1}", compressedData.Length, num);
				throw new Win32Exception((int)num, "ApplyRawLzxPatchToBuffer");
			}
			DataCompression.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "DataCompression: decompressed input of {0} bytes into {1} bytes", compressedData.Length, decompressedDataSize);
			return this.uncompressedBuffer.CopyOut(decompressedDataSize);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000382F7 File Offset: 0x000364F7
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.uncompressedBuffer.Dispose();
				this.compressedBuffer.Dispose();
				DataCompression.Tracer.TraceDebug((long)this.GetHashCode(), "DataCompression: released allocated memory for uncompressedBuffer and compressedBuffer.");
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00038328 File Offset: 0x00036528
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DataCompression>(this);
		}

		// Token: 0x04000711 RID: 1809
		private static readonly Trace Tracer = ExTraceGlobals.DataTracer;

		// Token: 0x04000712 RID: 1810
		private readonly NativeBuffer uncompressedBuffer;

		// Token: 0x04000713 RID: 1811
		private readonly NativeBuffer compressedBuffer;
	}
}
