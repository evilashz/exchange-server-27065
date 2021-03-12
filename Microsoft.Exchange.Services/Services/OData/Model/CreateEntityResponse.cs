using System;
using System.Collections.Specialized;
using System.Net;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EBC RID: 3772
	internal abstract class CreateEntityResponse<TEntity> : ODataResponse<TEntity> where TEntity : Entity
	{
		// Token: 0x06006223 RID: 25123 RVA: 0x00133764 File Offset: 0x00131964
		public CreateEntityResponse(CreateEntityRequest<TEntity> request) : base(request)
		{
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06006224 RID: 25124 RVA: 0x0013376D File Offset: 0x0013196D
		protected override HttpStatusCode HttpResponseCodeOnSuccess
		{
			get
			{
				return HttpStatusCode.Created;
			}
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x00133774 File Offset: 0x00131974
		protected override void ApplyResponseHeaders(NameValueCollection responseHeaders)
		{
			base.ApplyResponseHeaders(responseHeaders);
			responseHeaders["Location"] = base.GetEntityLocation();
		}
	}
}
