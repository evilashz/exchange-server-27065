using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000178 RID: 376
	internal class ConverterBufferInput : ConverterInput, ITextSink
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x0007606E File Offset: 0x0007426E
		public ConverterBufferInput(IProgressMonitor progressMonitor) : this(32768, progressMonitor)
		{
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0007607C File Offset: 0x0007427C
		public ConverterBufferInput(int maxLength, IProgressMonitor progressMonitor) : base(progressMonitor)
		{
			this.maxLength = maxLength;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0007608C File Offset: 0x0007428C
		public ConverterBufferInput(string fragment, IProgressMonitor progressMonitor) : this(32768, fragment, progressMonitor)
		{
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0007609C File Offset: 0x0007429C
		public ConverterBufferInput(int maxLength, string fragment, IProgressMonitor progressMonitor) : base(progressMonitor)
		{
			this.maxLength = maxLength;
			this.originalFragment = fragment;
			this.parseBuffer = new char[fragment.Length + 1];
			fragment.CopyTo(0, this.parseBuffer, 0, fragment.Length);
			this.parseBuffer[fragment.Length] = '\0';
			this.maxTokenSize = fragment.Length;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x000760FF File Offset: 0x000742FF
		public bool IsEnough
		{
			get
			{
				return this.maxTokenSize >= this.maxLength;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x00076112 File Offset: 0x00074312
		public bool IsEmpty
		{
			get
			{
				return this.maxTokenSize == 0;
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00076120 File Offset: 0x00074320
		public void Write(string str)
		{
			int num = this.PrepareToBuffer(str.Length);
			if (num > 0)
			{
				str.CopyTo(0, this.parseBuffer, this.maxTokenSize, num);
				this.maxTokenSize += num;
				this.parseBuffer[this.maxTokenSize] = '\0';
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00076170 File Offset: 0x00074370
		public void Write(char[] buffer, int offset, int count)
		{
			count = this.PrepareToBuffer(count);
			if (count > 0)
			{
				Buffer.BlockCopy(buffer, offset * 2, this.parseBuffer, this.maxTokenSize * 2, count * 2);
				this.maxTokenSize += count;
				this.parseBuffer[this.maxTokenSize] = '\0';
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000761C0 File Offset: 0x000743C0
		public void Write(int ucs32Char)
		{
			if (ucs32Char > 65535)
			{
				int num = this.PrepareToBuffer(2);
				if (num > 0)
				{
					this.parseBuffer[this.maxTokenSize] = ParseSupport.HighSurrogateCharFromUcs4(ucs32Char);
					this.parseBuffer[this.maxTokenSize + 1] = ParseSupport.LowSurrogateCharFromUcs4(ucs32Char);
					this.maxTokenSize += num;
					this.parseBuffer[this.maxTokenSize] = '\0';
					return;
				}
			}
			else
			{
				int num = this.PrepareToBuffer(1);
				if (num > 0)
				{
					this.parseBuffer[this.maxTokenSize++] = (char)ucs32Char;
					this.parseBuffer[this.maxTokenSize] = '\0';
				}
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0007625A File Offset: 0x0007445A
		public void Reset()
		{
			this.maxTokenSize = 0;
			this.endOfFile = false;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0007626C File Offset: 0x0007446C
		public void Initialize(string fragment)
		{
			if (this.originalFragment != fragment)
			{
				this.originalFragment = fragment;
				this.parseBuffer = new char[fragment.Length + 1];
				fragment.CopyTo(0, this.parseBuffer, 0, fragment.Length);
				this.parseBuffer[fragment.Length] = '\0';
				this.maxTokenSize = fragment.Length;
			}
			this.endOfFile = false;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000762D6 File Offset: 0x000744D6
		public override bool ReadMore(ref char[] buffer, ref int start, ref int current, ref int end)
		{
			if (buffer == null)
			{
				buffer = this.parseBuffer;
				start = 0;
				end = this.maxTokenSize;
				current = 0;
				if (end != 0)
				{
					return true;
				}
			}
			this.endOfFile = true;
			return true;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00076302 File Offset: 0x00074502
		public override void ReportProcessed(int processedSize)
		{
			this.progressMonitor.ReportProgress();
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0007630F File Offset: 0x0007450F
		public override int RemoveGap(int gapBegin, int gapEnd)
		{
			this.parseBuffer[gapBegin] = '\0';
			return gapBegin;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0007631B File Offset: 0x0007451B
		protected override void Dispose(bool disposing)
		{
			this.parseBuffer = null;
			base.Dispose(disposing);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0007632C File Offset: 0x0007452C
		private int PrepareToBuffer(int count)
		{
			if (this.maxTokenSize + count > this.maxLength)
			{
				count = this.maxLength - this.maxTokenSize;
			}
			if (count > 0)
			{
				if (this.parseBuffer == null)
				{
					this.parseBuffer = new char[count + 1];
				}
				else if (this.parseBuffer.Length <= this.maxTokenSize + count)
				{
					char[] src = this.parseBuffer;
					int num = (this.maxTokenSize + count) * 2;
					if (num > this.maxLength)
					{
						num = this.maxLength;
					}
					this.parseBuffer = new char[num + 1];
					if (this.maxTokenSize > 0)
					{
						Buffer.BlockCopy(src, 0, this.parseBuffer, 0, this.maxTokenSize * 2);
					}
				}
			}
			return count;
		}

		// Token: 0x040010C9 RID: 4297
		private const int DefaultMaxLength = 32768;

		// Token: 0x040010CA RID: 4298
		private int maxLength;

		// Token: 0x040010CB RID: 4299
		private string originalFragment;

		// Token: 0x040010CC RID: 4300
		private char[] parseBuffer;
	}
}
