using System;
using System.ComponentModel;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000038 RID: 56
	[ImmutableObject(true)]
	public sealed class EntityCommandFactory<TScope, TEntity, TCreate, TDelete, TFind, TRead, TUpdate> : IEntityCommandFactory<TScope, TEntity> where TEntity : IEntity where TCreate : ICreateEntityCommand<TScope, TEntity>, new() where TDelete : IDeleteEntityCommand<TScope>, new() where TFind : IFindEntitiesCommand<TScope, TEntity>, new() where TRead : IReadEntityCommand<TScope, TEntity>, new() where TUpdate : IUpdateEntityCommand<TScope, TEntity>, new()
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00004752 File Offset: 0x00002952
		private EntityCommandFactory()
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000475C File Offset: 0x0000295C
		public ICreateEntityCommand<TScope, TEntity> CreateCreateCommand(TEntity entity, TScope scope)
		{
			TCreate tcreate = (default(TCreate) == null) ? Activator.CreateInstance<TCreate>() : default(TCreate);
			tcreate.Entity = entity;
			tcreate.Scope = scope;
			return tcreate;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000047AC File Offset: 0x000029AC
		public IDeleteEntityCommand<TScope> CreateDeleteCommand(string key, TScope scope)
		{
			TDelete tdelete = (default(TDelete) == null) ? Activator.CreateInstance<TDelete>() : default(TDelete);
			tdelete.EntityKey = key;
			tdelete.Scope = scope;
			return tdelete;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000047FC File Offset: 0x000029FC
		public IFindEntitiesCommand<TScope, TEntity> CreateFindCommand(IEntityQueryOptions queryOptions, TScope scope)
		{
			TFind tfind = (default(TFind) == null) ? Activator.CreateInstance<TFind>() : default(TFind);
			tfind.QueryOptions = queryOptions;
			tfind.Scope = scope;
			return tfind;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000484C File Offset: 0x00002A4C
		public IReadEntityCommand<TScope, TEntity> CreateReadCommand(string key, TScope scope)
		{
			TRead tread = (default(TRead) == null) ? Activator.CreateInstance<TRead>() : default(TRead);
			tread.EntityKey = key;
			tread.Scope = scope;
			return tread;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000489C File Offset: 0x00002A9C
		public IUpdateEntityCommand<TScope, TEntity> CreateUpdateCommand(string key, TEntity entity, TScope scope)
		{
			TUpdate tupdate = (default(TUpdate) == null) ? Activator.CreateInstance<TUpdate>() : default(TUpdate);
			tupdate.EntityKey = key;
			tupdate.Entity = entity;
			tupdate.Scope = scope;
			return tupdate;
		}

		// Token: 0x0400004E RID: 78
		public static readonly IEntityCommandFactory<TScope, TEntity> Instance = new EntityCommandFactory<TScope, TEntity, TCreate, TDelete, TFind, TRead, TUpdate>();
	}
}
