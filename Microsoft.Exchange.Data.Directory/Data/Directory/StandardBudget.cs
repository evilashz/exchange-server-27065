using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009D1 RID: 2513
	internal class StandardBudget : Budget
	{
		// Token: 0x06007469 RID: 29801 RVA: 0x0017FE60 File Offset: 0x0017E060
		public static IStandardBudget Acquire(BudgetKey budgetKey)
		{
			StandardBudget innerBudget = StandardBudgetCache.Singleton.Get(budgetKey);
			return new StandardBudgetWrapper(innerBudget);
		}

		// Token: 0x0600746A RID: 29802 RVA: 0x0017FE80 File Offset: 0x0017E080
		public static IStandardBudget Acquire(SecurityIdentifier budgetSid, BudgetType budgetType, bool isServiceAccount, ADSessionSettings settings)
		{
			SidBudgetKey budgetKey = new SidBudgetKey(budgetSid, budgetType, isServiceAccount, settings);
			return StandardBudget.Acquire(budgetKey);
		}

		// Token: 0x0600746B RID: 29803 RVA: 0x0017FE9D File Offset: 0x0017E09D
		public static IStandardBudget Acquire(SecurityIdentifier budgetSid, BudgetType budgetType, ADSessionSettings settings)
		{
			return StandardBudget.Acquire(budgetSid, budgetType, false, settings);
		}

		// Token: 0x0600746C RID: 29804 RVA: 0x0017FEA8 File Offset: 0x0017E0A8
		public static IStandardBudget AcquireFallback(string identifier, BudgetType budgetType)
		{
			StringBudgetKey budgetKey = new StringBudgetKey(identifier, false, budgetType);
			return StandardBudget.Acquire(budgetKey);
		}

		// Token: 0x0600746D RID: 29805 RVA: 0x0017FEC4 File Offset: 0x0017E0C4
		public static IStandardBudget AcquireUnthrottledBudget(string identifier, BudgetType budgetType)
		{
			UnthrottledBudgetKey budgetKey = new UnthrottledBudgetKey(identifier, budgetType);
			return StandardBudget.Acquire(budgetKey);
		}

		// Token: 0x0600746E RID: 29806 RVA: 0x0017FEDF File Offset: 0x0017E0DF
		internal StandardBudget(BudgetKey owner, IThrottlingPolicy policy) : base(owner, policy)
		{
		}

		// Token: 0x0600746F RID: 29807 RVA: 0x0017FEEC File Offset: 0x0017E0EC
		public CostHandle StartConnection(Action<CostHandle> onRelease, string callerInfo)
		{
			CostHandle result;
			lock (base.SyncRoot)
			{
				int num = this.connections + 1;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(3701878077U, ref num);
				bool flag2 = false;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(2630233405U, ref flag2);
				if (num > this.maxConcurrency || flag2)
				{
					ThrottlingPerfCounterWrapper.IncrementBudgetsAtMaxConcurrency(base.Owner);
					throw base.CreateOverBudgetException("MaxConcurrency", flag2 ? "FaultInjection" : this.maxConcurrency.ToString(), 0);
				}
				this.connections++;
				result = new CostHandle(this, CostType.Connection, onRelease, callerInfo, default(TimeSpan));
			}
			return result;
		}

		// Token: 0x06007470 RID: 29808 RVA: 0x0017FFB4 File Offset: 0x0017E1B4
		public override string ToString()
		{
			return string.Format("Owner:{0},Conn:{1},MaxConn:{2},MaxBurst:{3},Balance:{4},Cutoff:{5},RechargeRate:{6},Policy:{7},IsServiceAccount:{8},LiveTime:{9}", new object[]
			{
				base.Owner,
				this.Connections,
				base.ThrottlingPolicy.MaxConcurrency,
				base.ThrottlingPolicy.MaxBurst,
				base.GetBalanceForTrace(),
				base.ThrottlingPolicy.CutoffBalance,
				base.ThrottlingPolicy.RechargeRate,
				base.ThrottlingPolicy.FullPolicy.GetShortIdentityString(),
				base.ThrottlingPolicy.FullPolicy.IsServiceAccount,
				TimeProvider.UtcNow - base.CreationTime
			});
		}

		// Token: 0x06007471 RID: 29809 RVA: 0x00180084 File Offset: 0x0017E284
		protected override void AccountForCostHandle(CostHandle costHandle)
		{
			if (costHandle.CostType != CostType.Connection)
			{
				base.AccountForCostHandle(costHandle);
				return;
			}
			if (this.connections <= 0)
			{
				throw new InvalidOperationException("[StandardBudget.AccountForCostHandle] End for Connections was called, but there are no outstanding Connections.");
			}
			this.connections--;
			ThrottlingPerfCounterWrapper.DecrementBudgetsAtMaxConcurrency(base.Owner);
		}

		// Token: 0x17002990 RID: 10640
		// (get) Token: 0x06007472 RID: 29810 RVA: 0x001800C3 File Offset: 0x0017E2C3
		internal int Connections
		{
			get
			{
				return this.connections;
			}
		}

		// Token: 0x06007473 RID: 29811 RVA: 0x001800CC File Offset: 0x0017E2CC
		protected override void UpdateCachedPolicyValues(bool resetBudgetValues)
		{
			base.UpdateCachedPolicyValues(resetBudgetValues);
			lock (base.SyncRoot)
			{
				this.maxConcurrency = (int)(base.ThrottlingPolicy.MaxConcurrency.IsUnlimited ? 2147483647U : base.ThrottlingPolicy.MaxConcurrency.Value);
				if (resetBudgetValues)
				{
					this.connections = 0;
					ThrottlingPerfCounterWrapper.DecrementBudgetsAtMaxConcurrency(base.Owner);
				}
			}
		}

		// Token: 0x04004B11 RID: 19217
		public const string MaxConcurrencyPart = "MaxConcurrency";

		// Token: 0x04004B12 RID: 19218
		private const string FormatString = "Owner:{0},Conn:{1},MaxConn:{2},MaxBurst:{3},Balance:{4},Cutoff:{5},RechargeRate:{6},Policy:{7},IsServiceAccount:{8},LiveTime:{9}";

		// Token: 0x04004B13 RID: 19219
		private const uint LidChangeConnectionValue = 3701878077U;

		// Token: 0x04004B14 RID: 19220
		private const uint LidChangeMaxConnExceeded = 2630233405U;

		// Token: 0x04004B15 RID: 19221
		private int connections;

		// Token: 0x04004B16 RID: 19222
		private int maxConcurrency;
	}
}
