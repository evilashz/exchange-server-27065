using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000B6 RID: 182
	public abstract class TraceBuffer : DisposableBase
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x000175F5 File Offset: 0x000157F5
		protected TraceBuffer(Guid recordGuid, byte[] data)
		{
			this.recordGuid = recordGuid;
			this.data = data;
			this.offset = 0;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00017612 File Offset: 0x00015812
		public Guid RecordGuid
		{
			get
			{
				return this.recordGuid;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001761A File Offset: 0x0001581A
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00017622 File Offset: 0x00015822
		public int Length
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001762A File Offset: 0x0001582A
		public static TraceBuffer Create(Guid recordGuid, int length, bool useBufferPool)
		{
			if (useBufferPool)
			{
				return TraceBuffer.TraceBufferPool.Create(recordGuid, length);
			}
			return TraceBuffer.TraceBufferInstance.Create(recordGuid, length);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001763E File Offset: 0x0001583E
		public void WriteByte(byte value)
		{
			if (this.offset + 1 <= this.data.Length)
			{
				this.data[this.offset] = value;
				this.offset++;
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001766E File Offset: 0x0001586E
		public void WriteShort(short value)
		{
			if (this.offset + 2 <= this.data.Length)
			{
				this.offset += ExBitConverter.Write(value, this.data, this.offset);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000176A1 File Offset: 0x000158A1
		public void WriteInt(int value)
		{
			if (this.offset + 4 <= this.data.Length)
			{
				this.offset += ExBitConverter.Write(value, this.data, this.offset);
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000176D4 File Offset: 0x000158D4
		public void WriteInt(uint value)
		{
			if (this.offset + 4 <= this.data.Length)
			{
				this.offset += ExBitConverter.Write(value, this.data, this.offset);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00017707 File Offset: 0x00015907
		public void WriteLong(long value)
		{
			if (this.offset + 8 <= this.data.Length)
			{
				this.offset += ExBitConverter.Write(value, this.data, this.offset);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001773A File Offset: 0x0001593A
		public void WriteDouble(double value)
		{
			if (this.offset + 8 <= this.data.Length)
			{
				this.offset += ExBitConverter.Write(value, this.data, this.offset);
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00017770 File Offset: 0x00015970
		public void WriteCountedAsciiString(string value)
		{
			string text = value ?? string.Empty;
			int byteCount = CTSGlobals.AsciiEncoding.GetByteCount(text);
			if (byteCount < 32767)
			{
				int num = byteCount + 2 + 1;
				short value2 = (short)byteCount;
				int num2 = 0;
				if (this.offset + num <= this.data.Length)
				{
					num2 += ExBitConverter.Write(value2, this.data, this.offset);
					num2 += ExBitConverter.Write(text, false, this.data, this.offset + 2);
					this.offset += num2 - 1;
				}
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000177FC File Offset: 0x000159FC
		public void WriteAsciiString(string value)
		{
			string text = value ?? string.Empty;
			int num = CTSGlobals.AsciiEncoding.GetByteCount(text) + 1;
			if (this.offset + num <= this.data.Length)
			{
				this.offset += ExBitConverter.Write(text, false, this.data, this.offset);
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00017854 File Offset: 0x00015A54
		public void WriteCountedUnicodeString(string value)
		{
			string text = value ?? string.Empty;
			int byteCount = CTSGlobals.UnicodeEncoding.GetByteCount(text);
			if (byteCount < 32767)
			{
				int num = byteCount + 2 + 2;
				short value2 = (short)byteCount;
				int num2 = 0;
				if (this.offset + num <= this.data.Length)
				{
					num2 += ExBitConverter.Write(value2, this.data, this.offset);
					num2 += ExBitConverter.Write(text, true, this.data, this.offset + 2);
					this.offset += num2 - 2;
				}
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000178E0 File Offset: 0x00015AE0
		public void WriteUnicodeString(string value)
		{
			string text = value ?? string.Empty;
			int num = CTSGlobals.UnicodeEncoding.GetByteCount(text) + 2;
			if (this.offset + num <= this.data.Length)
			{
				this.offset += ExBitConverter.Write(text, true, this.data, this.offset);
			}
		}

		// Token: 0x04000722 RID: 1826
		private readonly Guid recordGuid;

		// Token: 0x04000723 RID: 1827
		private readonly byte[] data;

		// Token: 0x04000724 RID: 1828
		private int offset;

		// Token: 0x020000B7 RID: 183
		private sealed class TraceBufferInstance : TraceBuffer
		{
			// Token: 0x06000875 RID: 2165 RVA: 0x00017938 File Offset: 0x00015B38
			private TraceBufferInstance(Guid recordGuid, byte[] data) : base(recordGuid, data)
			{
			}

			// Token: 0x06000876 RID: 2166 RVA: 0x00017942 File Offset: 0x00015B42
			public static TraceBuffer Create(Guid recordGuid, int length)
			{
				return new TraceBuffer.TraceBufferInstance(recordGuid, new byte[length]);
			}

			// Token: 0x06000877 RID: 2167 RVA: 0x00017950 File Offset: 0x00015B50
			protected override void InternalDispose(bool calledFromDispose)
			{
			}

			// Token: 0x06000878 RID: 2168 RVA: 0x00017952 File Offset: 0x00015B52
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<TraceBuffer.TraceBufferInstance>(this);
			}
		}

		// Token: 0x020000B8 RID: 184
		private sealed class TraceBufferPool : TraceBuffer
		{
			// Token: 0x06000879 RID: 2169 RVA: 0x0001795A File Offset: 0x00015B5A
			private TraceBufferPool(Guid recordGuid, BufferPool bufferPool, byte[] data) : base(recordGuid, data)
			{
				this.bufferPool = bufferPool;
			}

			// Token: 0x0600087A RID: 2170 RVA: 0x0001796C File Offset: 0x00015B6C
			public static TraceBuffer Create(Guid recordGuid, int length)
			{
				BufferPoolCollection.BufferSize bufferSize;
				if (!BufferPoolCollection.AutoCleanupCollection.TryMatchBufferSize(length, out bufferSize))
				{
					return new TraceBuffer.TraceBufferPool(recordGuid, null, TraceBuffer.TraceBufferPool.Empty);
				}
				BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(bufferSize);
				return new TraceBuffer.TraceBufferPool(recordGuid, bufferPool, bufferPool.Acquire());
			}

			// Token: 0x0600087B RID: 2171 RVA: 0x000179AE File Offset: 0x00015BAE
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose && this.bufferPool != null)
				{
					this.bufferPool.Release(base.Data);
				}
			}

			// Token: 0x0600087C RID: 2172 RVA: 0x000179CC File Offset: 0x00015BCC
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<TraceBuffer.TraceBufferPool>(this);
			}

			// Token: 0x04000725 RID: 1829
			private static readonly byte[] Empty = new byte[0];

			// Token: 0x04000726 RID: 1830
			private readonly BufferPool bufferPool;
		}
	}
}
