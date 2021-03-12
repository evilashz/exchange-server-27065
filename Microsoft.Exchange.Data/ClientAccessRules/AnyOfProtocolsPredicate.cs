using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000102 RID: 258
	internal class AnyOfProtocolsPredicate : PredicateCondition
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x0001CF38 File Offset: 0x0001B138
		public AnyOfProtocolsPredicate(Property property, ShortList<string> valueEntries, RulesCreationContext creationContext) : base(property, valueEntries, creationContext)
		{
			if (!base.Property.IsString && !typeof(ClientAccessProtocol).IsAssignableFrom(base.Property.Type))
			{
				throw new RulesValidationException(RulesTasksStrings.ClientAccessRulesProtocolPropertyRequired(this.Name));
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0001CF88 File Offset: 0x0001B188
		public override string Name
		{
			get
			{
				return "anyOfProtocolsPredicate";
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0001CF8F File Offset: 0x0001B18F
		public override Version MinimumVersion
		{
			get
			{
				return AnyOfProtocolsPredicate.PredicateBaseVersion;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0001CF96 File Offset: 0x0001B196
		public IEnumerable<ClientAccessProtocol> ProtocolList
		{
			get
			{
				return (IEnumerable<ClientAccessProtocol>)base.Value.ParsedValue;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001CFA8 File Offset: 0x0001B1A8
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			ClientAccessProtocol protocol = clientAccessRulesEvaluationContext.Protocol;
			return this.ProtocolList.Contains(protocol);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001D032 File Offset: 0x0001B232
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(entries.Select(delegate(string protocolString)
			{
				int result;
				if (int.TryParse(protocolString, out result))
				{
					return (ClientAccessProtocol)result;
				}
				if (protocolString != null)
				{
					if (protocolString == "EWS")
					{
						return ClientAccessProtocol.ExchangeWebServices;
					}
					if (protocolString == "RPS")
					{
						return ClientAccessProtocol.RemotePowerShell;
					}
					if (protocolString == "OA")
					{
						return ClientAccessProtocol.OutlookAnywhere;
					}
				}
				return (ClientAccessProtocol)Enum.Parse(typeof(ClientAccessProtocol), protocolString);
			}));
		}

		// Token: 0x040005D0 RID: 1488
		public const string Tag = "anyOfProtocolsPredicate";

		// Token: 0x040005D1 RID: 1489
		private static readonly Version PredicateBaseVersion = new Version("15.00.0010.00");
	}
}
