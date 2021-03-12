using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002A9 RID: 681
	internal sealed class GetMessageTrackingBaseQuery : BaseQuery
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x000571C6 File Offset: 0x000553C6
		public new GetMessageTrackingBaseQueryResult Result
		{
			get
			{
				return (GetMessageTrackingBaseQueryResult)base.Result;
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x000571D3 File Offset: 0x000553D3
		public static GetMessageTrackingBaseQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			return new GetMessageTrackingBaseQuery(recipientData, new GetMessageTrackingBaseQueryResult(exception));
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x000571E1 File Offset: 0x000553E1
		public static GetMessageTrackingBaseQuery CreateFromIndividual(RecipientData recipientData)
		{
			return new GetMessageTrackingBaseQuery(recipientData, null);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000571EA File Offset: 0x000553EA
		public static GetMessageTrackingBaseQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			return new GetMessageTrackingBaseQuery(recipientData, new GetMessageTrackingBaseQueryResult(exception));
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x000571F8 File Offset: 0x000553F8
		private GetMessageTrackingBaseQuery(RecipientData recipientData, GetMessageTrackingBaseQueryResult result) : base(recipientData, result)
		{
		}
	}
}
