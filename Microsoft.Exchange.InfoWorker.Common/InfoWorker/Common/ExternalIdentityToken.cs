using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200001D RID: 29
	internal class ExternalIdentityToken : ISecurityAccessToken
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000399C File Offset: 0x00001B9C
		internal static ExternalIdentityToken GetExternalIdentityToken(MailboxSession session, SmtpAddress externalId)
		{
			if (session != null && session.Capabilities.CanHaveExternalUsers)
			{
				using (ExternalUserCollection externalUsers = session.GetExternalUsers())
				{
					ExternalUser externalUser = externalUsers.FindExternalUser(externalId.ToString());
					if (externalUser != null)
					{
						return new ExternalIdentityToken(externalUser.Sid);
					}
					ExternalIdentityToken.Tracer.TraceError<SmtpAddress, IExchangePrincipal>(0L, "{0}: Unable to find the requester in the external user collection in mailbox {1}.", externalId, session.MailboxOwner);
				}
			}
			return null;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003A1C File Offset: 0x00001C1C
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003A29 File Offset: 0x00001C29
		public string UserSid
		{
			get
			{
				return this.sid.ToString();
			}
			set
			{
				throw new InvalidOperationException("UsedSid");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003A35 File Offset: 0x00001C35
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003A3C File Offset: 0x00001C3C
		public SidStringAndAttributes[] GroupSids
		{
			get
			{
				return ExternalIdentityToken.groupSidStringAndAttributesArray;
			}
			set
			{
				throw new InvalidOperationException("GroupSids");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003A48 File Offset: 0x00001C48
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003A4F File Offset: 0x00001C4F
		public SidStringAndAttributes[] RestrictedGroupSids
		{
			get
			{
				return ExternalIdentityToken.emptySidStringAndAttributesArray;
			}
			set
			{
				throw new InvalidOperationException("GroupSids");
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003A5B File Offset: 0x00001C5B
		private ExternalIdentityToken(SecurityIdentifier sid)
		{
			this.sid = sid;
		}

		// Token: 0x0400003C RID: 60
		private static readonly SidStringAndAttributes[] emptySidStringAndAttributesArray = new SidStringAndAttributes[0];

		// Token: 0x0400003D RID: 61
		private static readonly SidStringAndAttributes[] groupSidStringAndAttributesArray = new SidStringAndAttributes[]
		{
			new SidStringAndAttributes("S-1-1-0", 0U)
		};

		// Token: 0x0400003E RID: 62
		private SecurityIdentifier sid;

		// Token: 0x0400003F RID: 63
		private static readonly Trace Tracer = ExTraceGlobals.SecurityTracer;
	}
}
