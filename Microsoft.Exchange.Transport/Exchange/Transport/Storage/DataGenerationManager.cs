using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000DE RID: 222
	internal sealed class DataGenerationManager<TGeneration> where TGeneration : DataGeneration, new()
	{
		// Token: 0x060007F6 RID: 2038 RVA: 0x0001FD84 File Offset: 0x0001DF84
		public DataGenerationManager(TimeSpan newGenerationTimeSpan, Func<TimeSpan> ageToCleanup, DataGenerationCategory category, DataGenerationTable table, int recentGenerationCount, bool autoExpireEnabled, bool autoCreateEnabled, bool expireSuspended = false)
		{
			if (expireSuspended)
			{
				this.SuspendDataCleanup();
			}
			this.autoCreateEnabled = autoCreateEnabled;
			this.autoExpireEnabled = autoExpireEnabled;
			this.category = category;
			this.generationTimeSpan = newGenerationTimeSpan;
			this.maintenanceTimer = new GuardedTimer(delegate(object o)
			{
				this.DoMaintenance();
			});
			this.ageToCleanup = ageToCleanup;
			this.table = table;
			this.recentGenerationCount = recentGenerationCount;
			Stopwatch stopwatch = Stopwatch.StartNew();
			foreach (DataGenerationRow dataGenerationRow in table.GetAllRows())
			{
				if (dataGenerationRow.Category == (int)category)
				{
					TGeneration generation = Activator.CreateInstance<TGeneration>();
					generation.Load(dataGenerationRow);
					this.AddGeneration(generation);
				}
			}
			this.ScheduleMaintenance();
			ExTraceGlobals.StorageTracer.TraceDebug<DataGenerationCategory, int, TimeSpan>((long)this.GetHashCode(), "Generation Manager for {0} created and loaded with {1} pre-existing generations in {2}.", category, this.generations.Count, stopwatch.Elapsed);
			ExTraceGlobals.StorageTracer.TracePerformance<DataGenerationCategory, int, TimeSpan>((long)this.GetHashCode(), "Generation Manager for {0} created and loaded with {1} pre-existing generations in {2}.", category, this.generations.Count, stopwatch.Elapsed);
			this.perfCounters.GenerationCount.IncrementBy((long)this.generations.Count);
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0001FF3C File Offset: 0x0001E13C
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0001FF44 File Offset: 0x0001E144
		internal Func<DateTime> ReferenceClock
		{
			get
			{
				return this.referenceClock;
			}
			set
			{
				this.referenceClock = value;
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001FF4D File Offset: 0x0001E14D
		public void SuspendDataCleanup()
		{
			this.expirationLock.EnterReadLock();
			Interlocked.Increment(ref this.suspendExpirationCount);
			this.expirationLock.ExitReadLock();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001FF74 File Offset: 0x0001E174
		public void ResumeDataCleanup()
		{
			this.expirationLock.EnterReadLock();
			try
			{
				if (Interlocked.Decrement(ref this.suspendExpirationCount) < 0)
				{
					throw new InvalidOperationException("Cleanup was not suspended.");
				}
			}
			finally
			{
				this.expirationLock.ExitReadLock();
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001FFC4 File Offset: 0x0001E1C4
		public void SuspendDataCleanup(DateTime startDate, DateTime endDate)
		{
			this.expirationLock.EnterUpgradeableReadLock();
			this.suspendedExpirationPeriods.Add(new DataGenerationManager<TGeneration>.DatePeriod
			{
				Start = startDate,
				End = endDate
			});
			this.expirationLock.ExitUpgradeableReadLock();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0002000C File Offset: 0x0001E20C
		public void ResumeDataCleanup(DateTime startDate, DateTime endDate)
		{
			this.expirationLock.EnterUpgradeableReadLock();
			try
			{
				if (!this.suspendedExpirationPeriods.Remove(new DataGenerationManager<TGeneration>.DatePeriod
				{
					Start = startDate,
					End = endDate
				}))
				{
					throw new InvalidOperationException("Cleanup was not suspended for this period.");
				}
			}
			finally
			{
				this.expirationLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00020094 File Offset: 0x0001E294
		public TGeneration GetGeneration(int generationId)
		{
			this.generationsLock.EnterReadLock();
			TGeneration tgeneration;
			try
			{
				tgeneration = this.generations.Values.FirstOrDefault((TGeneration g) => g.GenerationId == generationId);
			}
			finally
			{
				this.generationsLock.ExitReadLock();
			}
			if (tgeneration != null)
			{
				tgeneration.Attach();
			}
			return tgeneration;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00020114 File Offset: 0x0001E314
		public TGeneration GetCurrentGeneration()
		{
			return this.GetGeneration(this.ReferenceClock());
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00020170 File Offset: 0x0001E370
		public TGeneration GetGeneration(DateTime timeStamp)
		{
			DataGenerationManager<TGeneration>.GenerationKey genKey = new DataGenerationManager<TGeneration>.GenerationKey(this.GetGenerationStart(timeStamp), this.generationTimeSpan);
			TGeneration tgeneration = this.generationCache.Find((TGeneration gen) => gen.StartTime == genKey.StartTime && gen.Duration == genKey.Duration);
			if (tgeneration == null)
			{
				this.generationsLock.EnterReadLock();
				try
				{
					ExTraceGlobals.StorageTracer.TraceDebug<DateTime>((long)this.GetHashCode(), "Could not find generation for {0} in cache, looking in the list.", timeStamp);
					this.generations.TryGetValue(genKey, out tgeneration);
				}
				finally
				{
					this.generationsLock.ExitReadLock();
				}
				if (tgeneration == null)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<DateTime>((long)this.GetHashCode(), "Could not find generation for {0}, creating new one.", timeStamp);
					tgeneration = this.CreateGeneration(genKey);
				}
			}
			if (tgeneration != null)
			{
				tgeneration.Attach();
			}
			return tgeneration;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0002027C File Offset: 0x0001E47C
		public TGeneration[] GetGenerations(DateTime startDate, DateTime endDate)
		{
			this.generationsLock.EnterReadLock();
			TGeneration[] array;
			try
			{
				array = (from gen in this.generations.Values
				where gen.Contains(startDate, endDate)
				select gen).ToArray<TGeneration>();
			}
			finally
			{
				this.generationsLock.ExitReadLock();
			}
			foreach (TGeneration tgeneration in array)
			{
				tgeneration.Attach();
			}
			ExTraceGlobals.StorageTracer.TraceDebug<DateTime, DateTime, int>((long)this.GetHashCode(), "GetGenerations for ({0},{1}) returned {2} generations.", startDate, endDate, array.Length);
			return array;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00020344 File Offset: 0x0001E544
		public TGeneration[] GetRecentGenerations()
		{
			DateTime dateTime = this.ReferenceClock();
			DateTime startDate = dateTime;
			if (this.recentGenerationCount > 0)
			{
				startDate = startDate.AddMilliseconds(-1.0 * this.generationTimeSpan.TotalMilliseconds * (double)(this.recentGenerationCount - 1));
			}
			else
			{
				startDate = DateTime.MinValue;
			}
			return this.GetGenerations(startDate, dateTime);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00020438 File Offset: 0x0001E638
		public void ExpireGenerations()
		{
			this.expirationLock.EnterWriteLock();
			try
			{
				if (this.suspendExpirationCount > 0)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<int>((long)this.GetHashCode(), "DataCleanup/Expiration was called but is disabled with {0} requests.", this.suspendExpirationCount);
				}
				else
				{
					DateTime now = this.ReferenceClock();
					this.generationsLock.EnterReadLock();
					TGeneration[] array;
					try
					{
						array = (from g in this.generations.Values.TakeWhile((TGeneration g) => now - g.EndTime > this.ageToCleanup())
						where !this.suspendedExpirationPeriods.Any((DataGenerationManager<TGeneration>.DatePeriod p) => g.Contains(p.Start, p.End))
						select g).ToArray<TGeneration>();
					}
					finally
					{
						this.generationsLock.ExitReadLock();
					}
					ExTraceGlobals.StorageTracer.TraceDebug<int>((long)this.GetHashCode(), "Expiring {0} generations.", array.Length);
					this.perfCounters.GenerationExpiredCount.RawValue = (long)array.Length;
					Stopwatch stopwatch = new Stopwatch();
					foreach (TGeneration generation in array)
					{
						stopwatch.Start();
						generation.Attach();
						GenerationCleanupMode generationCleanupMode = generation.Cleanup();
						if (generationCleanupMode == GenerationCleanupMode.Drop)
						{
							this.RemoveGeneration(generation);
							this.perfCounters.GenerationExpiredCount.Decrement();
						}
						stopwatch.Stop();
						ExTraceGlobals.StorageTracer.TracePerformance<TimeSpan, GenerationCleanupMode>((long)this.GetHashCode(), "Expiring generation took {0} mode = {1}", stopwatch.Elapsed, generationCleanupMode);
						this.perfCounters.GenerationLastCleanupLatency.RawValue = stopwatch.ElapsedMilliseconds;
						stopwatch.Reset();
					}
				}
			}
			finally
			{
				this.expirationLock.ExitWriteLock();
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00020618 File Offset: 0x0001E818
		public void Unload()
		{
			this.maintenanceTimer.Change(-1, -1);
			this.maintenanceTimer.Dispose(true);
			foreach (TGeneration tgeneration in this.generations.Values)
			{
				tgeneration.Unload();
				ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "Generation Manager detached table {0}", tgeneration.Name);
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000206BC File Offset: 0x0001E8BC
		public XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement("GenerationManager");
			xelement.Add(new XElement("SuspendAllExpirationCount", this.suspendExpirationCount));
			xelement.Add(new XElement("SuspendedExpirationPeriods", from p in this.suspendedExpirationPeriods
			select p.GetDiagnosticInfo()));
			if (argument.Equals("generations", StringComparison.InvariantCultureIgnoreCase))
			{
				foreach (TGeneration tgeneration in this.generations.Values)
				{
					xelement.Add(tgeneration.GetDiagnosticInfo(argument));
				}
			}
			return xelement;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00020798 File Offset: 0x0001E998
		private TGeneration CreateGeneration(DateTime timeStamp)
		{
			return this.CreateGeneration(new DataGenerationManager<TGeneration>.GenerationKey(this.GetGenerationStart(timeStamp), this.generationTimeSpan));
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000207B4 File Offset: 0x0001E9B4
		private TGeneration CreateGeneration(DataGenerationManager<TGeneration>.GenerationKey genKey)
		{
			this.generationsLock.EnterUpgradeableReadLock();
			TGeneration result;
			try
			{
				TGeneration tgeneration;
				this.generations.TryGetValue(genKey, out tgeneration);
				if (tgeneration == null)
				{
					tgeneration = Activator.CreateInstance<TGeneration>();
					tgeneration.Load(genKey.StartTime, genKey.StartTime + genKey.Duration, this.category, this.table);
					this.AddGeneration(tgeneration);
				}
				result = tgeneration;
			}
			finally
			{
				this.generationsLock.ExitUpgradeableReadLock();
			}
			return result;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00020844 File Offset: 0x0001EA44
		private void ScheduleMaintenance()
		{
			if (this.autoExpireEnabled || this.autoCreateEnabled)
			{
				DateTime dateTime = this.ReferenceClock();
				TimeSpan timeSpan = this.GetGenerationStart(dateTime).Add(TimeSpan.FromTicks(this.generationTimeSpan.Ticks * 90L / 100L)).Subtract(dateTime);
				if (this.autoCreateEnabled)
				{
					this.CreateGeneration(dateTime);
					if (timeSpan.Ticks < 0L)
					{
						this.CreateGeneration(dateTime + this.generationTimeSpan);
					}
				}
				if (timeSpan.Ticks < 0L)
				{
					timeSpan = timeSpan.Add(this.generationTimeSpan);
				}
				this.maintenanceTimer.Change(timeSpan, this.generationTimeSpan);
				ExTraceGlobals.StorageTracer.TraceDebug<DataGenerationCategory, TimeSpan, TimeSpan>((long)this.GetHashCode(), "Generation manager ({0}) maintenance scheduled for {1} from now and will repeat itself every {2}", this.category, timeSpan, this.generationTimeSpan);
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00020924 File Offset: 0x0001EB24
		private void DoMaintenance()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			if (this.autoCreateEnabled)
			{
				this.CreateGeneration(this.ReferenceClock() + this.generationTimeSpan);
			}
			if (this.autoExpireEnabled)
			{
				this.ExpireGenerations();
			}
			ExTraceGlobals.StorageTracer.TracePerformance<DataGenerationCategory, TimeSpan>((long)this.GetHashCode(), "Generation manager({0}) maintenance took {1}", this.category, stopwatch.Elapsed);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0002098C File Offset: 0x0001EB8C
		private DateTime GetGenerationStart(DateTime timeStamp)
		{
			return timeStamp.Subtract(TimeSpan.FromTicks(timeStamp.Ticks % this.generationTimeSpan.Ticks));
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000209BC File Offset: 0x0001EBBC
		private void RemoveGeneration(TGeneration generation)
		{
			this.generationsLock.EnterWriteLock();
			try
			{
				this.generations.Remove(new DataGenerationManager<TGeneration>.GenerationKey(generation));
				this.generationCache.Remove(generation);
				this.perfCounters.GenerationCount.Decrement();
			}
			finally
			{
				this.generationsLock.ExitWriteLock();
			}
			generation.Unload();
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00020A30 File Offset: 0x0001EC30
		private void AddGeneration(TGeneration generation)
		{
			this.generationsLock.EnterWriteLock();
			try
			{
				TransportHelpers.AttemptAddToDictionary<DataGenerationManager<TGeneration>.GenerationKey, TGeneration>(this.generations, new DataGenerationManager<TGeneration>.GenerationKey(generation), generation, null);
				this.generationCache.Add(generation);
				this.perfCounters.GenerationCount.Increment();
			}
			finally
			{
				this.generationsLock.ExitWriteLock();
			}
		}

		// Token: 0x04000400 RID: 1024
		private const int AutoCreateAdvancePercentage = 90;

		// Token: 0x04000401 RID: 1025
		private readonly Func<TimeSpan> ageToCleanup;

		// Token: 0x04000402 RID: 1026
		private readonly DataGenerationCategory category;

		// Token: 0x04000403 RID: 1027
		private readonly DataGenerationManager<TGeneration>.SimpleCache<TGeneration> generationCache = new DataGenerationManager<TGeneration>.SimpleCache<TGeneration>(3);

		// Token: 0x04000404 RID: 1028
		private readonly SortedList<DataGenerationManager<TGeneration>.GenerationKey, TGeneration> generations = new SortedList<DataGenerationManager<TGeneration>.GenerationKey, TGeneration>();

		// Token: 0x04000405 RID: 1029
		private readonly ReaderWriterLockSlim generationsLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		// Token: 0x04000406 RID: 1030
		private readonly ReaderWriterLockSlim expirationLock = new ReaderWriterLockSlim();

		// Token: 0x04000407 RID: 1031
		private readonly List<DataGenerationManager<TGeneration>.DatePeriod> suspendedExpirationPeriods = new List<DataGenerationManager<TGeneration>.DatePeriod>();

		// Token: 0x04000408 RID: 1032
		private readonly DatabasePerfCountersInstance perfCounters = DatabasePerfCounters.GetInstance("other");

		// Token: 0x04000409 RID: 1033
		private readonly GuardedTimer maintenanceTimer;

		// Token: 0x0400040A RID: 1034
		private readonly TimeSpan generationTimeSpan;

		// Token: 0x0400040B RID: 1035
		private readonly DataGenerationTable table;

		// Token: 0x0400040C RID: 1036
		private readonly int recentGenerationCount;

		// Token: 0x0400040D RID: 1037
		private readonly bool autoCreateEnabled;

		// Token: 0x0400040E RID: 1038
		private readonly bool autoExpireEnabled;

		// Token: 0x0400040F RID: 1039
		private int suspendExpirationCount;

		// Token: 0x04000410 RID: 1040
		private Func<DateTime> referenceClock = () => DateTime.UtcNow;

		// Token: 0x020000DF RID: 223
		private class SimpleCache<T> where T : class
		{
			// Token: 0x06000810 RID: 2064 RVA: 0x00020A98 File Offset: 0x0001EC98
			public SimpleCache(int size)
			{
				this.cachedInstances = new T[size];
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x00020ACC File Offset: 0x0001ECCC
			public T Find(Func<T, bool> func)
			{
				return this.cachedInstances.FirstOrDefault((T item) => item != null && func(item));
			}

			// Token: 0x06000812 RID: 2066 RVA: 0x00020B00 File Offset: 0x0001ED00
			public void Add(T item)
			{
				for (int i = this.cachedInstances.Length - 1; i >= 0; i--)
				{
					Interlocked.Exchange<T>(ref this.cachedInstances[i], (i == 0) ? item : this.cachedInstances[i - 1]);
				}
			}

			// Token: 0x06000813 RID: 2067 RVA: 0x00020B48 File Offset: 0x0001ED48
			public void Remove(T item)
			{
				for (int i = 0; i < this.cachedInstances.Length; i++)
				{
					Interlocked.CompareExchange<T>(ref this.cachedInstances[i], default(T), item);
				}
			}

			// Token: 0x04000413 RID: 1043
			private readonly T[] cachedInstances;
		}

		// Token: 0x020000E0 RID: 224
		private struct GenerationKey : IComparable<DataGenerationManager<TGeneration>.GenerationKey>
		{
			// Token: 0x06000814 RID: 2068 RVA: 0x00020B84 File Offset: 0x0001ED84
			public GenerationKey(DateTime start, TimeSpan duration)
			{
				this.StartTime = start;
				this.Duration = duration;
			}

			// Token: 0x06000815 RID: 2069 RVA: 0x00020B94 File Offset: 0x0001ED94
			public GenerationKey(TGeneration gen)
			{
				this = new DataGenerationManager<TGeneration>.GenerationKey(gen.StartTime, gen.Duration);
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x00020BB8 File Offset: 0x0001EDB8
			public int CompareTo(DataGenerationManager<TGeneration>.GenerationKey other)
			{
				int num = this.StartTime.CompareTo(other.StartTime);
				if (num == 0)
				{
					return this.Duration.CompareTo(other.Duration);
				}
				return num;
			}

			// Token: 0x04000414 RID: 1044
			public DateTime StartTime;

			// Token: 0x04000415 RID: 1045
			public TimeSpan Duration;
		}

		// Token: 0x020000E1 RID: 225
		private struct DatePeriod
		{
			// Token: 0x06000817 RID: 2071 RVA: 0x00020BF0 File Offset: 0x0001EDF0
			public XElement GetDiagnosticInfo()
			{
				XElement xelement = new XElement("DatePeriod");
				xelement.SetAttributeValue("Start", this.Start);
				xelement.SetAttributeValue("End", this.End);
				return xelement;
			}

			// Token: 0x04000416 RID: 1046
			public DateTime Start;

			// Token: 0x04000417 RID: 1047
			public DateTime End;
		}
	}
}
