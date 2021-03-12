using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000009 RID: 9
	internal class CountTracker<TEntityType, TCountType> : ICountTracker<TEntityType, TCountType>, IDisposable where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00003141 File Offset: 0x00001341
		public CountTracker(ICountTrackerConfig config, ICountTrackerDiagnostics<TEntityType, TCountType> perfCounters, Trace tracer) : this(config, perfCounters, tracer, () => DateTime.UtcNow)
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000316C File Offset: 0x0000136C
		public CountTracker(ICountTrackerConfig config, ICountTrackerDiagnostics<TEntityType, TCountType> perfCounters, Trace tracer, Func<DateTime> timeProvider)
		{
			if (!typeof(TEntityType).IsEnum)
			{
				throw new ArgumentException("TEntityType must be an enum");
			}
			if (!typeof(TCountType).IsEnum)
			{
				throw new ArgumentException("TCountType must be an enum");
			}
			if (typeof(TEntityType).IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException("TEntityType cannot be a flags enum");
			}
			if (typeof(TCountType).IsDefined(typeof(FlagsAttribute), false))
			{
				throw new ArgumentException("TCountType cannot be a flags enum");
			}
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNull("perfCounters", perfCounters);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			this.config = config;
			this.perfCounters = perfCounters;
			this.tracer = tracer;
			this.timeProvider = timeProvider;
			this.lastUnpromotedEmptiedTime = this.timeProvider();
			this.timer = new GuardedTimer(new TimerCallback(this.TimerCallback), null, TimeSpan.FromSeconds(5.0));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032DC File Offset: 0x000014DC
		public void AddUsage(ICountedEntity<TEntityType> entity, TCountType measure, ICountedConfig countConfig, long increment)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("countConfig", countConfig);
			CountTracker<TEntityType, TCountType>.EntityValue entityValue;
			if (this.entities.TryGetValue(entity.Name, out entityValue))
			{
				entityValue.AddMeasure(measure, countConfig, increment);
				return;
			}
			if (!countConfig.IsPromotable)
			{
				this.AddEntityAndMeasure(entity, measure, countConfig, increment);
				return;
			}
			if (this.entities.Any((KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue> e) => e.Value.Entity.GroupName.Equals(entity.GroupName)))
			{
				this.AddEntityAndMeasure(entity, measure, countConfig, increment);
				return;
			}
			CountTracker<TEntityType, TCountType>.EntityKey entityKey = new CountTracker<TEntityType, TCountType>.EntityKey(entity.GroupName, measure);
			long num = this.unpromotedMeasures.AddOrUpdate(entityKey, (CountTracker<TEntityType, TCountType>.EntityKey k) => increment, (CountTracker<TEntityType, TCountType>.EntityKey k, long v) => v + increment);
			if (num > (long)countConfig.MinActivityThreshold)
			{
				this.AddEntityAndMeasure(entity, measure, countConfig, increment);
				this.unpromotedMeasures.TryRemove(entityKey, out num);
				this.tracer.TraceDebug<IEntityName<TEntityType>, TCountType, int>((long)this.GetHashCode(), "Promoted entity {0} and measure {1} for crossing threshold {2}", entityKey.Entity, entityKey.Measure, countConfig.MinActivityThreshold);
				this.perfCounters.MeasurePromoted(measure);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003454 File Offset: 0x00001654
		public Task AddUsageAsync(ICountedEntity<TEntityType> entity, TCountType measure, ICountedConfig countConfig, long increment)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("countConfig", countConfig);
			return Task.Factory.StartNew(delegate()
			{
				this.AddUsage(entity, measure, countConfig, increment);
			});
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000034C4 File Offset: 0x000016C4
		public bool TrySetUsage(ICountedEntity<TEntityType> entity, TCountType measure, long value)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			CountTracker<TEntityType, TCountType>.EntityValue entityValue;
			return this.entities.TryGetValue(entity.Name, out entityValue) && entityValue.TrySetMeasure(measure, value);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003524 File Offset: 0x00001724
		public Task<bool> SetUsageAsync(ICountedEntity<TEntityType> entity, TCountType measure, long value)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			return Task<bool>.Factory.StartNew(() => this.TrySetUsage(entity, measure, value));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000357C File Offset: 0x0000177C
		public ICount<TEntityType, TCountType> GetUsage(ICountedEntity<TEntityType> entity, TCountType measure)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			CountTracker<TEntityType, TCountType>.EntityValue entityValue;
			if (this.entities.TryGetValue(entity.Name, out entityValue))
			{
				ICount<TEntityType, TCountType> usage = entityValue.GetUsage(measure);
				if (usage != null)
				{
					return usage;
				}
			}
			return new EmptyCount<TEntityType, TCountType>(entity, measure);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035E0 File Offset: 0x000017E0
		public Task<ICount<TEntityType, TCountType>> GetUsageAsync(ICountedEntity<TEntityType> entity, TCountType measure)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			return Task<ICount<TEntityType, TCountType>>.Factory.StartNew(() => this.GetUsage(entity, measure));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003648 File Offset: 0x00001848
		public IDictionary<TCountType, ICount<TEntityType, TCountType>> GetUsage(ICountedEntity<TEntityType> entity, TCountType[] measures)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("measures", measures);
			CountTracker<TEntityType, TCountType>.EntityValue entityValue;
			if (this.entities.TryGetValue(entity.Name, out entityValue))
			{
				IDictionary<TCountType, ICount<TEntityType, TCountType>> dictionary = new Dictionary<TCountType, ICount<TEntityType, TCountType>>();
				foreach (TCountType tcountType in measures)
				{
					ICount<TEntityType, TCountType> usage = entityValue.GetUsage(tcountType);
					if (usage != null)
					{
						dictionary.Add(tcountType, usage);
					}
				}
				return dictionary;
			}
			return measures.ToDictionary((TCountType countType) => countType, (TCountType countType) => new EmptyCount<TEntityType, TCountType>(entity, countType));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000372C File Offset: 0x0000192C
		public Task<IDictionary<TCountType, ICount<TEntityType, TCountType>>> GetUsageAsync(ICountedEntity<TEntityType> entity, TCountType[] measures)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("measures", measures);
			return Task<IDictionary<TCountType, ICount<TEntityType, TCountType>>>.Factory.StartNew(() => this.GetUsage(entity, measures));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003794 File Offset: 0x00001994
		public IEnumerable<ICount<TEntityType, TCountType>> GetAllUsages(ICountedEntity<TEntityType> entity)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			CountTracker<TEntityType, TCountType>.EntityValue source;
			if (this.entities.TryGetValue(entity.Name, out source))
			{
				return from c in source
				select new CountWrapper<TEntityType, TCountType>(c);
			}
			return null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003804 File Offset: 0x00001A04
		public Task<IEnumerable<ICount<TEntityType, TCountType>>> GetAllUsagesAsync(ICountedEntity<TEntityType> entity)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			return Task<IEnumerable<ICount<TEntityType, TCountType>>>.Factory.StartNew(() => this.GetAllUsages(entity));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000384B File Offset: 0x00001A4B
		public bool TryGetEntityObject(ICountedEntity<TEntityType> entity, out ICountedEntityWrapper<TEntityType, TCountType> wrapper)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			if (this.entities.ContainsKey(entity.Name))
			{
				wrapper = new CountedEntityWrapper<TEntityType, TCountType>(entity, this);
				return true;
			}
			wrapper = null;
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000387C File Offset: 0x00001A7C
		public ICountedConfig GetConfig(ICountedEntity<TEntityType> entity, TCountType measure)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			CountTracker<TEntityType, TCountType>.EntityValue entityValue;
			if (this.entities.TryGetValue(entity.Name, out entityValue))
			{
				ICount<TEntityType, TCountType> usage = entityValue.GetUsage(measure);
				if (usage != null)
				{
					return usage.Config;
				}
			}
			return null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000038C4 File Offset: 0x00001AC4
		public IEnumerable<ICount<TEntityType, TCountType>> Filter(Func<ICount<TEntityType, TCountType>, bool> isMatch)
		{
			ArgumentValidator.ThrowIfNull("isMatch", isMatch);
			List<ICount<TEntityType, TCountType>> list = new List<ICount<TEntityType, TCountType>>();
			foreach (CountTracker<TEntityType, TCountType>.EntityValue entityValue in this.entities.Values)
			{
				list.AddRange(from c in entityValue.Filter(isMatch)
				select new CountWrapper<TEntityType, TCountType>(c));
			}
			return list;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000396C File Offset: 0x00001B6C
		public Task<IEnumerable<ICount<TEntityType, TCountType>>> FilterAsync(Func<ICount<TEntityType, TCountType>, bool> isMatch)
		{
			ArgumentValidator.ThrowIfNull("isMatch", isMatch);
			return Task<IEnumerable<ICount<TEntityType, TCountType>>>.Factory.StartNew(() => this.Filter(isMatch));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000039D8 File Offset: 0x00001BD8
		public IEnumerable<ICountedEntityValue<TEntityType, TCountType>> Filter(Func<ICountedEntityValue<TEntityType, TCountType>, bool> isMatch)
		{
			ArgumentValidator.ThrowIfNull("isMatch", isMatch);
			return from e in this.entities
			where isMatch(e.Value)
			select e.Value;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003A58 File Offset: 0x00001C58
		public Task<IEnumerable<ICountedEntityValue<TEntityType, TCountType>>> FilterAsync(Func<ICountedEntityValue<TEntityType, TCountType>, bool> isMatch)
		{
			ArgumentValidator.ThrowIfNull("isMatch", isMatch);
			return Task<IEnumerable<ICountedEntityValue<TEntityType, TCountType>>>.Factory.StartNew(() => this.Filter(isMatch));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public void GetDiagnosticInfo(string argument, XElement element)
		{
			element.SetAttributeValue("UnpromotedCount", this.unpromotedMeasures.Count);
			element.SetAttributeValue("PromotedCount", this.entities.Count);
			element.SetAttributeValue("LastPromotionTime", this.lastUnpromotedEmptiedTime.ToString("yyyy-MM-ddTHH:mm:ss"));
			element.SetAttributeValue("NeedsCleanup", this.needsEntityCleanup);
			if (argument != null && argument.IndexOf("verbose", StringComparison.InvariantCultureIgnoreCase) != -1)
			{
				XElement xelement = new XElement("Entities");
				IEnumerable<CountTracker<TEntityType, TCountType>.EntityValue> enumerable = (from e in this.entities
				orderby e.Value.LastAccesTime descending
				select e.Value).Take(50);
				foreach (CountTracker<TEntityType, TCountType>.EntityValue entityValue in enumerable)
				{
					if (!entityValue.IsEmpty)
					{
						xelement.Add(entityValue.GetDiagnosticInfo());
					}
				}
				element.Add(xelement);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003C5C File Offset: 0x00001E5C
		public XElement GetDiagnosticInfo(IEntityName<TEntityType> entity)
		{
			IOrderedEnumerable<CountTracker<TEntityType, TCountType>.EntityValue> orderedEnumerable = from p in this.entities
			where p.Key.Equals(entity) || p.Value.Entity.GroupName.Equals(entity)
			select p.Value into e
			orderby e.LastAccesTime descending
			select e;
			XElement xelement = new XElement("Entities");
			xelement.SetAttributeValue("Name", entity.ToString());
			foreach (CountTracker<TEntityType, TCountType>.EntityValue entityValue in orderedEnumerable)
			{
				xelement.Add(entityValue.GetDiagnosticInfo());
			}
			return xelement;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003D44 File Offset: 0x00001F44
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003D54 File Offset: 0x00001F54
		internal void TimedUpdate()
		{
			DateTime dateTime = this.timeProvider();
			if (dateTime - this.lastUnpromotedEmptiedTime > this.config.PromotionInterval)
			{
				this.unpromotedMeasures.Clear();
				this.lastUnpromotedEmptiedTime = dateTime;
			}
			CountConfig.CleanupCachedConfig(dateTime, this.config.IdleCachedConfigCleanupInterval);
			foreach (CountTracker<TEntityType, TCountType>.EntityValue entityValue in this.entities.Values)
			{
				entityValue.TimedUpdate();
			}
			this.RemoveEmptyEntities(dateTime);
			if (this.needsEntityCleanup)
			{
				this.RemoveExcessEntitiesPerGroup(dateTime);
				this.RemoveExcessEntities(dateTime);
				this.needsEntityCleanup = false;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E18 File Offset: 0x00002018
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.timer != null)
			{
				this.timer.Dispose(false);
				this.timer = null;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003E38 File Offset: 0x00002038
		private void AddEntityAndMeasure(ICountedEntity<TEntityType> entity, TCountType measure, ICountedConfig countConfig, long increment)
		{
			CountTracker<TEntityType, TCountType>.EntityValue entityValue = this.AddEntity(entity);
			entityValue.AddMeasure(measure, countConfig, increment);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003E9C File Offset: 0x0000209C
		private CountTracker<TEntityType, TCountType>.EntityValue AddEntity(ICountedEntity<TEntityType> entity)
		{
			CountTracker<TEntityType, TCountType>.EntityValue result = this.entities.AddOrUpdate(entity.Name, delegate(IEntityName<TEntityType> e)
			{
				this.perfCounters.EntityAdded(entity);
				return new CountTracker<TEntityType, TCountType>.EntityValue(entity, this.perfCounters, this.timeProvider);
			}, (IEntityName<TEntityType> e, CountTracker<TEntityType, TCountType>.EntityValue v) => v);
			this.needsEntityCleanup = (this.entities.Count > this.config.MaxEntityCount);
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003F1C File Offset: 0x0000211C
		private void TimerCallback(object state)
		{
			this.TimedUpdate();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003F48 File Offset: 0x00002148
		private void RemoveEmptyEntities(DateTime now)
		{
			foreach (KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue> keyValuePair in from p in this.entities
			where p.Value.IsEmpty
			select p)
			{
				if (!(keyValuePair.Value.LastAccesTime > now))
				{
					CountTracker<TEntityType, TCountType>.EntityValue value;
					this.entities.TryRemove(keyValuePair.Key, out value);
					if (!value.IsEmpty)
					{
						this.entities.AddOrUpdate(keyValuePair.Key, value, (IEntityName<TEntityType> k, CountTracker<TEntityType, TCountType>.EntityValue v) => v.Merge(value));
					}
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000040C0 File Offset: 0x000022C0
		private void RemoveExcessEntitiesPerGroup(DateTime now)
		{
			int num = 0;
			foreach (IGrouping<IEntityName<TEntityType>, KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue>> grouping in from p in this.entities
			group p by p.Value.Entity.GroupName into p
			where p.Count<KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue>>() > this.config.MaxEntitiesPerGroup
			select p into g
			orderby g.Count<KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue>>() descending
			select g)
			{
				int num2 = grouping.Count<KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue>>();
				IEnumerable<KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue>> enumerable = grouping.Where((KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue> p) => p.Value.All((Count<TEntityType, TCountType> c) => c.IsRemovable)).OrderBy((KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue> p) => p.Value.LastAccesTime).ThenBy((KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue> p) => p.Value.Sum((Count<TEntityType, TCountType> c) => c.Total)).Take(num2 - this.config.MaxEntitiesPerGroup + (int)(0.05 * (double)this.config.MaxEntitiesPerGroup));
				foreach (KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue> keyValuePair in enumerable)
				{
					if (!(keyValuePair.Value.LastAccesTime > now))
					{
						CountTracker<TEntityType, TCountType>.EntityValue entityValue;
						this.entities.TryRemove(keyValuePair.Key, out entityValue);
						this.perfCounters.EntityRemoved(entityValue.Entity);
						num++;
					}
				}
				this.tracer.TraceDebug<int, IEntityName<TEntityType>>(0L, "Removing {0} low-use entities for group {1}", num2 - this.config.MaxEntitiesPerGroup, grouping.Key);
			}
			if (num > 0)
			{
				this.tracer.TraceDebug<int>(0L, "Removed {0} entities for exceeded perGroup count", num);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004340 File Offset: 0x00002540
		private void RemoveExcessEntities(DateTime now)
		{
			int num = 0;
			if (this.entities.Count > this.config.MaxEntityCount)
			{
				IEnumerable<KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue>> enumerable = (from p in this.entities
				where p.Value.All((Count<TEntityType, TCountType> c) => c.IsRemovable)
				select p into e
				orderby e.Value.LastAccesTime
				select e).ThenBy((KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue> v) => v.Value.Sum((Count<TEntityType, TCountType> c) => c.Total)).Take(this.entities.Count - this.config.MaxEntityCount + (int)(0.05 * (double)this.config.MaxEntityCount));
				foreach (KeyValuePair<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue> keyValuePair in enumerable)
				{
					if (!(keyValuePair.Value.LastAccesTime > now))
					{
						CountTracker<TEntityType, TCountType>.EntityValue entityValue;
						this.entities.TryRemove(keyValuePair.Key, out entityValue);
						this.perfCounters.EntityRemoved(entityValue.Entity);
						num++;
					}
				}
			}
			this.tracer.TraceDebug<int, int>(0L, "Removed {0} entities for exceeded maxEntity count {1}", num, this.config.MaxEntityCount);
		}

		// Token: 0x04000023 RID: 35
		private readonly ICountTrackerConfig config;

		// Token: 0x04000024 RID: 36
		private readonly ICountTrackerDiagnostics<TEntityType, TCountType> perfCounters;

		// Token: 0x04000025 RID: 37
		private readonly Trace tracer;

		// Token: 0x04000026 RID: 38
		private readonly ConcurrentDictionary<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue> entities = new ConcurrentDictionary<IEntityName<TEntityType>, CountTracker<TEntityType, TCountType>.EntityValue>();

		// Token: 0x04000027 RID: 39
		private readonly ConcurrentDictionary<CountTracker<TEntityType, TCountType>.EntityKey, long> unpromotedMeasures = new ConcurrentDictionary<CountTracker<TEntityType, TCountType>.EntityKey, long>();

		// Token: 0x04000028 RID: 40
		private Func<DateTime> timeProvider;

		// Token: 0x04000029 RID: 41
		private GuardedTimer timer;

		// Token: 0x0400002A RID: 42
		private bool needsEntityCleanup;

		// Token: 0x0400002B RID: 43
		private DateTime lastUnpromotedEmptiedTime;

		// Token: 0x0200000A RID: 10
		private class EntityKey
		{
			// Token: 0x06000096 RID: 150 RVA: 0x000044A0 File Offset: 0x000026A0
			public EntityKey(IEntityName<TEntityType> entity, TCountType measure)
			{
				this.entity = entity;
				this.measure = measure;
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000097 RID: 151 RVA: 0x000044B6 File Offset: 0x000026B6
			public IEntityName<TEntityType> Entity
			{
				get
				{
					return this.entity;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000098 RID: 152 RVA: 0x000044BE File Offset: 0x000026BE
			public TCountType Measure
			{
				get
				{
					return this.measure;
				}
			}

			// Token: 0x06000099 RID: 153 RVA: 0x000044C8 File Offset: 0x000026C8
			public bool Equals(CountTracker<TEntityType, TCountType>.EntityKey key)
			{
				if (object.ReferenceEquals(null, key))
				{
					return false;
				}
				TCountType tcountType = this.measure;
				return tcountType.Equals(key.measure) && this.entity.Equals(key.entity);
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00004514 File Offset: 0x00002714
			public override bool Equals(object obj)
			{
				return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj is CountTracker<TEntityType, TCountType>.EntityKey && this.Equals((CountTracker<TEntityType, TCountType>.EntityKey)obj)));
			}

			// Token: 0x0600009B RID: 155 RVA: 0x00004544 File Offset: 0x00002744
			public override int GetHashCode()
			{
				int hashCode = this.entity.GetHashCode();
				TCountType tcountType = this.measure;
				return hashCode ^ tcountType.GetHashCode();
			}

			// Token: 0x04000043 RID: 67
			private readonly IEntityName<TEntityType> entity;

			// Token: 0x04000044 RID: 68
			private readonly TCountType measure;
		}

		// Token: 0x0200000B RID: 11
		private class EntityValue : IEnumerable<Count<TEntityType, TCountType>>, IEnumerable, ICountedEntityValue<TEntityType, TCountType>
		{
			// Token: 0x0600009C RID: 156 RVA: 0x00004574 File Offset: 0x00002774
			static EntityValue()
			{
				Array values = Enum.GetValues(typeof(TCountType));
				int num = 0;
				foreach (object obj in values)
				{
					TCountType key = (TCountType)((object)obj);
					CountTracker<TEntityType, TCountType>.EntityValue.countTypeToIndexMap.Add(key, num++);
				}
			}

			// Token: 0x0600009D RID: 157 RVA: 0x000045F0 File Offset: 0x000027F0
			public EntityValue(ICountedEntity<TEntityType> entity, ICountTrackerDiagnostics<TEntityType, TCountType> perfCounters, Func<DateTime> timeProvider)
			{
				this.entity = entity;
				this.perfCounters = perfCounters;
				this.timeProvider = timeProvider;
				this.measures = new Count<TEntityType, TCountType>[CountTracker<TEntityType, TCountType>.EntityValue.countTypeToIndexMap.Count];
				this.lastAccessTime = this.timeProvider();
			}

			// Token: 0x0600009E RID: 158 RVA: 0x00004649 File Offset: 0x00002849
			private EntityValue(ICountedEntity<TEntityType> entity, ICountTrackerDiagnostics<TEntityType, TCountType> perfCounters, Count<TEntityType, TCountType>[] measures, Func<DateTime> timeProvider) : this(entity, perfCounters, timeProvider)
			{
				this.measures = measures;
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600009F RID: 159 RVA: 0x0000465C File Offset: 0x0000285C
			public ICountedEntity<TEntityType> Entity
			{
				get
				{
					return this.entity;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004671 File Offset: 0x00002871
			public bool IsEmpty
			{
				get
				{
					return this.measures.All((Count<TEntityType, TCountType> c) => c == null || c.IsEmpty);
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000469B File Offset: 0x0000289B
			public DateTime LastAccesTime
			{
				get
				{
					return this.lastAccessTime;
				}
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x000046A4 File Offset: 0x000028A4
			public void AddMeasure(TCountType measure, ICountedConfig config, long value)
			{
				int num = this.ConvertMeasureToIndex(measure);
				Count<TEntityType, TCountType> count = this.measures[num];
				if (count == null)
				{
					count = this.CreateNewCount(measure, config, num);
				}
				count.AddValue(value);
				this.lastAccessTime = this.timeProvider();
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x000046E8 File Offset: 0x000028E8
			public bool TrySetMeasure(TCountType measure, long value)
			{
				int num = this.ConvertMeasureToIndex(measure);
				Count<TEntityType, TCountType> count = this.measures[num];
				if (count == null)
				{
					return false;
				}
				this.lastAccessTime = this.timeProvider();
				return count.TrySetValue(value);
			}

			// Token: 0x060000A4 RID: 164 RVA: 0x00004724 File Offset: 0x00002924
			public ICount<TEntityType, TCountType> GetUsage(TCountType measure)
			{
				int num = this.ConvertMeasureToIndex(measure);
				this.lastAccessTime = this.timeProvider();
				Count<TEntityType, TCountType> count = this.measures[num];
				if (count != null)
				{
					return new CountWrapper<TEntityType, TCountType>(count);
				}
				return null;
			}

			// Token: 0x060000A5 RID: 165 RVA: 0x0000477C File Offset: 0x0000297C
			public IEnumerable<Count<TEntityType, TCountType>> Filter(Func<ICount<TEntityType, TCountType>, bool> isMatch)
			{
				IEnumerable<Count<TEntityType, TCountType>> enumerable = from measure in this.measures
				where measure != null && isMatch(measure)
				select measure;
				if (enumerable.Any<Count<TEntityType, TCountType>>())
				{
					this.lastAccessTime = this.timeProvider();
				}
				return enumerable;
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x000047C8 File Offset: 0x000029C8
			public bool HasUsage(TCountType measure)
			{
				int num = this.ConvertMeasureToIndex(measure);
				return this.measures[num] != null;
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x000047F8 File Offset: 0x000029F8
			public void TimedUpdate()
			{
				this.Expire();
				foreach (Count<TEntityType, TCountType> count in from m in this
				where m != null && m.NeedsUpdate
				select m)
				{
					count.TimedUpdate();
				}
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x00004871 File Offset: 0x00002A71
			public IEnumerator<Count<TEntityType, TCountType>> GetEnumerator()
			{
				this.lastAccessTime = this.timeProvider();
				return (from count in this.measures
				where count != null
				select count).GetEnumerator();
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x000048B1 File Offset: 0x00002AB1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060000AA RID: 170 RVA: 0x000048BC File Offset: 0x00002ABC
			public CountTracker<TEntityType, TCountType>.EntityValue Merge(CountTracker<TEntityType, TCountType>.EntityValue value)
			{
				if (!this.entity.Equals(value.entity))
				{
					throw new InvalidOperationException("Can't merge EntityValue objects for different entities");
				}
				if (this.IsEmpty)
				{
					return value;
				}
				if (value.IsEmpty)
				{
					return this;
				}
				CountTracker<TEntityType, TCountType>.EntityValue result;
				lock (this.syncObject)
				{
					Count<TEntityType, TCountType>[] array = new Count<TEntityType, TCountType>[CountTracker<TEntityType, TCountType>.EntityValue.countTypeToIndexMap.Count];
					for (int i = 0; i < this.measures.Length; i++)
					{
						if (this.measures[i] != null && value.measures[i] == null)
						{
							array[i] = this.measures[i];
						}
						else if (this.measures[i] == null && value.measures[i] != null)
						{
							array[i] = value.measures[i];
						}
						else if (this.measures[i] != null && value.measures[i] != null)
						{
							array[i] = this.measures[i].Merge(value.measures[i]);
						}
					}
					result = new CountTracker<TEntityType, TCountType>.EntityValue(this.Entity, this.perfCounters, array, this.timeProvider);
				}
				return result;
			}

			// Token: 0x060000AB RID: 171 RVA: 0x000049D8 File Offset: 0x00002BD8
			public XElement GetDiagnosticInfo()
			{
				XElement xelement = new XElement("Entity");
				xelement.SetAttributeValue("Name", this.Entity.ToString());
				foreach (Count<TEntityType, TCountType> count in this)
				{
					XElement diagnosticInfo = count.GetDiagnosticInfo();
					if (diagnosticInfo != null)
					{
						xelement.Add(diagnosticInfo);
					}
				}
				return xelement;
			}

			// Token: 0x060000AC RID: 172 RVA: 0x00004A58 File Offset: 0x00002C58
			private int ConvertMeasureToIndex(TCountType measure)
			{
				int num;
				if (!CountTracker<TEntityType, TCountType>.EntityValue.countTypeToIndexMap.TryGetValue(measure, out num))
				{
					throw new ArgumentException("Unrecognized value of TCountType", "measure");
				}
				if (num < 0 || num > this.measures.Length)
				{
					throw new InvalidOperationException("Returned an invalid index from the map");
				}
				return num;
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00004AA0 File Offset: 0x00002CA0
			private Count<TEntityType, TCountType> CreateNewCount(TCountType measure, ICountedConfig config, int measureIndex)
			{
				Count<TEntityType, TCountType> count;
				lock (this.syncObject)
				{
					if (this.measures[measureIndex] == null)
					{
						count = CountFactory.CreateCount<TEntityType, TCountType>(this.entity, measure, config, this.timeProvider);
						this.measures[measureIndex] = count;
					}
					else
					{
						count = this.measures[measureIndex];
					}
				}
				this.perfCounters.MeasureAdded(measure);
				return count;
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00004B1C File Offset: 0x00002D1C
			private void Expire()
			{
				for (int i = 0; i < this.measures.Length; i++)
				{
					Count<TEntityType, TCountType> count = this.measures[i];
					if (count != null && count.ShouldExpire)
					{
						this.perfCounters.MeasureExpired(count.Measure);
						this.measures[i] = null;
					}
				}
			}

			// Token: 0x04000045 RID: 69
			private static Dictionary<TCountType, int> countTypeToIndexMap = new Dictionary<TCountType, int>();

			// Token: 0x04000046 RID: 70
			private readonly ICountedEntity<TEntityType> entity;

			// Token: 0x04000047 RID: 71
			private readonly ICountTrackerDiagnostics<TEntityType, TCountType> perfCounters;

			// Token: 0x04000048 RID: 72
			private readonly Count<TEntityType, TCountType>[] measures;

			// Token: 0x04000049 RID: 73
			private DateTime lastAccessTime;

			// Token: 0x0400004A RID: 74
			private Func<DateTime> timeProvider;

			// Token: 0x0400004B RID: 75
			private object syncObject = new object();
		}
	}
}
