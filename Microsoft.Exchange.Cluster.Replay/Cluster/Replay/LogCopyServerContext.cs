using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Mapi;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000366 RID: 870
	internal class LogCopyServerContext
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x000A19FF File Offset: 0x0009FBFF
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LogCopyServerTracer;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x000A1A06 File Offset: 0x0009FC06
		private NetworkChannel Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x000A1A0E File Offset: 0x0009FC0E
		internal MonitoredDatabase Database
		{
			get
			{
				return this.m_database;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x000A1A16 File Offset: 0x0009FC16
		public long CurrentLogGeneration
		{
			get
			{
				return this.m_currentLogGeneration;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x000A1A1E File Offset: 0x0009FC1E
		private long MaxGenerationToSend
		{
			get
			{
				return this.m_currentLogCopyRequest.LastGeneration;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000A1A2B File Offset: 0x0009FC2B
		private bool ForAcll
		{
			get
			{
				return this.m_currentLogCopyRequest.ForAcll;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x000A1A38 File Offset: 0x0009FC38
		// (set) Token: 0x060022D1 RID: 8913 RVA: 0x000A1A40 File Offset: 0x0009FC40
		private SourceDatabasePerformanceCountersInstance SourceDatabasePerfCounters { get; set; }

		// Token: 0x060022D2 RID: 8914 RVA: 0x000A1A4C File Offset: 0x0009FC4C
		internal static void StartContinuousLogTransmission(NetworkChannel channel, ContinuousLogCopyRequest oldReq)
		{
			LogCopyServerContext logCopyServerContext = new LogCopyServerContext(channel, channel.MonitoredDatabase);
			logCopyServerContext.m_clientIsDownLevel = true;
			LogCopyServerContext.Tracer.TraceDebug<string, bool>((long)logCopyServerContext.GetHashCode(), "Passive({0}) is downlevel {1}}", channel.PartnerNodeName, logCopyServerContext.m_clientIsDownLevel);
			ContinuousLogCopyRequest2 initialRequest = LogCopyServerContext.UpgradeRequest(channel, oldReq);
			logCopyServerContext.InitContinuousLogTransmission(initialRequest);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000A1AA0 File Offset: 0x0009FCA0
		private static ContinuousLogCopyRequest2 UpgradeRequest(NetworkChannel channel, ContinuousLogCopyRequest oldReq)
		{
			ContinuousLogCopyRequest2.Flags flagsUsed = (ContinuousLogCopyRequest2.Flags)oldReq.FlagsUsed;
			return new ContinuousLogCopyRequest2(null, channel, oldReq.DatabaseGuid, oldReq.FirstGeneration, flagsUsed)
			{
				LastGeneration = oldReq.LastGeneration,
				ClientNodeName = channel.PartnerNodeName
			};
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000A1AE4 File Offset: 0x0009FCE4
		internal static void StartContinuousLogTransmission(NetworkChannel channel, ContinuousLogCopyRequest2 initialRequest)
		{
			LogCopyServerContext logCopyServerContext = new LogCopyServerContext(channel, channel.MonitoredDatabase);
			logCopyServerContext.InitContinuousLogTransmission(initialRequest);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000A1B08 File Offset: 0x0009FD08
		private static void NetworkReadComplete(object context, int bytesAvailable, bool completionIsSynchronous, Exception e)
		{
			LogCopyServerContext logCopyServerContext = (LogCopyServerContext)context;
			if (bytesAvailable > 0)
			{
				logCopyServerContext.HandleIncomingMessage();
				return;
			}
			if (e == null)
			{
				LogCopyServerContext.Tracer.TraceDebug((long)logCopyServerContext.GetHashCode(), "NetworkReadComplete: Client closed the channel");
			}
			else
			{
				LogCopyServerContext.Tracer.TraceError<Exception>((long)logCopyServerContext.GetHashCode(), "NetworkReadComplete: Channel exception: {0}", e);
			}
			logCopyServerContext.MarkForTermination();
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000A1B5F File Offset: 0x0009FD5F
		private void MarkForTermination()
		{
			LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "MarkForTermination({0})", this.PassiveCopyName);
			this.m_markedForTermination = true;
			this.SignalWorkPending();
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000A1B8C File Offset: 0x0009FD8C
		internal LogCopyServerContext(NetworkChannel channel, MonitoredDatabase database)
		{
			this.m_channel = channel;
			this.m_database = database;
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x000A1BCC File Offset: 0x0009FDCC
		private string LocalNodeName
		{
			get
			{
				return this.m_channel.LocalNodeName;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x000A1BD9 File Offset: 0x0009FDD9
		public bool IsBlockModeEnabled
		{
			get
			{
				return this.m_currentLogCopyRequest.UseGranular && GranularReplication.IsEnabled();
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x000A1BF2 File Offset: 0x0009FDF2
		// (set) Token: 0x060022DB RID: 8923 RVA: 0x000A1BFA File Offset: 0x0009FDFA
		private bool EnteredBlockMode { get; set; }

		// Token: 0x060022DC RID: 8924 RVA: 0x000A1C04 File Offset: 0x0009FE04
		public ConnectionStatus CollectConnectionStatus()
		{
			return new ConnectionStatus(this.m_clientNodeName, this.Channel.NetworkName, null, ConnectionDirection.Outgoing, false);
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000A1C2C File Offset: 0x0009FE2C
		private void Terminate()
		{
			LogCopyServerContext.Tracer.TraceFunction<string>((long)this.GetHashCode(), "{0}: Closing", this.PassiveCopyName);
			this.Channel.Abort();
			lock (this.m_networkReadLock)
			{
				lock (this.m_networkWriteLock)
				{
					lock (this)
					{
						this.m_sendDataEnabled = false;
						if (this.m_linkedWithMonitoredDatabaseTable)
						{
							this.m_database.RemoveActiveLogCopyClient(this);
							this.m_linkedWithMonitoredDatabaseTable = false;
						}
						this.Channel.Close();
						ManualResetEvent workIsPendingEvent = this.m_workIsPendingEvent;
						this.m_workIsPendingEvent = null;
						if (workIsPendingEvent != null)
						{
							workIsPendingEvent.Close();
						}
					}
				}
			}
			LogCopyServerContext.Tracer.TraceFunction<string>((long)this.GetHashCode(), "{0}: Closed", this.PassiveCopyName);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000A1D44 File Offset: 0x0009FF44
		internal void LinkWithMonitoredDatabase()
		{
			lock (this)
			{
				if (!this.m_linkedWithMonitoredDatabaseTable)
				{
					if (!this.m_database.AddActiveLogCopyClient(this))
					{
						LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Database stopped during linkage. Forcing client to reconnect to database '{0}'", this.Database.DatabaseName);
						throw new NetworkCancelledException();
					}
					this.m_linkedWithMonitoredDatabaseTable = true;
				}
			}
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000A1DC0 File Offset: 0x0009FFC0
		public void SourceIsStopping()
		{
			LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SourceIsStopping for '{0}'", this.Database.DatabaseName);
			lock (this)
			{
				this.m_linkedWithMonitoredDatabaseTable = false;
			}
			this.MarkForTermination();
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000A1E24 File Offset: 0x000A0024
		private bool TryGetNetworkWriteLock()
		{
			return Monitor.TryEnter(this.m_networkWriteLock);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000A1E31 File Offset: 0x000A0031
		private void GetNetworkWriteLock()
		{
			Monitor.Enter(this.m_networkWriteLock);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000A1E3E File Offset: 0x000A003E
		private void ReleaseNetworkWriteLock()
		{
			Monitor.Exit(this.m_networkWriteLock);
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000A1E4B File Offset: 0x000A004B
		public string PassiveCopyName
		{
			get
			{
				if (this.m_passiveCopyName == null)
				{
					this.m_passiveCopyName = string.Format("{0}\\{1}", this.Database.DatabaseName, this.m_clientNodeName);
				}
				return this.m_passiveCopyName;
			}
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000A1E7C File Offset: 0x000A007C
		private void InitContinuousLogTransmission(ContinuousLogCopyRequest2 initialRequest)
		{
			this.m_clientNodeName = initialRequest.ClientNodeName;
			LogCopyServerContext.Tracer.TraceDebug((long)this.GetHashCode(), "StartContinuousLogTransmission({0}): First=0x{1:X} Max=0x{2:X} Flags=0x{3:X}", new object[]
			{
				this.PassiveCopyName,
				initialRequest.FirstGeneration,
				initialRequest.LastGeneration,
				initialRequest.FlagsUsed
			});
			this.m_currentLogCopyRequest = initialRequest;
			this.m_nextLogCopyRequest = initialRequest;
			this.m_currentLogGeneration = initialRequest.FirstGeneration;
			this.m_sendDataEnabled = true;
			this.Channel.NetworkChannelManagesAsyncReads = false;
			try
			{
				this.m_workIsPendingEvent = new ManualResetEvent(false);
				this.LinkWithMonitoredDatabase();
			}
			catch (NetworkCancelledException arg)
			{
				LogCopyServerContext.Tracer.TraceError<NetworkCancelledException>((long)this.GetHashCode(), "InitContinuousLogTransmission cancelled {0}", arg);
				this.Terminate();
				return;
			}
			this.SourceDatabasePerfCounters = SourceDatabasePerformanceCounters.GetInstance(this.PassiveCopyName);
			try
			{
				this.StartNetworkRead();
			}
			catch (NetworkTransportException arg2)
			{
				LogCopyServerContext.Tracer.TraceError<NetworkTransportException>((long)this.GetHashCode(), "InitContinuousLogTransmission caught {0}", arg2);
				this.MarkForTermination();
			}
			this.SignalWorkPending();
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000A1FA8 File Offset: 0x000A01A8
		private void HandleIncomingMessage()
		{
			try
			{
				if (!this.m_markedForTermination)
				{
					lock (this.m_networkReadLock)
					{
						NetworkChannelMessage message = this.Channel.GetMessage();
						PingMessage pingMessage = message as PingMessage;
						if (pingMessage != null)
						{
							LogCopyServerContext.Tracer.TraceDebug((long)this.GetHashCode(), "HandleIncomingMessage: Ping received");
							Interlocked.Exchange(ref this.m_pingPending, 1);
							this.m_sendDataEnabled = true;
						}
						else
						{
							ContinuousLogCopyRequest2 continuousLogCopyRequest = message as ContinuousLogCopyRequest2;
							if (continuousLogCopyRequest == null)
							{
								if (message is ContinuousLogCopyRequest)
								{
									continuousLogCopyRequest = LogCopyServerContext.UpgradeRequest(this.Channel, message as ContinuousLogCopyRequest);
								}
								else
								{
									LogCopyServerContext.Tracer.TraceError<NetworkChannelMessage>((long)this.GetHashCode(), "HandleIncomingMessage: UnexpectedMsg:{0}", message);
									this.Channel.ThrowUnexpectedMessage(message);
								}
							}
							LogCopyServerContext.Tracer.TraceDebug<long, long, ContinuousLogCopyRequest2.Flags>((long)this.GetHashCode(), "HandleIncomingMessage: First=0x{0:X} Max=0x{1:X} Flags=0x{2:X}", continuousLogCopyRequest.FirstGeneration, continuousLogCopyRequest.LastGeneration, continuousLogCopyRequest.FlagsUsed);
							this.m_sendDataEnabled = true;
							this.m_nextLogCopyRequest = continuousLogCopyRequest;
						}
						this.StartNetworkRead();
						this.SignalWorkPending();
					}
				}
			}
			catch (NetworkTransportException arg)
			{
				LogCopyServerContext.Tracer.TraceError<NetworkTransportException>((long)this.GetHashCode(), "HandleIncomingMessage: Channel exception: {0}", arg);
				this.MarkForTermination();
			}
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000A2110 File Offset: 0x000A0310
		private void StartNetworkRead()
		{
			this.Channel.StartRead(new NetworkChannelCallback(LogCopyServerContext.NetworkReadComplete), this);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000A212A File Offset: 0x000A032A
		private void SendException(Exception e)
		{
			LogCopyServerContext.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "SendException({0}):{1}", this.PassiveCopyName, e);
			this.m_sendDataEnabled = false;
			this.Channel.SendException(e);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000A2160 File Offset: 0x000A0360
		private void SignalWorkPending()
		{
			bool flag = false;
			ManualResetEvent manualResetEvent = null;
			lock (this.m_senderThreadLock)
			{
				if (!this.m_senderIsScheduled)
				{
					this.m_senderIsScheduled = true;
					flag = true;
				}
				else if (this.m_senderIsWaiting || this.m_markedForTermination)
				{
					manualResetEvent = this.m_workIsPendingEvent;
				}
			}
			if (flag)
			{
				LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SignalWorkPending({0}) scheduled worker", this.PassiveCopyName);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendLogsEntryPoint));
				return;
			}
			if (manualResetEvent != null)
			{
				try
				{
					LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SignalWorkPending({0}): Signalling", this.PassiveCopyName);
					manualResetEvent.Set();
					return;
				}
				catch (ObjectDisposedException)
				{
					LogCopyServerContext.Tracer.TraceError<string>((long)this.GetHashCode(), "SignalWorkPending({0}): ObjectDisposedException", this.PassiveCopyName);
					return;
				}
			}
			LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SignalWorkPending({0}): Sender was busy so we skipped the signal", this.PassiveCopyName);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000A226C File Offset: 0x000A046C
		private void TriggerSendLogs()
		{
			if (this.m_sendDataEnabled)
			{
				this.SignalWorkPending();
				return;
			}
			LogCopyServerContext.Tracer.TraceError<string, long>((long)this.GetHashCode(), "TriggerSendLogs({0}): Transmission disabled. Target is still at 0x{1:X}", this.PassiveCopyName, this.m_currentLogGeneration);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000A22A4 File Offset: 0x000A04A4
		private void SendLogsEntryPoint(object dummy)
		{
			bool flag = true;
			this.GetNetworkWriteLock();
			while (!this.m_markedForTermination && flag)
			{
				Exception ex = null;
				try
				{
					this.SendLogs();
				}
				catch (NetworkTransportException ex2)
				{
					ex = ex2;
				}
				finally
				{
					flag = false;
					if (ex != null)
					{
						this.m_markedForTermination = true;
						LogCopyServerContext.Tracer.TraceError<Exception>((long)this.GetHashCode(), "SendLogsEntryPoint caught: {0}", ex);
					}
					else
					{
						LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "MarkSenderAsIdling({0})", this.PassiveCopyName);
						this.m_workIsPendingEvent.Reset();
					}
					lock (this.m_senderThreadLock)
					{
						if (!this.m_markedForTermination)
						{
							if (this.WorkIsPending())
							{
								this.m_senderIsWaiting = false;
								flag = true;
							}
							else
							{
								this.m_senderIsWaiting = true;
							}
						}
						else
						{
							this.m_senderIsWaiting = false;
						}
					}
					if (this.m_senderIsWaiting)
					{
						bool flag3 = this.m_workIsPendingEvent.WaitOne(RegistryParameters.LogShipTimeoutInMsec);
						lock (this.m_senderThreadLock)
						{
							this.m_senderIsWaiting = false;
							if (flag3 || this.WorkIsPending())
							{
								flag = true;
							}
							else
							{
								LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SendLogsEntryPoint({0}) timed out.", this.PassiveCopyName);
								this.m_senderIsScheduled = false;
							}
						}
					}
				}
			}
			this.ReleaseNetworkWriteLock();
			if (this.m_markedForTermination)
			{
				this.Terminate();
			}
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000A2470 File Offset: 0x000A0670
		private void SendLogs()
		{
			long num = 0L;
			bool flag = false;
			while (!this.m_markedForTermination && this.m_sendDataEnabled && !flag)
			{
				int num2 = Interlocked.Exchange(ref this.m_pingPending, 0);
				switch (this.SendNextLog())
				{
				case LogCopyServerContext.SendLogStatus.InSync:
					if (num2 != 0 || num == 0L)
					{
						this.SendInSyncMessage();
					}
					flag = true;
					break;
				case LogCopyServerContext.SendLogStatus.SentData:
					num += 1L;
					break;
				case LogCopyServerContext.SendLogStatus.SentE00:
					return;
				case LogCopyServerContext.SendLogStatus.EnteredBlockMode:
					this.MarkForTermination();
					return;
				case LogCopyServerContext.SendLogStatus.KeepChannelAlive:
					this.SendInSyncMessage();
					break;
				}
			}
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000A2509 File Offset: 0x000A0709
		private bool WorkIsPending()
		{
			return this.m_currentLogGeneration <= this.Database.EndOfLogGeneration || this.m_pingPending != 0 || !object.ReferenceEquals(this.m_currentLogCopyRequest, this.m_nextLogCopyRequest);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000A2544 File Offset: 0x000A0744
		private LogCopyServerContext.SendLogStatus SendNextLog()
		{
			if (!object.ReferenceEquals(this.m_currentLogCopyRequest, this.m_nextLogCopyRequest))
			{
				LogCopyServerContext.Tracer.TraceDebug((long)this.GetHashCode(), "SendLogs noticed a new copier request");
				this.m_currentLogCopyRequest = this.m_nextLogCopyRequest;
				this.m_sendDataEnabled = true;
			}
			if (this.MaxGenerationToSend != 0L && this.CurrentLogGeneration > this.MaxGenerationToSend)
			{
				LogCopyServerContext.Tracer.TraceDebug<long, long>((long)this.GetHashCode(), "target in pull model. Requested that we stop at 0x{0:X}. Target currGen at 0x{1:X}", this.MaxGenerationToSend, this.CurrentLogGeneration);
				return LogCopyServerContext.SendLogStatus.StopForPullModel;
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3219533117U);
			if (this.m_currentLogGeneration <= this.Database.EndOfLogGeneration)
			{
				LogCopyServerContext.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "SendingFullLog({0}):0x{1:x}", this.PassiveCopyName, this.m_currentLogGeneration);
				try
				{
					this.Database.SendLog(this.m_currentLogGeneration, this.m_channel, this.SourceDatabasePerfCounters);
				}
				catch (FileIOonSourceException wrappedEx)
				{
					this.m_sendDataEnabled = false;
					this.HandleReadError(wrappedEx);
					return LogCopyServerContext.SendLogStatus.StopForException;
				}
				this.m_currentLogGeneration += 1L;
				return LogCopyServerContext.SendLogStatus.SentData;
			}
			if (!this.ForAcll)
			{
				if (this.IsBlockModeEnabled)
				{
					if (this.m_blockModeEntryGeneration < this.m_currentLogGeneration)
					{
						if (this.EnterBlockMode())
						{
							return LogCopyServerContext.SendLogStatus.EnteredBlockMode;
						}
						this.m_blockModeEntryGeneration = this.m_currentLogGeneration;
						LogCopyServerContext.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "SendNextLog({0}): BlockMode failed. Will retry after 0x{1:X}", this.PassiveCopyName, this.m_blockModeEntryGeneration);
					}
				}
				else
				{
					LogCopyServerContext.Tracer.TraceDebug((long)this.GetHashCode(), "BlockMode disabled.");
				}
			}
			if (this.ForAcll)
			{
				return this.SendE00();
			}
			LogCopyServerContext.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "SendNextLog({0}): In sync at 0x{1:X}", this.PassiveCopyName, this.m_currentLogGeneration);
			return LogCopyServerContext.SendLogStatus.InSync;
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000A2710 File Offset: 0x000A0910
		private bool EnterBlockMode()
		{
			TimeSpan timeout = TimeSpan.FromSeconds(5.0);
			bool flag;
			Exception ex = AmStoreHelper.IsDatabaseMounted(this.Database.DatabaseGuid, this.LocalNodeName, timeout, out flag);
			if (ex != null)
			{
				LogCopyServerContext.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Store may not be running. Mount check failed: {0}", ex);
				return false;
			}
			if (!flag)
			{
				LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Db {0} not mounted. BlockMode is not possible", this.Database.DatabaseName);
				return false;
			}
			EnterBlockModeMsg enterBlockModeMsg = new EnterBlockModeMsg(this.Channel, EnterBlockModeMsg.Flags.PrepareToEnter, this.Database.DatabaseGuid, this.m_currentLogGeneration);
			bool flag2 = false;
			bool result;
			lock (this.m_networkReadLock)
			{
				if (this.m_markedForTermination)
				{
					result = false;
				}
				else
				{
					int readTimeoutInMs = this.Channel.TcpChannel.ReadTimeoutInMs;
					int writeTimeoutInMs = this.Channel.TcpChannel.WriteTimeoutInMs;
					try
					{
						this.Channel.TcpChannel.ReadTimeoutInMs = RegistryParameters.LogShipACLLTimeoutInMsec;
						this.Channel.TcpChannel.WriteTimeoutInMs = RegistryParameters.LogShipACLLTimeoutInMsec;
						LogCopyServerContext.Tracer.TraceDebug<string>((long)this.GetHashCode(), "EnterBlockMode requesting PrepareToEnter for {0}", this.PassiveCopyName);
						enterBlockModeMsg.Send();
						EnterBlockModeMsg enterBlockModeMsg2;
						string text;
						for (;;)
						{
							NetworkChannelMessage message = this.Channel.GetMessage();
							PingMessage pingMessage = message as PingMessage;
							if (pingMessage != null)
							{
								LogCopyServerContext.Tracer.TraceDebug((long)this.GetHashCode(), "PingMessage ignored");
							}
							else
							{
								ContinuousLogCopyRequest2 continuousLogCopyRequest = message as ContinuousLogCopyRequest2;
								if (continuousLogCopyRequest != null)
								{
									this.m_nextLogCopyRequest = continuousLogCopyRequest;
									LogCopyServerContext.Tracer.TraceDebug<long, long, ContinuousLogCopyRequest2.Flags>((long)this.GetHashCode(), "ContinuousLogCopyRequest2: First=0x{0:X} Max=0x{1:X} Flags=0x{2:X}", continuousLogCopyRequest.FirstGeneration, continuousLogCopyRequest.LastGeneration, continuousLogCopyRequest.FlagsUsed);
								}
								else
								{
									enterBlockModeMsg2 = (message as EnterBlockModeMsg);
									text = null;
									if (enterBlockModeMsg2 == null)
									{
										text = string.Format("Passive({0}) sent unexpected msg: {1}", this.PassiveCopyName, message.GetType());
									}
									else if (enterBlockModeMsg2.AckCounter != enterBlockModeMsg.AckCounter)
									{
										text = string.Format("Passive({0}) is out of sync. BlockModeEntry Aborted", this.PassiveCopyName);
									}
									if (text != null)
									{
										break;
									}
									if (enterBlockModeMsg2.FlagsUsed != EnterBlockModeMsg.Flags.PassiveIsReady)
									{
										goto Block_13;
									}
									if (!this.RequestBlockModeInStore())
									{
										goto Block_15;
									}
								}
							}
						}
						LogCopyServerContext.Tracer.TraceError((long)this.GetHashCode(), text);
						throw new NetworkUnexpectedMessageException(this.m_clientNodeName, text);
						Block_13:
						if (enterBlockModeMsg2.FlagsUsed == EnterBlockModeMsg.Flags.PassiveReject)
						{
							LogCopyServerContext.Tracer.TraceError<string>((long)this.GetHashCode(), "BlockMode rejected by passive {0}", this.PassiveCopyName);
							flag2 = true;
							return false;
						}
						text = string.Format("Passive({0}) passed unexpected flags 0x{1X}", this.PassiveCopyName, enterBlockModeMsg2.FlagsUsed);
						throw new NetworkUnexpectedMessageException(this.m_clientNodeName, text);
						Block_15:
						flag2 = true;
						result = false;
					}
					finally
					{
						if (flag2)
						{
							this.Channel.TcpChannel.ReadTimeoutInMs = readTimeoutInMs;
							this.Channel.TcpChannel.WriteTimeoutInMs = writeTimeoutInMs;
							this.StartNetworkRead();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000A2A10 File Offset: 0x000A0C10
		private bool RequestBlockModeInStore()
		{
			Exception ex = null;
			using (IStoreRpc newStoreControllerInstance = Dependencies.GetNewStoreControllerInstance(this.LocalNodeName))
			{
				try
				{
					newStoreControllerInstance.StartBlockModeReplicationToPassive(this.Database.DatabaseGuid, this.m_clientNodeName, (uint)this.m_currentLogGeneration);
					this.EnteredBlockMode = true;
					LogCopyServerContext.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "BlockMode entered for copy '{0}' at 0x{1:X}", this.PassiveCopyName, this.m_currentLogGeneration);
					return true;
				}
				catch (MapiPermanentException ex2)
				{
					ex = ex2;
				}
				catch (MapiRetryableException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					LogCopyServerContext.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "EnterBlockMode({0}) failed from store: {1}", this.PassiveCopyName, ex);
				}
			}
			return false;
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000A2ADC File Offset: 0x000A0CDC
		private void SendInSyncMessage()
		{
			NotifyEndOfLogAsyncReply notifyEndOfLogAsyncReply = new NotifyEndOfLogAsyncReply(this.m_channel, this.CurrentLogGeneration - 1L, DateTime.UtcNow);
			notifyEndOfLogAsyncReply.Send();
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000A2B0C File Offset: 0x000A0D0C
		private void HandleReadError(FileIOonSourceException wrappedEx)
		{
			Exception innerException = wrappedEx.InnerException;
			bool flag = false;
			if (innerException is CorruptLogDetectedException)
			{
				flag = true;
			}
			else if (innerException is IOException)
			{
				int hresult = 0;
				if (FileOperations.IsCorruptedIOException(innerException as IOException, out hresult))
				{
					flag = true;
					int num;
					FileOperations.ConvertHResultToWin32(hresult, out num);
					ReplayEventLogConstants.Tuple_FatalIOErrorEncountered.LogEvent(this.Database.Identity, new object[]
					{
						this.Database.DatabaseName,
						wrappedEx.FileFullPath,
						innerException.Message,
						num,
						innerException.ToString()
					});
				}
			}
			if (flag)
			{
				this.HandleCorruptLog(innerException);
				return;
			}
			this.HandleSourceReadError(wrappedEx);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000A2BBC File Offset: 0x000A0DBC
		private void HandleCorruptLog(Exception readEx)
		{
			this.SendException(new SourceLogBreakStallsPassiveException(Environment.MachineName, readEx.Message, readEx));
			ReplayEventLogConstants.Tuple_LogCopierErrorOnSourceTriggerFailover.LogEvent(this.Database.Identity, new object[]
			{
				this.Database.DatabaseName,
				this.Channel.PartnerNodeName,
				readEx.Message
			});
			this.Database.ProcessSourceLogCorruption(this.CurrentLogGeneration, readEx);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000A2C34 File Offset: 0x000A0E34
		public void NewLogNotification()
		{
			this.TriggerSendLogs();
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000A2C3C File Offset: 0x000A0E3C
		private LogCopyServerContext.SendLogStatus SendE00()
		{
			string text = this.Database.BuildLogFileName(0L);
			long num = 0L;
			FileIOonSourceException ex;
			if (this.m_dismountWorker == null)
			{
				ex = this.Database.GetE00Generation(out num, text);
			}
			else
			{
				ex = this.m_dismountWorker.LastE00ReadException;
			}
			if (ex != null)
			{
				bool flag = true;
				Exception innerException = ex.InnerException;
				if (innerException != null && innerException is EsentFileAccessDeniedException && !AmStoreServiceMonitor.WasKillTriggered())
				{
					if (this.m_dismountWorker == null)
					{
						LogCopyServerContext.Tracer.TraceDebug((long)this.GetHashCode(), "SendE00(): E00 still in use so starting a DismountDatabaseOrKillStore() in background...");
						this.m_dismountWorker = new DismountBackgroundWorker(new DismountBackgroundWorker.DismountDelegate(this.DismountDatabaseOrKillStore));
						this.m_dismountWorker.LastE00ReadException = ex;
						this.m_dismountWorker.Start();
					}
					if (this.m_dismountWorker.CompletedEvent.WaitOne(1000))
					{
						if (this.m_dismountWorker.DismountException == null)
						{
							flag = false;
							LogCopyServerContext.Tracer.TraceDebug((long)this.GetHashCode(), "SendE00(): DismountDatabaseOrKillStore() operation completed successfully");
						}
						else
						{
							LogCopyServerContext.Tracer.TraceError<Exception>((long)this.GetHashCode(), "SendE00(): DismountDatabaseOrKillStore() operation completed but encountered an exception: {0}", this.m_dismountWorker.DismountException);
						}
						this.m_dismountWorker.Dispose();
						this.m_dismountWorker = null;
					}
					else if ((ExDateTime.Now - this.m_dismountWorker.StartTime).TotalMilliseconds < (double)(RegistryParameters.AcllDismountOrKillTimeoutInSec2 * 1000))
					{
						return LogCopyServerContext.SendLogStatus.KeepChannelAlive;
					}
				}
				if (!flag)
				{
					this.Database.ProbeForMoreLogs(this.m_currentLogGeneration);
					return LogCopyServerContext.SendLogStatus.KeepTrying;
				}
				this.HandleSourceReadError(ex);
				return LogCopyServerContext.SendLogStatus.StopForException;
			}
			else if (num != this.m_currentLogGeneration)
			{
				LogCopyServerContext.Tracer.TraceDebug<long, long>((long)this.GetHashCode(), "SendE00 finds e00 at 0x{0:X} but expected 0x{1:X}", num, this.m_currentLogGeneration);
				if (num > this.m_currentLogGeneration)
				{
					this.Database.SyncWithE00(num);
					return LogCopyServerContext.SendLogStatus.KeepTrying;
				}
				AcllFailedException e = new AcllFailedException(ReplayStrings.LogCopierE00InconsistentError(num, this.m_currentLogGeneration));
				this.SendException(e);
				return LogCopyServerContext.SendLogStatus.StopForException;
			}
			else
			{
				Exception ex2 = null;
				try
				{
					using (SafeFileHandle safeFileHandle = LogCopy.OpenLogForRead(text))
					{
						CopyLogReply copyLogReply = new CopyLogReply(this.Channel);
						copyLogReply.ThisLogGeneration = 0L;
						copyLogReply.EndOfLogGeneration = num;
						copyLogReply.EndOfLogUtc = DateTime.UtcNow;
						CheckSummer summer = null;
						if (this.Channel.ChecksumDataTransfer)
						{
							summer = new CheckSummer();
						}
						this.Channel.SendLogFileTransferReply(copyLogReply, text, safeFileHandle, this.SourceDatabasePerfCounters, summer);
					}
				}
				catch (IOException ex3)
				{
					ex2 = new FileIOonSourceException(Environment.MachineName, text, ex3.Message, ex3);
				}
				catch (UnauthorizedAccessException ex4)
				{
					ex2 = new FileIOonSourceException(Environment.MachineName, text, ex4.Message, ex4);
				}
				catch (FileIOonSourceException ex5)
				{
					ex2 = ex5;
				}
				if (ex2 != null)
				{
					this.HandleSourceReadError(ex2);
					return LogCopyServerContext.SendLogStatus.StopForException;
				}
				return LogCopyServerContext.SendLogStatus.SentE00;
			}
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000A2F10 File Offset: 0x000A1110
		private Exception DismountDatabaseOrKillStore()
		{
			LogCopyServerContext.Tracer.TraceDebug((long)this.GetHashCode(), "DismountDatabaseOrKillStore() called.");
			Exception ex = AmStoreHelper.Dismount(this.Database.DatabaseGuid, UnmountFlags.SkipCacheFlush);
			if (ex != null)
			{
				ex = new DatabaseDismountOrKillStoreException(this.Database.DatabaseName, Environment.MachineName, ex.Message, ex);
				LogCopyServerContext.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "DismountDatabaseOrKillStore() for DB '{0}' failed: {1}", this.Database.DatabaseName, ex);
			}
			return ex;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000A2F8C File Offset: 0x000A118C
		private void HandleSourceReadError(Exception ex)
		{
			ReplayEventLogConstants.Tuple_LogCopierErrorOnSource.LogEvent(this.Database.Identity, new object[]
			{
				this.Database.DatabaseName,
				this.Channel.PartnerNodeName,
				ex.Message
			});
			this.SendException(ex);
		}

		// Token: 0x04000EA6 RID: 3750
		private NetworkChannel m_channel;

		// Token: 0x04000EA7 RID: 3751
		private MonitoredDatabase m_database;

		// Token: 0x04000EA8 RID: 3752
		private bool m_linkedWithMonitoredDatabaseTable;

		// Token: 0x04000EA9 RID: 3753
		private volatile bool m_sendDataEnabled = true;

		// Token: 0x04000EAA RID: 3754
		private ContinuousLogCopyRequest2 m_currentLogCopyRequest;

		// Token: 0x04000EAB RID: 3755
		private volatile ContinuousLogCopyRequest2 m_nextLogCopyRequest;

		// Token: 0x04000EAC RID: 3756
		private long m_currentLogGeneration;

		// Token: 0x04000EAD RID: 3757
		private long m_blockModeEntryGeneration;

		// Token: 0x04000EAE RID: 3758
		private DismountBackgroundWorker m_dismountWorker;

		// Token: 0x04000EAF RID: 3759
		private volatile bool m_markedForTermination;

		// Token: 0x04000EB0 RID: 3760
		private object m_networkReadLock = new object();

		// Token: 0x04000EB1 RID: 3761
		private object m_networkWriteLock = new object();

		// Token: 0x04000EB2 RID: 3762
		private string m_clientNodeName;

		// Token: 0x04000EB3 RID: 3763
		private bool m_clientIsDownLevel;

		// Token: 0x04000EB4 RID: 3764
		private string m_passiveCopyName;

		// Token: 0x04000EB5 RID: 3765
		private int m_pingPending;

		// Token: 0x04000EB6 RID: 3766
		private object m_senderThreadLock = new object();

		// Token: 0x04000EB7 RID: 3767
		private bool m_senderIsScheduled;

		// Token: 0x04000EB8 RID: 3768
		private bool m_senderIsWaiting;

		// Token: 0x04000EB9 RID: 3769
		private ManualResetEvent m_workIsPendingEvent;

		// Token: 0x02000367 RID: 871
		private enum SendLogStatus
		{
			// Token: 0x04000EBD RID: 3773
			InSync,
			// Token: 0x04000EBE RID: 3774
			SentData,
			// Token: 0x04000EBF RID: 3775
			SentE00,
			// Token: 0x04000EC0 RID: 3776
			StopForPullModel,
			// Token: 0x04000EC1 RID: 3777
			StopForException,
			// Token: 0x04000EC2 RID: 3778
			EnteredBlockMode,
			// Token: 0x04000EC3 RID: 3779
			KeepTrying,
			// Token: 0x04000EC4 RID: 3780
			KeepChannelAlive
		}
	}
}
