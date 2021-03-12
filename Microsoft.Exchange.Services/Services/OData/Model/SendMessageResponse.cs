using System;
using System.Net;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F18 RID: 3864
	internal class SendMessageResponse : EmptyResultResponse
	{
		// Token: 0x060062F2 RID: 25330 RVA: 0x00134C35 File Offset: 0x00132E35
		public SendMessageResponse(SendMessageRequest request) : base(request)
		{
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x060062F3 RID: 25331 RVA: 0x00134C3E File Offset: 0x00132E3E
		protected override HttpStatusCode HttpResponseCodeOnSuccess
		{
			get
			{
				return HttpStatusCode.Accepted;
			}
		}
	}
}
