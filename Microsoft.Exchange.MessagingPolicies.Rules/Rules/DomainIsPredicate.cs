using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000098 RID: 152
	internal class DomainIsPredicate : PredicateCondition
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00016391 File Offset: 0x00014591
		public DomainIsPredicate(Property property, ShortList<string> valueEntries, RulesCreationContext creationContext) : base(property, valueEntries, creationContext)
		{
			if (!base.Property.IsString || !base.Value.IsString)
			{
				throw new RulesValidationException(RulesStrings.StringPropertyOrValueRequired(this.Name));
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x000163C7 File Offset: 0x000145C7
		public override string Name
		{
			get
			{
				return "domainIs";
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x000163CE File Offset: 0x000145CE
		public override Version MinimumVersion
		{
			get
			{
				return DomainIsPredicate.DomainIsBaseVersion;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000163F8 File Offset: 0x000145F8
		public override bool Evaluate(RulesEvaluationContext context)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = context as TransportRulesEvaluationContext;
			if (transportRulesEvaluationContext != null)
			{
				transportRulesEvaluationContext.PredicateName = this.Name;
			}
			object value = base.Property.GetValue(context);
			object predicateValue = base.Value.GetValue(context);
			IEnumerable<string> enumerable = value as IEnumerable<string>;
			if (enumerable != null)
			{
				return enumerable.Any((string s) => DomainIsPredicate.DomainIs(predicateValue, s, this.Name));
			}
			string text = value as string;
			return !string.IsNullOrEmpty(text) && DomainIsPredicate.DomainIs(predicateValue, text, this.Name);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000164B0 File Offset: 0x000146B0
		internal static bool DomainIs(object domainsFromThePredicate, string domainFromTheMessage, string predicateName)
		{
			string text = domainsFromThePredicate as string;
			if (text != null)
			{
				return DomainIsPredicate.IsSubdomainOf(text, domainFromTheMessage);
			}
			IEnumerable<string> enumerable = domainsFromThePredicate as IEnumerable<string>;
			if (enumerable != null)
			{
				return enumerable.Any((string s) => DomainIsPredicate.IsSubdomainOf(s, domainFromTheMessage));
			}
			throw new RulesValidationException(TransportRulesStrings.InvalidPropertyValueType(predicateName));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00016510 File Offset: 0x00014710
		internal static bool IsSubdomainOf(string domain, string subDomain)
		{
			return domain != null && subDomain != null && (subDomain.Equals(domain, StringComparison.InvariantCultureIgnoreCase) || subDomain.EndsWith("." + domain, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x04000282 RID: 642
		internal static readonly Version DomainIsBaseVersion = new Version("15.00.0005.02");
	}
}
