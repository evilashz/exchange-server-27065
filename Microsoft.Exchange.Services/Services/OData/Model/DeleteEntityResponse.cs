using System;
using System.Net;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EBE RID: 3774
	internal abstract class DeleteEntityResponse<TEntity> : EmptyResultResponse where TEntity : Entity
	{
		// Token: 0x0600622B RID: 25131 RVA: 0x001337D9 File Offset: 0x001319D9
		public DeleteEntityResponse(DeleteEntityRequest<TEntity> request) : base(request)
		{
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x0600622C RID: 25132 RVA: 0x001337E2 File Offset: 0x001319E2
		protected override HttpStatusCode HttpResponseCodeOnSuccess
		{
			get
			{
				return HttpStatusCode.NoContent;
			}
		}
	}
}
