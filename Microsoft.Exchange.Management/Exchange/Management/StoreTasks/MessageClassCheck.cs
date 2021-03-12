using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200077A RID: 1914
	internal class MessageClassCheck : AnalysisRule
	{
		// Token: 0x0600439C RID: 17308 RVA: 0x0011536C File Offset: 0x0011356C
		internal MessageClassCheck()
		{
			base.Name = "MessageClassFilter";
			base.Message = string.Empty;
			base.AlertLevel = AnalysisRule.RuleAlertLevel.Info;
			base.RequiredProperties = new List<PropertyDefinition>
			{
				StoreObjectSchema.ItemClass,
				CalendarItemBaseSchema.CalendarLogTriggerAction
			};
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x001153C0 File Offset: 0x001135C0
		protected override void AnalyzeLog(LinkedListNode<CalendarLogAnalysis> logNode)
		{
			string propertyValue = logNode.Value.GetPropertyValue(StoreObjectSchema.ItemClass);
			string propertyValue2 = logNode.Value.GetPropertyValue(CalendarItemBaseSchema.CalendarLogTriggerAction);
			if (string.IsNullOrEmpty(propertyValue))
			{
				base.Message = Strings.Error_MessageClassFilter;
				base.AlertLevel = AnalysisRule.RuleAlertLevel.Error;
				logNode.Value.AddAlert(this);
				return;
			}
			if ("IPM.Appointment IPM.Schedule.Meeting.Request IPM.Schedule.Meeting.Canceled IPM.Schedule.Meeting.Resp.Pos IPM.Schedule.Meeting.Resp.Neg".IndexOf(propertyValue) != -1)
			{
				string a;
				if ((a = propertyValue2) != null)
				{
					if (a == "Create")
					{
						base.Message = Strings.Info_MessageItemHasBeenCreated;
						goto IL_B1;
					}
					if (a == "MoveToDeletedItems")
					{
						base.Message = Strings.Info_MessageItemHasBeenDeleted;
						goto IL_B1;
					}
				}
				base.Message = Strings.Info_MessageItemHasBeenUpdated;
				IL_B1:
				logNode.Value.AddAlert(this);
			}
		}

		// Token: 0x04002A14 RID: 10772
		private const string ValidMessageClasses = "IPM.Appointment IPM.Schedule.Meeting.Request IPM.Schedule.Meeting.Canceled IPM.Schedule.Meeting.Resp.Pos IPM.Schedule.Meeting.Resp.Neg";
	}
}
