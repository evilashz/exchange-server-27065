using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.UserPhotos
{
	// Token: 0x02000145 RID: 325
	internal sealed class UserPhotoQuery : BaseQuery
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000266E0 File Offset: 0x000248E0
		public new UserPhotoQueryResult Result
		{
			get
			{
				return (UserPhotoQueryResult)base.Result;
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000266ED File Offset: 0x000248ED
		public static UserPhotoQuery CreateFromUnknown(RecipientData data, LocalizedException exception, ITracer upstreamTracer)
		{
			return new UserPhotoQuery(data, new UserPhotoQueryResult(exception, upstreamTracer));
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000266FC File Offset: 0x000248FC
		public static UserPhotoQuery CreateFromIndividual(RecipientData data, ITracer upstreamTracer)
		{
			return new UserPhotoQuery(data, null);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00026705 File Offset: 0x00024905
		public static UserPhotoQuery CreateFromIndividual(RecipientData data, LocalizedException exception, ITracer upstreamTracer)
		{
			return new UserPhotoQuery(data, new UserPhotoQueryResult(exception, upstreamTracer));
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00026714 File Offset: 0x00024914
		private UserPhotoQuery(RecipientData recipientData, UserPhotoQueryResult result) : base(recipientData, result)
		{
		}
	}
}
