using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000780 RID: 1920
	internal class KnowniPhoneIssues : AnalysisRule
	{
		// Token: 0x060043AA RID: 17322 RVA: 0x00115824 File Offset: 0x00113A24
		internal KnowniPhoneIssues()
		{
			base.Name = "KnowniPhoneIssues";
			base.Message = Strings.Error_KnowniPhoneIssues;
			base.AlertLevel = AnalysisRule.RuleAlertLevel.Warning;
			base.RequiredProperties = new List<PropertyDefinition>
			{
				CalendarItemBaseSchema.ClientInfoString,
				CalendarItemBaseSchema.CalendarLogTriggerAction
			};
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x0011589C File Offset: 0x00113A9C
		protected override void AnalyzeLog(LinkedListNode<CalendarLogAnalysis> logNode)
		{
			logNode.Value.GetPropertyValue(CalendarItemBaseSchema.CalendarLogTriggerAction);
			string propertyValue = logNode.Value.GetPropertyValue(CalendarItemBaseSchema.ClientInfoString);
			foreach (string value in this.problomaticAgents)
			{
				if (propertyValue.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) != -1)
				{
					logNode.Value.AddAlert(this);
					return;
				}
			}
		}

		// Token: 0x04002A19 RID: 10777
		private string[] problomaticAgents = new string[]
		{
			"iphone",
			"ipad"
		};
	}
}
