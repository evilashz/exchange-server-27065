using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Autodiscover;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000D4 RID: 212
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAutodiscoverService
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000B4D RID: 2893
		// (set) Token: 0x06000B4E RID: 2894
		Uri Url { get; set; }

		// Token: 0x06000B4F RID: 2895
		GetUserSettingsResponse GetUserSettings(string userSmtpAddress, params UserSettingName[] userSettingNames);
	}
}
