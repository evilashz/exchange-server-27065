using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000CD RID: 205
	internal class JetIndexOrOperator : IndexOrOperator, IJetSimpleQueryOperator, IJetRecordCounter, ITWIR
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0002E2D5 File Offset: 0x0002C4D5
		internal JetIndexOrOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation) : this(connectionProvider, new IndexOrOperator.IndexOrOperatorDefinition(culture, columnsToFetch, (from op in queryOperators
		select op.OperatorDefinition).ToArray<SimpleQueryOperator.SimpleQueryOperatorDefinition>(), frequentOperation))
		{
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002E310 File Offset: 0x0002C510
		internal JetIndexOrOperator(IConnectionProvider connectionProvider, IndexOrOperator.IndexOrOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0002E31A File Offset: 0x0002C51A
		private JetConnection JetConnection
		{
			get
			{
				return (JetConnection)base.Connection;
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002E327 File Offset: 0x0002C527
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			return new JetReader(base.ConnectionProvider, this, disposeQueryOperator);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002E344 File Offset: 0x0002C544
		private List<object[]> OrTables(List<object[]> table1, List<object[]> table2)
		{
			if (table1 == null && table2 == null)
			{
				return null;
			}
			if (table1 == null)
			{
				return table2;
			}
			if (table2 == null)
			{
				return table1;
			}
			List<object[]> list = new List<object[]>(table1);
			foreach (object[] array in table2)
			{
				bool flag = false;
				foreach (object[] array2 in table1)
				{
					bool flag2 = true;
					for (int i = 0; i < array2.Length; i++)
					{
						if (!ValueHelper.ValuesEqual(array2[i], array[i], base.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(array);
					break;
				}
			}
			return list;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002E428 File Offset: 0x0002C628
		public bool MoveFirst(out int rowsSkipped)
		{
			rowsSkipped = 0;
			this.rowIndex = 0U;
			for (int i = 0; i < base.QueryOperators.Length; i++)
			{
				List<object[]> allRows = JetIndexAndOperator.GetAllRows(base.QueryOperators[i]);
				if (i == 0)
				{
					this.results = allRows;
				}
				else
				{
					this.results = this.OrTables(this.results, allRows);
				}
			}
			bool flag = false;
			if (this.results != null && this.results.Count > 0)
			{
				flag = true;
			}
			base.TraceMove("MoveFirst", flag);
			return flag;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002E4A8 File Offset: 0x0002C6A8
		public bool MoveNext()
		{
			bool flag = false;
			if (this.results != null && (long)this.results.Count > (long)((ulong)(this.rowIndex + 1U)))
			{
				this.rowIndex += 1U;
				flag = true;
			}
			base.TraceMove("MoveNext", flag);
			return flag;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002E4F4 File Offset: 0x0002C6F4
		private object GetColumnValue(Column column)
		{
			int num = -1;
			for (int i = 0; i < base.ColumnsToFetch.Count; i++)
			{
				if (base.ColumnsToFetch[i] == column)
				{
					num = i;
					break;
				}
			}
			return this.results[(int)this.rowIndex][num];
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002E548 File Offset: 0x0002C748
		byte[] IJetSimpleQueryOperator.GetColumnValueAsBytes(Column column)
		{
			return JetColumnValueHelper.GetAsByteArray(this.GetColumnValue(column), column);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002E564 File Offset: 0x0002C764
		byte[] IJetSimpleQueryOperator.GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexOr operator");
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0002E570 File Offset: 0x0002C770
		int IJetRecordCounter.GetCount()
		{
			return JetSimpleQueryOperatorHelper.GetCount(this);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002E578 File Offset: 0x0002C778
		int IJetRecordCounter.GetOrdinalPosition(SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			return JetSimpleQueryOperatorHelper.GetOrdinalPosition(this, sortOrder, stopKey, compareInfo);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002E583 File Offset: 0x0002C783
		int ITWIR.GetColumnSize(Column column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexOr operator");
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002E58F File Offset: 0x0002C78F
		object ITWIR.GetColumnValue(Column column)
		{
			return this.GetColumnValue(column);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002E598 File Offset: 0x0002C798
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexOr operator");
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexOr operator");
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002E5B0 File Offset: 0x0002C7B0
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexOr operator");
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002E5BC File Offset: 0x0002C7BC
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexOr operator");
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002E5C8 File Offset: 0x0002C7C8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetIndexOrOperator>(this);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002E5D0 File Offset: 0x0002C7D0
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				base.InternalDispose(calledFromDispose);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0002E5DC File Offset: 0x0002C7DC
		public override bool Interrupted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002E5DF File Offset: 0x0002C7DF
		void IJetSimpleQueryOperator.RequestResume()
		{
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0002E5E1 File Offset: 0x0002C7E1
		bool IJetSimpleQueryOperator.CanMoveBack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0002E5E4 File Offset: 0x0002C7E4
		void IJetSimpleQueryOperator.MoveBackAndInterrupt(int rows)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "This method should never be called.");
		}

		// Token: 0x04000326 RID: 806
		private const int DefaultRowsPerTable = 100;

		// Token: 0x04000327 RID: 807
		private const string ThisMethodShouldNotBeCalled = "This method should not be called in an IndexOr operator";

		// Token: 0x04000328 RID: 808
		private uint rowIndex;

		// Token: 0x04000329 RID: 809
		private List<object[]> results;
	}
}
