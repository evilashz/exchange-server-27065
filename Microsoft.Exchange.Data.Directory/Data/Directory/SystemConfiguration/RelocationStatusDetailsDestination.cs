using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D6 RID: 1494
	public enum RelocationStatusDetailsDestination : byte
	{
		// Token: 0x04002F47 RID: 12103
		NotStarted,
		// Token: 0x04002F48 RID: 12104
		InitializationStarted = 5,
		// Token: 0x04002F49 RID: 12105
		InitializationFinished = 10,
		// Token: 0x04002F4A RID: 12106
		Arriving = 75,
		// Token: 0x04002F4B RID: 12107
		Active = 80
	}
}
