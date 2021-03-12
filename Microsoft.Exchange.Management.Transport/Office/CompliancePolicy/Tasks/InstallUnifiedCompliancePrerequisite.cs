using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Management.Transport;
using Microsoft.SharePoint.Client;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000133 RID: 307
	[Cmdlet("Install", "UnifiedCompliancePrerequisite", DefaultParameterSetName = "Initialize")]
	public sealed class InstallUnifiedCompliancePrerequisite : NewMultitenancyFixedNameSystemConfigurationObjectTask<PolicyStorage>
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0003073D File Offset: 0x0002E93D
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x00030762 File Offset: 0x0002E962
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "Initialize")]
		public SmtpAddress PolicyCenterSiteOwner
		{
			get
			{
				return (SmtpAddress)(base.Fields["PolicyCenterSiteOwner"] ?? SmtpAddress.Empty);
			}
			set
			{
				base.Fields["PolicyCenterSiteOwner"] = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0003077A File Offset: 0x0002E97A
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x000307A0 File Offset: 0x0002E9A0
		[Parameter(Mandatory = false, ParameterSetName = "LoadOnly")]
		public SwitchParameter LoadOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["LoadOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["LoadOnly"] = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000307B8 File Offset: 0x0002E9B8
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x000307DE File Offset: 0x0002E9DE
		[Parameter(Mandatory = false, ParameterSetName = "Initialize")]
		public SwitchParameter ForceInitialize
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceInitialize"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceInitialize"] = value;
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000307F6 File Offset: 0x0002E9F6
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			Utils.ThrowIfNotRunInEOP();
			Utils.ValidateNotForestWideOrganization(base.CurrentOrganizationId);
			TaskLogger.LogExit();
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00030814 File Offset: 0x0002EA14
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			UnifiedCompliancePrerequisite unifiedCompliancePrerequisite = this.LoadInitializedPrerequisite();
			if (!this.LoadOnly && (base.Organization == null || this.ForceInitialize) && unifiedCompliancePrerequisite.CanInitializeSharepoint && (this.ForceInitialize || !unifiedCompliancePrerequisite.IsSharepointInitialized))
			{
				this.InitializeSharePoint(ref unifiedCompliancePrerequisite);
				this.SaveInitializedPrerequisite(unifiedCompliancePrerequisite);
			}
			if (base.NeedSuppressingPiiData)
			{
				unifiedCompliancePrerequisite.Redact();
			}
			base.WriteObject(unifiedCompliancePrerequisite);
			TaskLogger.LogExit();
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00030898 File Offset: 0x0002EA98
		private UnifiedCompliancePrerequisite LoadInitializedPrerequisite()
		{
			Uri spRootSiteUrl;
			Uri spTenantAdminUrl;
			UnifiedPolicyConfiguration.GetInstance().GetTenantSharePointUrls(this.ConfigurationSession, out spRootSiteUrl, out spTenantAdminUrl);
			return new UnifiedCompliancePrerequisite(spRootSiteUrl, spTenantAdminUrl, UnifiedPolicyConfiguration.GetInstance().GetUnifiedPolicyPreReqState(this.ConfigurationSession));
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000308D0 File Offset: 0x0002EAD0
		private void SaveInitializedPrerequisite(UnifiedCompliancePrerequisite prerequisite)
		{
			UnifiedPolicyConfiguration.GetInstance().SetUnifiedPolicyPreReqState(this.ConfigurationSession, prerequisite.ToPrerequisiteList());
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000308E8 File Offset: 0x0002EAE8
		private void InitializeSharePoint(ref UnifiedCompliancePrerequisite prerequisite)
		{
			if (!this.PolicyCenterSiteOwner.IsValidAddress && base.CurrentTaskContext != null && base.CurrentTaskContext.UserInfo != null)
			{
				this.PolicyCenterSiteOwner = base.CurrentTaskContext.UserInfo.ExecutingWindowsLiveId;
			}
			if (!this.PolicyCenterSiteOwner.IsValidAddress)
			{
				throw new ErrorInvalidPolicyCenterSiteOwnerException();
			}
			base.WriteVerbose(Strings.VerbosePolicyCenterSiteOwner(this.PolicyCenterSiteOwner.ToString()));
			SpPolicyCenterSite spPolicyCenterSite = new SpPolicyCenterSite(prerequisite.SharepointRootSiteUrl, prerequisite.SharepointTenantAdminUrl, UnifiedPolicyConfiguration.GetInstance().GetCredentials(this.ConfigurationSession, null));
			bool flag = true;
			long num = 3600000L;
			Stopwatch stopwatch = new Stopwatch();
			int num2 = 0;
			while (num2 <= 999 && num > 0L)
			{
				stopwatch.Restart();
				if (flag)
				{
					Uri policyCenterSite = spPolicyCenterSite.GetPolicyCenterSite(false);
					base.WriteVerbose(Strings.VerboseTryLoadPolicyCenterSite(policyCenterSite));
					flag = false;
					if (policyCenterSite != null)
					{
						prerequisite.SharepointPolicyCenterSiteUrl = policyCenterSite.AbsoluteUri;
						prerequisite.SharepointSuccessInitializedUtc = DateTime.UtcNow.ToString();
						return;
					}
				}
				Uri uri = spPolicyCenterSite.GeneratePolicyCenterSiteUri((num2 == 0) ? null : new int?(num2));
				ServerException ex;
				bool flag2 = !spPolicyCenterSite.IsAnExistingSite(uri, out ex);
				base.WriteVerbose(Strings.VerboseTrytoCheckSiteExistence(uri, (ex == null) ? string.Empty : ex.Message));
				if (flag2)
				{
					flag2 = !spPolicyCenterSite.IsADeletedSite(uri, out ex);
					base.WriteVerbose(Strings.VerboseTrytoCheckSiteDeletedState(uri, (ex == null) ? string.Empty : ex.Message));
				}
				if (flag2)
				{
					base.WriteVerbose(Strings.VerboseTrytoCreatePolicyCenterSite(uri));
					spPolicyCenterSite.CreatePolicyCenterSite(uri, this.PolicyCenterSiteOwner.ToString(), num);
					flag = true;
				}
				stopwatch.Stop();
				num -= stopwatch.ElapsedMilliseconds;
				num2++;
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00030AE8 File Offset: 0x0002ECE8
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || Utils.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(exception));
		}

		// Token: 0x0400044E RID: 1102
		private const long SpInitializationTimeoutInMilliSeconds = 3600000L;

		// Token: 0x0400044F RID: 1103
		private const int SpMaxSalt = 999;
	}
}
