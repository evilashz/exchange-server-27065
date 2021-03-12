using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200005C RID: 92
	internal abstract class HierarchyBulkOperation : BulkOperation
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00010C34 File Offset: 0x0000EE34
		public HierarchyBulkOperation(MapiFolder folder, bool processAssociatedDumpsterFolders, int chunkSize) : base(chunkSize)
		{
			this.principalFolder = folder;
			this.processAssociatedDumpsterFolders = processAssociatedDumpsterFolders;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00010C4B File Offset: 0x0000EE4B
		protected MapiFolder PrincipalFolder
		{
			get
			{
				return this.principalFolder;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00010C54 File Offset: 0x0000EE54
		public override bool DoChunk(MapiContext context, out bool progress, out bool incomplete, out ErrorCode error)
		{
			progress = false;
			incomplete = false;
			error = ErrorCode.NoError;
			if (this.stack == null)
			{
				this.stack = new Stack<HierarchyBulkOperation.FolderStackEntry>();
				if (!this.principalFolder.CheckAlive(context))
				{
					error = ErrorCode.CreateObjectDeleted((LID)33304U);
					return true;
				}
				this.stack.Push(new HierarchyBulkOperation.FolderStackEntry
				{
					FolderInfo = null,
					IsPrincipal = true,
					IsAssociatedDumpster = false,
					Expanded = false,
					ParentFolder = null,
					DestinationParentFolder = null
				});
				if (this.processAssociatedDumpsterFolders)
				{
					ExchangeId associatedFolderId = this.principalFolder.GetAssociatedFolderId(context);
					if (associatedFolderId != ExchangeId.Null)
					{
						this.stack.Push(new HierarchyBulkOperation.FolderStackEntry
						{
							FolderInfo = null,
							IsPrincipal = true,
							IsAssociatedDumpster = true,
							Expanded = false,
							ParentFolder = null,
							DestinationParentFolder = null
						});
					}
				}
			}
			this.chunkCount = 0;
			bool flag = false;
			while (this.chunkCount < base.ChunkSize && !flag)
			{
				if (this.stack.Count == 0)
				{
					flag = true;
					break;
				}
				HierarchyBulkOperation.FolderStackEntry folderStackEntry = this.stack.Pop();
				this.entryToDispose = folderStackEntry;
				if (!folderStackEntry.Expanded)
				{
					bool flag2;
					int num;
					if (!this.InitializeForFolder(context, folderStackEntry, out flag2, out num, ref incomplete, ref error))
					{
						flag = true;
						break;
					}
					if (num != 0)
					{
						this.chunkCount += num;
						progress = true;
					}
					if (!flag2)
					{
						folderStackEntry.Expanded = true;
						this.stack.Push(folderStackEntry);
						this.entryToDispose = null;
						if (folderStackEntry.ProcessSubfolders)
						{
							FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, this.PrincipalFolder.Logon.StoreMailbox, this.PrincipalFolder.Fid.ToExchangeShortId(), FolderInformationType.Basic);
							if (folderStackEntry.FolderInfo == null)
							{
								folderStackEntry.FolderInfo = folderHierarchy.Find(context, this.PrincipalFolder.Fid.ToExchangeShortId());
							}
							IList<IFolderInformation> children = folderHierarchy.GetChildren(context, folderStackEntry.FolderInfo);
							for (int i = children.Count - 1; i >= 0; i--)
							{
								this.stack.Push(new HierarchyBulkOperation.FolderStackEntry
								{
									FolderInfo = children[i],
									IsPrincipal = false,
									IsAssociatedDumpster = false,
									Expanded = false,
									ParentFolder = folderStackEntry.Folder,
									DestinationParentFolder = folderStackEntry.DestinationFolder
								});
								if (this.processAssociatedDumpsterFolders)
								{
									this.stack.Push(new HierarchyBulkOperation.FolderStackEntry
									{
										FolderInfo = null,
										IsPrincipal = false,
										IsAssociatedDumpster = true,
										Expanded = false,
										ParentFolder = null,
										DestinationParentFolder = null
									});
								}
							}
						}
					}
					else
					{
						folderStackEntry.Dispose();
						this.entryToDispose = null;
					}
				}
				else
				{
					if (!this.CheckDestinationFolder(context, folderStackEntry))
					{
						error = ErrorCode.CreateObjectDeleted((LID)49688U);
						flag = true;
						break;
					}
					if (this.CheckSourceFolder(context, folderStackEntry))
					{
						int num;
						if (!this.ContinueForFolder(context, folderStackEntry, out num, ref incomplete, ref error))
						{
							flag = true;
							break;
						}
						if (num != 0)
						{
							this.chunkCount += num;
							progress = true;
							this.stack.Push(folderStackEntry);
							this.entryToDispose = null;
							continue;
						}
						if (!this.FinishForFolder(context, folderStackEntry, out num, ref incomplete, ref error))
						{
							flag = true;
							break;
						}
						if (num != 0)
						{
							this.chunkCount += num;
							progress = true;
						}
					}
					else
					{
						incomplete = true;
					}
					folderStackEntry.Dispose();
					this.entryToDispose = null;
				}
			}
			return flag;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00010FF5 File Offset: 0x0000F1F5
		protected virtual bool CheckSourceFolder(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry)
		{
			return currentEntry.Folder.CheckAlive(context);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00011003 File Offset: 0x0000F203
		protected virtual bool CheckDestinationFolder(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry)
		{
			return currentEntry.DestinationFolder == null || currentEntry.DestinationFolder.CheckAlive(context);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0001101C File Offset: 0x0000F21C
		private bool InitializeForFolder(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out bool skipFolder, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			IReplidGuidMap cacheForMailbox = ReplidGuidMap.GetCacheForMailbox(context, context.LockedMailboxState);
			if (currentEntry.IsPrincipal)
			{
				if (currentEntry.IsAssociatedDumpster)
				{
					ExchangeId associatedFolderId = this.PrincipalFolder.GetAssociatedFolderId(context);
					currentEntry.FolderInfo = FolderHierarchy.FolderInformationFromFolderId(associatedFolderId.ToExchangeShortId());
					currentEntry.Folder = MapiFolder.OpenFolder(context, this.PrincipalFolder.Logon, associatedFolderId);
					currentEntry.DisposeFolder = true;
					if (currentEntry.Folder == null)
					{
						progressCount = 0;
						incomplete = true;
						skipFolder = true;
						error = ErrorCode.CreateNotFound((LID)51776U);
						return false;
					}
				}
				else
				{
					currentEntry.Folder = this.PrincipalFolder;
					currentEntry.DisposeFolder = false;
				}
			}
			else if (this.processAssociatedDumpsterFolders)
			{
				if (currentEntry.IsAssociatedDumpster)
				{
					HierarchyBulkOperation.FolderStackEntry folderStackEntry = this.stack.Peek();
					folderStackEntry.Folder = MapiFolder.OpenFolder(context, this.PrincipalFolder.Logon, ExchangeId.CreateFromInternalShortId(context, cacheForMailbox, folderStackEntry.FolderInfo.Fid));
					folderStackEntry.DisposeFolder = true;
					if (folderStackEntry.Folder != null)
					{
						ExchangeId associatedFolderId2 = folderStackEntry.Folder.GetAssociatedFolderId(context);
						if (associatedFolderId2 != ExchangeId.Null)
						{
							currentEntry.FolderInfo = FolderHierarchy.FolderInformationFromFolderId(associatedFolderId2.ToExchangeShortId());
							currentEntry.Folder = MapiFolder.OpenFolder(context, this.PrincipalFolder.Logon, associatedFolderId2);
							currentEntry.DisposeFolder = true;
							if (currentEntry.Folder == null)
							{
								progressCount = 0;
								incomplete = true;
								skipFolder = true;
								error = ErrorCode.CreateNotFound((LID)45632U);
								return false;
							}
						}
					}
					if (currentEntry.Folder == null)
					{
						progressCount = 0;
						incomplete = false;
						skipFolder = true;
						return true;
					}
				}
				else
				{
					if (currentEntry.Folder == null)
					{
						currentEntry.Folder = MapiFolder.OpenFolder(context, this.PrincipalFolder.Logon, ExchangeId.CreateFromInternalShortId(context, cacheForMailbox, currentEntry.FolderInfo.Fid));
						currentEntry.DisposeFolder = true;
					}
					if (currentEntry.Folder == null)
					{
						progressCount = 0;
						incomplete = true;
						skipFolder = true;
						return true;
					}
				}
			}
			else
			{
				currentEntry.Folder = MapiFolder.OpenFolder(context, this.PrincipalFolder.Logon, ExchangeId.CreateFromInternalShortId(context, cacheForMailbox, currentEntry.FolderInfo.Fid));
				currentEntry.DisposeFolder = true;
				if (currentEntry.Folder == null)
				{
					progressCount = 0;
					incomplete = true;
					skipFolder = true;
					return true;
				}
			}
			skipFolder = false;
			return this.ProcessFolderStart(context, currentEntry, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00011260 File Offset: 0x0000F460
		private bool ContinueForFolder(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			progressCount = 0;
			int num = base.ChunkSize - this.chunkCount;
			while (currentEntry.ProcessAssociatedMessages || currentEntry.ProcessNormalMessages || currentEntry.ProcessingMessages)
			{
				if (progressCount >= num)
				{
					return true;
				}
				int numRows = Math.Min(num - progressCount, (base.ChunkSize + 1) / 2);
				if (currentEntry.MessageView == null)
				{
					ViewMessageConfigureFlags viewMessageConfigureFlags = ViewMessageConfigureFlags.NoNotifications | ViewMessageConfigureFlags.DoNotUseLazyIndex;
					if (currentEntry.ProcessAssociatedMessages && currentEntry.ProcessNormalMessages)
					{
						viewMessageConfigureFlags |= ViewMessageConfigureFlags.ViewAll;
					}
					else if (currentEntry.ProcessAssociatedMessages)
					{
						viewMessageConfigureFlags |= ViewMessageConfigureFlags.ViewFAI;
					}
					currentEntry.ProcessAssociatedMessages = false;
					currentEntry.ProcessNormalMessages = false;
					if (currentEntry.Folder.IsGhosted(context, (LID)46796U))
					{
						continue;
					}
					currentEntry.ProcessingMessages = true;
					currentEntry.MessageView = new MapiViewMessage();
					currentEntry.MessageView.Configure(context, this.PrincipalFolder.Logon, currentEntry.Folder, viewMessageConfigureFlags);
					currentEntry.MessageView.SetColumns(context, BulkOperation.ColumnsToFetchMid, MapiViewSetColumnsFlag.NoColumnValidation);
				}
				IList<Properties> list = currentEntry.MessageView.QueryRowsBatch(context, numRows, QueryRowsFlags.None);
				if (list == null || list.Count == 0)
				{
					currentEntry.ProcessingMessages = false;
					currentEntry.MessageView.Dispose();
					currentEntry.MessageView = null;
				}
				else
				{
					if (this.midsToProcess == null)
					{
						this.midsToProcess = new List<ExchangeId>(list.Count);
					}
					else
					{
						this.midsToProcess.Clear();
					}
					for (int i = 0; i < list.Count; i++)
					{
						this.midsToProcess.Add(ExchangeId.CreateFrom26ByteArray(context, this.PrincipalFolder.Logon.StoreMailbox.ReplidGuidMap, (byte[])list[i][0].Value));
					}
					int num2;
					if (!this.ProcessMessages(context, currentEntry, this.midsToProcess, out num2, ref incomplete, ref error))
					{
						return false;
					}
					progressCount += num2;
				}
			}
			return true;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001143A File Offset: 0x0000F63A
		private bool FinishForFolder(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			return this.ProcessFolderEnd(context, currentEntry, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x0600025C RID: 604
		protected abstract bool ProcessFolderStart(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error);

		// Token: 0x0600025D RID: 605
		protected abstract bool ProcessMessages(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error);

		// Token: 0x0600025E RID: 606
		protected abstract bool ProcessFolderEnd(MapiContext context, HierarchyBulkOperation.FolderStackEntry currentEntry, out int progressCount, ref bool incomplete, ref ErrorCode error);

		// Token: 0x0600025F RID: 607 RVA: 0x0001144C File Offset: 0x0000F64C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.entryToDispose != null)
				{
					this.entryToDispose.Dispose();
					this.entryToDispose = null;
				}
				if (this.stack != null)
				{
					while (this.stack.Count != 0)
					{
						HierarchyBulkOperation.FolderStackEntry folderStackEntry = this.stack.Pop();
						folderStackEntry.Dispose();
					}
					this.stack = null;
				}
			}
		}

		// Token: 0x04000199 RID: 409
		private readonly MapiFolder principalFolder;

		// Token: 0x0400019A RID: 410
		private readonly bool processAssociatedDumpsterFolders;

		// Token: 0x0400019B RID: 411
		private Stack<HierarchyBulkOperation.FolderStackEntry> stack;

		// Token: 0x0400019C RID: 412
		private HierarchyBulkOperation.FolderStackEntry entryToDispose;

		// Token: 0x0400019D RID: 413
		private int chunkCount;

		// Token: 0x0400019E RID: 414
		private List<ExchangeId> midsToProcess;

		// Token: 0x0200005D RID: 93
		protected class FolderStackEntry : DisposableBase
		{
			// Token: 0x06000260 RID: 608 RVA: 0x000114A8 File Offset: 0x0000F6A8
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (this.DisposeFolder && this.Folder != null)
				{
					this.Folder.Dispose();
					this.Folder = null;
				}
				if (this.DisposeDestinationFolder && this.DestinationFolder != null)
				{
					this.DestinationFolder.Dispose();
					this.DestinationFolder = null;
				}
				if (this.MessageView != null)
				{
					this.MessageView.Dispose();
					this.MessageView = null;
				}
			}

			// Token: 0x06000261 RID: 609 RVA: 0x00011513 File Offset: 0x0000F713
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<HierarchyBulkOperation.FolderStackEntry>(this);
			}

			// Token: 0x0400019F RID: 415
			public IFolderInformation FolderInfo;

			// Token: 0x040001A0 RID: 416
			public bool IsPrincipal;

			// Token: 0x040001A1 RID: 417
			public bool IsAssociatedDumpster;

			// Token: 0x040001A2 RID: 418
			public bool Expanded;

			// Token: 0x040001A3 RID: 419
			public MapiFolder ParentFolder;

			// Token: 0x040001A4 RID: 420
			public MapiFolder Folder;

			// Token: 0x040001A5 RID: 421
			public bool DisposeFolder;

			// Token: 0x040001A6 RID: 422
			public bool ProcessSubfolders;

			// Token: 0x040001A7 RID: 423
			public bool ProcessAssociatedMessages;

			// Token: 0x040001A8 RID: 424
			public bool ProcessNormalMessages;

			// Token: 0x040001A9 RID: 425
			public bool ProcessingMessages;

			// Token: 0x040001AA RID: 426
			public MapiFolder DestinationParentFolder;

			// Token: 0x040001AB RID: 427
			public MapiFolder DestinationFolder;

			// Token: 0x040001AC RID: 428
			public bool DisposeDestinationFolder;

			// Token: 0x040001AD RID: 429
			public MapiViewMessage MessageView;
		}
	}
}
