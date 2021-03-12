using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200008C RID: 140
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CrossServerArchiveProcessor : RemoteArchiveProcessorBase
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00028B54 File Offset: 0x00026D54
		public override int MaxMessageSizeInArchive
		{
			get
			{
				if (this.maxMessageSizeInArchive == null)
				{
					object obj = this.primaryMailboxSession.Mailbox.TryGetProperty(MailboxSchema.MaxMessageSize);
					if (obj is int)
					{
						this.maxMessageSizeInArchive = new int?((int)obj * 1024);
						RemoteArchiveProcessorBase.Tracer.TraceDebug<IExchangePrincipal, int>((long)this.GetHashCode(), "{0}: The MaxMessageSize for this mailbox is {1}", this.primaryMailboxSession.MailboxOwner, this.maxMessageSizeInArchive.Value);
						if (this.maxMessageSizeInArchive > 36700160)
						{
							this.maxMessageSizeInArchive = new int?(36700160);
							RemoteArchiveProcessorBase.Tracer.TraceDebug<IExchangePrincipal, int>((long)this.GetHashCode(), "{0}: The MaxMessageSize for this mailbox is greater than EWS request limit of {1}.", this.primaryMailboxSession.MailboxOwner, 36700160);
						}
					}
					else
					{
						this.maxMessageSizeInArchive = new int?(36700160);
						RemoteArchiveProcessorBase.Tracer.TraceError<IExchangePrincipal>((long)this.GetHashCode(), "{0}: The property MaxMessageSize is not available of this mailbox.", this.primaryMailboxSession.MailboxOwner);
					}
				}
				return this.maxMessageSizeInArchive.Value;
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00028C6A File Offset: 0x00026E6A
		public CrossServerArchiveProcessor(MailboxSession mailboxSession, ADUser user, StatisticsLogEntry statisticsLogEntry, bool isTestMode) : base(mailboxSession, user, statisticsLogEntry, false, isTestMode)
		{
		}

		// Token: 0x040003FE RID: 1022
		private int? maxMessageSizeInArchive;
	}
}
