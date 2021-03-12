using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C58 RID: 3160
	internal sealed class NetworkBuffer : SafeHandleMinusOneIsInvalid, IDisposeTrackable, IDisposable
	{
		// Token: 0x060045EE RID: 17902 RVA: 0x000BAA02 File Offset: 0x000B8C02
		public NetworkBuffer(int bufferSize) : base(true)
		{
			this.disposeTracker = ((IDisposeTrackable)this).GetDisposeTracker();
			this.AllocateBuffer(bufferSize);
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x060045EF RID: 17903 RVA: 0x000BAA1E File Offset: 0x000B8C1E
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x060045F0 RID: 17904 RVA: 0x000BAA26 File Offset: 0x000B8C26
		public int Consumed
		{
			get
			{
				return this.consumed;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x000BAA2E File Offset: 0x000B8C2E
		// (set) Token: 0x060045F2 RID: 17906 RVA: 0x000BAA36 File Offset: 0x000B8C36
		public int Filled
		{
			get
			{
				return this.filled;
			}
			set
			{
				this.filled = value;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x000BAA3F File Offset: 0x000B8C3F
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x000BAA47 File Offset: 0x000B8C47
		public int Remaining
		{
			get
			{
				return this.filled - this.consumed;
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x000BAA56 File Offset: 0x000B8C56
		// (set) Token: 0x060045F6 RID: 17910 RVA: 0x000BAA5E File Offset: 0x000B8C5E
		public int EncryptedDataLength
		{
			get
			{
				return this.encryptedBytes;
			}
			set
			{
				this.encryptedBytes = value;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x000BAA67 File Offset: 0x000B8C67
		// (set) Token: 0x060045F8 RID: 17912 RVA: 0x000BAA6F File Offset: 0x000B8C6F
		public int EncryptedDataOffset
		{
			get
			{
				return this.encryptedDataOffset;
			}
			set
			{
				this.encryptedDataOffset = value;
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x000BAA78 File Offset: 0x000B8C78
		public int Unused
		{
			get
			{
				if (this.encryptedBytes == 0)
				{
					return this.length - this.filled;
				}
				return this.length - (this.encryptedDataOffset + this.encryptedBytes);
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060045FA RID: 17914 RVA: 0x000BAAA4 File Offset: 0x000B8CA4
		public int BufferStartOffset
		{
			get
			{
				return this.bufferStartOffset;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x000BAAAC File Offset: 0x000B8CAC
		public int DataStartOffset
		{
			get
			{
				return this.bufferStartOffset + this.consumed;
			}
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060045FC RID: 17916 RVA: 0x000BAABB File Offset: 0x000B8CBB
		public int UnusedStartOffset
		{
			get
			{
				if (this.encryptedBytes == 0)
				{
					return this.bufferStartOffset + this.filled;
				}
				return this.bufferStartOffset + (this.encryptedDataOffset + this.encryptedBytes);
			}
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x000BAAE7 File Offset: 0x000B8CE7
		public void ResetFindLineCache()
		{
			this.lastFindLineFind = -1;
			this.lastFindLineStart = -1;
			this.lastFindLineMaxLineLength = -1;
			this.lastFindLineOverflow = false;
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x000BAB08 File Offset: 0x000B8D08
		public void ReportBytesFilled(int bytes)
		{
			if (bytes < 0 || bytes > this.length - this.filled)
			{
				throw new ArgumentException(NetException.InvalidNumberOfBytes, "bytes");
			}
			if (this.encryptedBytes != 0)
			{
				throw new InvalidOperationException(NetException.UseReportEncryptedBytesFilled);
			}
			this.filled += bytes;
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x000BAB64 File Offset: 0x000B8D64
		public void ReportEncryptedBytesFilled(int bytes)
		{
			if (bytes < 0 || bytes > this.Unused)
			{
				throw new ArgumentException(NetException.InvalidNumberOfBytes, "bytes");
			}
			this.encryptedBytes += bytes;
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000BAB98 File Offset: 0x000B8D98
		public void ExpandBuffer(int newSize)
		{
			int maxBufferSize = NetworkBuffer.BufferFactory.GetMaxBufferSize(this.handle);
			if (newSize > maxBufferSize)
			{
				this.AdjustBuffer(newSize);
				return;
			}
			int optimalBufferSize = NetworkBuffer.BufferFactory.GetOptimalBufferSize(newSize);
			if (optimalBufferSize < maxBufferSize)
			{
				this.AdjustBuffer(optimalBufferSize);
				return;
			}
			this.length = newSize;
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000BABD8 File Offset: 0x000B8DD8
		public int FindLine(int maxLineLength, out bool overflow)
		{
			overflow = false;
			int i = this.consumed;
			int num = -1;
			if (this.lastFindLineStart > this.consumed || this.consumed >= this.lastFindLineFind || this.lastFindLineMaxLineLength != maxLineLength)
			{
				while (i < this.filled)
				{
					i = this.IndexOf(10, i);
					if (i == -1)
					{
						if (this.Remaining >= maxLineLength)
						{
							num = maxLineLength;
							overflow = true;
							break;
						}
						break;
					}
					else if (i > this.consumed && this.buffer[this.bufferStartOffset + i - 1] == 13)
					{
						num = i - this.consumed + 1 - 2;
						if (num > maxLineLength)
						{
							num = maxLineLength;
							overflow = true;
							break;
						}
						break;
					}
					else
					{
						if (i - this.consumed + 1 > maxLineLength)
						{
							num = maxLineLength;
							overflow = true;
							break;
						}
						i++;
					}
				}
				this.lastFindLineStart = this.consumed;
				this.lastFindLineFind = ((-1 == num) ? -1 : (this.lastFindLineStart + num));
				this.lastFindLineMaxLineLength = maxLineLength;
				this.lastFindLineOverflow = overflow;
				return num;
			}
			overflow = this.lastFindLineOverflow;
			if (-1 != this.lastFindLineFind)
			{
				return this.lastFindLineFind - this.lastFindLineStart;
			}
			return -1;
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x000BACE6 File Offset: 0x000B8EE6
		public void ConsumeData(int bytesConsumed)
		{
			if (bytesConsumed < 0 || bytesConsumed > this.Remaining)
			{
				throw new ArgumentException(NetException.InvalidNumberOfBytes, "bytesConsumed");
			}
			this.consumed += bytesConsumed;
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x000BAD18 File Offset: 0x000B8F18
		public void PutBackUnconsumedData(int bytesToPutBack)
		{
			if (bytesToPutBack < 0 || bytesToPutBack > this.consumed)
			{
				throw new ArgumentException(NetException.InvalidNumberOfBytes, "bytesToPutBack");
			}
			this.consumed -= bytesToPutBack;
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x000BAD4A File Offset: 0x000B8F4A
		public void EmptyBuffer()
		{
			this.ResetFindLineCache();
			this.consumed = 0;
			this.filled = 0;
			this.encryptedBytes = 0;
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x000BAD67 File Offset: 0x000B8F67
		public void EmptyBufferReservingBytes(int headerBytes)
		{
			this.ResetFindLineCache();
			this.consumed = headerBytes;
			this.filled = headerBytes;
			this.encryptedBytes = 0;
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x000BAD84 File Offset: 0x000B8F84
		public void ShuffleBuffer()
		{
			this.ResetFindLineCache();
			if (this.Remaining != 0)
			{
				System.Buffer.BlockCopy(this.buffer, this.DataStartOffset, this.buffer, this.BufferStartOffset, this.Remaining);
			}
			this.filled = this.Remaining;
			this.consumed = 0;
			if (this.encryptedBytes != 0)
			{
				System.Buffer.BlockCopy(this.buffer, this.bufferStartOffset + this.encryptedDataOffset, this.buffer, this.bufferStartOffset + this.filled, this.encryptedBytes);
			}
			this.encryptedDataOffset = this.filled;
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x000BAE1A File Offset: 0x000B901A
		DisposeTracker IDisposeTrackable.GetDisposeTracker()
		{
			return DisposeTracker.Get<NetworkBuffer>(this);
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x000BAE22 File Offset: 0x000B9022
		void IDisposeTrackable.SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x000BAE37 File Offset: 0x000B9037
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x000BAE53 File Offset: 0x000B9053
		protected override bool ReleaseHandle()
		{
			NetworkBuffer.BufferFactory.Free(this.handle);
			return true;
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x000BAE64 File Offset: 0x000B9064
		private void AdjustBuffer(int newSize)
		{
			IntPtr handle = this.handle;
			byte[] src = this.buffer;
			int srcOffset = this.bufferStartOffset;
			this.AllocateBuffer(newSize);
			System.Buffer.BlockCopy(src, srcOffset, this.buffer, this.bufferStartOffset, this.filled);
			NetworkBuffer.BufferFactory.Free(handle);
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x000BAEAC File Offset: 0x000B90AC
		private void AllocateBuffer(int bufferSize)
		{
			this.ResetFindLineCache();
			this.handle = NetworkBuffer.BufferFactory.Allocate(bufferSize, out this.buffer, out this.bufferStartOffset);
			this.length = bufferSize;
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x000BAED4 File Offset: 0x000B90D4
		private int IndexOf(byte val, int offset)
		{
			int num = ExBuffer.IndexOf(this.buffer, val, this.bufferStartOffset + offset, this.filled - offset);
			if (num == -1)
			{
				return num;
			}
			return num - this.bufferStartOffset;
		}

		// Token: 0x04003A79 RID: 14969
		private DisposeTracker disposeTracker;

		// Token: 0x04003A7A RID: 14970
		private byte[] buffer;

		// Token: 0x04003A7B RID: 14971
		private int bufferStartOffset;

		// Token: 0x04003A7C RID: 14972
		private int filled;

		// Token: 0x04003A7D RID: 14973
		private int consumed;

		// Token: 0x04003A7E RID: 14974
		private int encryptedBytes;

		// Token: 0x04003A7F RID: 14975
		private int encryptedDataOffset;

		// Token: 0x04003A80 RID: 14976
		private int length;

		// Token: 0x04003A81 RID: 14977
		private int lastFindLineStart;

		// Token: 0x04003A82 RID: 14978
		private int lastFindLineFind;

		// Token: 0x04003A83 RID: 14979
		private int lastFindLineMaxLineLength;

		// Token: 0x04003A84 RID: 14980
		private bool lastFindLineOverflow;

		// Token: 0x02000C59 RID: 3161
		internal static class BufferFactory
		{
			// Token: 0x0600460E RID: 17934 RVA: 0x000BAF0C File Offset: 0x000B910C
			internal static IntPtr Allocate(int size, out byte[] buffer, out int offset)
			{
				if (size < 0 || size > 262144)
				{
					throw new ArgumentException(NetException.InvalidNumberOfBytes, "size");
				}
				NetworkBuffer.BufferFactory.PoolId poolId;
				int num;
				if (size <= 4096)
				{
					poolId = NetworkBuffer.BufferFactory.PoolId.Small;
					num = NetworkBuffer.BufferFactory.bufferPoolSmall.Alloc(out buffer, out offset);
				}
				else if (size <= 20528)
				{
					poolId = NetworkBuffer.BufferFactory.PoolId.Medium;
					num = NetworkBuffer.BufferFactory.bufferPoolMedium.Alloc(out buffer, out offset);
				}
				else if (size <= 24576)
				{
					poolId = NetworkBuffer.BufferFactory.PoolId.MediumLarge;
					num = NetworkBuffer.BufferFactory.bufferPoolMediumLarge.Alloc(out buffer, out offset);
				}
				else
				{
					poolId = NetworkBuffer.BufferFactory.PoolId.Large;
					num = size;
					offset = 0;
					buffer = new byte[size];
				}
				return (IntPtr)(num << 8 | (int)poolId);
			}

			// Token: 0x0600460F RID: 17935 RVA: 0x000BAFA0 File Offset: 0x000B91A0
			internal static void Free(IntPtr bufferIndexAndPool)
			{
				NetworkBuffer.BufferFactory.PoolId poolId = (NetworkBuffer.BufferFactory.PoolId)((int)bufferIndexAndPool & 255);
				int bufferToFree = (int)bufferIndexAndPool >> 8;
				switch (poolId)
				{
				case NetworkBuffer.BufferFactory.PoolId.Small:
					NetworkBuffer.BufferFactory.bufferPoolSmall.Free(bufferToFree);
					return;
				case NetworkBuffer.BufferFactory.PoolId.Medium:
					NetworkBuffer.BufferFactory.bufferPoolMedium.Free(bufferToFree);
					return;
				case NetworkBuffer.BufferFactory.PoolId.MediumLarge:
					NetworkBuffer.BufferFactory.bufferPoolMediumLarge.Free(bufferToFree);
					return;
				case NetworkBuffer.BufferFactory.PoolId.Large:
					return;
				default:
					throw new ArgumentException(NetException.InvalidSize, "poolId");
				}
			}

			// Token: 0x06004610 RID: 17936 RVA: 0x000BB018 File Offset: 0x000B9218
			internal static int GetMaxBufferSize(IntPtr bufferIndexAndPool)
			{
				switch ((int)bufferIndexAndPool & 255)
				{
				case 1:
					return NetworkBuffer.BufferFactory.bufferPoolSmall.MaxBufferSize;
				case 2:
					return NetworkBuffer.BufferFactory.bufferPoolMedium.MaxBufferSize;
				case 3:
					return NetworkBuffer.BufferFactory.bufferPoolMediumLarge.MaxBufferSize;
				case 4:
					return (int)bufferIndexAndPool >> 8;
				default:
					throw new ArgumentException(NetException.InvalidSize, "poolId");
				}
			}

			// Token: 0x06004611 RID: 17937 RVA: 0x000BB08C File Offset: 0x000B928C
			internal static int GetOptimalBufferSize(int size)
			{
				if (size <= NetworkBuffer.BufferFactory.bufferPoolSmall.MaxBufferSize)
				{
					return NetworkBuffer.BufferFactory.bufferPoolSmall.MaxBufferSize;
				}
				if (size <= NetworkBuffer.BufferFactory.bufferPoolMedium.MaxBufferSize)
				{
					return NetworkBuffer.BufferFactory.bufferPoolMedium.MaxBufferSize;
				}
				if (size <= NetworkBuffer.BufferFactory.bufferPoolMediumLarge.MaxBufferSize)
				{
					return NetworkBuffer.BufferFactory.bufferPoolMediumLarge.MaxBufferSize;
				}
				return size;
			}

			// Token: 0x04003A85 RID: 14981
			private const int BufferSizeSmall = 4096;

			// Token: 0x04003A86 RID: 14982
			private const int BufferSizeMedium = 20528;

			// Token: 0x04003A87 RID: 14983
			private const int BufferSizeMediumLarge = 24576;

			// Token: 0x04003A88 RID: 14984
			private const int MaxBufferSize = 262144;

			// Token: 0x04003A89 RID: 14985
			private static BufferManager bufferPoolSmall = new BufferManager(4096, 1048576);

			// Token: 0x04003A8A RID: 14986
			private static BufferManager bufferPoolMedium = new BufferManager(20528, 1048576);

			// Token: 0x04003A8B RID: 14987
			private static BufferManager bufferPoolMediumLarge = new BufferManager(24576, 1048576);

			// Token: 0x02000C5A RID: 3162
			internal enum PoolId
			{
				// Token: 0x04003A8D RID: 14989
				Small = 1,
				// Token: 0x04003A8E RID: 14990
				Medium,
				// Token: 0x04003A8F RID: 14991
				MediumLarge,
				// Token: 0x04003A90 RID: 14992
				Large
			}
		}
	}
}
