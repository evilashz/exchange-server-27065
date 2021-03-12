using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x02000013 RID: 19
	internal abstract class StorageItemDataProvider<TSession, TEntity, TStoreItem> : StorageObjectDataProvider<TSession, TEntity, TStoreItem> where TSession : class, IStoreSession where TEntity : IStorageEntity where TStoreItem : IItem
	{
		// Token: 0x06000084 RID: 132 RVA: 0x000037BC File Offset: 0x000019BC
		protected StorageItemDataProvider(IStorageEntitySetScope<TSession> scope, StoreId containerFolderId, ITracer trace) : base(scope, trace)
		{
			this.ContainerFolderId = containerFolderId;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000037CD File Offset: 0x000019CD
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000037D5 File Offset: 0x000019D5
		public StoreId ContainerFolderId { get; private set; }

		// Token: 0x06000087 RID: 135 RVA: 0x000037E0 File Offset: 0x000019E0
		public override TStoreItem BindToWrite(StoreId id, string changeKey)
		{
			TStoreItem result = base.BindToWrite(id, changeKey);
			if (string.IsNullOrEmpty(changeKey))
			{
				result.OpenAsReadWrite();
			}
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003B0C File Offset: 0x00001D0C
		public virtual IEnumerable<TEntity> Find(QueryFilter queryFilter, SortBy[] sortColumns, params PropertyDefinition[] propertiesToLoad)
		{
			using (IFolder folder = this.BindToContainingFolder())
			{
				using (IQueryResult result = folder.IItemQuery(ItemQueryType.None, queryFilter, sortColumns, propertiesToLoad))
				{
					Dictionary<PropertyDefinition, int> propertyIndices = this.GetPropertyIndices(propertiesToLoad);
					object[][] rows;
					do
					{
						bool mightBeMoreRows;
						rows = result.GetRows(10, out mightBeMoreRows);
						IEnumerable<TEntity> currentBatch = this.ReadQueryResults(rows, propertyIndices);
						foreach (TEntity theEvent in currentBatch)
						{
							yield return theEvent;
						}
					}
					while (rows.Length > 0);
				}
			}
			yield break;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003B40 File Offset: 0x00001D40
		protected internal override void ValidateStoreObjectIdForCorrectType(StoreObjectId storeObjectId)
		{
			if (storeObjectId.IsFolderId)
			{
				string id = storeObjectId.ToString();
				throw new ObjectNotFoundException(Strings.CanNotUseFolderIdForItem(id));
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003B68 File Offset: 0x00001D68
		protected virtual IFolder BindToContainingFolder()
		{
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(this.ContainerFolderId);
			if (storeObjectId == null)
			{
				throw new InvalidRequestException(Strings.ErrorMissingRequiredParameter("ContainerFolderId"));
			}
			return base.XsoFactory.BindToFolder(base.Session, storeObjectId);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003BAC File Offset: 0x00001DAC
		protected override void SaveAndCheckForConflicts(TStoreItem storeObject, SaveMode saveMode)
		{
			ConflictResolutionResult result = storeObject.Save(saveMode);
			result.ThrowOnIrresolvableConflict();
		}
	}
}
