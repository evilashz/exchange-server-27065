using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class StorageDataProvider<TSession, TEntity, TId> : DataProvider<TEntity, TId> where TSession : class, IStoreSession where TEntity : IEntity
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002731 File Offset: 0x00000931
		protected StorageDataProvider(IStorageEntitySetScope<TSession> scope, ITracer trace) : base(trace)
		{
			this.Scope = scope;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002741 File Offset: 0x00000941
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002749 File Offset: 0x00000949
		public IStorageEntitySetScope<TSession> Scope { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002752 File Offset: 0x00000952
		protected IdConverter IdConverter
		{
			get
			{
				return this.Scope.IdConverter;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000275F File Offset: 0x0000095F
		protected TSession Session
		{
			get
			{
				return this.Scope.StoreSession;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000276C File Offset: 0x0000096C
		protected IXSOFactory XsoFactory
		{
			get
			{
				return this.Scope.XsoFactory;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002779 File Offset: 0x00000979
		protected virtual SaveMode ConflictResolutionSaveMode
		{
			get
			{
				return SaveMode.ResolveConflicts;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000277C File Offset: 0x0000097C
		protected virtual IItem BindToItem(StoreId id)
		{
			return this.XsoFactory.BindToItem(this.Session, id, new PropertyDefinition[0]);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000279B File Offset: 0x0000099B
		protected SaveMode GetSaveMode(string changeKeyInPayload, CommandContext commandContext)
		{
			if (commandContext != null && !string.IsNullOrEmpty(commandContext.IfMatchETag))
			{
				return SaveMode.FailOnAnyConflict;
			}
			if (!string.IsNullOrEmpty(changeKeyInPayload))
			{
				return this.ConflictResolutionSaveMode;
			}
			return SaveMode.NoConflictResolutionForceSave;
		}
	}
}
