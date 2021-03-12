using System;
using System.Collections;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x0200000D RID: 13
	public sealed class IndexExplosionTableFunctionTableFunction
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00003A08 File Offset: 0x00001C08
		internal IndexExplosionTableFunctionTableFunction()
		{
			this.instanceNum = Factory.CreatePhysicalColumn("InstanceNum", "InstanceNum", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
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
				this.InstanceNum
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("IndexExplosionTableFunction", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Public, new Type[]
			{
				typeof(Array)
			}, indexes, new PhysicalColumn[]
			{
				this.InstanceNum
			});
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003ADF File Offset: 0x00001CDF
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003AE7 File Offset: 0x00001CE7
		public PhysicalColumn InstanceNum
		{
			get
			{
				return this.instanceNum;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003AEF File Offset: 0x00001CEF
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			return this.GetTableContentsReally(parameters);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003C70 File Offset: 0x00001E70
		public IEnumerable GetTableContentsReally(object[] parameters)
		{
			if (parameters[0] == null)
			{
				yield return 1;
			}
			else
			{
				Array arrayOfValues = (Array)parameters[0];
				if (arrayOfValues.Length == 0)
				{
					yield return 1;
				}
				else
				{
					for (int i = 0; i < arrayOfValues.Length; i++)
					{
						yield return i + 1;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003C94 File Offset: 0x00001E94
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			if (columnToFetch == this.InstanceNum)
			{
				return (int)row;
			}
			return null;
		}

		// Token: 0x04000045 RID: 69
		public const string InstanceNumName = "InstanceNum";

		// Token: 0x04000046 RID: 70
		public const string TableFunctionName = "IndexExplosionTableFunction";

		// Token: 0x04000047 RID: 71
		private PhysicalColumn instanceNum;

		// Token: 0x04000048 RID: 72
		private TableFunction tableFunction;
	}
}
