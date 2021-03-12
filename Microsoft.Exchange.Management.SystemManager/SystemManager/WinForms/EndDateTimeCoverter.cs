using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200017E RID: 382
	internal class EndDateTimeCoverter : TextConverter
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x0003AEAC File Offset: 0x000390AC
		protected override object ParseObject(string s, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(s))
			{
				DateTime today = DateTime.Today;
				return new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
			}
			return base.ParseObject(s, provider);
		}
	}
}
