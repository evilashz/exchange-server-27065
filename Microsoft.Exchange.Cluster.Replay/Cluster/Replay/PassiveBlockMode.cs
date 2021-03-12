using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000372 RID: 882
	internal class PassiveBlockMode
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000A523F File Offset: 0x000A343F
		public static bool IsDebugTraceEnabled
		{
			get
			{
				return ExTraceGlobals.PassiveBlockModeTracer.IsTraceEnabled(TraceType.DebugTrace);
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x000A524C File Offset: 0x000A344C
		// (set) Token: 0x06002347 RID: 9031 RVA: 0x000A5254 File Offset: 0x000A3454
		private LogCopier Copier { get; set; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x000A525D File Offset: 0x000A345D
		// (set) Token: 0x06002349 RID: 9033 RVA: 0x000A5265 File Offset: 0x000A3465
		private IReplayConfiguration Configuration { get; set; }

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x000A526E File Offset: 0x000A346E
		private string ActiveServerName
		{
			get
			{
				return this.Configuration.SourceMachine;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x000A527B File Offset: 0x000A347B
		internal Guid DatabaseGuid
		{
			get
			{
				return this.Configuration.IdentityGuid;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x000A5288 File Offset: 0x000A3488
		internal string DatabaseName
		{
			get
			{
				return this.Configuration.DatabaseName;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x000A5295 File Offset: 0x000A3495
		// (set) Token: 0x0600234E RID: 9038 RVA: 0x000A529D File Offset: 0x000A349D
		internal bool IsBlockModeActive { get; private set; }

		// Token: 0x0600234F RID: 9039 RVA: 0x000A52A8 File Offset: 0x000A34A8
		public PassiveBlockMode(LogCopier copier, ISetBroken setBroken, int maxConsumerDepthInBytes)
		{
			this.Copier = copier;
			this.Configuration = copier.Configuration;
			this.m_maxConsumerDepthInBytes = maxConsumerDepthInBytes;
			this.m_maxBuffersInUse = PassiveBlockMode.GetMaxBuffersPerDatabase(this.m_maxConsumerDepthInBytes);
			this.m_consumer = new GranularWriter(copier, copier.PerfmonCounters, copier.Configuration, setBroken);
			this.m_timer = new Timer(new TimerCallback(this.WakeUpCallback));
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000A5360 File Offset: 0x000A3560
		public static int GetMaxMemoryPerDatabase(IADServer srv)
		{
			int maxBlockModeConsumerDepthInBytes = RegistryParameters.MaxBlockModeConsumerDepthInBytes;
			if (maxBlockModeConsumerDepthInBytes != 0)
			{
				return maxBlockModeConsumerDepthInBytes;
			}
			int result = 10485760;
			if (srv != null)
			{
				long? continuousReplicationMaxMemoryPerDatabase = srv.ContinuousReplicationMaxMemoryPerDatabase;
				if (continuousReplicationMaxMemoryPerDatabase != null)
				{
					if (continuousReplicationMaxMemoryPerDatabase.Value > 104857600L)
					{
						PassiveBlockMode.Tracer.TraceError<long>(0L, "AD.ContinuousReplicationMaxMemoryPerDatabase too big: {0}bytes", continuousReplicationMaxMemoryPerDatabase.Value);
						result = 104857600;
					}
					else if (continuousReplicationMaxMemoryPerDatabase.Value < 3145728L)
					{
						PassiveBlockMode.Tracer.TraceError<long>(0L, "AD.ContinuousReplicationMaxMemoryPerDatabase too small: {0}", continuousReplicationMaxMemoryPerDatabase.Value);
						result = 3145728;
					}
					else
					{
						result = (int)continuousReplicationMaxMemoryPerDatabase.Value;
					}
				}
			}
			return result;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000A53FC File Offset: 0x000A35FC
		public static int GetMaxBuffersPerDatabase(int maxMemInBytesPerDatabase)
		{
			int num = maxMemInBytesPerDatabase / 1048576;
			int num2 = maxMemInBytesPerDatabase % 1048576;
			if (num2 != 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000A5424 File Offset: 0x000A3624
		internal void SetCrossSiteFlag(IADServer activeServer, IADServer passiveServer)
		{
			this.m_isCrossSite = false;
			if (activeServer == null)
			{
				PassiveBlockMode.Tracer.TraceError((long)this.GetHashCode(), "Unable to determine the cross-site flag, because active server AD object is null.");
				return;
			}
			if (passiveServer == null)
			{
				PassiveBlockMode.Tracer.TraceError((long)this.GetHashCode(), "Unable to determine the cross-site flag, because passive server AD object is null.");
				return;
			}
			if (passiveServer.ServerSite == null && activeServer.ServerSite != null)
			{
				PassiveBlockMode.Tracer.TraceDebug<string>((long)this.GetHashCode(), "This block mode client is cross-site because passive server has no site and active server is in site '{0}'.", activeServer.ServerSite.Name);
				this.m_isCrossSite = true;
				return;
			}
			if (passiveServer.ServerSite != null && activeServer.ServerSite == null)
			{
				PassiveBlockMode.Tracer.TraceDebug<string>((long)this.GetHashCode(), "This block mode client is cross-site because active server has no site and passive server is in site '{0}'.", passiveServer.ServerSite.Name);
				this.m_isCrossSite = true;
				return;
			}
			if (!SharedHelper.StringIEquals(activeServer.ServerSite.Name, passiveServer.ServerSite.Name))
			{
				PassiveBlockMode.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "This block mode client is cross-site. Active site is '{0}' and passive site is '{1}'.", activeServer.ServerSite.Name, passiveServer.ServerSite.Name);
				this.m_isCrossSite = true;
			}
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000A5530 File Offset: 0x000A3730
		public void Destroy()
		{
			lock (this.m_workerLock)
			{
				this.m_timer.Dispose();
				this.m_timer = null;
			}
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000A557C File Offset: 0x000A377C
		private static void ReadCallback(object asyncState, int bytesAvailable, bool completionIsSynchronous, Exception e)
		{
			PassiveReadRequest passiveReadRequest = (PassiveReadRequest)asyncState;
			passiveReadRequest.Manager.ProcessReadCallback(passiveReadRequest, bytesAvailable, completionIsSynchronous, e);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000A55A0 File Offset: 0x000A37A0
		private void WakeUpCallback(object context)
		{
			PassiveBlockMode.Tracer.TraceFunction<string>((long)this.GetHashCode(), "WakeUpCallback({0})", this.DatabaseName);
			lock (this.m_workerLock)
			{
				if (!this.IsBlockModeActive)
				{
					PassiveBlockMode.Tracer.TraceError((long)this.GetHashCode(), "Timer fired after termination");
				}
				else if (this.Copier.TestHungPassiveBlockMode)
				{
					PassiveBlockMode.Tracer.TraceError((long)this.GetHashCode(), "TestHungPassiveBlockMode is active. Timer ignored");
				}
				else if (this.m_timeoutPending)
				{
					PassiveBlockMode.Tracer.TraceError<ExDateTime, ExDateTime>((long)this.GetHashCode(), "Active did not respond by {0}, cur time is {1}", this.m_timeoutLimit, ExDateTime.Now);
					this.Terminate();
				}
				else
				{
					this.m_timeoutPending = true;
					this.m_timeoutLimit = ExDateTime.Now.AddMilliseconds((double)RegistryParameters.LogShipTimeoutInMsec);
					NetworkTransportException ex;
					bool flag2;
					this.TrySendStatusMessageToActive(PassiveStatusMsg.Flags.PassiveIsRequestingAck, Win32StopWatch.GetSystemPerformanceCounter(), out ex, out flag2);
					if (ex == null)
					{
						this.ScheduleTimer();
					}
					else
					{
						PassiveBlockMode.Tracer.TraceError<NetworkTransportException>((long)this.GetHashCode(), "WakeUpCallback Failed to ping Active: {0}", ex);
						this.Terminate();
					}
				}
			}
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000A56CC File Offset: 0x000A38CC
		public bool UsePartialLogsDuringAcll(long expectedGen)
		{
			return this.m_consumer.UsePartialLogsDuringAcll(expectedGen);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000A56DC File Offset: 0x000A38DC
		public bool EnterBlockMode(EnterBlockModeMsg msg, NetworkChannel channel, int maxConsumerQDepth)
		{
			lock (this.m_workerLock)
			{
				Exception ex = null;
				try
				{
					if (msg.FirstGenerationToExpect != this.Copier.NextGenExpected)
					{
						string text = string.Format("EnterBlockMode({0}) received gen 0x{1:X} but was expecting 0x{2:X}", this.DatabaseName, msg.FirstGenerationToExpect, this.Copier.NextGenExpected);
						PassiveBlockMode.Tracer.TraceError((long)this.GetHashCode(), text);
						throw new NetworkUnexpectedMessageException(this.Configuration.SourceMachine, text);
					}
					PassiveBlockMode.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "EnterBlockMode({0}) starts at gen 0x{1:X}", this.DatabaseName, msg.FirstGenerationToExpect);
					this.m_oldestMessage = null;
					this.m_newestMessage = null;
					this.m_maxConsumerDepthInBytes = maxConsumerQDepth;
					this.m_maxBuffersInUse = PassiveBlockMode.GetMaxBuffersPerDatabase(this.m_maxConsumerDepthInBytes);
					this.m_consumer.Initialize();
					channel.KeepAlive = true;
					channel.NetworkChannelManagesAsyncReads = false;
					this.m_netChannel = channel;
					this.IsBlockModeActive = true;
					this.Copier.PerfmonCounters.GranularReplication = 1L;
					this.Copier.PerfmonCounters.EncryptionEnabled = (channel.IsEncryptionEnabled ? 1L : 0L);
					this.StartRead();
					msg.Send();
					this.ScheduleTimer();
					return true;
				}
				catch (NetworkTransportException ex2)
				{
					ex = ex2;
				}
				catch (GranularReplicationTerminatedException ex3)
				{
					ex = ex3;
				}
				catch (GranularReplicationInitFailedException ex4)
				{
					ex = ex4;
				}
				finally
				{
					if (ex != null)
					{
						PassiveBlockMode.Tracer.TraceError<Exception>((long)this.GetHashCode(), "EnterBlockMode failed: {0}", ex);
						this.Terminate();
					}
					else
					{
						PassiveBlockMode.Tracer.TraceDebug<string>((long)this.GetHashCode(), "EnterBlockMode({0}) succeeded", this.DatabaseName);
					}
				}
			}
			return false;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000A58F8 File Offset: 0x000A3AF8
		private PassiveReadRequest StartRead()
		{
			NetworkChannel netChannel = this.m_netChannel;
			PassiveReadRequest passiveReadRequest = new PassiveReadRequest(this, netChannel);
			bool flag = false;
			try
			{
				if (!this.Copier.TestHungPassiveBlockMode)
				{
					netChannel.StartRead(new NetworkChannelCallback(PassiveBlockMode.ReadCallback), passiveReadRequest);
					flag = true;
				}
			}
			finally
			{
				if (!flag)
				{
					PassiveBlockMode.Tracer.TraceError((long)this.GetHashCode(), "Failed to start async read");
				}
			}
			return passiveReadRequest;
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000A5968 File Offset: 0x000A3B68
		private void ProcessReadCallback(PassiveReadRequest ioReq, int bytesAvailable, bool completionIsSynchronous, Exception readEx)
		{
			PassiveBlockMode.Tracer.TraceFunction<string>((long)this.GetHashCode(), "ProcessReadCallback({0}) entered", this.DatabaseName);
			lock (this.m_workerLock)
			{
				this.DisableTimer();
				Exception ex = null;
				bool flag2 = false;
				try
				{
					this.m_recursionDepth++;
					DiagCore.RetailAssert(this.m_recursionDepth == 1 || completionIsSynchronous, "recursive async completion", new object[0]);
					ioReq.CompletionWasProcessed = true;
					if (completionIsSynchronous)
					{
						ioReq.CompletedSynchronously = completionIsSynchronous;
					}
					if (readEx != null)
					{
						PassiveBlockMode.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "ProcessReadCallback({0}) read failed: ex={1}", this.DatabaseName, readEx);
						ex = new NetworkCommunicationException(this.Configuration.SourceMachine, readEx.Message, readEx);
					}
					else if (bytesAvailable == 0)
					{
						PassiveBlockMode.Tracer.TraceError<string>((long)this.GetHashCode(), "ProcessReadCallback({0}) active closed connection", this.DatabaseName);
						ex = new NetworkEndOfDataException(this.Configuration.SourceMachine, ReplayStrings.NetworkReadEOF);
					}
					else if (ioReq.Channel != this.m_netChannel)
					{
						ex = new NetworkEndOfDataException(this.Configuration.SourceMachine, ReplayStrings.NetworkReadEOF);
						flag2 = true;
						ioReq.Channel.Close();
					}
					else if (!this.IsBlockModeActive)
					{
						PassiveBlockMode.Tracer.TraceError<string>((long)this.GetHashCode(), "Discarding read since BM was already terminated", this.DatabaseName);
					}
					else
					{
						QueuedBlockMsg queuedBlockMsg = this.ReadInputMessage(ioReq.Channel);
						if (queuedBlockMsg != null)
						{
							this.ProcessInput(queuedBlockMsg);
							if (BitMasker.IsOn((int)queuedBlockMsg.EmitContext.grbitOperationalFlags, 2))
							{
								this.HandleActiveDismount();
								return;
							}
							if (BitMasker.IsOn((int)queuedBlockMsg.EmitContext.grbitOperationalFlags, 16))
							{
								this.Copier.TrackLastContactTime(queuedBlockMsg.MessageUtc);
							}
						}
						if (this.m_recursionDepth == 1)
						{
							while (this.IsBlockModeActive)
							{
								PassiveReadRequest passiveReadRequest = this.StartRead();
								if (!passiveReadRequest.CompletionWasProcessed)
								{
									if (!this.m_runningAcll)
									{
										this.ScheduleTimer();
										break;
									}
									break;
								}
							}
						}
						else
						{
							PassiveBlockMode.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Recursive read avoided. Depth={0}", this.m_recursionDepth);
						}
					}
				}
				catch (NetworkTransportException ex2)
				{
					ex = ex2;
				}
				catch (NetworkRemoteException ex3)
				{
					ex = ex3;
				}
				catch (GranularReplicationOverflowException ex4)
				{
					ex = ex4;
				}
				finally
				{
					this.m_recursionDepth--;
					if (ex != null && !flag2)
					{
						this.Terminate();
					}
					PassiveBlockMode.Tracer.TraceFunction<string>((long)this.GetHashCode(), "ProcessReadCallback({0}) exits", this.DatabaseName);
				}
			}
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000A5C50 File Offset: 0x000A3E50
		private QueuedBlockMsg ReadInputMessage(NetworkChannel netChan)
		{
			this.m_timeoutPending = false;
			byte[] networkReadWorkingBuf = this.m_networkReadWorkingBuf;
			StopwatchStamp stamp = StopwatchStamp.GetStamp();
			NetworkChannelMessageHeader msgHdr = NetworkChannelMessage.ReadHeaderFromNet(netChan, networkReadWorkingBuf, 0);
			NetworkChannelMessage.MessageType messageType = msgHdr.MessageType;
			QueuedBlockMsg queuedBlockMsg;
			if (messageType != NetworkChannelMessage.MessageType.BlockModeCompressedData)
			{
				if (messageType == NetworkChannelMessage.MessageType.Ping)
				{
					PingMessage pingMessage = PingMessage.ReadFromNet(netChan, networkReadWorkingBuf, 0);
					long systemPerformanceCounter = Win32StopWatch.GetSystemPerformanceCounter();
					long arg = Win32StopWatch.ComputeElapsedTimeInUSec(systemPerformanceCounter, pingMessage.ReplyAckCounter) / 1000L;
					this.Copier.TrackLastContactTime(msgHdr.MessageUtc);
					PassiveBlockMode.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "ProcessReadCallback({0}) received a ping after {1}ms, so channel is healthy", this.DatabaseName, arg);
					return null;
				}
				if (messageType != NetworkChannelMessage.MessageType.GranularLogData)
				{
					throw new NetworkUnexpectedMessageException(netChan.PartnerNodeName, string.Format("Unknown Type {0}", msgHdr.MessageType));
				}
				queuedBlockMsg = this.ReadUncompressedMsg(netChan);
				this.Copier.PerfmonCounters.CompressionEnabled = 0L;
			}
			else
			{
				queuedBlockMsg = this.ReadCompressedMsg(netChan, msgHdr);
				this.Copier.PerfmonCounters.CompressionEnabled = 1L;
			}
			queuedBlockMsg.ReadDurationInTics = stamp.ElapsedTicks;
			queuedBlockMsg.MessageUtc = msgHdr.MessageUtc;
			this.Copier.PerfmonCounters.RecordLogCopierNetworkReadLatency(queuedBlockMsg.ReadDurationInTics);
			return queuedBlockMsg;
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000A5D8C File Offset: 0x000A3F8C
		private QueuedBlockMsg ReadUncompressedMsg(NetworkChannel netChan)
		{
			byte[] networkReadWorkingBuf = this.m_networkReadWorkingBuf;
			GranularLogDataMsg granularLogDataMsg = GranularLogDataMsg.ReadFromNet(netChan, networkReadWorkingBuf, 0);
			byte[] array = null;
			int num = 0;
			if (granularLogDataMsg.LogDataLength > 0)
			{
				this.GetAppendSpace(granularLogDataMsg.LogDataLength);
				array = this.m_currentBuffer.Buffer;
				num = this.m_currentBuffer.AppendOffset;
				netChan.Read(array, num, granularLogDataMsg.LogDataLength);
				this.m_currentBuffer.AppendOffset = num + granularLogDataMsg.LogDataLength;
			}
			return new QueuedBlockMsg(granularLogDataMsg.EmitContext, array, num, 0)
			{
				RequestAckCounter = granularLogDataMsg.RequestAckCounter,
				IOBuffer = this.m_currentBuffer
			};
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000A5E28 File Offset: 0x000A4028
		private QueuedBlockMsg ReadCompressedMsg(NetworkChannel netChan, NetworkChannelMessageHeader msgHdr)
		{
			byte[] networkReadWorkingBuf = this.m_networkReadWorkingBuf;
			BlockModeCompressedDataMsg blockModeCompressedDataMsg = BlockModeCompressedDataMsg.ReadFromNet(netChan, networkReadWorkingBuf, 0);
			byte[] array = null;
			int num = 0;
			int num2 = 0;
			if (blockModeCompressedDataMsg.LogDataLength > 0)
			{
				this.GetAppendSpace(blockModeCompressedDataMsg.LogDataLength);
				array = this.m_currentBuffer.Buffer;
				num = this.m_currentBuffer.AppendOffset;
				int num3 = blockModeCompressedDataMsg.LogDataLength;
				int num4 = num;
				foreach (int num5 in blockModeCompressedDataMsg.CompressedLengths)
				{
					num2 += num5;
					netChan.Read(networkReadWorkingBuf, 0, num5);
					int num6 = Math.Min(num3, 65536);
					if (!Xpress.Decompress(networkReadWorkingBuf, 0, num5, array, num4, num6))
					{
						throw new NetworkCorruptDataException(this.m_netChannel.PartnerNodeName);
					}
					num3 -= num6;
					num4 += num6;
				}
				this.m_currentBuffer.AppendOffset = num + blockModeCompressedDataMsg.LogDataLength;
			}
			return new QueuedBlockMsg(blockModeCompressedDataMsg.EmitContext, array, num, num2)
			{
				RequestAckCounter = blockModeCompressedDataMsg.RequestAckCounter,
				IOBuffer = this.m_currentBuffer
			};
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000A5F38 File Offset: 0x000A4138
		private void ProcessInput(QueuedBlockMsg dataMsg)
		{
			dataMsg.GetMessageSize();
			if (this.m_newestMessage != null)
			{
				this.m_newestMessage.NextMsg = dataMsg;
				this.m_newestMessage = dataMsg;
			}
			else
			{
				this.m_newestMessage = dataMsg;
				this.m_oldestMessage = dataMsg;
			}
			bool flag = BitMasker.IsOn((int)dataMsg.EmitContext.grbitOperationalFlags, 16);
			if (PassiveBlockMode.IsDebugTraceEnabled)
			{
				long num = StopwatchStamp.TicksToMicroSeconds(dataMsg.ReadDurationInTics);
				PassiveBlockMode.Tracer.TraceDebug((long)this.GetHashCode(), "MessageArrived({0}) Gen=0x{1:X} Sector=0x{2:X} JBits=0x{3:X} EmitSeq=0x{4:X} LogDataLen=0x{5:X} ReadUSec={6}", new object[]
				{
					this.DatabaseName,
					dataMsg.EmitContext.lgposLogData.lGeneration,
					dataMsg.EmitContext.lgposLogData.isec,
					(int)dataMsg.EmitContext.grbitOperationalFlags,
					dataMsg.EmitContext.qwSequenceNum,
					dataMsg.LogDataLength,
					num
				});
			}
			this.Copier.PerfmonCounters.RecordGranularBytesReceived((long)dataMsg.LogDataLength, flag);
			ExchangeNetworkPerfmonCounters perfCounters = this.m_netChannel.PerfCounters;
			if (perfCounters != null)
			{
				perfCounters.RecordLogCopyThruputReceived((long)dataMsg.LogDataLength);
				if (dataMsg.CompressedLogDataLength > 0)
				{
					perfCounters.RecordCompressedDataReceived(dataMsg.CompressedLogDataLength, dataMsg.LogDataLength, NetworkPath.ConnectionPurpose.LogCopy);
				}
			}
			this.TriggerConsumer();
			if (flag || DateTime.UtcNow >= this.m_nextPingDue)
			{
				uint lGeneration = (uint)dataMsg.EmitContext.lgposLogData.lGeneration;
				if (flag)
				{
					this.Copier.TrackKnownEndOfLog((long)((ulong)lGeneration), dataMsg.EmitContext.logtimeEmit);
				}
				NetworkTransportException ex;
				bool flag2;
				this.TrySendStatusMessageToActive(flag ? PassiveStatusMsg.Flags.AckEndOfGeneration : PassiveStatusMsg.Flags.None, dataMsg.RequestAckCounter, out ex, out flag2);
				if (ex != null)
				{
					throw ex;
				}
			}
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000A61C0 File Offset: 0x000A43C0
		private void TrySendStatusMessageToActive(PassiveStatusMsg.Flags statusFlags, long requestAckCounter, out NetworkTransportException sendException, out bool sendSkippedBecauseChannelBusy)
		{
			sendException = null;
			sendSkippedBecauseChannelBusy = false;
			NetworkChannel netCh = this.m_netChannel;
			if (this.m_asyncNetWritePending || netCh == null)
			{
				PassiveBlockMode.Tracer.TraceError((long)this.GetHashCode(), "SendStatusMessageToActive skipped because channel is busy");
				sendSkippedBecauseChannelBusy = true;
				return;
			}
			ReplayState replayState = this.Configuration.ReplayState;
			long highestCompleteGen = replayState.CopyNotificationGenerationNumber;
			byte[] statusMsgBuf = PassiveStatusMsg.SerializeToBytes(statusFlags, requestAckCounter, (uint)highestCompleteGen, (uint)replayState.CopyGenerationNumber, (uint)replayState.InspectorGenerationNumber, (uint)replayState.ReplayGenerationNumber, this.m_isCrossSite);
			bool writeStarted = false;
			try
			{
				Exception ex = NetworkChannel.RunNetworkFunction(delegate
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(PassiveBlockMode.ackMessageSendFailed);
					PassiveBlockMode.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "Sending PassiveStatusMsg({0}) Gen 0x{1:X}", this.DatabaseName, highestCompleteGen);
					this.m_nextPingDue = DateTime.UtcNow.AddMilliseconds((double)RegistryParameters.LogShipTimeoutInMsec);
					this.m_asyncNetWritePending = true;
					netCh.TcpChannel.Stream.BeginWrite(statusMsgBuf, 0, statusMsgBuf.Length, new AsyncCallback(this.NetWriteCompletion), netCh);
					writeStarted = true;
				});
				if (ex != null)
				{
					PassiveBlockMode.Tracer.TraceError<Exception>((long)this.GetHashCode(), "SendStatusMessageToActive Failed to ping Active: {0}", ex);
					ReplayEventLogConstants.Tuple_NotifyActiveSendFailed.LogEvent(null, new object[]
					{
						this.DatabaseName,
						replayState.ReplayGenerationNumber,
						replayState.InspectorGenerationNumber,
						replayState.CopyGenerationNumber,
						ex.Message
					});
					sendException = (ex as NetworkTransportException);
					if (sendException == null)
					{
						sendException = new NetworkCommunicationException(netCh.PartnerNodeName, ex.Message, ex);
					}
				}
			}
			finally
			{
				if (!writeStarted)
				{
					this.m_asyncNetWritePending = false;
				}
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000A6368 File Offset: 0x000A4568
		private void NetWriteCompletion(IAsyncResult ar)
		{
			PassiveBlockMode.Tracer.TraceFunction<string>((long)this.GetHashCode(), "NetWriteCompletion({0})", this.DatabaseName);
			NetworkChannel netCh = (NetworkChannel)ar.AsyncState;
			Exception ex = NetworkChannel.RunNetworkFunction(delegate
			{
				netCh.TcpChannel.Stream.EndWrite(ar);
			});
			if (netCh == this.m_netChannel)
			{
				this.m_asyncNetWritePending = false;
				if (ex != null)
				{
					PassiveBlockMode.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "NetWriteCompletion({0}) failed: {1}", this.DatabaseName, ex);
					this.Terminate();
				}
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000A6404 File Offset: 0x000A4604
		private void CloseChannel()
		{
			NetworkChannel netChannel = this.m_netChannel;
			if (netChannel != null)
			{
				this.m_netChannel = null;
				netChannel.Close();
				this.m_asyncNetWritePending = false;
			}
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000A6434 File Offset: 0x000A4634
		public void Terminate()
		{
			PassiveBlockMode.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Terminate({0})", this.DatabaseName);
			lock (this.m_workerLock)
			{
				this.DisableTimer();
				this.IsBlockModeActive = false;
				this.Copier.PerfmonCounters.GranularReplication = 0L;
				this.m_consumer.Terminate();
				this.CloseChannel();
				lock (this.m_consumerLock)
				{
					this.m_oldestMessage = null;
					this.m_newestMessage = null;
				}
				this.FreeAllBuffers();
			}
			this.Copier.NotifyBlockModeTermination();
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000A6504 File Offset: 0x000A4704
		private void DisableTimer()
		{
			if (this.m_timer != null)
			{
				this.m_timer.Change(-1, -1);
			}
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000A651C File Offset: 0x000A471C
		private void ScheduleTimer(int msecUntilDue)
		{
			this.m_timer.Change(msecUntilDue, -1);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000A652C File Offset: 0x000A472C
		private void ScheduleTimer()
		{
			this.ScheduleTimer(RegistryParameters.LogShipTimeoutInMsec);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000A653C File Offset: 0x000A473C
		private void TriggerConsumer()
		{
			if (Interlocked.Exchange(ref this.m_consumerScheduled, 1) == 0)
			{
				PassiveBlockMode.Tracer.TraceDebug((long)this.GetHashCode(), "Queuing Consumer");
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.ConsumerEntryPoint), null);
				return;
			}
			PassiveBlockMode.Tracer.TraceDebug((long)this.GetHashCode(), "Consumer is already scheduled or running");
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000A6598 File Offset: 0x000A4798
		private void ConsumerEntryPoint(object dummy)
		{
			Exception ex = null;
			try
			{
				lock (this.m_consumerLock)
				{
					this.ConsumeData();
				}
			}
			catch (GranularReplicationTerminatedException ex2)
			{
				ex = ex2;
			}
			finally
			{
				Interlocked.Exchange(ref this.m_consumerScheduled, 0);
			}
			if (ex == null)
			{
				if (this.m_oldestMessage != this.m_newestMessage)
				{
					this.TriggerConsumer();
					return;
				}
			}
			else
			{
				this.Terminate();
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000A6628 File Offset: 0x000A4828
		private void ConsumeData()
		{
			while (this.IsBlockModeActive)
			{
				QueuedBlockMsg queuedBlockMsg = this.m_oldestMessage;
				if (queuedBlockMsg == null)
				{
					return;
				}
				if (queuedBlockMsg.WasProcessed)
				{
					queuedBlockMsg = queuedBlockMsg.NextMsg;
					if (queuedBlockMsg == null)
					{
						return;
					}
				}
				queuedBlockMsg.WasProcessed = true;
				this.m_oldestMessage = queuedBlockMsg;
				this.m_consumer.Write(queuedBlockMsg.EmitContext, queuedBlockMsg.LogDataBuf, queuedBlockMsg.LogDataStartOffset);
				if (this.m_oldestMessage.IOBuffer != this.m_oldestBuffer && this.m_oldestMessage.IOBuffer != null)
				{
					this.RemoveOldestBuffer();
				}
			}
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000A66B0 File Offset: 0x000A48B0
		private void RemoveOldestBuffer()
		{
			lock (this.m_bufferManangementLock)
			{
				IOBuffer oldestBuffer = this.m_oldestBuffer;
				this.m_oldestBuffer = oldestBuffer.NextBuffer;
				this.m_buffersInUseCount--;
				this.ReleaseBuffer(oldestBuffer);
			}
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000A6714 File Offset: 0x000A4914
		private void GetAppendSpace(int len)
		{
			if (len > 1048576)
			{
				throw new NetworkUnexpectedMessageException(this.ActiveServerName, string.Format("Invalid BlockMode length: {0}", len));
			}
			if (this.m_currentBuffer == null || this.m_currentBuffer.RemainingSpace < len)
			{
				if (this.m_buffersInUseCount >= this.m_maxBuffersInUse)
				{
					this.CheckForOverflow();
				}
				this.AppendBuffer();
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000A6778 File Offset: 0x000A4978
		private void AppendBuffer()
		{
			lock (this.m_bufferManangementLock)
			{
				IOBuffer iobuffer = this.AcquireBuffer();
				if (this.m_currentBuffer != null)
				{
					this.m_currentBuffer.NextBuffer = iobuffer;
				}
				else
				{
					this.m_oldestBuffer = iobuffer;
				}
				this.m_currentBuffer = iobuffer;
				this.m_buffersInUseCount++;
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000A67EC File Offset: 0x000A49EC
		private void ReleaseBuffer(IOBuffer buf)
		{
			if (this.m_freeBuffers.Count >= 3 || !buf.PreAllocated)
			{
				IOBufferPool.Free(buf);
				return;
			}
			this.m_freeBuffers.Push(buf);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000A6818 File Offset: 0x000A4A18
		private IOBuffer AcquireBuffer()
		{
			IOBuffer iobuffer;
			if (this.m_freeBuffers.Count > 0)
			{
				iobuffer = this.m_freeBuffers.Pop();
			}
			else
			{
				iobuffer = IOBufferPool.Allocate();
			}
			iobuffer.AppendOffset = 0;
			iobuffer.NextBuffer = null;
			return iobuffer;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000A6858 File Offset: 0x000A4A58
		private void FreeAllBuffers()
		{
			lock (this.m_bufferManangementLock)
			{
				while (this.m_oldestBuffer != null)
				{
					IOBuffer iobuffer = this.m_oldestBuffer;
					this.m_oldestBuffer = iobuffer.NextBuffer;
					IOBufferPool.Free(iobuffer);
				}
				this.m_currentBuffer = null;
				this.m_buffersInUseCount = 0;
				while (this.m_freeBuffers.Count > 0)
				{
					IOBuffer iobuffer = this.m_freeBuffers.Pop();
					IOBufferPool.Free(iobuffer);
				}
			}
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000A68E8 File Offset: 0x000A4AE8
		public void PrepareForAcll()
		{
			this.m_runningAcll = true;
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000A68F4 File Offset: 0x000A4AF4
		public void FinishForAcll(int timeoutInMsec)
		{
			lock (this.m_workerLock)
			{
				this.DrainConsumerQ(new TimeSpan(0, 0, 0, 0, timeoutInMsec), true);
			}
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000A6940 File Offset: 0x000A4B40
		private void HandleActiveDismount()
		{
			PassiveBlockMode.Tracer.TraceDebug<string>((long)this.GetHashCode(), "HandleActiveDismount({0})", this.DatabaseName);
			this.CloseChannel();
			this.DrainConsumerQ(new TimeSpan(0, 2, 0), true);
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000A6974 File Offset: 0x000A4B74
		private bool DrainConsumerQ(TimeSpan maxWaitForConsumer, bool alwaysTerminate)
		{
			bool result = false;
			bool flag = false;
			try
			{
				Monitor.TryEnter(this.m_consumerLock, maxWaitForConsumer, ref flag);
				if (flag)
				{
					if (this.m_oldestMessage != null)
					{
						this.ConsumeData();
					}
					result = true;
				}
				else
				{
					PassiveBlockMode.Tracer.TraceError<TimeSpan>((long)this.GetHashCode(), "Drain failed to obtain consumer Lock after {0}", maxWaitForConsumer);
					ReplayCrimsonEvents.PasssiveBlockModeDrainTimeout.Log<string, TimeSpan, StackTrace>(this.DatabaseName, maxWaitForConsumer, new StackTrace());
				}
			}
			catch (GranularReplicationTerminatedException arg)
			{
				PassiveBlockMode.Tracer.TraceError<string, GranularReplicationTerminatedException>((long)this.GetHashCode(), "Drain({0}) caught: {1}", this.DatabaseName, arg);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.m_consumerLock);
				}
				if (alwaysTerminate)
				{
					this.Terminate();
				}
			}
			return result;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000A6A30 File Offset: 0x000A4C30
		private void CheckForOverflow()
		{
			if (RegistryParameters.DisableGranularReplicationOverflow)
			{
				return;
			}
			ReplayCrimsonEvents.BlockModeOverflowOnPassive.Log<string, int>(this.DatabaseName, this.m_maxConsumerDepthInBytes);
			GranularReplicationOverflowException ex = new GranularReplicationOverflowException();
			if (RegistryParameters.WatsonOnBlockModeConsumerOverflow)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(ex);
			}
			if (this.DrainConsumerQ(new TimeSpan(0, 0, 5), false))
			{
				return;
			}
			throw ex;
		}

		// Token: 0x04000F0D RID: 3853
		private const int MinFreeBufferCount = 3;

		// Token: 0x04000F0E RID: 3854
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.PassiveBlockModeTracer;

		// Token: 0x04000F0F RID: 3855
		private static uint ackMessageSendFailed = 3678809405U;

		// Token: 0x04000F10 RID: 3856
		private GranularWriter m_consumer;

		// Token: 0x04000F11 RID: 3857
		private NetworkChannel m_netChannel;

		// Token: 0x04000F12 RID: 3858
		private bool m_isCrossSite;

		// Token: 0x04000F13 RID: 3859
		private object m_bufferManangementLock = new object();

		// Token: 0x04000F14 RID: 3860
		private int m_buffersInUseCount;

		// Token: 0x04000F15 RID: 3861
		private IOBuffer m_oldestBuffer;

		// Token: 0x04000F16 RID: 3862
		private IOBuffer m_currentBuffer;

		// Token: 0x04000F17 RID: 3863
		private Stack<IOBuffer> m_freeBuffers = new Stack<IOBuffer>(3);

		// Token: 0x04000F18 RID: 3864
		private QueuedBlockMsg m_oldestMessage;

		// Token: 0x04000F19 RID: 3865
		private QueuedBlockMsg m_newestMessage;

		// Token: 0x04000F1A RID: 3866
		private Timer m_timer;

		// Token: 0x04000F1B RID: 3867
		private object m_workerLock = new object();

		// Token: 0x04000F1C RID: 3868
		private bool m_timeoutPending;

		// Token: 0x04000F1D RID: 3869
		private ExDateTime m_timeoutLimit;

		// Token: 0x04000F1E RID: 3870
		private int m_maxConsumerDepthInBytes;

		// Token: 0x04000F1F RID: 3871
		private int m_maxBuffersInUse;

		// Token: 0x04000F20 RID: 3872
		private int m_recursionDepth;

		// Token: 0x04000F21 RID: 3873
		private byte[] m_networkReadWorkingBuf = new byte[65536];

		// Token: 0x04000F22 RID: 3874
		private DateTime m_nextPingDue = DateTime.UtcNow;

		// Token: 0x04000F23 RID: 3875
		private volatile bool m_asyncNetWritePending;

		// Token: 0x04000F24 RID: 3876
		private object m_consumerLock = new object();

		// Token: 0x04000F25 RID: 3877
		private int m_consumerScheduled;

		// Token: 0x04000F26 RID: 3878
		private bool m_runningAcll;
	}
}
