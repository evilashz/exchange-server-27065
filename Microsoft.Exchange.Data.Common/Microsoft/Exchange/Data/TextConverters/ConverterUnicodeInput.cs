using System;
using System.IO;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200017D RID: 381
	internal class ConverterUnicodeInput : ConverterInput, IReusable
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x00078280 File Offset: 0x00076480
		public ConverterUnicodeInput(object source, bool push, int maxParseToken, bool testBoundaryConditions, IProgressMonitor progressMonitor) : base(progressMonitor)
		{
			if (push)
			{
				this.pushSource = (source as ConverterWriter);
			}
			else
			{
				this.pullSource = (source as TextReader);
			}
			this.maxTokenSize = maxParseToken;
			this.parseBuffer = new char[testBoundaryConditions ? 123 : 4096];
			if (this.pushSource != null)
			{
				this.pushSource.SetSink(this);
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000782E8 File Offset: 0x000764E8
		private void Reinitialize()
		{
			this.parseStart = (this.parseEnd = 0);
			this.pushChunkStart = 0;
			this.pushChunkCount = 0;
			this.pushChunkUsed = 0;
			this.pushChunkBuffer = null;
			this.endOfFile = false;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00078328 File Offset: 0x00076528
		void IReusable.Initialize(object newSourceOrDestination)
		{
			if (this.pullSource != null && newSourceOrDestination != null)
			{
				TextReader textReader = newSourceOrDestination as TextReader;
				if (textReader == null)
				{
					throw new InvalidOperationException("cannot reinitialize this converter - new input should be a TextReader object");
				}
				this.pullSource = textReader;
			}
			this.Reinitialize();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00078364 File Offset: 0x00076564
		public override bool ReadMore(ref char[] buffer, ref int start, ref int current, ref int end)
		{
			int num = this.parseEnd - end;
			if (this.parseBuffer.Length - this.parseEnd <= 1 && !this.EnsureFreeSpace() && num == 0)
			{
				return true;
			}
			while (!this.endOfFile && this.parseBuffer.Length - this.parseEnd > 1)
			{
				if (this.pushSource != null)
				{
					if (this.pushChunkCount == 0 && !this.pushSource.GetInputChunk(out this.pushChunkBuffer, out this.pushChunkStart, out this.pushChunkCount, out this.endOfFile))
					{
						break;
					}
					if (this.pushChunkCount - this.pushChunkUsed != 0)
					{
						int num2 = Math.Min(this.pushChunkCount - this.pushChunkUsed, this.parseBuffer.Length - this.parseEnd - 1);
						Buffer.BlockCopy(this.pushChunkBuffer, (this.pushChunkStart + this.pushChunkUsed) * 2, this.parseBuffer, this.parseEnd * 2, num2 * 2);
						this.pushChunkUsed += num2;
						this.parseEnd += num2;
						this.parseBuffer[this.parseEnd] = '\0';
						num += num2;
						if (this.pushChunkCount - this.pushChunkUsed == 0)
						{
							this.pushSource.ReportRead(this.pushChunkCount);
							this.pushChunkStart = 0;
							this.pushChunkCount = 0;
							this.pushChunkUsed = 0;
							this.pushChunkBuffer = null;
						}
					}
				}
				else
				{
					int num3 = this.pullSource.Read(this.parseBuffer, this.parseEnd, this.parseBuffer.Length - this.parseEnd - 1);
					if (num3 == 0)
					{
						this.endOfFile = true;
					}
					else
					{
						this.parseEnd += num3;
						this.parseBuffer[this.parseEnd] = '\0';
						num += num3;
					}
					if (this.progressMonitor != null)
					{
						this.progressMonitor.ReportProgress();
					}
				}
			}
			buffer = this.parseBuffer;
			if (start != this.parseStart)
			{
				current = this.parseStart + (current - start);
				start = this.parseStart;
			}
			end = this.parseEnd;
			return num != 0 || this.endOfFile;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00078573 File Offset: 0x00076773
		public override void ReportProcessed(int processedSize)
		{
			this.parseStart += processedSize;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00078584 File Offset: 0x00076784
		public override int RemoveGap(int gapBegin, int gapEnd)
		{
			if (gapEnd == this.parseEnd)
			{
				this.parseEnd = gapBegin;
				this.parseBuffer[gapBegin] = '\0';
				return gapBegin;
			}
			Buffer.BlockCopy(this.parseBuffer, gapEnd, this.parseBuffer, gapBegin, this.parseEnd - gapEnd);
			this.parseEnd = gapBegin + (this.parseEnd - gapEnd);
			this.parseBuffer[this.parseEnd] = '\0';
			return this.parseEnd;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000785EB File Offset: 0x000767EB
		public void GetInputBuffer(out char[] inputBuffer, out int inputOffset, out int inputCount, out int parseCount)
		{
			inputBuffer = this.parseBuffer;
			inputOffset = this.parseEnd;
			inputCount = this.parseBuffer.Length - this.parseEnd - 1;
			parseCount = this.parseEnd - this.parseStart;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00078620 File Offset: 0x00076820
		public void Commit(int inputCount)
		{
			this.parseEnd += inputCount;
			this.parseBuffer[this.parseEnd] = '\0';
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0007863E File Offset: 0x0007683E
		protected override void Dispose(bool disposing)
		{
			this.pullSource = null;
			this.pushSource = null;
			this.parseBuffer = null;
			this.pushChunkBuffer = null;
			base.Dispose(disposing);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00078664 File Offset: 0x00076864
		private bool EnsureFreeSpace()
		{
			if (this.parseBuffer.Length - (this.parseEnd - this.parseStart) <= 1 || (this.parseStart < 1 && (long)this.parseBuffer.Length < (long)this.maxTokenSize + 1L))
			{
				if ((long)this.parseBuffer.Length >= (long)this.maxTokenSize + 1L)
				{
					return false;
				}
				long num = (long)(this.parseBuffer.Length * 2);
				if (num > (long)this.maxTokenSize + 1L)
				{
					num = (long)this.maxTokenSize + 1L;
				}
				if (num > 2147483647L)
				{
					num = 2147483647L;
				}
				char[] dst = new char[(int)num];
				Buffer.BlockCopy(this.parseBuffer, this.parseStart * 2, dst, 0, (this.parseEnd - this.parseStart + 1) * 2);
				this.parseBuffer = dst;
				this.parseEnd -= this.parseStart;
				this.parseStart = 0;
			}
			else
			{
				Buffer.BlockCopy(this.parseBuffer, this.parseStart * 2, this.parseBuffer, 0, (this.parseEnd - this.parseStart + 1) * 2);
				this.parseEnd -= this.parseStart;
				this.parseStart = 0;
			}
			return true;
		}

		// Token: 0x04001102 RID: 4354
		private TextReader pullSource;

		// Token: 0x04001103 RID: 4355
		private ConverterWriter pushSource;

		// Token: 0x04001104 RID: 4356
		private char[] parseBuffer;

		// Token: 0x04001105 RID: 4357
		private int parseStart;

		// Token: 0x04001106 RID: 4358
		private int parseEnd;

		// Token: 0x04001107 RID: 4359
		private char[] pushChunkBuffer;

		// Token: 0x04001108 RID: 4360
		private int pushChunkStart;

		// Token: 0x04001109 RID: 4361
		private int pushChunkCount;

		// Token: 0x0400110A RID: 4362
		private int pushChunkUsed;
	}
}
