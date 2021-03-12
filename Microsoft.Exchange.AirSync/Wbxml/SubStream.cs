using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync.Wbxml
{
	// Token: 0x020002A2 RID: 674
	internal class SubStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x0009153C File Offset: 0x0008F73C
		public SubStream(Stream stream, long startPosition, long length)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.stream = stream;
			this.startPosition = startPosition;
			this.length = length;
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.RegisterDisposableData(this);
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00091577 File Offset: 0x0008F777
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00091584 File Offset: 0x0008F784
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00091591 File Offset: 0x0008F791
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00091594 File Offset: 0x0008F794
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0009159C File Offset: 0x0008F79C
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x000915B0 File Offset: 0x0008F7B0
		public override long Position
		{
			get
			{
				return this.stream.Position - this.startPosition;
			}
			set
			{
				if (value > this.length)
				{
					this.stream.Position = this.startPosition + this.length;
					return;
				}
				this.stream.Position = this.startPosition + value;
			}
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000915E7 File Offset: 0x0008F7E7
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000915F4 File Offset: 0x0008F7F4
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			if (origin == SeekOrigin.Begin)
			{
				num = this.startPosition + offset;
			}
			else if (origin == SeekOrigin.Current)
			{
				num = this.stream.Position + offset;
			}
			else
			{
				if (origin != SeekOrigin.End)
				{
					throw new ArgumentException();
				}
				num = this.length + offset;
			}
			if (num < this.startPosition)
			{
				num = this.startPosition;
			}
			if (num > this.startPosition + this.length)
			{
				num = this.startPosition + this.length;
			}
			return this.stream.Seek(num, SeekOrigin.Begin);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00091676 File Offset: 0x0008F876
		public override void SetLength(long value)
		{
			throw new NotImplementedException("The SubStream class doesn't allow changing the length");
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00091684 File Offset: 0x0008F884
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.stream.Position + (long)count > this.startPosition + this.length)
			{
				count = (int)(this.startPosition + this.length - this.stream.Position);
			}
			if (count > 0)
			{
				return this.stream.Read(buffer, offset, count);
			}
			return 0;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000916DE File Offset: 0x0008F8DE
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException("The SubStream class doesn't support writing");
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000916EA File Offset: 0x0008F8EA
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SubStream>(this);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x000916F2 File Offset: 0x0008F8F2
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0009170E File Offset: 0x0008F90E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001175 RID: 4469
		private Stream stream;

		// Token: 0x04001176 RID: 4470
		private long startPosition;

		// Token: 0x04001177 RID: 4471
		private long length;

		// Token: 0x04001178 RID: 4472
		private DisposeTracker disposeTracker;
	}
}
