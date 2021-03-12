using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.HA;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.Server.Storage.HA;

namespace Microsoft.Exchange.Server.Storage.BlockMode
{
	// Token: 0x0200000F RID: 15
	internal class BlockModeSender
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00004B5C File Offset: 0x00002D5C
		public BlockModeSender(string passiveName, BlockModeCollector collector, BlockModeMessageStream.SenderPosition startPos)
		{
			this.PassiveName = passiveName;
			this.DatabaseName = collector.DatabaseName;
			this.CopyName = string.Format("{0}\\{1}", this.DatabaseName, this.PassiveName);
			this.Collector = collector;
			this.Position = startPos;
			this.perfInstance = ActiveDatabaseSenderPerformanceCounters.GetInstance(this.CopyName);
			this.logCopyStatus = new LogCopyStatus(CopyType.BlockModePassive, this.PassiveName, false, 0UL, 0UL, 0UL);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004BD6 File Offset: 0x00002DD6
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.BlockModeSenderTracer;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004BDD File Offset: 0x00002DDD
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00004BE5 File Offset: 0x00002DE5
		public string PassiveName { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004BEE File Offset: 0x00002DEE
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00004BF6 File Offset: 0x00002DF6
		public string DatabaseName { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004BFF File Offset: 0x00002DFF
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00004C07 File Offset: 0x00002E07
		public string CopyName { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004C10 File Offset: 0x00002E10
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00004C18 File Offset: 0x00002E18
		public BlockModeMessageStream.SenderPosition Position { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004C21 File Offset: 0x00002E21
		public LogCopyStatus LogCopyStatus
		{
			get
			{
				return this.logCopyStatus;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004C29 File Offset: 0x00002E29
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004C31 File Offset: 0x00002E31
		public bool CompressionDesired { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004C3A File Offset: 0x00002E3A
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004C42 File Offset: 0x00002E42
		private BlockModeCollector Collector { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004C4B File Offset: 0x00002E4B
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00004C53 File Offset: 0x00002E53
		private NetworkChannel NetChannel { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004C5C File Offset: 0x00002E5C
		private Stream NetStream
		{
			get
			{
				return this.NetChannel.TcpChannel.Stream;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004C6E File Offset: 0x00002E6E
		public void PassiveIsReady(NetworkChannel channelToPassive)
		{
			this.NetChannel = channelToPassive;
			this.CompressionDesired = channelToPassive.NetworkPath.Compress;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004C88 File Offset: 0x00002E88
		public void Close()
		{
			if (this.NetChannel != null)
			{
				this.NetChannel.Close();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public BlockModeSender.WriteStatus TryStartWrite()
		{
			bool flag = false;
			bool flag2 = false;
			BlockModeSender.WriteStatus result;
			try
			{
				while (this.NetChannel != null)
				{
					if (this.writeIsActive)
					{
						BlockModeSender.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TryStartWrite({0}) did nothing because a pending write is active", this.CopyName);
						return BlockModeSender.WriteStatus.WritePending;
					}
					if (this.Position.NextSendOffset < this.Position.CurrentBuffer.AppendOffset)
					{
						BlockModeSender.IoReq ioReq = new BlockModeSender.IoReq();
						ioReq.Length = this.Position.CurrentBuffer.AppendOffset - this.Position.NextSendOffset;
						BlockModeSender.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "TryStartWrite({0}) sending {1} bytes", this.CopyName, ioReq.Length);
						flag2 = true;
						this.writeIsActive = true;
						this.passiveRequestingAck = false;
						Stopwatch stopwatch = Stopwatch.StartNew();
						IAsyncResult asyncResult = this.NetStream.BeginWrite(this.Position.CurrentBuffer.Buffer, this.Position.NextSendOffset, ioReq.Length, new AsyncCallback(this.CompletionOfWrite), ioReq);
						flag = true;
						long elapsedTicks = stopwatch.ElapsedTicks;
						this.perfInstance.AverageWriteTime.IncrementBy(elapsedTicks);
						this.perfInstance.AverageWriteTimeBase.Increment();
						this.perfInstance.TotalBytesSent.IncrementBy((long)ioReq.Length);
						this.perfInstance.TotalNetworkWrites.Increment();
						this.perfInstance.WritesPerSec.Increment();
						this.perfInstance.WriteThruput.IncrementBy((long)ioReq.Length);
						this.perfInstance.AverageWriteSize.IncrementBy((long)ioReq.Length);
						this.perfInstance.AverageWriteSizeBase.Increment();
						if (asyncResult.CompletedSynchronously)
						{
							BlockModeSender.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TryStartWrite({0}) sync completion, so we might want to keep looping", this.CopyName);
						}
						return BlockModeSender.WriteStatus.DataWritten;
					}
					if (this.Position.CurrentBuffer.NextBuffer != null)
					{
						if (this.Position.NextSendOffset >= this.Position.CurrentBuffer.AppendOffset)
						{
							this.Position.CurrentBuffer = this.Position.CurrentBuffer.NextBuffer;
							this.Position.NextSendOffset = 0;
						}
					}
					else
					{
						if (this.passiveRequestingAck)
						{
							BlockModeSender.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TryStartWrite({0}) sending ping", this.CopyName);
							new PingMessage(this.NetChannel)
							{
								ReplyAckCounter = this.passiveRequestingAckLastTimestamp
							}.Send();
							this.passiveRequestingAck = false;
							return BlockModeSender.WriteStatus.PingSent;
						}
						BlockModeSender.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TryStartWrite({0}) did nothing because all data has already been sent", this.CopyName);
						return BlockModeSender.WriteStatus.AlreadySent;
					}
				}
				BlockModeSender.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TryStartWrite({0}) did nothing because this passive is joining", this.CopyName);
				result = BlockModeSender.WriteStatus.WritePending;
			}
			finally
			{
				if (flag2 && !flag)
				{
					this.writeIsActive = false;
				}
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004FA4 File Offset: 0x000031A4
		public BlockModeSender.ActiveReadRequest StartRead()
		{
			NetworkChannel netChannel = this.NetChannel;
			BlockModeSender.ActiveReadRequest activeReadRequest = new BlockModeSender.ActiveReadRequest(this, netChannel);
			bool flag = false;
			try
			{
				netChannel.StartRead(new NetworkChannelCallback(BlockModeSender.ReadCallback), activeReadRequest);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					BlockModeSender.Tracer.TraceError((long)this.GetHashCode(), "Failed to starting async read");
				}
			}
			return activeReadRequest;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005004 File Offset: 0x00003204
		private static void ReadCallback(object asyncState, int bytesAvailable, bool completionIsSynchronous, Exception e)
		{
			BlockModeSender.ActiveReadRequest activeReadRequest = (BlockModeSender.ActiveReadRequest)asyncState;
			activeReadRequest.Sender.ProcessReadCallback(activeReadRequest, bytesAvailable, completionIsSynchronous, e);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000506C File Offset: 0x0000326C
		private void CompletionOfWrite(IAsyncResult ar)
		{
			BlockModeSender.IoReq ioReq = (BlockModeSender.IoReq)ar.AsyncState;
			Exception ex = NetworkChannel.RunNetworkFunction(delegate
			{
				this.NetStream.EndWrite(ar);
				this.Position.NextSendOffset += ioReq.Length;
			});
			this.writeIsActive = false;
			if (ex != null)
			{
				BlockModeSender.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "CompletionOfWrite({0}) failed: {1}", this.CopyName, ex);
				this.Collector.HandleSenderFailed(this, ex);
				return;
			}
			if (!ar.CompletedSynchronously)
			{
				BlockModeSender.Tracer.TraceDebug<string>((long)this.GetHashCode(), "CompletionOfWrite({0}) is trying to trigger more writes", this.CopyName);
				this.Collector.TryStartWrites();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005350 File Offset: 0x00003550
		private void ProcessReadCallback(BlockModeSender.ActiveReadRequest ioReq, int bytesAvailable, bool completionIsSynchronous, Exception readEx)
		{
			bool ackRequested = false;
			if (completionIsSynchronous)
			{
				ioReq.CompletedSynchronously = completionIsSynchronous;
			}
			Exception ex;
			if (readEx != null)
			{
				BlockModeSender.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "ProcessReadCallback({0}) read failed: ex={1}", this.DatabaseName, readEx);
				ex = new NetworkCommunicationException(this.PassiveName, readEx.Message, readEx);
			}
			else if (bytesAvailable == 0)
			{
				BlockModeSender.Tracer.TraceError<string>((long)this.GetHashCode(), "ProcessReadCallback({0}) passive closed connection", this.DatabaseName);
				ex = new NetworkEndOfDataException(this.PassiveName, Strings.NetworkReadEOF);
			}
			else
			{
				ex = NetworkChannel.RunNetworkFunction(delegate
				{
					NetworkChannelMessage message = ioReq.Channel.GetMessage();
					PassiveStatusMsg passiveStatusMsg = message as PassiveStatusMsg;
					if (passiveStatusMsg == null)
					{
						string text = string.Format("Active received unexpected message from copy '{0}'. MsgType={1}", this.CopyName, message.Type);
						BlockModeSender.Tracer.TraceError((long)this.GetHashCode(), text);
						throw new NetworkUnexpectedMessageException(this.PassiveName, text);
					}
					BlockModeSender.Tracer.TraceDebug((long)this.GetHashCode(), "PassiveStatusMsg({0}) gen 0x{1:X} sent at {2}UTC flags 0x{3:X}", new object[]
					{
						this.CopyName,
						passiveStatusMsg.GenInNetworkBuffer,
						passiveStatusMsg.MessageUtc,
						(long)passiveStatusMsg.FlagsUsed
					});
					if (BitMasker.IsOn64((long)passiveStatusMsg.FlagsUsed, 1L))
					{
						this.passiveRequestingAckLastTimestamp = passiveStatusMsg.AckCounter;
						this.passiveRequestingAck = true;
						ackRequested = true;
					}
					else if (BitMasker.IsOn64((long)passiveStatusMsg.FlagsUsed, 2L))
					{
						long systemPerformanceCounter = Win32StopWatch.GetSystemPerformanceCounter();
						long num = Win32StopWatch.ComputeElapsedTimeInUSec(systemPerformanceCounter, passiveStatusMsg.AckCounter) / 1000L;
						BlockModeSender.Tracer.TracePerformance<long, uint>((long)this.GetHashCode(), "RoundTripLatency: {0} mSec to gen 0x{1:X}", num, passiveStatusMsg.GenInNetworkBuffer);
						this.perfInstance.AverageWriteAckLatency.IncrementBy(num);
						this.perfInstance.AverageWriteAckLatencyBase.Increment();
					}
					this.perfInstance.AcknowledgedGenerationNumber.RawValue = (long)((ulong)passiveStatusMsg.GenInNetworkBuffer);
					this.logCopyStatus = new LogCopyStatus(CopyType.BlockModePassive, this.PassiveName, passiveStatusMsg.IsCrossSite, (ulong)passiveStatusMsg.GenInNetworkBuffer, (ulong)passiveStatusMsg.LastGenInspected, (ulong)passiveStatusMsg.LastGenReplayed);
					this.Collector.TriggerThrottlingUpdate();
					if (!completionIsSynchronous)
					{
						BlockModeSender.ActiveReadRequest activeReadRequest;
						do
						{
							activeReadRequest = this.StartRead();
						}
						while (activeReadRequest.CompletedSynchronously);
						return;
					}
					BlockModeSender.Tracer.TraceDebug((long)this.GetHashCode(), "Recursive read avoided.");
				});
			}
			if (ex != null)
			{
				this.Collector.HandleSenderFailed(this, ex);
				return;
			}
			if (ackRequested)
			{
				this.Collector.StartWrites();
			}
		}

		// Token: 0x04000054 RID: 84
		private volatile bool writeIsActive;

		// Token: 0x04000055 RID: 85
		private volatile bool passiveRequestingAck;

		// Token: 0x04000056 RID: 86
		private long passiveRequestingAckLastTimestamp;

		// Token: 0x04000057 RID: 87
		private ActiveDatabaseSenderPerformanceCountersInstance perfInstance;

		// Token: 0x04000058 RID: 88
		private LogCopyStatus logCopyStatus;

		// Token: 0x02000010 RID: 16
		public enum WriteStatus
		{
			// Token: 0x04000061 RID: 97
			WritePending = 1,
			// Token: 0x04000062 RID: 98
			DataWritten,
			// Token: 0x04000063 RID: 99
			PingSent,
			// Token: 0x04000064 RID: 100
			AlreadySent
		}

		// Token: 0x02000011 RID: 17
		internal class ActiveReadRequest
		{
			// Token: 0x06000096 RID: 150 RVA: 0x00005447 File Offset: 0x00003647
			public ActiveReadRequest(BlockModeSender sender, NetworkChannel channel)
			{
				this.Channel = channel;
				this.Sender = sender;
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000097 RID: 151 RVA: 0x0000545D File Offset: 0x0000365D
			// (set) Token: 0x06000098 RID: 152 RVA: 0x00005465 File Offset: 0x00003665
			public NetworkChannel Channel { get; set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000099 RID: 153 RVA: 0x0000546E File Offset: 0x0000366E
			// (set) Token: 0x0600009A RID: 154 RVA: 0x00005476 File Offset: 0x00003676
			public BlockModeSender Sender { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600009B RID: 155 RVA: 0x0000547F File Offset: 0x0000367F
			// (set) Token: 0x0600009C RID: 156 RVA: 0x00005487 File Offset: 0x00003687
			public bool CompletedSynchronously { get; set; }
		}

		// Token: 0x02000012 RID: 18
		private class IoReq
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x0600009D RID: 157 RVA: 0x00005490 File Offset: 0x00003690
			// (set) Token: 0x0600009E RID: 158 RVA: 0x00005498 File Offset: 0x00003698
			public int Length { get; set; }
		}
	}
}
