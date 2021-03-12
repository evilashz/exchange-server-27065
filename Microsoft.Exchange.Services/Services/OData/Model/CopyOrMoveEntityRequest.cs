using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EB9 RID: 3769
	internal abstract class CopyOrMoveEntityRequest<TEntity> : EntityActionRequest<TEntity> where TEntity : Entity
	{
		// Token: 0x06006216 RID: 25110 RVA: 0x0013361B File Offset: 0x0013181B
		public CopyOrMoveEntityRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06006217 RID: 25111 RVA: 0x00133624 File Offset: 0x00131824
		// (set) Token: 0x06006218 RID: 25112 RVA: 0x0013362C File Offset: 0x0013182C
		public string DestinationId { get; protected set; }

		// Token: 0x06006219 RID: 25113 RVA: 0x00133638 File Offset: 0x00131838
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			object obj;
			if (base.Parameters.TryGetValue("DestinationId", out obj))
			{
				this.DestinationId = (string)obj;
			}
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x0013366B File Offset: 0x0013186B
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateParameterEmpty("DestinationId", this.DestinationId);
		}
	}
}
