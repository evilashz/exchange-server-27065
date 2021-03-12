using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000B0 RID: 176
	internal class JetOrdinalPositionOperator : OrdinalPositionOperator
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x00025C7E File Offset: 0x00023E7E
		internal JetOrdinalPositionOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, SortOrder keySortOrder, StartStopKey key, bool frequentOperation) : this(connectionProvider, new OrdinalPositionOperator.OrdinalPositionOperatorDefinition(culture, (queryOperator != null) ? queryOperator.OperatorDefinition : null, keySortOrder, key, frequentOperation))
		{
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00025C9F File Offset: 0x00023E9F
		internal JetOrdinalPositionOperator(IConnectionProvider connectionProvider, OrdinalPositionOperator.OrdinalPositionOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00025CA9 File Offset: 0x00023EA9
		private IJetSimpleQueryOperator JetQueryOperator
		{
			get
			{
				return (IJetSimpleQueryOperator)base.QueryOperator;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00025CB6 File Offset: 0x00023EB6
		private JetConnection JetConnection
		{
			get
			{
				return (JetConnection)base.Connection;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00025CC4 File Offset: 0x00023EC4
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
						num = ((IJetRecordCounter)base.QueryOperator).GetOrdinalPosition(base.KeySortOrder, base.Key, base.CompareInfo);
					}
				}
			}
			object result = num;
			base.TraceOperationResult("ExecuteScalar", null, result);
			return result;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00025D80 File Offset: 0x00023F80
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<JetOrdinalPositionOperator>(this);
		}
	}
}
