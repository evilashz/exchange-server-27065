using System;
using System.IO;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000139 RID: 313
	internal class SeedPageReaderPageSizeRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x06000BC8 RID: 3016 RVA: 0x00034B1F File Offset: 0x00032D1F
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_databaseName);
			base.Packet.Append(this.m_databasePath);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00034B49 File Offset: 0x00032D49
		internal SeedPageReaderPageSizeRequest(NetworkChannel channel, Guid databaseGuid, string databaseName, string databasePath) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderPageSizeRequest, databaseGuid)
		{
			this.m_databaseName = databaseName;
			this.m_databasePath = databasePath;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00034B67 File Offset: 0x00032D67
		internal SeedPageReaderPageSizeRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderPageSizeRequest, packetContent)
		{
			this.m_databaseName = base.Packet.ExtractString();
			this.m_databasePath = base.Packet.ExtractString();
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00034B98 File Offset: 0x00032D98
		public override void Execute()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedPageReaderPageSizeRequest: databaseGuid ({0}).", base.DatabaseGuid);
			bool flag = false;
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			try
			{
				SeederPageReaderServerContext seederPageReaderServerContext = base.Channel.GetSeederPageReaderServerContext(this.m_databaseName, this.m_databasePath);
				replayStopwatch.Start();
				long pageSize = seederPageReaderServerContext.DatabaseReader.PageSize;
				SeedPageReaderPageSizeReply seedPageReaderPageSizeReply = new SeedPageReaderPageSizeReply(base.Channel);
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedPageReaderPageSizeRequest. Sending the response for {0}.", base.DatabaseGuid);
				seedPageReaderPageSizeReply.PageSize = pageSize;
				seedPageReaderPageSizeReply.Send();
				flag = true;
			}
			catch (IOException ex)
			{
				base.Channel.SendException(ex);
			}
			catch (TransientException ex2)
			{
				base.Channel.SendException(ex2);
			}
			finally
			{
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<long, string>((long)this.GetHashCode(), "SeedPageReaderPageRequest finished fetching the page size after {0} msec. Operation successful: {1}", replayStopwatch.ElapsedMilliseconds, flag.ToString());
			}
		}

		// Token: 0x04000520 RID: 1312
		private string m_databaseName;

		// Token: 0x04000521 RID: 1313
		private string m_databasePath;
	}
}
