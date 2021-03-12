using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C51 RID: 3153
	[Cmdlet("Set", "WebServicesVirtualDirectory", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetWebServicesVirtualDirectory : SetExchangeServiceVirtualDirectory<ADWebServicesVirtualDirectory>
	{
		// Token: 0x170024FE RID: 9470
		// (get) Token: 0x060077B4 RID: 30644 RVA: 0x001E7CAC File Offset: 0x001E5EAC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetWebServicesVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x170024FF RID: 9471
		// (get) Token: 0x060077B5 RID: 30645 RVA: 0x001E7CBE File Offset: 0x001E5EBE
		// (set) Token: 0x060077B6 RID: 30646 RVA: 0x001E7CD5 File Offset: 0x001E5ED5
		[Parameter]
		public Uri InternalNLBBypassUrl
		{
			get
			{
				return (Uri)base.Fields[SetWebServicesVirtualDirectory.InternalNLBBypassUrlKey];
			}
			set
			{
				base.Fields[SetWebServicesVirtualDirectory.InternalNLBBypassUrlKey] = value;
			}
		}

		// Token: 0x17002500 RID: 9472
		// (get) Token: 0x060077B7 RID: 30647 RVA: 0x001E7CE8 File Offset: 0x001E5EE8
		// (set) Token: 0x060077B8 RID: 30648 RVA: 0x001E7CFF File Offset: 0x001E5EFF
		[Parameter]
		public GzipLevel GzipLevel
		{
			get
			{
				return (GzipLevel)base.Fields["GzipLevel"];
			}
			set
			{
				base.Fields["GzipLevel"] = value;
			}
		}

		// Token: 0x17002501 RID: 9473
		// (get) Token: 0x060077B9 RID: 30649 RVA: 0x001E7D17 File Offset: 0x001E5F17
		// (set) Token: 0x060077BA RID: 30650 RVA: 0x001E7D42 File Offset: 0x001E5F42
		[Parameter(Mandatory = false)]
		public bool WSSecurityAuthentication
		{
			get
			{
				return base.Fields["WSSecurityAuthentication"] != null && (bool)base.Fields["WSSecurityAuthentication"];
			}
			set
			{
				base.Fields["WSSecurityAuthentication"] = value;
			}
		}

		// Token: 0x17002502 RID: 9474
		// (get) Token: 0x060077BB RID: 30651 RVA: 0x001E7D5A File Offset: 0x001E5F5A
		// (set) Token: 0x060077BC RID: 30652 RVA: 0x001E7D85 File Offset: 0x001E5F85
		[Parameter(Mandatory = false)]
		public bool OAuthAuthentication
		{
			get
			{
				return base.Fields["OAuthAuthentication"] != null && (bool)base.Fields["OAuthAuthentication"];
			}
			set
			{
				base.Fields["OAuthAuthentication"] = value;
			}
		}

		// Token: 0x17002503 RID: 9475
		// (get) Token: 0x060077BD RID: 30653 RVA: 0x001E7D9D File Offset: 0x001E5F9D
		// (set) Token: 0x060077BE RID: 30654 RVA: 0x001E7DC8 File Offset: 0x001E5FC8
		[Parameter(Mandatory = false)]
		public bool CertificateAuthentication
		{
			get
			{
				return base.Fields["CertificateAuthentication"] != null && (bool)base.Fields["CertificateAuthentication"];
			}
			set
			{
				base.Fields["CertificateAuthentication"] = value;
			}
		}

		// Token: 0x17002504 RID: 9476
		// (get) Token: 0x060077BF RID: 30655 RVA: 0x001E7DE0 File Offset: 0x001E5FE0
		// (set) Token: 0x060077C0 RID: 30656 RVA: 0x001E7DF7 File Offset: 0x001E5FF7
		[Parameter(Mandatory = false)]
		public bool MRSProxyEnabled
		{
			get
			{
				return (bool)base.Fields["MRSProxyEnabled"];
			}
			set
			{
				base.Fields["MRSProxyEnabled"] = value;
			}
		}

		// Token: 0x17002505 RID: 9477
		// (get) Token: 0x060077C1 RID: 30657 RVA: 0x001E7E0F File Offset: 0x001E600F
		// (set) Token: 0x060077C2 RID: 30658 RVA: 0x001E7E17 File Offset: 0x001E6017
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x060077C3 RID: 30659 RVA: 0x001E7E20 File Offset: 0x001E6020
		protected override IConfigurable PrepareDataObject()
		{
			ADWebServicesVirtualDirectory adwebServicesVirtualDirectory = (ADWebServicesVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (base.Fields.Contains(SetWebServicesVirtualDirectory.InternalNLBBypassUrlKey))
			{
				adwebServicesVirtualDirectory.InternalNLBBypassUrl = (Uri)base.Fields[SetWebServicesVirtualDirectory.InternalNLBBypassUrlKey];
			}
			adwebServicesVirtualDirectory.WSSecurityAuthentication = (bool?)base.Fields["WSSecurityAuthentication"];
			adwebServicesVirtualDirectory.OAuthAuthentication = (bool?)base.Fields["OAuthAuthentication"];
			if (base.Fields.IsModified("GzipLevel"))
			{
				if (this.GzipLevel == GzipLevel.Error)
				{
					base.WriteError(new TaskException(Strings.GzipCannotBeSetToError), ErrorCategory.NotSpecified, null);
				}
				else
				{
					if (this.GzipLevel == GzipLevel.Low)
					{
						this.WriteWarning(Strings.GzipLowDoesNotUseDynamicCompression);
					}
					adwebServicesVirtualDirectory.GzipLevel = this.GzipLevel;
					this.WriteWarning(Strings.NeedIisRestartWarning);
				}
			}
			if (base.Fields.IsModified("MRSProxyEnabled"))
			{
				adwebServicesVirtualDirectory.MRSProxyEnabled = this.MRSProxyEnabled;
			}
			return adwebServicesVirtualDirectory;
		}

		// Token: 0x060077C4 RID: 30660 RVA: 0x001E7F20 File Offset: 0x001E6120
		private void UpdateCompressionSettings()
		{
			if (base.Fields.IsModified("GzipLevel"))
			{
				string metabasePath = this.DataObject.MetabasePath;
				Gzip.SetIisGzipLevel(IisUtility.WebSiteFromMetabasePath(metabasePath), GzipLevel.High);
				Gzip.SetVirtualDirectoryGzipLevel(metabasePath, this.DataObject.GzipLevel);
				if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
				{
					try
					{
						Gzip.SetIisGzipMimeTypes();
					}
					catch (Exception ex)
					{
						TaskLogger.Trace("Exception occurred in SetIisGzipMimeTypes(): {0}", new object[]
						{
							ex.Message
						});
						this.WriteWarning(Strings.SetIISGzipMimeTypesFailure);
						throw;
					}
				}
			}
		}

		// Token: 0x060077C5 RID: 30661 RVA: 0x001E7FC8 File Offset: 0x001E61C8
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ADWebServicesVirtualDirectory adwebServicesVirtualDirectory = dataObject as ADWebServicesVirtualDirectory;
			adwebServicesVirtualDirectory.GzipLevel = Gzip.GetGzipLevel(adwebServicesVirtualDirectory.MetabasePath);
			dataObject.ResetChangeTracking();
			base.StampChangesOn(dataObject);
		}

		// Token: 0x060077C6 RID: 30662 RVA: 0x001E7FFA File Offset: 0x001E61FA
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			base.InternalValidateBasicLiveIdBasic();
		}

		// Token: 0x060077C7 RID: 30663 RVA: 0x001E8014 File Offset: 0x001E6214
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force)
			{
				foreach (ADPropertyDefinition adpropertyDefinition in NewWebServicesVirtualDirectory.HostProperties)
				{
					if (!NewWebServicesVirtualDirectory.IsValidHost(this.DataObject, adpropertyDefinition) && !base.ShouldContinue(Strings.ConfirmationMessageHostCannotBeResolved(adpropertyDefinition.Name)))
					{
						TaskLogger.LogExit();
						return;
					}
				}
			}
			if (!(this.DataObject.WindowsAuthentication ?? true))
			{
				if (base.ExchangeRunspaceConfig != null && base.ExchangeRunspaceConfig.ConfigurationSettings != null && (base.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.ECP || base.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.OSP))
				{
					if (!this.Force && !base.ShouldContinue(new LocalizedString(string.Format("{0} {1}", Strings.WarningMessageSetWebServicesVirtualDirectoryWindowsAuthentication(this.Identity.ToString()), Strings.ConfirmationMessageWebServicesVirtualDirectoryContinue))))
					{
						TaskLogger.LogExit();
						return;
					}
				}
				else
				{
					this.WriteWarning(Strings.WarningMessageSetWebServicesVirtualDirectoryWindowsAuthentication(this.Identity.ToString()));
					if (!this.Force && !base.ShouldContinue(Strings.ConfirmationMessageWebServicesVirtualDirectoryContinue))
					{
						TaskLogger.LogExit();
						return;
					}
				}
			}
			this.DataObject.CertificateAuthentication = null;
			base.InternalProcessRecord();
			base.InternalEnableLiveIdNegotiateAuxiliaryModule();
			if (Datacenter.IsMicrosoftHostedOnly(true))
			{
				string text = string.Format("{0}/{1}", this.DataObject.MetabasePath, "Nego2");
				if (!IisUtility.Exists(text))
				{
					DirectoryEntry directoryEntry = IisUtility.CreateWebDirObject(this.DataObject.MetabasePath, null, "Nego2");
					IisUtility.SetProperty(directoryEntry, "AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script, true);
					directoryEntry.CommitChanges();
				}
				ExchangeServiceVDirHelper.SetSplitVirtualDirectoryAuthenticationMethods(this.DataObject, text, new Task.TaskErrorLoggingDelegate(base.WriteError), this.MetabaseSetPropertiesFailureMessage);
				ExchangeServiceVDirHelper.ForceAnonymous(text);
			}
			ExchangeServiceVDirHelper.ForceAnonymous(this.DataObject.MetabasePath);
			ExchangeServiceVDirHelper.EwsAutodiscMWA.OnSetManageWCFEndpoints(this, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol.Ews, this.WSSecurityAuthentication, this.DataObject);
			this.UpdateCompressionSettings();
			TaskLogger.LogExit();
		}

		// Token: 0x04003BE9 RID: 15337
		private const string ParameterMRSProxyEnabled = "MRSProxyEnabled";

		// Token: 0x04003BEA RID: 15338
		internal const string EWSVDirNameForManagement = "Management";

		// Token: 0x04003BEB RID: 15339
		internal const string EWSVDirNameForNego2Auth = "Nego2";

		// Token: 0x04003BEC RID: 15340
		private static readonly string InternalNLBBypassUrlKey = "InternalNLBBypassUrl";
	}
}
