using System;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000CE RID: 206
	public abstract class PsComplianceRulePredicateBase
	{
		// Token: 0x06000881 RID: 2177
		internal abstract PredicateCondition ToEnginePredicate();

		// Token: 0x06000882 RID: 2178 RVA: 0x000247A0 File Offset: 0x000229A0
		internal static PsComplianceRulePredicateBase FromEnginePredicate(PredicateCondition predicate)
		{
			if (predicate is GreaterThanOrEqualPredicate && predicate.Property.Name.Equals("Item.WhenCreated"))
			{
				return PsContentDateFromPredicate.FromEnginePredicate(predicate as GreaterThanOrEqualPredicate);
			}
			if (predicate is LessThanOrEqualPredicate && predicate.Property.Name.Equals("Item.WhenCreated"))
			{
				return PsContentDateToPredicate.FromEnginePredicate(predicate as LessThanOrEqualPredicate);
			}
			if (predicate is TextQueryPredicate)
			{
				return PsContentMatchQueryPredicate.FromEnginePredicate(predicate as TextQueryPredicate);
			}
			if (predicate is EqualPredicate && predicate.Property.Name.Equals("Item.AccessScope"))
			{
				return PsAccessScopeIsPredicate.FromEnginePredicate(predicate as EqualPredicate);
			}
			if (predicate is ContentContainsSensitiveInformationPredicate)
			{
				return PsContentContainsSensitiveInformationPredicate.FromEnginePredicate(predicate as ContentContainsSensitiveInformationPredicate);
			}
			if (predicate is ContentMetadataContainsPredicate)
			{
				return PsContentPropertyContainsWordsPredicate.FromEnginePredicate(predicate as ContentMetadataContainsPredicate);
			}
			throw new UnexpectedConditionOrActionDetectedException();
		}
	}
}
