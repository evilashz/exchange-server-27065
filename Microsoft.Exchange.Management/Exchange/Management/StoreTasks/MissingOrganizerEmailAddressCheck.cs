using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200077B RID: 1915
	internal class MissingOrganizerEmailAddressCheck : AnalysisRule
	{
		// Token: 0x0600439E RID: 17310 RVA: 0x0011548C File Offset: 0x0011368C
		internal MissingOrganizerEmailAddressCheck()
		{
			base.Name = "MissingOrganizerEmail";
			base.Message = Strings.Error_MissingOrganizerEmail;
			base.AlertLevel = AnalysisRule.RuleAlertLevel.Error;
			base.RequiredProperties = new List<PropertyDefinition>
			{
				CalendarItemBaseSchema.OrganizerEmailAddress
			};
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x001154DC File Offset: 0x001136DC
		protected override void AnalyzeLog(LinkedListNode<CalendarLogAnalysis> logNode)
		{
			object obj = null;
			if (logNode.Value.InternalProperties.TryGetValue(CalendarItemBaseSchema.OrganizerEmailAddress, out obj) && !string.IsNullOrEmpty(obj as string))
			{
				return;
			}
			logNode.Value.AddAlert(this);
		}
	}
}
