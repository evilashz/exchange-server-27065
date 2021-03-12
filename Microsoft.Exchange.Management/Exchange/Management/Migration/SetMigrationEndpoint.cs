using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004F5 RID: 1269
	[Cmdlet("Set", "MigrationEndpoint", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetMigrationEndpoint : SetMigrationObjectTaskBase<MigrationEndpointIdParameter, MigrationEndpoint, MigrationEndpoint>
	{
		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000B5A5D File Offset: 0x000B3C5D
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x000B5A65 File Offset: 0x000B3C65
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override MigrationEndpointIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000B5A6E File Offset: 0x000B3C6E
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x000B5A93 File Offset: 0x000B3C93
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxConcurrentMigrations
		{
			get
			{
				return (Unlimited<int>)(base.Fields["MaxConcurrentMigrations"] ?? Unlimited<int>.UnlimitedValue);
			}
			set
			{
				base.Fields["MaxConcurrentMigrations"] = value;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x000B5AAB File Offset: 0x000B3CAB
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x000B5AD0 File Offset: 0x000B3CD0
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxConcurrentIncrementalSyncs
		{
			get
			{
				return (Unlimited<int>)(base.Fields["MaxConcurrentIncrementalSyncs"] ?? Unlimited<int>.UnlimitedValue);
			}
			set
			{
				base.Fields["MaxConcurrentIncrementalSyncs"] = value;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000B5AE8 File Offset: 0x000B3CE8
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x000B5AFF File Offset: 0x000B3CFF
		[Parameter(Mandatory = false)]
		public PSCredential Credentials
		{
			get
			{
				return (PSCredential)base.Fields["Credentials"];
			}
			set
			{
				base.Fields["Credentials"] = value;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000B5B12 File Offset: 0x000B3D12
		// (set) Token: 0x06002D63 RID: 11619 RVA: 0x000B5B33 File Offset: 0x000B3D33
		[Parameter(Mandatory = false)]
		public MigrationMailboxPermission MailboxPermission
		{
			get
			{
				return (MigrationMailboxPermission)(base.Fields["MailboxPermission"] ?? MigrationMailboxPermission.Admin);
			}
			set
			{
				base.Fields["MailboxPermission"] = value;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x000B5B4B File Offset: 0x000B3D4B
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x000B5B62 File Offset: 0x000B3D62
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string ExchangeServer
		{
			get
			{
				return (string)base.Fields["ExchangeServer"];
			}
			set
			{
				base.Fields["ExchangeServer"] = value;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x000B5B75 File Offset: 0x000B3D75
		// (set) Token: 0x06002D67 RID: 11623 RVA: 0x000B5B8C File Offset: 0x000B3D8C
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public Fqdn RemoteServer
		{
			get
			{
				return (Fqdn)base.Fields["RemoteServer"];
			}
			set
			{
				base.Fields["RemoteServer"] = value;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x000B5B9F File Offset: 0x000B3D9F
		// (set) Token: 0x06002D69 RID: 11625 RVA: 0x000B5BB6 File Offset: 0x000B3DB6
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public Fqdn RpcProxyServer
		{
			get
			{
				return (Fqdn)base.Fields["RPCProxyServer"];
			}
			set
			{
				base.Fields["RPCProxyServer"] = value;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x000B5BC9 File Offset: 0x000B3DC9
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x000B5BE0 File Offset: 0x000B3DE0
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string NspiServer
		{
			get
			{
				return (string)base.Fields["NspiServer"];
			}
			set
			{
				base.Fields["NspiServer"] = value;
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x000B5BF3 File Offset: 0x000B3DF3
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x000B5C0A File Offset: 0x000B3E0A
		[Parameter(Mandatory = false)]
		public int Port
		{
			get
			{
				return (int)base.Fields["Port"];
			}
			set
			{
				base.Fields["Port"] = value;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x000B5C22 File Offset: 0x000B3E22
		// (set) Token: 0x06002D6F RID: 11631 RVA: 0x000B5C39 File Offset: 0x000B3E39
		[Parameter(Mandatory = false)]
		public AuthenticationMethod Authentication
		{
			get
			{
				return (AuthenticationMethod)base.Fields["Authentication"];
			}
			set
			{
				base.Fields["Authentication"] = value;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x000B5C51 File Offset: 0x000B3E51
		// (set) Token: 0x06002D71 RID: 11633 RVA: 0x000B5C68 File Offset: 0x000B3E68
		[Parameter(Mandatory = false)]
		public IMAPSecurityMechanism Security
		{
			get
			{
				return (IMAPSecurityMechanism)base.Fields["Security"];
			}
			set
			{
				base.Fields["Security"] = value;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000B5C80 File Offset: 0x000B3E80
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x000B5C97 File Offset: 0x000B3E97
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public MailboxIdParameter TestMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["TestMailbox"];
			}
			set
			{
				base.Fields["TestMailbox"] = value;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000B5CAA File Offset: 0x000B3EAA
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x000B5CCF File Offset: 0x000B3ECF
		[Parameter(Mandatory = false)]
		public SmtpAddress EmailAddress
		{
			get
			{
				return (SmtpAddress)(base.Fields["EmailAddress"] ?? SmtpAddress.Empty);
			}
			set
			{
				base.Fields["EmailAddress"] = value;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000B5CE7 File Offset: 0x000B3EE7
		// (set) Token: 0x06002D77 RID: 11639 RVA: 0x000B5CFE File Offset: 0x000B3EFE
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string SourceMailboxLegacyDN
		{
			get
			{
				return (string)base.Fields["SourceMailboxLegacyDN"];
			}
			set
			{
				base.Fields["SourceMailboxLegacyDN"] = value;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000B5D11 File Offset: 0x000B3F11
		// (set) Token: 0x06002D79 RID: 11641 RVA: 0x000B5D28 File Offset: 0x000B3F28
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string PublicFolderDatabaseServerLegacyDN
		{
			get
			{
				return (string)base.Fields["PublicFolderDatabaseServerLegacyDN"];
			}
			set
			{
				base.Fields["PublicFolderDatabaseServerLegacyDN"] = value;
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000B5D3B File Offset: 0x000B3F3B
		// (set) Token: 0x06002D7B RID: 11643 RVA: 0x000B5D61 File Offset: 0x000B3F61
		[Parameter(Mandatory = false)]
		public SwitchParameter SkipVerification
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipVerification"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SkipVerification"] = value;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000B5D79 File Offset: 0x000B3F79
		// (set) Token: 0x06002D7D RID: 11645 RVA: 0x000B5D81 File Offset: 0x000B3F81
		internal MigrationDataProvider DataProvider { get; set; }

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000B5D8A File Offset: 0x000B3F8A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMigrationEndpoint(this.Identity.ToString());
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000B5D9C File Offset: 0x000B3F9C
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Set-MigrationEndpoint";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			this.DataProvider = MigrationDataProvider.CreateProviderForMigrationMailbox(base.GetType().Name, base.TenantGlobalCatalogSession, this.partitionMailbox);
			return MigrationEndpointDataProvider.CreateDataProvider("SetMigrationEndpoint", base.TenantGlobalCatalogSession, this.partitionMailbox);
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000B5E0A File Offset: 0x000B400A
		protected override bool IsObjectStateChanged()
		{
			return this.changed;
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000B5E14 File Offset: 0x000B4014
		protected override void InternalValidate()
		{
			base.InternalValidate();
			bool flag = false;
			MigrationType endpointType = this.DataObject.EndpointType;
			if (endpointType <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (endpointType == MigrationType.IMAP)
				{
					this.WriteParameterErrorIfSet("Credentials");
					this.WriteParameterErrorIfSet("MailboxPermission");
					this.WriteParameterErrorIfSet("ExchangeServer");
					this.WriteParameterErrorIfSet("RPCProxyServer");
					this.WriteParameterErrorIfSet("NspiServer");
					this.WriteParameterErrorIfSet("PublicFolderDatabaseServerLegacyDN");
					this.WriteParameterErrorIfSet("TestMailbox", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					this.WriteParameterErrorIfSet("SourceMailboxLegacyDN", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					this.WriteParameterErrorIfSet("EmailAddress", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					goto IL_2B4;
				}
				if (endpointType == MigrationType.ExchangeOutlookAnywhere)
				{
					this.WriteParameterErrorIfSet("Port");
					this.WriteParameterErrorIfSet("Security");
					this.WriteParameterErrorIfSet("PublicFolderDatabaseServerLegacyDN");
					goto IL_2B4;
				}
			}
			else
			{
				if (endpointType == MigrationType.ExchangeRemoteMove)
				{
					this.WriteParameterErrorIfSet("ExchangeServer");
					this.WriteParameterErrorIfSet("RPCProxyServer");
					this.WriteParameterErrorIfSet("Port");
					this.WriteParameterErrorIfSet("MailboxPermission");
					this.WriteParameterErrorIfSet("Authentication");
					this.WriteParameterErrorIfSet("Security");
					this.WriteParameterErrorIfSet("NspiServer");
					this.WriteParameterErrorIfSet("PublicFolderDatabaseServerLegacyDN");
					this.WriteParameterErrorIfSet("TestMailbox", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					this.WriteParameterErrorIfSet("SourceMailboxLegacyDN", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					this.WriteParameterErrorIfSet("EmailAddress", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					goto IL_2B4;
				}
				if (endpointType == MigrationType.PSTImport)
				{
					this.WriteParameterErrorIfSet("ExchangeServer");
					this.WriteParameterErrorIfSet("RPCProxyServer");
					this.WriteParameterErrorIfSet("Port");
					this.WriteParameterErrorIfSet("MailboxPermission");
					this.WriteParameterErrorIfSet("Authentication");
					this.WriteParameterErrorIfSet("Security");
					this.WriteParameterErrorIfSet("NspiServer");
					this.WriteParameterErrorIfSet("PublicFolderDatabaseServerLegacyDN");
					this.WriteParameterErrorIfSet("TestMailbox", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					this.WriteParameterErrorIfSet("SourceMailboxLegacyDN", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					this.WriteParameterErrorIfSet("EmailAddress", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					goto IL_2B4;
				}
				if (endpointType == MigrationType.PublicFolder)
				{
					this.WriteParameterErrorIfSet("RemoteServer");
					this.WriteParameterErrorIfSet("ExchangeServer");
					this.WriteParameterErrorIfSet("Port");
					this.WriteParameterErrorIfSet("MailboxPermission");
					this.WriteParameterErrorIfSet("Security");
					this.WriteParameterErrorIfSet("NspiServer");
					this.WriteParameterErrorIfSet("EmailAddress", new LocalizedString?(Strings.ErrorInvalidEndpointParameterReasonUsedForConnectionTest));
					goto IL_2B4;
				}
			}
			base.WriteError(new InvalidEndpointTypeException(this.Identity.RawIdentity, this.DataObject.EndpointType.ToString()));
			IL_2B4:
			if (base.IsFieldSet("MaxConcurrentMigrations") || base.IsFieldSet("MaxConcurrentIncrementalSyncs"))
			{
				Unlimited<int> unlimited = base.IsFieldSet("MaxConcurrentMigrations") ? this.MaxConcurrentMigrations : this.DataObject.MaxConcurrentMigrations;
				Unlimited<int> unlimited2 = base.IsFieldSet("MaxConcurrentIncrementalSyncs") ? this.MaxConcurrentIncrementalSyncs : this.DataObject.MaxConcurrentIncrementalSyncs;
				if (unlimited2 > unlimited)
				{
					base.WriteError(new MigrationMaxConcurrentIncrementalSyncsVerificationFailedException(unlimited2, unlimited));
				}
			}
			if (base.IsFieldSet("MaxConcurrentMigrations") && !this.MaxConcurrentMigrations.Equals(this.DataObject.MaxConcurrentMigrations))
			{
				this.DataObject.MaxConcurrentMigrations = this.MaxConcurrentMigrations;
				this.changed = true;
			}
			if (base.IsFieldSet("MaxConcurrentIncrementalSyncs") && !this.MaxConcurrentIncrementalSyncs.Equals(this.DataObject.MaxConcurrentIncrementalSyncs))
			{
				this.DataObject.MaxConcurrentIncrementalSyncs = this.MaxConcurrentIncrementalSyncs;
				this.changed = true;
			}
			if (base.IsFieldSet("Credentials") && (this.Credentials == null || !this.Credentials.Equals(this.DataObject.Credentials)))
			{
				this.DataObject.Credentials = this.Credentials;
				this.changed = true;
				flag = true;
			}
			if (base.IsFieldSet("MailboxPermission") && this.MailboxPermission != this.DataObject.MailboxPermission)
			{
				this.DataObject.MailboxPermission = this.MailboxPermission;
				this.changed = true;
				flag = true;
			}
			if (base.IsFieldSet("ExchangeServer") && !this.ExchangeServer.Equals(this.DataObject.ExchangeServer))
			{
				this.DataObject.ExchangeServer = this.ExchangeServer;
				this.changed = true;
				flag = true;
			}
			if (base.IsFieldSet("RPCProxyServer") && !this.RpcProxyServer.Equals(this.DataObject.RpcProxyServer))
			{
				this.DataObject.RpcProxyServer = this.RpcProxyServer;
				this.changed = true;
				flag = true;
			}
			if (base.IsFieldSet("Port") && !this.Port.Equals(this.DataObject.Port))
			{
				this.DataObject.Port = new int?(this.Port);
				this.changed = true;
				flag = true;
			}
			if (base.IsFieldSet("Authentication") && !this.Authentication.Equals(this.DataObject.Authentication))
			{
				this.DataObject.Authentication = new AuthenticationMethod?(this.Authentication);
				this.changed = true;
				flag = true;
			}
			if (base.IsFieldSet("Security") && !this.Security.Equals(this.DataObject.Security))
			{
				this.DataObject.Security = new IMAPSecurityMechanism?(this.Security);
				this.changed = true;
				flag = true;
			}
			if (base.IsFieldSet("RemoteServer") && !this.RemoteServer.Equals(this.DataObject.RemoteServer))
			{
				this.DataObject.RemoteServer = this.RemoteServer;
				this.changed = true;
				flag = true;
			}
			if (base.IsFieldSet("NspiServer") && !this.NspiServer.Equals(this.DataObject.NspiServer))
			{
				this.DataObject.NspiServer = this.NspiServer;
				this.changed = true;
				flag = true;
			}
			if (this.DataObject.EndpointType == MigrationType.PublicFolder && base.IsFieldSet("SourceMailboxLegacyDN") && !this.SourceMailboxLegacyDN.Equals(this.DataObject.SourceMailboxLegacyDN))
			{
				if (!LegacyDN.IsValidLegacyDN(this.SourceMailboxLegacyDN))
				{
					base.WriteError(new InvalidLegacyExchangeDnValueException("SourceMailboxLegacyDN"));
				}
				this.DataObject.SourceMailboxLegacyDN = this.SourceMailboxLegacyDN;
				this.changed = true;
				flag = true;
			}
			if (base.IsFieldSet("PublicFolderDatabaseServerLegacyDN") && !this.PublicFolderDatabaseServerLegacyDN.Equals(this.DataObject.PublicFolderDatabaseServerLegacyDN))
			{
				if (!LegacyDN.IsValidLegacyDN(this.PublicFolderDatabaseServerLegacyDN))
				{
					base.WriteError(new InvalidLegacyExchangeDnValueException("PublicFolderDatabaseServerLegacyDN"));
				}
				this.DataObject.PublicFolderDatabaseServerLegacyDN = this.PublicFolderDatabaseServerLegacyDN;
				this.changed = true;
				flag = true;
			}
			if (flag)
			{
				this.DataObject.LastModifiedTime = (DateTime)ExDateTime.UtcNow;
			}
			if (!this.SkipVerification)
			{
				MigrationEndpointBase migrationEndpointBase = MigrationEndpointBase.CreateFrom(this.DataObject);
				migrationEndpointBase.VerifyConnectivity();
				if (this.DataObject.EndpointType == MigrationType.ExchangeOutlookAnywhere)
				{
					ExchangeOutlookAnywhereEndpoint exchangeOutlookAnywhereEndpoint = (ExchangeOutlookAnywhereEndpoint)migrationEndpointBase;
					if (!string.IsNullOrEmpty(this.SourceMailboxLegacyDN) || this.EmailAddress != SmtpAddress.Empty || !string.IsNullOrEmpty(exchangeOutlookAnywhereEndpoint.EmailAddress))
					{
						MailboxData targetMailbox = TestMigrationServerAvailability.DiscoverTestMailbox(this.TestMailbox, ((MigrationADProvider)this.DataProvider.ADProvider).RecipientSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
						string text = (string)this.EmailAddress;
						if (string.IsNullOrEmpty(text))
						{
							text = exchangeOutlookAnywhereEndpoint.EmailAddress;
						}
						TestMigrationServerAvailability.VerifyExchangeOutlookAnywhereConnection(this.DataProvider, exchangeOutlookAnywhereEndpoint, text, this.SourceMailboxLegacyDN, targetMailbox, false);
						return;
					}
				}
				else if (this.DataObject.EndpointType == MigrationType.PublicFolder)
				{
					MailboxData mailboxData = TestMigrationServerAvailability.DiscoverPublicFolderTestMailbox(this.TestMailbox, this.ConfigurationSession, ((MigrationADProvider)this.DataProvider.ADProvider).RecipientSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
					TestMigrationServerAvailability.VerifyPublicFolderConnection(this.DataProvider, (PublicFolderEndpoint)migrationEndpointBase, this.SourceMailboxLegacyDN, this.PublicFolderDatabaseServerLegacyDN, mailboxData);
				}
			}
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000B6684 File Offset: 0x000B4884
		protected override void DisposeSession()
		{
			base.DisposeSession();
			if (this.DataProvider != null)
			{
				this.DataProvider.Dispose();
				this.DataProvider = null;
			}
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000B66A8 File Offset: 0x000B48A8
		private void WriteParameterErrorIfSet(string parameterName)
		{
			this.WriteParameterErrorIfSet(parameterName, null);
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000B66C8 File Offset: 0x000B48C8
		private void WriteParameterErrorIfSet(string parameterName, LocalizedString? reason)
		{
			if (base.IsFieldSet(parameterName))
			{
				base.WriteError(new InvalidEndpointParameterException(parameterName, this.DataObject.EndpointType.ToString(), reason ?? Strings.ErrorInvalidEndpointParameterReasonInvalidProperty));
			}
		}

		// Token: 0x040020A0 RID: 8352
		private const string ParameterNameMaxConcurrentMigrations = "MaxConcurrentMigrations";

		// Token: 0x040020A1 RID: 8353
		private const string ParameterNameMailboxPermission = "MailboxPermission";

		// Token: 0x040020A2 RID: 8354
		private const string ParameterNameCredentials = "Credentials";

		// Token: 0x040020A3 RID: 8355
		private const string ParameterNameExchangeServer = "ExchangeServer";

		// Token: 0x040020A4 RID: 8356
		private const string ParameterNameRpcProxyServer = "RPCProxyServer";

		// Token: 0x040020A5 RID: 8357
		private const string ParameterNameNspiServer = "NspiServer";

		// Token: 0x040020A6 RID: 8358
		private const string ParameterNameMaxConcurrentIncrementalSyncs = "MaxConcurrentIncrementalSyncs";

		// Token: 0x040020A7 RID: 8359
		private const string ParameterNamePort = "Port";

		// Token: 0x040020A8 RID: 8360
		private const string ParameterNameAuthentication = "Authentication";

		// Token: 0x040020A9 RID: 8361
		private const string ParameterNameSecurity = "Security";

		// Token: 0x040020AA RID: 8362
		private const string ParameterNameRemoteServer = "RemoteServer";

		// Token: 0x040020AB RID: 8363
		private const string ParameterNameTestMailbox = "TestMailbox";

		// Token: 0x040020AC RID: 8364
		private const string ParameterNameEmailAddress = "EmailAddress";

		// Token: 0x040020AD RID: 8365
		private const string ParameterNameSourceMailboxLegacyDN = "SourceMailboxLegacyDN";

		// Token: 0x040020AE RID: 8366
		private const string ParameterNamePublicFolderDatabaseServerLegacyDN = "PublicFolderDatabaseServerLegacyDN";

		// Token: 0x040020AF RID: 8367
		private const string ParameterNameSkipVerification = "SkipVerification";

		// Token: 0x040020B0 RID: 8368
		private bool changed;
	}
}
