using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.AirSync;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C49 RID: 3145
	public abstract class SetMobileSyncVirtualDirectoryBase : SetExchangeVirtualDirectory<ADMobileVirtualDirectory>
	{
		// Token: 0x170024C2 RID: 9410
		// (get) Token: 0x06007727 RID: 30503 RVA: 0x001E5B50 File Offset: 0x001E3D50
		// (set) Token: 0x06007728 RID: 30504 RVA: 0x001E5B67 File Offset: 0x001E3D67
		[Parameter]
		public string ActiveSyncServer
		{
			get
			{
				return (string)base.Fields["ActiveSyncServer"];
			}
			set
			{
				base.Fields["ActiveSyncServer"] = value;
			}
		}

		// Token: 0x170024C3 RID: 9411
		// (get) Token: 0x06007729 RID: 30505 RVA: 0x001E5B7A File Offset: 0x001E3D7A
		// (set) Token: 0x0600772A RID: 30506 RVA: 0x001E5B91 File Offset: 0x001E3D91
		[Parameter]
		public bool MobileClientCertificateProvisioningEnabled
		{
			get
			{
				return (bool)base.Fields["MobileClientCertificateProvisioningEnabled"];
			}
			set
			{
				base.Fields["MobileClientCertificateProvisioningEnabled"] = value;
			}
		}

		// Token: 0x170024C4 RID: 9412
		// (get) Token: 0x0600772B RID: 30507 RVA: 0x001E5BA9 File Offset: 0x001E3DA9
		// (set) Token: 0x0600772C RID: 30508 RVA: 0x001E5BC0 File Offset: 0x001E3DC0
		[Parameter]
		public bool BadItemReportingEnabled
		{
			get
			{
				return (bool)base.Fields["BadItemReportingEnabled"];
			}
			set
			{
				base.Fields["BadItemReportingEnabled"] = value;
			}
		}

		// Token: 0x170024C5 RID: 9413
		// (get) Token: 0x0600772D RID: 30509 RVA: 0x001E5BD8 File Offset: 0x001E3DD8
		// (set) Token: 0x0600772E RID: 30510 RVA: 0x001E5BEF File Offset: 0x001E3DEF
		[Parameter]
		public bool SendWatsonReport
		{
			get
			{
				return (bool)base.Fields["SendWatsonReport"];
			}
			set
			{
				base.Fields["SendWatsonReport"] = value;
			}
		}

		// Token: 0x170024C6 RID: 9414
		// (get) Token: 0x0600772F RID: 30511 RVA: 0x001E5C07 File Offset: 0x001E3E07
		// (set) Token: 0x06007730 RID: 30512 RVA: 0x001E5C1E File Offset: 0x001E3E1E
		[Parameter]
		public string MobileClientCertificateAuthorityURL
		{
			get
			{
				return (string)base.Fields["MobileClientCertificateAuthorityURL"];
			}
			set
			{
				base.Fields["MobileClientCertificateAuthorityURL"] = value;
			}
		}

		// Token: 0x170024C7 RID: 9415
		// (get) Token: 0x06007731 RID: 30513 RVA: 0x001E5C31 File Offset: 0x001E3E31
		// (set) Token: 0x06007732 RID: 30514 RVA: 0x001E5C48 File Offset: 0x001E3E48
		[Parameter]
		public string MobileClientCertTemplateName
		{
			get
			{
				return (string)base.Fields["MobileClientCertTemplateName"];
			}
			set
			{
				base.Fields["MobileClientCertTemplateName"] = value;
			}
		}

		// Token: 0x170024C8 RID: 9416
		// (get) Token: 0x06007733 RID: 30515 RVA: 0x001E5C5B File Offset: 0x001E3E5B
		// (set) Token: 0x06007734 RID: 30516 RVA: 0x001E5C72 File Offset: 0x001E3E72
		[Parameter]
		public ClientCertAuthTypes ClientCertAuth
		{
			get
			{
				return (ClientCertAuthTypes)base.Fields["ClientCertAuth"];
			}
			set
			{
				base.Fields["ClientCertAuth"] = value;
			}
		}

		// Token: 0x170024C9 RID: 9417
		// (get) Token: 0x06007735 RID: 30517 RVA: 0x001E5C8A File Offset: 0x001E3E8A
		// (set) Token: 0x06007736 RID: 30518 RVA: 0x001E5CA1 File Offset: 0x001E3EA1
		[Parameter]
		public bool BasicAuthEnabled
		{
			get
			{
				return (bool)base.Fields["BasicAuthEnabled"];
			}
			set
			{
				base.Fields["BasicAuthEnabled"] = value;
			}
		}

		// Token: 0x170024CA RID: 9418
		// (get) Token: 0x06007737 RID: 30519 RVA: 0x001E5CB9 File Offset: 0x001E3EB9
		// (set) Token: 0x06007738 RID: 30520 RVA: 0x001E5CD0 File Offset: 0x001E3ED0
		[Parameter]
		public bool WindowsAuthEnabled
		{
			get
			{
				return (bool)base.Fields["WindowsAuthEnabled"];
			}
			set
			{
				base.Fields["WindowsAuthEnabled"] = value;
			}
		}

		// Token: 0x170024CB RID: 9419
		// (get) Token: 0x06007739 RID: 30521 RVA: 0x001E5CE8 File Offset: 0x001E3EE8
		// (set) Token: 0x0600773A RID: 30522 RVA: 0x001E5CFF File Offset: 0x001E3EFF
		[Parameter]
		public bool CompressionEnabled
		{
			get
			{
				return (bool)base.Fields["CompressionEnabled"];
			}
			set
			{
				base.Fields["CompressionEnabled"] = value;
			}
		}

		// Token: 0x170024CC RID: 9420
		// (get) Token: 0x0600773B RID: 30523 RVA: 0x001E5D17 File Offset: 0x001E3F17
		// (set) Token: 0x0600773C RID: 30524 RVA: 0x001E5D2E File Offset: 0x001E3F2E
		[Parameter]
		public RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
		{
			get
			{
				return (RemoteDocumentsActions?)base.Fields["RemoteDocumentsActionForUnknownServers"];
			}
			set
			{
				base.Fields["RemoteDocumentsActionForUnknownServers"] = value;
			}
		}

		// Token: 0x170024CD RID: 9421
		// (get) Token: 0x0600773D RID: 30525 RVA: 0x001E5D46 File Offset: 0x001E3F46
		// (set) Token: 0x0600773E RID: 30526 RVA: 0x001E5D5D File Offset: 0x001E3F5D
		[Parameter]
		public MultiValuedProperty<string> RemoteDocumentsAllowedServers
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["RemoteDocumentsAllowedServers"];
			}
			set
			{
				base.Fields["RemoteDocumentsAllowedServers"] = value;
			}
		}

		// Token: 0x170024CE RID: 9422
		// (get) Token: 0x0600773F RID: 30527 RVA: 0x001E5D70 File Offset: 0x001E3F70
		// (set) Token: 0x06007740 RID: 30528 RVA: 0x001E5D87 File Offset: 0x001E3F87
		[Parameter]
		public MultiValuedProperty<string> RemoteDocumentsBlockedServers
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["RemoteDocumentsBlockedServers"];
			}
			set
			{
				base.Fields["RemoteDocumentsBlockedServers"] = value;
			}
		}

		// Token: 0x170024CF RID: 9423
		// (get) Token: 0x06007741 RID: 30529 RVA: 0x001E5D9A File Offset: 0x001E3F9A
		// (set) Token: 0x06007742 RID: 30530 RVA: 0x001E5DB1 File Offset: 0x001E3FB1
		[Parameter]
		public MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["RemoteDocumentsInternalDomainSuffixList"];
			}
			set
			{
				base.Fields["RemoteDocumentsInternalDomainSuffixList"] = value;
			}
		}

		// Token: 0x170024D0 RID: 9424
		// (get) Token: 0x06007743 RID: 30531 RVA: 0x001E5DC4 File Offset: 0x001E3FC4
		// (set) Token: 0x06007744 RID: 30532 RVA: 0x001E5DDB File Offset: 0x001E3FDB
		[Parameter]
		public bool InstallIsapiFilter
		{
			get
			{
				return (bool)base.Fields["InstallIsapiFilter"];
			}
			set
			{
				base.Fields["InstallIsapiFilter"] = value;
			}
		}

		// Token: 0x170024D1 RID: 9425
		// (get) Token: 0x06007745 RID: 30533 RVA: 0x001E5DF3 File Offset: 0x001E3FF3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMobileSyncVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x170024D2 RID: 9426
		// (get) Token: 0x06007746 RID: 30534 RVA: 0x001E5E05 File Offset: 0x001E4005
		protected virtual bool IsInSetup
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007747 RID: 30535 RVA: 0x001E5E08 File Offset: 0x001E4008
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			try
			{
				MobileSyncVirtualDirectoryHelper.ReadFromMetabase((ADMobileVirtualDirectory)dataObject, this);
			}
			catch (IISNotInstalledException exception)
			{
				base.WriteError(exception, ErrorCategory.ObjectNotFound, null);
			}
			catch (UnauthorizedAccessException exception2)
			{
				this.WriteError(exception2, ErrorCategory.PermissionDenied, null, false);
			}
			base.StampChangesOn(dataObject);
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x001E5E64 File Offset: 0x001E4064
		protected override IConfigurable PrepareDataObject()
		{
			ADMobileVirtualDirectory admobileVirtualDirectory = (ADMobileVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (base.Fields.Contains("ActiveSyncServer"))
			{
				admobileVirtualDirectory.ActiveSyncServer = (string)base.Fields["ActiveSyncServer"];
			}
			if (base.Fields.Contains("MobileClientCertificateProvisioningEnabled"))
			{
				admobileVirtualDirectory.MobileClientCertificateProvisioningEnabled = (bool)base.Fields["MobileClientCertificateProvisioningEnabled"];
			}
			if (base.Fields.Contains("BadItemReportingEnabled"))
			{
				admobileVirtualDirectory.BadItemReportingEnabled = (bool)base.Fields["BadItemReportingEnabled"];
			}
			if (base.Fields.Contains("SendWatsonReport"))
			{
				admobileVirtualDirectory.SendWatsonReport = (bool)base.Fields["SendWatsonReport"];
			}
			if (base.Fields.Contains("MobileClientCertificateAuthorityURL"))
			{
				admobileVirtualDirectory.MobileClientCertificateAuthorityURL = (string)base.Fields["MobileClientCertificateAuthorityURL"];
			}
			if (base.Fields.Contains("MobileClientCertTemplateName"))
			{
				admobileVirtualDirectory.MobileClientCertTemplateName = (string)base.Fields["MobileClientCertTemplateName"];
			}
			if (base.Fields.Contains("ClientCertAuth"))
			{
				admobileVirtualDirectory.ClientCertAuth = new ClientCertAuthTypes?((ClientCertAuthTypes)base.Fields["ClientCertAuth"]);
			}
			if (base.Fields.Contains("BasicAuthEnabled"))
			{
				admobileVirtualDirectory.BasicAuthEnabled = (bool)base.Fields["BasicAuthEnabled"];
			}
			if (base.Fields.Contains("WindowsAuthEnabled"))
			{
				admobileVirtualDirectory.WindowsAuthEnabled = (bool)base.Fields["WindowsAuthEnabled"];
			}
			if (base.Fields.Contains("CompressionEnabled"))
			{
				admobileVirtualDirectory.CompressionEnabled = (bool)base.Fields["CompressionEnabled"];
			}
			if (base.Fields.Contains("RemoteDocumentsActionForUnknownServers"))
			{
				admobileVirtualDirectory.RemoteDocumentsActionForUnknownServers = (RemoteDocumentsActions?)base.Fields["RemoteDocumentsActionForUnknownServers"];
			}
			if (base.Fields.Contains("RemoteDocumentsAllowedServers"))
			{
				admobileVirtualDirectory.RemoteDocumentsAllowedServers = (MultiValuedProperty<string>)base.Fields["RemoteDocumentsAllowedServers"];
			}
			if (base.Fields.Contains("RemoteDocumentsBlockedServers"))
			{
				admobileVirtualDirectory.RemoteDocumentsBlockedServers = (MultiValuedProperty<string>)base.Fields["RemoteDocumentsBlockedServers"];
			}
			if (base.Fields.Contains("RemoteDocumentsInternalDomainSuffixList"))
			{
				admobileVirtualDirectory.RemoteDocumentsInternalDomainSuffixList = (MultiValuedProperty<string>)base.Fields["RemoteDocumentsInternalDomainSuffixList"];
			}
			if (base.Fields.Contains("ExtendedProtectionTokenCheckingField"))
			{
				admobileVirtualDirectory.ExtendedProtectionTokenChecking = (ExtendedProtectionTokenCheckingMode)base.Fields["ExtendedProtectionTokenCheckingField"];
			}
			if (base.Fields.Contains("ExtendedProtectionSpnListField"))
			{
				admobileVirtualDirectory.ExtendedProtectionSPNList = (MultiValuedProperty<string>)base.Fields["ExtendedProtectionSpnListField"];
			}
			if (base.Fields.Contains("ExtendedProtectionFlagsField"))
			{
				ExtendedProtectionFlag flags = (ExtendedProtectionFlag)base.Fields["ExtendedProtectionFlagsField"];
				admobileVirtualDirectory.ExtendedProtectionFlags = ExchangeVirtualDirectory.ExtendedProtectionFlagsToMultiValuedProperty(flags);
			}
			return admobileVirtualDirectory;
		}

		// Token: 0x06007749 RID: 30537 RVA: 0x001E6194 File Offset: 0x001E4394
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!this.IsInSetup && this.DataObject.ExchangeVersion.IsOlderThan(ADMobileVirtualDirectory.MinimumSupportedExchangeObjectVersion))
			{
				base.WriteError(new TaskException(Strings.ErrorSetOlderVirtualDirectory(this.DataObject.Identity.ToString(), this.DataObject.ExchangeVersion.ToString(), ADMobileVirtualDirectory.MinimumSupportedExchangeObjectVersion.ToString())), ErrorCategory.InvalidArgument, null);
			}
			string metabasePath = this.DataObject.MetabasePath;
			if (!IisUtility.Exists(metabasePath))
			{
				base.WriteError(new WebObjectNotFoundException(Strings.ErrorObjectNotFound(metabasePath)), ErrorCategory.ObjectNotFound, metabasePath);
				return;
			}
			if (ExchangeServiceVDirHelper.IsBackEndVirtualDirectory(this.DataObject) && !this.IsInSetup)
			{
				bool flag = false;
				for (int i = 0; i < this.InvalidParametersOnMbxRole.Length; i++)
				{
					if (base.Fields.Contains(this.InvalidParametersOnMbxRole[i]))
					{
						this.WriteError(new InvalidArgumentForServerRoleException(this.InvalidParametersOnMbxRole[i], DirectoryStrings.ServerRoleCafe), ErrorCategory.InvalidArgument, this.DataObject, false);
						flag = true;
					}
				}
				if (flag)
				{
					return;
				}
			}
			bool flag2 = IisUtility.SSLEnabled(metabasePath);
			if (this.DataObject.BasicAuthEnabled && !flag2)
			{
				this.WriteWarning(Strings.WarnBasicAuthInClear);
			}
			if (this.DataObject.ClientCertAuth != ClientCertAuthTypes.Ignore && this.DataObject.ClientCertAuth != ClientCertAuthTypes.Accepted && this.DataObject.ClientCertAuth != ClientCertAuthTypes.Required)
			{
				base.WriteError(new ArgumentException(Strings.InvalidCertAuthValue, "ClientCertAuth"), ErrorCategory.InvalidArgument, null);
				return;
			}
			if (this.DataObject.ClientCertAuth == ClientCertAuthTypes.Required && !flag2)
			{
				base.WriteError(new ArgumentException(Strings.CertAuthWithoutSSLError, "ClientCertAuth"), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600774A RID: 30538 RVA: 0x001E6390 File Offset: 0x001E4590
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			ADMobileVirtualDirectory dataObject = this.DataObject;
			MobileSyncVirtualDirectoryHelper.SetToMetabase(dataObject, this);
			if (base.Fields.Contains("InstallIsapiFilter") && this.InstallIsapiFilter)
			{
				try
				{
					MobileSyncVirtualDirectoryHelper.InstallIsapiFilter(this.DataObject, !ExchangeServiceVDirHelper.IsBackEndVirtualDirectory(this.DataObject));
				}
				catch (Exception ex)
				{
					TaskLogger.Trace("Exception occurred in InstallIsapiFilter(): {0}", new object[]
					{
						ex.Message
					});
					this.WriteWarning(Strings.ActiveSyncMetabaseIsapiInstallFailure);
					throw;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003BCC RID: 15308
		private const string MobileSyncServerKey = "ActiveSyncServer";

		// Token: 0x04003BCD RID: 15309
		private const string MobileClientCertificateProvisioningEnabledKey = "MobileClientCertificateProvisioningEnabled";

		// Token: 0x04003BCE RID: 15310
		private const string BadItemReportingEnabledKey = "BadItemReportingEnabled";

		// Token: 0x04003BCF RID: 15311
		private const string SendWatsonReportKey = "SendWatsonReport";

		// Token: 0x04003BD0 RID: 15312
		private const string MobileClientCertificateAuthorityURLKey = "MobileClientCertificateAuthorityURL";

		// Token: 0x04003BD1 RID: 15313
		private const string MobileClientCertTemplateNameKey = "MobileClientCertTemplateName";

		// Token: 0x04003BD2 RID: 15314
		private const string ClientCertAuthKey = "ClientCertAuth";

		// Token: 0x04003BD3 RID: 15315
		private const string BasicAuthEnabledKey = "BasicAuthEnabled";

		// Token: 0x04003BD4 RID: 15316
		private const string WindowsAuthEnabledKey = "WindowsAuthEnabled";

		// Token: 0x04003BD5 RID: 15317
		private const string CompressionEnabledKey = "CompressionEnabled";

		// Token: 0x04003BD6 RID: 15318
		private const string RemoteDocumentsActionForUnknownServersKey = "RemoteDocumentsActionForUnknownServers";

		// Token: 0x04003BD7 RID: 15319
		private const string RemoteDocumentsAllowedServersKey = "RemoteDocumentsAllowedServers";

		// Token: 0x04003BD8 RID: 15320
		private const string RemoteDocumentsBlockedServersKey = "RemoteDocumentsBlockedServers";

		// Token: 0x04003BD9 RID: 15321
		private const string RemoteDocumentsInternalDomainSuffixListKey = "RemoteDocumentsInternalDomainSuffixList";

		// Token: 0x04003BDA RID: 15322
		private const string ExtendedProtectionTokenCheckingField = "ExtendedProtectionTokenCheckingField";

		// Token: 0x04003BDB RID: 15323
		private const string ExtendedProtectionSpnListField = "ExtendedProtectionSpnListField";

		// Token: 0x04003BDC RID: 15324
		private const string ExtendedProtectionFlagsField = "ExtendedProtectionFlagsField";

		// Token: 0x04003BDD RID: 15325
		private const string InstallIsapiFilterKey = "InstallIsapiFilter";

		// Token: 0x04003BDE RID: 15326
		private readonly string[] InvalidParametersOnMbxRole = new string[]
		{
			SetVirtualDirectory<ADMobileVirtualDirectory>.ExternalUrlKey,
			SetVirtualDirectory<ADMobileVirtualDirectory>.InternalUrlKey,
			"BasicAuthEnabled",
			"ClientCertAuth",
			SetVirtualDirectory<ADMobileVirtualDirectory>.ExternalAuthenticationMethodsKey,
			SetVirtualDirectory<ADMobileVirtualDirectory>.InternalAuthenticationMethodsKey,
			"MobileClientCertificateAuthorityURL",
			"MobileClientCertificateProvisioningEnabled",
			"MobileClientCertTemplateName",
			"BadItemReportingEnabled",
			"RemoteDocumentsActionForUnknownServers",
			"RemoteDocumentsAllowedServers",
			"RemoteDocumentsBlockedServers",
			"RemoteDocumentsInternalDomainSuffixList",
			"SendWatsonReport"
		};
	}
}
