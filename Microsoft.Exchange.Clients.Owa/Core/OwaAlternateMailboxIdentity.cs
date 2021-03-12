using System;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000175 RID: 373
	public sealed class OwaAlternateMailboxIdentity : OwaIdentity
	{
		// Token: 0x06000D35 RID: 3381 RVA: 0x00058C32 File Offset: 0x00056E32
		private OwaAlternateMailboxIdentity(OwaIdentity logonIdentity, ExchangePrincipal logonExchangePrincipal, Guid aggregatedMailboxGuid)
		{
			this.logonIdentity = logonIdentity;
			this.logonExchangePrincipal = logonExchangePrincipal;
			this.aggregatedMailboxGuid = aggregatedMailboxGuid;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00058C4F File Offset: 0x00056E4F
		public override WindowsIdentity WindowsIdentity
		{
			get
			{
				return this.logonIdentity.WindowsIdentity;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00058C5C File Offset: 0x00056E5C
		public override SecurityIdentifier UserSid
		{
			get
			{
				return this.logonIdentity.UserSid;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00058C69 File Offset: 0x00056E69
		public override string AuthenticationType
		{
			get
			{
				return this.logonIdentity.AuthenticationType;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00058C78 File Offset: 0x00056E78
		public override string UniqueId
		{
			get
			{
				return this.aggregatedMailboxGuid.ToString();
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00058C99 File Offset: 0x00056E99
		public override bool IsPartial
		{
			get
			{
				return this.logonIdentity.IsPartial;
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00058CA6 File Offset: 0x00056EA6
		public override string GetLogonName()
		{
			return this.logonIdentity.GetLogonName();
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00058CB3 File Offset: 0x00056EB3
		public override string SafeGetRenderableName()
		{
			return this.logonExchangePrincipal.MailboxInfo.DisplayName;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00058CC8 File Offset: 0x00056EC8
		internal static Guid? GetAlternateMailbox(IExchangePrincipal exchangePrincipal, string smtpAddress)
		{
			return null;
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00058CDE File Offset: 0x00056EDE
		internal override ClientSecurityContext ClientSecurityContext
		{
			get
			{
				return this.logonIdentity.ClientSecurityContext;
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00058CEB File Offset: 0x00056EEB
		internal static OwaAlternateMailboxIdentity Create(OwaIdentity logonIdentity, ExchangePrincipal logonExchangePrincipal, Guid aggregatedMailboxGuid)
		{
			return new OwaAlternateMailboxIdentity(logonIdentity, logonExchangePrincipal, aggregatedMailboxGuid);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00058CF5 File Offset: 0x00056EF5
		internal override ExchangePrincipal InternalCreateExchangePrincipal()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaAlternateMailboxIdentity.CreateExchangePrincipal");
			return this.logonExchangePrincipal.GetAggregatedExchangePrincipal(this.aggregatedMailboxGuid);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00058D19 File Offset: 0x00056F19
		internal override MailboxSession CreateMailboxSession(IExchangePrincipal exchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest)
		{
			return this.logonIdentity.CreateMailboxSession(exchangePrincipal, cultureInfo, clientRequest);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00058D29 File Offset: 0x00056F29
		internal override MailboxSession CreateWebPartMailboxSession(IExchangePrincipal mailBoxExchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest)
		{
			return this.logonIdentity.CreateWebPartMailboxSession(mailBoxExchangePrincipal, cultureInfo, clientRequest);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00058D39 File Offset: 0x00056F39
		internal override UncSession CreateUncSession(DocumentLibraryObjectId objectId)
		{
			base.ThrowNotSupported("CreateUncSession");
			return null;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00058D47 File Offset: 0x00056F47
		internal override SharepointSession CreateSharepointSession(DocumentLibraryObjectId objectId)
		{
			base.ThrowNotSupported("CreateSharepointSession");
			return null;
		}

		// Token: 0x0400091F RID: 2335
		private readonly Guid aggregatedMailboxGuid;

		// Token: 0x04000920 RID: 2336
		private readonly OwaIdentity logonIdentity;

		// Token: 0x04000921 RID: 2337
		private readonly ExchangePrincipal logonExchangePrincipal;
	}
}
