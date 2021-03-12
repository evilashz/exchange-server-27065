using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A1B RID: 2587
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ScopeStorage : UnifiedPolicyStorageBase
	{
		// Token: 0x06007792 RID: 30610 RVA: 0x001897DD File Offset: 0x001879DD
		public ScopeStorage()
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17002AB7 RID: 10935
		// (get) Token: 0x06007793 RID: 30611 RVA: 0x001897F1 File Offset: 0x001879F1
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ScopeStorage.mostDerivedClass;
			}
		}

		// Token: 0x17002AB8 RID: 10936
		// (get) Token: 0x06007794 RID: 30612 RVA: 0x001897F8 File Offset: 0x001879F8
		internal override ADObjectSchema Schema
		{
			get
			{
				return ScopeStorage.schema;
			}
		}

		// Token: 0x17002AB9 RID: 10937
		// (get) Token: 0x06007795 RID: 30613 RVA: 0x001897FF File Offset: 0x001879FF
		// (set) Token: 0x06007796 RID: 30614 RVA: 0x00189811 File Offset: 0x00187A11
		public string Scope
		{
			get
			{
				return (string)this[ScopeStorageSchema.Scope];
			}
			set
			{
				this[ScopeStorageSchema.Scope] = value;
			}
		}

		// Token: 0x17002ABA RID: 10938
		// (get) Token: 0x06007797 RID: 30615 RVA: 0x0018981F File Offset: 0x00187A1F
		// (set) Token: 0x06007798 RID: 30616 RVA: 0x00189831 File Offset: 0x00187A31
		public Mode Mode
		{
			get
			{
				return (Mode)this[ScopeStorageSchema.EnforcementMode];
			}
			set
			{
				this[ScopeStorageSchema.EnforcementMode] = value;
			}
		}

		// Token: 0x04004C9E RID: 19614
		private static readonly ScopeStorageSchema schema = ObjectSchema.GetInstance<ScopeStorageSchema>();

		// Token: 0x04004C9F RID: 19615
		private static string mostDerivedClass = "msExchUnifiedBindingScope";
	}
}
