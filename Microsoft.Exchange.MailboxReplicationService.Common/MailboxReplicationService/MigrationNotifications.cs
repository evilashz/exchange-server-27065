using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000159 RID: 345
	public sealed class MigrationNotifications
	{
		// Token: 0x040006E7 RID: 1767
		public const string MailboxLockedNotification = "MailboxLocked";

		// Token: 0x040006E8 RID: 1768
		public const string MailboxCannotBeUnlockedNotification = "MailboxCannotBeUnlocked";

		// Token: 0x040006E9 RID: 1769
		public const string RequestIsPoisonedNotification = "RequestIsPoisoned";

		// Token: 0x040006EA RID: 1770
		public const string CloudMailboxNotConvertedToMailUser = "SourceMailboxNotMorphedToMeu";

		// Token: 0x040006EB RID: 1771
		public const string MRSConfigSettingsErrorNotification = "MRSConfigSettingsCorrupted";

		// Token: 0x040006EC RID: 1772
		public static readonly string CorruptJobNotification = "CorruptJobError";

		// Token: 0x040006ED RID: 1773
		public static readonly string CorruptJobItemNotification = "CorruptJobItemError";

		// Token: 0x040006EE RID: 1774
		public static readonly string CannotMoveMailboxDueToExistingMoveNotInProgress = "CannotMoveMailboxDueToExistingMoveNotInProgress";
	}
}
