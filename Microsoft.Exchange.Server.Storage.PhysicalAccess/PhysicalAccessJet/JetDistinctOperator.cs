using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000AA RID: 170
	internal class JetDistinctOperator : DistinctOperator, IJetSimpleQueryOperator, IJetRecordCounter, ITWIR
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00022F87 File Offset: 0x00021187
		internal JetDistinctOperator(IConnectionProvider connectionProvider, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation) : this(connectionProvider, new DistinctOperator.DistinctOperatorDefinition(skipTo, maxRows, outerQuery.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00022FA0 File Offset: 0x000211A0
		internal JetDistinctOperator(IConnectionProvider connectionProvider, DistinctOperator.DistinctOperatorDefinition definition) : base(connectionProvider, definition)
		{
			this.uniqueRowsSeen = new HashSet<object[]>(new ValueArrayEqualityComparer());
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00022FBA File Offset: 0x000211BA
		private IJetSimpleQueryOperator JetOuterQuery
		{
			get
			{
				return (IJetSimpleQueryOperator)base.OuterQuery;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00022FC7 File Offset: 0x000211C7
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			base.Connection.CountStatement(Connection.OperationType.Query);
			return new JetReader(base.ConnectionProvider, this, disposeQueryOperator);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00022FED File Offset: 0x000211ED
		public override bool EnableInterrupts(IInterruptControl interruptControl)
		{
			return (interruptControl == null || base.SkipTo == 0) && base.OuterQuery.EnableInterrupts(interruptControl);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00023008 File Offset: 0x00021208
		public bool MoveFirst(out int rowsSkipped)
		{
			rowsSkipped = 0;
			this.rowsReturned = 0U;
			this.uniqueRowsSeen.Clear();
			int num;
			if (!this.JetOuterQuery.MoveFirst(out num))
			{
				base.TraceMove("MoveFirst", false);
				return false;
			}
			if (this.JetOuterQuery.Interrupted)
			{
				base.TraceCrumb("MoveFirst", "Interrupt");
				return true;
			}
			this.uniqueRowsSeen.Add(this.GetRowFromOuter());
			int num2 = base.SkipTo;
			if (num2 > 0)
			{
				rowsSkipped++;
				num2--;
				return this.MoveNext("MoveFirst", num2, ref rowsSkipped);
			}
			base.TraceMove("MoveFirst", true);
			this.rowsReturned += 1U;
			return true;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000230B8 File Offset: 0x000212B8
		public bool MoveNext()
		{
			int num = 0;
			return this.MoveNext("MoveNext", 0, ref num);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000230D8 File Offset: 0x000212D8
		internal bool MoveNext(string operation, int numberLeftToSkip, ref int rowsSkipped)
		{
			bool flag = false;
			if (base.MaxRows <= 0 || (ulong)this.rowsReturned < (ulong)((long)base.MaxRows))
			{
				for (;;)
				{
					flag = this.JetOuterQuery.MoveNext();
					if (!flag)
					{
						goto IL_92;
					}
					if (this.JetOuterQuery.Interrupted)
					{
						break;
					}
					object[] rowFromOuter = this.GetRowFromOuter();
					if (this.uniqueRowsSeen.Contains(rowFromOuter))
					{
						this.TraceRowNotUnique();
					}
					else
					{
						this.uniqueRowsSeen.Add(rowFromOuter);
						if (numberLeftToSkip <= 0)
						{
							goto IL_82;
						}
						rowsSkipped++;
						numberLeftToSkip--;
					}
				}
				base.TraceCrumb(operation, "Interrupt");
				return true;
				IL_82:
				flag = true;
				this.rowsReturned += 1U;
			}
			IL_92:
			base.TraceMove(operation, flag);
			return flag;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00023180 File Offset: 0x00021380
		byte[] IJetSimpleQueryOperator.GetColumnValueAsBytes(Column column)
		{
			return this.JetOuterQuery.GetColumnValueAsBytes(column);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0002318E File Offset: 0x0002138E
		byte[] IJetSimpleQueryOperator.GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			return this.JetOuterQuery.GetPhysicalColumnValueAsBytes(column);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002319C File Offset: 0x0002139C
		int IJetRecordCounter.GetCount()
		{
			return JetSimpleQueryOperatorHelper.GetCount(this);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000231A4 File Offset: 0x000213A4
		int IJetRecordCounter.GetOrdinalPosition(SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			return JetSimpleQueryOperatorHelper.GetOrdinalPosition(this, sortOrder, stopKey, compareInfo);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000231AF File Offset: 0x000213AF
		int ITWIR.GetColumnSize(Column column)
		{
			return this.JetOuterQuery.GetColumnSize(column);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000231BD File Offset: 0x000213BD
		object ITWIR.GetColumnValue(Column column)
		{
			return this.JetOuterQuery.GetColumnValue(column);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000231CB File Offset: 0x000213CB
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			return this.JetOuterQuery.GetPhysicalColumnSize(column);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000231D9 File Offset: 0x000213D9
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			return this.JetOuterQuery.GetPhysicalColumnValue(column);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000231E7 File Offset: 0x000213E7
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			return this.JetOuterQuery.GetPropertyColumnSize(column);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000231F5 File Offset: 0x000213F5
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			return this.JetOuterQuery.GetPropertyColumnValue(column);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00023203 File Offset: 0x00021403
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetDistinctOperator>(this);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0002320C File Offset: 0x0002140C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				try
				{
					if (this.detailTracingEnabled)
					{
						StringBuilder stringBuilder = new StringBuilder(200);
						base.AppendOperationInfo("Dispose", stringBuilder);
						ExTraceGlobals.DbInteractionDetailTracer.TraceDebug(0L, stringBuilder.ToString());
					}
				}
				finally
				{
					base.InternalDispose(calledFromDispose);
				}
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00023268 File Offset: 0x00021468
		private void TraceRowNotUnique()
		{
			if (this.intermediateTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				base.AppendOperationInfo("ROW IS NOT UNIQUE", stringBuilder);
				if (base.OuterQuery.ColumnsToFetch != null)
				{
					stringBuilder.Append("  outer_columns:[");
					base.TraceAppendColumns(stringBuilder, this.JetOuterQuery, base.OuterQuery.ColumnsToFetch);
					stringBuilder.Append("]");
				}
				ExTraceGlobals.DbInteractionIntermediateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000232E0 File Offset: 0x000214E0
		private object[] GetRowFromOuter()
		{
			IList<Column> columnsToFetch = base.OuterQuery.ColumnsToFetch;
			object[] array = new object[columnsToFetch.Count];
			for (int i = 0; i < columnsToFetch.Count; i++)
			{
				array[i] = this.JetOuterQuery.GetColumnValue(columnsToFetch[i]);
			}
			return array;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0002332C File Offset: 0x0002152C
		public override bool Interrupted
		{
			get
			{
				return this.JetOuterQuery.Interrupted;
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00023339 File Offset: 0x00021539
		void IJetSimpleQueryOperator.RequestResume()
		{
			this.JetOuterQuery.RequestResume();
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00023346 File Offset: 0x00021546
		bool IJetSimpleQueryOperator.CanMoveBack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00023349 File Offset: 0x00021549
		void IJetSimpleQueryOperator.MoveBackAndInterrupt(int rows)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "This method should never be called.");
		}

		// Token: 0x040002AC RID: 684
		private uint rowsReturned;

		// Token: 0x040002AD RID: 685
		private HashSet<object[]> uniqueRowsSeen;
	}
}
