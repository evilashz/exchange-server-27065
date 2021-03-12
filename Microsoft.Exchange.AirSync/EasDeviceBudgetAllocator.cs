using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000288 RID: 648
	internal class EasDeviceBudgetAllocator
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x0008CC87 File Offset: 0x0008AE87
		public static EasDeviceBudgetAllocator GetAllocator(SecurityIdentifier userSid)
		{
			return EasDeviceBudgetAllocator.allocators.GetOrAdd(userSid, (SecurityIdentifier sid) => new EasDeviceBudgetAllocator(sid));
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0008CCB1 File Offset: 0x0008AEB1
		public static void Clear()
		{
			EasDeviceBudgetAllocator.allocators.Clear();
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0008CCC0 File Offset: 0x0008AEC0
		private EasDeviceBudgetAllocator(SecurityIdentifier userSid)
		{
			this.UserSid = userSid;
			this.activeBudgets = new Dictionary<EasDeviceBudgetKey, EasDeviceBudget>();
			this.lastUpdate = TimeProvider.UtcNow.Add(-EasDeviceBudgetAllocator.UpdateInterval);
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0008CD0D File Offset: 0x0008AF0D
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x0008CD15 File Offset: 0x0008AF15
		public SecurityIdentifier UserSid { get; private set; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0008CD1E File Offset: 0x0008AF1E
		public DateTime LastUpdate
		{
			get
			{
				return this.lastUpdate;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x0008CD28 File Offset: 0x0008AF28
		public int Count
		{
			get
			{
				int count;
				lock (this.instanceLock)
				{
					this.UpdateIfNecessary(false);
					count = this.activeBudgets.Count;
				}
				return count;
			}
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0008CD78 File Offset: 0x0008AF78
		public void Add(EasDeviceBudget budget)
		{
			EasDeviceBudgetKey easDeviceBudgetKey = budget.Owner as EasDeviceBudgetKey;
			SecurityIdentifier sid = easDeviceBudgetKey.Sid;
			if (!sid.Equals(this.UserSid))
			{
				throw new InvalidOperationException(string.Format("[EasDeviceBudgetAllocator.Add] Attempted to add a budget for user {0} to allocator for user {1}. That is very naughty.", sid, this.UserSid));
			}
			bool flag = false;
			lock (this.instanceLock)
			{
				if (!this.activeBudgets.ContainsKey(easDeviceBudgetKey))
				{
					this.activeBudgets[easDeviceBudgetKey] = budget;
					flag = true;
				}
			}
			if (flag)
			{
				this.UpdateIfNecessary(true);
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0008CE18 File Offset: 0x0008B018
		public string GetActiveBudgetsString()
		{
			if (this.Count > 1)
			{
				lock (this.instanceLock)
				{
					if (this.Count > 1)
					{
						bool flag2 = true;
						StringBuilder stringBuilder = new StringBuilder();
						foreach (KeyValuePair<EasDeviceBudgetKey, EasDeviceBudget> keyValuePair in this.activeBudgets)
						{
							string value = string.Concat(new object[]
							{
								flag2 ? string.Empty : ",",
								keyValuePair.Key,
								"(",
								(keyValuePair.Value.Percentage * 100f).ToString("###"),
								"%)"
							});
							stringBuilder.Append(value);
							flag2 = false;
						}
						return stringBuilder.ToString();
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0008CF34 File Offset: 0x0008B134
		internal void Remove(EasDeviceBudget budget)
		{
			bool flag = false;
			lock (this.instanceLock)
			{
				EasDeviceBudget objB = null;
				EasDeviceBudgetKey key = budget.Owner as EasDeviceBudgetKey;
				if (this.activeBudgets.TryGetValue(key, out objB) && object.ReferenceEquals(budget, objB))
				{
					this.activeBudgets.Remove(key);
					this.UpdateIfNecessary(true);
					flag = true;
				}
				if (flag && this.activeBudgets.Count == 0)
				{
					EasDeviceBudgetAllocator easDeviceBudgetAllocator;
					EasDeviceBudgetAllocator.allocators.TryRemove(this.UserSid, out easDeviceBudgetAllocator);
				}
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0008CFD4 File Offset: 0x0008B1D4
		internal void UpdateIfNecessary(bool forceUpdate)
		{
			if (this.ShouldUpdate || forceUpdate)
			{
				bool flag = false;
				try
				{
					flag = Monitor.TryEnter(this.instanceLock, 0);
					if (flag && (this.ShouldUpdate || forceUpdate))
					{
						this.Update();
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this.instanceLock);
					}
				}
			}
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0008D030 File Offset: 0x0008B230
		private void Update()
		{
			EasDeviceBudget easDeviceBudget = this.primary;
			bool flag = this.activeBudgets.Count == 1;
			foreach (KeyValuePair<EasDeviceBudgetKey, EasDeviceBudget> keyValuePair in this.activeBudgets)
			{
				EasDeviceBudget value = keyValuePair.Value;
				if (flag)
				{
					easDeviceBudget = value;
					break;
				}
				if (value != easDeviceBudget)
				{
					if (easDeviceBudget == null)
					{
						easDeviceBudget = value;
					}
					else if (value.InteractiveCallCount > easDeviceBudget.InteractiveCallCount || (value.InteractiveCallCount == easDeviceBudget.InteractiveCallCount && value.CallCount > easDeviceBudget.CallCount))
					{
						easDeviceBudget = value;
					}
				}
			}
			if (this.primary != easDeviceBudget)
			{
				this.primary = easDeviceBudget;
			}
			int num = this.activeBudgets.Count + 1;
			float num2 = 1f / (float)num;
			foreach (KeyValuePair<EasDeviceBudgetKey, EasDeviceBudget> keyValuePair2 in this.activeBudgets)
			{
				keyValuePair2.Value.UpdatePercentage((this.primary == keyValuePair2.Value) ? (num2 * 2f) : num2);
			}
			this.lastUpdate = TimeProvider.UtcNow;
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x0008D174 File Offset: 0x0008B374
		private bool ShouldUpdate
		{
			get
			{
				return TimeProvider.UtcNow - this.lastUpdate >= EasDeviceBudgetAllocator.UpdateInterval;
			}
		}

		// Token: 0x04000E9D RID: 3741
		private static ConcurrentDictionary<SecurityIdentifier, EasDeviceBudgetAllocator> allocators = new ConcurrentDictionary<SecurityIdentifier, EasDeviceBudgetAllocator>();

		// Token: 0x04000E9E RID: 3742
		internal static readonly TimeSpan UpdateInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000E9F RID: 3743
		private object instanceLock = new object();

		// Token: 0x04000EA0 RID: 3744
		private DateTime lastUpdate;

		// Token: 0x04000EA1 RID: 3745
		private EasDeviceBudget primary;

		// Token: 0x04000EA2 RID: 3746
		private Dictionary<EasDeviceBudgetKey, EasDeviceBudget> activeBudgets;
	}
}
