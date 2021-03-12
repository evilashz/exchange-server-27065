using System;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x0200090F RID: 2319
	[Flags]
	public enum NspiGetTemplateInfoFlags
	{
		// Token: 0x04002B32 RID: 11058
		None = 0,
		// Token: 0x04002B33 RID: 11059
		Template = 1,
		// Token: 0x04002B34 RID: 11060
		Script = 4,
		// Token: 0x04002B35 RID: 11061
		EmailType = 16,
		// Token: 0x04002B36 RID: 11062
		HelpFileName = 32,
		// Token: 0x04002B37 RID: 11063
		HelpFile = 64
	}
}
