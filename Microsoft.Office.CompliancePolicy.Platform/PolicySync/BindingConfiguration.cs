using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000EB RID: 235
	[DataContract]
	public sealed class BindingConfiguration : PolicyConfigurationBase
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x00014226 File Offset: 0x00012426
		public BindingConfiguration() : base(ConfigurationObjectType.Binding)
		{
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001422F File Offset: 0x0001242F
		public BindingConfiguration(Guid tenantId, Guid objectId) : base(ConfigurationObjectType.Binding, tenantId, objectId)
		{
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001423A File Offset: 0x0001243A
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00014242 File Offset: 0x00012442
		[DataMember]
		public Guid PolicyId { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001424B File Offset: 0x0001244B
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00014253 File Offset: 0x00012453
		[SkipReflectionMapping]
		[DataMember]
		public IncrementalCollection<ScopeConfiguration> AppliedScopes { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001425C File Offset: 0x0001245C
		protected override IDictionary<string, string> PropertyNameMapping
		{
			get
			{
				return BindingConfiguration.propertyNameMapping;
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000142A4 File Offset: 0x000124A4
		public override void MergeInto(PolicyConfigBase originalObject, bool isObjectSync, PolicyConfigProvider policyConfigProvider)
		{
			base.MergeInto(originalObject, isObjectSync, null);
			ArgumentValidator.ThrowIfNull("policyConfigProvider", policyConfigProvider);
			if (this.AppliedScopes == null || !this.AppliedScopes.Changed)
			{
				return;
			}
			PolicyBindingSetConfig policyBindingSetConfig = (PolicyBindingSetConfig)originalObject;
			List<PolicyBindingConfig> list = (policyBindingSetConfig.AppliedScopes == null) ? new List<PolicyBindingConfig>() : new List<PolicyBindingConfig>(policyBindingSetConfig.AppliedScopes);
			policyBindingSetConfig.Identity = base.ObjectId;
			if (isObjectSync)
			{
				using (List<PolicyBindingConfig>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PolicyBindingConfig policyBindingConfig = enumerator.Current;
						policyBindingConfig.MarkAsDeleted();
					}
					goto IL_10B;
				}
			}
			if (this.AppliedScopes.RemovedValues != null)
			{
				using (IEnumerator<ScopeConfiguration> enumerator2 = this.AppliedScopes.RemovedValues.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ScopeConfiguration scope = enumerator2.Current;
						int num = list.FindIndex((PolicyBindingConfig item) => item.Identity == scope.ObjectId);
						if (num >= 0)
						{
							list[num].MarkAsDeleted();
						}
					}
				}
			}
			IL_10B:
			if (this.AppliedScopes.ChangedValues != null)
			{
				using (IEnumerator<ScopeConfiguration> enumerator3 = this.AppliedScopes.ChangedValues.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						ScopeConfiguration scope = enumerator3.Current;
						int num2 = list.FindIndex((PolicyBindingConfig item) => item.Identity == scope.ObjectId);
						if (num2 >= 0)
						{
							try
							{
								list[num2].Scope = BindingMetadata.FromStorage(scope.AppliedScope);
							}
							catch (InvalidOperationException ex)
							{
								throw new SyncAgentPermanentException(ex.Message, ex, true, SyncAgentErrorCode.Generic);
							}
							list[num2].Workload = scope.Workload;
							list[num2].Name = scope.Name;
							list[num2].Version = scope.Version;
							if (scope.Mode != null && scope.Mode.Changed)
							{
								list[num2].Mode = scope.Mode.Value;
							}
							list[num2].MarkAsUpdated();
						}
						else
						{
							PolicyBindingConfig policyBindingConfig2 = policyConfigProvider.NewBlankConfigInstanceWrapper(scope.ObjectId);
							policyBindingConfig2.Workload = scope.Workload;
							policyBindingConfig2.Name = scope.Name;
							policyBindingConfig2.Version = scope.Version;
							policyBindingConfig2.PolicyDefinitionConfigId = this.PolicyId;
							try
							{
								policyBindingConfig2.Scope = BindingMetadata.FromStorage(scope.AppliedScope);
							}
							catch (InvalidOperationException ex2)
							{
								throw new SyncAgentPermanentException(ex2.Message, ex2, true, SyncAgentErrorCode.Generic);
							}
							if (scope.Mode != null && scope.Mode.Changed)
							{
								policyBindingConfig2.Mode = scope.Mode.Value;
							}
							list.Add(policyBindingConfig2);
						}
					}
				}
			}
			policyBindingSetConfig.AppliedScopes = list;
		}

		// Token: 0x040003C3 RID: 963
		private static IDictionary<string, string> propertyNameMapping = new Dictionary<string, string>
		{
			{
				"PolicyId",
				PolicyBindingSetConfigSchema.PolicyDefinitionConfigId
			}
		}.Merge(PolicyConfigurationBase.BasePropertyNameMapping);
	}
}
