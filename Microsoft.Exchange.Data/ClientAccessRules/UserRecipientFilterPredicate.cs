using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Diagnostics.Components.Common;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000104 RID: 260
	internal class UserRecipientFilterPredicate : PredicateCondition
	{
		// Token: 0x06000920 RID: 2336 RVA: 0x0001D164 File Offset: 0x0001B364
		public UserRecipientFilterPredicate(Property property, ShortList<string> valueEntries, RulesCreationContext creationContext) : base(property, valueEntries, creationContext)
		{
			if (!base.Property.IsString && !typeof(string).IsAssignableFrom(base.Property.Type))
			{
				throw new RulesValidationException(RulesTasksStrings.ClientAccessRulesFilterPropertyRequired(this.Name));
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
		public override string Name
		{
			get
			{
				return "UserRecipientFilterPredicate";
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001D1BB File Offset: 0x0001B3BB
		public override Version MinimumVersion
		{
			get
			{
				return UserRecipientFilterPredicate.PredicateBaseVersion;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001D1C2 File Offset: 0x0001B3C2
		public string UserRecipientFilter
		{
			get
			{
				return ((IEnumerable<string>)base.Value.ParsedValue).FirstOrDefault<string>();
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001D1DC File Offset: 0x0001B3DC
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			if (clientAccessRulesEvaluationContext.UserSchema != null && clientAccessRulesEvaluationContext.User != null)
			{
				try
				{
					QueryParser queryParser = new QueryParser(this.UserRecipientFilter, clientAccessRulesEvaluationContext.UserSchema, QueryParser.Capabilities.All, null, new QueryParser.ConvertValueFromStringDelegate(QueryParserUtils.ConvertValueFromString));
					return OpathFilterEvaluator.FilterMatches(queryParser.ParseTree, clientAccessRulesEvaluationContext.User);
				}
				catch (ParsingException ex)
				{
					ExTraceGlobals.ClientAccessRulesTracer.TraceDebug(0L, string.Format("Unexpected exception: {0}", ex.ToString()));
				}
				catch (DataSourceOperationException ex2)
				{
					ExTraceGlobals.ClientAccessRulesTracer.TraceDebug(0L, string.Format("Missing information in property bag to process Monad Filter rule", ex2.ToString()));
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001D298 File Offset: 0x0001B498
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(entries);
		}

		// Token: 0x040005D5 RID: 1493
		public const string Tag = "UserRecipientFilterPredicate";

		// Token: 0x040005D6 RID: 1494
		private static readonly Version PredicateBaseVersion = new Version("15.00.0015.00");
	}
}
