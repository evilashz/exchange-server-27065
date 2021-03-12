using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000AB RID: 171
	internal class JetInsertOperator : InsertOperator
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00023358 File Offset: 0x00021558
		internal JetInsertOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, SimpleQueryOperator simpleQueryOperator, IList<Column> columnsToInsert, IList<object> valuesToInsert, Action<object[]> actionOnInsert, Column[] argumentColumns, Column columnToFetch, bool unversioned, bool ignoreDuplicateKey, bool frequentOperation) : base(culture, connectionProvider, table, simpleQueryOperator, columnsToInsert, valuesToInsert, columnToFetch, frequentOperation)
		{
			this.actionOnInsert = actionOnInsert;
			this.argumentColumns = argumentColumns;
			this.unversioned = unversioned;
			this.ignoreDuplicateKey = ignoreDuplicateKey;
			if (simpleQueryOperator != null && simpleQueryOperator is JetTableOperator && simpleQueryOperator.Table == table)
			{
				this.insertCopy = true;
				int num = 0;
				while (num < table.SpecialCols.NumberOfPartioningColumns && this.insertCopy)
				{
					int i = 0;
					while (i < columnsToInsert.Count)
					{
						if (columnsToInsert[i] == table.Columns[num])
						{
							if (columnsToInsert[i] != simpleQueryOperator.ColumnsToFetch[i])
							{
								this.insertCopy = false;
								break;
							}
							break;
						}
						else
						{
							i++;
						}
					}
					num++;
				}
			}
			if (this.insertCopy)
			{
				this.jetTableOperatorForInsert = (JetTableOperator)simpleQueryOperator;
				return;
			}
			this.jetTableOperatorForInsert = new JetTableOperator(base.Culture, connectionProvider, table, table.PrimaryKeyIndex, null, null, null, null, 0, 0, KeyRange.AllRows, false, true, frequentOperation);
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0002346F File Offset: 0x0002166F
		internal bool InsertCopy
		{
			get
			{
				return this.insertCopy;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00023477 File Offset: 0x00021677
		private IJetSimpleQueryOperator JetSimpleQueryOperator
		{
			get
			{
				return (IJetSimpleQueryOperator)base.SimpleQueryOperator;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00023484 File Offset: 0x00021684
		public override bool Interrupted
		{
			get
			{
				return base.SimpleQueryOperator != null && this.interruptControl != null && base.SimpleQueryOperator.Interrupted;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000234A4 File Offset: 0x000216A4
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			base.Connection.CountStatement(Connection.OperationType.Insert);
			int num = 0;
			object obj = null;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				using (base.Connection.TrackTimeInDatabase())
				{
					if (base.SimpleQueryOperator != null)
					{
						bool flag;
						if (this.interruptControl == null || !this.JetSimpleQueryOperator.Interrupted)
						{
							int num2;
							flag = this.JetSimpleQueryOperator.MoveFirst(out num2);
						}
						else
						{
							flag = this.JetSimpleQueryOperator.MoveNext();
						}
						object[] array = null;
						while (flag)
						{
							if (this.interruptControl != null && this.JetSimpleQueryOperator.Interrupted)
							{
								if (!this.insertCopy)
								{
									this.jetTableOperatorForInsert.CloseJetCursor();
									break;
								}
								break;
							}
							else
							{
								if (this.interruptControl != null)
								{
									this.interruptControl.RegisterWrite(this.jetTableOperatorForInsert.Table.TableClass);
								}
								bool flag2;
								if (this.insertCopy)
								{
									flag2 = this.jetTableOperatorForInsert.InsertCopy(base.ColumnsToInsert, base.SimpleQueryOperator.ColumnsToFetch, base.ColumnToFetch, this.unversioned, this.ignoreDuplicateKey, out obj);
								}
								else
								{
									if (array == null)
									{
										array = new object[base.ColumnsToInsert.Count];
									}
									for (int i = 0; i < base.ColumnsToInsert.Count; i++)
									{
										Column column = base.SimpleQueryOperator.ColumnsToFetch[i];
										array[i] = this.JetSimpleQueryOperator.GetColumnValue(column);
										if (array[i] is LargeValue)
										{
											if (!(base.SimpleQueryOperator is IColumnStreamAccess) || !(column is PhysicalColumn))
											{
												throw new StoreException((LID)56592U, ErrorCodeValue.NotSupported, "LargeValue value cannot be inserted.");
											}
											array[i] = this.GetStreamableColumnValue((IColumnStreamAccess)base.SimpleQueryOperator, (PhysicalColumn)column);
										}
									}
									flag2 = this.jetTableOperatorForInsert.Insert(base.ColumnsToInsert, array, base.ColumnToFetch, this.unversioned, this.ignoreDuplicateKey, out obj);
								}
								if (flag2)
								{
									if (this.actionOnInsert != null)
									{
										if (this.argumentColumns != null)
										{
											object[] array2 = new object[this.argumentColumns.Length];
											for (int j = 0; j < this.argumentColumns.Length; j++)
											{
												Column column2 = this.argumentColumns[j];
												array2[j] = this.JetSimpleQueryOperator.GetColumnValue(column2);
											}
											this.actionOnInsert(array2);
										}
										else
										{
											this.actionOnInsert(null);
										}
									}
									num++;
								}
								flag = this.JetSimpleQueryOperator.MoveNext();
							}
						}
					}
					else if (this.jetTableOperatorForInsert.Insert(base.ColumnsToInsert, base.ValuesToInsert, base.ColumnToFetch, this.unversioned, this.ignoreDuplicateKey, out obj))
					{
						num++;
					}
				}
			}
			object result = (base.ColumnToFetch != null) ? obj : num;
			base.TraceOperationResult("ExecuteScalar", base.ColumnToFetch, result);
			return result;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000237E0 File Offset: 0x000219E0
		public override bool EnableInterrupts(IInterruptControl interruptControl)
		{
			if (base.SimpleQueryOperator == null || base.ColumnToFetch != null)
			{
				return false;
			}
			if (!base.SimpleQueryOperator.EnableInterrupts(interruptControl))
			{
				return false;
			}
			this.interruptControl = interruptControl;
			return true;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00023812 File Offset: 0x00021A12
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.jetTableOperatorForInsert != null && !this.insertCopy)
				{
					this.jetTableOperatorForInsert.Dispose();
					this.jetTableOperatorForInsert = null;
				}
				base.InternalDispose(calledFromDispose);
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00023840 File Offset: 0x00021A40
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetInsertOperator>(this);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00023848 File Offset: 0x00021A48
		private byte[] GetStreamableColumnValue(IColumnStreamAccess streamAccess, PhysicalColumn column)
		{
			byte[] result;
			using (PhysicalColumnStream physicalColumnStream = new PhysicalColumnStream(streamAccess, column, true))
			{
				byte[] array = new byte[physicalColumnStream.Length];
				physicalColumnStream.Read(array, 0, array.Length);
				result = array;
			}
			return result;
		}

		// Token: 0x040002AE RID: 686
		private readonly Action<object[]> actionOnInsert;

		// Token: 0x040002AF RID: 687
		private readonly Column[] argumentColumns;

		// Token: 0x040002B0 RID: 688
		private readonly bool unversioned;

		// Token: 0x040002B1 RID: 689
		private readonly bool ignoreDuplicateKey;

		// Token: 0x040002B2 RID: 690
		private readonly bool insertCopy;

		// Token: 0x040002B3 RID: 691
		private JetTableOperator jetTableOperatorForInsert;

		// Token: 0x040002B4 RID: 692
		private IInterruptControl interruptControl;
	}
}
