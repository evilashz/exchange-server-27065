using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000781 RID: 1921
	internal class SentRepresentingChangeCheck : AnalysisRule
	{
		// Token: 0x060043AC RID: 17324 RVA: 0x001158FC File Offset: 0x00113AFC
		internal SentRepresentingChangeCheck()
		{
			base.Name = "SentRepresentingChangeCheck";
			base.AlertLevel = AnalysisRule.RuleAlertLevel.Error;
			base.RequiredProperties = new List<PropertyDefinition>
			{
				CalendarItemBaseSchema.OrganizerEmailAddress,
				ItemSchema.SentRepresentingDisplayName,
				StoreObjectSchema.ItemClass
			};
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x00115950 File Offset: 0x00113B50
		protected override void AnalyzeLog(LinkedListNode<CalendarLogAnalysis> logNode)
		{
			string propertyValue = logNode.Value.GetPropertyValue(StoreObjectSchema.ItemClass);
			if (propertyValue == "IPM.Schedule.Meeting.Request")
			{
				if (this.lastRequest != null)
				{
					string propertyValue2 = this.lastRequest.GetPropertyValue(CalendarItemBaseSchema.OrganizerEmailAddress);
					string propertyValue3 = logNode.Value.GetPropertyValue(CalendarItemBaseSchema.OrganizerEmailAddress);
					if (string.Compare(propertyValue2, propertyValue3, StringComparison.InvariantCultureIgnoreCase) != 0)
					{
						if (string.IsNullOrEmpty(propertyValue3))
						{
							base.Message = Strings.Error_SentRepresentingRemoved;
						}
						else
						{
							string propertyValue4 = this.lastRequest.GetPropertyValue(ItemSchema.SentRepresentingDisplayName);
							string propertyValue5 = logNode.Value.GetPropertyValue(ItemSchema.SentRepresentingDisplayName);
							base.Message = Strings.Error_SentRepresentingChanged(propertyValue4, propertyValue5);
						}
						logNode.Value.AddAlert(this);
					}
				}
				this.lastRequest = logNode.Value;
			}
		}

		// Token: 0x04002A1A RID: 10778
		private CalendarLogAnalysis lastRequest;
	}
}
