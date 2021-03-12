using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000002 RID: 2
	internal abstract class Count<TEntityType, TCountType> : ICount<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D7 File Offset: 0x000002D7
		public Count(ICountedEntity<TEntityType> entity, ICountedConfig config, TCountType measure) : this(entity, config, measure, () => DateTime.UtcNow)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002100 File Offset: 0x00000300
		public Count(ICountedEntity<TEntityType> entity, ICountedConfig config, TCountType measure, Func<DateTime> timeProvider)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNull("measure", measure);
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			this.Entity = entity;
			this.Config = config;
			this.Measure = measure;
			this.timeProvider = timeProvider;
			this.lastAccessTime = this.timeProvider();
			if (this.Config.TimeToLive != TimeSpan.Zero)
			{
				this.expirationTime = this.timeProvider().Add(this.Config.TimeToLive);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000021DA File Offset: 0x000003DA
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000021E2 File Offset: 0x000003E2
		public ICountedEntity<TEntityType> Entity { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021EB File Offset: 0x000003EB
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000021F3 File Offset: 0x000003F3
		public ICountedConfig Config { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021FC File Offset: 0x000003FC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002204 File Offset: 0x00000404
		public TCountType Measure { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000220D File Offset: 0x0000040D
		public bool IsPromoted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002210 File Offset: 0x00000410
		public bool IsEmpty
		{
			get
			{
				return this.updateQueue.IsEmpty && this.Total == 0L && (this.Trendline == null || this.Trendline.IsEmpty);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002242 File Offset: 0x00000442
		public bool IsRemovable
		{
			get
			{
				return this.Config.IsRemovable || this.IsEmpty;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002259 File Offset: 0x00000459
		public ITrendline Trend
		{
			get
			{
				return this.trendline;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13
		public abstract long Total { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000E RID: 14
		public abstract long Average { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002261 File Offset: 0x00000461
		internal bool NeedsUpdate
		{
			get
			{
				return this.updateQueue.Count > 0;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002271 File Offset: 0x00000471
		internal DateTime ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000227C File Offset: 0x0000047C
		internal bool ShouldExpire
		{
			get
			{
				DateTime dateTime = this.TimeProvider();
				return (this.Config.IdleTimeToLive != TimeSpan.Zero && dateTime - this.lastAccessTime > this.Config.IdleTimeToLive) || (this.expirationTime != DateTime.MaxValue && this.expirationTime < dateTime);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022EF File Offset: 0x000004EF
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022F7 File Offset: 0x000004F7
		protected Trendline Trendline
		{
			get
			{
				return this.trendline;
			}
			set
			{
				this.trendline = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002300 File Offset: 0x00000500
		protected Func<DateTime> TimeProvider
		{
			get
			{
				return this.timeProvider;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002308 File Offset: 0x00000508
		public void AddValue(long increment)
		{
			if (increment == 0L)
			{
				return;
			}
			bool flag = false;
			try
			{
				Monitor.TryEnter(this.syncObject, 0, ref flag);
				if (flag)
				{
					this.UpdateAccessTime();
					this.DrainUpdateQueue();
					this.InternalAddValue(increment);
				}
				else
				{
					this.updateQueue.Enqueue(increment);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.syncObject);
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002370 File Offset: 0x00000570
		public bool TrySetValue(long value)
		{
			bool flag = false;
			bool result;
			try
			{
				Monitor.TryEnter(this.syncObject, ref flag);
				if (flag)
				{
					this.UpdateAccessTime();
					result = this.InternalSetValue(value);
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.syncObject);
				}
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023C4 File Offset: 0x000005C4
		public bool TryGetObject(string key, out object value)
		{
			value = null;
			if (this.properties == null)
			{
				return false;
			}
			this.UpdateAccessTime();
			return this.properties.TryGetValue(key, out value);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E8 File Offset: 0x000005E8
		public void SetObject(string key, object value)
		{
			if (this.properties == null)
			{
				lock (this.propertiesSyncObject)
				{
					if (this.properties == null)
					{
						this.properties = new ConcurrentDictionary<string, object>();
					}
				}
			}
			this.UpdateAccessTime();
			this.properties[key] = value;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002450 File Offset: 0x00000650
		public void TimedUpdate()
		{
			if (!this.NeedsUpdate)
			{
				return;
			}
			lock (this.syncObject)
			{
				bool flag;
				if (flag)
				{
					this.DrainUpdateQueue();
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024A0 File Offset: 0x000006A0
		public Count<TEntityType, TCountType> Merge(Count<TEntityType, TCountType> count)
		{
			ArgumentValidator.ThrowIfNull("count", count);
			if (object.ReferenceEquals(this, count))
			{
				return this;
			}
			if (!object.ReferenceEquals(this.Config, count.Config))
			{
				throw new InvalidOperationException("cannot merge two counts with different config");
			}
			if (!this.Entity.Equals(count.Entity))
			{
				throw new InvalidOperationException("cannot merge two counts representing different entities");
			}
			TCountType measure = this.Measure;
			if (!measure.Equals(count.Measure))
			{
				throw new InvalidOperationException("cannot merge two counts representing different measures");
			}
			return this.InternalMerge(count);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002534 File Offset: 0x00000734
		public XElement GetDiagnosticInfo()
		{
			if (this.IsEmpty || this.ShouldExpire)
			{
				return null;
			}
			XElement xelement = new XElement("Count");
			xelement.SetAttributeValue("Name", this.Measure);
			xelement.SetAttributeValue("Value", this.Total);
			xelement.SetAttributeValue("Average", this.Average);
			xelement.SetAttributeValue("LastAccessTime", this.lastAccessTime.ToString("yyyy-MM-ddTHH:mm:ss"));
			xelement.SetAttributeValue("ExpirationTime", this.ExpirationTime.ToString("yyyy-MM-ddTHH:mm:ss"));
			xelement.SetAttributeValue("PropertyCount", (this.properties != null) ? this.properties.Count : 0);
			return xelement;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002623 File Offset: 0x00000823
		public override string ToString()
		{
			return string.Format("Count for Entity {0}, Measure:{1}", this.Entity, this.Measure);
		}

		// Token: 0x0600001D RID: 29
		protected abstract Count<TEntityType, TCountType> InternalMerge(Count<TEntityType, TCountType> count);

		// Token: 0x0600001E RID: 30
		protected abstract void InternalAddValue(long increment);

		// Token: 0x0600001F RID: 31
		protected abstract bool InternalSetValue(long value);

		// Token: 0x06000020 RID: 32 RVA: 0x00002640 File Offset: 0x00000840
		protected void CopyPropertiesTo(Count<TEntityType, TCountType> other)
		{
			if (this.properties != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.properties)
				{
					other.SetObject(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026A4 File Offset: 0x000008A4
		protected void UpdateAccessTime()
		{
			this.lastAccessTime = this.timeProvider();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026B8 File Offset: 0x000008B8
		private void DrainUpdateQueue()
		{
			if (!this.NeedsUpdate)
			{
				return;
			}
			long increment;
			while (this.updateQueue.TryDequeue(out increment))
			{
				this.InternalAddValue(increment);
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly object syncObject = new object();

		// Token: 0x04000002 RID: 2
		private readonly DateTime expirationTime = DateTime.MaxValue;

		// Token: 0x04000003 RID: 3
		private Func<DateTime> timeProvider;

		// Token: 0x04000004 RID: 4
		private ConcurrentQueue<long> updateQueue = new ConcurrentQueue<long>();

		// Token: 0x04000005 RID: 5
		private Trendline trendline;

		// Token: 0x04000006 RID: 6
		private ConcurrentDictionary<string, object> properties;

		// Token: 0x04000007 RID: 7
		private object propertiesSyncObject = new object();

		// Token: 0x04000008 RID: 8
		private DateTime lastAccessTime;
	}
}
