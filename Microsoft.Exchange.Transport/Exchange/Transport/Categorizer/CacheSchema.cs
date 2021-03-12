using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001C8 RID: 456
	internal class CacheSchema
	{
		// Token: 0x060014DB RID: 5339 RVA: 0x00053ABC File Offset: 0x00051CBC
		protected static void Set(CachedProperty[] properties, ADRawEntry entry, TransportMailItem mailItem)
		{
			foreach (CachedProperty cachedProperty in properties)
			{
				cachedProperty.Set(entry, mailItem);
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00053AF0 File Offset: 0x00051CF0
		protected static void Set(CachedProperty[] properties, ADRawEntry entry, MailRecipient recipient)
		{
			foreach (CachedProperty cachedProperty in properties)
			{
				cachedProperty.Set(entry, recipient);
			}
		}

		// Token: 0x04000A82 RID: 2690
		public const string DirectoryPrefix = "Microsoft.Exchange.Transport.DirectoryData.";
	}
}
