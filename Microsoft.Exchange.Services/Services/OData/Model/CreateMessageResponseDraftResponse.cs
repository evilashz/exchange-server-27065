using System;
using System.Net;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F12 RID: 3858
	internal class CreateMessageResponseDraftResponse : ODataResponse<Message>
	{
		// Token: 0x060062DE RID: 25310 RVA: 0x00134A40 File Offset: 0x00132C40
		public CreateMessageResponseDraftResponse(CreateMessageResponseDraftRequest request) : base(request)
		{
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x060062DF RID: 25311 RVA: 0x00134A49 File Offset: 0x00132C49
		protected override HttpStatusCode HttpResponseCodeOnSuccess
		{
			get
			{
				return HttpStatusCode.Created;
			}
		}
	}
}
