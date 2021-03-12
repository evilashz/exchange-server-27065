using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000CC RID: 204
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SubscriptionStateTransitionHelper
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x0001DFD1 File Offset: 0x0001C1D1
		public SubscriptionStateTransitionHelper(PimAggregationSubscription subscription)
		{
			this.subscription = subscription;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001DFE0 File Offset: 0x0001C1E0
		public void DisableAsPoisonous()
		{
			if (this.subscription.Status == AggregationStatus.Poisonous)
			{
				throw new InvalidOperationException("method shouldn't be invoked for a poisonous subscription.");
			}
			if (this.subscription.Status == AggregationStatus.InvalidVersion)
			{
				throw new InvalidOperationException("method shouldn't be invoked for an invalid version subscription.");
			}
			this.subscription.Status = AggregationStatus.Poisonous;
			this.subscription.DetailedAggregationStatus = DetailedAggregationStatus.None;
			if (string.IsNullOrEmpty(this.subscription.PoisonCallstack))
			{
				this.subscription.PoisonCallstack = this.ManuallyDisabledIndicator();
				return;
			}
			this.subscription.PoisonCallstack = this.ManuallyDisabledIndicator() + Environment.NewLine + this.subscription.PoisonCallstack;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001E084 File Offset: 0x0001C284
		public void Disable()
		{
			if (this.subscription.Status == AggregationStatus.Disabled)
			{
				return;
			}
			if (this.subscription.Status == AggregationStatus.InvalidVersion)
			{
				throw new InvalidOperationException("method shouldn't be invoked for an invalid version subscription.");
			}
			if (this.subscription.Status == AggregationStatus.Poisonous)
			{
				this.ResetPoisonProperties();
			}
			this.subscription.Status = AggregationStatus.Disabled;
			this.subscription.DetailedAggregationStatus = DetailedAggregationStatus.None;
			if (string.IsNullOrEmpty(this.subscription.Diagnostics))
			{
				this.subscription.Diagnostics = this.ManuallyDisabledIndicator();
				return;
			}
			PimAggregationSubscription pimAggregationSubscription = this.subscription;
			pimAggregationSubscription.Diagnostics = pimAggregationSubscription.Diagnostics + Environment.NewLine + this.ManuallyDisabledIndicator();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001E12C File Offset: 0x0001C32C
		public void Enable()
		{
			AggregationStatus status = this.subscription.Status;
			if (status == AggregationStatus.Poisonous)
			{
				throw new InvalidOperationException("method shouldn't be invoked for a poisonous subscription.");
			}
			if (status == AggregationStatus.InvalidVersion)
			{
				throw new InvalidOperationException("method shouldn't be invoked for an invalid version subscription.");
			}
			if (status == AggregationStatus.InProgress || status == AggregationStatus.Succeeded)
			{
				return;
			}
			this.SetEnabledStatus();
			if (status != AggregationStatus.Delayed)
			{
				this.ResetOutageDetectionProperties();
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001E17C File Offset: 0x0001C37C
		public void EnableFromPoison()
		{
			if (this.subscription.Status != AggregationStatus.Poisonous)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "method shouldn't be invoked for non-poisonous subscriptions, actual status: {0}", new object[]
				{
					this.subscription.Status
				}));
			}
			this.SetEnabledStatus();
			this.ResetPoisonProperties();
			this.ResetOutageDetectionProperties();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001E1D9 File Offset: 0x0001C3D9
		protected virtual DateTime ResetAdjustedLastSuccessfulSyncTime()
		{
			return DateTime.UtcNow;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001E1E0 File Offset: 0x0001C3E0
		private void SetEnabledStatus()
		{
			this.subscription.Status = AggregationStatus.InProgress;
			this.subscription.DetailedAggregationStatus = DetailedAggregationStatus.None;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001E1FA File Offset: 0x0001C3FA
		private void ResetOutageDetectionProperties()
		{
			this.subscription.AdjustedLastSuccessfulSyncTime = this.ResetAdjustedLastSuccessfulSyncTime();
			this.subscription.OutageDetectionDiagnostics = string.Empty;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001E21D File Offset: 0x0001C41D
		private void ResetPoisonProperties()
		{
			this.subscription.PoisonCallstack = null;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001E22C File Offset: 0x0001C42C
		private string ManuallyDisabledIndicator()
		{
			return string.Format(CultureInfo.InvariantCulture, "Manually disabled on {0}", new object[]
			{
				DateTime.UtcNow
			});
		}

		// Token: 0x04000340 RID: 832
		private PimAggregationSubscription subscription;
	}
}
