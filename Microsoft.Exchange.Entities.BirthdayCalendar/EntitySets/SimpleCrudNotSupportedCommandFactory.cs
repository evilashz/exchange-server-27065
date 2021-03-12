using System;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets
{
	// Token: 0x02000011 RID: 17
	internal class SimpleCrudNotSupportedCommandFactory<TScope, TEntity> : IEntityCommandFactory<TScope, TEntity> where TEntity : IEntity
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002B4A File Offset: 0x00000D4A
		[Obsolete("Use specific create command instead")]
		public ICreateEntityCommand<TScope, TEntity> CreateCreateCommand(TEntity entity, TScope scope)
		{
			throw SimpleCrudNotSupportedCommandFactory<TScope, TEntity>.CreateGenericCrudCommandException();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B51 File Offset: 0x00000D51
		[Obsolete("Use specific delete command instead")]
		public IDeleteEntityCommand<TScope> CreateDeleteCommand(string key, TScope scope)
		{
			throw SimpleCrudNotSupportedCommandFactory<TScope, TEntity>.CreateGenericCrudCommandException();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B58 File Offset: 0x00000D58
		[Obsolete("Use specific find command instead")]
		public IFindEntitiesCommand<TScope, TEntity> CreateFindCommand(IEntityQueryOptions queryOptions, TScope scope)
		{
			throw SimpleCrudNotSupportedCommandFactory<TScope, TEntity>.CreateGenericCrudCommandException();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B5F File Offset: 0x00000D5F
		[Obsolete("Use specific read command instead")]
		public IReadEntityCommand<TScope, TEntity> CreateReadCommand(string key, TScope scope)
		{
			throw SimpleCrudNotSupportedCommandFactory<TScope, TEntity>.CreateGenericCrudCommandException();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002B66 File Offset: 0x00000D66
		[Obsolete("Use specific update command instead")]
		public IUpdateEntityCommand<TScope, TEntity> CreateUpdateCommand(string key, TEntity entity, TScope scope)
		{
			throw SimpleCrudNotSupportedCommandFactory<TScope, TEntity>.CreateGenericCrudCommandException();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002B6D File Offset: 0x00000D6D
		private static Exception CreateGenericCrudCommandException()
		{
			throw new NotSupportedException("Generic CRUDF commands are not supported");
		}
	}
}
