using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000398 RID: 920
	internal sealed class UpdateItemInRecoverableItems : CreateUpdateItemCommandBase<UpdateItemInRecoverableItemsRequest, UpdateItemInRecoverableItemsResponseWrapper>, IDisposeTrackable, IDisposable
	{
		// Token: 0x060019DF RID: 6623 RVA: 0x00094BDC File Offset: 0x00092DDC
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UpdateItemInRecoverableItems>(this);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00094BE4 File Offset: 0x00092DE4
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00094BFC File Offset: 0x00092DFC
		public UpdateItemInRecoverableItems(CallContext callContext, UpdateItemInRecoverableItemsRequest request) : base(callContext, request)
		{
			this.messageDisposition = new MessageDispositionType?(MessageDispositionType.SaveOnly);
			this.savedItemFolderId = new TargetFolderId(new DistinguishedFolderId
			{
				Id = DistinguishedFolderIdName.recoverableitemspurges
			});
			this.responseShape = ServiceCommandBase.DefaultItemResponseShape;
			this.conflictResolutionType = ConflictResolutionType.AlwaysOverwrite;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00094C55 File Offset: 0x00092E55
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00094C78 File Offset: 0x00092E78
		internal override void PreExecuteCommand()
		{
			this.itemId = base.Request.ItemId;
			this.updates = base.Request.PropertyUpdates;
			this.attachments = base.Request.Attachments;
			this.makeItemImmutable = base.Request.MakeItemImmutable;
			ServiceCommandBase.ThrowIfNull(this.itemId, "this.itemId", "UpdateItemInRecoverableItems::PreExecuteCommand");
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x00094CDE File Offset: 0x00092EDE
		internal override int StepCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00094CE4 File Offset: 0x00092EE4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			UpdateItemInRecoverableItemsResponse updateItemInRecoverableItemsResponse = new UpdateItemInRecoverableItemsResponse();
			updateItemInRecoverableItemsResponse.BuildForUpdateItemInRecoverableItemsResults(base.Results);
			return updateItemInRecoverableItemsResponse;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00094D04 File Offset: 0x00092F04
		internal override ServiceResult<UpdateItemInRecoverableItemsResponseWrapper> Execute()
		{
			try
			{
				this.saveToFolderIdAndSession = base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(this.savedItemFolderId.BaseFolderId);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new SavedItemFolderNotFoundException(innerException);
			}
			UpdateItemInRecoverableItemsResponseWrapper value = this.DoUpdateItemInRecoverableItems();
			this.objectsChanged++;
			return new ServiceResult<UpdateItemInRecoverableItemsResponseWrapper>(value);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00094D64 File Offset: 0x00092F64
		protected override ConflictResolutionResult ExecuteCalendarOperation(CalendarItemBase calendarItem, ConflictResolutionType resolutionType)
		{
			throw new NotImplementedException("ExecuteCalendarOperation should not be called in UpdateItemInRecoverableItems");
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00094D70 File Offset: 0x00092F70
		private UpdateItemInRecoverableItemsResponseWrapper DoUpdateItemInRecoverableItems()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadWrite(this.itemId);
			ConflictResults conflictResults = new ConflictResults();
			UpdateItemInRecoverableItemsResponseWrapper result;
			using (Item xsoItemForUpdate = ServiceCommandBase.GetXsoItemForUpdate(idAndSession, new PropertyDefinition[]
			{
				MessageItemSchema.Flags
			}))
			{
				this.ValidateItemToUpdateExistInCorrectFolder(xsoItemForUpdate);
				this.ValidateItemToUpdateIsDraft(xsoItemForUpdate);
				if (this.makeItemImmutable)
				{
					xsoItemForUpdate[InternalSchema.IsDraft] = false;
				}
				ItemType itemType = null;
				bool flag = false;
				if (this.updates != null && this.updates.Length > 0)
				{
					this.UpdateProperties(xsoItemForUpdate, this.updates, false);
					ConflictResolutionResult conflictResolutionResult;
					itemType = base.ExecuteOperation(xsoItemForUpdate, this.responseShape, this.conflictResolutionType, out conflictResolutionResult);
					if (conflictResolutionResult != null && conflictResolutionResult.SaveStatus == SaveResult.SuccessWithConflictResolution && conflictResolutionResult.PropertyConflicts != null)
					{
						conflictResults.Count = conflictResolutionResult.PropertyConflicts.Length;
					}
					flag = true;
				}
				AttachmentType[] array = null;
				if (this.attachments != null && this.attachments.Length > 0)
				{
					List<AttachmentType> list = new List<AttachmentType>(this.attachments.Length);
					AttachmentHierarchy attachmentHierarchy = new AttachmentHierarchy(idAndSession, true, false);
					this.attachmentBuilder = new AttachmentBuilder(attachmentHierarchy, this.attachments, base.IdConverter);
					for (int i = 0; i < this.attachments.Length; i++)
					{
						ServiceError serviceError = null;
						using (Attachment attachment = this.attachmentBuilder.CreateAttachment(this.attachments[i], out serviceError))
						{
							list.Add(this.CreateAttachmentResult(attachment, idAndSession));
						}
					}
					attachmentHierarchy.SaveAll();
					attachmentHierarchy.RootItem.Load();
					using (Item xsoItem = ServiceCommandBase.GetXsoItem(idAndSession.Session, idAndSession.Id, new PropertyDefinition[]
					{
						MessageItemSchema.Flags
					}))
					{
						StoreObjectId storeObjectId = StoreId.GetStoreObjectId(idAndSession.Id);
						itemType = ItemType.CreateFromStoreObjectType(storeObjectId.ObjectType);
						base.LoadServiceObject(itemType, xsoItem, idAndSession, this.responseShape);
					}
					for (int j = 0; j < list.Count; j++)
					{
						AttachmentType attachmentType = list[j];
						ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(attachmentHierarchy.RootItem.Id, idAndSession, null);
						attachmentType.AttachmentId.RootItemId = concatenatedId.Id;
						attachmentType.AttachmentId.RootItemChangeKey = concatenatedId.ChangeKey;
						if (j == list.Count - 1)
						{
							itemType.ItemId.Id = concatenatedId.Id;
							itemType.ItemId.ChangeKey = concatenatedId.ChangeKey;
						}
					}
					array = list.ToArray();
					if (this.makeItemImmutable && !flag)
					{
						xsoItemForUpdate.Save(SaveMode.ResolveConflicts);
						flag = true;
					}
				}
				if (itemType == null)
				{
					throw new ServiceArgumentException((CoreResources.IDs)3654096821U);
				}
				result = new UpdateItemInRecoverableItemsResponseWrapper(itemType, array, conflictResults);
			}
			return result;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00095074 File Offset: 0x00093274
		private void ValidateItemToUpdateExistInCorrectFolder(Item storeItem)
		{
			StoreObjectId parentIdFromItemId = IdConverter.GetParentIdFromItemId(storeItem.StoreObjectId);
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(this.saveToFolderIdAndSession.Id);
			if (!parentIdFromItemId.Equals(asStoreObjectId))
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)2622305962U);
			}
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000950B8 File Offset: 0x000932B8
		private void ValidateItemToUpdateIsDraft(Item storeItem)
		{
			MessageItem messageItem;
			if (XsoDataConverter.TryGetStoreObject<MessageItem>(storeItem, out messageItem) && !messageItem.IsDraft)
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.UpdateNonDraftItemInDumpsterNotAllowed);
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000950E8 File Offset: 0x000932E8
		private AttachmentType CreateAttachmentResult(Attachment attachment, IdAndSession itemIdAndSession)
		{
			AttachmentType attachmentType;
			if (attachment is StreamAttachment)
			{
				attachmentType = new FileAttachmentType();
			}
			else
			{
				attachmentType = new ItemAttachmentType();
			}
			IdAndSession idAndSession = itemIdAndSession.Clone();
			attachment.Load();
			idAndSession.AttachmentIds.Add(attachment.Id);
			attachmentType.AttachmentId = new AttachmentIdType(idAndSession.GetConcatenatedId().Id);
			return attachmentType;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00095143 File Offset: 0x00093343
		private void Dispose(bool fromDispose)
		{
			if (this.attachmentBuilder != null)
			{
				this.attachmentBuilder.Dispose();
				this.attachmentBuilder = null;
			}
		}

		// Token: 0x0400113C RID: 4412
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0400113D RID: 4413
		private TargetFolderId savedItemFolderId;

		// Token: 0x0400113E RID: 4414
		private ItemResponseShape responseShape;

		// Token: 0x0400113F RID: 4415
		private ConflictResolutionType conflictResolutionType;

		// Token: 0x04001140 RID: 4416
		private ItemId itemId;

		// Token: 0x04001141 RID: 4417
		private PropertyUpdate[] updates;

		// Token: 0x04001142 RID: 4418
		private AttachmentType[] attachments;

		// Token: 0x04001143 RID: 4419
		private AttachmentBuilder attachmentBuilder;

		// Token: 0x04001144 RID: 4420
		private bool makeItemImmutable;
	}
}
