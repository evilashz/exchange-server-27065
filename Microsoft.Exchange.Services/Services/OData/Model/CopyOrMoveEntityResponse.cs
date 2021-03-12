using System;
using System.Collections.Specialized;
using System.Net;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EBA RID: 3770
	internal abstract class CopyOrMoveEntityResponse<TEntity> : ODataResponse<TEntity> where TEntity : Entity
	{
		// Token: 0x0600621B RID: 25115 RVA: 0x00133683 File Offset: 0x00131883
		public CopyOrMoveEntityResponse(CopyOrMoveEntityRequest<TEntity> request) : base(request)
		{
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x0600621C RID: 25116 RVA: 0x0013368C File Offset: 0x0013188C
		protected override HttpStatusCode HttpResponseCodeOnSuccess
		{
			get
			{
				return HttpStatusCode.Created;
			}
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x00133693 File Offset: 0x00131893
		protected override void ApplyResponseHeaders(NameValueCollection responseHeaders)
		{
			base.ApplyResponseHeaders(responseHeaders);
			responseHeaders["Location"] = base.GetEntityLocation();
		}
	}
}
