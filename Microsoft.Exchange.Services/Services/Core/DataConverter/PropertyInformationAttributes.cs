using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200020E RID: 526
	[Flags]
	internal enum PropertyInformationAttributes
	{
		// Token: 0x04000AB2 RID: 2738
		None = 0,
		// Token: 0x04000AB3 RID: 2739
		ImplementsSetCommand = 2,
		// Token: 0x04000AB4 RID: 2740
		ImplementsToXmlCommand = 4,
		// Token: 0x04000AB5 RID: 2741
		ImplementsAppendUpdateCommand = 8,
		// Token: 0x04000AB6 RID: 2742
		ImplementsDeleteUpdateCommand = 16,
		// Token: 0x04000AB7 RID: 2743
		ImplementsSetUpdateCommand = 32,
		// Token: 0x04000AB8 RID: 2744
		ImplementsToXmlForPropertyBagCommand = 64,
		// Token: 0x04000AB9 RID: 2745
		ImplementsToServiceObjectCommand = 128,
		// Token: 0x04000ABA RID: 2746
		ImplementsToServiceObjectForPropertyBagCommand = 256,
		// Token: 0x04000ABB RID: 2747
		ImplementsReadOnlyCommands = 452
	}
}
