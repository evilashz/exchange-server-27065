using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Filtering;
using Microsoft.Filtering.Results;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000091 RID: 145
	internal class AttachmentIsUnsupportedPredicate : PredicateCondition
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x00015AF4 File Offset: 0x00013CF4
		public AttachmentIsUnsupportedPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00015AFF File Offset: 0x00013CFF
		public override string Name
		{
			get
			{
				return "attachmentIsUnsupported";
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00015B06 File Offset: 0x00013D06
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00015B0D File Offset: 0x00013D0D
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			if (entries.Count != 0)
			{
				throw new RulesValidationException(RulesStrings.ValueIsNotAllowed(this.Name));
			}
			return null;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00015B34 File Offset: 0x00013D34
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			bool result;
			try
			{
				IEnumerable<StreamIdentity> source = from streamIdentity in transportRulesEvaluationContext.Message.GetAttachmentStreamIdentities()
				where RuleAgentResultUtils.IsUnsupported(streamIdentity)
				select streamIdentity;
				result = source.Any<StreamIdentity>();
			}
			catch (FilteringServiceFailureException ex)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceDebug<string>(0L, "Exception (FIPS-related) encountered while getting the extracted content information from the attachment.  The attachment will be treated as unsupported. Error: {0}", ex.ToString());
				result = true;
			}
			return result;
		}
	}
}
