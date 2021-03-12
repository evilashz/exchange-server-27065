using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WorkBuffer : BaseObject
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00008960 File Offset: 0x00006B60
		public WorkBuffer(int maxSizeOfBuffer)
		{
			if (maxSizeOfBuffer < 0)
			{
				throw new ArgumentException("Max buffer size cannot be negative.");
			}
			this.maxSizeOfBuffer = Math.Min(maxSizeOfBuffer, 16777216);
			if (this.maxSizeOfBuffer <= 0)
			{
				this.arraySegment = Array<byte>.EmptySegment;
				return;
			}
			if (this.maxSizeOfBuffer > AsyncBufferPools.MaxBufferSize)
			{
				this.oneOffBuffer = new byte[this.maxSizeOfBuffer];
				this.arraySegment = new ArraySegment<byte>(this.oneOffBuffer);
				return;
			}
			this.leasedBuffer = AsyncBufferPools.GetBuffer(this.maxSizeOfBuffer);
			this.arraySegment = new ArraySegment<byte>(this.leasedBuffer, 0, this.maxSizeOfBuffer);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008A01 File Offset: 0x00006C01
		public WorkBuffer(ArraySegment<byte> data)
		{
			this.maxSizeOfBuffer = data.Count;
			this.arraySegment = data;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00008A1D File Offset: 0x00006C1D
		public ArraySegment<byte> ArraySegment
		{
			get
			{
				base.CheckDisposed();
				return this.arraySegment;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00008A2B File Offset: 0x00006C2B
		public byte[] Array
		{
			get
			{
				base.CheckDisposed();
				return this.arraySegment.Array;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00008A3E File Offset: 0x00006C3E
		public int Offset
		{
			get
			{
				base.CheckDisposed();
				return this.arraySegment.Offset;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00008A51 File Offset: 0x00006C51
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00008A64 File Offset: 0x00006C64
		public int Count
		{
			get
			{
				base.CheckDisposed();
				return this.arraySegment.Count;
			}
			set
			{
				base.CheckDisposed();
				if (value > this.maxSizeOfBuffer)
				{
					throw new ArgumentException("Cannot set count to more than maximum size of buffer.");
				}
				if (this.leasedBuffer != null)
				{
					this.arraySegment = new ArraySegment<byte>(this.leasedBuffer, 0, value);
					return;
				}
				if (this.oneOffBuffer != null)
				{
					this.arraySegment = new ArraySegment<byte>(this.oneOffBuffer, 0, value);
					return;
				}
				this.arraySegment = new ArraySegment<byte>(this.arraySegment.Array, this.arraySegment.Offset, value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00008AE5 File Offset: 0x00006CE5
		public int MaxSize
		{
			get
			{
				base.CheckDisposed();
				return this.maxSizeOfBuffer;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008AF3 File Offset: 0x00006CF3
		protected override void InternalDispose()
		{
			if (this.leasedBuffer != null)
			{
				AsyncBufferPools.ReleaseBuffer(this.leasedBuffer);
			}
			base.InternalDispose();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008B0E File Offset: 0x00006D0E
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<WorkBuffer>(this);
		}

		// Token: 0x040000F3 RID: 243
		private const int MaximumAllowedAllocationSize = 16777216;

		// Token: 0x040000F4 RID: 244
		private readonly byte[] leasedBuffer;

		// Token: 0x040000F5 RID: 245
		private readonly byte[] oneOffBuffer;

		// Token: 0x040000F6 RID: 246
		private readonly int maxSizeOfBuffer;

		// Token: 0x040000F7 RID: 247
		private ArraySegment<byte> arraySegment;
	}
}
