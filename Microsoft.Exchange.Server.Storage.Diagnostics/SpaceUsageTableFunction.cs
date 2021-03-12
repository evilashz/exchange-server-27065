using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PhysicalAccessJet;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000007 RID: 7
	public sealed class SpaceUsageTableFunction
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00004120 File Offset: 0x00002320
		internal SpaceUsageTableFunction()
		{
			this.tableName = Factory.CreatePhysicalColumn("TableName", "TableName", typeof(string), false, false, false, false, false, Visibility.Public, 0, 128, 128);
			this.mailboxNumber = Factory.CreatePhysicalColumn("MailboxNumber", "MailboxNumber", typeof(int), false, false, false, false, false, Visibility.Public, 0, 4, 4);
			this.ownedMB = Factory.CreatePhysicalColumn("OwnedMB", "OwnedMB", typeof(double), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.availableMB = Factory.CreatePhysicalColumn("AvailableMB", "AvailableMB", typeof(double), false, false, false, false, false, Visibility.Public, 0, 8, 8);
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
				this.TableName
			});
			Index[] indexes = new Index[]
			{
				index
			};
			this.tableFunction = Factory.CreateTableFunction("SpaceUsage", new TableFunction.GetTableContentsDelegate(this.GetTableContents), new TableFunction.GetColumnFromRowDelegate(this.GetColumnFromRow), Visibility.Public, new Type[0], indexes, new PhysicalColumn[]
			{
				this.TableName,
				this.MailboxNumber,
				this.OwnedMB,
				this.AvailableMB
			});
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00004283 File Offset: 0x00002483
		public TableFunction TableFunction
		{
			get
			{
				return this.tableFunction;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000428B File Offset: 0x0000248B
		public PhysicalColumn TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00004293 File Offset: 0x00002493
		public PhysicalColumn MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000429B File Offset: 0x0000249B
		public PhysicalColumn OwnedMB
		{
			get
			{
				return this.ownedMB;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000042A3 File Offset: 0x000024A3
		public PhysicalColumn AvailableMB
		{
			get
			{
				return this.availableMB;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000042BC File Offset: 0x000024BC
		public object GetTableContents(IConnectionProvider connectionProvider, object[] parameters)
		{
			if (connectionProvider.Database.DatabaseType == DatabaseType.Jet)
			{
				return from t in connectionProvider.Database.GetTableNames(connectionProvider).Union(SpaceUsageTableFunction.internalJetTables)
				select new SpaceUsageTableFunction.SpaceUsageRow(t) into s
				orderby s.TableName
				select s;
			}
			return new string[0];
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004338 File Offset: 0x00002538
		public object GetColumnFromRow(IConnectionProvider connectionProvider, object row, PhysicalColumn columnToFetch)
		{
			SpaceUsageTableFunction.SpaceUsageRow spaceUsageRow = row as SpaceUsageTableFunction.SpaceUsageRow;
			if (this.tableName != null)
			{
				if (columnToFetch == this.TableName)
				{
					return spaceUsageRow.TableName;
				}
				if (columnToFetch == this.MailboxNumber)
				{
					return spaceUsageRow.MailboxNumber;
				}
				if (columnToFetch == this.OwnedMB)
				{
					return spaceUsageRow.GetOwnedMB(connectionProvider);
				}
				if (columnToFetch == this.AvailableMB)
				{
					return spaceUsageRow.GetAvailableMB(connectionProvider);
				}
			}
			return null;
		}

		// Token: 0x0400005F RID: 95
		public const string TableNameName = "TableName";

		// Token: 0x04000060 RID: 96
		public const string MailboxNumberName = "MailboxNumber";

		// Token: 0x04000061 RID: 97
		public const string OwnedMBName = "OwnedMB";

		// Token: 0x04000062 RID: 98
		public const string AvailableMBName = "AvailableMB";

		// Token: 0x04000063 RID: 99
		public const string TableFunctionName = "SpaceUsage";

		// Token: 0x04000064 RID: 100
		private PhysicalColumn tableName;

		// Token: 0x04000065 RID: 101
		private PhysicalColumn mailboxNumber;

		// Token: 0x04000066 RID: 102
		private PhysicalColumn ownedMB;

		// Token: 0x04000067 RID: 103
		private PhysicalColumn availableMB;

		// Token: 0x04000068 RID: 104
		private TableFunction tableFunction;

		// Token: 0x04000069 RID: 105
		private static string[] internalJetTables = new string[]
		{
			null,
			"MSysDatabaseMaintenance",
			"MSysLocales",
			"MSysObjects",
			"MSysObjectsShadow",
			"MSysObjids"
		};

		// Token: 0x02000008 RID: 8
		private class SpaceUsageRow
		{
			// Token: 0x06000056 RID: 86 RVA: 0x00004404 File Offset: 0x00002604
			public SpaceUsageRow(string tableName)
			{
				this.tableName = tableName;
				this.mailboxNumber = null;
				if (this.tableName != null)
				{
					string[] array = this.tableName.Split(new char[]
					{
						'_'
					});
					int value;
					if (array.Length >= 2 && int.TryParse(array[1], out value))
					{
						this.mailboxNumber = new int?(value);
					}
				}
				this.isLoaded = false;
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x06000057 RID: 87 RVA: 0x00004470 File Offset: 0x00002670
			public string TableName
			{
				get
				{
					return this.tableName;
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000058 RID: 88 RVA: 0x00004478 File Offset: 0x00002678
			public int? MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
			}

			// Token: 0x06000059 RID: 89 RVA: 0x00004480 File Offset: 0x00002680
			public double GetOwnedMB(IConnectionProvider connectionProvider)
			{
				this.LoadSpaceUsage(connectionProvider);
				return this.ownedMB;
			}

			// Token: 0x0600005A RID: 90 RVA: 0x0000448F File Offset: 0x0000268F
			public double GetAvailableMB(IConnectionProvider connectionProvider)
			{
				this.LoadSpaceUsage(connectionProvider);
				return this.availableMB;
			}

			// Token: 0x0600005B RID: 91 RVA: 0x000044A0 File Offset: 0x000026A0
			private void LoadSpaceUsage(IConnectionProvider connectionProvider)
			{
				if (!this.isLoaded)
				{
					if (this.tableName == null)
					{
						uint num;
						uint num2;
						uint num3;
						connectionProvider.Database.GetDatabaseSize(connectionProvider, out num, out num2, out num3);
						this.ownedMB = num * num3 / 1048576.0;
						this.availableMB = num2 * num3 / 1048576.0;
					}
					else
					{
						JetConnection jetConnection = connectionProvider.GetConnection() as JetConnection;
						if (jetConnection != null)
						{
							int num4;
							int num5;
							jetConnection.GetTableSize(this.tableName, out num4, out num5);
							this.ownedMB = (double)num4 * (double)connectionProvider.Database.PageSize / 1048576.0;
							this.availableMB = (double)num5 * (double)connectionProvider.Database.PageSize / 1048576.0;
						}
					}
					this.isLoaded = true;
				}
			}

			// Token: 0x0400006C RID: 108
			private readonly string tableName;

			// Token: 0x0400006D RID: 109
			private readonly int? mailboxNumber;

			// Token: 0x0400006E RID: 110
			private bool isLoaded;

			// Token: 0x0400006F RID: 111
			private double ownedMB;

			// Token: 0x04000070 RID: 112
			private double availableMB;
		}
	}
}
