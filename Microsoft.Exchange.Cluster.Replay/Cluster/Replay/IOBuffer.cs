using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200033F RID: 831
	internal class IOBuffer
	{
		// Token: 0x060021A9 RID: 8617 RVA: 0x0009C7AC File Offset: 0x0009A9AC
		public IOBuffer(int size, bool preAllocated)
		{
			this.AppendOffset = 0;
			this.NextBuffer = null;
			this.Buffer = new byte[size];
			this.PreAllocated = preAllocated;
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x0009C7D5 File Offset: 0x0009A9D5
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x0009C7DD File Offset: 0x0009A9DD
		public byte[] Buffer { get; private set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x0009C7E6 File Offset: 0x0009A9E6
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x0009C7EE File Offset: 0x0009A9EE
		public int AppendOffset
		{
			get
			{
				return this.appendOffset;
			}
			set
			{
				this.appendOffset = value;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x0009C7F7 File Offset: 0x0009A9F7
		// (set) Token: 0x060021AF RID: 8623 RVA: 0x0009C7FF File Offset: 0x0009A9FF
		public IOBuffer NextBuffer
		{
			get
			{
				return this.nextBuffer;
			}
			set
			{
				this.nextBuffer = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x0009C808 File Offset: 0x0009AA08
		public int RemainingSpace
		{
			get
			{
				return this.Buffer.Length - this.AppendOffset;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0009C819 File Offset: 0x0009AA19
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x0009C821 File Offset: 0x0009AA21
		public bool PreAllocated { get; private set; }

		// Token: 0x04000DDE RID: 3550
		private int appendOffset;

		// Token: 0x04000DDF RID: 3551
		private IOBuffer nextBuffer;
	}
}
