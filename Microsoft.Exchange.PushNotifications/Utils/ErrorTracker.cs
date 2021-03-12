using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Utils
{
	// Token: 0x0200003F RID: 63
	internal class ErrorTracker<T> where T : struct, IConvertible
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000050C8 File Offset: 0x000032C8
		public ErrorTracker(Dictionary<T, int> errorWeightTable, int maxErrorWeight, int backOffTimeInSeconds = 0, int baseDelayInMilliseconds = 0)
		{
			ArgumentValidator.ThrowIfNull("errorWeightTable", errorWeightTable);
			ArgumentValidator.ThrowIfZeroOrNegative("maxErrorWeight", maxErrorWeight);
			ArgumentValidator.ThrowIfNegative("backOffTimeInSeconds", backOffTimeInSeconds);
			ArgumentValidator.ThrowIfNegative("baseDelayInMilliseconds", baseDelayInMilliseconds);
			ArgumentValidator.ThrowIfZeroOrNegative("errorWeightTable.Count", errorWeightTable.Count);
			this.BackOffTime = backOffTimeInSeconds;
			this.MaxErrorWeight = maxErrorWeight;
			this.ErrorWeightTable = errorWeightTable;
			this.BaseDelay = baseDelayInMilliseconds;
			this.CurrentErrorWeight = 0;
			this.ResetEndTimes();
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005142 File Offset: 0x00003342
		public virtual ExDateTime DelayEndTime
		{
			get
			{
				return this.delayEndTime;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000514A File Offset: 0x0000334A
		public virtual ExDateTime BackOffEndTime
		{
			get
			{
				return this.backOffEndTime;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005152 File Offset: 0x00003352
		public virtual bool ShouldDelay
		{
			get
			{
				return this.DelayEndTime > ExDateTime.UtcNow;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005164 File Offset: 0x00003364
		public virtual bool ShouldBackOff
		{
			get
			{
				return this.BackOffEndTime > ExDateTime.UtcNow;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005176 File Offset: 0x00003376
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000517E File Offset: 0x0000337E
		public int CurrentErrorWeight { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005187 File Offset: 0x00003387
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000518F File Offset: 0x0000338F
		private Dictionary<T, int> ErrorWeightTable { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005198 File Offset: 0x00003398
		// (set) Token: 0x06000183 RID: 387 RVA: 0x000051A0 File Offset: 0x000033A0
		private int MaxErrorWeight { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000051A9 File Offset: 0x000033A9
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000051B1 File Offset: 0x000033B1
		private int BaseDelay { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000051BA File Offset: 0x000033BA
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000051C2 File Offset: 0x000033C2
		private int BackOffTime { get; set; }

		// Token: 0x06000188 RID: 392 RVA: 0x000051CB File Offset: 0x000033CB
		public virtual void ReportSuccess()
		{
			this.Reset();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000051D4 File Offset: 0x000033D4
		public virtual void ReportError(T failureType)
		{
			int num;
			if (!this.ErrorWeightTable.TryGetValue(failureType, out num))
			{
				throw new ArgumentException(string.Format("Element {0} is not being tracked by error tracker", failureType), "failureType");
			}
			this.CurrentErrorWeight += num;
			if (this.BackOffTime > 0 && this.CurrentErrorWeight >= this.MaxErrorWeight)
			{
				this.SetBackOffEndTime(this.BackOffTime);
				return;
			}
			if (this.BaseDelay > 0)
			{
				this.SetDelayEndTime(this.CurrentErrorWeight * this.BaseDelay);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000525C File Offset: 0x0000345C
		public virtual void ConsumeDelay(int amountInMilliseconds)
		{
			ArgumentValidator.ThrowIfNegative("amountInMilliseconds", amountInMilliseconds);
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (utcNow >= this.DelayEndTime)
			{
				return;
			}
			int val = (int)this.DelayEndTime.Subtract(utcNow).TotalMilliseconds;
			int num = Math.Min(val, amountInMilliseconds);
			if (num > 0)
			{
				Thread.Sleep(num);
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000052B5 File Offset: 0x000034B5
		public virtual void Reset()
		{
			this.CurrentErrorWeight = 0;
			this.ResetEndTimes();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000052C4 File Offset: 0x000034C4
		private void SetDelayEndTime(int delayTimeInMilliseconds)
		{
			this.delayEndTime = ExDateTime.UtcNow.AddMilliseconds((double)delayTimeInMilliseconds);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000052E8 File Offset: 0x000034E8
		private void SetBackOffEndTime(int backOffTimeInSeconds)
		{
			this.backOffEndTime = ExDateTime.UtcNow.AddSeconds((double)backOffTimeInSeconds);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000530A File Offset: 0x0000350A
		private void ResetEndTimes()
		{
			this.delayEndTime = ExDateTime.MinValue;
			this.backOffEndTime = ExDateTime.MinValue;
		}

		// Token: 0x04000086 RID: 134
		private ExDateTime delayEndTime;

		// Token: 0x04000087 RID: 135
		private ExDateTime backOffEndTime;
	}
}
