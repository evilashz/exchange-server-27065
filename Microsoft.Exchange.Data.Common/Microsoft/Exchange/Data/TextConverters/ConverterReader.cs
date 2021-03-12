using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200014F RID: 335
	public class ConverterReader : TextReader, IProgressMonitor
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x0006F6B4 File Offset: 0x0006D8B4
		public ConverterReader(Stream sourceStream, TextConverter converter)
		{
			if (sourceStream == null)
			{
				throw new ArgumentNullException("sourceStream");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			if (!sourceStream.CanRead)
			{
				throw new ArgumentException(TextConvertersStrings.CannotReadFromSource, "sourceStream");
			}
			this.producer = converter.CreatePullChain(sourceStream, this);
			this.source = sourceStream;
			this.maxLoopsWithoutProgress = 100000 + converter.InputStreamBufferSize + converter.OutputStreamBufferSize;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0006F72C File Offset: 0x0006D92C
		public ConverterReader(TextReader sourceReader, TextConverter converter)
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
			this.source = sourceReader;
			this.maxLoopsWithoutProgress = 100000 + converter.InputStreamBufferSize + converter.OutputStreamBufferSize;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0006F78C File Offset: 0x0006D98C
		public override int Peek()
		{
			if (this.source == null)
			{
				throw new ObjectDisposedException("ConverterReader");
			}
			if (this.inconsistentState)
			{
				throw new InvalidOperationException(TextConvertersStrings.ConverterReaderInInconsistentStare);
			}
			long num = 0L;
			this.inconsistentState = true;
			while (!this.endOfFile)
			{
				char[] array;
				int num2;
				int num3;
				if (this.sourceOutputObject.GetOutputChunk(out array, out num2, out num3))
				{
					this.inconsistentState = false;
					return (int)array[num2];
				}
				this.producer.Run();
				if (this.madeProgress)
				{
					this.madeProgress = false;
					num = 0L;
				}
				else
				{
					long num4 = (long)this.maxLoopsWithoutProgress;
					long num5 = num;
					num = num5 + 1L;
					if (num4 == num5)
					{
						throw new TextConvertersException(TextConvertersStrings.TooManyIterationsToProduceOutput);
					}
				}
			}
			this.inconsistentState = false;
			return -1;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0006F834 File Offset: 0x0006DA34
		public override int Read()
		{
			if (this.source == null)
			{
				throw new ObjectDisposedException("ConverterReader");
			}
			if (this.inconsistentState)
			{
				throw new InvalidOperationException(TextConvertersStrings.ConverterReaderInInconsistentStare);
			}
			long num = 0L;
			this.inconsistentState = true;
			while (!this.endOfFile)
			{
				char[] array;
				int num2;
				int num3;
				if (this.sourceOutputObject.GetOutputChunk(out array, out num2, out num3))
				{
					this.sourceOutputObject.ReportOutput(1);
					this.inconsistentState = false;
					return (int)array[num2];
				}
				this.producer.Run();
				if (this.madeProgress)
				{
					this.madeProgress = false;
					num = 0L;
				}
				else
				{
					long num4 = (long)this.maxLoopsWithoutProgress;
					long num5 = num;
					num = num5 + 1L;
					if (num4 == num5)
					{
						throw new TextConvertersException(TextConvertersStrings.TooManyIterationsToProduceOutput);
					}
				}
			}
			this.inconsistentState = false;
			return -1;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0006F8E8 File Offset: 0x0006DAE8
		public override int Read(char[] buffer, int index, int count)
		{
			if (this.source == null)
			{
				throw new ObjectDisposedException("ConverterReader");
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
				throw new InvalidOperationException(TextConvertersStrings.ConverterReaderInInconsistentStare);
			}
			int num = count;
			char[] src;
			int num2;
			int val;
			while (count != 0 && this.sourceOutputObject.GetOutputChunk(out src, out num2, out val))
			{
				int num3 = Math.Min(val, count);
				Buffer.BlockCopy(src, num2 * 2, buffer, index * 2, num3 * 2);
				index += num3;
				count -= num3;
				this.sourceOutputObject.ReportOutput(num3);
			}
			if (count != 0)
			{
				long num4 = 0L;
				this.writeBuffer = buffer;
				this.writeIndex = index;
				this.writeCount = count;
				this.inconsistentState = true;
				while (this.writeCount != 0 && !this.endOfFile)
				{
					this.producer.Run();
					if (this.madeProgress)
					{
						this.madeProgress = false;
						num4 = 0L;
					}
					else
					{
						long num5 = (long)this.maxLoopsWithoutProgress;
						long num6 = num4;
						num4 = num6 + 1L;
						if (num5 == num6)
						{
							throw new TextConvertersException(TextConvertersStrings.TooManyIterationsToProduceOutput);
						}
					}
				}
				count = this.writeCount;
				this.writeBuffer = null;
				this.writeIndex = 0;
				this.writeCount = 0;
				this.inconsistentState = false;
			}
			return num - count;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0006FA5D File Offset: 0x0006DC5D
		internal void SetSource(ConverterUnicodeOutput sourceOutputObject)
		{
			this.sourceOutputObject = sourceOutputObject;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0006FA66 File Offset: 0x0006DC66
		internal void GetOutputBuffer(out char[] outputBuffer, out int outputIndex, out int outputCount)
		{
			outputBuffer = this.writeBuffer;
			outputIndex = this.writeIndex;
			outputCount = this.writeCount;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0006FA80 File Offset: 0x0006DC80
		internal void ReportOutput(int outputCount)
		{
			if (outputCount != 0)
			{
				this.writeCount -= outputCount;
				this.writeIndex += outputCount;
				this.madeProgress = true;
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0006FAA8 File Offset: 0x0006DCA8
		internal void ReportEndOfFile()
		{
			this.endOfFile = true;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0006FAB4 File Offset: 0x0006DCB4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.source != null)
				{
					if (this.source is Stream)
					{
						((Stream)this.source).Dispose();
					}
					else
					{
						((TextReader)this.source).Dispose();
					}
				}
				if (this.producer != null && this.producer is IDisposable)
				{
					((IDisposable)this.producer).Dispose();
				}
			}
			this.source = null;
			this.producer = null;
			this.sourceOutputObject = null;
			this.writeBuffer = null;
			base.Dispose(disposing);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0006FB43 File Offset: 0x0006DD43
		void IProgressMonitor.ReportProgress()
		{
			this.madeProgress = true;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0006FB4C File Offset: 0x0006DD4C
		internal void Reuse(object newSource)
		{
			if (!(this.producer is IReusable))
			{
				throw new NotSupportedException("this converter is not reusable");
			}
			((IReusable)this.producer).Initialize(newSource);
			this.source = newSource;
			this.writeBuffer = null;
			this.writeIndex = 0;
			this.writeCount = 0;
			this.endOfFile = false;
			this.inconsistentState = false;
		}

		// Token: 0x04000F62 RID: 3938
		private ConverterUnicodeOutput sourceOutputObject;

		// Token: 0x04000F63 RID: 3939
		private IProducerConsumer producer;

		// Token: 0x04000F64 RID: 3940
		private bool madeProgress;

		// Token: 0x04000F65 RID: 3941
		private int maxLoopsWithoutProgress;

		// Token: 0x04000F66 RID: 3942
		private char[] writeBuffer;

		// Token: 0x04000F67 RID: 3943
		private int writeIndex;

		// Token: 0x04000F68 RID: 3944
		private int writeCount;

		// Token: 0x04000F69 RID: 3945
		private object source;

		// Token: 0x04000F6A RID: 3946
		private bool endOfFile;

		// Token: 0x04000F6B RID: 3947
		private bool inconsistentState;
	}
}
