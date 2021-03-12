using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000008 RID: 8
	public interface IDatabaseExecutionContext : IExecutionContext
	{
		// Token: 0x0600004F RID: 79
		void OnBeforeTableAccess(Connection.OperationType operationType, Table table, IList<object> partitionValues);
	}
}
