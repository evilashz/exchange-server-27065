using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C24 RID: 3108
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarItemBaseSchema : ItemSchema
	{
		// Token: 0x17001DE7 RID: 7655
		// (get) Token: 0x06006E80 RID: 28288 RVA: 0x001DB337 File Offset: 0x001D9537
		public new static CalendarItemBaseSchema Instance
		{
			get
			{
				if (CalendarItemBaseSchema.instance == null)
				{
					CalendarItemBaseSchema.instance = new CalendarItemBaseSchema();
				}
				return CalendarItemBaseSchema.instance;
			}
		}

		// Token: 0x06006E81 RID: 28289 RVA: 0x001DB34F File Offset: 0x001D954F
		protected override void AddConstraints(List<StoreObjectConstraint> constraints)
		{
			base.AddConstraints(constraints);
			constraints.Add(new OrganizerPropertiesConstraint());
			constraints.Add(new CalendarOriginatorIdConstraint());
		}

		// Token: 0x06006E82 RID: 28290 RVA: 0x001DB36E File Offset: 0x001D956E
		internal override void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			CalendarItemBase.CoreObjectUpdateLocationAddress(coreItem);
			base.CoreObjectUpdate(coreItem, operation);
		}

		// Token: 0x17001DE8 RID: 7656
		// (get) Token: 0x06006E83 RID: 28291 RVA: 0x001DB37E File Offset: 0x001D957E
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				if (this.propertyRulesCache == null)
				{
					this.propertyRulesCache = base.PropertyRules.Concat(CalendarItemBaseSchema.CalendarItemBasePropertyRules);
				}
				return this.propertyRulesCache;
			}
		}

		// Token: 0x06006E85 RID: 28293 RVA: 0x001DB3B4 File Offset: 0x001D95B4
		// Note: this type is marked as 'beforefieldinit'.
		static CalendarItemBaseSchema()
		{
			PropertyRule[] array = new PropertyRule[4];
			array[0] = PropertyRuleLibrary.DefaultClientIntent;
			array[1] = PropertyRuleLibrary.ResponseAndReplyRequested;
			array[2] = new SequenceCompositePropertyRule(string.Empty, delegate(ILocationIdentifierSetter lidSetter)
			{
				lidSetter.SetLocationIdentifier(60447U, LastChangeAction.SequenceCompositePropertyRuleApplied);
			}, new PropertyRule[]
			{
				PropertyRuleLibrary.EventLocationRule
			});
			array[3] = PropertyRuleLibrary.HasAttendees;
			CalendarItemBaseSchema.CalendarItemBasePropertyRules = array;
			CalendarItemBaseSchema.instance = null;
		}

		// Token: 0x04004141 RID: 16705
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition AttendeeCriticalChangeTime = InternalSchema.AttendeeCriticalChangeTime;

		// Token: 0x04004142 RID: 16706
		[Autoload]
		public static readonly StorePropertyDefinition OldLocation = InternalSchema.OldLocation;

		// Token: 0x04004143 RID: 16707
		[Autoload]
		public static readonly StorePropertyDefinition MeetingRequestType = InternalSchema.MeetingRequestType;

		// Token: 0x04004144 RID: 16708
		[Autoload]
		public static readonly StorePropertyDefinition OwnerCriticalChangeTime = InternalSchema.OwnerCriticalChangeTime;

		// Token: 0x04004145 RID: 16709
		[Autoload]
		internal static readonly StorePropertyDefinition OnlineMeetingChanged = InternalSchema.OnlineMeetingChanged;

		// Token: 0x04004146 RID: 16710
		[Autoload]
		public static readonly StorePropertyDefinition Duration = InternalSchema.Duration;

		// Token: 0x04004147 RID: 16711
		[Autoload]
		public static readonly StorePropertyDefinition IsDraft = InternalSchema.IsDraft;

		// Token: 0x04004148 RID: 16712
		[Autoload]
		internal static readonly StorePropertyDefinition IsSilent = InternalSchema.IsSilent;

		// Token: 0x04004149 RID: 16713
		[Autoload]
		internal static readonly StorePropertyDefinition NetMeetingServer = InternalSchema.NetMeetingServer;

		// Token: 0x0400414A RID: 16714
		[Autoload]
		internal static readonly StorePropertyDefinition NetMeetingOrganizerAlias = InternalSchema.NetMeetingOrganizerAlias;

		// Token: 0x0400414B RID: 16715
		[Autoload]
		internal static readonly StorePropertyDefinition NetMeetingDocPathName = InternalSchema.NetMeetingDocPathName;

		// Token: 0x0400414C RID: 16716
		[Autoload]
		internal static readonly StorePropertyDefinition NetMeetingConferenceServerAllowExternal = InternalSchema.NetMeetingConferenceServerAllowExternal;

		// Token: 0x0400414D RID: 16717
		[Autoload]
		internal static readonly StorePropertyDefinition NetMeetingConferenceSerPassword = InternalSchema.NetMeetingConferenceSerPassword;

		// Token: 0x0400414E RID: 16718
		[LegalTracking]
		[Autoload]
		internal static readonly StorePropertyDefinition ReceivedBy = InternalSchema.ReceivedBy;

		// Token: 0x0400414F RID: 16719
		[Autoload]
		internal static readonly StorePropertyDefinition ReceivedRepresenting = InternalSchema.ReceivedRepresenting;

		// Token: 0x04004150 RID: 16720
		[Autoload]
		internal static readonly StorePropertyDefinition MapiSubject = InternalSchema.MapiSubject;

		// Token: 0x04004151 RID: 16721
		[Autoload]
		public static readonly StorePropertyDefinition ConferenceType = InternalSchema.ConferenceType;

		// Token: 0x04004152 RID: 16722
		[Autoload]
		public static readonly StorePropertyDefinition NetShowURL = InternalSchema.NetShowURL;

		// Token: 0x04004153 RID: 16723
		[Autoload]
		public static readonly StorePropertyDefinition DisallowNewTimeProposal = InternalSchema.DisallowNewTimeProposal;

		// Token: 0x04004154 RID: 16724
		[Autoload]
		public static readonly StorePropertyDefinition MeetingWorkspaceUrl = InternalSchema.MeetingWorkspaceUrl;

		// Token: 0x04004155 RID: 16725
		[Autoload]
		internal static readonly StorePropertyDefinition MarkedForDownload = InternalSchema.MarkedForDownload;

		// Token: 0x04004156 RID: 16726
		[Autoload]
		internal static readonly StorePropertyDefinition Mileage = InternalSchema.Mileage;

		// Token: 0x04004157 RID: 16727
		[Autoload]
		internal static readonly StorePropertyDefinition Companies = InternalSchema.Companies;

		// Token: 0x04004158 RID: 16728
		[Autoload]
		internal static readonly StorePropertyDefinition BillingInformation = InternalSchema.BillingInformation;

		// Token: 0x04004159 RID: 16729
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentLastSequenceNumber = InternalSchema.AppointmentLastSequenceNumber;

		// Token: 0x0400415A RID: 16730
		[Autoload]
		internal static readonly StorePropertyDefinition CdoSequenceNumber = InternalSchema.CdoSequenceNumber;

		// Token: 0x0400415B RID: 16731
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentRecurrenceBlob = InternalSchema.AppointmentRecurrenceBlob;

		// Token: 0x0400415C RID: 16732
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentRecurring = InternalSchema.AppointmentRecurring;

		// Token: 0x0400415D RID: 16733
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentSequenceNumber = InternalSchema.AppointmentSequenceNumber;

		// Token: 0x0400415E RID: 16734
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentSequenceTime = InternalSchema.AppointmentSequenceTime;

		// Token: 0x0400415F RID: 16735
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentState = InternalSchema.AppointmentState;

		// Token: 0x04004160 RID: 16736
		[Autoload]
		public static readonly StorePropertyDefinition ChangeHighlight = InternalSchema.ChangeHighlight;

		// Token: 0x04004161 RID: 16737
		[Autoload]
		public static readonly StorePropertyDefinition GlobalObjectId = InternalSchema.GlobalObjectId;

		// Token: 0x04004162 RID: 16738
		[Autoload]
		public static readonly StorePropertyDefinition CleanGlobalObjectId = InternalSchema.CleanGlobalObjectId;

		// Token: 0x04004163 RID: 16739
		[Autoload]
		public static readonly StorePropertyDefinition EventClientId = InternalSchema.EventClientId;

		// Token: 0x04004164 RID: 16740
		[Autoload]
		public static readonly StorePropertyDefinition SeriesId = InternalSchema.SeriesId;

		// Token: 0x04004165 RID: 16741
		[Autoload]
		public static readonly StorePropertyDefinition IsHiddenFromLegacyClients = InternalSchema.IsHiddenFromLegacyClients;

		// Token: 0x04004166 RID: 16742
		[Autoload]
		public static readonly StorePropertyDefinition MeetingUniqueId = InternalSchema.MeetingUniqueId;

		// Token: 0x04004167 RID: 16743
		[Autoload]
		public static readonly StorePropertyDefinition FreeBusyStatus = InternalSchema.FreeBusyStatus;

		// Token: 0x04004168 RID: 16744
		[Autoload]
		public static readonly StorePropertyDefinition AllAttachmentsHidden = InternalSchema.AllAttachmentsHidden;

		// Token: 0x04004169 RID: 16745
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition IsAllDayEvent = InternalSchema.IsAllDayEvent;

		// Token: 0x0400416A RID: 16746
		[Autoload]
		public static readonly StorePropertyDefinition IsEvent = InternalSchema.IsEvent;

		// Token: 0x0400416B RID: 16747
		[Autoload]
		public static readonly StorePropertyDefinition IsException = InternalSchema.IsException;

		// Token: 0x0400416C RID: 16748
		[Autoload]
		public static readonly StorePropertyDefinition IsMeeting = InternalSchema.IsMeeting;

		// Token: 0x0400416D RID: 16749
		[Autoload]
		public static readonly StorePropertyDefinition IsOnlineMeeting = InternalSchema.IsOnlineMeeting;

		// Token: 0x0400416E RID: 16750
		[Autoload]
		internal static readonly StorePropertyDefinition OutlookUserPropsFormStorage = InternalSchema.OutlookUserPropsFormStorage;

		// Token: 0x0400416F RID: 16751
		[Autoload]
		internal static readonly StorePropertyDefinition OutlookUserPropsScriptStream = InternalSchema.OutlookUserPropsScriptStream;

		// Token: 0x04004170 RID: 16752
		[Autoload]
		internal static readonly StorePropertyDefinition OutlookUserPropsFormPropStream = InternalSchema.OutlookUserPropsFormPropStream;

		// Token: 0x04004171 RID: 16753
		[Autoload]
		internal static readonly StorePropertyDefinition OutlookUserPropsPageDirStream = InternalSchema.OutlookUserPropsPageDirStream;

		// Token: 0x04004172 RID: 16754
		[Autoload]
		internal static readonly StorePropertyDefinition OutlookUserPropsVerbStream = InternalSchema.OutlookUserPropsVerbStream;

		// Token: 0x04004173 RID: 16755
		[Autoload]
		internal static readonly StorePropertyDefinition OutlookUserPropsPropDefStream = InternalSchema.OutlookUserPropsPropDefStream;

		// Token: 0x04004174 RID: 16756
		[Autoload]
		internal static readonly StorePropertyDefinition OutlookUserPropsCustomFlag = InternalSchema.OutlookUserPropsCustomFlag;

		// Token: 0x04004175 RID: 16757
		[Autoload]
		public static readonly StorePropertyDefinition IsRecurring = InternalSchema.IsRecurring;

		// Token: 0x04004176 RID: 16758
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition Location = InternalSchema.Location;

		// Token: 0x04004177 RID: 16759
		[Autoload]
		public static readonly StorePropertyDefinition LocationDisplayName = InternalSchema.LocationDisplayName;

		// Token: 0x04004178 RID: 16760
		[Autoload]
		public static readonly StorePropertyDefinition LocationAnnotation = InternalSchema.LocationAnnotation;

		// Token: 0x04004179 RID: 16761
		[Autoload]
		public static readonly StorePropertyDefinition LocationSource = InternalSchema.LocationSource;

		// Token: 0x0400417A RID: 16762
		[Autoload]
		public static readonly StorePropertyDefinition LocationUri = InternalSchema.LocationUri;

		// Token: 0x0400417B RID: 16763
		[Autoload]
		public static readonly StorePropertyDefinition Latitude = InternalSchema.Latitude;

		// Token: 0x0400417C RID: 16764
		[Autoload]
		public static readonly StorePropertyDefinition Longitude = InternalSchema.Longitude;

		// Token: 0x0400417D RID: 16765
		[Autoload]
		public static readonly StorePropertyDefinition Accuracy = InternalSchema.Accuracy;

		// Token: 0x0400417E RID: 16766
		[Autoload]
		public static readonly StorePropertyDefinition Altitude = InternalSchema.Altitude;

		// Token: 0x0400417F RID: 16767
		[Autoload]
		public static readonly StorePropertyDefinition AltitudeAccuracy = InternalSchema.AltitudeAccuracy;

		// Token: 0x04004180 RID: 16768
		[Autoload]
		public static readonly StorePropertyDefinition LocationStreet = InternalSchema.LocationStreet;

		// Token: 0x04004181 RID: 16769
		[Autoload]
		public static readonly StorePropertyDefinition LocationCity = InternalSchema.LocationCity;

		// Token: 0x04004182 RID: 16770
		[Autoload]
		public static readonly StorePropertyDefinition LocationState = InternalSchema.LocationState;

		// Token: 0x04004183 RID: 16771
		[Autoload]
		public static readonly StorePropertyDefinition LocationCountry = InternalSchema.LocationCountry;

		// Token: 0x04004184 RID: 16772
		[Autoload]
		public static readonly StorePropertyDefinition LocationPostalCode = InternalSchema.LocationPostalCode;

		// Token: 0x04004185 RID: 16773
		[Autoload]
		public static readonly StorePropertyDefinition LocationAddressInternal = InternalSchema.LocationAddressInternal;

		// Token: 0x04004186 RID: 16774
		[Autoload]
		internal static readonly StorePropertyDefinition LidWhere = InternalSchema.LidWhere;

		// Token: 0x04004187 RID: 16775
		[Autoload]
		public static readonly StorePropertyDefinition MapiIsAllDayEvent = InternalSchema.MapiIsAllDayEvent;

		// Token: 0x04004188 RID: 16776
		[Autoload]
		public static readonly StorePropertyDefinition MeetingRequestWasSent = InternalSchema.MeetingRequestWasSent;

		// Token: 0x04004189 RID: 16777
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition Organizer = InternalSchema.From;

		// Token: 0x0400418A RID: 16778
		[Autoload]
		public static readonly StorePropertyDefinition OrganizerDisplayName = InternalSchema.SentRepresentingDisplayName;

		// Token: 0x0400418B RID: 16779
		[Autoload]
		public static readonly StorePropertyDefinition OrganizerEmailAddress = InternalSchema.SentRepresentingEmailAddress;

		// Token: 0x0400418C RID: 16780
		[Autoload]
		public static readonly StorePropertyDefinition OrganizerEntryId = InternalSchema.SentRepresentingEntryId;

		// Token: 0x0400418D RID: 16781
		[Autoload]
		public static readonly StorePropertyDefinition OrganizerType = InternalSchema.SentRepresentingType;

		// Token: 0x0400418E RID: 16782
		[Autoload]
		public static readonly StorePropertyDefinition CalendarOriginatorId = InternalSchema.CalendarOriginatorId;

		// Token: 0x0400418F RID: 16783
		[Autoload]
		public static readonly StorePropertyDefinition OwnerAppointmentID = InternalSchema.OwnerAppointmentID;

		// Token: 0x04004190 RID: 16784
		[Autoload]
		public static readonly StorePropertyDefinition RecurrencePattern = InternalSchema.RecurrencePattern;

		// Token: 0x04004191 RID: 16785
		[Autoload]
		public static readonly StorePropertyDefinition RecurrenceType = InternalSchema.CalculatedRecurrenceType;

		// Token: 0x04004192 RID: 16786
		[Autoload]
		public static readonly StorePropertyDefinition ResponseState = InternalSchema.ResponseState;

		// Token: 0x04004193 RID: 16787
		[Autoload]
		public static readonly StorePropertyDefinition ResponseType = InternalSchema.MapiResponseType;

		// Token: 0x04004194 RID: 16788
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition TimeZone = InternalSchema.TimeZone;

		// Token: 0x04004195 RID: 16789
		[Autoload]
		public static readonly StorePropertyDefinition StartTimeZone = InternalSchema.StartTimeZone;

		// Token: 0x04004196 RID: 16790
		[Autoload]
		public static readonly StorePropertyDefinition EndTimeZone = InternalSchema.EndTimeZone;

		// Token: 0x04004197 RID: 16791
		public static readonly StorePropertyDefinition StartTimeZoneId = InternalSchema.StartTimeZoneId;

		// Token: 0x04004198 RID: 16792
		public static readonly StorePropertyDefinition EndTimeZoneId = InternalSchema.EndTimeZoneId;

		// Token: 0x04004199 RID: 16793
		[Autoload]
		public static readonly StorePropertyDefinition TimeZoneBlob = InternalSchema.TimeZoneBlob;

		// Token: 0x0400419A RID: 16794
		[Autoload]
		public static readonly StorePropertyDefinition TimeZoneDefinitionEnd = InternalSchema.TimeZoneDefinitionEnd;

		// Token: 0x0400419B RID: 16795
		[Autoload]
		public static readonly StorePropertyDefinition TimeZoneDefinitionRecurring = InternalSchema.TimeZoneDefinitionRecurring;

		// Token: 0x0400419C RID: 16796
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition When = InternalSchema.When;

		// Token: 0x0400419D RID: 16797
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentReplyTime = InternalSchema.AppointmentReplyTime;

		// Token: 0x0400419E RID: 16798
		[Autoload]
		public static readonly StorePropertyDefinition IntendedFreeBusyStatus = InternalSchema.IntendedFreeBusyStatus;

		// Token: 0x0400419F RID: 16799
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentExtractVersion = InternalSchema.AppointmentExtractVersion;

		// Token: 0x040041A0 RID: 16800
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentExtractTime = InternalSchema.AppointmentExtractTime;

		// Token: 0x040041A1 RID: 16801
		[Autoload]
		public static readonly StorePropertyDefinition IsOrganizer = InternalSchema.IsOrganizer;

		// Token: 0x040041A2 RID: 16802
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentCounterProposalCount = InternalSchema.AppointmentCounterProposalCount;

		// Token: 0x040041A3 RID: 16803
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentCounterProposal = InternalSchema.AppointmentCounterProposal;

		// Token: 0x040041A4 RID: 16804
		[Autoload]
		public static readonly StorePropertyDefinition ClipStartTime = InternalSchema.ClipStartTime;

		// Token: 0x040041A5 RID: 16805
		[Autoload]
		public static readonly StorePropertyDefinition ClipEndTime = InternalSchema.ClipEndTime;

		// Token: 0x040041A6 RID: 16806
		[Autoload]
		internal static readonly StorePropertyDefinition OriginalStoreEntryId = InternalSchema.OriginalStoreEntryId;

		// Token: 0x040041A7 RID: 16807
		[Autoload]
		internal static readonly StorePropertyDefinition LocationUrl = InternalSchema.LocationURL;

		// Token: 0x040041A8 RID: 16808
		[Autoload]
		internal static readonly StorePropertyDefinition Contact = InternalSchema.Contact;

		// Token: 0x040041A9 RID: 16809
		[Autoload]
		internal static readonly StorePropertyDefinition ContactUrl = InternalSchema.ContactURL;

		// Token: 0x040041AA RID: 16810
		[Autoload]
		internal static readonly StorePropertyDefinition Keywords = InternalSchema.Keywords;

		// Token: 0x040041AB RID: 16811
		[Autoload]
		public static readonly StorePropertyDefinition ClientIntent = InternalSchema.ClientIntent;

		// Token: 0x040041AC RID: 16812
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentAuxiliaryFlags = InternalSchema.AppointmentAuxiliaryFlags;

		// Token: 0x040041AD RID: 16813
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentReplyName = InternalSchema.AppointmentReplyName;

		// Token: 0x040041AE RID: 16814
		internal static readonly StorePropertyDefinition AttachCalendarFlags = InternalSchema.AttachCalendarFlags;

		// Token: 0x040041AF RID: 16815
		internal static readonly StorePropertyDefinition AttachCalendarHidden = InternalSchema.AttachCalendarHidden;

		// Token: 0x040041B0 RID: 16816
		internal static readonly StorePropertyDefinition AttachCalendarLinkId = InternalSchema.AttachCalendarLinkId;

		// Token: 0x040041B1 RID: 16817
		internal static readonly StorePropertyDefinition AttachEncoding = InternalSchema.AttachEncoding;

		// Token: 0x040041B2 RID: 16818
		[Autoload]
		internal static readonly StorePropertyDefinition AppointmentClass = InternalSchema.AppointmentClass;

		// Token: 0x040041B3 RID: 16819
		public static readonly StorePropertyDefinition AppointmentColor = InternalSchema.AppointmentColor;

		// Token: 0x040041B4 RID: 16820
		internal static readonly StorePropertyDefinition AppointmentExceptionEndTime = InternalSchema.AppointmentExceptionEndTime;

		// Token: 0x040041B5 RID: 16821
		internal static readonly StorePropertyDefinition AppointmentExceptionStartTime = InternalSchema.AppointmentExceptionStartTime;

		// Token: 0x040041B6 RID: 16822
		public static readonly StorePropertyDefinition ChangeList = InternalSchema.ChangeList;

		// Token: 0x040041B7 RID: 16823
		[Autoload]
		public static readonly StorePropertyDefinition CalendarLogTriggerAction = InternalSchema.CalendarLogTriggerAction;

		// Token: 0x040041B8 RID: 16824
		[Autoload]
		public static readonly StorePropertyDefinition ItemVersion = InternalSchema.ItemVersion;

		// Token: 0x040041B9 RID: 16825
		internal static readonly StorePropertyDefinition OutlookVersion = InternalSchema.OutlookVersion;

		// Token: 0x040041BA RID: 16826
		internal static readonly StorePropertyDefinition OutlookInternalVersion = InternalSchema.OutlookInternalVersion;

		// Token: 0x040041BB RID: 16827
		public static readonly StorePropertyDefinition CalendarItemType = InternalSchema.CalendarItemType;

		// Token: 0x040041BC RID: 16828
		[Autoload]
		internal static readonly StorePropertyDefinition AcceptLanguage = InternalSchema.AcceptLanguage;

		// Token: 0x040041BD RID: 16829
		[Autoload]
		public static readonly StorePropertyDefinition StartRecurTime = InternalSchema.StartRecurTime;

		// Token: 0x040041BE RID: 16830
		[Autoload]
		public static readonly StorePropertyDefinition StartRecurDate = InternalSchema.StartRecurDate;

		// Token: 0x040041BF RID: 16831
		[Autoload]
		internal static readonly StorePropertyDefinition DisplayAttendeesAll = InternalSchema.DisplayAttendeesAll;

		// Token: 0x040041C0 RID: 16832
		[Autoload]
		public static readonly StorePropertyDefinition DisplayAttendeesTo = InternalSchema.DisplayAttendeesTo;

		// Token: 0x040041C1 RID: 16833
		[Autoload]
		public static readonly StorePropertyDefinition DisplayAttendeesCc = InternalSchema.DisplayAttendeesCc;

		// Token: 0x040041C2 RID: 16834
		[LegalTracking]
		public static readonly StorePropertyDefinition BirthdayContactAttributionDisplayName = InternalSchema.BirthdayContactAttributionDisplayName;

		// Token: 0x040041C3 RID: 16835
		public static readonly StorePropertyDefinition BirthdayContactPersonId = InternalSchema.PersonId;

		// Token: 0x040041C4 RID: 16836
		public static readonly StorePropertyDefinition BirthdayContactId = InternalSchema.BirthdayContactId;

		// Token: 0x040041C5 RID: 16837
		[LegalTracking]
		public static readonly StorePropertyDefinition Birthday = InternalSchema.BirthdayLocal;

		// Token: 0x040041C6 RID: 16838
		public static readonly StorePropertyDefinition IsBirthdayContactWritable = InternalSchema.IsBirthdayContactWritable;

		// Token: 0x040041C7 RID: 16839
		public static readonly StorePropertyDefinition OriginalLastModifiedTime = InternalSchema.OriginalLastModifiedTime;

		// Token: 0x040041C8 RID: 16840
		public static readonly StorePropertyDefinition ResponsibleUserName = InternalSchema.ResponsibleUserName;

		// Token: 0x040041C9 RID: 16841
		public static readonly StorePropertyDefinition SenderEmailAddress = InternalSchema.SenderEmailAddress;

		// Token: 0x040041CA RID: 16842
		public static readonly StorePropertyDefinition ClientInfoString = InternalSchema.ClientInfoString;

		// Token: 0x040041CB RID: 16843
		public static readonly StorePropertyDefinition IsProcessed = InternalSchema.IsProcessed;

		// Token: 0x040041CC RID: 16844
		public static readonly StorePropertyDefinition MiddleTierServerName = InternalSchema.MiddleTierServerName;

		// Token: 0x040041CD RID: 16845
		public static readonly StorePropertyDefinition MiddleTierServerBuildVersion = InternalSchema.MiddleTierServerBuildVersion;

		// Token: 0x040041CE RID: 16846
		public static readonly StorePropertyDefinition MailboxServerName = InternalSchema.MailboxServerName;

		// Token: 0x040041CF RID: 16847
		public static readonly StorePropertyDefinition MiddleTierProcessName = InternalSchema.MiddleTierProcessName;

		// Token: 0x040041D0 RID: 16848
		[Autoload]
		public static readonly StorePropertyDefinition UCOpenedConferenceID = InternalSchema.UCOpenedConferenceID;

		// Token: 0x040041D1 RID: 16849
		[Autoload]
		public static readonly StorePropertyDefinition OnlineMeetingExternalLink = InternalSchema.OnlineMeetingExternalLink;

		// Token: 0x040041D2 RID: 16850
		[Autoload]
		public static readonly StorePropertyDefinition OnlineMeetingInternalLink = InternalSchema.OnlineMeetingInternalLink;

		// Token: 0x040041D3 RID: 16851
		[Autoload]
		public static readonly StorePropertyDefinition OnlineMeetingConfLink = InternalSchema.OnlineMeetingConfLink;

		// Token: 0x040041D4 RID: 16852
		[Autoload]
		public static readonly StorePropertyDefinition UCCapabilities = InternalSchema.UCCapabilities;

		// Token: 0x040041D5 RID: 16853
		[Autoload]
		public static readonly StorePropertyDefinition UCInband = InternalSchema.UCInband;

		// Token: 0x040041D6 RID: 16854
		[Autoload]
		public static readonly StorePropertyDefinition UCMeetingSetting = InternalSchema.UCMeetingSetting;

		// Token: 0x040041D7 RID: 16855
		[Autoload]
		public static readonly StorePropertyDefinition UCMeetingSettingSent = InternalSchema.UCMeetingSettingSent;

		// Token: 0x040041D8 RID: 16856
		[Autoload]
		public static readonly StorePropertyDefinition ConferenceTelURI = InternalSchema.ConferenceTelURI;

		// Token: 0x040041D9 RID: 16857
		[Autoload]
		public static readonly StorePropertyDefinition ConferenceInfo = InternalSchema.ConferenceInfo;

		// Token: 0x040041DA RID: 16858
		public static readonly StorePropertyDefinition EventTimeBasedInboxReminders = InternalSchema.EventTimeBasedInboxReminders;

		// Token: 0x040041DB RID: 16859
		public static readonly StorePropertyDefinition EventTimeBasedInboxRemindersState = InternalSchema.EventTimeBasedInboxRemindersState;

		// Token: 0x040041DC RID: 16860
		[Autoload]
		public static readonly StorePropertyDefinition EventEmailReminderTimer = InternalSchema.EventEmailReminderTimer;

		// Token: 0x040041DD RID: 16861
		[Autoload]
		public static readonly StorePropertyDefinition HasAttendees = InternalSchema.HasAttendees;

		// Token: 0x040041DE RID: 16862
		[Autoload]
		public static readonly StorePropertyDefinition CharmId = InternalSchema.CharmId;

		// Token: 0x040041DF RID: 16863
		private static readonly PropertyRule[] CalendarItemBasePropertyRules;

		// Token: 0x040041E0 RID: 16864
		private static CalendarItemBaseSchema instance;

		// Token: 0x040041E1 RID: 16865
		private ICollection<PropertyRule> propertyRulesCache;
	}
}
