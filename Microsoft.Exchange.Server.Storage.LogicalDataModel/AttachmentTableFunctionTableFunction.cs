using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.AttachmentBlob;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000002 RID: 2
	public sealed class AttachmentTableFunctionTableFunction
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal AttachmentTableFunctionTableFunction()
		{
			this.attachmentNumber = Factory.CreatePhysicalColumn("AttachmentNumber", "AttachmentNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.inid = Factory.CreatePhysicalColumn("Inid", "Inid", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			string name = "PrimaryKey";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[1];
			Index index = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.AttachmentNumber
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("AttachmentTableFunction", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Redacted, new Type[]
			{
				typeof(byte[])
			}, indexes, new PhysicalColumn[]
			{
				this.AttachmentNumber,
				this.Inid
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000021D9 File Offset: 0x000003D9
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000021E1 File Offset: 0x000003E1
		public PhysicalColumn AttachmentNumber
		{
			get
			{
				return this.attachmentNumber;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021E9 File Offset: 0x000003E9
		public PhysicalColumn Inid
		{
			get
			{
				return this.inid;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021F4 File Offset: 0x000003F4
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			if (parameters != null)
			{
				return AttachmentBlob.Deserialize((byte[])parameters[0], true);
			}
			return Enumerable.Empty<KeyValuePair<int, long>>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000221C File Offset: 0x0000041C
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			KeyValuePair<int, long> keyValuePair = (KeyValuePair<int, long>)row;
			if (columnToFetch == this.AttachmentNumber)
			{
				return keyValuePair.Key;
			}
			if (columnToFetch == this.Inid)
			{
				return keyValuePair.Value;
			}
			return null;
		}

		// Token: 0x04000001 RID: 1
		public const string AttachmentNumberName = "AttachmentNumber";

		// Token: 0x04000002 RID: 2
		public const string InidName = "Inid";

		// Token: 0x04000003 RID: 3
		public const string TableFunctionName = "AttachmentTableFunction";

		// Token: 0x04000004 RID: 4
		private PhysicalColumn attachmentNumber;

		// Token: 0x04000005 RID: 5
		private PhysicalColumn inid;

		// Token: 0x04000006 RID: 6
		private TableFunction tableFunction;
	}
}
