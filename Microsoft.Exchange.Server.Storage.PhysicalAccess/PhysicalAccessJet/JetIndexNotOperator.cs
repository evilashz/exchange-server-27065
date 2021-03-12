using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000CE RID: 206
	internal class JetIndexNotOperator : IndexNotOperator, IJetSimpleQueryOperator, IJetRecordCounter, ITWIR
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x0002E5F1 File Offset: 0x0002C7F1
		internal JetIndexNotOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator queryOperator, SimpleQueryOperator notOperator, bool frequentOperation) : this(connectionProvider, new IndexNotOperator.IndexNotOperatorDefinition(culture, columnsToFetch, queryOperator.OperatorDefinition, notOperator.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002E611 File Offset: 0x0002C811
		internal JetIndexNotOperator(IConnectionProvider connectionProvider, IndexNotOperator.IndexNotOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0002E61B File Offset: 0x0002C81B
		private JetConnection JetConnection
		{
			get
			{
				return (JetConnection)base.Connection;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002E628 File Offset: 0x0002C828
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			return new JetReader(base.ConnectionProvider, this, disposeQueryOperator);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0002E644 File Offset: 0x0002C844
		private List<object[]> NotTables(List<object[]> table1, List<object[]> table2)
		{
			List<object[]> list = null;
			if (table1 != null)
			{
				foreach (object[] array in table1)
				{
					bool flag = false;
					if (table2 != null)
					{
						foreach (object[] array2 in table2)
						{
							bool flag2 = true;
							for (int i = 0; i < array2.Length; i++)
							{
								if (!ValueHelper.ValuesEqual(array[i], array2[i], base.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
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
					}
					if (!flag)
					{
						if (list == null)
						{
							list = new List<object[]>(100);
						}
						list.Add(array);
					}
				}
			}
			return list;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002E728 File Offset: 0x0002C928
		public bool MoveFirst(out int rowsSkipped)
		{
			rowsSkipped = 0;
			this.rowIndex = 0U;
			List<object[]> allRows = JetIndexAndOperator.GetAllRows(base.QueryOperator);
			List<object[]> allRows2 = JetIndexAndOperator.GetAllRows(base.NotOperator);
			this.results = this.NotTables(allRows, allRows2);
			bool flag = false;
			if (this.results != null && this.results.Count > 0)
			{
				flag = true;
			}
			base.TraceMove("MoveFirst", flag);
			return flag;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002E78C File Offset: 0x0002C98C
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

		// Token: 0x0600090E RID: 2318 RVA: 0x0002E7D8 File Offset: 0x0002C9D8
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

		// Token: 0x0600090F RID: 2319 RVA: 0x0002E82C File Offset: 0x0002CA2C
		byte[] IJetSimpleQueryOperator.GetColumnValueAsBytes(Column column)
		{
			return JetColumnValueHelper.GetAsByteArray(this.GetColumnValue(column), column);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0002E848 File Offset: 0x0002CA48
		byte[] IJetSimpleQueryOperator.GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexNot operator");
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002E854 File Offset: 0x0002CA54
		int IJetRecordCounter.GetCount()
		{
			return JetSimpleQueryOperatorHelper.GetCount(this);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002E85C File Offset: 0x0002CA5C
		int IJetRecordCounter.GetOrdinalPosition(SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			return JetSimpleQueryOperatorHelper.GetOrdinalPosition(this, sortOrder, stopKey, compareInfo);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002E867 File Offset: 0x0002CA67
		int ITWIR.GetColumnSize(Column column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexNot operator");
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002E873 File Offset: 0x0002CA73
		object ITWIR.GetColumnValue(Column column)
		{
			return this.GetColumnValue(column);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002E87C File Offset: 0x0002CA7C
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexNot operator");
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0002E888 File Offset: 0x0002CA88
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexNot operator");
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0002E894 File Offset: 0x0002CA94
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexNot operator");
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002E8A0 File Offset: 0x0002CAA0
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexNot operator");
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002E8AC File Offset: 0x0002CAAC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetIndexNotOperator>(this);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002E8B4 File Offset: 0x0002CAB4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				base.InternalDispose(calledFromDispose);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0002E8C0 File Offset: 0x0002CAC0
		public override bool Interrupted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002E8C3 File Offset: 0x0002CAC3
		void IJetSimpleQueryOperator.RequestResume()
		{
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002E8C5 File Offset: 0x0002CAC5
		bool IJetSimpleQueryOperator.CanMoveBack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002E8C8 File Offset: 0x0002CAC8
		void IJetSimpleQueryOperator.MoveBackAndInterrupt(int rows)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "This method should never be called.");
		}

		// Token: 0x0400032B RID: 811
		private const int DefaultRowsPerTable = 100;

		// Token: 0x0400032C RID: 812
		private const string ThisMethodShouldNotBeCalled = "This method should not be called in an IndexNot operator";

		// Token: 0x0400032D RID: 813
		private uint rowIndex;

		// Token: 0x0400032E RID: 814
		private List<object[]> results;
	}
}
