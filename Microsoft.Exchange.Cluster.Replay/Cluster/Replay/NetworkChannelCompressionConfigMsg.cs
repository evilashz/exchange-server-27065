using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200025C RID: 604
	internal class NetworkChannelCompressionConfigMsg : NetworkChannelMessage, INetworkChannelRequest
	{
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00061CB4 File Offset: 0x0005FEB4
		// (set) Token: 0x0600179C RID: 6044 RVA: 0x00061CBC File Offset: 0x0005FEBC
		public NetworkChannelCompressionConfigMsg.MessagePurpose Purpose { get; set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x00061CC5 File Offset: 0x0005FEC5
		// (set) Token: 0x0600179E RID: 6046 RVA: 0x00061CCD File Offset: 0x0005FECD
		public string ConfigXml { get; set; }

		// Token: 0x0600179F RID: 6047 RVA: 0x00061CD6 File Offset: 0x0005FED6
		internal NetworkChannelCompressionConfigMsg(NetworkChannel channel, NetworkChannelCompressionConfigMsg.MessagePurpose purpose, string configXml) : base(channel, NetworkChannelMessage.MessageType.CompressionConfig)
		{
			this.Purpose = purpose;
			this.ConfigXml = configXml;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00061CF2 File Offset: 0x0005FEF2
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append((uint)this.Purpose);
			base.Packet.Append(this.ConfigXml);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00061D1C File Offset: 0x0005FF1C
		protected internal NetworkChannelCompressionConfigMsg(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.CompressionConfig, packetContent)
		{
			this.Purpose = (NetworkChannelCompressionConfigMsg.MessagePurpose)base.Packet.ExtractUInt32();
			this.ConfigXml = base.Packet.ExtractString();
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00061D50 File Offset: 0x0005FF50
		public void Execute()
		{
			Exception ex;
			CompressionConfig encoding = CompressionConfig.Deserialize(this.ConfigXml, out ex);
			string configXml;
			if (ex != null)
			{
				ReplayCrimsonEvents.InvalidCompressionConfigReceived.LogPeriodic<string, string, Exception>(base.Channel.PartnerNodeName, DiagCore.DefaultEventSuppressionInterval, base.Channel.PartnerNodeName, this.ConfigXml, ex);
				CompressionConfig obj = new CompressionConfig();
				configXml = SerializationUtil.ObjectToXml(obj);
			}
			else
			{
				configXml = this.ConfigXml;
			}
			NetworkChannelCompressionConfigMsg networkChannelCompressionConfigMsg = new NetworkChannelCompressionConfigMsg(base.Channel, NetworkChannelCompressionConfigMsg.MessagePurpose.DeclareEncoding, configXml);
			networkChannelCompressionConfigMsg.Send();
			base.Channel.SetEncoding(encoding);
			base.Channel.KeepAlive = true;
		}

		// Token: 0x0200025D RID: 605
		public enum MessagePurpose
		{
			// Token: 0x04000946 RID: 2374
			RequestEncoding = 1,
			// Token: 0x04000947 RID: 2375
			DeclareEncoding
		}
	}
}
