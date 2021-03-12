using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200007F RID: 127
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IMdmNotificationSettings_Implementation_ : IMdmNotificationSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IMdmNotificationSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00005ED6 File Offset: 0x000040D6
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00005EDE File Offset: 0x000040DE
		_DynamicStorageSelection_IMdmNotificationSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IMdmNotificationSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00005EE6 File Offset: 0x000040E6
		void IDataAccessorBackedObject<_DynamicStorageSelection_IMdmNotificationSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IMdmNotificationSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00005EF6 File Offset: 0x000040F6
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00005F03 File Offset: 0x00004103
		public Uri EnrollmentUrl
		{
			get
			{
				if (this.dataAccessor._EnrollmentUrl_ValueProvider_ != null)
				{
					return this.dataAccessor._EnrollmentUrl_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._EnrollmentUrl_MaterializedValue_;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00005F34 File Offset: 0x00004134
		public Uri ComplianceStatusUrl
		{
			get
			{
				if (this.dataAccessor._ComplianceStatusUrl_ValueProvider_ != null)
				{
					return this.dataAccessor._ComplianceStatusUrl_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ComplianceStatusUrl_MaterializedValue_;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00005F65 File Offset: 0x00004165
		public string ADRegistrationServiceHost
		{
			get
			{
				if (this.dataAccessor._ADRegistrationServiceHost_ValueProvider_ != null)
				{
					return this.dataAccessor._ADRegistrationServiceHost_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ADRegistrationServiceHost_MaterializedValue_;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00005F96 File Offset: 0x00004196
		public Uri EnrollmentUrlWithBasicSteps
		{
			get
			{
				if (this.dataAccessor._EnrollmentUrlWithBasicSteps_ValueProvider_ != null)
				{
					return this.dataAccessor._EnrollmentUrlWithBasicSteps_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._EnrollmentUrlWithBasicSteps_MaterializedValue_;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00005FC7 File Offset: 0x000041C7
		public string ActivationUrlWithBasicSteps
		{
			get
			{
				if (this.dataAccessor._ActivationUrlWithBasicSteps_ValueProvider_ != null)
				{
					return this.dataAccessor._ActivationUrlWithBasicSteps_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ActivationUrlWithBasicSteps_MaterializedValue_;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00005FF8 File Offset: 0x000041F8
		public TimeSpan DeviceStatusCacheExpirationInternal
		{
			get
			{
				if (this.dataAccessor._DeviceStatusCacheExpirationInternal_ValueProvider_ != null)
				{
					return this.dataAccessor._DeviceStatusCacheExpirationInternal_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DeviceStatusCacheExpirationInternal_MaterializedValue_;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00006029 File Offset: 0x00004229
		public TimeSpan NegativeDeviceStatusCacheExpirationInterval
		{
			get
			{
				if (this.dataAccessor._NegativeDeviceStatusCacheExpirationInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._NegativeDeviceStatusCacheExpirationInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NegativeDeviceStatusCacheExpirationInterval_MaterializedValue_;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000605A File Offset: 0x0000425A
		public bool PolicyEvaluationEnabled
		{
			get
			{
				if (this.dataAccessor._PolicyEvaluationEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._PolicyEvaluationEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._PolicyEvaluationEnabled_MaterializedValue_;
			}
		}

		// Token: 0x0400025A RID: 602
		private _DynamicStorageSelection_IMdmNotificationSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400025B RID: 603
		private VariantContextSnapshot context;
	}
}
