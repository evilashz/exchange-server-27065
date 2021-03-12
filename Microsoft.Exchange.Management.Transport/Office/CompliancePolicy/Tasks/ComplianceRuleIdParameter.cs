using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	public class ComplianceRuleIdParameter : ADIdParameter
	{
		// Token: 0x06000D59 RID: 3417 RVA: 0x0002FF6D File Offset: 0x0002E16D
		public ComplianceRuleIdParameter()
		{
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002FF75 File Offset: 0x0002E175
		public ComplianceRuleIdParameter(Guid identity) : base(identity.ToString())
		{
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0002FF8A File Offset: 0x0002E18A
		public ComplianceRuleIdParameter(string identity) : base(Utils.ConvertObjectIdentityInFfo(identity))
		{
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002FF98 File Offset: 0x0002E198
		public ComplianceRuleIdParameter(ADObjectId adObjectId) : this(adObjectId.ToString())
		{
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002FFA6 File Offset: 0x0002E1A6
		public ComplianceRuleIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0002FFAF File Offset: 0x0002E1AF
		public ComplianceRuleIdParameter(PsComplianceRuleBase rule) : this(rule.Identity.ToString())
		{
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002FFC2 File Offset: 0x0002E1C2
		public static explicit operator string(ComplianceRuleIdParameter ruleIdParameter)
		{
			return ruleIdParameter.ToString();
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002FFCA File Offset: 0x0002E1CA
		public static ComplianceRuleIdParameter Parse(string identity)
		{
			return new ComplianceRuleIdParameter(identity);
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0002FFD4 File Offset: 0x0002E1D4
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
