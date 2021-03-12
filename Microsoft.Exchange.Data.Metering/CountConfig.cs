using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000004 RID: 4
	internal abstract class CountConfig : ICountedConfig
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000028B4 File Offset: 0x00000AB4
		protected CountConfig(bool isPromotable, int minActivityThreshold, TimeSpan timeToLive, TimeSpan idleTimeToLive, bool isRemovable, TimeSpan idleCleanupInterval) : this(isPromotable, minActivityThreshold, timeToLive, idleTimeToLive, isRemovable, idleCleanupInterval, () => DateTime.UtcNow)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028F0 File Offset: 0x00000AF0
		protected CountConfig(bool isPromotable, int minActivityThreshold, TimeSpan timeToLive, TimeSpan idleTimeToLive, bool isRemovable, TimeSpan idleCleanupInterval, Func<DateTime> timeProvider)
		{
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("timeToLive", timeToLive, TimeSpan.Zero, TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("idleTimeToLive", idleTimeToLive, TimeSpan.Zero, TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("idleCleanupInterval", idleCleanupInterval, TimeSpan.Zero, TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			this.isPromotable = isPromotable;
			this.minActivityThreshold = minActivityThreshold;
			this.timeToLive = timeToLive;
			this.idleTimeToLive = idleTimeToLive;
			this.isRemovable = isRemovable;
			this.idleCleanupInterval = idleCleanupInterval;
			this.timeProvider = timeProvider;
			this.UpdateAccessTime();
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000298B File Offset: 0x00000B8B
		public bool IsPromotable
		{
			get
			{
				this.UpdateAccessTime();
				return this.isPromotable;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002999 File Offset: 0x00000B99
		public int MinActivityThreshold
		{
			get
			{
				this.UpdateAccessTime();
				return this.minActivityThreshold;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000029A7 File Offset: 0x00000BA7
		public TimeSpan TimeToLive
		{
			get
			{
				this.UpdateAccessTime();
				return this.timeToLive;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000029B5 File Offset: 0x00000BB5
		public TimeSpan IdleTimeToLive
		{
			get
			{
				this.UpdateAccessTime();
				return this.idleTimeToLive;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000029C3 File Offset: 0x00000BC3
		public bool IsRemovable
		{
			get
			{
				this.UpdateAccessTime();
				return this.isRemovable;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000029D1 File Offset: 0x00000BD1
		public TimeSpan IdleCleanupInterval
		{
			get
			{
				this.UpdateAccessTime();
				return this.idleCleanupInterval;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029E0 File Offset: 0x00000BE0
		public bool Equals(CountConfig config)
		{
			return !object.ReferenceEquals(null, config) && (object.ReferenceEquals(this, config) || (this.isPromotable.Equals(config.isPromotable) && this.minActivityThreshold == config.minActivityThreshold && this.timeToLive.Equals(config.timeToLive) && this.idleTimeToLive.Equals(config.idleTimeToLive) && this.isRemovable.Equals(config.isRemovable) && this.idleCleanupInterval.Equals(config.idleCleanupInterval)));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A80 File Offset: 0x00000C80
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj is CountConfig && this.Equals(obj as CountConfig)));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public override int GetHashCode()
		{
			int num = this.isPromotable.GetHashCode();
			num = (num * 397 ^ this.minActivityThreshold);
			num = (num * 397 ^ this.timeToLive.GetHashCode());
			num = (num * 397 ^ this.idleTimeToLive.GetHashCode());
			num = (num * 397 ^ this.isRemovable.GetHashCode());
			return num * 397 ^ this.idleCleanupInterval.GetHashCode();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B84 File Offset: 0x00000D84
		internal static void CleanupCachedConfig(DateTime currentTime, TimeSpan idleCleanupInterval)
		{
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("idleCleanupInterval", idleCleanupInterval, TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue);
			TimeSpan t = new TimeSpan(idleCleanupInterval.Ticks / 10L);
			if (currentTime - CountConfig.lastCleanupTime > t)
			{
				try
				{
					CountConfig.syncObject.EnterWriteLock();
					List<CountConfig> list = (from p in CountConfig.ExistingConfig
					where currentTime - p.Value.lastAccessTime > idleCleanupInterval
					select p.Key).ToList<CountConfig>();
					foreach (CountConfig key in list)
					{
						CountConfig.ExistingConfig.Remove(key);
					}
					CountConfig.lastCleanupTime = currentTime;
				}
				finally
				{
					if (CountConfig.syncObject.IsWriteLockHeld)
					{
						CountConfig.syncObject.ExitWriteLock();
					}
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002CC8 File Offset: 0x00000EC8
		protected static CountConfig GetCachedObject(CountConfig config)
		{
			try
			{
				CountConfig.syncObject.EnterUpgradeableReadLock();
				CountConfig result;
				if (CountConfig.ExistingConfig.TryGetValue(config, out result))
				{
					return result;
				}
				try
				{
					CountConfig.syncObject.EnterWriteLock();
					CountConfig.ExistingConfig.Add(config, config);
				}
				finally
				{
					if (CountConfig.syncObject.IsWriteLockHeld)
					{
						CountConfig.syncObject.ExitWriteLock();
					}
				}
			}
			finally
			{
				if (CountConfig.syncObject.IsUpgradeableReadLockHeld)
				{
					CountConfig.syncObject.ExitUpgradeableReadLock();
				}
			}
			return config;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002D5C File Offset: 0x00000F5C
		protected void UpdateAccessTime()
		{
			this.lastAccessTime = this.timeProvider();
		}

		// Token: 0x04000010 RID: 16
		private static readonly Dictionary<CountConfig, CountConfig> ExistingConfig = new Dictionary<CountConfig, CountConfig>();

		// Token: 0x04000011 RID: 17
		private static readonly ReaderWriterLockSlim syncObject = new ReaderWriterLockSlim();

		// Token: 0x04000012 RID: 18
		private static DateTime lastCleanupTime = DateTime.MinValue;

		// Token: 0x04000013 RID: 19
		private readonly bool isPromotable;

		// Token: 0x04000014 RID: 20
		private readonly int minActivityThreshold;

		// Token: 0x04000015 RID: 21
		private readonly TimeSpan timeToLive;

		// Token: 0x04000016 RID: 22
		private readonly TimeSpan idleTimeToLive;

		// Token: 0x04000017 RID: 23
		private readonly bool isRemovable;

		// Token: 0x04000018 RID: 24
		private readonly TimeSpan idleCleanupInterval;

		// Token: 0x04000019 RID: 25
		private Func<DateTime> timeProvider;

		// Token: 0x0400001A RID: 26
		private DateTime lastAccessTime;
	}
}
