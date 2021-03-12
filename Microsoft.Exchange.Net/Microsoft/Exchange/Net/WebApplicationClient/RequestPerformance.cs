using System;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B26 RID: 2854
	public struct RequestPerformance
	{
		// Token: 0x06003D97 RID: 15767 RVA: 0x000A0633 File Offset: 0x0009E833
		internal RequestPerformance(SendRequestOperation sendRequestOperation)
		{
			this.sendRequestOperation = sendRequestOperation;
			this.bytesSent = -1L;
			this.bytesReceived = -1L;
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000A064C File Offset: 0x0009E84C
		public long BytesSent
		{
			get
			{
				if (this.sendRequestOperation == null)
				{
					return 0L;
				}
				if (this.bytesSent == -1L)
				{
					this.bytesSent = this.sendRequestOperation.BytesSent;
				}
				return this.bytesSent;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x000A067A File Offset: 0x0009E87A
		public long BytesReceived
		{
			get
			{
				if (this.sendRequestOperation == null)
				{
					return 0L;
				}
				if (this.bytesReceived == -1L)
				{
					this.bytesReceived = this.sendRequestOperation.BytesReceived;
				}
				return this.bytesReceived;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x000A06A8 File Offset: 0x0009E8A8
		public long ElapsedTicks
		{
			get
			{
				if (this.sendRequestOperation == null)
				{
					return 0L;
				}
				return this.sendRequestOperation.ElapsedTicks;
			}
		}

		// Token: 0x040035A1 RID: 13729
		private SendRequestOperation sendRequestOperation;

		// Token: 0x040035A2 RID: 13730
		private long bytesSent;

		// Token: 0x040035A3 RID: 13731
		private long bytesReceived;
	}
}
