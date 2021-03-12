using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002A4 RID: 676
	internal sealed class FindMessageTrackingBaseQuery : BaseQuery
	{
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00056C9A File Offset: 0x00054E9A
		public new FindMessageTrackingBaseQueryResult Result
		{
			get
			{
				return (FindMessageTrackingBaseQueryResult)base.Result;
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00056CA7 File Offset: 0x00054EA7
		public static FindMessageTrackingBaseQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			return new FindMessageTrackingBaseQuery(recipientData, new FindMessageTrackingBaseQueryResult(exception));
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00056CB5 File Offset: 0x00054EB5
		public static FindMessageTrackingBaseQuery CreateFromIndividual(RecipientData recipientData)
		{
			return new FindMessageTrackingBaseQuery(recipientData, null);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00056CBE File Offset: 0x00054EBE
		public static FindMessageTrackingBaseQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			return new FindMessageTrackingBaseQuery(recipientData, new FindMessageTrackingBaseQueryResult(exception));
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00056CCC File Offset: 0x00054ECC
		private FindMessageTrackingBaseQuery(RecipientData recipientData, FindMessageTrackingBaseQueryResult result) : base(recipientData, result)
		{
		}
	}
}
