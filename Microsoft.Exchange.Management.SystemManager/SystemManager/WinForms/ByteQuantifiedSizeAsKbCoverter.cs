using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000175 RID: 373
	internal class ByteQuantifiedSizeAsKbCoverter : TextConverter
	{
		// Token: 0x06000F1E RID: 3870 RVA: 0x0003AC4C File Offset: 0x00038E4C
		protected override string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			return ((((ByteQuantifiedSize)arg).ToBytes() == 0UL) ? 0UL : Math.Max(1UL, ((ByteQuantifiedSize)arg).ToKB())).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003AC91 File Offset: 0x00038E91
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			return ByteQuantifiedSize.Parse(s, ByteQuantifiedSize.Quantifier.KB);
		}
	}
}
