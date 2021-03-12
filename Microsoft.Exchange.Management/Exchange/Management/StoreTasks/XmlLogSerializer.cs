using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000788 RID: 1928
	internal class XmlLogSerializer : LogSerializer
	{
		// Token: 0x060043DE RID: 17374 RVA: 0x00116763 File Offset: 0x00114963
		public XmlLogSerializer(IEnumerable<PropertyDefinition> propertyMask)
		{
			this.propertyMask = propertyMask;
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x00116772 File Offset: 0x00114972
		protected override void SerializeHeader(IEnumerable<CalendarLogAnalysis> logs, IEnumerable<PropertyDefinition> properties, ref StringBuilder sb)
		{
			sb.AppendLine("<?xml version=\"1.0\"?>\r\n<CalendarLogAnalysisItems>");
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x00116784 File Offset: 0x00114984
		protected override void SerializeBody(IEnumerable<CalendarLogAnalysis> logs, IEnumerable<PropertyDefinition> properties, ref StringBuilder sb)
		{
			foreach (CalendarLogAnalysis calendarLogAnalysis in logs)
			{
				if (calendarLogAnalysis.HasAlerts)
				{
					sb.AppendFormat("    <CalendarLogAnalysis logtime=\"{0}\">", calendarLogAnalysis.LocalLogTime);
					sb.AppendLine("        <Properties>");
					foreach (PropertyDefinition propertyDefinition in properties)
					{
						if (!this.propertyMask.Contains(propertyDefinition) && calendarLogAnalysis.InternalProperties.ContainsKey(propertyDefinition))
						{
							sb.AppendFormat("          <{0} Type=\"{1}\">{2}</{0}>{3}", new object[]
							{
								propertyDefinition.Name,
								propertyDefinition.Type,
								calendarLogAnalysis[propertyDefinition],
								Environment.NewLine
							});
						}
					}
					sb.AppendLine("        </Properties>");
					if (calendarLogAnalysis.Alerts.Count<AnalysisRule>() == 0)
					{
						sb.AppendLine("        <Alerts />");
					}
					else
					{
						sb.AppendLine("        <Alerts>");
						foreach (AnalysisRule analysisRule in calendarLogAnalysis.Alerts)
						{
							sb.AppendFormat("          <{0} Severity=\"{1}\">{2}</{0}>{3}", new object[]
							{
								analysisRule.Name,
								analysisRule.AlertLevel,
								analysisRule.Message,
								Environment.NewLine
							});
						}
						sb.AppendLine("        </Alerts>");
					}
					sb.AppendLine("    </CalendarLogAnalysis>");
				}
			}
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x0011697C File Offset: 0x00114B7C
		protected override void SerializeFooter(IEnumerable<CalendarLogAnalysis> logs, IEnumerable<PropertyDefinition> properties, ref StringBuilder sb)
		{
			sb.AppendLine("</CalendarLogAnalysisItems>");
		}
	}
}
