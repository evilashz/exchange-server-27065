using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000172 RID: 370
	internal class EnhancedTimeSpanAsMinutesCoverter : TextConverter
	{
		// Token: 0x06000F16 RID: 3862 RVA: 0x0003AB40 File Offset: 0x00038D40
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			return ((EnhancedTimeSpan)arg).TotalMinutes.ToString(TextConverter.DoubleFormatString, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003AB6D File Offset: 0x00038D6D
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			return EnhancedTimeSpan.FromMinutes((double)long.Parse(s));
		}
	}
}
