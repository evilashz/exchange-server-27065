using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C86 RID: 3206
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingRequestSchema : MeetingMessageInstanceSchema
	{
		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x0600704D RID: 28749 RVA: 0x001F18E0 File Offset: 0x001EFAE0
		public new static MeetingRequestSchema Instance
		{
			get
			{
				if (MeetingRequestSchema.instance == null)
				{
					MeetingRequestSchema.instance = new MeetingRequestSchema();
				}
				return MeetingRequestSchema.instance;
			}
		}

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x0600704E RID: 28750 RVA: 0x001F18F8 File Offset: 0x001EFAF8
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				return base.PropertyRules.Concat(MeetingRequestSchema.MeetingRequestPropertyRules);
			}
		}

		// Token: 0x04004D6D RID: 19821
		[Autoload]
		internal static readonly StorePropertyDefinition AppointmentClass = InternalSchema.AppointmentClass;

		// Token: 0x04004D6E RID: 19822
		public static readonly StorePropertyDefinition AppointmentReplyTime = InternalSchema.AppointmentReplyTime;

		// Token: 0x04004D6F RID: 19823
		public static readonly StorePropertyDefinition IntendedFreeBusyStatus = InternalSchema.IntendedFreeBusyStatus;

		// Token: 0x04004D70 RID: 19824
		[Autoload]
		public static readonly StorePropertyDefinition OldStartWhole = InternalSchema.OldStartWhole;

		// Token: 0x04004D71 RID: 19825
		[Autoload]
		public static readonly StorePropertyDefinition OldEndWhole = InternalSchema.OldEndWhole;

		// Token: 0x04004D72 RID: 19826
		public static readonly StorePropertyDefinition OccurrencesExceptionalViewProperties = InternalSchema.OccurrencesExceptionalViewProperties;

		// Token: 0x04004D73 RID: 19827
		[Autoload]
		internal static readonly StorePropertyDefinition UnsendableRecipients = InternalSchema.UnsendableRecipients;

		// Token: 0x04004D74 RID: 19828
		private static readonly PropertyRule[] MeetingRequestPropertyRules = new PropertyRule[]
		{
			PropertyRuleLibrary.ResponseAndReplyRequested
		};

		// Token: 0x04004D75 RID: 19829
		private static MeetingRequestSchema instance = null;
	}
}
