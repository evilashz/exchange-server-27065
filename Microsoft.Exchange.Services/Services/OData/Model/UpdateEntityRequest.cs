using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EC6 RID: 3782
	internal abstract class UpdateEntityRequest<TEntity> : ODataRequest<TEntity> where TEntity : Entity
	{
		// Token: 0x0600623A RID: 25146 RVA: 0x00133882 File Offset: 0x00131A82
		public UpdateEntityRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x0600623B RID: 25147 RVA: 0x0013388B File Offset: 0x00131A8B
		// (set) Token: 0x0600623C RID: 25148 RVA: 0x00133893 File Offset: 0x00131A93
		public string Id { get; protected set; }

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x0600623D RID: 25149 RVA: 0x0013389C File Offset: 0x00131A9C
		// (set) Token: 0x0600623E RID: 25150 RVA: 0x001338A4 File Offset: 0x00131AA4
		public TEntity Change { get; protected set; }

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x0600623F RID: 25151 RVA: 0x001338AD File Offset: 0x00131AAD
		public string ChangeKey
		{
			get
			{
				return base.IfMatchETag;
			}
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x001338B5 File Offset: 0x00131AB5
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			this.Id = base.ODataContext.ODataPath.ResolveEntityKey();
			this.Change = base.ReadPostBodyAsEntity<TEntity>();
		}

		// Token: 0x06006241 RID: 25153 RVA: 0x001338DF File Offset: 0x00131ADF
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.Id);
		}
	}
}
