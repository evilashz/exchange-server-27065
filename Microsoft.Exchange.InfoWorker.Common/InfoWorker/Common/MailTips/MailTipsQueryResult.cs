using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000124 RID: 292
	internal sealed class MailTipsQueryResult : BaseQueryResult
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x000246C9 File Offset: 0x000228C9
		public MailTips MailTips
		{
			get
			{
				return this.mailTips;
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000246D1 File Offset: 0x000228D1
		internal MailTipsQueryResult(MailTips mailTips)
		{
			this.mailTips = mailTips;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000246E0 File Offset: 0x000228E0
		internal MailTipsQueryResult(LocalizedException exception) : base(exception)
		{
		}

		// Token: 0x040004C5 RID: 1221
		private MailTips mailTips;
	}
}
