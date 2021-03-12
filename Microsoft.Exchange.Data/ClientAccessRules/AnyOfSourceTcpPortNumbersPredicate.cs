using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000103 RID: 259
	internal class AnyOfSourceTcpPortNumbersPredicate : PredicateCondition
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0001D070 File Offset: 0x0001B270
		public AnyOfSourceTcpPortNumbersPredicate(Property property, ShortList<string> valueEntries, RulesCreationContext creationContext) : base(property, valueEntries, creationContext)
		{
			if (!base.Property.IsString && !typeof(IntRange).IsAssignableFrom(base.Property.Type))
			{
				throw new RulesValidationException(RulesTasksStrings.ClientAccessRulesPortRangePropertyRequired(this.Name));
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001D0C0 File Offset: 0x0001B2C0
		public override string Name
		{
			get
			{
				return "anyOfSourceTcpPortNumbersPredicate";
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001D0C7 File Offset: 0x0001B2C7
		public override Version MinimumVersion
		{
			get
			{
				return AnyOfSourceTcpPortNumbersPredicate.PredicateBaseVersion;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0001D0CE File Offset: 0x0001B2CE
		public IEnumerable<IntRange> TargetPortRanges
		{
			get
			{
				return (IEnumerable<IntRange>)base.Value.ParsedValue;
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001D0F8 File Offset: 0x0001B2F8
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			int discoveredValue = clientAccessRulesEvaluationContext.RemoteEndpoint.Port;
			return this.TargetPortRanges.Any((IntRange target) => target.Contains(discoveredValue));
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001D13A File Offset: 0x0001B33A
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(entries.Select(new Func<string, IntRange>(IntRange.Parse)));
		}

		// Token: 0x040005D3 RID: 1491
		public const string Tag = "anyOfSourceTcpPortNumbersPredicate";

		// Token: 0x040005D4 RID: 1492
		private static readonly Version PredicateBaseVersion = new Version("15.00.0008.00");
	}
}
