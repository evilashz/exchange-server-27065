using System;
using System.Reflection;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BA6 RID: 2982
	[Serializable]
	public abstract class BifurcationInfoPredicate : TransportRulePredicate
	{
		// Token: 0x0600707A RID: 28794 RVA: 0x001CC85C File Offset: 0x001CAA5C
		internal static bool TryCreatePredicateFromBifInfo(TypeMapping[] mappings, RuleBifurcationInfo bifInfo1, RuleBifurcationInfo bifInfo2, out TransportRulePredicate predicate, out bool twoBifInfoConverted)
		{
			foreach (TypeMapping typeMapping in mappings)
			{
				if (typeMapping.Type.IsSubclassOf(typeof(BifurcationInfoPredicate)))
				{
					MethodInfo method = typeMapping.Type.GetMethod("CreatePredicateFromBifInfo", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(RuleBifurcationInfo)
					}, null);
					TransportRulePredicate transportRulePredicate;
					if (method != null)
					{
						transportRulePredicate = (TransportRulePredicate)method.Invoke(null, new object[]
						{
							bifInfo1
						});
						if (transportRulePredicate != null)
						{
							twoBifInfoConverted = false;
							predicate = transportRulePredicate;
							predicate.Initialize(mappings);
							return true;
						}
					}
					if (bifInfo2 == null)
					{
						goto IL_116;
					}
					method = typeMapping.Type.GetMethod("CreatePredicateFromBifInfo", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(RuleBifurcationInfo),
						typeof(RuleBifurcationInfo)
					}, null);
					if (!(method != null))
					{
						goto IL_116;
					}
					transportRulePredicate = (TransportRulePredicate)method.Invoke(null, new object[]
					{
						bifInfo1,
						bifInfo2
					});
					if (transportRulePredicate == null)
					{
						goto IL_116;
					}
					twoBifInfoConverted = true;
					predicate = transportRulePredicate;
					predicate.Initialize(mappings);
					return true;
				}
				IL_116:;
			}
			predicate = null;
			twoBifInfoConverted = false;
			return false;
		}

		// Token: 0x0600707B RID: 28795
		internal abstract RuleBifurcationInfo ToRuleBifurcationInfo(out RuleBifurcationInfo additionalBifurcationInfo);

		// Token: 0x0600707C RID: 28796 RVA: 0x001CC99A File Offset: 0x001CAB9A
		internal sealed override Condition ToInternalCondition()
		{
			throw new NotSupportedException();
		}
	}
}
