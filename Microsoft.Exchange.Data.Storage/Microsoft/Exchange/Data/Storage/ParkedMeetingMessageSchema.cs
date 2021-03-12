using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C93 RID: 3219
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ParkedMeetingMessageSchema : ItemSchema
	{
		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x06007089 RID: 28809 RVA: 0x001F2906 File Offset: 0x001F0B06
		public new static ParkedMeetingMessageSchema Instance
		{
			get
			{
				if (ParkedMeetingMessageSchema.instance == null)
				{
					ParkedMeetingMessageSchema.instance = new ParkedMeetingMessageSchema();
				}
				return ParkedMeetingMessageSchema.instance;
			}
		}

		// Token: 0x04004DB1 RID: 19889
		[Autoload]
		public static readonly StorePropertyDefinition ParkedCorrelationId = InternalSchema.ParkedCorrelationId;

		// Token: 0x04004DB2 RID: 19890
		[Autoload]
		public static readonly StorePropertyDefinition OriginalMessageId = InternalSchema.OriginalMessageId;

		// Token: 0x04004DB3 RID: 19891
		[Autoload]
		public static readonly StorePropertyDefinition CleanGlobalObjectId = InternalSchema.CleanGlobalObjectId;

		// Token: 0x04004DB4 RID: 19892
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentSequenceNumber = InternalSchema.AppointmentSequenceNumber;

		// Token: 0x04004DB5 RID: 19893
		private static ParkedMeetingMessageSchema instance;
	}
}
