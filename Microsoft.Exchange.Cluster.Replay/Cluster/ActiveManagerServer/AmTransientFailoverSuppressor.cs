using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000097 RID: 151
	internal class AmTransientFailoverSuppressor
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001E848 File Offset: 0x0001CA48
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001E850 File Offset: 0x0001CA50
		internal Dictionary<AmServerName, AmFailoverEntry> EntryMap { get; set; }

		// Token: 0x0600062E RID: 1582 RVA: 0x0001E859 File Offset: 0x0001CA59
		internal static bool CheckIfMajorityNodesReachable(out int totalServersCount, out int successfulReplyCount)
		{
			return AmTransientFailoverSuppressor.CheckIfMajorityNodesReachable(null, out totalServersCount, out successfulReplyCount);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001E864 File Offset: 0x0001CA64
		internal static bool CheckIfMajorityNodesReachable(AmServerName[] members, out int totalServersCount, out int successfulReplyCount)
		{
			bool flag = false;
			totalServersCount = 0;
			successfulReplyCount = 0;
			if (members == null)
			{
				AmLastKnownGoodConfig lastKnownGoodConfig = AmSystemManager.Instance.LastKnownGoodConfig;
				if (lastKnownGoodConfig != null && (lastKnownGoodConfig.Role == AmRole.PAM || lastKnownGoodConfig.Role == AmRole.SAM))
				{
					members = lastKnownGoodConfig.Members;
				}
			}
			if (members != null && members.Length > 0)
			{
				totalServersCount = members.Length;
				Stopwatch stopwatch = new Stopwatch();
				try
				{
					stopwatch.Start();
					AmMultiNodeRoleFetcher amMultiNodeRoleFetcher = new AmMultiNodeRoleFetcher(members.ToList<AmServerName>(), TimeSpan.FromSeconds((double)RegistryParameters.MajorityDecisionRpcTimeoutInSec), true);
					amMultiNodeRoleFetcher.Run();
					return amMultiNodeRoleFetcher.IsMajoritySuccessfulRepliesReceived(out totalServersCount, out successfulReplyCount);
				}
				finally
				{
					ReplayCrimsonEvents.MajorityNodeCheckCompleted.Log<TimeSpan, bool, int, int>(stopwatch.Elapsed, flag, totalServersCount, successfulReplyCount);
				}
			}
			ReplayCrimsonEvents.MajorityNodeNotAttemptedSinceNoMembersAvailable.Log();
			return flag;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001E918 File Offset: 0x0001CB18
		internal AmTransientFailoverSuppressor()
		{
			this.EntryMap = new Dictionary<AmServerName, AmFailoverEntry>();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001E938 File Offset: 0x0001CB38
		internal void Initialize()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsPAM)
			{
				lock (this.m_locker)
				{
					AmServerName[] memberServers = config.DagConfig.MemberServers;
					foreach (AmServerName amServerName in memberServers)
					{
						AmFailoverEntry amFailoverEntry = AmFailoverEntry.ReadFromPersistentStoreBestEffort(amServerName);
						if (amFailoverEntry != null)
						{
							if (!config.DagConfig.IsNodePubliclyUp(amServerName))
							{
								this.AddEntry(amFailoverEntry);
							}
							else
							{
								AmTrace.Debug("Skipped adding server to deferred failover. Removing from persistent store (server: {0})", new object[]
								{
									amServerName
								});
								amFailoverEntry.DeleteFromPersistentStoreBestEffort();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		internal List<AmDeferredRecoveryEntry> GetEntriesForTask()
		{
			List<AmDeferredRecoveryEntry> list = new List<AmDeferredRecoveryEntry>();
			lock (this.m_locker)
			{
				Dictionary<AmServerName, AmFailoverEntry> entryMap = AmSystemManager.Instance.TransientFailoverSuppressor.EntryMap;
				foreach (KeyValuePair<AmServerName, AmFailoverEntry> keyValuePair in entryMap)
				{
					AmFailoverEntry value = keyValuePair.Value;
					string recoveryDueTimeInUtc = (value.TimeCreated + value.Delay).ToString("o");
					string fqdn = value.ServerName.Fqdn;
					AmDeferredRecoveryEntry item = new AmDeferredRecoveryEntry(fqdn, recoveryDueTimeInUtc);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001EAD0 File Offset: 0x0001CCD0
		internal bool IsEntryExist(AmServerName serverName)
		{
			bool result = false;
			lock (this.m_locker)
			{
				AmFailoverEntry amFailoverEntry = null;
				result = this.EntryMap.TryGetValue(serverName, out amFailoverEntry);
			}
			return result;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001EB20 File Offset: 0x0001CD20
		internal int EntriesCount
		{
			get
			{
				int count;
				lock (this.m_locker)
				{
					count = this.EntryMap.Count;
				}
				return count;
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001EB68 File Offset: 0x0001CD68
		internal void AddEntry(AmDbActionReason reasonCode, AmServerName serverName)
		{
			if (AmSystemManager.Instance.Config.IsPAM)
			{
				this.AddEntry(new AmFailoverEntry(reasonCode, serverName)
				{
					TimeCreated = ExDateTime.Now,
					Delay = TimeSpan.FromSeconds((double)RegistryParameters.TransientFailoverSuppressionDelayInSec)
				});
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
		private bool AddEntry(AmFailoverEntry failoverEntry)
		{
			bool result = false;
			if (AmSystemManager.Instance.Config.IsPAM)
			{
				failoverEntry.Timer = new Timer(new TimerCallback(this.InitiateFailoverIfRequired), failoverEntry.ServerName, TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0));
				lock (this.m_locker)
				{
					if (!this.IsEntryExist(failoverEntry.ServerName))
					{
						result = true;
						ReplayCrimsonEvents.AddedDelayedFailoverEntry.Log<AmServerName, AmDbActionReason, ExDateTime, TimeSpan>(failoverEntry.ServerName, failoverEntry.ReasonCode, failoverEntry.TimeCreated, failoverEntry.Delay);
						this.EntryMap[failoverEntry.ServerName] = failoverEntry;
						failoverEntry.WriteToPersistentStoreBestEffort();
						failoverEntry.Timer.Change(failoverEntry.Delay, TimeSpan.FromMilliseconds(-1.0));
					}
					else
					{
						ReplayCrimsonEvents.EntryAlredayExistForDelayedFailover.Log<AmServerName, ExDateTime>(failoverEntry.ServerName, failoverEntry.TimeCreated + failoverEntry.Delay);
					}
				}
			}
			return result;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001ECCC File Offset: 0x0001CECC
		private bool RemoveEntryInternal(AmServerName serverName, bool isRemoveFromClusdb)
		{
			bool result = false;
			AmFailoverEntry amFailoverEntry = null;
			if (this.EntryMap.TryGetValue(serverName, out amFailoverEntry))
			{
				result = true;
				this.EntryMap.Remove(serverName);
				if (amFailoverEntry.Timer != null)
				{
					amFailoverEntry.Timer.Dispose();
				}
				if (isRemoveFromClusdb)
				{
					amFailoverEntry.DeleteFromPersistentStoreBestEffort();
				}
			}
			return result;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001ED1C File Offset: 0x0001CF1C
		internal bool RemoveEntry(AmServerName serverName, bool isRemoveFromClusdb, string hint)
		{
			bool flag = false;
			lock (this.m_locker)
			{
				flag = this.RemoveEntryInternal(serverName, isRemoveFromClusdb);
			}
			if (flag)
			{
				ReplayCrimsonEvents.RemovingDelayedFailoverEntry.Log<AmServerName, string>(serverName, hint);
			}
			return flag;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001ED94 File Offset: 0x0001CF94
		internal void RemoveAllEntries(bool isRemoveFromClusdb)
		{
			lock (this.m_locker)
			{
				List<AmServerName> list = this.EntryMap.Keys.ToList<AmServerName>();
				list.ForEach(delegate(AmServerName serverName)
				{
					this.RemoveEntryInternal(serverName, isRemoveFromClusdb);
				});
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001EE10 File Offset: 0x0001D010
		internal void InitiateFailoverIfRequired(object stateInfo)
		{
			AmServerName amServerName = (AmServerName)stateInfo;
			lock (this.m_locker)
			{
				if (AmSystemManager.Instance.Config.IsPAM)
				{
					AmFailoverEntry amFailoverEntry = null;
					if (this.EntryMap.TryGetValue(amServerName, out amFailoverEntry))
					{
						AmNodeState nodeState = AmSystemManager.Instance.Config.DagConfig.GetNodeState(amFailoverEntry.ServerName);
						if (nodeState != AmNodeState.Up)
						{
							AmEvtNodeDownForLongTime amEvtNodeDownForLongTime = new AmEvtNodeDownForLongTime(amFailoverEntry.ServerName);
							amEvtNodeDownForLongTime.Notify();
						}
						else
						{
							ReplayCrimsonEvents.DelayedFailoverSkippedSinceNodeIsUp.Log<AmServerName>(amServerName);
						}
					}
					this.RemoveEntryInternal(amServerName, true);
				}
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
		internal bool AdminRequestedForRemoval(AmServerName serverName, string hint)
		{
			return this.RemoveEntry(serverName, true, hint);
		}

		// Token: 0x0400028C RID: 652
		private object m_locker = new object();
	}
}
