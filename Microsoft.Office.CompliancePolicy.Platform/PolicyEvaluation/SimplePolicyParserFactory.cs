using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000E2 RID: 226
	public class SimplePolicyParserFactory : IPolicyParserFactory
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x00012D1F File Offset: 0x00010F1F
		public IEnumerable<string> GetSupportedActions()
		{
			return SimplePolicyParserFactory.supportedActions;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00012D26 File Offset: 0x00010F26
		public PredicateCondition CreatePredicate(string predicateName, Property property, List<string> valueEntries)
		{
			return null;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00012D2C File Offset: 0x00010F2C
		public Action CreateAction(string actionName, List<Argument> arguments, string externalName)
		{
			if (actionName != null)
			{
				if (actionName == "Hold")
				{
					return new HoldAction(arguments, externalName);
				}
				if (actionName == "RetentionExpire")
				{
					return new RetentionExpireAction(arguments, externalName);
				}
				if (actionName == "RetentionRecycle")
				{
					return new RetentionRecycleAction(arguments, externalName);
				}
				if (actionName == "BlockAccess")
				{
					return new BlockAccessAction(arguments, externalName);
				}
				if (actionName == "GenerateIncidentReport")
				{
					return new GenerateIncidentReportAction(arguments, externalName);
				}
				if (actionName == "NotifyAuthors")
				{
					return new NotifyAuthorsAction(arguments, externalName);
				}
			}
			return null;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00012DC2 File Offset: 0x00010FC2
		public Property CreateProperty(string propertyName, string typeName)
		{
			return null;
		}

		// Token: 0x0400039D RID: 925
		private static List<string> supportedActions = new List<string>
		{
			"Hold",
			"RetentionExpire",
			"RetentionRecycle",
			"BlockAccess",
			"GenerateIncidentReport",
			"NotifyAuthors"
		};
	}
}
