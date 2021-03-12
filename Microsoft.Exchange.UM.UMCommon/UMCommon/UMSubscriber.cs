using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200018A RID: 394
	internal class UMSubscriber : UMMailboxRecipient
	{
		// Token: 0x06000C81 RID: 3201 RVA: 0x0002DDB2 File Offset: 0x0002BFB2
		public UMSubscriber(ADRecipient adrecipient)
		{
			this.Initialize(adrecipient, true);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002DDCF File Offset: 0x0002BFCF
		public UMSubscriber(ADRecipient adrecipient, MailboxSession mbxSession)
		{
			this.Initialize(adrecipient, mbxSession, true);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002DDED File Offset: 0x0002BFED
		protected UMSubscriber()
		{
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002DE01 File Offset: 0x0002C001
		public UMDialPlan DialPlan
		{
			get
			{
				return this.dialPlan;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0002DE09 File Offset: 0x0002C009
		public bool IsFaxEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForFax;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002DE16 File Offset: 0x0002C016
		public bool IsVirtualNumberEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForVirtualNumber;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0002DE23 File Offset: 0x0002C023
		public bool IsCalenderAccessEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForCalendarAccess;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0002DE30 File Offset: 0x0002C030
		public bool IsEmailAccessEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForEmailAccess;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0002DE3D File Offset: 0x0002C03D
		public bool IsMissedCallNotificationEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForMissedCallNotification;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0002DE4A File Offset: 0x0002C04A
		public bool IsPinlessVoicemailAccessEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForPinlessVoiceMailAccess;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002DE57 File Offset: 0x0002C057
		public bool IsVoiceResponseToOtherMessageTypesEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForVoiceResponseToOtherMessageTypes;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0002DE64 File Offset: 0x0002C064
		public bool CanAnonymousCallersLeaveMessage
		{
			get
			{
				return this.enabledFlags.EnabledForAnonymousCallerMessages;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002DE71 File Offset: 0x0002C071
		public bool IsASREnabled
		{
			get
			{
				return this.enabledFlags.EnabledForASR;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0002DE7E File Offset: 0x0002C07E
		public bool UseASR
		{
			get
			{
				return this.IsASREnabled && base.ConfigFolder.UseAsr;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0002DE95 File Offset: 0x0002C095
		public bool IsSubscriberAccessEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForSubscriberAccess;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0002DEA2 File Offset: 0x0002C0A2
		public bool IsTUIAccessToDirectoryEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForDirectoryAccess;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002DEAF File Offset: 0x0002C0AF
		public bool IsTUIAccessToContactsEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForContactsAccess;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0002DEBC File Offset: 0x0002C0BC
		public bool IsTUIAccessToAddressBookEnabled
		{
			get
			{
				return this.IsTUIAccessToDirectoryEnabled || this.IsTUIAccessToContactsEnabled;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0002DECE File Offset: 0x0002C0CE
		public bool IsEnabledForOutcalling
		{
			get
			{
				return this.enabledFlags.EnabledForOutcalling;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0002DEDB File Offset: 0x0002C0DB
		public bool IsPlayOnPhoneEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForPlayOnPhone;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002DEE8 File Offset: 0x0002C0E8
		public bool IsSmsNotificationsEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForSmsNotifications;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0002DEF5 File Offset: 0x0002C0F5
		public bool IsRequireProtectedPlayOnPhone
		{
			get
			{
				return this.enabledFlags.RequireProtectedPlayOnPhone;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0002DF02 File Offset: 0x0002C102
		public bool IsPAAEnabled
		{
			get
			{
				return this.enabledFlags.EnabledForPAA;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0002DF0F File Offset: 0x0002C10F
		public UMMailboxPolicy UMMailboxPolicy
		{
			get
			{
				return base.InternalUMMailboxPolicy;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0002DF17 File Offset: 0x0002C117
		public PasswordPolicy PasswordPolicy
		{
			get
			{
				return this.pwdPolicy;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0002DF1F File Offset: 0x0002C11F
		public string Extension
		{
			get
			{
				return base.ADRecipient.UMExtension;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0002DF2C File Offset: 0x0002C12C
		public string VirtualNumber
		{
			get
			{
				return this.Extension;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0002DF34 File Offset: 0x0002C134
		public string OutboundCallingLineId
		{
			get
			{
				if (!string.IsNullOrEmpty(this.DialPlan.DefaultOutboundCallingLineId))
				{
					return this.DialPlan.DefaultOutboundCallingLineId;
				}
				if (this.IsVirtualNumberEnabled)
				{
					return this.VirtualNumber;
				}
				return this.Extension;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002DF69 File Offset: 0x0002C169
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x0002DF71 File Offset: 0x0002C171
		public bool IsAuthenticated
		{
			get
			{
				return this.authenticated;
			}
			set
			{
				this.authenticated = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0002DF7A File Offset: 0x0002C17A
		public UMMailbox ADUMMailboxSettings
		{
			get
			{
				return base.InternalADUMMailboxSettings;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0002DF82 File Offset: 0x0002C182
		public override CultureInfo TelephonyCulture
		{
			get
			{
				ExAssert.RetailAssert(this.telephonyCulture != null, "TelephonyCulture: UMSubscriber not initialized");
				CultureInfo result;
				if ((result = this.telephonyCulture.Value) == null)
				{
					result = (this.DialPlan.DefaultLanguage.Culture ?? CommonConstants.DefaultCulture);
				}
				return result;
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002DFC4 File Offset: 0x0002C1C4
		public new static bool TryCreate(ADRecipient adrecipient, out UMRecipient umrecipient)
		{
			UMSubscriber umsubscriber = new UMSubscriber();
			if (umsubscriber.Initialize(adrecipient, false))
			{
				umrecipient = umsubscriber;
				return true;
			}
			umsubscriber.Dispose();
			umrecipient = null;
			return false;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
		public static bool IsValidSubscriber(ADRecipient recipient)
		{
			bool result;
			using (UMSubscriber umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(recipient))
			{
				result = (umsubscriber != null);
			}
			return result;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002E02C File Offset: 0x0002C22C
		public static bool IsValidSubscriber(string extension, UMDialPlan dialPlan, UMRecipient scopingUser)
		{
			bool result;
			using (UMSubscriber umsubscriber = UMRecipient.Factory.FromExtension<UMSubscriber>(extension, dialPlan, scopingUser))
			{
				result = (umsubscriber != null);
			}
			return result;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002E068 File Offset: 0x0002C268
		public static TranscriptionEnabledSetting IsPartnerTranscriptionEnabled(UMMailboxPolicy mailboxPolicy, TranscriptionEnabledSetting transcriptionEnabledInMailboxConfig)
		{
			ValidateArgument.NotNull(mailboxPolicy, "mailboxPolicy");
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "IsPartnerTranscriptionEnabled(VoiceMailPreviewPartnerAddress = '{0}', transcriptionEnabledInMailboxConfig = '{1}'", new object[]
			{
				mailboxPolicy.VoiceMailPreviewPartnerAddress,
				transcriptionEnabledInMailboxConfig
			});
			if (mailboxPolicy.VoiceMailPreviewPartnerAddress != null && mailboxPolicy.VoiceMailPreviewPartnerAddress.Value.IsValidAddress)
			{
				return UMSubscriber.IsTranscriptionEnabled(mailboxPolicy, transcriptionEnabledInMailboxConfig);
			}
			return TranscriptionEnabledSetting.Disabled;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002E0E0 File Offset: 0x0002C2E0
		public static TranscriptionEnabledSetting IsTranscriptionEnabled(UMMailboxPolicy mailboxPolicy, TranscriptionEnabledSetting transcriptionEnabledInMailboxConfig)
		{
			ValidateArgument.NotNull(mailboxPolicy, "mailboxPolicy");
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "IsTranscriptionEnabled(AllowVoiceMailPreview = '{0}', transcriptionEnabledInMailboxConfig = '{1}'", new object[]
			{
				mailboxPolicy.AllowVoiceMailPreview,
				transcriptionEnabledInMailboxConfig
			});
			if (mailboxPolicy.AllowVoiceMailPreview)
			{
				return transcriptionEnabledInMailboxConfig;
			}
			return TranscriptionEnabledSetting.Disabled;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002E134 File Offset: 0x0002C334
		public bool IsBlockedNumber(PhoneNumber callerId)
		{
			PIIMessage[] data = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._User, this),
				PIIMessage.Create(PIIType._Caller, callerId)
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data, "IsBlockedNumber(subscriber=_User, callerId=_Caller). BlockedNumbers has {0} entries.", new object[]
			{
				base.ConfigFolder.BlockedNumbers.Count
			});
			foreach (string matchString in base.ConfigFolder.BlockedNumbers)
			{
				if (callerId.IsMatch(matchString, this.DialPlan))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data, "IsBlockedNumber: Subscriber=_User. _PhoneNumber is in the blocked number list.", new object[0]);
					return true;
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data, "IsBlockedNumber: Subscriber=_User. _PhoneNumber not found in blocked number list.", new object[]
			{
				this,
				callerId
			});
			return false;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002E22C File Offset: 0x0002C42C
		public bool HasCustomGreeting(MailboxGreetingEnum t)
		{
			return base.ConfigFolder.HasCustomMailboxGreeting(t);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002E23A File Offset: 0x0002C43A
		public void RemoveCustomGreeting(MailboxGreetingEnum t)
		{
			base.ConfigFolder.RemoveCustomMailboxGreeting(t);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002E248 File Offset: 0x0002C448
		public bool IsOOF()
		{
			bool result = false;
			try
			{
				if (base.ConfigFolder == null)
				{
					return false;
				}
				result = base.ConfigFolder.IsOof;
			}
			catch (LocalizedException se)
			{
				this.LogException(se, "get OOF status");
			}
			return result;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002E294 File Offset: 0x0002C494
		public ITempWavFile GetGreeting()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "RecordVoicemailManager::FetchGreetingFromXSO", new object[0]);
			GreetingBase greetingBase = null;
			ITempWavFile tempWavFile = null;
			try
			{
				if (base.ConfigFolder == null)
				{
					return null;
				}
				greetingBase = (base.ConfigFolder.IsOof ? base.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Away) : base.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Voicemail));
				if (greetingBase == null)
				{
					return null;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Using greeting g={0}", new object[]
				{
					greetingBase.Name
				});
				tempWavFile = greetingBase.Get();
				if (tempWavFile == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "No greeting found in XSO.", new object[0]);
				}
				else
				{
					tempWavFile.ExtraInfo = greetingBase.Name;
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Greeting g={0} was downloaded from XSO", new object[]
					{
						greetingBase.Name
					});
				}
			}
			catch (LocalizedException se)
			{
				this.LogException(se, "get greeting");
			}
			finally
			{
				if (greetingBase != null)
				{
					greetingBase.Dispose();
				}
			}
			return tempWavFile;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		public IList<string> GetExtensionsInPrimaryDialPlan()
		{
			return Utils.GetExtensionsInDialPlan(this.DialPlan, base.ADRecipient);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
		public bool ShouldMessageBeProtected(bool callAnswering, bool messageMarkedPrivate)
		{
			DRMProtectionOptions drmprotectionOptions = callAnswering ? this.DRMPolicyForCA : this.DRMPolicyForInterpersonal;
			return drmprotectionOptions == DRMProtectionOptions.All || (drmprotectionOptions == DRMProtectionOptions.Private && messageMarkedPrivate);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002E40C File Offset: 0x0002C60C
		internal bool IsLinkedToDialPlan(UMDialPlan dialPlanToCheck)
		{
			ValidateArgument.NotNull(dialPlanToCheck, "dialPlanToCheck");
			bool flag = this.DialPlan.Guid == dialPlanToCheck.Guid;
			if (!flag)
			{
				List<string> dialPlanPhoneContexts = UMMailbox.GetDialPlanPhoneContexts(base.ADRecipient.EmailAddresses, true);
				flag = dialPlanPhoneContexts.Exists((string currentObj) => string.Equals(dialPlanToCheck.PhoneContext, currentObj, StringComparison.OrdinalIgnoreCase));
			}
			return flag;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002E49C File Offset: 0x0002C69C
		internal bool IsLimitedOVAAccessAllowed(UMDialPlan dp, PhoneNumber callerId)
		{
			bool flag = false;
			ValidateArgument.NotNull(dp, "dp");
			ValidateArgument.NotNull(callerId, "callerId");
			if (this.IsPinlessVoicemailAccessEnabled)
			{
				ProxyAddress address = UMMailbox.BuildProxyAddressFromExtensionAndPhoneContext(callerId.ToDial, ProxyAddressPrefix.UM.PrimaryPrefix, dp.PhoneContext);
				flag = (base.ADRecipient.EmailAddresses.Find((ProxyAddress o) => ProxyAddressBase.Equals(o, address, StringComparison.OrdinalIgnoreCase)) != null);
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "IsLimitedOVAAccessAllowed(): {0} {1} entry in the proxyaddresses property of the user {2}", new object[]
				{
					flag ? "Found" : "Could not find",
					address,
					base.ADUser.LegacyExchangeDN
				});
			}
			return flag;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002E558 File Offset: 0x0002C758
		internal TranscriptionEnabledSetting IsTranscriptionEnabledInMailboxConfig(VoiceMailTypeEnum voiceMailType)
		{
			TranscriptionEnabledSetting transcriptionEnabledSetting = TranscriptionEnabledSetting.Enabled;
			try
			{
				switch (voiceMailType)
				{
				case VoiceMailTypeEnum.ReceivedVoiceMails:
					if (!base.ConfigFolder.ReceivedVoiceMailPreviewEnabled)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "User has transcription turned off for Received voicemails", new object[0]);
						transcriptionEnabledSetting = TranscriptionEnabledSetting.Disabled;
					}
					break;
				case VoiceMailTypeEnum.SentVoiceMails:
					if (!base.ConfigFolder.SentVoiceMailPreviewEnabled)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "User has transcription turned off for sent voicemails", new object[0]);
						transcriptionEnabledSetting = TranscriptionEnabledSetting.Disabled;
					}
					break;
				default:
					throw new InvalidArgumentException("voiceMailType");
				}
			}
			catch (StorageTransientException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "Failed to get user's settings for transcription. Most likely mailbox is down. Error: {0}", new object[]
				{
					ex.ToString()
				});
				transcriptionEnabledSetting = TranscriptionEnabledSetting.Unknown;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "IsTranscriptionEnabledInMailboxConfig returned {0}", new object[]
			{
				transcriptionEnabledSetting
			});
			return transcriptionEnabledSetting;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002E630 File Offset: 0x0002C830
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UMSubscriber>(this);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002E638 File Offset: 0x0002C838
		protected override bool Initialize(ADRecipient recipient, bool throwOnFailure)
		{
			bool flag = false;
			try
			{
				if (!base.Initialize(recipient, throwOnFailure))
				{
					return flag;
				}
				flag = this.InitializeInternal(recipient, throwOnFailure);
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002E680 File Offset: 0x0002C880
		protected override bool Initialize(ADRecipient recipient, MailboxSession session, bool throwOnFailure)
		{
			bool flag = false;
			try
			{
				if (!base.Initialize(recipient, session, throwOnFailure))
				{
					return flag;
				}
				flag = this.InitializeInternal(recipient, throwOnFailure);
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002E710 File Offset: 0x0002C910
		private bool InitializeInternal(ADRecipient recipient, bool throwOnFailure)
		{
			this.telephonyCulture = new Lazy<CultureInfo>(new Func<CultureInfo>(this.InitTelephonyCulture));
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(base.ADUser);
			if (!base.CheckField(base.ADUser.UMRecipientDialPlanId, "dialPlanId", UMRecipient.FieldMissingCheck, throwOnFailure))
			{
				return false;
			}
			this.dialPlan = iadsystemConfigurationLookup.GetDialPlanFromId(base.ADUser.UMRecipientDialPlanId);
			if (!base.CheckField(this.dialPlan, "dialPlan", UMRecipient.FieldMissingCheck, throwOnFailure))
			{
				return false;
			}
			if (!string.IsNullOrEmpty(this.Extension) && !base.CheckField(base.ADUser.UMExtension, "UMExtensionLength", (object fieldValue) => UMUriType.TelExtn != Utils.DetermineNumberType(base.ADUser.UMExtension) || base.ADUser.UMExtension.Length == this.DialPlan.NumberOfDigitsInExtension, throwOnFailure))
			{
				return false;
			}
			if (!base.CheckField(base.InternalADUMMailboxSettings, "InternalADUMMailboxSettings", UMRecipient.FieldMissingCheck, throwOnFailure))
			{
				return false;
			}
			if (!base.CheckField(this.UMMailboxPolicy, "UMMailboxPolicy", UMRecipient.FieldMissingCheck, throwOnFailure))
			{
				return false;
			}
			this.pwdPolicy = new PasswordPolicy(this.UMMailboxPolicy);
			if (!base.CheckField(this.pwdPolicy, "PasswordPolicy", UMRecipient.FieldMissingCheck, throwOnFailure))
			{
				return false;
			}
			if (!base.CheckField(this, "UMEnabled", (object fieldValue) => (base.ADUser.UMEnabledFlags & UMEnabledFlags.UMEnabled) == UMEnabledFlags.UMEnabled, throwOnFailure))
			{
				return false;
			}
			this.enabledFlags.Initialize(this, this.UMMailboxPolicy);
			return true;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002E858 File Offset: 0x0002CA58
		private CultureInfo InitTelephonyCulture()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "InitTelephonyCulture: lazy initialization", new object[0]);
			if (base.PreferredCultures.Length == 0)
			{
				return null;
			}
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
			Server serverFromName = adtopologyLookup.GetServerFromName(Utils.GetLocalHostName());
			if (serverFromName == null)
			{
				return base.PreferredCultures[0];
			}
			if ((serverFromName.CurrentServerRole & ServerRole.UnifiedMessaging) != ServerRole.UnifiedMessaging)
			{
				return base.PreferredCultures[0];
			}
			return this.InitBestPromptCulture();
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002E8C4 File Offset: 0x0002CAC4
		private CultureInfo InitBestPromptCulture()
		{
			CultureInfo preferredClientCulture = UmCultures.GetPreferredClientCulture(base.PreferredCultures);
			if (preferredClientCulture == null || !UmCultures.IsPromptCultureAvailable(preferredClientCulture))
			{
				return null;
			}
			return preferredClientCulture;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002E8EC File Offset: 0x0002CAEC
		private void LogException(LocalizedException se, string info)
		{
			PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, this.MailAddress);
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, data, "Exception trying to {1} from XSO for user=_EmailAddress. e={0}", new object[]
			{
				se,
				info
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FailedToRetrieveMailboxData, null, new object[]
			{
				CallId.Id,
				this.MailAddress,
				CommonUtil.ToEventLogString(Utils.ConcatenateMessagesOnException(se))
			});
		}

		// Token: 0x040006C3 RID: 1731
		private UMDialPlan dialPlan;

		// Token: 0x040006C4 RID: 1732
		private PasswordPolicy pwdPolicy;

		// Token: 0x040006C5 RID: 1733
		private bool authenticated;

		// Token: 0x040006C6 RID: 1734
		private UmFeatureFlags enabledFlags = default(UmFeatureFlags);

		// Token: 0x040006C7 RID: 1735
		private Lazy<CultureInfo> telephonyCulture;
	}
}
