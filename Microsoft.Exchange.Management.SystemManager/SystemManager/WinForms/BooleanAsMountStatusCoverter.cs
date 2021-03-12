using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000179 RID: 377
	internal class BooleanAsMountStatusCoverter : TextConverter
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x0003AD9C File Offset: 0x00038F9C
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			if (!(bool)arg)
			{
				return Strings.DismountedStatus.ToString();
			}
			return Strings.MountedStatus.ToString();
		}
	}
}
