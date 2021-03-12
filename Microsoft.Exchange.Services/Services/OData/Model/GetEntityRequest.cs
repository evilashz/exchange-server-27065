using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EC4 RID: 3780
	internal abstract class GetEntityRequest<TEntity> : ODataRequest<TEntity> where TEntity : Entity
	{
		// Token: 0x06006234 RID: 25140 RVA: 0x0013382E File Offset: 0x00131A2E
		public GetEntityRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06006235 RID: 25141 RVA: 0x00133837 File Offset: 0x00131A37
		// (set) Token: 0x06006236 RID: 25142 RVA: 0x0013383F File Offset: 0x00131A3F
		public string Id { get; protected set; }

		// Token: 0x06006237 RID: 25143 RVA: 0x00133848 File Offset: 0x00131A48
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			this.Id = base.ODataContext.ODataPath.ResolveEntityKey();
		}

		// Token: 0x06006238 RID: 25144 RVA: 0x00133866 File Offset: 0x00131A66
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.Id);
		}
	}
}
