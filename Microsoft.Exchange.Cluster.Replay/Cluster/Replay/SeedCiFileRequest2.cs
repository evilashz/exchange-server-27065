using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000293 RID: 659
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SeedCiFileRequest2 : SeedCiFileRequest
	{
		// Token: 0x060019D9 RID: 6617 RVA: 0x0006C3EC File Offset: 0x0006A5EC
		internal SeedCiFileRequest2(NetworkChannel channel, Guid dbGuid, string endpoint, string reason) : base(channel, NetworkChannelMessage.MessageType.SeedCiFileRequest2, dbGuid, endpoint, reason)
		{
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0006C400 File Offset: 0x0006A600
		internal SeedCiFileRequest2(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedCiFileRequest2, packetContent)
		{
			string xml = base.Packet.ExtractString();
			SeedCiFileRequestPayload seedCiFileRequestPayload;
			Exception ex = DataContractSerializeHelper.DeserializeFromXmlString<SeedCiFileRequestPayload>(xml, out seedCiFileRequestPayload);
			if (ex != null)
			{
				ReplayCrimsonEvents.CISeedingSourceBeginFailed.Log<Guid, string, string, Exception>(base.DatabaseGuid, string.Empty, base.Channel.PartnerNodeName, ex);
				SeederServerContext.ProcessSourceSideException(ex, base.Channel);
				return;
			}
			this.endpoint = seedCiFileRequestPayload.Endpoint;
			this.reason = seedCiFileRequestPayload.Reason;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0006C478 File Offset: 0x0006A678
		protected override void SerializeAdditionalProperties()
		{
			SeedCiFileRequestPayload toSerialize = new SeedCiFileRequestPayload(this.endpoint, this.reason);
			string str;
			Exception ex = DataContractSerializeHelper.SerializeToXmlString(toSerialize, out str);
			if (ex != null)
			{
				throw new SeederServerException(ex.Message, ex);
			}
			base.Packet.Append(str);
		}
	}
}
