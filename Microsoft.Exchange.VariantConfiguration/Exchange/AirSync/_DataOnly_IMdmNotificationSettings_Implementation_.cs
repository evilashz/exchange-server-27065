using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000080 RID: 128
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IMdmNotificationSettings_Implementation_ : IMdmNotificationSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00006093 File Offset: 0x00004293
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00006096 File Offset: 0x00004296
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00006099 File Offset: 0x00004299
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000060A1 File Offset: 0x000042A1
		public Uri EnrollmentUrl
		{
			get
			{
				return this._EnrollmentUrl_MaterializedValue_;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000060A9 File Offset: 0x000042A9
		public Uri ComplianceStatusUrl
		{
			get
			{
				return this._ComplianceStatusUrl_MaterializedValue_;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000060B1 File Offset: 0x000042B1
		public string ADRegistrationServiceHost
		{
			get
			{
				return this._ADRegistrationServiceHost_MaterializedValue_;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000060B9 File Offset: 0x000042B9
		public Uri EnrollmentUrlWithBasicSteps
		{
			get
			{
				return this._EnrollmentUrlWithBasicSteps_MaterializedValue_;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000060C1 File Offset: 0x000042C1
		public string ActivationUrlWithBasicSteps
		{
			get
			{
				return this._ActivationUrlWithBasicSteps_MaterializedValue_;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000321 RID: 801 RVA: 0x000060C9 File Offset: 0x000042C9
		public TimeSpan DeviceStatusCacheExpirationInternal
		{
			get
			{
				return this._DeviceStatusCacheExpirationInternal_MaterializedValue_;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000322 RID: 802 RVA: 0x000060D1 File Offset: 0x000042D1
		public TimeSpan NegativeDeviceStatusCacheExpirationInterval
		{
			get
			{
				return this._NegativeDeviceStatusCacheExpirationInterval_MaterializedValue_;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000060D9 File Offset: 0x000042D9
		public bool PolicyEvaluationEnabled
		{
			get
			{
				return this._PolicyEvaluationEnabled_MaterializedValue_;
			}
		}

		// Token: 0x0400025C RID: 604
		internal string _Name_MaterializedValue_;

		// Token: 0x0400025D RID: 605
		internal Uri _EnrollmentUrl_MaterializedValue_;

		// Token: 0x0400025E RID: 606
		internal Uri _ComplianceStatusUrl_MaterializedValue_;

		// Token: 0x0400025F RID: 607
		internal string _ADRegistrationServiceHost_MaterializedValue_;

		// Token: 0x04000260 RID: 608
		internal Uri _EnrollmentUrlWithBasicSteps_MaterializedValue_;

		// Token: 0x04000261 RID: 609
		internal string _ActivationUrlWithBasicSteps_MaterializedValue_;

		// Token: 0x04000262 RID: 610
		internal TimeSpan _DeviceStatusCacheExpirationInternal_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000263 RID: 611
		internal TimeSpan _NegativeDeviceStatusCacheExpirationInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000264 RID: 612
		internal bool _PolicyEvaluationEnabled_MaterializedValue_;
	}
}
