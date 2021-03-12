using System;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x0200001F RID: 31
	public abstract class CreateEntityCommand<TContext, TEntity> : EntityCommand<TContext, TEntity>, ICreateEntityCommand<TContext, TEntity>, IEntityCommand<TContext, TEntity> where TEntity : IEntity
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000414C File Offset: 0x0000234C
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004154 File Offset: 0x00002354
		public TEntity Entity { get; set; }

		// Token: 0x060000C0 RID: 192 RVA: 0x0000415D File Offset: 0x0000235D
		protected override void UpdateCustomLoggingData()
		{
			base.UpdateCustomLoggingData();
			this.SetCustomLoggingData("InputEntity", this.Entity);
		}
	}
}
