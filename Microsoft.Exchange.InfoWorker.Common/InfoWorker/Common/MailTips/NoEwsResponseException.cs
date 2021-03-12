using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x0200011C RID: 284
	public class NoEwsResponseException : LocalizedException
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x00023387 File Offset: 0x00021587
		public NoEwsResponseException() : base(Strings.descNoEwsResponse)
		{
		}
	}
}
