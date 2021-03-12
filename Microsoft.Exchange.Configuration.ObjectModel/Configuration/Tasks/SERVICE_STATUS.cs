using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000C5 RID: 197
	internal struct SERVICE_STATUS
	{
		// Token: 0x040001EB RID: 491
		public uint dwServiceType;

		// Token: 0x040001EC RID: 492
		public uint dwCurrentState;

		// Token: 0x040001ED RID: 493
		public uint dwControlsAccepted;

		// Token: 0x040001EE RID: 494
		public uint dwWin32ExitCode;

		// Token: 0x040001EF RID: 495
		public uint dwServiceSpecificExitCode;

		// Token: 0x040001F0 RID: 496
		public uint dwCheckPoint;

		// Token: 0x040001F1 RID: 497
		public uint dwWaitHint;
	}
}
