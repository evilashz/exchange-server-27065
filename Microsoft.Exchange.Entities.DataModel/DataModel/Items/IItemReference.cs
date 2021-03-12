using System;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x0200005A RID: 90
	public interface IItemReference<out TEntity> : IEntityReference<TEntity> where TEntity : class, IEntity
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002ED RID: 749
		IAttachments Attachments { get; }
	}
}
