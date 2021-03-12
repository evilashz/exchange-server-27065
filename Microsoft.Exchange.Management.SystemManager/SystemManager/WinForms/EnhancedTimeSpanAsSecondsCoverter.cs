using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000173 RID: 371
	internal class EnhancedTimeSpanAsSecondsCoverter : TextConverter
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x0003AB88 File Offset: 0x00038D88
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			return ((EnhancedTimeSpan)arg).TotalSeconds.ToString(TextConverter.DoubleFormatString, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0003ABB5 File Offset: 0x00038DB5
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			return EnhancedTimeSpan.FromSeconds((double)long.Parse(s));
		}
	}
}
