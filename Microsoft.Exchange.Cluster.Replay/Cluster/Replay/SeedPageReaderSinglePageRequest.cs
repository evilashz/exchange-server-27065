using System;
using System.IO;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000137 RID: 311
	internal class SeedPageReaderSinglePageRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x000347D6 File Offset: 0x000329D6
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_databaseName);
			base.Packet.Append(this.m_databasePath);
			base.Packet.Append(this.m_pageno);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00034811 File Offset: 0x00032A11
		internal SeedPageReaderSinglePageRequest(NetworkChannel channel, Guid databaseGuid, string databaseName, string databasePath, uint pageno) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderSinglePageRequest, databaseGuid)
		{
			this.m_databaseName = databaseName;
			this.m_databasePath = databasePath;
			this.m_pageno = pageno;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00034838 File Offset: 0x00032A38
		internal SeedPageReaderSinglePageRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderSinglePageRequest, packetContent)
		{
			this.m_databaseName = base.Packet.ExtractString();
			this.m_databasePath = base.Packet.ExtractString();
			this.m_pageno = base.Packet.ExtractUInt32();
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00034888 File Offset: 0x00032A88
		public override void Execute()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug<Guid, uint>((long)this.GetHashCode(), "SeedPageReaderSinglePageRequest: databaseGuid ({0}), pageno ({1}).", base.DatabaseGuid, this.m_pageno);
			bool flag = false;
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			try
			{
				SeederPageReaderServerContext seederPageReaderServerContext = base.Channel.GetSeederPageReaderServerContext(this.m_databaseName, this.m_databasePath);
				replayStopwatch.Start();
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<uint, Guid>((long)this.GetHashCode(), "SeedPageReaderSinglePageRequest. Reading page {0} from {1}.", this.m_pageno, base.DatabaseGuid);
				long lowGeneration;
				long highGeneration;
				byte[] pageBytes = seederPageReaderServerContext.DatabaseReader.ReadOnePage((long)((ulong)this.m_pageno), out lowGeneration, out highGeneration);
				SeedPageReaderSinglePageReply seedPageReaderSinglePageReply = new SeedPageReaderSinglePageReply(base.Channel);
				seedPageReaderSinglePageReply.PageNumber = (long)((ulong)this.m_pageno);
				seedPageReaderSinglePageReply.LowGeneration = lowGeneration;
				seedPageReaderSinglePageReply.HighGeneration = highGeneration;
				seedPageReaderSinglePageReply.PageBytes = pageBytes;
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<Guid>((long)this.GetHashCode(), "SeedPageReaderSinglePageRequest. Sending the data for {0}.", base.DatabaseGuid);
				seedPageReaderSinglePageReply.Send();
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
				ExTraceGlobals.IncrementalReseederTracer.TraceDebug<uint, long, string>((long)this.GetHashCode(), "SeedPageReader finished reading and sending page {0} after {1} sec. Operation successful: {2}", this.m_pageno, replayStopwatch.ElapsedMilliseconds / 1000L, flag.ToString());
			}
		}

		// Token: 0x04000519 RID: 1305
		private uint m_pageno;

		// Token: 0x0400051A RID: 1306
		private string m_databaseName;

		// Token: 0x0400051B RID: 1307
		private string m_databasePath;
	}
}
