using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000009 RID: 9
	public abstract class TextMatchingPredicate : PredicateCondition
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000029D5 File Offset: 0x00000BD5
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000029DD File Offset: 0x00000BDD
		public List<string> Patterns { get; set; }

		// Token: 0x0600003A RID: 58 RVA: 0x000029E8 File Offset: 0x00000BE8
		public TextMatchingPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
			if (base.Property != null && !base.Property.IsString && base.Property.Type != typeof(IContent))
			{
				throw new RulesValidationException(RulesStrings.SearchablePropertyRequired);
			}
			this.Patterns = new List<string>(entries);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A48 File Offset: 0x00000C48
		public override bool Evaluate(RulesEvaluationContext context)
		{
			if (this.property == null)
			{
				throw new InvalidOperationException(RulesStrings.InvalidPropertyType);
			}
			object value = base.Property.GetValue(context);
			MultiMatcher multiMatcher = base.Value.GetValue(context) as MultiMatcher;
			if (multiMatcher == null)
			{
				throw new InvalidOperationException(RulesStrings.InvalidValue("MatchAnyIMatch"));
			}
			bool flag;
			if (base.Property.Type == typeof(string))
			{
				flag = TextMatchingPredicate.IsMatch((string)value, base.Property.Name, multiMatcher, context);
			}
			else if (base.Property.Type == typeof(string[]) || base.Property.Type == typeof(List<string>))
			{
				flag = TextMatchingPredicate.IsMatch((IEnumerable<string>)value, base.Property.Name, multiMatcher, context);
			}
			else
			{
				if (!(base.Property.Type == typeof(IContent)))
				{
					throw new InvalidOperationException(RulesStrings.InvalidPropertyType);
				}
				flag = ((IContent)value).Matches(multiMatcher, context);
			}
			context.Trace("Text match condition evaluated as {0}Match", new object[]
			{
				flag ? string.Empty : "Not "
			});
			return flag;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B95 File Offset: 0x00000D95
		private static bool IsMatch(string text, string propertyName, MultiMatcher matcher, RulesEvaluationContext rulesEvaluationContext)
		{
			return text != null && matcher.IsMatch(text, propertyName, rulesEvaluationContext);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BA8 File Offset: 0x00000DA8
		private static bool IsMatch(IEnumerable<string> value, string propertyName, MultiMatcher matcher, RulesEvaluationContext rulesEvaluationContext)
		{
			int num = 0;
			foreach (string text in value)
			{
				if (TextMatchingPredicate.IsMatch(text, propertyName + num, matcher, rulesEvaluationContext))
				{
					return true;
				}
				num++;
			}
			return false;
		}
	}
}
