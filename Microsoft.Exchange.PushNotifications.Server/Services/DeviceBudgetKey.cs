using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x02000019 RID: 25
	internal class DeviceBudgetKey : LookupBudgetKey
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003582 File Offset: 0x00001782
		public DeviceBudgetKey(string deviceId, OrganizationId tenantId) : this(deviceId, tenantId, ThrottlingPolicyCache.Singleton)
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003594 File Offset: 0x00001794
		internal DeviceBudgetKey(string deviceId, OrganizationId tenantId, ThrottlingPolicyCache throttlingPolicyCache) : base(BudgetType.PushNotificationTenant, false)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("deviceId", deviceId);
			ArgumentValidator.ThrowIfNull("tenantId", tenantId);
			this.tenantId = tenantId;
			this.deviceId = deviceId;
			this.toString = DeviceBudgetKey.ToString(deviceId, tenantId);
			this.getHashCode = this.toString.GetHashCode();
			this.throttlingPolicyCache = throttlingPolicyCache;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000035F4 File Offset: 0x000017F4
		public override bool Equals(object obj)
		{
			DeviceBudgetKey deviceBudgetKey = obj as DeviceBudgetKey;
			return !(deviceBudgetKey == null) && (object.ReferenceEquals(deviceBudgetKey, this) || (deviceBudgetKey.tenantId.Equals(this.tenantId) && deviceBudgetKey.deviceId.Equals(this.deviceId, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003645 File Offset: 0x00001845
		public override string ToString()
		{
			return this.toString;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000364D File Offset: 0x0000184D
		public override int GetHashCode()
		{
			return this.getHashCode;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003668 File Offset: 0x00001868
		internal override IThrottlingPolicy InternalLookup()
		{
			return base.ADRetryLookup(() => this.throttlingPolicyCache.Get(this.tenantId));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000367C File Offset: 0x0000187C
		private static string ToString(string deviceId, OrganizationId tenantId)
		{
			return string.Format("deviceId~{0}~tenantId~{1}", deviceId, tenantId);
		}

		// Token: 0x0400003E RID: 62
		private readonly string deviceId;

		// Token: 0x0400003F RID: 63
		private readonly OrganizationId tenantId;

		// Token: 0x04000040 RID: 64
		private readonly ThrottlingPolicyCache throttlingPolicyCache;

		// Token: 0x04000041 RID: 65
		private readonly string toString;

		// Token: 0x04000042 RID: 66
		private readonly int getHashCode;
	}
}
