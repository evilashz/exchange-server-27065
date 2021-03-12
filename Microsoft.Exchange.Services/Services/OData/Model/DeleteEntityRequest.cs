using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EBD RID: 3773
	internal abstract class DeleteEntityRequest<TEntity> : EmptyResultRequest where TEntity : Entity
	{
		// Token: 0x06006226 RID: 25126 RVA: 0x0013378E File Offset: 0x0013198E
		public DeleteEntityRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06006227 RID: 25127 RVA: 0x00133797 File Offset: 0x00131997
		// (set) Token: 0x06006228 RID: 25128 RVA: 0x0013379F File Offset: 0x0013199F
		public string Id { get; protected set; }

		// Token: 0x06006229 RID: 25129 RVA: 0x001337A8 File Offset: 0x001319A8
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			this.Id = base.ODataContext.ODataPath.ResolveEntityKey();
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x001337C6 File Offset: 0x001319C6
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.Id);
		}
	}
}
