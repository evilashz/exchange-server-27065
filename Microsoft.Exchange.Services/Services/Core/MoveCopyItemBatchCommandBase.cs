using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002BA RID: 698
	internal abstract class MoveCopyItemBatchCommandBase<RequestType, MoveCopyCommand> : ItemBatchCommandBase<RequestType, ItemType>, IDisposeTrackable, IDisposable where RequestType : BaseMoveCopyItemRequest where MoveCopyCommand : MoveCopyItemCommandBase
	{
		// Token: 0x060012C1 RID: 4801 RVA: 0x0005BAB0 File Offset: 0x00059CB0
		public MoveCopyItemBatchCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
			this.ServiceResults = null;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0005BAD0 File Offset: 0x00059CD0
		internal override int StepCount
		{
			get
			{
				RequestType request = base.Request;
				return request.Ids.Length;
			}
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0005BAF4 File Offset: 0x00059CF4
		internal override void PreExecuteCommand()
		{
			RequestType request = base.Request;
			this.ItemIds = request.ItemStoreIds;
			try
			{
				IdConverter idConverter = base.IdConverter;
				RequestType request2 = base.Request;
				this.DestFolderIdAndSession = idConverter.ConvertFolderIdToIdAndSessionReadOnly(request2.ToFolderId.BaseFolderId);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new ToFolderNotFoundException(innerException);
			}
			try
			{
				RequestType request3 = base.Request;
				BaseItemId baseItemId = request3.Ids[0];
				this.SourceFolderIdAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(baseItemId);
			}
			catch (ObjectNotFoundException)
			{
				throw new MoveCopyException();
			}
			RequestType request4 = base.Request;
			this.ReturnNewItemIds = request4.ReturnNewItemIds;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0005BBBC File Offset: 0x00059DBC
		protected StoreId FindMovedOrCopiedItemId(Folder destFolder, byte[] searchKey)
		{
			ComparisonFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.SearchKey, searchKey);
			StoreId result;
			using (QueryResult queryResult = destFolder.ItemQuery(ItemQueryType.None, queryFilter, null, new PropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				object[][] rows = queryResult.GetRows(1);
				if (rows.Length > 0)
				{
					result = (StoreId)rows[0][0];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0005BC64 File Offset: 0x00059E64
		protected bool TryCopyItemBatch(out int itemsCopied)
		{
			Func<AggregateOperationResult> batchCall = () => this.SourceFolderIdAndSession.Session.Copy(this.DestFolderIdAndSession.Session, this.DestFolderIdAndSession.Id, true, this.ItemIds.ToArray());
			return this.TryExecuteBatchCall(batchCall, out itemsCopied);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0005BCBC File Offset: 0x00059EBC
		protected bool TryMoveItemBatch(out int itemsMoved)
		{
			Func<AggregateOperationResult> batchCall = () => this.SourceFolderIdAndSession.Session.Move(this.DestFolderIdAndSession.Session, this.DestFolderIdAndSession.Id, true, this.ItemIds.ToArray());
			return this.TryExecuteBatchCall(batchCall, out itemsMoved);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0005BCE0 File Offset: 0x00059EE0
		private bool TryExecuteBatchCall(Func<AggregateOperationResult> batchCall, out int itemsChanged)
		{
			bool flag = false;
			itemsChanged = 0;
			if (!base.VerifyItemsCanBeBatched(this.ItemIds, this.SourceFolderIdAndSession, this.DestFolderIdAndSession.Session, ref flag))
			{
				this.ServiceResults = null;
				return false;
			}
			StoreSession session = this.SourceFolderIdAndSession.Session;
			Dictionary<StoreId, byte[]> dictionary = new Dictionary<StoreId, byte[]>();
			if (this.ReturnNewItemIds && !this.BuildItemIdToSearchKeyDictionary(dictionary, session, this.ItemIds))
			{
				this.ServiceResults = null;
				return false;
			}
			AggregateOperationResult aggregateOperationResult = batchCall();
			switch (aggregateOperationResult.OperationResult)
			{
			case OperationResult.Succeeded:
				itemsChanged = this.ItemIds.Count;
				return this.PrepareOperationSucceededResults(aggregateOperationResult.GroupOperationResults, dictionary);
			case OperationResult.Failed:
				return this.PrepareMoveCopyFailedServiceResults();
			case OperationResult.PartiallySucceeded:
				return this.PreparePartiallySucceededResults(dictionary, ref itemsChanged);
			default:
				this.ServiceResults = null;
				return false;
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0005BDA8 File Offset: 0x00059FA8
		private bool PrepareOperationSucceededResults(GroupOperationResult[] groupOperationResults, Dictionary<StoreId, byte[]> searchKeyDictionary)
		{
			if (groupOperationResults == null || groupOperationResults.Count<GroupOperationResult>() == 0 || groupOperationResults[0].ResultObjectIds == null || groupOperationResults[0].ResultObjectIds.Count<StoreObjectId>() < this.ItemIds.Count)
			{
				return this.PrepareEmptySuccessfulServiceResults();
			}
			List<StoreId> list = new List<StoreId>();
			for (int i = 0; i < groupOperationResults[0].ResultObjectIds.Count; i++)
			{
				StoreId item = IdConverter.CombineStoreObjectIdWithChangeKey(groupOperationResults[0].ResultObjectIds[i], groupOperationResults[0].ResultChangeKeys[i]);
				list.Add(item);
			}
			return this.PrepareServiceResultForItemIds(list);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0005BE3C File Offset: 0x0005A03C
		private bool PreparePartiallySucceededResults(Dictionary<StoreId, byte[]> itemIdToSearchKeyDictionary, ref int itemsChanged)
		{
			if (!this.ReturnNewItemIds)
			{
				return this.PrepareMoveCopyFailedServiceResults();
			}
			using (Folder folder = Folder.Bind(this.DestFolderIdAndSession.Session, this.DestFolderIdAndSession.Id, null))
			{
				this.ServiceResults = new ServiceResult<ItemType>[this.ItemIds.Count];
				for (int i = 0; i < this.ItemIds.Count; i++)
				{
					StoreId key = this.ItemIds[i];
					byte[] searchKey = itemIdToSearchKeyDictionary[key];
					StoreId storeId = this.FindMovedOrCopiedItemId(folder, searchKey);
					if (storeId != null)
					{
						itemsChanged++;
						this.ServiceResults[i] = this.PrepareServiceResultForItemId(storeId);
					}
					else
					{
						this.ServiceResults[i] = new ServiceResult<ItemType>(this.CreateMoveCopyFailedServiceError());
					}
				}
			}
			return true;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0005BF14 File Offset: 0x0005A114
		private bool PrepareEmptySuccessfulServiceResults()
		{
			this.ServiceResults = this.PrepareBulkServiceResults(this.ItemIds.Count, () => new ServiceResult<ItemType>(null));
			return true;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0005BF60 File Offset: 0x0005A160
		private bool PrepareMoveCopyFailedServiceResults()
		{
			ServiceError serviceError = this.CreateMoveCopyFailedServiceError();
			this.ServiceResults = this.PrepareBulkServiceResults(this.ItemIds.Count, () => new ServiceResult<ItemType>(serviceError));
			return true;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0005BFA4 File Offset: 0x0005A1A4
		private ServiceResult<ItemType>[] PrepareBulkServiceResults(int count, Func<ServiceResult<ItemType>> createServiceResult)
		{
			ServiceResult<ItemType>[] array = new ServiceResult<ItemType>[count];
			for (int i = 0; i < this.ItemIds.Count; i++)
			{
				array[i] = createServiceResult();
			}
			return array;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0005BFD8 File Offset: 0x0005A1D8
		private ServiceError CreateMoveCopyFailedServiceError()
		{
			return new ServiceError((CoreResources.IDs)2524108663U, ResponseCodeType.ErrorMoveCopyFailed, 0, ExchangeVersion.Exchange2007SP1);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0005BFF4 File Offset: 0x0005A1F4
		private bool PrepareServiceResultForItemIds(IList<StoreId> itemIdList)
		{
			this.ServiceResults = new ServiceResult<ItemType>[itemIdList.Count<StoreId>()];
			for (int i = 0; i < itemIdList.Count<StoreId>(); i++)
			{
				this.ServiceResults[i] = this.PrepareServiceResultForItemId(itemIdList[i]);
			}
			return true;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0005C03C File Offset: 0x0005A23C
		private ServiceResult<ItemType> PrepareServiceResultForItemId(StoreId itemId)
		{
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(itemId);
			ItemType itemType = ItemType.CreateFromStoreObjectType(storeObjectId.ObjectType);
			itemType.ItemId = base.GetServiceItemIdFromStoreId(itemId, this.DestFolderIdAndSession);
			return new ServiceResult<ItemType>(itemType);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0005C078 File Offset: 0x0005A278
		private bool BuildItemIdToSearchKeyDictionary(Dictionary<StoreId, byte[]> searchKeyDictionary, StoreSession session, List<StoreId> itemIds)
		{
			foreach (StoreId storeId in itemIds)
			{
				byte[] itemSearchKey = this.GetItemSearchKey(session, storeId);
				if (itemSearchKey == null || searchKeyDictionary.Keys.Contains(storeId))
				{
					return false;
				}
				searchKeyDictionary.Add(storeId, itemSearchKey);
			}
			return true;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0005C0EC File Offset: 0x0005A2EC
		private byte[] GetItemSearchKey(StoreSession session, StoreId itemId)
		{
			byte[] result;
			try
			{
				using (Item item = Item.Bind(session, itemId, new PropertyDefinition[]
				{
					StoreObjectSchema.SearchKey
				}))
				{
					result = (byte[])item.TryGetProperty(StoreObjectSchema.SearchKey);
				}
			}
			catch (ObjectNotFoundException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0005C150 File Offset: 0x0005A350
		internal override void PostExecuteCommand()
		{
			if (this.FallbackCommand != null)
			{
				MoveCopyCommand fallbackCommand = this.FallbackCommand;
				fallbackCommand.PostExecuteCommand();
			}
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0005C17E File Offset: 0x0005A37E
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MoveCopyItemBatchCommandBase<RequestType, MoveCopyCommand>>(this);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0005C186 File Offset: 0x0005A386
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0005C19B File Offset: 0x0005A39B
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0005C1C0 File Offset: 0x0005A3C0
		private void Dispose(bool fromDispose)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (!this.disposed)
			{
				if (this.FallbackCommand != null)
				{
					MoveCopyCommand fallbackCommand = this.FallbackCommand;
					fallbackCommand.Dispose();
					this.FallbackCommand = default(MoveCopyCommand);
				}
				this.disposed = true;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0005C21F File Offset: 0x0005A41F
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0005C227 File Offset: 0x0005A427
		protected MoveCopyCommand FallbackCommand { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0005C230 File Offset: 0x0005A430
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x0005C238 File Offset: 0x0005A438
		protected List<StoreId> ItemIds { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0005C241 File Offset: 0x0005A441
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x0005C249 File Offset: 0x0005A449
		protected IdAndSession SourceFolderIdAndSession { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0005C252 File Offset: 0x0005A452
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x0005C25A File Offset: 0x0005A45A
		protected IdAndSession DestFolderIdAndSession { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0005C263 File Offset: 0x0005A463
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x0005C26B File Offset: 0x0005A46B
		protected bool ReturnNewItemIds { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0005C274 File Offset: 0x0005A474
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x0005C27C File Offset: 0x0005A47C
		protected ServiceResult<ItemType>[] ServiceResults { get; set; }

		// Token: 0x04000D11 RID: 3345
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000D12 RID: 3346
		private bool disposed;
	}
}
