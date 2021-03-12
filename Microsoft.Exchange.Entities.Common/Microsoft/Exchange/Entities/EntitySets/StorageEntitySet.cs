using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000031 RID: 49
	internal abstract class StorageEntitySet<TEntitySet, TEntity, TCommandFactory, TSession> : EntitySet<TEntitySet, TEntity, TCommandFactory>, IStorageEntitySetScope<TSession> where TEntitySet : class, IEntitySet<TEntity> where TEntity : class, IEntity where TCommandFactory : IEntityCommandFactory<TEntitySet, TEntity> where TSession : class, IStoreSession
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00004504 File Offset: 0x00002704
		internal StorageEntitySet(IStorageEntitySetScope<TSession> parentScope, string relativeName, TCommandFactory commandFactory) : base(commandFactory)
		{
			this.description = string.Format("{0}.{1}", parentScope, relativeName);
			this.Session = parentScope.StoreSession;
			this.StoreSession = this.Session;
			this.XsoFactory = parentScope.XsoFactory;
			this.RecipientSession = parentScope.RecipientSession;
			this.IdConverter = parentScope.IdConverter;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004566 File Offset: 0x00002766
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000456E File Offset: 0x0000276E
		public IRecipientSession RecipientSession { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004577 File Offset: 0x00002777
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000457F File Offset: 0x0000277F
		public TSession Session { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004588 File Offset: 0x00002788
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00004590 File Offset: 0x00002790
		public TSession StoreSession { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004599 File Offset: 0x00002799
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000045A1 File Offset: 0x000027A1
		public IXSOFactory XsoFactory { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000045AA File Offset: 0x000027AA
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000045B2 File Offset: 0x000027B2
		public IdConverter IdConverter { get; private set; }

		// Token: 0x06000103 RID: 259 RVA: 0x000045BB File Offset: 0x000027BB
		public sealed override string ToString()
		{
			return this.description;
		}

		// Token: 0x04000043 RID: 67
		private readonly string description;
	}
}
