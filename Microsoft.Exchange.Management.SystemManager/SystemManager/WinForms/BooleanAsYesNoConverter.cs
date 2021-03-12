using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200017A RID: 378
	internal class BooleanAsYesNoConverter : TextConverter
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x0003ADE0 File Offset: 0x00038FE0
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			if (!(bool)arg)
			{
				return Strings.NoString.ToString();
			}
			return Strings.YesString.ToString();
		}
	}
}
