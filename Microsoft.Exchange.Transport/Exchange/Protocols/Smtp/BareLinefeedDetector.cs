using System;
using System.IO;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003F6 RID: 1014
	internal class BareLinefeedDetector : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06002E57 RID: 11863 RVA: 0x000BB368 File Offset: 0x000B9568
		public BareLinefeedDetector()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000BB37C File Offset: 0x000B957C
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000BB37F File Offset: 0x000B957F
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000BB382 File Offset: 0x000B9582
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000BB385 File Offset: 0x000B9585
		public override bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x000BB388 File Offset: 0x000B9588
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x000BB38F File Offset: 0x000B958F
		// (set) Token: 0x06002E5E RID: 11870 RVA: 0x000BB396 File Offset: 0x000B9596
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

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000BB39D File Offset: 0x000B959D
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x000BB3A4 File Offset: 0x000B95A4
		public override int ReadTimeout
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

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000BB3AB File Offset: 0x000B95AB
		// (set) Token: 0x06002E62 RID: 11874 RVA: 0x000BB3B2 File Offset: 0x000B95B2
		public override int WriteTimeout
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

		// Token: 0x06002E63 RID: 11875 RVA: 0x000BB3B9 File Offset: 0x000B95B9
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000BB3C0 File Offset: 0x000B95C0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000BB3C7 File Offset: 0x000B95C7
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000BB3CE File Offset: 0x000B95CE
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000BB3D5 File Offset: 0x000B95D5
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000BB3DC File Offset: 0x000B95DC
		public override int Read(byte[] buffer, int offset, int size)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000BB3E3 File Offset: 0x000B95E3
		public override int ReadByte()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000BB3EA File Offset: 0x000B95EA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000BB3F1 File Offset: 0x000B95F1
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000BB3F8 File Offset: 0x000B95F8
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num = offset;
			int num2;
			while ((num2 = MimeInternalHelpers.IndexOf(buffer, 10, num, size - (num - offset))) != -1)
			{
				if (num2 == 0)
				{
					if (!this.lastByteWasCarriageReturn)
					{
						throw new BareLinefeedException(num2 - offset + this.bytesExamined);
					}
				}
				else if (buffer[num2 - 1] != 13)
				{
					throw new BareLinefeedException(num2 - offset + this.bytesExamined);
				}
				num = num2 + 1;
				if (num == buffer.Length)
				{
					break;
				}
			}
			if (buffer.Length == 1)
			{
				this.lastByteWasCarriageReturn = (buffer[0] == 13);
			}
			else if (buffer.Length > 1)
			{
				this.lastByteWasCarriageReturn = (buffer[buffer.Length - 1] == 13);
			}
			this.bytesExamined += buffer.Length;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000BB4D6 File Offset: 0x000B96D6
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000BB4DD File Offset: 0x000B96DD
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BareLinefeedDetector>(this);
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000BB4E5 File Offset: 0x000B96E5
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000BB501 File Offset: 0x000B9701
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040016EA RID: 5866
		private bool lastByteWasCarriageReturn;

		// Token: 0x040016EB RID: 5867
		private int bytesExamined;

		// Token: 0x040016EC RID: 5868
		private DisposeTracker disposeTracker;
	}
}
