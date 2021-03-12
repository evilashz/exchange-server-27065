using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000101 RID: 257
	internal class AnyOfClientIPAddressesOrRangesPredicate : PredicateCondition
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x0001CE44 File Offset: 0x0001B044
		public AnyOfClientIPAddressesOrRangesPredicate(Property property, ShortList<string> valueEntries, RulesCreationContext creationContext) : base(property, valueEntries, creationContext)
		{
			if (!base.Property.IsString && !typeof(IPAddress).IsAssignableFrom(base.Property.Type))
			{
				throw new RulesValidationException(RulesTasksStrings.ClientAccessRulesIpPropertyRequired(this.Name));
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001CE94 File Offset: 0x0001B094
		public override string Name
		{
			get
			{
				return "anyOfClientIPAddressesOrRangesPredicate";
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0001CE9B File Offset: 0x0001B09B
		public override Version MinimumVersion
		{
			get
			{
				return AnyOfClientIPAddressesOrRangesPredicate.PredicateBaseVersion;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001CEA2 File Offset: 0x0001B0A2
		public IEnumerable<IPRange> TargetIpRanges
		{
			get
			{
				return (IEnumerable<IPRange>)base.Value.ParsedValue;
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001CECC File Offset: 0x0001B0CC
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			IPAddress discoveredValue = clientAccessRulesEvaluationContext.RemoteEndpoint.Address;
			return this.TargetIpRanges.Any((IPRange target) => target.Contains(discoveredValue));
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001CF0E File Offset: 0x0001B10E
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(entries.Select(new Func<string, IPRange>(IPRange.Parse)));
		}

		// Token: 0x040005CE RID: 1486
		public const string Tag = "anyOfClientIPAddressesOrRangesPredicate";

		// Token: 0x040005CF RID: 1487
		private static readonly Version PredicateBaseVersion = new Version("15.00.0008.00");
	}
}
