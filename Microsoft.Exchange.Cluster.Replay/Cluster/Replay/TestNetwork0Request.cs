using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000278 RID: 632
	internal class TestNetwork0Request : NetworkChannelMessage, INetworkChannelRequest
	{
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x00065291 File Offset: 0x00063491
		// (set) Token: 0x060018A7 RID: 6311 RVA: 0x00065299 File Offset: 0x00063499
		public TestNetworkParms Parms { get; private set; }

		// Token: 0x060018A8 RID: 6312 RVA: 0x000652A2 File Offset: 0x000634A2
		internal TestNetwork0Request(NetworkChannel channel, TestNetworkParms parms) : base(channel, NetworkChannelMessage.MessageType.TestNetwork0)
		{
			this.Parms = parms;
			this.m_serializedParms = parms.ToBytes();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000652C3 File Offset: 0x000634C3
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.AppendByteArray(this.m_serializedParms);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x000652DC File Offset: 0x000634DC
		internal TestNetwork0Request(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.TestNetwork0, packetContent)
		{
			this.m_serializedParms = base.Packet.ExtractByteArray();
			this.Parms = TestNetworkParms.FromBytes(this.m_serializedParms);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00065310 File Offset: 0x00063510
		public void Execute()
		{
			if (this.Parms.TimeoutInSec != 0)
			{
				base.Channel.TcpChannel.WriteTimeoutInMs = 1000 * this.Parms.TimeoutInSec;
			}
			if (this.Parms.TransferSize <= 0)
			{
				this.Parms.TransferSize = 65536;
			}
			if (this.Parms.TransferCount <= 0L)
			{
				this.Parms.TransferCount = 1L;
			}
			if (this.Parms.ReplyCount <= 0L)
			{
				this.Parms.ReplyCount = 1L;
			}
			if (this.Parms.TcpWindowSize != 0)
			{
				base.Channel.TcpChannel.BufferSize = this.Parms.TcpWindowSize;
			}
			for (long num = 0L; num < this.Parms.ReplyCount; num += 1L)
			{
				new SeedDatabaseFileReply(base.Channel)
				{
					FileSize = (long)this.Parms.TransferSize * this.Parms.TransferCount,
					LastWriteUtc = DateTime.UtcNow
				}.Send();
				byte[] buf = new byte[this.Parms.TransferSize];
				int num2 = 0;
				while ((long)num2 < this.Parms.TransferCount)
				{
					base.Channel.Write(buf, 0, this.Parms.TransferSize);
					num2++;
				}
			}
		}

		// Token: 0x040009CD RID: 2509
		private byte[] m_serializedParms;
	}
}
