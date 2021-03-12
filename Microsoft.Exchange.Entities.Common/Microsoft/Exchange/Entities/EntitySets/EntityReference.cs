using System;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000039 RID: 57
	public class EntityReference<TEntitySet, TEntity> : IEntityReference<TEntity> where TEntitySet : IEntitySet<TEntity> where TEntity : class, IEntity
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00004906 File Offset: 0x00002B06
		public EntityReference(TEntitySet entitySet, string key)
		{
			this.EntitySet = entitySet;
			this.EntityKey = key;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000491C File Offset: 0x00002B1C
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00004924 File Offset: 0x00002B24
		public string EntityKey { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000492D File Offset: 0x00002B2D
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00004935 File Offset: 0x00002B35
		public TEntitySet EntitySet { get; private set; }

		// Token: 0x0600012A RID: 298 RVA: 0x0000493E File Offset: 0x00002B3E
		string IEntityReference<!1>.GetKey()
		{
			return this.EntityKey;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004948 File Offset: 0x00002B48
		TEntity IEntityReference<!1>.Read(CommandContext context)
		{
			TEntitySet entitySet = this.EntitySet;
			return entitySet.Read(this.EntityKey, context);
		}
	}
}
