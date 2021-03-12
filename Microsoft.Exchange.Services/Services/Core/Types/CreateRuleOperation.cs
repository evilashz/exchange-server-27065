using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000584 RID: 1412
	[XmlType(TypeName = "CreateRuleOperationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class CreateRuleOperation : RuleOperation
	{
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x000A6FA1 File Offset: 0x000A51A1
		// (set) Token: 0x0600272D RID: 10029 RVA: 0x000A6FA9 File Offset: 0x000A51A9
		[XmlElement]
		public EwsRule Rule { get; set; }

		// Token: 0x0600272E RID: 10030 RVA: 0x000A6FC4 File Offset: 0x000A51C4
		internal override void Execute(RuleOperationParser ruleOperationParser, Rules serverRules)
		{
			ruleOperationParser.ValidateRuleField(() => string.IsNullOrEmpty(this.Rule.RuleId), RuleValidationErrorCode.CreateWithRuleId, CoreResources.RuleErrorCreateWithRuleId, RuleFieldURI.RuleId, this.Rule.RuleId);
			this.Rule.ServerRule = Microsoft.Exchange.Data.Storage.Rule.Create(serverRules);
			ruleOperationParser.ParseRule(this.Rule);
			if (!ruleOperationParser.HasValidationError)
			{
				ruleOperationParser.InsertParsedRule(this.Rule.ServerRule);
			}
		}
	}
}
