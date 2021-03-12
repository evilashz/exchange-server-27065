using System;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200017A RID: 378
	public class OwaClientSecurityContextIdentity : OwaIdentity
	{
		// Token: 0x06000D57 RID: 3415 RVA: 0x00059508 File Offset: 0x00057708
		private OwaClientSecurityContextIdentity(ClientSecurityContext clientSecurityContext, string logonName, string authenticationType)
		{
			this.clientSecurityContext = clientSecurityContext;
			this.logonName = logonName;
			this.authenticationType = authenticationType;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00059525 File Offset: 0x00057725
		protected OwaClientSecurityContextIdentity(SecurityIdentifier userSid)
		{
			this.userSid = userSid;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00059534 File Offset: 0x00057734
		internal static OwaClientSecurityContextIdentity CreateFromClientSecurityContextIdentity(ClientSecurityContextIdentity cscIdentity)
		{
			ClientSecurityContext clientSecurityContext = cscIdentity.CreateClientSecurityContext();
			return new OwaClientSecurityContextIdentity(clientSecurityContext, cscIdentity.Name, cscIdentity.AuthenticationType);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0005955A File Offset: 0x0005775A
		internal static OwaClientSecurityContextIdentity CreateFromClientSecurityContext(ClientSecurityContext clientSecurityContext, string logonName, string authenticationType)
		{
			return new OwaClientSecurityContextIdentity(clientSecurityContext, logonName, authenticationType);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00059564 File Offset: 0x00057764
		public static OwaClientSecurityContextIdentity CreateFromSecurityIdentifier(SecurityIdentifier userSid)
		{
			return new OwaClientSecurityContextIdentity(userSid);
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0005956C File Offset: 0x0005776C
		public override bool IsPartial
		{
			get
			{
				return this.clientSecurityContext == null;
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00059578 File Offset: 0x00057778
		internal void UpgradePartialIdentity(ClientSecurityContext clientSecurityContext, string logonName, string authenticationType)
		{
			this.clientSecurityContext = clientSecurityContext;
			this.logonName = logonName;
			this.authenticationType = authenticationType;
			if (this.clientSecurityContext.UserSid != this.userSid)
			{
				throw new OwaInvalidOperationException("Can't upgrade a partial identity to a full identity that doesn't correspond to the same user sid");
			}
			this.userSid = null;
			this.owaMiniRecipient = null;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x000595CB File Offset: 0x000577CB
		public override WindowsIdentity WindowsIdentity
		{
			get
			{
				base.ThrowNotSupported("WindowsIdentity");
				return null;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000595D9 File Offset: 0x000577D9
		public override SecurityIdentifier UserSid
		{
			get
			{
				if (this.IsPartial)
				{
					return this.userSid;
				}
				return this.clientSecurityContext.UserSid;
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x000595F5 File Offset: 0x000577F5
		public override string GetLogonName()
		{
			return this.logonName;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000595FD File Offset: 0x000577FD
		public override string SafeGetRenderableName()
		{
			if (!this.IsPartial)
			{
				return this.logonName;
			}
			return this.userSid.ToString();
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00059619 File Offset: 0x00057819
		public override string UniqueId
		{
			get
			{
				return this.UserSid.ToString();
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00059626 File Offset: 0x00057826
		public override string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0005962E File Offset: 0x0005782E
		internal override ClientSecurityContext ClientSecurityContext
		{
			get
			{
				return this.clientSecurityContext;
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00059638 File Offset: 0x00057838
		internal override ExchangePrincipal InternalCreateExchangePrincipal()
		{
			ADSessionSettings adSettings = Utilities.CreateScopedADSessionSettings(this.DomainName);
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaWindowsIdentity.CreateExchangePrincipal");
			return ExchangePrincipal.FromUserSid(adSettings, this.UserSid, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00059670 File Offset: 0x00057870
		internal override MailboxSession CreateMailboxSession(IExchangePrincipal exchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaClientSecurityContextIdentity.CreateMailboxSession");
			MailboxSession result;
			try
			{
				MailboxSession mailboxSession = MailboxSession.Open(exchangePrincipal, this.clientSecurityContext, cultureInfo, "Client=OWA;Action=ViaProxy");
				GccUtils.SetStoreSessionClientIPEndpointsFromHttpRequest(mailboxSession, clientRequest);
				result = mailboxSession;
			}
			catch (AccessDeniedException innerException)
			{
				throw new OwaExplicitLogonException("User has no access rights to the mailbox", LocalizedStrings.GetNonEncoded(882888134), innerException);
			}
			return result;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000596D8 File Offset: 0x000578D8
		internal override MailboxSession CreateWebPartMailboxSession(IExchangePrincipal mailBoxExchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest)
		{
			MailboxSession mailboxSession = MailboxSession.OpenWithBestAccess(mailBoxExchangePrincipal, base.CreateADOrgPersonForWebPartUserBySid(), this.clientSecurityContext, cultureInfo, "Client=OWA;Action=WebPart + Delegate + ViaProxy");
			GccUtils.SetStoreSessionClientIPEndpointsFromHttpRequest(mailboxSession, clientRequest);
			return mailboxSession;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00059707 File Offset: 0x00057907
		internal override UncSession CreateUncSession(DocumentLibraryObjectId objectId)
		{
			base.ThrowNotSupported("CreateUncSession");
			return null;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00059715 File Offset: 0x00057915
		internal override SharepointSession CreateSharepointSession(DocumentLibraryObjectId objectId)
		{
			base.ThrowNotSupported("CreateSharepointSession");
			return null;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00059723 File Offset: 0x00057923
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.clientSecurityContext != null)
			{
				this.clientSecurityContext.Dispose();
				this.clientSecurityContext = null;
			}
			base.InternalDispose(isDisposing);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00059749 File Offset: 0x00057949
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaClientSecurityContextIdentity>(this);
		}

		// Token: 0x0400092C RID: 2348
		private SecurityIdentifier userSid;

		// Token: 0x0400092D RID: 2349
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x0400092E RID: 2350
		private string logonName;

		// Token: 0x0400092F RID: 2351
		private string authenticationType;
	}
}
