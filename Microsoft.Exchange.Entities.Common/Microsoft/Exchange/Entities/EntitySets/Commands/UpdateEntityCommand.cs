using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x0200002D RID: 45
	[DataContract]
	internal abstract class UpdateEntityCommand<TEntitySet, TEntity> : KeyedEntityCommand<TEntitySet, TEntity>, IUpdateEntityCommand<TEntitySet, TEntity>, IKeyedEntityCommand<TEntitySet, TEntity>, IEntityCommand<TEntitySet, TEntity> where TEntitySet : IStorageEntitySetScope<IStoreSession> where TEntity : IEntity
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000042F6 File Offset: 0x000024F6
		protected UpdateEntityCommand()
		{
			base.RegisterOnBeforeExecute(new Action(this.EnforceContextConditions));
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004310 File Offset: 0x00002510
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004318 File Offset: 0x00002518
		[DataMember]
		public TEntity Entity { get; set; }

		// Token: 0x060000E3 RID: 227 RVA: 0x00004321 File Offset: 0x00002521
		protected override void UpdateCustomLoggingData()
		{
			base.UpdateCustomLoggingData();
			this.SetCustomLoggingData("InputEntity", this.Entity);
		}

		// Token: 0x060000E4 RID: 228
		protected abstract void SetETag(string eTag);

		// Token: 0x060000E5 RID: 229 RVA: 0x00004340 File Offset: 0x00002540
		private void EnforceContextConditions()
		{
			TEntity entity = this.Entity;
			entity.Id = base.EntityKey;
			if (this.Context != null && !string.IsNullOrEmpty(this.Context.IfMatchETag))
			{
				this.SetETag(this.Context.IfMatchETag);
			}
		}
	}
}
