using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000152 RID: 338
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OABDecompressStream : Stream
	{
		// Token: 0x06000DA0 RID: 3488 RVA: 0x00038ED4 File Offset: 0x000370D4
		public OABDecompressStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("stream must allow read");
			}
			this.stream = new NoCloseStream(stream);
			this.reader = new BinaryReader(this.stream);
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00038F25 File Offset: 0x00037125
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00038F28 File Offset: 0x00037128
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00038F2B File Offset: 0x0003712B
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00038F2E File Offset: 0x0003712E
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00038F3B File Offset: 0x0003713B
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00038F43 File Offset: 0x00037143
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				throw new InvalidOperationException("set_Position");
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00038F4F File Offset: 0x0003714F
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00038F5C File Offset: 0x0003715C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException("Seek'");
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00038F68 File Offset: 0x00037168
		public override void SetLength(long value)
		{
			throw new InvalidOperationException("SetLength'");
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00038F74 File Offset: 0x00037174
		public override void Close()
		{
			this.reader.Dispose();
			if (this.decompressor != null)
			{
				this.decompressor.Dispose();
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00038F94 File Offset: 0x00037194
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("Write'");
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00038FA0 File Offset: 0x000371A0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.stage == 0)
			{
				this.ReadHeader();
				this.stage = 1;
			}
			int num = 0;
			if (this.stage == 1)
			{
				for (;;)
				{
					int num2 = this.buffer.Dequeue(count, buffer, offset);
					count -= num2;
					offset += num2;
					num += num2;
					if (count == 0)
					{
						goto IL_6F;
					}
					if (this.uncompressedLength == this.header.UncompressedFileSize)
					{
						break;
					}
					this.buffer.Enqueue(this.ReadBlock());
				}
				this.stage = 2;
			}
			IL_6F:
			this.position += (long)num;
			return num;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003902C File Offset: 0x0003722C
		private void ReadHeader()
		{
			long arg = this.reader.BaseStream.Position;
			this.header = OABCompressedHeader.ReadFrom(this.reader);
			OABDecompressStream.Tracer.TraceDebug<long, OABCompressedHeader>((long)this.GetHashCode(), "OABDecompressStream: read OABCompressedHeader at position {0}:\r\n{1}", arg, this.header);
			if (this.header.MaxVersion != OABCompressedHeader.DefaultMaxVersion)
			{
				throw new InvalidDataException(string.Format("MaxVersion: expected={0}, actual={1}", OABCompressedHeader.DefaultMaxVersion, this.header.MaxVersion));
			}
			if (this.header.MinVersion != OABCompressedHeader.DefaultMinVersion)
			{
				throw new InvalidDataException(string.Format("MinVersion: expected={0}, actual={1}", OABCompressedHeader.DefaultMinVersion, this.header.MinVersion));
			}
			this.decompressor = new DataCompression(this.header.MaximumCompressionBlockSize, this.header.MaximumCompressionBlockSize);
			this.buffer = new ByteQueue(this.header.MaximumCompressionBlockSize);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00039128 File Offset: 0x00037328
		private byte[] ReadBlock()
		{
			long num = this.reader.BaseStream.Position;
			OABCompressedBlock oabcompressedBlock = OABCompressedBlock.ReadFrom(this.reader);
			OABDecompressStream.Tracer.TraceDebug<long, OABCompressedBlock>((long)this.GetHashCode(), "OABDecompressStream: read OABCompressedBlock at position {0}:\r\n{1}", num, oabcompressedBlock);
			if (oabcompressedBlock.CompressedLength > this.header.MaximumCompressionBlockSize)
			{
				throw new InvalidDataException(string.Format("Compressed block starting at position {0}: data is larger than header stated. MaximumCompressionBlockSize={1}, CompressedLength={2}", num, this.header.MaximumCompressionBlockSize, oabcompressedBlock.CompressedLength));
			}
			if (oabcompressedBlock.UncompressedLength > this.header.MaximumCompressionBlockSize)
			{
				throw new InvalidDataException(string.Format("Compressed block starting at position {0}: data is larger than header stated. MaximumCompressionBlockSize={1}, UncompressedLength={2}", num, this.header.MaximumCompressionBlockSize, oabcompressedBlock.UncompressedLength));
			}
			byte[] array;
			if (oabcompressedBlock.Flags == CompressionBlockFlags.Compressed)
			{
				try
				{
					array = this.decompressor.Decompress(oabcompressedBlock.Data, oabcompressedBlock.UncompressedLength);
					goto IL_FF;
				}
				catch (Win32Exception innerException)
				{
					throw new InvalidDataException(string.Format("Compressed block starting at position {0}: unable to decompress data", num), innerException);
				}
			}
			array = oabcompressedBlock.Data;
			IL_FF:
			uint num2 = OABCRC.ComputeCRC(OABCRC.DefaultSeed, array);
			if (num2 != oabcompressedBlock.CRC)
			{
				throw new InvalidDataException(string.Format("Compressed block starting at position {0}: invalid CRC. Expected: {1:X8}, actual: {2:X8}", num, num2, oabcompressedBlock.CRC));
			}
			this.uncompressedLength += (uint)array.Length;
			if (this.uncompressedLength > this.header.UncompressedFileSize)
			{
				throw new InvalidDataException(string.Format("Compressed block starting at position {0}: decompressed data so far is already longer than header stated. Header size: {1}, so far: {2}.", num, this.header.UncompressedFileSize, this.uncompressedLength));
			}
			return array;
		}

		// Token: 0x0400073B RID: 1851
		private static readonly Trace Tracer = ExTraceGlobals.DataTracer;

		// Token: 0x0400073C RID: 1852
		private readonly Stream stream;

		// Token: 0x0400073D RID: 1853
		private readonly BinaryReader reader;

		// Token: 0x0400073E RID: 1854
		private DataCompression decompressor;

		// Token: 0x0400073F RID: 1855
		private OABCompressedHeader header;

		// Token: 0x04000740 RID: 1856
		private uint uncompressedLength;

		// Token: 0x04000741 RID: 1857
		private long position;

		// Token: 0x04000742 RID: 1858
		private int stage;

		// Token: 0x04000743 RID: 1859
		private ByteQueue buffer;
	}
}
