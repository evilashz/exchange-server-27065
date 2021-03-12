using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class EmptyFolderSegmentedOperation : SegmentedRopOperation
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00013AE8 File Offset: 0x00011CE8
		protected EmptyFolderSegmentedOperation(ReferenceCount<CoreFolder> folder, EmptyFolderFlags emptyFolderFlags) : base(((emptyFolderFlags & EmptyFolderFlags.HardDelete) == EmptyFolderFlags.HardDelete) ? RopId.HardEmptyFolder : RopId.EmptyFolder)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.folder = folder;
				this.folder.AddRef();
				this.emptyFolderFlags = emptyFolderFlags;
				this.deleteAssociated = ((emptyFolderFlags & EmptyFolderFlags.DeleteAssociatedMessages) == EmptyFolderFlags.DeleteAssociatedMessages);
				this.subFolderIds = new List<StoreObjectId>();
				this.subFolderIds.Add(this.folder.ReferencedObject.Id.ObjectId);
				base.TotalWork = SegmentedRopOperation.EstimateWork(this.folder.ReferencedObject, this.deleteAssociated, this.subFolderIds);
				this.subFolderIds.Reverse();
				this.emptyFolderEnumerator = this.InternalEmptyFolder().GetEnumerator();
				disposeGuard.Success();
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00013BC8 File Offset: 0x00011DC8
		protected override SegmentOperationResult InternalDoNextBatchOperation()
		{
			if (this.emptyFolderEnumerator.MoveNext())
			{
				return this.emptyFolderEnumerator.Current;
			}
			return SegmentedRopOperation.FinalResult;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000141C8 File Offset: 0x000123C8
		private IEnumerable<SegmentOperationResult> InternalEmptyFolder()
		{
			using (List<StoreObjectId>.Enumerator enumerator = this.subFolderIds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EmptyFolderSegmentedOperation.<>c__DisplayClass5 CS$<>8__locals2 = new EmptyFolderSegmentedOperation.<>c__DisplayClass5();
					CS$<>8__locals2.subFolderId = enumerator.Current;
					TestInterceptor.Intercept(TestInterceptorLocation.EmptyFolderSegmentedOperation_CoreFolderBind, new object[]
					{
						this.folder.ReferencedObject.Session,
						CS$<>8__locals2.subFolderId
					});
					CoreFolder subFolder = null;
					if (base.SafeSegmentExecution(delegate()
					{
						subFolder = CoreFolder.Bind(this.folder.ReferencedObject.Session, CS$<>8__locals2.subFolderId);
					}))
					{
						using (subFolder)
						{
							if (!RopHandler.IsSearchFolder(subFolder.Id))
							{
								goto IL_168;
							}
						}
						continue;
						IL_168:
						foreach (SegmentOperationResult segmentOperationResult in this.DeleteContents(subFolder))
						{
							yield return segmentOperationResult;
						}
					}
					else
					{
						yield return SegmentedRopOperation.FailedSegmentResult;
					}
				}
			}
			foreach (SegmentOperationResult segmentOperationResult2 in this.DeleteContents(this.folder.ReferencedObject))
			{
				yield return segmentOperationResult2;
			}
			GroupOperationResult emptyFolderOperationResult = null;
			if (base.SafeSegmentExecution(delegate()
			{
				TestInterceptor.Intercept(TestInterceptorLocation.EmptyFolderSegmentedOperation_EmptyFolderHierarchy, new object[0]);
				emptyFolderOperationResult = this.folder.ReferencedObject.EmptyFolder(false, this.emptyFolderFlags);
			}))
			{
				yield return new SegmentOperationResult
				{
					CompletedWork = this.subFolderIds.Count,
					OperationResult = emptyFolderOperationResult.OperationResult,
					Exception = emptyFolderOperationResult.Exception,
					IsCompleted = true
				};
			}
			else
			{
				yield return SegmentedRopOperation.FailedSegmentResult;
			}
			yield break;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00014460 File Offset: 0x00012660
		private IEnumerable<SegmentOperationResult> DeleteContents(CoreFolder subFolder)
		{
			foreach (SegmentOperationResult deletedMessages in this.DeleteMessages(subFolder, ItemQueryType.None))
			{
				yield return deletedMessages;
			}
			if (this.deleteAssociated)
			{
				foreach (SegmentOperationResult deletedMessages2 in this.DeleteMessages(subFolder, ItemQueryType.Associated))
				{
					yield return deletedMessages2;
				}
			}
			yield break;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00014930 File Offset: 0x00012B30
		private IEnumerable<SegmentOperationResult> DeleteMessages(CoreFolder coreFolder, ItemQueryType itemQueryType)
		{
			DeleteItemFlags deleteItemFlags = ((this.emptyFolderFlags & EmptyFolderFlags.HardDelete) == EmptyFolderFlags.HardDelete) ? DeleteItemFlags.HardDelete : DeleteItemFlags.SoftDelete;
			using (QuerySegmentEnumerator deleteMessages = new QuerySegmentEnumerator(coreFolder, itemQueryType, SegmentEnumerator.MessageSegmentSize))
			{
				TestInterceptor.Intercept(TestInterceptorLocation.EmptyFolderSegmentedOperation_CoreFolderQuery, new object[]
				{
					coreFolder
				});
				StoreObjectId[] messageIds = null;
				if (!base.SafeSegmentExecution(delegate()
				{
					messageIds = deleteMessages.GetNextBatchIds();
				}))
				{
					yield return SegmentedRopOperation.FailedSegmentResult;
					yield break;
				}
				while (messageIds.Length > 0)
				{
					TestInterceptor.Intercept(TestInterceptorLocation.EmptyFolderSegmentedOperation_CoreFolderDeleteMessages, new object[]
					{
						coreFolder
					});
					GroupOperationResult groupOperationResult = null;
					if (!base.SafeSegmentExecution(delegate()
					{
						groupOperationResult = coreFolder.DeleteItems(deleteItemFlags, messageIds);
					}))
					{
						yield return SegmentedRopOperation.FailedSegmentResult;
						break;
					}
					yield return new SegmentOperationResult
					{
						OperationResult = groupOperationResult.OperationResult,
						CompletedWork = messageIds.Length,
						Exception = groupOperationResult.Exception,
						IsCompleted = false
					};
					if (!base.SafeSegmentExecution(delegate()
					{
						messageIds = deleteMessages.GetNextBatchIds();
					}))
					{
						yield return SegmentedRopOperation.FailedSegmentResult;
						break;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0001495B File Offset: 0x00012B5B
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.emptyFolderEnumerator);
			if (this.folder != null)
			{
				this.folder.Release();
			}
			base.InternalDispose();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00014982 File Offset: 0x00012B82
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmptyFolderSegmentedOperation>(this);
		}

		// Token: 0x040000BE RID: 190
		private readonly ReferenceCount<CoreFolder> folder;

		// Token: 0x040000BF RID: 191
		private readonly EmptyFolderFlags emptyFolderFlags;

		// Token: 0x040000C0 RID: 192
		private readonly bool deleteAssociated;

		// Token: 0x040000C1 RID: 193
		private readonly IEnumerator<SegmentOperationResult> emptyFolderEnumerator;

		// Token: 0x040000C2 RID: 194
		private readonly List<StoreObjectId> subFolderIds;
	}
}
