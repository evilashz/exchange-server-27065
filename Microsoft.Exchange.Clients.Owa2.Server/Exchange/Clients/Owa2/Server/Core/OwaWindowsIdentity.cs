using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000A7 RID: 167
	public sealed class OwaWindowsIdentity : OwaIdentity
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x00014694 File Offset: 0x00012894
		private OwaWindowsIdentity(WindowsIdentity windowsIdentity)
		{
			this.windowsIdentity = new WindowsIdentity(windowsIdentity.Token);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000146AD File Offset: 0x000128AD
		public WindowsPrincipal WindowsPrincipal
		{
			get
			{
				if (this.windowsPrincipal == null && this.WindowsIdentity != null)
				{
					this.windowsPrincipal = new WindowsPrincipal(this.WindowsIdentity);
				}
				return this.windowsPrincipal;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x000146D6 File Offset: 0x000128D6
		public override WindowsIdentity WindowsIdentity
		{
			get
			{
				return this.windowsIdentity;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000146DE File Offset: 0x000128DE
		public override SecurityIdentifier UserSid
		{
			get
			{
				return this.windowsIdentity.User;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x000146EB File Offset: 0x000128EB
		public override string UniqueId
		{
			get
			{
				return this.UserSid.ToString();
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000146F8 File Offset: 0x000128F8
		public override string AuthenticationType
		{
			get
			{
				return this.windowsIdentity.AuthenticationType;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00014705 File Offset: 0x00012905
		public override bool IsPartial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00014708 File Offset: 0x00012908
		internal override ClientSecurityContext ClientSecurityContext
		{
			get
			{
				if (this.clientSecurityContext == null)
				{
					this.clientSecurityContext = new ClientSecurityContext(this.windowsIdentity);
				}
				return this.clientSecurityContext;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00014729 File Offset: 0x00012929
		public static OwaWindowsIdentity CreateFromWindowsIdentity(WindowsIdentity windowsIdentity)
		{
			return new OwaWindowsIdentity(windowsIdentity);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00014734 File Offset: 0x00012934
		public override string GetLogonName()
		{
			string result = string.Empty;
			try
			{
				result = this.windowsIdentity.Name;
			}
			catch (SystemException innerException)
			{
				throw new OwaIdentityException("Failed to retrieve user name", innerException);
			}
			return result;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00014774 File Offset: 0x00012974
		public override string SafeGetRenderableName()
		{
			string result = null;
			try
			{
				result = this.GetLogonName();
			}
			catch (OwaIdentityException)
			{
				result = this.UniqueId;
			}
			return result;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000147A8 File Offset: 0x000129A8
		internal override ExchangePrincipal InternalCreateExchangePrincipal()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaWindowsIdentity.CreateExchangePrincipal");
			return ExchangePrincipal.FromMiniRecipient(base.GetOWAMiniRecipient());
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000147C6 File Offset: 0x000129C6
		internal override MailboxSession CreateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return this.CreateMailboxSession(exchangePrincipal, cultureInfo, "Client=OWA");
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000147D5 File Offset: 0x000129D5
		internal override MailboxSession CreateInstantSearchMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return this.CreateMailboxSession(exchangePrincipal, cultureInfo, "Client=OWA;Action=InstantSearch");
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000147E4 File Offset: 0x000129E4
		internal MailboxSession CreateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo, string userContextString)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaWindowsIdentity.CreateMailboxSession");
			MailboxSession result;
			try
			{
				MailboxSession mailboxSession = MailboxSession.OpenAsTransport(exchangePrincipal, userContextString);
				result = mailboxSession;
			}
			catch (AccessDeniedException innerException)
			{
				throw new OwaExplicitLogonException("User has no access rights to the mailbox", "ErrorExplicitLogonAccessDenied", innerException);
			}
			return result;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00014834 File Offset: 0x00012A34
		internal override MailboxSession CreateDelegateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return null;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00014838 File Offset: 0x00012A38
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && !this.isDisposed)
			{
				if (this.clientSecurityContext != null)
				{
					this.clientSecurityContext.Dispose();
					this.clientSecurityContext = null;
					if (this.windowsIdentity != null)
					{
						this.windowsIdentity.Dispose();
						this.windowsIdentity = null;
					}
				}
				this.isDisposed = true;
			}
			base.InternalDispose(isDisposing);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00014892 File Offset: 0x00012A92
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaWindowsIdentity>(this);
		}

		// Token: 0x040003A9 RID: 937
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x040003AA RID: 938
		private WindowsIdentity windowsIdentity;

		// Token: 0x040003AB RID: 939
		private bool isDisposed;

		// Token: 0x040003AC RID: 940
		private WindowsPrincipal windowsPrincipal;
	}
}
