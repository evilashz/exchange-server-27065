using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009A2 RID: 2466
	internal abstract class Budget : IReadOnlyPropertyBag
	{
		// Token: 0x1700282D RID: 10285
		// (get) Token: 0x060071B4 RID: 29108 RVA: 0x00178953 File Offset: 0x00176B53
		// (set) Token: 0x060071B5 RID: 29109 RVA: 0x0017895A File Offset: 0x00176B5A
		public static Action<Budget, CostHandle, TimeSpan, TimeSpan> OnLeakDetectionForTest { get; set; }

		// Token: 0x1700282E RID: 10286
		// (get) Token: 0x060071B6 RID: 29110 RVA: 0x00178962 File Offset: 0x00176B62
		// (set) Token: 0x060071B7 RID: 29111 RVA: 0x00178969 File Offset: 0x00176B69
		public static Action<StringBuilder> OnLeakWatsonInfoForTest { get; set; }

		// Token: 0x1700282F RID: 10287
		// (get) Token: 0x060071B8 RID: 29112 RVA: 0x00178971 File Offset: 0x00176B71
		// (set) Token: 0x060071B9 RID: 29113 RVA: 0x00178978 File Offset: 0x00176B78
		public static Func<IThrottlingPolicy, IThrottlingPolicy> OnPolicyUpdateForTest { get; set; }

		// Token: 0x060071BA RID: 29114 RVA: 0x00178980 File Offset: 0x00176B80
		static Budget()
		{
			Budget.AllCostTypes = Budget.GetAllCostTypes();
			Budget.maximumActionTimes = Budget.GetMaxActionTimesFromConfig();
		}

		// Token: 0x060071BB RID: 29115 RVA: 0x001789F0 File Offset: 0x00176BF0
		internal Budget(BudgetKey key, IThrottlingPolicy policy)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (policy == null)
			{
				throw new ArgumentNullException("policy");
			}
			this.CreationTime = TimeProvider.UtcNow;
			this.Owner = key;
			this.percentileUsage = BudgetUsageTracker.Get(key);
			this.SetPolicy(policy, true);
		}

		// Token: 0x060071BC RID: 29116 RVA: 0x00178A77 File Offset: 0x00176C77
		internal static TimeSpan GetMaxActionTime(CostType costType)
		{
			return Budget.maximumActionTimes[costType];
		}

		// Token: 0x060071BD RID: 29117 RVA: 0x00178A84 File Offset: 0x00176C84
		internal static void SetMaxActionTime(CostType costType, TimeSpan timeout)
		{
			if (timeout.TotalMilliseconds <= 0.0)
			{
				throw new ArgumentException("Timeout must be greater than zero.");
			}
			Budget.maximumActionTimes[costType] = timeout;
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x00178AB0 File Offset: 0x00176CB0
		private static TimeSpan GetMaxActionTimeDefault(CostType costType)
		{
			if (costType == CostType.ActiveRunspace)
			{
				return Budget.defaultActiveRunspaceMaximumActionTime;
			}
			return Budget.defaultMaximumActionTime;
		}

		// Token: 0x060071BF RID: 29119 RVA: 0x00178AD0 File Offset: 0x00176CD0
		private static Dictionary<CostType, TimeSpan> GetMaxActionTimesFromConfig()
		{
			Dictionary<CostType, TimeSpan> dictionary = new Dictionary<CostType, TimeSpan>();
			foreach (CostType costType in Budget.AllCostTypes)
			{
				TimeSpan maxActionTimeDefault = Budget.GetMaxActionTimeDefault(costType);
				string text = costType + "_MaxTimeInMinutes";
				TimeSpanAppSettingsEntry timeSpanAppSettingsEntry = new TimeSpanAppSettingsEntry(text, TimeSpanUnit.Minutes, maxActionTimeDefault, ExTraceGlobals.ClientThrottlingTracer);
				if (timeSpanAppSettingsEntry.Value > TimeSpan.Zero)
				{
					dictionary[costType] = timeSpanAppSettingsEntry.Value;
				}
				else
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceError<string, TimeSpan, TimeSpan>(0L, "[Budget.GetMaxActionTimesFromConfig] Invalid value for appSetting {0}.  Read Value: {1}.  Using {2} instead.", text, timeSpanAppSettingsEntry.Value, maxActionTimeDefault);
					dictionary[costType] = maxActionTimeDefault;
				}
			}
			return dictionary;
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x00178B90 File Offset: 0x00176D90
		private static HashSet<CostType> GetAllCostTypes()
		{
			Array values = Enum.GetValues(typeof(CostType));
			HashSet<CostType> hashSet = new HashSet<CostType>();
			foreach (object obj in values)
			{
				CostType item = (CostType)obj;
				hashSet.Add(item);
			}
			return hashSet;
		}

		// Token: 0x17002830 RID: 10288
		// (get) Token: 0x060071C1 RID: 29121 RVA: 0x00178C00 File Offset: 0x00176E00
		internal ITokenBucket CasTokenBucket
		{
			get
			{
				return this.casTokenBucket;
			}
		}

		// Token: 0x17002831 RID: 10289
		// (get) Token: 0x060071C2 RID: 29122 RVA: 0x00178C08 File Offset: 0x00176E08
		internal bool IsExpired
		{
			get
			{
				return this.isExpired;
			}
		}

		// Token: 0x17002832 RID: 10290
		// (get) Token: 0x060071C3 RID: 29123 RVA: 0x00178C10 File Offset: 0x00176E10
		// (set) Token: 0x060071C4 RID: 29124 RVA: 0x00178C18 File Offset: 0x00176E18
		internal BudgetKey Owner { get; private set; }

		// Token: 0x17002833 RID: 10291
		// (get) Token: 0x060071C5 RID: 29125 RVA: 0x00178C21 File Offset: 0x00176E21
		// (set) Token: 0x060071C6 RID: 29126 RVA: 0x00178C29 File Offset: 0x00176E29
		internal SingleComponentThrottlingPolicy ThrottlingPolicy { get; private set; }

		// Token: 0x17002834 RID: 10292
		// (get) Token: 0x060071C7 RID: 29127 RVA: 0x00178C32 File Offset: 0x00176E32
		protected object SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x00178C3C File Offset: 0x00176E3C
		public LocalTimeCostHandle StartLocal(Action<CostHandle> onRelease, string callerInfo, TimeSpan preCharge)
		{
			LocalTimeCostHandle result = null;
			lock (this.SyncRoot)
			{
				this.casTokenBucket.Increment();
				result = new LocalTimeCostHandle(this, onRelease, string.Format("Caller: {0}, ThreadID: {1}, PreCharge: {2}ms", callerInfo, Environment.CurrentManagedThreadId, preCharge.TotalMilliseconds), preCharge);
			}
			return result;
		}

		// Token: 0x060071C9 RID: 29129 RVA: 0x00178CB0 File Offset: 0x00176EB0
		internal void Expire()
		{
			Dictionary<long, CostHandle> dictionary = null;
			lock (this.SyncRoot)
			{
				this.isExpired = true;
				if (this.outstandingActions.Count > 0)
				{
					dictionary = this.outstandingActions;
					this.outstandingActions = new Dictionary<long, CostHandle>();
				}
			}
			if (dictionary != null)
			{
				foreach (KeyValuePair<long, CostHandle> keyValuePair in dictionary)
				{
					keyValuePair.Value.Dispose();
				}
			}
			this.AfterExpire();
		}

		// Token: 0x060071CA RID: 29130 RVA: 0x00178D60 File Offset: 0x00176F60
		protected virtual void AfterExpire()
		{
		}

		// Token: 0x060071CB RID: 29131 RVA: 0x00178D64 File Offset: 0x00176F64
		internal void End(CostHandle costHandle)
		{
			bool flag = false;
			lock (this.SyncRoot)
			{
				flag = this.outstandingActions.Remove(costHandle.Key);
				if (flag)
				{
					this.AccountForCostHandle(costHandle);
				}
			}
			if (!flag)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceError((long)this.GetHashCode(), "[Budget.End] CostHandle was not in outstanding actions collection.  Ignoring.");
			}
		}

		// Token: 0x17002835 RID: 10293
		// (get) Token: 0x060071CC RID: 29132 RVA: 0x00178DD8 File Offset: 0x00176FD8
		public int OutstandingActionsCount
		{
			get
			{
				return this.outstandingActions.Count;
			}
		}

		// Token: 0x060071CD RID: 29133 RVA: 0x00178DE8 File Offset: 0x00176FE8
		internal void AddOutstandingAction(CostHandle costHandle)
		{
			lock (this.SyncRoot)
			{
				this.outstandingActions.Add(costHandle.Key, costHandle);
			}
		}

		// Token: 0x060071CE RID: 29134 RVA: 0x00178E34 File Offset: 0x00177034
		public virtual void CheckOverBudget()
		{
			this.CheckOverBudget(Budget.AllCostTypes);
		}

		// Token: 0x060071CF RID: 29135 RVA: 0x00178E44 File Offset: 0x00177044
		public virtual void CheckOverBudget(ICollection<CostType> costTypesToConsider)
		{
			OverBudgetException ex = null;
			if (this.TryCheckOverBudget(costTypesToConsider, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x060071D0 RID: 29136 RVA: 0x00178E60 File Offset: 0x00177060
		internal bool TryCheckOverBudget(ICollection<CostType> costTypesToConsider, out OverBudgetException exception)
		{
			bool flag = false;
			exception = null;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3670420797U, ref flag);
			return !flag && this.InternalTryCheckOverBudget(costTypesToConsider, out exception);
		}

		// Token: 0x060071D1 RID: 29137 RVA: 0x00178E95 File Offset: 0x00177095
		internal bool TryCheckOverBudget(out OverBudgetException exception)
		{
			return this.TryCheckOverBudget(Budget.AllCostTypes, out exception);
		}

		// Token: 0x060071D2 RID: 29138 RVA: 0x00178EA4 File Offset: 0x001770A4
		internal OverBudgetException CreateOverBudgetException(string reason, string policyValue, int backoffTime)
		{
			ExTraceGlobals.ClientThrottlingTracer.TraceDebug((long)this.GetHashCode(), "[Budget.CreateOverBudgetException] user '{0}' is over-budget for {1} budget part '{2}', policy Value '{3}', backoffTime: {4} msec", new object[]
			{
				this.Owner,
				this.Owner.IsServiceAccountBudget ? "service account" : "normal",
				reason,
				policyValue,
				backoffTime
			});
			return new OverBudgetException(this, reason, policyValue, backoffTime);
		}

		// Token: 0x060071D3 RID: 29139 RVA: 0x00178F0E File Offset: 0x0017710E
		internal virtual void AfterCacheHit()
		{
			this.UpdatePolicy();
			this.UpdatePercentileReference();
		}

		// Token: 0x060071D4 RID: 29140 RVA: 0x00178F1D File Offset: 0x0017711D
		private void UpdatePercentileReference()
		{
			if (this.percentileUsage == null || this.percentileUsage.Expired)
			{
				this.percentileUsage = BudgetUsageTracker.Get(this.Owner);
			}
		}

		// Token: 0x060071D5 RID: 29141 RVA: 0x00178F45 File Offset: 0x00177145
		internal virtual EffectiveThrottlingPolicy GetEffectiveThrottlingPolicy()
		{
			return this.ThrottlingPolicy.FullPolicy as EffectiveThrottlingPolicy;
		}

		// Token: 0x060071D6 RID: 29142 RVA: 0x00178F58 File Offset: 0x00177158
		internal virtual bool UpdatePolicy()
		{
			EffectiveThrottlingPolicy effectiveThrottlingPolicy = this.GetEffectiveThrottlingPolicy();
			if (effectiveThrottlingPolicy == null && Budget.OnPolicyUpdateForTest == null)
			{
				return false;
			}
			if (TimeProvider.UtcNow - this.lastPolicyCheck >= Budget.PolicyUpdateCheckInterval)
			{
				this.lastPolicyCheck = TimeProvider.UtcNow;
				IThrottlingPolicy throttlingPolicy;
				if (Budget.OnPolicyUpdateForTest != null)
				{
					throttlingPolicy = Budget.OnPolicyUpdateForTest(this.ThrottlingPolicy.FullPolicy);
				}
				else
				{
					switch (effectiveThrottlingPolicy.ThrottlingPolicyScope)
					{
					case ThrottlingPolicyScopeType.Regular:
						throttlingPolicy = ThrottlingPolicyCache.Singleton.Get(effectiveThrottlingPolicy.OrganizationId, effectiveThrottlingPolicy.Id);
						break;
					case ThrottlingPolicyScopeType.Organization:
						throttlingPolicy = ThrottlingPolicyCache.Singleton.Get(effectiveThrottlingPolicy.OrganizationId);
						break;
					case ThrottlingPolicyScopeType.Global:
						throttlingPolicy = ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
						break;
					default:
						throw new NotSupportedException(string.Format("Unsupported enum value {0}.", effectiveThrottlingPolicy.ThrottlingPolicyScope));
					}
				}
				if (!object.ReferenceEquals(effectiveThrottlingPolicy, throttlingPolicy))
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceDebug((long)this.GetHashCode(), "[Budget.UpdatePolicy] Obtained a refreshed policy from the cache.");
					this.SetPolicy(throttlingPolicy, false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060071D7 RID: 29143 RVA: 0x0017905C File Offset: 0x0017725C
		internal string GetBalanceForTrace()
		{
			if (!(this.CasTokenBucket is UnthrottledTokenBucket))
			{
				return this.CasTokenBucket.GetBalance().ToString();
			}
			return "$null";
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x00179090 File Offset: 0x00177290
		internal void SetPolicy(IThrottlingPolicy policy, bool resetBudgetValues)
		{
			lock (this.SyncRoot)
			{
				SingleComponentThrottlingPolicy singleComponentPolicy = this.GetSingleComponentPolicy(policy);
				this.ThrottlingPolicy = singleComponentPolicy;
				this.InternalUpdateCachedPolicyValues(resetBudgetValues);
			}
		}

		// Token: 0x060071D9 RID: 29145 RVA: 0x001790E0 File Offset: 0x001772E0
		protected virtual SingleComponentThrottlingPolicy GetSingleComponentPolicy(IThrottlingPolicy policy)
		{
			return new SingleComponentThrottlingPolicy(this.Owner.BudgetType, policy);
		}

		// Token: 0x17002836 RID: 10294
		// (get) Token: 0x060071DA RID: 29146 RVA: 0x001790F4 File Offset: 0x001772F4
		internal bool CanExpire
		{
			get
			{
				bool flag = false;
				lock (this.SyncRoot)
				{
					flag = (this.OutstandingActionsCount == 0 && this.IsTokenBucketFullyRecharged());
				}
				if (flag)
				{
					flag = this.InternalCanExpire;
				}
				if (!flag)
				{
					this.CheckLeakedActions();
					LookupBudgetKey lookupBudgetKey = this.Owner as LookupBudgetKey;
					if (lookupBudgetKey != null)
					{
						IThrottlingPolicy throttlingPolicy = lookupBudgetKey.Lookup();
						if (throttlingPolicy.GetIdentityString() != this.ThrottlingPolicy.FullPolicy.GetIdentityString())
						{
							ExTraceGlobals.ClientThrottlingTracer.TraceDebug<string, string, BudgetKey>((long)this.GetHashCode(), "[Budget.CanExpire] Budget throttling policy reference changed from {0} to {1}.  Updating policy link for account {2}.", this.ThrottlingPolicy.FullPolicy.GetIdentityString(), throttlingPolicy.GetIdentityString(), this.Owner);
							this.SetPolicy(throttlingPolicy, false);
						}
					}
				}
				return flag;
			}
		}

		// Token: 0x17002837 RID: 10295
		// (get) Token: 0x060071DB RID: 29147 RVA: 0x001791CC File Offset: 0x001773CC
		protected virtual bool InternalCanExpire
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060071DC RID: 29148 RVA: 0x001791D0 File Offset: 0x001773D0
		private bool IsTokenBucketFullyRecharged()
		{
			bool result;
			lock (this.SyncRoot)
			{
				if (this.ThrottlingPolicy.RechargeRate.IsUnlimited || this.ThrottlingPolicy.MaxBurst.IsUnlimited)
				{
					result = true;
				}
				else
				{
					uint value = this.ThrottlingPolicy.MaxBurst.Value;
					if (this.CasTokenBucket.GetBalance() == value && !this.CasTokenBucket.Locked)
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x00179274 File Offset: 0x00177474
		internal virtual void SetStateFromPolicyForTest(bool resetState)
		{
			this.SetPolicy(this.ThrottlingPolicy.FullPolicy, resetState);
		}

		// Token: 0x060071DE RID: 29150 RVA: 0x00179288 File Offset: 0x00177488
		internal virtual void SetStateFromPolicyForTest()
		{
			this.SetStateFromPolicyForTest(true);
		}

		// Token: 0x17002838 RID: 10296
		// (get) Token: 0x060071DF RID: 29151 RVA: 0x00179291 File Offset: 0x00177491
		// (set) Token: 0x060071E0 RID: 29152 RVA: 0x00179299 File Offset: 0x00177499
		private protected DateTime CreationTime { protected get; private set; }

		// Token: 0x060071E1 RID: 29153 RVA: 0x001792A2 File Offset: 0x001774A2
		protected virtual void UpdateCachedPolicyValues(bool resetBudgetValues)
		{
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x001792A4 File Offset: 0x001774A4
		private void InternalUpdateCachedPolicyValues(bool resetBudgetValues)
		{
			lock (this.SyncRoot)
			{
				this.casTokenBucket = TokenBucket.Create(resetBudgetValues ? null : this.casTokenBucket, this.ThrottlingPolicy.MaxBurst, this.ThrottlingPolicy.RechargeRate, this.ThrottlingPolicy.CutoffBalance, this.Owner);
			}
			this.UpdateCachedPolicyValues(resetBudgetValues);
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x00179324 File Offset: 0x00177524
		protected virtual void AccountForCostHandle(CostHandle costHandle)
		{
			if (costHandle.CostType == CostType.CAS)
			{
				TimeSpan timeSpan = costHandle.PreCharge;
				LocalTimeCostHandle localTimeCostHandle = costHandle as LocalTimeCostHandle;
				bool flag = localTimeCostHandle != null && localTimeCostHandle.ReverseBudgetCharge;
				if (flag)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceDebug((long)this.GetHashCode(), "[Budget.AccountForCostHandle] Not charging budget for CAS time per protocol's request.");
					timeSpan += TimeProvider.UtcNow - costHandle.StartTime;
				}
				this.casTokenBucket.Decrement(timeSpan, flag);
				this.percentileUsage.AddUsage((int)(TimeProvider.UtcNow - costHandle.StartTime).TotalMilliseconds);
			}
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x001793B8 File Offset: 0x001775B8
		protected virtual bool InternalTryCheckOverBudget(ICollection<CostType> costTypes, out OverBudgetException exception)
		{
			exception = null;
			bool flag = false;
			int num = 0;
			string text = null;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(4238748989U, ref num);
			bool flag2 = false;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3871747389U, ref flag2);
			if (flag2)
			{
				exception = this.CreateOverBudgetException("LocalTime", "faultInjection", num);
				return true;
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3165007165U, ref flag);
			if (flag)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2628136253U, ref text);
				if (num != 0 && !string.IsNullOrEmpty(text))
				{
					exception = this.CreateOverBudgetException(text, "faultInjection", num);
					return true;
				}
			}
			DateTime? lockedUntilUtc = this.casTokenBucket.LockedUntilUtc;
			if (lockedUntilUtc != null && costTypes.Contains(CostType.CAS))
			{
				DateTime value = lockedUntilUtc.Value;
				TimeSpan timeSpan = value - TimeProvider.UtcNow;
				if (value == DateTime.MaxValue || timeSpan.TotalMilliseconds > 2147483647.0)
				{
					num = int.MaxValue;
				}
				else
				{
					num = Math.Max(0, (int)timeSpan.TotalMilliseconds);
				}
				if (num > 0)
				{
					this.CheckLeakedActions();
					exception = this.CreateOverBudgetException("LocalTime", this.ThrottlingPolicy.CutoffBalance.ToString(), num);
				}
			}
			return exception != null;
		}

		// Token: 0x060071E5 RID: 29157 RVA: 0x001794F8 File Offset: 0x001776F8
		internal void CheckLeakedActions()
		{
			DateTime utcNow = TimeProvider.UtcNow;
			StringBuilder stringBuilder = null;
			List<CostHandle> list = null;
			if (utcNow - this.lastLeakDetectionUtc >= Budget.LeakDetectionCheckInterval && this.OutstandingActionsCount > 0)
			{
				lock (this.syncRoot)
				{
					if (utcNow - this.lastLeakDetectionUtc >= Budget.LeakDetectionCheckInterval && this.OutstandingActionsCount > 0)
					{
						this.lastLeakDetectionUtc = utcNow;
						foreach (KeyValuePair<long, CostHandle> keyValuePair in this.outstandingActions)
						{
							CostHandle value = keyValuePair.Value;
							TimeSpan timeSpan = utcNow - value.StartTime;
							if (value.CostType == CostType.CAS && timeSpan > value.MaxLiveTime && !value.LeakLogged)
							{
								if (Budget.OnLeakDetectionForTest != null)
								{
									Budget.OnLeakDetectionForTest(this, value, timeSpan, value.MaxLiveTime);
								}
								if (Budget.OnLeakDetectionForTest == null || Budget.OnLeakWatsonInfoForTest != null)
								{
									if (stringBuilder == null)
									{
										stringBuilder = new StringBuilder();
									}
									stringBuilder.AppendLine(string.Format("CostType: {0}, Key: {1}, Limit: {2}, Elapsed: {3}, Actions: {4}, Description: {5}, Snapshot: {6}", new object[]
									{
										value.CostType,
										keyValuePair.Key,
										value.MaxLiveTime,
										timeSpan,
										this.OutstandingActionsCount,
										value.Description,
										this
									}));
								}
								value.LeakLogged = true;
								if (list == null)
								{
									list = new List<CostHandle>();
								}
								list.Add(value);
							}
						}
					}
				}
			}
			if (Budget.OnLeakWatsonInfoForTest != null)
			{
				Budget.OnLeakWatsonInfoForTest(stringBuilder);
			}
			else if (stringBuilder != null)
			{
				try
				{
					throw new LongRunningCostHandleException();
				}
				catch (LongRunningCostHandleException exception)
				{
					ExWatson.SendReport(exception, ReportOptions.None, stringBuilder.ToString());
				}
			}
			if (stringBuilder != null)
			{
				string text = stringBuilder.ToString();
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_BudgetActionExceededExpectedTime, text, new object[]
				{
					"\n" + text
				});
			}
			if (list != null)
			{
				foreach (CostHandle costHandle in list)
				{
					costHandle.Dispose();
				}
			}
		}

		// Token: 0x060071E6 RID: 29158 RVA: 0x001797BC File Offset: 0x001779BC
		object[] IReadOnlyPropertyBag.GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			object[] array = new object[propertyDefinitionArray.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitionArray)
			{
				array[num++] = ((IReadOnlyPropertyBag)this)[propertyDefinition];
			}
			return array;
		}

		// Token: 0x17002839 RID: 10297
		object IReadOnlyPropertyBag.this[PropertyDefinition propertyDefinition]
		{
			get
			{
				if (propertyDefinition == BudgetMetadataSchema.Balance)
				{
					return this.CasTokenBucket.GetBalance();
				}
				if (propertyDefinition == BudgetMetadataSchema.InCutoff)
				{
					return this.CasTokenBucket.Locked;
				}
				if (propertyDefinition == BudgetMetadataSchema.InMicroDelay)
				{
					return !this.CasTokenBucket.Locked && this.CasTokenBucket.GetBalance() < 0f;
				}
				if (propertyDefinition == BudgetMetadataSchema.IsGlobalPolicy)
				{
					return this.ThrottlingPolicy.FullPolicy.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Global;
				}
				if (propertyDefinition == BudgetMetadataSchema.IsOrgPolicy)
				{
					return this.ThrottlingPolicy.FullPolicy.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Organization;
				}
				if (propertyDefinition == BudgetMetadataSchema.IsRegularPolicy)
				{
					return this.ThrottlingPolicy.FullPolicy.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Regular;
				}
				if (propertyDefinition == BudgetMetadataSchema.IsServiceAccount)
				{
					return this.Owner.IsServiceAccountBudget;
				}
				if (propertyDefinition == BudgetMetadataSchema.LiveTime)
				{
					return DateTime.UtcNow - this.CreationTime;
				}
				if (propertyDefinition == BudgetMetadataSchema.Name)
				{
					return this.Owner.ToString();
				}
				if (propertyDefinition == BudgetMetadataSchema.NotThrottled)
				{
					return !this.CasTokenBucket.Locked && this.CasTokenBucket.GetBalance() >= 0f;
				}
				if (propertyDefinition == BudgetMetadataSchema.OutstandingActionCount)
				{
					return this.OutstandingActionsCount;
				}
				if (propertyDefinition == BudgetMetadataSchema.ThrottlingPolicy)
				{
					return this.ThrottlingPolicy.FullPolicy.GetShortIdentityString();
				}
				throw new ArgumentException(string.Format("Unexpected property name '{0}'.", propertyDefinition.Name));
			}
		}

		// Token: 0x040049A9 RID: 18857
		public const int IndefiniteBackoff = 2147483647;

		// Token: 0x040049AA RID: 18858
		public const string LocalTimeReason = "LocalTime";

		// Token: 0x040049AB RID: 18859
		private const uint LidChangeIsOverBudgetValue = 3165007165U;

		// Token: 0x040049AC RID: 18860
		private const uint LidChangeBackoffTimeValue = 4238748989U;

		// Token: 0x040049AD RID: 18861
		private const uint LidChangeOverBudgetReason = 2628136253U;

		// Token: 0x040049AE RID: 18862
		private const uint LidChangeLocked = 3871747389U;

		// Token: 0x040049AF RID: 18863
		private const uint LidChangeIgnoreOverBudget = 3670420797U;

		// Token: 0x040049B0 RID: 18864
		public static readonly TimeSpan IndefiniteDelay = TimeSpan.MaxValue;

		// Token: 0x040049B1 RID: 18865
		public static readonly TimeSpan PolicyUpdateCheckInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x040049B2 RID: 18866
		public static readonly TimeSpan LeakDetectionCheckInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x040049B3 RID: 18867
		private static readonly Dictionary<CostType, TimeSpan> maximumActionTimes;

		// Token: 0x040049B4 RID: 18868
		internal static readonly HashSet<CostType> AllCostTypes;

		// Token: 0x040049B5 RID: 18869
		private static readonly TimeSpan defaultMaximumActionTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x040049B6 RID: 18870
		private static readonly TimeSpan defaultActiveRunspaceMaximumActionTime = TimeSpan.MaxValue;

		// Token: 0x040049B7 RID: 18871
		private Dictionary<long, CostHandle> outstandingActions = new Dictionary<long, CostHandle>();

		// Token: 0x040049B8 RID: 18872
		private DateTime lastLeakDetectionUtc = DateTime.MinValue;

		// Token: 0x040049B9 RID: 18873
		private DateTime lastPolicyCheck = TimeProvider.UtcNow;

		// Token: 0x040049BA RID: 18874
		private PercentileUsage percentileUsage;

		// Token: 0x040049BB RID: 18875
		private bool isExpired;

		// Token: 0x040049BC RID: 18876
		private ITokenBucket casTokenBucket;

		// Token: 0x040049BD RID: 18877
		private object syncRoot = new object();
	}
}
