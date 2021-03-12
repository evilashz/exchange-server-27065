using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000C3 RID: 195
	[DataContract]
	public class GetVoiceMailConfiguration : GetVoiceMailBase
	{
		// Token: 0x06001CEA RID: 7402 RVA: 0x0005938F File Offset: 0x0005758F
		public GetVoiceMailConfiguration(UMMailbox mailbox) : base(mailbox)
		{
			this.UMMailboxPolicy = mailbox.GetPolicy();
			this.carrierData = this.GetCarrierData();
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x000593B0 File Offset: 0x000575B0
		// (set) Token: 0x06001CEC RID: 7404 RVA: 0x000593B8 File Offset: 0x000575B8
		public UMMailboxPolicy UMMailboxPolicy { get; private set; }

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x000593C1 File Offset: 0x000575C1
		// (set) Token: 0x06001CEE RID: 7406 RVA: 0x000593CE File Offset: 0x000575CE
		[DataMember]
		public string PhoneNumber
		{
			get
			{
				return base.UMMailbox.PhoneNumber;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x000593D5 File Offset: 0x000575D5
		// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x000593DD File Offset: 0x000575DD
		[DataMember]
		public string PIN
		{
			get
			{
				return this.GetDisplayPIN();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x000593E4 File Offset: 0x000575E4
		// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x000593F1 File Offset: 0x000575F1
		[DataMember]
		public string CountryOrRegionCode
		{
			get
			{
				return base.UMDialPlan.CountryOrRegionCode;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x000593F8 File Offset: 0x000575F8
		// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x000593FF File Offset: 0x000575FF
		[DataMember]
		public string CountryOrRegionId
		{
			get
			{
				return "US";
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x00059406 File Offset: 0x00057606
		// (set) Token: 0x06001CF6 RID: 7414 RVA: 0x0005940E File Offset: 0x0005760E
		[DataMember]
		public string CallForwardingPilotNumber
		{
			get
			{
				return this.GetCallForwardingPilotNumber();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x00059415 File Offset: 0x00057615
		// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x0005941D File Offset: 0x0005761D
		[DataMember]
		public string VoiceMailAccessNumbers
		{
			get
			{
				return this.GetVoiceMailAccessNumbers();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x00059424 File Offset: 0x00057624
		// (set) Token: 0x06001CFA RID: 7418 RVA: 0x00059431 File Offset: 0x00057631
		[DataMember]
		public string PhoneProviderId
		{
			get
			{
				return base.UMMailbox.PhoneProviderId;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x00059438 File Offset: 0x00057638
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x00059453 File Offset: 0x00057653
		[DataMember]
		public string PhoneProviderName
		{
			get
			{
				if (this.carrierData != null)
				{
					return this.carrierData.Name;
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0005945A File Offset: 0x0005765A
		// (set) Token: 0x06001CFE RID: 7422 RVA: 0x00059467 File Offset: 0x00057667
		[DataMember]
		public int RequiredPINLength
		{
			get
			{
				return this.UMMailboxPolicy.MinPINLength;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0005946E File Offset: 0x0005766E
		// (set) Token: 0x06001D00 RID: 7424 RVA: 0x00059494 File Offset: 0x00057694
		[DataMember]
		public string CallForwardingDisableDigits
		{
			get
			{
				if (this.carrierData != null)
				{
					return this.carrierData.UnifiedMessagingInfo.RenderDisableSequence(this.PhoneNumber);
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0005949B File Offset: 0x0005769B
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x000594C3 File Offset: 0x000576C3
		[DataMember]
		public string SMSNotificationPhoneNumber
		{
			get
			{
				if (base.SmsOptions == null || !base.SmsOptions.NotificationPhoneNumberVerified)
				{
					return string.Empty;
				}
				return base.SmsOptions.NotificationPhoneNumber;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x000594CA File Offset: 0x000576CA
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x000594F2 File Offset: 0x000576F2
		[DataMember]
		public string SMSNotificationPhoneProviderId
		{
			get
			{
				if (base.SmsOptions == null || !base.SmsOptions.NotificationPhoneNumberVerified)
				{
					return string.Empty;
				}
				return base.SmsOptions.MobileOperatorId;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x000594F9 File Offset: 0x000576F9
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x00059501 File Offset: 0x00057701
		[DataMember]
		public bool VerificationCodeRequired { get; internal set; }

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0005950A File Offset: 0x0005770A
		// (set) Token: 0x06001D08 RID: 7432 RVA: 0x00059511 File Offset: 0x00057711
		[DataMember]
		public string VerificationCode
		{
			get
			{
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00059518 File Offset: 0x00057718
		private string GetDisplayPIN()
		{
			return new string('*', this.UMMailboxPolicy.MinPINLength);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0005952C File Offset: 0x0005772C
		private CarrierData GetCarrierData()
		{
			if (!string.IsNullOrEmpty(this.PhoneProviderId))
			{
				CarrierData result = null;
				if (SmsServiceProviders.Instance.VoiceMailCarrierDictionary.TryGetValue(this.PhoneProviderId, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00059564 File Offset: 0x00057764
		private string GetCallForwardingPilotNumber()
		{
			string result = string.Empty;
			TelephonyInfo telephonyInfo;
			if (AirSyncUtils.GetTelephonyInfo(base.UMDialPlan, base.UMMailbox.SIPResourceIdentifier, out telephonyInfo))
			{
				result = telephonyInfo.VoicemailNumber.Number;
			}
			return result;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000595A0 File Offset: 0x000577A0
		private string GetVoiceMailAccessNumbers()
		{
			string text = string.Empty;
			if (base.UMDialPlan.AccessTelephoneNumbers != null)
			{
				foreach (string text2 in base.UMDialPlan.AccessTelephoneNumbers)
				{
					if (!string.IsNullOrEmpty(text2))
					{
						if (text.Length == 0)
						{
							text = text2;
						}
						else
						{
							text = text + " " + OwaOptionStrings.VoicemailAccessNumbersTemplate(text2).ToString();
						}
					}
				}
			}
			return text;
		}

		// Token: 0x04001BB8 RID: 7096
		private CarrierData carrierData;
	}
}
