using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x0200009D RID: 157
	internal class JetApplyOperator : ApplyOperator, IJetSimpleQueryOperator, IJetRecordCounter, ITWIR, IRowAccess
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x0001D604 File Offset: 0x0001B804
		internal JetApplyOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, IList<Column> tableFunctionParameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation) : this(connectionProvider, new ApplyOperator.ApplyOperatorDefinition(culture, tableFunction, tableFunctionParameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, outerQuery.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001D634 File Offset: 0x0001B834
		internal JetApplyOperator(IConnectionProvider connectionProvider, ApplyOperator.ApplyOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001D63E File Offset: 0x0001B83E
		private JetConnection JetConnection
		{
			get
			{
				return (JetConnection)base.Connection;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001D64B File Offset: 0x0001B84B
		private IJetSimpleQueryOperator JetOuterQuery
		{
			get
			{
				return (IJetSimpleQueryOperator)base.OuterQuery;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001D658 File Offset: 0x0001B858
		private JetTableFunction JetTableFunction
		{
			get
			{
				return (JetTableFunction)base.TableFunction;
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001D665 File Offset: 0x0001B865
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			base.Connection.CountStatement(Connection.OperationType.Query);
			return new JetReader(base.ConnectionProvider, this, disposeQueryOperator);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001D68B File Offset: 0x0001B88B
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
			return true;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		public bool MoveFirst(out int rowsSkipped)
		{
			rowsSkipped = 0;
			this.rowsReturned = 0U;
			if (base.Criteria is SearchCriteriaFalse)
			{
				base.TraceMove("MoveFirst", false);
				return false;
			}
			if (this.interruptControl != null)
			{
				this.interrupted = false;
			}
			int num;
			if (!this.JetOuterQuery.MoveFirst(out num))
			{
				base.TraceMove("MoveFirst", false);
				return false;
			}
			int num2 = base.SkipTo;
			if (this.interruptControl != null && this.JetOuterQuery.Interrupted)
			{
				this.Interrupt();
				base.TraceCrumb("MoveFirst", "Interrupt");
				return true;
			}
			this.tableFunctionContents = this.CreateInnerTableFunctionIterator();
			bool flag = this.tableFunctionContents.MoveNext();
			if (flag)
			{
				base.Connection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Read);
				if (base.Criteria != null)
				{
					bool flag2 = base.Criteria.Evaluate(this, base.CompareInfo);
					bool? flag3 = new bool?(true);
					if (!flag2 || flag3 == null)
					{
						goto IL_125;
					}
				}
				if (num2 <= 0)
				{
					base.TraceMove("MoveFirst", true);
					base.Connection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Accept);
					this.rowsReturned += 1U;
					return true;
				}
				rowsSkipped++;
				num2--;
			}
			IL_125:
			return this.MoveNext("MoveFirst", num2, ref rowsSkipped);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		public bool MoveNext()
		{
			int num = 0;
			return this.MoveNext("MoveNext", 0, ref num);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001D814 File Offset: 0x0001BA14
		private bool MoveNext(string operation, int numberLeftToSkip, ref int rowsSkipped)
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
			if (base.MaxRows > 0 && (ulong)this.rowsReturned >= (ulong)((long)base.MaxRows))
			{
				base.TraceMove(operation, false);
				return false;
			}
			using (base.Connection.TrackTimeInDatabase())
			{
				for (;;)
				{
					if (this.tableFunctionContents == null)
					{
						flag = this.JetOuterQuery.MoveNext();
						if (!flag)
						{
							goto IL_14B;
						}
						if (this.interruptControl != null && this.JetOuterQuery.Interrupted)
						{
							break;
						}
						this.tableFunctionContents = this.CreateInnerTableFunctionIterator();
					}
					flag = this.tableFunctionContents.MoveNext();
					if (!flag)
					{
						this.tableFunctionContents = null;
					}
					else
					{
						base.Connection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Read);
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
							goto IL_129;
						}
						rowsSkipped++;
						numberLeftToSkip--;
					}
				}
				this.Interrupt();
				base.TraceCrumb(operation, "Interrupt");
				return true;
				IL_129:
				base.Connection.IncrementRowStatsCounter(base.Table, RowStatsCounterType.Accept);
				flag = true;
				this.rowsReturned += 1U;
				IL_14B:;
			}
			base.TraceMove(operation, flag);
			return flag;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001D998 File Offset: 0x0001BB98
		private IEnumerator CreateInnerTableFunctionIterator()
		{
			object[] array = new object[base.TableFunctionParameters.Count];
			for (int i = 0; i < base.TableFunctionParameters.Count; i++)
			{
				array[i] = ((ITWIR)this).GetColumnValue(base.TableFunctionParameters[i]);
			}
			return ((IEnumerable)this.JetTableFunction.GetTableContents(base.ConnectionProvider, array)).GetEnumerator();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001DA04 File Offset: 0x0001BC04
		private byte[] GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			object obj = base.TableFunction.GetColumnFromRow(base.ConnectionProvider, this.tableFunctionContents.Current, column);
			base.Connection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, ValueTypeHelper.ValueSize(column.ExtendedTypeCode, obj));
			return JetColumnValueHelper.GetAsByteArray(obj, column);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		private object GetPhysicalColumnValue(PhysicalColumn column)
		{
			object obj = base.TableFunction.GetColumnFromRow(base.ConnectionProvider, this.tableFunctionContents.Current, column);
			base.Connection.AddRowStatsCounter(base.Table, RowStatsCounterType.ReadBytes, ValueTypeHelper.ValueSize(column.ExtendedTypeCode, obj));
			return obj;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001DAAB File Offset: 0x0001BCAB
		private object GetPropertyColumnValue(PropertyColumn column)
		{
			throw new NotSupportedException("Table function does not have property columns");
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		byte[] IJetSimpleQueryOperator.GetColumnValueAsBytes(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			if (base.OuterQuery.ColumnsToFetch.Contains(column))
			{
				return this.JetOuterQuery.GetColumnValueAsBytes(column);
			}
			IJetColumn jetColumn = (IJetColumn)column;
			return jetColumn.GetValueAsBytes(this);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001DB04 File Offset: 0x0001BD04
		byte[] IJetSimpleQueryOperator.GetPhysicalColumnValueAsBytes(PhysicalColumn column)
		{
			return this.GetPhysicalColumnValueAsBytes(column);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001DB0D File Offset: 0x0001BD0D
		int IJetRecordCounter.GetCount()
		{
			return JetSimpleQueryOperatorHelper.GetCount(this);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001DB15 File Offset: 0x0001BD15
		int IJetRecordCounter.GetOrdinalPosition(SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			return JetSimpleQueryOperatorHelper.GetOrdinalPosition(this, sortOrder, stopKey, compareInfo);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001DB20 File Offset: 0x0001BD20
		int ITWIR.GetColumnSize(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			if (base.OuterQuery.ColumnsToFetch.Contains(column))
			{
				return this.JetOuterQuery.GetColumnSize(column);
			}
			IColumn column2 = column;
			return column2.GetSize(this);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001DB68 File Offset: 0x0001BD68
		object ITWIR.GetColumnValue(Column column)
		{
			if (base.RenameDictionary != null)
			{
				column = base.ResolveColumn(column);
			}
			if (base.OuterQuery.ColumnsToFetch.Contains(column))
			{
				return this.JetOuterQuery.GetColumnValue(column);
			}
			IColumn column2 = column;
			return column2.GetValue(this);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		int ITWIR.GetPhysicalColumnSize(PhysicalColumn column)
		{
			byte[] physicalColumnValueAsBytes = this.GetPhysicalColumnValueAsBytes(column);
			if (physicalColumnValueAsBytes == null)
			{
				return 0;
			}
			return physicalColumnValueAsBytes.Length;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001DBCD File Offset: 0x0001BDCD
		object ITWIR.GetPhysicalColumnValue(PhysicalColumn column)
		{
			return this.GetPhysicalColumnValue(column);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001DBD6 File Offset: 0x0001BDD6
		int ITWIR.GetPropertyColumnSize(PropertyColumn column)
		{
			return this.JetOuterQuery.GetPropertyColumnSize(column);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
		object ITWIR.GetPropertyColumnValue(PropertyColumn column)
		{
			return this.GetPropertyColumnValue(column);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001DBED File Offset: 0x0001BDED
		object IRowAccess.GetPhysicalColumn(PhysicalColumn column)
		{
			return this.GetPhysicalColumnValue(column);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001DBF6 File Offset: 0x0001BDF6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetApplyOperator>(this);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001DBFE File Offset: 0x0001BDFE
		public override bool Interrupted
		{
			get
			{
				return this.interrupted;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001DC06 File Offset: 0x0001BE06
		void IJetSimpleQueryOperator.RequestResume()
		{
			base.TraceCrumb("RequestResume", "Resume");
			this.Resume();
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001DC1F File Offset: 0x0001BE1F
		bool IJetSimpleQueryOperator.CanMoveBack
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001DC22 File Offset: 0x0001BE22
		void IJetSimpleQueryOperator.MoveBackAndInterrupt(int rows)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "This method should never be called.");
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001DC2F File Offset: 0x0001BE2F
		private void Interrupt()
		{
			this.interrupted = true;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001DC38 File Offset: 0x0001BE38
		private bool Resume()
		{
			this.interrupted = false;
			this.JetOuterQuery.RequestResume();
			return true;
		}

		// Token: 0x0400025E RID: 606
		private IInterruptControl interruptControl;

		// Token: 0x0400025F RID: 607
		private bool interrupted;

		// Token: 0x04000260 RID: 608
		private uint rowsReturned;

		// Token: 0x04000261 RID: 609
		private IEnumerator tableFunctionContents;
	}
}
