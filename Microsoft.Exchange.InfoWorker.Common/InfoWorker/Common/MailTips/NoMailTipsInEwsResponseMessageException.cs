using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x0200011D RID: 285
	public class NoMailTipsInEwsResponseMessageException : LocalizedException
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x00023394 File Offset: 0x00021594
		public NoMailTipsInEwsResponseMessageException() : base(Strings.descNoMailTipsInEwsResponseMessage)
		{
		}
	}
}
