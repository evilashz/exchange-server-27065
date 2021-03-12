using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CCA RID: 3274
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupMailboxJoinRequestMessageSchema : MessageItemSchema
	{
		// Token: 0x17001E61 RID: 7777
		// (get) Token: 0x06007197 RID: 29079 RVA: 0x001F7A6F File Offset: 0x001F5C6F
		public new static GroupMailboxJoinRequestMessageSchema Instance
		{
			get
			{
				if (GroupMailboxJoinRequestMessageSchema.instance == null)
				{
					GroupMailboxJoinRequestMessageSchema.instance = new GroupMailboxJoinRequestMessageSchema();
				}
				return GroupMailboxJoinRequestMessageSchema.instance;
			}
		}

		// Token: 0x04004EF8 RID: 20216
		[Autoload]
		public static readonly StorePropertyDefinition GroupSmtpAddress = InternalSchema.XGroupMailboxSmtpAddressId;

		// Token: 0x04004EF9 RID: 20217
		private static GroupMailboxJoinRequestMessageSchema instance;
	}
}
