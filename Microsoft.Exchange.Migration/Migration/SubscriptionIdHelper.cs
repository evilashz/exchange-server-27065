using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000142 RID: 322
	internal static class SubscriptionIdHelper
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x00044B5C File Offset: 0x00042D5C
		internal static ISubscriptionId CreateFromMessage(IMigrationStoreObject message, MigrationType migrationType, IMailboxData mailboxData, bool isPAW)
		{
			ISubscriptionId subscriptionId = SubscriptionIdHelper.Create(migrationType, mailboxData, isPAW);
			if (subscriptionId != null && subscriptionId.ReadFromMessageItem(message))
			{
				return subscriptionId;
			}
			return null;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00044B84 File Offset: 0x00042D84
		internal static PropertyDefinition[] GetPropertyDefinitions(MigrationType migrationType, bool isPAW)
		{
			if (migrationType <= MigrationType.ExchangeRemoteMove)
			{
				if (migrationType != MigrationType.IMAP)
				{
					if (migrationType != MigrationType.ExchangeOutlookAnywhere && migrationType != MigrationType.ExchangeRemoteMove)
					{
						goto IL_3D;
					}
				}
				else
				{
					if (!isPAW)
					{
						return SyncSubscriptionId.SyncSubscriptionIdPropertyDefinitions;
					}
					return MRSSubscriptionId.MRSSubscriptionIdPropertyDefinitions;
				}
			}
			else if (migrationType != MigrationType.ExchangeLocalMove && migrationType != MigrationType.PSTImport && migrationType != MigrationType.PublicFolder)
			{
				goto IL_3D;
			}
			return MRSSubscriptionId.MRSSubscriptionIdPropertyDefinitions;
			IL_3D:
			return null;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00044BD0 File Offset: 0x00042DD0
		internal static ISubscriptionId Create(MigrationType migrationType, IMailboxData mailboxData, bool isPAW)
		{
			if (migrationType <= MigrationType.ExchangeRemoteMove)
			{
				if (migrationType != MigrationType.IMAP)
				{
					if (migrationType != MigrationType.ExchangeOutlookAnywhere && migrationType != MigrationType.ExchangeRemoteMove)
					{
						goto IL_44;
					}
				}
				else
				{
					if (!isPAW)
					{
						return new SyncSubscriptionId(mailboxData);
					}
					return new MRSSubscriptionId(migrationType, mailboxData);
				}
			}
			else if (migrationType != MigrationType.ExchangeLocalMove && migrationType != MigrationType.PSTImport && migrationType != MigrationType.PublicFolder)
			{
				goto IL_44;
			}
			return new MRSSubscriptionId(migrationType, mailboxData);
			IL_44:
			return null;
		}
	}
}
