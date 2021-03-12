using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000181 RID: 385
	internal class StringIDToLocalizedStringConverter : TextConverter
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x0003AF43 File Offset: 0x00039143
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(s))
			{
				return LocalizedString.Empty;
			}
			return Strings.GetLocalizedString((Strings.IDs)Enum.Parse(typeof(Strings.IDs), s));
		}
	}
}
