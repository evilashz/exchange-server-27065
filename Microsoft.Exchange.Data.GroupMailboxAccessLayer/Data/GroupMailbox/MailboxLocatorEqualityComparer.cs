using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200004D RID: 77
	internal static class MailboxLocatorEqualityComparer
	{
		// Token: 0x0400015B RID: 347
		public static readonly IEqualityComparer<MailboxLocator> ByLegacyDn = new MailboxLocatorEqualityComparer.LegacyDnComparer();

		// Token: 0x0400015C RID: 348
		public static readonly IEqualityComparer<MailboxLocator> ByExternalIdAndLegacyDn = new MailboxLocatorEqualityComparer.ExternalIdAndLegacyDnComparer();

		// Token: 0x0200004E RID: 78
		private sealed class LegacyDnComparer : IEqualityComparer<MailboxLocator>
		{
			// Token: 0x06000273 RID: 627 RVA: 0x0000FADD File Offset: 0x0000DCDD
			public bool Equals(MailboxLocator a, MailboxLocator b)
			{
				return StringComparer.OrdinalIgnoreCase.Equals(a.LegacyDn, b.LegacyDn);
			}

			// Token: 0x06000274 RID: 628 RVA: 0x0000FAF5 File Offset: 0x0000DCF5
			public int GetHashCode(MailboxLocator mailboxLocator)
			{
				return StringComparer.OrdinalIgnoreCase.GetHashCode(mailboxLocator.LegacyDn);
			}
		}

		// Token: 0x0200004F RID: 79
		private sealed class ExternalIdAndLegacyDnComparer : IEqualityComparer<MailboxLocator>
		{
			// Token: 0x06000276 RID: 630 RVA: 0x0000FB0F File Offset: 0x0000DD0F
			public bool Equals(MailboxLocator a, MailboxLocator b)
			{
				if (a.ExternalId != null && b.ExternalId != null)
				{
					return StringComparer.OrdinalIgnoreCase.Equals(a.ExternalId, a.ExternalId);
				}
				return StringComparer.OrdinalIgnoreCase.Equals(a.LegacyDn, b.LegacyDn);
			}

			// Token: 0x06000277 RID: 631 RVA: 0x0000FB4E File Offset: 0x0000DD4E
			public int GetHashCode(MailboxLocator mailboxLocator)
			{
				throw new InvalidOperationException();
			}
		}
	}
}
