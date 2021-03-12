using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200017C RID: 380
	internal class NullableDateTimeAsLogTimeCoverter : TextConverter
	{
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0003AE3C File Offset: 0x0003903C
		protected override string NullValueText
		{
			get
			{
				return Strings.LogTimeNever.ToString();
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003AE5C File Offset: 0x0003905C
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			return ((ExDateTime)arg).ToString("F");
		}
	}
}
