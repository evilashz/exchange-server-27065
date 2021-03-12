using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000CA RID: 202
	internal class DataTable
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x0001C9FC File Offset: 0x0001ABFC
		public DataTable()
		{
			Type type = base.GetType();
			this.name = type.Name;
			this.isNewTable = false;
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public;
			Type typeFromHandle = typeof(DataColumnDefinitionAttribute);
			int num = 0;
			int num2 = 0;
			foreach (FieldInfo fieldInfo in type.GetFields(bindingAttr))
			{
				if (fieldInfo.IsLiteral && !(fieldInfo.FieldType != typeof(int)))
				{
					if (!fieldInfo.IsPublic)
					{
						throw new InvalidOperationException();
					}
					DataColumnDefinitionAttribute[] array = (DataColumnDefinitionAttribute[])fieldInfo.GetCustomAttributes(typeFromHandle, true);
					if (array.Length != 0)
					{
						num++;
						num2 = Math.Max((int)fieldInfo.GetValue(null) + 1, num2);
					}
				}
			}
			if (num2 == 0)
			{
				throw new SchemaException(Strings.NoColumns);
			}
			if (num != num2)
			{
				throw new SchemaException(Strings.ColumnIndexesMustBeSequential);
			}
			DataColumn[] array2 = new DataColumn[num2];
			int[] array3 = new int[num2];
			foreach (FieldInfo fieldInfo2 in type.GetFields(bindingAttr))
			{
				if (fieldInfo2.IsLiteral && !(fieldInfo2.FieldType != typeof(int)))
				{
					DataColumnDefinitionAttribute[] array4 = (DataColumnDefinitionAttribute[])fieldInfo2.GetCustomAttributes(typeFromHandle, true);
					if (array4.Length != 0)
					{
						int num3 = (int)fieldInfo2.GetValue(null);
						if (array2[num3] != null)
						{
							throw new SchemaException(Strings.DuplicateColumnIndexes(array2[num3].Name, fieldInfo2.Name));
						}
						DataColumnDefinitionAttribute dataColumnDefinitionAttribute = array4[0];
						DataColumn dataColumn = DataColumn.CreateInstance(dataColumnDefinitionAttribute.ColumnType);
						dataColumn.Name = fieldInfo2.Name;
						dataColumn.Required = dataColumnDefinitionAttribute.Required;
						dataColumn.AutoIncrement = dataColumnDefinitionAttribute.AutoIncrement;
						dataColumn.AutoVersioned = dataColumnDefinitionAttribute.AutoVersioned;
						dataColumn.IntrinsicLV = dataColumnDefinitionAttribute.IntrinsicLV;
						dataColumn.MultiValued = dataColumnDefinitionAttribute.MultiValued;
						array2[num3] = dataColumn;
						if (dataColumnDefinitionAttribute.ColumnAccess == ColumnAccess.Stream)
						{
							array3[num3] = 3;
						}
						else
						{
							if (dataColumnDefinitionAttribute.ColumnAccess != ColumnAccess.CachedProp)
							{
								throw new SchemaException(Strings.ColumnAccessInvalid(fieldInfo2.Name));
							}
							dataColumn.Cached = true;
							array3[num3] = (dataColumnDefinitionAttribute.PrimaryKey ? 1 : 2);
						}
					}
				}
			}
			for (int k = 0; k < array3.Length; k++)
			{
				switch (array3[k])
				{
				case 1:
					this.KeyCount++;
					this.CacheCount++;
					break;
				case 2:
					this.CacheCount++;
					break;
				}
			}
			for (int l = 0; l < array2.Length; l++)
			{
				array2[l].CacheIndex = l;
			}
			this.allColumns = new DataTable.ColumnList(array2);
			this.defaultView = new DataTableView(this);
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001CCF5 File Offset: 0x0001AEF5
		protected int OpenCursorCount
		{
			get
			{
				return this.cursorCreationLock.CurrentReadCount;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001CD02 File Offset: 0x0001AF02
		public DataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001CD0A File Offset: 0x0001AF0A
		public IList<DataColumn> Schemas
		{
			get
			{
				return this.allColumns;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001CD12 File Offset: 0x0001AF12
		public DataTableView DefaultView
		{
			get
			{
				return this.defaultView;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001CD1A File Offset: 0x0001AF1A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001CD22 File Offset: 0x0001AF22
		public bool IsNewTable
		{
			get
			{
				return this.isNewTable;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001CD2A File Offset: 0x0001AF2A
		public bool IsAttached
		{
			get
			{
				return this.dataSource != null;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001CD38 File Offset: 0x0001AF38
		protected static void Rename(DataConnection connection, string tableName, string newTableName)
		{
			try
			{
				Api.JetRenameTable(connection.Session, connection.Database, tableName, newTableName);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001CD80 File Offset: 0x0001AF80
		public virtual DataTableCursor OpenOrCreateCursor(DataConnection connection)
		{
			DataTableCursor result = null;
			try
			{
				result = this.OpenCursorInternal(connection, false, true);
			}
			catch (EsentObjectNotFoundException)
			{
				result = this.OpenCursorInternal(connection, true, false);
			}
			return result;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001CDBC File Offset: 0x0001AFBC
		public virtual DataTableCursor OpenCursor(DataConnection connection)
		{
			try
			{
				return this.OpenCursorInternal(connection, false, false);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.dataSource))
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001CDFC File Offset: 0x0001AFFC
		public virtual DataTableCursor GetCursor()
		{
			if (this.DataSource == null)
			{
				throw new InvalidOperationException("not attached to data source.");
			}
			DataConnection dataConnection = this.DataSource.DemandNewConnection();
			DataTableCursor result = this.OpenCursor(dataConnection);
			dataConnection.Release();
			return result;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001CE38 File Offset: 0x0001B038
		public void Attach(DataSource source, DataConnection connection)
		{
			this.Attach(source, connection, this.name);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001CE48 File Offset: 0x0001B048
		public void Attach(DataSource source, DataConnection connection, string tableName)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (tableName == null)
			{
				throw new ArgumentNullException("tableName");
			}
			this.dataSource = source;
			this.dataSource.AddRef();
			this.name = tableName;
			using (Transaction transaction = connection.BeginTransaction())
			{
				using (DataTableCursor dataTableCursor = this.OpenOrCreateCursor(connection))
				{
					this.AttachColumns(dataTableCursor.Session, connection.Database, dataTableCursor.TableId, this.Name);
					this.AttachPrimaryIndex(dataTableCursor.Session, dataTableCursor.TableId);
					this.AttachLoadInitValues(transaction, dataTableCursor);
				}
				transaction.Commit();
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001CF1C File Offset: 0x0001B11C
		public void Detach()
		{
			if (this.dataSource != null)
			{
				this.dataSource.Release();
				this.dataSource = null;
				this.isNewTable = false;
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001CF40 File Offset: 0x0001B140
		public virtual bool TryDrop(DataConnection connection)
		{
			return this.TryDropInternal(connection, false);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001CF4A File Offset: 0x0001B14A
		public virtual void Drop(DataConnection connection)
		{
			this.TryDropInternal(connection, true);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001CF55 File Offset: 0x0001B155
		public void ReleaseCursor()
		{
			this.cursorCreationLock.ExitReadLock();
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001CF64 File Offset: 0x0001B164
		internal void ValidateCachedColumn(int index)
		{
			if (index >= this.Schemas.Count)
			{
				throw new ArgumentException(Strings.IncorrectColumn, "column");
			}
			if (!this.Schemas[index].Cached)
			{
				throw new ArgumentException(Strings.InvalidColumn(this.Name, index), "column");
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001CFC4 File Offset: 0x0001B1C4
		protected void AttachPrimaryIndex(JET_SESID session, JET_TABLEID table)
		{
			if (this.IsNewTable && this.KeyCount > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this.KeyCount; i++)
				{
					stringBuilder.AppendFormat("+{0}\0", this.allColumns[i].Name);
				}
				stringBuilder.Append("\0");
				string text = stringBuilder.ToString();
				try
				{
					Api.JetCreateIndex(session, table, "primary", CreateIndexGrbit.IndexUnique | CreateIndexGrbit.IndexPrimary, text, text.Length, 100);
				}
				catch (EsentErrorException ex)
				{
					if (!DataSource.HandleIsamException(ex, this.dataSource))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001D064 File Offset: 0x0001B264
		protected virtual void AttachLoadInitValues(Transaction transaction, DataTableCursor cursor)
		{
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001D066 File Offset: 0x0001B266
		protected bool TryStopOpenCursor()
		{
			return this.cursorCreationLock.TryEnterWriteLock();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001D073 File Offset: 0x0001B273
		protected void ContinueOpenCursor()
		{
			this.cursorCreationLock.ExitWriteLock();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001D080 File Offset: 0x0001B280
		protected void CopyRow(DataTableCursor cursorFrom, DataTableCursor cursorTo)
		{
			foreach (DataColumn dataColumn in this.Schemas)
			{
				dataColumn.CopyData(cursorFrom, cursorTo);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
		private bool TryDropInternal(DataConnection connection, bool throwTableInUse = false)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			try
			{
				Api.JetDeleteTable(connection.Session, connection.Database, this.Name);
				this.Detach();
			}
			catch (EsentTableInUseException ex)
			{
				if (throwTableInUse && !DataSource.HandleIsamException(ex, this.dataSource))
				{
					throw;
				}
				return false;
			}
			catch (EsentErrorException ex2)
			{
				if (!DataSource.HandleIsamException(ex2, this.dataSource))
				{
					throw;
				}
			}
			return true;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001D154 File Offset: 0x0001B354
		private DataTableCursor OpenCursorInternal(DataConnection connection, bool createTable = false, bool throwIfTableNotFound = false)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.cursorCreationLock.EnterReadLock();
			try
			{
				JET_TABLEID tableId;
				if (createTable)
				{
					this.isNewTable = true;
					Api.JetCreateTable(connection.Session, connection.Database, this.Name, 16, 100, out tableId);
				}
				else
				{
					Api.JetOpenTable(connection.Session, connection.Database, this.Name, null, 0, OpenTableGrbit.None, out tableId);
				}
				return new DataTableCursor(tableId, connection, this);
			}
			catch (EsentObjectNotFoundException ex)
			{
				if (throwIfTableNotFound || !DataSource.HandleIsamException(ex, connection.Source))
				{
					this.cursorCreationLock.ExitReadLock();
					throw;
				}
			}
			catch (EsentErrorException ex2)
			{
				if (!DataSource.HandleIsamException(ex2, connection.Source))
				{
					this.cursorCreationLock.ExitReadLock();
					throw;
				}
			}
			this.cursorCreationLock.ExitReadLock();
			return null;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001D234 File Offset: 0x0001B434
		private void AttachColumns(JET_SESID session, JET_DBID database, JET_TABLEID table, string tableName)
		{
			IEnumerable<ColumnInfo> enumerable = null;
			IDictionary<string, ColumnInfo> dictionary = new Dictionary<string, ColumnInfo>(16, StringComparer.OrdinalIgnoreCase);
			try
			{
				if (!this.isNewTable)
				{
					enumerable = Api.GetTableColumns(session, database, tableName);
					foreach (ColumnInfo columnInfo in enumerable)
					{
						dictionary[columnInfo.Name] = columnInfo;
					}
				}
				int num = 0;
				int num2 = 0;
				foreach (DataColumn dataColumn in this.allColumns)
				{
					JET_COLUMNDEF jet_COLUMNDEF = dataColumn.MakeColumnDef();
					if (this.isNewTable)
					{
						JET_COLUMNID columnId;
						Api.JetAddColumn(session, table, dataColumn.Name, jet_COLUMNDEF, null, 0, out columnId);
						dataColumn.ColumnId = columnId;
					}
					else
					{
						bool flag = false;
						ColumnInfo columnInfo2;
						if (dictionary.TryGetValue(dataColumn.Name, out columnInfo2))
						{
							flag = true;
							num2++;
							if (columnInfo2.Coltyp != jet_COLUMNDEF.coltyp)
							{
								ExTraceGlobals.ExpoTracer.TraceDebug(0L, "Column {1} on table {0} expected data type {2}. Actual data type {3}; fatal error", new object[]
								{
									this.Name,
									dataColumn.Name,
									jet_COLUMNDEF.coltyp,
									columnInfo2.Coltyp
								});
								DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SchemaTypeMismatch, null, new object[]
								{
									this.DataSource.DatabasePath,
									this.Name,
									dataColumn.Name,
									jet_COLUMNDEF.coltyp,
									columnInfo2.Coltyp
								});
								this.dataSource.HandleSchemaException(new SchemaException(Strings.SchemaTypeMismatch(jet_COLUMNDEF.coltyp, columnInfo2.Coltyp), table, dataColumn));
							}
							dataColumn.ColumnId = columnInfo2.Columnid;
						}
						if (!flag)
						{
							if (dataColumn.Required)
							{
								string text = string.Format("Non-Nullable schema column {1} not found in existing table {0}; fatal error", this.Name, dataColumn.Name);
								ExTraceGlobals.ExpoTracer.TraceDebug(0L, text);
								DataSource.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SchemaRequiredColumnNotFound, null, new object[]
								{
									this.DataSource.DatabasePath,
									this.Name,
									dataColumn.Name
								});
								EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, text, ResultSeverityLevel.Warning, false);
								this.dataSource.HandleSchemaException(new SchemaException(Strings.SchemaRequiredColumnNotFound(this.Name, dataColumn.Name), table, dataColumn));
							}
							else
							{
								ExTraceGlobals.ExpoTracer.TraceDebug<string, string>(0L, "Nullable schema column {1} not found in existing table {0}; creating column", this.Name, dataColumn.Name);
								JET_COLUMNID columnId2;
								Api.JetAddColumn(session, table, dataColumn.Name, jet_COLUMNDEF, null, 0, out columnId2);
								dataColumn.ColumnId = columnId2;
							}
						}
					}
					num++;
				}
				if (!this.dataSource.NewDatabase && num2 != dictionary.Count)
				{
					foreach (ColumnInfo columnInfo3 in enumerable)
					{
						bool flag2 = false;
						foreach (DataColumn dataColumn2 in this.allColumns)
						{
							if (string.Equals(columnInfo3.Name, dataColumn2.Name, StringComparison.OrdinalIgnoreCase))
							{
								flag2 = true;
								break;
							}
						}
						if (!flag2)
						{
							ExTraceGlobals.ExpoTracer.TraceDebug<string, string>(0L, "Column {1} from existing table {0} not found in schema; deleting column", this.Name, columnInfo3.Name);
							Api.JetDeleteColumn(session, table, columnInfo3.Name);
						}
					}
				}
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.dataSource))
				{
					throw;
				}
			}
		}

		// Token: 0x04000366 RID: 870
		private const int Density = 100;

		// Token: 0x04000367 RID: 871
		private const int GrowthInPages = 16;

		// Token: 0x04000368 RID: 872
		internal readonly int KeyCount;

		// Token: 0x04000369 RID: 873
		internal readonly int CacheCount;

		// Token: 0x0400036A RID: 874
		private readonly DataTable.UnsafeReaderWriterLock cursorCreationLock = new DataTable.UnsafeReaderWriterLock();

		// Token: 0x0400036B RID: 875
		private readonly DataTableView defaultView;

		// Token: 0x0400036C RID: 876
		private string name;

		// Token: 0x0400036D RID: 877
		private DataSource dataSource;

		// Token: 0x0400036E RID: 878
		private DataTable.ColumnList allColumns;

		// Token: 0x0400036F RID: 879
		private bool isNewTable;

		// Token: 0x020000CB RID: 203
		private class ColumnList : IList<DataColumn>, ICollection<DataColumn>, IEnumerable<DataColumn>, IEnumerable
		{
			// Token: 0x06000739 RID: 1849 RVA: 0x0001D67C File Offset: 0x0001B87C
			public ColumnList(DataColumn[] data)
			{
				this.columns = data;
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001D68B File Offset: 0x0001B88B
			public int Count
			{
				get
				{
					return this.columns.Length;
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001D695 File Offset: 0x0001B895
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170001B2 RID: 434
			public DataColumn this[int index]
			{
				get
				{
					return this.columns[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x0001D6A9 File Offset: 0x0001B8A9
			public void Add(DataColumn item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x0001D6B0 File Offset: 0x0001B8B0
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x0001D6B7 File Offset: 0x0001B8B7
			public bool Contains(DataColumn item)
			{
				return item != null && this.columns.Length > item.CacheIndex && this.columns[item.CacheIndex] == item;
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
			public void CopyTo(DataColumn[] array, int arrayIndex)
			{
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x0001D6E2 File Offset: 0x0001B8E2
			public bool Remove(DataColumn item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x0001D6E9 File Offset: 0x0001B8E9
			public int IndexOf(DataColumn item)
			{
				if (item == null)
				{
					return -1;
				}
				if (!this.Contains(item))
				{
					return -1;
				}
				return item.CacheIndex;
			}

			// Token: 0x06000744 RID: 1860 RVA: 0x0001D701 File Offset: 0x0001B901
			public void Insert(int index, DataColumn item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000745 RID: 1861 RVA: 0x0001D708 File Offset: 0x0001B908
			public void RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000746 RID: 1862 RVA: 0x0001D70F File Offset: 0x0001B90F
			public IEnumerator<DataColumn> GetEnumerator()
			{
				return new DataTable.ColumnList.Enumerator(this.columns);
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x0001D721 File Offset: 0x0001B921
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new DataTable.ColumnList.Enumerator(this.columns);
			}

			// Token: 0x04000370 RID: 880
			private DataColumn[] columns;

			// Token: 0x020000CC RID: 204
			public struct Enumerator : IEnumerator<DataColumn>, IDisposable, IEnumerator
			{
				// Token: 0x06000748 RID: 1864 RVA: 0x0001D733 File Offset: 0x0001B933
				internal Enumerator(DataColumn[] list)
				{
					this.list = list;
					this.index = 0;
					this.current = null;
				}

				// Token: 0x170001B3 RID: 435
				// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001D74A File Offset: 0x0001B94A
				public DataColumn Current
				{
					get
					{
						return this.current;
					}
				}

				// Token: 0x170001B4 RID: 436
				// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001D752 File Offset: 0x0001B952
				object IEnumerator.Current
				{
					get
					{
						if (this.index == 0 || this.index > this.list.Length + 1)
						{
							throw new InvalidOperationException(Strings.EnumeratorBadPosition);
						}
						return this.Current;
					}
				}

				// Token: 0x0600074B RID: 1867 RVA: 0x0001D784 File Offset: 0x0001B984
				public void Dispose()
				{
				}

				// Token: 0x0600074C RID: 1868 RVA: 0x0001D786 File Offset: 0x0001B986
				public bool MoveNext()
				{
					if (this.index < this.list.Length)
					{
						this.current = this.list[this.index];
						this.index++;
						return true;
					}
					this.current = null;
					return false;
				}

				// Token: 0x0600074D RID: 1869 RVA: 0x0001D7C3 File Offset: 0x0001B9C3
				void IEnumerator.Reset()
				{
					this.index = 0;
					this.current = null;
				}

				// Token: 0x04000371 RID: 881
				private DataColumn[] list;

				// Token: 0x04000372 RID: 882
				private int index;

				// Token: 0x04000373 RID: 883
				private DataColumn current;
			}
		}

		// Token: 0x020000CD RID: 205
		private class UnsafeReaderWriterLock
		{
			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001D7D3 File Offset: 0x0001B9D3
			public int CurrentReadCount
			{
				get
				{
					return this.readerCount;
				}
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x0001D7DB File Offset: 0x0001B9DB
			public bool TryEnterWriteLock()
			{
				this.openCursorEvent.Reset();
				return Interlocked.CompareExchange(ref this.readerCount, -1, 0) == 0;
			}

			// Token: 0x06000750 RID: 1872 RVA: 0x0001D7F9 File Offset: 0x0001B9F9
			public void ExitWriteLock()
			{
				Interlocked.CompareExchange(ref this.readerCount, 0, -1);
				this.openCursorEvent.Set();
			}

			// Token: 0x06000751 RID: 1873 RVA: 0x0001D818 File Offset: 0x0001BA18
			public void EnterReadLock()
			{
				int num;
				do
				{
					num = this.readerCount;
					if (num == -1)
					{
						this.openCursorEvent.WaitOne();
					}
				}
				while (Interlocked.CompareExchange(ref this.readerCount, num + 1, num) != num);
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x0001D84E File Offset: 0x0001BA4E
			public void ExitReadLock()
			{
				Interlocked.Decrement(ref this.readerCount);
			}

			// Token: 0x04000374 RID: 884
			private readonly ManualResetEvent openCursorEvent = new ManualResetEvent(true);

			// Token: 0x04000375 RID: 885
			private int readerCount;
		}
	}
}
