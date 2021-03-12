using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x0200000C RID: 12
	internal abstract class DataProvider<TEntity, TId>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000270F File Offset: 0x0000090F
		protected DataProvider(ITracer trace)
		{
			this.Trace = trace;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000271E File Offset: 0x0000091E
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002726 File Offset: 0x00000926
		private protected ITracer Trace { protected get; private set; }

		// Token: 0x06000033 RID: 51
		public abstract TEntity Create(TEntity entity);

		// Token: 0x06000034 RID: 52
		public abstract void Delete(TId id, DeleteItemFlags flags);

		// Token: 0x06000035 RID: 53
		public abstract TEntity Read(TId id);

		// Token: 0x06000036 RID: 54
		public abstract TEntity Update(TEntity entity, CommandContext commandContext);

		// Token: 0x06000037 RID: 55 RVA: 0x0000272F File Offset: 0x0000092F
		public virtual void Validate(TEntity entity, bool isNew)
		{
		}
	}
}
