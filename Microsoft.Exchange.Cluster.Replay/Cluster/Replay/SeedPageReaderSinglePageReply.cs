using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000138 RID: 312
	internal class SeedPageReaderSinglePageReply : NetworkChannelMessage
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000349F4 File Offset: 0x00032BF4
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x000349FC File Offset: 0x00032BFC
		internal long PageNumber
		{
			get
			{
				return this.m_pageno;
			}
			set
			{
				this.m_pageno = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00034A05 File Offset: 0x00032C05
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x00034A0D File Offset: 0x00032C0D
		internal long LowGeneration
		{
			get
			{
				return this.m_lowGeneration;
			}
			set
			{
				this.m_lowGeneration = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00034A16 File Offset: 0x00032C16
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00034A1E File Offset: 0x00032C1E
		internal long HighGeneration
		{
			get
			{
				return this.m_highGeneration;
			}
			set
			{
				this.m_highGeneration = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00034A27 File Offset: 0x00032C27
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x00034A2F File Offset: 0x00032C2F
		internal byte[] PageBytes
		{
			get
			{
				return this.m_pageBytes;
			}
			set
			{
				this.m_pageBytes = value;
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00034A38 File Offset: 0x00032C38
		internal SeedPageReaderSinglePageReply(NetworkChannel channel) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderSinglePageReply)
		{
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00034A48 File Offset: 0x00032C48
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_pageno);
			base.Packet.Append(this.m_lowGeneration);
			base.Packet.Append(this.m_highGeneration);
			int val = this.m_pageBytes.Length;
			base.Packet.Append(val);
			base.Packet.Append(this.m_pageBytes);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00034AB4 File Offset: 0x00032CB4
		internal SeedPageReaderSinglePageReply(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderSinglePageReply, packetContent)
		{
			this.m_pageno = base.Packet.ExtractInt64();
			this.m_lowGeneration = base.Packet.ExtractInt64();
			this.m_highGeneration = base.Packet.ExtractInt64();
			int len = base.Packet.ExtractInt32();
			this.m_pageBytes = base.Packet.ExtractBytes(len);
		}

		// Token: 0x0400051C RID: 1308
		private long m_pageno;

		// Token: 0x0400051D RID: 1309
		private long m_lowGeneration;

		// Token: 0x0400051E RID: 1310
		private long m_highGeneration;

		// Token: 0x0400051F RID: 1311
		private byte[] m_pageBytes;
	}
}
