using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200028F RID: 655
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MapiUtil
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x0007E264 File Offset: 0x0007C464
		internal static RecipientType RecipientItemTypeToMapiRecipientType(RecipientItemType recipientItemType, bool isSubmitted)
		{
			return (RecipientType)(recipientItemType | (isSubmitted ? ((RecipientItemType)(-2147483648)) : ((RecipientItemType)0)));
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0007E273 File Offset: 0x0007C473
		internal static SearchCriteriaFlags SetSearchCriteriaFlagsToMapiSearchCriteriaFlags(SetSearchCriteriaFlags setSearchCriteriaFlags)
		{
			return (SearchCriteriaFlags)setSearchCriteriaFlags;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0007E278 File Offset: 0x0007C478
		internal static RecipientItemType MapiRecipientTypeToRecipientItemType(RecipientType recipientType)
		{
			if (recipientType == RecipientType.Unknown)
			{
				return RecipientItemType.Unknown;
			}
			RecipientType recipientType2 = recipientType & (RecipientType)2147483647;
			RecipientType recipientType3 = recipientType2;
			switch (recipientType3)
			{
			case RecipientType.To:
			case RecipientType.Cc:
			case RecipientType.Bcc:
				break;
			default:
				if (recipientType3 != RecipientType.P1)
				{
					return RecipientItemType.Unknown;
				}
				break;
			}
			return (RecipientItemType)recipientType2;
		}
	}
}
