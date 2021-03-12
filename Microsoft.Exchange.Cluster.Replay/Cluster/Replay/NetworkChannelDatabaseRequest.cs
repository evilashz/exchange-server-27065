using System;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000134 RID: 308
	internal abstract class NetworkChannelDatabaseRequest : NetworkChannelMessage, INetworkChannelRequest
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x000342EB File Offset: 0x000324EB
		internal Guid DatabaseGuid
		{
			get
			{
				return this.m_dbGuid;
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000342F3 File Offset: 0x000324F3
		internal NetworkChannelDatabaseRequest(NetworkChannel channel, NetworkChannelMessage.MessageType msgType, Guid dbGuid) : base(channel, msgType)
		{
			this.m_dbGuid = dbGuid;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00034304 File Offset: 0x00032504
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_dbGuid);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00034320 File Offset: 0x00032520
		protected NetworkChannelDatabaseRequest(NetworkChannel channel, NetworkChannelMessage.MessageType msgType, byte[] packetContent) : base(channel, msgType, packetContent)
		{
			byte[] b = base.Packet.ExtractBytes(16);
			this.m_dbGuid = new Guid(b);
		}

		// Token: 0x06000BAF RID: 2991
		public abstract void Execute();

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00034350 File Offset: 0x00032550
		public void LinkWithMonitoredDatabase(MonitoredDatabase curDatabase)
		{
			if (curDatabase.IsPassiveCopy)
			{
				bool flag = false;
				NetworkChannelMessage.MessageType type = base.Type;
				if (type <= NetworkChannelMessage.MessageType.ProgressCiFileRequest)
				{
					if (type <= NetworkChannelMessage.MessageType.SeedLogCopyRequest)
					{
						if (type != NetworkChannelMessage.MessageType.CancelCiFileRequest && type != NetworkChannelMessage.MessageType.SeedLogCopyRequest)
						{
							goto IL_7C;
						}
					}
					else if (type != NetworkChannelMessage.MessageType.PassiveDatabaseFileRequest && type != NetworkChannelMessage.MessageType.ProgressCiFileRequest)
					{
						goto IL_7C;
					}
				}
				else if (type <= NetworkChannelMessage.MessageType.SeedCiFileRequest2)
				{
					if (type != NetworkChannelMessage.MessageType.SeedCiFileRequest && type != NetworkChannelMessage.MessageType.SeedCiFileRequest2)
					{
						goto IL_7C;
					}
				}
				else if (type != NetworkChannelMessage.MessageType.CancelCiFileReply && type != NetworkChannelMessage.MessageType.ProgressCiFileReply && type != NetworkChannelMessage.MessageType.SeedCiFileReply)
				{
					goto IL_7C;
				}
				flag = true;
				IL_7C:
				if (!flag)
				{
					ExTraceGlobals.NetworkChannelTracer.TraceError<NetworkChannelMessage.MessageType, string, string>((long)this.GetHashCode(), "LinkWithMonitoredDatabase rejecting request {0} from {1} for database {2}", base.Type, base.Channel.PartnerNodeName, curDatabase.DatabaseName);
					Exception ex = new SourceDatabaseNotFoundException(this.DatabaseGuid, Environment.MachineName);
					base.Channel.SendException(ex);
					throw ex;
				}
			}
			if (base.Channel.MonitoredDatabase != curDatabase)
			{
				if (base.Channel.MonitoredDatabase == null)
				{
					base.Channel.MonitoredDatabase = curDatabase;
					return;
				}
				if (curDatabase.DatabaseGuid != this.DatabaseGuid)
				{
					ExTraceGlobals.NetworkChannelTracer.TraceError<Guid, string>(0L, "Unexpected dbguid: got: {0}, expected:{1}", this.DatabaseGuid, curDatabase.Identity);
					base.Channel.ThrowUnexpectedMessage(this);
				}
				base.Channel.TraceDebug("Source instance must have restarted. Relinking to database {0}", new object[]
				{
					curDatabase.DatabaseName
				});
				base.Channel.MonitoredDatabase = curDatabase;
			}
		}

		// Token: 0x04000512 RID: 1298
		private Guid m_dbGuid;
	}
}
