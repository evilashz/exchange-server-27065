using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DD7 RID: 3543
	internal class BudgetAdapter : IEwsBudget, IStandardBudget, IBudget, IDisposable
	{
		// Token: 0x06005A87 RID: 23175 RVA: 0x0011A6D6 File Offset: 0x001188D6
		public BudgetAdapter(IStandardBudget budget)
		{
			if (budget == null)
			{
				throw new ArgumentNullException("budget");
			}
			this.InnerBudget = budget;
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06005A88 RID: 23176 RVA: 0x0011A6F3 File Offset: 0x001188F3
		// (set) Token: 0x06005A89 RID: 23177 RVA: 0x0011A6FB File Offset: 0x001188FB
		public IStandardBudget InnerBudget { get; private set; }

		// Token: 0x06005A8A RID: 23178 RVA: 0x0011A704 File Offset: 0x00118904
		public void StartLocal(string callerInfo, TimeSpan preCharge = default(TimeSpan))
		{
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x0011A706 File Offset: 0x00118906
		public void EndLocal()
		{
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06005A8C RID: 23180 RVA: 0x0011A708 File Offset: 0x00118908
		public LocalTimeCostHandle LocalCostHandle
		{
			get
			{
				return this.InnerBudget.LocalCostHandle;
			}
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x0011A715 File Offset: 0x00118915
		public DelayInfo GetDelay()
		{
			return this.InnerBudget.GetDelay();
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x0011A722 File Offset: 0x00118922
		public DelayInfo GetDelay(ICollection<CostType> consideredCostTypes)
		{
			return this.InnerBudget.GetDelay(consideredCostTypes);
		}

		// Token: 0x06005A8F RID: 23183 RVA: 0x0011A730 File Offset: 0x00118930
		public void CheckOverBudget()
		{
			this.InnerBudget.CheckOverBudget();
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x0011A73D File Offset: 0x0011893D
		public void CheckOverBudget(ICollection<CostType> consideredCostTypes)
		{
			this.InnerBudget.CheckOverBudget(consideredCostTypes);
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x0011A74B File Offset: 0x0011894B
		public bool TryCheckOverBudget(out OverBudgetException exception)
		{
			exception = null;
			return this.InnerBudget.TryCheckOverBudget(out exception);
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x0011A75C File Offset: 0x0011895C
		public bool TryCheckOverBudget(ICollection<CostType> consideredCostTypes, out OverBudgetException exception)
		{
			exception = null;
			return this.InnerBudget.TryCheckOverBudget(consideredCostTypes, out exception);
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06005A93 RID: 23187 RVA: 0x0011A76E File Offset: 0x0011896E
		public BudgetKey Owner
		{
			get
			{
				return this.InnerBudget.Owner;
			}
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06005A94 RID: 23188 RVA: 0x0011A77B File Offset: 0x0011897B
		public IThrottlingPolicy ThrottlingPolicy
		{
			get
			{
				return this.InnerBudget.ThrottlingPolicy;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06005A95 RID: 23189 RVA: 0x0011A788 File Offset: 0x00118988
		public TimeSpan ResourceWorkAccomplished
		{
			get
			{
				return this.InnerBudget.ResourceWorkAccomplished;
			}
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x0011A795 File Offset: 0x00118995
		public void ResetWorkAccomplished()
		{
			this.InnerBudget.ResetWorkAccomplished();
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x0011A7A2 File Offset: 0x001189A2
		public bool TryGetBudgetBalance(out string budgetBalance)
		{
			budgetBalance = null;
			return this.InnerBudget.TryGetBudgetBalance(out budgetBalance);
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x0011A7B3 File Offset: 0x001189B3
		public CostHandle StartConnection(string callerInfo)
		{
			return this.InnerBudget.StartConnection(callerInfo);
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x0011A7C1 File Offset: 0x001189C1
		public void EndConnection()
		{
			this.InnerBudget.EndConnection();
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x0011A7CE File Offset: 0x001189CE
		public void Dispose()
		{
			this.InnerBudget.Dispose();
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x0011A7DB File Offset: 0x001189DB
		public bool SleepIfNecessary()
		{
			return false;
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x0011A7DE File Offset: 0x001189DE
		public bool SleepIfNecessary(out int sleepTime, out float cpuPercent)
		{
			cpuPercent = 0f;
			sleepTime = 0;
			return false;
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x0011A7EB File Offset: 0x001189EB
		public void LogEndStateToIIS()
		{
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x0011A7ED File Offset: 0x001189ED
		public bool TryIncrementFoundObjectCount(uint foundCount, out int maxPossible)
		{
			maxPossible = (int)foundCount;
			return true;
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x0011A7F3 File Offset: 0x001189F3
		public bool CanAllocateFoundObjects(uint foundCount, out int maxPossible)
		{
			maxPossible = (int)foundCount;
			return true;
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06005AA0 RID: 23200 RVA: 0x0011A7F9 File Offset: 0x001189F9
		public uint TotalRpcRequestCount
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06005AA1 RID: 23201 RVA: 0x0011A7FC File Offset: 0x001189FC
		public ulong TotalRpcRequestLatency
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06005AA2 RID: 23202 RVA: 0x0011A800 File Offset: 0x00118A00
		public uint TotalLdapRequestCount
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06005AA3 RID: 23203 RVA: 0x0011A803 File Offset: 0x00118A03
		public long TotalLdapRequestLatency
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x0011A807 File Offset: 0x00118A07
		public void StartPerformanceContext()
		{
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x0011A809 File Offset: 0x00118A09
		public void StopPerformanceContext()
		{
		}
	}
}
