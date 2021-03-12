using System;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000013 RID: 19
	internal interface IClusterWriter
	{
		// Token: 0x060000A0 RID: 160
		bool IsClusterRunning();

		// Token: 0x060000A1 RID: 161
		Exception TryWriteLastLog(Guid dbGuid, long lastLogGen);
	}
}
