using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000172 RID: 370
	internal static class OtherMailboxConfiguration
	{
		// Token: 0x06000D08 RID: 3336 RVA: 0x0005859C File Offset: 0x0005679C
		private static SimpleConfiguration<OtherMailboxConfigEntry> GetOtherMailboxConfig(UserContext userContext)
		{
			SimpleConfiguration<OtherMailboxConfigEntry> simpleConfiguration = new SimpleConfiguration<OtherMailboxConfigEntry>(userContext);
			simpleConfiguration.Load();
			return simpleConfiguration;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000585B8 File Offset: 0x000567B8
		private static OtherMailboxConfigEntry FindOtherMailboxConfigEntry(SimpleConfiguration<OtherMailboxConfigEntry> config, string legacyDN)
		{
			foreach (OtherMailboxConfigEntry otherMailboxConfigEntry in config.Entries)
			{
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString(otherMailboxConfigEntry.RootFolderId);
				if (string.Equals(owaStoreObjectId.MailboxOwnerLegacyDN, legacyDN, StringComparison.OrdinalIgnoreCase))
				{
					return otherMailboxConfigEntry;
				}
			}
			return null;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00058620 File Offset: 0x00056820
		internal static IList<OtherMailboxConfigEntry> GetOtherMailboxes(UserContext userContext)
		{
			return OtherMailboxConfiguration.GetOtherMailboxConfig(userContext).Entries;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00058630 File Offset: 0x00056830
		internal static OtherMailboxConfigEntry AddOtherMailboxSession(UserContext userContext, MailboxSession mailboxSession)
		{
			SimpleConfiguration<OtherMailboxConfigEntry> otherMailboxConfig = OtherMailboxConfiguration.GetOtherMailboxConfig(userContext);
			if (OtherMailboxConfiguration.FindOtherMailboxConfigEntry(otherMailboxConfig, mailboxSession.MailboxOwnerLegacyDN) != null)
			{
				return null;
			}
			StoreObjectId defaultFolderId = Utilities.GetDefaultFolderId(mailboxSession, DefaultFolderType.Root);
			OtherMailboxConfigEntry otherMailboxConfigEntry = new OtherMailboxConfigEntry(Utilities.GetMailboxOwnerDisplayName(mailboxSession), OwaStoreObjectId.CreateFromOtherUserMailboxFolderId(defaultFolderId, mailboxSession.MailboxOwner.LegacyDn));
			otherMailboxConfig.Entries.Add(otherMailboxConfigEntry);
			otherMailboxConfig.Save();
			return otherMailboxConfigEntry;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00058690 File Offset: 0x00056890
		internal static bool RemoveOtherMailbox(UserContext userContext, string legacyDN)
		{
			SimpleConfiguration<OtherMailboxConfigEntry> otherMailboxConfig = OtherMailboxConfiguration.GetOtherMailboxConfig(userContext);
			OtherMailboxConfigEntry otherMailboxConfigEntry = OtherMailboxConfiguration.FindOtherMailboxConfigEntry(otherMailboxConfig, legacyDN);
			if (otherMailboxConfigEntry != null)
			{
				otherMailboxConfig.Entries.Remove(otherMailboxConfigEntry);
				otherMailboxConfig.Save();
				return true;
			}
			return false;
		}
	}
}
