using System;
using System.Net;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F39 RID: 3897
	internal class RespondToEventResponse : EmptyResultResponse
	{
		// Token: 0x06006341 RID: 25409 RVA: 0x001357B7 File Offset: 0x001339B7
		public RespondToEventResponse(RespondToEventRequest request) : base(request)
		{
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06006342 RID: 25410 RVA: 0x001357C0 File Offset: 0x001339C0
		protected override HttpStatusCode HttpResponseCodeOnSuccess
		{
			get
			{
				return HttpStatusCode.Accepted;
			}
		}
	}
}
