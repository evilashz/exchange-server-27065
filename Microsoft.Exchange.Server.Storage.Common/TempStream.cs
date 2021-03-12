using System;
using System.IO;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000A0 RID: 160
	public class TempStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x000158F0 File Offset: 0x00013AF0
		private TempStream(Stream stream)
		{
			this.stream = stream;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001590B File Offset: 0x00013B0B
		private Stream WrappedStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00015913 File Offset: 0x00013B13
		public static BufferPool Pool
		{
			get
			{
				return TempStream.virtualStreamBlockPool;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001591C File Offset: 0x00013B1C
		public static void Configure(string tempPath)
		{
			TempStream.virtualStreamBlockPool = new BufferPool(8192, 32, ConfigurationSchema.CleanupTempStreamBuffersOnRelease.Value, true);
			Streams.ConfigureTempStorage(131072, 8192, TempStream.GetTempPath(tempPath), new Func<int, byte[]>(TempStream.AcquireBuffer), new Action<byte[]>(TempStream.ReleaseBuffer));
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00015974 File Offset: 0x00013B74
		public static Stream CreateInstance()
		{
			Stream stream = Streams.CreateTemporaryStorageStream();
			return new TempStream(stream);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00015990 File Offset: 0x00013B90
		public static Stream CloneStream(Stream originalStream)
		{
			TempStream tempStream = originalStream as TempStream;
			if (tempStream == null)
			{
				return Streams.CloneTemporaryStorageStream(originalStream);
			}
			Stream stream = Streams.CloneTemporaryStorageStream(tempStream.WrappedStream);
			return new TempStream(stream);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000159C0 File Offset: 0x00013BC0
		public static int CopyStream(Stream source, Stream destination)
		{
			if (source == null)
			{
				return 0;
			}
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(BufferPoolCollection.BufferSize.Size32K);
			byte[] array = bufferPool.Acquire();
			int result;
			try
			{
				int num = 0;
				int num2;
				do
				{
					num2 = source.Read(array, 0, array.Length);
					destination.Write(array, 0, num2);
					num += num2;
				}
				while (num2 > 0);
				result = num;
			}
			finally
			{
				bufferPool.Release(array);
			}
			return result;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00015A28 File Offset: 0x00013C28
		public override bool CanRead
		{
			get
			{
				return this.stream != null && this.stream.CanRead;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00015A3F File Offset: 0x00013C3F
		public override bool CanWrite
		{
			get
			{
				return this.stream != null && this.stream.CanWrite;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00015A56 File Offset: 0x00013C56
		public override bool CanSeek
		{
			get
			{
				return this.stream != null && this.stream.CanSeek;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00015A6D File Offset: 0x00013C6D
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00015A7A File Offset: 0x00013C7A
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x00015A87 File Offset: 0x00013C87
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00015A95 File Offset: 0x00013C95
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00015AA5 File Offset: 0x00013CA5
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00015AB5 File Offset: 0x00013CB5
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00015AC2 File Offset: 0x00013CC2
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00015AD0 File Offset: 0x00013CD0
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00015ADF File Offset: 0x00013CDF
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00015B18 File Offset: 0x00013D18
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<TempStream>(this);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00015B20 File Offset: 0x00013D20
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00015B35 File Offset: 0x00013D35
		private static string GetTempPath(string tempPath)
		{
			if (!string.IsNullOrEmpty(tempPath))
			{
				return tempPath;
			}
			return Path.GetTempPath();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00015B46 File Offset: 0x00013D46
		private static byte[] AcquireBuffer(int size)
		{
			return TempStream.virtualStreamBlockPool.Acquire();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00015B52 File Offset: 0x00013D52
		private static void ReleaseBuffer(byte[] buffer)
		{
			TempStream.virtualStreamBlockPool.Release(buffer);
		}

		// Token: 0x040006E2 RID: 1762
		private const int BlockSize = 8192;

		// Token: 0x040006E3 RID: 1763
		private const int MaximumInMemorySize = 131072;

		// Token: 0x040006E4 RID: 1764
		private const int MaxPoolBufferCountPerProcessor = 32;

		// Token: 0x040006E5 RID: 1765
		private static BufferPool virtualStreamBlockPool;

		// Token: 0x040006E6 RID: 1766
		private Stream stream;

		// Token: 0x040006E7 RID: 1767
		private DisposeTracker disposeTracker;
	}
}
