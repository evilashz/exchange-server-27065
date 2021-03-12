using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxDataHelper
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0000BB34 File Offset: 0x00009D34
		internal static PropertyDefinition[] GetPropertyDefinitions(MigrationType migrationType)
		{
			if (migrationType == MigrationType.XO1)
			{
				return ConsumerMailboxData.ConsumerMailboxDataPropertyDefinition;
			}
			return MailboxData.MailboxDataPropertyDefinition;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000BB54 File Offset: 0x00009D54
		internal static IMailboxData CreateFromMessage(IMigrationStoreObject message, MigrationType migrationType)
		{
			IMailboxData mailboxData;
			if (migrationType == MigrationType.XO1)
			{
				mailboxData = new ConsumerMailboxData();
			}
			else
			{
				mailboxData = new MailboxData();
			}
			if (!mailboxData.ReadFromMessageItem(message))
			{
				return null;
			}
			return mailboxData;
		}
	}
}
