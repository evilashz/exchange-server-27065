using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Server2003;
using Microsoft.Isam.Esent.Interop.Vista;
using Microsoft.Isam.Esent.Interop.Windows8;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000C9 RID: 201
	internal class JetTableOperator : TableOperator, IJetSimpleQueryOperator, IJetRecordCounter, ITWIR, IRowAccess, IColumnStreamAccess
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x0002A0A0 File Offset: 0x000282A0
		internal JetTableOperator(CultureInfo culture, IConnectionProvider connectionProvider, Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table, Index index, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool opportunedPreread, bool frequentOperation) : this(connectionProvider, new TableOperator.TableOperatorDefinition(culture, table, index, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, opportunedPreread, frequentOperation))
		{
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002A0D4 File Offset: 0x000282D4
		internal JetTableOperator(IConnectionProvider connectionProvider, TableOperator.TableOperatorDefinition definition) : base(connectionProvider, definition)
		{
			if (base.Index.Unique && base.KeyRanges.Count == 1)
			{
				StartStopKey startKey = base.KeyRanges[0].StartKey;
				if (startKey.Inclusive)
				{
					StartStopKey stopKey = base.KeyRanges[0].StopKey;
					if (stopKey.Inclusive && StartStopKey.CommonKeyPrefix(base.KeyRanges[0].StartKey, base.KeyRanges[0].StopKey, base.CompareInfo) == base.Index.Columns.Count)
					{
						this.matchesAtMostOneRow = true;
					}
				}
			}
			if (base.OpportunedPreread && !base.Index.PrimaryKey && !this.matchesAtMostOneRow)
			{
				this.usingPreRead = true;
				if (base.MaxRows > 0 && base.SkipTo + base.MaxRows < 150)
				{
					base.PrereadChunkSize /= 2;
				}
			}
			this.effectiveCriteria = base.Criteria;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0002A1EA File Offset: 0x000283EA
		private JetConnection JetConnection
		{
			get
			{
				return (JetConnection)base.Connection;
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002A1F7 File Offset: 0x000283F7
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			return this.ExecuteReader(disposeQueryOperator, true);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0002A204 File Offset: 0x00028404
		internal static RetrieveColumnGrbit GetRetrieveColumnSizeGrbit(bool compressedSize)
		{
			RetrieveColumnGrbit result = RetrieveColumnGrbit.None;
			if (compressedSize)
			{
				result = (RetrieveColumnGrbit)65536;
			}
			return result;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002A21D File Offset: 0x0002841D
		internal static bool InternalPepareUpdate(JET_SESID sesid, JET_DBID jetDatabase, string tableName, JET_TABLEID jetCursor, bool primaryKeyUpdate)
		{
			Api.JetPrepareUpdate(sesid, jetCursor, primaryKeyUpdate ? ((JET_prep)9) : JET_prep.ReplaceNoLock);
			return true;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002A230 File Offset: 0x00028430
		internal static JET_TABLEID InternalDupCursor(JET_SESID sesid, JET_DBID jetDatabase, string tableName, JET_TABLEID jetCursor)
		{
			JET_TABLEID result;
			Api.JetDupCursor(sesid, jetCursor, out result, DupCursorGrbit.None);
			return result;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002A248 File Offset: 0x00028448
		internal JetReader ExecuteReader(bool disposeQueryOperator, bool traceOperation)
		{
			if (traceOperation)
			{
				base.TraceOperation("ExecuteReader");
			}
			base.Connection.CountStatement(Connection.OperationType.Query);
			return new JetReader(base.ConnectionProvider, this, disposeQueryOperator);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002A271 File Offset: 0x00028471
		public override bool EnableInterrupts(IInterruptControl interruptControl)
		{
			if (interruptControl != null && (base.SkipTo != 0 || !base.Index.Unique))
			{
				return false;
			}
			this.interruptControl = interruptControl;
			return true;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002A295 File Offset: 0x00028495
		public bool MoveFirst(out int rowsSkipped)
		{
			rowsSkipped = 0;
			return this.MoveFirst(false, Connection.OperationType.Query, ref rowsSkipped);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002A2A4 File Offset: 0x000284A4
		internal bool MoveFirst(bool positionForUpdate, Connection.OperationType operationType, ref int rowsSkipped)
		{
			this.rowsReturned = 0U;
			if (base.KeyRanges.Count == 0)
			{
				base.TraceMove("MoveFirst", false);
				return false;
			}
			if (base.Criteria is SearchCriteriaFalse)
			{
				base.TraceMove("MoveFirst", false);
				return false;
			}
			bool flag;
			if (!this.TryOpenJetCursorIfNecessary(operationType, false, out flag))
			{
				base.TraceMove("MoveFirst", false);
				return false;
			}
			JetConnection jetConnection = this.JetConnection;
			if (this.interruptControl == null && base.Criteria != null && base.Index.PrimaryKey && JetTableOperator.EnableJetFiltering && base.KeyRanges.Count == 1)
			{
				StartStopKey stopKey = base.KeyRanges[0].StopKey;
				if (stopKey.Count <= base.Table.SpecialCols.NumberOfPartioningColumns)
				{
					using (base.Connection.TrackTimeInDatabase())
					{
						JET_INDEX_COLUMN[] filters = this.ExtractJetSimpleFilter(base.Criteria, out this.effectiveCriteria);
						Windows8Api.JetSetCursorFilter(jetConnection.JetSession, this.jetCursor, filters, CursorFilterGrbit.None);
						this.jetFilterSet = true;
					}
				}
			}
			this.SetupColumnCaching();
			if (this.interruptControl != null)
			{
				this.interrupted = false;
			}
			bool result;
			try
			{
				int num = base.SkipTo;
				using (base.Connection.TrackTimeInDatabase())
				{
					if (!this.matchesAtMostOneRow)
					{
						Api.JetSetTableSequential(jetConnection.JetSession, this.jetCursor, base.Backwards ? ((SetTableSequentialGrbit)2) : ((SetTableSequentialGrbit)1));
					}
					this.ResetCachedColumns();
					if (!this.InternalMoveFirst(this.jetCursor, ref this.keyRangeIndex))
					{
						base.TraceMove("MoveFirst", false);
						return false;
					}
					if (!this.jetFilterSet && this.interruptControl == null && base.Criteria != null && base.Index.PrimaryKey && JetTableOperator.EnableJetFiltering && base.KeyRanges.Count == 1)
					{
						SearchCriteria searchCriteria;
						JET_INDEX_COLUMN[] filters2 = this.ExtractJetSimpleFilter(base.Criteria, out searchCriteria);
						Windows8Api.JetSetCursorFilter(jetConnection.JetSession, this.jetCursor, filters2, CursorFilterGrbit.None);
						this.jetFilterSet = true;
					}
					if (this.usingPreRead)
					{
						this.remainingPreread = 0;
						this.prereadIsEOF = false;
						if (this.jetCursorForReadAhead != JET_TABLEID.Nil)
						{
							Api.JetCloseTable(jetConnection.JetSession, this.jetCursorForReadAhead);
							this.jetCursorForReadAhead = JET_TABLEID.Nil;
						}
						this.PrereadOnPrimaryIndex();
					}
					jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Read);
					this.rowsRead += 1U;
					if (this.interruptControl != null)
					{
						this.interruptControl.RegisterRead(!base.Index.PrimaryKey, base.Table.TableClass);
					}
					if (this.effectiveCriteria != null)
					{
						bool flag2 = this.effectiveCriteria.Evaluate(this, base.CompareInfo);
						bool? flag3 = new bool?(true);
						if (!flag2 || flag3 == null)
						{
							goto IL_2F8;
						}
					}
					if (num <= 0)
					{
						base.TraceMove("MoveFirst", true);
						jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Accept);
						this.rowsReturned += 1U;
						return true;
					}
					rowsSkipped++;
					num--;
					IL_2F8:;
				}
				result = this.MoveNext("MoveFirst", num, ref rowsSkipped);
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)49608U, "JetTableOperator.MoveFirst", ex);
			}
			return result;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002A63C File Offset: 0x0002883C
		public bool MoveNext()
		{
			int num = 0;
			return this.MoveNext("MoveNext", 0, ref num);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002A65C File Offset: 0x0002885C
		internal bool MoveNext(string operation, int numberLeftToSkip, ref int rowsSkipped)
		{
			if (this.matchesAtMostOneRow)
			{
				base.TraceMove(operation, false);
				return false;
			}
			if (base.MaxRows > 0 && (ulong)this.rowsReturned >= (ulong)((long)base.MaxRows))
			{
				base.TraceMove(operation, false);
				return false;
			}
			if (this.interrupted)
			{
				base.TraceCrumb(operation, "Resume");
				if (!this.Resume())
				{
					base.TraceMove(operation, false);
					return false;
				}
				this.interruptControl.Reset();
			}
			JetConnection jetConnection = this.JetConnection;
			bool result;
			try
			{
				using (base.Connection.TrackTimeInDatabase())
				{
					for (;;)
					{
						this.ResetCachedColumns();
						if (this.interruptControl != null && this.interruptControl.WantToInterrupt && this.Interrupt())
						{
							break;
						}
						this.savedNavigationBookmark = null;
						if (!this.InternalMoveNext(this.jetCursor, ref this.keyRangeIndex))
						{
							goto Block_12;
						}
						if (this.usingPreRead)
						{
							this.remainingPreread--;
							if (this.remainingPreread <= base.PrereadChunkSize / 3 && !this.prereadIsEOF)
							{
								this.PrereadOnPrimaryIndex();
							}
						}
						jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Read);
						this.rowsRead += 1U;
						if (this.interruptControl != null)
						{
							this.interruptControl.RegisterRead(!base.Index.PrimaryKey, base.Table.TableClass);
						}
						if (this.effectiveCriteria != null)
						{
							bool flag = this.effectiveCriteria.Evaluate(this, base.CompareInfo);
							bool? flag2 = new bool?(true);
							if (!flag || flag2 == null)
							{
								continue;
							}
						}
						if (numberLeftToSkip <= 0)
						{
							goto IL_1A2;
						}
						rowsSkipped++;
						numberLeftToSkip--;
					}
					base.TraceCrumb(operation, "Interrupt");
					return true;
					Block_12:
					base.TraceMove(operation, false);
					return false;
					IL_1A2:
					base.TraceMove(operation, true);
					jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Accept);
					this.rowsReturned += 1U;
					result = true;
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)49096U, "JetTableOperator.MoveNext", ex);
			}
			return result;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002A894 File Offset: 0x00028A94
		internal bool Insert(IList<Column> columns, IList<object> values, Column identityColumnToFetch, bool unversioned, bool ignoreDuplicateKey, out object identityValue)
		{
			bool result = false;
			object[] array = base.Table.IsPartitioned ? JetPartitionHelper.GetPartitionKeyValues(base.Table, columns, values) : null;
			this.OpenJetCursorIfNecessary(array, (array != null) ? array.Length : 0, Connection.OperationType.Insert);
			JetConnection jetConnection = this.JetConnection;
			if (this.intermediateTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append(" >>> Insert row  columns:[");
				for (int i = 0; i < columns.Count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					columns[i].AppendToString(stringBuilder, StringFormatOptions.None);
					stringBuilder.Append("=[");
					if (this.detailTracingEnabled || !(values[i] is byte[]) || ((byte[])values[i]).Length <= 32)
					{
						stringBuilder.AppendAsString(values[i]);
					}
					else
					{
						stringBuilder.Append("<long_blob>");
					}
					stringBuilder.Append("]");
				}
				ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Write);
			jetConnection.BeginTransactionIfNeeded(Connection.TransactionOption.NeedTransaction);
			identityValue = null;
			try
			{
				JET_SETCOLUMN[] array2 = new JET_SETCOLUMN[columns.Count];
				int num = 0;
				for (int j = 0; j < columns.Count; j++)
				{
					JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)columns[j];
					if (!JetPartitionHelper.IsPartitioningColumn(base.Table, jetPhysicalColumn))
					{
						ArraySegment<byte> arraySegment = default(ArraySegment<byte>);
						if (values[j] is ArraySegment<byte>)
						{
							arraySegment = (ArraySegment<byte>)values[j];
						}
						else
						{
							byte[] asByteArray = JetColumnValueHelper.GetAsByteArray(values[j], jetPhysicalColumn);
							if (asByteArray != null)
							{
								arraySegment = new ArraySegment<byte>(asByteArray);
							}
						}
						if (arraySegment.Array == null && !jetPhysicalColumn.IsNullable)
						{
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, string.Format("non-nullable column {0} in table {1} being written as null", jetPhysicalColumn.Name, base.Table.Name));
						}
						if (arraySegment.Array != null || !jetPhysicalColumn.IsNullable)
						{
							SetColumnGrbit setColumnGrbit = JetTableOperator.GetInOrOutOfLineJetHint(arraySegment.Count, jetPhysicalColumn);
							if (arraySegment.Array != null && arraySegment.Count == 0)
							{
								setColumnGrbit |= SetColumnGrbit.ZeroLength;
								arraySegment = default(ArraySegment<byte>);
							}
							jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.WriteBytes, arraySegment.Count);
							array2[num] = new JET_SETCOLUMN();
							array2[num].columnid = jetPhysicalColumn.GetJetColumnId(jetConnection);
							array2[num].pvData = arraySegment.Array;
							array2[num].cbData = arraySegment.Count;
							array2[num].ibData = arraySegment.Offset;
							array2[num].itagSequence = 1;
							array2[num].grbit = setColumnGrbit;
							num++;
						}
					}
				}
				JET_COLUMNID columnid = default(JET_COLUMNID);
				if (null != identityColumnToFetch)
				{
					columnid = ((JetPhysicalColumn)identityColumnToFetch).GetJetColumnId(jetConnection);
				}
				using (jetConnection.TrackTimeInDatabase())
				{
					Api.JetPrepareUpdate(jetConnection.JetSession, this.jetCursor, JET_prep.Insert);
					bool flag = true;
					try
					{
						Api.JetSetColumns(jetConnection.JetSession, this.jetCursor, array2, num);
						this.ResetCachedColumns();
						if (null != identityColumnToFetch)
						{
							JetPhysicalColumn jetPhysicalColumn2 = (JetPhysicalColumn)identityColumnToFetch;
							if (typeof(int) == jetPhysicalColumn2.Type)
							{
								jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, 4);
								identityValue = Api.RetrieveColumnAsInt32(jetConnection.JetSession, this.jetCursor, columnid, RetrieveColumnGrbit.RetrieveCopy);
							}
							else if (typeof(long) == jetPhysicalColumn2.Type)
							{
								jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, 8);
								identityValue = Api.RetrieveColumnAsInt64(jetConnection.JetSession, this.jetCursor, columnid, RetrieveColumnGrbit.RetrieveCopy);
							}
						}
						UpdateGrbit updateGrbit = JetTableOperator.GetUpdateGrbit(unversioned);
						int num2;
						Server2003Api.JetUpdate2(jetConnection.JetSession, this.jetCursor, null, 0, out num2, updateGrbit);
						flag = false;
						result = true;
					}
					finally
					{
						if (flag)
						{
							Api.JetPrepareUpdate(jetConnection.JetSession, this.jetCursor, JET_prep.Cancel);
						}
					}
				}
			}
			catch (EsentErrorException ex)
			{
				if (ex.Error != JET_err.KeyDuplicate || !ignoreDuplicateKey)
				{
					jetConnection.OnExceptionCatch(ex);
					throw jetConnection.ProcessJetError((LID)56380U, "JetTableOperator.Insert", ex);
				}
				if (this.intermediateTracingEnabled)
				{
					ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, " >>> Ignoring duplicate key detected on insert.");
				}
			}
			return result;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002AD28 File Offset: 0x00028F28
		internal bool InsertCopy(IList<Column> columnsToInsert, IList<Column> columnsToFetch, Column identityColumnToFetch, bool unversioned, bool ignoreDuplicateKey, out object identityValue)
		{
			bool result = false;
			JetConnection jetConnection = this.JetConnection;
			if (this.intermediateTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append(" >>> Insert row copy  columns:[");
				for (int i = 0; i < columnsToInsert.Count; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					columnsToInsert[i].AppendToString(stringBuilder, StringFormatOptions.None);
					stringBuilder.Append("=[");
					object columnValue = this.GetColumnValue(columnsToFetch[i]);
					if (this.detailTracingEnabled || !(columnValue is byte[]) || ((byte[])columnValue).Length <= 32)
					{
						stringBuilder.AppendAsString(columnValue);
					}
					else
					{
						stringBuilder.Append("<long_blob>");
					}
					stringBuilder.Append("]");
				}
				ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Write);
			jetConnection.BeginTransactionIfNeeded(Connection.TransactionOption.NeedTransaction);
			identityValue = null;
			try
			{
				JET_SETCOLUMN[] array = new JET_SETCOLUMN[base.Table.Columns.Count];
				int num = 0;
				for (int j = 0; j < base.Table.Columns.Count; j++)
				{
					JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)base.Table.Columns[j];
					if (!JetPartitionHelper.IsPartitioningColumn(base.Table, jetPhysicalColumn) && !jetPhysicalColumn.IsIdentity)
					{
						int num2 = -1;
						for (int k = 0; k < columnsToInsert.Count; k++)
						{
							if (jetPhysicalColumn == columnsToInsert[k])
							{
								num2 = k;
								break;
							}
						}
						byte[] array2 = null;
						if (num2 != -1)
						{
							if (columnsToInsert[num2] == columnsToFetch[num2])
							{
								goto IL_231;
							}
							array2 = ((IJetColumn)columnsToFetch[num2].ActualColumn).GetValueAsBytes(this);
						}
						SetColumnGrbit setColumnGrbit = JetTableOperator.GetInOrOutOfLineJetHint((array2 != null) ? array2.Length : 0, jetPhysicalColumn);
						int num3 = 0;
						if (array2 != null)
						{
							num3 = array2.Length;
							if (num3 == 0)
							{
								setColumnGrbit |= SetColumnGrbit.ZeroLength;
								array2 = null;
							}
						}
						jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.WriteBytes, num3);
						array[num] = new JET_SETCOLUMN();
						array[num].columnid = jetPhysicalColumn.GetJetColumnId(jetConnection);
						array[num].pvData = array2;
						array[num].cbData = num3;
						array[num].itagSequence = 1;
						array[num].grbit = setColumnGrbit;
						num++;
					}
					IL_231:;
				}
				JET_COLUMNID columnid = default(JET_COLUMNID);
				if (null != identityColumnToFetch)
				{
					columnid = ((JetPhysicalColumn)identityColumnToFetch).GetJetColumnId(jetConnection);
				}
				using (jetConnection.TrackTimeInDatabase())
				{
					Api.JetPrepareUpdate(jetConnection.JetSession, this.jetCursor, JET_prep.InsertCopy);
					bool flag = true;
					try
					{
						Api.JetSetColumns(jetConnection.JetSession, this.jetCursor, array, num);
						if (null != identityColumnToFetch)
						{
							JetPhysicalColumn jetPhysicalColumn2 = (JetPhysicalColumn)identityColumnToFetch;
							if (typeof(int) == jetPhysicalColumn2.Type)
							{
								jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, 4);
								identityValue = Api.RetrieveColumnAsInt32(jetConnection.JetSession, this.jetCursor, columnid, RetrieveColumnGrbit.RetrieveCopy);
							}
							else if (typeof(long) == jetPhysicalColumn2.Type)
							{
								jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, 8);
								identityValue = Api.RetrieveColumnAsInt64(jetConnection.JetSession, this.jetCursor, columnid, RetrieveColumnGrbit.RetrieveCopy);
							}
						}
						UpdateGrbit updateGrbit = JetTableOperator.GetUpdateGrbit(unversioned);
						int num4;
						Server2003Api.JetUpdate2(jetConnection.JetSession, this.jetCursor, null, 0, out num4, updateGrbit);
						flag = false;
						result = true;
					}
					finally
					{
						if (flag)
						{
							Api.JetPrepareUpdate(jetConnection.JetSession, this.jetCursor, JET_prep.Cancel);
						}
					}
				}
			}
			catch (EsentErrorException ex)
			{
				if (ex.Error != JET_err.KeyDuplicate || !ignoreDuplicateKey)
				{
					jetConnection.OnExceptionCatch(ex);
					throw jetConnection.ProcessJetError((LID)65480U, "JetTableOperator.Insert", ex);
				}
				if (this.intermediateTracingEnabled)
				{
					ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, " >>> Ignoring duplicate key detected on insert-copy.");
				}
			}
			return result;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002B160 File Offset: 0x00029360
		internal void Update(IList<Column> columns, IList<object> values)
		{
			JetConnection jetConnection = this.JetConnection;
			object y;
			if (columns.Count == 1 && this.columnCache != null && this.columnCache.TryGetValue((PhysicalColumn)columns[0], out y) && ValueHelper.ValuesEqual(values[0], y))
			{
				return;
			}
			StringBuilder stringBuilder = null;
			if (this.detailTracingEnabled)
			{
				stringBuilder = new StringBuilder(100);
				stringBuilder.Append(" >>> Update row  columns:[");
			}
			jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Write);
			jetConnection.BeginTransactionIfNeeded(Connection.TransactionOption.NeedTransaction);
			bool primaryKeyUpdate = false;
			Index primaryKeyIndex = base.Table.PrimaryKeyIndex;
			for (int i = 0; i < columns.Count; i++)
			{
				if (primaryKeyIndex.PositionInIndex(columns[i]) != -1)
				{
					primaryKeyUpdate = true;
					break;
				}
			}
			try
			{
				JET_SETCOLUMN[] array = new JET_SETCOLUMN[columns.Count];
				for (int j = 0; j < columns.Count; j++)
				{
					object obj = values[j];
					ArraySegment<byte> arraySegment = default(ArraySegment<byte>);
					if (obj is ArraySegment<byte>)
					{
						arraySegment = (ArraySegment<byte>)obj;
					}
					else
					{
						Column column = obj as Column;
						byte[] array2;
						if (column != null)
						{
							array2 = ((IJetColumn)column).GetValueAsBytes(this);
							if (this.detailTracingEnabled)
							{
								obj = ((IColumn)column).GetValue(this);
							}
						}
						else
						{
							array2 = JetColumnValueHelper.GetAsByteArray(obj, columns[j]);
						}
						if (array2 != null)
						{
							arraySegment = new ArraySegment<byte>(array2);
						}
					}
					if (arraySegment.Array == null && !columns[j].IsNullable)
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, string.Format("non-nullable column {0} in table {1} being written as null", columns[j].Name, base.Table.Name));
					}
					if (this.detailTracingEnabled)
					{
						if (j != 0)
						{
							stringBuilder.Append(", ");
						}
						columns[j].AppendToString(stringBuilder, StringFormatOptions.None);
						stringBuilder.Append("=[");
						stringBuilder.AppendAsString(obj);
						stringBuilder.Append("]");
					}
					JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)columns[j];
					SetColumnGrbit setColumnGrbit = JetTableOperator.GetInOrOutOfLineJetHint(arraySegment.Count, jetPhysicalColumn);
					if (arraySegment.Array != null && arraySegment.Count == 0)
					{
						setColumnGrbit |= SetColumnGrbit.ZeroLength;
						arraySegment = default(ArraySegment<byte>);
					}
					jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.WriteBytes, arraySegment.Count);
					array[j] = new JET_SETCOLUMN();
					array[j].columnid = jetPhysicalColumn.GetJetColumnId(jetConnection);
					array[j].pvData = arraySegment.Array;
					array[j].cbData = arraySegment.Count;
					array[j].ibData = arraySegment.Offset;
					array[j].grbit = setColumnGrbit;
					array[j].itagSequence = 1;
				}
				JET_SESID jetSession = jetConnection.JetSession;
				bool flag = false;
				using (jetConnection.TrackTimeInDatabase())
				{
					try
					{
						flag = JetTableOperator.InternalPepareUpdate(jetSession, this.JetConnection.JetDatabase, base.Table.Name, this.jetCursor, primaryKeyUpdate);
						Api.JetSetColumns(jetSession, this.jetCursor, array, array.Length);
						if (this.detailTracingEnabled)
						{
							ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
						}
						if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
						{
							ExTraceGlobals.FaultInjectionTracer.TraceTest(2898668861U);
						}
						Api.JetUpdate(jetSession, this.jetCursor);
						flag = false;
					}
					finally
					{
						if (flag)
						{
							Api.JetPrepareUpdate(jetSession, this.jetCursor, JET_prep.Cancel);
						}
					}
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)40904U, "JetTableOperator:Update", ex);
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0002B528 File Offset: 0x00029728
		internal bool QuickDeleteAllMatchingRows(out int rowsDeleted)
		{
			rowsDeleted = 0;
			if (base.Criteria is SearchCriteriaFalse)
			{
				return true;
			}
			bool flag = false;
			if (base.Table.IsPartitioned && !this.TryOpenJetCursorIfNecessary(Connection.OperationType.Delete, true, out flag) && !flag)
			{
				return true;
			}
			if (base.Table.IsPartitioned && this.MatchesAllRecordsInPartition())
			{
				JetConnection jetConnection = base.Connection as JetConnection;
				rowsDeleted = -1;
				for (int i = 0; i < base.KeyRanges.Count; i++)
				{
					jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Write);
					this.DeletePartition(jetConnection, i);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002B5BC File Offset: 0x000297BC
		internal void Delete()
		{
			this.ResetCachedColumns();
			if (this.detailTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append(" >>> Delete row");
				ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			JetConnection jetConnection = this.JetConnection;
			jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Write);
			jetConnection.BeginTransactionIfNeeded(Connection.TransactionOption.NeedTransaction);
			try
			{
				using (jetConnection.TrackTimeInDatabase())
				{
					if (this.interruptControl != null && this.interruptControl.WantToInterrupt)
					{
						this.savedNavigationBookmark = Api.RetrieveKey(this.JetConnection.JetSession, this.jetCursor, RetrieveKeyGrbit.None);
					}
					Api.JetDelete(jetConnection.JetSession, this.jetCursor);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)36808U, "JetTableOperator.Delete", ex);
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002B6B0 File Offset: 0x000298B0
		internal int? GetPhysicalColumnNullableSize(JetPhysicalColumn column)
		{
			return this.GetPhysicalColumnNullableSize(column, false);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0002B6BC File Offset: 0x000298BC
		int IColumnStreamAccess.GetColumnSize(PhysicalColumn column)
		{
			return this.GetPhysicalColumnNullableSize((JetPhysicalColumn)column).GetValueOrDefault(0);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002B6DE File Offset: 0x000298DE
		int IColumnStreamAccess.ReadStream(PhysicalColumn physicalColumn, long position, byte[] buffer, int offset, int count)
		{
			return this.ReadBytesFromStream((JetPhysicalColumn)physicalColumn, position, buffer, offset, count);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0002B6F2 File Offset: 0x000298F2
		void IColumnStreamAccess.WriteStream(PhysicalColumn physicalColumn, long position, byte[] buffer, int offset, int count)
		{
			this.WriteBytesToStream((JetPhysicalColumn)physicalColumn, position, buffer, offset, count);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0002B708 File Offset: 0x00029908
		internal int ReadBytesFromStream(JetPhysicalColumn jetPhysicalColumn, long position, byte[] buffer, int offset, int count)
		{
			if (this.summaryTracingEnabled)
			{
				base.TraceOperation("ReadBytesFromStream", string.Format("position:[{0}] count:[{1}]", position, count));
			}
			this.OpenJetCursorIfNecessary(Connection.OperationType.Query);
			JetConnection jetConnection = base.Connection as JetConnection;
			jetConnection.CountStatement(Connection.OperationType.Query);
			if (position == 0L && object.ReferenceEquals(jetPhysicalColumn, base.Table.SpecialCols.OffPagePropertyBlob) && !this.hitOffPageBlob)
			{
				jetConnection.IncrementOffPageBlobHits();
				this.hitOffPageBlob = true;
			}
			JET_RETINFO jet_RETINFO = new JET_RETINFO();
			jet_RETINFO.ibLongValue = (int)position;
			jet_RETINFO.itagSequence = 1;
			ArraySegment<byte> userBuffer = new ArraySegment<byte>(buffer, offset, count);
			long? num = JetRetrieveColumnHelper.RetrieveColumnValueToArraySegment(jetConnection, this.jetCursor, jetPhysicalColumn.GetJetColumnId(jetConnection), userBuffer, jet_RETINFO);
			jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, (int)num.GetValueOrDefault(0L));
			if (this.intermediateTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append(" <<< Read chunk  col:[");
				jetPhysicalColumn.AppendToString(stringBuilder, StringFormatOptions.None);
				stringBuilder.Append("]=[");
				if (this.detailTracingEnabled || num.Value <= 32L)
				{
					stringBuilder.AppendAsString(buffer, offset, (int)num.GetValueOrDefault());
				}
				else
				{
					stringBuilder.Append("<long_blob>");
				}
				stringBuilder.Append("]");
				ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			return (int)num.GetValueOrDefault();
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002B870 File Offset: 0x00029A70
		internal void WriteBytesToStream(JetPhysicalColumn jetPhysicalColumn, long position, byte[] buffer, int offset, int count)
		{
			if (this.summaryTracingEnabled)
			{
				base.TraceOperation("WriteBytesToStream", string.Format("position:[{0}] count:[{1}]", position, count));
			}
			long num = (long)Math.Max(jetPhysicalColumn.MaxLength, jetPhysicalColumn.Size);
			if (num < position + (long)count)
			{
				DiagnosticContext.TraceDwordAndString((LID)61600U, (uint)num, jetPhysicalColumn.Name);
				DiagnosticContext.TraceDword((LID)37024U, (uint)(position + (long)count));
				throw new StoreException((LID)38368U, ErrorCodeValue.TooBig, string.Format("Value too big. Column {0}, Size {1}", jetPhysicalColumn.Name, position + (long)count));
			}
			this.OpenJetCursorIfNecessary(Connection.OperationType.Update);
			JetConnection jetConnection = base.Connection as JetConnection;
			jetConnection.CountStatement(Connection.OperationType.Update);
			JET_SETINFO jet_SETINFO = new JET_SETINFO();
			jet_SETINFO.ibLongValue = (int)position;
			jet_SETINFO.itagSequence = 1;
			SetColumnGrbit grbit = SetColumnGrbit.OverwriteLV;
			jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Write);
			jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.WriteBytes, buffer.Length);
			jetConnection.BeginTransactionIfNeeded(Connection.TransactionOption.NeedTransaction);
			if (this.intermediateTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append(" >>> Write chunk  col:[");
				jetPhysicalColumn.AppendToString(stringBuilder, StringFormatOptions.None);
				stringBuilder.Append("]=[");
				if (this.detailTracingEnabled || count <= 32)
				{
					stringBuilder.AppendAsString(buffer, offset, count);
				}
				else
				{
					stringBuilder.Append("<long_blob>");
				}
				stringBuilder.Append("]");
				ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			try
			{
				JET_COLUMNID jetColumnId = jetPhysicalColumn.GetJetColumnId(jetConnection);
				using (jetConnection.TrackTimeInDatabase())
				{
					Api.JetPrepareUpdate(jetConnection.JetSession, this.jetCursor, JET_prep.ReplaceNoLock);
					Api.JetSetColumn(jetConnection.JetSession, this.jetCursor, jetColumnId, buffer, count, offset, grbit, jet_SETINFO);
					Api.JetUpdate(jetConnection.JetSession, this.jetCursor);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)45000U, "JetTableOperator.WriteBytesToStream", ex);
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0002BA90 File Offset: 0x00029C90
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetTableOperator>(this);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002BA98 File Offset: 0x00029C98
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.ResetCachedColumns();
				if (this.jetCursor != JET_TABLEID.Nil)
				{
					this.CloseJetCursor();
					if (this.detailTracingEnabled)
					{
						StringBuilder stringBuilder = new StringBuilder(200);
						base.AppendOperationInfo("Dispose", stringBuilder);
						stringBuilder.Append("  rowsRead:[");
						stringBuilder.Append(this.rowsRead);
						stringBuilder.Append("]");
						ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
						this.rowsRead = 0U;
					}
				}
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002BB24 File Offset: 0x00029D24
		private static SetColumnGrbit GetInOrOutOfLineJetHint(int sizeOfValueInBytes, JetPhysicalColumn column)
		{
			if (sizeOfValueInBytes > 0)
			{
				int num = column.MaxInlineLength;
				if (column.ExtendedTypeCode == ExtendedTypeCode.String)
				{
					num *= 2;
				}
				if (sizeOfValueInBytes > num)
				{
					return SetColumnGrbit.SeparateLV;
				}
			}
			return SetColumnGrbit.IntrinsicLV;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002BB56 File Offset: 0x00029D56
		private static UpdateGrbit GetUpdateGrbit(bool unversioned)
		{
			if (!unversioned)
			{
				return UpdateGrbit.None;
			}
			return (UpdateGrbit)2;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002BB60 File Offset: 0x00029D60
		private int? GetPhysicalColumnNullableSize(JetPhysicalColumn column, bool compressedSize)
		{
			if (JetPartitionHelper.IsPartitioningColumn(base.Table, column))
			{
				return new int?(column.Size);
			}
			JetConnection jetConnection = this.JetConnection;
			if (object.ReferenceEquals(column, base.Table.SpecialCols.OffPagePropertyBlob) && !this.hitOffPageBlob)
			{
				jetConnection.IncrementOffPageBlobHits();
				this.hitOffPageBlob = true;
			}
			int? result;
			try
			{
				jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, 4);
				JET_COLUMNID jetColumnId = column.GetJetColumnId(jetConnection);
				RetrieveColumnGrbit retrieveColumnSizeGrbit = JetTableOperator.GetRetrieveColumnSizeGrbit(compressedSize);
				using (jetConnection.TrackTimeInDatabase())
				{
					result = Api.RetrieveColumnSize(jetConnection.JetSession, this.jetCursor, jetColumnId, 1, retrieveColumnSizeGrbit);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)57288U, "JetTableOperator.GetPhysicalColumnSize", ex);
			}
			return result;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002BC54 File Offset: 0x00029E54
		private void SetupColumnCaching()
		{
			if (this.columnCache != null)
			{
				return;
			}
			IList<Column> list2 = null;
			if (this.effectiveCriteria != null)
			{
				list2 = new List<Column>();
				this.effectiveCriteria.EnumerateColumns(delegate(Column c, object list)
				{
					((List<Column>)list).Add(c);
				}, list2, false);
			}
			HashSet<PhysicalColumn> hashSet = ConcurrentLookAside<HashSet<PhysicalColumn>>.Pool.Get();
			if (hashSet == null)
			{
				hashSet = new HashSet<PhysicalColumn>();
			}
			JetColumnValueHelper.GetPhysicalColumns(base.Table, list2, null, this, hashSet);
			HashSet<PhysicalColumn> hashSet2 = ConcurrentLookAside<HashSet<PhysicalColumn>>.Pool.Get();
			if (hashSet2 == null)
			{
				hashSet2 = new HashSet<PhysicalColumn>();
			}
			JetColumnValueHelper.GetPhysicalColumns(base.Table, base.ColumnsToFetch, hashSet, this, hashSet2);
			BitArray retrieveFromPrimaryBookmarkMap = null;
			BitArray retrieveFromIndexMap = null;
			if (!base.Index.PrimaryKey)
			{
				retrieveFromPrimaryBookmarkMap = new BitArray(base.Table.Columns.Count, false);
				retrieveFromIndexMap = new BitArray(base.Table.Columns.Count, false);
				JetRetrieveColumnHelper.BuildColumnRetrieveGrbitMap(hashSet, base.Table.PrimaryKeyIndex, base.Index, retrieveFromPrimaryBookmarkMap, retrieveFromIndexMap);
				JetRetrieveColumnHelper.BuildColumnRetrieveGrbitMap(hashSet2, base.Table.PrimaryKeyIndex, base.Index, retrieveFromPrimaryBookmarkMap, retrieveFromIndexMap);
			}
			this.restrictedColumnValues = new CachedColumnValues(this.JetConnection, hashSet, retrieveFromPrimaryBookmarkMap, retrieveFromIndexMap);
			this.fetchColumnValues = new CachedColumnValues(this.JetConnection, hashSet2, retrieveFromPrimaryBookmarkMap, retrieveFromIndexMap);
			this.columnCache = new Dictionary<PhysicalColumn, object>(hashSet.Count + hashSet2.Count);
			this.ResetCachedColumns();
			if (hashSet != null)
			{
				hashSet.Clear();
				ConcurrentLookAside<HashSet<PhysicalColumn>>.Pool.Put(hashSet);
			}
			if (hashSet2 != null)
			{
				hashSet2.Clear();
				ConcurrentLookAside<HashSet<PhysicalColumn>>.Pool.Put(hashSet2);
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002BDDC File Offset: 0x00029FDC
		private object GetColumnValue(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			IColumn column2 = column;
			return column2.GetValue(this);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002BE04 File Offset: 0x0002A004
		private byte[] GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			if (JetPartitionHelper.IsPartitioningColumn(base.Table, column))
			{
				StartStopKey startKey = base.KeyRanges[0].StartKey;
				return JetColumnValueHelper.GetAsByteArray(startKey.Values[column.Index], column);
			}
			JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)column;
			JetConnection jetConnection = this.JetConnection;
			JET_COLUMNID jetColumnId = jetPhysicalColumn.GetJetColumnId(jetConnection);
			if (object.ReferenceEquals(column, base.Table.SpecialCols.OffPagePropertyBlob) && !this.hitOffPageBlob)
			{
				jetConnection.IncrementOffPageBlobHits();
				this.hitOffPageBlob = true;
			}
			byte[] array;
			try
			{
				using (jetConnection.TrackTimeInDatabase())
				{
					array = Api.RetrieveColumn(jetConnection.JetSession, this.jetCursor, jetColumnId);
					jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, (array == null) ? 0 : array.Length);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)61384U, "JetTableOperator:GetColumn", ex);
			}
			return array;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002BF10 File Offset: 0x0002A110
		private object GetPhysicalColumnValue(PhysicalColumn column)
		{
			Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table = base.Table;
			if (JetPartitionHelper.IsPartitioningColumn(table, column))
			{
				StartStopKey startKey = base.KeyRanges[0].StartKey;
				return startKey.Values[column.Index];
			}
			object obj;
			if (this.columnCache.TryGetValue(column, out obj))
			{
				return obj;
			}
			JetConnection jetConnection = this.JetConnection;
			JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)column;
			JET_TABLEID tableid = this.jetCursor;
			JET_SESID jetSession = jetConnection.JetSession;
			JET_COLUMNID jetColumnId = jetPhysicalColumn.GetJetColumnId(jetConnection);
			if (object.ReferenceEquals(column, base.Table.SpecialCols.OffPagePropertyBlob) && !this.hitOffPageBlob)
			{
				jetConnection.IncrementOffPageBlobHits();
				this.hitOffPageBlob = true;
			}
			if (!this.restrictedColumnValues.TryGetValue(jetConnection, table, tableid, jetColumnId, out obj) && !this.fetchColumnValues.TryGetValue(jetConnection, table, tableid, jetColumnId, out obj))
			{
				if (this.intermediateTracingEnabled && !column.StreamSupport)
				{
					ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug<JetPhysicalColumn, JET_COLUMNID>(0L, "Column {0} (columnid {1}) isn't in the retrieval set. Retrieving directly from Jet", jetPhysicalColumn, jetColumnId);
				}
				if (jetPhysicalColumn.StreamSupport)
				{
					obj = JetColumnValueHelper.LoadFullOrTruncatedValue(jetPhysicalColumn, this);
				}
				else
				{
					Microsoft.Isam.Esent.Interop.ColumnValue columnValue = JetColumnValueHelper.CreateColumnValue(jetConnection, jetPhysicalColumn, null, null);
					try
					{
						using (jetConnection.TrackTimeInDatabase())
						{
							Api.RetrieveColumns(jetConnection.JetSession, tableid, new Microsoft.Isam.Esent.Interop.ColumnValue[]
							{
								columnValue
							});
						}
					}
					catch (EsentErrorException ex)
					{
						jetConnection.OnExceptionCatch(ex);
						throw jetConnection.ProcessJetError((LID)38704U, "JetTableOperator.GetPhysicalColumnValue", ex);
					}
					obj = columnValue.ValueAsObject;
					jetConnection.AddRowStatsCounter(table, RowStatsCounterType.ReadBytes, columnValue.Length);
				}
			}
			obj = JetColumnValueHelper.GetValueFromJetValue(column, obj);
			this.columnCache[column] = obj;
			return obj;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0002C0CC File Offset: 0x0002A2CC
		private object GetPropertyColumnValue(PropertyColumn column)
		{
			if (this.rowPropertyBag == null)
			{
				this.rowPropertyBag = column.PropertyBagCreator(this);
			}
			return this.rowPropertyBag.GetPropertyValue(base.Connection, column.StorePropTag);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0002C100 File Offset: 0x0002A300
		internal byte[] GetVirtualColumnValueAsBytes(VirtualColumn column)
		{
			VirtualColumnDefinition virtualColumnDefinition;
			if (!JetTable.SupportedVirtualColumns.TryGetValue(column.Name, out virtualColumnDefinition))
			{
				throw new NotSupportedException("Unrecognized virtual column");
			}
			if (virtualColumnDefinition.Type == typeof(int))
			{
				int value = (int)this.GetVirtualColumnValue(column);
				return BitConverter.GetBytes(value);
			}
			if (virtualColumnDefinition.Type == typeof(long))
			{
				long value2 = (long)this.GetVirtualColumnValue(column);
				return BitConverter.GetBytes(value2);
			}
			throw new NotSupportedException("Unsupported type for a virtual column");
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0002C18C File Offset: 0x0002A38C
		internal int GetVirtualColumnSize(VirtualColumn column)
		{
			VirtualColumnDefinition virtualColumnDefinition;
			if (JetTable.SupportedVirtualColumns.TryGetValue(column.Name, out virtualColumnDefinition))
			{
				return virtualColumnDefinition.Size;
			}
			throw new NotSupportedException("Unrecognized virtual column");
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002C1C0 File Offset: 0x0002A3C0
		object IRowAccess.GetPhysicalColumn(PhysicalColumn column)
		{
			Column column2;
			if (base.RenameDictionary != null && base.RenameDictionary.TryGetValue(column, out column2))
			{
				return column2.Evaluate(this);
			}
			return this.GetPhysicalColumnValue((JetPhysicalColumn)column);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002C1FC File Offset: 0x0002A3FC
		byte[] IJetSimpleQueryOperator.GetColumnValueAsBytes(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			IJetColumn jetColumn = (IJetColumn)column;
			return jetColumn.GetValueAsBytes(this);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002C228 File Offset: 0x0002A428
		byte[] IJetSimpleQueryOperator.GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			return this.GetPhysicalColumnValueAsBytes(column);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002C231 File Offset: 0x0002A431
		int IJetRecordCounter.GetCount()
		{
			return JetSimpleQueryOperatorHelper.GetCount(this);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002C239 File Offset: 0x0002A439
		int IJetRecordCounter.GetOrdinalPosition(SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			return JetSimpleQueryOperatorHelper.GetOrdinalPosition(this, sortOrder, stopKey, compareInfo);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002C244 File Offset: 0x0002A444
		int ITWIR.GetColumnSize(Column column)
		{
			return ((IColumn)column).GetSize(this);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002C25A File Offset: 0x0002A45A
		object ITWIR.GetColumnValue(Column column)
		{
			return this.GetColumnValue(column);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002C264 File Offset: 0x0002A464
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			int? physicalColumnNullableSize = this.GetPhysicalColumnNullableSize((JetPhysicalColumn)column, false);
			if (physicalColumnNullableSize == null)
			{
				return 0;
			}
			return physicalColumnNullableSize.GetValueOrDefault();
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002C294 File Offset: 0x0002A494
		internal int GetPhysicalColumnCompressedSize(PhysicalColumn column)
		{
			int? physicalColumnNullableSize = this.GetPhysicalColumnNullableSize((JetPhysicalColumn)column, true);
			if (physicalColumnNullableSize == null)
			{
				return 0;
			}
			return physicalColumnNullableSize.GetValueOrDefault();
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002C2C1 File Offset: 0x0002A4C1
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			return this.GetPhysicalColumnValue(column);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			object propertyColumnValue = this.GetPropertyColumnValue(column);
			return SizeOfColumn.GetColumnSize(column, propertyColumnValue).GetValueOrDefault();
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002C2F2 File Offset: 0x0002A4F2
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			return this.GetPropertyColumnValue(column);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002C2FB File Offset: 0x0002A4FB
		private bool FetchFromKey(JET_TABLEID jetCursor, int keyRangeIndex)
		{
			return this.FetchFromKey(jetCursor, base.KeyRanges[keyRangeIndex], base.Backwards);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0002C318 File Offset: 0x0002A518
		private bool FetchFromKey(JET_TABLEID jetCursor, KeyRange keyRange, bool backwards)
		{
			JetConnection jetConnection = this.JetConnection;
			bool result = false;
			try
			{
				StartStopKey startKey = keyRange.StartKey;
				StartStopKey stopKey = keyRange.StopKey;
				bool flag = startKey.Count < base.Index.Columns.Count;
				bool flag2 = false;
				int numberOfPartioningColumns = base.Table.SpecialCols.NumberOfPartioningColumns;
				for (int i = numberOfPartioningColumns; i < startKey.Count; i++)
				{
					MakeKeyGrbit makeKeyGrbit = (i == numberOfPartioningColumns) ? MakeKeyGrbit.NewKey : MakeKeyGrbit.None;
					if (flag && i == startKey.Count - 1)
					{
						if (startKey.Inclusive)
						{
							if (backwards)
							{
								makeKeyGrbit |= MakeKeyGrbit.FullColumnEndLimit;
							}
							else
							{
								makeKeyGrbit |= MakeKeyGrbit.FullColumnStartLimit;
							}
						}
						else if (backwards)
						{
							makeKeyGrbit |= MakeKeyGrbit.FullColumnStartLimit;
						}
						else
						{
							makeKeyGrbit |= MakeKeyGrbit.FullColumnEndLimit;
						}
					}
					JetColumnValueHelper.MakeJetKeyFromValue(jetConnection.JetSession, jetCursor, makeKeyGrbit, startKey.Values[i], base.SortOrder.Columns[i]);
					flag2 = true;
				}
				if (flag2)
				{
					SeekGrbit grbit;
					if (base.Index.Unique && startKey.Inclusive && stopKey.Inclusive && StartStopKey.CommonKeyPrefix(startKey, stopKey, base.CompareInfo) == base.Index.Columns.Count)
					{
						grbit = SeekGrbit.SeekEQ;
					}
					else if (!backwards)
					{
						grbit = (startKey.Inclusive ? SeekGrbit.SeekGE : SeekGrbit.SeekGT);
					}
					else
					{
						grbit = (startKey.Inclusive ? SeekGrbit.SeekLE : SeekGrbit.SeekLT);
					}
					jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Seek);
					result = Api.TrySeek(jetConnection.JetSession, jetCursor, grbit);
				}
				else if (backwards)
				{
					result = Api.TryMoveLast(jetConnection.JetSession, jetCursor);
				}
				else
				{
					result = Api.TryMoveFirst(jetConnection.JetSession, jetCursor);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)53192U, "JetTableOperator.FetchFromKey", ex);
			}
			return result;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002C508 File Offset: 0x0002A708
		private bool SetKeyRange(JET_TABLEID jetCursor, int keyRangeIndex)
		{
			JetConnection jetConnection = base.Connection as JetConnection;
			bool result = false;
			try
			{
				StartStopKey stopKey = base.KeyRanges[keyRangeIndex].StopKey;
				bool flag = stopKey.Count < base.Index.Columns.Count;
				bool flag2 = false;
				int numberOfPartioningColumns = base.Table.SpecialCols.NumberOfPartioningColumns;
				for (int i = numberOfPartioningColumns; i < stopKey.Count; i++)
				{
					MakeKeyGrbit makeKeyGrbit = (i == numberOfPartioningColumns) ? MakeKeyGrbit.NewKey : MakeKeyGrbit.None;
					if (flag && i == stopKey.Count - 1)
					{
						if (stopKey.Inclusive)
						{
							if (base.Backwards)
							{
								makeKeyGrbit |= MakeKeyGrbit.FullColumnStartLimit;
							}
							else
							{
								makeKeyGrbit |= MakeKeyGrbit.FullColumnEndLimit;
							}
						}
						else if (base.Backwards)
						{
							makeKeyGrbit |= MakeKeyGrbit.FullColumnEndLimit;
						}
						else
						{
							makeKeyGrbit |= MakeKeyGrbit.FullColumnStartLimit;
						}
					}
					JetColumnValueHelper.MakeJetKeyFromValue(jetConnection.JetSession, jetCursor, makeKeyGrbit, stopKey.Values[i], base.SortOrder.Columns[i]);
					flag2 = true;
				}
				if (flag2)
				{
					SetIndexRangeGrbit setIndexRangeGrbit = SetIndexRangeGrbit.None;
					if (!base.Backwards)
					{
						setIndexRangeGrbit = SetIndexRangeGrbit.RangeUpperLimit;
					}
					if (stopKey.Inclusive)
					{
						setIndexRangeGrbit |= SetIndexRangeGrbit.RangeInclusive;
					}
					result = Api.TrySetIndexRange(jetConnection.JetSession, jetCursor, setIndexRangeGrbit);
				}
				else
				{
					result = true;
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)47048U, "JetTableOperator.SetkeyRange", ex);
			}
			return result;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002C690 File Offset: 0x0002A890
		private void ResetCachedColumns()
		{
			this.hitOffPageBlob = false;
			this.restrictedColumnValues.Reset();
			this.fetchColumnValues.Reset();
			if (this.columnCache != null)
			{
				this.columnCache.Clear();
			}
			this.rowPropertyBag = null;
			this.ResetCachedVirtualColumns();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0002C6D0 File Offset: 0x0002A8D0
		private bool TryOpenJetCursorIfNecessary(Connection.OperationType operationType, bool checkForCorruptedPrimaryIndex, out bool primaryIndexCorrupted)
		{
			primaryIndexCorrupted = false;
			if (this.jetCursor == JET_TABLEID.Nil)
			{
				JetConnection jetConnection = this.JetConnection;
				string text = base.Table.IsPartitioned ? this.GetPartitionName() : base.Table.Name;
				try
				{
					if (base.Table.IsPartitioned)
					{
						JetConnection jetConnection2 = jetConnection;
						Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table = base.Table;
						string tableName = text;
						StartStopKey startKey = base.KeyRanges[0].StartKey;
						if (!jetConnection2.TryOpenTable(table, tableName, startKey.Values, operationType, checkForCorruptedPrimaryIndex, out primaryIndexCorrupted, out this.jetCursor))
						{
							this.jetCursor = JET_TABLEID.Nil;
						}
					}
					else
					{
						this.jetCursor = jetConnection.GetOpenTable(base.Table, text, null, operationType);
					}
					if (this.jetCursor != JET_TABLEID.Nil)
					{
						using (jetConnection.TrackTimeInDatabase())
						{
							Api.JetSetCurrentIndex(jetConnection.JetSession, this.jetCursor, base.Index.Name);
						}
					}
				}
				catch (EsentErrorException ex)
				{
					jetConnection.OnExceptionCatch(ex);
					throw jetConnection.ProcessJetError((LID)63432U, "JetTableOperator.TryOpenJetCursorIfNecessary", ex);
				}
			}
			return this.jetCursor != JET_TABLEID.Nil;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002C810 File Offset: 0x0002AA10
		private void OpenJetCursorIfNecessary(Connection.OperationType operationType)
		{
			if (base.Table.IsPartitioned)
			{
				JetPartitionHelper.CheckPartitionKeys(base.Table, base.Index, base.KeyRanges[0].StartKey, base.KeyRanges[0].StopKey);
				StartStopKey startKey = base.KeyRanges[0].StartKey;
				this.OpenJetCursorIfNecessary(startKey.Values, base.Table.SpecialCols.NumberOfPartioningColumns, operationType);
				return;
			}
			this.OpenJetCursorIfNecessary(null, 0, operationType);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002C898 File Offset: 0x0002AA98
		private void OpenJetCursorIfNecessary(IList<object> partitionValues, int numberOfPartitionKeyValues, Connection.OperationType operationType)
		{
			if (this.jetCursor == JET_TABLEID.Nil)
			{
				JetConnection jetConnection = this.JetConnection;
				try
				{
					if (base.Table.IsPartitioned)
					{
						if (partitionValues == null || numberOfPartitionKeyValues > partitionValues.Count || numberOfPartitionKeyValues == 0)
						{
							throw new ArgumentNullException("partitionValue");
						}
						this.OpenOrCreatePartition(jetConnection, partitionValues, numberOfPartitionKeyValues, operationType);
					}
					else
					{
						this.jetCursor = jetConnection.GetOpenTable(base.Table, base.Table.Name, null, operationType);
					}
					using (jetConnection.TrackTimeInDatabase())
					{
						Api.JetSetCurrentIndex(jetConnection.JetSession, this.jetCursor, base.Index.Name);
					}
				}
				catch (EsentErrorException ex)
				{
					jetConnection.OnExceptionCatch(ex);
					throw jetConnection.ProcessJetError((LID)38856U, "JetTableOperator.OpenJetCursorIfNecessary", ex);
				}
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002C984 File Offset: 0x0002AB84
		internal void CloseJetCursor()
		{
			if (this.jetCursor != JET_TABLEID.Nil)
			{
				JetConnection jetConnection = this.JetConnection;
				try
				{
					using (jetConnection.TrackTimeInDatabase())
					{
						if (this.usingPreRead)
						{
							if (this.jetCursorForReadAhead != JET_TABLEID.Nil)
							{
								Api.JetCloseTable(jetConnection.JetSession, this.jetCursorForReadAhead);
								this.jetCursorForReadAhead = JET_TABLEID.Nil;
							}
							if (this.jetCursorForPreread != JET_TABLEID.Nil)
							{
								Api.JetCloseTable(jetConnection.JetSession, this.jetCursorForPreread);
								this.jetCursorForPreread = JET_TABLEID.Nil;
							}
						}
						if (this.jetFilterSet)
						{
							this.jetFilterSet = false;
							Windows8Api.JetSetCursorFilter(jetConnection.JetSession, this.jetCursor, null, CursorFilterGrbit.None);
						}
						Api.JetCloseTable(jetConnection.JetSession, this.jetCursor);
						this.jetCursor = JET_TABLEID.Nil;
					}
				}
				catch (EsentErrorException ex)
				{
					jetConnection.OnExceptionCatch(ex);
					throw jetConnection.ProcessJetError((LID)55240U, "JetTableOperator.CloseJetCursor", ex);
				}
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002CAA4 File Offset: 0x0002ACA4
		private void OpenOrCreatePartition(JetConnection jetConnection, IList<object> partitionValues, int numberOfPartitionKeyValues, Connection.OperationType operationType)
		{
			string partitionName = JetPartitionHelper.GetPartitionName(base.Table, partitionValues, numberOfPartitionKeyValues);
			try
			{
				if (!jetConnection.TryOpenTable(base.Table, partitionName, partitionValues, operationType, out this.jetCursor))
				{
					this.jetCursor = JET_TABLEID.Nil;
					if (this.detailTracingEnabled)
					{
						ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "Creating partition table: " + partitionName);
					}
					jetConnection.BeginTransactionIfNeeded(Connection.TransactionOption.NeedTransaction);
					jetConnection.CreateDerivedTable(partitionName, base.Table.Name);
					this.jetCursor = jetConnection.GetOpenTable(base.Table, partitionName, partitionValues, operationType);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)42952U, "JetTableOperator.OpenOrCreatePartition", ex);
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002CB64 File Offset: 0x0002AD64
		private void DeletePartition(JetConnection jetConnection, int keyNum)
		{
			JetPartitionHelper.CheckPartitionKeys(base.Table, base.Index, base.KeyRanges[keyNum].StartKey, base.KeyRanges[keyNum].StopKey);
			if (this.jetCursor != JET_TABLEID.Nil)
			{
				this.CloseJetCursor();
			}
			try
			{
				Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table = base.Table;
				StartStopKey startKey = base.KeyRanges[keyNum].StartKey;
				string partitionName = JetPartitionHelper.GetPartitionName(table, startKey.Values, base.Table.SpecialCols.NumberOfPartioningColumns);
				if (this.detailTracingEnabled)
				{
					ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, "Deleting partition table: " + partitionName);
				}
				jetConnection.BeginTransactionIfNeeded(Connection.TransactionOption.NeedTransaction);
				jetConnection.DeleteTable(partitionName);
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)59336U, "JetTableOperator.DeletePartition", ex);
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002CC50 File Offset: 0x0002AE50
		private bool MatchesAllRecordsInPartition()
		{
			JetPartitionHelper.CheckPartitionKeys(base.Table, base.Index, base.KeyRanges[0].StartKey, base.KeyRanges[0].StopKey);
			if (base.Criteria == null || base.Criteria is SearchCriteriaTrue)
			{
				StartStopKey startKey = base.KeyRanges[0].StartKey;
				if (startKey.Count == base.Table.SpecialCols.NumberOfPartioningColumns)
				{
					StartStopKey stopKey = base.KeyRanges[0].StopKey;
					if (stopKey.Count == base.Table.SpecialCols.NumberOfPartioningColumns)
					{
						StartStopKey stopKey2 = base.KeyRanges[0].StopKey;
						if (stopKey2.Inclusive)
						{
							StartStopKey startKey2 = base.KeyRanges[0].StartKey;
							if (startKey2.Inclusive)
							{
								int num = 0;
								StartStopKey startKey3 = base.KeyRanges[0].StartKey;
								object values = startKey3.Values;
								StartStopKey stopKey3 = base.KeyRanges[0].StopKey;
								return num == ValueHelper.ValuesCompare(values, stopKey3.Values);
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002CD78 File Offset: 0x0002AF78
		private string GetPartitionName()
		{
			JetPartitionHelper.CheckPartitionKeys(base.Table, base.Index, base.KeyRanges[0].StartKey, base.KeyRanges[0].StopKey);
			Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table = base.Table;
			StartStopKey startKey = base.KeyRanges[0].StartKey;
			return JetPartitionHelper.GetPartitionName(table, startKey.Values, base.Table.SpecialCols.NumberOfPartioningColumns);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002D008 File Offset: 0x0002B208
		private JET_INDEX_COLUMN[] ExtractJetSimpleFilter(SearchCriteria criteria, out SearchCriteria simplifiedCriteria)
		{
			List<JET_INDEX_COLUMN> filterList = null;
			simplifiedCriteria = criteria;
			if (criteria is SearchCriteriaAnd || criteria is SearchCriteriaCompare || criteria is SearchCriteriaBitMask)
			{
				simplifiedCriteria = criteria.InspectAndFix(delegate(SearchCriteria criterion, CompareInfo compareInfo)
				{
					Column column = null;
					JetRelop relop = JetRelop.Equals;
					object obj = null;
					if (criterion is SearchCriteriaAnd)
					{
						return criterion;
					}
					if (criterion is SearchCriteriaCompare)
					{
						SearchCriteriaCompare searchCriteriaCompare = (SearchCriteriaCompare)criterion;
						if (searchCriteriaCompare.Rhs is ConstantColumn)
						{
							column = searchCriteriaCompare.Lhs;
							switch (searchCriteriaCompare.RelOp)
							{
							case SearchCriteriaCompare.SearchRelOp.Equal:
								relop = JetRelop.Equals;
								break;
							case SearchCriteriaCompare.SearchRelOp.NotEqual:
								relop = JetRelop.NotEquals;
								break;
							case SearchCriteriaCompare.SearchRelOp.LessThan:
								relop = JetRelop.LessThan;
								break;
							case SearchCriteriaCompare.SearchRelOp.LessThanEqual:
								relop = JetRelop.LessThanOrEqual;
								break;
							case SearchCriteriaCompare.SearchRelOp.GreaterThan:
								relop = JetRelop.GreaterThan;
								break;
							case SearchCriteriaCompare.SearchRelOp.GreaterThanEqual:
								relop = JetRelop.GreaterThanOrEqual;
								break;
							}
							obj = ((ConstantColumn)searchCriteriaCompare.Rhs).Value;
						}
					}
					else if (criterion is SearchCriteriaBitMask)
					{
						SearchCriteriaBitMask searchCriteriaBitMask = (SearchCriteriaBitMask)criterion;
						if (searchCriteriaBitMask.Rhs is ConstantColumn)
						{
							column = searchCriteriaBitMask.Lhs;
							relop = ((searchCriteriaBitMask.Op == SearchCriteriaBitMask.SearchBitMaskOp.EqualToZero) ? JetRelop.BitmaskEqualsZero : JetRelop.BitmaskNotEqualsZero);
							obj = ((ConstantColumn)searchCriteriaBitMask.Rhs).Value;
						}
					}
					if (column != null)
					{
						if (this.RenameDictionary != null)
						{
							column = this.ResolveColumn(column);
						}
						JetPhysicalColumn jetPhysicalColumn = column.ActualColumn as JetPhysicalColumn;
						if (jetPhysicalColumn != null && !JetPartitionHelper.IsPartitioningColumn(this.Table, jetPhysicalColumn) && (byte)(jetPhysicalColumn.ExtendedTypeCode & ExtendedTypeCode.MVFlag) == 0 && jetPhysicalColumn.ExtendedTypeCode != ExtendedTypeCode.String && (jetPhysicalColumn.ExtendedTypeCode != ExtendedTypeCode.Binary || (jetPhysicalColumn.MaxLength <= 32 && jetPhysicalColumn.Size <= 32)))
						{
							JetConnection jetConnection = this.JetConnection;
							JET_COLUMNID jetColumnId = jetPhysicalColumn.GetJetColumnId(jetConnection);
							JetIndexColumnGrbit grbit = JetIndexColumnGrbit.None;
							if (jetPhysicalColumn.ExtendedTypeCode == ExtendedTypeCode.Binary && obj != null && ((byte[])obj).Length == 0)
							{
								grbit = JetIndexColumnGrbit.ZeroLength;
								obj = null;
							}
							JET_INDEX_COLUMN item = new JET_INDEX_COLUMN
							{
								columnid = jetColumnId,
								relop = relop,
								pvData = ((obj != null) ? JetColumnValueHelper.GetAsByteArray(obj, jetPhysicalColumn) : null),
								grbit = grbit
							};
							if (filterList == null)
							{
								filterList = new List<JET_INDEX_COLUMN>(4);
							}
							filterList.Add(item);
							return Factory.CreateSearchCriteriaTrue();
						}
					}
					return null;
				}, base.CompareInfo, true);
			}
			if (simplifiedCriteria is SearchCriteriaTrue)
			{
				simplifiedCriteria = null;
			}
			if (filterList != null)
			{
				return filterList.ToArray();
			}
			return null;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002D088 File Offset: 0x0002B288
		private bool InternalMoveFirst(JET_TABLEID jetCursor, ref int keyRangeIndex)
		{
			keyRangeIndex = 0;
			JetConnection jetConnection = this.JetConnection;
			StartStopKey startKey = base.KeyRanges[keyRangeIndex].StartKey;
			bool flag;
			if (!startKey.IsEmpty)
			{
				flag = this.FetchFromKey(jetCursor, keyRangeIndex);
			}
			else if (base.Backwards)
			{
				flag = Api.TryMoveLast(jetConnection.JetSession, jetCursor);
			}
			else
			{
				flag = Api.TryMoveFirst(jetConnection.JetSession, jetCursor);
			}
			if (flag)
			{
				StartStopKey stopKey = base.KeyRanges[keyRangeIndex].StopKey;
				if (!stopKey.IsEmpty)
				{
					flag = this.SetKeyRange(jetCursor, keyRangeIndex);
				}
			}
			if (!flag)
			{
				flag = this.MoveToNextRange(jetCursor, ref keyRangeIndex);
			}
			return flag;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002D124 File Offset: 0x0002B324
		private bool InternalMoveNext(JET_TABLEID jetCursor, ref int keyRangeIndex)
		{
			JetConnection jetConnection = this.JetConnection;
			bool flag;
			if (base.Backwards)
			{
				flag = Api.TryMovePrevious(jetConnection.JetSession, jetCursor);
			}
			else
			{
				flag = Api.TryMoveNext(jetConnection.JetSession, jetCursor);
			}
			if (!flag)
			{
				flag = this.MoveToNextRange(jetCursor, ref keyRangeIndex);
			}
			return flag;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002D16C File Offset: 0x0002B36C
		private bool MoveToNextRange(JET_TABLEID jetCursor, ref int keyRangeIndex)
		{
			bool flag = false;
			while (!flag && keyRangeIndex < base.KeyRanges.Count)
			{
				keyRangeIndex++;
				if (keyRangeIndex < base.KeyRanges.Count)
				{
					flag = this.FetchFromKey(jetCursor, keyRangeIndex);
					if (flag)
					{
						StartStopKey stopKey = base.KeyRanges[keyRangeIndex].StopKey;
						if (!stopKey.IsEmpty)
						{
							flag = this.SetKeyRange(jetCursor, keyRangeIndex);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002D1D8 File Offset: 0x0002B3D8
		private void PrereadOnPrimaryIndex()
		{
			bool flag = true;
			if (this.jetCursorForReadAhead == JET_TABLEID.Nil)
			{
				using (this.JetConnection.TrackTimeInDatabase())
				{
					this.jetCursorForReadAhead = JetTableOperator.InternalDupCursor(this.JetConnection.JetSession, this.JetConnection.JetDatabase, base.Table.Name, this.jetCursor);
					Api.JetSetCurrentIndex(this.JetConnection.JetSession, this.jetCursorForReadAhead, base.Index.Name);
					Api.JetSetTableSequential(this.JetConnection.JetSession, this.jetCursorForReadAhead, base.Backwards ? ((SetTableSequentialGrbit)2) : ((SetTableSequentialGrbit)1));
					if (base.Index.Unique)
					{
						byte[] data = Api.RetrieveKey(this.JetConnection.JetSession, this.jetCursor, RetrieveKeyGrbit.None);
						Api.MakeKey(this.JetConnection.JetSession, this.jetCursorForReadAhead, data, MakeKeyGrbit.NormalizedKey);
						flag = Api.TrySeek(this.JetConnection.JetSession, this.jetCursorForReadAhead, SeekGrbit.SeekEQ);
					}
					else
					{
						byte[] array = new byte[2048];
						byte[] array2 = new byte[2048];
						int secondaryKeySize;
						int primaryKeySize;
						Api.JetGetSecondaryIndexBookmark(this.JetConnection.JetSession, this.jetCursor, array, array.Length, out secondaryKeySize, array2, array2.Length, out primaryKeySize, GetSecondaryIndexBookmarkGrbit.None);
						Api.JetGotoSecondaryIndexBookmark(this.JetConnection.JetSession, this.jetCursorForReadAhead, array, secondaryKeySize, array2, primaryKeySize, GotoSecondaryIndexBookmarkGrbit.None);
					}
					this.keyRangeIndexForReadAhead = this.keyRangeIndex;
					StartStopKey stopKey = base.KeyRanges[this.keyRangeIndexForReadAhead].StopKey;
					if (!stopKey.IsEmpty)
					{
						this.SetKeyRange(this.jetCursorForReadAhead, this.keyRangeIndexForReadAhead);
					}
					int num = 0;
					while (num < this.remainingPreread && flag)
					{
						flag = this.InternalMoveNext(this.jetCursorForReadAhead, ref this.keyRangeIndexForReadAhead);
						num++;
					}
				}
			}
			int num2 = 0;
			byte[][] array3 = new byte[base.PrereadChunkSize][];
			while (flag)
			{
				byte[] bookmark = Api.GetBookmark(this.JetConnection.JetSession, this.jetCursorForReadAhead);
				array3[num2++] = bookmark;
				if (num2 == base.PrereadChunkSize)
				{
					break;
				}
				flag = this.InternalMoveNext(this.jetCursorForReadAhead, ref this.keyRangeIndexForReadAhead);
			}
			if (!flag)
			{
				this.prereadIsEOF = true;
			}
			if (num2 > 1)
			{
				if (this.jetCursorForPreread == JET_TABLEID.Nil)
				{
					using (this.JetConnection.TrackTimeInDatabase())
					{
						this.jetCursorForPreread = JetTableOperator.InternalDupCursor(this.JetConnection.JetSession, this.JetConnection.JetDatabase, base.Table.Name, this.jetCursor);
					}
				}
				Array.Resize<byte[]>(ref array3, num2);
				int num3 = JetPreReadOperator.PreReadKeys(this.JetConnection, this.jetCursorForPreread, array3, base.LongValueColumnsToPreread);
				if (this.intermediateTracingEnabled)
				{
					ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, string.Concat(new object[]
					{
						"Requested preread ",
						array3.Length,
						" keys, actual preread: ",
						num3,
						" keys."
					}));
				}
				if (num3 < num2)
				{
					if (this.intermediateTracingEnabled)
					{
						ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, string.Format("Preread fewer keys than asked: {0}/{1}", num2, num3));
					}
					if (base.PrereadChunkSize > 25)
					{
						base.PrereadChunkSize = Math.Max(num3, Math.Max(25, base.PrereadChunkSize * 2 / 3));
					}
				}
				this.remainingPreread += num2;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0002D588 File Offset: 0x0002B788
		public override bool Interrupted
		{
			get
			{
				return this.interrupted;
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002D590 File Offset: 0x0002B790
		void IJetSimpleQueryOperator.RequestResume()
		{
			base.TraceCrumb("RequestResume", "Resume");
			this.Resume();
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0002D5A9 File Offset: 0x0002B7A9
		bool IJetSimpleQueryOperator.CanMoveBack
		{
			get
			{
				return base.Index.PrimaryKey && base.Criteria == null && base.KeyRanges.Count == 1;
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0002D5D0 File Offset: 0x0002B7D0
		void IJetSimpleQueryOperator.MoveBackAndInterrupt(int rows)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(((IJetSimpleQueryOperator)this).CanMoveBack, "This method should only be used if operator can support move backwards.");
			JetConnection jetConnection = this.JetConnection;
			using (base.Connection.TrackTimeInDatabase())
			{
				if (this.interrupted)
				{
					base.TraceCrumb("MoveBackAndInterrupt", "Resume");
					this.Resume();
				}
				else if (this.keyRangeIndex == base.KeyRanges.Count)
				{
					this.keyRangeIndex--;
					KeyRange keyRange = new KeyRange(base.KeyRanges[this.keyRangeIndex].StopKey, base.KeyRanges[this.keyRangeIndex].StartKey);
					this.FetchFromKey(this.jetCursor, keyRange, !base.Backwards);
				}
				this.savedNavigationBookmark = null;
				base.TraceCrumb("MoveBackAndInterrupt", rows);
				while (rows != 0)
				{
					if (base.Backwards)
					{
						Api.TryMoveNext(jetConnection.JetSession, this.jetCursor);
					}
					else
					{
						Api.TryMovePrevious(jetConnection.JetSession, this.jetCursor);
					}
					rows--;
				}
				this.Interrupt();
				base.TraceCrumb("MoveBackAndInterrupt", "Interrupt");
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002D714 File Offset: 0x0002B914
		private bool Interrupt()
		{
			if (this.savedNavigationBookmark == null)
			{
				try
				{
					this.savedNavigationBookmark = Api.RetrieveKey(this.JetConnection.JetSession, this.jetCursor, RetrieveKeyGrbit.None);
				}
				catch (EsentRecordDeletedException exception)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
					return false;
				}
			}
			this.CloseJetCursor();
			this.interrupted = true;
			return true;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002D778 File Offset: 0x0002B978
		private bool Resume()
		{
			this.interrupted = false;
			bool flag;
			if (!this.TryOpenJetCursorIfNecessary(Connection.OperationType.Query, false, out flag))
			{
				return false;
			}
			if (!this.matchesAtMostOneRow)
			{
				using (this.JetConnection.TrackTimeInDatabase())
				{
					Api.JetSetTableSequential(this.JetConnection.JetSession, this.jetCursor, base.Backwards ? ((SetTableSequentialGrbit)2) : ((SetTableSequentialGrbit)1));
				}
			}
			using (base.Connection.TrackTimeInDatabase())
			{
				Api.MakeKey(this.JetConnection.JetSession, this.jetCursor, this.savedNavigationBookmark, MakeKeyGrbit.NormalizedKey);
				Api.TrySeek(this.JetConnection.JetSession, this.jetCursor, SeekGrbit.SeekEQ);
				StartStopKey stopKey = base.KeyRanges[this.keyRangeIndex].StopKey;
				if (!stopKey.IsEmpty)
				{
					this.SetKeyRange(this.jetCursor, this.keyRangeIndex);
				}
			}
			return true;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002D884 File Offset: 0x0002BA84
		internal object GetVirtualColumnValue(VirtualColumn column)
		{
			switch (column.VirtualColumnId)
			{
			case VirtualColumnId.PageNumber:
				return Api.RetrieveColumnAsInt32(this.JetConnection.JetSession, this.jetCursor, JET_COLUMNID.Nil, (RetrieveColumnGrbit)4096).Value;
			case VirtualColumnId.DataSize:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cbData;
			case VirtualColumnId.LongValueDataSize:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cbLongValueData;
			case VirtualColumnId.OverheadSize:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cbOverhead;
			case VirtualColumnId.LongValueOverheadSize:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cbLongValueOverhead;
			case VirtualColumnId.NonTaggedColumnCount:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cNonTaggedColumns;
			case VirtualColumnId.TaggedColumnCount:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cTaggedColumns;
			case VirtualColumnId.LongValueCount:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cLongValues;
			case VirtualColumnId.MultiValueCount:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cMultiValues;
			case VirtualColumnId.CompressedColumnCount:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cCompressedColumns;
			case VirtualColumnId.CompressedDataSize:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cbDataCompressed;
			case VirtualColumnId.CompressedLongValueDataSize:
				this.LoadRecordSize();
				return this.cachedRecSize.Value.cbLongValueDataCompressed;
			default:
				throw new NotSupportedException("Unrecognized virtual column");
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002DA6B File Offset: 0x0002BC6B
		internal void ResetCachedVirtualColumns()
		{
			this.cachedRecSize = null;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002DA7C File Offset: 0x0002BC7C
		private void LoadRecordSize()
		{
			if (this.cachedRecSize == null)
			{
				JET_RECSIZE value = default(JET_RECSIZE);
				VistaApi.JetGetRecordSize(this.JetConnection.JetSession, this.jetCursor, ref value, GetRecordSizeGrbit.None);
				this.cachedRecSize = new JET_RECSIZE?(value);
			}
		}

		// Token: 0x04000308 RID: 776
		private readonly bool matchesAtMostOneRow;

		// Token: 0x04000309 RID: 777
		private readonly bool usingPreRead;

		// Token: 0x0400030A RID: 778
		public static bool EnableJetFiltering = true;

		// Token: 0x0400030B RID: 779
		private CachedColumnValues restrictedColumnValues;

		// Token: 0x0400030C RID: 780
		private CachedColumnValues fetchColumnValues;

		// Token: 0x0400030D RID: 781
		private Dictionary<PhysicalColumn, object> columnCache;

		// Token: 0x0400030E RID: 782
		private bool hitOffPageBlob;

		// Token: 0x0400030F RID: 783
		private int keyRangeIndex;

		// Token: 0x04000310 RID: 784
		private JET_TABLEID jetCursor;

		// Token: 0x04000311 RID: 785
		private IInterruptControl interruptControl;

		// Token: 0x04000312 RID: 786
		private bool interrupted;

		// Token: 0x04000313 RID: 787
		private byte[] savedNavigationBookmark;

		// Token: 0x04000314 RID: 788
		private uint rowsReturned;

		// Token: 0x04000315 RID: 789
		private uint rowsRead;

		// Token: 0x04000316 RID: 790
		private int keyRangeIndexForReadAhead;

		// Token: 0x04000317 RID: 791
		private JET_TABLEID jetCursorForReadAhead;

		// Token: 0x04000318 RID: 792
		private JET_TABLEID jetCursorForPreread;

		// Token: 0x04000319 RID: 793
		private int remainingPreread;

		// Token: 0x0400031A RID: 794
		private bool prereadIsEOF;

		// Token: 0x0400031B RID: 795
		private IRowPropertyBag rowPropertyBag;

		// Token: 0x0400031C RID: 796
		private bool jetFilterSet;

		// Token: 0x0400031D RID: 797
		private SearchCriteria effectiveCriteria;

		// Token: 0x0400031E RID: 798
		private JET_RECSIZE? cachedRecSize = null;
	}
}
