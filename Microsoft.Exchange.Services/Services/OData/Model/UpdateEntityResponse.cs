using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EC7 RID: 3783
	internal abstract class UpdateEntityResponse<TEntity> : ODataResponse<TEntity> where TEntity : Entity
	{
		// Token: 0x06006242 RID: 25154 RVA: 0x001338F2 File Offset: 0x00131AF2
		public UpdateEntityResponse(UpdateEntityRequest<TEntity> request) : base(request)
		{
		}
	}
}
