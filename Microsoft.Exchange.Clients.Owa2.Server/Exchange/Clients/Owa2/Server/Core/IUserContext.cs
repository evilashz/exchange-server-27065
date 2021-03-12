using System;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000099 RID: 153
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IUserContext : IMailboxContext, IDisposable
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060005B2 RID: 1458
		// (set) Token: 0x060005B3 RID: 1459
		long SignIntoIMTime { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060005B4 RID: 1460
		InstantMessagingTypeOptions InstantMessageType { get; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060005B5 RID: 1461
		string SipUri { get; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060005B6 RID: 1462
		long LastUserRequestTime { get; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060005B7 RID: 1463
		// (set) Token: 0x060005B8 RID: 1464
		CultureInfo UserCulture { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060005B9 RID: 1465
		PlayOnPhoneNotificationManager PlayOnPhoneNotificationManager { get; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060005BA RID: 1466
		InstantMessageManager InstantMessageManager { get; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060005BB RID: 1467
		BposNavBarInfoAssetReader BposNavBarInfoAssetReader { get; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060005BC RID: 1468
		BposShellInfoAssetReader BposShellInfoAssetReader { get; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060005BD RID: 1469
		bool IsInstantMessageEnabled { get; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060005BE RID: 1470
		FeaturesManager FeaturesManager { get; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060005BF RID: 1471
		string CurrentOwaVersion { get; }

		// Token: 0x060005C0 RID: 1472
		void UpdateLastUserRequestTime();
	}
}
