using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x02000017 RID: 23
	internal class DeviceBudget : Budget
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000322B File Offset: 0x0000142B
		internal DeviceBudget(BudgetKey owner, IThrottlingPolicy policy) : this(owner, policy, TokenBucketFactory.Default)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000323A File Offset: 0x0000143A
		internal DeviceBudget(BudgetKey owner, IThrottlingPolicy policy, ITokenBucketFactory tokenBucketFactory) : base(owner, policy)
		{
			ArgumentValidator.ThrowIfNull("tokenBucketFactory", tokenBucketFactory);
			this.tokenBucketFactory = tokenBucketFactory;
			this.UpdateCachedPolicyValues(true);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003268 File Offset: 0x00001468
		protected override bool InternalCanExpire
		{
			get
			{
				bool result;
				lock (this.syncRoot)
				{
					result = (this.sentNotificationsBucket.IsFull && this.invalidNotificationsBucket.IsFull);
				}
				return result;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000032C0 File Offset: 0x000014C0
		public override string ToString()
		{
			string result;
			lock (this.syncRoot)
			{
				result = string.Format("{{owner:{0}; policy:{1}; sentNotifications:{2}; invalidNotifications:{3}}}", new object[]
				{
					base.Owner.ToString(),
					base.ThrottlingPolicy.FullPolicy.GetIdentityString(),
					this.sentNotificationsBucket.ToString(),
					this.invalidNotificationsBucket.ToString()
				});
			}
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000334C File Offset: 0x0000154C
		internal static IDeviceBudget Acquire(DeviceBudgetKey budgetKey)
		{
			return new DeviceBudgetWrapper(DeviceBudget.Get(budgetKey));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003359 File Offset: 0x00001559
		internal static DeviceBudget Get(DeviceBudgetKey budgetKey)
		{
			ArgumentValidator.ThrowIfNull("budgetKey", budgetKey);
			return DeviceBudget.DeviceBudgetCache.Singleton.Get(budgetKey);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003371 File Offset: 0x00001571
		internal bool TryApproveSendNotification(out OverBudgetException obe)
		{
			return this.TryTakeToken(this.sentNotificationsBucket, out obe);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003380 File Offset: 0x00001580
		internal bool TryApproveInvalidNotification(out OverBudgetException obe)
		{
			return this.TryTakeToken(this.invalidNotificationsBucket, out obe);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003390 File Offset: 0x00001590
		protected override void UpdateCachedPolicyValues(bool resetBudgetValues)
		{
			if (this.tokenBucketFactory == null)
			{
				return;
			}
			lock (this.syncRoot)
			{
				IThrottlingPolicy fullPolicy = base.ThrottlingPolicy.FullPolicy;
				this.sentNotificationsBucket = this.tokenBucketFactory.Create(resetBudgetValues ? null : this.sentNotificationsBucket, fullPolicy.PushNotificationMaxBurstPerDevice, fullPolicy.PushNotificationRechargeRatePerDevice, fullPolicy.PushNotificationSamplingPeriodPerDevice);
				if (this.invalidNotificationsBucket == null || resetBudgetValues)
				{
					this.invalidNotificationsBucket = this.tokenBucketFactory.Create(1U, 1U, 86400000U);
				}
			}
			base.UpdateCachedPolicyValues(resetBudgetValues);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003448 File Offset: 0x00001648
		protected override bool InternalTryCheckOverBudget(ICollection<CostType> costTypes, out OverBudgetException exception)
		{
			bool result;
			lock (this.syncRoot)
			{
				exception = null;
				if (this.invalidNotificationsBucket.IsEmpty)
				{
					exception = base.CreateOverBudgetException("PushNotificationInvalidNotificationMaxBurst", 1.ToString(), 86400000);
				}
				if (this.sentNotificationsBucket.IsEmpty)
				{
					exception = base.CreateOverBudgetException("PushNotificationMaxBurstPerDevice", base.ThrottlingPolicy.FullPolicy.PushNotificationMaxBurstPerDevice.Value.ToString(), (int)base.ThrottlingPolicy.FullPolicy.PushNotificationSamplingPeriodPerDevice.Value);
				}
				result = (exception != null);
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000350C File Offset: 0x0000170C
		private bool TryTakeToken(ITokenBucket tokenBucket, out OverBudgetException obe)
		{
			bool result;
			lock (this.syncRoot)
			{
				bool flag2 = !base.TryCheckOverBudget(out obe);
				if (flag2)
				{
					tokenBucket.TryTakeToken();
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x04000037 RID: 55
		private const int InvalidNotificationMaxBurst = 1;

		// Token: 0x04000038 RID: 56
		private const int InvalidNotificationRechargeInterval = 86400000;

		// Token: 0x04000039 RID: 57
		private ITokenBucketFactory tokenBucketFactory;

		// Token: 0x0400003A RID: 58
		private ITokenBucket sentNotificationsBucket;

		// Token: 0x0400003B RID: 59
		private ITokenBucket invalidNotificationsBucket;

		// Token: 0x0400003C RID: 60
		private object syncRoot = new object();

		// Token: 0x02000018 RID: 24
		private class DeviceBudgetCache : BudgetCache<DeviceBudget>
		{
			// Token: 0x0600008F RID: 143 RVA: 0x00003560 File Offset: 0x00001760
			protected override DeviceBudget CreateBudget(BudgetKey key, IThrottlingPolicy policy)
			{
				return new DeviceBudget(key, policy, TokenBucketFactory.Default);
			}

			// Token: 0x0400003D RID: 61
			public static readonly DeviceBudget.DeviceBudgetCache Singleton = new DeviceBudget.DeviceBudgetCache();
		}
	}
}
