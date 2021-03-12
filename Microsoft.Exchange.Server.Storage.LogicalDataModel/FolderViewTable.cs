using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000055 RID: 85
	public class FolderViewTable : ViewTable
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x00044F20 File Offset: 0x00043120
		public FolderViewTable(Context context, Mailbox mailbox, ExchangeId parentFolderId, FolderViewTable.ConfigureFlags configureFlags) : this(context, mailbox, parentFolderId, configureFlags, FolderInformationType.Basic, null)
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00044F30 File Offset: 0x00043130
		public FolderViewTable(Context context, Mailbox mailbox, ExchangeId parentFolderId, FolderViewTable.ConfigureFlags configureFlags, FolderInformationType folderInformationType, Func<Context, IFolderInformation, bool> isVisiblePredicate) : base(mailbox, DatabaseSchema.FolderTable(mailbox.Database).Table)
		{
			this.parentFolderId = parentFolderId;
			this.configureFlags = configureFlags;
			this.isVisiblePredicate = isVisiblePredicate;
			this.folderInformationType = folderInformationType;
			this.folderTable = DatabaseSchema.FolderTable(mailbox.Database);
			base.SetImplicitCriteria(Factory.CreateSearchCriteriaTrue());
			this.folderHierarchyBlobTableFunction = DatabaseSchema.FolderHierarchyBlobTableFunction(base.Mailbox.Database);
			this.renameDictionary = new Dictionary<Column, Column>(5);
			this.renameDictionary.Add(this.folderTable.MailboxPartitionNumber, this.folderHierarchyBlobTableFunction.MailboxPartitionNumber);
			this.renameDictionary.Add(this.folderTable.FolderId, this.folderHierarchyBlobTableFunction.FolderId);
			this.renameDictionary.Add(this.folderTable.ParentFolderId, this.folderHierarchyBlobTableFunction.ParentFolderId);
			this.renameDictionary.Add(this.folderTable.DisplayName, this.folderHierarchyBlobTableFunction.DisplayName);
			if (UnifiedMailbox.IsReady(context, context.Database))
			{
				this.renameDictionary.Add(this.folderTable.MailboxNumber, this.folderHierarchyBlobTableFunction.MailboxNumber);
			}
			base.SortTable(this.folderHierarchyBlobTableFunction.TableFunction.PrimaryKeyIndex.SortOrder);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0004507F File Offset: 0x0004327F
		public FolderViewTable(Context context, Mailbox mailbox, ExchangeId parentFolderId, FolderViewTable.ConfigureFlags configureFlags, IList<Column> columns, SortOrder sortOrder, SearchCriteria criteria) : this(context, mailbox, parentFolderId, configureFlags)
		{
			base.SetColumns(context, columns);
			this.SortTable(sortOrder);
			this.Restrict(context, criteria);
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x000450A6 File Offset: 0x000432A6
		public bool Recursive
		{
			get
			{
				return (this.configureFlags & FolderViewTable.ConfigureFlags.Recursive) == FolderViewTable.ConfigureFlags.Recursive;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x000450B3 File Offset: 0x000432B3
		protected override Index LogicalKeyIndex
		{
			get
			{
				return this.folderHierarchyBlobTableFunction.TableFunction.PrimaryKeyIndex;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x000450C5 File Offset: 0x000432C5
		protected override bool MustUseLazyIndex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000450C8 File Offset: 0x000432C8
		public override void SortTable(SortOrder sortOrder)
		{
			if (!this.Recursive)
			{
				bool flag = sortOrder.Count == 0;
				bool flag2 = false;
				if (sortOrder.Count == 1 && sortOrder.Columns[0] == this.folderTable.DisplayName)
				{
					flag = true;
					flag2 = !sortOrder.Ascending[0];
				}
				if (flag)
				{
					sortOrder = this.folderHierarchyBlobTableFunction.TableFunction.PrimaryKeyIndex.SortOrder;
					if (flag2)
					{
						sortOrder = sortOrder.Reverse();
					}
				}
				base.SortTable(sortOrder);
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00045154 File Offset: 0x00043354
		internal byte[] GetRecursiveQueryBlobForTest(Context context)
		{
			IList<IIndex> list;
			object obj = ((SimplePseudoIndex)this.GetInScopePseudoIndexes(context, null, out list)[0]).IndexTableFunctionParameters[0];
			if (context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
			{
				IEnumerable<FolderHierarchyBlob> source = (IEnumerable<FolderHierarchyBlob>)obj;
				return FolderHierarchyBlob.Serialize(source.ToArray<FolderHierarchyBlob>());
			}
			return (byte[])obj;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000451AC File Offset: 0x000433AC
		protected internal override IList<IIndex> GetInScopePseudoIndexes(Context context, SearchCriteria findRowCriteria, out IList<IIndex> masterIndexes)
		{
			masterIndexes = null;
			object[] array = new object[1];
			FolderViewTable.HierarchyTableContentsGenerator hierarchyTableContentsGenerator = new FolderViewTable.HierarchyTableContentsGenerator(context, this);
			if (context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
			{
				array[0] = hierarchyTableContentsGenerator;
			}
			else
			{
				array[0] = FolderHierarchyBlob.Serialize(hierarchyTableContentsGenerator.ToArray<FolderHierarchyBlob>());
			}
			SimplePseudoIndex simplePseudoIndex = new SimplePseudoIndex(this.folderTable.Table, this.folderHierarchyBlobTableFunction.TableFunction, array, this.folderHierarchyBlobTableFunction.TableFunction.PrimaryKeyIndex.SortOrder, this.renameDictionary, null, true);
			return new IIndex[]
			{
				simplePseudoIndex
			};
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00045237 File Offset: 0x00043437
		protected override void BringIndexesToCurrent(Context context, IList<IIndex> indexList, DataAccessOperator queryPlan)
		{
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0004523C File Offset: 0x0004343C
		protected override IReadOnlyDictionary<Column, Column> GetColumnRenames(Context context)
		{
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(1);
			dictionary[this.folderTable.VirtualUnreadMessageCount] = Factory.CreateFunctionColumn("PerUserUnreadMessageCount", typeof(long), PropertyTypeHelper.SizeFromPropType(PropertyType.Int64), PropertyTypeHelper.MaxLengthFromPropType(PropertyType.Int64), base.Table, new Func<object[], object>(this.GetFolderUnreadCountColumnFunction), "ComputeGetUnreadMessageCount", new Column[]
			{
				this.folderTable.FolderId,
				this.folderTable.UnreadMessageCount
			});
			return dictionary;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000452C0 File Offset: 0x000434C0
		private object GetFolderUnreadCountColumnFunction(object[] columnValues)
		{
			Context currentOperationContext = base.Mailbox.CurrentOperationContext;
			ExchangeId id = ExchangeId.CreateFrom26ByteArray(currentOperationContext, base.Mailbox.ReplidGuidMap, (byte[])columnValues[0]);
			Folder folder = Folder.OpenFolder(currentOperationContext, base.Mailbox, id);
			long num = (long)columnValues[1];
			if (folder == null)
			{
				return num;
			}
			return folder.GetUnreadMessageCount(currentOperationContext, num);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00045324 File Offset: 0x00043524
		private IEnumerable<FolderHierarchyBlob> GetTableContents(Context context, bool backwards, StartStopKey startKey)
		{
			FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, base.Mailbox, this.parentFolderId.ToExchangeShortId(), this.folderInformationType);
			IFolderInformation root = this.parentFolderId.IsNullOrZero ? null : folderHierarchy.Find(context, this.parentFolderId.ToExchangeShortId());
			ExchangeId exchangeId = ExchangeId.Null;
			int startFolderSortPosition = 0;
			bool startFolderInclusive = true;
			if (!backwards && base.SortOrder.Count == 2 && base.SortOrder.Columns[0] == this.folderHierarchyBlobTableFunction.TableFunction.PrimaryKeyIndex.SortOrder.Columns[0] && base.SortOrder.Columns[1] == this.folderHierarchyBlobTableFunction.TableFunction.PrimaryKeyIndex.SortOrder.Columns[1] && !startKey.IsEmpty)
			{
				startFolderSortPosition = (int)startKey.Values[0];
				exchangeId = ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, (byte[])startKey.Values[1]);
				startFolderInclusive = startKey.Inclusive;
			}
			IEnumerable<FolderHierarchyBlob> enumerable = folderHierarchy.SerializeRecursiveHierarchyBlob(context, base.Mailbox, root, this.Recursive, this.isVisiblePredicate, exchangeId.ToExchangeShortId(), startFolderSortPosition, startFolderInclusive);
			if (backwards)
			{
				IEnumerator<FolderHierarchyBlob> enumerator = enumerable.GetEnumerator();
				Stack<FolderHierarchyBlob> stack = new Stack<FolderHierarchyBlob>();
				while (enumerator.MoveNext())
				{
					FolderHierarchyBlob item = enumerator.Current;
					stack.Push(item);
				}
				enumerable = stack;
			}
			return enumerable;
		}

		// Token: 0x040003D4 RID: 980
		private readonly FolderViewTable.ConfigureFlags configureFlags;

		// Token: 0x040003D5 RID: 981
		private readonly FolderHierarchyBlobTableFunction folderHierarchyBlobTableFunction;

		// Token: 0x040003D6 RID: 982
		private readonly Dictionary<Column, Column> renameDictionary;

		// Token: 0x040003D7 RID: 983
		private readonly FolderTable folderTable;

		// Token: 0x040003D8 RID: 984
		private readonly ExchangeId parentFolderId;

		// Token: 0x040003D9 RID: 985
		private readonly Func<Context, IFolderInformation, bool> isVisiblePredicate;

		// Token: 0x040003DA RID: 986
		private readonly FolderInformationType folderInformationType;

		// Token: 0x040003DB RID: 987
		protected SearchCriteria viewCriteria;

		// Token: 0x02000056 RID: 86
		[Flags]
		public enum ConfigureFlags
		{
			// Token: 0x040003DD RID: 989
			None = 0,
			// Token: 0x040003DE RID: 990
			NeedOnlyFolderList = 2,
			// Token: 0x040003DF RID: 991
			Recursive = 4,
			// Token: 0x040003E0 RID: 992
			NoNotifications = 16,
			// Token: 0x040003E1 RID: 993
			EmptyTable = 32,
			// Token: 0x040003E2 RID: 994
			SuppressNotifications = 128,
			// Token: 0x040003E3 RID: 995
			UseChangeNumberIndex = 256
		}

		// Token: 0x02000057 RID: 87
		private class HierarchyTableContentsGenerator : IEnumerable<FolderHierarchyBlob>, IEnumerable, IConfigurableTableContents
		{
			// Token: 0x060007C4 RID: 1988 RVA: 0x000454C8 File Offset: 0x000436C8
			public HierarchyTableContentsGenerator(Context context, FolderViewTable view)
			{
				this.context = context;
				this.view = view;
			}

			// Token: 0x060007C5 RID: 1989 RVA: 0x000454DE File Offset: 0x000436DE
			public void Configure(bool backwards, StartStopKey startKey)
			{
				this.backwards = backwards;
				this.startKey = startKey;
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x000454F0 File Offset: 0x000436F0
			public IEnumerator<FolderHierarchyBlob> GetEnumerator()
			{
				IEnumerable<FolderHierarchyBlob> tableContents = this.view.GetTableContents(this.context, this.backwards, this.startKey);
				return tableContents.GetEnumerator();
			}

			// Token: 0x060007C7 RID: 1991 RVA: 0x00045521 File Offset: 0x00043721
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040003E4 RID: 996
			private Context context;

			// Token: 0x040003E5 RID: 997
			private FolderViewTable view;

			// Token: 0x040003E6 RID: 998
			private bool backwards;

			// Token: 0x040003E7 RID: 999
			private StartStopKey startKey;
		}
	}
}
