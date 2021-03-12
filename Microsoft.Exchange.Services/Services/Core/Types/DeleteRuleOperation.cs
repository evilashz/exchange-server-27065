using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000585 RID: 1413
	[XmlType(TypeName = "DeleteRuleOperationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class DeleteRuleOperation : RuleOperation
	{
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x000A7034 File Offset: 0x000A5234
		// (set) Token: 0x06002732 RID: 10034 RVA: 0x000A703C File Offset: 0x000A523C
		[XmlElement]
		public string RuleId { get; set; }

		// Token: 0x06002733 RID: 10035 RVA: 0x000A7048 File Offset: 0x000A5248
		internal override void Execute(RuleOperationParser ruleOperationParser, Rules serverRules)
		{
			Rule rule = ruleOperationParser.ParseRuleId(this.RuleId, 0);
			if (!ruleOperationParser.HasValidationError)
			{
				rule.MarkDelete();
				ruleOperationParser.AddDeletedRule(rule);
			}
		}
	}
}
