using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009AE RID: 2478
	internal abstract class BudgetWrapper<T> : IBudget, IDisposable where T : Budget
	{
		// Token: 0x0600724E RID: 29262 RVA: 0x0017A800 File Offset: 0x00178A00
		internal BudgetWrapper(T innerBudget)
		{
			if (innerBudget == null)
			{
				throw new ArgumentNullException("innerBudget");
			}
			this.innerBudget = innerBudget;
			WorkloadManagementLogger.SetBudgetType(this.BudgetType.ToString(), null);
		}

		// Token: 0x17002853 RID: 10323
		// (get) Token: 0x0600724F RID: 29263 RVA: 0x0017A870 File Offset: 0x00178A70
		public BudgetKey Owner
		{
			get
			{
				return this.innerBudget.Owner;
			}
		}

		// Token: 0x06007250 RID: 29264 RVA: 0x0017A884 File Offset: 0x00178A84
		protected void HandleCostHandleRelease(CostHandle costHandle)
		{
			lock (this.instanceLock)
			{
				this.CalculateElapsedTime(costHandle);
				this.myActions.Remove(costHandle.Key);
				if (costHandle == this.localCostHandle)
				{
					this.localCostHandle = null;
				}
			}
		}

		// Token: 0x17002854 RID: 10324
		// (get) Token: 0x06007251 RID: 29265 RVA: 0x0017A8EC File Offset: 0x00178AEC
		public BudgetType BudgetType
		{
			get
			{
				return this.innerBudget.Owner.BudgetType;
			}
		}

		// Token: 0x17002855 RID: 10325
		// (get) Token: 0x06007252 RID: 29266 RVA: 0x0017A904 File Offset: 0x00178B04
		internal Dictionary<long, CostHandle> OutstandingActions
		{
			get
			{
				return this.myActions;
			}
		}

		// Token: 0x06007253 RID: 29267 RVA: 0x0017A90C File Offset: 0x00178B0C
		internal T GetInnerBudget()
		{
			this.CheckExpired();
			return this.innerBudget;
		}

		// Token: 0x06007254 RID: 29268 RVA: 0x0017A91C File Offset: 0x00178B1C
		protected void CheckExpired()
		{
			if (this.innerBudget.IsExpired)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug<BudgetKey, int>((long)this.GetHashCode(), "[BudgetWrapper.CheckExpired] Budget has expired for owner: {0}.  Outstanding actions ignored: {1}", this.innerBudget.Owner, this.innerBudget.OutstandingActionsCount);
				lock (this.instanceLock)
				{
					if (this.innerBudget.IsExpired)
					{
						this.innerBudget = this.ReacquireBudget();
						this.myActions.Clear();
						this.localCostHandle = null;
					}
				}
			}
		}

		// Token: 0x06007255 RID: 29269
		protected abstract T ReacquireBudget();

		// Token: 0x06007256 RID: 29270 RVA: 0x0017A9D8 File Offset: 0x00178BD8
		private void CloseAllActions()
		{
			this.CheckExpired();
			if (this.myActions.Count > 0)
			{
				Dictionary<long, CostHandle> dictionary = null;
				lock (this.instanceLock)
				{
					dictionary = this.myActions;
					this.myActions = new Dictionary<long, CostHandle>();
				}
				foreach (KeyValuePair<long, CostHandle> keyValuePair in dictionary)
				{
					keyValuePair.Value.Dispose();
				}
			}
		}

		// Token: 0x06007257 RID: 29271 RVA: 0x0017AA80 File Offset: 0x00178C80
		public void Dispose()
		{
			string budgetBalance;
			if (this.TryGetBudgetBalance(out budgetBalance))
			{
				WorkloadManagementLogger.SetBudgetBalance(budgetBalance, null);
			}
			this.CloseAllActions();
			this.AfterDispose();
		}

		// Token: 0x06007258 RID: 29272 RVA: 0x0017AAAB File Offset: 0x00178CAB
		protected virtual void AfterDispose()
		{
		}

		// Token: 0x17002856 RID: 10326
		// (get) Token: 0x06007259 RID: 29273 RVA: 0x0017AAB0 File Offset: 0x00178CB0
		public IThrottlingPolicy ThrottlingPolicy
		{
			get
			{
				T t = this.GetInnerBudget();
				return t.ThrottlingPolicy.FullPolicy;
			}
		}

		// Token: 0x0600725A RID: 29274 RVA: 0x0017AAD8 File Offset: 0x00178CD8
		public bool TryGetBudgetBalance(out string budgetBalance)
		{
			budgetBalance = null;
			if (!(this.innerBudget.CasTokenBucket is UnthrottledTokenBucket))
			{
				budgetBalance = this.innerBudget.CasTokenBucket.GetBalance().ToString();
				return true;
			}
			return false;
		}

		// Token: 0x0600725B RID: 29275 RVA: 0x0017AB24 File Offset: 0x00178D24
		protected void AddAction(CostHandle costHandle)
		{
			lock (this.instanceLock)
			{
				this.myActions.Add(costHandle.Key, costHandle);
			}
		}

		// Token: 0x0600725C RID: 29276 RVA: 0x0017AB70 File Offset: 0x00178D70
		public void CheckOverBudget()
		{
			this.CheckOverBudget(Budget.AllCostTypes);
		}

		// Token: 0x0600725D RID: 29277 RVA: 0x0017AB80 File Offset: 0x00178D80
		public void CheckOverBudget(ICollection<CostType> consideredCostTypes)
		{
			T t = this.GetInnerBudget();
			t.CheckOverBudget(consideredCostTypes);
		}

		// Token: 0x0600725E RID: 29278 RVA: 0x0017ABA2 File Offset: 0x00178DA2
		public bool TryCheckOverBudget(out OverBudgetException exception)
		{
			return this.TryCheckOverBudget(Budget.AllCostTypes, out exception);
		}

		// Token: 0x0600725F RID: 29279 RVA: 0x0017ABB0 File Offset: 0x00178DB0
		public bool TryCheckOverBudget(ICollection<CostType> consideredCostTypes, out OverBudgetException exception)
		{
			T t = this.GetInnerBudget();
			return t.TryCheckOverBudget(consideredCostTypes, out exception);
		}

		// Token: 0x06007260 RID: 29280 RVA: 0x0017ABD4 File Offset: 0x00178DD4
		public void StartLocal(string callerInfo, TimeSpan preCharge = default(TimeSpan))
		{
			this.CheckExpired();
			lock (this.instanceLock)
			{
				this.StartLocalImpl(callerInfo, preCharge);
			}
		}

		// Token: 0x06007261 RID: 29281 RVA: 0x0017AC1C File Offset: 0x00178E1C
		protected virtual void StartLocalImpl(string callerInfo, TimeSpan preCharge)
		{
			if (this.localCostHandle != null)
			{
				throw new InvalidOperationException("[BudgetWrapper.StartLocal] Only one outstanding LocalTime cost handle can be active on a budget wrapper.");
			}
			this.localCostHandle = this.InternalStartLocal(callerInfo, preCharge);
			this.AddAction(this.localCostHandle);
		}

		// Token: 0x06007262 RID: 29282 RVA: 0x0017AC54 File Offset: 0x00178E54
		public void EndLocal()
		{
			this.CheckExpired();
			lock (this.instanceLock)
			{
				if (this.localCostHandle != null)
				{
					LocalTimeCostHandle localTimeCostHandle = this.localCostHandle;
					this.localCostHandle = null;
					localTimeCostHandle.Dispose();
				}
			}
		}

		// Token: 0x06007263 RID: 29283 RVA: 0x0017ACB8 File Offset: 0x00178EB8
		protected virtual LocalTimeCostHandle InternalStartLocal(string callerInfo, TimeSpan preCharge)
		{
			return this.innerBudget.StartLocal(new Action<CostHandle>(this.HandleCostHandleRelease), callerInfo, preCharge);
		}

		// Token: 0x17002857 RID: 10327
		// (get) Token: 0x06007264 RID: 29284 RVA: 0x0017ACD9 File Offset: 0x00178ED9
		// (set) Token: 0x06007265 RID: 29285 RVA: 0x0017ACE3 File Offset: 0x00178EE3
		public LocalTimeCostHandle LocalCostHandle
		{
			get
			{
				return this.localCostHandle;
			}
			protected set
			{
				this.localCostHandle = value;
			}
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x0017ACF0 File Offset: 0x00178EF0
		public override string ToString()
		{
			T t = this.GetInnerBudget();
			return t.ToString();
		}

		// Token: 0x06007267 RID: 29287 RVA: 0x0017AD11 File Offset: 0x00178F11
		public DelayInfo GetDelay()
		{
			return this.GetDelay(Budget.AllCostTypes);
		}

		// Token: 0x06007268 RID: 29288 RVA: 0x0017AD20 File Offset: 0x00178F20
		public DelayInfo GetDelay(ICollection<CostType> consideredCostTypes)
		{
			DelayInfo faultInjectionDelay = this.GetFaultInjectionDelay();
			if (faultInjectionDelay != DelayInfo.NoDelay)
			{
				return faultInjectionDelay;
			}
			BudgetTypeSetting budgetTypeSetting = BudgetTypeSettings.Get(this.Owner.BudgetType);
			DelayInfo hardQuotaDelay = this.GetHardQuotaDelay(consideredCostTypes, budgetTypeSetting);
			if (hardQuotaDelay.Delay == budgetTypeSetting.MaxDelay)
			{
				return hardQuotaDelay;
			}
			DelayInfo microDelay = this.GetMicroDelay(consideredCostTypes, budgetTypeSetting);
			if (hardQuotaDelay.Delay >= microDelay.Delay)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug<TimeSpan, TimeSpan>((long)this.GetHashCode(), "[BudgetWrapper.GetDelay] UserQuota delay '{0}' was greater than micro delay '{1}'", hardQuotaDelay.Delay, microDelay.Delay);
				return hardQuotaDelay;
			}
			ExTraceGlobals.ClientThrottlingTracer.TraceDebug<TimeSpan, TimeSpan>((long)this.GetHashCode(), "[BudgetWrapper.GetDelay] Micro delay '{0}' was greater than user quota delay '{1}'", microDelay.Delay, hardQuotaDelay.Delay);
			return microDelay;
		}

		// Token: 0x06007269 RID: 29289 RVA: 0x0017ADD0 File Offset: 0x00178FD0
		private DelayInfo GetFaultInjectionDelay()
		{
			string text = null;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3645254973U, ref text);
			if (string.IsNullOrEmpty(text))
			{
				return DelayInfo.NoDelay;
			}
			bool flag = false;
			int num = 0;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3645254973U, ref flag);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(2571513149U, ref num);
			TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)num);
			if (flag)
			{
				TimeSpan delay = timeSpan;
				T t = this.GetInnerBudget();
				return new UserQuotaDelayInfo(delay, t.CreateOverBudgetException(text, "faultInjection", num), flag);
			}
			return new DelayInfo(timeSpan, flag);
		}

		// Token: 0x0600726A RID: 29290 RVA: 0x0017AE5C File Offset: 0x0017905C
		private DelayInfo GetHardQuotaDelay(ICollection<CostType> consideredCostTypes, BudgetTypeSetting budgetTypeSetting)
		{
			OverBudgetException ex = null;
			if (this.TryCheckOverBudget(consideredCostTypes, out ex))
			{
				int backoffTime = ex.BackoffTime;
				TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)backoffTime);
				TimeSpan delay = (timeSpan > budgetTypeSetting.MaxDelay) ? budgetTypeSetting.MaxDelay : timeSpan;
				return new UserQuotaDelayInfo(delay, ex, true);
			}
			return DelayInfo.NoDelay;
		}

		// Token: 0x0600726B RID: 29291 RVA: 0x0017AEB0 File Offset: 0x001790B0
		private DelayInfo GetMicroDelay(ICollection<CostType> consideredCostTypes, BudgetTypeSetting budgetTypeSetting)
		{
			if (this.microDelayWorthyWork == TimeSpan.Zero || !consideredCostTypes.Contains(CostType.CAS))
			{
				return DelayInfo.NoDelay;
			}
			float balance = this.innerBudget.CasTokenBucket.GetBalance();
			if (balance < 0f)
			{
				SingleComponentThrottlingPolicy throttlingPolicy = this.innerBudget.ThrottlingPolicy;
				int num = (int)this.microDelayWorthyWork.TotalMilliseconds;
				int num2 = num * (int)(3600000U / throttlingPolicy.RechargeRate.Value);
				float num3 = -balance / throttlingPolicy.RechargeRate.Value;
				TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)((float)num2 * num3));
				TimeSpan timeSpan2 = (BudgetWrapper<T>.MinimumMicroDelay > timeSpan) ? BudgetWrapper<T>.MinimumMicroDelay : timeSpan;
				TimeSpan timeSpan3 = timeSpan2;
				TimeSpan timeSpan4 = (budgetTypeSetting.MaxMicroDelayMultiplier == int.MaxValue) ? TimeSpan.MaxValue : TimeSpan.FromMilliseconds((double)(num * budgetTypeSetting.MaxMicroDelayMultiplier));
				if (timeSpan3 > timeSpan4)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceDebug((long)this.GetHashCode(), "[BudgetWrapper.GetDelay] Budget '{0}' calculated an overBudgetFactor of '{1}', but used registry cap of '{2}' instead.  Budget Snapshot: '{3}'", new object[]
					{
						this.Owner,
						num3,
						budgetTypeSetting.MaxMicroDelayMultiplier,
						this
					});
					timeSpan3 = timeSpan4;
				}
				if (timeSpan3 > budgetTypeSetting.MaxDelay)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceDebug<BudgetKey, TimeSpan, TimeSpan>((long)this.GetHashCode(), "[BudgetWrapper.GetDelay] Budget '{0}' calculated a cappedDelay of '{1}' which was higher than registry MaxDelay of '{2}'.  Using MaxDelay instead.", this.Owner, timeSpan3, budgetTypeSetting.MaxDelay);
					ThrottlingPerfCounterWrapper.IncrementBudgetsAtMaxDelay(this.Owner);
					timeSpan3 = budgetTypeSetting.MaxDelay;
				}
				ThrottlingPerfCounterWrapper.IncrementBudgetsMicroDelayed(this.Owner);
				DelayInfo.TraceMicroDelays(this, TimeSpan.FromMilliseconds((double)num), timeSpan3);
				return new DelayInfo(timeSpan3, false);
			}
			return DelayInfo.NoDelay;
		}

		// Token: 0x0600726C RID: 29292 RVA: 0x0017B064 File Offset: 0x00179264
		private void CalculateElapsedTime(CostHandle costHandle)
		{
			LocalTimeCostHandle localTimeCostHandle = costHandle as LocalTimeCostHandle;
			if (localTimeCostHandle != null)
			{
				TimeSpan unaccountedForTime = localTimeCostHandle.UnaccountedForTime;
				if (unaccountedForTime > TimeSpan.Zero)
				{
					T t = this.GetInnerBudget();
					if (t.CasTokenBucket.GetBalance() < 0f)
					{
						this.microDelayWorthyWork += unaccountedForTime;
					}
					else
					{
						this.microDelayWorthyWork = TimeSpan.Zero;
					}
					this.allWork += unaccountedForTime;
					WorkloadManagementLogger.SetBudgetUsage(unaccountedForTime, null, null);
				}
			}
		}

		// Token: 0x17002858 RID: 10328
		// (get) Token: 0x0600726D RID: 29293 RVA: 0x0017B0EC File Offset: 0x001792EC
		public TimeSpan ResourceWorkAccomplished
		{
			get
			{
				TimeSpan result;
				lock (this.instanceLock)
				{
					result = ((this.localCostHandle != null) ? (this.allWork + this.localCostHandle.UnaccountedForTime) : this.allWork);
				}
				return result;
			}
		}

		// Token: 0x0600726E RID: 29294 RVA: 0x0017B154 File Offset: 0x00179354
		public void ResetWorkAccomplished()
		{
			ExTraceGlobals.ClientThrottlingTracer.TraceDebug<BudgetKey>((long)this.GetHashCode(), "[BudgetWrapper.ResetWorkAccomplished] Resetting work for budget '{0}'", this.Owner);
			lock (this.instanceLock)
			{
				this.microDelayWorthyWork = TimeSpan.Zero;
				this.allWork = TimeSpan.Zero;
				if (this.localCostHandle != null)
				{
					this.localCostHandle.UnaccountedStartTime = TimeProvider.UtcNow;
				}
			}
		}

		// Token: 0x17002859 RID: 10329
		// (get) Token: 0x0600726F RID: 29295 RVA: 0x0017B1DC File Offset: 0x001793DC
		private TimeSpan MicroDelayWorkAccomplished
		{
			get
			{
				TimeSpan result;
				lock (this.instanceLock)
				{
					T t = this.GetInnerBudget();
					if (t.CasTokenBucket.GetBalance() < 0f)
					{
						result = TimeSpan.Zero;
					}
					else
					{
						result = ((this.localCostHandle != null) ? (this.microDelayWorthyWork + this.localCostHandle.UnaccountedForTime) : this.microDelayWorthyWork);
					}
				}
				return result;
			}
		}

		// Token: 0x04004A11 RID: 18961
		private const uint LidDelayInfoUserQuotaBackoff = 2571513149U;

		// Token: 0x04004A12 RID: 18962
		private const uint LidDelayInfoUserQuotaReason = 3645254973U;

		// Token: 0x04004A13 RID: 18963
		private const uint LidDelayInfoUserQuotaStrict = 3645254973U;

		// Token: 0x04004A14 RID: 18964
		private const int MsecInOneHour = 3600000;

		// Token: 0x04004A15 RID: 18965
		public static readonly TimeSpan MinimumMicroDelay = TimeSpan.FromMilliseconds(15.0);

		// Token: 0x04004A16 RID: 18966
		private T innerBudget;

		// Token: 0x04004A17 RID: 18967
		private Dictionary<long, CostHandle> myActions = new Dictionary<long, CostHandle>();

		// Token: 0x04004A18 RID: 18968
		private TimeSpan microDelayWorthyWork = TimeSpan.Zero;

		// Token: 0x04004A19 RID: 18969
		private TimeSpan allWork = TimeSpan.Zero;

		// Token: 0x04004A1A RID: 18970
		private volatile LocalTimeCostHandle localCostHandle;

		// Token: 0x04004A1B RID: 18971
		protected object instanceLock = new object();
	}
}
