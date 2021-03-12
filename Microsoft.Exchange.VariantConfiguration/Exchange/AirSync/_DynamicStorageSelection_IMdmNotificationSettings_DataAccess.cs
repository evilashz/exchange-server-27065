using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200007E RID: 126
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IMdmNotificationSettings_DataAccessor_ : VariantObjectDataAccessorBase<IMdmNotificationSettings, _DynamicStorageSelection_IMdmNotificationSettings_Implementation_, _DynamicStorageSelection_IMdmNotificationSettings_DataAccessor_>
	{
		// Token: 0x04000249 RID: 585
		internal string _Name_MaterializedValue_;

		// Token: 0x0400024A RID: 586
		internal Uri _EnrollmentUrl_MaterializedValue_;

		// Token: 0x0400024B RID: 587
		internal ValueProvider<Uri> _EnrollmentUrl_ValueProvider_;

		// Token: 0x0400024C RID: 588
		internal Uri _ComplianceStatusUrl_MaterializedValue_;

		// Token: 0x0400024D RID: 589
		internal ValueProvider<Uri> _ComplianceStatusUrl_ValueProvider_;

		// Token: 0x0400024E RID: 590
		internal string _ADRegistrationServiceHost_MaterializedValue_;

		// Token: 0x0400024F RID: 591
		internal ValueProvider<string> _ADRegistrationServiceHost_ValueProvider_;

		// Token: 0x04000250 RID: 592
		internal Uri _EnrollmentUrlWithBasicSteps_MaterializedValue_;

		// Token: 0x04000251 RID: 593
		internal ValueProvider<Uri> _EnrollmentUrlWithBasicSteps_ValueProvider_;

		// Token: 0x04000252 RID: 594
		internal string _ActivationUrlWithBasicSteps_MaterializedValue_;

		// Token: 0x04000253 RID: 595
		internal ValueProvider<string> _ActivationUrlWithBasicSteps_ValueProvider_;

		// Token: 0x04000254 RID: 596
		internal TimeSpan _DeviceStatusCacheExpirationInternal_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000255 RID: 597
		internal ValueProvider<TimeSpan> _DeviceStatusCacheExpirationInternal_ValueProvider_;

		// Token: 0x04000256 RID: 598
		internal TimeSpan _NegativeDeviceStatusCacheExpirationInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000257 RID: 599
		internal ValueProvider<TimeSpan> _NegativeDeviceStatusCacheExpirationInterval_ValueProvider_;

		// Token: 0x04000258 RID: 600
		internal bool _PolicyEvaluationEnabled_MaterializedValue_;

		// Token: 0x04000259 RID: 601
		internal ValueProvider<bool> _PolicyEvaluationEnabled_ValueProvider_;
	}
}
