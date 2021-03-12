using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000005 RID: 5
	internal class AbsoluteCountConfig : CountConfig, IAbsoluteCountConfig, ICountedConfig, IEquatable<AbsoluteCountConfig>
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002D8F File Offset: 0x00000F8F
		private AbsoluteCountConfig(bool isPromotable, int minActivityThreshold, TimeSpan timeToLive, TimeSpan idleTimeToLive, bool isRemovable, TimeSpan idleCleanupInterval, TimeSpan historyLookbackWindow, Func<DateTime> timeProvider) : base(isPromotable, minActivityThreshold, timeToLive, idleTimeToLive, isRemovable, idleCleanupInterval, timeProvider)
		{
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("historyLookbackWindow", historyLookbackWindow, TimeSpan.Zero, TimeSpan.MaxValue);
			this.historyLookbackWindow = historyLookbackWindow;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public TimeSpan HistoryLookbackWindow
		{
			get
			{
				base.UpdateAccessTime();
				return this.historyLookbackWindow;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public static AbsoluteCountConfig Create(bool promotable, int minActivityThreshold, TimeSpan timeToLive, TimeSpan idleTimeToLive, bool removable, TimeSpan idleCleanupInterval, TimeSpan historyLookbackWindow)
		{
			return AbsoluteCountConfig.Create(promotable, minActivityThreshold, timeToLive, idleTimeToLive, removable, idleCleanupInterval, historyLookbackWindow, () => DateTime.UtcNow);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002E14 File Offset: 0x00001014
		public static AbsoluteCountConfig Create(bool promotable, int minActivityThreshold, TimeSpan timeToLive, TimeSpan idleTimeToLive, bool removable, TimeSpan idleCleanupInterval, TimeSpan historyLookbackWindow, Func<DateTime> timeProvider)
		{
			AbsoluteCountConfig config = new AbsoluteCountConfig(promotable, minActivityThreshold, timeToLive, idleTimeToLive, removable, idleCleanupInterval, historyLookbackWindow, timeProvider);
			return (AbsoluteCountConfig)CountConfig.GetCachedObject(config);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002E40 File Offset: 0x00001040
		public bool Equals(AbsoluteCountConfig config)
		{
			return !object.ReferenceEquals(null, config) && (object.ReferenceEquals(this, config) || (this.historyLookbackWindow.Equals(config.historyLookbackWindow) && base.Equals(config)));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E82 File Offset: 0x00001082
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj is AbsoluteCountConfig && this.Equals(obj as AbsoluteCountConfig)));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002EB0 File Offset: 0x000010B0
		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();
			return hashCode * 397 ^ this.historyLookbackWindow.GetHashCode();
		}

		// Token: 0x0400001D RID: 29
		private readonly TimeSpan historyLookbackWindow;
	}
}
