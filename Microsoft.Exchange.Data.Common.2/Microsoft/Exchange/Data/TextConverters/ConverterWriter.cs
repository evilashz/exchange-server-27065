using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000152 RID: 338
	public class ConverterWriter : TextWriter, IProgressMonitor
	{
		// Token: 0x06000D1A RID: 3354 RVA: 0x000703A0 File Offset: 0x0006E5A0
		public ConverterWriter(Stream destinationStream, TextConverter converter)
		{
			if (destinationStream == null)
			{
				throw new ArgumentNullException("destinationStream");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			if (!destinationStream.CanWrite)
			{
				throw new ArgumentException(TextConvertersStrings.CannotWriteToDestination, "destinationStream");
			}
			this.consumer = converter.CreatePushChain(this, destinationStream);
			this.destination = destinationStream;
			this.boundaryTesting = converter.TestBoundaryConditions;
			this.maxLoopsWithoutProgress = 100000 + converter.InputStreamBufferSize + converter.OutputStreamBufferSize;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00070424 File Offset: 0x0006E624
		public ConverterWriter(TextWriter destinationWriter, TextConverter converter)
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
			this.destination = destinationWriter;
			this.boundaryTesting = converter.TestBoundaryConditions;
			this.maxLoopsWithoutProgress = 100000 + converter.InputStreamBufferSize + converter.OutputStreamBufferSize;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0007048D File Offset: 0x0006E68D
		public override Encoding Encoding
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00070490 File Offset: 0x0006E690
		public override void Flush()
		{
			if (this.destination == null)
			{
				throw new ObjectDisposedException("ConverterWriter");
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
			if (this.destination is Stream)
			{
				((Stream)this.destination).Flush();
				return;
			}
			((TextWriter)this.destination).Flush();
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0007053C File Offset: 0x0006E73C
		public override void Write(char value)
		{
			if (this.destination == null)
			{
				throw new ObjectDisposedException("ConverterWriter");
			}
			if (this.inconsistentState)
			{
				throw new InvalidOperationException(TextConvertersStrings.ConverterWriterInInconsistentStare);
			}
			int parseCount = 10000;
			if (!this.boundaryTesting)
			{
				char[] array;
				int num;
				int num2;
				this.sinkInputObject.GetInputBuffer(out array, out num, out num2, out parseCount);
				if (num2 >= 1)
				{
					array[num] = value;
					this.sinkInputObject.Commit(1);
					return;
				}
			}
			char[] buffer = new char[]
			{
				value
			};
			this.WriteBig(buffer, 0, 1, parseCount);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000705C4 File Offset: 0x0006E7C4
		public override void Write(char[] buffer)
		{
			if (this.destination == null)
			{
				throw new ObjectDisposedException("ConverterWriter");
			}
			if (this.inconsistentState)
			{
				throw new InvalidOperationException(TextConvertersStrings.ConverterWriterInInconsistentStare);
			}
			if (buffer == null)
			{
				return;
			}
			int parseCount = 10000;
			if (!this.boundaryTesting)
			{
				char[] dst;
				int num;
				int num2;
				this.sinkInputObject.GetInputBuffer(out dst, out num, out num2, out parseCount);
				if (num2 >= buffer.Length)
				{
					Buffer.BlockCopy(buffer, 0, dst, num * 2, buffer.Length * 2);
					this.sinkInputObject.Commit(buffer.Length);
					return;
				}
			}
			this.WriteBig(buffer, 0, buffer.Length, parseCount);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00070650 File Offset: 0x0006E850
		public override void Write(char[] buffer, int index, int count)
		{
			if (this.destination == null)
			{
				throw new ObjectDisposedException("ConverterWriter");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index", TextConvertersStrings.IndexOutOfRange);
			}
			if (count < 0 || count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountOutOfRange);
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count", TextConvertersStrings.CountTooLarge);
			}
			if (this.inconsistentState)
			{
				throw new InvalidOperationException(TextConvertersStrings.ConverterWriterInInconsistentStare);
			}
			int parseCount = 10000;
			if (!this.boundaryTesting)
			{
				char[] dst;
				int num;
				int num2;
				this.sinkInputObject.GetInputBuffer(out dst, out num, out num2, out parseCount);
				if (num2 >= count)
				{
					Buffer.BlockCopy(buffer, index * 2, dst, num * 2, count * 2);
					this.sinkInputObject.Commit(count);
					return;
				}
			}
			this.WriteBig(buffer, index, count, parseCount);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0007072C File Offset: 0x0006E92C
		public override void Write(string value)
		{
			if (this.destination == null)
			{
				throw new ObjectDisposedException("ConverterWriter");
			}
			if (this.inconsistentState)
			{
				throw new InvalidOperationException(TextConvertersStrings.ConverterWriterInInconsistentStare);
			}
			if (value == null)
			{
				return;
			}
			int parseCount = 10000;
			if (!this.boundaryTesting)
			{
				char[] array;
				int destinationIndex;
				int num;
				this.sinkInputObject.GetInputBuffer(out array, out destinationIndex, out num, out parseCount);
				if (num >= value.Length)
				{
					value.CopyTo(0, array, destinationIndex, value.Length);
					this.sinkInputObject.Commit(value.Length);
					return;
				}
			}
			char[] buffer = value.ToCharArray();
			this.WriteBig(buffer, 0, value.Length, parseCount);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000707C6 File Offset: 0x0006E9C6
		public override void WriteLine(string value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000707D5 File Offset: 0x0006E9D5
		internal void SetSink(ConverterUnicodeInput sinkInputObject)
		{
			this.sinkInputObject = sinkInputObject;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000707E0 File Offset: 0x0006E9E0
		internal bool GetInputChunk(out char[] chunkBuffer, out int chunkIndex, out int chunkCount, out bool eof)
		{
			chunkBuffer = this.chunkToReadBuffer;
			chunkIndex = this.chunkToReadIndex;
			chunkCount = this.chunkToReadCount;
			eof = (this.endOfFile && 0 == this.chunkToReadCount);
			return this.chunkToReadCount != 0 || this.endOfFile;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0007082C File Offset: 0x0006EA2C
		internal void ReportRead(int readCount)
		{
			if (readCount != 0)
			{
				this.chunkToReadCount -= readCount;
				this.chunkToReadIndex += readCount;
				if (this.chunkToReadCount == 0)
				{
					this.chunkToReadBuffer = null;
					this.chunkToReadIndex = 0;
				}
				this.madeProgress = true;
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0007086C File Offset: 0x0006EA6C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.destination != null)
				{
					if (!this.inconsistentState)
					{
						this.Flush();
					}
					if (this.destination is Stream)
					{
						((Stream)this.destination).Dispose();
					}
					else
					{
						((TextWriter)this.destination).Dispose();
					}
				}
				if (this.consumer != null && this.consumer is IDisposable)
				{
					((IDisposable)this.consumer).Dispose();
				}
			}
			this.destination = null;
			this.consumer = null;
			this.sinkInputObject = null;
			this.chunkToReadBuffer = null;
			base.Dispose(disposing);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0007090C File Offset: 0x0006EB0C
		private void WriteBig(char[] buffer, int index, int count, int parseCount)
		{
			this.chunkToReadBuffer = buffer;
			this.chunkToReadIndex = index;
			this.chunkToReadCount = count;
			long num = 0L;
			this.inconsistentState = true;
			while (this.chunkToReadCount != 0)
			{
				this.consumer.Run();
				if (this.madeProgress)
				{
					this.madeProgress = false;
					num = 0L;
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
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00070982 File Offset: 0x0006EB82
		void IProgressMonitor.ReportProgress()
		{
			this.madeProgress = true;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0007098C File Offset: 0x0006EB8C
		internal void Reuse(object newSink)
		{
			if (!(this.consumer is IReusable))
			{
				throw new NotSupportedException("this converter is not reusable");
			}
			((IReusable)this.consumer).Initialize(newSink);
			this.destination = newSink;
			this.chunkToReadBuffer = null;
			this.chunkToReadIndex = 0;
			this.chunkToReadCount = 0;
			this.endOfFile = false;
			this.inconsistentState = false;
		}

		// Token: 0x04000F7D RID: 3965
		private ConverterUnicodeInput sinkInputObject;

		// Token: 0x04000F7E RID: 3966
		private IProducerConsumer consumer;

		// Token: 0x04000F7F RID: 3967
		private bool madeProgress;

		// Token: 0x04000F80 RID: 3968
		private int maxLoopsWithoutProgress;

		// Token: 0x04000F81 RID: 3969
		private char[] chunkToReadBuffer;

		// Token: 0x04000F82 RID: 3970
		private int chunkToReadIndex;

		// Token: 0x04000F83 RID: 3971
		private int chunkToReadCount;

		// Token: 0x04000F84 RID: 3972
		private object destination;

		// Token: 0x04000F85 RID: 3973
		private bool endOfFile;

		// Token: 0x04000F86 RID: 3974
		private bool inconsistentState;

		// Token: 0x04000F87 RID: 3975
		private bool boundaryTesting;
	}
}
