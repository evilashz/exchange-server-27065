using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000100 RID: 256
	internal class AnyOfAuthenticationTypesPredicate : PredicateCondition
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x0001CD40 File Offset: 0x0001AF40
		public AnyOfAuthenticationTypesPredicate(Property property, ShortList<string> valueEntries, RulesCreationContext creationContext) : base(property, valueEntries, creationContext)
		{
			if (!base.Property.IsString && !typeof(ClientAccessAuthenticationMethod).IsAssignableFrom(base.Property.Type))
			{
				throw new RulesValidationException(RulesTasksStrings.ClientAccessRulesAuthenticationTypeRequired(this.Name));
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0001CD90 File Offset: 0x0001AF90
		public override string Name
		{
			get
			{
				return "anyOfAuthenticationTypesPredicate";
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001CD97 File Offset: 0x0001AF97
		public override Version MinimumVersion
		{
			get
			{
				return AnyOfAuthenticationTypesPredicate.PredicateBaseVersion;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001CD9E File Offset: 0x0001AF9E
		public IEnumerable<ClientAccessAuthenticationMethod> AuthenticationTypeList
		{
			get
			{
				return (IEnumerable<ClientAccessAuthenticationMethod>)base.Value.ParsedValue;
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001CDB0 File Offset: 0x0001AFB0
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			ClientAccessAuthenticationMethod authenticationType = clientAccessRulesEvaluationContext.AuthenticationType;
			return this.AuthenticationTypeList.Contains(authenticationType);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001CE06 File Offset: 0x0001B006
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(entries.Select(delegate(string authenticationTypeString)
			{
				int result;
				if (int.TryParse(authenticationTypeString, out result))
				{
					return (ClientAccessAuthenticationMethod)result;
				}
				return (ClientAccessAuthenticationMethod)Enum.Parse(typeof(ClientAccessAuthenticationMethod), authenticationTypeString);
			}));
		}

		// Token: 0x040005CB RID: 1483
		public const string Tag = "anyOfAuthenticationTypesPredicate";

		// Token: 0x040005CC RID: 1484
		private static readonly Version PredicateBaseVersion = new Version("15.00.0012.00");
	}
}
