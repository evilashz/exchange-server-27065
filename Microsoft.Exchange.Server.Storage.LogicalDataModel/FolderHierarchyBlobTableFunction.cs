using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200000A RID: 10
	public sealed class FolderHierarchyBlobTableFunction
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000053F8 File Offset: 0x000035F8
		internal FolderHierarchyBlobTableFunction()
		{
			this.mailboxPartitionNumber = Factory.CreatePhysicalColumn("MailboxPartitionNumber", "MailboxPartitionNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.parentFolderId = Factory.CreatePhysicalColumn("ParentFolderId", "ParentFolderId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.folderId = Factory.CreatePhysicalColumn("FolderId", "FolderId", typeof(byte[]), false, false, false, false, false, Visibility.Public, 0, 26, 26);
			this.displayName = Factory.CreatePhysicalColumn("DisplayName", "DisplayName", typeof(string), false, false, false, false, false, Visibility.Public, 512, 0, 512);
			this.depth = Factory.CreatePhysicalColumn("Depth", "Depth", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.sortPosition = Factory.CreatePhysicalColumn("SortPosition", "SortPosition", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			string name = "PrimaryKey";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[2];
			Index index = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true,
				true
			}, new PhysicalColumn[]
			{
				this.SortPosition,
				this.FolderId
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("FolderHierarchyBlob", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Redacted, new Type[]
			{
				typeof(IEnumerable<FolderHierarchyBlob>)
			}, indexes, new PhysicalColumn[]
			{
				this.MailboxPartitionNumber,
				this.MailboxNumber,
				this.ParentFolderId,
				this.FolderId,
				this.DisplayName,
				this.Depth,
				this.SortPosition
			});
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00005615 File Offset: 0x00003815
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000561D File Offset: 0x0000381D
		public PhysicalColumn MailboxPartitionNumber
		{
			get
			{
				return this.mailboxPartitionNumber;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00005625 File Offset: 0x00003825
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000562D File Offset: 0x0000382D
		public PhysicalColumn ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00005635 File Offset: 0x00003835
		public PhysicalColumn FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000563D File Offset: 0x0000383D
		public PhysicalColumn DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00005645 File Offset: 0x00003845
		public PhysicalColumn Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000564D File Offset: 0x0000384D
		public PhysicalColumn SortPosition
		{
			get
			{
				return this.sortPosition;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005655 File Offset: 0x00003855
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			return parameters[0];
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000565C File Offset: 0x0000385C
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			FolderHierarchyBlob folderHierarchyBlob = (FolderHierarchyBlob)row;
			if (columnToFetch == this.MailboxPartitionNumber)
			{
				return folderHierarchyBlob.MailboxPartitionNumber;
			}
			if (columnToFetch == this.MailboxNumber)
			{
				return folderHierarchyBlob.MailboxNumber;
			}
			if (columnToFetch == this.ParentFolderId)
			{
				return folderHierarchyBlob.ParentFolderId;
			}
			if (columnToFetch == this.FolderId)
			{
				return folderHierarchyBlob.FolderId;
			}
			if (columnToFetch == this.DisplayName)
			{
				return folderHierarchyBlob.DisplayName;
			}
			if (columnToFetch == this.Depth)
			{
				return folderHierarchyBlob.Depth;
			}
			if (columnToFetch == this.SortPosition)
			{
				return folderHierarchyBlob.SortPosition;
			}
			return null;
		}

		// Token: 0x04000097 RID: 151
		public const string MailboxPartitionNumberName = "MailboxPartitionNumber";

		// Token: 0x04000098 RID: 152
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x04000099 RID: 153
		public const string ParentFolderIdName = "ParentFolderId";

		// Token: 0x0400009A RID: 154
		public const string FolderIdName = "FolderId";

		// Token: 0x0400009B RID: 155
		public const string DisplayNameName = "DisplayName";

		// Token: 0x0400009C RID: 156
		public const string DepthName = "Depth";

		// Token: 0x0400009D RID: 157
		public const string SortPositionName = "SortPosition";

		// Token: 0x0400009E RID: 158
		public const string TableFunctionName = "FolderHierarchyBlob";

		// Token: 0x0400009F RID: 159
		private PhysicalColumn mailboxPartitionNumber;

		// Token: 0x040000A0 RID: 160
		private PhysicalColumn mailboxNumber;

		// Token: 0x040000A1 RID: 161
		private PhysicalColumn parentFolderId;

		// Token: 0x040000A2 RID: 162
		private PhysicalColumn folderId;

		// Token: 0x040000A3 RID: 163
		private PhysicalColumn displayName;

		// Token: 0x040000A4 RID: 164
		private PhysicalColumn depth;

		// Token: 0x040000A5 RID: 165
		private PhysicalColumn sortPosition;

		// Token: 0x040000A6 RID: 166
		private TableFunction tableFunction;
	}
}
