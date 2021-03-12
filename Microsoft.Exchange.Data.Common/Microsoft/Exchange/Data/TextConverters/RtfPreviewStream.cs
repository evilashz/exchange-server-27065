using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000162 RID: 354
	internal class RtfPreviewStream : Stream
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x00073E60 File Offset: 0x00072060
		public RtfPreviewStream(Stream inputRtfStream, int inputBufferSize)
		{
			this.InputRtfStream = inputRtfStream;
			this.Parser = new RtfParserBase(inputBufferSize, false, null);
			int offset;
			int num = this.Parser.GetBufferSpace(false, out offset);
			num = this.InputRtfStream.Read(this.Parser.ParseBuffer, offset, num);
			this.Parser.ReportMoreDataAvailable(num, num == 0);
			int num2 = 0;
			while (this.Parser.ParseRun())
			{
				RtfRunKind runKind = this.Parser.RunKind;
				if (runKind != RtfRunKind.Ignore)
				{
					if (runKind != RtfRunKind.Begin)
					{
						if (runKind != RtfRunKind.Keyword)
						{
							return;
						}
						if (num2++ > 10)
						{
							return;
						}
						if (this.Parser.KeywordId == 292)
						{
							if (this.Parser.KeywordValue >= 1)
							{
								this.rtfEncapsulation = RtfEncapsulation.Html;
								return;
							}
							break;
						}
						else if (this.Parser.KeywordId == 329)
						{
							this.rtfEncapsulation = RtfEncapsulation.Text;
							return;
						}
					}
					else if (num2++ != 0)
					{
						return;
					}
				}
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00073F49 File Offset: 0x00072149
		public RtfEncapsulation Encapsulation
		{
			get
			{
				return this.rtfEncapsulation;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00073F51 File Offset: 0x00072151
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00073F54 File Offset: 0x00072154
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00073F57 File Offset: 0x00072157
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00073F5A File Offset: 0x0007215A
		public override long Length
		{
			get
			{
				throw new NotSupportedException(TextConvertersStrings.SeekUnsupported);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00073F66 File Offset: 0x00072166
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x00073F72 File Offset: 0x00072172
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

		// Token: 0x06000F04 RID: 3844 RVA: 0x00073F7E File Offset: 0x0007217E
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(TextConvertersStrings.SeekUnsupported);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00073F8A File Offset: 0x0007218A
		public override void SetLength(long value)
		{
			throw new NotSupportedException(TextConvertersStrings.SeekUnsupported);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00073F96 File Offset: 0x00072196
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(TextConvertersStrings.WriteUnsupported);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00073FA2 File Offset: 0x000721A2
		public override void Flush()
		{
			throw new NotSupportedException(TextConvertersStrings.WriteUnsupported);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00073FB0 File Offset: 0x000721B0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.InputRtfStream == null)
			{
				throw new ObjectDisposedException("RtfPreviewStream");
			}
			int num = 0;
			if (this.InternalPosition != 2147483647)
			{
				if (this.Parser != null && this.InternalPosition < this.Parser.ParseEnd)
				{
					int num2 = Math.Min(this.Parser.ParseEnd - this.InternalPosition, count);
					Buffer.BlockCopy(this.Parser.ParseBuffer, 0, buffer, offset, num2);
					this.InternalPosition += num2;
					count -= num2;
					offset += num2;
					num += num2;
					if (this.InternalPosition == this.Parser.ParseEnd)
					{
						this.Parser = null;
					}
				}
				if (count != 0)
				{
					int num2 = this.InputRtfStream.Read(buffer, offset, count);
					num += num2;
				}
			}
			return num;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00074077 File Offset: 0x00072277
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.InputRtfStream != null)
			{
				this.InputRtfStream.Dispose();
			}
			this.InputRtfStream = null;
			this.Parser = null;
			base.Dispose(disposing);
		}

		// Token: 0x04001055 RID: 4181
		internal Stream InputRtfStream;

		// Token: 0x04001056 RID: 4182
		internal RtfParserBase Parser;

		// Token: 0x04001057 RID: 4183
		internal int InternalPosition;

		// Token: 0x04001058 RID: 4184
		private RtfEncapsulation rtfEncapsulation;
	}
}
