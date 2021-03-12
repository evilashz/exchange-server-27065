using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxProcessorDefinitions;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxProcessorHelpers
{
	// Token: 0x0200023A RID: 570
	internal static class MailboxTableQueryUtils
	{
		// Token: 0x06001577 RID: 5495 RVA: 0x0007A018 File Offset: 0x00078218
		public static List<MailboxData> GetMailboxTable(PropTag[] requiredMailboxTableProperties, DatabaseInfo databaseInfo)
		{
			PropValue[][] mailboxes;
			try
			{
				mailboxes = MailboxTableQuery.GetMailboxes("Client=TBA", databaseInfo, requiredMailboxTableProperties);
			}
			catch (MapiRetryableException innerException)
			{
				throw new SkipException(innerException);
			}
			catch (MapiPermanentException innerException2)
			{
				throw new SkipException(innerException2);
			}
			List<MailboxData> list = new List<MailboxData>();
			foreach (PropValue[] array2 in mailboxes)
			{
				if (array2 != null)
				{
					PropValue mailboxProperty = MailboxTableQuery.GetMailboxProperty(array2, PropTag.UserGuid);
					PropValue mailboxProperty2 = MailboxTableQuery.GetMailboxProperty(array2, PropTag.MailboxNumber);
					if (mailboxProperty.PropTag.Id() == PropTag.UserGuid.Id() && mailboxProperty2.PropTag.Id() == PropTag.MailboxNumber.Id())
					{
						Guid mailboxGuid = new Guid(mailboxProperty.GetBytes());
						int @int = mailboxProperty2.GetInt();
						list.Add(new MailboxProcessorMailboxData(databaseInfo, mailboxGuid, @int, array2));
					}
				}
			}
			return list;
		}
	}
}
