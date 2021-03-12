using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000182 RID: 386
	internal class IntegerAsPercentageConverter : TextConverter
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x0003AF7F File Offset: 0x0003917F
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			return string.Format("{0}%", (int)arg);
		}
	}
}
