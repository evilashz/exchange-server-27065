using System;
using System.IO;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x02000009 RID: 9
	[Cmdlet("New", "App", DefaultParameterSetName = "ExtensionFileData", SupportsShouldProcess = true)]
	public sealed class NewApp : NewTenantADTaskBase<App>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000317A File Offset: 0x0000137A
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003187 File Offset: 0x00001387
		[Parameter(Mandatory = false, ParameterSetName = "ExtensionOfficeMarketplace")]
		public string MarketplaceAssetID
		{
			get
			{
				return this.DataObject.MarketplaceAssetID;
			}
			set
			{
				this.DataObject.MarketplaceAssetID = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003195 File Offset: 0x00001395
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000319D File Offset: 0x0000139D
		[Parameter(Mandatory = false, ParameterSetName = "ExtensionOfficeMarketplace")]
		public string MarketplaceQueryMarket { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000031A6 File Offset: 0x000013A6
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000031AE File Offset: 0x000013AE
		[Parameter(Mandatory = false)]
		public SwitchParameter DownloadOnly { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000031B7 File Offset: 0x000013B7
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000031BF File Offset: 0x000013BF
		[Parameter(Mandatory = false, ParameterSetName = "ExtensionOfficeMarketplace")]
		public string Etoken { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000031C8 File Offset: 0x000013C8
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000031D0 File Offset: 0x000013D0
		[Parameter(Mandatory = false, ParameterSetName = "ExtensionOfficeMarketplace")]
		public string MarketplaceServicesUrl { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000031D9 File Offset: 0x000013D9
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000031F0 File Offset: 0x000013F0
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003203 File Offset: 0x00001403
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000321A File Offset: 0x0000141A
		[Parameter(Mandatory = false, ParameterSetName = "ExtensionFileData")]
		public byte[] FileData
		{
			get
			{
				return (byte[])base.Fields["FileData"];
			}
			set
			{
				base.Fields["FileData"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000322D File Offset: 0x0000142D
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003244 File Offset: 0x00001444
		[Parameter(Mandatory = false, ParameterSetName = "ExtensionFileStream")]
		public Stream FileStream
		{
			get
			{
				return (Stream)base.Fields["FileStream"];
			}
			set
			{
				base.Fields["FileStream"] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003257 File Offset: 0x00001457
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000325F File Offset: 0x0000145F
		[Parameter(Mandatory = false, ParameterSetName = "ExtensionPrivateURL")]
		public Uri Url { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003268 File Offset: 0x00001468
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000328E File Offset: 0x0000148E
		[Parameter(Mandatory = false)]
		public SwitchParameter OrganizationApp
		{
			get
			{
				return (SwitchParameter)(base.Fields["OrganizationApp"] ?? false);
			}
			set
			{
				base.Fields["OrganizationApp"] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000032A6 File Offset: 0x000014A6
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000032C7 File Offset: 0x000014C7
		[Parameter(Mandatory = false)]
		public ClientExtensionProvidedTo ProvidedTo
		{
			get
			{
				return (ClientExtensionProvidedTo)(base.Fields["ProvidedTo"] ?? ClientExtensionProvidedTo.Everyone);
			}
			set
			{
				base.Fields["ProvidedTo"] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000032DF File Offset: 0x000014DF
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000032F6 File Offset: 0x000014F6
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> UserList
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>>)base.Fields["UserList"];
			}
			set
			{
				base.Fields["UserList"] = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003309 File Offset: 0x00001509
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000332A File Offset: 0x0000152A
		[Parameter(Mandatory = false)]
		public DefaultStateForUser DefaultStateForUser
		{
			get
			{
				return (DefaultStateForUser)(base.Fields["DefaultStateForUser"] ?? DefaultStateForUser.Disabled);
			}
			set
			{
				base.Fields["DefaultStateForUser"] = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003342 File Offset: 0x00001542
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003363 File Offset: 0x00001563
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.Fields["Enabled"] ?? true);
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000337B File Offset: 0x0000157B
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000033A1 File Offset: 0x000015A1
		[Parameter(Mandatory = false)]
		public SwitchParameter AllowReadWriteMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["AllowReadWriteMailbox"] ?? false);
			}
			set
			{
				base.Fields["AllowReadWriteMailbox"] = value;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000033BC File Offset: 0x000015BC
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId executingUserId;
			if (!base.TryGetExecutingUserId(out executingUserId) && this.Mailbox == null)
			{
				return this.CreateDataProviderForNonMailboxUser();
			}
			MailboxIdParameter mailboxIdParameter = this.Mailbox ?? MailboxTaskHelper.ResolveMailboxIdentity(executingUserId, new Task.ErrorLoggerDelegate(base.WriteError));
			try
			{
				this.adUser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			}
			catch (ManagementObjectNotFoundException)
			{
				return this.CreateDataProviderForNonMailboxUser();
			}
			this.isBposUser = CapabilityHelper.HasBposSKUCapability(this.adUser.PersistedCapabilities);
			ADScopeException ex;
			if (!TaskHelper.UnderscopeSessionToOrganization(base.TenantGlobalCatalogSession, this.adUser.OrganizationId, true).TryVerifyIsWithinScopes(this.adUser, true, out ex))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotChangeMailboxOutOfWriteScope(this.adUser.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ErrorCategory.InvalidOperation, this.adUser.Identity);
			}
			IConfigDataProvider configDataProvider = GetApp.CreateOwaExtensionDataProvider(null, base.TenantGlobalCatalogSession, base.SessionSettings, !this.OrganizationApp, this.adUser, "New-App", false, new Task.ErrorLoggerDelegate(base.WriteError));
			this.mailboxOwner = ((OWAExtensionDataProvider)configDataProvider).MailboxSession.MailboxOwner.ObjectId.ToString();
			return configDataProvider;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003534 File Offset: 0x00001734
		protected override void InternalValidate()
		{
			base.InternalValidate();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, (this.adUser != null) ? this.adUser.OrganizationId : base.ExecutingUserOrganizationId, base.ExecutingUserOrganizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.FullyConsistent, sessionSettings, 281, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Extension\\NewApp.cs");
			if (!tenantOrTopologyConfigurationSession.GetOrgContainer().AppsForOfficeEnabled)
			{
				this.WriteWarning(Strings.WarningExtensionFeatureDisabled);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000035B8 File Offset: 0x000017B8
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			base.CheckExclusiveParameters(new object[]
			{
				"Mailbox",
				"OrganizationApp"
			});
			if (!this.OrganizationApp && (base.Fields.IsModified("UserList") || base.Fields.IsModified("ProvidedTo") || base.Fields.IsModified("DefaultStateForUser")))
			{
				base.WriteError(new LocalizedException(Strings.ErrorCannotSpecifyParameterWithoutOrgExtensionParameter), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields.IsModified("UserList") && this.UserList != null && this.UserList.Count > 1000)
			{
				base.WriteError(new LocalizedException(Strings.ErrorTooManyUsersInUserList(1000)), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003684 File Offset: 0x00001884
		protected override IConfigurable PrepareDataObject()
		{
			try
			{
				if (this.FileData != null)
				{
					return this.InstallFromFile(new MemoryStream(this.FileData));
				}
				if (this.FileStream != null)
				{
					return this.InstallFromFile(this.FileStream);
				}
				if (this.MarketplaceAssetID != null && this.MarketplaceServicesUrl != null)
				{
					if (string.IsNullOrWhiteSpace(this.MarketplaceQueryMarket))
					{
						this.MarketplaceQueryMarket = "en-us";
					}
					using (Stream stream = this.DownloadDataFromOfficeMarketPlace(this.MarketplaceAssetID, this.MarketplaceQueryMarket, this.MarketplaceServicesUrl, this.Etoken))
					{
						return this.ReadManifest(stream, ExtensionType.MarketPlace);
					}
				}
				if (null == this.Url)
				{
					base.WriteError(new LocalizedException(Strings.ErrorNoInputForExtensionInstall), ErrorCategory.InvalidOperation, null);
				}
				using (Stream stream2 = this.DownloadDataFromUri(this.Url))
				{
					return this.ReadManifest(stream2, ExtensionType.Private);
				}
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
			return null;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000037A0 File Offset: 0x000019A0
		protected override void WriteResult()
		{
			if (this.DownloadOnly)
			{
				this.WriteResult(this.DataObject);
				return;
			}
			base.WriteResult();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003830 File Offset: 0x00001A30
		private Stream DownloadDataFromOfficeMarketPlace(string marketplaceAssetID, string marketplaceQueryMarket, string marketplaceServicesUrl, string etoken)
		{
			IConfigDataProvider currentDataSession = base.DataSession;
			return this.DownloadDataFromUri(delegate()
			{
				SynchronousDownloadData synchronousDownloadData = new SynchronousDownloadData();
				string domain = ((OWAExtensionDataProvider)currentDataSession).MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.Domain;
				string deploymentId = ExtensionDataHelper.GetDeploymentId(domain);
				return synchronousDownloadData.Execute(marketplaceServicesUrl, marketplaceAssetID, marketplaceQueryMarket, deploymentId, etoken);
			});
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000038B4 File Offset: 0x00001AB4
		private Stream DownloadDataFromUri(Uri uri)
		{
			return this.DownloadDataFromUri(() => SynchronousDownloadData.DownloadDataFromUri(uri, 262144L, new Func<long, bool, bool>(ExtensionData.ValidateManifestSize), true, this.isBposUser));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000038E8 File Offset: 0x00001AE8
		private IConfigDataProvider CreateDataProviderForNonMailboxUser()
		{
			if (!this.OrganizationApp)
			{
				base.WriteError(new LocalizedException(Strings.ErrorAppTargetMailboxNotFound("OrganizationApp", "Mailbox")), ErrorCategory.InvalidArgument, null);
			}
			if (base.IsDebugOn)
			{
				base.WriteDebug("Creating data provider for non mailbox user.");
			}
			return new OWAAppDataProviderForNonMailboxUser(null, base.TenantGlobalCatalogSession, base.SessionSettings, !this.OrganizationApp, "New-App");
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003958 File Offset: 0x00001B58
		private Stream DownloadDataFromUri(Func<MemoryStream> downloadCallback)
		{
			MemoryStream result = null;
			try
			{
				try
				{
					result = downloadCallback();
				}
				catch (WebException ex)
				{
					if (WebExceptionStatus.TrustFailure == ex.Status)
					{
						base.WriteError(new LocalizedException(Strings.ErrorServerCertificateError), ErrorCategory.InvalidData, null);
					}
					throw;
				}
			}
			catch
			{
				base.WriteError(new LocalizedException(Strings.ErrorCanNotDownloadPackage), ErrorCategory.InvalidData, null);
			}
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000039C4 File Offset: 0x00001BC4
		private IConfigurable ReadManifest(Stream source, ExtensionType extensionType)
		{
			ExtensionData extensionData = ExtensionData.ParseOsfManifest(source, this.MarketplaceAssetID, this.MarketplaceQueryMarket, extensionType, this.OrganizationApp ? ExtensionInstallScope.Organization : ExtensionInstallScope.User, true, DisableReasonType.NotDisabled, string.Empty, null);
			if (this.OrganizationApp)
			{
				return new OrgApp(new DefaultStateForUser?(this.DefaultStateForUser), this.ProvidedTo, OrgApp.ConvertUserListToPresentationFormat(this, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), this.UserList), extensionData.MarketplaceAssetID, extensionData.MarketplaceContentMarket, extensionData.ProviderName, extensionData.IconURL, extensionData.ExtensionId, extensionData.VersionAsString, extensionData.Type, extensionData.Scope, extensionData.RequestedCapabilities, extensionData.DisplayName, extensionData.Description, this.Enabled, extensionData.Manifest.OuterXml, this.Etoken, null, extensionData.AppStatus, (this.adUser != null) ? this.adUser.Id : new ADObjectId())
				{
					IsDownloadOnly = this.DownloadOnly
				};
			}
			if (!base.ExchangeRunspaceConfig.HasRoleOfType(RoleType.OrgMarketplaceApps) && !this.AllowReadWriteMailbox && RequestedCapabilities.ReadWriteMailbox == extensionData.RequestedCapabilities)
			{
				throw new OwaExtensionOperationException(Strings.ErrorReasonUserNotAllowedToInstallReadWriteMailbox);
			}
			return new App(null, extensionData.MarketplaceAssetID, extensionData.MarketplaceContentMarket, extensionData.ProviderName, extensionData.IconURL, extensionData.ExtensionId, extensionData.VersionAsString, extensionData.Type, extensionData.Scope, extensionData.RequestedCapabilities, extensionData.DisplayName, extensionData.Description, this.Enabled, extensionData.Manifest.OuterXml, this.adUser.Id, this.Etoken, null, extensionData.AppStatus)
			{
				IsDownloadOnly = this.DownloadOnly
			};
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003B9C File Offset: 0x00001D9C
		private IConfigurable InstallFromFile(Stream manifestStream)
		{
			if (manifestStream == null || manifestStream == Stream.Null)
			{
				base.WriteError(new LocalizedException(Strings.ErrorMissingFile), ErrorCategory.InvalidOperation, null);
			}
			Exception ex = null;
			try
			{
				ExtensionData.ValidateManifestSize(manifestStream.Length, true);
				return this.ReadManifest(manifestStream, ExtensionType.Private);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			base.WriteError(new LocalizedException(Strings.ErrorCannotReadManifestStream(ex.Message)), ErrorCategory.InvalidOperation, null);
			return null;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003C24 File Offset: 0x00001E24
		protected override void InternalProcessRecord()
		{
			OWAExtensionHelper.ProcessRecord(new Action(base.InternalProcessRecord), new Task.TaskErrorLoggingDelegate(base.WriteError), this.DataObject.Identity);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003C4E File Offset: 0x00001E4E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			OWAExtensionHelper.CleanupOWAExtensionDataProvider(base.DataSession);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003C68 File Offset: 0x00001E68
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.OrganizationApp)
				{
					return Strings.ConfirmationMessageInstallOwaOrgExtension;
				}
				return Strings.ConfirmationMessageInstallOwaExtension(this.mailboxOwner);
			}
		}

		// Token: 0x0400001F RID: 31
		internal const string MailboxParameterName = "Mailbox";

		// Token: 0x04000020 RID: 32
		internal const string OrganizationAppParameterName = "OrganizationApp";

		// Token: 0x04000021 RID: 33
		internal const int MaxUserListCount = 1000;

		// Token: 0x04000022 RID: 34
		internal const string TypeAttributeName = "xsi:type";

		// Token: 0x04000023 RID: 35
		internal const string DefaultMarketplaceQueryMarket = "en-us";

		// Token: 0x04000024 RID: 36
		internal const string ItemTypeAttributeName = "ItemType";

		// Token: 0x04000025 RID: 37
		public const string FileDataParameterSetName = "ExtensionFileData";

		// Token: 0x04000026 RID: 38
		public const string OfficeMarketplaceParameterSetName = "ExtensionOfficeMarketplace";

		// Token: 0x04000027 RID: 39
		public const string PrivateURLParameterSetName = "ExtensionPrivateURL";

		// Token: 0x04000028 RID: 40
		public const string FileStreamParameterSetName = "ExtensionFileStream";

		// Token: 0x04000029 RID: 41
		private ADUser adUser;

		// Token: 0x0400002A RID: 42
		private string mailboxOwner;

		// Token: 0x0400002B RID: 43
		private bool isBposUser;
	}
}
