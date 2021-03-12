using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000C4 RID: 196
	[DataContract]
	public class GetVoiceMailSettings : GetVoiceMailBase
	{
		// Token: 0x06001D0D RID: 7437 RVA: 0x0005963C File Offset: 0x0005783C
		static GetVoiceMailSettings()
		{
			GetVoiceMailSettings.smsOptionNames[UMSMSNotificationOptions.None] = OwaOptionStrings.VoicemailSMSOptionNone;
			GetVoiceMailSettings.smsOptionNames[UMSMSNotificationOptions.VoiceMail] = OwaOptionStrings.VoicemailSMSOptionVoiceMailOnly;
			GetVoiceMailSettings.smsOptionNames[UMSMSNotificationOptions.VoiceMailAndMissedCalls] = OwaOptionStrings.VoicemailSMSOptionVoiceMailAndMissedCalls;
			GetVoiceMailSettings.smsAvailableOptions = new Dictionary<UMSubscriberType, UMSMSNotificationOptions[]>(2);
			GetVoiceMailSettings.smsAvailableOptions[UMSubscriberType.Consumer] = new UMSMSNotificationOptions[]
			{
				UMSMSNotificationOptions.None,
				UMSMSNotificationOptions.VoiceMail
			};
			GetVoiceMailSettings.smsAvailableOptions[UMSubscriberType.Enterprise] = new UMSMSNotificationOptions[]
			{
				UMSMSNotificationOptions.None,
				UMSMSNotificationOptions.VoiceMail,
				UMSMSNotificationOptions.VoiceMailAndMissedCalls
			};
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000596C1 File Offset: 0x000578C1
		public GetVoiceMailSettings(UMMailbox mailbox) : base(mailbox)
		{
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x000596CC File Offset: 0x000578CC
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x00059717 File Offset: 0x00057917
		[DataMember]
		public string[] AvailableSMSOptionValues
		{
			get
			{
				UMSMSNotificationOptions[] array = GetVoiceMailSettings.smsAvailableOptions[base.UMDialPlan.SubscriberType];
				string[] array2 = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i].ToString();
				}
				return array2;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x00059720 File Offset: 0x00057920
		// (set) Token: 0x06001D12 RID: 7442 RVA: 0x00059779 File Offset: 0x00057979
		[DataMember]
		public string[] AvailableSMSOptionNames
		{
			get
			{
				UMSMSNotificationOptions[] array = GetVoiceMailSettings.smsAvailableOptions[base.UMDialPlan.SubscriberType];
				string[] array2 = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = GetVoiceMailSettings.smsOptionNames[array[i]].ToString();
				}
				return array2;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04001BBB RID: 7099
		private static Dictionary<UMSMSNotificationOptions, LocalizedString> smsOptionNames = new Dictionary<UMSMSNotificationOptions, LocalizedString>(3);

		// Token: 0x04001BBC RID: 7100
		private static Dictionary<UMSubscriberType, UMSMSNotificationOptions[]> smsAvailableOptions;
	}
}
