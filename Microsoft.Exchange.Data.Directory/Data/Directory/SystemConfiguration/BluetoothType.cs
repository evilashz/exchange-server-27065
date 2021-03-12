using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004F3 RID: 1267
	public enum BluetoothType
	{
		// Token: 0x04002674 RID: 9844
		[LocDescription(DirectoryStrings.IDs.BluetoothDisable)]
		Disable,
		// Token: 0x04002675 RID: 9845
		[LocDescription(DirectoryStrings.IDs.BluetoothHandsfreeOnly)]
		HandsfreeOnly,
		// Token: 0x04002676 RID: 9846
		[LocDescription(DirectoryStrings.IDs.BluetoothAllow)]
		Allow
	}
}
