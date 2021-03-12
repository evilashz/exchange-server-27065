using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200005F RID: 95
	internal interface IAmDbState : IDisposable
	{
		// Token: 0x06000406 RID: 1030
		AmDbStateInfo Read(Guid dbGuid);

		// Token: 0x06000407 RID: 1031
		AmDbStateInfo[] ReadAll();

		// Token: 0x06000408 RID: 1032
		void Write(AmDbStateInfo state);

		// Token: 0x06000409 RID: 1033
		bool GetLastLogGenerationNumber(Guid dbGuid, out long lastLogGenNumber);

		// Token: 0x0600040A RID: 1034
		void SetLastLogGenerationNumber(Guid dbGuid, long generation);

		// Token: 0x0600040B RID: 1035
		bool GetLastLogGenerationTimeStamp(Guid dbGuid, out ExDateTime lastLogGenTimeStamp);

		// Token: 0x0600040C RID: 1036
		void SetLastLogGenerationTimeStamp(Guid dbGuid, ExDateTime timeStamp);

		// Token: 0x0600040D RID: 1037
		string ReadStateString(Guid dbGuid);

		// Token: 0x0600040E RID: 1038
		T GetDebugOption<T>(AmServerName serverName, AmDebugOptions dbgOption, T defaultValue);

		// Token: 0x0600040F RID: 1039
		bool GetLastServerTimeStamp(string serverName, out ExDateTime lastServerTimeStamp);
	}
}
