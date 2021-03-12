using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000E RID: 14
	internal class FolderMap
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000486E File Offset: 0x00002A6E
		public FolderMap(FolderHierarchyFlags folderHierarchyFlags)
		{
			this.Flags = folderHierarchyFlags;
			this.folders = new EntryIdMap<FolderRecWrapper>();
			this.roots = new List<FolderRecWrapper>();
			if (this.IsPublicFolderMailbox)
			{
				this.publicFolderDumpsters = new EntryIdMap<FolderRecWrapper>();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000048A6 File Offset: 0x00002AA6
		public FolderMap(List<FolderRecWrapper> folders)
		{
			this.folders = new EntryIdMap<FolderRecWrapper>();
			this.roots = new List<FolderRecWrapper>();
			this.Flags = FolderHierarchyFlags.None;
			this.Config(folders);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000048D2 File Offset: 0x00002AD2
		public bool IsPublicFolderMailbox
		{
			get
			{
				return this.Flags.HasFlag(FolderHierarchyFlags.PublicFolderMailbox);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000048EA File Offset: 0x00002AEA
		public virtual FolderRecWrapper RootRec
		{
			get
			{
				this.ValidateMap();
				return this.roots[0];
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000048FE File Offset: 0x00002AFE
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00004906 File Offset: 0x00002B06
		public CultureInfo TargetMailboxCulture { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000490F File Offset: 0x00002B0F
		public int Count
		{
			get
			{
				return this.folders.Count;
			}
		}

		// Token: 0x17000012 RID: 18
		public FolderRecWrapper this[byte[] folderId]
		{
			get
			{
				FolderRecWrapper result;
				if (folderId == null || !this.folders.TryGetValue(folderId, out result))
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (value != null)
				{
					this.folders[folderId] = value;
					return;
				}
				if (folderId != null)
				{
					this.folders.Remove(folderId);
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004980 File Offset: 0x00002B80
		public FolderMap Copy()
		{
			List<FolderRecWrapper> copyList = new List<FolderRecWrapper>(this.folders.Count);
			this.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper folderRec, FolderMap.EnumFolderContext ctx)
			{
				copyList.Add(new FolderRecWrapper(folderRec));
			});
			return new FolderMap(copyList);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000049C8 File Offset: 0x00002BC8
		public void Config(List<FolderRecWrapper> folders)
		{
			this.Clear();
			foreach (FolderRecWrapper folderRecWrapper in folders)
			{
				if (this.folders.ContainsKey(folderRecWrapper.EntryId))
				{
					FolderRecWrapper folderRecWrapper2 = this.folders[folderRecWrapper.EntryId];
					MrsTracer.Service.Error("Folder {0} is listed more than once in the input folder list", new object[]
					{
						folderRecWrapper.FolderRec.ToString()
					});
					this.TraceFolders(folders, "Input folder list");
					throw new FolderHierarchyContainsDuplicatesPermanentException(folderRecWrapper.FolderRec.ToString(), folderRecWrapper2.FolderRec.ToString());
				}
				if (this.IsPublicFolderMailbox && folderRecWrapper.IsPublicFolderDumpster)
				{
					byte[] array = (byte[])folderRecWrapper.FolderRec[PropTag.LTID];
					if (array != null)
					{
						this.publicFolderDumpsters.Add(array, folderRecWrapper);
					}
				}
				this.InsertFolderInternal(folderRecWrapper);
			}
			if (this.IsPublicFolderMailbox)
			{
				foreach (FolderRecWrapper folderRecWrapper3 in folders)
				{
					if (!folderRecWrapper3.IsPublicFolderDumpster)
					{
						byte[] array2 = (byte[])folderRecWrapper3.FolderRec[PropTag.IpmWasteBasketEntryId];
						FolderRecWrapper publicFolderDumpster;
						if (array2 != null && this.publicFolderDumpsters.TryGetValue(array2, out publicFolderDumpster))
						{
							folderRecWrapper3.PublicFolderDumpster = publicFolderDumpster;
						}
					}
				}
			}
			this.ValidateMap();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004B68 File Offset: 0x00002D68
		public List<FolderRecWrapper> GetFolderList()
		{
			List<FolderRecWrapper> result = new List<FolderRecWrapper>(this.folders.Count);
			this.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper folderRec, FolderMap.EnumFolderContext ctx)
			{
				result.Add(folderRec);
			});
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004BAA File Offset: 0x00002DAA
		public void EnumerateFolderHierarchy(EnumHierarchyFlags flags, FolderMap.EnumFolderCallback callback)
		{
			this.EnumerateSubtree(flags, this.RootRec, callback);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004BBC File Offset: 0x00002DBC
		public virtual void EnumerateSubtree(EnumHierarchyFlags flags, FolderRecWrapper root, FolderMap.EnumFolderCallback callback)
		{
			FolderMap.EnumFolderContext ctx = new FolderMap.EnumFolderContext();
			this.EnumSingleFolder(root, ctx, callback, flags);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004BD9 File Offset: 0x00002DD9
		public IEnumerator<FolderRecWrapper> GetFolderHierarchyEnumerator(EnumHierarchyFlags flags)
		{
			if (this.subtreeEnumerator == null)
			{
				this.subtreeEnumerator = this.GetFolderList(flags, this.RootRec).GetEnumerator();
			}
			return this.subtreeEnumerator;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004C01 File Offset: 0x00002E01
		public void ResetFolderHierarchyEnumerator()
		{
			this.subtreeEnumerator = null;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004C0A File Offset: 0x00002E0A
		public void InsertFolder(FolderRecWrapper rec)
		{
			this.InsertFolderInternal(rec);
			this.ValidateMap();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004C1C File Offset: 0x00002E1C
		public void UpdateFolder(FolderRec updatedFolderData)
		{
			FolderRecWrapper folderRecWrapper = this[updatedFolderData.EntryId];
			if (folderRecWrapper == null)
			{
				return;
			}
			if (!CommonUtils.IsSameEntryId(updatedFolderData.ParentId, folderRecWrapper.ParentId))
			{
				FolderRecWrapper folderRecWrapper2 = this[updatedFolderData.ParentId];
				if (folderRecWrapper2 == null || folderRecWrapper2.IsChildOf(folderRecWrapper))
				{
					throw new UnableToApplyFolderHierarchyChangesTransientException();
				}
				folderRecWrapper.Parent = folderRecWrapper2;
			}
			folderRecWrapper.FolderRec.CopyFrom(updatedFolderData);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004C80 File Offset: 0x00002E80
		public void RemoveFolder(FolderRecWrapper rec)
		{
			if (rec == this.RootRec)
			{
				throw new UnexpectedErrorPermanentException(-2147467259);
			}
			this[rec.EntryId] = null;
			rec.Parent = null;
			if (rec.Children != null)
			{
				while (rec.Children.Count > 0)
				{
					this.RemoveFolder(rec.Children[0]);
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004CE0 File Offset: 0x00002EE0
		public void RemoveFolder(byte[] folderId)
		{
			FolderRecWrapper folderRecWrapper = this[folderId];
			if (folderRecWrapper != null)
			{
				this.RemoveFolder(folderRecWrapper);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004CFF File Offset: 0x00002EFF
		public virtual void Clear()
		{
			this.folders.Clear();
			this.roots.Clear();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004D34 File Offset: 0x00002F34
		public override string ToString()
		{
			StringBuilder strBuilder = new StringBuilder();
			this.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper fR, FolderMap.EnumFolderContext c)
			{
				strBuilder.AppendLine(fR.ToString());
			});
			return strBuilder.ToString();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004D70 File Offset: 0x00002F70
		protected static bool IsValidFolderType(EnumHierarchyFlags flags, FolderRecWrapper folderRec)
		{
			switch (folderRec.FolderType)
			{
			case FolderType.Root:
				return (flags & EnumHierarchyFlags.RootFolder) != EnumHierarchyFlags.None;
			case FolderType.Generic:
				return (flags & EnumHierarchyFlags.NormalFolders) != EnumHierarchyFlags.None;
			case FolderType.Search:
				return (flags & EnumHierarchyFlags.SearchFolders) != EnumHierarchyFlags.None;
			default:
				return false;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004F68 File Offset: 0x00003168
		protected virtual IEnumerable<FolderRecWrapper> GetFolderList(EnumHierarchyFlags flags, FolderRecWrapper folderRec)
		{
			Stack<FolderRecWrapper> stack = new Stack<FolderRecWrapper>();
			stack.Push(folderRec);
			while (stack.Count > 0)
			{
				FolderRecWrapper currentFolderRec = stack.Pop();
				if (!currentFolderRec.IsSpoolerQueue)
				{
					if (FolderMap.IsValidFolderType(flags, currentFolderRec))
					{
						yield return currentFolderRec;
					}
					if (currentFolderRec.Children != null)
					{
						foreach (FolderRecWrapper item in currentFolderRec.Children)
						{
							stack.Push(item);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004F94 File Offset: 0x00003194
		private void EnumSingleFolder(FolderRecWrapper folderRec, FolderMap.EnumFolderContext ctx, FolderMap.EnumFolderCallback callback, EnumHierarchyFlags flags)
		{
			ctx.Result = EnumHierarchyResult.Continue;
			if (folderRec.IsSpoolerQueue)
			{
				return;
			}
			if (FolderMap.IsValidFolderType(flags, folderRec))
			{
				callback(folderRec, ctx);
			}
			if (ctx.Result == EnumHierarchyResult.Cancel || ctx.Result == EnumHierarchyResult.SkipSubtree)
			{
				return;
			}
			if (folderRec.Children != null)
			{
				foreach (FolderRecWrapper folderRec2 in folderRec.Children)
				{
					ctx.Result = EnumHierarchyResult.Continue;
					this.EnumSingleFolder(folderRec2, ctx, callback, flags);
					if (ctx.Result == EnumHierarchyResult.Cancel)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005038 File Offset: 0x00003238
		protected virtual void InsertFolderInternal(FolderRecWrapper rec)
		{
			FolderRecWrapper folderRecWrapper;
			if (rec.ParentId != null)
			{
				folderRecWrapper = this[rec.ParentId];
			}
			else
			{
				folderRecWrapper = null;
			}
			for (FolderRecWrapper folderRecWrapper2 = folderRecWrapper; folderRecWrapper2 != null; folderRecWrapper2 = folderRecWrapper2.Parent)
			{
				if (CommonUtils.IsSameEntryId(folderRecWrapper2.ParentId, rec.EntryId))
				{
					MrsTracer.Service.Error("Loop in the parent chain detected, folder {0}", new object[]
					{
						rec.FolderRec.ToString()
					});
					this.TraceFolders(this.folders.Values, "Folders");
					throw new FolderHierarchyContainsParentChainLoopPermanentException(rec.FolderRec.ToString());
				}
			}
			this[rec.EntryId] = rec;
			rec.Parent = folderRecWrapper;
			for (int i = this.roots.Count - 1; i >= 0; i--)
			{
				FolderRecWrapper folderRecWrapper3 = this.roots[i];
				if (CommonUtils.IsSameEntryId(folderRecWrapper3.ParentId, rec.EntryId))
				{
					folderRecWrapper3.Parent = rec;
					this.roots.RemoveAt(i);
				}
			}
			if (folderRecWrapper == null)
			{
				this.roots.Add(rec);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000513C File Offset: 0x0000333C
		protected virtual void ValidateMap()
		{
			if (this.roots.Count == 1)
			{
				return;
			}
			if (this.roots.Count == 0)
			{
				MrsTracer.Service.Error("No roots present in folder hierarchy.", new object[0]);
				this.TraceFolders(this.folders.Values, "Folders");
				throw new FolderHierarchyContainsNoRootsPermanentException();
			}
			MrsTracer.Service.Error("More than one root is present in folder hierarchy.", new object[0]);
			foreach (FolderRecWrapper folderRecWrapper in this.roots)
			{
				MrsTracer.Service.Error("Root: {0}", new object[]
				{
					folderRecWrapper.FolderRec.ToString()
				});
			}
			this.TraceFolders(this.folders.Values, "Folders");
			throw new FolderHierarchyContainsMultipleRootsTransientException(this.roots[0].FolderRec.ToString(), this.roots[1].FolderRec.ToString());
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000525C File Offset: 0x0000345C
		private void TraceFolders(ICollection<FolderRecWrapper> folders, string header)
		{
			MrsTracer.Service.Error("{0}", new object[]
			{
				header
			});
			foreach (FolderRecWrapper folderRecWrapper in folders)
			{
				MrsTracer.Service.Error("{0}", new object[]
				{
					folderRecWrapper.FolderRec.ToString()
				});
			}
		}

		// Token: 0x04000029 RID: 41
		private List<FolderRecWrapper> roots;

		// Token: 0x0400002A RID: 42
		private IEnumerator<FolderRecWrapper> subtreeEnumerator;

		// Token: 0x0400002B RID: 43
		protected EntryIdMap<FolderRecWrapper> publicFolderDumpsters;

		// Token: 0x0400002C RID: 44
		protected readonly FolderHierarchyFlags Flags;

		// Token: 0x0400002D RID: 45
		protected EntryIdMap<FolderRecWrapper> folders;

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x06000076 RID: 118
		public delegate void EnumFolderCallback(FolderRecWrapper folderRec, FolderMap.EnumFolderContext ctx);

		// Token: 0x02000010 RID: 16
		public class EnumFolderContext
		{
			// Token: 0x06000079 RID: 121 RVA: 0x000052E0 File Offset: 0x000034E0
			public EnumFolderContext()
			{
				this.result = EnumHierarchyResult.Continue;
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x0600007A RID: 122 RVA: 0x000052EF File Offset: 0x000034EF
			// (set) Token: 0x0600007B RID: 123 RVA: 0x000052F7 File Offset: 0x000034F7
			public EnumHierarchyResult Result
			{
				get
				{
					return this.result;
				}
				set
				{
					this.result = value;
				}
			}

			// Token: 0x0400002F RID: 47
			private EnumHierarchyResult result;
		}
	}
}
