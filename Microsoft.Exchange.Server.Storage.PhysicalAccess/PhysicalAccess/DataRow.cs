using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000080 RID: 128
	public abstract class DataRow : DisposableBase
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x0001A184 File Offset: 0x00018384
		private DataRow(CultureInfo culture, Table table, bool newItem, bool writeThrough)
		{
			this.table = table;
			this.culture = culture;
			this.state = (newItem ? DataRow.DataRowState.New : DataRow.DataRowState.Existing);
			this.writeThrough = writeThrough;
			this.objects = new object[this.table.Columns.Count];
			this.primaryKey = new object[this.table.PrimaryKeyIndex.ColumnCount];
			this.dirtyColumn = new BitArray(this.table.Columns.Count, false);
			this.fetched = new BitArray(this.table.Columns.Count, newItem);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001A228 File Offset: 0x00018428
		protected DataRow(DataRow.CreateDataRowFlag createFlag, CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, ColumnValue[] initialValues) : this(culture, table, true, writeThrough)
		{
			foreach (ColumnValue columnValue in initialValues)
			{
				PhysicalColumn physicalColumn = (PhysicalColumn)columnValue.Column;
				int num = table.PrimaryKeyIndex.PositionInIndex(physicalColumn);
				if (num >= 0)
				{
					this.primaryKey[num] = columnValue.Value;
				}
				this.dirtyColumn[physicalColumn.Index] = true;
				this.objects[physicalColumn.Index] = columnValue.Value;
				this.fetched[physicalColumn.Index] = true;
			}
			this.SetDirtyFlag(connectionProvider);
			for (int j = 0; j < table.Columns.Count; j++)
			{
				PhysicalColumn physicalColumn2 = table.Columns[j];
				if (physicalColumn2.StreamSupport && !physicalColumn2.IsNullable)
				{
					this.SetValue(connectionProvider, physicalColumn2, Array<byte>.Empty);
				}
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001A31D File Offset: 0x0001851D
		protected DataRow(DataRow.OpenDataRowFlag openFlag, CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, ColumnValue[] primaryKeyValues) : this(culture, table, false, writeThrough)
		{
			this.SetPrimaryKey(primaryKeyValues);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001A334 File Offset: 0x00018534
		protected DataRow(DataRow.OpenDataRowFlag openFlag, CultureInfo culture, IConnectionProvider connectionProvider, Table table, bool writeThrough, Reader reader) : this(culture, table, false, writeThrough)
		{
			List<Column> list = new List<Column>(this.table.Columns.Count);
			list.AddRange(this.table.CommonColumns);
			for (int i = 0; i < this.table.PrimaryKeyIndex.Columns.Count; i++)
			{
				Column item = this.table.PrimaryKeyIndex.Columns[i];
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			this.LoadFromReader(reader, list);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001A3C4 File Offset: 0x000185C4
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001A3CC File Offset: 0x000185CC
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001A3D4 File Offset: 0x000185D4
		public bool IsNew
		{
			get
			{
				return this.state == DataRow.DataRowState.New;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001A3DF File Offset: 0x000185DF
		public bool IsExisting
		{
			get
			{
				return this.state == DataRow.DataRowState.Existing;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001A3EA File Offset: 0x000185EA
		public bool IsDisconnected
		{
			get
			{
				return this.state == DataRow.DataRowState.Disconnected;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001A3F5 File Offset: 0x000185F5
		public bool IsDead
		{
			get
			{
				return this.state == DataRow.DataRowState.Dead;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001A400 File Offset: 0x00018600
		public bool IsDirty
		{
			get
			{
				return this.dirty;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001A408 File Offset: 0x00018608
		public bool WriteThrough
		{
			get
			{
				return this.writeThrough;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001A410 File Offset: 0x00018610
		public bool CacheDiscardedForTest
		{
			get
			{
				return this.cacheDiscarded;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001A418 File Offset: 0x00018618
		public int DirtyColumnCount
		{
			get
			{
				int num = 0;
				foreach (object obj in this.dirtyColumn)
				{
					bool flag = (bool)obj;
					if (flag)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001A474 File Offset: 0x00018674
		protected object[] PrimaryKey
		{
			get
			{
				return this.primaryKey;
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001A47C File Offset: 0x0001867C
		public void SetPrimaryKey(params ColumnValue[] primaryKeyValues)
		{
			foreach (ColumnValue columnValue in primaryKeyValues)
			{
				PhysicalColumn physicalColumn = (PhysicalColumn)columnValue.Column;
				int num = this.table.PrimaryKeyIndex.PositionInIndex(physicalColumn);
				this.primaryKey[num] = columnValue.Value;
				this.objects[physicalColumn.Index] = columnValue.Value;
				this.fetched[physicalColumn.Index] = true;
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001A500 File Offset: 0x00018700
		public void DiscardCache(IConnectionProvider connectionProvider, bool discardUnsavedChanges)
		{
			for (int i = 0; i < this.objects.Length; i++)
			{
				if (this.table.PrimaryKeyIndex.PositionInIndex(this.table.Columns[i]) < 0 && (discardUnsavedChanges || !this.dirtyColumn[i]))
				{
					IDisposable disposable = this.objects[i] as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					this.fetched[i] = false;
					this.objects[i] = null;
				}
			}
			if (discardUnsavedChanges && this.dirty)
			{
				this.ClearDirtyFlag(connectionProvider, null);
			}
			this.cacheDiscarded = true;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001A59C File Offset: 0x0001879C
		public ErrorCode ReloadCacheIfDiscarded(IConnectionProvider connectionProvider)
		{
			ErrorCode result = ErrorCode.NoError;
			if (!this.cacheDiscarded)
			{
				return ErrorCode.NoError;
			}
			if (this.table.CommonColumns.Count > 0)
			{
				result = this.Load(connectionProvider, this.table.CommonColumns);
			}
			this.cacheDiscarded = false;
			return result;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001A5EB File Offset: 0x000187EB
		public void MarkDisconnected()
		{
			this.state = DataRow.DataRowState.Disconnected;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001A5F4 File Offset: 0x000187F4
		public void MarkReconnected()
		{
			this.state = DataRow.DataRowState.Existing;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001A5FD File Offset: 0x000187FD
		internal DataRow VerifyAndLoad(IConnectionProvider connectionProvider)
		{
			if (this.Load(connectionProvider, this.table.CommonColumns) != ErrorCode.NoError)
			{
				base.Dispose();
				return null;
			}
			return this;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001A626 File Offset: 0x00018826
		public ErrorCode Load(IConnectionProvider connectionProvider, params PhysicalColumn[] columns)
		{
			return this.Load(connectionProvider, (IList<PhysicalColumn>)columns, false);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001A636 File Offset: 0x00018836
		public ErrorCode Load(IConnectionProvider connectionProvider, IList<PhysicalColumn> columns)
		{
			return this.Load(connectionProvider, columns, false);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001A644 File Offset: 0x00018844
		public ErrorCode Load(IConnectionProvider connectionProvider, IList<PhysicalColumn> columns, bool loadFullStreamableColumns)
		{
			bool flag;
			IList<Column> columnsToLoad = this.GetColumnsToLoad(columns, loadFullStreamableColumns, out flag);
			if (columnsToLoad.Count == 0 && !flag)
			{
				columnsToLoad.Add(this.table.PrimaryKeyIndex.Columns[this.table.PrimaryKeyIndex.ColumnCount - 1]);
			}
			if (columnsToLoad.Count != 0)
			{
				StartStopKey startStopKey = new StartStopKey(true, this.PrimaryKey);
				using (TableOperator tableOperator = Factory.CreateTableOperator(this.culture, connectionProvider, this.Table, this.Table.PrimaryKeyIndex, columnsToLoad, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, true))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (!reader.Read())
						{
							return ErrorCode.CreateNotFound((LID)65304U);
						}
						this.LoadFromReader(reader, columnsToLoad);
					}
				}
			}
			if (flag)
			{
				this.LoadStreamColumns(connectionProvider, columns, loadFullStreamableColumns);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001A754 File Offset: 0x00018954
		public void MarkAsDeleted(IConnectionProvider connectionProvider)
		{
			if (!this.IsDead)
			{
				try
				{
					if (this.IsDirty)
					{
						this.ClearDirtyFlag(connectionProvider, null);
					}
				}
				finally
				{
					this.state = DataRow.DataRowState.Dead;
				}
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001A794 File Offset: 0x00018994
		public void Delete(IConnectionProvider connectionProvider)
		{
			try
			{
				if (!this.IsNew)
				{
					StartStopKey startStopKey = new StartStopKey(true, this.PrimaryKey);
					using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(this.culture, connectionProvider, Factory.CreateTableOperator(this.culture, connectionProvider, this.Table, this.Table.PrimaryKeyIndex, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true), true))
					{
						int num = (int)deleteOperator.ExecuteScalar();
					}
				}
			}
			finally
			{
				this.MarkAsDeleted(connectionProvider);
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001A830 File Offset: 0x00018A30
		public void Flush(IConnectionProvider connectionProvider)
		{
			this.Flush(connectionProvider, true);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001A83A File Offset: 0x00018A3A
		public void Flush(IConnectionProvider connectionProvider, bool flushLargeDirtyStreams)
		{
			if (!this.IsNew)
			{
				this.Update(connectionProvider, flushLargeDirtyStreams, null);
				return;
			}
			this.Insert(connectionProvider, flushLargeDirtyStreams);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001A858 File Offset: 0x00018A58
		public int GetLargeDirtyStreamsSize()
		{
			int num = 0;
			if (this.IsDirty)
			{
				for (int i = 0; i < this.Table.Columns.Count; i++)
				{
					PhysicalColumn physicalColumn = this.Table.Columns[i];
					if (this.dirtyColumn[physicalColumn.Index])
					{
						object obj = this.objects[physicalColumn.Index];
						Stream stream = obj as Stream;
						if (stream != null && stream.Length > (long)Factory.GetOptimalStreamChunkSize())
						{
							num += (int)stream.Length;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001ABA4 File Offset: 0x00018DA4
		public IEnumerable<bool> FlushDirtyStreams(IConnectionProvider connectionProvider)
		{
			if (this.IsDirty)
			{
				byte[] streamBuffer = DataRow.GetBufferPool(Factory.GetOptimalStreamChunkSize()).Acquire();
				BitArray stillDirtyColumn = null;
				for (int i = 0; i < this.Table.Columns.Count; i++)
				{
					PhysicalColumn c = this.Table.Columns[i];
					if (this.dirtyColumn[c.Index])
					{
						Stream valueStream = this.objects[c.Index] as Stream;
						if (valueStream != null && valueStream.Length > (long)Factory.GetOptimalStreamChunkSize())
						{
							long position = 0L;
							valueStream.Position = position;
							do
							{
								int count = valueStream.Read(streamBuffer, 0, Factory.GetOptimalStreamChunkSize());
								this.WriteBytesToStream(connectionProvider, c, position, streamBuffer, 0, count);
								position += (long)count;
								yield return false;
							}
							while (position < valueStream.Length);
							this.dirtyColumn[c.Index] = false;
						}
						else
						{
							if (stillDirtyColumn == null)
							{
								stillDirtyColumn = new BitArray(this.table.Columns.Count, false);
							}
							stillDirtyColumn[c.Index] = true;
						}
					}
				}
				this.ClearDirtyFlag(connectionProvider, stillDirtyColumn);
			}
			yield break;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001ABC8 File Offset: 0x00018DC8
		public object GetValue(IConnectionProvider connectionProvider, PhysicalColumn column)
		{
			if (!this.fetched[column.Index])
			{
				if (ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "faulting in column " + column.ToString());
				}
				this.Load(connectionProvider, new PhysicalColumn[]
				{
					column
				});
			}
			object obj = this.objects[column.Index];
			Stream stream = obj as Stream;
			if (stream != null)
			{
				int num = object.ReferenceEquals(column, this.Table.SpecialCols.OffPagePropertyBlob) ? 65536 : 8192;
				if (stream.Length < (long)num)
				{
					byte[] array = new byte[stream.Length];
					stream.Position = 0L;
					stream.Read(array, 0, array.Length);
					obj = array;
				}
				else
				{
					byte[] array2 = new byte[2048];
					stream.Position = 0L;
					stream.Read(array2, 0, array2.Length);
					obj = new LargeValue(stream.Length, array2);
				}
			}
			else if (obj != null && column.StreamSupport && !(obj is LargeValue))
			{
				byte[] array3 = (byte[])obj;
				int num2 = object.ReferenceEquals(column, this.Table.SpecialCols.OffPagePropertyBlob) ? 65536 : 8192;
				if (array3.Length >= num2)
				{
					byte[] array4 = new byte[2048];
					Array.Copy(array3, 0, array4, 0, array4.Length);
					obj = new LargeValue((long)array3.Length, array4);
				}
			}
			return obj;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001AD3E File Offset: 0x00018F3E
		public void SetValue(IConnectionProvider connectionProvider, PhysicalColumn column, object value)
		{
			this.SetValue(connectionProvider, column, value, false);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001AD4C File Offset: 0x00018F4C
		public void SetValue(IConnectionProvider connectionProvider, PhysicalColumn column, object value, bool notDirty)
		{
			if (this.fetched[column.Index] && !(this.objects[column.Index] is Stream) && !(this.objects[column.Index] is LargeValue) && ValueHelper.ValuesEqual(this.objects[column.Index], value))
			{
				return;
			}
			IDisposable disposable = this.objects[column.Index] as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this.fetched[column.Index] = true;
			this.objects[column.Index] = value;
			int num = this.table.PrimaryKeyIndex.PositionInIndex(column);
			if (num >= 0)
			{
				this.primaryKey[num] = value;
			}
			if (!notDirty)
			{
				this.SetDirtyFlag(connectionProvider);
				this.dirtyColumn[column.Index] = true;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001AE22 File Offset: 0x00019022
		public void DirtyValue(IConnectionProvider connectionProvider, PhysicalColumn column)
		{
			this.dirtyColumn[column.Index] = true;
			this.SetDirtyFlag(connectionProvider);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001AE3D File Offset: 0x0001903D
		public bool ColumnFetched(PhysicalColumn column)
		{
			return this.fetched[column.Index];
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001AE50 File Offset: 0x00019050
		public bool ColumnDirty(PhysicalColumn column)
		{
			return this.dirtyColumn[column.Index];
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001AE64 File Offset: 0x00019064
		public int? GetColumnSize(IConnectionProvider connectionProvider, PhysicalColumn column)
		{
			int? result = null;
			if (!this.fetched[column.Index] && !column.StreamSupport && (column.Size == 0 || column.IsNullable))
			{
				this.Load(connectionProvider, new PhysicalColumn[]
				{
					column
				});
			}
			if (this.fetched[column.Index])
			{
				object obj = this.objects[column.Index];
				Stream stream = obj as Stream;
				if (stream != null)
				{
					result = new int?((int)stream.Length);
				}
				else if (obj is LargeValue)
				{
					result = new int?((int)((LargeValue)obj).ActualLength);
				}
				else
				{
					result = SizeOfColumn.GetColumnSize(column, obj);
				}
			}
			else if (!column.StreamSupport)
			{
				result = new int?(column.Size);
			}
			else
			{
				DataRow.LargeValueSize largeValueSize = (DataRow.LargeValueSize)this.objects[column.Index];
				if (largeValueSize != null)
				{
					result = new int?((int)largeValueSize.Size);
				}
				else
				{
					result = this.ColumnSize(connectionProvider, column);
					if (result != null)
					{
						largeValueSize = new DataRow.LargeValueSize
						{
							Size = (long)result.Value
						};
						this.objects[column.Index] = largeValueSize;
					}
					else
					{
						this.fetched[column.Index] = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001AFB4 File Offset: 0x000191B4
		public void WriteStream(IConnectionProvider connectionProvider, PhysicalColumn column, long position, byte[] buffer, int offset, int count, out long deltaSize)
		{
			deltaSize = 0L;
			DataRow.LargeValueSize largeValueSize = null;
			Stream stream = null;
			if (this.fetched[column.Index] && this.objects[column.Index] is LargeValue)
			{
				largeValueSize = new DataRow.LargeValueSize
				{
					Size = ((LargeValue)this.objects[column.Index]).ActualLength
				};
				this.fetched[column.Index] = false;
				this.objects[column.Index] = largeValueSize;
			}
			if (!this.fetched[column.Index])
			{
				if (this.WriteThrough)
				{
					largeValueSize = (DataRow.LargeValueSize)this.objects[column.Index];
					if (largeValueSize == null && this.GetColumnSize(connectionProvider, column) != null)
					{
						largeValueSize = (DataRow.LargeValueSize)this.objects[column.Index];
					}
				}
				else
				{
					this.Load(connectionProvider, (IList<PhysicalColumn>)new PhysicalColumn[]
					{
						column
					}, true);
				}
			}
			if (this.fetched[column.Index])
			{
				if (this.IsExisting && this.WriteThrough)
				{
					if (this.objects[column.Index] == null)
					{
						this.SetValue(connectionProvider, column, Array<byte>.Empty);
					}
					if (this.dirtyColumn[column.Index])
					{
						this.Update(connectionProvider, true, new PhysicalColumn[]
						{
							column
						});
					}
					byte[] array = this.objects[column.Index] as byte[];
					long size;
					if (array != null)
					{
						size = (long)array.Length;
					}
					else
					{
						size = ((Stream)this.objects[column.Index]).Length;
					}
					largeValueSize = new DataRow.LargeValueSize
					{
						Size = size
					};
					this.fetched[column.Index] = false;
					this.objects[column.Index] = largeValueSize;
				}
				else if (this.objects[column.Index] == null || this.objects[column.Index] is byte[])
				{
					stream = TempStream.CreateInstance();
					byte[] array2 = this.objects[column.Index] as byte[];
					if (array2 != null)
					{
						stream.Write(array2, 0, array2.Length);
					}
					this.objects[column.Index] = stream;
				}
				else
				{
					stream = (Stream)this.objects[column.Index];
					if (!stream.CanWrite)
					{
						Stream stream2 = TempStream.CreateInstance();
						stream.Position = 0L;
						TempStream.CopyStream(stream, stream2);
						stream.Dispose();
						stream = stream2;
						this.objects[column.Index] = stream;
					}
				}
			}
			if (stream == null)
			{
				if (position > largeValueSize.Size)
				{
					throw new NotSupportedException("writing beyond value size is currently not supported");
				}
				if (count == 0)
				{
					return;
				}
				this.WriteBytesToStream(connectionProvider, column, position, buffer, offset, count);
				long num = position + (long)count;
				if (num > largeValueSize.Size)
				{
					deltaSize = num - largeValueSize.Size;
					largeValueSize.Size = num;
					return;
				}
			}
			else
			{
				long length = stream.Length;
				if (position > length)
				{
					throw new NotSupportedException("writing beyond value size is currently not supported");
				}
				stream.Position = position;
				stream.Write(buffer, offset, count);
				if (stream.Length > length)
				{
					deltaSize = stream.Length - length;
				}
				this.SetDirtyFlag(connectionProvider);
				this.dirtyColumn[column.Index] = true;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001B2E8 File Offset: 0x000194E8
		public int ReadStream(IConnectionProvider connectionProvider, PhysicalColumn column, long position, byte[] buffer, int offset, int count)
		{
			DataRow.LargeValueSize largeValueSize = null;
			if (!this.fetched[column.Index])
			{
				largeValueSize = (DataRow.LargeValueSize)this.objects[column.Index];
				if (largeValueSize == null && this.GetColumnSize(connectionProvider, column) != null)
				{
					largeValueSize = (DataRow.LargeValueSize)this.objects[column.Index];
				}
			}
			else if (this.objects[column.Index] is LargeValue)
			{
				largeValueSize = new DataRow.LargeValueSize
				{
					Size = ((LargeValue)this.objects[column.Index]).ActualLength
				};
			}
			if (this.fetched[column.Index] && !(this.objects[column.Index] is LargeValue))
			{
				if (this.objects[column.Index] == null)
				{
					throw new NullColumnException(column);
				}
				byte[] array = this.objects[column.Index] as byte[];
				if (array == null)
				{
					Stream stream = (Stream)this.objects[column.Index];
					stream.Position = position;
					return stream.Read(buffer, offset, count);
				}
				if (position >= (long)array.Length)
				{
					return 0;
				}
				int num = (int)Math.Min((long)array.Length - position, (long)count);
				Buffer.BlockCopy(array, (int)position, buffer, offset, num);
				return num;
			}
			else
			{
				if (position >= largeValueSize.Size)
				{
					return 0;
				}
				int count2 = (int)Math.Min((long)count, largeValueSize.Size - position);
				long num2 = (long)this.ReadBytesFromStream(connectionProvider, column, position, buffer, offset, count2);
				return (int)num2;
			}
		}

		// Token: 0x060005D4 RID: 1492
		public abstract bool CheckTableExists(IConnectionProvider connectionProvider);

		// Token: 0x060005D5 RID: 1493
		protected abstract int? ColumnSize(IConnectionProvider connectionProvider, PhysicalColumn column);

		// Token: 0x060005D6 RID: 1494
		protected abstract int ReadBytesFromStream(IConnectionProvider connectionProvider, PhysicalColumn column, long position, byte[] buffer, int offset, int count);

		// Token: 0x060005D7 RID: 1495
		protected abstract void WriteBytesToStream(IConnectionProvider connectionProvider, PhysicalColumn column, long position, byte[] buffer, int offset, int count);

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001B461 File Offset: 0x00019661
		internal object GetCachedValueForTest(IConnectionProvider connectionProvider, PhysicalColumn column)
		{
			return this.objects[column.Index];
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001B470 File Offset: 0x00019670
		internal bool GetIsFetchedForTest(IConnectionProvider connectionProvider, PhysicalColumn column)
		{
			return this.fetched[column.Index];
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001B483 File Offset: 0x00019683
		[Conditional("DEBUG")]
		private void CheckColumn(PhysicalColumn column)
		{
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001B488 File Offset: 0x00019688
		private void LoadFromReader(Reader reader, IList<Column> columnsToLoad)
		{
			for (int i = 0; i < columnsToLoad.Count; i++)
			{
				PhysicalColumn physicalColumn = (PhysicalColumn)columnsToLoad[i];
				object value = reader.GetValue(physicalColumn);
				this.objects[physicalColumn.Index] = value;
				int num = this.table.PrimaryKeyIndex.PositionInIndex(physicalColumn);
				if (num >= 0)
				{
					this.primaryKey[num] = value;
				}
				this.fetched[physicalColumn.Index] = true;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001B4FC File Offset: 0x000196FC
		private IList<Column> GetColumnsToLoad(IList<PhysicalColumn> columns, bool loadFullStreamableColumns, out bool hasStreamColumns)
		{
			IList<Column> list = new List<Column>(columns.Count);
			hasStreamColumns = false;
			for (int i = 0; i < columns.Count; i++)
			{
				PhysicalColumn physicalColumn = columns[i];
				if (!this.fetched[physicalColumn.Index] || (loadFullStreamableColumns && this.objects[physicalColumn.Index] is LargeValue))
				{
					if (physicalColumn.StreamSupport)
					{
						hasStreamColumns = true;
					}
					else
					{
						list.Add(physicalColumn);
					}
				}
			}
			return list;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001B570 File Offset: 0x00019770
		private void LoadStreamColumns(IConnectionProvider connectionProvider, IEnumerable<PhysicalColumn> columns, bool loadFullStreamableColumns)
		{
			foreach (PhysicalColumn physicalColumn in columns)
			{
				if ((!this.fetched[physicalColumn.Index] || (loadFullStreamableColumns && this.objects[physicalColumn.Index] is LargeValue)) && physicalColumn.StreamSupport)
				{
					if (this.objects[physicalColumn.Index] is LargeValue)
					{
						this.objects[physicalColumn.Index] = new DataRow.LargeValueSize
						{
							Size = ((LargeValue)this.objects[physicalColumn.Index]).ActualLength
						};
						this.fetched[physicalColumn.Index] = false;
					}
					DataRow.LargeValueSize largeValueSize = (DataRow.LargeValueSize)this.objects[physicalColumn.Index];
					if (largeValueSize == null && this.GetColumnSize(connectionProvider, physicalColumn) != null)
					{
						largeValueSize = (DataRow.LargeValueSize)this.objects[physicalColumn.Index];
					}
					if (largeValueSize != null)
					{
						int num = object.ReferenceEquals(physicalColumn, this.Table.SpecialCols.OffPagePropertyBlob) ? 65536 : 8192;
						if (!loadFullStreamableColumns && largeValueSize.Size >= (long)num)
						{
							byte[] array = new byte[2048];
							this.ReadBytesFromStream(connectionProvider, physicalColumn, 0L, array, 0, 2048);
							this.objects[physicalColumn.Index] = new LargeValue(largeValueSize.Size, array);
							this.fetched[physicalColumn.Index] = true;
						}
						else
						{
							BufferPool bufferPool = DataRow.GetBufferPool((int)Math.Min(largeValueSize.Size, (long)Factory.GetOptimalStreamChunkSize()));
							byte[] array2 = bufferPool.Acquire();
							try
							{
								Stream stream = TempStream.CreateInstance();
								int num3;
								for (long num2 = 0L; num2 < largeValueSize.Size; num2 += (long)num3)
								{
									num3 = this.ReadBytesFromStream(connectionProvider, physicalColumn, num2, array2, 0, Math.Min(array2.Length, (int)(largeValueSize.Size - num2)));
									stream.Write(array2, 0, num3);
								}
								this.objects[physicalColumn.Index] = stream;
								this.fetched[physicalColumn.Index] = true;
							}
							finally
							{
								bufferPool.Release(array2);
							}
						}
					}
				}
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001B7D4 File Offset: 0x000199D4
		public static BufferPool GetBufferPool(int bufferSize)
		{
			if (bufferSize > 8192)
			{
				return DataRow.largePool;
			}
			BufferPoolCollection.BufferSize bufferSize2;
			BufferPoolCollection.AutoCleanupCollection.TryMatchBufferSize(bufferSize, out bufferSize2);
			return BufferPoolCollection.AutoCleanupCollection.Acquire(bufferSize2);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001B808 File Offset: 0x00019A08
		private void Update(IConnectionProvider connectionProvider, bool flushLargeDirtyStreams, IList<PhysicalColumn> columns)
		{
			if (!this.dirty)
			{
				return;
			}
			List<Column> list = new List<Column>(10);
			List<object> list2 = new List<object>(10);
			List<byte[]> list3 = null;
			IList<PhysicalColumn> list4 = this.Table.Columns;
			BitArray bitArray = null;
			if (columns != null)
			{
				list4 = columns;
				bitArray = new BitArray(this.dirtyColumn);
				for (int i = 0; i < columns.Count; i++)
				{
					bitArray[columns[i].Index] = false;
				}
				if (bitArray.All(false))
				{
					bitArray = null;
				}
			}
			try
			{
				byte[] array = null;
				for (int j = 0; j < list4.Count; j++)
				{
					PhysicalColumn physicalColumn = list4[j];
					if (this.dirtyColumn[physicalColumn.Index])
					{
						object obj = this.objects[physicalColumn.Index];
						Stream stream = obj as Stream;
						if (stream != null)
						{
							int num;
							if (!flushLargeDirtyStreams && stream.Length > (long)Factory.GetOptimalStreamChunkSize())
							{
								if (bitArray == null)
								{
									bitArray = new BitArray(this.table.Columns.Count, false);
								}
								bitArray[physicalColumn.Index] = true;
								num = 0;
							}
							else
							{
								num = (int)Math.Min(stream.Length, (long)Factory.GetOptimalStreamChunkSize());
							}
							if (list3 == null)
							{
								list3 = new List<byte[]>();
							}
							byte[] array2 = DataRow.GetBufferPool(num).Acquire();
							list3.Add(array2);
							stream.Position = 0L;
							stream.Read(array2, 0, num);
							obj = new ArraySegment<byte>(array2, 0, num);
							if (num == Factory.GetOptimalStreamChunkSize())
							{
								array = array2;
							}
						}
						list.Add(physicalColumn);
						list2.Add(obj);
					}
				}
				if (list.Count != 0)
				{
					StartStopKey startStopKey = new StartStopKey(true, this.PrimaryKey);
					using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(this.culture, connectionProvider, Factory.CreateTableOperator(this.culture, connectionProvider, this.Table, this.Table.PrimaryKeyIndex, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true), list, list2, true))
					{
						int num2 = (int)updateOperator.ExecuteScalar();
					}
				}
				if (flushLargeDirtyStreams && array != null)
				{
					for (int k = 0; k < list4.Count; k++)
					{
						PhysicalColumn physicalColumn2 = list4[k];
						if (this.dirtyColumn[physicalColumn2.Index])
						{
							Stream stream2 = this.objects[physicalColumn2.Index] as Stream;
							if (stream2 != null && stream2.Length > (long)Factory.GetOptimalStreamChunkSize())
							{
								this.CopyStreamToColumn(connectionProvider, physicalColumn2, stream2, (long)Factory.GetOptimalStreamChunkSize(), array);
							}
						}
					}
				}
				for (int l = 0; l < list4.Count; l++)
				{
					PhysicalColumn physicalColumn3 = list4[l];
					if (this.dirtyColumn[physicalColumn3.Index])
					{
						int num3 = this.table.PrimaryKeyIndex.PositionInIndex(physicalColumn3);
						if (num3 >= 0)
						{
							this.primaryKey[num3] = this.objects[physicalColumn3.Index];
						}
					}
				}
				this.ClearDirtyFlag(connectionProvider, bitArray);
			}
			finally
			{
				if (list3 != null)
				{
					foreach (byte[] array3 in list3)
					{
						DataRow.GetBufferPool(array3.Length).Release(array3);
					}
				}
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001BB80 File Offset: 0x00019D80
		private void Insert(IConnectionProvider connectionProvider, bool flushLargeDirtyStreams)
		{
			List<Column> list = new List<Column>(this.Table.Columns.Count);
			List<object> list2 = new List<object>(this.Table.Columns.Count);
			PhysicalColumn physicalColumn = null;
			List<byte[]> list3 = null;
			BitArray bitArray = null;
			try
			{
				byte[] array = null;
				for (int i = 0; i < this.Table.Columns.Count; i++)
				{
					PhysicalColumn physicalColumn2 = this.Table.Columns[i];
					if (physicalColumn2.IsIdentity)
					{
						physicalColumn = physicalColumn2;
					}
					if (this.dirtyColumn[physicalColumn2.Index])
					{
						object obj = this.objects[physicalColumn2.Index];
						Stream stream = obj as Stream;
						if (stream != null)
						{
							int num;
							if (!flushLargeDirtyStreams && stream.Length > (long)Factory.GetOptimalStreamChunkSize())
							{
								if (bitArray == null)
								{
									bitArray = new BitArray(this.table.Columns.Count, false);
								}
								bitArray[physicalColumn2.Index] = true;
								num = 0;
							}
							else
							{
								num = (int)Math.Min(stream.Length, (long)Factory.GetOptimalStreamChunkSize());
							}
							if (list3 == null)
							{
								list3 = new List<byte[]>();
							}
							byte[] array2 = DataRow.GetBufferPool(num).Acquire();
							list3.Add(array2);
							stream.Position = 0L;
							stream.Read(array2, 0, num);
							obj = new ArraySegment<byte>(array2, 0, num);
							if (num == Factory.GetOptimalStreamChunkSize())
							{
								array = array2;
							}
						}
						list.Add(physicalColumn2);
						list2.Add(obj);
					}
				}
				object value;
				using (InsertOperator insertOperator = Factory.CreateInsertOperator(this.culture, connectionProvider, this.Table, null, list, list2, physicalColumn, true))
				{
					value = insertOperator.ExecuteScalar();
				}
				if (physicalColumn != null)
				{
					if (physicalColumn.Type == typeof(long))
					{
						this.objects[physicalColumn.Index] = Convert.ToInt64(value);
					}
					else
					{
						this.objects[physicalColumn.Index] = Convert.ToInt32(value);
					}
					this.fetched[physicalColumn.Index] = true;
					int num2 = this.table.PrimaryKeyIndex.PositionInIndex(physicalColumn);
					if (num2 >= 0)
					{
						this.primaryKey[num2] = this.objects[physicalColumn.Index];
					}
				}
				if (flushLargeDirtyStreams && array != null)
				{
					for (int j = 0; j < this.Table.Columns.Count; j++)
					{
						PhysicalColumn physicalColumn3 = this.Table.Columns[j];
						if (this.dirtyColumn[physicalColumn3.Index])
						{
							Stream stream2 = this.objects[physicalColumn3.Index] as Stream;
							if (stream2 != null && stream2.Length > (long)Factory.GetOptimalStreamChunkSize())
							{
								this.CopyStreamToColumn(connectionProvider, physicalColumn3, stream2, (long)Factory.GetOptimalStreamChunkSize(), array);
							}
						}
					}
				}
				this.state = DataRow.DataRowState.Existing;
				this.ClearDirtyFlag(connectionProvider, bitArray);
			}
			finally
			{
				if (list3 != null)
				{
					foreach (byte[] array3 in list3)
					{
						DataRow.GetBufferPool(array3.Length).Release(array3);
					}
				}
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		private void CopyStreamToColumn(IConnectionProvider connectionProvider, PhysicalColumn column, Stream valueStream, long startPosition, byte[] tempBuffer)
		{
			long num = startPosition;
			valueStream.Position = num;
			do
			{
				int num2 = valueStream.Read(tempBuffer, 0, tempBuffer.Length);
				this.WriteBytesToStream(connectionProvider, column, num, tempBuffer, 0, num2);
				num += (long)num2;
			}
			while (num < valueStream.Length);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001BF30 File Offset: 0x0001A130
		public void ClearDirtyFlag(IConnectionProvider connectionProvider, BitArray stillDirtyColumn)
		{
			this.dirtyColumn.Xor(this.dirtyColumn);
			if (stillDirtyColumn != null)
			{
				this.dirtyColumn.Or(stillDirtyColumn);
				return;
			}
			this.dirty = false;
			if (this.Table.TrackDirtyObjects)
			{
				connectionProvider.GetConnection().CleanDirtyObject(this);
				this.connectionDirtyThisObject = null;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001BF87 File Offset: 0x0001A187
		private void SetDirtyFlag(IConnectionProvider connectionProvider)
		{
			if (!this.dirty)
			{
				this.dirty = true;
				if (this.Table.TrackDirtyObjects)
				{
					connectionProvider.GetConnection().AddDirtyObject(this);
					this.connectionDirtyThisObject = connectionProvider.GetConnection();
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001BFBD File Offset: 0x0001A1BD
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DataRow>(this);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.IsDirty && this.Table.TrackDirtyObjects)
				{
					this.connectionDirtyThisObject.GetConnection().CleanDirtyObject(this);
				}
				if (this.objects != null)
				{
					for (int i = 0; i < this.objects.Length; i++)
					{
						IDisposable disposable = this.objects[i] as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C030 File Offset: 0x0001A230
		internal void AppendDirtyTrackingInfoToString(StringBuilder sb)
		{
			sb.Append("id = [");
			for (int i = 0; i < this.primaryKey.Length; i++)
			{
				sb.Append(this.table.Columns[i]);
				sb.Append(" = ");
				sb.AppendAsString(this.primaryKey[i]);
			}
			sb.Append("], type = [");
			sb.Append(this.table.Name);
			sb.Append("] dirty columns = [");
			for (int j = 0; j < this.table.Columns.Count; j++)
			{
				if (this.dirtyColumn[j])
				{
					sb.Append("column = [");
					sb.Append(this.table.Columns[j]);
					sb.Append("] value = [");
					sb.Append((this.objects[j] is Stream) ? "<stream>" : this.objects[j]);
					sb.Append("] ");
				}
			}
			sb.Append("]");
		}

		// Token: 0x040001AE RID: 430
		public const int MinimumColumnSizeToStream = 8192;

		// Token: 0x040001AF RID: 431
		public const int OffPageBlobColumnSizeToStream = 65536;

		// Token: 0x040001B0 RID: 432
		public const int TruncatedLargeValueSize = 2048;

		// Token: 0x040001B1 RID: 433
		public static readonly DataRow.CreateDataRowFlag Create = default(DataRow.CreateDataRowFlag);

		// Token: 0x040001B2 RID: 434
		public static readonly DataRow.OpenDataRowFlag Open = default(DataRow.OpenDataRowFlag);

		// Token: 0x040001B3 RID: 435
		private static BufferPool largePool = new BufferPool(Factory.GetOptimalStreamChunkSize(), 4, true, true);

		// Token: 0x040001B4 RID: 436
		private BitArray fetched;

		// Token: 0x040001B5 RID: 437
		private BitArray dirtyColumn;

		// Token: 0x040001B6 RID: 438
		private DataRow.DataRowState state;

		// Token: 0x040001B7 RID: 439
		private bool dirty;

		// Token: 0x040001B8 RID: 440
		private bool cacheDiscarded;

		// Token: 0x040001B9 RID: 441
		private readonly CultureInfo culture;

		// Token: 0x040001BA RID: 442
		private bool writeThrough;

		// Token: 0x040001BB RID: 443
		private Table table;

		// Token: 0x040001BC RID: 444
		private object[] objects;

		// Token: 0x040001BD RID: 445
		private object[] primaryKey;

		// Token: 0x040001BE RID: 446
		private Connection connectionDirtyThisObject;

		// Token: 0x02000081 RID: 129
		public struct CreateDataRowFlag
		{
		}

		// Token: 0x02000082 RID: 130
		public struct OpenDataRowFlag
		{
		}

		// Token: 0x02000083 RID: 131
		private enum DataRowState
		{
			// Token: 0x040001C0 RID: 448
			Dead,
			// Token: 0x040001C1 RID: 449
			New,
			// Token: 0x040001C2 RID: 450
			Existing,
			// Token: 0x040001C3 RID: 451
			Disconnected
		}

		// Token: 0x02000084 RID: 132
		private class LargeValueSize
		{
			// Token: 0x040001C4 RID: 452
			public long Size;
		}
	}
}
