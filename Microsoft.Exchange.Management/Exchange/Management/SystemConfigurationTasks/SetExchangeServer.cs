using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009C6 RID: 2502
	[Cmdlet("Set", "ExchangeServer", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetExchangeServer : SetTopologySystemConfigurationObjectTask<ServerIdParameter, ExchangeServer, Server>
	{
		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x06005927 RID: 22823 RVA: 0x00175FFF File Offset: 0x001741FF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetExchangeServer(this.Identity.ToString());
			}
		}

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x06005928 RID: 22824 RVA: 0x00176011 File Offset: 0x00174211
		// (set) Token: 0x06005929 RID: 22825 RVA: 0x00176036 File Offset: 0x00174236
		[ValidateNotNullOrEmpty]
		[Parameter]
		public ProductKey ProductKey
		{
			get
			{
				return (ProductKey)(base.Fields["ProductKey"] ?? ProductKey.Empty);
			}
			set
			{
				base.Fields["ProductKey"] = value;
			}
		}

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x0600592A RID: 22826 RVA: 0x0017604E File Offset: 0x0017424E
		// (set) Token: 0x0600592B RID: 22827 RVA: 0x0017606F File Offset: 0x0017426F
		[Parameter]
		public bool ErrorReportingEnabled
		{
			get
			{
				return (bool)(base.Fields[ServerSchema.ErrorReportingEnabled] ?? false);
			}
			set
			{
				base.Fields[ServerSchema.ErrorReportingEnabled] = value;
			}
		}

		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x0600592C RID: 22828 RVA: 0x00176088 File Offset: 0x00174288
		// (set) Token: 0x0600592D RID: 22829 RVA: 0x001760B7 File Offset: 0x001742B7
		[Parameter(Mandatory = false)]
		public MailboxRelease MailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)base.Fields[ActiveDirectoryServerSchema.MailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				base.Fields[ActiveDirectoryServerSchema.MailboxRelease] = value.ToString();
			}
		}

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x0600592E RID: 22830 RVA: 0x001760D4 File Offset: 0x001742D4
		// (set) Token: 0x0600592F RID: 22831 RVA: 0x001760EB File Offset: 0x001742EB
		[Parameter(Mandatory = false)]
		public MailboxProvisioningAttributes MailboxProvisioningAttributes
		{
			get
			{
				return (MailboxProvisioningAttributes)base.Fields[ServerSchema.MailboxProvisioningAttributes];
			}
			set
			{
				base.Fields[ServerSchema.MailboxProvisioningAttributes] = value;
			}
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x00176100 File Offset: 0x00174300
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			base.StampChangesOn(dataObject);
			Server server = (Server)dataObject;
			ExchangeServer instance = this.Instance;
			if (!((Server)dataObject).IsProvisionedServer)
			{
				if (string.IsNullOrEmpty(server.Fqdn))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorInvalidObjectMissingCriticalProperty(typeof(Server).Name, server.Identity.ToString(), ServerSchema.Fqdn.Name)), ErrorCategory.ReadError, server.Identity);
				}
				if (instance.IsModified(ExchangeServerSchema.StaticDomainControllers) || instance.IsModified(ExchangeServerSchema.StaticGlobalCatalogs) || instance.IsModified(ExchangeServerSchema.StaticConfigDomainController) || instance.IsModified(ExchangeServerSchema.StaticExcludedDomainControllers) || instance.IsModified(ExchangeServerSchema.ErrorReportingEnabled) || base.Fields.IsModified(ServerSchema.ErrorReportingEnabled))
				{
					Exception ex = null;
					GetExchangeServer.GetConfigurationFromRegistry(server.Fqdn, out this.oldDomainControllers, out this.oldGlobalCatalogs, out this.oldConfigDomainController, out this.oldExcludedDomainControllers, out this.oldErrorReporting, out ex);
					if (ex != null)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorAccessingRegistryRaisesException(server.Fqdn, ex.Message)), ErrorCategory.ReadError, server.Identity);
					}
				}
				else
				{
					this.oldDomainControllers = new string[0];
					this.oldGlobalCatalogs = new string[0];
					this.oldConfigDomainController = string.Empty;
					this.oldExcludedDomainControllers = new string[0];
					this.oldErrorReporting = null;
				}
			}
			else if (instance.IsModified(ExchangeServerSchema.StaticDomainControllers) || instance.IsModified(ExchangeServerSchema.StaticGlobalCatalogs) || instance.IsModified(ExchangeServerSchema.StaticConfigDomainController) || instance.IsModified(ExchangeServerSchema.StaticExcludedDomainControllers) || instance.IsModified(ExchangeServerSchema.ErrorReportingEnabled) || base.Fields.IsModified(ServerSchema.ErrorReportingEnabled))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorInvalidOperationOnProvisionedServer), ErrorCategory.InvalidArgument, server.Identity);
			}
			if (!instance.IsModified(ExchangeServerSchema.StaticDomainControllers))
			{
				this.isDCChanged = false;
				server.StaticDomainControllers = this.oldDomainControllers;
			}
			else
			{
				server.StaticDomainControllers = instance.StaticDomainControllers;
			}
			if (!instance.IsModified(ExchangeServerSchema.StaticGlobalCatalogs))
			{
				this.isGCChanged = false;
				server.StaticGlobalCatalogs = this.oldGlobalCatalogs;
			}
			else
			{
				server.StaticGlobalCatalogs = instance.StaticGlobalCatalogs;
			}
			if (!instance.IsModified(ExchangeServerSchema.StaticExcludedDomainControllers))
			{
				this.isExcludedDCChanged = false;
				server.StaticExcludedDomainControllers = this.oldExcludedDomainControllers;
			}
			else
			{
				server.StaticExcludedDomainControllers = instance.StaticExcludedDomainControllers;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x00176384 File Offset: 0x00174584
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			Server server = (Server)base.PrepareDataObject();
			if (base.Fields.IsModified(ServerSchema.ErrorReportingEnabled))
			{
				server.ErrorReportingEnabled = new bool?(this.ErrorReportingEnabled);
			}
			if (base.Fields.IsModified(ActiveDirectoryServerSchema.MailboxRelease))
			{
				server.MailboxRelease = this.MailboxRelease;
			}
			if (base.Fields.IsModified(ServerSchema.MailboxProvisioningAttributes))
			{
				server.MailboxProvisioningAttributes = this.MailboxProvisioningAttributes;
			}
			TaskLogger.LogExit();
			return server;
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x00176408 File Offset: 0x00174608
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Instance.IsModified(ADObjectSchema.Name))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorServerNameModified), ErrorCategory.InvalidOperation, this.Identity);
			}
			base.InternalValidate();
			if (this.DataObject.IsModified(ExchangeServerSchema.ErrorReportingEnabled) && this.DataObject.ErrorReportingEnabled == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorSettingErrorReportingEnabledtoNull), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (this.DataObject.IsModified(ExchangeServerSchema.CustomerFeedbackEnabled))
			{
				if (this.DataObject.IsE14OrLater)
				{
					if (this.DataObject.CustomerFeedbackEnabled == null)
					{
						base.WriteError(new InvalidOperationException(Strings.CustomerFeedbackEnabledError), ErrorCategory.InvalidOperation, null);
					}
					else
					{
						IConfigurable[] array = base.DataSession.Find<Organization>(null, null, true, null);
						if (array == null || array.Length != 1 || !(((Organization)array[0]).CustomerFeedbackEnabled != false))
						{
							base.WriteError(new InvalidOperationException(Strings.OrgIsntOptinError), ErrorCategory.InvalidOperation, null);
						}
					}
				}
				else
				{
					base.WriteError(new InvalidOperationException(Strings.NonE14ServerError), ErrorCategory.InvalidOperation, null);
				}
			}
			if (!this.DataObject.IsExchange2007OrLater)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorModifyTiServerNotAllowed), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (this.isDCChanged)
			{
				foreach (string fqdn in this.DataObject.StaticDomainControllers)
				{
					this.ValidateDC(fqdn);
				}
			}
			if (this.isGCChanged)
			{
				foreach (string fqdn2 in this.DataObject.StaticGlobalCatalogs)
				{
					this.ValidateGC(fqdn2);
				}
			}
			if (!this.DataObject.IsModified(ExchangeServerSchema.StaticConfigDomainController))
			{
				this.isCDCChanged = false;
				this.DataObject.StaticConfigDomainController = this.oldConfigDomainController;
			}
			if (!string.IsNullOrEmpty(this.DataObject.StaticConfigDomainController) && this.isCDCChanged)
			{
				this.ValidateGC(this.DataObject.StaticConfigDomainController);
			}
			if (!this.DataObject.IsModified(ExchangeServerSchema.ErrorReportingEnabled))
			{
				this.isErrorReportingChanged = false;
				this.DataObject.ErrorReportingEnabled = this.oldErrorReporting;
			}
			if (this.isDCChanged || this.isGCChanged || this.isCDCChanged || this.isExcludedDCChanged)
			{
				foreach (string text in this.DataObject.StaticExcludedDomainControllers)
				{
					foreach (string b in this.DataObject.StaticDomainControllers)
					{
						if (string.Equals(text, b, StringComparison.InvariantCultureIgnoreCase))
						{
							base.WriteError(new InvalidOperationException(Strings.ErrorExcludedDCConflict(text)), ErrorCategory.InvalidOperation, this.Identity);
						}
					}
					foreach (string b2 in this.DataObject.StaticGlobalCatalogs)
					{
						if (string.Equals(text, b2, StringComparison.InvariantCultureIgnoreCase))
						{
							base.WriteError(new InvalidOperationException(Strings.ErrorExcludedDCConflict(text)), ErrorCategory.InvalidOperation, this.Identity);
						}
					}
					if (string.Equals(text, this.DataObject.StaticConfigDomainController, StringComparison.InvariantCultureIgnoreCase))
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorExcludedDCConflict(text)), ErrorCategory.InvalidOperation, this.Identity);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x00176854 File Offset: 0x00174A54
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = false;
			bool flag2 = false;
			InformationStoreSkuLimits informationStoreSkuLimits = null;
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			ExchangeServer exchangeServer = new ExchangeServer(this.DataObject);
			if (this.isErrorReportingChanged && this.oldErrorReporting != null && !this.oldErrorReporting.Value && (this.DataObject.ErrorReportingEnabled == null || this.DataObject.ErrorReportingEnabled.Value) && !base.ShouldContinue(Strings.ErrorReportingEnabledLegalMessage))
			{
				return;
			}
			if (this.ShouldGeneratePid())
			{
				this.DataObject.ProductID = this.ProductKey.GenerateProductID();
				this.DataObject.Edition = this.ProductKey.Sku.ServerEdition;
				ADObjectId childId = this.DataObject.Id.GetChildId("InformationStore");
				InformationStore informationStore = configurationSession.Read<InformationStore>(childId);
				if (informationStore != null)
				{
					informationStoreSkuLimits = new InformationStoreSkuLimits(informationStore);
					informationStore.MinAdminVersion = new int?(ExchangeObjectVersion.Exchange2007.ExchangeBuild.ToExchange2003FormatInt32());
					informationStore.SetExchangeVersion(ExchangeObjectVersion.Exchange2007);
					this.ProductKey.Sku.InformationStoreSkuLimits.UpdateInformationStore(informationStore);
					configurationSession.Save(informationStore);
					flag = true;
				}
			}
			if ((this.isDCChanged || this.isGCChanged || this.isCDCChanged || this.isExcludedDCChanged || this.isErrorReportingChanged) && !this.DataObject.IsProvisionedServer)
			{
				Exception ex = null;
				GetExchangeServer.SetConfigurationToRegistry(exchangeServer.Fqdn, exchangeServer.StaticDomainControllers, exchangeServer.StaticGlobalCatalogs, exchangeServer.StaticConfigDomainController ?? string.Empty, exchangeServer.StaticExcludedDomainControllers, exchangeServer.ErrorReportingEnabled, out ex);
				if (ex != null)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorFaildToWriteRegistry(exchangeServer.Fqdn, ex.Message)), ErrorCategory.WriteError, this.Identity);
				}
				flag2 = true;
			}
			bool flag3 = false;
			try
			{
				if (this.DataObject.IsModified(ServerSchema.MonitoringGroup))
				{
					bool useConfigNC = configurationSession.UseConfigNC;
					bool useGlobalCatalog = configurationSession.UseGlobalCatalog;
					try
					{
						configurationSession.UseConfigNC = false;
						configurationSession.UseGlobalCatalog = true;
						ADComputer adcomputer = ((ITopologyConfigurationSession)configurationSession).FindComputerByHostName(this.DataObject.Name);
						if (adcomputer == null)
						{
							base.WriteError(new DirectoryObjectNotFoundException(this.DataObject.Id.ToString()), ErrorCategory.WriteError, this.Identity);
						}
						adcomputer.MonitoringGroup = this.DataObject.MonitoringGroup;
						configurationSession.Save(adcomputer);
					}
					finally
					{
						configurationSession.UseConfigNC = useConfigNC;
						configurationSession.UseGlobalCatalog = useGlobalCatalog;
					}
				}
				base.InternalProcessRecord();
				flag3 = true;
				if (flag)
				{
					this.WriteWarning(Strings.ProductKeyRestartWarning);
				}
			}
			finally
			{
				if (!flag3)
				{
					if (flag2)
					{
						Exception ex2 = null;
						GetExchangeServer.SetConfigurationToRegistry(exchangeServer.Fqdn, this.oldDomainControllers, this.oldGlobalCatalogs, this.oldConfigDomainController ?? string.Empty, this.oldExcludedDomainControllers, new bool?(this.oldErrorReporting != null && this.oldErrorReporting.Value), out ex2);
						if (ex2 != null)
						{
							this.WriteError(new InvalidOperationException(Strings.ErrorFaildToWriteRegistry(this.DataObject.Name, ex2.Message)), ErrorCategory.WriteError, this.Identity, false);
						}
					}
					if (flag)
					{
						ADObjectId childId2 = this.DataObject.Id.GetChildId("InformationStore");
						InformationStore informationStore2 = configurationSession.Read<InformationStore>(childId2);
						informationStoreSkuLimits.UpdateInformationStore(informationStore2);
						configurationSession.Save(informationStore2);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x00176BEC File Offset: 0x00174DEC
		private bool ShouldGeneratePid()
		{
			if (this.ProductKey.IsEmpty)
			{
				return false;
			}
			bool flag = IntPtr.Size == 4;
			bool flag2 = StringComparer.InvariantCultureIgnoreCase.Equals(this.DataObject.Name, Environment.MachineName);
			if (flag && !flag2)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotLicenseRemoteServerWith32BitAdmin(this.DataObject.Name)), ErrorCategory.WriteError, this.Identity);
				return false;
			}
			if (!this.ProductKey.Sku.IsValidVersion(this.DataObject.VersionNumber))
			{
				base.WriteError(new InvalidOperationException(Strings.CannotLicenseServer(this.DataObject.Name)), ErrorCategory.WriteError, this.Identity);
				return false;
			}
			if (!this.DataObject.IsExchangeTrialEdition)
			{
				if (this.DataObject.Edition == this.ProductKey.Sku.ServerEdition)
				{
					this.WriteWarning(Strings.ServerAlreadyLicensed(this.DataObject.Name));
				}
				else if (this.DataObject.Edition == ServerEditionType.Enterprise && this.ProductKey.Sku.ServerEdition != ServerEditionType.Enterprise)
				{
					base.WriteError(new InvalidOperationException(Strings.ServerAlreadyLicensed(this.DataObject.Name)), ErrorCategory.WriteError, this.Identity);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x00176D44 File Offset: 0x00174F44
		private ADServer ValidateDC(string fqdn)
		{
			ADServer adserver = base.GlobalConfigSession.FindDCByFqdn(fqdn);
			if (adserver == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorDCNotFound(fqdn)), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (!adserver.IsAvailable())
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorDCNotAvailable(fqdn)), ErrorCategory.InvalidOperation, this.Identity);
			}
			return adserver;
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x00176DA4 File Offset: 0x00174FA4
		private void ValidateGC(string fqdn)
		{
			ADServer adserver = this.ValidateDC(fqdn);
			if (!base.HasErrors)
			{
				NtdsDsa[] array = base.GlobalConfigSession.Find<NtdsDsa>((ADObjectId)adserver.Identity, QueryScope.SubTree, new BitMaskAndFilter(NtdsDsaSchema.Options, 1UL), null, 1);
				if (array == null && array.Length == 0)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorDCIsNotGC(fqdn)), ErrorCategory.InvalidOperation, this.Identity);
				}
			}
		}

		// Token: 0x04003314 RID: 13076
		private const string ProductKeyParameterName = "ProductKey";

		// Token: 0x04003315 RID: 13077
		private bool isDCChanged = true;

		// Token: 0x04003316 RID: 13078
		private string[] oldDomainControllers;

		// Token: 0x04003317 RID: 13079
		private bool isGCChanged = true;

		// Token: 0x04003318 RID: 13080
		private string[] oldGlobalCatalogs;

		// Token: 0x04003319 RID: 13081
		private bool isCDCChanged = true;

		// Token: 0x0400331A RID: 13082
		private string oldConfigDomainController;

		// Token: 0x0400331B RID: 13083
		private bool isExcludedDCChanged = true;

		// Token: 0x0400331C RID: 13084
		private string[] oldExcludedDomainControllers;

		// Token: 0x0400331D RID: 13085
		private bool isErrorReportingChanged = true;

		// Token: 0x0400331E RID: 13086
		private bool? oldErrorReporting;
	}
}
