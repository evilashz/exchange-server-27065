using System;
using System.Security.Principal;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000287 RID: 647
	internal class EasDeviceBudget : StandardBudget
	{
		// Token: 0x060017C6 RID: 6086 RVA: 0x0008C9A4 File Offset: 0x0008ABA4
		public static IStandardBudget Acquire(EasDeviceBudgetKey budgetKey)
		{
			EasDeviceBudget innerBudget = EasDeviceBudgetCache.Singleton.Get(budgetKey);
			return new EasDeviceBudgetWrapper(innerBudget);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0008C9C4 File Offset: 0x0008ABC4
		public static IStandardBudget Acquire(SecurityIdentifier sid, string deviceId, string deviceType, ADSessionSettings sessionSettings)
		{
			EasDeviceBudgetKey budgetKey = new EasDeviceBudgetKey(sid, deviceId, deviceType, sessionSettings);
			return EasDeviceBudget.Acquire(budgetKey);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0008C9E1 File Offset: 0x0008ABE1
		public void AddInteractiveCall()
		{
			this.interactivecCallsPerMinute.Add(1U);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0008C9EF File Offset: 0x0008ABEF
		public void AddCall()
		{
			this.clientCallsPerMinute.Add(1U);
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0008C9FD File Offset: 0x0008ABFD
		public uint InteractiveCallCount
		{
			get
			{
				return this.interactivecCallsPerMinute.GetValue();
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0008CA0A File Offset: 0x0008AC0A
		public uint CallCount
		{
			get
			{
				return this.clientCallsPerMinute.GetValue();
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0008CA17 File Offset: 0x0008AC17
		public float Percentage
		{
			get
			{
				return this.percentage;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x0008CA1F File Offset: 0x0008AC1F
		public EasDeviceBudgetAllocator Allocator
		{
			get
			{
				return this.allocator;
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0008CA28 File Offset: 0x0008AC28
		internal EasDeviceBudget(BudgetKey key, IThrottlingPolicy policy) : base(key, policy)
		{
			this.allocator = EasDeviceBudgetAllocator.GetAllocator((key as EasDeviceBudgetKey).Sid);
			this.allocator.Add(this);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0008CA90 File Offset: 0x0008AC90
		internal void UpdatePercentage(float percentage)
		{
			if (percentage <= 0f || percentage > 1f)
			{
				throw new ArgumentOutOfRangeException("percentage", percentage, "Percentage must be > 0 and <= 1");
			}
			if (this.percentage != percentage)
			{
				this.percentage = percentage;
				base.SetPolicy(base.ThrottlingPolicy.FullPolicy, false);
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0008CAE5 File Offset: 0x0008ACE5
		protected override void AfterExpire()
		{
			base.AfterExpire();
			this.allocator.Remove(this);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0008CAF9 File Offset: 0x0008ACF9
		internal override void AfterCacheHit()
		{
			base.AfterCacheHit();
			this.allocator.UpdateIfNecessary(false);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0008CB10 File Offset: 0x0008AD10
		protected override SingleComponentThrottlingPolicy GetSingleComponentPolicy(IThrottlingPolicy policy)
		{
			EasDeviceBudgetKey easDeviceBudgetKey = base.Owner as EasDeviceBudgetKey;
			return new EasDeviceThrottlingPolicy(policy, easDeviceBudgetKey.DeviceId, easDeviceBudgetKey.DeviceType, this.Percentage);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0008CB44 File Offset: 0x0008AD44
		internal override bool UpdatePolicy()
		{
			bool result = base.UpdatePolicy();
			this.allocator.UpdateIfNecessary(false);
			return result;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0008CB68 File Offset: 0x0008AD68
		public override string ToString()
		{
			return string.Format("Owner:{0},Allocation:{1}%,LastUpdate:{2},MaxConn:{3},Conn:{4},MaxBurst:{5},Balance:{6},Cutoff:{7},InterCallsFiveMin:{8},CallsFiveMin:{9},LiveTime:{10},ActiveDevices:{11},Policy:{12},ActiveDevicesDetails:{13}", new object[]
			{
				base.Owner,
				this.Percentage * 100f,
				this.allocator.LastUpdate,
				base.ThrottlingPolicy.MaxConcurrency,
				base.Connections,
				base.ThrottlingPolicy.MaxBurst,
				base.CasTokenBucket.GetBalance(),
				base.ThrottlingPolicy.CutoffBalance,
				this.InteractiveCallCount,
				this.CallCount,
				TimeProvider.UtcNow - base.CreationTime,
				this.allocator.Count,
				base.ThrottlingPolicy.FullPolicy.GetShortIdentityString(),
				this.allocator.GetActiveBudgetsString()
			});
		}

		// Token: 0x04000E99 RID: 3737
		private readonly FixedTimeSum interactivecCallsPerMinute = new FixedTimeSum(10000, 30);

		// Token: 0x04000E9A RID: 3738
		private readonly FixedTimeSum clientCallsPerMinute = new FixedTimeSum(10000, 30);

		// Token: 0x04000E9B RID: 3739
		private EasDeviceBudgetAllocator allocator;

		// Token: 0x04000E9C RID: 3740
		private float percentage = 1f;
	}
}
