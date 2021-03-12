using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000025 RID: 37
	internal class AirSyncStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		public AirSyncStream()
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.internalStream = Streams.CreateTemporaryStorageStream(new Func<int, byte[]>(AirSyncStream.AcquireBuffer), new Action<byte[]>(AirSyncStream.ReleaseBuffer));
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.RegisterDisposableData(this);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000F020 File Offset: 0x0000D220
		public override bool CanRead
		{
			get
			{
				return this.internalStream.CanRead;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000F02D File Offset: 0x0000D22D
		public override bool CanWrite
		{
			get
			{
				return this.internalStream.CanWrite;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000F03A File Offset: 0x0000D23A
		public override bool CanSeek
		{
			get
			{
				return this.internalStream.CanSeek;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000F048 File Offset: 0x0000D248
		public override long Length
		{
			get
			{
				long length = this.internalStream.Length;
				if (this.DoBase64Conversion)
				{
					long num = length / 3L * 4L;
					return num + ((length % 3L != 0L) ? 4L : 0L);
				}
				return length;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000F084 File Offset: 0x0000D284
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000F091 File Offset: 0x0000D291
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

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000F09F File Offset: 0x0000D29F
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000F0A7 File Offset: 0x0000D2A7
		public bool DoBase64Conversion { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000F0DB File Offset: 0x0000D2DB
		public int StreamHash
		{
			get
			{
				if (this.streamHash == 0 && this.internalStream.Length > 0L)
				{
					this.streamHash = this.GetStreamHash();
				}
				return this.streamHash;
			}
			set
			{
				this.streamHash = value;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.streamHash = 0;
			this.internalStream.Write(buffer, offset, count);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(string.Format(CultureInfo.InstalledUICulture, "buffer is not long enough: buffer.Length={0}, offset={1}, count={2}", new object[]
				{
					buffer.Length,
					offset,
					count
				}));
			}
			if (this.DoBase64Conversion)
			{
				int byteCount = (count * 3 / 4 < 1) ? 1 : (count * 3 / 4);
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					return StreamHelper.CopyStreamWithBase64Conversion(this.internalStream, memoryStream, byteCount, true);
				}
			}
			return this.internalStream.Read(buffer, offset, count);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		public void CopyStream(Stream outputStream, int count)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (this.DoBase64Conversion)
			{
				StreamHelper.CopyStreamWithBase64Conversion(this.internalStream, outputStream, count, true);
				return;
			}
			StreamHelper.CopyStream(this.internalStream, outputStream, count);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000F1EA File Offset: 0x0000D3EA
		public override void SetLength(long value)
		{
			this.internalStream.SetLength(value);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public override void Flush()
		{
			this.internalStream.Flush();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000F205 File Offset: 0x0000D405
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.internalStream.Seek(offset, origin);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000F214 File Offset: 0x0000D414
		public override string ToString()
		{
			long position = this.Position;
			this.Position = 0L;
			StreamReader streamReader = new StreamReader(this, Encoding.UTF8);
			string result = streamReader.ReadToEnd();
			this.Position = position;
			return result;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000F24B File Offset: 0x0000D44B
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AirSyncStream>(this);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000F253 File Offset: 0x0000D453
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000F26F File Offset: 0x0000D46F
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.internalStream != null)
				{
					this.internalStream.Dispose();
					this.internalStream = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		private static byte[] AcquireBuffer(int bufferSize)
		{
			if (AirSyncStream.streamBufferPool == null)
			{
				lock (AirSyncStream.streamBufferPoolLock)
				{
					if (AirSyncStream.streamBufferPool == null)
					{
						AirSyncStream.streamBufferPool = new BufferPool(bufferSize, AirSyncStream.defaultMaxBufferCountPerProcessor);
					}
				}
			}
			if (bufferSize != AirSyncStream.streamBufferPool.BufferSize)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "bufferSize '{0}' is different from streamBufferPool.BufferSize '{1}'", new object[]
				{
					bufferSize,
					AirSyncStream.streamBufferPool.BufferSize
				}));
			}
			return AirSyncStream.streamBufferPool.Acquire();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000F364 File Offset: 0x0000D564
		private static void ReleaseBuffer(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			AirSyncStream.streamBufferPool.Release(buffer);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000F384 File Offset: 0x0000D584
		private int GetStreamHash()
		{
			int num = 0;
			uint num2 = 0U;
			long position = this.internalStream.Position;
			this.internalStream.Position = 0L;
			bool flag = false;
			byte[] array;
			if (AirSyncStream.streamBufferPool != null)
			{
				array = AirSyncStream.streamBufferPool.Acquire();
				flag = true;
				goto IL_46;
			}
			array = new byte[8192];
			try
			{
				do
				{
					IL_46:
					int num3 = this.internalStream.Read(array, 0, array.Length);
					if (num3 == 0)
					{
						break;
					}
					num2 = StreamHelper.UpdateCrc32(num2, array, 0, num3);
					num += num3;
				}
				while ((long)num < this.Length);
				this.internalStream.Position = position;
			}
			finally
			{
				if (flag && array != null)
				{
					AirSyncStream.streamBufferPool.Release(array);
				}
			}
			return (int)num2;
		}

		// Token: 0x04000246 RID: 582
		private static int defaultMaxBufferCountPerProcessor = GlobalSettings.MaxWorkerThreadsPerProc * 16;

		// Token: 0x04000247 RID: 583
		private static object streamBufferPoolLock = new object();

		// Token: 0x04000248 RID: 584
		private static volatile BufferPool streamBufferPool;

		// Token: 0x04000249 RID: 585
		private Stream internalStream;

		// Token: 0x0400024A RID: 586
		private DisposeTracker disposeTracker;

		// Token: 0x0400024B RID: 587
		private int streamHash;
	}
}
