using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C84 RID: 3204
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingForwardNotificationSchema : MeetingMessageInstanceSchema
	{
		// Token: 0x17001E2B RID: 7723
		// (get) Token: 0x06007046 RID: 28742 RVA: 0x001F17D6 File Offset: 0x001EF9D6
		public new static MeetingForwardNotificationSchema Instance
		{
			get
			{
				if (MeetingForwardNotificationSchema.instance == null)
				{
					MeetingForwardNotificationSchema.instance = new MeetingForwardNotificationSchema();
				}
				return MeetingForwardNotificationSchema.instance;
			}
		}

		// Token: 0x06007047 RID: 28743 RVA: 0x001F17EE File Offset: 0x001EF9EE
		protected MeetingForwardNotificationSchema()
		{
			base.RemoveConstraints(CalendarItemSchema.Instance.Constraints);
		}

		// Token: 0x04004D5F RID: 19807
		[Autoload]
		internal static readonly StorePropertyDefinition ForwardNotificationRecipients = InternalSchema.ForwardNotificationRecipients;

		// Token: 0x04004D60 RID: 19808
		[Autoload]
		internal static readonly StorePropertyDefinition MFNAddedRecipients = InternalSchema.MFNAddedRecipients;

		// Token: 0x04004D61 RID: 19809
		private static MeetingForwardNotificationSchema instance = null;
	}
}
