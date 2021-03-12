using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Filtering;
using Microsoft.Filtering.Results;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000094 RID: 148
	internal class AttachmentProcessingLimitExceededPredicate : PredicateCondition
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x00015D84 File Offset: 0x00013F84
		public AttachmentProcessingLimitExceededPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00015D8F File Offset: 0x00013F8F
		public override string Name
		{
			get
			{
				return "attachmentProcessingLimitExceeded";
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00015D96 File Offset: 0x00013F96
		public override Version MinimumVersion
		{
			get
			{
				return AttachmentProcessingLimitExceededPredicate.AttachmentProcessingLimitExceededBaseVersion;
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00015D9D File Offset: 0x00013F9D
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			if (entries.Count != 0)
			{
				throw new RulesValidationException(RulesStrings.ValueIsNotAllowed(this.Name));
			}
			return null;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			IEnumerable<StreamIdentity> source = from streamIdentity in transportRulesEvaluationContext.Message.GetSupportedAttachmentStreamIdentities()
			where RuleAgentResultUtils.HasExceededProcessingLimit(streamIdentity)
			select streamIdentity;
			return source.Any<StreamIdentity>();
		}

		// Token: 0x04000275 RID: 629
		internal static readonly Version AttachmentProcessingLimitExceededBaseVersion = new Version("15.00.0004.00");
	}
}
