using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200077D RID: 1917
	internal class StartTimeCheck : AnalysisRule
	{
		// Token: 0x060043A2 RID: 17314 RVA: 0x001155B4 File Offset: 0x001137B4
		internal StartTimeCheck()
		{
			base.Name = "StartDateCheck";
			base.Message = Strings.Error_StartDateCheck;
			base.AlertLevel = AnalysisRule.RuleAlertLevel.Warning;
			base.RequiredProperties = new List<PropertyDefinition>
			{
				CalendarItemInstanceSchema.StartTime
			};
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x00115604 File Offset: 0x00113804
		protected override void AnalyzeLog(LinkedListNode<CalendarLogAnalysis> logNode)
		{
			object obj = null;
			if (logNode.Value.InternalProperties.TryGetValue(CalendarItemInstanceSchema.StartTime, out obj))
			{
				ExDateTime t = (ExDateTime)obj;
				if (t < StartTimeCheck.LatestValidDate && t > StartTimeCheck.EarliestValidDate)
				{
					return;
				}
			}
			logNode.Value.AddAlert(this);
		}

		// Token: 0x04002A15 RID: 10773
		private static ExDateTime EarliestValidDate = new ExDateTime(ExTimeZone.CurrentTimeZone, 1995, 1, 1);

		// Token: 0x04002A16 RID: 10774
		private static ExDateTime LatestValidDate = new ExDateTime(ExTimeZone.CurrentTimeZone, 2025, 1, 1);
	}
}
