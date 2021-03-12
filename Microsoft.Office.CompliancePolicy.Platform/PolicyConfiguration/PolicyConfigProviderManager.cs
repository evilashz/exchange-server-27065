using System;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x020000A1 RID: 161
	public abstract class PolicyConfigProviderManager<T> : IPolicyConfigProviderManager where T : PolicyConfigProviderManager<T>, new()
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000D32C File Offset: 0x0000B52C
		public static T Instance
		{
			get
			{
				return PolicyConfigProviderManager<T>.instance;
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000D333 File Offset: 0x0000B533
		public virtual PolicyConfigProvider CreateForSyncEngine(Guid organizationId, string auxiliaryStore, bool enablePolicyApplication = true, ExecutionLog logProvider = null)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040002A9 RID: 681
		private static readonly T instance = Activator.CreateInstance<T>();
	}
}
