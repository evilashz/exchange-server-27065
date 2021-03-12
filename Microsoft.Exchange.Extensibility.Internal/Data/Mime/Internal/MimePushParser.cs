using System;
using System.IO;

namespace Microsoft.Exchange.Data.Mime.Internal
{
	// Token: 0x02000019 RID: 25
	internal class MimePushParser : Stream, IMimeHandlerInternal
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00004547 File Offset: 0x00002747
		public MimePushParser(IMimeHandler handler) : this(handler, true, DecodingOptions.Default, MimeLimits.Default, true)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000455C File Offset: 0x0000275C
		public MimePushParser(IMimeHandler handler, bool inferMime, DecodingOptions decodingOptions, MimeLimits mimeLimits, bool parseInline)
		{
			this.handler = handler;
			this.reader = new MimeReader(this, inferMime, decodingOptions, mimeLimits, parseInline);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000457D File Offset: 0x0000277D
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004580 File Offset: 0x00002780
		public override bool CanWrite
		{
			get
			{
				return this.reader != null;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000458E File Offset: 0x0000278E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004591 File Offset: 0x00002791
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004598 File Offset: 0x00002798
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000459F File Offset: 0x0000279F
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

		// Token: 0x0600007D RID: 125 RVA: 0x000045A6 File Offset: 0x000027A6
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000045AD File Offset: 0x000027AD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000045B4 File Offset: 0x000027B4
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000045BB File Offset: 0x000027BB
		public override void Write(byte[] buffer, int offset, int length)
		{
			if (this.reader == null)
			{
				throw new ObjectDisposedException("MimePushParser");
			}
			this.reader.Write(buffer, offset, length);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000045DE File Offset: 0x000027DE
		public override void Flush()
		{
			if (this.reader == null)
			{
				throw new ObjectDisposedException("MimePushParser");
			}
			this.reader.Flush();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000045FE File Offset: 0x000027FE
		protected override void Dispose(bool disposing)
		{
			if (this.reader != null)
			{
				this.reader.Close();
				this.reader = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004624 File Offset: 0x00002824
		void IMimeHandlerInternal.PartStart(bool isInline, string inlineFileName, out PartParseOptionInternal partParseOption, out Stream outerContentWriteStream)
		{
			PartParseOption partParseOption2;
			this.handler.PartStart(isInline, inlineFileName, out partParseOption2, out outerContentWriteStream);
			partParseOption = (PartParseOptionInternal)partParseOption2;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004648 File Offset: 0x00002848
		void IMimeHandlerInternal.HeaderStart(HeaderId headerId, string name, out HeaderParseOptionInternal headerParseOption)
		{
			HeaderParseOption headerParseOption2;
			this.handler.HeaderStart(headerId, name, out headerParseOption2);
			headerParseOption = (HeaderParseOptionInternal)headerParseOption2;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004667 File Offset: 0x00002867
		void IMimeHandlerInternal.Header(Header header)
		{
			this.handler.Header(header);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004678 File Offset: 0x00002878
		void IMimeHandlerInternal.EndOfHeaders(string mediaType, ContentTransferEncoding cte, out PartContentParseOptionInternal partContentParseOption)
		{
			PartContentParseOption partContentParseOption2;
			this.handler.EndOfHeaders(mediaType, cte, out partContentParseOption2);
			partContentParseOption = (PartContentParseOptionInternal)partContentParseOption2;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004697 File Offset: 0x00002897
		void IMimeHandlerInternal.PartContent(byte[] buffer, int offset, int length)
		{
			this.handler.PartContent(buffer, offset, length);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000046A7 File Offset: 0x000028A7
		void IMimeHandlerInternal.PartEnd()
		{
			this.handler.PartEnd();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000046B4 File Offset: 0x000028B4
		void IMimeHandlerInternal.EndOfFile()
		{
			this.handler.EndOfFile();
		}

		// Token: 0x04000100 RID: 256
		private MimeReader reader;

		// Token: 0x04000101 RID: 257
		private IMimeHandler handler;
	}
}
