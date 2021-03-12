using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EC5 RID: 3781
	internal abstract class GetEntityResponse<TEntity> : ODataResponse<TEntity> where TEntity : Entity
	{
		// Token: 0x06006239 RID: 25145 RVA: 0x00133879 File Offset: 0x00131A79
		public GetEntityResponse(GetEntityRequest<TEntity> request) : base(request)
		{
		}
	}
}
