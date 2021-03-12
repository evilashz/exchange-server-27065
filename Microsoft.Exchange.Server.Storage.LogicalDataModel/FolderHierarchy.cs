using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000050 RID: 80
	public class FolderHierarchy : IStateObject
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x00042DF0 File Offset: 0x00040FF0
		private FolderHierarchy(FolderInformationComparer folderInformationComparer, IDictionary<ExchangeShortId, FolderHierarchy.FolderInformationImpl> hierarchyFolders, List<IFolderInformation> hierarchyRoots, FolderInformationType informationType)
		{
			this.folderInformationComparer = folderInformationComparer;
			this.hierarchyFolders = hierarchyFolders;
			this.hierarchyRoots = hierarchyRoots;
			this.informationType = informationType;
			this.isValid = true;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00042E1C File Offset: 0x0004101C
		public bool IsEmpty
		{
			get
			{
				return this.hierarchyRoots.Count == 0;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00042E2C File Offset: 0x0004102C
		public IList<IFolderInformation> HierarchyRoots
		{
			get
			{
				return this.hierarchyRoots;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00042E34 File Offset: 0x00041034
		public int TotalFolderCount
		{
			get
			{
				return this.hierarchyFolders.Count;
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00042E41 File Offset: 0x00041041
		public static void Initialize()
		{
			if (FolderHierarchy.folderHierarchyDataSlot == -1)
			{
				FolderHierarchy.folderHierarchyDataSlot = MailboxState.AllocateComponentDataSlot(false);
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00042E58 File Offset: 0x00041058
		public static FolderHierarchy GetFolderHierarchy(Context context, Mailbox mailbox, ExchangeShortId rootFolderId, FolderInformationType requestedInformationType)
		{
			FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchyNoCreate(context, mailbox);
			if (folderHierarchy == null || (folderHierarchy.informationType != requestedInformationType && requestedInformationType == FolderInformationType.Extended))
			{
				folderHierarchy = FolderHierarchy.CreateFolderHierarchy(context, mailbox, requestedInformationType);
				if (context.TransactionStarted)
				{
					context.RegisterStateObject(folderHierarchy);
				}
				mailbox.SharedState.SetComponentData(FolderHierarchy.folderHierarchyDataSlot, folderHierarchy);
			}
			if (rootFolderId.IsZero)
			{
				return folderHierarchy;
			}
			FolderHierarchy.FolderInformationImpl folderInformationImpl;
			if (!folderHierarchy.hierarchyFolders.TryGetValue(rootFolderId, out folderInformationImpl))
			{
				return FolderHierarchy.emptyHierarchy;
			}
			if (folderInformationImpl.Parent != null)
			{
				folderHierarchy.PopulateChildren(context, folderInformationImpl.Parent);
			}
			List<IFolderInformation> list = new List<IFolderInformation>(1);
			list.Add(folderInformationImpl);
			return new FolderHierarchy(folderHierarchy.folderInformationComparer, folderHierarchy.hierarchyFolders, list, folderHierarchy.informationType);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00042F04 File Offset: 0x00041104
		public static FolderHierarchy GetFolderHierarchyNoCreate(Context context, Mailbox mailbox)
		{
			FolderHierarchy folderHierarchy = (FolderHierarchy)mailbox.SharedState.GetComponentData(FolderHierarchy.folderHierarchyDataSlot);
			if (folderHierarchy != null && !folderHierarchy.isValid)
			{
				folderHierarchy = null;
			}
			return folderHierarchy;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00042F38 File Offset: 0x00041138
		public static FolderHierarchy CreateFolderHierarchyForTest(Tuple<ExchangeShortId, ExchangeShortId, string>[] folders, CultureInfo culture, bool sortChildren)
		{
			FolderInformationComparer comparer = new FolderInformationComparer(culture.CompareInfo);
			List<IFolderInformation> list = new List<IFolderInformation>();
			Dictionary<ExchangeShortId, FolderHierarchy.FolderInformationImpl> dictionary = new Dictionary<ExchangeShortId, FolderHierarchy.FolderInformationImpl>();
			foreach (Tuple<ExchangeShortId, ExchangeShortId, string> tuple in folders)
			{
				ExchangeShortId item = tuple.Item1;
				ExchangeShortId item2 = tuple.Item2;
				string item3 = tuple.Item3;
				FolderHierarchy.FolderInformationImpl folderInformationImpl = null;
				if (!item2.IsZero)
				{
					folderInformationImpl = dictionary[item2];
				}
				FolderHierarchy.FolderInformationImpl folderInformationImpl2 = new FolderHierarchy.FolderInformationImpl(item, folderInformationImpl, FolderHierarchy.FolderInformationImpl.FolderInformationFlags.None, 0, item3, 0L, null);
				folderInformationImpl2.ChildrenArePopulated = true;
				dictionary.Add(item, folderInformationImpl2);
				if (folderInformationImpl != null)
				{
					folderInformationImpl.LinkChild(folderInformationImpl2, comparer);
				}
			}
			foreach (FolderHierarchy.FolderInformationImpl folderInformationImpl3 in dictionary.Values)
			{
				if (sortChildren)
				{
					folderInformationImpl3.SortChildrenAndCompact(comparer);
				}
				if (folderInformationImpl3.Parent == null)
				{
					list.Add(folderInformationImpl3);
				}
			}
			list.Sort(comparer);
			return new FolderHierarchy(comparer, dictionary, list, FolderInformationType.Basic);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00043048 File Offset: 0x00041248
		public static void SortFolderInformationChildrenAndCompactForTest(Context context, FolderHierarchy folderHierarchy, IFolderInformation folderInformation, FolderInformationComparer folderInformationComparer)
		{
			folderHierarchy.PopulateChildren(context, (FolderHierarchy.FolderInformationImpl)folderInformation);
			((FolderHierarchy.FolderInformationImpl)folderInformation).SortChildrenAndCompact(folderInformationComparer);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00043063 File Offset: 0x00041263
		public static IFolderInformation CreateFolderInformationForTest(ExchangeShortId fid, IFolderInformation parentInformation, int mailboxNumber, string displayName, long messageCount, SecurityDescriptor securityDescriptor)
		{
			return new FolderHierarchy.FolderInformationImpl(fid, (FolderHierarchy.FolderInformationImpl)parentInformation, FolderHierarchy.FolderInformationImpl.FolderInformationFlags.None, mailboxNumber, displayName, messageCount, securityDescriptor);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00043078 File Offset: 0x00041278
		public static void LinkChildForTest(IFolderInformation parentInformation, IFolderInformation folderInformationToLink, FolderInformationComparer folderInformationComparer)
		{
			((FolderHierarchy.FolderInformationImpl)parentInformation).LinkChild((FolderHierarchy.FolderInformationImpl)folderInformationToLink, folderInformationComparer);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0004308C File Offset: 0x0004128C
		public static void UnlinkChildForTest(IFolderInformation parentInformation, IFolderInformation folderInformationToLink, FolderInformationComparer folderInformationComparer)
		{
			((FolderHierarchy.FolderInformationImpl)parentInformation).UnlinkChild((FolderHierarchy.FolderInformationImpl)folderInformationToLink, folderInformationComparer, false);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000430A1 File Offset: 0x000412A1
		public static void ChangeFolderInformationPropertiesForTest(IFolderInformation folderInformation, string newDisplayName, long newMessageCount, FolderInformationComparer folderInformationComparer)
		{
			((FolderHierarchy.FolderInformationImpl)folderInformation).ChangeDisplayName(newDisplayName, folderInformationComparer);
			((FolderHierarchy.FolderInformationImpl)folderInformation).MessageCount = newMessageCount;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000430BC File Offset: 0x000412BC
		public static IDisposable SetMaxChildrenForCompactionForTest(int maxChildrenForCompaction)
		{
			return FolderHierarchy.maxChildrenForCompaction.SetTestHook(maxChildrenForCompaction);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000430CC File Offset: 0x000412CC
		public static void OnFolderCreated(Context context, Folder folder)
		{
			FolderHierarchy folderHierarchyNoCreate = FolderHierarchy.GetFolderHierarchyNoCreate(context, folder.Mailbox);
			if (folderHierarchyNoCreate == null)
			{
				return;
			}
			if (!context.IsStateObjectRegistered(folderHierarchyNoCreate))
			{
				context.RegisterStateObject(folderHierarchyNoCreate);
			}
			FolderTable folderTable = DatabaseSchema.FolderTable(context.Database);
			ExchangeShortId exchangeShortId = folder.GetId(context).ToExchangeShortId();
			ExchangeShortId key = folder.GetParentFolderId(context).ToExchangeShortId();
			FolderHierarchy.FolderInformationImpl folderInformationImpl = null;
			if (!key.IsZero)
			{
				folderInformationImpl = folderHierarchyNoCreate.hierarchyFolders[key];
			}
			FolderHierarchy.FolderInformationImpl.FolderInformationFlags folderInformationFlags = FolderHierarchy.FolderInformationImpl.FolderInformationFlags.None;
			int mailboxNumber = 0;
			string displayName = null;
			long messageCount = 0L;
			SecurityDescriptor securityDescriptor = null;
			if (folderInformationImpl == null || folderInformationImpl.ChildrenArePopulated)
			{
				if (folder.IsSearchFolder(context))
				{
					folderInformationFlags |= FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsSearchFolder;
				}
				if ((bool)folder.GetPropertyValue(context, PropTag.Folder.PartOfContentIndexing))
				{
					folderInformationFlags |= FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsPartOfContentIndexing;
				}
				if (folder.IsInternalAccess(context))
				{
					folderInformationFlags |= FolderHierarchy.FolderInformationImpl.FolderInformationFlags.InternalAccess;
				}
				mailboxNumber = (int)folder.GetPropertyValue(context, PropTag.Folder.MailboxNum);
				displayName = (((string)folder.GetColumnValue(context, folderTable.DisplayName)) ?? string.Empty);
				messageCount = folder.GetMessageCount(context);
				if (folderHierarchyNoCreate.informationType == FolderInformationType.Extended)
				{
					byte[] buffer = (byte[])folder.GetColumnValue(context, folderTable.AclTableAndSecurityDescriptor);
					FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = AclTableHelper.Parse(context, buffer);
					securityDescriptor = aclTableAndSecurityDescriptorProperty.SecurityDescriptor;
				}
			}
			FolderHierarchy.FolderInformationImpl folderInformationImpl2 = new FolderHierarchy.FolderInformationImpl(exchangeShortId, folderInformationImpl, folderInformationFlags, mailboxNumber, displayName, messageCount, securityDescriptor);
			if (folderInformationImpl != null)
			{
				folderInformationImpl.LinkChild(folderInformationImpl2, folderHierarchyNoCreate.folderInformationComparer);
			}
			folderHierarchyNoCreate.hierarchyFolders.Add(exchangeShortId, folderInformationImpl2);
			if (key.IsZero)
			{
				folderHierarchyNoCreate.hierarchyRoots.Add(folderInformationImpl2);
				folderHierarchyNoCreate.hierarchyRoots.Sort(folderHierarchyNoCreate.folderInformationComparer);
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0004327C File Offset: 0x0004147C
		public static void OnFolderDeleted(Context context, Folder folder, ExchangeShortId parentFid)
		{
			FolderHierarchy folderHierarchyNoCreate = FolderHierarchy.GetFolderHierarchyNoCreate(context, folder.Mailbox);
			if (folderHierarchyNoCreate == null)
			{
				return;
			}
			if (!context.IsStateObjectRegistered(folderHierarchyNoCreate))
			{
				context.RegisterStateObject(folderHierarchyNoCreate);
			}
			ExchangeShortId fid = folder.GetId(context).ToExchangeShortId();
			if (!parentFid.IsZero)
			{
				FolderHierarchy.FolderInformationImpl folderInformationImpl = folderHierarchyNoCreate.hierarchyFolders[parentFid];
				FolderHierarchy.FolderInformationImpl child = folderHierarchyNoCreate.hierarchyFolders[fid];
				folderInformationImpl.UnlinkChild(child, folderHierarchyNoCreate.folderInformationComparer, false);
			}
			else
			{
				int index = folderHierarchyNoCreate.hierarchyRoots.FindIndex((IFolderInformation fi) => fi.Fid == fid);
				folderHierarchyNoCreate.hierarchyRoots.RemoveAt(index);
			}
			folderHierarchyNoCreate.hierarchyFolders.Remove(fid);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00043348 File Offset: 0x00041548
		public static void OnFolderMoved(Context context, Folder folder, ExchangeShortId originalParentFid)
		{
			FolderHierarchy folderHierarchyNoCreate = FolderHierarchy.GetFolderHierarchyNoCreate(context, folder.Mailbox);
			if (folderHierarchyNoCreate == null)
			{
				return;
			}
			if (!context.IsStateObjectRegistered(folderHierarchyNoCreate))
			{
				context.RegisterStateObject(folderHierarchyNoCreate);
			}
			ExchangeShortId key = folder.GetId(context).ToExchangeShortId();
			FolderHierarchy.FolderInformationImpl folderInformationImpl = folderHierarchyNoCreate.hierarchyFolders[key];
			IList<IFolderInformation> children = folderInformationImpl.Children;
			bool childrenArePopulated = folderInformationImpl.ChildrenArePopulated;
			FolderHierarchy.OnFolderDeleted(context, folder, originalParentFid);
			FolderHierarchy.OnFolderCreated(context, folder);
			FolderHierarchy.FolderInformationImpl folderInformationImpl2 = folderHierarchyNoCreate.hierarchyFolders[key];
			folderInformationImpl2.SetChildren(children);
			folderInformationImpl2.ChildrenArePopulated = childrenArePopulated;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000433D4 File Offset: 0x000415D4
		public static void OnFolderChanged(Context context, Folder folder)
		{
			FolderHierarchy folderHierarchyNoCreate = FolderHierarchy.GetFolderHierarchyNoCreate(context, folder.Mailbox);
			if (folderHierarchyNoCreate == null)
			{
				return;
			}
			FolderHierarchy.FolderInformationImpl folderInformationImpl = folderHierarchyNoCreate.hierarchyFolders[folder.GetId(context).ToExchangeShortId()];
			if (folderInformationImpl.Parent == null || folderInformationImpl.Parent.ChildrenArePopulated)
			{
				if (!context.IsStateObjectRegistered(folderHierarchyNoCreate))
				{
					context.RegisterStateObject(folderHierarchyNoCreate);
				}
				FolderTable folderTable = DatabaseSchema.FolderTable(context.Database);
				if (folder.DataRow.ColumnDirty(folderTable.MessageCount))
				{
					folderInformationImpl.MessageCount = (long)folder.GetColumnValue(context, folderTable.MessageCount);
				}
				if (folder.DataRow.ColumnDirty(folderTable.DisplayName))
				{
					folderInformationImpl.ChangeDisplayName(((string)folder.GetColumnValue(context, folderTable.DisplayName)) ?? string.Empty, folderHierarchyNoCreate.folderInformationComparer);
					if (folderInformationImpl.Parent == null)
					{
						folderHierarchyNoCreate.hierarchyRoots.Sort(folderHierarchyNoCreate.folderInformationComparer);
					}
				}
				if (folder.Mailbox.SharedState.UnifiedState != null && folder.DataRow.ColumnDirty(folderTable.MailboxNumber))
				{
					int mailboxNumber = (int)folder.GetColumnValue(context, folderTable.MailboxNumber);
					folderInformationImpl.MailboxNumber = mailboxNumber;
				}
				if (folderHierarchyNoCreate.informationType == FolderInformationType.Extended && folder.DataRow.ColumnDirty(folderTable.AclTableAndSecurityDescriptor))
				{
					byte[] buffer = (byte[])folder.GetColumnValue(context, folderTable.AclTableAndSecurityDescriptor);
					FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = AclTableHelper.Parse(context, buffer);
					folderInformationImpl.SecurityDescriptor = aclTableAndSecurityDescriptorProperty.SecurityDescriptor;
				}
				folderInformationImpl.IsPartOfContentIndexing = folder.IsPartOfContentIndexing(context);
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00043553 File Offset: 0x00041753
		public static void DiscardFolderHierarchyCache(Context context, MailboxState mailboxState)
		{
			mailboxState.SetComponentData(FolderHierarchy.folderHierarchyDataSlot, null);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00043561 File Offset: 0x00041761
		public static IFolderInformation FolderInformationFromFolderId(ExchangeShortId folderId)
		{
			return new FolderHierarchy.FolderInformationImpl(folderId);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00043569 File Offset: 0x00041769
		public void OnBeforeCommit(Context context)
		{
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0004356B File Offset: 0x0004176B
		public void OnCommit(Context context)
		{
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0004356D File Offset: 0x0004176D
		public void OnAbort(Context context)
		{
			this.isValid = false;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00043578 File Offset: 0x00041778
		public IFolderInformation Find(Context context, ExchangeShortId fid)
		{
			int num;
			return this.Find(context, fid, out num);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00043590 File Offset: 0x00041790
		public IFolderInformation GetParent(Context context, IFolderInformation child)
		{
			FolderHierarchy.FolderInformationImpl folderInformationImpl = (FolderHierarchy.FolderInformationImpl)child;
			if (folderInformationImpl.Parent != null && folderInformationImpl.Parent.Parent != null)
			{
				this.PopulateChildren(context, folderInformationImpl.Parent.Parent);
			}
			return folderInformationImpl.Parent;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000435D1 File Offset: 0x000417D1
		public IList<IFolderInformation> GetChildren(Context context, IFolderInformation parent)
		{
			return this.GetSortedChildrenList(context, (FolderHierarchy.FolderInformationImpl)parent, null);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000435E4 File Offset: 0x000417E4
		public IFolderInformation Find(Context context, ExchangeShortId fid, out int depth)
		{
			depth = 0;
			FolderHierarchy.FolderInformationImpl folderInformationImpl;
			if (!this.hierarchyFolders.TryGetValue(fid, out folderInformationImpl))
			{
				return null;
			}
			if (folderInformationImpl.Parent != null)
			{
				this.PopulateChildren(context, folderInformationImpl.Parent);
			}
			for (FolderHierarchy.FolderInformationImpl folderInformationImpl2 = folderInformationImpl; folderInformationImpl2 != null; folderInformationImpl2 = folderInformationImpl2.Parent)
			{
				if (this.hierarchyRoots.Contains(folderInformationImpl2))
				{
					return folderInformationImpl;
				}
				depth++;
			}
			return null;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00043640 File Offset: 0x00041840
		public IFolderInformation FindByName(Context context, ExchangeShortId parentFid, string displayName, CompareInfo compareInfo)
		{
			int num;
			return this.FindByName(context, parentFid, displayName, compareInfo, false, out num);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0004365C File Offset: 0x0004185C
		public IFolderInformation FindByName(Context context, ExchangeShortId parentFid, string displayName, CompareInfo compareInfo, bool alwaysBSearch, out int childIndex)
		{
			bool flag = this.folderInformationComparer.CompareInfo == compareInfo;
			IList<IFolderInformation> sortedChildrenList;
			bool flag2;
			if (parentFid.IsZero)
			{
				sortedChildrenList = this.hierarchyRoots;
				flag2 = flag;
			}
			else
			{
				FolderHierarchy.FolderInformationImpl folderInformationImpl;
				if (!this.hierarchyFolders.TryGetValue(parentFid, out folderInformationImpl))
				{
					childIndex = -1;
					return null;
				}
				this.PopulateChildren(context, folderInformationImpl);
				sortedChildrenList = this.GetSortedChildrenList(context, folderInformationImpl, null);
				flag2 = (flag && folderInformationImpl.IsSorted);
			}
			if (sortedChildrenList == null || sortedChildrenList.Count == 0)
			{
				childIndex = -1;
				return null;
			}
			if (flag2 && (alwaysBSearch || sortedChildrenList.Count > 4))
			{
				FolderHierarchy.FolderInformationImpl folderInformationImpl2 = new FolderHierarchy.FolderInformationImpl(ExchangeShortId.Zero, null, FolderHierarchy.FolderInformationImpl.FolderInformationFlags.None, 0, displayName, 0L, null);
				if (sortedChildrenList is List<IFolderInformation>)
				{
					List<IFolderInformation> list = (List<IFolderInformation>)sortedChildrenList;
					childIndex = list.BinarySearch(folderInformationImpl2, this.folderInformationComparer);
				}
				else
				{
					IFolderInformation[] array = (IFolderInformation[])sortedChildrenList;
					childIndex = Array.BinarySearch<IFolderInformation>(array, folderInformationImpl2, this.folderInformationComparer);
				}
				if (childIndex < 0)
				{
					childIndex = ~childIndex;
				}
				if (childIndex < sortedChildrenList.Count)
				{
					IFolderInformation folderInformation = sortedChildrenList[childIndex];
					if (compareInfo.Compare(folderInformation.DisplayName, displayName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0)
					{
						return folderInformation;
					}
				}
			}
			else
			{
				for (int i = 0; i < sortedChildrenList.Count; i++)
				{
					IFolderInformation folderInformation2 = sortedChildrenList[i];
					if (compareInfo.Compare(folderInformation2.DisplayName, displayName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0)
					{
						childIndex = i;
						return folderInformation2;
					}
				}
				childIndex = -1;
			}
			return null;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000437B8 File Offset: 0x000419B8
		public long GetFolderHierarchyDepth(ExchangeShortId folderId)
		{
			long num = 0L;
			FolderHierarchy.FolderInformationImpl parent;
			if (this.hierarchyFolders.TryGetValue(folderId, out parent))
			{
				num = 1L;
				while (parent.Parent != null && !parent.Parent.Fid.IsZero)
				{
					parent = parent.Parent;
					num += 1L;
				}
			}
			return num;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00043808 File Offset: 0x00041A08
		public void ForEachFolderInformation(Context context, Action<IFolderInformation> action)
		{
			foreach (KeyValuePair<ExchangeShortId, FolderHierarchy.FolderInformationImpl> keyValuePair in this.hierarchyFolders)
			{
				if (keyValuePair.Value.Parent != null)
				{
					this.PopulateChildren(context, keyValuePair.Value.Parent);
				}
				action(keyValuePair.Value);
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00043BA0 File Offset: 0x00041DA0
		public IEnumerable<FolderHierarchyBlob> SerializeRecursiveHierarchyBlob(Context context, Mailbox mailbox, IFolderInformation root, bool recursive, Func<Context, IFolderInformation, bool> isVisiblePredicate, ExchangeShortId startFolderId, int startFolderSortPosition, bool startFolderInclusive)
		{
			IReplidGuidMap replidGuidMap = mailbox.ReplidGuidMap;
			FolderInformationComparer alternateComparer = null;
			if (this.folderInformationComparer.CompareInfo != context.Culture.CompareInfo)
			{
				alternateComparer = new FolderInformationComparer(context.Culture.CompareInfo);
			}
			int sortPosition;
			Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry> enumeratorStack = this.InitEnumeratorStack(context, (FolderHierarchy.FolderInformationImpl)root, recursive, isVisiblePredicate, startFolderId, startFolderSortPosition, startFolderInclusive, alternateComparer, out sortPosition);
			ExchangeId lastParentId = ExchangeId.Zero;
			ExchangeShortId lastParentShortId = ExchangeShortId.Zero;
			while (enumeratorStack.Count != 0)
			{
				FolderHierarchy.FolderHierarchyEnumeratorStackEntry entry = this.PopFromEnumeratorStack(context, enumeratorStack, recursive, isVisiblePredicate, alternateComparer);
				FolderHierarchy.FolderInformationImpl folderInfo = (FolderHierarchy.FolderInformationImpl)entry.List[entry.Position];
				ExchangeShortId parentFolderShortId = (folderInfo.Parent == null) ? ExchangeShortId.Zero : folderInfo.Parent.Fid;
				if (parentFolderShortId != lastParentShortId)
				{
					lastParentShortId = parentFolderShortId;
					lastParentId = ExchangeId.CreateFromInternalShortId(context, replidGuidMap, parentFolderShortId);
				}
				int mailboxPartitionNumber = mailbox.MailboxPartitionNumber;
				int mailboxNumber = mailbox.MailboxNumber;
				byte[] parentFolderId = lastParentId.To26ByteArray();
				byte[] folderId = ExchangeId.CreateFromInternalShortId(context, replidGuidMap, folderInfo.Fid).To26ByteArray();
				string displayName = folderInfo.DisplayName;
				int depth = entry.Depth;
				int sortPosition2;
				sortPosition = (sortPosition2 = sortPosition) + 1;
				yield return new FolderHierarchyBlob(mailboxPartitionNumber, mailboxNumber, parentFolderId, folderId, displayName, depth, sortPosition2);
			}
			yield break;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00043BFC File Offset: 0x00041DFC
		private static FolderHierarchy CreateFolderHierarchy(Context context, Mailbox mailbox, FolderInformationType informationType)
		{
			if (context.PerfInstance != null)
			{
				context.PerfInstance.FolderHierarchyLoadRecursiveRate.Increment();
			}
			bool flag = false;
			FolderHierarchy result;
			try
			{
				if (!context.TransactionStarted)
				{
					context.BeginTransactionIfNeeded();
					flag = true;
				}
				ReplidGuidMap replidGuidMap = mailbox.ReplidGuidMap;
				FolderTable folderTable = DatabaseSchema.FolderTable(mailbox.Database);
				List<IFolderInformation> list = new List<IFolderInformation>(5);
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					mailbox.MailboxPartitionNumber
				});
				Column[] columnsToFetch = new Column[]
				{
					folderTable.FolderId,
					folderTable.ParentFolderId
				};
				Dictionary<ExchangeShortId, FolderHierarchy.FolderInformationImpl> dictionary;
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, folderTable.Table, folderTable.FolderByParentIndex, columnsToFetch, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, false, true))
				{
					using (CountOperator countOperator = Factory.CreateCountOperator(context.Culture, context, tableOperator, false))
					{
						dictionary = new Dictionary<ExchangeShortId, FolderHierarchy.FolderInformationImpl>((int)countOperator.ExecuteScalar());
					}
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						while (reader.Read())
						{
							ExchangeId exchangeId = ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, reader.GetBinary(folderTable.FolderId));
							FolderHierarchy.FolderInformationImpl parentInformation = null;
							ExchangeId exchangeId2 = ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, reader.GetBinary(folderTable.ParentFolderId));
							if (exchangeId2.IsValid)
							{
								parentInformation = new FolderHierarchy.FolderInformationImpl(exchangeId2.ToExchangeShortId());
							}
							FolderHierarchy.FolderInformationImpl folderInformationImpl = new FolderHierarchy.FolderInformationImpl(exchangeId.ToExchangeShortId(), parentInformation, FolderHierarchy.FolderInformationImpl.FolderInformationFlags.None, 0, null, 0L, null);
							dictionary.Add(folderInformationImpl.Fid, folderInformationImpl);
						}
					}
				}
				FolderInformationComparer comparer = new FolderInformationComparer(context.Culture.CompareInfo);
				foreach (KeyValuePair<ExchangeShortId, FolderHierarchy.FolderInformationImpl> keyValuePair in dictionary)
				{
					FolderHierarchy.FolderInformationImpl folderInformationImpl2 = null;
					if (keyValuePair.Value.Parent != null)
					{
						dictionary.TryGetValue(keyValuePair.Value.Parent.Fid, out folderInformationImpl2);
					}
					if (folderInformationImpl2 != null)
					{
						folderInformationImpl2.LinkChild(keyValuePair.Value, comparer);
					}
					else
					{
						list.Add(keyValuePair.Value);
					}
				}
				foreach (KeyValuePair<ExchangeShortId, FolderHierarchy.FolderInformationImpl> keyValuePair2 in dictionary)
				{
					FolderHierarchy.FolderInformationImpl value = keyValuePair2.Value;
					if (value.Children.Count <= FolderHierarchy.maxChildrenForCompaction.Value && value.Children is List<IFolderInformation>)
					{
						value.Children = value.Children.ToArray<IFolderInformation>();
					}
				}
				FolderHierarchy folderHierarchy = new FolderHierarchy(comparer, dictionary, list, informationType);
				folderHierarchy.PopulateChildren(context, null);
				if (list.Count > 1)
				{
					list.Sort(comparer);
				}
				result = folderHierarchy;
			}
			finally
			{
				if (flag)
				{
					context.Commit();
				}
			}
			return result;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00043F54 File Offset: 0x00042154
		private void PopulateChildren(Context context, FolderHierarchy.FolderInformationImpl parent)
		{
			if (parent != null && parent.ChildrenArePopulated)
			{
				return;
			}
			IReplidGuidMap cacheForMailbox = ReplidGuidMap.GetCacheForMailbox(context, context.LockedMailboxState);
			object lockObject = (parent != null) ? parent : this.hierarchyRoots;
			using (LockManager.Lock(lockObject, LockManager.LockType.LeafMonitorLock))
			{
				if (parent == null || !parent.ChildrenArePopulated)
				{
					IList<IFolderInformation> list = (parent != null) ? parent.Children : this.hierarchyRoots;
					if (list != null && list.Count != 0)
					{
						bool flag = false;
						try
						{
							if (!context.TransactionStarted)
							{
								context.BeginTransactionIfNeeded();
								flag = true;
							}
							FolderTable folderTable = DatabaseSchema.FolderTable(context.Database);
							FolderHierarchy.FolderInformationImpl.FolderColumnsInformation folderColumnsInformation = new FolderHierarchy.FolderInformationImpl.FolderColumnsInformation
							{
								Table = folderTable,
								PartOfContentIndexingColumn = PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.PartOfContentIndexing),
								MailboxNumberColumn = PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.MailboxNum),
								InternalAccessColumn = PropertySchema.MapToColumn(context.Database, ObjectType.Folder, PropTag.Folder.InternalAccess)
							};
							IList<Column> columnsToFetch = FolderHierarchy.FolderInformationImpl.GetColumnsToFetch(folderColumnsInformation, this.informationType);
							KeyRange[] array = new KeyRange[list.Count];
							for (int i = 0; i < list.Count; i++)
							{
								StartStopKey startStopKey = new StartStopKey(true, new object[]
								{
									context.LockedMailboxState.MailboxPartitionNumber,
									ExchangeId.CreateFromInternalShortId(context, cacheForMailbox, list[i].Fid).To26ByteArray()
								});
								array[i] = new KeyRange(startStopKey, startStopKey);
							}
							using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, folderTable.Table, folderTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 0, array, false, true))
							{
								using (Reader reader = tableOperator.ExecuteReader(false))
								{
									while (reader.Read())
									{
										ExchangeId exchangeId = ExchangeId.CreateFrom26ByteArray(context, null, reader.GetBinary(folderColumnsInformation.Table.FolderId));
										this.hierarchyFolders[exchangeId.ToExchangeShortId()].Populate(context, folderColumnsInformation, this.informationType, reader);
									}
								}
							}
						}
						finally
						{
							if (flag)
							{
								context.Commit();
							}
						}
					}
					if (parent != null)
					{
						parent.ChildrenArePopulated = true;
					}
				}
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000441F4 File Offset: 0x000423F4
		internal static FolderHierarchy FolderHierarchySnapshotFromDisk(Context context, Mailbox mailbox, FolderInformationType informationType)
		{
			return FolderHierarchy.CreateFolderHierarchy(context, mailbox, informationType);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00044200 File Offset: 0x00042400
		private Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry> InitEnumeratorStack(Context context, FolderHierarchy.FolderInformationImpl root, bool recursive, Func<Context, IFolderInformation, bool> isVisiblePredicate, ExchangeShortId startFolderId, int startFolderSortPosition, bool startFolderInclusive, FolderInformationComparer alternateComparer, out int sortPosition)
		{
			Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry> result;
			if (startFolderId.IsZero)
			{
				result = this.InitEnumeratorStackForSortPosition(context, root, recursive, isVisiblePredicate, 1, alternateComparer);
				sortPosition = 1;
			}
			else
			{
				result = this.InitEnumeratorStackForFolderId(context, root, recursive, isVisiblePredicate, startFolderId, startFolderSortPosition, startFolderInclusive, alternateComparer, out sortPosition);
			}
			return result;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00044244 File Offset: 0x00042444
		private Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry> InitEnumeratorStackForSortPosition(Context context, FolderHierarchy.FolderInformationImpl root, bool recursive, Func<Context, IFolderInformation, bool> isVisiblePredicate, int startFolderSortPosition, FolderInformationComparer alternateComparer)
		{
			Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry> stack = new Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry>(recursive ? 4 : 1);
			IList<IFolderInformation> sortedChildrenList = this.GetSortedChildrenList(context, root, alternateComparer);
			if (sortedChildrenList.Count != 0)
			{
				int num = 0;
				if (isVisiblePredicate != null)
				{
					while (!isVisiblePredicate(context, sortedChildrenList[num]))
					{
						num++;
						if (num >= sortedChildrenList.Count)
						{
							break;
						}
					}
				}
				if (num < sortedChildrenList.Count)
				{
					stack.Push(new FolderHierarchy.FolderHierarchyEnumeratorStackEntry(sortedChildrenList, num, 1));
					if (startFolderSortPosition > 1)
					{
						int num2 = 1;
						while (stack.Count != 0 && num2 != startFolderSortPosition)
						{
							this.PopFromEnumeratorStack(context, stack, recursive, isVisiblePredicate, alternateComparer);
							num2++;
						}
					}
				}
			}
			return stack;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000442D8 File Offset: 0x000424D8
		private Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry> InitEnumeratorStackForFolderId(Context context, FolderHierarchy.FolderInformationImpl root, bool recursive, Func<Context, IFolderInformation, bool> isVisiblePredicate, ExchangeShortId startFolderId, int startFolderSortPosition, bool startFolderInclusive, FolderInformationComparer alternateComparer, out int sortPosition)
		{
			sortPosition = startFolderSortPosition;
			Stack<FolderHierarchy.FolderInformationImpl> stack = null;
			FolderHierarchy.FolderInformationImpl folderInformationImpl;
			if (this.hierarchyFolders.TryGetValue(startFolderId, out folderInformationImpl))
			{
				stack = new Stack<FolderHierarchy.FolderInformationImpl>();
				if (!recursive)
				{
					if (folderInformationImpl.Parent == root)
					{
						stack.Push(folderInformationImpl);
						stack.Push(root);
					}
					else
					{
						stack = null;
					}
				}
				else
				{
					FolderHierarchy.FolderInformationImpl folderInformationImpl2 = folderInformationImpl;
					do
					{
						stack.Push(folderInformationImpl2);
						folderInformationImpl2 = folderInformationImpl2.Parent;
					}
					while (folderInformationImpl2 != null && folderInformationImpl2 != root);
					stack.Push(folderInformationImpl2);
					if (folderInformationImpl2 != root)
					{
						stack = null;
					}
				}
			}
			Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry> stack2;
			if (stack == null)
			{
				stack2 = this.InitEnumeratorStackForSortPosition(context, root, recursive, isVisiblePredicate, startFolderSortPosition, alternateComparer);
				if (!startFolderInclusive)
				{
					sortPosition++;
				}
			}
			else
			{
				FolderInformationComparer comparer = alternateComparer ?? this.folderInformationComparer;
				stack2 = new Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry>(recursive ? 4 : 1);
				int num = 1;
				bool flag = false;
				while (stack.Count > 1)
				{
					FolderHierarchy.FolderInformationImpl parent = stack.Pop();
					IList<IFolderInformation> sortedChildrenList = this.GetSortedChildrenList(context, parent, alternateComparer);
					FolderHierarchy.FolderInformationImpl folderInformationImpl3 = stack.Peek();
					int num2;
					if (sortedChildrenList is List<IFolderInformation>)
					{
						List<IFolderInformation> list = (List<IFolderInformation>)sortedChildrenList;
						num2 = list.BinarySearch(folderInformationImpl3, comparer);
					}
					else
					{
						IFolderInformation[] array = (IFolderInformation[])sortedChildrenList;
						num2 = Array.BinarySearch<IFolderInformation>(array, folderInformationImpl3, comparer);
					}
					if (num2 < 0)
					{
						for (int i = 0; i < sortedChildrenList.Count; i++)
						{
							if (object.ReferenceEquals(sortedChildrenList[i], folderInformationImpl3))
							{
								num2 = i;
								break;
							}
						}
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num2 >= 0, "the child was not found in a parent's children list? #2");
					}
					if (stack.Count > 1)
					{
						num2++;
					}
					if (isVisiblePredicate != null)
					{
						flag = false;
						while (num2 < sortedChildrenList.Count && !isVisiblePredicate(context, sortedChildrenList[num2]))
						{
							flag = true;
							num2++;
						}
					}
					if (num2 < sortedChildrenList.Count)
					{
						stack2.Push(new FolderHierarchy.FolderHierarchyEnumeratorStackEntry(sortedChildrenList, num2, num));
					}
					num++;
				}
				if (!startFolderInclusive)
				{
					if (!flag && stack2.Count != 0)
					{
						this.PopFromEnumeratorStack(context, stack2, recursive, isVisiblePredicate, alternateComparer);
					}
					sortPosition++;
				}
			}
			return stack2;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000444C4 File Offset: 0x000426C4
		private FolderHierarchy.FolderHierarchyEnumeratorStackEntry PopFromEnumeratorStack(Context context, Stack<FolderHierarchy.FolderHierarchyEnumeratorStackEntry> enumeratorStack, bool recursive, Func<Context, IFolderInformation, bool> isVisiblePredicate, FolderInformationComparer alternateComparer)
		{
			FolderHierarchy.FolderHierarchyEnumeratorStackEntry result = enumeratorStack.Pop();
			FolderHierarchy.FolderInformationImpl folderInformationImpl = (FolderHierarchy.FolderInformationImpl)result.List[result.Position];
			int num = result.Position + 1;
			if (num < result.List.Count)
			{
				if (isVisiblePredicate != null)
				{
					while (!isVisiblePredicate(context, result.List[num]))
					{
						num++;
						if (num >= result.List.Count)
						{
							break;
						}
					}
				}
				if (num < result.List.Count)
				{
					enumeratorStack.Push(new FolderHierarchy.FolderHierarchyEnumeratorStackEntry(result.List, num, result.Depth));
				}
			}
			if (recursive && folderInformationImpl.Children.Count != 0)
			{
				IList<IFolderInformation> sortedChildrenList = this.GetSortedChildrenList(context, folderInformationImpl, alternateComparer);
				num = 0;
				if (isVisiblePredicate != null)
				{
					while (!isVisiblePredicate(context, sortedChildrenList[num]))
					{
						num++;
						if (num >= sortedChildrenList.Count)
						{
							break;
						}
					}
				}
				if (num < sortedChildrenList.Count)
				{
					enumeratorStack.Push(new FolderHierarchy.FolderHierarchyEnumeratorStackEntry(sortedChildrenList, num, result.Depth + 1));
				}
			}
			return result;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000445C0 File Offset: 0x000427C0
		private IList<IFolderInformation> GetSortedChildrenList(Context context, FolderHierarchy.FolderInformationImpl parent, FolderInformationComparer alternateComparer)
		{
			if (parent != null)
			{
				this.PopulateChildren(context, parent);
			}
			IList<IFolderInformation> list = (parent == null) ? this.hierarchyRoots : parent.Children;
			if (list.Count > 1)
			{
				if (alternateComparer != null)
				{
					IFolderInformation[] array = list.ToArray<IFolderInformation>();
					Array.Sort<IFolderInformation>(array, alternateComparer);
					list = array;
				}
				else if (parent != null)
				{
					parent.SortChildrenAndCompact(this.folderInformationComparer);
					list = parent.Children;
				}
			}
			else if (parent != null)
			{
				parent.IsSorted = true;
			}
			return list;
		}

		// Token: 0x040003B4 RID: 948
		private const int InitialSizeOfRootsList = 5;

		// Token: 0x040003B5 RID: 949
		private const int InitialSizeOfDescendantStack = 5;

		// Token: 0x040003B6 RID: 950
		private static Hookable<int> maxChildrenForCompaction = Hookable<int>.Create(true, 1000);

		// Token: 0x040003B7 RID: 951
		private static readonly FolderHierarchy emptyHierarchy = new FolderHierarchy(new FolderInformationComparer(null), new Dictionary<ExchangeShortId, FolderHierarchy.FolderInformationImpl>(0), new List<IFolderInformation>(), FolderInformationType.Basic);

		// Token: 0x040003B8 RID: 952
		private static int folderHierarchyDataSlot = -1;

		// Token: 0x040003B9 RID: 953
		private readonly IDictionary<ExchangeShortId, FolderHierarchy.FolderInformationImpl> hierarchyFolders;

		// Token: 0x040003BA RID: 954
		private readonly List<IFolderInformation> hierarchyRoots;

		// Token: 0x040003BB RID: 955
		private readonly FolderInformationType informationType;

		// Token: 0x040003BC RID: 956
		private readonly FolderInformationComparer folderInformationComparer;

		// Token: 0x040003BD RID: 957
		private bool isValid;

		// Token: 0x02000051 RID: 81
		private class FolderInformationImpl : IFolderInformation
		{
			// Token: 0x06000788 RID: 1928 RVA: 0x00044660 File Offset: 0x00042860
			internal FolderInformationImpl(ExchangeShortId fid, FolderHierarchy.FolderInformationImpl parentInformation, FolderHierarchy.FolderInformationImpl.FolderInformationFlags folderFlags, int mailboxNumber, string displayName, long messageCount, SecurityDescriptor securityDescriptor)
			{
				this.parent = parentInformation;
				this.fid = fid;
				this.folderFlags = (int)folderFlags;
				this.mailboxNumber = mailboxNumber;
				this.displayName = displayName;
				this.messageCount = messageCount;
				this.securityDescriptor = securityDescriptor;
			}

			// Token: 0x06000789 RID: 1929 RVA: 0x000446B3 File Offset: 0x000428B3
			internal FolderInformationImpl(ExchangeShortId parentFid)
			{
				this.fid = parentFid;
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x0600078A RID: 1930 RVA: 0x000446CD File Offset: 0x000428CD
			public FolderHierarchy.FolderInformationImpl Parent
			{
				get
				{
					return this.parent;
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x0600078B RID: 1931 RVA: 0x000446D5 File Offset: 0x000428D5
			public ExchangeShortId Fid
			{
				get
				{
					return this.fid;
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x0600078C RID: 1932 RVA: 0x000446DD File Offset: 0x000428DD
			public ExchangeShortId ParentFid
			{
				get
				{
					if (this.parent == null)
					{
						return ExchangeShortId.Zero;
					}
					return this.parent.Fid;
				}
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x0600078D RID: 1933 RVA: 0x000446F8 File Offset: 0x000428F8
			// (set) Token: 0x0600078E RID: 1934 RVA: 0x00044700 File Offset: 0x00042900
			public int MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
				set
				{
					this.mailboxNumber = value;
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x0600078F RID: 1935 RVA: 0x00044709 File Offset: 0x00042909
			public bool IsSearchFolder
			{
				get
				{
					return this.GetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsSearchFolder);
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000790 RID: 1936 RVA: 0x00044712 File Offset: 0x00042912
			public string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06000791 RID: 1937 RVA: 0x0004471A File Offset: 0x0004291A
			// (set) Token: 0x06000792 RID: 1938 RVA: 0x00044722 File Offset: 0x00042922
			public IList<IFolderInformation> Children
			{
				get
				{
					return this.children;
				}
				set
				{
					this.children = value;
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06000793 RID: 1939 RVA: 0x0004472B File Offset: 0x0004292B
			// (set) Token: 0x06000794 RID: 1940 RVA: 0x00044733 File Offset: 0x00042933
			public SecurityDescriptor SecurityDescriptor
			{
				get
				{
					return this.securityDescriptor;
				}
				set
				{
					this.securityDescriptor = value;
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06000795 RID: 1941 RVA: 0x0004473C File Offset: 0x0004293C
			// (set) Token: 0x06000796 RID: 1942 RVA: 0x00044746 File Offset: 0x00042946
			public bool ChildrenArePopulated
			{
				get
				{
					return this.GetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.ChildrenArePopulated);
				}
				set
				{
					if (value)
					{
						this.SetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.ChildrenArePopulated);
						return;
					}
					this.RemoveFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.ChildrenArePopulated);
				}
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06000797 RID: 1943 RVA: 0x0004475C File Offset: 0x0004295C
			// (set) Token: 0x06000798 RID: 1944 RVA: 0x00044765 File Offset: 0x00042965
			public bool IsSorted
			{
				get
				{
					return this.GetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsSorted);
				}
				set
				{
					if (value)
					{
						this.SetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsSorted);
						return;
					}
					this.RemoveFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsSorted);
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000799 RID: 1945 RVA: 0x00044779 File Offset: 0x00042979
			// (set) Token: 0x0600079A RID: 1946 RVA: 0x00044782 File Offset: 0x00042982
			public bool IsPartOfContentIndexing
			{
				get
				{
					return this.GetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsPartOfContentIndexing);
				}
				set
				{
					if (value)
					{
						this.SetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsPartOfContentIndexing);
						return;
					}
					this.RemoveFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsPartOfContentIndexing);
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x0600079B RID: 1947 RVA: 0x00044796 File Offset: 0x00042996
			// (set) Token: 0x0600079C RID: 1948 RVA: 0x0004479F File Offset: 0x0004299F
			public bool IsInternalAccess
			{
				get
				{
					return this.GetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.InternalAccess);
				}
				set
				{
					if (value)
					{
						this.SetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.InternalAccess);
						return;
					}
					this.RemoveFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.InternalAccess);
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x0600079D RID: 1949 RVA: 0x000447B3 File Offset: 0x000429B3
			// (set) Token: 0x0600079E RID: 1950 RVA: 0x000447BB File Offset: 0x000429BB
			public long MessageCount
			{
				get
				{
					return this.messageCount;
				}
				set
				{
					this.messageCount = value;
				}
			}

			// Token: 0x0600079F RID: 1951 RVA: 0x000447C4 File Offset: 0x000429C4
			public static FolderHierarchy.FolderInformationImpl FromFolderId(ExchangeShortId folderId)
			{
				return new FolderHierarchy.FolderInformationImpl(folderId);
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x000447CC File Offset: 0x000429CC
			public static IList<Column> GetColumnsToFetch(FolderHierarchy.FolderInformationImpl.FolderColumnsInformation folderColumnsInformation, FolderInformationType informationType)
			{
				List<Column> list = new List<Column>(8);
				list.Add(folderColumnsInformation.Table.ParentFolderId);
				list.Add(folderColumnsInformation.Table.FolderId);
				list.Add(folderColumnsInformation.Table.DisplayName);
				list.Add(folderColumnsInformation.Table.QueryCriteria);
				list.Add(folderColumnsInformation.PartOfContentIndexingColumn);
				list.Add(folderColumnsInformation.Table.MessageCount);
				list.Add(folderColumnsInformation.MailboxNumberColumn);
				list.Add(folderColumnsInformation.InternalAccessColumn);
				if (informationType == FolderInformationType.Extended)
				{
					list.Add(folderColumnsInformation.Table.AclTableAndSecurityDescriptor);
				}
				return list;
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x00044A2C File Offset: 0x00042C2C
			public IEnumerable<ExchangeShortId> AllDescendantFolderIds()
			{
				Queue<FolderHierarchy.FolderInformationImpl> queue = new Queue<FolderHierarchy.FolderInformationImpl>(100);
				queue.Enqueue(this);
				while (queue.Count != 0)
				{
					FolderHierarchy.FolderInformationImpl fi = queue.Dequeue();
					if (fi.Children != null && fi.Children.Count != 0)
					{
						using (LockManager.Lock(fi, LockManager.LockType.LeafMonitorLock))
						{
							for (int i = 0; i < fi.Children.Count; i++)
							{
								queue.Enqueue((FolderHierarchy.FolderInformationImpl)fi.Children[i]);
							}
						}
					}
					yield return fi.Fid;
				}
				yield break;
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x00044A4C File Offset: 0x00042C4C
			public void LinkChild(FolderHierarchy.FolderInformationImpl child, FolderInformationComparer folderInformationComparer)
			{
				if (!(this.children is List<IFolderInformation>))
				{
					List<IFolderInformation> list = new List<IFolderInformation>(this.children.Count + 1);
					list.AddRange(this.children);
					this.children = list;
				}
				if (!this.IsSorted)
				{
					this.children.Add(child);
				}
				else
				{
					int num = ((List<IFolderInformation>)this.children).BinarySearch(child, folderInformationComparer);
					this.children.Insert(~num, child);
				}
				child.parent = this;
			}

			// Token: 0x060007A3 RID: 1955 RVA: 0x00044ACC File Offset: 0x00042CCC
			public void Populate(Context context, FolderHierarchy.FolderInformationImpl.FolderColumnsInformation folderColumnsInformation, FolderInformationType informationType, Reader reader)
			{
				this.displayName = (reader.GetString(folderColumnsInformation.Table.DisplayName) ?? string.Empty);
				this.messageCount = reader.GetInt64(folderColumnsInformation.Table.MessageCount);
				this.mailboxNumber = reader.GetInt32(folderColumnsInformation.MailboxNumberColumn);
				bool flag = reader.GetBinary(folderColumnsInformation.Table.QueryCriteria) != null;
				if (flag)
				{
					this.SetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsSearchFolder);
				}
				bool boolean = reader.GetBoolean(folderColumnsInformation.PartOfContentIndexingColumn);
				if (boolean)
				{
					this.SetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.IsPartOfContentIndexing);
				}
				bool? nullableBoolean = reader.GetNullableBoolean(folderColumnsInformation.InternalAccessColumn);
				if (nullableBoolean != null && nullableBoolean.Value)
				{
					this.SetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags.InternalAccess);
				}
				if (informationType == FolderInformationType.Extended)
				{
					byte[] binary = reader.GetBinary(folderColumnsInformation.Table.AclTableAndSecurityDescriptor);
					FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = AclTableHelper.Parse(context, binary);
					this.securityDescriptor = aclTableAndSecurityDescriptorProperty.SecurityDescriptor;
				}
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x00044BBC File Offset: 0x00042DBC
			public void UnlinkChild(FolderHierarchy.FolderInformationImpl child, FolderInformationComparer folderInformationComparer, bool skipCompaction)
			{
				if (!(this.children is List<IFolderInformation>))
				{
					this.children = new List<IFolderInformation>(this.children);
				}
				List<IFolderInformation> list = (List<IFolderInformation>)this.children;
				int num;
				if (this.ChildrenArePopulated)
				{
					if (!this.IsSorted)
					{
						list.Sort(folderInformationComparer);
						this.IsSorted = true;
					}
					num = list.BinarySearch(child, folderInformationComparer);
					if (num < 0)
					{
						for (int i = 0; i < list.Count; i++)
						{
							if (object.ReferenceEquals(list[i], child))
							{
								num = i;
								break;
							}
						}
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num >= 0, "the child was not found in a parent's children list? #1");
					}
				}
				else
				{
					num = list.IndexOf(child);
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num >= 0, "the child was not found in a parent's children list? #2");
				}
				list.RemoveAt(num);
				if (!skipCompaction)
				{
					if (list.Count == 0)
					{
						this.children = Array<IFolderInformation>.Empty;
						return;
					}
					if (list.Count <= FolderHierarchy.maxChildrenForCompaction.Value)
					{
						this.children = list.ToArray();
					}
				}
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x00044CAC File Offset: 0x00042EAC
			public void ChangeDisplayName(string newDisplayName, FolderInformationComparer folderInformationComparer)
			{
				if (this.parent == null || !this.parent.IsSorted)
				{
					this.displayName = newDisplayName;
					return;
				}
				FolderHierarchy.FolderInformationImpl folderInformationImpl = this.parent;
				folderInformationImpl.UnlinkChild(this, folderInformationComparer, true);
				this.displayName = newDisplayName;
				folderInformationImpl.LinkChild(this, folderInformationComparer);
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x00044CF8 File Offset: 0x00042EF8
			public void SortChildrenAndCompact(FolderInformationComparer folderInformationComparer)
			{
				if (this.children.Count <= FolderHierarchy.maxChildrenForCompaction.Value)
				{
					if (this.IsSorted && !(this.children is List<IFolderInformation>))
					{
						return;
					}
				}
				else if (this.IsSorted)
				{
					return;
				}
				using (LockManager.Lock(this, LockManager.LockType.LeafMonitorLock))
				{
					if (this.children.Count <= FolderHierarchy.maxChildrenForCompaction.Value)
					{
						if (!this.IsSorted)
						{
							if (!(this.children is List<IFolderInformation>))
							{
								this.children = new List<IFolderInformation>(this.children);
							}
							((List<IFolderInformation>)this.children).Sort(folderInformationComparer);
							this.IsSorted = true;
						}
						if (this.children is List<IFolderInformation>)
						{
							this.children = this.children.ToArray<IFolderInformation>();
						}
					}
					else if (!this.IsSorted)
					{
						((List<IFolderInformation>)this.children).Sort(folderInformationComparer);
						this.IsSorted = true;
					}
				}
			}

			// Token: 0x060007A7 RID: 1959 RVA: 0x00044DFC File Offset: 0x00042FFC
			public void SetChildren(IList<IFolderInformation> children)
			{
				this.children = children;
				for (int i = 0; i < children.Count; i++)
				{
					((FolderHierarchy.FolderInformationImpl)children[i]).parent = this;
				}
			}

			// Token: 0x060007A8 RID: 1960 RVA: 0x00044E33 File Offset: 0x00043033
			private bool GetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags flag)
			{
				return (this.folderFlags & (int)flag) != 0;
			}

			// Token: 0x060007A9 RID: 1961 RVA: 0x00044E44 File Offset: 0x00043044
			private void SetFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags flag)
			{
				int num;
				do
				{
					num = this.folderFlags;
				}
				while (num != Interlocked.CompareExchange(ref this.folderFlags, num | (int)flag, num));
			}

			// Token: 0x060007AA RID: 1962 RVA: 0x00044E6C File Offset: 0x0004306C
			private void RemoveFolderInformationFlag(FolderHierarchy.FolderInformationImpl.FolderInformationFlags flag)
			{
				int num;
				do
				{
					num = this.folderFlags;
				}
				while (num != Interlocked.CompareExchange(ref this.folderFlags, num & (int)(~(int)flag), num));
			}

			// Token: 0x040003BE RID: 958
			private readonly ExchangeShortId fid;

			// Token: 0x040003BF RID: 959
			private int folderFlags;

			// Token: 0x040003C0 RID: 960
			private int mailboxNumber;

			// Token: 0x040003C1 RID: 961
			private long messageCount;

			// Token: 0x040003C2 RID: 962
			private string displayName;

			// Token: 0x040003C3 RID: 963
			private FolderHierarchy.FolderInformationImpl parent;

			// Token: 0x040003C4 RID: 964
			private IList<IFolderInformation> children = Array<IFolderInformation>.Empty;

			// Token: 0x040003C5 RID: 965
			private SecurityDescriptor securityDescriptor;

			// Token: 0x02000052 RID: 82
			[Flags]
			internal enum FolderInformationFlags
			{
				// Token: 0x040003C7 RID: 967
				None = 0,
				// Token: 0x040003C8 RID: 968
				IsSearchFolder = 1,
				// Token: 0x040003C9 RID: 969
				IsPartOfContentIndexing = 2,
				// Token: 0x040003CA RID: 970
				IsSorted = 4,
				// Token: 0x040003CB RID: 971
				InternalAccess = 8,
				// Token: 0x040003CC RID: 972
				ChildrenArePopulated = 16
			}

			// Token: 0x02000053 RID: 83
			public struct FolderColumnsInformation
			{
				// Token: 0x170001B3 RID: 435
				// (get) Token: 0x060007AB RID: 1963 RVA: 0x00044E93 File Offset: 0x00043093
				// (set) Token: 0x060007AC RID: 1964 RVA: 0x00044E9B File Offset: 0x0004309B
				internal FolderTable Table { get; set; }

				// Token: 0x170001B4 RID: 436
				// (get) Token: 0x060007AD RID: 1965 RVA: 0x00044EA4 File Offset: 0x000430A4
				// (set) Token: 0x060007AE RID: 1966 RVA: 0x00044EAC File Offset: 0x000430AC
				internal Column PartOfContentIndexingColumn { get; set; }

				// Token: 0x170001B5 RID: 437
				// (get) Token: 0x060007AF RID: 1967 RVA: 0x00044EB5 File Offset: 0x000430B5
				// (set) Token: 0x060007B0 RID: 1968 RVA: 0x00044EBD File Offset: 0x000430BD
				internal Column MailboxNumberColumn { get; set; }

				// Token: 0x170001B6 RID: 438
				// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00044EC6 File Offset: 0x000430C6
				// (set) Token: 0x060007B2 RID: 1970 RVA: 0x00044ECE File Offset: 0x000430CE
				internal Column InternalAccessColumn { get; set; }
			}
		}

		// Token: 0x02000054 RID: 84
		private struct FolderHierarchyEnumeratorStackEntry
		{
			// Token: 0x060007B3 RID: 1971 RVA: 0x00044ED7 File Offset: 0x000430D7
			internal FolderHierarchyEnumeratorStackEntry(IList<IFolderInformation> list, int position, int depth)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(position >= 0 && position < list.Count, "Entry position is out of range");
				this.list = list;
				this.position = position;
				this.depth = depth;
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00044F08 File Offset: 0x00043108
			public IList<IFolderInformation> List
			{
				get
				{
					return this.list;
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00044F10 File Offset: 0x00043110
			public int Position
			{
				get
				{
					return this.position;
				}
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00044F18 File Offset: 0x00043118
			public int Depth
			{
				get
				{
					return this.depth;
				}
			}

			// Token: 0x040003D1 RID: 977
			private IList<IFolderInformation> list;

			// Token: 0x040003D2 RID: 978
			private int position;

			// Token: 0x040003D3 RID: 979
			private int depth;
		}
	}
}
