using System;
using Microsoft.Filtering;
using Microsoft.Filtering.Results;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000006 RID: 6
	internal class AttachmentMatcher
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000036B8 File Offset: 0x000018B8
		internal static bool AttachmentMatches(Value value, RulesEvaluationContext context, AttachmentMatcher.TracingDelegate tracer)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)context;
			MultiMatcher multiMatcher = value.GetValue(transportRulesEvaluationContext) as MultiMatcher;
			if (multiMatcher == null)
			{
				throw new RuleInvalidOperationException(RulesStrings.InvalidValue("MatchAnyIMatch"));
			}
			int num = 0;
			int attachmentTextScanLimit = TransportRulesEvaluationContext.GetAttachmentTextScanLimit(transportRulesEvaluationContext.MailItem);
			try
			{
				foreach (StreamIdentity streamIdentity in transportRulesEvaluationContext.Message.GetSupportedAttachmentStreamIdentities())
				{
					tracer(0L, "Scanning attachment: '{0}'.", new object[]
					{
						streamIdentity.Name
					});
					if (streamIdentity.Content.IsTextAvailable)
					{
						bool flag = multiMatcher.IsMatch(RuleAgentResultUtils.GetSubjectPrependedReader(streamIdentity), "attachmentMatchesRegexPatterns" + '.' + num++, attachmentTextScanLimit, context);
						if (flag)
						{
							return true;
						}
					}
				}
			}
			catch (NotSupportedException ex)
			{
				string text = TransportRulesStrings.AttachmentReadError(string.Format("NotSupportedException. Most likely reason - FIPS not configured properly. Check if TextExtractionHandler is enabled in FIP-FS\\Data\\configuration.xml. Error message {0}", ex.Message));
				tracer(0L, text, new object[0]);
				throw new TransportRulePermanentException(text, ex);
			}
			return false;
		}

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x0600004A RID: 74
		internal delegate void TracingDelegate(long id, string formatString, params object[] args);
	}
}
