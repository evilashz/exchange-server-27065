using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x02000012 RID: 18
	internal abstract class StorageFolderDataProvider<TSession, TEntity, TStoreFolder> : StorageObjectDataProvider<TSession, TEntity, TStoreFolder> where TSession : class, IStoreSession where TEntity : IStorageEntity, new() where TStoreFolder : IFolder
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003787 File Offset: 0x00001987
		protected StorageFolderDataProvider(IStorageEntitySetScope<TSession> scope, ITracer trace) : base(scope, trace)
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003791 File Offset: 0x00001991
		protected override SaveMode ConflictResolutionSaveMode
		{
			get
			{
				return SaveMode.FailOnAnyConflict;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003794 File Offset: 0x00001994
		protected internal override void ValidateStoreObjectIdForCorrectType(StoreObjectId storeObjectId)
		{
			if (!storeObjectId.IsFolderId)
			{
				string id = storeObjectId.ToString();
				throw new ObjectNotFoundException(Strings.CanNotUseFolderIdForItem(id));
			}
		}
	}
}
