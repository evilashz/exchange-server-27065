using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E89 RID: 3721
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PolicyConfigConverterBase
	{
		// Token: 0x06008184 RID: 33156 RVA: 0x002363D4 File Offset: 0x002345D4
		protected PolicyConfigConverterBase(Type policyConfigType, ConfigurationObjectType configurationObjectType, Type storageType, ADPropertyDefinition policyIdProperty)
		{
			ArgumentValidator.ThrowIfNull("policyConfigType", policyConfigType);
			ArgumentValidator.ThrowIfNull("storageType", storageType);
			ArgumentValidator.ThrowIfNull("policyIdProperty", policyIdProperty);
			this.PolicyConfigType = policyConfigType;
			this.ConfigurationObjectType = configurationObjectType;
			this.StorageType = storageType;
			this.PolicyIdProperty = policyIdProperty;
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x06008185 RID: 33157 RVA: 0x00236426 File Offset: 0x00234626
		// (set) Token: 0x06008186 RID: 33158 RVA: 0x0023642E File Offset: 0x0023462E
		public Type PolicyConfigType { get; private set; }

		// Token: 0x17002262 RID: 8802
		// (get) Token: 0x06008187 RID: 33159 RVA: 0x00236437 File Offset: 0x00234637
		// (set) Token: 0x06008188 RID: 33160 RVA: 0x0023643F File Offset: 0x0023463F
		public Type StorageType { get; private set; }

		// Token: 0x17002263 RID: 8803
		// (get) Token: 0x06008189 RID: 33161 RVA: 0x00236448 File Offset: 0x00234648
		// (set) Token: 0x0600818A RID: 33162 RVA: 0x00236450 File Offset: 0x00234650
		public ConfigurationObjectType ConfigurationObjectType { get; private set; }

		// Token: 0x17002264 RID: 8804
		// (get) Token: 0x0600818B RID: 33163 RVA: 0x00236459 File Offset: 0x00234659
		// (set) Token: 0x0600818C RID: 33164 RVA: 0x00236461 File Offset: 0x00234661
		public ADPropertyDefinition PolicyIdProperty { get; private set; }

		// Token: 0x0600818D RID: 33165
		public abstract Func<QueryFilter, ObjectId, bool, SortBy, IConfigurable[]> GetFindStorageObjectsDelegate(ExPolicyConfigProvider provider);

		// Token: 0x0600818E RID: 33166
		public abstract PolicyConfigBase ConvertFromStorage(ExPolicyConfigProvider provider, UnifiedPolicyStorageBase storageObject);

		// Token: 0x0600818F RID: 33167
		public abstract UnifiedPolicyStorageBase ConvertToStorage(ExPolicyConfigProvider provider, PolicyConfigBase policyConfig);
	}
}
