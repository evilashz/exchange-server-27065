using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000294 RID: 660
	internal class TimeTracker
	{
		// Token: 0x06001839 RID: 6201 RVA: 0x0008E040 File Offset: 0x0008C240
		private static bool ReadEnabled()
		{
			return GlobalSettings.TimeTrackingEnabled;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0008E047 File Offset: 0x0008C247
		internal static void SetEnabledForTest(bool enabled)
		{
			TimeTracker.Enabled = enabled;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0008E04F File Offset: 0x0008C24F
		internal ConcurrentDictionary<int, PerThreadTimeTracker> GetPerThreadTrackersForTest()
		{
			return this.perThreadTimeTrackers;
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0008E058 File Offset: 0x0008C258
		public ITimeEntry Start(TimeId timeId)
		{
			if (!TimeTracker.Enabled)
			{
				return DummyTimeEntry.Singleton;
			}
			return this.GetTimeTracker().Start(timeId);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0008E080 File Offset: 0x0008C280
		public override string ToString()
		{
			if (!TimeTracker.Enabled)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<int, PerThreadTimeTracker> keyValuePair in this.perThreadTimeTrackers)
			{
				stringBuilder.Append(keyValuePair.Value.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0008E0F4 File Offset: 0x0008C2F4
		public void Clear()
		{
			this.perThreadTimeTrackers.Clear();
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0008E108 File Offset: 0x0008C308
		private PerThreadTimeTracker GetTimeTracker()
		{
			return this.perThreadTimeTrackers.GetOrAdd(ThreadIdProvider.ManagedThreadId, (int threadId) => new PerThreadTimeTracker());
		}

		// Token: 0x04000EF4 RID: 3828
		private ConcurrentDictionary<int, PerThreadTimeTracker> perThreadTimeTrackers = new ConcurrentDictionary<int, PerThreadTimeTracker>();

		// Token: 0x04000EF5 RID: 3829
		private static bool Enabled = TimeTracker.ReadEnabled();
	}
}
