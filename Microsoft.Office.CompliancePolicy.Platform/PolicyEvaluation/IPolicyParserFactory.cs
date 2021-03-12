using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000BA RID: 186
	public interface IPolicyParserFactory
	{
		// Token: 0x0600049A RID: 1178
		IEnumerable<string> GetSupportedActions();

		// Token: 0x0600049B RID: 1179
		PredicateCondition CreatePredicate(string predicateName, Property property, List<string> valueEntries);

		// Token: 0x0600049C RID: 1180
		Action CreateAction(string actionName, List<Argument> arguments, string externalName);

		// Token: 0x0600049D RID: 1181
		Property CreateProperty(string propertyName, string typeName);
	}
}
