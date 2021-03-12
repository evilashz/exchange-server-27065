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
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000035 RID: 53
	[Cmdlet("Set", "ConnectSubscription", SupportsShouldProcess = true, DefaultParameterSetName = "FacebookParameterSet")]
	public sealed class SetConnectSubscription : SetSubscriptionBase<ConnectSubscriptionProxy>
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000A6C1 File Offset: 0x000088C1
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000A6C9 File Offset: 0x000088C9
		[ValidateNotNull]
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		public new AggregationSubscriptionIdParameter Identity
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000A6D2 File Offset: 0x000088D2
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000A702 File Offset: 0x00008902
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000A71A File Offset: 0x0000891A
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000A731 File Offset: 0x00008931
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "FacebookParameterSet")]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000A744 File Offset: 0x00008944
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000A75B File Offset: 0x0000895B
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000A76E File Offset: 0x0000896E
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000A79E File Offset: 0x0000899E
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000A7B6 File Offset: 0x000089B6
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000A7CD File Offset: 0x000089CD
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A7E0 File Offset: 0x000089E0
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000A7F7 File Offset: 0x000089F7
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = false, ParameterSetName = "LinkedInParameterSet")]
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000A80A File Offset: 0x00008A0A
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000A821 File Offset: 0x00008A21
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A834 File Offset: 0x00008A34
		public new string DisplayName
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000A83C File Offset: 0x00008A3C
		protected override AggregationSubscriptionType IdentityType
		{
			get
			{
				string parameterSetName;
				if ((parameterSetName = base.ParameterSetName) != null)
				{
					if (parameterSetName == "FacebookParameterSet")
					{
						return AggregationSubscriptionType.Facebook;
					}
					if (parameterSetName == "LinkedInParameterSet")
					{
						return AggregationSubscriptionType.LinkedIn;
					}
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A87C File Offset: 0x00008A7C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string parameterSetName;
				if ((parameterSetName = base.ParameterSetName) != null)
				{
					if (parameterSetName == "FacebookParameterSet")
					{
						return Strings.SetFacebookSubscriptionConfirmation;
					}
					if (parameterSetName == "LinkedInParameterSet")
					{
						return Strings.SetLinkedInSubscriptionConfirmation;
					}
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		protected override AggregationType AggregationType
		{
			get
			{
				return AggregationType.PeopleConnection;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider result = base.CreateSession();
			this.InitializeMailboxPrincipal();
			return result;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		private void InitializeMailboxPrincipal()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 221, "InitializeMailboxPrincipal", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\SetConnectSubscription.cs");
			ADUser user = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorUserNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(this.Mailbox.ToString())));
			this.mailboxPrincipal = ExchangePrincipal.FromADUser(tenantOrRootOrgRecipientSession.SessionSettings, user, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A96C File Offset: 0x00008B6C
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			base.StampChangesOn(dataObject);
			ConnectSubscriptionProxy connectSubscriptionProxy = dataObject as ConnectSubscriptionProxy;
			if (connectSubscriptionProxy == null)
			{
				return;
			}
			if (base.Fields.IsModified("AppAuthorizationCode"))
			{
				connectSubscriptionProxy.AppAuthorizationCode = this.AppAuthorizationCode;
			}
			if (base.Fields.IsModified("RedirectUri"))
			{
				connectSubscriptionProxy.RedirectUri = this.RedirectUri;
			}
			if (base.Fields.IsModified("RequestToken"))
			{
				connectSubscriptionProxy.RequestToken = this.RequestToken;
			}
			if (base.Fields.IsModified("RequestSecret"))
			{
				connectSubscriptionProxy.RequestSecret = this.RequestSecret;
			}
			if (base.Fields.IsModified("OAuthVerifier"))
			{
				connectSubscriptionProxy.OAuthVerifier = this.OAuthVerifier;
			}
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (!(parameterSetName == "FacebookParameterSet"))
				{
					if (!(parameterSetName == "LinkedInParameterSet"))
					{
						goto IL_ED;
					}
					this.providerImpl = new SetLinkedInSubscription();
				}
				else
				{
					this.providerImpl = new SetFacebookSubscription();
				}
				this.providerImpl.StampChangesOn(connectSubscriptionProxy);
				TaskLogger.LogExit();
				return;
			}
			IL_ED:
			throw new InvalidOperationException();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000AA7D File Offset: 0x00008C7D
		protected override bool SendAsCheckNeeded()
		{
			return false;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000AA80 File Offset: 0x00008C80
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || ConnectSubscriptionTaskKnownExceptions.IsKnown(exception);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000AA93 File Offset: 0x00008C93
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			this.NotifyApps();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		private void NotifyApps()
		{
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(this.mailboxPrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=SetConnectSubscription"))
			{
				this.providerImpl.NotifyApps(mailboxSession);
			}
		}

		// Token: 0x04000096 RID: 150
		private const string ClientInfoString = "Client=Management;Action=SetConnectSubscription";

		// Token: 0x04000097 RID: 151
		private const string FacebookParameterSet = "FacebookParameterSet";

		// Token: 0x04000098 RID: 152
		private const string LinkedInParameterSet = "LinkedInParameterSet";

		// Token: 0x04000099 RID: 153
		private ISetConnectSubscription providerImpl;

		// Token: 0x0400009A RID: 154
		private ExchangePrincipal mailboxPrincipal;
	}
}
