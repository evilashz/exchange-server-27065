using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000A3 RID: 163
	internal class JetCountOperator : CountOperator
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x000200B4 File Offset: 0x0001E2B4
		internal JetCountOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, bool frequentOperation) : this(connectionProvider, new CountOperator.CountOperatorDefinition(culture, (queryOperator != null) ? queryOperator.OperatorDefinition : null, frequentOperation))
		{
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000200D1 File Offset: 0x0001E2D1
		internal JetCountOperator(IConnectionProvider connectionProvider, CountOperator.CountOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x000200DB File Offset: 0x0001E2DB
		private IJetSimpleQueryOperator JetQueryOperator
		{
			get
			{
				return (IJetSimpleQueryOperator)base.QueryOperator;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x000200E8 File Offset: 0x0001E2E8
		private JetConnection JetConnection
		{
			get
			{
				return (JetConnection)base.Connection;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000200F8 File Offset: 0x0001E2F8
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			base.Connection.CountStatement(Connection.OperationType.Query);
			int num = 0;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				using (base.Connection.TrackTimeInDatabase())
				{
					if (base.QueryOperator != null)
					{
						num = ((IJetRecordCounter)base.QueryOperator).GetCount();
					}
				}
			}
			object result = num;
			base.TraceOperationResult("ExecuteScalar", null, result);
			return result;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000201A4 File Offset: 0x0001E3A4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetCountOperator>(this);
		}
	}
}
