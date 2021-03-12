using System;
using System.Globalization;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000264 RID: 612
	internal struct SipCulture
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x0004EB26 File Offset: 0x0004CD26
		internal SipCulture(CultureInfo parentCulture, string languageCode)
		{
			this.parentCulture = parentCulture;
			this.languageCode = languageCode;
		}

		// Token: 0x04000665 RID: 1637
		private CultureInfo parentCulture;

		// Token: 0x04000666 RID: 1638
		private string languageCode;
	}
}
