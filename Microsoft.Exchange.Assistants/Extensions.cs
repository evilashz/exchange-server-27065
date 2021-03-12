using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000B9 RID: 185
	public static class Extensions
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0001B49E File Offset: 0x0001969E
		internal static bool Contains(this MapiEventTypeFlags eventMask, MapiEventTypeFlags eventFlag)
		{
			return (eventMask & eventFlag) != (MapiEventTypeFlags)0;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001B4A9 File Offset: 0x000196A9
		internal static bool Contains(this MapiExtendedEventFlags eventMask, MapiExtendedEventFlags eventFlag)
		{
			return (eventMask & eventFlag) != MapiExtendedEventFlags.None;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001B4B5 File Offset: 0x000196B5
		internal static bool Contains(this MailboxType mailboxFilter, MailboxType mailboxType)
		{
			return (mailboxFilter & mailboxType) != (MailboxType)0;
		}
	}
}
