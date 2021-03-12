using System;
using System.IO;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200013B RID: 315
	internal class SeedPageReaderRollLogFileRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x00034CFC File Offset: 0x00032EFC
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_databaseName);
			base.Packet.Append(this.m_databasePath);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00034D26 File Offset: 0x00032F26
		internal SeedPageReaderRollLogFileRequest(NetworkChannel channel, Guid databaseGuid, string databaseName, string databasePath) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileRequest, databaseGuid)
		{
			this.m_databaseName = databaseName;
			this.m_databasePath = databasePath;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00034D44 File Offset: 0x00032F44
		internal SeedPageReaderRollLogFileRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderRollLogFileRequest, packetContent)
		{
			this.m_databaseName = base.Packet.ExtractString();
			this.m_databasePath = base.Packet.ExtractString();
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00034D78 File Offset: 0x00032F78
		public override void Execute()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedPageReaderRollLogFileRequest: databaseGuid ({0}).", base.DatabaseGuid);
			bool flag = false;
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			try
			{
				SeederPageReaderServerContext seederPageReaderServerContext = base.Channel.GetSeederPageReaderServerContext(this.m_databaseName, this.m_databasePath);
				replayStopwatch.Start();
				seederPageReaderServerContext.DatabaseReader.ForceNewLog();
				SeedPageReaderRollLogFileReply seedPageReaderRollLogFileReply = new SeedPageReaderRollLogFileReply(base.Channel);
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedPageReaderRollLogFileRequest. Sending the response for {0}.", base.DatabaseGuid);
				seedPageReaderRollLogFileReply.Send();
				flag = true;
			}
			catch (TransientException ex)
			{
				base.Channel.SendException(ex);
			}
			catch (IOException ex2)
			{
				base.Channel.SendException(ex2);
			}
			finally
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, string>((long)this.GetHashCode(), "SeedPageReaderRollLogFile finished rolling a log file after {0} sec. Operation successful: {1}", replayStopwatch.ElapsedMilliseconds / 1000L, flag.ToString());
			}
		}

		// Token: 0x04000523 RID: 1315
		private string m_databaseName;

		// Token: 0x04000524 RID: 1316
		private string m_databasePath;
	}
}
