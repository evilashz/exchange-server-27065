using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000170 RID: 368
	internal class EnhancedTimeSpanAsDaysCoverter : TextConverter
	{
		// Token: 0x06000F10 RID: 3856 RVA: 0x0003AAB0 File Offset: 0x00038CB0
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			return ((EnhancedTimeSpan)arg).TotalDays.ToString(TextConverter.DoubleFormatString, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0003AADD File Offset: 0x00038CDD
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			return EnhancedTimeSpan.FromDays((double)long.Parse(s));
		}
	}
}
