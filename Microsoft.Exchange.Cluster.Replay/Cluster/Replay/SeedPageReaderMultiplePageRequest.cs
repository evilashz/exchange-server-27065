using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000135 RID: 309
	internal class SeedPageReaderMultiplePageRequest : NetworkChannelDatabaseRequest
	{
		// Token: 0x06000BB1 RID: 2993 RVA: 0x000344BC File Offset: 0x000326BC
		protected override void Serialize()
		{
			base.Serialize();
			base.Packet.Append(this.m_databaseName);
			base.Packet.Append(this.m_databasePath);
			base.Packet.Append(this.m_numPages);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000344F7 File Offset: 0x000326F7
		internal SeedPageReaderMultiplePageRequest(NetworkChannel channel, Guid databaseGuid, string databaseName, string databasePath, long numPages) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderMultiplePageRequest, databaseGuid)
		{
			this.m_databaseName = databaseName;
			this.m_databasePath = databasePath;
			this.m_numPages = numPages;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00034520 File Offset: 0x00032720
		internal SeedPageReaderMultiplePageRequest(NetworkChannel channel, byte[] packetContent) : base(channel, NetworkChannelMessage.MessageType.SeedPageReaderMultiplePageRequest, packetContent)
		{
			this.m_databaseName = base.Packet.ExtractString();
			this.m_databasePath = base.Packet.ExtractString();
			this.m_numPages = base.Packet.ExtractInt64();
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00034570 File Offset: 0x00032770
		public override void Execute()
		{
			SeedPageReaderMultiplePageRequest.Tracer.TraceDebug<Guid, long>((long)this.GetHashCode(), "SeedPageReaderMultiplePageRequest: databaseGuid ({0}), numPages ({1}).", base.DatabaseGuid, this.m_numPages);
			bool flag = false;
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			Exception ex = null;
			int i = (int)this.m_numPages;
			byte[] array = new byte[4];
			try
			{
				SeederPageReaderServerContext seederPageReaderServerContext = base.Channel.GetSeederPageReaderServerContext(this.m_databaseName, this.m_databasePath);
				replayStopwatch.Start();
				while (i > 0)
				{
					base.Channel.Read(array, 0, 4);
					i--;
					uint num = BitConverter.ToUInt32(array, 0);
					long lowGeneration;
					long highGeneration;
					byte[] pageBytes = seederPageReaderServerContext.DatabaseReader.ReadOnePage((long)((ulong)num), out lowGeneration, out highGeneration);
					new SeedPageReaderSinglePageReply(base.Channel)
					{
						PageNumber = (long)((ulong)num),
						LowGeneration = lowGeneration,
						HighGeneration = highGeneration,
						PageBytes = pageBytes
					}.Send();
				}
				flag = true;
			}
			catch (JetErrorFileIOBeyondEOFException ex2)
			{
				ex = ex2;
			}
			catch (FailedToReadDatabasePage failedToReadDatabasePage)
			{
				ex = failedToReadDatabasePage;
			}
			catch (NetworkTransportException ex3)
			{
				ex = ex3;
			}
			finally
			{
				SeedPageReaderMultiplePageRequest.Tracer.TraceDebug<long, long, string>((long)this.GetHashCode(), "SeedPageReader finished reading and sending {0} pages after {1} sec. Operation successful: {2}", this.m_numPages, replayStopwatch.ElapsedMilliseconds / 1000L, flag.ToString());
			}
			if (!flag)
			{
				SeedPageReaderMultiplePageRequest.Tracer.TraceError<Exception>((long)this.GetHashCode(), "SeedPageReaderMultiplePageRequest.Execute failed: {0}", ex);
				if (ex is NetworkTransportException)
				{
					base.Channel.KeepAlive = false;
					return;
				}
				base.Channel.SendException(ex);
				while (i > 0)
				{
					base.Channel.Read(array, 0, 4);
					i--;
				}
			}
		}

		// Token: 0x04000513 RID: 1299
		private static readonly Trace Tracer = ExTraceGlobals.IncrementalReseederTracer;

		// Token: 0x04000514 RID: 1300
		private readonly long m_numPages;

		// Token: 0x04000515 RID: 1301
		private readonly string m_databaseName;

		// Token: 0x04000516 RID: 1302
		private readonly string m_databasePath;
	}
}
