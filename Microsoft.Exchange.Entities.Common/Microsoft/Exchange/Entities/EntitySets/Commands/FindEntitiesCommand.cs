using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x02000027 RID: 39
	public abstract class FindEntitiesCommand<TContext, TEntity> : EntityCommand<TContext, IEnumerable<TEntity>>, IFindEntitiesCommand<TContext, TEntity>, IEntityCommand<TContext, IEnumerable<TEntity>> where TEntity : IEntity
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004268 File Offset: 0x00002468
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004270 File Offset: 0x00002470
		public IEntityQueryOptions QueryOptions
		{
			get
			{
				return this.queryOptions;
			}
			set
			{
				this.queryOptions = value;
				this.OnQueryOptionsChanged();
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000427F File Offset: 0x0000247F
		protected virtual void OnQueryOptionsChanged()
		{
		}

		// Token: 0x04000040 RID: 64
		private IEntityQueryOptions queryOptions;
	}
}
