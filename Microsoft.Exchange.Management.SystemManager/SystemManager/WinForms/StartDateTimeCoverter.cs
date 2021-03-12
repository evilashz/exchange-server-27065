using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200017D RID: 381
	internal class StartDateTimeCoverter : TextConverter
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x0003AE84 File Offset: 0x00039084
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(s))
			{
				return DateTime.Today;
			}
			return base.ParseObject(s, provider);
		}
	}
}
