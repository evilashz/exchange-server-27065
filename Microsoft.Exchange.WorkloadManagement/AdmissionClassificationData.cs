using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000029 RID: 41
	internal class AdmissionClassificationData
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00006680 File Offset: 0x00004880
		public AdmissionClassificationData(WorkloadClassification classification, string id)
		{
			if (classification == WorkloadClassification.Unknown)
			{
				throw new ArgumentException("WorkloadClassification.Unknown is not acceptable value for the parameter.", "classification");
			}
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException("Data owner id should be non null or white space", "id");
			}
			this.Classification = classification;
			this.Id = id;
			this.AvailableAtLastStatusChange = true;
			this.FilledDuringThisCycle = true;
			this.ConcurrencyLimits = 0;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000066E1 File Offset: 0x000048E1
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000066E9 File Offset: 0x000048E9
		public string Id { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000066F2 File Offset: 0x000048F2
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000066FA File Offset: 0x000048FA
		public WorkloadClassification Classification { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00006703 File Offset: 0x00004903
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000670B File Offset: 0x0000490B
		public int ConcurrencyLimits { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006714 File Offset: 0x00004914
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000671C File Offset: 0x0000491C
		public int ConcurrencyUsed { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00006725 File Offset: 0x00004925
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000672D File Offset: 0x0000492D
		public double DelayFactor { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006736 File Offset: 0x00004936
		// (set) Token: 0x06000161 RID: 353 RVA: 0x0000673E File Offset: 0x0000493E
		public bool AvailableAtLastStatusChange { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00006747 File Offset: 0x00004947
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000674F File Offset: 0x0000494F
		public bool FilledDuringThisCycle { get; private set; }

		// Token: 0x06000164 RID: 356 RVA: 0x00006758 File Offset: 0x00004958
		public bool TryAquireSlot(out double delayFactor)
		{
			int num = this.ConcurrencyLimits - this.ConcurrencyUsed;
			if (num <= 0)
			{
				this.FilledDuringThisCycle = true;
				delayFactor = 0.0;
				return false;
			}
			this.ConcurrencyUsed++;
			if (num == 1)
			{
				this.FilledDuringThisCycle = true;
			}
			delayFactor = this.DelayFactor;
			return true;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000067AD File Offset: 0x000049AD
		public void ReleaseSlot()
		{
			this.ConcurrencyUsed--;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000067C0 File Offset: 0x000049C0
		public void Refresh(ResourceKey resource, ResourceLoad load)
		{
			int num = this.ConcurrencyLimits;
			double num2 = 0.0;
			if (!this.FilledDuringThisCycle)
			{
				this.FilledDuringThisCycle = (this.ConcurrencyUsed >= this.ConcurrencyLimits);
			}
			switch (load.State)
			{
			case ResourceLoadState.Unknown:
				if (this.ConcurrencyLimits == 0)
				{
					num = 1;
					num2 = 0.0;
				}
				ExTraceGlobals.AdmissionControlTracer.TraceDebug<ResourceKey, int, double>((long)this.GetHashCode(), "[AdmissionClassificationData.Refresh] Resource load for {0} is unknown. Setting new concurrency to {1} and delay factor to {2}.", resource, num, num2);
				break;
			case ResourceLoadState.Underloaded:
				if (this.FilledDuringThisCycle)
				{
					if (this.DelayFactor > 0.0)
					{
						num2 = 0.0;
					}
					else
					{
						num = this.ConcurrencyLimits + 1;
					}
					ExTraceGlobals.AdmissionControlTracer.TraceDebug((long)this.GetHashCode(), "[AdmissionClassificationData.Refresh] (Underloaded, Resource: {0}, Classification: {1}) All slots were filled in this cycle so updating delay factor to {2} and newConcurrency to {3}.", new object[]
					{
						resource,
						this.Classification,
						num2,
						num
					});
				}
				else
				{
					ExTraceGlobals.AdmissionControlTracer.TraceDebug<ResourceKey, WorkloadClassification>((long)this.GetHashCode(), "[AdmissionClassificationData.Refresh] (Underloaded, Resource: {0}, Classification: {1}) Slots were NOT filled in this cycle, so we will NOT update the delay factor nor concurrency.", resource, this.Classification);
				}
				break;
			case ResourceLoadState.Full:
				if (this.ConcurrencyLimits < 1)
				{
					num = 1;
				}
				break;
			case ResourceLoadState.Overloaded:
				if (this.ConcurrencyLimits > 1)
				{
					num = (int)((double)this.ConcurrencyLimits / load.LoadRatio);
					if (num == this.ConcurrencyLimits && num > 0)
					{
						num--;
					}
					if (num <= 0)
					{
						num = 1;
					}
					ExTraceGlobals.AdmissionControlTracer.TraceDebug<ResourceKey, WorkloadClassification, int>((long)this.GetHashCode(), "[AdmissionClassificationData.Refresh] Resource {0} is overloaded for classification: {1}. Setting new concurrency to {2}.", resource, this.Classification, num);
				}
				else if (this.ConcurrencyLimits == 1)
				{
					num = 1;
					num2 = load.LoadRatio - 1.0;
					ExTraceGlobals.AdmissionControlTracer.TraceDebug<ResourceKey, WorkloadClassification, double>((long)this.GetHashCode(), "[AdmissionClassificationData.Refresh] Resource {0} is overloaded for classification {1}. Run one thread with delay factor {2}.", resource, this.Classification, num2);
				}
				break;
			case ResourceLoadState.Critical:
				num = 0;
				num2 = 0.0;
				ExTraceGlobals.AdmissionControlTracer.TraceDebug<ResourceKey, WorkloadClassification, int>((long)this.GetHashCode(), "[AdmissionClassificationData.Refresh] Resource {0} is critical for Classification {1}. Setting new concurrency to {2}.", resource, this.Classification, num);
				break;
			}
			this.FilledDuringThisCycle = (num == 0);
			this.ConcurrencyLimits = num;
			this.DelayFactor = num2;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000069D4 File Offset: 0x00004BD4
		public bool RefreshAvailable()
		{
			int num = this.ConcurrencyLimits - this.ConcurrencyUsed;
			bool flag = num > 0;
			bool availableAtLastStatusChange = this.AvailableAtLastStatusChange;
			this.AvailableAtLastStatusChange = flag;
			return availableAtLastStatusChange != flag;
		}
	}
}
