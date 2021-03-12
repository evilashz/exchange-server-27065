using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x02000020 RID: 32
	internal class PushNotificationServiceBudgetKey : LookupBudgetKey
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00003E7F File Offset: 0x0000207F
		internal PushNotificationServiceBudgetKey(ADObjectId policyId) : this(policyId, ThrottlingPolicyCache.Singleton)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003E90 File Offset: 0x00002090
		internal PushNotificationServiceBudgetKey(ADObjectId policyId, ThrottlingPolicyCache throttlingPolicyCache) : base(BudgetType.PushNotificationTenant, false)
		{
			ArgumentValidator.ThrowIfNull("policyId", policyId);
			ArgumentValidator.ThrowIfNull("throttlingPolicyCache", throttlingPolicyCache);
			this.policyId = policyId;
			this.toString = PushNotificationServiceBudgetKey.ToString(policyId);
			this.getHashCode = this.toString.GetHashCode();
			this.throttlingPolicyCache = throttlingPolicyCache;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003EE8 File Offset: 0x000020E8
		public override bool Equals(object obj)
		{
			PushNotificationServiceBudgetKey pushNotificationServiceBudgetKey = obj as PushNotificationServiceBudgetKey;
			return !(pushNotificationServiceBudgetKey == null) && (object.ReferenceEquals(pushNotificationServiceBudgetKey, this) || ADObjectId.Equals(this.policyId, pushNotificationServiceBudgetKey.policyId));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003F23 File Offset: 0x00002123
		public override string ToString()
		{
			return this.toString;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003F2B File Offset: 0x0000212B
		public override int GetHashCode()
		{
			return this.getHashCode;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003F64 File Offset: 0x00002164
		internal static ADObjectId ResolveServiceThrottlingPolicyId()
		{
			IConfigurationSession session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 119, "ResolveServiceThrottlingPolicyId", "f:\\15.00.1497\\sources\\dev\\PushNotifications\\src\\server\\Services\\PushNotificationServiceBudgetKey.cs");
			IConfigurable[] policies = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				policies = session.Find<ThrottlingPolicy>(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "PushNotificationServiceThrottlingPolicy"), null, true, null);
			});
			if (!adoperationResult.Succeeded || policies == null || policies.Length < 1)
			{
				string text = adoperationResult.Exception.ToTraceString();
				PushNotificationsCrimsonEvents.CannotResolvePushNotificationServicePolicy.Log<string, string>("PushNotificationServiceThrottlingPolicy", text);
				ExTraceGlobals.PushNotificationServiceTracer.TraceError(0L, string.Format("Failed to resolve the PushNotification Service policy '{0}' with error: {1}.", "PushNotificationServiceThrottlingPolicy", text));
				return null;
			}
			return (ADObjectId)policies[0].Identity;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004032 File Offset: 0x00002232
		internal override IThrottlingPolicy InternalLookup()
		{
			return base.ADRetryLookup(() => this.throttlingPolicyCache.Get(OrganizationId.ForestWideOrgId, this.policyId));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004046 File Offset: 0x00002246
		private static string ToString(ADObjectId policyId)
		{
			return string.Format("policyName~{0}", policyId.Name);
		}

		// Token: 0x04000054 RID: 84
		private readonly ThrottlingPolicyCache throttlingPolicyCache;

		// Token: 0x04000055 RID: 85
		private readonly ADObjectId policyId;

		// Token: 0x04000056 RID: 86
		private readonly string toString;

		// Token: 0x04000057 RID: 87
		private readonly int getHashCode;
	}
}
