using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using FUSE.Paxos;
using FUSE.Paxos.Esent;
using FUSE.Paxos.Network;
using FUSE.Weld.Base;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x02000053 RID: 83
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	public class DxStoreInstance : IDxStoreInstance
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x0000577C File Offset: 0x0000397C
		public DxStoreInstance(InstanceGroupConfig groupConfig, IDxStoreEventLogger eventLogger)
		{
			this.Identity = groupConfig.Identity;
			this.IdentityHash = groupConfig.Identity.GetHashCode();
			this.EventLogger = eventLogger;
			this.GroupConfig = groupConfig;
			this.LocalProcessInfo = new ProcessBasicInfo(true);
			this.StopEvent = new ManualResetEvent(false);
			this.StopCompletedEvent = new ManualResetEvent(false);
			this.State = InstanceState.Initialized;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000057F0 File Offset: 0x000039F0
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.InstanceTracer;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000057F7 File Offset: 0x000039F7
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x000057FF File Offset: 0x000039FF
		public string Identity { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00005808 File Offset: 0x00003A08
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00005810 File Offset: 0x00003A10
		public int IdentityHash { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00005819 File Offset: 0x00003A19
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00005821 File Offset: 0x00003A21
		public IDxStoreEventLogger EventLogger { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000582A File Offset: 0x00003A2A
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00005832 File Offset: 0x00003A32
		public ProcessBasicInfo LocalProcessInfo { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000583B File Offset: 0x00003A3B
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00005843 File Offset: 0x00003A43
		public ManualResetEvent StopEvent { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000584C File Offset: 0x00003A4C
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00005854 File Offset: 0x00003A54
		public ManualResetEvent StopCompletedEvent { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000585D File Offset: 0x00003A5D
		public bool IsStopping
		{
			get
			{
				return this.State == InstanceState.Stopping;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00005868 File Offset: 0x00003A68
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00005870 File Offset: 0x00003A70
		public WCF.ServiceHostCommon InstanceServiceHost { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00005879 File Offset: 0x00003A79
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00005881 File Offset: 0x00003A81
		public WCF.ServiceHostCommon InstanceDefaultGroupServiceHost { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000588A File Offset: 0x00003A8A
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00005892 File Offset: 0x00003A92
		public ServiceHost ClientAccessServiceHost { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000589B File Offset: 0x00003A9B
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x000058A3 File Offset: 0x00003AA3
		public ServiceHost ClientAccessDefaultGroupServiceHost { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000058AC File Offset: 0x00003AAC
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000058B4 File Offset: 0x00003AB4
		public InstanceGroupConfig GroupConfig { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000058BD File Offset: 0x00003ABD
		// (set) Token: 0x060002ED RID: 749 RVA: 0x000058C5 File Offset: 0x00003AC5
		public ILocalDataStore LocalDataStore { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000058CE File Offset: 0x00003ACE
		// (set) Token: 0x060002EF RID: 751 RVA: 0x000058D6 File Offset: 0x00003AD6
		public DxStoreAccess StoreAccess { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000058DF File Offset: 0x00003ADF
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x000058E7 File Offset: 0x00003AE7
		public DxStoreStateMachine StateMachine { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000058F0 File Offset: 0x00003AF0
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x000058F8 File Offset: 0x00003AF8
		public SnapshotManager SnapshotManager { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00005901 File Offset: 0x00003B01
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00005909 File Offset: 0x00003B09
		public DxStoreHealthChecker HealthChecker { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00005912 File Offset: 0x00003B12
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000591A File Offset: 0x00003B1A
		public LocalCommitAcknowledger CommitAcknowledger { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00005923 File Offset: 0x00003B23
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000592B File Offset: 0x00003B2B
		public InstanceState State { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00005934 File Offset: 0x00003B34
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000593C File Offset: 0x00003B3C
		public InstanceClientFactory InstanceClientFactory { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00005945 File Offset: 0x00003B45
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000594D File Offset: 0x00003B4D
		public AccessClientFactory AccessClientFactory { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00005956 File Offset: 0x00003B56
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000595E File Offset: 0x00003B5E
		public bool IsStartupCompleted { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00005967 File Offset: 0x00003B67
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000596F File Offset: 0x00003B6F
		public Counters PerfCounters { get; set; }

		// Token: 0x06000302 RID: 770 RVA: 0x000059B0 File Offset: 0x00003BB0
		public static bool RemoveGroupStorage(IDxStoreEventLogger eventLogger, InstanceGroupConfig group)
		{
			bool flag;
			if (Directory.Exists(group.Settings.PaxosStorageDir))
			{
				flag = (Utils.RunOperation(group.Identity, "Removing Paxos directory", delegate
				{
					Directory.Delete(group.Settings.PaxosStorageDir, true);
				}, eventLogger, LogOptions.LogException | LogOptions.LogStart | LogOptions.LogSuccess | LogOptions.LogPeriodic | group.Settings.AdditionalLogOptions, true, null, null, null, null, null) == null);
			}
			else
			{
				flag = true;
			}
			bool flag2;
			if (Directory.Exists(group.Settings.SnapshotStorageDir))
			{
				flag2 = (Utils.RunOperation(group.Identity, "Removing Snapshot directory", delegate
				{
					Directory.Delete(group.Settings.SnapshotStorageDir, true);
				}, eventLogger, LogOptions.LogException | LogOptions.LogStart | LogOptions.LogSuccess | LogOptions.LogPeriodic | group.Settings.AdditionalLogOptions, true, null, null, null, null, null) == null);
			}
			else
			{
				flag2 = true;
			}
			return flag && flag2;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public void Start()
		{
			this.RunOperation("InstanceStart", new Action(this.StartInternal), LogOptions.LogAll, null, null, null, null);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00005B00 File Offset: 0x00003D00
		public void RegisterStoreInstanceServiceHost()
		{
			if (this.InstanceServiceHost == null || this.InstanceServiceHost.State != CommunicationState.Opened || this.InstanceServiceHost.State != CommunicationState.Opening)
			{
				WCF.ServiceHostCommon serviceHostCommon = new WCF.ServiceHostCommon(this, new Uri[0]);
				ServiceEndpoint storeInstanceEndpoint = this.GroupConfig.GetStoreInstanceEndpoint(this.GroupConfig.Self, false, true, null);
				this.AddEndPoint("Instance", serviceHostCommon, storeInstanceEndpoint);
				this.RunOperation("OpenStoreInstanceServiceHost", new Action(serviceHostCommon.Open), LogOptions.LogAll, null, null, null, null);
				this.InstanceServiceHost = serviceHostCommon;
				if (this.GroupConfig.IsDefaultGroup)
				{
					WCF.ServiceHostCommon serviceHostCommon2 = new WCF.ServiceHostCommon(this, new Uri[0]);
					ServiceEndpoint storeInstanceEndpoint2 = this.GroupConfig.GetStoreInstanceEndpoint(this.GroupConfig.Self, true, true, null);
					this.AddEndPoint("Instance (default)", serviceHostCommon2, storeInstanceEndpoint2);
					this.RunOperation("OpenStoreInstanceServiceHost (default)", new Action(serviceHostCommon2.Open), LogOptions.LogAll, null, null, null, null);
					this.InstanceServiceHost = serviceHostCommon2;
				}
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00005C14 File Offset: 0x00003E14
		public void RegisterStoreAccessServiceHost()
		{
			if (this.ClientAccessServiceHost == null || this.ClientAccessServiceHost.State != CommunicationState.Opened || this.ClientAccessServiceHost.State != CommunicationState.Opening)
			{
				ServiceHost serviceHost = new ServiceHost(this.StoreAccess, new Uri[0]);
				ServiceEndpoint storeAccessEndpoint = this.GroupConfig.GetStoreAccessEndpoint(this.GroupConfig.Self, false, true, null);
				this.AddEndPoint("Store Access", serviceHost, storeAccessEndpoint);
				this.RunOperation("OpenStoreAccessServiceHost", new Action(serviceHost.Open), LogOptions.LogAll, null, null, null, null);
				this.ClientAccessServiceHost = serviceHost;
				if (this.GroupConfig.IsDefaultGroup)
				{
					ServiceHost serviceHost2 = new ServiceHost(this.StoreAccess, new Uri[0]);
					ServiceEndpoint storeAccessEndpoint2 = this.GroupConfig.GetStoreAccessEndpoint(this.GroupConfig.Self, true, true, null);
					this.AddEndPoint("Store Access (default)", serviceHost2, storeAccessEndpoint2);
					this.RunOperation("OpenStoreAccessServiceHost (default)", new Action(serviceHost2.Open), LogOptions.LogAll, null, null, null, null);
					this.ClientAccessDefaultGroupServiceHost = serviceHost2;
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00005D34 File Offset: 0x00003F34
		public Exception RunBestEffortOperation(string actionLabel, Action action, LogOptions options = LogOptions.LogAll, TimeSpan? timeout = null, string periodicKey = null, TimeSpan? periodicDuration = null, Action<Exception> exitAction = null)
		{
			return Utils.RunOperation(this.Identity, actionLabel, action, this.EventLogger, options | this.GroupConfig.Settings.AdditionalLogOptions, true, timeout, new TimeSpan?(periodicDuration ?? this.GroupConfig.Settings.PeriodicTimeoutLoggingDuration), periodicKey, exitAction, null);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00005D98 File Offset: 0x00003F98
		public void RunOperation(string actionLabel, Action action, LogOptions options = LogOptions.LogAll, TimeSpan? timeout = null, string periodicKey = null, TimeSpan? periodicDuration = null, Action<Exception> exitAction = null)
		{
			Utils.RunOperation(this.Identity, actionLabel, action, this.EventLogger, options | this.GroupConfig.Settings.AdditionalLogOptions, false, timeout, new TimeSpan?(periodicDuration ?? this.GroupConfig.Settings.PeriodicTimeoutLoggingDuration), periodicKey, exitAction, null);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00005E00 File Offset: 0x00004000
		public void Stop(bool isExitProcess = true)
		{
			DxStoreInstance.Tracer.TraceDebug<string>((long)this.IdentityHash, "{0}: Stopping instance", this.Identity);
			this.State = InstanceState.Stopping;
			this.SnapshotManager.ForceFlush();
			this.SnapshotManager.Stop();
			this.StopEvent.Set();
			bool flag = this.RunBestEffortOperation("StopStateMachine", new Action(this.StopStateMachine), LogOptions.LogAll, new TimeSpan?(this.GroupConfig.Settings.StateMachineStopTimeout), null, null, null) == null;
			this.StopCompletedEvent.Set();
			if (isExitProcess)
			{
				int exitCode = flag ? 99 : 0;
				DxStoreInstance.Tracer.TraceWarning<string, int>((long)this.IdentityHash, "{0}: Will be exiting process after {1}ms", this.Identity, 500);
				Utils.ExitProcessDelayed(TimeSpan.FromMilliseconds(500.0), exitCode);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00005EDC File Offset: 0x000040DC
		public void CloseServiceHosts()
		{
			if (this.ClientAccessServiceHost != null)
			{
				this.RunBestEffortOperation("StoreAccessServiceHostClose", new Action(this.ClientAccessServiceHost.Close), LogOptions.LogAll, new TimeSpan?(this.GroupConfig.Settings.ServiceHostCloseTimeout), null, null, null);
				this.ClientAccessServiceHost = null;
			}
			if (this.ClientAccessDefaultGroupServiceHost != null)
			{
				this.RunBestEffortOperation("StoreAccessServiceHostClose (default group)", new Action(this.ClientAccessDefaultGroupServiceHost.Close), LogOptions.LogAll, new TimeSpan?(this.GroupConfig.Settings.ServiceHostCloseTimeout), null, null, null);
				this.ClientAccessDefaultGroupServiceHost = null;
			}
			if (this.InstanceServiceHost != null)
			{
				this.RunBestEffortOperation("StoreInstanceServiceHostClose", new Action(this.InstanceServiceHost.Close), LogOptions.LogAll, new TimeSpan?(this.GroupConfig.Settings.ServiceHostCloseTimeout), null, null, null);
				this.InstanceServiceHost = null;
			}
			if (this.InstanceDefaultGroupServiceHost != null)
			{
				this.RunBestEffortOperation("StoreInstanceServiceHostClose (default group)", new Action(this.InstanceDefaultGroupServiceHost.Close), LogOptions.LogAll, new TimeSpan?(this.GroupConfig.Settings.ServiceHostCloseTimeout), null, null, null);
				this.InstanceDefaultGroupServiceHost = null;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00006020 File Offset: 0x00004220
		public void ApplySnapshot(InstanceSnapshotInfo snapshot, bool isForce = false)
		{
			this.EventLogger.Log(isForce ? DxEventSeverity.Warning : DxEventSeverity.Info, 0, "{0}: Applying snapshot (isForce={1})", new object[]
			{
				this.Identity,
				isForce
			});
			if (isForce)
			{
				this.LocalDataStore.ApplySnapshot(snapshot, null);
				return;
			}
			this.EnsureInstanceIsReady();
			snapshot.Compress();
			DxStoreCommand.ApplySnapshot command = new DxStoreCommand.ApplySnapshot
			{
				SnapshotInfo = snapshot
			};
			this.StateMachine.ReplicateCommand(command, null);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000060AC File Offset: 0x000042AC
		public InstanceSnapshotInfo AcquireSnapshot(string fullKeyName, bool isCompress)
		{
			this.EnsureInstanceIsReady();
			bool isStale = !this.HealthChecker.IsStoreReady();
			InstanceSnapshotInfo snapshot = this.LocalDataStore.GetSnapshot(fullKeyName, isCompress);
			if (snapshot != null)
			{
				snapshot.IsStale = isStale;
			}
			return snapshot;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000060E7 File Offset: 0x000042E7
		public void TryBecomeLeader()
		{
			this.StateMachine.BecomeLeader(this.GroupConfig.Settings.LeaderPromotionTimeout);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00006104 File Offset: 0x00004304
		public void Flush()
		{
			if (this.SnapshotManager != null)
			{
				this.SnapshotManager.ForceFlush();
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00006119 File Offset: 0x00004319
		public void Reconfigure(InstanceGroupMemberConfig[] members)
		{
			this.EnsureInstanceIsReady();
			this.GroupConfig.Members = members;
			this.StateMachine.Reconfigure(members);
			this.GroupConfig.Members = members;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00006148 File Offset: 0x00004348
		public InstanceStatusInfo GetStatus()
		{
			if (this.SnapshotManager == null || !this.SnapshotManager.IsInitialLoadAttempted)
			{
				throw new DxStoreInstanceNotReadyException(this.State.ToString());
			}
			InstanceStatusInfo instanceStatusInfo = new InstanceStatusInfo();
			instanceStatusInfo.State = this.State;
			instanceStatusInfo.Self = this.GroupConfig.Self;
			instanceStatusInfo.MemberConfigs = this.GroupConfig.Members;
			if (this.LocalDataStore != null)
			{
				instanceStatusInfo.LastInstanceExecuted = this.LocalDataStore.LastInstanceExecuted;
			}
			if (this.StateMachine != null)
			{
				instanceStatusInfo.PaxosInfo = this.StateMachine.GetPaxosInfo();
			}
			instanceStatusInfo.HostProcessInfo = this.LocalProcessInfo;
			if (this.CommitAcknowledger != null)
			{
				instanceStatusInfo.CommitAckOldestItemTime = this.CommitAcknowledger.OldestItemTime;
			}
			return instanceStatusInfo;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000620C File Offset: 0x0000440C
		public void NotifyInitiator(Guid commandId, string sender, int instanceNumber, bool isSucceeded, string errorMessage)
		{
			this.CommitAcknowledger.HandleAcknowledge(commandId, sender);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000621C File Offset: 0x0000441C
		public Task PaxosMessageAsync(string sender, Message message)
		{
			Task result;
			try
			{
				if (this.StateMachine != null && this.StateMachine.Mesh != null)
				{
					this.StateMachine.Mesh.Incoming.OnNext(Tuple.Create<string, Message>(sender, message));
				}
				else
				{
					DxStoreInstance.Tracer.TraceError<string>(0L, "{0}: PaxosMessageAsync skipped since state machine is not configured yet.", this.GroupConfig.Identity);
				}
				result = TaskFactoryExtensions.FromResult<object>(Task.Factory, null);
			}
			catch (Exception ex)
			{
				result = TaskFactoryExtensions.FromException(Task.Factory, ex);
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000062A8 File Offset: 0x000044A8
		public void EnsureInstanceIsReady()
		{
			if (!this.IsInstanceInReadyState())
			{
				throw new DxStoreInstanceNotReadyException(this.State.ToString());
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000062C8 File Offset: 0x000044C8
		public bool IsInstanceInReadyState()
		{
			return this.State == InstanceState.Running;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000062D4 File Offset: 0x000044D4
		private HttpReply HandleIncomingMessage(HttpRequest msg)
		{
			if (msg is HttpRequest.PaxosMessage)
			{
				this.HandleIncomingPaxosMessage(msg as HttpRequest.PaxosMessage);
				return null;
			}
			if (msg is HttpRequest.GetStatusRequest)
			{
				return new HttpReply.GetInstanceStatusReply(this.GetStatus());
			}
			if (msg is HttpRequest.DxStoreRequest)
			{
				return this.HandleStoreAccess(msg as HttpRequest.DxStoreRequest);
			}
			return new HttpReply.ExceptionReply(new Exception(string.Format("Unknown request: {0}", msg.GetType().FullName)));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00006340 File Offset: 0x00004540
		private HttpReply HandleStoreAccess(HttpRequest.DxStoreRequest req)
		{
			if (req.Request is DxStoreAccessRequest.CheckKey)
			{
				return new HttpReply.DxStoreReply(this.StoreAccess.CheckKey(req.Request as DxStoreAccessRequest.CheckKey));
			}
			if (req.Request is DxStoreAccessRequest.DeleteKey)
			{
				return new HttpReply.DxStoreReply(this.StoreAccess.DeleteKey(req.Request as DxStoreAccessRequest.DeleteKey));
			}
			if (req.Request is DxStoreAccessRequest.SetProperty)
			{
				return new HttpReply.DxStoreReply(this.StoreAccess.SetProperty(req.Request as DxStoreAccessRequest.SetProperty));
			}
			if (req.Request is DxStoreAccessRequest.DeleteProperty)
			{
				return new HttpReply.DxStoreReply(this.StoreAccess.DeleteProperty(req.Request as DxStoreAccessRequest.DeleteProperty));
			}
			if (req.Request is DxStoreAccessRequest.ExecuteBatch)
			{
				return new HttpReply.DxStoreReply(this.StoreAccess.ExecuteBatch(req.Request as DxStoreAccessRequest.ExecuteBatch));
			}
			if (req.Request is DxStoreAccessRequest.GetProperty)
			{
				return new HttpReply.DxStoreReply(this.StoreAccess.GetProperty(req.Request as DxStoreAccessRequest.GetProperty));
			}
			if (req.Request is DxStoreAccessRequest.GetAllProperties)
			{
				return new HttpReply.DxStoreReply(this.StoreAccess.GetAllProperties(req.Request as DxStoreAccessRequest.GetAllProperties));
			}
			if (req.Request is DxStoreAccessRequest.GetPropertyNames)
			{
				return new HttpReply.DxStoreReply(this.StoreAccess.GetPropertyNames(req.Request as DxStoreAccessRequest.GetPropertyNames));
			}
			if (req.Request is DxStoreAccessRequest.GetSubkeyNames)
			{
				return new HttpReply.DxStoreReply(this.StoreAccess.GetSubkeyNames(req.Request as DxStoreAccessRequest.GetSubkeyNames));
			}
			return new HttpReply.ExceptionReply(new Exception(string.Format("Unknown StoreAccess request: {0}", req.Request.GetType().FullName)));
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000064E4 File Offset: 0x000046E4
		private void HandleIncomingPaxosMessage(HttpRequest.PaxosMessage msg)
		{
			try
			{
				if (this.StateMachine != null && this.StateMachine.Mesh != null)
				{
					this.StateMachine.Mesh.Incoming.OnNext(Tuple.Create<string, Message>(msg.Sender, msg.Message));
				}
				else
				{
					DxStoreInstance.Tracer.TraceError<string>(0L, "{0}: HandleIncomingMessage skipped since state machine is not configured yet.", this.GroupConfig.Identity);
				}
			}
			catch (Exception arg)
			{
				DxStoreInstance.Tracer.TraceError<string, Exception>(0L, "{0}: HandleIncomingMessage caught {1}", this.GroupConfig.Identity, arg);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00006588 File Offset: 0x00004788
		private void StartInternal()
		{
			this.State = InstanceState.Starting;
			if (this.GroupConfig.Settings.StartupDelay != TimeSpan.Zero)
			{
				this.EventLogger.Log(DxEventSeverity.Info, 0, "Delaying instance startup for {0}", new object[]
				{
					this.GroupConfig.Settings.StartupDelay
				});
				Thread.Sleep(this.GroupConfig.Settings.StartupDelay);
			}
			this.CreateClientFactories();
			this.LocalDataStore = new LocalMemoryStore(this.Identity);
			this.SnapshotManager = new SnapshotManager(this);
			this.SnapshotManager.InitializeDataStore();
			this.StoreAccess = new DxStoreAccess(this);
			this.RegisterStoreAccessServiceHost();
			Microsoft.Exchange.DxStore.Common.EventLogger.Instance = this.EventLogger;
			if (this.GroupConfig.Settings.IsUseHttpTransportForInstanceCommunication)
			{
				HttpConfiguration.Configure(this.GroupConfig);
				this.httpListener = new DxStoreHttpListener(new Func<HttpRequest, HttpReply>(this.HandleIncomingMessage));
				Exception ex;
				if (!this.httpListener.StartListening(this.GroupConfig.Self, this.GroupConfig.Name, "B1563499-EA40-4101-A9E6-59A8EB26FF1E", out ex))
				{
					string text = string.Format("DxStoreHttpListener startup fails: {0}", ex);
					DxStoreInstance.Tracer.TraceError(0L, text);
					Microsoft.Exchange.DxStore.Common.EventLogger.LogErr(text, new object[0]);
					throw ex;
				}
			}
			this.RegisterStoreInstanceServiceHost();
			this.CommitAcknowledger = new LocalCommitAcknowledger(this);
			this.HealthChecker = new DxStoreHealthChecker(this);
			this.majorityNotificationSubscription = ObservableExtensions.Subscribe<GroupStatusInfo>(this.HealthChecker.WhenMajority, delegate(GroupStatusInfo gsi)
			{
				this.WhenHealthCheckerSeeMajorityOfNodes(gsi);
			});
			this.HealthChecker.Start();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000067A0 File Offset: 0x000049A0
		private void WhenHealthCheckerSeeMajorityOfNodes(GroupStatusInfo gsi)
		{
			if (this.IsStartupCompleted)
			{
				return;
			}
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = this.instanceLock, ref flag);
				bool isWaitForNextRound = false;
				bool flag2 = gsi.Lag > this.GroupConfig.Settings.MaxAllowedLagToCatchup;
				DxStoreInstance.Tracer.TraceDebug((long)this.IdentityHash, "{0}: Instance start - Majority replied (LocalInstance# {1}, Lag: {2}, CatchupLimit: {3}", new object[]
				{
					this.Identity,
					(gsi.LocalInstance != null) ? gsi.LocalInstance.InstanceNumber : -1,
					gsi.Lag,
					this.GroupConfig.Settings.MaxAllowedLagToCatchup
				});
				if (flag2)
				{
					isWaitForNextRound = true;
				}
				DxStoreInstanceClient client = this.InstanceClientFactory.GetClient(gsi.HighestInstance.NodeName);
				InstanceSnapshotInfo snapshotInfo = null;
				this.RunBestEffortOperation("GetSnapshot :" + gsi.HighestInstance.NodeName, delegate
				{
					snapshotInfo = client.AcquireSnapshot(null, true, null);
				}, LogOptions.LogAll, null, null, null, null);
				if (snapshotInfo != null && snapshotInfo.LastInstanceExecuted > gsi.LocalInstance.InstanceNumber)
				{
					this.RunBestEffortOperation("Apply local snapshot", delegate
					{
						this.SnapshotManager.ApplySnapshot(snapshotInfo, true);
						isWaitForNextRound = false;
					}, LogOptions.LogAll, null, null, null, null);
				}
				if (!isWaitForNextRound)
				{
					this.majorityNotificationSubscription.Dispose();
					this.majorityNotificationSubscription = null;
					this.HealthChecker.ChangeTimerDuration(this.GroupConfig.Settings.GroupHealthCheckDuration);
					Round<string>? leaderHint = null;
					if (gsi.IsLeaderExist)
					{
						leaderHint = new Round<string>?(gsi.LeaderHint);
					}
					this.StateMachine = this.CreateStateMachine(leaderHint, null);
					Task task = this.StartStateMachine();
					task.ContinueWith(delegate(Task t)
					{
						this.EventLogger.Log(DxEventSeverity.Info, 0, "Successfully started state machine", new object[0]);
						this.SnapshotManager.Start();
						this.State = InstanceState.Running;
						this.IsStartupCompleted = true;
					});
				}
				else
				{
					DxStoreInstance.Tracer.TraceWarning<string>((long)this.IdentityHash, "{0}: Instance start - waiting for the next round to start local instance", this.Identity);
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00006A04 File Offset: 0x00004C04
		private Dictionary<string, ServiceEndpoint> GetMembersInstanceClientEndPoints(string[] members)
		{
			Dictionary<string, ServiceEndpoint> dictionary = new Dictionary<string, ServiceEndpoint>();
			foreach (string text in members)
			{
				ServiceEndpoint storeInstanceEndpoint = this.GroupConfig.GetStoreInstanceEndpoint(text, false, false, this.GroupConfig.Settings.StoreInstanceWcfTimeout);
				dictionary[text] = storeInstanceEndpoint;
			}
			return dictionary;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00006A68 File Offset: 0x00004C68
		private DxStoreStateMachine CreateStateMachine(Round<string>? leaderHint, PaxosBasicInfo referencePaxos)
		{
			Policy policy = new Policy
			{
				DebugName = this.GroupConfig.Self
			};
			bool flag = false;
			string[] array = (from m in this.GroupConfig.Members
			select m.Name).ToArray<string>();
			if (referencePaxos != null && referencePaxos.IsMember(this.GroupConfig.Self))
			{
				array = referencePaxos.Members;
				flag = true;
			}
			IDxStoreEventLogger eventLogger = this.EventLogger;
			DxEventSeverity severity = DxEventSeverity.Info;
			int id = 0;
			string formatString = "{0}: Creating state machine with membership '{1}' (IsUsingReferencePaxos: {2}, IsReferencePaxosLeader: {3}, GroupConfig.Members: {4}";
			object[] array2 = new object[5];
			array2[0] = this.GroupConfig.Identity;
			array2[1] = array.JoinWithComma("<null>");
			array2[2] = flag;
			array2[3] = (flag ? referencePaxos.IsLeader.ToString() : "<unknown>");
			array2[4] = (from m in this.GroupConfig.Members
			select m.Name).JoinWithComma("<null>");
			eventLogger.Log(severity, id, formatString, array2);
			Dictionary<string, ServiceEndpoint> membersInstanceClientEndPoints = this.GetMembersInstanceClientEndPoints(array);
			NodeEndPointsBase<ServiceEndpoint> nodeEndPointsBase = new NodeEndPointsBase<ServiceEndpoint>(this.GroupConfig.Self, membersInstanceClientEndPoints);
			this.PerfCounters = new Counters(this.GroupConfig.Identity);
			Configuration<string> configuration = new Configuration<string>(nodeEndPointsBase.Nodes, nodeEndPointsBase.Nodes, nodeEndPointsBase.Nodes, null);
			GroupMembersMesh mesh = new GroupMembersMesh(this.Identity, nodeEndPointsBase, this.GroupConfig);
			EsentStorage<string, DxStoreCommand> esentStorage = new EsentStorage<string, DxStoreCommand>(this.GroupConfig.Settings.PaxosStorageDir, this.PerfCounters, null, null, true);
			esentStorage.TryInitialize(configuration);
			return new DxStoreStateMachine(policy, this, nodeEndPointsBase, esentStorage, mesh, this.PerfCounters, leaderHint);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00006C30 File Offset: 0x00004E30
		private Task StartStateMachine()
		{
			string text = string.Join(",", (from m in this.GroupConfig.Members
			select m.Name).ToArray<string>());
			this.EventLogger.Log(DxEventSeverity.Info, 0, "Starting state machine with {0} (LastInstanceExecuted = {1})", new object[]
			{
				text,
				this.LocalDataStore.LastInstanceExecuted
			});
			int num = this.LocalDataStore.LastInstanceExecuted;
			if (num > 0)
			{
				num++;
			}
			return this.StateMachine.StartAsync(num);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00006CCB File Offset: 0x00004ECB
		private void StopStateMachine()
		{
			if (this.StateMachine != null)
			{
				this.StateMachine.Stop();
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00006CE0 File Offset: 0x00004EE0
		private void CreateClientFactories()
		{
			this.InstanceClientFactory = new InstanceClientFactory(this.GroupConfig, null);
			this.AccessClientFactory = new AccessClientFactory(this.GroupConfig, null);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00006D08 File Offset: 0x00004F08
		private void AddEndPoint(string name, ServiceHost svcHost, ServiceEndpoint endPoint)
		{
			this.EventLogger.Log(DxEventSeverity.Info, 0, "Added endpoint {0} to service host {1}", new object[]
			{
				endPoint.Address.Uri,
				name
			});
			svcHost.AddServiceEndpoint(endPoint);
		}

		// Token: 0x04000189 RID: 393
		private object instanceLock = new object();

		// Token: 0x0400018A RID: 394
		private IDisposable majorityNotificationSubscription;

		// Token: 0x0400018B RID: 395
		private DxStoreHttpListener httpListener;
	}
}
