using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000028 RID: 40
	[Cmdlet("New", "ConnectSubscription", SupportsShouldProcess = true, DefaultParameterSetName = "FacebookParameterSet")]
	public sealed class NewConnectSubscription : NewSubscriptionBase<ConnectSubscriptionProxy>
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000805E File Offset: 0x0000625E
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00008066 File Offset: 0x00006266
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		public new MailboxIdParameter Mailbox
		{
			get
			{
				return base.Mailbox;
			}
			set
			{
				base.Mailbox = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000806F File Offset: 0x0000626F
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000809F File Offset: 0x0000629F
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "FacebookParameterSet")]
		public SwitchParameter Facebook
		{
			get
			{
				if (base.Fields["Facebook"] == null)
				{
					return new SwitchParameter(false);
				}
				return (SwitchParameter)base.Fields["Facebook"];
			}
			set
			{
				base.Fields["Facebook"] = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000080B7 File Offset: 0x000062B7
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000080CE File Offset: 0x000062CE
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "FacebookParameterSet")]
		public string AppAuthorizationCode
		{
			get
			{
				return (string)base.Fields["AppAuthorizationCode"];
			}
			set
			{
				base.Fields["AppAuthorizationCode"] = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000080E1 File Offset: 0x000062E1
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000080F8 File Offset: 0x000062F8
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "FacebookParameterSet")]
		public string RedirectUri
		{
			get
			{
				return (string)base.Fields["RedirectUri"];
			}
			set
			{
				base.Fields["RedirectUri"] = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000810B File Offset: 0x0000630B
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000813B File Offset: 0x0000633B
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "LinkedInParameterSet")]
		public SwitchParameter LinkedIn
		{
			get
			{
				if (base.Fields["LinkedIn"] == null)
				{
					return new SwitchParameter(false);
				}
				return (SwitchParameter)base.Fields["LinkedIn"];
			}
			set
			{
				base.Fields["LinkedIn"] = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00008153 File Offset: 0x00006353
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000816A File Offset: 0x0000636A
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "LinkedInParameterSet")]
		[ValidateNotNullOrEmpty]
		public string RequestToken
		{
			get
			{
				return (string)base.Fields["RequestToken"];
			}
			set
			{
				base.Fields["RequestToken"] = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000817D File Offset: 0x0000637D
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00008194 File Offset: 0x00006394
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "LinkedInParameterSet")]
		[ValidateNotNullOrEmpty]
		public string RequestSecret
		{
			get
			{
				return (string)base.Fields["RequestSecret"];
			}
			set
			{
				base.Fields["RequestSecret"] = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000081A7 File Offset: 0x000063A7
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000081BE File Offset: 0x000063BE
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "LinkedInParameterSet")]
		[ValidateNotNullOrEmpty]
		public string OAuthVerifier
		{
			get
			{
				return (string)base.Fields["OAuthVerifier"];
			}
			set
			{
				base.Fields["OAuthVerifier"] = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000081D1 File Offset: 0x000063D1
		public new string Name
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000081D8 File Offset: 0x000063D8
		public new string DisplayName
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000081DF File Offset: 0x000063DF
		public new SmtpAddress EmailAddress
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000081E8 File Offset: 0x000063E8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string parameterSetName;
				if ((parameterSetName = base.ParameterSetName) != null)
				{
					if (parameterSetName == "FacebookParameterSet")
					{
						return Strings.CreateFacebookSubscriptionConfirmation;
					}
					if (parameterSetName == "LinkedInParameterSet")
					{
						return Strings.CreateLinkedInSubscriptionConfirmation;
					}
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000822C File Offset: 0x0000642C
		protected override AggregationType AggregationType
		{
			get
			{
				return AggregationType.PeopleConnection;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008230 File Offset: 0x00006430
		protected override IConfigDataProvider CreateSession()
		{
			this.InitializeMailboxPrincipal();
			return base.CreateSession();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008240 File Offset: 0x00006440
		protected override IConfigurable PrepareDataObject()
		{
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (!(parameterSetName == "FacebookParameterSet"))
				{
					if (!(parameterSetName == "LinkedInParameterSet"))
					{
						goto IL_40;
					}
					this.providerImpl = new NewLinkedInSubscription();
				}
				else
				{
					this.providerImpl = new NewFacebookSubscription();
				}
				base.Name = this.providerImpl.SubscriptionName;
				base.DisplayName = this.providerImpl.SubscriptionDisplayName;
				base.EmailAddress = SmtpAddress.NullReversePath;
				return this.PrepareSubscription((ConnectSubscriptionProxy)base.PrepareDataObject());
			}
			IL_40:
			throw new InvalidOperationException();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000082D1 File Offset: 0x000064D1
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			this.InitializeFolderAndNotifyApps();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000082DF File Offset: 0x000064DF
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || ConnectSubscriptionTaskKnownExceptions.IsKnown(exception);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000082F4 File Offset: 0x000064F4
		private IConfigurable PrepareSubscription(ConnectSubscriptionProxy subscription)
		{
			subscription.AppAuthorizationCode = this.AppAuthorizationCode;
			subscription.RedirectUri = this.RedirectUri;
			subscription.RequestToken = this.RequestToken;
			subscription.RequestSecret = this.RequestSecret;
			subscription.OAuthVerifier = this.OAuthVerifier;
			IConfigurable result;
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(this.mailboxPrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=NewConnectSubscription"))
			{
				result = this.providerImpl.PrepareSubscription(mailboxSession, subscription);
			}
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008380 File Offset: 0x00006580
		private void InitializeFolderAndNotifyApps()
		{
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(this.mailboxPrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=NewConnectSubscription"))
			{
				this.providerImpl.InitializeFolderAndNotifyApps(mailboxSession, this.DataObject);
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000083D4 File Offset: 0x000065D4
		private void InitializeMailboxPrincipal()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 311, "InitializeMailboxPrincipal", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\NewConnectSubscription.cs");
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorUserNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(this.Mailbox.ToString())));
			ADSessionSettings adSettings = ADSessionSettings.RescopeToOrganization(base.SessionSettings, aduser.OrganizationId, true);
			this.mailboxPrincipal = ExchangePrincipal.FromADUser(adSettings, aduser, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x0400007C RID: 124
		private const string ClientInfoString = "Client=Management;Action=NewConnectSubscription";

		// Token: 0x0400007D RID: 125
		private const string FacebookParameterSet = "FacebookParameterSet";

		// Token: 0x0400007E RID: 126
		private const string LinkedInParameterSet = "LinkedInParameterSet";

		// Token: 0x0400007F RID: 127
		private INewConnectSubscription providerImpl;

		// Token: 0x04000080 RID: 128
		private ExchangePrincipal mailboxPrincipal;
	}
}
