using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class StorageObjectDataProvider<TSession, TEntity, TStoreObject> : StorageDataProvider<TSession, TEntity, StoreId> where TSession : class, IStoreSession where TEntity : IStorageEntity where TStoreObject : IStoreObject, IDisposable
	{
		// Token: 0x06000061 RID: 97 RVA: 0x0000327F File Offset: 0x0000147F
		protected StorageObjectDataProvider(IStorageEntitySetScope<TSession> scope, ITracer trace) : base(scope, trace)
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000062 RID: 98 RVA: 0x0000328C File Offset: 0x0000148C
		// (remove) Token: 0x06000063 RID: 99 RVA: 0x000032C4 File Offset: 0x000014C4
		public event EventHandler<TStoreObject> StoreObjectSaved;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000064 RID: 100 RVA: 0x000032FC File Offset: 0x000014FC
		// (remove) Token: 0x06000065 RID: 101 RVA: 0x00003334 File Offset: 0x00001534
		public event Action<TEntity, TStoreObject> BeforeStoreObjectSaved;

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102
		protected abstract IStorageTranslator<TStoreObject, TEntity> Translator { get; }

		// Token: 0x06000067 RID: 103 RVA: 0x0000336C File Offset: 0x0000156C
		public virtual TStoreObject Bind(StoreId id)
		{
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(id);
			if (storeObjectId != null)
			{
				this.ValidateStoreObjectIdForCorrectType(storeObjectId);
			}
			TStoreObject result;
			try
			{
				result = this.BindToStoreObject(id);
			}
			catch (WrongObjectTypeException innerException)
			{
				throw new ObjectNotFoundException(Strings.ItemWithGivenIdNotFound(id.ToString()), innerException);
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000033B8 File Offset: 0x000015B8
		public virtual TStoreObject BindToWrite(StoreId id, string changeKey)
		{
			return this.Bind(id);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033C4 File Offset: 0x000015C4
		public override TEntity Create(TEntity entity)
		{
			this.Validate(entity, true);
			TEntity result;
			using (TStoreObject tstoreObject = this.CreateNewStoreObject())
			{
				result = this.Update(entity, tstoreObject, SaveMode.NoConflictResolution);
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003420 File Offset: 0x00001620
		public override void Delete(StoreId id, DeleteItemFlags flags)
		{
			AggregateOperationResult aggregateOperationResult;
			try
			{
				TSession session = base.Session;
				aggregateOperationResult = session.Delete(flags, new StoreId[]
				{
					id
				});
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new AccessDeniedException(Strings.ErrorAccessDenied, innerException);
			}
			if (aggregateOperationResult.OperationResult == OperationResult.Failed)
			{
				GroupOperationResult groupOperationResult = aggregateOperationResult.GroupOperationResults.First((GroupOperationResult singleResult) => singleResult.OperationResult == OperationResult.Failed);
				throw groupOperationResult.Exception;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000034AC File Offset: 0x000016AC
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> MapProperties(IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> properties)
		{
			return this.Translator.Map(properties);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000034BC File Offset: 0x000016BC
		public override TEntity Read(StoreId id)
		{
			TEntity result;
			using (TStoreObject tstoreObject = this.Bind(id))
			{
				result = this.ConvertToEntity(tstoreObject);
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003504 File Offset: 0x00001704
		public override TEntity Update(TEntity entity, CommandContext commandContext)
		{
			TEntity result;
			using (TStoreObject tstoreObject = this.ValidateAndBindToWrite(entity))
			{
				result = this.Update(entity, tstoreObject, commandContext);
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000354C File Offset: 0x0000174C
		public virtual TEntity Update(TEntity sourceEntity, TStoreObject targetStoreObject, CommandContext commandContext)
		{
			SaveMode saveMode = base.GetSaveMode(sourceEntity.ChangeKey, commandContext);
			return this.Update(sourceEntity, targetStoreObject, saveMode);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003577 File Offset: 0x00001777
		public virtual TEntity Update(TEntity sourceEntity, TStoreObject targetStoreObject, SaveMode saveMode)
		{
			this.UpdateOnly(sourceEntity, targetStoreObject, saveMode);
			return this.ConvertToEntity(targetStoreObject);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000358C File Offset: 0x0000178C
		public virtual void UpdateOnly(TEntity sourceEntity, StoreId targetStoreObjectId)
		{
			using (TStoreObject tstoreObject = this.BindToWrite(targetStoreObjectId, null))
			{
				this.UpdateOnly(sourceEntity, tstoreObject, SaveMode.NoConflictResolution);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035D4 File Offset: 0x000017D4
		public virtual void UpdateOnly(TEntity sourceEntity, TStoreObject targetStoreObject, SaveMode saveMode)
		{
			this.ValidateAndApplyChanges(sourceEntity, targetStoreObject);
			this.OnBeforeStoreObjectSaved(sourceEntity, targetStoreObject);
			this.SaveAndCheckForConflicts(targetStoreObject, saveMode);
			this.LoadStoreObject(targetStoreObject);
			this.OnStoreObjectSaved(targetStoreObject);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035FC File Offset: 0x000017FC
		public virtual TStoreObject ValidateAndBindToWrite(TEntity entity)
		{
			this.Validate(entity, false);
			StoreId storeId = base.IdConverter.GetStoreId(entity);
			return this.BindToWrite(storeId, entity.ChangeKey);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003637 File Offset: 0x00001837
		public virtual TEntity ConvertToEntity(TStoreObject storeObject)
		{
			return this.Translator.ConvertToEntity(storeObject);
		}

		// Token: 0x06000074 RID: 116
		protected internal abstract TStoreObject BindToStoreObject(StoreId id);

		// Token: 0x06000075 RID: 117
		protected internal abstract void ValidateStoreObjectIdForCorrectType(StoreObjectId storeObjectId);

		// Token: 0x06000076 RID: 118 RVA: 0x00003645 File Offset: 0x00001845
		protected virtual void LoadStoreObject(TStoreObject storeObject)
		{
			storeObject.Load();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003654 File Offset: 0x00001854
		protected virtual void ValidateChanges(TEntity sourceEntity, TStoreObject targetStoreObject)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003656 File Offset: 0x00001856
		protected virtual void ValidateAndApplyChanges(TEntity sourceEntity, TStoreObject targetStoreObject)
		{
			this.ValidateChanges(sourceEntity, targetStoreObject);
			this.Translator.SetPropertiesFromEntityOnStorageObject(sourceEntity, targetStoreObject);
		}

		// Token: 0x06000079 RID: 121
		protected abstract TStoreObject CreateNewStoreObject();

		// Token: 0x0600007A RID: 122 RVA: 0x00003698 File Offset: 0x00001898
		protected virtual Dictionary<Microsoft.Exchange.Data.PropertyDefinition, int> GetPropertyIndices(Microsoft.Exchange.Data.PropertyDefinition[] loadedProperties)
		{
			int index = 0;
			return loadedProperties.ToDictionary((Microsoft.Exchange.Data.PropertyDefinition property) => property, (Microsoft.Exchange.Data.PropertyDefinition property) => index++);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003714 File Offset: 0x00001914
		protected virtual IEnumerable<TEntity> ReadQueryResults(object[][] rows, Dictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices)
		{
			return from row in rows
			select this.Translator.ConvertToEntity(propertyIndices, row, this.Session);
		}

		// Token: 0x0600007C RID: 124
		protected abstract void SaveAndCheckForConflicts(TStoreObject storeObject, SaveMode saveMode);

		// Token: 0x0600007D RID: 125 RVA: 0x00003748 File Offset: 0x00001948
		protected void OnStoreObjectSaved(TStoreObject storeObject)
		{
			EventHandler<TStoreObject> storeObjectSaved = this.StoreObjectSaved;
			if (storeObjectSaved != null)
			{
				storeObjectSaved(this, storeObject);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003768 File Offset: 0x00001968
		protected void OnBeforeStoreObjectSaved(TEntity sourceEntity, TStoreObject storeObject)
		{
			Action<TEntity, TStoreObject> beforeStoreObjectSaved = this.BeforeStoreObjectSaved;
			if (beforeStoreObjectSaved != null)
			{
				beforeStoreObjectSaved(sourceEntity, storeObject);
			}
		}
	}
}
