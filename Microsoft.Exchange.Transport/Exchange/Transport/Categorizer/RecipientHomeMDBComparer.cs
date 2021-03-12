using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001EA RID: 490
	internal sealed class RecipientHomeMDBComparer : IComparer<MailRecipient>
	{
		// Token: 0x060015DC RID: 5596 RVA: 0x00058F30 File Offset: 0x00057130
		public static Guid GetHomeMDBGuid(MailRecipient recipient)
		{
			if (recipient == null || !recipient.IsActive)
			{
				return Guid.Empty;
			}
			ADObjectId value = recipient.ExtendedProperties.GetValue<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.Database", null);
			if (value != null)
			{
				return value.ObjectGuid;
			}
			return Guid.Empty;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00058F70 File Offset: 0x00057170
		public int Compare(MailRecipient leftRecipient, MailRecipient rightRecipient)
		{
			return RecipientHomeMDBComparer.GetHomeMDBGuid(leftRecipient).CompareTo(RecipientHomeMDBComparer.GetHomeMDBGuid(rightRecipient));
		}
	}
}
