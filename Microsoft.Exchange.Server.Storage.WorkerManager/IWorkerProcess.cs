using System;

namespace Microsoft.Exchange.Server.Storage.WorkerManager
{
	// Token: 0x02000004 RID: 4
	public interface IWorkerProcess
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5
		int ProcessId { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6
		Guid InstanceId { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		int Generation { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8
		string InstanceName { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9
		// (set) Token: 0x0600000A RID: 10
		DatabaseType InstanceDBType { get; set; }
	}
}
