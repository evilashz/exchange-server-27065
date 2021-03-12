using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000455 RID: 1109
	internal struct ProcessingResult
	{
		// Token: 0x04001574 RID: 5492
		public ResultCode Code;

		// Token: 0x04001575 RID: 5493
		public FrameSourceType SourceType;

		// Token: 0x04001576 RID: 5494
		public string RawErrorFrame;

		// Token: 0x04001577 RID: 5495
		public string Package;

		// Token: 0x04001578 RID: 5496
		public string Function;

		// Token: 0x04001579 RID: 5497
		public int Line;

		// Token: 0x0400157A RID: 5498
		public int Column;
	}
}
