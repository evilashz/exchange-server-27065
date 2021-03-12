using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000216 RID: 534
	internal abstract class BudgetManager
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001295 RID: 4757
		protected abstract TimeSpan BudgetTimeout { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0003BFA6 File Offset: 0x0003A1A6
		protected IDictionary<string, IPowerShellBudget> Budgets
		{
			get
			{
				return this.budgets;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0003BFAE File Offset: 0x0003A1AE
		protected object InstanceLock
		{
			get
			{
				return this.instanceLock;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0003BFC8 File Offset: 0x0003A1C8
		internal int TotalActiveUsers
		{
			get
			{
				int result;
				lock (this.InstanceLock)
				{
					result = this.Budgets.Count((KeyValuePair<string, IPowerShellBudget> x) => x.Value.TotalActiveRunspacesCount > 0);
				}
				return result;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0003C03C File Offset: 0x0003A23C
		internal int TotalActiveRunspaces
		{
			get
			{
				int result;
				lock (this.InstanceLock)
				{
					result = this.Budgets.Sum((KeyValuePair<string, IPowerShellBudget> x) => x.Value.TotalActiveRunspacesCount);
				}
				return result;
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0003C0A0 File Offset: 0x0003A2A0
		internal string GetWSManBudgetUsage(AuthZPluginUserToken userToken)
		{
			string result;
			lock (this.instanceLock)
			{
				IPowerShellBudget budget = this.GetBudget(userToken, false, false);
				if (budget == null)
				{
					result = null;
				}
				else
				{
					result = budget.GetWSManBudgetUsage();
				}
			}
			return result;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0003C0F4 File Offset: 0x0003A2F4
		internal bool CheckOverBudget(AuthZPluginUserToken userToken, CostType costType, out OverBudgetException exception)
		{
			exception = null;
			if (!this.ShouldThrottling(userToken))
			{
				return false;
			}
			lock (this.instanceLock)
			{
				IPowerShellBudget budget = this.GetBudget(userToken, false, false);
				if (budget != null)
				{
					return budget.TryCheckOverBudget(costType, out exception);
				}
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Try to check overbudget for key {0}. But the budget doesn't exist.", this.CreateKey(userToken));
			}
			return false;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0003C170 File Offset: 0x0003A370
		internal void RemoveBudgetIfNoActiveRunspace(AuthZPluginUserToken userToken)
		{
			lock (this.instanceLock)
			{
				IPowerShellBudget budget = this.GetBudget(userToken, false, false);
				if (budget != null && budget.TotalActiveRunspacesCount <= 0)
				{
					string text = this.CreateKey(userToken);
					Unlimited<uint> powerShellMaxRunspacesTimePeriod = budget.ThrottlingPolicy.PowerShellMaxRunspacesTimePeriod;
					if (powerShellMaxRunspacesTimePeriod.IsUnlimited)
					{
						ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Key {0} is removed from budgets dictionary immediately.", text);
						this.budgets.Remove(text);
						this.keyToRemoveBudgets.Remove(text);
					}
					else
					{
						int num = (int)(powerShellMaxRunspacesTimePeriod.Value + 5U);
						ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string, int>(0L, "Register key {0} to keyToRemoveBudgets, timeout {1} seconds.", text, num);
						this.keyToRemoveBudgets.InsertAbsolute(text, BudgetManager.NormalCleanupCacheValue, TimeSpan.FromSeconds((double)num), new RemoveItemDelegate<string, string>(this.OnKeyToRemoveBudgetsCacheValueRemoved));
					}
				}
			}
			this.UpdateBudgetsPerfCounter(this.budgets.Count);
			this.UpdateKeyToRemoveBudgetsPerfCounter(this.keyToRemoveBudgets.Count);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0003C278 File Offset: 0x0003A478
		internal void HeartBeat(AuthZPluginUserToken userToken)
		{
			lock (this.instanceLock)
			{
				this.HeartBeatImpl(userToken);
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0003C2BC File Offset: 0x0003A4BC
		internal void CorrectRunspacesLeakPassively(string key, int leakedValue)
		{
			lock (this.instanceLock)
			{
				IPowerShellBudget powerShellBudget;
				if (this.budgets.TryGetValue(key, out powerShellBudget))
				{
					int totalActiveRunspacesCount = powerShellBudget.TotalActiveRunspacesCount;
					if (totalActiveRunspacesCount > 0)
					{
						ExTraceGlobals.PublicPluginAPITracer.TraceError(0L, "Correct runspaces leak passively for Key {0} in class {1}. Current Value {2}, Leaked value {3}.", new object[]
						{
							key,
							base.GetType().ToString(),
							totalActiveRunspacesCount,
							leakedValue
						});
						AuthZLogger.SafeAppendGenericError("WSManBudgetManagerBase.CorrectRunspacesLeakPassively", string.Format("Correct runspaces leak passively for Key {0} in class {1}. Current Value {2}, Leaked value {3}.", new object[]
						{
							key,
							base.GetType(),
							totalActiveRunspacesCount,
							leakedValue
						}), false);
						TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_PSConnectionLeakPassivelyCorrected, null, new object[]
						{
							key,
							base.GetType().ToString(),
							totalActiveRunspacesCount,
							leakedValue
						});
						powerShellBudget.CorrectRunspacesLeak(leakedValue);
					}
				}
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		protected virtual CostHandle StartRunspaceImpl(AuthZPluginUserToken userToken)
		{
			IPowerShellBudget budget = this.GetBudget(userToken, true, true);
			if (budget != null)
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Start budget tracking for ActiveRunspaces, key {0}", this.CreateKey(userToken));
				return budget.StartActiveRunspace();
			}
			ExTraceGlobals.PublicPluginAPITracer.TraceError<string>(0L, "Try to start budget tracking for ActiveRunspaces, key {0} But the budget doesn't exist.", this.CreateKey(userToken));
			return null;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0003C432 File Offset: 0x0003A632
		protected virtual void HeartBeatImpl(AuthZPluginUserToken userToken)
		{
			this.GetBudget(userToken, false, true);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0003C43E File Offset: 0x0003A63E
		protected virtual bool ShouldThrottling(AuthZPluginUserToken userToken)
		{
			return true;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0003C441 File Offset: 0x0003A641
		protected virtual string CreateKey(AuthZPluginUserToken userToken)
		{
			return userToken.UserName;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0003C449 File Offset: 0x0003A649
		protected virtual IPowerShellBudget CreateBudget(AuthZPluginUserToken userToken)
		{
			return userToken.CreateBudget(BudgetType.WSMan);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0003C454 File Offset: 0x0003A654
		protected virtual void UpdateBudgetsPerfCounter(int size)
		{
			RemotePowershellPerformanceCountersInstance remotePowershellPerfCounter = ExchangeAuthorizationPlugin.RemotePowershellPerfCounter;
			if (remotePowershellPerfCounter != null)
			{
				remotePowershellPerfCounter.PerUserBudgetsDicSize.RawValue = (long)size;
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0003C478 File Offset: 0x0003A678
		protected virtual void UpdateKeyToRemoveBudgetsPerfCounter(int size)
		{
			RemotePowershellPerformanceCountersInstance remotePowershellPerfCounter = ExchangeAuthorizationPlugin.RemotePowershellPerfCounter;
			if (remotePowershellPerfCounter != null)
			{
				remotePowershellPerfCounter.PerUserKeyToRemoveBudgetsCacheSize.RawValue = (long)size;
			}
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0003C49C File Offset: 0x0003A69C
		protected virtual void UpdateConnectionLeakPerfCounter(int leakedConnection)
		{
			RemotePowershellPerformanceCountersInstance remotePowershellPerfCounter = ExchangeAuthorizationPlugin.RemotePowershellPerfCounter;
			if (remotePowershellPerfCounter != null)
			{
				remotePowershellPerfCounter.ConnectionLeak.RawValue += (long)leakedConnection;
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0003C4C6 File Offset: 0x0003A6C6
		protected virtual string CreateRelatedBudgetKey(AuthZPluginUserToken userToken)
		{
			return null;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0003C4C9 File Offset: 0x0003A6C9
		protected virtual void CorrectRelatedBudgetWhenLeak(string key, int delta)
		{
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0003C4CC File Offset: 0x0003A6CC
		protected IPowerShellBudget GetBudget(AuthZPluginUserToken userToken, bool createIfNotExist, bool isHeartBeat)
		{
			string text = this.CreateKey(userToken);
			if (text == null)
			{
				return null;
			}
			IPowerShellBudget powerShellBudget;
			if (!this.budgets.TryGetValue(text, out powerShellBudget) && createIfNotExist)
			{
				powerShellBudget = this.CreateBudget(userToken);
				if (powerShellBudget != null)
				{
					ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Budget of key {0} is added to budgets.", text);
					this.budgets.Add(text, powerShellBudget);
					this.UpdateBudgetsPerfCounter(this.budgets.Count);
				}
			}
			if (powerShellBudget != null && isHeartBeat)
			{
				this.keyToRemoveBudgets.InsertAbsolute(text, this.CreateRelatedBudgetKey(userToken), this.BudgetTimeout, new RemoveItemDelegate<string, string>(this.OnKeyToRemoveBudgetsCacheValueRemoved));
			}
			return powerShellBudget;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0003C560 File Offset: 0x0003A760
		private void OnKeyToRemoveBudgetsCacheValueRemoved(string key, string value, RemoveReason reason)
		{
			lock (this.instanceLock)
			{
				if (reason != RemoveReason.Removed)
				{
					ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string, RemoveReason>(0L, "Key {0} is removed from budgets dictionary after timeout. Remove reason = {1}", key, reason);
					if (!BudgetManager.NormalCleanupCacheValue.Equals(value))
					{
						this.RunspacesLeakDetected(key, value);
					}
					this.budgets.Remove(key);
				}
			}
			if (reason != RemoveReason.Removed)
			{
				this.UpdateBudgetsPerfCounter(this.budgets.Count);
				AuthZPluginHelper.UpdateAuthZPluginPerfCounters(this);
			}
			this.UpdateKeyToRemoveBudgetsPerfCounter(this.keyToRemoveBudgets.Count);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0003C600 File Offset: 0x0003A800
		private void RunspacesLeakDetected(string key, string relatedBudgetKey)
		{
			int num = 0;
			IPowerShellBudget powerShellBudget = null;
			if (this.budgets.TryGetValue(key, out powerShellBudget))
			{
				num = powerShellBudget.TotalActiveRunspacesCount;
			}
			if (powerShellBudget != null)
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceError<string, string, int>(0L, "Connection leak detected for Key {0} in class {1}. Leaked value {2}.", key, base.GetType().ToString(), num);
				if (num > 0)
				{
					AuthZLogger.SafeAppendGenericError("WSManBudgetManagerBase.RunspacesLeakDetected", string.Format("Connection leak detected for Key {0} in class {1}. Leaked value {2}.", key, base.GetType(), num), false);
					TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_PSConnectionLeakDetected, null, new object[]
					{
						key,
						base.GetType().ToString(),
						num
					});
					this.UpdateConnectionLeakPerfCounter(num);
				}
				powerShellBudget.Dispose();
			}
			if (num > 0 && relatedBudgetKey != null)
			{
				this.CorrectRelatedBudgetWhenLeak(relatedBudgetKey, num);
			}
		}

		// Token: 0x0400048A RID: 1162
		private const int DefaultTimeoutBufferToAdd = 5;

		// Token: 0x0400048B RID: 1163
		private readonly IDictionary<string, IPowerShellBudget> budgets = new Dictionary<string, IPowerShellBudget>();

		// Token: 0x0400048C RID: 1164
		private readonly TimeoutCache<string, string> keyToRemoveBudgets = new TimeoutCache<string, string>(20, 5000, false);

		// Token: 0x0400048D RID: 1165
		private static readonly string NormalCleanupCacheValue = Guid.NewGuid().ToString();

		// Token: 0x0400048E RID: 1166
		private readonly object instanceLock = new object();
	}
}
