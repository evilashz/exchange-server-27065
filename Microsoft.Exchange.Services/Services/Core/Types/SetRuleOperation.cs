using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000597 RID: 1431
	[XmlType(TypeName = "SetRuleOperationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class SetRuleOperation : RuleOperation
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x000AB70D File Offset: 0x000A990D
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x000AB715 File Offset: 0x000A9915
		[XmlElement]
		public EwsRule Rule { get; set; }

		// Token: 0x0600285D RID: 10333 RVA: 0x000AB720 File Offset: 0x000A9920
		internal override void Execute(RuleOperationParser ruleOperationParser, Rules serverRules)
		{
			int ruleIndex = this.Rule.Priority + 10 - 1;
			this.Rule.ServerRule = ruleOperationParser.ParseRuleId(this.Rule.RuleId, ruleIndex);
			if (this.Rule.ServerRule == null)
			{
				this.Rule.ServerRule = Microsoft.Exchange.Data.Storage.Rule.Create(serverRules);
			}
			ruleOperationParser.ParseRule(this.Rule);
			if (!ruleOperationParser.HasValidationError)
			{
				ruleOperationParser.InsertParsedRule(this.Rule.ServerRule);
			}
		}
	}
}
