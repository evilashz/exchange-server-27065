using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000074 RID: 116
	internal sealed class IcsFolderUpload : IcsUpload
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00020808 File Offset: 0x0001EA08
		public IcsFolderUpload(ReferenceCount<CoreFolder> sourceFolder, Logon logonObject) : base(sourceFolder, logonObject)
		{
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00020812 File Offset: 0x0001EA12
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IcsFolderUpload>(this);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0002081A File Offset: 0x0001EA1A
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.hierarchySynchronizationUploadContext);
			base.InternalDispose();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00020830 File Offset: 0x0001EA30
		internal StoreId ImportFolderChange(PropertyValue[] hierarchyPropertyValues, PropertyValue[] folderPropertyValues)
		{
			Util.ThrowOnNullArgument(hierarchyPropertyValues, "hierarchyPropertyValues");
			Util.ThrowOnNullArgument(folderPropertyValues, "folderPropertyValues");
			if (hierarchyPropertyValues.Length != IcsFolderUpload.FolderChangePropertyTags.Length)
			{
				throw new RopExecutionException(string.Format("Incorrect number of Folder Change properties. Expected = {0}. Found = {1}.", IcsFolderUpload.FolderChangePropertyTags.Length, hierarchyPropertyValues.Length), (ErrorCode)2147942487U);
			}
			for (int i = 0; i < IcsFolderUpload.FolderChangePropertyTags.Length; i++)
			{
				if (hierarchyPropertyValues[i].PropertyTag.PropertyId != IcsFolderUpload.FolderChangePropertyTags[i].PropertyId)
				{
					throw new RopExecutionException(string.Format("Unexpected property tag found in Folder Change properties. Expected = {0}. Found = {1}.", IcsFolderUpload.FolderChangePropertyTags[i], hierarchyPropertyValues[i].PropertyTag), (ErrorCode)2147942487U);
				}
			}
			byte[] value = hierarchyPropertyValues[0].GetValue<byte[]>();
			StoreObjectId parentFolderId;
			if (value != null && value.Length == 0)
			{
				parentFolderId = base.CoreFolder.Id.ObjectId;
			}
			else
			{
				IcsUpload.ValidateSourceKey(value, "parentSourceKey");
				long idFromLongTermId = base.CoreFolder.Session.IdConverter.GetIdFromLongTermId(value);
				parentFolderId = base.CoreFolder.Session.IdConverter.CreateFolderId(idFromLongTermId);
			}
			byte[] value2 = hierarchyPropertyValues[1].GetValue<byte[]>();
			IcsUpload.ValidateSourceKey(value2, "sourceKey");
			long idFromLongTermId2 = base.CoreFolder.Session.IdConverter.GetIdFromLongTermId(value2);
			ExDateTime value3 = hierarchyPropertyValues[2].GetValue<ExDateTime>();
			StoreObjectId storeObjectId = base.CoreFolder.Session.IdConverter.CreateFolderId(idFromLongTermId2);
			byte[] value4 = hierarchyPropertyValues[3].GetValue<byte[]>();
			IcsUpload.ValidateChangeKey(value4, "changeKey");
			VersionedId folderId = VersionedId.FromStoreObjectId(storeObjectId, value4);
			byte[] value5 = hierarchyPropertyValues[4].GetValue<byte[]>();
			string value6 = hierarchyPropertyValues[5].GetValue<string>();
			Folder.CheckDisplayNameValid(value6, (ErrorCode)2147942487U);
			this.EnsureSynchronizationUploadContext();
			StoreId empty;
			using (CoreFolder coreFolder = CoreFolder.Import(this.hierarchySynchronizationUploadContext, parentFolderId, folderId, value3, value5, value6))
			{
				using (Folder folder = new Folder(coreFolder, base.LogonObject))
				{
					folder.SetProperties(folderPropertyValues, true);
					empty = StoreId.Empty;
				}
			}
			return empty;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00020A78 File Offset: 0x0001EC78
		internal override ErrorCode InternalImportDelete(ImportDeleteFlags importDeleteFlags, byte[][] sourceKeys)
		{
			if ((byte)(importDeleteFlags & ImportDeleteFlags.Hierarchy) != 1)
			{
				throw new RopExecutionException("Unable to delete message(s) using a hierarchy collector.", (ErrorCode)2147746050U);
			}
			if (sourceKeys.Length == 0)
			{
				return ErrorCode.None;
			}
			StoreObjectId[] array = new StoreObjectId[sourceKeys.Length];
			int num = 0;
			foreach (byte[] array2 in sourceKeys)
			{
				IcsUpload.ValidateSourceKey(array2, "folderSourceKey");
				long idFromLongTermId = base.CoreFolder.Session.IdConverter.GetIdFromLongTermId(array2);
				array[num++] = base.CoreFolder.Session.IdConverter.CreateFolderId(idFromLongTermId);
			}
			this.EnsureSynchronizationUploadContext();
			OperationResult result = CoreFolder.Delete(this.hierarchySynchronizationUploadContext, IcsUpload.GetXsoDeleteItemFlags(importDeleteFlags), array);
			return IcsUpload.ConvertGroupOperationResultToErrorCode(result);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00020B2C File Offset: 0x0001ED2C
		internal override void UpdateState()
		{
			this.EnsureSynchronizationUploadContext();
			ISession session = new SessionAdaptor(base.CoreFolder.Session);
			IPropertyBag propertyBag = new MemoryPropertyBag(session);
			IcsStateStream icsStateStream = new IcsStateStream(propertyBag);
			StorageIcsState state = icsStateStream.ToXsoState();
			this.hierarchySynchronizationUploadContext.GetCurrentState(ref state);
			icsStateStream.FromXsoState(state);
			base.IcsStateHelper.IcsState.Load(IcsStateOrigin.ServerIncremental, propertyBag);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00020B8C File Offset: 0x0001ED8C
		private void EnsureSynchronizationUploadContext()
		{
			if (this.hierarchySynchronizationUploadContext == null)
			{
				ISession session = new SessionAdaptor(base.CoreFolder.Session);
				IPropertyBag propertyBag = new MemoryPropertyBag(session);
				base.IcsStateHelper.IcsState.Checkpoint(propertyBag);
				IcsStateStream icsStateStream = new IcsStateStream(propertyBag);
				this.hierarchySynchronizationUploadContext = new HierarchySynchronizationUploadContext(base.CoreFolder, icsStateStream.ToXsoState());
			}
		}

		// Token: 0x040001AD RID: 429
		private static readonly PropertyTag[] FolderChangePropertyTags = new PropertyTag[]
		{
			PropertyTag.ParentSourceKey,
			PropertyTag.SourceKey,
			PropertyTag.LastModificationTime,
			PropertyTag.ChangeKey,
			PropertyTag.PredecessorChangeList,
			PropertyTag.DisplayName
		};

		// Token: 0x040001AE RID: 430
		private HierarchySynchronizationUploadContext hierarchySynchronizationUploadContext;

		// Token: 0x02000075 RID: 117
		internal enum FolderChangePropertyTagIndex
		{
			// Token: 0x040001B0 RID: 432
			ParentSourceKey,
			// Token: 0x040001B1 RID: 433
			SourceKey,
			// Token: 0x040001B2 RID: 434
			LastModificationTime,
			// Token: 0x040001B3 RID: 435
			ChangeKey,
			// Token: 0x040001B4 RID: 436
			PredecessorChangeList,
			// Token: 0x040001B5 RID: 437
			DisplayName
		}
	}
}
