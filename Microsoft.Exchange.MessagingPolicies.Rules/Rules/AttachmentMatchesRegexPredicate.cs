using System;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000093 RID: 147
	internal class AttachmentMatchesRegexPredicate : PredicateCondition
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x00015CA0 File Offset: 0x00013EA0
		public AttachmentMatchesRegexPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00015CAB File Offset: 0x00013EAB
		public override string Name
		{
			get
			{
				return "attachmentMatchesRegexPatterns";
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00015CB2 File Offset: 0x00013EB2
		public override Version MinimumVersion
		{
			get
			{
				return Rule.BaseVersion15;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00015CBC File Offset: 0x00013EBC
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			Value result;
			try
			{
				MultiMatcher multiMatcher = new MultiMatcher();
				foreach (string pattern in entries)
				{
					multiMatcher.Add(creationContext.MatchFactory.CreateRegex(pattern, CaseSensitivityMode.Insensitive, MatchRegexOptions.ExplicitCaptures, MatchesRegexPredicate.RegexMatchTimeout));
				}
				result = Value.CreateValue(multiMatcher, entries);
			}
			catch (ArgumentException innerException)
			{
				throw new RulesValidationException(TextMatchingStrings.RegexInternalParsingError, innerException);
			}
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00015D48 File Offset: 0x00013F48
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			return AttachmentMatcher.AttachmentMatches(base.Value, baseContext, new AttachmentMatcher.TracingDelegate(ExTraceGlobals.TransportRulesEngineTracer.TraceDebug));
		}
	}
}
