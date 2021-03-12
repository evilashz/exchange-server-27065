using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.PersonalAutoAttendant;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000034 RID: 52
	internal class AutoAttendantCore
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00009B64 File Offset: 0x00007D64
		protected AutoAttendantCore(IAutoAttendantUI autoAttendantManager, BaseUMCallSession voiceObject)
		{
			this.VoiceObject = voiceObject;
			this.CallContext = voiceObject.CurrentCallContext;
			this.AutoAttendantManager = autoAttendantManager;
			this.startTime = ExDateTime.UtcNow;
			this.Config = voiceObject.CurrentCallContext.AutoAttendantInfo;
			this.settings = voiceObject.CurrentCallContext.CurrentAutoAttendantSettings;
			this.holidaySchedule = voiceObject.CurrentCallContext.AutoAttendantHolidaySettings;
			this.nameLookupConfigured = this.Config.NameLookupEnabled;
			this.CustomizedMenuConfigured = false;
			this.businessHoursCall = voiceObject.CurrentCallContext.AutoAttendantBusinessHourCall;
			this.transferToOperatorConfigured = Util.GetOperatorExtension(this.callContext, out this.operatorNumberConfigured);
			this.aaCoutersUtil = new AutoAttendantCountersUtil(this.VoiceObject);
			this.PerfCounters.IncrementCallTypeCounters(this.businessHoursCall);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00009C31 File Offset: 0x00007E31
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00009C39 File Offset: 0x00007E39
		public CustomMenuKeyMapping[] CustomMenu
		{
			get
			{
				return this.customMenu;
			}
			set
			{
				this.customMenu = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00009C42 File Offset: 0x00007E42
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00009C4A File Offset: 0x00007E4A
		public CustomMenuKeyMapping CustomMenuTimeoutOption
		{
			get
			{
				return this.customMenuTimeoutOption;
			}
			set
			{
				this.customMenuTimeoutOption = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00009C53 File Offset: 0x00007E53
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00009C5B File Offset: 0x00007E5B
		public int NumCustomizedMenuOptions
		{
			get
			{
				return this.numCustomizedMenuOptions;
			}
			set
			{
				this.numCustomizedMenuOptions = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009C64 File Offset: 0x00007E64
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00009C6C File Offset: 0x00007E6C
		public bool NameLookupConfigured
		{
			get
			{
				return this.nameLookupConfigured;
			}
			set
			{
				this.nameLookupConfigured = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009C75 File Offset: 0x00007E75
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00009C7D File Offset: 0x00007E7D
		public bool CustomizedMenuConfigured
		{
			get
			{
				return this.customizedMenuConfigured;
			}
			set
			{
				this.customizedMenuConfigured = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009C86 File Offset: 0x00007E86
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00009C8E File Offset: 0x00007E8E
		public UMAutoAttendant Config
		{
			get
			{
				return this.config;
			}
			set
			{
				this.config = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00009C97 File Offset: 0x00007E97
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00009C9F File Offset: 0x00007E9F
		public bool BusinessHoursCall
		{
			get
			{
				return this.businessHoursCall;
			}
			set
			{
				this.businessHoursCall = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00009CA8 File Offset: 0x00007EA8
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00009CB0 File Offset: 0x00007EB0
		public AutoAttendantSettings Settings
		{
			get
			{
				return this.settings;
			}
			set
			{
				this.settings = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009CB9 File Offset: 0x00007EB9
		public bool CustomMenuTimeoutEnabled
		{
			get
			{
				return this.customMenuTimeoutEnabled;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009CC1 File Offset: 0x00007EC1
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00009CC9 File Offset: 0x00007EC9
		public CustomMenuKeyMapping SelectedMenu
		{
			get
			{
				return this.selectedMenu;
			}
			set
			{
				this.selectedMenu = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00009CD2 File Offset: 0x00007ED2
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00009CDA File Offset: 0x00007EDA
		public bool TimeoutPending
		{
			get
			{
				return this.timeoutPending;
			}
			set
			{
				this.timeoutPending = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00009CE3 File Offset: 0x00007EE3
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00009CEB File Offset: 0x00007EEB
		internal CallContext CallContext
		{
			get
			{
				return this.callContext;
			}
			set
			{
				this.callContext = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00009CF4 File Offset: 0x00007EF4
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00009CFC File Offset: 0x00007EFC
		internal IAutoAttendantUI AutoAttendantManager
		{
			get
			{
				return this.autoAttendantManager;
			}
			set
			{
				this.autoAttendantManager = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009D05 File Offset: 0x00007F05
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00009D0D File Offset: 0x00007F0D
		internal BaseUMCallSession VoiceObject
		{
			get
			{
				return this.voiceObject;
			}
			set
			{
				this.voiceObject = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009D16 File Offset: 0x00007F16
		internal bool StarOutToDialPlanEnabled
		{
			get
			{
				return this.Config.StarOutToDialPlanEnabled;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009D23 File Offset: 0x00007F23
		internal bool ForwardCallsToDefaultMailbox
		{
			get
			{
				return this.Config.ForwardCallsToDefaultMailbox;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009D30 File Offset: 0x00007F30
		internal string BusinessName
		{
			get
			{
				return Utils.TrimSpaces(this.Config.BusinessName);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00009D42 File Offset: 0x00007F42
		protected AutoAttendantCountersUtil PerfCounters
		{
			get
			{
				return this.aaCoutersUtil;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009D4A File Offset: 0x00007F4A
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00009D52 File Offset: 0x00007F52
		protected PhoneNumber OperatorNumber
		{
			get
			{
				return this.operatorNumberConfigured;
			}
			set
			{
				this.operatorNumberConfigured = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009D5B File Offset: 0x00007F5B
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00009D63 File Offset: 0x00007F63
		protected bool OperatorEnabled
		{
			get
			{
				return this.transferToOperatorConfigured;
			}
			set
			{
				this.transferToOperatorConfigured = value;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009D6C File Offset: 0x00007F6C
		internal static bool IsRunnableAutoAttendant(UMAutoAttendant aaconfig, out LocalizedString error)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "IsRunnableAutoAttendant {0}.", new object[]
			{
				aaconfig.Name
			});
			bool flag = true;
			HolidaySchedule holidaySchedule = null;
			error = LocalizedString.Empty;
			if (aaconfig.Status == StatusEnum.Disabled)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "IsRunnableAA: Autoattendant with Name={0} Is Disabled.", new object[]
				{
					aaconfig.Name
				});
				error = Strings.DisabledAA;
				return false;
			}
			if (!aaconfig.ForwardCallsToDefaultMailbox)
			{
				AutoAttendantSettings currentSettings = aaconfig.GetCurrentSettings(out holidaySchedule, ref flag);
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(aaconfig.OrganizationId);
				UMDialPlan dialPlanFromId = iadsystemConfigurationLookup.GetDialPlanFromId(aaconfig.UMDialPlan);
				bool flag2 = false;
				PhoneNumber phoneNumber = null;
				if (currentSettings.TransferToOperatorEnabled)
				{
					flag2 = CommonUtil.GetOperatorExtensionForAutoAttendant(aaconfig, currentSettings, dialPlanFromId, false, out phoneNumber);
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "AA_Name={0} CallSomeone={1} SendVoiceMsg={2} NameLookup={3} KeyMapping={4} Status={5} OperatorEnabled={6}.", new object[]
				{
					aaconfig.Name,
					aaconfig.CallSomeoneEnabled,
					aaconfig.SendVoiceMsgEnabled,
					aaconfig.NameLookupEnabled,
					currentSettings.KeyMappingEnabled,
					aaconfig.Status,
					flag2
				});
				bool flag3;
				if (aaconfig.SpeechEnabled)
				{
					flag3 = ((aaconfig.NameLookupEnabled && (aaconfig.CallSomeoneEnabled || aaconfig.SendVoiceMsgEnabled)) || (currentSettings.KeyMappingEnabled && currentSettings.KeyMapping.Length > 0));
					if (!flag3)
					{
						error = Strings.NonFunctionalAsrAA;
					}
				}
				else
				{
					flag3 = (aaconfig.CallSomeoneEnabled || aaconfig.SendVoiceMsgEnabled || (currentSettings.KeyMappingEnabled && currentSettings.KeyMapping.Length > 0) || flag2);
					if (!flag3)
					{
						error = Strings.NonFunctionalDtmfAA;
					}
				}
				return flag3;
			}
			if (!string.IsNullOrEmpty(aaconfig.DefaultMailboxLegacyDN))
			{
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(aaconfig.OrganizationId, null);
				aaconfig.DefaultMailbox = (iadrecipientLookup.LookupByLegacyExchangeDN(aaconfig.DefaultMailboxLegacyDN) as ADUser);
			}
			if (!Utils.IsUserUMEnabledInGivenDialplan(aaconfig.DefaultMailbox, aaconfig.UMDialPlan))
			{
				error = Strings.InvalidDefaultMailboxAA;
				return false;
			}
			return true;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009F88 File Offset: 0x00008188
		internal static AutoAttendantCore Create(IAutoAttendantUI autoAttendantManager, BaseUMCallSession voiceObject)
		{
			if (autoAttendantManager is SpeechAutoAttendantManager)
			{
				return new SpeechAutoAttendant(autoAttendantManager, voiceObject);
			}
			if (autoAttendantManager is AutoAttendantManager)
			{
				return new DtmfAutoAttendant(autoAttendantManager, voiceObject);
			}
			return null;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009FAC File Offset: 0x000081AC
		internal static bool GetExtensionForKey(int key, CustomMenuKeyMapping[] km, out CustomMenuKeyMapping keyMapping)
		{
			keyMapping = null;
			for (int i = 0; i < km.Length; i++)
			{
				int mappedKey = (int)km[i].MappedKey;
				if (mappedKey == key)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "DTMF Key [{0}] Mapped to Menu option [{1}:{2}].", new object[]
					{
						key,
						km[i].Description,
						km[i].Extension
					});
					keyMapping = km[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A018 File Offset: 0x00008218
		internal static bool CheckCustomMenuTimeoutOption(CustomMenuKeyMapping[] km, out CustomMenuKeyMapping timeoutOption)
		{
			timeoutOption = null;
			for (int i = 0; i < km.Length; i++)
			{
				CustomMenuKey mappedKey = km[i].MappedKey;
				if (mappedKey == CustomMenuKey.Timeout)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "[{0}] Mapped to Menu option [{1}:{2}].", new object[]
					{
						mappedKey,
						km[i].Description,
						km[i].Extension
					});
					timeoutOption = km[i];
					return true;
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "Did not get a timeout option for this AA.", new object[0]);
			return false;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A09C File Offset: 0x0000829C
		internal static PhoneNumber GetCustomExtensionNumberToDial(CallContext cc, string number)
		{
			PhoneNumber phoneNumber = null;
			if (!PhoneNumber.TryParse(cc.DialPlan, number, out phoneNumber))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "GetCustomExtensionNumberToDial: selectedMenu.Extension ={0} is not a valid PhoneNumber", new object[]
				{
					number
				});
			}
			else
			{
				PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber.ToString());
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, data, "GetCustomExtensionNumberToDial::Trying with Phone Number = _PhoneNumber", new object[0]);
				DialingPermissionsCheck dialingPermissionsCheck = new DialingPermissionsCheck(cc.AutoAttendantInfo, cc.CurrentAutoAttendantSettings, cc.DialPlan);
				DialingPermissionsCheck.DialingPermissionsCheckResult dialingPermissionsCheckResult = dialingPermissionsCheck.CheckPhoneNumber(phoneNumber);
				if (dialingPermissionsCheckResult.AllowCall)
				{
					phoneNumber = dialingPermissionsCheckResult.NumberToDial;
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, null, "GetCustomExtensionNumberToDial::DialPermissionsCheck failed", new object[0]);
					phoneNumber = null;
				}
			}
			return phoneNumber;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A14B File Offset: 0x0000834B
		internal virtual void Initialize()
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A14D File Offset: 0x0000834D
		internal virtual void Dispose()
		{
			this.PerfCounters.ComputeSuccessRate();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A15C File Offset: 0x0000835C
		internal virtual bool ExecuteAction(string action, BaseUMCallSession voiceObject, ref string autoEvent)
		{
			bool result = false;
			if (string.Compare(action, "initializeState", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				this.Initialize();
				autoEvent = null;
			}
			else if (string.Compare(action, "setOperatorNumber", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_SetOperatorNumber();
			}
			else if (string.Compare(action, "processCustomMenuSelection", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_ProcessCustomMenuExtension();
			}
			else if (string.Compare(action, "prepareForTransferToPaa", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_PrepareForTransferToPaa();
			}
			else if (string.Compare(action, "setCustomMenuAutoAttendant", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_ProcessCustomMenuAutoAttendant();
			}
			else if (string.Compare(action, "setCustomMenuTargetPAA", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_ProcessCustomMenuTargetPAA();
			}
			else if (string.Compare(action, "setCustomMenuVoicemailTarget", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_ProcessCustomMenuVoicemailTarget();
			}
			else if (string.Compare(action, "transferToPAASiteFailed", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_TransferToPAASiteFailed();
			}
			else if (string.Compare(action, "setCustomExtensionNumber", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_ProcessCustomMenuTransferToNumber();
			}
			else if (string.Compare(action, "handleFaxTone", true, CultureInfo.InvariantCulture) == 0)
			{
				autoEvent = this.Action_OnFaxTone();
				result = true;
			}
			else if (string.Compare(action, "prepareForProtectedSubscriberOperatorTransfer", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_PrepareForProtectedSubscriberOperatorTransfer();
			}
			else if (string.Compare(action, "prepareForTransferToSendMessage", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_PrepareForTransferToSendMessage();
			}
			else if (string.Compare(action, "prepareForTransferToKeyMappingAutoAttendant", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_PrepareForTransferToKeyMappingAutoAttendant();
			}
			else if (string.Compare(action, "prepareForTransferToDtmfFallbackAutoAttendant", true, CultureInfo.InvariantCulture) == 0)
			{
				result = true;
				autoEvent = this.Action_PrepareForTransferToDtmfFallbackAutoAttendant();
			}
			else if (string.Compare(action, "prepareForUserInitiatedOperatorTransfer", true, CultureInfo.InvariantCulture) == 0)
			{
				this.PerfCounters.IncrementUserInitiatedTransferToOperatorCounter();
				result = true;
				autoEvent = null;
			}
			else if (string.Compare(action, "prepareForUserInitiatedOperatorTransferFromOpeningMenu", true, CultureInfo.InvariantCulture) == 0)
			{
				this.PerfCounters.IncrementUserInitiatedTransferToOperatorFromMainMenuCounter();
				result = true;
				autoEvent = null;
			}
			return result;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A38B File Offset: 0x0000858B
		internal string Action_PrepareForProtectedSubscriberOperatorTransfer()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Action_PrepareForProtectedSubscriberOperatorTransfer().", new object[0]);
			this.PerfCounters.IncrementDisallowedTransferCalls();
			return null;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A3AF File Offset: 0x000085AF
		internal string Action_PrepareForTransferToSendMessage()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Action_PrepareForTransferToSendMessage().", new object[0]);
			this.PerfCounters.IncrementTransfersToSendMessageCounter();
			return null;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A3D3 File Offset: 0x000085D3
		internal string Action_PrepareForTransferToKeyMappingAutoAttendant()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Action_PrepareForTransferToKeyMappingAutoAttendant().", new object[0]);
			this.UpdateCountersOnTransferToAA(false);
			return null;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A3F3 File Offset: 0x000085F3
		internal string Action_PrepareForTransferToDtmfFallbackAutoAttendant()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Action_PrepareForTransferToDtmfFallbackAutoAttendant().", new object[0]);
			this.UpdateCountersOnTransferToAA(true);
			return null;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A414 File Offset: 0x00008614
		internal string Action_OnFaxTone()
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMNumberNotConfiguredForFax, null, new object[]
			{
				this.VoiceObject.CurrentCallContext.Extension,
				this.VoiceObject.CurrentCallContext.CallerId
			});
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "AutoAttendant got FaxTone. Dropping Call", new object[0]);
			((ActivityManager)this.AutoAttendantManager).DropCall(this.VoiceObject, DropCallReason.UserError);
			return "stopEvent";
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A492 File Offset: 0x00008692
		internal string Action_SetOperatorNumber()
		{
			PhoneUtil.SetTransferTargetPhone((ActivityManager)this.AutoAttendantManager, TransferExtension.Operator, this.OperatorNumber);
			return null;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A4AC File Offset: 0x000086AC
		internal virtual string Action_ProcessCustomMenuExtension()
		{
			string dtmfDigits = ((ActivityManager)this.AutoAttendantManager).DtmfDigits;
			string result = null;
			bool flag = false;
			if (this.TimeoutPending)
			{
				flag = true;
				this.TimeoutPending = false;
				this.selectedMenu = this.customMenuTimeoutOption;
				this.selectedMenu = this.customMenuTimeoutOption;
			}
			else if (Constants.RegularExpressions.ValidDigitRegex.IsMatch(dtmfDigits))
			{
				int key = int.Parse(dtmfDigits, CultureInfo.InvariantCulture);
				CustomMenuKeyMapping[] keyMapping = this.settings.KeyMapping;
				CustomMenuKeyMapping customMenuKeyMapping;
				if (AutoAttendantCore.GetExtensionForKey(key, keyMapping, out customMenuKeyMapping))
				{
					this.selectedMenu = customMenuKeyMapping;
					flag = true;
				}
				else
				{
					result = "invalidOption";
					this.WriteVariable("invalidExtension", dtmfDigits);
				}
			}
			else
			{
				result = "invalidOption";
				this.WriteVariable("invalidExtension", dtmfDigits);
			}
			if (flag)
			{
				this.PerfCounters.Increment(DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccess);
				this.IncrementKeyMappingPerfCounters(this.selectedMenu.MappedKey);
				string promptFileName = this.selectedMenu.PromptFileName;
				bool flag2 = !string.IsNullOrEmpty(promptFileName);
				this.WriteVariable("haveCustomMenuOptionPrompt", flag2);
				if (flag2)
				{
					this.WriteVariable("customMenuOptionPrompt", this.CallContext.UMConfigCache.GetPrompt<UMAutoAttendant>(this.Config, promptFileName));
				}
				AutoAttendantCustomOptionType autoAttendantCustomOptionType = AutoAttendantCustomOptionType.None;
				if (!string.IsNullOrEmpty(this.selectedMenu.AutoAttendantName))
				{
					autoAttendantCustomOptionType = AutoAttendantCustomOptionType.TransferToAutoAttendant;
				}
				else if (!string.IsNullOrEmpty(this.selectedMenu.Extension))
				{
					autoAttendantCustomOptionType = AutoAttendantCustomOptionType.TransferToExtension;
				}
				else if (!string.IsNullOrEmpty(this.selectedMenu.LegacyDNToUseForLeaveVoicemailFor))
				{
					autoAttendantCustomOptionType = AutoAttendantCustomOptionType.TransferToVoicemailDirectly;
				}
				else if (!string.IsNullOrEmpty(this.selectedMenu.LegacyDNToUseForTransferToMailbox))
				{
					autoAttendantCustomOptionType = AutoAttendantCustomOptionType.TransferToVoicemailPAA;
				}
				else if (!string.IsNullOrEmpty(this.selectedMenu.AnnounceBusinessLocation))
				{
					autoAttendantCustomOptionType = AutoAttendantCustomOptionType.ReadBusinessLocation;
				}
				else if (!string.IsNullOrEmpty(this.selectedMenu.AnnounceBusinessHours))
				{
					autoAttendantCustomOptionType = AutoAttendantCustomOptionType.ReadBusinessHours;
				}
				this.WriteVariable("customMenuOption", autoAttendantCustomOptionType.ToString());
			}
			return result;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A678 File Offset: 0x00008878
		internal string Action_ProcessCustomMenuTargetPAA()
		{
			string customMenuTargetPAA = this.GetCustomMenuTargetPAA();
			return this.SwitchToTargetPAA(customMenuTargetPAA);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A694 File Offset: 0x00008894
		internal string Action_ProcessCustomMenuVoicemailTarget()
		{
			string customMenuVoicemailTarget = this.GetCustomMenuVoicemailTarget();
			return this.SwitchToVoicemailTarget(customMenuVoicemailTarget);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A6AF File Offset: 0x000088AF
		internal string Action_TransferToPAASiteFailed()
		{
			this.WriteGlobalVariable("directorySearchResult", ContactSearchItem.CreateFromRecipient(this.mailboxTransferTarget));
			return null;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A6C8 File Offset: 0x000088C8
		internal string Action_ProcessCustomMenuAutoAttendant()
		{
			string customMenuAutoAttendant = this.GetCustomMenuAutoAttendant();
			return this.SwitchToAutoAttendant(customMenuAutoAttendant);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A6E3 File Offset: 0x000088E3
		internal virtual string GetCustomMenuAutoAttendant()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "AutoAttendantCore::GetCustomMenuAutoAttendant().", new object[0]);
			return this.selectedMenu.AutoAttendantName;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A706 File Offset: 0x00008906
		internal virtual string GetCustomMenuVoicemailTarget()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "AutoAttendantCore::GetCustomMenuVoicemailTarget().", new object[0]);
			return this.selectedMenu.LegacyDNToUseForLeaveVoicemailFor;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A729 File Offset: 0x00008929
		internal virtual string GetCustomMenuTargetPAA()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "AutoAttendantCore::GetCustomMenuTargetPAA().", new object[0]);
			return this.selectedMenu.LegacyDNToUseForTransferToMailbox;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A74C File Offset: 0x0000894C
		internal virtual void Configure()
		{
			this.ConfigureCommonVariables();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A754 File Offset: 0x00008954
		internal void ConfigureCommonVariables()
		{
			this.WriteVariable("aa_isBusinessHours", this.businessHoursCall);
			if (this.CallContext.AutoAttendantHolidaySettings != null)
			{
				this.WriteVariable("holidayHours", true);
				this.WriteVariable("holidayIntroductoryGreetingPrompt", this.CallContext.UMConfigCache.GetPrompt<UMAutoAttendant>(this.Config, this.holidaySchedule.Greeting));
			}
			else
			{
				this.WriteVariable("holidayHours", false);
				this.WriteVariable("holidayIntroductoryGreetingPrompt", null);
			}
			this.WriteVariable("infoAnnouncementEnabled", this.Config.InfoAnnouncementEnabled.ToString());
			if (this.Config.InfoAnnouncementEnabled != InfoAnnouncementEnabledEnum.False)
			{
				this.WriteVariable("infoAnnouncementFilename", this.CallContext.UMConfigCache.GetPrompt<UMAutoAttendant>(this.Config, this.Config.InfoAnnouncementFilename));
			}
			bool mainMenuCustomPromptEnabled = this.settings.MainMenuCustomPromptEnabled;
			this.WriteVariable("mainMenuCustomPromptEnabled", mainMenuCustomPromptEnabled);
			this.WriteVariable("mainMenuCustomPromptFilename", this.CallContext.UMConfigCache.GetPrompt<UMAutoAttendant>(this.Config, this.settings.MainMenuCustomPromptFilename));
			CustomMenuKeyMapping[] keyMapping = this.settings.KeyMapping;
			this.NumCustomizedMenuOptions = ((keyMapping != null) ? keyMapping.Length : 0);
			bool keyMappingEnabled = this.settings.KeyMappingEnabled;
			this.CustomizedMenuConfigured = false;
			if (keyMappingEnabled && this.NumCustomizedMenuOptions > 0)
			{
				this.CustomizedMenuConfigured = true;
				this.CustomMenu = keyMapping;
				this.customMenuTimeoutEnabled = AutoAttendantCore.CheckCustomMenuTimeoutOption(keyMapping, out this.customMenuTimeoutOption);
				this.ConfigureCustomMenuDtmfKeyInput(keyMapping);
			}
			else
			{
				this.CustomizedMenuConfigured = false;
				this.NumCustomizedMenuOptions = 0;
				this.CustomMenu = null;
			}
			this.WriteVariable("customizedMenuOptions", this.NumCustomizedMenuOptions);
			this.WriteVariable("aa_customizedMenuEnabled", this.CustomizedMenuConfigured);
			this.WriteVariable("aa_callSomeoneEnabled", this.Config.CallSomeoneEnabled);
			bool flag = this.Config.CallSomeoneEnabled || this.Config.SendVoiceMsgEnabled;
			this.WriteVariable("aa_contactSomeoneEnabled", flag);
			this.WriteVariable("aa_transferToOperatorEnabled", this.OperatorEnabled);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A988 File Offset: 0x00008B88
		internal void ConfigureCustomMenuDtmfKeyInput(CustomMenuKeyMapping[] customExtensions)
		{
			int num = 0;
			foreach (CustomMenuKeyMapping customMenuKeyMapping in customExtensions)
			{
				num++;
				if (customMenuKeyMapping.MappedKey == CustomMenuKey.Timeout)
				{
					this.customMenuTimeoutEnabled = true;
					this.customMenuTimeoutOption = customMenuKeyMapping;
					this.WriteVariable("TimeoutOption", true);
				}
				else
				{
					num = (int)customMenuKeyMapping.MappedKey;
					this.WriteVariable(string.Format(CultureInfo.InvariantCulture, "DtmfKey{0}", new object[]
					{
						num
					}), true);
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000AA10 File Offset: 0x00008C10
		internal virtual void OnTimeout()
		{
			if (this.customMenuTimeoutEnabled)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "setting timeoutPending = true.", new object[0]);
				this.TimeoutPending = true;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AA37 File Offset: 0x00008C37
		internal virtual void OnInput()
		{
			this.receivedInput = true;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000AA40 File Offset: 0x00008C40
		internal virtual void OnHangup()
		{
			this.PerfCounters.UpdateAverageTimeCounters(this.startTime);
			if (!this.receivedInput)
			{
				this.VoiceObject.IncrementCounter(this.PerfCounters.GetInstance().DisconnectedWithoutInput);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000AA76 File Offset: 0x00008C76
		internal virtual void OnTransferComplete(TransferExtension ext)
		{
			this.PerfCounters.IncrementTransferCounters(ext);
			this.PerfCounters.UpdateAverageTimeCounters(this.startTime);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000AA95 File Offset: 0x00008C95
		internal virtual void OnSpeech()
		{
			this.receivedInput = true;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000AA9E File Offset: 0x00008C9E
		internal virtual void OnNameSpoken()
		{
			this.PerfCounters.Increment(DirectoryAccessCountersUtil.DirectoryAccessCounter.DirectoryAccessedBySpokenName);
			if (!this.nameSpokenAtleastOnce)
			{
				this.nameSpokenAtleastOnce = true;
				this.PerfCounters.IncrementNameSpokenCounter();
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		internal string Action_PrepareForTransferToPaa()
		{
			if (this.targetPAA == null)
			{
				throw new InvalidOperationException("Got a NULL TargetPAA");
			}
			PIIMessage data = PIIMessage.Create(PIIType._PII, this.mailboxTransferTarget.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, data, "AutoAttendantCore::Action_PrepareForTransferToPaa: Target Mailbox = _PII, Target PAA = {0}", new object[]
			{
				this.targetPAA.Identity.ToString()
			});
			this.WriteGlobalVariable("TargetPAA", this.targetPAA);
			return null;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000AB44 File Offset: 0x00008D44
		internal string Action_ProcessCustomMenuTransferToNumber()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "AutoAttendantCore::Action_ProcessCustomMenuTransferToNumber()", new object[0]);
			PhoneNumber customExtensionNumberToDial = AutoAttendantCore.GetCustomExtensionNumberToDial(this.CallContext, this.selectedMenu.Extension);
			if (customExtensionNumberToDial == null)
			{
				return "cannotTransferToCustomExtension";
			}
			PhoneUtil.SetTransferTargetPhone((ActivityManager)this.AutoAttendantManager, TransferExtension.CustomMenuExtension, customExtensionNumberToDial);
			return null;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000AB9C File Offset: 0x00008D9C
		internal string Action_PrepareForCallAnswering()
		{
			UMAutoAttendant autoAttendantInfo = this.CallContext.AutoAttendantInfo;
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "Action_PrepareForCallAnswering: DefaultMailbox = {0}", new object[]
			{
				autoAttendantInfo.DefaultMailbox
			});
			if (!autoAttendantInfo.ForwardCallsToDefaultMailbox)
			{
				throw new InvalidOperationException("ForwardCallsToDefaultMailbox is false");
			}
			if (autoAttendantInfo.DefaultMailbox == null)
			{
				throw new InvalidOperationException("DefaultMailbox is null");
			}
			UMSubscriber umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(autoAttendantInfo.DefaultMailbox);
			if (umsubscriber == null)
			{
				throw new InvalidOperationException("Could not create UMSubscriber from DefaultMailbox");
			}
			this.CallContext.SwitchToCallAnswering(umsubscriber);
			return null;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000AC23 File Offset: 0x00008E23
		protected void WriteVariable(string name, object value)
		{
			this.AutoAttendantManager.WriteProperty(name, value);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000AC32 File Offset: 0x00008E32
		protected object ReadVariable(string name)
		{
			return this.AutoAttendantManager.ReadProperty(name);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000AC40 File Offset: 0x00008E40
		protected void WriteGlobalVariable(string name, object value)
		{
			this.AutoAttendantManager.WriteGlobalProperty(name, value);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AC4F File Offset: 0x00008E4F
		protected object ReadGlobalVariable(string name)
		{
			return this.AutoAttendantManager.ReadGlobalProperty(name);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AC5D File Offset: 0x00008E5D
		protected void IncrementKeyMappingPerfCounters(CustomMenuKey keyPress)
		{
			this.PerfCounters.IncrementCustomMenuCounters(keyPress);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AC6C File Offset: 0x00008E6C
		protected string SwitchToAutoAttendant(string autoAttendantName)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "AutoAttendantCore::SwitchToAutoAttendant().", new object[0]);
			string result = null;
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.Config.OrganizationId);
			UMAutoAttendant autoAttendantFromName = iadsystemConfigurationLookup.GetAutoAttendantFromName(autoAttendantName);
			if (autoAttendantFromName != null && !this.CallContext.SwitchToAutoAttendant(autoAttendantFromName.Id, autoAttendantFromName.OrganizationId))
			{
				result = "fallbackAutoAttendantFailure";
			}
			return result;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		private string SwitchToTargetPAA(string user)
		{
			ADRecipient adrecipient;
			string text = this.TryEvaluateUser(user, out adrecipient);
			if (text != null)
			{
				return text;
			}
			this.mailboxTransferTarget = adrecipient;
			bool flag = false;
			PersonalAutoAttendant personalAutoAttendant = null;
			BricksRoutingBasedServerChooser bricksRoutingBasedServerChooser = null;
			bool flag2 = PersonalAutoAttendantManager.TryGetTargetPAA(this.VoiceObject.CurrentCallContext, adrecipient, this.VoiceObject.CurrentCallContext.DialPlan, this.VoiceObject.CurrentCallContext.CallerId, out personalAutoAttendant, out flag, out bricksRoutingBasedServerChooser);
			PIIMessage data = PIIMessage.Create(PIIType._PII, this.mailboxTransferTarget.DisplayName);
			if (flag2)
			{
				this.targetPAA = personalAutoAttendant;
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, data, "AutoAttendantCore::SwitchToTargetPAA: Target Mailbox = _PII, Target PAA = {0}", new object[]
				{
					this.targetPAA.Identity.ToString()
				});
				this.VoiceObject.CurrentCallContext.CalleeInfo = UMRecipient.Factory.FromADRecipient<UMRecipient>(adrecipient);
				this.VoiceObject.CurrentCallContext.AsyncGetCallAnsweringData(false);
				return null;
			}
			if (flag)
			{
				ExAssert.RetailAssert(bricksRoutingBasedServerChooser != null, "ServerPicker cannot be null if transferToAnotherServer is needed");
				this.VoiceObject.CurrentCallContext.ServerPicker = bricksRoutingBasedServerChooser;
				UserTransferWithContext userTransferWithContext = new UserTransferWithContext(this.VoiceObject.CurrentCallContext.CallInfo.ApplicationAor);
				this.WriteGlobalVariable("ReferredByUri", userTransferWithContext.SerializeCACallTransferWithContextUri(adrecipient.UMExtension, this.VoiceObject.CurrentCallContext.DialPlan.PhoneContext));
				this.WriteGlobalVariable("transferExtension", TransferExtension.CustomMenuExtension);
				return "targetPAAInDifferentSite";
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, data, "AutoAttendantCore::SwitchToTargetPAA: NO PAA found for Target Mailbox = _PII", new object[0]);
			this.WriteGlobalVariable("directorySearchResult", ContactSearchItem.CreateFromRecipient(adrecipient));
			return "noPAAFound";
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AE70 File Offset: 0x00009070
		private string SwitchToVoicemailTarget(string user)
		{
			ADRecipient r;
			string text = this.TryEvaluateUser(user, out r);
			if (text == null)
			{
				this.WriteGlobalVariable("directorySearchResult", ContactSearchItem.CreateFromRecipient(r));
				return text;
			}
			return text;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000AEA0 File Offset: 0x000090A0
		private string TryEvaluateUser(string user, out ADRecipient recipient)
		{
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(this.config.OrganizationId, null);
			recipient = iadrecipientLookup.LookupByLegacyExchangeDN(user);
			PIIMessage data = PIIMessage.Create(PIIType._User, user);
			if (recipient == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, data, "AutoAttendantCore::TryEvaluateUser: NO user found for = _User", new object[0]);
				return "noResultsMatched";
			}
			ADUser user2 = recipient as ADUser;
			if (!Utils.IsUserUMEnabledInGivenDialplan(user2, this.config.UMDialPlan))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, data, "AutoAttendantCore::TryEvaluateUser: user not umenabled or not in same dialplan = _User", new object[0]);
				return "invalidOption";
			}
			return null;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AF2C File Offset: 0x0000912C
		private void UpdateCountersOnTransferToAA(bool dtmfFallbackAA)
		{
			if (dtmfFallbackAA)
			{
				this.PerfCounters.IncrementTransfersToDtmfFallbackAutoAttendantCounter();
			}
			else
			{
				this.PerfCounters.IncrementTransferToKeyMappingAutoAttendantCounter();
			}
			this.PerfCounters.UpdateAverageTimeCounters(this.startTime);
		}

		// Token: 0x040000A8 RID: 168
		private bool receivedInput;

		// Token: 0x040000A9 RID: 169
		private CallContext callContext;

		// Token: 0x040000AA RID: 170
		private IAutoAttendantUI autoAttendantManager;

		// Token: 0x040000AB RID: 171
		private BaseUMCallSession voiceObject;

		// Token: 0x040000AC RID: 172
		private bool customMenuTimeoutEnabled;

		// Token: 0x040000AD RID: 173
		private CustomMenuKeyMapping customMenuTimeoutOption;

		// Token: 0x040000AE RID: 174
		private CustomMenuKeyMapping selectedMenu;

		// Token: 0x040000AF RID: 175
		private PersonalAutoAttendant targetPAA;

		// Token: 0x040000B0 RID: 176
		private ADRecipient mailboxTransferTarget;

		// Token: 0x040000B1 RID: 177
		private bool timeoutPending;

		// Token: 0x040000B2 RID: 178
		private PhoneNumber operatorNumberConfigured;

		// Token: 0x040000B3 RID: 179
		private bool transferToOperatorConfigured;

		// Token: 0x040000B4 RID: 180
		private CustomMenuKeyMapping[] customMenu;

		// Token: 0x040000B5 RID: 181
		private int numCustomizedMenuOptions;

		// Token: 0x040000B6 RID: 182
		private bool nameLookupConfigured;

		// Token: 0x040000B7 RID: 183
		private bool customizedMenuConfigured;

		// Token: 0x040000B8 RID: 184
		private bool businessHoursCall;

		// Token: 0x040000B9 RID: 185
		private UMAutoAttendant config;

		// Token: 0x040000BA RID: 186
		private AutoAttendantSettings settings;

		// Token: 0x040000BB RID: 187
		private HolidaySchedule holidaySchedule;

		// Token: 0x040000BC RID: 188
		private ExDateTime startTime;

		// Token: 0x040000BD RID: 189
		private bool nameSpokenAtleastOnce;

		// Token: 0x040000BE RID: 190
		private AutoAttendantCountersUtil aaCoutersUtil;
	}
}
