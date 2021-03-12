using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.FaultInjection;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002E6 RID: 742
	internal class StageRetryDetails
	{
		// Token: 0x0600168E RID: 5774 RVA: 0x0005FE8C File Offset: 0x0005E08C
		internal StageRetryDetails(StageRetryDetails.FinalAction finalAction, TimeSpan retryInterval, TimeSpan allowedTimeForRetries)
		{
			this.finalAction = finalAction;
			this.retryInterval = retryInterval;
			this.stageEnteredTime = ExDateTime.UtcNow;
			this.lastRunTime = ExDateTime.UtcNow;
			this.allowedTimeForRetries = allowedTimeForRetries;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0005FEC6 File Offset: 0x0005E0C6
		internal StageRetryDetails(StageRetryDetails.FinalAction finalAction) : this(finalAction, TimeSpan.Zero, TimeSpan.Zero)
		{
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0005FED9 File Offset: 0x0005E0D9
		internal bool IsStageOptional
		{
			get
			{
				return this.finalAction == StageRetryDetails.FinalAction.SkipStage;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0005FEE4 File Offset: 0x0005E0E4
		internal bool TimeToTry
		{
			get
			{
				if (this.stageNeverRan)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "StageRetryDetails : stageneverRan = true", new object[0]);
					return true;
				}
				ExDateTime exDateTime = this.lastRunTime + this.retryInterval;
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "StageRetryDetails : this.lastRunTime = {0}, this.retryInterval = {1}, temp ={2}, now = {3}", new object[]
				{
					this.lastRunTime,
					this.retryInterval,
					exDateTime,
					ExDateTime.UtcNow
				});
				return ExDateTime.UtcNow >= exDateTime;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0005FF8C File Offset: 0x0005E18C
		internal bool IsTimeToGiveUp
		{
			get
			{
				if (TimeSpan.Zero.Equals(this.allowedTimeForRetries))
				{
					return true;
				}
				bool result = ExDateTime.UtcNow >= this.stageEnteredTime + this.allowedTimeForRetries;
				FaultInjectionUtils.FaultInjectChangeValue<bool>(2162568509U, ref result);
				return result;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0005FFD9 File Offset: 0x0005E1D9
		internal TimeSpan TotalDelayDueToThisStage
		{
			get
			{
				return ExDateTime.UtcNow - this.stageEnteredTime;
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005FFEB File Offset: 0x0005E1EB
		internal void UpgdateStageRunTimestamp()
		{
			this.stageNeverRan = false;
			this.lastRunTime = ExDateTime.UtcNow;
		}

		// Token: 0x04000D5C RID: 3420
		private StageRetryDetails.FinalAction finalAction;

		// Token: 0x04000D5D RID: 3421
		private TimeSpan retryInterval;

		// Token: 0x04000D5E RID: 3422
		private TimeSpan allowedTimeForRetries;

		// Token: 0x04000D5F RID: 3423
		private ExDateTime stageEnteredTime;

		// Token: 0x04000D60 RID: 3424
		private ExDateTime lastRunTime;

		// Token: 0x04000D61 RID: 3425
		private bool stageNeverRan = true;

		// Token: 0x020002E7 RID: 743
		internal enum FinalAction
		{
			// Token: 0x04000D63 RID: 3427
			DropMessage,
			// Token: 0x04000D64 RID: 3428
			SkipStage
		}
	}
}
