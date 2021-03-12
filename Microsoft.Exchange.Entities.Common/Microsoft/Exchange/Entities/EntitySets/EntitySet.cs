using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.Entities.EntitySets.Linq;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x0200002F RID: 47
	internal abstract class EntitySet<TEntitySet, TEntity, TCommandFactory> : IEntitySet<TEntity> where TEntitySet : class where TEntity : class, IEntity where TCommandFactory : IEntityCommandFactory<TEntitySet, TEntity>
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000043B9 File Offset: 0x000025B9
		protected EntitySet(TCommandFactory commandFactory)
		{
			this.CommandFactory = commandFactory;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000043C8 File Offset: 0x000025C8
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000043D0 File Offset: 0x000025D0
		private protected TCommandFactory CommandFactory { protected get; private set; }

		// Token: 0x060000ED RID: 237 RVA: 0x000043D9 File Offset: 0x000025D9
		public IQueryable<TEntity> AsQueryable(CommandContext context = null)
		{
			return new EntitySetQueryProvider<TEntity>(this, context);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000043E4 File Offset: 0x000025E4
		public TEntity Create(TEntity entity, CommandContext context = null)
		{
			TCommandFactory commandFactory = this.CommandFactory;
			ICreateEntityCommand<TEntitySet, TEntity> createEntityCommand = commandFactory.CreateCreateCommand(entity, this as TEntitySet);
			return createEntityCommand.Execute(context);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000441C File Offset: 0x0000261C
		public void Delete(string key, CommandContext context = null)
		{
			TCommandFactory commandFactory = this.CommandFactory;
			IDeleteEntityCommand<TEntitySet> deleteEntityCommand = commandFactory.CreateDeleteCommand(key, this as TEntitySet);
			deleteEntityCommand.Execute(context);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004452 File Offset: 0x00002652
		public virtual int EstimateTotalCount(IEntityQueryOptions queryOptions, CommandContext context = null)
		{
			throw new UnsupportedEstimatedTotalCountException();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000445C File Offset: 0x0000265C
		public IEnumerable<TEntity> Find(IEntityQueryOptions queryOptions = null, CommandContext context = null)
		{
			TCommandFactory commandFactory = this.CommandFactory;
			IFindEntitiesCommand<TEntitySet, TEntity> findEntitiesCommand = commandFactory.CreateFindCommand(queryOptions, this as TEntitySet);
			return findEntitiesCommand.Execute(context);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004494 File Offset: 0x00002694
		public TEntity Read(string key, CommandContext context = null)
		{
			TCommandFactory commandFactory = this.CommandFactory;
			IReadEntityCommand<TEntitySet, TEntity> readEntityCommand = commandFactory.CreateReadCommand(key, this as TEntitySet);
			return readEntityCommand.Execute(context);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000044CC File Offset: 0x000026CC
		public TEntity Update(string key, TEntity entity, CommandContext context = null)
		{
			TCommandFactory commandFactory = this.CommandFactory;
			IUpdateEntityCommand<TEntitySet, TEntity> updateEntityCommand = commandFactory.CreateUpdateCommand(key, entity, this as TEntitySet);
			return updateEntityCommand.Execute(context);
		}
	}
}
