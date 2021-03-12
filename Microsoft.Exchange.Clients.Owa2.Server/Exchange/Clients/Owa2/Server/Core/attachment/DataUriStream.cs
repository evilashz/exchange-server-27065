using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.attachment
{
	// Token: 0x02000044 RID: 68
	public class DataUriStream : Stream
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x000078FC File Offset: 0x00005AFC
		public DataUriStream(Stream dataStream, string contentType)
		{
			if (dataStream == null)
			{
				throw new ArgumentNullException("dataStream");
			}
			if (string.IsNullOrEmpty(contentType))
			{
				throw new ArgumentNullException("contentType");
			}
			this.templateStream = this.GetTemplateStream(contentType);
			this.dataStream = dataStream;
			this.streams = new Queue<Stream>();
			this.streams.Enqueue(this.templateStream);
			this.streams.Enqueue(this.dataStream);
			this.SetCurrentStream();
			this.templateStream.Seek(0L, SeekOrigin.Begin);
			this.dataStream.Seek(0L, SeekOrigin.Begin);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007995 File Offset: 0x00005B95
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00007998 File Offset: 0x00005B98
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000799B File Offset: 0x00005B9B
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000799E File Offset: 0x00005B9E
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this.templateStream.Length + this.dataStream.Length;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000079BD File Offset: 0x00005BBD
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000079C4 File Offset: 0x00005BC4
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

		// Token: 0x060001DD RID: 477 RVA: 0x000079CB File Offset: 0x00005BCB
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000079D4 File Offset: 0x00005BD4
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			if (this.currentStream == null)
			{
				return 0;
			}
			int num = this.currentStream.Read(buffer, offset, count);
			if (num <= 0)
			{
				this.SetCurrentStream();
				return this.Read(buffer, offset, count);
			}
			return num;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007A15 File Offset: 0x00005C15
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007A1C File Offset: 0x00005C1C
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007A23 File Offset: 0x00005C23
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007A2A File Offset: 0x00005C2A
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.dataStream != null)
				{
					this.dataStream.Dispose();
					this.dataStream = null;
				}
				if (this.templateStream != null)
				{
					this.templateStream.Dispose();
					this.templateStream = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007A6A File Offset: 0x00005C6A
		private Stream GetTemplateStream(string contentType)
		{
			return new MemoryStream(new UTF8Encoding().GetBytes(string.Format("data:{0};base64,", contentType)));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007A86 File Offset: 0x00005C86
		private void SetCurrentStream()
		{
			if (this.streams.Count > 0)
			{
				this.currentStream = this.streams.Dequeue();
				return;
			}
			this.currentStream = null;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007AAF File Offset: 0x00005CAF
		private void ThrowIfDisposed()
		{
			if (this.dataStream == null || this.templateStream == null)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x040000BE RID: 190
		public const string DataUriTemplate = "data:{0};base64,";

		// Token: 0x040000BF RID: 191
		private Stream dataStream;

		// Token: 0x040000C0 RID: 192
		private Stream templateStream;

		// Token: 0x040000C1 RID: 193
		private Queue<Stream> streams;

		// Token: 0x040000C2 RID: 194
		private Stream currentStream;
	}
}
