using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000099 RID: 153
	internal class HasSenderOverridePredicate : PredicateCondition
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x00016549 File Offset: 0x00014749
		public HasSenderOverridePredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (!base.Property.IsString)
			{
				throw new RulesValidationException(RulesStrings.StringPropertyOrValueRequired(this.Name));
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00016572 File Offset: 0x00014772
		public override string Name
		{
			get
			{
				return "hasSenderOverride";
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00016579 File Offset: 0x00014779
		public override Version MinimumVersion
		{
			get
			{
				return HasSenderOverridePredicate.ComplianceProgramsBaseVersion;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001658C File Offset: 0x0001478C
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			if (!TransportUtils.IsInternalMail(transportRulesEvaluationContext))
			{
				return false;
			}
			IEnumerable<string> canonicalizedStringProperty = TransportUtils.GetCanonicalizedStringProperty(base.Property, transportRulesEvaluationContext);
			if (canonicalizedStringProperty.Any((string thisValue) => !string.IsNullOrWhiteSpace(thisValue)))
			{
				transportRulesEvaluationContext.SenderOverridden = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000165F2 File Offset: 0x000147F2
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.Empty;
		}

		// Token: 0x04000283 RID: 643
		internal static readonly Version ComplianceProgramsBaseVersion = new Version("15.00.0001.00");
	}
}
