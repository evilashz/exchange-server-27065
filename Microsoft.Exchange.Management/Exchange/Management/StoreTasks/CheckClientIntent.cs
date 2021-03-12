using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200077F RID: 1919
	internal class CheckClientIntent : AnalysisRule
	{
		// Token: 0x060043A8 RID: 17320 RVA: 0x0011575C File Offset: 0x0011395C
		internal CheckClientIntent()
		{
			base.Name = "CheckClientIntent";
			base.Message = Strings.Error_CheckClientIntent;
			base.AlertLevel = AnalysisRule.RuleAlertLevel.Warning;
			base.RequiredProperties = new List<PropertyDefinition>
			{
				CalendarItemBaseSchema.ClientInfoString,
				CalendarItemBaseSchema.ClientIntent
			};
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x001157B4 File Offset: 0x001139B4
		protected override void AnalyzeLog(LinkedListNode<CalendarLogAnalysis> logNode)
		{
			object obj = null;
			object obj2 = null;
			if (logNode.Value.InternalProperties.TryGetValue(CalendarItemBaseSchema.ClientInfoString, out obj2))
			{
				if (string.IsNullOrEmpty(obj2 as string))
				{
					base.Message = Strings.Error_CheckClientInfo;
				}
				if (logNode.Value.InternalProperties.TryGetValue(CalendarItemBaseSchema.ClientIntent, out obj))
				{
					return;
				}
			}
			logNode.Value.AddAlert(this);
		}
	}
}
