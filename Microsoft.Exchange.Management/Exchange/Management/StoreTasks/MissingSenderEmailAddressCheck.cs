using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200077C RID: 1916
	internal class MissingSenderEmailAddressCheck : AnalysisRule
	{
		// Token: 0x060043A0 RID: 17312 RVA: 0x00115520 File Offset: 0x00113720
		internal MissingSenderEmailAddressCheck()
		{
			base.Name = "MissingSenderEmail";
			base.Message = Strings.Error_MissingSenderEmail;
			base.AlertLevel = AnalysisRule.RuleAlertLevel.Error;
			base.RequiredProperties = new List<PropertyDefinition>
			{
				CalendarItemBaseSchema.SenderEmailAddress
			};
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x00115570 File Offset: 0x00113770
		protected override void AnalyzeLog(LinkedListNode<CalendarLogAnalysis> logNode)
		{
			object obj = null;
			if (logNode.Value.InternalProperties.TryGetValue(CalendarItemBaseSchema.SenderEmailAddress, out obj) && !string.IsNullOrEmpty(obj as string))
			{
				return;
			}
			logNode.Value.AddAlert(this);
		}
	}
}
