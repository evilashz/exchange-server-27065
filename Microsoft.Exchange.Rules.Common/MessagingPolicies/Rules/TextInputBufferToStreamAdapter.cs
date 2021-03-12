using System;
using System.IO;
using Microsoft.Exchange.TextMatching;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200003C RID: 60
	internal class TextInputBufferToStreamAdapter : Stream
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006DCB File Offset: 0x00004FCB
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006DCE File Offset: 0x00004FCE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006DD1 File Offset: 0x00004FD1
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006DDF File Offset: 0x00004FDF
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006DE6 File Offset: 0x00004FE6
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006DED File Offset: 0x00004FED
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006DF4 File Offset: 0x00004FF4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			do
			{
				int nextChar;
				while ((nextChar = this.textInputBuffer.NextChar) == 1)
				{
				}
				if (nextChar <= 1)
				{
					break;
				}
				buffer[offset + num] = (byte)nextChar;
			}
			while (++num != count);
			return num;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006E29 File Offset: 0x00005029
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006E30 File Offset: 0x00005030
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006E37 File Offset: 0x00005037
		public TextInputBufferToStreamAdapter(ITextInputBuffer textInputBuffer)
		{
			if (textInputBuffer == null)
			{
				throw new ArgumentNullException();
			}
			this.textInputBuffer = textInputBuffer;
		}

		// Token: 0x040000B2 RID: 178
		private ITextInputBuffer textInputBuffer;
	}
}
