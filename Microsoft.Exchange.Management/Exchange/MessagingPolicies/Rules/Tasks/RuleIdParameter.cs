using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B6B RID: 2923
	[Serializable]
	public class RuleIdParameter : ADIdParameter
	{
		// Token: 0x06006CA2 RID: 27810 RVA: 0x001BD01C File Offset: 0x001BB21C
		public RuleIdParameter()
		{
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x001BD024 File Offset: 0x001BB224
		public RuleIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x001BD02D File Offset: 0x001BB22D
		public RuleIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x001BD036 File Offset: 0x001BB236
		public RuleIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x001BD03F File Offset: 0x001BB23F
		public static explicit operator string(RuleIdParameter ruleIdParameter)
		{
			return ruleIdParameter.ToString();
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x001BD047 File Offset: 0x001BB247
		public static RuleIdParameter Parse(string identity)
		{
			return new RuleIdParameter(identity);
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x001BD050 File Offset: 0x001BB250
		public static ADObjectId GetRuleCollectionRdn(string ruleCollection)
		{
			ADObjectId adobjectId = new ADObjectId(new AdName("cn", "Transport Settings"));
			return adobjectId.GetChildId("Rules").GetChildId(ruleCollection);
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x001BD084 File Offset: 0x001BB284
		public static ADObjectId GetRuleCollectionId(IConfigDataProvider session, string ruleCollection)
		{
			IConfigurationSession configurationSession = (IConfigurationSession)session;
			ADObjectId orgContainerId = configurationSession.GetOrgContainerId();
			return orgContainerId.GetChildId("Transport Settings").GetChildId("Rules").GetChildId(ruleCollection);
		}

		// Token: 0x170021F2 RID: 8690
		// (get) Token: 0x06006CAA RID: 27818 RVA: 0x001BD0BC File Offset: 0x001BB2BC
		internal override ADPropertyDefinition[] AdditionalMatchingProperties
		{
			get
			{
				return new ADPropertyDefinition[]
				{
					TransportRuleSchema.ImmutableId
				};
			}
		}
	}
}
