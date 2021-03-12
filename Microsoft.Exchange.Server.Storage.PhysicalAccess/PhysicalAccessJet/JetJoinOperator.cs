using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000AC RID: 172
	internal class JetJoinOperator : JoinOperator, IJetSimpleQueryOperator, IJetRecordCounter, ITWIR, IRowAccess, IColumnStreamAccess
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x00023898 File Offset: 0x00021A98
		internal JetJoinOperator(CultureInfo culture, IConnectionProvider connectionProvider, Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<Column> keyColumns, SimpleQueryOperator outerQuery, bool frequentOperation) : this(connectionProvider, new JoinOperator.JoinOperatorDefinition(culture, table, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyColumns, outerQuery.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000238CC File Offset: 0x00021ACC
		internal JetJoinOperator(IConnectionProvider connectionProvider, JoinOperator.JoinOperatorDefinition definition) : base(connectionProvider, definition)
		{
			if (!this.CanUsePreRead())
			{
				base.PreReadCacheSize = 0;
				return;
			}
			if (base.MaxRows > 0 && base.SkipTo + base.MaxRows < 150)
			{
				base.PreReadCacheSize /= 2;
			}
			if (base.OuterQuery.MaxRows > 0)
			{
				base.PreReadCacheSize = Math.Min(base.PreReadCacheSize, base.OuterQuery.MaxRows);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00023946 File Offset: 0x00021B46
		private JetConnection JetConnection
		{
			get
			{
				return (JetConnection)base.Connection;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00023953 File Offset: 0x00021B53
		private IJetSimpleQueryOperator JetOuterQuery
		{
			get
			{
				return (IJetSimpleQueryOperator)base.OuterQuery;
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00023960 File Offset: 0x00021B60
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			this.ResetCachedColumns();
			base.Connection.CountStatement(Connection.OperationType.Query);
			return new JetReader(base.ConnectionProvider, this, disposeQueryOperator);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0002398C File Offset: 0x00021B8C
		public override bool EnableInterrupts(IInterruptControl interruptControl)
		{
			if (interruptControl != null && base.SkipTo != 0)
			{
				return false;
			}
			if (!base.OuterQuery.EnableInterrupts(interruptControl))
			{
				return false;
			}
			this.interruptControl = interruptControl;
			this.outerCanMoveBack = (interruptControl != null && this.JetOuterQuery.CanMoveBack);
			return true;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000239CC File Offset: 0x00021BCC
		public bool MoveFirst(out int rowsSkipped)
		{
			rowsSkipped = 0;
			this.rowsReturned = 0U;
			this.ResetCachedColumns();
			if (base.Criteria is SearchCriteriaFalse)
			{
				base.TraceMove("MoveFirst", false);
				return false;
			}
			if (!this.MoveFirstFromOuter(out rowsSkipped))
			{
				base.TraceMove("MoveFirst", false);
				return false;
			}
			this.SetupColumnCaching();
			int num = base.SkipTo;
			if (this.interrupted)
			{
				return true;
			}
			JetConnection jetConnection = this.JetConnection;
			bool flag = this.FetchFromKey(new StartStopKey(true, (IList<object>)this.outerValues.Key));
			if (flag)
			{
				jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Read);
				if (base.Criteria != null)
				{
					bool flag2 = base.Criteria.Evaluate(this, base.CompareInfo);
					bool? flag3 = new bool?(true);
					if (!flag2 || flag3 == null)
					{
						goto IL_FE;
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
			}
			else
			{
				this.TraceInnerRowNotFound();
			}
			IL_FE:
			return this.MoveNext("MoveFirst", num, ref rowsSkipped);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00023AE4 File Offset: 0x00021CE4
		public bool MoveNext()
		{
			int num = 0;
			return this.MoveNext("MoveNext", 0, ref num);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00023B04 File Offset: 0x00021D04
		internal bool MoveNext(string operation, int numberLeftToSkip, ref int rowsSkipped)
		{
			bool flag = false;
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
			if (base.MaxRows <= 0 || (ulong)this.rowsReturned < (ulong)((long)base.MaxRows))
			{
				JetConnection jetConnection = this.JetConnection;
				using (base.Connection.TrackTimeInDatabase())
				{
					for (;;)
					{
						flag = false;
						this.ResetCachedColumns();
						flag = this.MoveNextFromOuter();
						if (!flag)
						{
							goto IL_123;
						}
						if (this.interrupted)
						{
							break;
						}
						flag = this.FetchFromKey(new StartStopKey(true, (IList<object>)this.outerValues.Key));
						if (!flag)
						{
							this.TraceInnerRowNotFound();
						}
						else
						{
							jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Read);
							if (base.Criteria != null)
							{
								bool flag2 = base.Criteria.Evaluate(this, base.CompareInfo);
								bool? flag3 = new bool?(true);
								if (!flag2 || flag3 == null)
								{
									continue;
								}
							}
							if (numberLeftToSkip <= 0)
							{
								goto IL_106;
							}
							rowsSkipped++;
							numberLeftToSkip--;
						}
					}
					return true;
					IL_106:
					jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Accept);
					flag = true;
					this.rowsReturned += 1U;
					IL_123:;
				}
			}
			base.TraceMove(operation, flag);
			return flag;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00023C70 File Offset: 0x00021E70
		private void SetupColumnCaching()
		{
			if (this.columnCache != null)
			{
				return;
			}
			this.outerColumnsToFetch = new Dictionary<Column, int>(base.OuterQuery.ColumnsToFetch.Count);
			for (int i = 0; i < base.OuterQuery.ColumnsToFetch.Count; i++)
			{
				Column column = base.OuterQuery.ColumnsToFetch[i];
				if (!(column is ConstantColumn))
				{
					this.outerColumnsToFetch.Add(column, i);
				}
			}
			IList<Column> list2 = null;
			if (base.Criteria != null)
			{
				list2 = new List<Column>();
				base.Criteria.EnumerateColumns(delegate(Column c, object list)
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
			HashSet<Column> hashSet2 = new HashSet<Column>(base.ColumnsToFetch);
			hashSet2.RemoveWhere(new Predicate<Column>(this.outerColumnsToFetch.ContainsKey));
			HashSet<PhysicalColumn> hashSet3 = ConcurrentLookAside<HashSet<PhysicalColumn>>.Pool.Get();
			if (hashSet3 == null)
			{
				hashSet3 = new HashSet<PhysicalColumn>();
			}
			JetColumnValueHelper.GetPhysicalColumns(base.Table, hashSet2, hashSet, this, hashSet3);
			JetConnection jetConnection = this.JetConnection;
			this.restrictedColumnValues = new CachedColumnValues(jetConnection, hashSet, null, null);
			this.fetchColumnValues = new CachedColumnValues(jetConnection, hashSet3, null, null);
			this.columnCache = new Dictionary<PhysicalColumn, object>(hashSet.Count + hashSet3.Count);
			if (hashSet != null)
			{
				hashSet.Clear();
				ConcurrentLookAside<HashSet<PhysicalColumn>>.Pool.Put(hashSet);
			}
			if (hashSet3 != null)
			{
				hashSet3.Clear();
				ConcurrentLookAside<HashSet<PhysicalColumn>>.Pool.Put(hashSet3);
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00023E04 File Offset: 0x00022004
		private byte[] GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			if (JetPartitionHelper.IsPartitioningColumn(base.Table, column))
			{
				return JetColumnValueHelper.GetAsByteArray(this.outerValues.Key[column.Index], column);
			}
			byte[] array = null;
			JetConnection jetConnection = this.JetConnection;
			if (object.ReferenceEquals(column, base.Table.SpecialCols.OffPagePropertyBlob) && !this.hitOffPageBlob)
			{
				jetConnection.IncrementOffPageBlobHits();
				this.hitOffPageBlob = true;
			}
			try
			{
				JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)column;
				JET_COLUMNID jetColumnId = jetPhysicalColumn.GetJetColumnId(jetConnection);
				using (jetConnection.TrackTimeInDatabase())
				{
					array = Api.RetrieveColumn(jetConnection.JetSession, this.jetCursor, jetColumnId);
					jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, (array == null) ? 0 : array.Length);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)47560U, "JetJoinOperator.GetColumn", ex);
			}
			return array;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00023F00 File Offset: 0x00022100
		private object GetPhysicalColumnValue(PhysicalColumn column)
		{
			Microsoft.Exchange.Server.Storage.PhysicalAccess.Table table = base.Table;
			if (JetPartitionHelper.IsPartitioningColumn(table, column))
			{
				return this.outerValues.Key[column.Index];
			}
			object obj;
			if (this.columnCache.TryGetValue(column, out obj))
			{
				return obj;
			}
			JetConnection jetConnection = this.JetConnection;
			JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)column;
			JET_TABLEID tableid = this.jetCursor;
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
					Microsoft.Isam.Esent.Interop.ColumnValue columnValue = JetColumnValueHelper.CreateColumnValue((JetConnection)base.Connection, jetPhysicalColumn, null, null);
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
						throw jetConnection.ProcessJetError((LID)50992U, "JetJoinOperator.GetPhysicalColumnValue", ex);
					}
					obj = columnValue.ValueAsObject;
					jetConnection.AddRowStatsCounter(table, RowStatsCounterType.ReadBytes, columnValue.Length);
				}
			}
			obj = JetColumnValueHelper.GetValueFromJetValue(column, obj);
			this.columnCache[column] = obj;
			return obj;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000240B0 File Offset: 0x000222B0
		private object GetPropertyColumnValue(PropertyColumn column)
		{
			if (this.rowPropertyBag == null)
			{
				this.rowPropertyBag = column.PropertyBagCreator(this);
			}
			return this.rowPropertyBag.GetPropertyValue(base.Connection, column.StorePropTag);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000240E4 File Offset: 0x000222E4
		byte[] IJetSimpleQueryOperator.GetColumnValueAsBytes(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			int outerColumnIndex;
			if (this.outerColumnsToFetch.TryGetValue(column, out outerColumnIndex))
			{
				return JetColumnValueHelper.GetAsByteArray(this.GetOuterValue(outerColumnIndex), column);
			}
			IJetColumn jetColumn = (IJetColumn)column;
			return jetColumn.GetValueAsBytes(this);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0002412E File Offset: 0x0002232E
		byte[] IJetSimpleQueryOperator.GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			return this.GetPhysicalColumnValueAsBytes(column);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00024137 File Offset: 0x00022337
		int IJetRecordCounter.GetCount()
		{
			return JetSimpleQueryOperatorHelper.GetCount(this);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002413F File Offset: 0x0002233F
		int IJetRecordCounter.GetOrdinalPosition(SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			return JetSimpleQueryOperatorHelper.GetOrdinalPosition(this, sortOrder, stopKey, compareInfo);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0002414C File Offset: 0x0002234C
		int ITWIR.GetColumnSize(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			int outerColumnIndex;
			if (this.outerColumnsToFetch.TryGetValue(column, out outerColumnIndex))
			{
				return SizeOfColumn.GetColumnSize(column, this.GetOuterValue(outerColumnIndex)).GetValueOrDefault(0);
			}
			IColumn column2 = column;
			return column2.GetSize(this);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0002419C File Offset: 0x0002239C
		object ITWIR.GetColumnValue(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			int outerColumnIndex;
			if (this.outerColumnsToFetch.TryGetValue(column, out outerColumnIndex))
			{
				return this.GetOuterValue(outerColumnIndex);
			}
			IColumn column2 = column;
			return column2.GetValue(this);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000241DB File Offset: 0x000223DB
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			return this.GetPhysicalColumnSize(column, false);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000241E5 File Offset: 0x000223E5
		internal int GetPhysicalColumnCompressedSize(PhysicalColumn column)
		{
			return this.GetPhysicalColumnSize(column, true);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000241F0 File Offset: 0x000223F0
		private int GetPhysicalColumnSize(PhysicalColumn column, bool compressedSize)
		{
			if (JetPartitionHelper.IsPartitioningColumn(base.Table, column))
			{
				return column.Size;
			}
			JetConnection jetConnection = this.JetConnection;
			if (object.ReferenceEquals(column, base.Table.SpecialCols.OffPagePropertyBlob) && !this.hitOffPageBlob)
			{
				jetConnection.IncrementOffPageBlobHits();
				this.hitOffPageBlob = true;
			}
			int result;
			try
			{
				JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)column;
				JET_COLUMNID jetColumnId = jetPhysicalColumn.GetJetColumnId(jetConnection);
				RetrieveColumnGrbit retrieveColumnSizeGrbit = JetTableOperator.GetRetrieveColumnSizeGrbit(compressedSize);
				using (jetConnection.TrackTimeInDatabase())
				{
					int? num = Api.RetrieveColumnSize(jetConnection.JetSession, this.jetCursor, jetColumnId, 1, retrieveColumnSizeGrbit);
					jetConnection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, 4);
					result = (num ?? 0);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)63944U, "JetJoinOperator.GetPhysicalColumnSize", ex);
			}
			return result;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000242F4 File Offset: 0x000224F4
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			return this.GetPhysicalColumnValue(column);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00024300 File Offset: 0x00022500
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			object propertyColumnValue = this.GetPropertyColumnValue(column);
			return SizeOfColumn.GetColumnSize(column, propertyColumnValue).GetValueOrDefault();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00024326 File Offset: 0x00022526
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			return this.GetPropertyColumnValue(column);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00024330 File Offset: 0x00022530
		object IRowAccess.GetPhysicalColumn(PhysicalColumn column)
		{
			if (column.Index == -1 && base.RenameDictionary != null)
			{
				Column column2 = base.ResolveColumn(column);
				if (column2 != column)
				{
					return column2.Evaluate(this);
				}
			}
			return this.GetPhysicalColumnValue(column);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0002436E File Offset: 0x0002256E
		int IColumnStreamAccess.GetColumnSize(PhysicalColumn column)
		{
			return ((ITWIR)this).GetPhysicalColumnSize(column);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00024378 File Offset: 0x00022578
		int IColumnStreamAccess.ReadStream(PhysicalColumn physicalColumn, long position, byte[] buffer, int offset, int count)
		{
			if (this.summaryTracingEnabled)
			{
				base.TraceOperation("ReadBytesFromStream", string.Format("position:[{0}] count:[{1}]", position, count));
			}
			JetConnection jetConnection = this.JetConnection;
			JetPhysicalColumn jetPhysicalColumn = (JetPhysicalColumn)physicalColumn;
			if (position == 0L && object.ReferenceEquals(physicalColumn, base.Table.SpecialCols.OffPagePropertyBlob) && !this.hitOffPageBlob)
			{
				jetConnection.IncrementOffPageBlobHits();
				this.hitOffPageBlob = true;
			}
			JET_RETINFO jet_RETINFO = new JET_RETINFO();
			jet_RETINFO.ibLongValue = (int)position;
			jet_RETINFO.itagSequence = 1;
			ArraySegment<byte> userBuffer = new ArraySegment<byte>(buffer, offset, count);
			long? num = JetRetrieveColumnHelper.RetrieveColumnValueToArraySegment(jetConnection, this.jetCursor, jetPhysicalColumn.GetJetColumnId(jetConnection), userBuffer, jet_RETINFO);
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

		// Token: 0x06000790 RID: 1936 RVA: 0x000244BB File Offset: 0x000226BB
		void IColumnStreamAccess.WriteStream(PhysicalColumn physicalColumn, long position, byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Write is not supported");
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000244C7 File Offset: 0x000226C7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetJoinOperator>(this);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000244D0 File Offset: 0x000226D0
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				try
				{
					if (this.jetCursorOpen)
					{
						this.CloseJetCursor();
					}
					if (this.detailTracingEnabled)
					{
						StringBuilder stringBuilder = new StringBuilder(200);
						base.AppendOperationInfo("Dispose", stringBuilder);
						stringBuilder.Append("  rowsRead:[");
						stringBuilder.Append(this.rowsRead);
						stringBuilder.Append("]");
						ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
					}
				}
				finally
				{
					base.InternalDispose(calledFromDispose);
				}
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00024560 File Offset: 0x00022760
		private void TraceInnerRowNotFound()
		{
			if (this.intermediateTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				base.AppendOperationInfo("INNER ROW NOT FOUND", stringBuilder);
				if (base.OuterQuery.ColumnsToFetch != null)
				{
					stringBuilder.Append("  outer_columns:[");
					if (base.PreReadCacheSize > 1)
					{
						for (int i = 0; i < base.OuterQuery.ColumnsToFetch.Count; i++)
						{
							if (i != 0)
							{
								stringBuilder.Append(", ");
							}
							base.OuterQuery.ColumnsToFetch[i].AppendToString(stringBuilder, StringFormatOptions.None);
							stringBuilder.Append("=[");
							stringBuilder.AppendAsString(this.GetOuterValue(i));
							stringBuilder.Append("]");
						}
					}
					else
					{
						base.TraceAppendColumns(stringBuilder, this.JetOuterQuery, base.OuterQuery.ColumnsToFetch);
					}
					stringBuilder.Append("]");
				}
				ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00024650 File Offset: 0x00022850
		private bool FetchFromKey(StartStopKey startKey)
		{
			if (!this.TryOpenJetCursorIfNecessary(startKey))
			{
				return false;
			}
			JetConnection jetConnection = this.JetConnection;
			bool result = false;
			try
			{
				using (jetConnection.TrackTimeInDatabase())
				{
					int numberOfPartioningColumns = base.Table.SpecialCols.NumberOfPartioningColumns;
					for (int i = numberOfPartioningColumns; i < startKey.Values.Count; i++)
					{
						MakeKeyGrbit grbit = (i == numberOfPartioningColumns) ? MakeKeyGrbit.NewKey : MakeKeyGrbit.None;
						JetColumnValueHelper.MakeJetKeyFromValue(jetConnection.JetSession, this.jetCursor, grbit, startKey.Values[i], this.GetKeyIndex().SortOrder.Columns[i]);
					}
					jetConnection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Seek);
					result = Api.TrySeek(jetConnection.JetSession, this.jetCursor, SeekGrbit.SeekEQ);
				}
				this.rowsRead += 1U;
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)55752U, "JetJoinOperator.FetchFromKey", ex);
			}
			return result;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00024764 File Offset: 0x00022964
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
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000247A0 File Offset: 0x000229A0
		private bool TryOpenJetCursorIfNecessary(StartStopKey key)
		{
			if (base.Table.IsPartitioned)
			{
				JetPartitionHelper.CheckPartitionKeys(base.Table, base.Table.PrimaryKeyIndex, key, key);
			}
			if (!this.jetCursorOpen)
			{
				JetConnection jetConnection = this.JetConnection;
				string tableName = base.Table.IsPartitioned ? JetPartitionHelper.GetPartitionName(base.Table, key.Values, base.Table.SpecialCols.NumberOfPartioningColumns) : base.Table.Name;
				try
				{
					if (base.Table.IsPartitioned)
					{
						if (jetConnection.TryOpenTable(base.Table, tableName, key.Values, Connection.OperationType.Query, out this.jetCursor))
						{
							this.jetCursorOpen = true;
						}
						this.partitionKey = key.Values[0];
					}
					else
					{
						this.jetCursor = jetConnection.GetOpenTable(base.Table, tableName, null, Connection.OperationType.Query);
						this.jetCursorOpen = true;
					}
					Api.JetSetCurrentIndex(jetConnection.JetSession, this.jetCursor, this.GetKeyIndex().Name);
					goto IL_14A;
				}
				catch (EsentErrorException ex)
				{
					jetConnection.OnExceptionCatch(ex);
					throw jetConnection.ProcessJetError((LID)43464U, "JetJoinOperator.TryOpenJetCursorIfNecessary", ex);
				}
			}
			if (base.Table.IsPartitioned && ValueHelper.ValuesCompare(this.partitionKey, key.Values[0]) != 0)
			{
				throw new ArgumentOutOfRangeException("key", "partition key has changed");
			}
			IL_14A:
			return this.jetCursorOpen;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00024910 File Offset: 0x00022B10
		private void CloseJetCursor()
		{
			JetConnection jetConnection = this.JetConnection;
			try
			{
				this.jetCursorOpen = false;
				using (jetConnection.TrackTimeInDatabase())
				{
					Api.JetCloseTable(jetConnection.JetSession, this.jetCursor);
				}
			}
			catch (EsentErrorException ex)
			{
				jetConnection.OnExceptionCatch(ex);
				throw jetConnection.ProcessJetError((LID)39368U, "JetJoinOperator.InternalDispose", ex);
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00024990 File Offset: 0x00022B90
		private Index GetKeyIndex()
		{
			if (this.keyIndex == null)
			{
				for (int i = 0; i < base.Table.Indexes.Count; i++)
				{
					bool flag = true;
					if (base.Table.Indexes[i].Columns.Count < base.KeyColumns.Count)
					{
						flag = false;
					}
					else
					{
						for (int j = 0; j < base.KeyColumns.Count; j++)
						{
							if (base.Table.Indexes[i].Columns[j].Name != base.KeyColumns[j].Name)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						this.keyIndex = base.Table.Indexes[i];
						break;
					}
				}
			}
			return this.keyIndex;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00024A6C File Offset: 0x00022C6C
		private bool CanUsePreRead()
		{
			return base.OuterQuery.MaxRows != 1;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00024A80 File Offset: 0x00022C80
		private bool MoveFirstFromOuter(out int rowsSkipped)
		{
			if (this.interruptControl != null)
			{
				this.interrupted = false;
			}
			if (base.PreReadCacheSize <= 1)
			{
				bool flag = this.JetOuterQuery.MoveFirst(out rowsSkipped);
				if (flag)
				{
					if (this.interruptControl != null)
					{
						if (this.JetOuterQuery.Interrupted)
						{
							this.Interrupt();
							base.TraceCrumb("MoveFirst", "Interrupt");
							return true;
						}
						this.interruptControl.RegisterRead(true, base.Table.TableClass);
					}
					this.outerValues = this.GetValuesFromOuterWithoutPreReadCache();
				}
				return flag;
			}
			this.preReadIsEOF = false;
			this.joinValuesFromOuter = new Queue<KeyValuePair<object[], object[]>>(base.PreReadCacheSize + base.PreReadCacheSize / 3);
			this.prereadsToConsume = 0;
			if (!this.JetOuterQuery.MoveFirst(out rowsSkipped))
			{
				this.preReadIsEOF = true;
				return false;
			}
			if (this.interruptControl != null && this.JetOuterQuery.Interrupted)
			{
				this.Interrupt();
				base.TraceCrumb("MoveFirst", "Interrupt");
				return true;
			}
			this.PopulateOuterCache();
			this.outerValues = this.joinValuesFromOuter.Dequeue();
			this.prereadsToConsume--;
			return true;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00024B9C File Offset: 0x00022D9C
		private bool MoveNextFromOuter()
		{
			if (base.PreReadCacheSize <= 1 || (this.outerCanMoveBack && this.prereadsToConsume != 0 && this.joinValuesFromOuter.Count == 0))
			{
				bool flag = this.JetOuterQuery.MoveNext();
				if (flag)
				{
					if (this.interruptControl != null)
					{
						if (this.JetOuterQuery.Interrupted)
						{
							this.Interrupt();
							base.TraceCrumb("MoveNext", "Interrupt");
							return true;
						}
						this.interruptControl.RegisterRead(true, base.Table.TableClass);
					}
					this.outerValues = this.GetValuesFromOuterWithoutPreReadCache();
					if (this.prereadsToConsume > 0)
					{
						this.prereadsToConsume--;
					}
				}
				return flag;
			}
			bool flag2 = false;
			if (!this.preReadIsEOF && (this.interruptControl == null || !this.JetOuterQuery.Interrupted) && ((base.PreReadAhead && this.joinValuesFromOuter.Count < base.PreReadCacheSize / 3) || this.joinValuesFromOuter.Count == 0))
			{
				if (this.JetOuterQuery.MoveNext())
				{
					if (this.interruptControl == null || !this.JetOuterQuery.Interrupted)
					{
						flag2 = true;
						this.PopulateOuterCache();
					}
				}
				else
				{
					this.preReadIsEOF = true;
				}
			}
			if (this.interruptControl != null && this.outerCanMoveBack && (this.joinValuesFromOuter.Count != 0 || this.preReadIsEOF) && !flag2 && (this.JetOuterQuery.Interrupted || this.interruptControl.WantToInterrupt))
			{
				this.JetOuterQuery.MoveBackAndInterrupt(this.joinValuesFromOuter.Count);
				this.joinValuesFromOuter.Clear();
				this.preReadIsEOF = false;
			}
			if (this.joinValuesFromOuter.Count != 0)
			{
				this.outerValues = this.joinValuesFromOuter.Dequeue();
				this.prereadsToConsume--;
				return true;
			}
			if (this.interruptControl != null && this.JetOuterQuery.Interrupted)
			{
				this.Interrupt();
				base.TraceCrumb("MoveNext", "Interrupt");
				return true;
			}
			return false;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00024D90 File Offset: 0x00022F90
		private void PopulateOuterCache()
		{
			using (base.Connection.TrackTimeInDatabase())
			{
				List<KeyRange> list = new List<KeyRange>(base.PreReadCacheSize);
				while (list.Count < base.PreReadCacheSize && !this.preReadIsEOF)
				{
					if (this.interruptControl != null)
					{
						this.interruptControl.RegisterRead(true, base.Table.TableClass);
					}
					KeyValuePair<object[], object[]> valuesFromOuterWithoutPreReadCache = this.GetValuesFromOuterWithoutPreReadCache();
					this.joinValuesFromOuter.Enqueue(valuesFromOuterWithoutPreReadCache);
					this.prereadsToConsume++;
					StartStopKey startStopKey = new StartStopKey(true, (IList<object>)valuesFromOuterWithoutPreReadCache.Key);
					list.Add(new KeyRange(startStopKey, startStopKey));
					if (list.Count < base.PreReadCacheSize)
					{
						if (this.JetOuterQuery.MoveNext())
						{
							if (this.interruptControl != null && this.JetOuterQuery.Interrupted)
							{
								break;
							}
						}
						else
						{
							this.preReadIsEOF = true;
						}
					}
				}
				if (list.Count > 1)
				{
					using (PreReadOperator preReadOperator = Factory.CreatePreReadOperator(base.Culture, base.Connection, base.Table, this.GetKeyIndex(), list, base.LongValueColumnsToPreread, true))
					{
						int num = (int)preReadOperator.ExecuteScalar();
						if (num < list.Count)
						{
							if (this.intermediateTracingEnabled)
							{
								ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, string.Format("Preread fewer keys than asked: {0}/{1}", list.Count, num));
							}
							if (base.PreReadCacheSize > 25)
							{
								base.PreReadCacheSize = Math.Max(num, Math.Max(25, base.PreReadCacheSize * 2 / 3));
							}
						}
					}
				}
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00024F60 File Offset: 0x00023160
		private KeyValuePair<object[], object[]> GetValuesFromOuterWithoutPreReadCache()
		{
			IList<Column> columnsToFetch = base.OuterQuery.ColumnsToFetch;
			object[] array = new object[base.KeyColumns.Count];
			int i;
			for (i = 0; i < base.KeyColumns.Count; i++)
			{
				array[i] = this.JetOuterQuery.GetColumnValue(columnsToFetch[i]);
			}
			object[] array2 = null;
			if (i < columnsToFetch.Count)
			{
				array2 = new object[columnsToFetch.Count - base.KeyColumns.Count];
				while (i < columnsToFetch.Count)
				{
					array2[i - base.KeyColumns.Count] = this.JetOuterQuery.GetColumnValue(columnsToFetch[i]);
					i++;
				}
			}
			return new KeyValuePair<object[], object[]>(array, array2);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0002500F File Offset: 0x0002320F
		private object GetOuterValue(int outerColumnIndex)
		{
			if (outerColumnIndex >= base.KeyColumns.Count)
			{
				return this.outerValues.Value[outerColumnIndex - base.KeyColumns.Count];
			}
			return this.outerValues.Key[outerColumnIndex];
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00025046 File Offset: 0x00023246
		public override bool Interrupted
		{
			get
			{
				return this.interrupted;
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0002504E File Offset: 0x0002324E
		void IJetSimpleQueryOperator.RequestResume()
		{
			base.TraceCrumb("RequestResume", "Resume");
			this.Resume();
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00025067 File Offset: 0x00023267
		bool IJetSimpleQueryOperator.CanMoveBack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0002506A File Offset: 0x0002326A
		void IJetSimpleQueryOperator.MoveBackAndInterrupt(int rows)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "This method should never be called.");
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00025077 File Offset: 0x00023277
		private void Interrupt()
		{
			this.interrupted = true;
			if (this.jetCursorOpen)
			{
				this.CloseJetCursor();
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0002508E File Offset: 0x0002328E
		private bool Resume()
		{
			this.interrupted = false;
			this.JetOuterQuery.RequestResume();
			return true;
		}

		// Token: 0x040002B5 RID: 693
		private CachedColumnValues restrictedColumnValues;

		// Token: 0x040002B6 RID: 694
		private CachedColumnValues fetchColumnValues;

		// Token: 0x040002B7 RID: 695
		private KeyValuePair<object[], object[]> outerValues;

		// Token: 0x040002B8 RID: 696
		private Dictionary<PhysicalColumn, object> columnCache;

		// Token: 0x040002B9 RID: 697
		private bool hitOffPageBlob;

		// Token: 0x040002BA RID: 698
		private Dictionary<Column, int> outerColumnsToFetch;

		// Token: 0x040002BB RID: 699
		private JET_TABLEID jetCursor;

		// Token: 0x040002BC RID: 700
		private IInterruptControl interruptControl;

		// Token: 0x040002BD RID: 701
		private bool interrupted;

		// Token: 0x040002BE RID: 702
		private bool outerCanMoveBack;

		// Token: 0x040002BF RID: 703
		private bool jetCursorOpen;

		// Token: 0x040002C0 RID: 704
		private uint rowsReturned;

		// Token: 0x040002C1 RID: 705
		private uint rowsRead;

		// Token: 0x040002C2 RID: 706
		private IRowPropertyBag rowPropertyBag;

		// Token: 0x040002C3 RID: 707
		private Index keyIndex;

		// Token: 0x040002C4 RID: 708
		private object partitionKey;

		// Token: 0x040002C5 RID: 709
		private Queue<KeyValuePair<object[], object[]>> joinValuesFromOuter;

		// Token: 0x040002C6 RID: 710
		private int prereadsToConsume;

		// Token: 0x040002C7 RID: 711
		private bool preReadIsEOF;
	}
}
