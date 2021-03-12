using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200077E RID: 1918
	internal class EndTimeCheck : AnalysisRule
	{
		// Token: 0x060043A5 RID: 17317 RVA: 0x00115688 File Offset: 0x00113888
		internal EndTimeCheck()
		{
			base.Name = "EndDateCheck";
			base.Message = Strings.Error_EndDateCheck;
			base.AlertLevel = AnalysisRule.RuleAlertLevel.Warning;
			base.RequiredProperties = new List<PropertyDefinition>
			{
				CalendarItemInstanceSchema.EndTime
			};
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x001156D8 File Offset: 0x001138D8
		protected override void AnalyzeLog(LinkedListNode<CalendarLogAnalysis> logNode)
		{
			object obj = null;
			if (logNode.Value.InternalProperties.TryGetValue(CalendarItemInstanceSchema.EndTime, out obj))
			{
				ExDateTime t = (ExDateTime)obj;
				if (t < EndTimeCheck.LatestValidDate && t > EndTimeCheck.EarliestValidDate)
				{
					return;
				}
			}
			logNode.Value.AddAlert(this);
		}

		// Token: 0x04002A17 RID: 10775
		private static ExDateTime EarliestValidDate = new ExDateTime(ExTimeZone.CurrentTimeZone, 1995, 1, 1);

		// Token: 0x04002A18 RID: 10776
		private static ExDateTime LatestValidDate = new ExDateTime(ExTimeZone.CurrentTimeZone, 2025, 1, 1);
	}
}
