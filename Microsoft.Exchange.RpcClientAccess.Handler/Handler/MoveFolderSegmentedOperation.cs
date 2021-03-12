using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MoveFolderSegmentedOperation : SegmentedRopOperation
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00015088 File Offset: 0x00013288
		internal MoveFolderSegmentedOperation(ReferenceCount<CoreFolder> sourcefolderReferenceCount, ReferenceCount<CoreFolder> destinationFolderReferenceCount, Logon logon, StoreObjectId sourceFolderId, string newFolderName) : base(RopId.MoveFolder)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.sourceRootContainerFolder = sourcefolderReferenceCount;
				this.sourceRootContainerFolder.AddRef();
				this.destinationRootContainerFolder = destinationFolderReferenceCount;
				this.destinationRootContainerFolder.AddRef();
				this.logon = logon;
				this.newFolderName = newFolderName;
				this.sourceRootFolderId = sourceFolderId;
				List<StoreObjectId> list = new List<StoreObjectId>();
				using (CoreFolder coreFolder = CoreFolder.Bind(this.sourceRootContainerFolder.ReferencedObject.Session, sourceFolderId, new PropertyDefinition[]
				{
					FolderSchema.DisplayName
				}))
				{
					base.TotalWork = SegmentedRopOperation.EstimateWork(coreFolder, true, list);
				}
				base.TotalWork = 1;
				base.DetectCopyMoveLoop(destinationFolderReferenceCount, list);
				disposeGuard.Success();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00015240 File Offset: 0x00013440
		protected override SegmentOperationResult InternalDoNextBatchOperation()
		{
			GroupOperationResult result = null;
			if (base.SafeSegmentExecution(delegate()
			{
				string folderName = (this.newFolderName == string.Empty) ? null : this.newFolderName;
				PublicLogon publicLogon = this.logon as PublicLogon;
				if (publicLogon != null && !publicLogon.IsPrimaryHierarchyLogon)
				{
					result = PublicFolderOperations.MoveFolder(publicLogon, this.sourceRootContainerFolder.ReferencedObject.Id, this.destinationRootContainerFolder.ReferencedObject.Id, this.sourceRootFolderId, folderName);
					return;
				}
				result = this.sourceRootContainerFolder.ReferencedObject.MoveFolder(this.destinationRootContainerFolder.ReferencedObject, this.sourceRootFolderId, folderName);
			}))
			{
				return new SegmentOperationResult
				{
					CompletedWork = 1,
					Exception = result.Exception,
					IsCompleted = true,
					OperationResult = result.OperationResult
				};
			}
			return new SegmentOperationResult
			{
				CompletedWork = 0,
				Exception = null,
				IsCompleted = true,
				OperationResult = OperationResult.Failed
			};
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000152DC File Offset: 0x000134DC
		internal override RopResult CreateCompleteResult(object progressToken, IProgressResultFactory resultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return ((MoveFolderResultFactory)resultFactory).CreateSuccessfulResult(base.IsPartiallyCompleted);
			}
			return ((MoveFolderResultFactory)resultFactory).CreateFailedResult(base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0001530F File Offset: 0x0001350F
		internal override RopResult CreateCompleteResultForProgress(object progressToken, ProgressResultFactory progressResultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return progressResultFactory.CreateSuccessfulMoveFolderResult(progressToken, base.IsPartiallyCompleted);
			}
			return progressResultFactory.CreateFailedMoveFolderResult(progressToken, base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0001533A File Offset: 0x0001353A
		protected override void InternalDispose()
		{
			this.sourceRootContainerFolder.Release();
			this.destinationRootContainerFolder.Release();
			base.InternalDispose();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0001535A File Offset: 0x0001355A
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MoveFolderSegmentedOperation>(this);
		}

		// Token: 0x040000D3 RID: 211
		private readonly ReferenceCount<CoreFolder> sourceRootContainerFolder;

		// Token: 0x040000D4 RID: 212
		private readonly ReferenceCount<CoreFolder> destinationRootContainerFolder;

		// Token: 0x040000D5 RID: 213
		private readonly Logon logon;

		// Token: 0x040000D6 RID: 214
		private readonly StoreObjectId sourceRootFolderId;

		// Token: 0x040000D7 RID: 215
		private string newFolderName;
	}
}
