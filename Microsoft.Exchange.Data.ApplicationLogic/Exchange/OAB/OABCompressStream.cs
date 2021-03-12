using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000151 RID: 337
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OABCompressStream : Stream
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x00038BB4 File Offset: 0x00036DB4
		public OABCompressStream(Stream stream, int maximumCompressionBlockSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("stream must allow seek");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("stream must allow write");
			}
			this.stream = stream;
			this.maximumCompressionBlockSize = maximumCompressionBlockSize;
			this.writer = new BinaryWriter(new NoCloseStream(stream));
			this.compressor = new DataCompression(maximumCompressionBlockSize, maximumCompressionBlockSize);
			this.buffer = new ByteQueue(maximumCompressionBlockSize * 2);
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00038C35 File Offset: 0x00036E35
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00038C38 File Offset: 0x00036E38
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00038C3B File Offset: 0x00036E3B
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00038C3E File Offset: 0x00036E3E
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00038C4B File Offset: 0x00036E4B
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x00038C58 File Offset: 0x00036E58
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				throw new InvalidOperationException("set Position");
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00038C64 File Offset: 0x00036E64
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00038C71 File Offset: 0x00036E71
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("Read'");
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00038C7D File Offset: 0x00036E7D
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException("Seek'");
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00038C89 File Offset: 0x00036E89
		public override void SetLength(long value)
		{
			throw new InvalidOperationException("SetLength'");
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00038C98 File Offset: 0x00036E98
		public override void Close()
		{
			try
			{
				while (this.buffer.Count > 0)
				{
					byte[] data = this.buffer.Dequeue(this.maximumCompressionBlockSize);
					this.WriteData(data);
				}
				this.stream.Seek(0L, SeekOrigin.Begin);
				this.WriteHeader(this.uncompressedLength);
			}
			finally
			{
				this.writer.Dispose();
				this.compressor.Dispose();
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00038D14 File Offset: 0x00036F14
		public override void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0 || this.buffer.Count >= this.maximumCompressionBlockSize)
			{
				if (this.buffer.Count >= this.maximumCompressionBlockSize)
				{
					byte[] data = this.buffer.Dequeue(this.maximumCompressionBlockSize);
					this.WriteData(data);
				}
				if (count > 0)
				{
					int num = Math.Min(this.maximumCompressionBlockSize, count);
					this.buffer.Enqueue(num, buffer, offset);
					count -= num;
					offset += num;
				}
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00038D90 File Offset: 0x00036F90
		private void WriteData(byte[] data)
		{
			if (!this.header)
			{
				this.WriteHeader(0U);
				this.header = true;
			}
			this.uncompressedLength += (uint)data.Length;
			byte[] compressedData;
			if (this.compressor.TryCompress(data, out compressedData))
			{
				this.WriteBlock(CompressionBlockFlags.Compressed, data, compressedData);
				return;
			}
			this.WriteBlock(CompressionBlockFlags.NotCompressed, data, data);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00038DE8 File Offset: 0x00036FE8
		private void WriteHeader(uint uncompressedFileSize)
		{
			OABCompressedHeader oabcompressedHeader = new OABCompressedHeader
			{
				MaxVersion = OABCompressedHeader.DefaultMaxVersion,
				MinVersion = OABCompressedHeader.DefaultMinVersion,
				MaximumCompressionBlockSize = this.maximumCompressionBlockSize,
				UncompressedFileSize = uncompressedFileSize
			};
			OABCompressStream.Tracer.TraceDebug<long, OABCompressedHeader>((long)this.GetHashCode(), "OABCompressStream: writing header at stream position {0}:\n\r{1}", this.stream.Position, oabcompressedHeader);
			oabcompressedHeader.WriteTo(this.writer);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00038E54 File Offset: 0x00037054
		private void WriteBlock(CompressionBlockFlags compressionBlockFlags, byte[] uncompressedData, byte[] compressedData)
		{
			OABCompressedBlock oabcompressedBlock = new OABCompressedBlock
			{
				Flags = compressionBlockFlags,
				CompressedLength = compressedData.Length,
				UncompressedLength = uncompressedData.Length,
				CRC = OABCRC.ComputeCRC(OABCRC.DefaultSeed, uncompressedData),
				Data = compressedData
			};
			OABCompressStream.Tracer.TraceDebug<long, OABCompressedBlock>((long)this.GetHashCode(), "OABCompressStream: writing block at stream position {0}:\n\r{1}", this.stream.Position, oabcompressedBlock);
			oabcompressedBlock.WriteTo(this.writer);
		}

		// Token: 0x04000733 RID: 1843
		private static readonly Trace Tracer = ExTraceGlobals.DataTracer;

		// Token: 0x04000734 RID: 1844
		private readonly Stream stream;

		// Token: 0x04000735 RID: 1845
		private readonly int maximumCompressionBlockSize;

		// Token: 0x04000736 RID: 1846
		private readonly BinaryWriter writer;

		// Token: 0x04000737 RID: 1847
		private readonly DataCompression compressor;

		// Token: 0x04000738 RID: 1848
		private readonly ByteQueue buffer;

		// Token: 0x04000739 RID: 1849
		private uint uncompressedLength;

		// Token: 0x0400073A RID: 1850
		private bool header;
	}
}
