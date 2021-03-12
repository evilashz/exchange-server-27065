using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200002D RID: 45
	public interface ILogReplayStatus
	{
		// Token: 0x06000264 RID: 612
		void GetReplayStatus(out uint nextLogToReplay, out byte[] databaseInfo, out uint patchPageNumber, out byte[] patchToken, out byte[] patchData, out uint[] corruptPages);

		// Token: 0x06000265 RID: 613
		void SetMaxLogGenerationToReplay(uint value, uint logReplayFlags);

		// Token: 0x06000266 RID: 614
		void GetDatabaseInformation(out byte[] databaseInfo);
	}
}
