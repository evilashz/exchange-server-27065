using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x02000022 RID: 34
	[DataContract]
	internal abstract class KeyedEntityCommand<TEntitySet, TResult> : EntityCommand<TEntitySet, TResult>, IKeyedEntityCommand<TEntitySet, TResult>, IEntityCommand<TEntitySet, TResult> where TEntitySet : IStorageEntitySetScope<IStoreSession>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000041AA File Offset: 0x000023AA
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000041B2 File Offset: 0x000023B2
		[DataMember]
		public string EntityKey { get; set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x000041BC File Offset: 0x000023BC
		protected virtual StoreId GetEntityStoreId()
		{
			string changeKey = (this.Context == null) ? null : this.Context.IfMatchETag;
			TEntitySet scope = this.Scope;
			return scope.IdConverter.ToStoreId(this.EntityKey, changeKey);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004200 File Offset: 0x00002400
		protected override string GetCommandTraceDetails()
		{
			return string.Format("({0})", this.EntityKey);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004212 File Offset: 0x00002412
		protected override void UpdateCustomLoggingData()
		{
			base.UpdateCustomLoggingData();
			this.SetCustomLoggingData("InputKey", this.EntityKey);
		}
	}
}
