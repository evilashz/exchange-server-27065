using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A17 RID: 2583
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class PolicyStorage : UnifiedPolicyStorageBase
	{
		// Token: 0x06007761 RID: 30561 RVA: 0x00188E9B File Offset: 0x0018709B
		public PolicyStorage()
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17002AA0 RID: 10912
		// (get) Token: 0x06007762 RID: 30562 RVA: 0x00188EAF File Offset: 0x001870AF
		internal override string MostDerivedObjectClass
		{
			get
			{
				return PolicyStorage.mostDerivedClass;
			}
		}

		// Token: 0x17002AA1 RID: 10913
		// (get) Token: 0x06007763 RID: 30563 RVA: 0x00188EB6 File Offset: 0x001870B6
		internal override ADObjectSchema Schema
		{
			get
			{
				return PolicyStorage.schema;
			}
		}

		// Token: 0x17002AA2 RID: 10914
		// (get) Token: 0x06007764 RID: 30564 RVA: 0x00188EBD File Offset: 0x001870BD
		internal override ADObjectId ParentPath
		{
			get
			{
				return PolicyStorage.PoliciesContainer;
			}
		}

		// Token: 0x17002AA3 RID: 10915
		// (get) Token: 0x06007765 RID: 30565 RVA: 0x00188EC4 File Offset: 0x001870C4
		// (set) Token: 0x06007766 RID: 30566 RVA: 0x00188ED6 File Offset: 0x001870D6
		public Mode Mode
		{
			get
			{
				return (Mode)this[PolicyStorageSchema.EnforcementMode];
			}
			set
			{
				this[PolicyStorageSchema.EnforcementMode] = value;
			}
		}

		// Token: 0x17002AA4 RID: 10916
		// (get) Token: 0x06007767 RID: 30567 RVA: 0x00188EE9 File Offset: 0x001870E9
		// (set) Token: 0x06007768 RID: 30568 RVA: 0x00188EFB File Offset: 0x001870FB
		public PolicyScenario Scenario
		{
			get
			{
				return (PolicyScenario)this[PolicyStorageSchema.Scenario];
			}
			set
			{
				this[PolicyStorageSchema.Scenario] = value;
			}
		}

		// Token: 0x17002AA5 RID: 10917
		// (get) Token: 0x06007769 RID: 30569 RVA: 0x00188F0E File Offset: 0x0018710E
		// (set) Token: 0x0600776A RID: 30570 RVA: 0x00188F20 File Offset: 0x00187120
		public Guid? DefaultRuleId
		{
			get
			{
				return (Guid?)this[PolicyStorageSchema.DefaultRuleId];
			}
			set
			{
				this[PolicyStorageSchema.DefaultRuleId] = value;
			}
		}

		// Token: 0x17002AA6 RID: 10918
		// (get) Token: 0x0600776B RID: 30571 RVA: 0x00188F33 File Offset: 0x00187133
		// (set) Token: 0x0600776C RID: 30572 RVA: 0x00188F45 File Offset: 0x00187145
		public string Comments
		{
			get
			{
				return (string)this[PolicyStorageSchema.Comments];
			}
			set
			{
				this[PolicyStorageSchema.Comments] = value;
			}
		}

		// Token: 0x17002AA7 RID: 10919
		// (get) Token: 0x0600776D RID: 30573 RVA: 0x00188F53 File Offset: 0x00187153
		// (set) Token: 0x0600776E RID: 30574 RVA: 0x00188F65 File Offset: 0x00187165
		public string Description
		{
			get
			{
				return (string)this[PolicyStorageSchema.Description];
			}
			set
			{
				this[PolicyStorageSchema.Description] = value;
			}
		}

		// Token: 0x17002AA8 RID: 10920
		// (get) Token: 0x0600776F RID: 30575 RVA: 0x00188F73 File Offset: 0x00187173
		// (set) Token: 0x06007770 RID: 30576 RVA: 0x00188F85 File Offset: 0x00187185
		public bool IsEnabled
		{
			get
			{
				return (bool)this[PolicyStorageSchema.IsEnabled];
			}
			set
			{
				this[PolicyStorageSchema.IsEnabled] = value;
			}
		}

		// Token: 0x17002AA9 RID: 10921
		// (get) Token: 0x06007771 RID: 30577 RVA: 0x00188F98 File Offset: 0x00187198
		// (set) Token: 0x06007772 RID: 30578 RVA: 0x00188FAA File Offset: 0x001871AA
		public string CreatedBy
		{
			get
			{
				return (string)this[PolicyStorageSchema.CreatedBy];
			}
			set
			{
				this[PolicyStorageSchema.CreatedBy] = value;
			}
		}

		// Token: 0x17002AAA RID: 10922
		// (get) Token: 0x06007773 RID: 30579 RVA: 0x00188FB8 File Offset: 0x001871B8
		// (set) Token: 0x06007774 RID: 30580 RVA: 0x00188FCA File Offset: 0x001871CA
		public string LastModifiedBy
		{
			get
			{
				return (string)this[PolicyStorageSchema.LastModifiedBy];
			}
			set
			{
				this[PolicyStorageSchema.LastModifiedBy] = value;
			}
		}

		// Token: 0x04004C7E RID: 19582
		private static readonly PolicyStorageSchema schema = ObjectSchema.GetInstance<PolicyStorageSchema>();

		// Token: 0x04004C7F RID: 19583
		private static readonly string mostDerivedClass = "msExchUnifiedPolicy";

		// Token: 0x04004C80 RID: 19584
		internal static readonly ADObjectId PoliciesContainer = new ADObjectId("CN=Unified Policies");
	}
}
