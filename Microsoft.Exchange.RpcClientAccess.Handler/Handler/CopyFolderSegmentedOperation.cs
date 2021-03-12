using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000027 RID: 39
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CopyFolderSegmentedOperation : SegmentedRopOperation
	{
		// Token: 0x0600020A RID: 522 RVA: 0x000120E8 File Offset: 0x000102E8
		internal CopyFolderSegmentedOperation(ReferenceCount<CoreFolder> sourcefolderReferenceCount, ReferenceCount<CoreFolder> destinationFolderReferenceCount, StoreObjectId sourceFolderId, string newFolderName, bool isRecursive) : base(RopId.CopyFolder)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.sourceRootContainerFolder = sourcefolderReferenceCount;
				this.sourceRootContainerFolder.AddRef();
				this.destinationRootContainerFolder = destinationFolderReferenceCount;
				this.destinationRootContainerFolder.AddRef();
				this.newFolderName = newFolderName;
				this.isRecursive = isRecursive;
				this.sourceRootFolder = CoreFolder.Bind(this.sourceRootContainerFolder.ReferencedObject.Session, sourceFolderId, new PropertyDefinition[]
				{
					FolderSchema.DisplayName
				});
				List<StoreObjectId> list = new List<StoreObjectId>();
				base.TotalWork = SegmentedRopOperation.EstimateWork(this.sourceRootFolder, true, list);
				if (base.RopId == RopId.CopyFolder)
				{
					base.TotalWork -= list.Count;
				}
				base.DetectCopyMoveLoop(destinationFolderReferenceCount, list);
				this.copyFolderEnumerator = this.InternalCopyFolder(this.sourceRootContainerFolder.ReferencedObject, this.destinationRootContainerFolder.ReferencedObject, sourceFolderId, true).GetEnumerator();
				disposeGuard.Success();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000121F4 File Offset: 0x000103F4
		private static CoreFolder CreateDuplicateFolder(CoreFolder sourceFolder, CoreFolder destinationContainerFolder, string folderName)
		{
			CoreFolder result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				TestInterceptor.Intercept(TestInterceptorLocation.CopyFolderSegmentedOperation_CoreFolderDeletedCreatingNewSubFolder, new object[]
				{
					destinationContainerFolder,
					destinationContainerFolder.Id
				});
				if (folderName == string.Empty)
				{
					folderName = (string)sourceFolder.PropertyBag[FolderSchema.DisplayName];
				}
				CoreFolder coreFolder = CoreFolder.Create(destinationContainerFolder.Session, destinationContainerFolder.Id, false, folderName, CreateMode.CreateNew);
				disposeGuard.Add<CoreFolder>(coreFolder);
				FolderSaveResult folderSaveResult = coreFolder.Save(SaveMode.FailOnAnyConflict);
				if (folderSaveResult.OperationResult != OperationResult.Succeeded)
				{
					throw folderSaveResult.ToException(new LocalizedString("Cannot save the folder of displayName " + folderName));
				}
				coreFolder.PropertyBag.Load(null);
				PropertyError[] array = sourceFolder.CopyFolder(coreFolder, CopyPropertiesFlags.None, CopySubObjects.DoNotCopy, new NativeStorePropertyDefinition[]
				{
					(NativeStorePropertyDefinition)FolderSchema.DisplayName
				});
				if (array.Length > 0)
				{
					throw new RopExecutionException(new LocalizedString(string.Format("Cannot copy folder properties. Errors = {0:!E}", array)), (ErrorCode)2147500037U);
				}
				coreFolder.PropertyBag.Load(null);
				disposeGuard.Success();
				result = coreFolder;
			}
			return result;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00012324 File Offset: 0x00010524
		[Conditional("DEBUG")]
		private static void DebugCheckFolderType(CoreFolder folder, bool expectSearchFolder)
		{
			VersionedId versionedId = (VersionedId)folder.PropertyBag[FolderSchema.Id];
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0001233C File Offset: 0x0001053C
		protected override SegmentOperationResult InternalDoNextBatchOperation()
		{
			if (this.copyFolderEnumerator.MoveNext())
			{
				return this.copyFolderEnumerator.Current;
			}
			return SegmentedRopOperation.FinalResult;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00012AB4 File Offset: 0x00010CB4
		private IEnumerable<SegmentOperationResult> InternalCopyFolder(CoreFolder sourceContainerFolder, CoreFolder destinationContainerFolder, StoreObjectId sourceFolderId, bool isTopLevel)
		{
			CoreFolder sourceFolder = null;
			bool processedSearchFolder = false;
			GroupOperationResult result = null;
			TestInterceptor.Intercept(TestInterceptorLocation.CopyFolderSegmentedOperation_CoreFolderDeletedAboutToCreateNewSubFolder, new object[]
			{
				sourceContainerFolder,
				sourceFolderId
			});
			if (!base.SafeSegmentExecution(delegate()
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					if (isTopLevel)
					{
						sourceFolder = this.sourceRootFolder;
						this.sourceRootFolder = null;
					}
					else
					{
						sourceFolder = CoreFolder.Bind(sourceContainerFolder.Session, sourceFolderId);
					}
					disposeGuard.Add<CoreFolder>(sourceFolder);
					if (sourceFolder.Id.ObjectId.ObjectType == StoreObjectType.SearchFolder || sourceFolder.Id.ObjectId.ObjectType == StoreObjectType.OutlookSearchFolder)
					{
						processedSearchFolder = true;
						result = this.ProcessSearchFolder(sourceFolder, destinationContainerFolder, this.isRecursive ? CopySubObjects.Copy : CopySubObjects.DoNotCopy, sourceFolderId);
					}
					disposeGuard.Success();
				}
			}))
			{
				yield return SegmentedRopOperation.FailedSegmentResult;
			}
			else
			{
				using (sourceFolder)
				{
					if (processedSearchFolder)
					{
						yield return new SegmentOperationResult
						{
							IsCompleted = false,
							CompletedWork = 1,
							OperationResult = result.OperationResult,
							Exception = result.Exception
						};
						yield break;
					}
					CoreFolder destinationFolder = null;
					if (!base.SafeSegmentExecution(delegate()
					{
						destinationFolder = CopyFolderSegmentedOperation.CreateDuplicateFolder(sourceFolder, destinationContainerFolder, isTopLevel ? this.newFolderName : string.Empty);
					}))
					{
						yield return SegmentedRopOperation.FailedSegmentResult;
						yield break;
					}
					using (destinationFolder)
					{
						foreach (SegmentOperationResult copyMessages in this.CopyContents(sourceFolder, destinationFolder))
						{
							yield return copyMessages;
						}
						if (!this.isRecursive)
						{
							yield break;
						}
						TestInterceptor.Intercept(TestInterceptorLocation.CopyFolderSegmentedOperation_CoreFolderDeletedAboutToQueryingSubFolders, new object[]
						{
							sourceFolder,
							sourceFolder.Id
						});
						foreach (SegmentOperationResult copySubfolders in this.CopySubfolders(sourceFolder, destinationFolder))
						{
							yield return copySubfolders;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00012D74 File Offset: 0x00010F74
		private IEnumerable<SegmentOperationResult> CopyContents(CoreFolder sourceFolder, CoreFolder destinationFolder)
		{
			foreach (SegmentOperationResult copyMessages in this.CopyMessages(sourceFolder, destinationFolder, ItemQueryType.None))
			{
				yield return copyMessages;
			}
			foreach (SegmentOperationResult copyMessages2 in this.CopyMessages(sourceFolder, destinationFolder, ItemQueryType.Associated))
			{
				yield return copyMessages2;
			}
			yield break;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00013230 File Offset: 0x00011430
		private IEnumerable<SegmentOperationResult> CopySubfolders(CoreFolder sourceFolder, CoreFolder destinationFolder)
		{
			CopyFolderSegmentedOperation.<>c__DisplayClass23 CS$<>8__locals1 = new CopyFolderSegmentedOperation.<>c__DisplayClass23();
			CS$<>8__locals1.sourceFolder = sourceFolder;
			CS$<>8__locals1.sourcefolderQuery = null;
			if (!base.SafeSegmentExecution(delegate()
			{
				CS$<>8__locals1.sourcefolderQuery = new QuerySegmentEnumerator(CS$<>8__locals1.sourceFolder, FolderQueryFlags.None, 10);
			}))
			{
				yield return SegmentedRopOperation.FailedSegmentResult;
				yield break;
			}
			QuerySegmentEnumerator sourcefolderQuery = CS$<>8__locals1.sourcefolderQuery;
			StoreObjectId[] sourceSubfolderIds = null;
			for (;;)
			{
				TestInterceptor.Intercept(TestInterceptorLocation.CopyFolderSegmentedOperation_CoreFolderDeletedAboutToPeruseSubFolders, new object[]
				{
					CS$<>8__locals1.sourceFolder,
					CS$<>8__locals1.sourceFolder.Id
				});
				if (!base.SafeSegmentExecution(delegate()
				{
					sourceSubfolderIds = CS$<>8__locals1.sourcefolderQuery.GetNextBatchIds();
				}))
				{
					break;
				}
				if (sourceSubfolderIds.Length == 0)
				{
					goto Block_4;
				}
				foreach (StoreObjectId sourceSubfolderId in sourceSubfolderIds)
				{
					TestInterceptor.Intercept(TestInterceptorLocation.CopyFolderSegmentedOperation_CoreFolderDeletedAboutToCopySubFolder, new object[]
					{
						CS$<>8__locals1.sourceFolder,
						CS$<>8__locals1.sourceFolder.Id
					});
					foreach (SegmentOperationResult copySubfolderResult in this.InternalCopyFolder(CS$<>8__locals1.sourceFolder, destinationFolder, sourceSubfolderId, false))
					{
						yield return copySubfolderResult;
					}
				}
			}
			yield return SegmentedRopOperation.FailedSegmentResult;
			yield break;
			Block_4:
			yield break;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000136B8 File Offset: 0x000118B8
		private IEnumerable<SegmentOperationResult> CopyMessages(CoreFolder sourceFolder, CoreFolder destinationFolder, ItemQueryType itemQueryType)
		{
			using (QuerySegmentEnumerator copyMessages = new QuerySegmentEnumerator(sourceFolder, itemQueryType, SegmentEnumerator.MessageSegmentSize))
			{
				TestInterceptor.Intercept(TestInterceptorLocation.CopyFolderSegmentedOperation_CoreFolderDeletedWhenAboutToCopyMessages, new object[]
				{
					sourceFolder,
					sourceFolder.Id
				});
				for (;;)
				{
					StoreObjectId[] messageIds = null;
					if (!base.SafeSegmentExecution(delegate()
					{
						messageIds = copyMessages.GetNextBatchIds();
					}))
					{
						yield return SegmentedRopOperation.FailedSegmentResult;
					}
					if (messageIds == null || messageIds.Length <= 0)
					{
						break;
					}
					GroupOperationResult groupOperationResult = null;
					if (!base.SafeSegmentExecution(delegate()
					{
						TestInterceptor.Intercept(TestInterceptorLocation.CopyFolderSegmentedOperation_CoreFolderDeletedWhenDoingCopyMessages, new object[]
						{
							destinationFolder,
							destinationFolder.Id
						});
						groupOperationResult = sourceFolder.CopyItems(destinationFolder, messageIds, null, null, null);
					}))
					{
						yield return SegmentedRopOperation.FailedSegmentResult;
					}
					else
					{
						yield return new SegmentOperationResult
						{
							OperationResult = groupOperationResult.OperationResult,
							Exception = groupOperationResult.Exception,
							CompletedWork = messageIds.Length,
							IsCompleted = false
						};
					}
					TestInterceptor.Intercept(TestInterceptorLocation.CopyFolderSegmentedOperation_CoreFolderDeletedWhenDoingNextCopyMessages, new object[]
					{
						sourceFolder,
						sourceFolder.Id
					});
				}
			}
			yield break;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000136EA File Offset: 0x000118EA
		private GroupOperationResult ProcessSearchFolder(CoreFolder sourceFolder, CoreFolder destinationFolder, CopySubObjects copySubObjects, StoreObjectId sourceFolderId)
		{
			return sourceFolder.CopyFolder(destinationFolder, copySubObjects, sourceFolderId);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000136F6 File Offset: 0x000118F6
		internal override RopResult CreateCompleteResult(object progressToken, IProgressResultFactory resultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return ((CopyFolderResultFactory)resultFactory).CreateSuccessfulResult(base.IsPartiallyCompleted);
			}
			return ((CopyFolderResultFactory)resultFactory).CreateFailedResult(base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00013729 File Offset: 0x00011929
		internal override RopResult CreateCompleteResultForProgress(object progressToken, ProgressResultFactory progressResultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return progressResultFactory.CreateSuccessfulCopyFolderResult(progressToken, base.IsPartiallyCompleted);
			}
			return progressResultFactory.CreateFailedCopyFolderResult(progressToken, base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00013754 File Offset: 0x00011954
		protected override void InternalDispose()
		{
			if (this.sourceRootContainerFolder != null)
			{
				this.sourceRootContainerFolder.Release();
			}
			if (this.destinationRootContainerFolder != null)
			{
				this.destinationRootContainerFolder.Release();
			}
			Util.DisposeIfPresent(this.sourceRootFolder);
			Util.DisposeIfPresent(this.copyFolderEnumerator);
			base.InternalDispose();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000137A5 File Offset: 0x000119A5
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CopyFolderSegmentedOperation>(this);
		}

		// Token: 0x040000B4 RID: 180
		private readonly ReferenceCount<CoreFolder> sourceRootContainerFolder;

		// Token: 0x040000B5 RID: 181
		private readonly ReferenceCount<CoreFolder> destinationRootContainerFolder;

		// Token: 0x040000B6 RID: 182
		private readonly bool isRecursive;

		// Token: 0x040000B7 RID: 183
		private readonly string newFolderName;

		// Token: 0x040000B8 RID: 184
		private readonly IEnumerator<SegmentOperationResult> copyFolderEnumerator;

		// Token: 0x040000B9 RID: 185
		private CoreFolder sourceRootFolder;
	}
}
