using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C82 RID: 3202
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingMessageSchema : MessageItemSchema
	{
		// Token: 0x17001E28 RID: 7720
		// (get) Token: 0x0600703C RID: 28732 RVA: 0x001F1373 File Offset: 0x001EF573
		public new static MeetingMessageSchema Instance
		{
			get
			{
				if (MeetingMessageSchema.instance == null)
				{
					MeetingMessageSchema.instance = new MeetingMessageSchema();
				}
				return MeetingMessageSchema.instance;
			}
		}

		// Token: 0x0600703D RID: 28733 RVA: 0x001F138B File Offset: 0x001EF58B
		protected MeetingMessageSchema()
		{
		}

		// Token: 0x04004D13 RID: 19731
		[Autoload]
		internal static readonly StorePropertyDefinition OnlineMeetingChanged = InternalSchema.OnlineMeetingChanged;

		// Token: 0x04004D14 RID: 19732
		[Autoload]
		public static readonly StorePropertyDefinition CalendarProcessed = InternalSchema.CalendarProcessed;

		// Token: 0x04004D15 RID: 19733
		public static readonly StorePropertyDefinition IsOutOfDate = InternalSchema.IsOutOfDate;

		// Token: 0x04004D16 RID: 19734
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentAuxiliaryFlags = InternalSchema.AppointmentAuxiliaryFlags;

		// Token: 0x04004D17 RID: 19735
		[Autoload]
		public static readonly StorePropertyDefinition HijackedMeeting = InternalSchema.HijackedMeeting;

		// Token: 0x04004D18 RID: 19736
		[Autoload]
		public static readonly StorePropertyDefinition SeriesId = InternalSchema.SeriesId;

		// Token: 0x04004D19 RID: 19737
		[Autoload]
		public static readonly StorePropertyDefinition SeriesSequenceNumber = InternalSchema.SeriesSequenceNumber;

		// Token: 0x04004D1A RID: 19738
		[Autoload]
		internal new static readonly StorePropertyDefinition ReceivedRepresentingAddressType = InternalSchema.ReceivedRepresentingAddressType;

		// Token: 0x04004D1B RID: 19739
		[Autoload]
		internal new static readonly StorePropertyDefinition ReceivedRepresentingDisplayName = InternalSchema.ReceivedRepresentingDisplayName;

		// Token: 0x04004D1C RID: 19740
		[Autoload]
		internal new static readonly StorePropertyDefinition ReceivedRepresentingEmailAddress = InternalSchema.ReceivedRepresentingEmailAddress;

		// Token: 0x04004D1D RID: 19741
		[Autoload]
		internal new static readonly StorePropertyDefinition ReceivedRepresentingEntryId = InternalSchema.ReceivedRepresentingEntryId;

		// Token: 0x04004D1E RID: 19742
		[Autoload]
		internal new static readonly StorePropertyDefinition ReceivedByName = InternalSchema.ReceivedByName;

		// Token: 0x04004D1F RID: 19743
		[Autoload]
		internal new static readonly StorePropertyDefinition ReceivedByEmailAddress = InternalSchema.ReceivedByEmailAddress;

		// Token: 0x04004D20 RID: 19744
		[Autoload]
		internal new static readonly StorePropertyDefinition ReceivedByAddrType = InternalSchema.ReceivedByAddrType;

		// Token: 0x04004D21 RID: 19745
		[Autoload]
		internal new static readonly StorePropertyDefinition ReceivedByEntryId = InternalSchema.ReceivedByEntryId;

		// Token: 0x04004D22 RID: 19746
		[Autoload]
		internal static readonly StorePropertyDefinition ClipStartTime = InternalSchema.ClipStartTime;

		// Token: 0x04004D23 RID: 19747
		[Autoload]
		internal static readonly StorePropertyDefinition ClipEndTime = InternalSchema.ClipEndTime;

		// Token: 0x04004D24 RID: 19748
		[Autoload]
		internal static readonly StorePropertyDefinition Location = InternalSchema.Location;

		// Token: 0x04004D25 RID: 19749
		[Autoload]
		public static readonly StorePropertyDefinition LocationDisplayName = InternalSchema.LocationDisplayName;

		// Token: 0x04004D26 RID: 19750
		[Autoload]
		public static readonly StorePropertyDefinition LocationAnnotation = InternalSchema.LocationAnnotation;

		// Token: 0x04004D27 RID: 19751
		[Autoload]
		public static readonly StorePropertyDefinition LocationSource = InternalSchema.LocationSource;

		// Token: 0x04004D28 RID: 19752
		[Autoload]
		public static readonly StorePropertyDefinition LocationUri = InternalSchema.LocationUri;

		// Token: 0x04004D29 RID: 19753
		[Autoload]
		public static readonly StorePropertyDefinition Latitude = InternalSchema.Latitude;

		// Token: 0x04004D2A RID: 19754
		[Autoload]
		public static readonly StorePropertyDefinition Longitude = InternalSchema.Longitude;

		// Token: 0x04004D2B RID: 19755
		[Autoload]
		public static readonly StorePropertyDefinition Accuracy = InternalSchema.Accuracy;

		// Token: 0x04004D2C RID: 19756
		[Autoload]
		public static readonly StorePropertyDefinition Altitude = InternalSchema.Altitude;

		// Token: 0x04004D2D RID: 19757
		[Autoload]
		public static readonly StorePropertyDefinition AltitudeAccuracy = InternalSchema.AltitudeAccuracy;

		// Token: 0x04004D2E RID: 19758
		[Autoload]
		public static readonly StorePropertyDefinition LocationStreet = InternalSchema.LocationStreet;

		// Token: 0x04004D2F RID: 19759
		[Autoload]
		public static readonly StorePropertyDefinition LocationCity = InternalSchema.LocationCity;

		// Token: 0x04004D30 RID: 19760
		[Autoload]
		public static readonly StorePropertyDefinition LocationState = InternalSchema.LocationState;

		// Token: 0x04004D31 RID: 19761
		[Autoload]
		public static readonly StorePropertyDefinition LocationCountry = InternalSchema.LocationCountry;

		// Token: 0x04004D32 RID: 19762
		[Autoload]
		public static readonly StorePropertyDefinition LocationPostalCode = InternalSchema.LocationPostalCode;

		// Token: 0x04004D33 RID: 19763
		[Autoload]
		public static readonly StorePropertyDefinition LocationAddressInternal = InternalSchema.LocationAddressInternal;

		// Token: 0x04004D34 RID: 19764
		internal static readonly StorePropertyDefinition ChangeList = InternalSchema.ChangeList;

		// Token: 0x04004D35 RID: 19765
		[Autoload]
		public static readonly StorePropertyDefinition CalendarLogTriggerAction = InternalSchema.CalendarLogTriggerAction;

		// Token: 0x04004D36 RID: 19766
		[Autoload]
		internal static readonly StorePropertyDefinition ItemVersion = InternalSchema.ItemVersion;

		// Token: 0x04004D37 RID: 19767
		[Autoload]
		internal static readonly StorePropertyDefinition EHAMigrationExpirationDate = InternalSchema.EHAMigrationExpirationDate;

		// Token: 0x04004D38 RID: 19768
		[Autoload]
		internal static readonly StorePropertyDefinition SeriesReminderIsSet = InternalSchema.SeriesReminderIsSet;

		// Token: 0x04004D39 RID: 19769
		private static MeetingMessageSchema instance = null;
	}
}
