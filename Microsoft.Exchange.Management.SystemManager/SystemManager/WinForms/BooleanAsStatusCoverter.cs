using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000178 RID: 376
	internal class BooleanAsStatusCoverter : TextConverter
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x0003AD58 File Offset: 0x00038F58
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			if (!(bool)arg)
			{
				return Strings.DisabledStatus.ToString();
			}
			return Strings.EnabledStatus.ToString();
		}
	}
}
