using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200025F RID: 607
	internal abstract class NetworkChannelFileTransferReply : NetworkChannelMessage
	{
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00061E35 File Offset: 0x00060035
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x00061E3D File Offset: 0x0006003D
		internal string DestinationFileName
		{
			get
			{
				return this.m_destFileName;
			}
			set
			{
				this.m_destFileName = value;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x00061E46 File Offset: 0x00060046
		// (set) Token: 0x060017AA RID: 6058 RVA: 0x00061E4E File Offset: 0x0006004E
		internal long FileSize
		{
			get
			{
				return this.m_fileSize;
			}
			set
			{
				this.m_fileSize = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x00061E57 File Offset: 0x00060057
		// (set) Token: 0x060017AC RID: 6060 RVA: 0x00061E5F File Offset: 0x0006005F
		internal DateTime LastWriteUtc
		{
			get
			{
				return this.m_lastWriteUtc;
			}
			set
			{
				this.m_lastWriteUtc = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x00061E68 File Offset: 0x00060068
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x00061E70 File Offset: 0x00060070
		internal NetworkChannel.DataEncodingScheme DataEncoding
		{
			get
			{
				return this.m_dataEncoding;
			}
			set
			{
				this.m_dataEncoding = value;
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00061E79 File Offset: 0x00060079
		internal NetworkChannelFileTransferReply(NetworkChannel channel, NetworkChannelMessage.MessageType msgType) : base(channel, msgType)
		{
			this.m_dataEncoding = NetworkChannel.DataEncodingScheme.Uncompressed;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00061E8A File Offset: 0x0006008A
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_fileSize);
			base.Packet.Append(this.m_lastWriteUtc);
			base.Packet.Append((uint)this.DataEncoding);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00061EC5 File Offset: 0x000600C5
		internal NetworkChannelFileTransferReply(NetworkChannel channel, NetworkChannelMessage.MessageType msgType, byte[] packetContent) : base(channel, msgType, packetContent)
		{
			this.FileSize = base.Packet.ExtractInt64();
			this.LastWriteUtc = base.Packet.ExtractDateTime();
			this.DataEncoding = (NetworkChannel.DataEncodingScheme)base.Packet.ExtractUInt32();
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00061F03 File Offset: 0x00060103
		internal void ReceiveFile(string fullDestinationFileName, IReplicaSeederCallback callback, IPerfmonCounters copyPerfCtrs, CheckSummer summer)
		{
			this.DestinationFileName = fullDestinationFileName;
			this.m_channel.ReceiveFile(this, callback, copyPerfCtrs, summer);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00061F1C File Offset: 0x0006011C
		internal void ReceiveFile(string fullDestinationFileName, IPerfmonCounters copyPerfCtrs, CheckSummer summer)
		{
			this.ReceiveFile(fullDestinationFileName, null, copyPerfCtrs, summer);
		}

		// Token: 0x04000949 RID: 2377
		private long m_fileSize;

		// Token: 0x0400094A RID: 2378
		private DateTime m_lastWriteUtc;

		// Token: 0x0400094B RID: 2379
		private NetworkChannel.DataEncodingScheme m_dataEncoding;

		// Token: 0x0400094C RID: 2380
		private string m_destFileName;
	}
}
