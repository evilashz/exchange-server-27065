using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EC0 RID: 3776
	internal abstract class FindEntitiesResponse<TEntity> : ODataResponse<IFindEntitiesResult<TEntity>> where TEntity : Entity
	{
		// Token: 0x0600622E RID: 25134 RVA: 0x001337F2 File Offset: 0x001319F2
		public FindEntitiesResponse(FindEntitiesRequest<TEntity> request) : base(request)
		{
		}
	}
}
