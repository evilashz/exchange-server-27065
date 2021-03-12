using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200030C RID: 780
	public enum DeviceAccessCharacteristic
	{
		// Token: 0x04001654 RID: 5716
		[LocDescription(DirectoryStrings.IDs.DeviceType)]
		DeviceType,
		// Token: 0x04001655 RID: 5717
		[LocDescription(DirectoryStrings.IDs.DeviceModel)]
		DeviceModel,
		// Token: 0x04001656 RID: 5718
		[LocDescription(DirectoryStrings.IDs.DeviceOS)]
		DeviceOS,
		// Token: 0x04001657 RID: 5719
		[LocDescription(DirectoryStrings.IDs.UserAgent)]
		UserAgent,
		// Token: 0x04001658 RID: 5720
		[LocDescription(DirectoryStrings.IDs.XMSWLHeader)]
		XMSWLHeader
	}
}
