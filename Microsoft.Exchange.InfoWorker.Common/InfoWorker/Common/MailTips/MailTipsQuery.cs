using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000123 RID: 291
	internal sealed class MailTipsQuery : BaseQuery
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0002466B File Offset: 0x0002286B
		public new MailTipsQueryResult Result
		{
			get
			{
				return (MailTipsQueryResult)base.Result;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00024678 File Offset: 0x00022878
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00024680 File Offset: 0x00022880
		internal Dictionary<string, long> LatencyTracker { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00024689 File Offset: 0x00022889
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00024691 File Offset: 0x00022891
		internal MailTipsPermission Permission { get; set; }

		// Token: 0x06000808 RID: 2056 RVA: 0x0002469A File Offset: 0x0002289A
		public static MailTipsQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			return new MailTipsQuery(recipientData, new MailTipsQueryResult(exception));
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000246A8 File Offset: 0x000228A8
		public static MailTipsQuery CreateFromIndividual(RecipientData recipientData)
		{
			return new MailTipsQuery(recipientData, null);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000246B1 File Offset: 0x000228B1
		public static MailTipsQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			return new MailTipsQuery(recipientData, new MailTipsQueryResult(exception));
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000246BF File Offset: 0x000228BF
		private MailTipsQuery(RecipientData recipientData, MailTipsQueryResult result) : base(recipientData, result)
		{
		}
	}
}
