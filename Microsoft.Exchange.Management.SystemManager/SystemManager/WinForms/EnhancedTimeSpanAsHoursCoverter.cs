using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000171 RID: 369
	internal class EnhancedTimeSpanAsHoursCoverter : TextConverter
	{
		// Token: 0x06000F13 RID: 3859 RVA: 0x0003AAF8 File Offset: 0x00038CF8
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			return ((EnhancedTimeSpan)arg).TotalHours.ToString(TextConverter.DoubleFormatString, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003AB25 File Offset: 0x00038D25
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			return EnhancedTimeSpan.FromHours((double)long.Parse(s));
		}
	}
}
