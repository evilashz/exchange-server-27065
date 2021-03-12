using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000151 RID: 337
	public class ConverterStream : Stream, IProgressMonitor
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x0006FBAC File Offset: 0x0006DDAC
		public ConverterStream(Stream stream, TextConverter converter, ConverterStreamAccess access)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			if (access < ConverterStreamAccess.Read || ConverterStreamAccess.Write < access)
			{
				throw new ArgumentException(TextConvertersStrings.AccessShouldBeReadOrWrite, "access");
			}
			if (access == ConverterStreamAccess.Read)
			{
				if (!stream.CanRead)
				{
					throw new ArgumentException(TextConvertersStrings.CannotReadFromSource, "stream");
				}
				this.producer = converter.CreatePullChain(stream, this);
			}
			else
			{
				if (!stream.CanWrite)
				{
					throw new ArgumentException(TextConvertersStrings.CannotWriteToDestination, "stream");
				}
				this.consumer = converter.CreatePushChain(this, stream);
			}
			this.sourceOrDestination = stream;
			this.maxLoopsWithoutProgress = 100000 + converter.InputStreamBufferSize + converter.OutputStreamBufferSize;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0006FC64 File Offset: 0x0006DE64
		public ConverterStream(TextReader sourceReader, TextConverter converter)
		{
			if (sourceReader == null)
			{
				throw new ArgumentNullException("sourceReader");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			this.producer = converter.CreatePullChain(sourceReader, this);
			this.sourceOrDestination = sourceReader;
			this.maxLoopsWithoutProgress = 100000 + converter.InputStreamBufferSize + converter.OutputStreamBufferSize;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0006FCC4 File Offset: 0x0006DEC4
		public ConverterStream(TextWriter destinationWriter, TextConverter converter)
		{
			if (destinationWriter == null)
			{
				throw new ArgumentNullException("destinationWriter");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			this.consumer = converter.CreatePushChain(this, destinationWriter);
			this.sourceOrDestination = destinationWriter;
			this.maxLoopsWithoutProgress = 100000 + converter.InputStreamBufferSize + converter.OutputStreamBufferSize;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0006FD21 File Offset: 0x0006DF21
		public override bool CanRead
		{
			get
			{
				return this.producer != null;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0006FD2E File Offset: 0x0006DF2E
		public override bool CanWrite
		{
			get
			{
				return this.consumer != null;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0006FD3B File Offset: 0x0006DF3B
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0006FD3E File Offset: 0x0006DF3E
		public override long Length
		{
			get
			{
				throw new NotSupportedException(TextConvertersStrings.SeekUnsupported);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0006FD4A File Offset: 0x0006DF4A
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x0006FD56 File Offset: 0x0006DF56
		public override long Position
		{
			get
			{
				throw new NotSupportedException(TextConvertersStrings.SeekUnsupported);
			}
			set
			{
				throw new NotSupportedException(TextConvertersStrings.SeekUnsupported);
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0006FD62 File Offset: 0x0006DF62
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(TextConvertersStrings.SeekUnsupported);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0006FD6E File Offset: 0x0006DF6E
		public override void SetLength(long value)
		{
			throw new NotSupportedException(TextConvertersStrings.SeekUnsupported);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0006FD7C File Offset: 0x0006DF7C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.sourceOrDestination == null)
			{
				throw new ObjectDisposedException("ConverterStream");
			}
			if (this.consumer == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.WriteUnsupported);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TextConvertersStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountTooLarge);
			}
			if (this.endOfFile)
			{
				throw new InvalidOperationException(TextConvertersStrings.WriteAfterFlush);
			}
			if (this.inconsistentState)
			{
				throw new InvalidOperationException(TextConvertersStrings.ConverterStreamInInconsistentStare);
			}
			this.chunkToReadBuffer = buffer;
			this.chunkToReadOffset = offset;
			this.chunkToReadCount = count;
			long num = 0L;
			this.inconsistentState = true;
			while (this.chunkToReadCount != 0)
			{
				this.consumer.Run();
				if (this.madeProgress)
				{
					num = 0L;
					this.madeProgress = false;
				}
				else
				{
					long num2 = (long)this.maxLoopsWithoutProgress;
					long num3 = num;
					num = num3 + 1L;
					if (num2 == num3)
					{
						throw new TextConvertersException(TextConvertersStrings.TooManyIterationsToProcessInput);
					}
				}
			}
			this.inconsistentState = false;
			this.chunkToReadBuffer = null;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0006FEA0 File Offset: 0x0006E0A0
		public override void Flush()
		{
			if (this.sourceOrDestination == null)
			{
				throw new ObjectDisposedException("ConverterStream");
			}
			if (this.consumer == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.WriteUnsupported);
			}
			this.endOfFile = true;
			if (!this.inconsistentState)
			{
				long num = 0L;
				this.inconsistentState = true;
				while (!this.consumer.Flush())
				{
					if (this.madeProgress)
					{
						num = 0L;
						this.madeProgress = false;
					}
					else
					{
						long num2 = (long)this.maxLoopsWithoutProgress;
						long num3 = num;
						num = num3 + 1L;
						if (num2 == num3)
						{
							throw new TextConvertersException(TextConvertersStrings.TooManyIterationsToFlushConverter);
						}
					}
				}
				this.inconsistentState = false;
			}
			if (this.sourceOrDestination is Stream)
			{
				((Stream)this.sourceOrDestination).Flush();
				return;
			}
			if (this.sourceOrDestination is TextWriter)
			{
				((TextWriter)this.sourceOrDestination).Flush();
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0006FF6C File Offset: 0x0006E16C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.sourceOrDestination != null && this.consumer != null && !this.inconsistentState)
					{
						this.Flush();
					}
					if (this.producer != null && this.producer is IDisposable)
					{
						((IDisposable)this.producer).Dispose();
					}
					if (this.consumer != null && this.consumer is IDisposable)
					{
						((IDisposable)this.consumer).Dispose();
					}
				}
			}
			finally
			{
				if (disposing && this.sourceOrDestination != null)
				{
					if (this.sourceOrDestination is Stream)
					{
						((Stream)this.sourceOrDestination).Dispose();
					}
					else if (this.sourceOrDestination is TextReader)
					{
						((TextReader)this.sourceOrDestination).Dispose();
					}
					else
					{
						((TextWriter)this.sourceOrDestination).Dispose();
					}
				}
				this.sourceOrDestination = null;
				this.consumer = null;
				this.producer = null;
				this.chunkToReadBuffer = null;
				this.writeBuffer = null;
				this.byteSource = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00070084 File Offset: 0x0006E284
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.sourceOrDestination == null)
			{
				throw new ObjectDisposedException("ConverterStream");
			}
			if (this.producer == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.ReadUnsupported);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TextConvertersStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountTooLarge);
			}
			if (this.inconsistentState)
			{
				throw new InvalidOperationException(TextConvertersStrings.ConverterStreamInInconsistentStare);
			}
			int num = count;
			if (this.byteSource != null)
			{
				byte[] src;
				int srcOffset;
				int val;
				while (count != 0 && this.byteSource.GetOutputChunk(out src, out srcOffset, out val))
				{
					int num2 = Math.Min(val, count);
					Buffer.BlockCopy(src, srcOffset, buffer, offset, num2);
					offset += num2;
					count -= num2;
					this.byteSource.ReportOutput(num2);
				}
			}
			if (count != 0)
			{
				long num3 = 0L;
				this.writeBuffer = buffer;
				this.writeOffset = offset;
				this.writeCount = count;
				this.inconsistentState = true;
				while (this.writeCount != 0 && !this.endOfFile)
				{
					this.producer.Run();
					if (this.madeProgress)
					{
						num3 = 0L;
						this.madeProgress = false;
					}
					else
					{
						long num4 = (long)this.maxLoopsWithoutProgress;
						long num5 = num3;
						num3 = num5 + 1L;
						if (num4 == num5)
						{
							throw new TextConvertersException(TextConvertersStrings.TooManyIterationsToProduceOutput);
						}
					}
				}
				count = this.writeCount;
				this.writeBuffer = null;
				this.writeOffset = 0;
				this.writeCount = 0;
				this.inconsistentState = false;
			}
			return num - count;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0007020E File Offset: 0x0006E40E
		internal void SetSource(IByteSource byteSource)
		{
			this.byteSource = byteSource;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00070217 File Offset: 0x0006E417
		internal void GetOutputBuffer(out byte[] outputBuffer, out int outputOffset, out int outputCount)
		{
			outputBuffer = this.writeBuffer;
			outputOffset = this.writeOffset;
			outputCount = this.writeCount;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00070231 File Offset: 0x0006E431
		internal void ReportOutput(int outputCount)
		{
			if (outputCount != 0)
			{
				this.madeProgress = true;
				this.writeCount -= outputCount;
				this.writeOffset += outputCount;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00070259 File Offset: 0x0006E459
		internal void ReportEndOfFile()
		{
			this.endOfFile = true;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00070264 File Offset: 0x0006E464
		internal bool GetInputChunk(out byte[] chunkBuffer, out int chunkOffset, out int chunkCount, out bool eof)
		{
			chunkBuffer = this.chunkToReadBuffer;
			chunkOffset = this.chunkToReadOffset;
			chunkCount = this.chunkToReadCount;
			eof = (this.endOfFile && 0 == this.chunkToReadCount);
			return this.chunkToReadCount != 0 || this.endOfFile;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000702B0 File Offset: 0x0006E4B0
		internal void ReportRead(int readCount)
		{
			if (readCount != 0)
			{
				this.madeProgress = true;
				this.chunkToReadCount -= readCount;
				this.chunkToReadOffset += readCount;
				if (this.chunkToReadCount == 0)
				{
					this.chunkToReadBuffer = null;
					this.chunkToReadOffset = 0;
				}
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000702EE File Offset: 0x0006E4EE
		void IProgressMonitor.ReportProgress()
		{
			this.madeProgress = true;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000702F8 File Offset: 0x0006E4F8
		internal void Reuse(object newSourceOrSink)
		{
			if (this.producer != null)
			{
				if (!(this.producer is IReusable))
				{
					throw new NotSupportedException("this converter is not reusable");
				}
				((IReusable)this.producer).Initialize(newSourceOrSink);
			}
			else
			{
				if (!(this.consumer is IReusable))
				{
					throw new NotSupportedException("this converter is not reusable");
				}
				((IReusable)this.consumer).Initialize(newSourceOrSink);
			}
			this.sourceOrDestination = newSourceOrSink;
			this.chunkToReadBuffer = null;
			this.chunkToReadOffset = 0;
			this.chunkToReadCount = 0;
			this.writeBuffer = null;
			this.writeOffset = 0;
			this.writeCount = 0;
			this.endOfFile = false;
			this.inconsistentState = false;
		}

		// Token: 0x04000F6F RID: 3951
		private IProducerConsumer consumer;

		// Token: 0x04000F70 RID: 3952
		private int maxLoopsWithoutProgress;

		// Token: 0x04000F71 RID: 3953
		private bool madeProgress;

		// Token: 0x04000F72 RID: 3954
		private byte[] chunkToReadBuffer;

		// Token: 0x04000F73 RID: 3955
		private int chunkToReadOffset;

		// Token: 0x04000F74 RID: 3956
		private int chunkToReadCount;

		// Token: 0x04000F75 RID: 3957
		private IByteSource byteSource;

		// Token: 0x04000F76 RID: 3958
		private IProducerConsumer producer;

		// Token: 0x04000F77 RID: 3959
		private byte[] writeBuffer;

		// Token: 0x04000F78 RID: 3960
		private int writeOffset;

		// Token: 0x04000F79 RID: 3961
		private int writeCount;

		// Token: 0x04000F7A RID: 3962
		private object sourceOrDestination;

		// Token: 0x04000F7B RID: 3963
		private bool endOfFile;

		// Token: 0x04000F7C RID: 3964
		private bool inconsistentState;
	}
}
