using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000786 RID: 1926
	internal class CsvLogSerializer : LogSerializer
	{
		// Token: 0x060043D6 RID: 17366 RVA: 0x001163A4 File Offset: 0x001145A4
		public CsvLogSerializer(IEnumerable<PropertyDefinition> propertyMask)
		{
			this.propertyMask = propertyMask;
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x001163B4 File Offset: 0x001145B4
		protected override void SerializeHeader(IEnumerable<CalendarLogAnalysis> logs, IEnumerable<PropertyDefinition> properties, ref StringBuilder sb)
		{
			sb.AppendFormat("Local Log Time{0}", ',');
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				if (!this.propertyMask.Contains(propertyDefinition))
				{
					sb.AppendFormat("{0}{1}", this.CsvEscapeString(propertyDefinition.Name), ',');
				}
			}
			sb.Length--;
			sb.AppendLine();
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x00116450 File Offset: 0x00114650
		protected override void SerializeBody(IEnumerable<CalendarLogAnalysis> logs, IEnumerable<PropertyDefinition> properties, ref StringBuilder sb)
		{
			foreach (CalendarLogAnalysis calendarLogAnalysis in logs)
			{
				if (calendarLogAnalysis.HasAlerts)
				{
					sb.AppendFormat("{0}{1}", this.CsvEscapeString(calendarLogAnalysis.LocalLogTime), ',');
					foreach (PropertyDefinition propertyDefinition in properties)
					{
						if (!this.propertyMask.Contains(propertyDefinition))
						{
							sb.AppendFormat("{0}{1}", this.CsvEscapeString(calendarLogAnalysis[propertyDefinition]), ',');
						}
					}
					if (calendarLogAnalysis.Alerts.Count<AnalysisRule>() > 0)
					{
						sb.AppendFormat("{0} ALERTS:", calendarLogAnalysis.Alerts.Count<AnalysisRule>());
						foreach (AnalysisRule analysisRule in calendarLogAnalysis.Alerts)
						{
							sb.AppendFormat("[{0}]", analysisRule.ToString());
						}
					}
					sb.AppendLine();
				}
			}
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x001165CC File Offset: 0x001147CC
		private string CsvEscapeString(string str)
		{
			if (str.Contains(','))
			{
				return string.Format("\"{0}\"", str);
			}
			return str;
		}

		// Token: 0x04002A29 RID: 10793
		public const char Delimiter = ',';
	}
}
