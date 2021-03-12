using System;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000090 RID: 144
	internal class AttachmentContainsWordsPredicate : ContainsPredicate
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00015A9D File Offset: 0x00013C9D
		public AttachmentContainsWordsPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00015AA8 File Offset: 0x00013CA8
		public override string Name
		{
			get
			{
				return "attachmentContainsWords";
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00015AAF File Offset: 0x00013CAF
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00015AB8 File Offset: 0x00013CB8
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			return AttachmentMatcher.AttachmentMatches(base.Value, baseContext, new AttachmentMatcher.TracingDelegate(ExTraceGlobals.TransportRulesEngineTracer.TraceDebug));
		}
	}
}
