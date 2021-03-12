using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009B0 RID: 2480
	internal class CostHandle : DisposeTrackableBase
	{
		// Token: 0x06007275 RID: 29301 RVA: 0x0017B2A5 File Offset: 0x001794A5
		public CostHandle(Budget budget, CostType costType, Action<CostHandle> onRelease, string description, TimeSpan preCharge = default(TimeSpan)) : this(budget, onRelease, description, preCharge)
		{
			this.CostType = costType;
		}

		// Token: 0x06007276 RID: 29302 RVA: 0x0017B2BC File Offset: 0x001794BC
		private CostHandle(Budget budget, Action<CostHandle> releaseAction, string description, TimeSpan preCharge)
		{
			if (budget == null)
			{
				throw new ArgumentNullException("budget");
			}
			if (string.IsNullOrEmpty(description))
			{
				ExWatson.SendReport(new ArgumentNullException("cost handle description is null or empty"), ReportOptions.DoNotCollectDumps | ReportOptions.DeepStackTraceHash, null);
			}
			if (preCharge < TimeSpan.Zero)
			{
				throw new ArgumentException("preCharge cannot be a negative timespan", "preCharge");
			}
			this.Budget = budget;
			this.Key = Interlocked.Increment(ref CostHandle.nextKey);
			this.StartTime = TimeProvider.UtcNow - preCharge;
			this.PreCharge = preCharge;
			this.ReleaseAction = releaseAction;
			this.Budget.AddOutstandingAction(this);
			this.DisposedByThread = -1;
			this.DisposedAt = DateTime.MinValue;
			this.Description = description;
			this.MaxLiveTime = Budget.GetMaxActionTime(this.CostType);
		}

		// Token: 0x1700285C RID: 10332
		// (get) Token: 0x06007277 RID: 29303 RVA: 0x0017B38D File Offset: 0x0017958D
		// (set) Token: 0x06007278 RID: 29304 RVA: 0x0017B395 File Offset: 0x00179595
		internal TimeSpan MaxLiveTime { get; set; }

		// Token: 0x1700285D RID: 10333
		// (get) Token: 0x06007279 RID: 29305 RVA: 0x0017B39E File Offset: 0x0017959E
		// (set) Token: 0x0600727A RID: 29306 RVA: 0x0017B3A6 File Offset: 0x001795A6
		internal long Key { get; private set; }

		// Token: 0x1700285E RID: 10334
		// (get) Token: 0x0600727B RID: 29307 RVA: 0x0017B3AF File Offset: 0x001795AF
		// (set) Token: 0x0600727C RID: 29308 RVA: 0x0017B3B7 File Offset: 0x001795B7
		internal int DisposedByThread { get; private set; }

		// Token: 0x1700285F RID: 10335
		// (get) Token: 0x0600727D RID: 29309 RVA: 0x0017B3C0 File Offset: 0x001795C0
		// (set) Token: 0x0600727E RID: 29310 RVA: 0x0017B3C8 File Offset: 0x001795C8
		internal DateTime DisposedAt { get; private set; }

		// Token: 0x17002860 RID: 10336
		// (get) Token: 0x0600727F RID: 29311 RVA: 0x0017B3D1 File Offset: 0x001795D1
		// (set) Token: 0x06007280 RID: 29312 RVA: 0x0017B3D9 File Offset: 0x001795D9
		internal Budget Budget { get; private set; }

		// Token: 0x17002861 RID: 10337
		// (get) Token: 0x06007281 RID: 29313 RVA: 0x0017B3E2 File Offset: 0x001795E2
		// (set) Token: 0x06007282 RID: 29314 RVA: 0x0017B3EA File Offset: 0x001795EA
		internal DateTime StartTime { get; private set; }

		// Token: 0x17002862 RID: 10338
		// (get) Token: 0x06007283 RID: 29315 RVA: 0x0017B3F3 File Offset: 0x001795F3
		// (set) Token: 0x06007284 RID: 29316 RVA: 0x0017B3FB File Offset: 0x001795FB
		internal CostType CostType { get; private set; }

		// Token: 0x17002863 RID: 10339
		// (get) Token: 0x06007285 RID: 29317 RVA: 0x0017B404 File Offset: 0x00179604
		// (set) Token: 0x06007286 RID: 29318 RVA: 0x0017B40C File Offset: 0x0017960C
		internal Action<CostHandle> ReleaseAction { get; private set; }

		// Token: 0x17002864 RID: 10340
		// (get) Token: 0x06007287 RID: 29319 RVA: 0x0017B415 File Offset: 0x00179615
		// (set) Token: 0x06007288 RID: 29320 RVA: 0x0017B41D File Offset: 0x0017961D
		internal bool LeakLogged { get; set; }

		// Token: 0x17002865 RID: 10341
		// (get) Token: 0x06007289 RID: 29321 RVA: 0x0017B426 File Offset: 0x00179626
		// (set) Token: 0x0600728A RID: 29322 RVA: 0x0017B42E File Offset: 0x0017962E
		internal TimeSpan PreCharge { get; private set; }

		// Token: 0x17002866 RID: 10342
		// (get) Token: 0x0600728B RID: 29323 RVA: 0x0017B437 File Offset: 0x00179637
		// (set) Token: 0x0600728C RID: 29324 RVA: 0x0017B43F File Offset: 0x0017963F
		internal string Description { get; private set; }

		// Token: 0x0600728D RID: 29325 RVA: 0x0017B448 File Offset: 0x00179648
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CostHandle>(this);
		}

		// Token: 0x0600728E RID: 29326 RVA: 0x0017B450 File Offset: 0x00179650
		protected override void InternalDispose(bool isDisposing)
		{
			if (!this.disposed && isDisposing)
			{
				lock (this.instanceLock)
				{
					if (!this.disposed)
					{
						this.Budget.End(this);
						if (this.ReleaseAction != null)
						{
							this.ReleaseAction(this);
						}
						this.disposed = true;
						this.DisposedByThread = Environment.CurrentManagedThreadId;
						this.DisposedAt = TimeProvider.UtcNow;
					}
				}
			}
		}

		// Token: 0x04004A1D RID: 18973
		private static long nextKey = long.MinValue;

		// Token: 0x04004A1E RID: 18974
		private bool disposed;

		// Token: 0x04004A1F RID: 18975
		private object instanceLock = new object();
	}
}
