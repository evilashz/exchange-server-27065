using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public class PolicyIdParameter : ADIdParameter
	{
		// Token: 0x06000D50 RID: 3408 RVA: 0x0002FEEA File Offset: 0x0002E0EA
		public PolicyIdParameter()
		{
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002FEF2 File Offset: 0x0002E0F2
		public PolicyIdParameter(Guid identity) : base(identity.ToString())
		{
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002FF07 File Offset: 0x0002E107
		public PolicyIdParameter(string identity) : base(Utils.ConvertObjectIdentityInFfo(identity))
		{
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002FF15 File Offset: 0x0002E115
		public PolicyIdParameter(ADObjectId adObjectId) : this(adObjectId.ToString())
		{
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002FF23 File Offset: 0x0002E123
		public PolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002FF2C File Offset: 0x0002E12C
		public PolicyIdParameter(PsCompliancePolicyBase policy) : this(policy.Identity.ToString())
		{
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002FF3F File Offset: 0x0002E13F
		public static explicit operator string(PolicyIdParameter policyIdParameter)
		{
			return policyIdParameter.ToString();
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002FF47 File Offset: 0x0002E147
		public static PolicyIdParameter Parse(string identity)
		{
			return new PolicyIdParameter(identity);
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0002FF50 File Offset: 0x0002E150
		internal override ADPropertyDefinition[] AdditionalMatchingProperties
		{
			get
			{
				return new ADPropertyDefinition[]
				{
					UnifiedPolicyStorageBaseSchema.MasterIdentity
				};
			}
		}
	}
}
