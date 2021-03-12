using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002D5 RID: 725
	internal sealed class EmptyFolder : MultiStepServiceCommand<EmptyFolderRequest, ServiceResultNone>
	{
		// Token: 0x06001416 RID: 5142 RVA: 0x000647EC File Offset: 0x000629EC
		public EmptyFolder(CallContext callContext, EmptyFolderRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x000647F8 File Offset: 0x000629F8
		internal override void PreExecuteCommand()
		{
			this.folderIds = base.Request.FolderIds;
			this.disposalType = base.Request.DeleteType;
			this.deleteSubFolders = base.Request.DeleteSubFolders;
			this.allowSearchFolder = base.Request.AllowSearchFolder;
			this.lastSyncTime = ServiceCommandBase.GetUtcDateTime(ServiceCommandBase.ParseExDateTimeString(base.Request.ClientLastSyncTime));
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseFolderId>(this.folderIds, "folderIds", "EmptyFolder::PreExecuteCommand");
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0006487C File Offset: 0x00062A7C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			EmptyFolderResponse emptyFolderResponse = new EmptyFolderResponse();
			emptyFolderResponse.BuildForNoReturnValue(base.Results);
			return emptyFolderResponse;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0006489C File Offset: 0x00062A9C
		internal override int StepCount
		{
			get
			{
				return this.folderIds.Length;
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x000648A6 File Offset: 0x00062AA6
		private Folder GetStoreObject(IdAndSession idAndSession)
		{
			return Folder.Bind(idAndSession.Session, idAndSession.Id, null);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x000648BA File Offset: 0x00062ABA
		private IdAndSession GetIdAndSession(BaseFolderId folderId)
		{
			return base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(folderId);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x000648C8 File Offset: 0x00062AC8
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession idAndSession = this.GetIdAndSession(this.folderIds[base.CurrentStep]);
			this.Empty(idAndSession);
			this.objectsChanged++;
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00064908 File Offset: 0x00062B08
		private void Empty(IdAndSession idAndSession)
		{
			ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<StoreId, DisposalType>(0L, "Empty called for storeObjectId '{0}' using disposalType '{1}'", idAndSession.Id, this.disposalType);
			DeleteItemFlags deleteItemFlags = (DeleteItemFlags)(this.disposalType | (DisposalType)65536);
			if (this.allowSearchFolder && this.IsClutterViewFolder(idAndSession))
			{
				deleteItemFlags |= DeleteItemFlags.DeleteAllClutter;
			}
			if (base.Request.SuppressReadReceipt)
			{
				deleteItemFlags |= DeleteItemFlags.SuppressReadReceipt;
			}
			LocalizedException innerException = null;
			using (Folder storeObject = this.GetStoreObject(idAndSession))
			{
				this.ValidateOperation(storeObject);
				if (!this.TryExecuteEmptyFolder(storeObject, deleteItemFlags, idAndSession, out innerException))
				{
					throw new EmptyFolderException(innerException);
				}
			}
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x000649B0 File Offset: 0x00062BB0
		private bool TryExecuteEmptyFolder(Folder folder, DeleteItemFlags deleteItemFlags, IdAndSession idAndSession, out LocalizedException localizedException)
		{
			bool flag = false;
			try
			{
				AggregateOperationResult aggregateOperationResult = this.ExecuteEmptyFolder(folder, deleteItemFlags, idAndSession);
				flag = (aggregateOperationResult.OperationResult == OperationResult.Succeeded);
				localizedException = (flag ? null : aggregateOperationResult.GroupOperationResults[0].Exception);
				ExTraceGlobals.DeleteItemCallTracer.TraceDebug<OperationResult>(0L, "EmptyFolder item operation result '{0}'", aggregateOperationResult.OperationResult);
			}
			catch (DumpsterOperationException ex)
			{
				localizedException = ex;
			}
			if (localizedException != null)
			{
				ExTraceGlobals.DeleteItemCallTracer.TraceError<LocalizedException, string>(0L, "Exception while emptying folder '{0}', StackTrace '{1}'", localizedException, localizedException.StackTrace);
			}
			return flag;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00064A3C File Offset: 0x00062C3C
		private AggregateOperationResult ExecuteEmptyFolder(Folder folder, DeleteItemFlags deleteItemFlags, IdAndSession idAndSession)
		{
			if (!this.deleteSubFolders)
			{
				return folder.DeleteAllItems(deleteItemFlags, this.lastSyncTime);
			}
			if ((deleteItemFlags & DeleteItemFlags.MoveToDeletedItems) == DeleteItemFlags.MoveToDeletedItems)
			{
				AggregateOperationResult aggregateOperationResult = folder.DeleteAllItems(deleteItemFlags);
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
				{
					FolderSchema.Id
				}))
				{
					object[][] rows;
					do
					{
						rows = queryResult.GetRows(100);
						if (rows.Length != 0)
						{
							StoreObjectId[] array = new StoreObjectId[rows.Length];
							for (int i = 0; i < rows.Length; i++)
							{
								if (rows[i][0] is StoreId)
								{
									array[i] = StoreId.GetStoreObjectId((StoreId)rows[i][0]);
								}
							}
							aggregateOperationResult = this.MergeAggregateOperationResults(aggregateOperationResult, folder.DeleteObjects(deleteItemFlags, array));
						}
					}
					while (rows.Length != 0);
				}
				return aggregateOperationResult;
			}
			GroupOperationResult groupOperationResult = folder.DeleteAllObjects(deleteItemFlags);
			return new AggregateOperationResult(groupOperationResult.OperationResult, new GroupOperationResult[]
			{
				groupOperationResult
			});
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00064B34 File Offset: 0x00062D34
		private void ValidateOperation(Folder folder)
		{
			if (folder.Session is PublicFolderSession && (this.disposalType == DisposalType.MoveToDeletedItems || this.deleteSubFolders))
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCannotEmptyPublicFolderToDeletedItems);
			}
			this.ExecuteCommandValidations(folder);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00064B6C File Offset: 0x00062D6C
		private void ExecuteCommandValidations(Folder folder)
		{
			if (folder is CalendarFolder || (folder is SearchFolder && !this.allowSearchFolder))
			{
				throw new EmptyFolderException((CoreResources.IDs)3080652515U);
			}
			if (this.deleteSubFolders && folder.Session is MailboxSession)
			{
				MailboxSession mailboxSession = (MailboxSession)folder.Session;
				if (folder.Id.ObjectId.Equals(mailboxSession.GetDefaultFolderId(DefaultFolderType.Root)) || folder.Id.ObjectId.Equals(mailboxSession.GetDefaultFolderId(DefaultFolderType.Configuration)))
				{
					throw new EmptyFolderException(CoreResources.IDs.ErrorCannotDeleteSubfoldersOfMsgRootFolder);
				}
			}
			if (this.lastSyncTime != null && this.deleteSubFolders)
			{
				throw new EmptyFolderException((CoreResources.IDs)2838198776U);
			}
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00064C2C File Offset: 0x00062E2C
		private AggregateOperationResult MergeAggregateOperationResults(AggregateOperationResult aggregateResult, AggregateOperationResult aggregateResult2)
		{
			OperationResult operationResult = aggregateResult.OperationResult;
			if (aggregateResult2.OperationResult != operationResult)
			{
				operationResult = OperationResult.PartiallySucceeded;
			}
			GroupOperationResult[] array = new GroupOperationResult[aggregateResult.GroupOperationResults.Length + aggregateResult2.GroupOperationResults.Length];
			aggregateResult.GroupOperationResults.CopyTo(array, 0);
			aggregateResult2.GroupOperationResults.CopyTo(array, aggregateResult.GroupOperationResults.Length);
			return new AggregateOperationResult(operationResult, array);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00064C8C File Offset: 0x00062E8C
		private bool IsClutterViewFolder(IdAndSession idAndSession)
		{
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(idAndSession.Id);
			if (asStoreObjectId == null || asStoreObjectId.ObjectType != StoreObjectType.SearchFolder)
			{
				throw new EmptyFolderException(CoreResources.IDs.ErrorInvalidFolderId);
			}
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession != null)
			{
				OwaFilterState owaFilterStateForExistingFolder = OwaFilterState.GetOwaFilterStateForExistingFolder(mailboxSession, asStoreObjectId);
				if (owaFilterStateForExistingFolder != null)
				{
					return owaFilterStateForExistingFolder.ViewFilter == OwaViewFilter.Clutter;
				}
			}
			return false;
		}

		// Token: 0x04000D84 RID: 3460
		private BaseFolderId[] folderIds;

		// Token: 0x04000D85 RID: 3461
		private DisposalType disposalType;

		// Token: 0x04000D86 RID: 3462
		private bool deleteSubFolders;

		// Token: 0x04000D87 RID: 3463
		private bool allowSearchFolder;

		// Token: 0x04000D88 RID: 3464
		private DateTime? lastSyncTime;
	}
}
