using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.WorkerManager
{
	// Token: 0x02000002 RID: 2
	public interface IWorkerManager
	{
		// Token: 0x06000001 RID: 1
		ErrorCode StartWorker(string workerPath, Guid instance, Guid dagOrServerGuid, string instanceName, Action<IWorkerProcess> workerCompleteCallback, CancellationToken cancellationToken, out IWorkerProcess worker);

		// Token: 0x06000002 RID: 2
		void StopWorker(Guid instance, bool terminate);

		// Token: 0x06000003 RID: 3
		ErrorCode GetWorker(Guid instance, out IWorkerProcess worker);

		// Token: 0x06000004 RID: 4
		IEnumerable<IWorkerProcess> GetActiveWorkers();
	}
}
