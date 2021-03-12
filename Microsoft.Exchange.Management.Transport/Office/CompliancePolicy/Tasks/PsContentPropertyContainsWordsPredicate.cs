using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000D5 RID: 213
	public sealed class PsContentPropertyContainsWordsPredicate : PsContainsWordsPredicate
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x000249D5 File Offset: 0x00022BD5
		public PsContentPropertyContainsWordsPredicate(IEnumerable<string> words) : base(words)
		{
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x000249E0 File Offset: 0x00022BE0
		internal override PredicateCondition ToEnginePredicate()
		{
			PredicateCondition result = null;
			try
			{
				result = new ContentMetadataContainsPredicate(base.Words.ToList<string>());
			}
			catch (CompliancePolicyValidationException innerException)
			{
				throw new InvalidContentContentPropertyContainsWordsException(Strings.InvalidContentPropertyContainsWordsPredicate, innerException);
			}
			return result;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00024A20 File Offset: 0x00022C20
		internal static PsContentPropertyContainsWordsPredicate FromEnginePredicate(ContentMetadataContainsPredicate condition)
		{
			return new PsContentPropertyContainsWordsPredicate((IEnumerable<string>)condition.Value.ParsedValue);
		}
	}
}
