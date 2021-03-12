using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Filtering;
using Microsoft.Filtering.Results;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200008F RID: 143
	internal class AttachmentIsPasswordProtectedPredicate : PredicateCondition
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x000159B5 File Offset: 0x00013BB5
		public AttachmentIsPasswordProtectedPredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000159C0 File Offset: 0x00013BC0
		public override string Name
		{
			get
			{
				return "attachmentIsPasswordProtected";
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x000159C7 File Offset: 0x00013BC7
		public override Version MinimumVersion
		{
			get
			{
				return AttachmentIsPasswordProtectedPredicate.AttachmentIsPasswordProtectedBaseVersion;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000159CE File Offset: 0x00013BCE
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			if (entries.Count != 0)
			{
				throw new RulesValidationException(RulesStrings.ValueIsNotAllowed(this.Name));
			}
			return null;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000159F4 File Offset: 0x00013BF4
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			bool result;
			try
			{
				IEnumerable<StreamIdentity> source = from streamIdentity in transportRulesEvaluationContext.Message.GetAttachmentStreamIdentities()
				where RuleAgentResultUtils.IsEncrypted(streamIdentity)
				select streamIdentity;
				result = source.Any<StreamIdentity>();
			}
			catch (Exception ex)
			{
				if (!TransportRulesErrorHandler.IsKnownFipsException(ex) || TransportRulesErrorHandler.IsTimeoutException(ex))
				{
					throw;
				}
				ExTraceGlobals.TransportRulesEngineTracer.TraceDebug<string>(0L, "Exception (FIPS-related) encountered while getting the extracted content information from the attachment.  The attachment will be treated as non-password protected. Error: {0}", ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x04000272 RID: 626
		internal static readonly Version AttachmentIsPasswordProtectedBaseVersion = new Version("15.00.0005.01");
	}
}
