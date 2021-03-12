using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.HA;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.HA;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.BlockMode
{
	// Token: 0x02000006 RID: 6
	internal class BlockModeCollector
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00003000 File Offset: 0x00001200
		public BlockModeCollector(Guid dbGuid, string dbName)
		{
			this.DatabaseGuid = dbGuid;
			this.DatabaseName = dbName;
			this.PreallocateIoResources();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00003061 File Offset: 0x00001261
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.BlockModeCollectorTracer;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00003068 File Offset: 0x00001268
		public static bool IsDebugTraceEnabled
		{
			get
			{
				return ExTraceGlobals.BlockModeCollectorTracer.IsTraceEnabled(TraceType.DebugTrace);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00003075 File Offset: 0x00001275
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000307D File Offset: 0x0000127D
		public Guid DatabaseGuid { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00003086 File Offset: 0x00001286
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000308E File Offset: 0x0000128E
		public string DatabaseName { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003098 File Offset: 0x00001298
		public ThrottlingData ThrottlingData
		{
			get
			{
				ThrottlingUpdater throttlingUpdater = this.throttling;
				if (throttlingUpdater != null)
				{
					return throttlingUpdater.ThrottlingData;
				}
				return null;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000030B7 File Offset: 0x000012B7
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000030BF File Offset: 0x000012BF
		private ISimpleBufferPool SocketStreamBufferPool { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000030C8 File Offset: 0x000012C8
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000030D0 File Offset: 0x000012D0
		private IPool<SocketStreamAsyncArgs> SocketStreamAsyncArgPool { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000030D9 File Offset: 0x000012D9
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000030E1 File Offset: 0x000012E1
		private BlockModeMessageStream MsgStream { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x000030EC File Offset: 0x000012EC
		public void PrepareToMountAsActive(JET_INSTANCE jetInstance)
		{
			BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PrepareToMountAsActive({0})", this.DatabaseName);
			using (LockManager.Lock(this.senderLock, LockManager.LockType.BlockModeSender))
			{
				this.HookupCallback(jetInstance);
				this.PrepareForActivation();
			}
			this.StartThrottling();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003158 File Offset: 0x00001358
		public void PrepareToMountAsPassive(JET_INSTANCE jetInstance)
		{
			BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PrepareToMountAsPassive({0})", this.DatabaseName);
			using (LockManager.Lock(this.senderLock, LockManager.LockType.BlockModeSender))
			{
				this.role = BlockModeCollector.Role.Passive;
				this.HookupCallback(jetInstance);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000031C0 File Offset: 0x000013C0
		public void PrepareToTransitionToActive()
		{
			BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PrepareToTransitionToActive({0})", this.DatabaseName);
			using (LockManager.Lock(this.senderLock, LockManager.LockType.BlockModeSender))
			{
				this.PrepareForActivation();
			}
			this.StartThrottling();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003224 File Offset: 0x00001424
		public void DismountComplete()
		{
			BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DismountComplete({0})", this.DatabaseName);
			this.DisposeCallback();
			this.StopThrottling();
			int num = 5;
			int num2 = 0;
			BlockModeCollector.Role role;
			using (LockManager.Lock(this.senderLock, LockManager.LockType.BlockModeSender))
			{
				role = this.role;
				this.role = BlockModeCollector.Role.Unknown;
				if (role == BlockModeCollector.Role.Active)
				{
					this.sendersCompleteEvent.Signal();
					num2 = this.passiveSenders.Count;
					if (num2 > 0)
					{
						this.StartWritesInternal();
					}
				}
			}
			if (role == BlockModeCollector.Role.Active)
			{
				if (num2 > 0 && !this.sendersCompleteEvent.Wait(num * 1000))
				{
					BlockModeCollector.Tracer.TraceError((long)this.GetHashCode(), "Dismount proceeds without confirming all data was sent");
				}
				using (LockManager.Lock(this.senderLock, LockManager.LockType.BlockModeSender))
				{
					BlockModeSender[] array = new BlockModeSender[this.passiveSenders.Count];
					this.passiveSenders.Values.CopyTo(array, 0);
					this.passiveSenders.Clear();
					foreach (BlockModeSender blockModeSender in array)
					{
						blockModeSender.Close();
						if (ActiveDatabaseSenderPerformanceCounters.InstanceExists(blockModeSender.CopyName))
						{
							ActiveDatabaseSenderPerformanceCounters.RemoveInstance(blockModeSender.CopyName);
						}
					}
					this.sendersCompleteEvent.Dispose();
					this.sendersCompleteEvent = null;
					this.MsgStream = null;
				}
				if (ActiveDatabasePerformanceCounters.InstanceExists(this.DatabaseName))
				{
					ActiveDatabasePerformanceCounters.RemoveInstance(this.DatabaseName);
				}
				this.perfInstance = null;
			}
			IDisposable disposable = this.SocketStreamAsyncArgPool as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000033DC File Offset: 0x000015DC
		public void HandleSenderFailed(BlockModeSender sender, Exception ex)
		{
			bool flag = false;
			if (!LockManager.TestLock(this.senderLock, LockManager.LockType.BlockModeSender))
			{
				LockManager.GetLock(this.senderLock, LockManager.LockType.BlockModeSender);
			}
			else
			{
				flag = true;
			}
			try
			{
				BlockModeSender blockModeSender = null;
				if (this.passiveSenders.TryGetValue(sender.PassiveName, out blockModeSender))
				{
					if (blockModeSender == sender)
					{
						this.RemoveSender(sender);
						this.UpdateOldestSenderPosition();
					}
					else
					{
						BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "failing sender to passive {0} is old and not removed", sender.PassiveName);
					}
				}
			}
			finally
			{
				sender.Close();
				if (!flag)
				{
					LockManager.ReleaseLock(this.senderLock, LockManager.LockType.BlockModeSender);
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000347C File Offset: 0x0000167C
		public void StartReplicationToPassive(string passiveName, uint firstGenToSend)
		{
			bool flag = false;
			bool flag2 = false;
			BlockModeSender blockModeSender = null;
			BlockModeSender blockModeSender2 = null;
			NetworkChannel networkChannel = null;
			string text = string.Format("{0}\\{1}", this.DatabaseName, passiveName);
			BlockModeCollector.Tracer.TraceDebug<string, uint>((long)this.GetHashCode(), "StartReplicationToPassive ({0}) firstGen 0x{1:X}", text, firstGenToSend);
			try
			{
				using (LockManager.Lock(this.senderLock, LockManager.LockType.BlockModeSender))
				{
					if (this.passiveSenders.TryGetValue(passiveName, out blockModeSender))
					{
						BlockModeCollector.Tracer.TraceError<string>((long)this.GetHashCode(), "EnterBlockMode({0}) replacing old sender", text);
						this.RemoveSender(blockModeSender);
					}
					if (this.role != BlockModeCollector.Role.Active)
					{
						string message = string.Format("The local copy is not yet active. This is a race that will resolve itself. Passive requester is {0}", passiveName);
						BlockModeCollector.Tracer.TraceError((long)this.GetHashCode(), message);
						throw new GranularReplicationInitFailedException(message);
					}
					BlockModeMessageStream.SenderPosition senderPosition = this.MsgStream.Join(firstGenToSend);
					if (senderPosition == null)
					{
						string message2 = string.Format("Sender failed to join: passiveName({0}) genToSend(0x{1:X})", passiveName, firstGenToSend);
						BlockModeCollector.Tracer.TraceError((long)this.GetHashCode(), message2);
						throw new GranularReplicationInitFailedException(message2);
					}
					blockModeSender2 = new BlockModeSender(passiveName, this, senderPosition);
					this.passiveSenders.Add(passiveName, blockModeSender2);
					this.BuildPassiveSendersList();
					this.sendersCompleteEvent.AddCount();
				}
				BlockModeCollector.SocketStreamPerfCtrs perfCtrs = new BlockModeCollector.SocketStreamPerfCtrs(text);
				networkChannel = NetworkChannel.OpenChannel(passiveName, this.SocketStreamBufferPool, this.SocketStreamAsyncArgPool, perfCtrs, true);
				EnterBlockModeMsg enterBlockModeMsg = new EnterBlockModeMsg(networkChannel, EnterBlockModeMsg.Flags.None, this.DatabaseGuid, (long)((ulong)firstGenToSend));
				enterBlockModeMsg.Send();
				NetworkChannelMessage message3 = networkChannel.GetMessage();
				if (!(message3 is EnterBlockModeMsg))
				{
					string text2 = string.Format("Passive({0}) failed to ack", passiveName);
					BlockModeCollector.Tracer.TraceError((long)this.GetHashCode(), text2);
					throw new NetworkUnexpectedMessageException(this.DatabaseName, text2);
				}
				using (LockManager.Lock(this.senderLock, LockManager.LockType.BlockModeSender))
				{
					blockModeSender2.PassiveIsReady(networkChannel);
					flag2 = true;
					this.CheckCompression();
					blockModeSender2.StartRead();
					flag = true;
					this.StartWritesInternal();
				}
			}
			finally
			{
				if (!flag)
				{
					BlockModeCollector.Tracer.TraceError<string>((long)this.GetHashCode(), "Failed to setup repl with passive {0}", passiveName);
					if (blockModeSender2 != null)
					{
						this.HandleSenderFailed(blockModeSender2, null);
						if (!flag2 && networkChannel != null)
						{
							networkChannel.Close();
						}
					}
				}
				if (blockModeSender != null)
				{
					blockModeSender.Close();
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000036F0 File Offset: 0x000018F0
		public void MarkWritesShouldBeAttempted()
		{
			this.writesShouldBeAttemptedFlag = true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000036FC File Offset: 0x000018FC
		public void TryStartWrites()
		{
			if (LockManager.TryGetLock(this.senderLock, LockManager.LockType.BlockModeSender))
			{
				try
				{
					this.StartWritesInternal();
					return;
				}
				finally
				{
					LockManager.ReleaseLock(this.senderLock, LockManager.LockType.BlockModeSender);
				}
			}
			BlockModeCollector.Tracer.TraceDebug((long)this.GetHashCode(), "TryStartWrites: lock conflict, exitting");
			this.MarkWritesShouldBeAttempted();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000375C File Offset: 0x0000195C
		public void StartWrites()
		{
			bool flag = false;
			if (!LockManager.TestLock(this.senderLock, LockManager.LockType.BlockModeSender))
			{
				LockManager.GetLock(this.senderLock, LockManager.LockType.BlockModeSender);
			}
			else
			{
				flag = true;
			}
			try
			{
				this.StartWritesInternal();
			}
			finally
			{
				if (!flag)
				{
					LockManager.ReleaseLock(this.senderLock, LockManager.LockType.BlockModeSender);
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000037B4 File Offset: 0x000019B4
		internal void TriggerThrottlingUpdate()
		{
			if (this.lastLogGenerated > 0UL)
			{
				LogCopyStatus activeCopyLogGenerationInfo = new LogCopyStatus(CopyType.Active, LocalHost.Fqdn, false, this.lastLogGenerated, 0UL, 0UL);
				List<BlockModeSender> list = this.passiveSendersForThrottling;
				List<LogCopyStatus> list2 = new List<LogCopyStatus>(list.Count);
				foreach (BlockModeSender blockModeSender in list)
				{
					list2.Add(blockModeSender.LogCopyStatus);
				}
				ThrottlingUpdater throttlingUpdater = this.throttling;
				if (throttlingUpdater != null)
				{
					throttlingUpdater.TriggerUpdate(activeCopyLogGenerationInfo, list2, this.lastLogGeneratedTimeUtc);
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003858 File Offset: 0x00001A58
		private void PrepareForActivation()
		{
			this.AllocateMessageStream();
			this.sendersCompleteEvent = new CountdownEvent(1);
			this.perfInstance = ActiveDatabasePerformanceCounters.GetInstance(this.DatabaseName);
			this.role = BlockModeCollector.Role.Active;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003884 File Offset: 0x00001A84
		private void RemoveSender(BlockModeSender sender)
		{
			this.passiveSenders.Remove(sender.PassiveName);
			this.sendersCompleteEvent.Signal();
			this.BuildPassiveSendersList();
			this.CheckCompression();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000038B0 File Offset: 0x00001AB0
		private void HookupCallback(JET_INSTANCE jetInstance)
		{
			IntPtr emitContext = new IntPtr(42);
			this.emitCallback = new EmitLogDataCallback(jetInstance, new JET_PFNEMITLOGDATA(this.EmitCallback), emitContext);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000038DF File Offset: 0x00001ADF
		private void DisposeCallback()
		{
			if (this.emitCallback != null)
			{
				this.emitCallback.Dispose();
				this.emitCallback = null;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000038FB File Offset: 0x00001AFB
		private void StartThrottling()
		{
			BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "StartThrottling({0})", this.DatabaseName);
			this.throttling = new ThrottlingUpdater(this.DatabaseGuid);
			this.throttling.Start();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003935 File Offset: 0x00001B35
		private void StopThrottling()
		{
			BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "StopThrottling({0})", this.DatabaseName);
			if (this.throttling != null)
			{
				this.throttling.Stop();
				this.throttling = null;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003970 File Offset: 0x00001B70
		private void BuildPassiveSendersList()
		{
			List<BlockModeSender> list = new List<BlockModeSender>(this.passiveSenders.Values.Count);
			foreach (BlockModeSender item in this.passiveSenders.Values)
			{
				list.Add(item);
			}
			this.passiveSendersForThrottling = list;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003D4C File Offset: 0x00001F4C
		private JET_err EmitCallback(JET_INSTANCE instance, JET_EMITDATACTX emitContext, byte[] logdata, int logdataLength, object callbackctx)
		{
			BlockModeCollector.<>c__DisplayClass1 CS$<>8__locals1 = new BlockModeCollector.<>c__DisplayClass1();
			CS$<>8__locals1.emitContext = emitContext;
			CS$<>8__locals1.logdata = logdata;
			CS$<>8__locals1.logdataLength = logdataLength;
			CS$<>8__locals1.<>4__this = this;
			WatsonOnUnhandledException.Guard(NullExecutionDiagnostics.Instance, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<EmitCallback>b__0)));
			return JET_err.Success;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003D93 File Offset: 0x00001F93
		private void AllocateMessageStream()
		{
			this.MsgStream = new BlockModeMessageStream(this.DatabaseName, 2, this.bigWriteBuffers);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003DB0 File Offset: 0x00001FB0
		private void PreallocateIoResources()
		{
			IRegistryReader instance = RegistryReader.Instance;
			int value = instance.GetValue<int>(Registry.LocalMachine, "Software\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "MaxBlockModeSendDepthInMB", 0);
			long num;
			if (value != 0)
			{
				num = (long)(value * 1048576);
			}
			else
			{
				using (Context context = Context.CreateForSystem())
				{
					ServerInfo serverInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetServerInfo(context);
					num = serverInfo.ContinuousReplicationMaxMemoryPerDatabase;
				}
			}
			num = Math.Min(104857600L, num);
			num = Math.Max(3145728L, num);
			this.maxBuffersToKeepForSenders = (int)(num / 1048576L);
			BlockModeCollector.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Max Sender Depth is {0} buffers", this.maxBuffersToKeepForSenders);
			int numberOfBuffersToPreAllocate = 6;
			this.bigWriteBuffers = new BlockModeMessageStream.FreeIOBuffers(1048576, numberOfBuffersToPreAllocate);
			int num2 = 4;
			int num3 = 1;
			int num4 = 2;
			int num5 = 1;
			int preAllocCount = num2 * num3 * num5 * num4;
			int bufSize = 65536;
			this.SocketStreamBufferPool = new SimpleBufferPool(bufSize, preAllocCount);
			this.SocketStreamAsyncArgPool = new SocketStreamAsyncArgsPool(preAllocCount);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003ED0 File Offset: 0x000020D0
		private void StartWritesInternal()
		{
			do
			{
				this.writesShouldBeAttemptedFlag = false;
				BlockModeSender[] array = new BlockModeSender[this.passiveSenders.Count];
				this.passiveSenders.Values.CopyTo(array, 0);
				BlockModeSender[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					BlockModeSender sender = array2[i];
					if (this.CheckSenderForOverflow(sender))
					{
						Exception ex = NetworkChannel.RunNetworkFunction(delegate
						{
							sender.TryStartWrite();
						});
						if (ex != null)
						{
							this.HandleSenderFailed(sender, ex);
						}
					}
				}
				this.UpdateOldestSenderPosition();
			}
			while (this.writesShouldBeAttemptedFlag);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003F7C File Offset: 0x0000217C
		private bool CheckSenderForOverflow(BlockModeSender sender)
		{
			ulong num = this.MsgStream.LatestBufferLifetimeOrdinal - sender.Position.CurrentBuffer.LifetimeOrdinal;
			if (num <= (ulong)((long)this.maxBuffersToKeepForSenders))
			{
				return true;
			}
			BlockModeCollector.Tracer.TraceError<string, ulong>((long)this.GetHashCode(), "Sender({0}) overflowed with depth {1}", sender.CopyName, num);
			ReplayCrimsonEvents.BlockModeOverflowOnActive.Log<string, string>(this.DatabaseName, sender.PassiveName);
			this.perfInstance.BlockModeOverflows.Increment();
			this.RemoveSender(sender);
			sender.Close();
			return false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004004 File Offset: 0x00002204
		private void UpdateOldestSenderPosition()
		{
			ulong num = ulong.MaxValue;
			foreach (BlockModeSender blockModeSender in this.passiveSenders.Values)
			{
				ulong lifetimeOrdinal = blockModeSender.Position.CurrentBuffer.LifetimeOrdinal;
				if (lifetimeOrdinal < num)
				{
					num = lifetimeOrdinal;
				}
			}
			this.MsgStream.OldestBufferLifetimeReferencedBySenders = num;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000407C File Offset: 0x0000227C
		private void CheckCompression()
		{
			bool flag = false;
			DagNetConfig dagNetConfig = DagNetEnvironment.FetchLastKnownNetConfig();
			if (dagNetConfig != null)
			{
				switch (dagNetConfig.NetworkCompression)
				{
				case NetworkOption.Enabled:
				case NetworkOption.InterSubnetOnly:
					using (Dictionary<string, BlockModeSender>.ValueCollection.Enumerator enumerator = this.passiveSenders.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							BlockModeSender blockModeSender = enumerator.Current;
							if (blockModeSender.CompressionDesired)
							{
								BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Passive {0} wants compression.", blockModeSender.CopyName);
								flag = true;
								break;
							}
						}
						goto IL_B2;
					}
					break;
				}
				BlockModeCollector.Tracer.TraceDebug((long)this.GetHashCode(), "DagNet does not enable compression.");
			}
			else
			{
				BlockModeCollector.Tracer.TraceError((long)this.GetHashCode(), "Failed to get dagnet config. Compression disabled.");
			}
			IL_B2:
			if (flag)
			{
				if (!this.MsgStream.CompressLogData)
				{
					BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Compression is being enabled for {0}.", this.DatabaseName);
					this.MsgStream.CompressLogData = true;
					this.perfInstance.CompressionEnabled.RawValue = 1L;
					return;
				}
			}
			else if (this.MsgStream.CompressLogData)
			{
				BlockModeCollector.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Compression is being disabled for {0}.", this.DatabaseName);
				this.MsgStream.CompressLogData = false;
				this.perfInstance.CompressionEnabled.RawValue = 0L;
			}
		}

		// Token: 0x0400001F RID: 31
		private readonly object senderLock = new object();

		// Token: 0x04000020 RID: 32
		private readonly object appendLock = new object();

		// Token: 0x04000021 RID: 33
		private EmitLogDataCallback emitCallback;

		// Token: 0x04000022 RID: 34
		private Dictionary<string, BlockModeSender> passiveSenders = new Dictionary<string, BlockModeSender>(3);

		// Token: 0x04000023 RID: 35
		private BlockModeCollector.Role role;

		// Token: 0x04000024 RID: 36
		private volatile bool writesShouldBeAttemptedFlag;

		// Token: 0x04000025 RID: 37
		private CountdownEvent sendersCompleteEvent;

		// Token: 0x04000026 RID: 38
		private int maxBuffersToKeepForSenders;

		// Token: 0x04000027 RID: 39
		private ActiveDatabasePerformanceCountersInstance perfInstance;

		// Token: 0x04000028 RID: 40
		private ThrottlingUpdater throttling;

		// Token: 0x04000029 RID: 41
		private ulong lastLogGenerated;

		// Token: 0x0400002A RID: 42
		private DateTime? lastLogGeneratedTimeUtc = null;

		// Token: 0x0400002B RID: 43
		private List<BlockModeSender> passiveSendersForThrottling = new List<BlockModeSender>(3);

		// Token: 0x0400002C RID: 44
		private BlockModeMessageStream.FreeIOBuffers bigWriteBuffers;

		// Token: 0x02000007 RID: 7
		private enum Role
		{
			// Token: 0x04000033 RID: 51
			Unknown,
			// Token: 0x04000034 RID: 52
			Passive,
			// Token: 0x04000035 RID: 53
			Active
		}

		// Token: 0x02000008 RID: 8
		internal class SocketStreamPerfCtrs : SocketStream.ISocketStreamPerfCounters
		{
			// Token: 0x06000045 RID: 69 RVA: 0x000041E0 File Offset: 0x000023E0
			public SocketStreamPerfCtrs(string passiveName)
			{
				this.perfInstance = ActiveDatabaseSenderPerformanceCounters.GetInstance(passiveName);
			}

			// Token: 0x06000046 RID: 70 RVA: 0x000041F4 File Offset: 0x000023F4
			public void RecordWriteLatency(long tics)
			{
				long incrementValue = StopwatchStamp.TicksToMicroSeconds(tics);
				this.perfInstance.AverageSocketWriteLatency.IncrementBy(incrementValue);
				this.perfInstance.AverageSocketWriteLatencyBase.Increment();
			}

			// Token: 0x04000036 RID: 54
			private ActiveDatabaseSenderPerformanceCountersInstance perfInstance;
		}
	}
}
