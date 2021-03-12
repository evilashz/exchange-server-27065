using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000177 RID: 375
	internal class ByteQuantifiedSizeAsDetailedFormatCoverter : TextConverter
	{
		// Token: 0x06000F24 RID: 3876 RVA: 0x0003ACF0 File Offset: 0x00038EF0
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			ByteQuantifiedSize byteQuantifiedSize = (ByteQuantifiedSize)arg;
			if (byteQuantifiedSize.ToGB() > 0UL)
			{
				return Strings.ByteQuantifiedSizeAsGB(byteQuantifiedSize.ToGB());
			}
			if (byteQuantifiedSize.ToMB() > 0UL)
			{
				return Strings.ByteQuantifiedSizeAsMB(byteQuantifiedSize.ToMB());
			}
			return Strings.ByteQuantifiedSizeAsKB(byteQuantifiedSize.ToKB());
		}
	}
}
