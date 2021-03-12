using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004F3 RID: 1267
	[Cmdlet("New", "MigrationEndpoint", DefaultParameterSetName = "ExchangeRemoteMove", SupportsShouldProcess = true)]
	public sealed class NewMigrationEndpoint : NewMigrationObjectTaskBase<MigrationEndpoint>
	{
		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000B4B27 File Offset: 0x000B2D27
		// (set) Token: 0x06002D14 RID: 11540 RVA: 0x000B4B3E File Offset: 0x000B2D3E
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return (string)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x000B4B51 File Offset: 0x000B2D51
		// (set) Token: 0x06002D16 RID: 11542 RVA: 0x000B4B78 File Offset: 0x000B2D78
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxConcurrentMigrations
		{
			get
			{
				return (Unlimited<int>)(base.Fields["MaxConcurrentMigrations"] ?? new Unlimited<int>(20));
			}
			set
			{
				base.Fields["MaxConcurrentMigrations"] = value;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x000B4B90 File Offset: 0x000B2D90
		// (set) Token: 0x06002D18 RID: 11544 RVA: 0x000B4BED File Offset: 0x000B2DED
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxConcurrentIncrementalSyncs
		{
			get
			{
				Unlimited<int>? unlimited = (Unlimited<int>?)base.Fields["MaxConcurrentIncrementalSyncs"];
				if (unlimited == null)
				{
					unlimited = new Unlimited<int>?((this.MaxConcurrentMigrations < 10) ? this.MaxConcurrentMigrations : new Unlimited<int>(10));
				}
				return unlimited.Value;
			}
			set
			{
				base.Fields["MaxConcurrentIncrementalSyncs"] = value;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x000B4C05 File Offset: 0x000B2E05
		// (set) Token: 0x06002D1A RID: 11546 RVA: 0x000B4C1C File Offset: 0x000B2E1C
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhere")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeRemoteMove")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeRemoteMoveAutoDiscover")]
		[Parameter(Mandatory = false, ParameterSetName = "PSTImport")]
		[Parameter(Mandatory = true, ParameterSetName = "PublicFolder")]
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

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000B4C2F File Offset: 0x000B2E2F
		// (set) Token: 0x06002D1C RID: 11548 RVA: 0x000B4C54 File Offset: 0x000B2E54
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeRemoteMoveAutoDiscover")]
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

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x000B4C6C File Offset: 0x000B2E6C
		// (set) Token: 0x06002D1E RID: 11550 RVA: 0x000B4C83 File Offset: 0x000B2E83
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "IMAP")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeRemoteMove")]
		[Parameter(Mandatory = true, ParameterSetName = "PSTImport")]
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

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x000B4C96 File Offset: 0x000B2E96
		// (set) Token: 0x06002D20 RID: 11552 RVA: 0x000B4CB7 File Offset: 0x000B2EB7
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
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

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x000B4CCF File Offset: 0x000B2ECF
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x000B4CE6 File Offset: 0x000B2EE6
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
		[Parameter(Mandatory = true, ParameterSetName = "PublicFolder")]
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

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000B4CF9 File Offset: 0x000B2EF9
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x000B4D10 File Offset: 0x000B2F10
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "PublicFolder")]
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

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x000B4D23 File Offset: 0x000B2F23
		// (set) Token: 0x06002D26 RID: 11558 RVA: 0x000B4D3A File Offset: 0x000B2F3A
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = false, ParameterSetName = "PublicFolder")]
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

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x000B4D4D File Offset: 0x000B2F4D
		// (set) Token: 0x06002D28 RID: 11560 RVA: 0x000B4D64 File Offset: 0x000B2F64
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
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

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x000B4D77 File Offset: 0x000B2F77
		// (set) Token: 0x06002D2A RID: 11562 RVA: 0x000B4D8E File Offset: 0x000B2F8E
		[Parameter(Mandatory = true, ParameterSetName = "PublicFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
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

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000B4DA1 File Offset: 0x000B2FA1
		// (set) Token: 0x06002D2C RID: 11564 RVA: 0x000B4DB8 File Offset: 0x000B2FB8
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x000B4DCB File Offset: 0x000B2FCB
		// (set) Token: 0x06002D2E RID: 11566 RVA: 0x000B4DF0 File Offset: 0x000B2FF0
		[Parameter(Mandatory = false, ParameterSetName = "IMAP")]
		public int Port
		{
			get
			{
				return (int)(base.Fields["Port"] ?? 993);
			}
			set
			{
				base.Fields["Port"] = value;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000B4E08 File Offset: 0x000B3008
		// (set) Token: 0x06002D30 RID: 11568 RVA: 0x000B4E29 File Offset: 0x000B3029
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
		[Parameter(Mandatory = false, ParameterSetName = "IMAP")]
		[Parameter(Mandatory = false, ParameterSetName = "PublicFolder")]
		public AuthenticationMethod Authentication
		{
			get
			{
				return (AuthenticationMethod)(base.Fields["Authentication"] ?? AuthenticationMethod.Basic);
			}
			set
			{
				base.Fields["Authentication"] = value;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x000B4E41 File Offset: 0x000B3041
		// (set) Token: 0x06002D32 RID: 11570 RVA: 0x000B4E62 File Offset: 0x000B3062
		[Parameter(Mandatory = false, ParameterSetName = "IMAP")]
		public IMAPSecurityMechanism Security
		{
			get
			{
				return (IMAPSecurityMechanism)(base.Fields["Security"] ?? IMAPSecurityMechanism.Ssl);
			}
			set
			{
				base.Fields["Security"] = value;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x000B4E7A File Offset: 0x000B307A
		// (set) Token: 0x06002D34 RID: 11572 RVA: 0x000B4EA0 File Offset: 0x000B30A0
		[Parameter(Mandatory = true, ParameterSetName = "IMAP")]
		public SwitchParameter IMAP
		{
			get
			{
				return (SwitchParameter)(base.Fields["IMAP"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IMAP"] = value;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x000B4EB8 File Offset: 0x000B30B8
		// (set) Token: 0x06002D36 RID: 11574 RVA: 0x000B4EDE File Offset: 0x000B30DE
		[Parameter(Mandatory = true, ParameterSetName = "PSTImport")]
		public SwitchParameter PSTImport
		{
			get
			{
				return (SwitchParameter)(base.Fields["PSTImport"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["PSTImport"] = value;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x000B4EF6 File Offset: 0x000B30F6
		// (set) Token: 0x06002D38 RID: 11576 RVA: 0x000B4F1C File Offset: 0x000B311C
		[Parameter(Mandatory = true, ParameterSetName = "PublicFolder")]
		public SwitchParameter PublicFolder
		{
			get
			{
				return (SwitchParameter)(base.Fields["PublicFolder"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["PublicFolder"] = value;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000B4F34 File Offset: 0x000B3134
		// (set) Token: 0x06002D3A RID: 11578 RVA: 0x000B4F5A File Offset: 0x000B315A
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhere")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		public SwitchParameter ExchangeOutlookAnywhere
		{
			get
			{
				return (SwitchParameter)(base.Fields["ExchangeOutlookAnywhere"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ExchangeOutlookAnywhere"] = value;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000B4F72 File Offset: 0x000B3172
		// (set) Token: 0x06002D3C RID: 11580 RVA: 0x000B4F98 File Offset: 0x000B3198
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeRemoteMove")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeRemoteMoveAutoDiscover")]
		public SwitchParameter ExchangeRemoteMove
		{
			get
			{
				return (SwitchParameter)(base.Fields["ExchangeRemoteMove"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ExchangeRemoteMove"] = value;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x000B4FB0 File Offset: 0x000B31B0
		// (set) Token: 0x06002D3E RID: 11582 RVA: 0x000B4FD6 File Offset: 0x000B31D6
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeRemoteMoveAutoDiscover")]
		public SwitchParameter Autodiscover
		{
			get
			{
				return (SwitchParameter)(base.Fields["Autodiscover"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Autodiscover"] = value;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000B4FEE File Offset: 0x000B31EE
		// (set) Token: 0x06002D40 RID: 11584 RVA: 0x000B5014 File Offset: 0x000B3214
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

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000B502C File Offset: 0x000B322C
		// (set) Token: 0x06002D42 RID: 11586 RVA: 0x000B5034 File Offset: 0x000B3234
		internal MigrationDataProvider DataProvider { get; set; }

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x000B503D File Offset: 0x000B323D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMigrationEndpoint(this.Name);
			}
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000B504C File Offset: 0x000B324C
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "New-MigrationEndpoint";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			this.DataProvider = MigrationDataProvider.CreateProviderForMigrationMailbox(base.GetType().Name, base.TenantGlobalCatalogSession, this.partitionMailbox);
			return MigrationEndpointDataProvider.CreateDataProvider("NewMigrationEndpoint", base.TenantGlobalCatalogSession, this.partitionMailbox);
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000B50BC File Offset: 0x000B32BC
		protected override void InternalValidate()
		{
			if (this.MaxConcurrentIncrementalSyncs > this.MaxConcurrentMigrations)
			{
				base.WriteError(new MigrationMaxConcurrentIncrementalSyncsVerificationFailedException(this.MaxConcurrentIncrementalSyncs, this.MaxConcurrentMigrations));
			}
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x60029d6-1 == null)
				{
					<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x60029d6-1 = new Dictionary<string, int>(7)
					{
						{
							"ExchangeOutlookAnywhere",
							0
						},
						{
							"ExchangeOutlookAnywhereAutoDiscover",
							1
						},
						{
							"ExchangeRemoteMove",
							2
						},
						{
							"ExchangeRemoteMoveAutoDiscover",
							3
						},
						{
							"IMAP",
							4
						},
						{
							"PSTImport",
							5
						},
						{
							"PublicFolder",
							6
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x60029d6-1.TryGetValue(parameterSetName, out num))
				{
					switch (num)
					{
					case 0:
					case 1:
						this.endpointType = MigrationType.ExchangeOutlookAnywhere;
						break;
					case 2:
					case 3:
						this.endpointType = MigrationType.ExchangeRemoteMove;
						break;
					case 4:
						this.endpointType = MigrationType.IMAP;
						break;
					case 5:
						this.endpointType = MigrationType.PSTImport;
						break;
					case 6:
						this.endpointType = MigrationType.PublicFolder;
						break;
					default:
						goto IL_10C;
					}
					base.InternalValidate();
					this.DataObject.Identity = new MigrationEndpointId(this.Name, Guid.Empty);
					this.DataObject.EndpointType = this.endpointType;
					this.DataObject.MaxConcurrentIncrementalSyncs = this.MaxConcurrentIncrementalSyncs;
					this.DataObject.MaxConcurrentMigrations = this.MaxConcurrentMigrations;
					this.DataObject.Credentials = this.Credentials;
					string parameterSetName2;
					switch (parameterSetName2 = base.ParameterSetName)
					{
					case "ExchangeOutlookAnywhereAutoDiscover":
					case "ExchangeRemoteMoveAutoDiscover":
						this.PopulateAutoDiscover();
						break;
					case "ExchangeOutlookAnywhere":
						this.PopulateExchangeOutlookAnywhere();
						break;
					case "ExchangeRemoteMove":
						this.PopulateExchangeRemoteMove();
						break;
					case "IMAP":
						this.PopulateImap();
						break;
					case "PSTImport":
						this.PopulatePSTImport();
						break;
					case "PublicFolder":
						this.PopulatePublicFolder();
						break;
					}
					if (this.ExchangeOutlookAnywhere)
					{
						this.targetMailbox = TestMigrationServerAvailability.DiscoverTestMailbox(this.TestMailbox, ((MigrationADProvider)this.DataProvider.ADProvider).RecipientSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
					}
					bool flag = this.ExchangeOutlookAnywhere && !base.IsFieldSet("MailboxPermission");
					if (flag)
					{
						if (this.endpointObject == null)
						{
							this.endpointObject = MigrationEndpointBase.CreateFrom(this.DataObject);
						}
						try
						{
							ExchangeOutlookAnywhereEndpoint exchangeOutlookAnywhereEndpoint = (ExchangeOutlookAnywhereEndpoint)this.endpointObject;
							TestMigrationServerAvailability.VerifyExchangeOutlookAnywhereConnection(this.DataProvider, exchangeOutlookAnywhereEndpoint, (string)this.EmailAddress, this.SourceMailboxLegacyDN, this.targetMailbox, true);
							this.DataObject.MailboxPermission = exchangeOutlookAnywhereEndpoint.MailboxPermission;
						}
						catch (LocalizedException innerException)
						{
							base.WriteError(new UnableToDiscoverMailboxPermissionException(innerException));
						}
					}
					if (!this.SkipVerification)
					{
						if (this.endpointObject == null)
						{
							this.endpointObject = MigrationEndpointBase.CreateFrom(this.DataObject);
						}
						this.endpointObject.VerifyConnectivity();
						if (this.ExchangeOutlookAnywhere && !flag)
						{
							ExchangeOutlookAnywhereEndpoint outlookAnywhereEndpoint = (ExchangeOutlookAnywhereEndpoint)this.endpointObject;
							TestMigrationServerAvailability.VerifyExchangeOutlookAnywhereConnection(this.DataProvider, outlookAnywhereEndpoint, (string)this.EmailAddress, this.SourceMailboxLegacyDN, this.targetMailbox, false);
						}
						else if (this.PublicFolder)
						{
							this.targetMailbox = TestMigrationServerAvailability.DiscoverPublicFolderTestMailbox(this.TestMailbox, this.ConfigurationSession, ((MigrationADProvider)this.DataProvider.ADProvider).RecipientSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
							PublicFolderEndpoint publicFolderEndpoint = (PublicFolderEndpoint)this.endpointObject;
							TestMigrationServerAvailability.VerifyPublicFolderConnection(this.DataProvider, publicFolderEndpoint, this.SourceMailboxLegacyDN, this.PublicFolderDatabaseServerLegacyDN, this.targetMailbox);
						}
					}
					this.DataObject.LastModifiedTime = (DateTime)ExDateTime.UtcNow;
					return;
				}
			}
			IL_10C:
			throw new ArgumentException("Unexpected parameter set!");
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000B5540 File Offset: 0x000B3740
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationBatchDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000B5554 File Offset: 0x000B3754
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.disposed)
				{
					if (disposing)
					{
						this.DisposeSession();
					}
					this.disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000B5594 File Offset: 0x000B3794
		private void PopulateImap()
		{
			this.ValidateImap();
			this.DataObject.RemoteServer = this.RemoteServer;
			this.DataObject.Port = new int?(this.Port);
			this.DataObject.Security = new IMAPSecurityMechanism?(this.Security);
			this.DataObject.Authentication = new AuthenticationMethod?(this.Authentication);
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000B55FC File Offset: 0x000B37FC
		private void PopulateExchangeOutlookAnywhere()
		{
			this.ValidateExchangeOutlookAnywhere();
			this.DataObject.ExchangeServer = this.ExchangeServer;
			this.DataObject.RpcProxyServer = this.RpcProxyServer;
			this.DataObject.NspiServer = this.NspiServer;
			this.DataObject.Authentication = new AuthenticationMethod?(this.Authentication);
			this.DataObject.MailboxPermission = this.MailboxPermission;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000B566C File Offset: 0x000B386C
		private void PopulatePublicFolder()
		{
			this.ValidatePublicFolder();
			this.DataObject.Authentication = new AuthenticationMethod?(this.Authentication);
			this.DataObject.RpcProxyServer = (this.DataObject.RemoteServer = this.RpcProxyServer);
			this.DataObject.PublicFolderDatabaseServerLegacyDN = this.PublicFolderDatabaseServerLegacyDN;
			this.DataObject.SourceMailboxLegacyDN = this.SourceMailboxLegacyDN;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000B56D6 File Offset: 0x000B38D6
		private void PopulateExchangeRemoteMove()
		{
			this.ValidateExchangeRemoteMove();
			this.DataObject.RemoteServer = this.RemoteServer;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000B56F0 File Offset: 0x000B38F0
		private void PopulateAutoDiscover()
		{
			ValidationError validationError = MigrationConstraints.NameLengthConstraint.Validate(this.EmailAddress.ToString(), MigrationBatchMessageSchema.MigrationJobExchangeEmailAddress, null);
			if (validationError != null)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationRemoteServerTooLongException("EmailAddress")), (ErrorCategory)1000, null);
			}
			if (this.ExchangeOutlookAnywhere)
			{
				this.DataObject.MailboxPermission = this.MailboxPermission;
			}
			this.endpointObject = MigrationEndpointBase.CreateFrom(this.DataObject);
			this.endpointObject.InitializeFromAutoDiscover(this.EmailAddress, this.Credentials);
			this.DataObject = this.endpointObject.ToMigrationEndpoint();
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000B5797 File Offset: 0x000B3997
		private void PopulatePSTImport()
		{
			this.ValidatePSTImport();
			this.DataObject.RemoteServer = this.RemoteServer;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000B57B0 File Offset: 0x000B39B0
		private void ValidatePSTImport()
		{
			this.ValidateRemoteServerConstraint(this.RemoteServer.ToString(), MigrationBatchMessageSchema.MigrationJobRemoteServerHostName, "RemoteServer");
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000B57D0 File Offset: 0x000B39D0
		private void ValidateImap()
		{
			this.ValidateRemoteServerConstraint(this.RemoteServer.ToString(), MigrationBatchMessageSchema.MigrationJobRemoteServerHostName, "RemoteServer");
			ValidationError validationError = MigrationConstraints.PortRangeConstraint.Validate(this.Port, MigrationBatchMessageSchema.MigrationJobRemoteServerPortNumber, null);
			if (validationError != null)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationPortVerificationFailed(this.Port, MigrationConstraints.PortRangeConstraint.MinimumValue, MigrationConstraints.PortRangeConstraint.MaximumValue)));
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000B5844 File Offset: 0x000B3A44
		private void ValidateExchangeOutlookAnywhere()
		{
			this.ValidateRemoteServerConstraint(this.RpcProxyServer, MigrationBatchMessageSchema.MigrationJobExchangeRPCProxyServerHostName, "RpcProxyServer");
			this.ValidateRemoteServerConstraint(this.ExchangeServer, MigrationBatchMessageSchema.MigrationJobExchangeRemoteServerHostName, "ExchangeServer");
			bool flag = this.EmailAddress == SmtpAddress.Empty && string.IsNullOrEmpty(this.SourceMailboxLegacyDN);
			if (!base.IsFieldSet("MailboxPermission") && flag)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationMustSpecifyEmailOrMailboxDNOrMailboxPermission));
			}
			if (!this.SkipVerification && flag)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationMustSpecifyEmailOrMailboxDNOrSkipVerification));
			}
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000B58E4 File Offset: 0x000B3AE4
		private void ValidatePublicFolder()
		{
			this.ValidateRemoteServerConstraint(this.RpcProxyServer, MigrationBatchMessageSchema.MigrationJobExchangeRPCProxyServerHostName, "RpcProxyServer");
			if (!LegacyDN.IsValidLegacyDN(this.SourceMailboxLegacyDN))
			{
				base.WriteError(new InvalidLegacyExchangeDnValueException("SourceMailboxLegacyDN"));
			}
			if (!LegacyDN.IsValidLegacyDN(this.PublicFolderDatabaseServerLegacyDN))
			{
				base.WriteError(new InvalidLegacyExchangeDnValueException("PublicFolderDatabaseServerLegacyDN"));
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000B5946 File Offset: 0x000B3B46
		private void ValidateExchangeRemoteMove()
		{
			this.ValidateRemoteServerConstraint(this.RemoteServer.ToString(), MigrationBatchMessageSchema.MigrationJobRemoteServerHostName, "RemoteServer");
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000B5964 File Offset: 0x000B3B64
		private void ValidateRemoteServerConstraint(string remoteServer, StorePropertyDefinition propertyDefinition, string propertyName)
		{
			if (remoteServer == null)
			{
				base.WriteError(new MissingParameterException(propertyName));
			}
			ValidationError validationError = MigrationConstraints.RemoteServerNameConstraint.Validate(remoteServer, propertyDefinition, null);
			if (validationError != null)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationRemoteServerTooLongException(propertyName)));
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000B59A4 File Offset: 0x000B3BA4
		private void DisposeSession()
		{
			IDisposable disposable = base.DataSession as IDisposable;
			if (disposable != null)
			{
				MigrationLogger.Close();
				disposable.Dispose();
			}
			if (this.DataProvider != null)
			{
				this.DataProvider.Dispose();
				this.DataProvider = null;
			}
		}

		// Token: 0x0400207B RID: 8315
		private const int DefaultMaxConcurrentMigrations = 20;

		// Token: 0x0400207C RID: 8316
		private const int DefaultMaxConcurrentIncrementalSyncs = 10;

		// Token: 0x0400207D RID: 8317
		private const string ParameterNameIdentity = "Identity";

		// Token: 0x0400207E RID: 8318
		private const string ParameterNameMaxConcurrentMigrations = "MaxConcurrentMigrations";

		// Token: 0x0400207F RID: 8319
		private const string ParameterNameAutodiscover = "Autodiscover";

		// Token: 0x04002080 RID: 8320
		private const string ParameterNameTestMailbox = "TestMailbox";

		// Token: 0x04002081 RID: 8321
		private const string ParameterNameSourceMailboxLegacyDN = "SourceMailboxLegacyDN";

		// Token: 0x04002082 RID: 8322
		private const string ParameterNamePublicFolderDatabaseServerLegacyDN = "PublicFolderDatabaseServerLegacyDN";

		// Token: 0x04002083 RID: 8323
		private const string ParameterNameMailboxPermission = "MailboxPermission";

		// Token: 0x04002084 RID: 8324
		private const string ParameterNameCredentials = "Credentials";

		// Token: 0x04002085 RID: 8325
		private const string ParameterNameRemoteServer = "RemoteServer";

		// Token: 0x04002086 RID: 8326
		private const string ParameterNameExchangeServer = "ExchangeServer";

		// Token: 0x04002087 RID: 8327
		private const string ParameterNameRpcProxyServer = "RPCProxyServer";

		// Token: 0x04002088 RID: 8328
		private const string ParameterNameNspiServer = "NspiServer";

		// Token: 0x04002089 RID: 8329
		private const string ParameterNameImap = "IMAP";

		// Token: 0x0400208A RID: 8330
		private const string ParameterNamePstImport = "PSTImport";

		// Token: 0x0400208B RID: 8331
		private const string ParameterNamePublicFolder = "PublicFolder";

		// Token: 0x0400208C RID: 8332
		private const string ParameterNameExchangeOutlookAnywhere = "ExchangeOutlookAnywhere";

		// Token: 0x0400208D RID: 8333
		private const string ParameterNameExchangeRemoteMove = "ExchangeRemoteMove";

		// Token: 0x0400208E RID: 8334
		private const string ParameterNameMaxConcurrentIncrementalSyncs = "MaxConcurrentIncrementalSyncs";

		// Token: 0x0400208F RID: 8335
		private const string ParameterNamePort = "Port";

		// Token: 0x04002090 RID: 8336
		private const string ParameterNameAuthentication = "Authentication";

		// Token: 0x04002091 RID: 8337
		private const string ParameterNameEmailAddress = "EmailAddress";

		// Token: 0x04002092 RID: 8338
		private const string ParameterNameSecurity = "Security";

		// Token: 0x04002093 RID: 8339
		private const string ParameterSetNameImap = "IMAP";

		// Token: 0x04002094 RID: 8340
		private const string ParameterNameSkipVerification = "SkipVerification";

		// Token: 0x04002095 RID: 8341
		private const string ParameterSetNameExchangeOutlookAnywhere = "ExchangeOutlookAnywhere";

		// Token: 0x04002096 RID: 8342
		private const string ParameterSetNameExchangeOutlookAnywhereAutoDiscover = "ExchangeOutlookAnywhereAutoDiscover";

		// Token: 0x04002097 RID: 8343
		private const string ParameterSetNameExchangeRemoteMove = "ExchangeRemoteMove";

		// Token: 0x04002098 RID: 8344
		private const string ParameterSetNameExchangeRemoteMoveAutoDiscover = "ExchangeRemoteMoveAutoDiscover";

		// Token: 0x04002099 RID: 8345
		private const string ParameterSetNamePstImport = "PSTImport";

		// Token: 0x0400209A RID: 8346
		private const string ParameterSetNamePublicFolder = "PublicFolder";

		// Token: 0x0400209B RID: 8347
		private MailboxData targetMailbox;

		// Token: 0x0400209C RID: 8348
		private MigrationType endpointType;

		// Token: 0x0400209D RID: 8349
		private MigrationEndpointBase endpointObject;

		// Token: 0x0400209E RID: 8350
		private bool disposed;
	}
}
