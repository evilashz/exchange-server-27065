using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200009A RID: 154
	internal class IpMatchPredicate : PredicateCondition
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0001660C File Offset: 0x0001480C
		public IpMatchPredicate(Property property, ShortList<string> valueEntries, RulesCreationContext creationContext) : base(property, valueEntries, creationContext)
		{
			if (!base.Property.IsString && !typeof(IPAddress).IsAssignableFrom(base.Property.Type))
			{
				throw new RulesValidationException(TransportRulesStrings.IpMatchPropertyRequired(this.Name));
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00016667 File Offset: 0x00014867
		public override string Name
		{
			get
			{
				return "ipMatch";
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001666E File Offset: 0x0001486E
		public override Version MinimumVersion
		{
			get
			{
				return IpMatchPredicate.IpMatchBaseVersion;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00016678 File Offset: 0x00014878
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			if (entries.Count == 0)
			{
				throw new RulesValidationException(RulesStrings.StringPropertyOrValueRequired(this.Name));
			}
			this.targetIpRanges = entries.Select(new Func<string, IPRange>(IPRange.Parse)).ToList<IPRange>();
			return Value.CreateValue(typeof(string), entries);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000166E4 File Offset: 0x000148E4
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			object value = base.Property.GetValue(transportRulesEvaluationContext);
			if (value == null)
			{
				return false;
			}
			IPAddress discoveredValue = (IPAddress)value;
			return this.targetIpRanges.Any((IPRange target) => target.Contains(discoveredValue));
		}

		// Token: 0x04000285 RID: 645
		internal static readonly Version IpMatchBaseVersion = new Version("15.00.0002.00");

		// Token: 0x04000286 RID: 646
		private List<IPRange> targetIpRanges = new List<IPRange>();
	}
}
