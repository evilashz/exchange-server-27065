using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000116 RID: 278
	internal class UsernameMatchesAnyOfPatternsPredicate : PredicateCondition
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x0001E360 File Offset: 0x0001C560
		public UsernameMatchesAnyOfPatternsPredicate(Property property, ShortList<string> valueEntries, RulesCreationContext creationContext) : base(property, valueEntries, creationContext)
		{
			if (!base.Property.IsString && !typeof(string).IsAssignableFrom(base.Property.Type))
			{
				throw new RulesValidationException(RulesTasksStrings.ClientAccessRulesUsernamePatternRequired(this.Name));
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
		public override string Name
		{
			get
			{
				return "usernameMatchesAnyOfPatternsPredicate";
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0001E3B7 File Offset: 0x0001C5B7
		public override Version MinimumVersion
		{
			get
			{
				return UsernameMatchesAnyOfPatternsPredicate.PredicateBaseVersion;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001E3BE File Offset: 0x0001C5BE
		public IEnumerable<Regex> RegexPatterns
		{
			get
			{
				return (IEnumerable<Regex>)base.Value.ParsedValue;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001E3E8 File Offset: 0x0001C5E8
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			string username = clientAccessRulesEvaluationContext.UserName;
			return this.RegexPatterns.Any((Regex target) => target.IsMatch(username));
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001E425 File Offset: 0x0001C625
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(entries.Select(new Func<string, Regex>(ClientAccessRulesUsernamePatternProperty.GetWildcardPatternRegex)));
		}

		// Token: 0x0400060F RID: 1551
		public const string Tag = "usernameMatchesAnyOfPatternsPredicate";

		// Token: 0x04000610 RID: 1552
		private static readonly Version PredicateBaseVersion = new Version("15.00.0011.00");
	}
}
