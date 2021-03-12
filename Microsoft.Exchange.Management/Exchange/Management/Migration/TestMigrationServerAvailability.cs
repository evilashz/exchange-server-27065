using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004F0 RID: 1264
	[Cmdlet("Test", "MigrationServerAvailability", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class TestMigrationServerAvailability : MigrationOrganizationTaskBase
	{
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000B33B7 File Offset: 0x000B15B7
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x000B33DD File Offset: 0x000B15DD
		[Parameter(Mandatory = true, ParameterSetName = "IMAP")]
		public SwitchParameter Imap
		{
			get
			{
				return (SwitchParameter)(base.Fields["Imap"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Imap"] = value;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000B33F5 File Offset: 0x000B15F5
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x000B3411 File Offset: 0x000B1611
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "IMAP")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeRemoteMove")]
		[Parameter(Mandatory = true, ParameterSetName = "PSTImport")]
		public Fqdn RemoteServer
		{
			get
			{
				return Fqdn.Parse((string)base.Fields["RemoteServer"]);
			}
			set
			{
				base.Fields["RemoteServer"] = value.ToString();
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000B3429 File Offset: 0x000B1629
		// (set) Token: 0x06002CBF RID: 11455 RVA: 0x000B344E File Offset: 0x000B164E
		[Parameter(Mandatory = true, ParameterSetName = "IMAP")]
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

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000B3466 File Offset: 0x000B1666
		// (set) Token: 0x06002CC1 RID: 11457 RVA: 0x000B3487 File Offset: 0x000B1687
		[Parameter(Mandatory = false, ParameterSetName = "IMAP")]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
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

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x000B349F File Offset: 0x000B169F
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x000B34C0 File Offset: 0x000B16C0
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

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000B34D8 File Offset: 0x000B16D8
		// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x000B34FE File Offset: 0x000B16FE
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

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000B3516 File Offset: 0x000B1716
		// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x000B353C File Offset: 0x000B173C
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

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000B3554 File Offset: 0x000B1754
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x000B357A File Offset: 0x000B177A
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

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000B3592 File Offset: 0x000B1792
		// (set) Token: 0x06002CCB RID: 11467 RVA: 0x000B35A9 File Offset: 0x000B17A9
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeRemoteMove")]
		[Parameter(Mandatory = true, ParameterSetName = "PSTImport")]
		[Parameter(Mandatory = true, ParameterSetName = "PublicFolder")]
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeRemoteMoveAutoDiscover")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhere")]
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

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000B35BC File Offset: 0x000B17BC
		// (set) Token: 0x06002CCD RID: 11469 RVA: 0x000B35E1 File Offset: 0x000B17E1
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeRemoteMoveAutoDiscover")]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
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

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x000B35F9 File Offset: 0x000B17F9
		// (set) Token: 0x06002CCF RID: 11471 RVA: 0x000B3610 File Offset: 0x000B1810
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhere")]
		[Parameter(Mandatory = true, ParameterSetName = "PublicFolder")]
		public Fqdn RPCProxyServer
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

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x000B3623 File Offset: 0x000B1823
		// (set) Token: 0x06002CD1 RID: 11473 RVA: 0x000B363A File Offset: 0x000B183A
		[Parameter(Mandatory = true, ParameterSetName = "ExchangeOutlookAnywhere")]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000B364D File Offset: 0x000B184D
		// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x000B366E File Offset: 0x000B186E
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

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x000B3686 File Offset: 0x000B1886
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x000B369D File Offset: 0x000B189D
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = true, ParameterSetName = "PublicFolder")]
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

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000B36B0 File Offset: 0x000B18B0
		// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x000B36C7 File Offset: 0x000B18C7
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

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000B36DA File Offset: 0x000B18DA
		// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x000B36F1 File Offset: 0x000B18F1
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhereAutoDiscover")]
		[Parameter(Mandatory = false, ParameterSetName = "ExchangeOutlookAnywhere")]
		[Parameter(Mandatory = false, ParameterSetName = "PublicFolder")]
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

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x000B3704 File Offset: 0x000B1904
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x000B371B File Offset: 0x000B191B
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "TestEndpoint")]
		public MigrationEndpointIdParameter Endpoint
		{
			get
			{
				return (MigrationEndpointIdParameter)base.Fields["Endpoint"];
			}
			set
			{
				base.Fields["Endpoint"] = value;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000B372E File Offset: 0x000B192E
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000B3754 File Offset: 0x000B1954
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

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000B376C File Offset: 0x000B196C
		// (set) Token: 0x06002CDF RID: 11487 RVA: 0x000B3792 File Offset: 0x000B1992
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

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000B37AA File Offset: 0x000B19AA
		// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x000B37C1 File Offset: 0x000B19C1
		[Parameter(Mandatory = false, ParameterSetName = "PSTImport")]
		public string FilePath
		{
			get
			{
				return (string)base.Fields["FilePath"];
			}
			set
			{
				base.Fields["FilePath"] = value;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000B37D4 File Offset: 0x000B19D4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestMigrationServerAvailability;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000B37DB File Offset: 0x000B19DB
		// (set) Token: 0x06002CE4 RID: 11492 RVA: 0x000B37E3 File Offset: 0x000B19E3
		private bool SupportsCutover { get; set; }

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x000B37EC File Offset: 0x000B19EC
		private IRecipientSession RecipientSession
		{
			get
			{
				return base.DataSession as IRecipientSession;
			}
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000B37FC File Offset: 0x000B19FC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			string parameterSetName;
			switch (parameterSetName = base.ParameterSetName)
			{
			case "IMAP":
			case "ExchangeOutlookAnywhere":
			case "ExchangeOutlookAnywhereAutoDiscover":
			case "ExchangeRemoteMove":
			case "ExchangeRemoteMoveAutoDiscover":
			case "PSTImport":
			case "PublicFolder":
				goto IL_D4;
			case "TestEndpoint":
				this.ValidateEndpoint();
				return;
			}
			this.WriteError(new TestMigrationServerAvailabilityProtocolArgumentException());
			IL_D4:
			if (this.Imap)
			{
				this.ValidateImapParameters();
				return;
			}
			if (this.ExchangeOutlookAnywhere)
			{
				this.ValidateExchangeOutlookAnywhereParameters();
				return;
			}
			if (this.ExchangeRemoteMove)
			{
				this.ValidateExchangeRemoteMoveParameters();
				return;
			}
			if (this.PSTImport)
			{
				this.ValidatePSTImportParameters();
				return;
			}
			if (this.PublicFolder)
			{
				this.ValidatePublicFolderParameters();
				return;
			}
			this.WriteError(new TestMigrationServerAvailabilityProtocolArgumentException());
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000B394C File Offset: 0x000B1B4C
		protected override void InternalProcessRecord()
		{
			this.SupportsCutover = false;
			if (this.Imap || this.ExchangeOutlookAnywhere)
			{
				this.SupportsCutover = MigrationSession.SupportsCutover(base.DataProvider);
			}
			if (this.ExchangeOutlookAnywhere)
			{
				this.InternalProcessExchangeOutlookAnywhere(base.DataProvider);
				return;
			}
			if (this.ExchangeRemoteMove && this.Autodiscover)
			{
				this.InternalProcessExchangeRemoteMoveAutoDiscover();
				return;
			}
			if (this.PSTImport)
			{
				this.InternalProcessPSTImport();
				return;
			}
			if (this.PublicFolder)
			{
				this.InternalProcessPublicFolder();
				return;
			}
			if (this.endpoint != null)
			{
				this.InternalProcessEndpoint(false);
				return;
			}
			MigrationLogger.Log(MigrationEventType.Error, "TestMigrationServerAvailability.InternalProcessRecord: Wrong protocol", new object[0]);
			this.WriteError(new TestMigrationServerAvailabilityProtocolArgumentException());
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000B3A1C File Offset: 0x000B1C1C
		private TestMigrationServerAvailabilityOutcome CreateAutodsicoverFailedOutcome(LocalizedException exception)
		{
			LocalizedString message = exception.Data.Contains("AutoDiscoverResponseMessage") ? ((LocalizedString)exception.Data["AutoDiscoverResponseMessage"]) : exception.LocalizedString;
			string errorDetail = exception.Data.Contains("AutoDiscoverResponseErrorDetail") ? ((string)exception.Data["AutoDiscoverResponseErrorDetail"]) : exception.ToString();
			return TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Failed, this.SupportsCutover, message, errorDetail);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000B3A98 File Offset: 0x000B1C98
		private void ValidateEndpoint()
		{
			using (MigrationEndpointDataProvider migrationEndpointDataProvider = MigrationEndpointDataProvider.CreateDataProvider("Test-MigrationServerAvailability", this.RecipientSession, this.partitionMailbox))
			{
				this.endpoint = (MigrationEndpoint)base.GetDataObject<MigrationEndpoint>(this.Endpoint, migrationEndpointDataProvider, null, new LocalizedString?(Strings.MigrationEndpointNotFound(this.Endpoint.RawIdentity)), new LocalizedString?(Strings.MigrationEndpointIdentityAmbiguous(this.Endpoint.RawIdentity)));
			}
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000B3B20 File Offset: 0x000B1D20
		private void InternalProcessEndpoint(bool fromAutoDiscover)
		{
			TestMigrationServerAvailabilityOutcome sendToPipeline;
			try
			{
				this.endpoint.VerifyConnectivity();
				ExchangeConnectionSettings connectionSettings = this.endpoint.ConnectionSettings as ExchangeConnectionSettings;
				sendToPipeline = TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Success, this.SupportsCutover, connectionSettings);
			}
			catch (LocalizedException ex)
			{
				if (fromAutoDiscover)
				{
					ex = new MigrationRemoteEndpointSettingsCouldNotBeAutodiscoveredException(this.endpoint.RemoteServer.ToString(), ex);
				}
				sendToPipeline = TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Failed, this.SupportsCutover, ex.LocalizedString, ex.ToString());
			}
			base.WriteObject(sendToPipeline);
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000B3BA8 File Offset: 0x000B1DA8
		private void InternalProcessExchangeRemoteMoveAutoDiscover()
		{
			try
			{
				ExchangeRemoteMoveEndpoint exchangeRemoteMoveEndpoint = new ExchangeRemoteMoveEndpoint();
				exchangeRemoteMoveEndpoint.InitializeFromAutoDiscover(this.EmailAddress, this.Credentials);
				this.endpoint = exchangeRemoteMoveEndpoint;
			}
			catch (LocalizedException ex)
			{
				MigrationLogger.Log(MigrationEventType.Error, ex, "Failed to determine remote endpoint via auto-discover.", new object[0]);
				base.WriteObject(TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Failed, this.SupportsCutover, ex.LocalizedString, ex.ToString()));
				return;
			}
			this.InternalProcessEndpoint(true);
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000B3C20 File Offset: 0x000B1E20
		private void InternalProcessPSTImport()
		{
			this.InternalProcessEndpoint(false);
			if (!string.IsNullOrEmpty(this.FilePath))
			{
				PSTImportEndpoint pstimportEndpoint = this.endpoint as PSTImportEndpoint;
				this.TestPstImportSubscription(base.DataProvider, pstimportEndpoint, this.targetMailbox);
			}
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000B3C60 File Offset: 0x000B1E60
		private void InternalProcessPublicFolder()
		{
			TestMigrationServerAvailabilityOutcome sendToPipeline;
			try
			{
				TestMigrationServerAvailability.VerifyPublicFolderConnection(base.DataProvider, (PublicFolderEndpoint)this.endpoint, this.SourceMailboxLegacyDN, this.PublicFolderDatabaseServerLegacyDN, this.targetMailbox);
				ExchangeConnectionSettings connectionSettings = this.endpoint.ConnectionSettings as ExchangeConnectionSettings;
				sendToPipeline = TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Success, this.SupportsCutover, connectionSettings);
			}
			catch (LocalizedException ex)
			{
				sendToPipeline = TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Failed, this.SupportsCutover, ex.LocalizedString, ex.ToString());
			}
			base.WriteObject(sendToPipeline);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000B3CEC File Offset: 0x000B1EEC
		private void ValidateRemoteServerConstraint(string remoteServer, StorePropertyDefinition propertyDefinition, string propertyName)
		{
			if (remoteServer == null)
			{
				this.WriteError(new MissingParameterException(propertyName));
			}
			ValidationError validationError = MigrationConstraints.RemoteServerNameConstraint.Validate(remoteServer, propertyDefinition, null);
			if (validationError != null)
			{
				this.WriteError(new MigrationPermanentException(Strings.MigrationRemoteServerTooLongException(propertyName)));
			}
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000B3D2C File Offset: 0x000B1F2C
		private void InternalProcessExchangeOutlookAnywhere(IMigrationDataProvider dataProvider)
		{
			ExchangeOutlookAnywhereEndpoint exchangeOutlookAnywhereEndpoint = new ExchangeOutlookAnywhereEndpoint();
			try
			{
				if (this.Autodiscover)
				{
					TestMigrationServerAvailabilityOutcome testMigrationServerAvailabilityOutcome = null;
					try
					{
						exchangeOutlookAnywhereEndpoint.InitializeFromAutoDiscover(this.EmailAddress, this.Credentials);
					}
					catch (AutoDiscoverFailedConfigurationErrorException exception)
					{
						testMigrationServerAvailabilityOutcome = this.CreateAutodsicoverFailedOutcome(exception);
					}
					catch (AutoDiscoverFailedInternalErrorException exception2)
					{
						testMigrationServerAvailabilityOutcome = this.CreateAutodsicoverFailedOutcome(exception2);
					}
					if (testMigrationServerAvailabilityOutcome != null)
					{
						MigrationLogger.Log(MigrationEventType.Information, testMigrationServerAvailabilityOutcome.ToString(), new object[0]);
						base.WriteObject(testMigrationServerAvailabilityOutcome);
						return;
					}
				}
				else
				{
					exchangeOutlookAnywhereEndpoint.RpcProxyServer = this.RPCProxyServer;
					exchangeOutlookAnywhereEndpoint.Credentials = this.Credentials;
					exchangeOutlookAnywhereEndpoint.ExchangeServer = this.ExchangeServer;
					exchangeOutlookAnywhereEndpoint.AuthenticationMethod = this.Authentication;
				}
				IMigrationNspiClient nspiClient = MigrationServiceFactory.Instance.GetNspiClient(null);
				exchangeOutlookAnywhereEndpoint.NspiServer = nspiClient.GetNewDSA(exchangeOutlookAnywhereEndpoint);
				exchangeOutlookAnywhereEndpoint.MailboxPermission = this.MailboxPermission;
				NspiMigrationDataReader nspiDataReader = exchangeOutlookAnywhereEndpoint.GetNspiDataReader(null);
				nspiDataReader.Ping();
				ExchangeOutlookAnywhereEndpoint.ValidateEndpoint(exchangeOutlookAnywhereEndpoint);
			}
			catch (MigrationTransientException ex)
			{
				MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(ex, null), new object[0]);
				base.WriteObject(TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Failed, this.SupportsCutover, ex.LocalizedString, ex.InternalError));
				return;
			}
			catch (MigrationPermanentException ex2)
			{
				MigrationLogger.Log(MigrationEventType.Error, MigrationLogger.GetDiagnosticInfo(ex2, null), new object[0]);
				base.WriteObject(TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Failed, this.SupportsCutover, ex2.LocalizedString, ex2.InternalError));
				return;
			}
			TestMigrationServerAvailabilityOutcome testMigrationServerAvailabilityOutcome2;
			try
			{
				TestMigrationServerAvailability.VerifyExchangeOutlookAnywhereConnection(dataProvider, exchangeOutlookAnywhereEndpoint, (string)this.EmailAddress, this.SourceMailboxLegacyDN, this.targetMailbox, !this.IsFieldSet("MailboxPermission"));
				testMigrationServerAvailabilityOutcome2 = TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Success, this.SupportsCutover, (ExchangeConnectionSettings)exchangeOutlookAnywhereEndpoint.ConnectionSettings);
			}
			catch (LocalizedException ex3)
			{
				string diagnosticInfo = MigrationLogger.GetDiagnosticInfo(ex3, null);
				MigrationLogger.Log(MigrationEventType.Error, diagnosticInfo, new object[0]);
				testMigrationServerAvailabilityOutcome2 = TestMigrationServerAvailabilityOutcome.Create(TestMigrationServerAvailabilityResult.Failed, this.SupportsCutover, ex3.LocalizedString, diagnosticInfo);
				testMigrationServerAvailabilityOutcome2.ConnectionSettings = (ExchangeConnectionSettings)exchangeOutlookAnywhereEndpoint.ConnectionSettings;
			}
			base.WriteObject(testMigrationServerAvailabilityOutcome2);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000B3F54 File Offset: 0x000B2154
		internal static MailboxData DiscoverTestMailbox(IIdentityParameter identity, IRecipientSession adSession, ADServerSettings serverSettings, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject, Task.TaskVerboseLoggingDelegate writeVerbose, Task.ErrorLoggerDelegate writeError)
		{
			if (identity == null)
			{
				MigrationADProvider migrationADProvider = new MigrationADProvider(adSession);
				return migrationADProvider.GetMailboxDataForManagementMailbox();
			}
			ADUser aduser = RequestTaskHelper.ResolveADUser(adSession, adSession, serverSettings, identity, null, null, getDataObject, writeVerbose, writeError, true);
			MailboxData mailboxData = new MailboxData(aduser.ExchangeGuid, new Fqdn(aduser.ServerName), aduser.LegacyExchangeDN, aduser.Id, aduser.ExchangeObjectId);
			mailboxData.Update(identity.RawIdentity, aduser.OrganizationId);
			return mailboxData;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000B3FC0 File Offset: 0x000B21C0
		internal static MailboxData DiscoverPublicFolderTestMailbox(IIdentityParameter identity, IConfigurationSession configurationSession, IRecipientSession recipientSession, ADServerSettings serverSettings, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject, Task.TaskVerboseLoggingDelegate writeVerbose, Task.ErrorLoggerDelegate writeError)
		{
			if (identity == null)
			{
				Organization orgContainer = configurationSession.GetOrgContainer();
				if (orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid != default(Guid))
				{
					identity = new MailboxIdParameter(orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid.ToString());
				}
				else
				{
					writeError(new MigrationPermanentException(Strings.ErrorUnableToFindValidPublicFolderMailbox), ExchangeErrorCategory.Client, null);
				}
			}
			ADUser aduser = RequestTaskHelper.ResolveADUser(recipientSession, recipientSession, serverSettings, identity, new OptionalIdentityData(), null, getDataObject, writeVerbose, writeError, true);
			if (aduser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
			{
				writeError(new MigrationPermanentException(Strings.ErrorNotPublicFolderMailbox(identity.RawIdentity)), ExchangeErrorCategory.Client, null);
			}
			return MailboxData.CreateFromADUser(aduser);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000B4078 File Offset: 0x000B2278
		internal static bool VerifyExchangeOutlookAnywhereConnection(IMigrationDataProvider dataProvider, ExchangeOutlookAnywhereEndpoint outlookAnywhereEndpoint, string emailAddress, string sourceMailboxLegacyDN, MailboxData targetMailbox, bool discoverAdminPrivilege)
		{
			string mailboxDN;
			if (!string.IsNullOrEmpty(sourceMailboxLegacyDN))
			{
				mailboxDN = sourceMailboxLegacyDN;
			}
			else if (!string.IsNullOrEmpty(emailAddress) && (!outlookAnywhereEndpoint.UseAutoDiscover || outlookAnywhereEndpoint.AutodiscoverResponse == null))
			{
				NspiMigrationDataReader nspiDataReader = outlookAnywhereEndpoint.GetNspiDataReader(null);
				IMigrationDataRow recipient = nspiDataReader.GetRecipient(emailAddress);
				NspiMigrationDataRow nspiMigrationDataRow = recipient as NspiMigrationDataRow;
				if (nspiMigrationDataRow == null)
				{
					LocalizedString localizedErrorMessage = LocalizedString.Empty;
					InvalidDataRow invalidDataRow = recipient as InvalidDataRow;
					if (invalidDataRow != null && invalidDataRow.Error != null)
					{
						LocalizedString localizedErrorMessage2 = invalidDataRow.Error.LocalizedErrorMessage;
						localizedErrorMessage = invalidDataRow.Error.LocalizedErrorMessage;
					}
					throw new MigrationPermanentException(localizedErrorMessage);
				}
				mailboxDN = nspiMigrationDataRow.Recipient.GetPropertyValue<string>(PropTag.EmailAddress);
			}
			else
			{
				mailboxDN = outlookAnywhereEndpoint.AutodiscoverResponse.MailboxDN;
			}
			ExchangeJobItemSubscriptionSettings subscriptionSettings = ExchangeJobItemSubscriptionSettings.CreateFromProperties(mailboxDN, outlookAnywhereEndpoint.ExchangeServer, outlookAnywhereEndpoint.ExchangeServer, outlookAnywhereEndpoint.RpcProxyServer);
			bool flag = false;
			try
			{
				IL_C3:
				MRSMergeRequestAccessor mrsmergeRequestAccessor = new MRSMergeRequestAccessor(dataProvider, null, false);
				mrsmergeRequestAccessor.TestCreateSubscription(outlookAnywhereEndpoint, null, subscriptionSettings, targetMailbox, null, false);
			}
			catch (LocalizedException ex)
			{
				if (discoverAdminPrivilege && !flag && CommonUtils.GetExceptionSide(ex) == ExceptionSide.Source)
				{
					Exception ex2 = ex;
					while (ex2.InnerException != null)
					{
						ex2 = ex2.InnerException;
					}
					if (CommonUtils.ExceptionIs(ex2, new WellKnownException[]
					{
						WellKnownException.MapiLogonFailed
					}))
					{
						if (outlookAnywhereEndpoint.MailboxPermission == MigrationMailboxPermission.Admin)
						{
							outlookAnywhereEndpoint.MailboxPermission = MigrationMailboxPermission.FullAccess;
						}
						else
						{
							outlookAnywhereEndpoint.MailboxPermission = MigrationMailboxPermission.Admin;
						}
						flag = true;
						goto IL_C3;
					}
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000B41FC File Offset: 0x000B23FC
		internal static void VerifyPublicFolderConnection(IMigrationDataProvider dataProvider, PublicFolderEndpoint publicFolderEndpoint, string sourceMailboxLegacyDn, string publicFolderDatabaseServerLegacyDn, MailboxData mailboxData)
		{
			MrsPublicFolderAccessor mrsPublicFolderAccessor = new MrsPublicFolderAccessor(dataProvider, "TestMigrationServerAvailability");
			using (Stream stream = new MemoryStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					streamWriter.WriteLine("\"{0}\",\"{1}\"", "FolderPath", "TargetMailbox");
					streamWriter.WriteLine("\"\\\",\"{0}\"", mailboxData.Name);
					streamWriter.Flush();
					stream.Seek(0L, SeekOrigin.Begin);
					mrsPublicFolderAccessor.TestCreateSubscription(publicFolderEndpoint, mailboxData, stream);
				}
			}
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000B4298 File Offset: 0x000B2498
		private void ValidateAutodiscoverParameters()
		{
			ValidationError validationError = MigrationConstraints.NameLengthConstraint.Validate(this.EmailAddress.ToString(), MigrationBatchMessageSchema.MigrationJobExchangeEmailAddress, null);
			if (validationError != null)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationRemoteServerTooLongException("EmailAddress")), (ErrorCategory)1000, null);
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000B42E8 File Offset: 0x000B24E8
		private void ValidateImapParameters()
		{
			if (base.CurrentOrganizationId == null || OrganizationId.ForestWideOrgId.Equals(base.CurrentOrganizationId))
			{
				this.WriteError(new InvalidOrganizationException());
			}
			this.ValidateRemoteServerConstraint(this.RemoteServer.ToString(), MigrationBatchMessageSchema.MigrationJobRemoteServerHostName, "RemoteServer");
			ValidationError validationError = MigrationConstraints.PortRangeConstraint.Validate(this.Port, MigrationBatchMessageSchema.MigrationJobRemoteServerPortNumber, null);
			if (validationError != null)
			{
				this.WriteError(new MigrationPermanentException(Strings.MigrationPortVerificationFailed(this.Port, MigrationConstraints.PortRangeConstraint.MinimumValue, MigrationConstraints.PortRangeConstraint.MaximumValue)));
			}
			this.endpoint = new ImapEndpoint
			{
				RemoteServer = this.RemoteServer,
				Credentials = this.Credentials,
				Port = this.Port,
				Security = this.Security,
				AuthenticationMethod = this.Authentication
			};
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000B43D0 File Offset: 0x000B25D0
		private void ValidateExchangeOutlookAnywhereParameters()
		{
			if (!this.Autodiscover)
			{
				this.ValidateRemoteServerConstraint(this.RPCProxyServer.ToString(), MigrationBatchMessageSchema.MigrationJobExchangeRPCProxyServerHostName, "RPCProxyServer");
				this.ValidateRemoteServerConstraint(this.ExchangeServer, MigrationBatchMessageSchema.MigrationJobExchangeRemoteServerHostName, "ExchangeServer");
				if (this.EmailAddress == SmtpAddress.Empty && string.IsNullOrEmpty(this.SourceMailboxLegacyDN))
				{
					this.WriteError(new MigrationPermanentException(Strings.MigrationMustSpecifyEmailOrMailboxDN));
				}
			}
			else
			{
				this.ValidateAutodiscoverParameters();
			}
			if (base.CurrentOrganizationId == null || OrganizationId.ForestWideOrgId.Equals(base.CurrentOrganizationId))
			{
				this.WriteError(new InvalidOrganizationException());
			}
			this.targetMailbox = TestMigrationServerAvailability.DiscoverTestMailbox(this.TestMailbox, this.RecipientSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000B44C0 File Offset: 0x000B26C0
		private void ValidateExchangeRemoteMoveParameters()
		{
			if (!this.Autodiscover)
			{
				this.ValidateRemoteServerConstraint(this.RemoteServer.ToString(), MigrationBatchMessageSchema.MigrationJobRemoteServerHostName, "RemoteServer");
				this.endpoint = new ExchangeRemoteMoveEndpoint
				{
					RemoteServer = this.RemoteServer,
					Credentials = this.Credentials
				};
				return;
			}
			this.ValidateAutodiscoverParameters();
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000B4524 File Offset: 0x000B2724
		private void ValidatePSTImportParameters()
		{
			this.ValidateRemoteServerConstraint(this.RemoteServer.ToString(), MigrationBatchMessageSchema.MigrationJobRemoteServerHostName, "RemoteServer");
			this.endpoint = new PSTImportEndpoint
			{
				RemoteServer = this.RemoteServer,
				Credentials = this.Credentials
			};
			this.targetMailbox = TestMigrationServerAvailability.DiscoverTestMailbox(this.TestMailbox, this.RecipientSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000B45B4 File Offset: 0x000B27B4
		private void ValidatePublicFolderParameters()
		{
			this.ValidateRemoteServerConstraint(this.RPCProxyServer, MigrationBatchMessageSchema.MigrationJobExchangeRPCProxyServerHostName, "RpcProxyServer");
			if (!LegacyDN.IsValidLegacyDN(this.SourceMailboxLegacyDN))
			{
				this.WriteError(new InvalidLegacyExchangeDnValueException("SourceMailboxLegacyDN"));
			}
			if (!LegacyDN.IsValidLegacyDN(this.PublicFolderDatabaseServerLegacyDN))
			{
				this.WriteError(new InvalidLegacyExchangeDnValueException("PublicFolderDatabaseServerLegacyDN"));
			}
			this.endpoint = new PublicFolderEndpoint
			{
				RpcProxyServer = this.RPCProxyServer,
				AuthenticationMethod = this.Authentication,
				SourceMailboxLegacyDN = this.SourceMailboxLegacyDN,
				PublicFolderDatabaseServerLegacyDN = this.PublicFolderDatabaseServerLegacyDN,
				Credentials = this.Credentials
			};
			this.targetMailbox = TestMigrationServerAvailability.DiscoverPublicFolderTestMailbox(this.TestMailbox, this.ConfigurationSession, this.RecipientSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000B46A6 File Offset: 0x000B28A6
		private bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000B46C4 File Offset: 0x000B28C4
		private void TestPstImportSubscription(IMigrationDataProvider dataProvider, PSTImportEndpoint endpoint, MailboxData targetMailbox)
		{
			PSTJobItemSubscriptionSettings jobItemSettings = PSTJobItemSubscriptionSettings.CreateFromProperties(this.FilePath);
			PSTImportAccessor pstimportAccessor = new PSTImportAccessor(dataProvider, null);
			pstimportAccessor.TestCreateSubscription(endpoint, null, jobItemSettings, targetMailbox, "PSTImport");
		}

		// Token: 0x04002066 RID: 8294
		private const string ParameterSetNameTestEndpoint = "TestEndpoint";

		// Token: 0x04002067 RID: 8295
		private const string ParameterSetNameImap = "IMAP";

		// Token: 0x04002068 RID: 8296
		private const string ParameterSetNameExchangeOutlookAnywhere = "ExchangeOutlookAnywhere";

		// Token: 0x04002069 RID: 8297
		private const string ParameterSetNameExchangeOutlookAnywhereAutoDiscover = "ExchangeOutlookAnywhereAutoDiscover";

		// Token: 0x0400206A RID: 8298
		private const string ParameterSetNameExchangeRemoteMove = "ExchangeRemoteMove";

		// Token: 0x0400206B RID: 8299
		private const string ParameterSetNameExchangeRemoteMoveAutoDiscover = "ExchangeRemoteMoveAutoDiscover";

		// Token: 0x0400206C RID: 8300
		private const string ParameterSetNamePstImport = "PSTImport";

		// Token: 0x0400206D RID: 8301
		private const string ParameterSetNamePublicFolder = "PublicFolder";

		// Token: 0x0400206E RID: 8302
		private const string ParameterNameMailboxPermission = "MailboxPermission";

		// Token: 0x0400206F RID: 8303
		private const string ParameterNameEmailAddress = "EmailAddress";

		// Token: 0x04002070 RID: 8304
		private const string ParameterNameSourceMailboxLegacyDN = "SourceMailboxLegacyDN";

		// Token: 0x04002071 RID: 8305
		private const string ParameterNamePublicFolderDatabaseServerLegacyDN = "PublicFolderDatabaseServerLegacyDN";

		// Token: 0x04002072 RID: 8306
		private MailboxData targetMailbox;

		// Token: 0x04002073 RID: 8307
		private MigrationEndpointBase endpoint;
	}
}
