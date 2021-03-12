using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000196 RID: 406
	internal class ResourceHealthTracker : DisposeTrackableBase
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x00022780 File Offset: 0x00020980
		public ResourceHealthTracker(ReservationBase reservation)
		{
			this.Reservation = reservation;
			this.openedContexts = new Stack<ResourceHealthTracker.BudgetCostHandle>();
			this.openedContexts.Push(ResourceHealthTracker.OuterContext);
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x000227B5 File Offset: 0x000209B5
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x000227BD File Offset: 0x000209BD
		public ReservationBase Reservation { get; private set; }

		// Token: 0x06000F38 RID: 3896 RVA: 0x000227C6 File Offset: 0x000209C6
		public IDisposable Start()
		{
			return this.Start(CallChargeType.Include);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x000227CF File Offset: 0x000209CF
		public IDisposable StartExclusive()
		{
			return this.Start(CallChargeType.Exclude);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000227D8 File Offset: 0x000209D8
		public IDisposable Start(CallChargeType callChargeType)
		{
			return new ResourceHealthTracker.BudgetCostHandle(callChargeType, this);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000227E4 File Offset: 0x000209E4
		public void Charge(uint bytes)
		{
			if (this.Reservation != null)
			{
				foreach (ResourceBase resourceBase in this.Reservation.ReservedResources)
				{
					resourceBase.Charge(bytes);
				}
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00022844 File Offset: 0x00020A44
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				lock (this.syncRoot)
				{
				}
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00022884 File Offset: 0x00020A84
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ResourceHealthTracker>(this);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0002288C File Offset: 0x00020A8C
		private void EnterContext(ResourceHealthTracker.BudgetCostHandle context)
		{
			lock (this.syncRoot)
			{
				ResourceHealthTracker.BudgetCostHandle currentContext = this.openedContexts.Peek();
				this.openedContexts.Push(context);
				this.OpenOrCloseCostHandles(currentContext, context);
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000228E8 File Offset: 0x00020AE8
		private void ExitContext(ResourceHealthTracker.BudgetCostHandle context)
		{
			lock (this.syncRoot)
			{
				this.openedContexts.Pop();
				ResourceHealthTracker.BudgetCostHandle newContext = this.openedContexts.Peek();
				this.OpenOrCloseCostHandles(context, newContext);
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00022944 File Offset: 0x00020B44
		private void StartCharging()
		{
			this.startChargeTimestamp = ExDateTime.UtcNow;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00022954 File Offset: 0x00020B54
		private void StopCharging()
		{
			double totalMilliseconds = (ExDateTime.UtcNow - this.startChargeTimestamp).TotalMilliseconds;
			this.startChargeTimestamp = ExDateTime.MinValue;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00022985 File Offset: 0x00020B85
		private void OpenOrCloseCostHandles(ResourceHealthTracker.BudgetCostHandle currentContext, ResourceHealthTracker.BudgetCostHandle newContext)
		{
			if (!currentContext.IsInclusive && newContext.IsInclusive)
			{
				this.StartCharging();
				return;
			}
			if (currentContext.IsInclusive && !newContext.IsInclusive)
			{
				this.StopCharging();
			}
		}

		// Token: 0x04000894 RID: 2196
		private static readonly ResourceHealthTracker.BudgetCostHandle OuterContext = new ResourceHealthTracker.BudgetCostHandle(CallChargeType.Exclude, null);

		// Token: 0x04000895 RID: 2197
		private readonly object syncRoot = new object();

		// Token: 0x04000896 RID: 2198
		private ExDateTime startChargeTimestamp;

		// Token: 0x04000897 RID: 2199
		private Stack<ResourceHealthTracker.BudgetCostHandle> openedContexts;

		// Token: 0x02000197 RID: 407
		private class BudgetCostHandle : DisposeTrackableBase
		{
			// Token: 0x06000F44 RID: 3908 RVA: 0x000229C2 File Offset: 0x00020BC2
			public BudgetCostHandle(CallChargeType callChargeType, ResourceHealthTracker ownerTracker)
			{
				this.callChargeType = callChargeType;
				this.ownerTracker = ownerTracker;
				if (this.ownerTracker != null)
				{
					this.ownerTracker.EnterContext(this);
				}
			}

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000229EC File Offset: 0x00020BEC
			public bool IsInclusive
			{
				get
				{
					return this.callChargeType == CallChargeType.Include;
				}
			}

			// Token: 0x06000F46 RID: 3910 RVA: 0x000229F7 File Offset: 0x00020BF7
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose && this.ownerTracker != null)
				{
					this.ownerTracker.ExitContext(this);
				}
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x00022A10 File Offset: 0x00020C10
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<ResourceHealthTracker.BudgetCostHandle>(this);
			}

			// Token: 0x04000899 RID: 2201
			private CallChargeType callChargeType;

			// Token: 0x0400089A RID: 2202
			private ResourceHealthTracker ownerTracker;
		}
	}
}
