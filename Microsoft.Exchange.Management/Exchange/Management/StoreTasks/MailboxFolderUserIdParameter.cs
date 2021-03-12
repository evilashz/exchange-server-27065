using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000C5D RID: 3165
	[Serializable]
	public class MailboxFolderUserIdParameter : IIdentityParameter
	{
		// Token: 0x06007814 RID: 30740 RVA: 0x001E93C1 File Offset: 0x001E75C1
		internal MailboxFolderUserId ResolveMailboxFolderUserId(MailboxSession mailboxSession)
		{
			if (this.mailboxFolderUserId == null)
			{
				this.mailboxFolderUserId = this.CreateMailboxFolderUserId(mailboxSession);
			}
			if (this.mailboxFolderUserId.UserType == MailboxFolderUserId.MailboxFolderUserType.External)
			{
				this.mailboxFolderUserId.EnsureExternalUser(mailboxSession);
			}
			return this.mailboxFolderUserId;
		}

		// Token: 0x06007815 RID: 30741 RVA: 0x001E93F8 File Offset: 0x001E75F8
		private MailboxFolderUserId CreateMailboxFolderUserId(MailboxSession mailboxSession)
		{
			bool flag = !string.IsNullOrEmpty(this.rawIdentity) && SmtpAddress.IsValidSmtpAddress(this.rawIdentity);
			if (flag)
			{
				MailboxFolderUserId mailboxFolderUserId = MailboxFolderUserId.TryCreateFromSmtpAddress(this.rawIdentity, mailboxSession);
				if (mailboxFolderUserId != null)
				{
					return mailboxFolderUserId;
				}
			}
			IRecipientSession adrecipientSession = mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
			IEnumerable<ADRecipient> objects = this.RecipientIdParameter.GetObjects<ADRecipient>(null, adrecipientSession);
			using (IEnumerator<ADRecipient> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ADRecipient recipient = enumerator.Current;
					if (enumerator.MoveNext())
					{
						throw new ManagementObjectAmbiguousException(Strings.ErrorManagementObjectAmbiguous(this.RecipientIdParameter.ToString()));
					}
					bool allowInvalidSecurityPrincipal = !flag;
					MailboxFolderUserId mailboxFolderUserId = MailboxFolderUserId.TryCreateFromADRecipient(recipient, allowInvalidSecurityPrincipal);
					if (mailboxFolderUserId != null)
					{
						return mailboxFolderUserId;
					}
					if (!flag)
					{
						throw new InvalidInternalUserIdException(this.RecipientIdParameter.ToString());
					}
				}
			}
			if (flag)
			{
				throw new InvalidExternalUserIdException(this.rawIdentity);
			}
			return MailboxFolderUserId.CreateFromUnknownUser(this.rawIdentity);
		}

		// Token: 0x1700251D RID: 9501
		// (get) Token: 0x06007816 RID: 30742 RVA: 0x001E94F4 File Offset: 0x001E76F4
		private RecipientIdParameter RecipientIdParameter
		{
			get
			{
				if (this.recipientIdParameter == null)
				{
					this.recipientIdParameter = RecipientIdParameter.Parse(this.rawIdentity);
				}
				return this.recipientIdParameter;
			}
		}

		// Token: 0x06007817 RID: 30743 RVA: 0x001E9518 File Offset: 0x001E7718
		public MailboxFolderUserIdParameter(string identity)
		{
			if (string.Equals(MailboxFolderUserId.AnonymousUserId.ToString(), identity, StringComparison.InvariantCultureIgnoreCase) || string.Equals("Anonymous", identity, StringComparison.InvariantCultureIgnoreCase))
			{
				this.mailboxFolderUserId = MailboxFolderUserId.AnonymousUserId;
				return;
			}
			if (string.Equals(MailboxFolderUserId.DefaultUserId.ToString(), identity, StringComparison.InvariantCultureIgnoreCase) || string.Equals("Default", identity, StringComparison.InvariantCultureIgnoreCase))
			{
				this.mailboxFolderUserId = MailboxFolderUserId.DefaultUserId;
				return;
			}
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentException("identity");
			}
			this.rawIdentity = identity;
		}

		// Token: 0x06007818 RID: 30744 RVA: 0x001E959F File Offset: 0x001E779F
		public MailboxFolderUserIdParameter()
		{
		}

		// Token: 0x06007819 RID: 30745 RVA: 0x001E95A7 File Offset: 0x001E77A7
		public MailboxFolderUserIdParameter(ADObjectId adObjectId)
		{
			this.recipientIdParameter = new RecipientIdParameter(adObjectId);
		}

		// Token: 0x0600781A RID: 30746 RVA: 0x001E95BB File Offset: 0x001E77BB
		public MailboxFolderUserIdParameter(Mailbox recipient)
		{
			this.recipientIdParameter = new RecipientIdParameter(recipient.Id);
		}

		// Token: 0x0600781B RID: 30747 RVA: 0x001E95D4 File Offset: 0x001E77D4
		public MailboxFolderUserIdParameter(DistributionGroup recipient)
		{
			this.recipientIdParameter = new RecipientIdParameter(recipient.Id);
		}

		// Token: 0x0600781C RID: 30748 RVA: 0x001E95ED File Offset: 0x001E77ED
		public MailboxFolderUserIdParameter(MailUser recipient)
		{
			this.recipientIdParameter = new RecipientIdParameter(recipient.Id);
		}

		// Token: 0x0600781D RID: 30749 RVA: 0x001E9606 File Offset: 0x001E7806
		public MailboxFolderUserIdParameter(MailboxFolderUserId mailboxFolderUserId)
		{
			if (mailboxFolderUserId == null)
			{
				throw new ArgumentNullException("mailboxFolderUserId");
			}
			this.mailboxFolderUserId = mailboxFolderUserId;
		}

		// Token: 0x0600781E RID: 30750 RVA: 0x001E9623 File Offset: 0x001E7823
		public static MailboxFolderUserIdParameter Parse(string identity)
		{
			return new MailboxFolderUserIdParameter(identity);
		}

		// Token: 0x0600781F RID: 30751 RVA: 0x001E962B File Offset: 0x001E782B
		public override string ToString()
		{
			if (this.mailboxFolderUserId != null)
			{
				return this.mailboxFolderUserId.ToString();
			}
			if (this.recipientIdParameter != null)
			{
				return this.recipientIdParameter.ToString();
			}
			return this.rawIdentity;
		}

		// Token: 0x06007820 RID: 30752 RVA: 0x001E965C File Offset: 0x001E785C
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			MailboxFolderUserId mailboxFolderUserId = objectId as MailboxFolderUserId;
			if (mailboxFolderUserId == null)
			{
				ADObjectId adObjectId = objectId as ADObjectId;
				this.recipientIdParameter = new RecipientIdParameter(adObjectId);
				return;
			}
			this.mailboxFolderUserId = mailboxFolderUserId;
		}

		// Token: 0x1700251E RID: 9502
		// (get) Token: 0x06007821 RID: 30753 RVA: 0x001E969C File Offset: 0x001E789C
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x06007822 RID: 30754 RVA: 0x001E96A4 File Offset: 0x001E78A4
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007823 RID: 30755 RVA: 0x001E96AB File Offset: 0x001E78AB
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04003BFB RID: 15355
		private MailboxFolderUserId mailboxFolderUserId;

		// Token: 0x04003BFC RID: 15356
		private RecipientIdParameter recipientIdParameter;

		// Token: 0x04003BFD RID: 15357
		private readonly string rawIdentity;
	}
}
