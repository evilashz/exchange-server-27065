using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x0200002D RID: 45
	public sealed class PhysicalIndex
	{
		// Token: 0x0600023E RID: 574 RVA: 0x00013931 File Offset: 0x00011B31
		private PhysicalIndex()
		{
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00013939 File Offset: 0x00011B39
		public int PhysicalIndexNumber
		{
			get
			{
				return this.physicalIndexNumber;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00013941 File Offset: 0x00011B41
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00013949 File Offset: 0x00011B49
		private string IndexName
		{
			get
			{
				return this.indexName;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00013951 File Offset: 0x00011B51
		private string TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00013959 File Offset: 0x00011B59
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00013961 File Offset: 0x00011B61
		public long CreationTimeStamp { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0001396A File Offset: 0x00011B6A
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00013972 File Offset: 0x00011B72
		public Connection ConnectionLimitVisibility
		{
			get
			{
				return this.connectionLimitVisibility;
			}
			set
			{
				this.connectionLimitVisibility = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0001397B File Offset: 0x00011B7B
		internal int IdentityColumnIndex
		{
			get
			{
				return (int)this.identityColumnIndex;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00013983 File Offset: 0x00011B83
		public static int MaxSortColumnLength(Type type)
		{
			return SortColumn.MaxSortColumnLength(type);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0001398B File Offset: 0x00011B8B
		public static int MaxSortColumnLength(PropertyType propType)
		{
			return SortColumn.MaxSortColumnLength(propType);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00013993 File Offset: 0x00011B93
		public static void AppendColumnName(int columnNumber, StringBuilder sb)
		{
			if (columnNumber < PhysicalIndex.columnNames.Length)
			{
				sb.Append(PhysicalIndex.columnNames[columnNumber]);
				return;
			}
			Statistics.Unsorted.MaxPIColumnIndex.Bump(columnNumber);
			sb.Append("C");
			sb.Append(columnNumber);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000139CD File Offset: 0x00011BCD
		public static string GetColumnName(int columnNumber)
		{
			if (columnNumber < PhysicalIndex.columnNames.Length)
			{
				return PhysicalIndex.columnNames[columnNumber];
			}
			Statistics.Unsorted.MaxPIColumnIndex.Bump(columnNumber);
			return "C" + columnNumber.ToString();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000139FD File Offset: 0x00011BFD
		public static string GetTableName(int physicalIndexNumber)
		{
			return "pi" + physicalIndexNumber.ToString();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00013A10 File Offset: 0x00011C10
		public bool IndexIsVisibleForConnection(Connection connection)
		{
			return this.connectionLimitVisibility == null || object.ReferenceEquals(this.connectionLimitVisibility, connection);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00013A28 File Offset: 0x00011C28
		public CultureInfo GetCulture()
		{
			return CultureHelper.TranslateLcid(this.lcid);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00013A35 File Offset: 0x00011C35
		public Column GetColumn(int columnNumber)
		{
			return this.Table.Columns[columnNumber];
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00013A48 File Offset: 0x00011C48
		public int GetColumnCount()
		{
			return this.Table.Columns.Count;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00013A5A File Offset: 0x00011C5A
		public void AppendPiName(StringBuilder sb)
		{
			sb.Append("[Exchange].[");
			sb.Append(this.tableName);
			sb.Append("]");
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00013A84 File Offset: 0x00011C84
		internal static PhysicalIndex CreatePhysicalIndex(Context context, int keyColumnCount, short identityColumnIndex, PropertyType[] columnTypes, int[] columnMaxLengths, bool[] columnFixedLengths, bool[] columnAscendings)
		{
			PhysicalIndex physicalIndex = null;
			for (int i = 0; i < keyColumnCount; i++)
			{
			}
			PseudoIndexDefinitionTable pseudoIndexDefinitionTable = DatabaseSchema.PseudoIndexDefinitionTable(context.Database);
			IndexDefinitionBlob[] array = new IndexDefinitionBlob[columnTypes.Length];
			for (int j = 0; j < array.Length; j++)
			{
				int num = (int)columnTypes[j];
				if (columnTypes[j] == PropertyType.Unicode && j < keyColumnCount)
				{
					num |= int.MinValue;
				}
				array[j] = new IndexDefinitionBlob(num, columnMaxLengths[j], columnFixedLengths[j], columnAscendings[j]);
			}
			byte[] value = IndexDefinitionBlob.Serialize(keyColumnCount, CultureHelper.GetLcidFromCulture(context.Culture), identityColumnIndex, array);
			using (DataRow dataRow = Factory.CreateDataRow(context.Culture, context, pseudoIndexDefinitionTable.Table, true, new ColumnValue[]
			{
				new ColumnValue(pseudoIndexDefinitionTable.ColumnBlob, value)
			}))
			{
				dataRow.Flush(context);
				physicalIndex = new PhysicalIndex();
				physicalIndex.PopulateFromDataRow(context, dataRow, pseudoIndexDefinitionTable);
			}
			physicalIndex.Table.CreateTable(context, 0);
			return physicalIndex;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00013B98 File Offset: 0x00011D98
		internal static PhysicalIndex GetPhysicalIndex(Context context, int physicalIndexNumber)
		{
			PhysicalIndex physicalIndex = new PhysicalIndex();
			if (!physicalIndex.Configure(context, physicalIndexNumber))
			{
				physicalIndex = null;
			}
			return physicalIndex;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00013BB8 File Offset: 0x00011DB8
		internal bool Ascending(int colNumber)
		{
			return this.indexDefinitionEntries[colNumber].Ascending;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00013BCC File Offset: 0x00011DCC
		internal bool IndexMatch(int lcid, int keyColumnCount, int identityColumnIndex, PropertyType[] columnTypes, int[] columnMaxLengths, bool[] columnFixedLengths, bool[] columnAscendings, bool permitReverseOrder)
		{
			bool result = true;
			bool flag = permitReverseOrder && this.indexDefinitionEntries[0].Ascending != columnAscendings[0];
			if (this.keyColumnCount != keyColumnCount)
			{
				result = false;
			}
			else if ((int)this.identityColumnIndex != identityColumnIndex)
			{
				result = false;
			}
			else if (this.indexDefinitionEntries.Length != columnTypes.Length)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < this.indexDefinitionEntries.Length; i++)
				{
					bool flag2 = (this.indexDefinitionEntries[i].ColumnType & int.MinValue) != 0;
					if ((PropertyType)this.indexDefinitionEntries[i].ColumnType != columnTypes[i] || this.indexDefinitionEntries[i].FixedLength != columnFixedLengths[i] || this.indexDefinitionEntries[i].MaxLength != columnMaxLengths[i])
					{
						result = false;
						break;
					}
					if (i < keyColumnCount)
					{
						if ((!flag && this.indexDefinitionEntries[i].Ascending != columnAscendings[i]) || (flag && this.indexDefinitionEntries[i].Ascending == columnAscendings[i]))
						{
							result = false;
							break;
						}
						if ((this.lcid != lcid || !flag2) && (ushort)this.indexDefinitionEntries[i].ColumnType == 31)
						{
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00013D18 File Offset: 0x00011F18
		private bool Configure(Context context, int physicalIndexNumber)
		{
			this.tableName = PhysicalIndex.GetTableName(physicalIndexNumber);
			this.indexName = this.tableName + "PK";
			PseudoIndexDefinitionTable pseudoIndexDefinitionTable = DatabaseSchema.PseudoIndexDefinitionTable(context.Database);
			using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, pseudoIndexDefinitionTable.Table, true, new ColumnValue[]
			{
				new ColumnValue(pseudoIndexDefinitionTable.PhysicalIndexNumber, physicalIndexNumber)
			}))
			{
				if (dataRow != null)
				{
					this.PopulateFromDataRow(context, dataRow, pseudoIndexDefinitionTable);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00013DBC File Offset: 0x00011FBC
		private void PopulateFromDataRow(Context context, DataRow dataRow, PseudoIndexDefinitionTable pseudoIndexDefinitionTable)
		{
			this.physicalIndexNumber = (int)dataRow.GetValue(context, pseudoIndexDefinitionTable.PhysicalIndexNumber);
			this.tableName = PhysicalIndex.GetTableName(this.physicalIndexNumber);
			this.indexName = this.tableName + "PK";
			byte[] theBlob = (byte[])dataRow.GetValue(context, pseudoIndexDefinitionTable.ColumnBlob);
			this.indexDefinitionEntries = IndexDefinitionBlob.Deserialize(out this.keyColumnCount, out this.lcid, out this.identityColumnIndex, theBlob);
			this.table = context.Database.PhysicalDatabase.GetTableMetadata(this.TableName);
			if (this.table == null)
			{
				PhysicalColumn[] array = new PhysicalColumn[this.indexDefinitionEntries.Length];
				PhysicalColumn[] array2 = new PhysicalColumn[this.keyColumnCount];
				bool[] array3 = new bool[this.keyColumnCount];
				bool[] array4 = new bool[this.keyColumnCount];
				Index index = null;
				for (int i = 0; i < this.indexDefinitionEntries.Length; i++)
				{
					int num = 0;
					int num2 = 0;
					bool flag = i < this.keyColumnCount;
					PropertyType propType = (PropertyType)this.indexDefinitionEntries[i].ColumnType;
					if (!this.indexDefinitionEntries[i].FixedLength)
					{
						num = this.indexDefinitionEntries[i].MaxLength;
					}
					else
					{
						num2 = this.indexDefinitionEntries[i].MaxLength;
					}
					bool flag2 = i == (int)this.identityColumnIndex;
					string columnName = PhysicalIndex.GetColumnName(i);
					array[i] = Factory.CreatePhysicalColumn(columnName, columnName, PropertyTypeHelper.ClrTypeFromPropType(propType), !flag2, flag2, false, false, Visibility.Public, num, num2, Math.Max(num, num2));
					if (flag)
					{
						array2[i] = array[i];
						array4[i] = this.indexDefinitionEntries[i].Ascending;
						array3[i] = false;
					}
					if (flag2 && context.Database.PhysicalDatabase.DatabaseType == DatabaseType.Jet && i != 2)
					{
						PhysicalColumn[] array5 = new PhysicalColumn[3];
						bool[] array6 = new bool[3];
						bool[] array7 = new bool[3];
						int j;
						for (j = 0; j < 2; j++)
						{
							array5[j] = array2[j];
							array6[j] = array4[j];
							array7[j] = false;
						}
						array5[j] = array[i];
						array6[j] = true;
						array7[j] = false;
						index = new Index(array[i].Name + " Identity Column Secondary Index", false, true, false, array7, array6, array5);
					}
				}
				Index index2 = new Index(this.IndexName, true, true, false, array3, array4, array2);
				SpecialColumns specialCols = default(SpecialColumns);
				specialCols.NumberOfPartioningColumns = 2;
				int num3 = (index != null) ? 2 : 1;
				Index[] array8 = new Index[num3];
				array8[0] = index2;
				if (index != null)
				{
					array8[1] = index;
				}
				this.table = Factory.CreateTable(this.TableName, TableClass.LazyIndex, this.GetCulture(), false, TableAccessHints.ForwardScan, false, Visibility.Redacted, false, specialCols, array8, Table.NoColumns, array);
				context.Database.PhysicalDatabase.AddTableMetadata(this.table);
			}
		}

		// Token: 0x04000149 RID: 329
		private const string ClusteringKeySuffix = "PK";

		// Token: 0x0400014A RID: 330
		public const string IndexNamePrefix = "pi";

		// Token: 0x0400014B RID: 331
		public const int LogicalIndexKeyPrefixLength = 2;

		// Token: 0x0400014C RID: 332
		public const string MailboxPartitionNumberColumnName = "MailboxPartitionNumber";

		// Token: 0x0400014D RID: 333
		public const string LogicalIndexNumberColumnName = "LogicalIndexNumber";

		// Token: 0x0400014E RID: 334
		private const int UseLinguisticCasingRulesFlag = -2147483648;

		// Token: 0x0400014F RID: 335
		private static string[] columnNames = new string[]
		{
			"MailboxPartitionNumber",
			"LogicalIndexNumber",
			"C1",
			"C2",
			"C3",
			"C4",
			"C5",
			"C6",
			"C7",
			"C8",
			"C9",
			"C10",
			"C11",
			"C12",
			"C13",
			"C14",
			"C15",
			"C16",
			"C17",
			"C18",
			"C19",
			"C20",
			"C21",
			"C22",
			"C23",
			"C24",
			"C25",
			"C26",
			"C27",
			"C28",
			"C29",
			"C30",
			"C31",
			"C32",
			"C33",
			"C34",
			"C35",
			"C36",
			"C37",
			"C38",
			"C39",
			"C40",
			"C41",
			"C42",
			"C43",
			"C44",
			"C45",
			"C46",
			"C47",
			"C48",
			"C49",
			"C50",
			"C51",
			"C52",
			"C53",
			"C54",
			"C55",
			"C56",
			"C57",
			"C58",
			"C59",
			"C60",
			"C61",
			"C62",
			"C63",
			"C64",
			"C65",
			"C66",
			"C67",
			"C68",
			"C69",
			"C70",
			"C71",
			"C72",
			"C73",
			"C74",
			"C75",
			"C76",
			"C77",
			"C78",
			"C79",
			"C80",
			"C81",
			"C82",
			"C83",
			"C84",
			"C85",
			"C86",
			"C87",
			"C88",
			"C89",
			"C90",
			"C91",
			"C92",
			"C93",
			"C94",
			"C95",
			"C96",
			"C97",
			"C98",
			"C99"
		};

		// Token: 0x04000150 RID: 336
		private int keyColumnCount;

		// Token: 0x04000151 RID: 337
		private int lcid;

		// Token: 0x04000152 RID: 338
		private IndexDefinitionBlob[] indexDefinitionEntries;

		// Token: 0x04000153 RID: 339
		private short identityColumnIndex;

		// Token: 0x04000154 RID: 340
		private int physicalIndexNumber;

		// Token: 0x04000155 RID: 341
		private Table table;

		// Token: 0x04000156 RID: 342
		private string indexName;

		// Token: 0x04000157 RID: 343
		private string tableName;

		// Token: 0x04000158 RID: 344
		private Connection connectionLimitVisibility;
	}
}
