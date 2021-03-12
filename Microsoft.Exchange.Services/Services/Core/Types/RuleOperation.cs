using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000583 RID: 1411
	[XmlInclude(typeof(DeleteRuleOperation))]
	[XmlInclude(typeof(SetRuleOperation))]
	[XmlType(TypeName = "RuleOperationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(CreateRuleOperation))]
	public abstract class RuleOperation
	{
		// Token: 0x0600272A RID: 10026
		internal abstract void Execute(RuleOperationParser ruleOperationParser, Rules serverRules);
	}
}
