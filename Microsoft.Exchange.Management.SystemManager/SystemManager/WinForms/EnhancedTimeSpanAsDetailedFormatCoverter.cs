using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000174 RID: 372
	internal class EnhancedTimeSpanAsDetailedFormatCoverter : TextConverter
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x0003ABD0 File Offset: 0x00038DD0
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			EnhancedTimeSpan enhancedTimeSpan = (EnhancedTimeSpan)arg;
			if (enhancedTimeSpan.Days > 0)
			{
				return Strings.EnhancedTimeSpanDetailedFormat(enhancedTimeSpan.Days, enhancedTimeSpan.Hours, enhancedTimeSpan.Minutes);
			}
			if (enhancedTimeSpan.Hours > 0)
			{
				return Strings.EnhancedTimeSpanDetailedFormatWithoutDays(enhancedTimeSpan.Hours, enhancedTimeSpan.Minutes);
			}
			return Strings.EnhancedTimeSpanDetailedFormatWithoutHours(enhancedTimeSpan.Minutes);
		}
	}
}
