using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200053D RID: 1341
	internal abstract class MultiReplicationCheck : DisposeTrackableBase
	{
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x000C2694 File Offset: 0x000C0894
		// (set) Token: 0x0600300F RID: 12303 RVA: 0x000C269C File Offset: 0x000C089C
		public bool BreakOnCriticalFailures
		{
			get
			{
				return this.m_breakOnCriticalFailures;
			}
			set
			{
				this.m_breakOnCriticalFailures = value;
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000C26A5 File Offset: 0x000C08A5
		public MultiReplicationCheck(string serverName, IEventManager eventManager, string momEventSource) : this(serverName, eventManager, momEventSource, null, null, null, 0U)
		{
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000C26B4 File Offset: 0x000C08B4
		public MultiReplicationCheck(string serverName, IEventManager eventManager, string momEventSource, IADDatabaseAvailabilityGroup dag, uint ignoreTransientErrorsThreshold) : this(serverName, eventManager, momEventSource, null, null, null, dag, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000C26D1 File Offset: 0x000C08D1
		public MultiReplicationCheck(string serverName, IEventManager eventManager, string momEventSource, List<ReplayConfiguration> replayConfigs, uint ignoreTransientErrorsThreshold) : this(serverName, eventManager, momEventSource, null, replayConfigs, null, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000C26E4 File Offset: 0x000C08E4
		public MultiReplicationCheck(string serverName, IEventManager eventManager, string momEventSource, DatabaseHealthValidationRunner validationRunner, List<ReplayConfiguration> replayConfigs, Dictionary<Guid, RpcDatabaseCopyStatus2> copyStatuses, uint ignoreTransientErrorsThreshold) : this(serverName, eventManager, momEventSource, validationRunner, replayConfigs, copyStatuses, null, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000C2704 File Offset: 0x000C0904
		public MultiReplicationCheck(string serverName, IEventManager eventManager, string momEventSource, DatabaseHealthValidationRunner validationRunner, List<ReplayConfiguration> replayConfigs, Dictionary<Guid, RpcDatabaseCopyStatus2> copyStatuses, IADDatabaseAvailabilityGroup dag, uint ignoreTransientErrorsThreshold)
		{
			this.m_ServerName = serverName;
			this.m_EventManager = eventManager;
			this.m_MomEventSource = momEventSource;
			this.m_ValidationRunner = validationRunner;
			this.m_ReplayConfigs = replayConfigs;
			this.m_CopyStatuses = copyStatuses;
			this.m_Dag = dag;
			this.m_IgnoreTransientErrorsThreshold = ignoreTransientErrorsThreshold;
			this.Initialize();
			this.BuildJaggedArray();
		}

		// Token: 0x06003015 RID: 12309
		protected abstract void Initialize();

		// Token: 0x06003016 RID: 12310 RVA: 0x000C2768 File Offset: 0x000C0968
		private void BuildJaggedArray()
		{
			int length = Enum.GetValues(typeof(CheckCategory)).Length;
			this.m_JaggedArrayChecks = new List<IReplicationCheck>[length];
			for (int i = 0; i < length; i++)
			{
				this.m_JaggedArrayChecks[i] = new List<IReplicationCheck>();
			}
			foreach (IReplicationCheck replicationCheck in this.m_Checks)
			{
				this.m_JaggedArrayChecks[(int)replicationCheck.Category].Add(replicationCheck);
			}
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000C27E0 File Offset: 0x000C09E0
		public virtual void Run()
		{
			for (int i = 0; i < this.m_JaggedArrayChecks.Length; i++)
			{
				foreach (IReplicationCheck replicationCheck in this.m_JaggedArrayChecks[i])
				{
					if (this.m_breakOnCriticalFailures)
					{
						if (!this.m_breakOnCriticalFailures)
						{
							continue;
						}
						if (i != 0)
						{
							if (this.m_CriticalCheckHasFailed)
							{
								continue;
							}
						}
					}
					try
					{
						replicationCheck.Run();
					}
					catch (ReplicationCheckHighPriorityFailedException)
					{
						this.m_CriticalCheckHasFailed = true;
					}
					catch (ReplicationCheckFailedException)
					{
					}
					catch (ReplicationCheckWarningException)
					{
					}
				}
			}
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000C2898 File Offset: 0x000C0A98
		public void LogEvents()
		{
			for (int i = 0; i < this.m_JaggedArrayChecks.Length; i++)
			{
				foreach (IReplicationCheck replicationCheck in this.m_JaggedArrayChecks[i])
				{
					ReplicationCheck replicationCheck2 = (ReplicationCheck)replicationCheck;
					if (replicationCheck2.HasRun)
					{
						replicationCheck2.LogEvents();
					}
				}
			}
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000C290C File Offset: 0x000C0B0C
		public List<ReplicationCheckOutcome> GetAllOutcomes()
		{
			List<ReplicationCheckOutcome> list = new List<ReplicationCheckOutcome>();
			for (int i = 0; i < this.m_JaggedArrayChecks.Length; i++)
			{
				foreach (IReplicationCheck replicationCheck in this.m_JaggedArrayChecks[i])
				{
					ReplicationCheck replicationCheck2 = (ReplicationCheck)replicationCheck;
					if (replicationCheck2.HasRun)
					{
						list.Add(replicationCheck2.GetCheckOutcome());
					}
				}
			}
			return list;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000C2990 File Offset: 0x000C0B90
		public List<ReplicationCheckOutputObject> GetAllOutputObjects()
		{
			List<ReplicationCheckOutputObject> list = new List<ReplicationCheckOutputObject>();
			for (int i = 0; i < this.m_JaggedArrayChecks.Length; i++)
			{
				foreach (IReplicationCheck replicationCheck in this.m_JaggedArrayChecks[i])
				{
					ReplicationCheck replicationCheck2 = (ReplicationCheck)replicationCheck;
					if (replicationCheck2.HasRun)
					{
						List<ReplicationCheckOutputObject> checkOutputObjects = replicationCheck2.GetCheckOutputObjects();
						list.AddRange(checkOutputObjects);
					}
				}
			}
			return list;
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000C2A18 File Offset: 0x000C0C18
		internal IReplicationCheck[] Checks
		{
			get
			{
				return this.m_Checks;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x0600301C RID: 12316 RVA: 0x000C2A20 File Offset: 0x000C0C20
		internal List<IReplicationCheck>[] JaggedArrayChecks
		{
			get
			{
				return this.m_JaggedArrayChecks;
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000C2A28 File Offset: 0x000C0C28
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MultiReplicationCheck>(this);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000C2A30 File Offset: 0x000C0C30
		protected override void InternalDispose(bool calledFromDispose)
		{
			foreach (ReplicationCheck replicationCheck in this.m_Checks)
			{
				if (replicationCheck != null)
				{
					replicationCheck.Dispose();
				}
			}
		}

		// Token: 0x0400222F RID: 8751
		protected IReplicationCheck[] m_Checks;

		// Token: 0x04002230 RID: 8752
		protected bool m_breakOnCriticalFailures = true;

		// Token: 0x04002231 RID: 8753
		protected IEventManager m_EventManager;

		// Token: 0x04002232 RID: 8754
		protected List<ReplayConfiguration> m_ReplayConfigs;

		// Token: 0x04002233 RID: 8755
		protected Dictionary<Guid, RpcDatabaseCopyStatus2> m_CopyStatuses;

		// Token: 0x04002234 RID: 8756
		protected IADDatabaseAvailabilityGroup m_Dag;

		// Token: 0x04002235 RID: 8757
		protected DatabaseHealthValidationRunner m_ValidationRunner;

		// Token: 0x04002236 RID: 8758
		protected string m_MomEventSource;

		// Token: 0x04002237 RID: 8759
		protected uint m_IgnoreTransientErrorsThreshold;

		// Token: 0x04002238 RID: 8760
		protected string m_ServerName;

		// Token: 0x04002239 RID: 8761
		private List<IReplicationCheck>[] m_JaggedArrayChecks;

		// Token: 0x0400223A RID: 8762
		private bool m_CriticalCheckHasFailed;
	}
}
