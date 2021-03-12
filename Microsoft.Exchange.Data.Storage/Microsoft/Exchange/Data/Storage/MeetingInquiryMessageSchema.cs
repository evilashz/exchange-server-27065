using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C85 RID: 3205
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MeetingInquiryMessageSchema : MessageItemSchema
	{
		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x06007049 RID: 28745 RVA: 0x001F1822 File Offset: 0x001EFA22
		public new static MeetingInquiryMessageSchema Instance
		{
			get
			{
				if (MeetingInquiryMessageSchema.instance == null)
				{
					MeetingInquiryMessageSchema.instance = new MeetingInquiryMessageSchema();
				}
				return MeetingInquiryMessageSchema.instance;
			}
		}

		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x0600704A RID: 28746 RVA: 0x001F183A File Offset: 0x001EFA3A
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				if (this.propertyRulesCache == null)
				{
					this.propertyRulesCache = base.PropertyRules.Concat(MeetingInquiryMessageSchema.MeetingInquiryMessagePropertyRules);
				}
				return this.propertyRulesCache;
			}
		}

		// Token: 0x04004D62 RID: 19810
		[Autoload]
		public static readonly StorePropertyDefinition CalendarProcessed = InternalSchema.CalendarProcessed;

		// Token: 0x04004D63 RID: 19811
		[Autoload]
		public static readonly StorePropertyDefinition IsProcessed = InternalSchema.IsProcessed;

		// Token: 0x04004D64 RID: 19812
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentAuxiliaryFlags = InternalSchema.AppointmentAuxiliaryFlags;

		// Token: 0x04004D65 RID: 19813
		[Autoload]
		internal static readonly StorePropertyDefinition GlobalObjectId = InternalSchema.GlobalObjectId;

		// Token: 0x04004D66 RID: 19814
		[Autoload]
		public static readonly StorePropertyDefinition CalendarLogTriggerAction = InternalSchema.CalendarLogTriggerAction;

		// Token: 0x04004D67 RID: 19815
		[Autoload]
		internal static readonly StorePropertyDefinition ItemVersion = InternalSchema.ItemVersion;

		// Token: 0x04004D68 RID: 19816
		[Autoload]
		internal static readonly StorePropertyDefinition CleanGlobalObjectId = InternalSchema.CleanGlobalObjectId;

		// Token: 0x04004D69 RID: 19817
		internal static readonly StorePropertyDefinition ChangeList = InternalSchema.ChangeList;

		// Token: 0x04004D6A RID: 19818
		private static readonly PropertyRule[] MeetingInquiryMessagePropertyRules = new PropertyRule[]
		{
			PropertyRuleLibrary.DefaultCleanGlobalObjectIdFromGlobalObjectId
		};

		// Token: 0x04004D6B RID: 19819
		private static MeetingInquiryMessageSchema instance = null;

		// Token: 0x04004D6C RID: 19820
		private ICollection<PropertyRule> propertyRulesCache;
	}
}
