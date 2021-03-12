using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x02000055 RID: 85
	public interface IEntitySet<TEntity> where TEntity : IEntity
	{
		// Token: 0x060002DC RID: 732
		IQueryable<TEntity> AsQueryable(CommandContext context = null);

		// Token: 0x060002DD RID: 733
		TEntity Create(TEntity entity, CommandContext context = null);

		// Token: 0x060002DE RID: 734
		void Delete(string key, CommandContext context = null);

		// Token: 0x060002DF RID: 735
		int EstimateTotalCount(IEntityQueryOptions queryOptions, CommandContext context = null);

		// Token: 0x060002E0 RID: 736
		IEnumerable<TEntity> Find(IEntityQueryOptions queryOptions = null, CommandContext context = null);

		// Token: 0x060002E1 RID: 737
		TEntity Read(string key, CommandContext context = null);

		// Token: 0x060002E2 RID: 738
		TEntity Update(string key, TEntity entity, CommandContext context = null);
	}
}
