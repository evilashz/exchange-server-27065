using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000B1 RID: 177
	public enum ShadowMessagePreference
	{
		// Token: 0x040002B9 RID: 697
		[LocDescription(DataStrings.IDs.ShadowMessagePreferencePreferRemote)]
		PreferRemote,
		// Token: 0x040002BA RID: 698
		[LocDescription(DataStrings.IDs.ShadowMessagePreferenceLocalOnly)]
		LocalOnly,
		// Token: 0x040002BB RID: 699
		[LocDescription(DataStrings.IDs.ShadowMessagePreferenceRemoteOnly)]
		RemoteOnly
	}
}
