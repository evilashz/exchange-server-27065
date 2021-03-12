using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000A9 RID: 169
	internal class JetDeleteOperator : DeleteOperator
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x00022E18 File Offset: 0x00021018
		internal JetDeleteOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, bool frequentOperation) : base(culture, connectionProvider, tableOperator, frequentOperation)
		{
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00022E25 File Offset: 0x00021025
		public override bool Interrupted
		{
			get
			{
				return this.interruptControl != null && base.TableOperator.Interrupted;
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00022E3C File Offset: 0x0002103C
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			base.Connection.CountStatement(Connection.OperationType.Delete);
			object result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				using (base.Connection.TrackTimeInDatabase())
				{
					JetTableOperator jetTableOperator = base.TableOperator as JetTableOperator;
					int num = 0;
					if (jetTableOperator.Interrupted || !jetTableOperator.QuickDeleteAllMatchingRows(out num))
					{
						int num2 = 0;
						bool flag;
						if (this.interruptControl == null || !jetTableOperator.Interrupted)
						{
							flag = jetTableOperator.MoveFirst(true, Connection.OperationType.Delete, ref num2);
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
							jetTableOperator.Delete();
							num++;
							flag = jetTableOperator.MoveNext();
						}
					}
					result = num;
				}
			}
			base.TraceOperationResult("ExecuteScalar", null, result);
			return result;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00022F5C File Offset: 0x0002115C
		public override bool EnableInterrupts(IInterruptControl interruptControl)
		{
			if (!base.TableOperator.EnableInterrupts(interruptControl))
			{
				return false;
			}
			this.interruptControl = interruptControl;
			return true;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00022F76 File Offset: 0x00021176
		protected override void InternalDispose(bool calledFromDispose)
		{
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00022F7F File Offset: 0x0002117F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetDeleteOperator>(this);
		}

		// Token: 0x040002AB RID: 683
		private IInterruptControl interruptControl;
	}
}
