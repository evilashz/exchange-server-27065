using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200009B RID: 155
	internal class IsExternalPartnerPredicate : PredicateCondition
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x00016750 File Offset: 0x00014950
		public IsExternalPartnerPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001675B File Offset: 0x0001495B
		public override string Name
		{
			get
			{
				return "isExternalPartner";
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00016764 File Offset: 0x00014964
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			BaseTransportRulesEvaluationContext baseTransportRulesEvaluationContext = (BaseTransportRulesEvaluationContext)baseContext;
			baseTransportRulesEvaluationContext.PredicateName = this.Name;
			object value = base.Property.GetValue(baseTransportRulesEvaluationContext);
			return IsExternalPartnerPredicate.IsExternalPartner((string)value);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001679C File Offset: 0x0001499C
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			if (entries.Count != 0)
			{
				throw new RulesValidationException(RulesStrings.ValueIsNotAllowed(this.Name));
			}
			return null;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000167B8 File Offset: 0x000149B8
		internal static bool IsExternalPartner(string recipient)
		{
			if (string.IsNullOrEmpty(recipient))
			{
				return false;
			}
			string domainPart = ((RoutingAddress)recipient).DomainPart;
			return !string.IsNullOrEmpty(domainPart) && SmtpAddress.IsValidDomain(domainPart) && Configuration.TransportConfigObject.IsTLSSendSecureDomain(domainPart);
		}
	}
}
