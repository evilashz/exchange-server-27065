using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000C6 RID: 198
	internal struct SERVICE_STATUS_EX
	{
		// Token: 0x040001F2 RID: 498
		public uint dwServiceType;

		// Token: 0x040001F3 RID: 499
		public uint dwCurrentState;

		// Token: 0x040001F4 RID: 500
		public uint dwControlsAccepted;

		// Token: 0x040001F5 RID: 501
		public uint dwWin32ExitCode;

		// Token: 0x040001F6 RID: 502
		public uint dwServiceSpecificExitCode;

		// Token: 0x040001F7 RID: 503
		public uint dwCheckPoint;

		// Token: 0x040001F8 RID: 504
		public uint dwWaitHint;

		// Token: 0x040001F9 RID: 505
		public uint dwProcessId;

		// Token: 0x040001FA RID: 506
		public uint dwServiceFlags;
	}
}
