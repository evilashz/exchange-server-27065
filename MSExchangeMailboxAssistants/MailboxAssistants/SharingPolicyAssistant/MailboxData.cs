using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.SharingPolicyAssistant
{
	// Token: 0x0200015D RID: 349
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MailboxData : IDisposable
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x000556F4 File Offset: 0x000538F4
		internal MailboxData(MailboxSession mailboxSession, SharingPolicy sharingPolicy)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			this.mailboxSession = mailboxSession;
			this.sharingPolicy = sharingPolicy;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0005571F File Offset: 0x0005391F
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00055727 File Offset: 0x00053927
		internal SharingPolicy SharingPolicy
		{
			get
			{
				return this.sharingPolicy;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0005572F File Offset: 0x0005392F
		internal ExternalUserCollection ExternalUserCollection
		{
			get
			{
				if (this.externalUserCollection == null)
				{
					this.externalUserCollection = this.mailboxSession.GetExternalUsers();
				}
				return this.externalUserCollection;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00055750 File Offset: 0x00053950
		internal IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession == null)
				{
					this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.FullyConsistent, (this.mailboxSession.MailboxOwner.MailboxInfo.OrganizationId ?? OrganizationId.ForestWideOrgId).ToADSessionSettings(), 114, "RecipientSession", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\SharingPolicy\\MailboxData.cs");
				}
				return this.recipientSession;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x000557AC File Offset: 0x000539AC
		internal int MaxAnonymousDetailLevel
		{
			get
			{
				if (this.maxAllowedDetailLevel == -1)
				{
					SharingPolicyAction sharingPolicyAction = (SharingPolicyAction)0;
					if (this.sharingPolicy != null)
					{
						sharingPolicyAction = this.sharingPolicy.GetAllowedForAnonymousCalendarSharing();
					}
					if (sharingPolicyAction != (SharingPolicyAction)0)
					{
						this.maxAllowedDetailLevel = PolicyAllowedDetailLevel.GetMaxAllowed(sharingPolicyAction);
					}
					else
					{
						this.maxAllowedDetailLevel = 0;
					}
				}
				return this.maxAllowedDetailLevel;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000557F6 File Offset: 0x000539F6
		public void Dispose()
		{
			if (this.externalUserCollection != null)
			{
				this.externalUserCollection.Dispose();
				this.externalUserCollection = null;
			}
		}

		// Token: 0x04000927 RID: 2343
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000928 RID: 2344
		private readonly SharingPolicy sharingPolicy;

		// Token: 0x04000929 RID: 2345
		private ExternalUserCollection externalUserCollection;

		// Token: 0x0400092A RID: 2346
		private IRecipientSession recipientSession;

		// Token: 0x0400092B RID: 2347
		private int maxAllowedDetailLevel = -1;
	}
}
