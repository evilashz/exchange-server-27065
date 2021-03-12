using System;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200034A RID: 842
	internal class CopyLogRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x06002259 RID: 8793 RVA: 0x000A0376 File Offset: 0x0009E576
		internal CopyLogRequest(NetworkChannel channel, Guid dbGuid, long logNum) : base(channel, NetworkChannelMessage.MessageType.CopyLogRequest, dbGuid)
		{
			this.m_logGeneration = logNum;
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000A038C File Offset: 0x0009E58C
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_logGeneration);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000A03A5 File Offset: 0x0009E5A5
		internal CopyLogRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.CopyLogRequest, packetContent)
		{
			this.m_logGeneration = base.Packet.ExtractInt64();
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000A03C5 File Offset: 0x0009E5C5
		public override void Execute()
		{
			ExTraceGlobals.LogCopyServerTracer.TraceDebug<long>((long)this.GetHashCode(), "Requesting log 0x{0:x}, d({0})", this.m_logGeneration);
			base.Channel.MonitoredDatabase.TrySendLogWithStandardHandling(this.m_logGeneration, base.Channel);
		}

		// Token: 0x04000E36 RID: 3638
		private long m_logGeneration;
	}
}
