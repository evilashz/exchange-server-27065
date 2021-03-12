using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000074 RID: 116
	public class LohFriendlyStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000955C File Offset: 0x0000775C
		public LohFriendlyStream(int size)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", size, NetException.NegativeParameter);
			}
			this.isOpen = true;
			this.startingSize = LohFriendlyStream.GetCapacityToUse(size);
			this.buffers = new List<byte[]>(this.startingSize / 16384);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002CD RID: 717 RVA: 0x000095DC File Offset: 0x000077DC
		public override bool CanRead
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000095E4 File Offset: 0x000077E4
		public override bool CanSeek
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000095EC File Offset: 0x000077EC
		public override bool CanWrite
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x000095F4 File Offset: 0x000077F4
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x000095FC File Offset: 0x000077FC
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
			set
			{
				this.TryIncreaseCapacity(value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00009606 File Offset: 0x00007806
		public override long Length
		{
			get
			{
				return (long)this.length;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000960F File Offset: 0x0000780F
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00009618 File Offset: 0x00007818
		public override long Position
		{
			get
			{
				return (long)this.position;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", value, NetException.NegativeParameter);
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", value, NetException.LargeParameter);
				}
				this.position = (int)value;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00009671 File Offset: 0x00007871
		public LohFriendlyStream GetReference()
		{
			Interlocked.Increment(ref this.referenceCount);
			this.position = 0;
			return this;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00009687 File Offset: 0x00007887
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LohFriendlyStream>(this);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000968F File Offset: 0x0000788F
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000096A4 File Offset: 0x000078A4
		public override void Flush()
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000096A8 File Offset: 0x000078A8
		public override int Read(byte[] buffer, int offset, int count)
		{
			LohFriendlyStream.CheckBufferArguments(buffer, offset, count);
			int num = this.length - this.position;
			if (num > count)
			{
				num = count;
			}
			if (num > 0)
			{
				int i = 0;
				int num2 = this.position / 16384;
				int num3 = this.position % 16384;
				while (i < num)
				{
					byte[] array = this.buffers[num2];
					int num4 = Math.Min(array.Length - num3, num - i);
					Buffer.BlockCopy(array, num3, buffer, offset + i, num4);
					this.position += num4;
					i += num4;
					num2++;
					num3 = 0;
				}
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009744 File Offset: 0x00007944
		public override int ReadByte()
		{
			if (this.position >= this.length)
			{
				return -1;
			}
			int result = (int)this.buffers[this.position / 16384][this.position % 16384];
			this.position++;
			return result;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009798 File Offset: 0x00007998
		public override long Seek(long offset, SeekOrigin origin)
		{
			LohFriendlyStream.CheckForLargeBufferIndex(offset);
			switch (origin)
			{
			case SeekOrigin.Begin:
				LohFriendlyStream.CheckForNegativeBufferIndex(offset);
				this.position = (int)offset;
				break;
			case SeekOrigin.Current:
			{
				long num = offset + (long)this.position;
				LohFriendlyStream.CheckForLargeBufferIndex(num);
				LohFriendlyStream.CheckForNegativeBufferIndex(num);
				this.position = (int)num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = (long)this.length + offset;
				LohFriendlyStream.CheckForLargeBufferIndex(num);
				LohFriendlyStream.CheckForNegativeBufferIndex(num);
				this.position = (int)num;
				break;
			}
			default:
				throw new ArgumentException(NetException.SeekOrigin, "origin");
			}
			return (long)this.position;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009830 File Offset: 0x00007A30
		public override void SetLength(long value)
		{
			if (value > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("value", value, NetException.LargeParameter);
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", value, NetException.NegativeParameter);
			}
			int num = (int)value;
			this.TryIncreaseCapacity(num);
			this.length = num;
			if (this.position > num)
			{
				this.position = num;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000098A4 File Offset: 0x00007AA4
		public byte[] ToArray()
		{
			byte[] array = new byte[this.length];
			int num = 0;
			for (int i = 0; i < this.buffers.Count; i++)
			{
				int num2 = Math.Min(this.buffers[i].Length, this.length - num);
				Buffer.BlockCopy(this.buffers[i], 0, array, num, num2);
				num += num2;
			}
			return array;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000990C File Offset: 0x00007B0C
		public override void Write(byte[] buffer, int offset, int count)
		{
			LohFriendlyStream.CheckBufferArguments(buffer, offset, count);
			long num = (long)(this.position + count);
			LohFriendlyStream.CheckForLargeBufferIndex(num);
			LohFriendlyStream.CheckForNegativeBufferIndex(num);
			int value = (int)num;
			if (num > (long)this.length)
			{
				if (num > (long)this.capacity)
				{
					this.TryIncreaseCapacity(value);
				}
				this.length = value;
			}
			int i = 0;
			int num2 = this.position / 16384;
			int num3 = this.position % 16384;
			while (i < count)
			{
				byte[] array = this.buffers[num2];
				int num4 = Math.Min(array.Length - num3, count - i);
				Buffer.BlockCopy(buffer, offset + i, array, num3, num4);
				this.position += num4;
				i += num4;
				num2++;
				num3 = 0;
			}
			this.position = value;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000099D0 File Offset: 0x00007BD0
		public override void WriteByte(byte value)
		{
			if (this.position >= this.length)
			{
				int num = this.position + 1;
				if (num >= this.capacity)
				{
					this.TryIncreaseCapacity(num);
				}
				this.length = num;
			}
			this.buffers[this.position / 16384][this.position % 16384] = value;
			this.position++;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009A40 File Offset: 0x00007C40
		public void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			int num = 0;
			int num2 = 0;
			while (num2 < this.buffers.Count && this.Length - (long)num > 0L)
			{
				int num3 = Math.Min(this.buffers[num2].Length, this.length - num);
				stream.Write(this.buffers[num2], 0, num3);
				num += num3;
				num2++;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00009AB4 File Offset: 0x00007CB4
		protected override void Dispose(bool disposing)
		{
			if (Interlocked.Decrement(ref this.referenceCount) == 0)
			{
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009ACA File Offset: 0x00007CCA
		private static void CheckForNegativeBufferIndex(long index)
		{
			if (index < 0L)
			{
				throw new IOException(NetException.NegativeIndex);
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009AE1 File Offset: 0x00007CE1
		private static void CheckForLargeBufferIndex(long index)
		{
			if (index > 2147483647L)
			{
				throw new IOException(NetException.LargeIndex);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009AFC File Offset: 0x00007CFC
		private static void CheckBufferArguments(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", offset, NetException.NegativeParameter);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, NetException.NegativeParameter);
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(NetException.BufferOverflow);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009B70 File Offset: 0x00007D70
		private static int GetCapacityToUse(int minimumCapacity)
		{
			if (minimumCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("minimumCapacity", minimumCapacity, NetException.NegativeCapacity);
			}
			int num = minimumCapacity / 16384 + ((minimumCapacity % 16384 > 0) ? 1 : 0);
			return num * 16384;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00009BBC File Offset: 0x00007DBC
		private void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.isOpen = false;
					this.ReleaseCurrentBuffers();
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00009C08 File Offset: 0x00007E08
		private bool TryIncreaseCapacity(int value)
		{
			if (value < 0)
			{
				throw new IOException(NetException.NegativeCapacity);
			}
			int num = Math.Max(value, this.startingSize);
			bool flag = num > this.capacity;
			if (flag)
			{
				this.SetCapacity(LohFriendlyStream.GetCapacityToUse(num));
			}
			return flag;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00009C50 File Offset: 0x00007E50
		private void SetCapacity(int value)
		{
			if (value != this.capacity)
			{
				if (value < this.length)
				{
					throw new ArgumentOutOfRangeException("value", value, NetException.SmallCapacity);
				}
				ExTraceGlobals.CommonTracer.TraceWarning<int, int>((long)this.GetHashCode(), "LohFriendlyStream.SetCapacity({0}) called for a stream that had a buffer with capacity = {1}.", value, this.capacity);
				if (value > 0)
				{
					while (this.buffers.Count * 16384 < value)
					{
						this.buffers.Add(this.currentPool.Acquire());
					}
				}
				this.capacity = value;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00009CE0 File Offset: 0x00007EE0
		private void ReleaseCurrentBuffers()
		{
			for (int i = 0; i < this.buffers.Count; i++)
			{
				this.currentPool.Release(this.buffers[i]);
			}
			this.buffers.Clear();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009D25 File Offset: 0x00007F25
		[Conditional("DEBUG")]
		private void CheckDisposed()
		{
			if (!this.isOpen)
			{
				throw new ObjectDisposedException(NetException.ClosedStream);
			}
		}

		// Token: 0x04000133 RID: 307
		public const int MaximumBufferSize = 2147483647;

		// Token: 0x04000134 RID: 308
		private const int DefaultBufferSize = 16384;

		// Token: 0x04000135 RID: 309
		private readonly int startingSize;

		// Token: 0x04000136 RID: 310
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000137 RID: 311
		private List<byte[]> buffers;

		// Token: 0x04000138 RID: 312
		private BufferPool currentPool = BufferPoolCollection.AutoCleanupCollection.Acquire(BufferPoolCollection.BufferSize.Size16K);

		// Token: 0x04000139 RID: 313
		private int referenceCount = 1;

		// Token: 0x0400013A RID: 314
		private bool isOpen;

		// Token: 0x0400013B RID: 315
		private int capacity;

		// Token: 0x0400013C RID: 316
		private int length;

		// Token: 0x0400013D RID: 317
		private int position;
	}
}
