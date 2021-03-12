using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000022 RID: 34
	public class StreamWrapper : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00006EC4 File Offset: 0x000050C4
		public StreamWrapper()
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.disposeTracker = this.GetDisposeTracker();
				this.internalStream = Streams.CreateTemporaryStorageStream(new Func<int, byte[]>(StreamWrapper.AcquireBuffer), new Action<byte[]>(StreamWrapper.ReleaseBuffer));
				disposeGuard.Success();
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006F34 File Offset: 0x00005134
		public override bool CanRead
		{
			get
			{
				return this.internalStream.CanRead;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006F41 File Offset: 0x00005141
		public override bool CanWrite
		{
			get
			{
				return this.internalStream.CanWrite;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006F4E File Offset: 0x0000514E
		public override bool CanSeek
		{
			get
			{
				return this.internalStream.CanSeek;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006F5B File Offset: 0x0000515B
		public override long Length
		{
			get
			{
				return this.internalStream.Length;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006F68 File Offset: 0x00005168
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00006F75 File Offset: 0x00005175
		public override long Position
		{
			get
			{
				return this.internalStream.Position;
			}
			set
			{
				this.internalStream.Position = value;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006F83 File Offset: 0x00005183
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.internalStream.Read(buffer, offset, count);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006F93 File Offset: 0x00005193
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.internalStream.Write(buffer, offset, count);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006FA3 File Offset: 0x000051A3
		public override void SetLength(long value)
		{
			this.internalStream.SetLength(value);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006FB1 File Offset: 0x000051B1
		public override void Flush()
		{
			this.internalStream.Flush();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006FBE File Offset: 0x000051BE
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.internalStream.Seek(offset, origin);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006FCD File Offset: 0x000051CD
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamWrapper>(this);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006FD5 File Offset: 0x000051D5
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006FF4 File Offset: 0x000051F4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					if (this.internalStream != null)
					{
						this.internalStream.Dispose();
					}
				}
				finally
				{
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000704C File Offset: 0x0000524C
		private static byte[] AcquireBuffer(int bufferSize)
		{
			if (StreamWrapper.streamBufferPool == null)
			{
				lock (StreamWrapper.streamBufferPoolLock)
				{
					if (StreamWrapper.streamBufferPool == null)
					{
						StreamWrapper.streamBufferPool = new BufferPool(bufferSize, StreamWrapper.GetBufferCountFromConfig());
					}
				}
			}
			if (bufferSize != StreamWrapper.streamBufferPool.BufferSize)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "bufferSize '{0}' is different from streamBufferPool.BufferSize '{1}'", new object[]
				{
					bufferSize,
					StreamWrapper.streamBufferPool.BufferSize
				}));
			}
			return StreamWrapper.streamBufferPool.Acquire();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000070F4 File Offset: 0x000052F4
		private static void ReleaseBuffer(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			StreamWrapper.streamBufferPool.Release(buffer);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007110 File Offset: 0x00005310
		private static int GetBufferCountFromConfig()
		{
			int num;
			if (!int.TryParse(ConfigurationManager.AppSettings["MaxMimeBufferPoolPerCPU"], out num) || num <= 0)
			{
				num = 100;
			}
			return num;
		}

		// Token: 0x0400010C RID: 268
		private const int DefaultMaxBufferCountPerProcessor = 100;

		// Token: 0x0400010D RID: 269
		private static object streamBufferPoolLock = new object();

		// Token: 0x0400010E RID: 270
		private static BufferPool streamBufferPool;

		// Token: 0x0400010F RID: 271
		private Stream internalStream;

		// Token: 0x04000110 RID: 272
		private DisposeTracker disposeTracker;
	}
}
