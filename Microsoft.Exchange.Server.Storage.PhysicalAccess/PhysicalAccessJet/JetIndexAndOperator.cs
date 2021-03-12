using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000CC RID: 204
	internal class JetIndexAndOperator : IndexAndOperator, IJetSimpleQueryOperator, IJetRecordCounter, ITWIR
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0002DF06 File Offset: 0x0002C106
		internal JetIndexAndOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation) : this(connectionProvider, new IndexAndOperator.IndexAndOperatorDefinition(culture, columnsToFetch, (from op in queryOperators
		select op.OperatorDefinition).ToArray<SimpleQueryOperator.SimpleQueryOperatorDefinition>(), frequentOperation))
		{
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002DF41 File Offset: 0x0002C141
		internal JetIndexAndOperator(IConnectionProvider connectionProvider, IndexAndOperator.IndexAndOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0002DF4B File Offset: 0x0002C14B
		private JetConnection JetConnection
		{
			get
			{
				return (JetConnection)base.Connection;
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002DF58 File Offset: 0x0002C158
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			return new JetReader(base.ConnectionProvider, this, disposeQueryOperator);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002DF74 File Offset: 0x0002C174
		internal static List<object[]> GetAllRows(SimpleQueryOperator queryOperator)
		{
			List<object[]> result;
			using (queryOperator.Connection.TrackTimeInDatabase())
			{
				List<object[]> list = new List<object[]>(100);
				IJetSimpleQueryOperator jetSimpleQueryOperator = (IJetSimpleQueryOperator)queryOperator;
				int num;
				if (!jetSimpleQueryOperator.MoveFirst(out num))
				{
					result = null;
				}
				else
				{
					bool flag = true;
					while (flag)
					{
						IList<Column> columnsToFetch = queryOperator.ColumnsToFetch;
						object[] array = new object[columnsToFetch.Count];
						for (int i = 0; i < columnsToFetch.Count; i++)
						{
							Column column = columnsToFetch[i];
							array[i] = jetSimpleQueryOperator.GetColumnValue(column);
						}
						list.Add(array);
						flag = jetSimpleQueryOperator.MoveNext();
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002E02C File Offset: 0x0002C22C
		private List<object[]> AndTables(List<object[]> table1, List<object[]> table2)
		{
			List<object[]> list = null;
			if (table1 != null && table2 != null)
			{
				foreach (object[] array in table1)
				{
					foreach (object[] array2 in table2)
					{
						bool flag = true;
						for (int i = 0; i < array.Length; i++)
						{
							if (!ValueHelper.ValuesEqual(array[i], array2[i], base.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							if (list == null)
							{
								list = new List<object[]>(100);
							}
							list.Add(array);
							break;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002E104 File Offset: 0x0002C304
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
					this.results = this.AndTables(this.results, allRows);
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

		// Token: 0x060008DB RID: 2267 RVA: 0x0002E184 File Offset: 0x0002C384
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

		// Token: 0x060008DC RID: 2268 RVA: 0x0002E1D0 File Offset: 0x0002C3D0
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

		// Token: 0x060008DD RID: 2269 RVA: 0x0002E224 File Offset: 0x0002C424
		byte[] IJetSimpleQueryOperator.GetColumnValueAsBytes(Column column)
		{
			return JetColumnValueHelper.GetAsByteArray(this.GetColumnValue(column), column);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002E240 File Offset: 0x0002C440
		byte[] IJetSimpleQueryOperator.GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexAnd operator");
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002E24C File Offset: 0x0002C44C
		int IJetRecordCounter.GetCount()
		{
			return JetSimpleQueryOperatorHelper.GetCount(this);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002E254 File Offset: 0x0002C454
		int IJetRecordCounter.GetOrdinalPosition(SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			return JetSimpleQueryOperatorHelper.GetOrdinalPosition(this, sortOrder, stopKey, compareInfo);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002E25F File Offset: 0x0002C45F
		int ITWIR.GetColumnSize(Column column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexAnd operator");
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002E26B File Offset: 0x0002C46B
		object ITWIR.GetColumnValue(Column column)
		{
			return this.GetColumnValue(column);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002E274 File Offset: 0x0002C474
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexAnd operator");
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002E280 File Offset: 0x0002C480
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexAnd operator");
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002E28C File Offset: 0x0002C48C
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexAnd operator");
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002E298 File Offset: 0x0002C498
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			throw new InvalidOperationException("This method should not be called in an IndexAnd operator");
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002E2A4 File Offset: 0x0002C4A4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetIndexAndOperator>(this);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002E2AC File Offset: 0x0002C4AC
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				base.InternalDispose(calledFromDispose);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
		public override bool Interrupted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002E2BB File Offset: 0x0002C4BB
		void IJetSimpleQueryOperator.RequestResume()
		{
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0002E2BD File Offset: 0x0002C4BD
		bool IJetSimpleQueryOperator.CanMoveBack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002E2C0 File Offset: 0x0002C4C0
		void IJetSimpleQueryOperator.MoveBackAndInterrupt(int rows)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "This method should never be called.");
		}

		// Token: 0x04000321 RID: 801
		private const int DefaultRowsPerTable = 100;

		// Token: 0x04000322 RID: 802
		private const string ThisMethodShouldNotBeCalled = "This method should not be called in an IndexAnd operator";

		// Token: 0x04000323 RID: 803
		private uint rowIndex;

		// Token: 0x04000324 RID: 804
		private List<object[]> results;
	}
}
