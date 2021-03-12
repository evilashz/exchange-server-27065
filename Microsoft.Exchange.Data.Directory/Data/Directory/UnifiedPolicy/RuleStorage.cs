using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A19 RID: 2585
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class RuleStorage : UnifiedPolicyStorageBase
	{
		// Token: 0x06007778 RID: 30584 RVA: 0x001891E3 File Offset: 0x001873E3
		public RuleStorage()
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17002AAB RID: 10923
		// (get) Token: 0x06007779 RID: 30585 RVA: 0x001891F7 File Offset: 0x001873F7
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RuleStorage.mostDerivedClass;
			}
		}

		// Token: 0x17002AAC RID: 10924
		// (get) Token: 0x0600777A RID: 30586 RVA: 0x001891FE File Offset: 0x001873FE
		internal override ADObjectSchema Schema
		{
			get
			{
				return RuleStorage.schema;
			}
		}

		// Token: 0x17002AAD RID: 10925
		// (get) Token: 0x0600777B RID: 30587 RVA: 0x00189205 File Offset: 0x00187405
		// (set) Token: 0x0600777C RID: 30588 RVA: 0x00189217 File Offset: 0x00187417
		public Guid ParentPolicyId
		{
			get
			{
				return (Guid)this[RuleStorageSchema.ParentPolicyId];
			}
			set
			{
				this[RuleStorageSchema.ParentPolicyId] = value;
			}
		}

		// Token: 0x17002AAE RID: 10926
		// (get) Token: 0x0600777D RID: 30589 RVA: 0x0018922A File Offset: 0x0018742A
		// (set) Token: 0x0600777E RID: 30590 RVA: 0x0018923C File Offset: 0x0018743C
		public Mode Mode
		{
			get
			{
				return (Mode)this[RuleStorageSchema.EnforcementMode];
			}
			set
			{
				this[RuleStorageSchema.EnforcementMode] = value;
			}
		}

		// Token: 0x17002AAF RID: 10927
		// (get) Token: 0x0600777F RID: 30591 RVA: 0x0018924F File Offset: 0x0018744F
		// (set) Token: 0x06007780 RID: 30592 RVA: 0x00189261 File Offset: 0x00187461
		public int Priority
		{
			get
			{
				return (int)this[RuleStorageSchema.Priority];
			}
			set
			{
				this[RuleStorageSchema.Priority] = value;
			}
		}

		// Token: 0x17002AB0 RID: 10928
		// (get) Token: 0x06007781 RID: 30593 RVA: 0x00189274 File Offset: 0x00187474
		// (set) Token: 0x06007782 RID: 30594 RVA: 0x00189286 File Offset: 0x00187486
		public string RuleBlob
		{
			get
			{
				return (string)this[RuleStorageSchema.RuleBlob];
			}
			set
			{
				this[RuleStorageSchema.RuleBlob] = value;
			}
		}

		// Token: 0x17002AB1 RID: 10929
		// (get) Token: 0x06007783 RID: 30595 RVA: 0x00189294 File Offset: 0x00187494
		// (set) Token: 0x06007784 RID: 30596 RVA: 0x001892A6 File Offset: 0x001874A6
		public string Comments
		{
			get
			{
				return (string)this[RuleStorageSchema.Comments];
			}
			set
			{
				this[RuleStorageSchema.Comments] = value;
			}
		}

		// Token: 0x17002AB2 RID: 10930
		// (get) Token: 0x06007785 RID: 30597 RVA: 0x001892B4 File Offset: 0x001874B4
		// (set) Token: 0x06007786 RID: 30598 RVA: 0x001892C6 File Offset: 0x001874C6
		public string Description
		{
			get
			{
				return (string)this[RuleStorageSchema.Description];
			}
			set
			{
				this[RuleStorageSchema.Description] = value;
			}
		}

		// Token: 0x17002AB3 RID: 10931
		// (get) Token: 0x06007787 RID: 30599 RVA: 0x001892D4 File Offset: 0x001874D4
		// (set) Token: 0x06007788 RID: 30600 RVA: 0x001892E6 File Offset: 0x001874E6
		public bool IsEnabled
		{
			get
			{
				return (bool)this[RuleStorageSchema.IsEnabled];
			}
			set
			{
				this[RuleStorageSchema.IsEnabled] = value;
			}
		}

		// Token: 0x17002AB4 RID: 10932
		// (get) Token: 0x06007789 RID: 30601 RVA: 0x001892F9 File Offset: 0x001874F9
		// (set) Token: 0x0600778A RID: 30602 RVA: 0x0018930B File Offset: 0x0018750B
		public string CreatedBy
		{
			get
			{
				return (string)this[RuleStorageSchema.CreatedBy];
			}
			set
			{
				this[RuleStorageSchema.CreatedBy] = value;
			}
		}

		// Token: 0x17002AB5 RID: 10933
		// (get) Token: 0x0600778B RID: 30603 RVA: 0x00189319 File Offset: 0x00187519
		// (set) Token: 0x0600778C RID: 30604 RVA: 0x0018932B File Offset: 0x0018752B
		public string LastModifiedBy
		{
			get
			{
				return (string)this[RuleStorageSchema.LastModifiedBy];
			}
			set
			{
				this[RuleStorageSchema.LastModifiedBy] = value;
			}
		}

		// Token: 0x17002AB6 RID: 10934
		// (get) Token: 0x0600778D RID: 30605 RVA: 0x00189339 File Offset: 0x00187539
		// (set) Token: 0x0600778E RID: 30606 RVA: 0x0018934B File Offset: 0x0018754B
		public PolicyScenario Scenario
		{
			get
			{
				return (PolicyScenario)this[RuleStorageSchema.Scenario];
			}
			set
			{
				this[RuleStorageSchema.Scenario] = value;
			}
		}

		// Token: 0x04004C89 RID: 19593
		private static readonly RuleStorageSchema schema = ObjectSchema.GetInstance<RuleStorageSchema>();

		// Token: 0x04004C8A RID: 19594
		private static string mostDerivedClass = "msExchUnifiedRule";
	}
}
