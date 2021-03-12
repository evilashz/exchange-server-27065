using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000CB RID: 203
	internal class JetUpdateOperator : UpdateOperator
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x0002DDAE File Offset: 0x0002BFAE
		internal JetUpdateOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, IList<Column> columnsToUpdate, IList<object> valuesToUpdate, bool frequentOperation) : base(culture, connectionProvider, tableOperator, columnsToUpdate, valuesToUpdate, frequentOperation)
		{
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0002DDBF File Offset: 0x0002BFBF
		public override bool Interrupted
		{
			get
			{
				return base.TableOperator.Interrupted;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002DDCC File Offset: 0x0002BFCC
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			base.Connection.CountStatement(Connection.OperationType.Update);
			int num = 0;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				using (base.Connection.TrackTimeInDatabase())
				{
					JetTableOperator jetTableOperator = base.TableOperator as JetTableOperator;
					int num2 = 0;
					bool flag;
					if (!jetTableOperator.Interrupted)
					{
						flag = jetTableOperator.MoveFirst(true, Connection.OperationType.Update, ref num2);
					}
					else
					{
						flag = jetTableOperator.MoveNext();
					}
					while (flag && (this.interruptControl == null || !jetTableOperator.Interrupted))
					{
						if (this.interruptControl != null)
						{
							this.interruptControl.RegisterWrite(jetTableOperator.Table.TableClass);
						}
						jetTableOperator.Update(base.ColumnsToUpdate, base.ValuesToUpdate);
						num++;
						flag = jetTableOperator.MoveNext();
					}
				}
			}
			object result = num;
			base.TraceOperationResult("ExecuteScalar", null, result);
			return result;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002DEDC File Offset: 0x0002C0DC
		public override bool EnableInterrupts(IInterruptControl interruptControl)
		{
			if (!base.TableOperator.EnableInterrupts(interruptControl))
			{
				return false;
			}
			this.interruptControl = interruptControl;
			return true;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002DEF6 File Offset: 0x0002C0F6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetUpdateOperator>(this);
		}

		// Token: 0x04000320 RID: 800
		private IInterruptControl interruptControl;
	}
}
