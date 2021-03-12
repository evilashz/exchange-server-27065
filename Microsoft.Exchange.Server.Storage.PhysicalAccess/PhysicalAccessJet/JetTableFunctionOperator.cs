using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000C8 RID: 200
	internal class JetTableFunctionOperator : TableFunctionOperator, IJetSimpleQueryOperator, IJetRecordCounter, ITWIR
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x000297E0 File Offset: 0x000279E0
		internal JetTableFunctionOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, object[] parameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation) : this(connectionProvider, new TableFunctionOperator.TableFunctionOperatorDefinition(culture, tableFunction, parameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, frequentOperation))
		{
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00029810 File Offset: 0x00027A10
		internal JetTableFunctionOperator(IConnectionProvider connectionProvider, TableFunctionOperator.TableFunctionOperatorDefinition definition) : base(connectionProvider, definition)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				for (int i = 0; i < base.KeyRanges.Count; i++)
				{
					StartStopKey startKey = base.KeyRanges[i].StartKey;
					if (!startKey.IsEmpty)
					{
						JetTableFunctionOperator.CheckKeyColumnsMatchIndexColumns(base.KeyRanges[i].StartKey, base.Table.PrimaryKeyIndex);
					}
					StartStopKey stopKey = base.KeyRanges[i].StopKey;
					if (!stopKey.IsEmpty)
					{
						JetTableFunctionOperator.CheckKeyColumnsMatchIndexColumns(base.KeyRanges[i].StopKey, base.Table.PrimaryKeyIndex);
					}
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000298E4 File Offset: 0x00027AE4
		public override uint RowsReturned
		{
			get
			{
				return this.rowsReturned;
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000298EC File Offset: 0x00027AEC
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			base.Connection.CountStatement(Connection.OperationType.Query);
			return new JetReader(base.ConnectionProvider, this, disposeQueryOperator);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00029912 File Offset: 0x00027B12
		public override bool EnableInterrupts(IInterruptControl interruptControl)
		{
			if (interruptControl != null && base.SkipTo != 0)
			{
				return false;
			}
			this.interruptControl = interruptControl;
			return true;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002992C File Offset: 0x00027B2C
		public bool MoveFirst(out int rowsSkipped)
		{
			rowsSkipped = 0;
			this.rowsReturned = 0U;
			this.keyRangeIndex = 0;
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
			if (this.interruptControl != null)
			{
				this.interrupted = false;
			}
			this.tableContents = (IEnumerable)base.TableFunction.GetTableContents(base.ConnectionProvider, base.Parameters);
			if (this.tableContents == null)
			{
				base.TraceMove("MoveFirst", false);
				return false;
			}
			IConfigurableTableContents configurableTableContents = this.tableContents as IConfigurableTableContents;
			if (configurableTableContents != null)
			{
				configurableTableContents.Configure(base.Backwards, base.KeyRanges[0].StartKey);
			}
			this.enumerator = this.tableContents.GetEnumerator();
			if (base.Backwards && configurableTableContents == null)
			{
				Stack<object> stack = new Stack<object>();
				while (this.enumerator.MoveNext())
				{
					stack.Push(this.enumerator.Current);
				}
				this.enumerator = ((IEnumerable)stack).GetEnumerator();
			}
			while (this.enumerator != null && this.enumerator.MoveNext())
			{
				if (this.CursorIsAfterAllKeyRanges())
				{
					base.TraceMove("MoveFirst", false);
					return false;
				}
				if (this.CursorIsInValidKeyRange())
				{
					base.Connection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Read);
					int num = base.SkipTo;
					if (base.Criteria == null || base.Criteria.Evaluate(this, base.CompareInfo))
					{
						if (num <= 0)
						{
							base.TraceMove("MoveFirst", true);
							base.Connection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Accept);
							this.rowsReturned += 1U;
							return true;
						}
						rowsSkipped++;
						num--;
					}
					return this.MoveNext("MoveFirst", num, ref rowsSkipped);
				}
			}
			IRefillableTableContents refillableTableContents = this.tableContents as IRefillableTableContents;
			if (refillableTableContents != null)
			{
				refillableTableContents.MarkChunkConsumed();
				if (refillableTableContents.CanRefill)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.interruptControl != null, "We cannot refill if interrupts are disabled");
					this.Interrupt();
					this.enumerator = null;
					base.TraceCrumb("MoveFirst", "Interrupt to refill");
					return true;
				}
			}
			base.TraceMove("MoveFirst", false);
			return false;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00029B5C File Offset: 0x00027D5C
		public bool MoveNext()
		{
			int num = 0;
			return this.MoveNext("MoveNext", 0, ref num);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00029B7C File Offset: 0x00027D7C
		public int GetColumnSize(Column column)
		{
			return ((IColumn)column).GetSize(this);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00029B94 File Offset: 0x00027D94
		public byte[] GetColumnValueAsBytes(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			IJetColumn jetColumn = (IJetColumn)column;
			return jetColumn.GetValueAsBytes(this);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00029BC0 File Offset: 0x00027DC0
		internal bool MoveNext(string operation, int numberLeftToSkip, ref int rowsSkipped)
		{
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
			if (base.MaxRows > 0 && (ulong)this.rowsReturned >= (ulong)((long)base.MaxRows))
			{
				base.TraceMove(operation, false);
				return false;
			}
			while (this.interruptControl == null || !this.interruptControl.WantToInterrupt)
			{
				if (!this.enumerator.MoveNext())
				{
					IRefillableTableContents refillableTableContents = this.tableContents as IRefillableTableContents;
					if (refillableTableContents != null)
					{
						refillableTableContents.MarkChunkConsumed();
						if (refillableTableContents.CanRefill)
						{
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.interruptControl != null, "We cannot refill if interrupts are disabled");
							this.Interrupt();
							this.enumerator = null;
							base.TraceCrumb(operation, "Interrupt to refill");
							return true;
						}
					}
					base.TraceMove(operation, false);
					return false;
				}
				if (this.CursorIsAfterAllKeyRanges())
				{
					base.TraceMove(operation, false);
					return false;
				}
				if (this.CursorIsInValidKeyRange())
				{
					base.Connection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Read);
					if (base.Criteria == null || base.Criteria.Evaluate(this, base.CompareInfo))
					{
						if (numberLeftToSkip <= 0)
						{
							base.TraceMove(operation, true);
							base.Connection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Accept);
							this.rowsReturned += 1U;
							return true;
						}
						rowsSkipped++;
						numberLeftToSkip--;
					}
				}
			}
			this.Interrupt();
			base.TraceCrumb(operation, "Interrupt");
			return true;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00029D3A File Offset: 0x00027F3A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetTableFunctionOperator>(this);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00029D42 File Offset: 0x00027F42
		protected override void InternalDispose(bool calledFromDispose)
		{
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00029D44 File Offset: 0x00027F44
		private static void CheckKeyColumnsMatchIndexColumns(StartStopKey key, Index index)
		{
			if (key.Count > index.ColumnCount)
			{
				throw new InvalidOperationException("Key has more columns than primary index");
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00029D60 File Offset: 0x00027F60
		private static int AdjustKeyColumnCompareResults(bool isAscending, bool isBackwards, int compareResults)
		{
			if (!isAscending)
			{
				compareResults = -compareResults;
			}
			if (isBackwards)
			{
				compareResults = -compareResults;
			}
			return compareResults;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00029D71 File Offset: 0x00027F71
		byte[] IJetSimpleQueryOperator.GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			return this.GetPhysicalColumnValueAsBytes(column);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00029D7A File Offset: 0x00027F7A
		int IJetRecordCounter.GetCount()
		{
			return JetSimpleQueryOperatorHelper.GetCount(this);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00029D82 File Offset: 0x00027F82
		int IJetRecordCounter.GetOrdinalPosition(SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			return JetSimpleQueryOperatorHelper.GetOrdinalPosition(this, sortOrder, stopKey, compareInfo);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00029D8D File Offset: 0x00027F8D
		object ITWIR.GetColumnValue(Column column)
		{
			return this.GetColumnValue(column);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00029D98 File Offset: 0x00027F98
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			object physicalColumnValue = ((ITWIR)this).GetPhysicalColumnValue(column);
			return SizeOfColumn.GetColumnSize(column, physicalColumnValue).GetValueOrDefault();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00029DC0 File Offset: 0x00027FC0
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			object obj = base.TableFunction.GetColumnFromRow(base.ConnectionProvider, this.enumerator.Current, column);
			base.Connection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, ValueTypeHelper.ValueSize(column.ExtendedTypeCode, obj));
			return obj;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00029E0F File Offset: 0x0002800F
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			throw new InvalidOperationException("TableFunctions do not support property columns");
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00029E1B File Offset: 0x0002801B
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			throw new InvalidOperationException("TableFunctions do not support property columns");
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00029E28 File Offset: 0x00028028
		private object GetColumnValue(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			IColumn column2 = column;
			return column2.GetValue(this);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00029E50 File Offset: 0x00028050
		private byte[] GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			object obj = base.TableFunction.GetColumnFromRow(base.ConnectionProvider, this.enumerator.Current, column);
			base.Connection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, ValueTypeHelper.ValueSize(column.ExtendedTypeCode, obj));
			return JetColumnValueHelper.GetAsByteArray(obj, column);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00029EA8 File Offset: 0x000280A8
		private int CompareCurrentRecordToKey(StartStopKey key)
		{
			for (int i = 0; i < key.Count; i++)
			{
				Column column = base.SortOrder.Columns[i];
				object columnValue = this.GetColumnValue(column);
				int num = JetTableFunctionOperator.AdjustKeyColumnCompareResults(base.Table.PrimaryKeyIndex.Ascending[i], base.Backwards, ValueHelper.ValuesCompare(columnValue, key.Values[i], base.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth));
				if (num != 0)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00029F28 File Offset: 0x00028128
		private bool CursorIsInValidKeyRange()
		{
			while (this.CursorIsAfterStartKey())
			{
				if (!this.CursorIsAfterStopKey())
				{
					return true;
				}
				this.keyRangeIndex++;
				if (this.keyRangeIndex >= base.KeyRanges.Count)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00029F62 File Offset: 0x00028162
		private bool CursorIsAfterAllKeyRanges()
		{
			return this.keyRangeIndex >= base.KeyRanges.Count;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00029F7C File Offset: 0x0002817C
		private bool CursorIsAfterStartKey()
		{
			StartStopKey startKey = base.KeyRanges[this.keyRangeIndex].StartKey;
			return startKey.IsEmpty || this.CursorIsAfterKey(base.KeyRanges[this.keyRangeIndex].StartKey, false);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00029FC8 File Offset: 0x000281C8
		private bool CursorIsAfterStopKey()
		{
			StartStopKey stopKey = base.KeyRanges[this.keyRangeIndex].StopKey;
			return !stopKey.IsEmpty && this.CursorIsAfterKey(base.KeyRanges[this.keyRangeIndex].StopKey, true);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002A014 File Offset: 0x00028214
		private bool CursorIsAfterKey(StartStopKey key, bool isStopKey)
		{
			int num = this.CompareCurrentRecordToKey(key);
			return num >= 0 && (num > 0 || key.Inclusive != isStopKey);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0002A042 File Offset: 0x00028242
		public override bool Interrupted
		{
			get
			{
				return this.interrupted;
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002A04A File Offset: 0x0002824A
		void IJetSimpleQueryOperator.RequestResume()
		{
			base.TraceCrumb("RequestResume", "Resume");
			this.Resume();
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0002A063 File Offset: 0x00028263
		bool IJetSimpleQueryOperator.CanMoveBack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002A066 File Offset: 0x00028266
		void IJetSimpleQueryOperator.MoveBackAndInterrupt(int rows)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "This method should never be called.");
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0002A073 File Offset: 0x00028273
		private void Interrupt()
		{
			this.interrupted = true;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002A07C File Offset: 0x0002827C
		private bool Resume()
		{
			this.interrupted = false;
			if (this.enumerator == null)
			{
				this.enumerator = this.tableContents.GetEnumerator();
			}
			return true;
		}

		// Token: 0x04000301 RID: 769
		private const string TableFunctionsDoNotSupportPropertyColumns = "TableFunctions do not support property columns";

		// Token: 0x04000302 RID: 770
		private IInterruptControl interruptControl;

		// Token: 0x04000303 RID: 771
		private bool interrupted;

		// Token: 0x04000304 RID: 772
		private uint rowsReturned;

		// Token: 0x04000305 RID: 773
		private IEnumerable tableContents;

		// Token: 0x04000306 RID: 774
		private IEnumerator enumerator;

		// Token: 0x04000307 RID: 775
		private int keyRangeIndex;
	}
}
