using System;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000207 RID: 519
	public sealed class OwaWindowsIdentity : OwaIdentity
	{
		// Token: 0x06001184 RID: 4484 RVA: 0x00069EE3 File Offset: 0x000680E3
		private OwaWindowsIdentity(WindowsIdentity windowsIdentity)
		{
			this.windowsIdentity = new WindowsIdentity(windowsIdentity.Token);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00069EFC File Offset: 0x000680FC
		public static OwaWindowsIdentity CreateFromWindowsIdentity(WindowsIdentity windowsIdentity)
		{
			return new OwaWindowsIdentity(windowsIdentity);
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00069F04 File Offset: 0x00068104
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

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00069F2D File Offset: 0x0006812D
		public override WindowsIdentity WindowsIdentity
		{
			get
			{
				return this.windowsIdentity;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00069F35 File Offset: 0x00068135
		public override SecurityIdentifier UserSid
		{
			get
			{
				return this.windowsIdentity.User;
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00069F44 File Offset: 0x00068144
		public override string GetLogonName()
		{
			string name;
			try
			{
				name = this.windowsIdentity.Name;
			}
			catch (SystemException innerException)
			{
				throw new OwaIdentityException("Failed to retrieve user name", innerException);
			}
			return name;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00069F80 File Offset: 0x00068180
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

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00069FB4 File Offset: 0x000681B4
		public override string UniqueId
		{
			get
			{
				return this.UserSid.ToString();
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00069FC1 File Offset: 0x000681C1
		public override string AuthenticationType
		{
			get
			{
				return this.windowsIdentity.AuthenticationType;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00069FCE File Offset: 0x000681CE
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

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00069FEF File Offset: 0x000681EF
		public override bool IsPartial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00069FF2 File Offset: 0x000681F2
		internal override ExchangePrincipal InternalCreateExchangePrincipal()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaWindowsIdentity.CreateExchangePrincipal");
			return ExchangePrincipal.FromMiniRecipient(base.GetOWAMiniRecipient());
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0006A010 File Offset: 0x00068210
		internal override MailboxSession CreateMailboxSession(IExchangePrincipal exchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaWindowsIdentity.CreateMailboxSession");
			MailboxSession result;
			try
			{
				MailboxSession mailboxSession = MailboxSession.Open(exchangePrincipal, this.WindowsPrincipal, cultureInfo, "Client=OWA");
				GccUtils.SetStoreSessionClientIPEndpointsFromHttpRequest(mailboxSession, clientRequest);
				result = mailboxSession;
			}
			catch (AccessDeniedException innerException)
			{
				throw new OwaExplicitLogonException("User has no access rights to the mailbox", LocalizedStrings.GetNonEncoded(882888134), innerException);
			}
			return result;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0006A078 File Offset: 0x00068278
		internal override MailboxSession CreateWebPartMailboxSession(IExchangePrincipal mailBoxExchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest)
		{
			MailboxSession result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MailboxSession mailboxSession = MailboxSession.OpenWithBestAccess(mailBoxExchangePrincipal, base.CreateADOrgPersonForWebPartUserBySid(), this.WindowsPrincipal, cultureInfo, "Client=OWA;Action=WebPart + Delegate");
				disposeGuard.Add<MailboxSession>(mailboxSession);
				GccUtils.SetStoreSessionClientIPEndpointsFromHttpRequest(mailboxSession, clientRequest);
				disposeGuard.Success();
				result = mailboxSession;
			}
			return result;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0006A0E4 File Offset: 0x000682E4
		internal override UncSession CreateUncSession(DocumentLibraryObjectId objectId)
		{
			return UncSession.Open(objectId, this.WindowsPrincipal);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0006A0F2 File Offset: 0x000682F2
		internal override SharepointSession CreateSharepointSession(DocumentLibraryObjectId objectId)
		{
			return SharepointSession.Open(objectId, this.WindowsPrincipal);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0006A100 File Offset: 0x00068300
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

		// Token: 0x06001195 RID: 4501 RVA: 0x0006A15A File Offset: 0x0006835A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaWindowsIdentity>(this);
		}

		// Token: 0x04000BD7 RID: 3031
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x04000BD8 RID: 3032
		private WindowsIdentity windowsIdentity;

		// Token: 0x04000BD9 RID: 3033
		private bool isDisposed;

		// Token: 0x04000BDA RID: 3034
		private WindowsPrincipal windowsPrincipal;
	}
}
