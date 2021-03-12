using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000176 RID: 374
	internal class ByteQuantifiedSizeAsMbCoverter : TextConverter
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x0003ACAC File Offset: 0x00038EAC
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			return ((ByteQuantifiedSize)arg).ToMB().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003ACD4 File Offset: 0x00038ED4
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			return ByteQuantifiedSize.Parse(s, ByteQuantifiedSize.Quantifier.MB);
		}
	}
}
