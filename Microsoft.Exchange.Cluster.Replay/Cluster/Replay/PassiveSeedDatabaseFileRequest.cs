using System;
using System.IO;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002A3 RID: 675
	internal class PassiveSeedDatabaseFileRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x06001A59 RID: 6745 RVA: 0x0006F1ED File Offset: 0x0006D3ED
		internal PassiveSeedDatabaseFileRequest(NetworkChannel channel, Guid dbGuid, Guid targetServerGuid) : base(channel, NetworkChannelMessage.MessageType.PassiveDatabaseFileRequest, dbGuid)
		{
			this.m_serverGuid = targetServerGuid;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0006F203 File Offset: 0x0006D403
		internal PassiveSeedDatabaseFileRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.PassiveDatabaseFileRequest, packetContent)
		{
			this.m_serverGuid = base.Packet.ExtractGuid();
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0006F224 File Offset: 0x0006D424
		public override void Execute()
		{
			string databaseName = base.Channel.MonitoredDatabase.DatabaseName;
			string partnerNodeName = base.Channel.PartnerNodeName;
			ExTraceGlobals.SeederServerTracer.TraceDebug<Guid>((long)this.GetHashCode(), "PassiveSeedDatabaseFileRequest: databaseGuid ({0}).", base.DatabaseGuid);
			Exception ex = null;
			try
			{
				SeederServerContext seederServerContext = base.Channel.CreateSeederServerContext(base.DatabaseGuid, new Guid?(this.m_serverGuid), SeedType.Database);
				seederServerContext.SendDatabaseFile();
			}
			catch (OperationCanceledException ex2)
			{
				ex = ex2;
			}
			catch (SeedingChannelIsClosedException ex3)
			{
				ex = ex3;
			}
			catch (SeedingSourceReplicaInstanceNotFoundException ex4)
			{
				ex = ex4;
			}
			catch (IOException ex5)
			{
				ex = ex5;
			}
			catch (FileIOonSourceException ex6)
			{
				ex = ex6;
			}
			catch (NetworkTransportException ex7)
			{
				ex = ex7;
			}
			catch (MapiPermanentException ex8)
			{
				ex = ex8;
			}
			catch (TaskServerException ex9)
			{
				ex = ex9;
			}
			catch (TransientException ex10)
			{
				ex = ex10;
			}
			if (ex != null)
			{
				ExTraceGlobals.SeederServerTracer.TraceError<string, Exception>((long)this.GetHashCode(), "PassiveSeedDatabaseFileRequest({0}): Failed: {1}", databaseName, ex);
				ReplayCrimsonEvents.PassiveSeedSourceFailedToSendEDB.Log<string, string, string, string>(databaseName, Environment.MachineName, partnerNodeName, ex.ToString());
				if (!(ex is NetworkTransportException))
				{
					base.Channel.SendException(ex);
				}
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0006F388 File Offset: 0x0006D588
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_serverGuid);
		}

		// Token: 0x04000A8E RID: 2702
		private Guid m_serverGuid;
	}
}
