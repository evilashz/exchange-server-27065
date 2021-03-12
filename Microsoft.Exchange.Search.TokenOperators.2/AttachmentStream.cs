using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000004 RID: 4
	internal class AttachmentStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002D04 File Offset: 0x00000F04
		public AttachmentStream(IAttachment attachment)
		{
			Util.ThrowOnNullArgument(attachment, "attachment");
			this.attachment = attachment;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002D38 File Offset: 0x00000F38
		public override bool CanRead
		{
			get
			{
				bool canRead;
				lock (this.lockObject)
				{
					canRead = this.UnderlyingStream.CanRead;
				}
				return canRead;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002D80 File Offset: 0x00000F80
		public override bool CanSeek
		{
			get
			{
				bool canSeek;
				lock (this.lockObject)
				{
					canSeek = this.UnderlyingStream.CanSeek;
				}
				return canSeek;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public override bool CanWrite
		{
			get
			{
				bool canWrite;
				lock (this.lockObject)
				{
					canWrite = this.UnderlyingStream.CanWrite;
				}
				return canWrite;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002E10 File Offset: 0x00001010
		public override long Length
		{
			get
			{
				long length;
				lock (this.lockObject)
				{
					length = this.UnderlyingStream.Length;
				}
				return length;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002E58 File Offset: 0x00001058
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002EA0 File Offset: 0x000010A0
		public override long Position
		{
			get
			{
				long position;
				lock (this.lockObject)
				{
					position = this.UnderlyingStream.Position;
				}
				return position;
			}
			set
			{
				lock (this.lockObject)
				{
					this.UnderlyingStream.Position = value;
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002EE8 File Offset: 0x000010E8
		private Stream UnderlyingStream
		{
			get
			{
				if (this.underlyingStream == null)
				{
					this.underlyingStream = this.attachment.GetContentStream();
				}
				return this.underlyingStream;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F09 File Offset: 0x00001109
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AttachmentStream>(this);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002F11 File Offset: 0x00001111
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002F28 File Offset: 0x00001128
		public override void Flush()
		{
			lock (this.lockObject)
			{
				this.UnderlyingStream.Flush();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F70 File Offset: 0x00001170
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection) && this.Position == 0L)
			{
				string text = null;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3322293565U, ref text);
				if (text != null)
				{
					byte[] array = new byte[36];
					int num = 0;
					lock (this.lockObject)
					{
						long position = this.UnderlyingStream.Position;
						num = this.UnderlyingStream.Read(array, 0, 36);
						this.UnderlyingStream.Position = position;
					}
					if (num == 36 && text.CompareTo(Encoding.ASCII.GetString(array, 0, num)) == 0)
					{
						Environment.Exit(0);
					}
				}
			}
			int result;
			lock (this.lockObject)
			{
				result = this.UnderlyingStream.Read(buffer, offset, count);
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003074 File Offset: 0x00001274
		public override long Seek(long offset, SeekOrigin origin)
		{
			long result;
			lock (this.lockObject)
			{
				result = this.UnderlyingStream.Seek(offset, origin);
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000030C0 File Offset: 0x000012C0
		public override void SetLength(long value)
		{
			lock (this.lockObject)
			{
				this.UnderlyingStream.SetLength(value);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003108 File Offset: 0x00001308
		public override void Write(byte[] buffer, int offset, int count)
		{
			lock (this.lockObject)
			{
				this.UnderlyingStream.Write(buffer, offset, count);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003150 File Offset: 0x00001350
		protected override void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				lock (this.lockObject)
				{
					if (this.underlyingStream != null)
					{
						this.underlyingStream.Close();
						this.underlyingStream = null;
					}
				}
			}
		}

		// Token: 0x04000014 RID: 20
		private const byte GuidLength = 36;

		// Token: 0x04000015 RID: 21
		private DisposeTracker disposeTracker;

		// Token: 0x04000016 RID: 22
		private Stream underlyingStream;

		// Token: 0x04000017 RID: 23
		private IAttachment attachment;

		// Token: 0x04000018 RID: 24
		private object lockObject = new object();
	}
}
