using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000607 RID: 1543
	[Flags]
	internal enum DialPlanFlagBits
	{
		// Token: 0x040032B6 RID: 12982
		[LocDescription(DirectoryStrings.IDs.None)]
		None = 0,
		// Token: 0x040032B7 RID: 12983
		[LocDescription(DirectoryStrings.IDs.TUIPromptEditingEnabled)]
		TUIPromptEditingEnabled = 1
	}
}
