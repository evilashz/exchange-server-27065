using System;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x0200007C RID: 124
	public static class EntitySetExtensions
	{
		// Token: 0x06000365 RID: 869 RVA: 0x000069D2 File Offset: 0x00004BD2
		public static TEntity Update<TEntity>(this IEntitySet<TEntity> entitySet, TEntity entity, CommandContext context = null) where TEntity : class, IEntity
		{
			return entitySet.Update(entity.Id, entity, context);
		}
	}
}
