using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000784 RID: 1924
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CalendarLogAnalysisSerializer
	{
		// Token: 0x060043CB RID: 17355 RVA: 0x0011617C File Offset: 0x0011437C
		internal static string Serialize(IEnumerable<CalendarLogAnalysis> logs, OutputType outputType, AnalysisDetailLevel detailLevel, bool showAll)
		{
			IEnumerable<PropertyDefinition> propertyMask = showAll ? new List<PropertyDefinition>() : CalendarLogAnalysisSerializer.FindUnchangedProperties(logs);
			LogSerializer logSerializer;
			switch (outputType)
			{
			case OutputType.HTML:
				logSerializer = new HtmlLogSerializer(propertyMask);
				goto IL_42;
			case OutputType.XML:
				logSerializer = new XmlLogSerializer(propertyMask);
				goto IL_42;
			}
			logSerializer = new CsvLogSerializer(propertyMask);
			IL_42:
			IEnumerable<PropertyDefinition> properties = AnalysisDetailLevels.GetDisplayProperties(detailLevel).Union(CalendarLogAnalysis.GetDisplayProperties(logs));
			logs.OrderBy((CalendarLogAnalysis f) => f, CalendarLogAnalysis.GetComparer());
			return logSerializer.Serialize(logs, properties, null);
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x00116210 File Offset: 0x00114410
		private static IEnumerable<PropertyDefinition> FindUnchangedProperties(IEnumerable<CalendarLogAnalysis> logs)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			Dictionary<PropertyDefinition, object>.KeyCollection keys = logs.First<CalendarLogAnalysis>().InternalProperties.Keys;
			foreach (PropertyDefinition propertyDefinition in keys)
			{
				if (!CalendarLogAnalysisSerializer.MinPropSet.Contains(propertyDefinition))
				{
					string b = logs.First<CalendarLogAnalysis>()[propertyDefinition];
					bool flag = false;
					foreach (CalendarLogAnalysis calendarLogAnalysis in logs)
					{
						if (calendarLogAnalysis[propertyDefinition] != b)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(propertyDefinition);
					}
				}
			}
			return list;
		}

		// Token: 0x04002A26 RID: 10790
		private static IEnumerable<PropertyDefinition> MinPropSet = new List<PropertyDefinition>
		{
			CalendarItemBaseSchema.OriginalLastModifiedTime,
			CalendarItemBaseSchema.CalendarLogTriggerAction,
			CalendarItemBaseSchema.ClientInfoString
		};
	}
}
