using System;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000092 RID: 146
	internal class AttachmentMatchesPatternsPredicate : LegacyMatchesPredicate
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00015BB8 File Offset: 0x00013DB8
		public AttachmentMatchesPatternsPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00015BC3 File Offset: 0x00013DC3
		public override string Name
		{
			get
			{
				return "attachmentMatchesPatterns";
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00015BCA File Offset: 0x00013DCA
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00015BD4 File Offset: 0x00013DD4
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			Value result;
			try
			{
				MultiMatcher multiMatcher = new MultiMatcher();
				foreach (string legacyPattern in entries)
				{
					multiMatcher.Add(creationContext.MatchFactory.CreateRegex(RegexUtils.ConvertLegacyRegexToTpl(legacyPattern), CaseSensitivityMode.Insensitive, MatchRegexOptions.ExplicitCaptures, MatchesRegexPredicate.RegexMatchTimeout));
				}
				result = Value.CreateValue(multiMatcher, entries);
			}
			catch (ArgumentException innerException)
			{
				throw new RulesValidationException(TextMatchingStrings.RegexInternalParsingError, innerException);
			}
			return result;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00015C64 File Offset: 0x00013E64
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			return AttachmentMatcher.AttachmentMatches(base.Value, baseContext, new AttachmentMatcher.TracingDelegate(ExTraceGlobals.TransportRulesEngineTracer.TraceDebug));
		}
	}
}
