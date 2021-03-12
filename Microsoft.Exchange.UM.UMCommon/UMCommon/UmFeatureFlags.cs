using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000176 RID: 374
	internal struct UmFeatureFlags
	{
		// Token: 0x06000BCA RID: 3018 RVA: 0x0002B648 File Offset: 0x00029848
		public void Initialize(UMSubscriber subscriber, UMMailboxPolicy policy)
		{
			if (subscriber == null)
			{
				throw new ArgumentNullException("subscriber");
			}
			if (subscriber.ADRecipient == null)
			{
				throw new ArgumentException("subscriber.ADRecipient is null");
			}
			if (policy == null)
			{
				throw new ArgumentNullException("policy");
			}
			UMMailbox adummailboxSettings = subscriber.ADUMMailboxSettings;
			this.EnabledForAnonymousCallerMessages = adummailboxSettings.AnonymousCallersCanLeaveMessages;
			this.EnabledForCalendarAccess = (policy.AllowTUIAccessToCalendar && adummailboxSettings.TUIAccessToCalendarEnabled);
			this.EnabledForEmailAccess = (policy.AllowTUIAccessToEmail && adummailboxSettings.TUIAccessToEmailEnabled);
			this.EnabledForSubscriberAccess = (policy.AllowSubscriberAccess && adummailboxSettings.SubscriberAccessEnabled);
			this.EnabledForDirectoryAccess = policy.AllowTUIAccessToDirectory;
			this.EnabledForContactsAccess = policy.AllowTUIAccessToPersonalContacts;
			this.EnabledForPlayOnPhone = (policy.AllowPlayOnPhone && adummailboxSettings.PlayOnPhoneEnabled);
			this.EnabledForMissedCallNotification = (policy.AllowMissedCallNotifications && adummailboxSettings.MissedCallNotificationEnabled);
			this.EnabledForMessageWaitingIndicator = policy.AllowMessageWaitingIndicator;
			this.EnabledForVirtualNumber = policy.AllowVirtualNumber;
			this.EnabledForPinlessVoiceMailAccess = (policy.AllowPinlessVoiceMailAccess && adummailboxSettings.PinlessAccessToVoiceMailEnabled);
			this.EnabledForSmsNotifications = (policy.AllowSMSNotification && !subscriber.ADRecipient.IsPersonToPersonTextMessagingEnabled && subscriber.ADRecipient.IsMachineToPersonTextMessagingEnabled);
			this.RequireProtectedPlayOnPhone = policy.RequireProtectedPlayOnPhone;
			this.EnabledForVoiceResponseToOtherMessageTypes = policy.AllowVoiceResponseToOtherMessageTypes;
			UMDialPlan dialPlan = subscriber.DialPlan;
			this.EnabledForFax = (dialPlan.FaxEnabled && policy.AllowFax && adummailboxSettings.FaxEnabled);
			this.EnabledForASR = (dialPlan.AutomaticSpeechRecognitionEnabled && policy.AllowAutomaticSpeechRecognition && adummailboxSettings.AutomaticSpeechRecognitionEnabled);
			this.EnabledForPAA = (dialPlan.CallAnsweringRulesEnabled && policy.AllowCallAnsweringRules && adummailboxSettings.CallAnsweringRulesEnabled);
			bool flag = DialGroups.HaveIntersection(dialPlan.ConfiguredInCountryOrRegionGroups, policy.AllowedInCountryOrRegionGroups) || DialGroups.HaveIntersection(dialPlan.ConfiguredInternationalGroups, policy.AllowedInternationalGroups);
			this.EnabledForOutcalling = (policy.AllowDialPlanSubscribers || policy.AllowExtensions || flag);
		}

		// Token: 0x0400065F RID: 1631
		public bool EnabledForFax;

		// Token: 0x04000660 RID: 1632
		public bool EnabledForCalendarAccess;

		// Token: 0x04000661 RID: 1633
		public bool EnabledForEmailAccess;

		// Token: 0x04000662 RID: 1634
		public bool EnabledForASR;

		// Token: 0x04000663 RID: 1635
		public bool EnabledForSubscriberAccess;

		// Token: 0x04000664 RID: 1636
		public bool EnabledForDirectoryAccess;

		// Token: 0x04000665 RID: 1637
		public bool EnabledForContactsAccess;

		// Token: 0x04000666 RID: 1638
		public bool EnabledForPlayOnPhone;

		// Token: 0x04000667 RID: 1639
		public bool EnabledForSmsNotifications;

		// Token: 0x04000668 RID: 1640
		public bool RequireProtectedPlayOnPhone;

		// Token: 0x04000669 RID: 1641
		public bool EnabledForPAA;

		// Token: 0x0400066A RID: 1642
		public bool EnabledForMessageWaitingIndicator;

		// Token: 0x0400066B RID: 1643
		public bool EnabledForMissedCallNotification;

		// Token: 0x0400066C RID: 1644
		public bool EnabledForAnonymousCallerMessages;

		// Token: 0x0400066D RID: 1645
		public bool EnabledForOutcalling;

		// Token: 0x0400066E RID: 1646
		public bool EnabledForVirtualNumber;

		// Token: 0x0400066F RID: 1647
		public bool EnabledForPinlessVoiceMailAccess;

		// Token: 0x04000670 RID: 1648
		public bool EnabledForVoiceResponseToOtherMessageTypes;
	}
}
