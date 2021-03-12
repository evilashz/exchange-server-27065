using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B21 RID: 2849
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Cmdlet("Install", "ThrottlingPolicies")]
	public sealed class InstallThrottlingPolicies : SetupTaskBase
	{
		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x0600651C RID: 25884 RVA: 0x001A5D80 File Offset: 0x001A3F80
		// (set) Token: 0x0600651D RID: 25885 RVA: 0x001A5DA1 File Offset: 0x001A3FA1
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public InstallationModes InstallationMode
		{
			get
			{
				return (InstallationModes)(base.Fields["InstallationMode"] ?? InstallationModes.Install);
			}
			set
			{
				base.Fields["InstallationMode"] = value;
			}
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x001A5DBC File Offset: 0x001A3FBC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (this.InstallationMode != InstallationModes.Install && this.InstallationMode != InstallationModes.BuildToBuildUpgrade)
			{
				base.WriteError(new ArgumentOutOfRangeException("InstallationMode", this.InstallationMode, Strings.ErrorInstallationModeNotSupported), ErrorCategory.InvalidArgument, null);
			}
			base.InternalBeginProcessing();
			this.rootOrgSession.SessionSettings.IsSharedConfigChecked = true;
			base.WriteVerbose(Strings.RetrievingGlobalThrottlingPolicy);
			this.globalPolicy = this.rootOrgSession.GetGlobalThrottlingPolicy();
			if (this.globalPolicy != null)
			{
				if (this.InstallationMode == InstallationModes.Install)
				{
					this.WriteWarning(Strings.FoundGlobalThrottlingPolicy(this.globalPolicy.Id.ToString()));
					this.WriteWarning(Strings.WillNotCreateGlobalThrottlingPolicy);
				}
				else
				{
					base.WriteVerbose(Strings.FoundGlobalThrottlingPolicy(this.globalPolicy.Id.ToString()));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x001A5E93 File Offset: 0x001A4093
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (this.globalPolicy == null)
			{
				this.InstallGlobalThrottlingPolicy();
			}
			if (this.InstallationMode == InstallationModes.BuildToBuildUpgrade)
			{
				this.ResetGlobalThrottlingPolicySettings();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x001A5EC4 File Offset: 0x001A40C4
		private void InstallGlobalThrottlingPolicy()
		{
			string text = "GlobalThrottlingPolicy_" + Guid.NewGuid().ToString("D");
			base.WriteVerbose(Strings.InstallingGlobalThrottlingPolicy(text));
			ThrottlingPolicy throttlingPolicy = new ThrottlingPolicy();
			throttlingPolicy.CloneThrottlingSettingsFrom(FallbackThrottlingPolicy.GetSingleton());
			throttlingPolicy.ThrottlingPolicyScope = ThrottlingPolicyScopeType.Global;
			throttlingPolicy.SetId(this.configurationSession, new ADObjectId("CN=Global Settings"), text);
			if (base.CurrentOrganizationId != null)
			{
				throttlingPolicy.OrganizationId = base.CurrentOrganizationId;
			}
			else
			{
				throttlingPolicy.OrganizationId = base.ExecutingUserOrganizationId;
			}
			this.SaveObject(throttlingPolicy);
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x001A5F58 File Offset: 0x001A4158
		private void ResetGlobalThrottlingPolicySettings()
		{
			this.rootOrgSession.SessionSettings.IsSharedConfigChecked = true;
			ThrottlingPolicy globalThrottlingPolicy = this.rootOrgSession.GetGlobalThrottlingPolicy();
			if (globalThrottlingPolicy == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorGlobalThrottlingPolicyNotFound), ErrorCategory.InvalidResult, null);
			}
			globalThrottlingPolicy.CloneThrottlingSettingsFrom(FallbackThrottlingPolicy.GetSingleton());
			this.SaveObject(globalThrottlingPolicy);
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x001A5FAC File Offset: 0x001A41AC
		private void SaveObject(ADConfigurationObject dataObject)
		{
			try
			{
				if (dataObject.Identity != null)
				{
					base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(dataObject, this.configurationSession, dataObject.GetType()));
				}
				using (TaskPerformanceData.SaveResult.StartRequestTimer())
				{
					this.configurationSession.Save(dataObject);
				}
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, (ErrorCategory)1002, null);
			}
			finally
			{
				if (dataObject.Identity != null)
				{
					base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(this.configurationSession));
				}
			}
		}

		// Token: 0x04003644 RID: 13892
		private ThrottlingPolicy globalPolicy;
	}
}
