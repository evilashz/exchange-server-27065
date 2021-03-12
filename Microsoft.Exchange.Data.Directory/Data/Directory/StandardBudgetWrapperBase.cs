using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009D2 RID: 2514
	internal abstract class StandardBudgetWrapperBase<T> : BudgetWrapper<T>, IStandardBudget, IBudget, IDisposable where T : StandardBudget
	{
		// Token: 0x06007474 RID: 29812 RVA: 0x00180158 File Offset: 0x0017E358
		internal StandardBudgetWrapperBase(T innerBudget) : base(innerBudget)
		{
		}

		// Token: 0x06007475 RID: 29813 RVA: 0x00180164 File Offset: 0x0017E364
		public CostHandle StartConnection(string callerInfo)
		{
			CostHandle result;
			lock (this.instanceLock)
			{
				if (this.connectionCostHandle != null)
				{
					throw new InvalidOperationException("You can only have a single connection open against a budget wrapper at a time.");
				}
				this.connectionCostHandle = this.InternalStartConnection(callerInfo);
				base.AddAction(this.connectionCostHandle);
				result = this.connectionCostHandle;
			}
			return result;
		}

		// Token: 0x06007476 RID: 29814 RVA: 0x001801DC File Offset: 0x0017E3DC
		protected virtual CostHandle InternalStartConnection(string callerInfo)
		{
			T innerBudget = base.GetInnerBudget();
			return innerBudget.StartConnection(new Action<CostHandle>(base.HandleCostHandleRelease), callerInfo);
		}

		// Token: 0x06007477 RID: 29815 RVA: 0x0018020C File Offset: 0x0017E40C
		public virtual void EndConnection()
		{
			lock (this.instanceLock)
			{
				if (this.connectionCostHandle != null)
				{
					this.connectionCostHandle.Dispose();
					this.connectionCostHandle = null;
				}
			}
		}

		// Token: 0x04004B17 RID: 19223
		private volatile CostHandle connectionCostHandle;
	}
}
