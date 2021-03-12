using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200007D RID: 125
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IMdmNotificationSettings : ISettings
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000303 RID: 771
		Uri EnrollmentUrl { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000304 RID: 772
		Uri ComplianceStatusUrl { get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000305 RID: 773
		string ADRegistrationServiceHost { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000306 RID: 774
		Uri EnrollmentUrlWithBasicSteps { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000307 RID: 775
		string ActivationUrlWithBasicSteps { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000308 RID: 776
		TimeSpan DeviceStatusCacheExpirationInternal { get; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000309 RID: 777
		TimeSpan NegativeDeviceStatusCacheExpirationInterval { get; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600030A RID: 778
		bool PolicyEvaluationEnabled { get; }
	}
}
