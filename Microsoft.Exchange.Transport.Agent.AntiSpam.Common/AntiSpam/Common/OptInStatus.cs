using System;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000006 RID: 6
	public enum OptInStatus
	{
		// Token: 0x04000014 RID: 20
		[LocDescription(Strings.IDs.OptInNotConfigured)]
		NotConfigured,
		// Token: 0x04000015 RID: 21
		[LocDescription(Strings.IDs.OptInRequestDisabled)]
		RequestDisabled,
		// Token: 0x04000016 RID: 22
		[LocDescription(Strings.IDs.OptInRequestNotifyDownload)]
		RequestNotifyDownload,
		// Token: 0x04000017 RID: 23
		[LocDescription(Strings.IDs.OptInRequestNotifyInstall)]
		RequestNotifyInstall,
		// Token: 0x04000018 RID: 24
		[LocDescription(Strings.IDs.OptInRequestScheduled)]
		RequestScheduled,
		// Token: 0x04000019 RID: 25
		[LocDescription(Strings.IDs.OptInConfigured)]
		Configured
	}
}
