using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Configuration
{
	// Token: 0x0200009F RID: 159
	internal abstract class PerformanceCounterSession
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x00015FE8 File Offset: 0x000141E8
		public PerformanceCounterSession(bool enablePerformanceCounters)
		{
			this.EnablePerformanceCounters = enablePerformanceCounters;
			this.stopwatch = new Stopwatch();
			this.stopwatch.Start();
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600052D RID: 1325
		protected abstract ExPerformanceCounter RequestTime { get; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600052E RID: 1326
		protected abstract ExPerformanceCounter RequestCount { get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600052F RID: 1327
		protected abstract ExPerformanceCounter TimeSinceLast { get; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000530 RID: 1328
		protected abstract PerformanceCounterSession.HitRatePerformanceCounters Success { get; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000531 RID: 1329
		protected abstract PerformanceCounterSession.HitRatePerformanceCounters SystemError { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000532 RID: 1330
		protected abstract PerformanceCounterSession.HitRatePerformanceCounters UserError { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001600D File Offset: 0x0001420D
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00016015 File Offset: 0x00014215
		private protected bool EnablePerformanceCounters { protected get; private set; }

		// Token: 0x06000535 RID: 1333 RVA: 0x00016020 File Offset: 0x00014220
		public void Initialize()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "EnablePerformanceCounters = {0}", this.EnablePerformanceCounters);
			if (this.EnablePerformanceCounters)
			{
				this.RequestCount.Increment();
				ExTraceGlobals.BackSyncTracer.TraceDebug<long>((long)SyncConfiguration.TraceId, "RequestCount = {0}", this.RequestCount.RawValue);
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001607C File Offset: 0x0001427C
		public virtual void Finish()
		{
			if (this.EnablePerformanceCounters)
			{
				this.Success.RecordHit(this.userErrorCount == 0 && this.systemErrorCount == 0);
				this.UserError.RecordHit(this.userErrorCount > 0);
				this.SystemError.RecordHit(this.systemErrorCount > 0);
				this.TimeSinceLast.RawValue = Stopwatch.GetTimestamp();
				this.RequestTime.RawValue = (long)Math.Round(this.stopwatch.Elapsed.TotalSeconds, 0);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001610D File Offset: 0x0001430D
		public virtual void ReportChangeCount(int changeCount)
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001610F File Offset: 0x0001430F
		public virtual void ReportSameCookie(bool sameCookie)
		{
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00016111 File Offset: 0x00014311
		public void IncrementUserError()
		{
			this.userErrorCount++;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00016121 File Offset: 0x00014321
		public void IncrementSystemError()
		{
			this.systemErrorCount++;
		}

		// Token: 0x04000296 RID: 662
		private Stopwatch stopwatch;

		// Token: 0x04000297 RID: 663
		private int userErrorCount;

		// Token: 0x04000298 RID: 664
		private int systemErrorCount;

		// Token: 0x020000A0 RID: 160
		public class HitRatePerformanceCounters
		{
			// Token: 0x0600053B RID: 1339 RVA: 0x00016131 File Offset: 0x00014331
			public HitRatePerformanceCounters(ExPerformanceCounter hit, ExPerformanceCounter rate, ExPerformanceCounter custom)
			{
				this.hit = hit;
				this.rate = rate;
				this.custom = custom;
			}

			// Token: 0x0600053C RID: 1340 RVA: 0x00016150 File Offset: 0x00014350
			public void RecordHit(bool hit)
			{
				this.hit.RawValue = (hit ? 100L : 0L);
				PerformanceCounterSession.HitRatePerformanceCounters.MostRecentHitCollection mostRecentHitCollection = new PerformanceCounterSession.HitRatePerformanceCounters.MostRecentHitCollection((ulong)this.custom.RawValue);
				mostRecentHitCollection.Add(hit);
				this.custom.RawValue = (long)mostRecentHitCollection.RawValue;
				this.rate.RawValue = (long)mostRecentHitCollection.HitRate;
			}

			// Token: 0x0400029A RID: 666
			private const long CounterValueHit = 100L;

			// Token: 0x0400029B RID: 667
			private const long CounterValueNoHit = 0L;

			// Token: 0x0400029C RID: 668
			private ExPerformanceCounter hit;

			// Token: 0x0400029D RID: 669
			private ExPerformanceCounter rate;

			// Token: 0x0400029E RID: 670
			private ExPerformanceCounter custom;

			// Token: 0x020000A1 RID: 161
			public class MostRecentHitCollection
			{
				// Token: 0x0600053D RID: 1341 RVA: 0x000161AD File Offset: 0x000143AD
				public MostRecentHitCollection(ulong value)
				{
					this.RawValue = value;
				}

				// Token: 0x170001F1 RID: 497
				// (get) Token: 0x0600053E RID: 1342 RVA: 0x000161BC File Offset: 0x000143BC
				// (set) Token: 0x0600053F RID: 1343 RVA: 0x000161C4 File Offset: 0x000143C4
				public ulong RawValue { get; private set; }

				// Token: 0x170001F2 RID: 498
				// (get) Token: 0x06000540 RID: 1344 RVA: 0x000161D0 File Offset: 0x000143D0
				public int HitRate
				{
					get
					{
						int count = this.Count;
						if (count == 0)
						{
							return 0;
						}
						return (this.HitCount * 1000 / count + 5) / 10;
					}
				}

				// Token: 0x170001F3 RID: 499
				// (get) Token: 0x06000541 RID: 1345 RVA: 0x000161FC File Offset: 0x000143FC
				public int Count
				{
					get
					{
						return PerformanceCounterSession.HitRatePerformanceCounters.MostRecentHitCollection.CountBits(this.Mask);
					}
				}

				// Token: 0x170001F4 RID: 500
				// (get) Token: 0x06000542 RID: 1346 RVA: 0x00016209 File Offset: 0x00014409
				private int HitCount
				{
					get
					{
						return PerformanceCounterSession.HitRatePerformanceCounters.MostRecentHitCollection.CountBits(this.Samples);
					}
				}

				// Token: 0x170001F5 RID: 501
				// (get) Token: 0x06000543 RID: 1347 RVA: 0x00016216 File Offset: 0x00014416
				private ulong Mask
				{
					get
					{
						return PerformanceCounterSession.HitRatePerformanceCounters.MostRecentHitCollection.GetHigh(this.RawValue);
					}
				}

				// Token: 0x170001F6 RID: 502
				// (get) Token: 0x06000544 RID: 1348 RVA: 0x00016223 File Offset: 0x00014423
				private ulong Samples
				{
					get
					{
						return PerformanceCounterSession.HitRatePerformanceCounters.MostRecentHitCollection.GetLow(this.RawValue);
					}
				}

				// Token: 0x06000545 RID: 1349 RVA: 0x00016230 File Offset: 0x00014430
				public void Add(bool value)
				{
					ulong high = (this.Mask << 1) + 1UL;
					ulong low = (this.Samples << 1) + (ulong)(value ? 1L : 0L);
					this.RawValue = PerformanceCounterSession.HitRatePerformanceCounters.MostRecentHitCollection.Build(high, low);
				}

				// Token: 0x06000546 RID: 1350 RVA: 0x00016268 File Offset: 0x00014468
				private static ulong Build(ulong high, ulong low)
				{
					return (high << 32) + (low & (ulong)-1);
				}

				// Token: 0x06000547 RID: 1351 RVA: 0x00016273 File Offset: 0x00014473
				private static ulong GetHigh(ulong value)
				{
					return value >> 32;
				}

				// Token: 0x06000548 RID: 1352 RVA: 0x00016279 File Offset: 0x00014479
				private static ulong GetLow(ulong value)
				{
					return value & (ulong)-1;
				}

				// Token: 0x06000549 RID: 1353 RVA: 0x00016280 File Offset: 0x00014480
				private static int CountBits(ulong value)
				{
					int num = 0;
					while (value > 0UL)
					{
						if ((value & 1UL) == 1UL)
						{
							num++;
						}
						value >>= 1;
					}
					return num;
				}
			}
		}
	}
}
