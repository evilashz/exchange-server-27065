using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Data.Providers
{
	// Token: 0x0200000A RID: 10
	internal class MailMessageConfigDataProvider : IConfigDataProvider
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000274B File Offset: 0x0000094B
		public MailMessageConfigDataProvider(IRecipientSession adSession, ADUser mailbox)
		{
			this.adSession = adSession;
			this.mailbox = mailbox;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002761 File Offset: 0x00000961
		public void Delete(IConfigurable instance)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002768 File Offset: 0x00000968
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000276F File Offset: 0x0000096F
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002776 File Offset: 0x00000976
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			if (this.mailMessage != null && this.mailMessage.Identity == identity)
			{
				return this.mailMessage;
			}
			return null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002798 File Offset: 0x00000998
		public void Save(IConfigurable instance)
		{
			this.mailMessage = (MailMessage)instance;
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(ExchangePrincipal.FromDirectoryObjectId(this.adSession, this.mailbox.Id, RemotingOptions.LocalConnectionsOnly), currentCulture, "Client=Management;Action=New-MailMessage"))
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts);
				using (MessageItem messageItem = MessageItem.Create(mailboxSession, defaultFolderId))
				{
					if (!string.IsNullOrEmpty(this.mailMessage.Subject))
					{
						messageItem.Subject = this.mailMessage.Subject;
					}
					if (!string.IsNullOrEmpty(this.mailMessage.Body))
					{
						using (TextWriter textWriter = messageItem.Body.OpenTextWriter((BodyFormat)this.mailMessage.BodyFormat))
						{
							textWriter.WriteLine(this.mailMessage.Body);
						}
					}
					messageItem.Save(SaveMode.NoConflictResolution);
					messageItem.Load();
					this.mailMessage.SetIdentity(messageItem.Id.ObjectId);
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000028BC File Offset: 0x00000ABC
		public string Source
		{
			get
			{
				if (this.adSession != null)
				{
					return this.adSession.Source;
				}
				return null;
			}
		}

		// Token: 0x04000018 RID: 24
		private IRecipientSession adSession;

		// Token: 0x04000019 RID: 25
		private ADUser mailbox;

		// Token: 0x0400001A RID: 26
		private MailMessage mailMessage;
	}
}
