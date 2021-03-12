using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EBF RID: 3775
	internal abstract class FindEntitiesRequest<TEntity> : ODataRequest<IFindEntitiesResult<TEntity>> where TEntity : Entity
	{
		// Token: 0x0600622D RID: 25133 RVA: 0x001337E9 File Offset: 0x001319E9
		public FindEntitiesRequest(ODataContext odataContext) : base(odataContext)
		{
		}
	}
}
