using System;
using System.Net;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F15 RID: 3861
	internal class RespondToMessageResponse : EmptyResultResponse
	{
		// Token: 0x060062EC RID: 25324 RVA: 0x00134BB2 File Offset: 0x00132DB2
		public RespondToMessageResponse(RespondToMessageRequest request) : base(request)
		{
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x060062ED RID: 25325 RVA: 0x00134BBB File Offset: 0x00132DBB
		protected override HttpStatusCode HttpResponseCodeOnSuccess
		{
			get
			{
				return HttpStatusCode.Accepted;
			}
		}
	}
}
