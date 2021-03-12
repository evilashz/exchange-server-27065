using System;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000346 RID: 838
	internal class LogCopyClient : LogSource
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x0009F94B File Offset: 0x0009DB4B
		public override string SourcePath
		{
			get
			{
				return "TCP:" + this.m_sourceAddr + "/" + this.m_config.DatabaseName;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x0009F96D File Offset: 0x0009DB6D
		internal Guid DatabaseGuid
		{
			get
			{
				return this.m_config.IdentityGuid;
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0009F97A File Offset: 0x0009DB7A
		public LogCopyClient(IReplayConfiguration config, IPerfmonCounters perfmonCounters, NetworkPath initialNetworkPath, int timeoutMs)
		{
			this.m_config = config;
			this.m_perfmonCounters = perfmonCounters;
			this.m_defaultTimeoutInMs = timeoutMs;
			this.m_sourceAddr = config.SourceMachine;
			this.m_initialNetworkPath = initialNetworkPath;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x0009F9B8 File Offset: 0x0009DBB8
		public override void Cancel()
		{
			ExTraceGlobals.LogCopyClientTracer.TraceDebug((long)this.GetHashCode(), "Cancelling use of this channel.");
			lock (this)
			{
				this.m_cancelling = true;
				if (this.m_channel != null)
				{
					this.m_channel.Abort();
				}
			}
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0009FA20 File Offset: 0x0009DC20
		public override void Close()
		{
			if (this.m_channel != null)
			{
				this.m_channel.Close();
			}
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0009FA38 File Offset: 0x0009DC38
		public override void SetTimeoutInMsec(int msTimeout)
		{
			this.m_defaultTimeoutInMs = msTimeout;
			if (this.m_channel != null)
			{
				TcpChannel tcpChannel = this.m_channel.TcpChannel;
				tcpChannel.ReadTimeoutInMs = msTimeout;
				tcpChannel.WriteTimeoutInMs = msTimeout;
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0009FA70 File Offset: 0x0009DC70
		public bool TryGetChannelLock()
		{
			return Monitor.TryEnter(this.m_channelLock);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0009FA8A File Offset: 0x0009DC8A
		public void GetChannelLock()
		{
			Monitor.Enter(this.m_channelLock);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0009FA97 File Offset: 0x0009DC97
		public void ReleaseChannelLock()
		{
			Monitor.Exit(this.m_channelLock);
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x0009FAA4 File Offset: 0x0009DCA4
		public NetworkChannel Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0009FAAC File Offset: 0x0009DCAC
		public NetworkChannel OpenChannel()
		{
			string networkName = null;
			NetworkPath.ConnectionPurpose purpose = NetworkPath.ConnectionPurpose.LogCopy;
			NetworkPath netPath;
			if (this.m_initialNetworkPath == null)
			{
				netPath = NetworkManager.ChooseNetworkPath(this.m_sourceAddr, networkName, purpose);
			}
			else
			{
				netPath = this.m_initialNetworkPath;
				this.m_initialNetworkPath = null;
			}
			this.m_channel = NetworkChannel.Connect(netPath, base.DefaultTimeoutInMs, false);
			return this.m_channel;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x0009FAFC File Offset: 0x0009DCFC
		private void OpenChannelIfFirstRequest()
		{
			if (this.m_channel == null)
			{
				this.OpenChannel();
			}
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0009FB10 File Offset: 0x0009DD10
		private void DiscardChannel()
		{
			if (this.m_channel != null)
			{
				NetworkChannel channel = this.m_channel;
				this.m_channel = null;
				channel.Close();
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0009FB39 File Offset: 0x0009DD39
		private void SendMessage(NetworkChannelMessage msg)
		{
			msg.Send();
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0009FB44 File Offset: 0x0009DD44
		private NetworkChannelMessage GetReply()
		{
			return this.m_channel.GetMessage();
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0009FB60 File Offset: 0x0009DD60
		public override long QueryLogRange()
		{
			long num = 0L;
			bool flag = false;
			int num2 = 0;
			TcpChannel tcpChannel = null;
			this.GetChannelLock();
			try
			{
				this.OpenChannelIfFirstRequest();
				tcpChannel = this.m_channel.TcpChannel;
				if (tcpChannel.ReadTimeoutInMs < RegistryParameters.QueryLogRangeTimeoutInMsec)
				{
					num2 = tcpChannel.ReadTimeoutInMs;
					tcpChannel.ReadTimeoutInMs = RegistryParameters.QueryLogRangeTimeoutInMsec;
				}
				QueryLogRangeRequest msg = new QueryLogRangeRequest(this.m_channel, this.DatabaseGuid);
				this.SendMessage(msg);
				NetworkChannelMessage reply = this.GetReply();
				QueryLogRangeReply queryLogRangeReply = reply as QueryLogRangeReply;
				if (queryLogRangeReply == null)
				{
					this.m_channel.ThrowUnexpectedMessage(reply);
				}
				this.m_endOfLog.SetValue(queryLogRangeReply.EndOfLogGeneration, new DateTime?(queryLogRangeReply.EndOfLogUtc));
				num = queryLogRangeReply.FirstAvailableGeneration;
				ExTraceGlobals.LogCopyClientTracer.TraceDebug<long, long>((long)this.GetHashCode(), "LogCopyClient:QueryLogRange: 0x{0:x} .. {1:x}", num, this.m_endOfLog.Generation);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.DiscardChannel();
				}
				else if (num2 > 0)
				{
					tcpChannel.ReadTimeoutInMs = num2;
				}
				this.ReleaseChannelLock();
			}
			return num;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0009FC80 File Offset: 0x0009DE80
		internal static void QueryLogRange(Guid dbGuid, NetworkChannel ch, out long minGen, out long maxGen, out DateTime maxGenUtc)
		{
			minGen = 0L;
			maxGen = 0L;
			maxGenUtc = DateTime.UtcNow;
			bool flag = false;
			int timeoutToRestore = 0;
			TcpChannel tcpChannel = null;
			try
			{
				tcpChannel = ch.TcpChannel;
				if (tcpChannel.ReadTimeoutInMs < RegistryParameters.QueryLogRangeTimeoutInMsec)
				{
					timeoutToRestore = tcpChannel.ReadTimeoutInMs;
					tcpChannel.ReadTimeoutInMs = RegistryParameters.QueryLogRangeTimeoutInMsec;
				}
				QueryLogRangeRequest queryLogRangeRequest = new QueryLogRangeRequest(ch, dbGuid);
				queryLogRangeRequest.Send();
				NetworkChannelMessage message = ch.GetMessage();
				QueryLogRangeReply queryLogRangeReply = message as QueryLogRangeReply;
				if (queryLogRangeReply == null)
				{
					ch.ThrowUnexpectedMessage(message);
				}
				minGen = queryLogRangeReply.FirstAvailableGeneration;
				maxGen = queryLogRangeReply.EndOfLogGeneration;
				maxGenUtc = queryLogRangeReply.EndOfLogUtc;
				ExTraceGlobals.LogCopyClientTracer.TraceDebug<long, long>((long)ch.GetHashCode(), "LogCopyClient:TryQueryLogRange: 0x{0:x} .. {1:x}", minGen, maxGen);
				flag = true;
			}
			finally
			{
				if (timeoutToRestore > 0)
				{
					if (!flag)
					{
						NetworkChannel.RunNetworkFunction(delegate
						{
							tcpChannel.ReadTimeoutInMs = timeoutToRestore;
						});
					}
					else
					{
						tcpChannel.ReadTimeoutInMs = timeoutToRestore;
					}
				}
			}
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0009FDB0 File Offset: 0x0009DFB0
		public override long QueryEndOfLog()
		{
			long num = -1L;
			this.GetChannelLock();
			try
			{
				this.OpenChannelIfFirstRequest();
				NotifyEndOfLogRequest msg = new NotifyEndOfLogRequest(this.m_channel, this.DatabaseGuid, 0L);
				this.SendMessage(msg);
				NetworkChannelMessage reply = this.GetReply();
				NotifyEndOfLogReply notifyEndOfLogReply = reply as NotifyEndOfLogReply;
				if (notifyEndOfLogReply == null)
				{
					this.m_channel.ThrowUnexpectedMessage(reply);
				}
				this.m_endOfLog.SetValue(notifyEndOfLogReply.EndOfLogGeneration, new DateTime?(notifyEndOfLogReply.EndOfLogUtc));
				num = notifyEndOfLogReply.EndOfLogGeneration;
			}
			finally
			{
				this.ReleaseChannelLock();
			}
			ExTraceGlobals.LogCopyClientTracer.TraceDebug<long>((long)this.GetHashCode(), "LogCopyClient:QueryEndOfLog: 0x{0:x}", num);
			return num;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x0009FE58 File Offset: 0x0009E058
		public override void CopyLog(long fromNumber, string destinationFileName, out DateTime writeTimeUtc)
		{
			writeTimeUtc = (DateTime)ExDateTime.UtcNow;
			ExTraceGlobals.LogCopyClientTracer.TraceDebug<string>((long)this.GetHashCode(), "CopyLog {0} starting", destinationFileName);
			base.AllocateBuffer();
			this.GetChannelLock();
			try
			{
				this.OpenChannelIfFirstRequest();
				CopyLogRequest msg = new CopyLogRequest(this.m_channel, this.DatabaseGuid, fromNumber);
				this.SendMessage(msg);
				ReplayStopwatch replayStopwatch = new ReplayStopwatch();
				replayStopwatch.Start();
				NetworkChannelMessage reply = this.GetReply();
				CopyLogReply copyLogReply = reply as CopyLogReply;
				if (copyLogReply == null)
				{
					this.m_channel.ThrowUnexpectedMessage(reply);
				}
				long elapsedMilliseconds = replayStopwatch.ElapsedMilliseconds;
				ExTraceGlobals.LogCopyClientTracer.TraceDebug<long>((long)this.GetHashCode(), "Log Copy Response took: {0}ms", elapsedMilliseconds);
				this.m_endOfLog.SetValue(copyLogReply.EndOfLogGeneration, new DateTime?(copyLogReply.EndOfLogUtc));
				writeTimeUtc = copyLogReply.LastWriteUtc;
				CheckSummer summer = null;
				if (this.m_channel.ChecksumDataTransfer)
				{
					summer = new CheckSummer();
				}
				copyLogReply.ReceiveFile(destinationFileName, null, summer);
				elapsedMilliseconds = replayStopwatch.ElapsedMilliseconds;
				ExTraceGlobals.LogCopyClientTracer.TraceDebug<long>((long)this.GetHashCode(), "Transmit/Decomp took: {0}ms", elapsedMilliseconds);
				base.RecordThruput(copyLogReply.FileSize);
				ExchangeNetworkPerfmonCounters perfCounters = this.m_channel.PerfCounters;
				if (perfCounters != null)
				{
					perfCounters.RecordLogCopyThruputReceived(copyLogReply.FileSize);
				}
				replayStopwatch.Stop();
				ExTraceGlobals.LogCopyClientTracer.TraceDebug((long)this.GetHashCode(), "{0}: LogCopy success: {1} for {2} after {3}ms", new object[]
				{
					ExDateTime.Now,
					replayStopwatch.ToString(),
					destinationFileName,
					replayStopwatch.ElapsedMilliseconds
				});
			}
			finally
			{
				this.ReleaseChannelLock();
			}
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000A000C File Offset: 0x0009E20C
		internal static void CopyLog(Guid dbGuid, NetworkChannel ch, long logGen, string destinationFileName)
		{
			ExTraceGlobals.LogCopyClientTracer.TraceDebug<string>((long)ch.GetHashCode(), "static CopyLog {0} starting", destinationFileName);
			CopyLogRequest copyLogRequest = new CopyLogRequest(ch, dbGuid, logGen);
			copyLogRequest.Send();
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			replayStopwatch.Start();
			NetworkChannelMessage message = ch.GetMessage();
			CopyLogReply copyLogReply = message as CopyLogReply;
			if (copyLogReply == null)
			{
				ch.ThrowUnexpectedMessage(message);
			}
			long elapsedMilliseconds = replayStopwatch.ElapsedMilliseconds;
			ExTraceGlobals.LogCopyClientTracer.TraceDebug<long>((long)ch.GetHashCode(), "Log Copy Response took: {0}ms", elapsedMilliseconds);
			CheckSummer summer = null;
			if (ch.ChecksumDataTransfer)
			{
				summer = new CheckSummer();
			}
			copyLogReply.ReceiveFile(destinationFileName, null, summer);
			elapsedMilliseconds = replayStopwatch.ElapsedMilliseconds;
			ExTraceGlobals.LogCopyClientTracer.TraceDebug<long>((long)ch.GetHashCode(), "Transmit/Decomp took: {0}ms", elapsedMilliseconds);
			ExchangeNetworkPerfmonCounters perfCounters = ch.PerfCounters;
			if (perfCounters != null)
			{
				perfCounters.RecordLogCopyThruputReceived(copyLogReply.FileSize);
			}
			replayStopwatch.Stop();
			ExTraceGlobals.LogCopyClientTracer.TraceDebug((long)ch.GetHashCode(), "{0}: LogCopy success: {1} for {2} after {3}ms", new object[]
			{
				ExDateTime.Now,
				replayStopwatch.ToString(),
				destinationFileName,
				replayStopwatch.ElapsedMilliseconds
			});
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000A012C File Offset: 0x0009E32C
		public override long GetE00Generation()
		{
			long num = 0L;
			this.GetChannelLock();
			try
			{
				this.OpenChannelIfFirstRequest();
				GetE00GenerationRequest msg = new GetE00GenerationRequest(this.m_channel, this.DatabaseGuid);
				this.SendMessage(msg);
				NetworkChannelMessage reply = this.GetReply();
				GetE00GenerationReply getE00GenerationReply = reply as GetE00GenerationReply;
				if (getE00GenerationReply == null)
				{
					this.m_channel.ThrowUnexpectedMessage(reply);
				}
				num = getE00GenerationReply.LogGeneration;
				ExTraceGlobals.LogCopyClientTracer.TraceDebug<long>((long)this.GetHashCode(), "LogCopyClient:GetE00Gen: 0x{0:x}", num);
			}
			finally
			{
				this.ReleaseChannelLock();
			}
			return num;
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000A01B8 File Offset: 0x0009E3B8
		public override bool LogExists(long logNum)
		{
			bool flag = false;
			this.GetChannelLock();
			try
			{
				this.OpenChannelIfFirstRequest();
				TestLogExistenceRequest msg = new TestLogExistenceRequest(this.m_channel, this.DatabaseGuid, logNum);
				this.SendMessage(msg);
				NetworkChannelMessage reply = this.GetReply();
				TestLogExistenceReply testLogExistenceReply = reply as TestLogExistenceReply;
				if (testLogExistenceReply == null)
				{
					this.m_channel.ThrowUnexpectedMessage(reply);
				}
				flag = testLogExistenceReply.LogExists;
				ExTraceGlobals.LogCopyClientTracer.TraceDebug<long, bool>((long)this.GetHashCode(), "LogCopyClient:LogExists(0x{0:x})={1}", logNum, flag);
			}
			finally
			{
				this.ReleaseChannelLock();
			}
			return flag;
		}

		// Token: 0x04000E2F RID: 3631
		private object m_channelLock = new object();

		// Token: 0x04000E30 RID: 3632
		private NetworkChannel m_channel;

		// Token: 0x04000E31 RID: 3633
		private string m_sourceAddr;

		// Token: 0x04000E32 RID: 3634
		private NetworkPath m_initialNetworkPath;
	}
}
