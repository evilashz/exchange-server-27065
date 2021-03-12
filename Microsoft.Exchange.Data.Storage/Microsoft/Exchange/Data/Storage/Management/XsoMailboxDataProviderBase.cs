using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A08 RID: 2568
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class XsoMailboxDataProviderBase : XsoStoreDataProviderBase
	{
		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x0018E24D File Offset: 0x0018C44D
		// (set) Token: 0x06005E45 RID: 24133 RVA: 0x0018E25A File Offset: 0x0018C45A
		public MailboxSession MailboxSession
		{
			get
			{
				return (MailboxSession)base.StoreSession;
			}
			private set
			{
				base.StoreSession = value;
			}
		}

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x0018E263 File Offset: 0x0018C463
		// (set) Token: 0x06005E47 RID: 24135 RVA: 0x0018E26B File Offset: 0x0018C46B
		public ADUser MailboxOwner { get; protected set; }

		// Token: 0x06005E48 RID: 24136 RVA: 0x0018E274 File Offset: 0x0018C474
		public XsoMailboxDataProviderBase(ADSessionSettings adSessionSettings, ADUser mailboxOwner, string action) : this(ExchangePrincipal.FromADUser(adSessionSettings, mailboxOwner, RemotingOptions.AllowCrossSite), action)
		{
			this.MailboxOwner = mailboxOwner;
		}

		// Token: 0x06005E49 RID: 24137 RVA: 0x0018E28C File Offset: 0x0018C48C
		public XsoMailboxDataProviderBase(ADSessionSettings adSessionSettings, ADUser mailboxOwner, ISecurityAccessToken userToken, string action) : this(XsoStoreDataProviderBase.GetExchangePrincipalWithAdSessionSettingsForOrg(adSessionSettings.CurrentOrganizationId, mailboxOwner), userToken, action)
		{
			this.MailboxOwner = mailboxOwner;
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x0018E2AC File Offset: 0x0018C4AC
		public XsoMailboxDataProviderBase(ExchangePrincipal mailboxOwner, string action)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				Util.ThrowOnNullArgument(mailboxOwner, "mailboxOwner");
				Util.ThrowOnNullOrEmptyArgument(action, "action");
				this.MailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, string.Format("Client=Management;Action={0};Privilege:ActAsAdmin", action));
				disposeGuard.Success();
			}
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x0018E320 File Offset: 0x0018C520
		protected XsoMailboxDataProviderBase(ExchangePrincipal mailboxOwner, ISecurityAccessToken userToken, string action)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				Util.ThrowOnNullArgument(mailboxOwner, "mailboxOwner");
				Util.ThrowOnNullOrEmptyArgument(action, "action");
				if (userToken == null)
				{
					this.MailboxSession = MailboxSession.Open(mailboxOwner, new WindowsPrincipal(WindowsIdentity.GetCurrent()), CultureInfo.InvariantCulture, string.Format("Client=Management;Action={0}", action));
				}
				else
				{
					try
					{
						using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(userToken, AuthzFlags.AuthzSkipTokenGroups))
						{
							clientSecurityContext.SetSecurityAccessToken(userToken);
							this.MailboxSession = MailboxSession.Open(mailboxOwner, clientSecurityContext, CultureInfo.InvariantCulture, string.Format("Client=Management;Action={0}", action));
						}
					}
					catch (AuthzException ex)
					{
						throw new AccessDeniedException(new LocalizedString(ex.Message));
					}
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x0018E408 File Offset: 0x0018C608
		public XsoMailboxDataProviderBase(MailboxSession session) : base(session)
		{
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x0018E414 File Offset: 0x0018C614
		public XsoMailboxDataProviderBase()
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				string stackTrace = Environment.StackTrace;
				if (!stackTrace.Contains("Internal.Exchange.Management.OWAOptionTasks.XsoDriverUnitTest") && !stackTrace.Contains("Internal.Exchange.Test.Data.Storage.DisposeSuite") && !stackTrace.Contains("Internal.Exchange.Migration.MigrationUnitTests"))
				{
					throw new InvalidOperationException(string.Format("The default constructor is used only to help test code right now. Current stack trace is: {0}", stackTrace));
				}
				disposeGuard.Success();
			}
		}
	}
}
