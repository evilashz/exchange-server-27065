using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000FA RID: 250
	public class SqlPreReadOperator : PreReadOperator
	{
		// Token: 0x06000AB2 RID: 2738 RVA: 0x00034289 File Offset: 0x00032489
		internal SqlPreReadOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, Index index, IList<KeyRange> keyRanges, IList<Column> longValueColumns, bool frequentOperation) : base(culture, connectionProvider, table, index, keyRanges, longValueColumns, frequentOperation)
		{
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0003429C File Offset: 0x0003249C
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			base.TraceOperationResult("ExecuteScalar", null, null);
			return null;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000342B7 File Offset: 0x000324B7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlPreReadOperator>(this);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000342BF File Offset: 0x000324BF
		protected override void InternalDispose(bool calledFromDispose)
		{
		}
	}
}
