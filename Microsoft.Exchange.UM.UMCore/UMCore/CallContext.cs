using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200005D RID: 93
	internal sealed class CallContext : DisposableBase, IRoutingContext
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		public CallContext()
		{
			base.SuppressDisposeTracker();
			this.callLogHelper = new CallLoggingHelper(this);
			this.IncrementCounter(AvailabilityCounters.TotalWorkerProcessCallCount);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00011E85 File Offset: 0x00010085
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00011E8D File Offset: 0x0001008D
		public SipRoutingHelper RoutingHelper { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00011E96 File Offset: 0x00010096
		public string CallId
		{
			get
			{
				return this.callId;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00011E9E File Offset: 0x0001009E
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00011EA6 File Offset: 0x000100A6
		public UMDialPlan DialPlan
		{
			get
			{
				return this.dialPlan;
			}
			set
			{
				this.dialPlan = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00011EAF File Offset: 0x000100AF
		public bool IsSecuredCall
		{
			get
			{
				return this.securedCall;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00011EB7 File Offset: 0x000100B7
		public PlatformSipUri RequestUriOfCall
		{
			get
			{
				return this.requestUri;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00011EBF File Offset: 0x000100BF
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00011EC7 File Offset: 0x000100C7
		public Guid TenantGuid { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00011ED0 File Offset: 0x000100D0
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00011ED8 File Offset: 0x000100D8
		internal bool IsTroubleshootingToolCall { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00011EE1 File Offset: 0x000100E1
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00011EE9 File Offset: 0x000100E9
		internal bool IsActiveMonitoringCall { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00011EF2 File Offset: 0x000100F2
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00011EFA File Offset: 0x000100FA
		internal PlatformCallInfo CallInfo { get; private set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00011F03 File Offset: 0x00010103
		internal bool DivertedExtensionAllowVoiceMail
		{
			get
			{
				return this.divertedExtensionAllowVoiceMail;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00011F0B File Offset: 0x0001010B
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00011F13 File Offset: 0x00010113
		internal UMRecipient LegacySubscriber
		{
			get
			{
				return this.unsupportedSubscriber;
			}
			set
			{
				this.UpdateDisposableInstance<UMRecipient>(ref this.unsupportedSubscriber, value, "LegacySubscriber");
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00011F27 File Offset: 0x00010127
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00011F2F File Offset: 0x0001012F
		internal IRedirectTargetChooser ServerPicker
		{
			get
			{
				return this.serverPicker;
			}
			set
			{
				this.serverPicker = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00011F38 File Offset: 0x00010138
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00011F40 File Offset: 0x00010140
		internal NonBlockingCallAnsweringData UmSubscriberData
		{
			get
			{
				return this.subscriberData;
			}
			set
			{
				this.UpdateDisposableInstance<NonBlockingCallAnsweringData>(ref this.subscriberData, value, "UmSubscriberData");
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00011F54 File Offset: 0x00010154
		internal bool IsPlayOnPhoneCall
		{
			get
			{
				return this.CallType == 5;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00011F5F File Offset: 0x0001015F
		internal bool IsAutoAttendantCall
		{
			get
			{
				return this.CallType == 2;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00011F6A File Offset: 0x0001016A
		internal bool IsSubscriberAccessCall
		{
			get
			{
				return this.CallType == 3;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00011F75 File Offset: 0x00010175
		internal bool IsPromptProvisioningCall
		{
			get
			{
				return this.CallType == 8;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00011F80 File Offset: 0x00010180
		internal bool IsVirtualNumberCall
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00011F83 File Offset: 0x00010183
		internal bool IsFindMeSubscriberCall
		{
			get
			{
				return this.CallType == 9;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00011F8F File Offset: 0x0001018F
		internal bool IsPlayOnPhonePAAGreetingCall
		{
			get
			{
				return this.CallType == 10;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00011F9B File Offset: 0x0001019B
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00011FA3 File Offset: 0x000101A3
		internal bool FaxToneReceived
		{
			get
			{
				return this.faxToneReceived;
			}
			set
			{
				this.faxToneReceived = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00011FAC File Offset: 0x000101AC
		internal string RemoteUserAgent
		{
			get
			{
				return this.CallInfo.RemoteUserAgent;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00011FB9 File Offset: 0x000101B9
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00011FC1 File Offset: 0x000101C1
		internal PlayOnPhoneRequest WebServiceRequest
		{
			get
			{
				return this.webServiceRequest;
			}
			set
			{
				this.webServiceRequest = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00011FCA File Offset: 0x000101CA
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00011FD2 File Offset: 0x000101D2
		internal bool IsDiagnosticCall
		{
			get
			{
				return this.callIsDiagnostic;
			}
			set
			{
				this.callIsDiagnostic = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00011FDB File Offset: 0x000101DB
		internal bool IsLocalDiagnosticCall
		{
			get
			{
				return this.callIsLocalDiagnostic;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00011FE3 File Offset: 0x000101E3
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00011FEB File Offset: 0x000101EB
		internal bool IsTUIDiagnosticCall
		{
			get
			{
				return this.callIsTUIDiagnostic;
			}
			set
			{
				this.callIsTUIDiagnostic = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00011FF4 File Offset: 0x000101F4
		internal bool IsAnonymousCaller
		{
			get
			{
				return this.callerId.IsEmpty;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00012001 File Offset: 0x00010201
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00012009 File Offset: 0x00010209
		internal PhoneNumber CallerId
		{
			get
			{
				return this.callerId;
			}
			set
			{
				this.callerId = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00012012 File Offset: 0x00010212
		internal string CallerIdDisplayName
		{
			get
			{
				return this.callerIdDisplayName;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0001201A File Offset: 0x0001021A
		internal PhoneNumber CalleeId
		{
			get
			{
				return this.calleeId;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00012022 File Offset: 0x00010222
		internal string Extension
		{
			get
			{
				return this.extnNumber;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001202A File Offset: 0x0001022A
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00012032 File Offset: 0x00010232
		internal Exception CallRejectionException { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0001203B File Offset: 0x0001023B
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00012043 File Offset: 0x00010243
		internal UMSubscriber CallerInfo
		{
			get
			{
				return this.callerInfo;
			}
			set
			{
				this.UpdateDisposableInstance<UMSubscriber>(ref this.callerInfo, value, "CallerInfo");
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00012057 File Offset: 0x00010257
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00012060 File Offset: 0x00010260
		internal UMRecipient CalleeInfo
		{
			get
			{
				return this.calleeInfo;
			}
			set
			{
				this.UpdateDisposableInstance<UMRecipient>(ref this.calleeInfo, value, string.Empty);
				UMSubscriber umsubscriber = value as UMSubscriber;
				if (umsubscriber != null && !string.IsNullOrEmpty(umsubscriber.Extension))
				{
					this.calleeId = new PhoneNumber(umsubscriber.Extension);
					this.extnNumber = this.calleeId.ToDial;
				}
				else
				{
					this.calleeId = PhoneNumber.Empty;
					this.extnNumber = string.Empty;
				}
				PIIMessage[] data = new PIIMessage[]
				{
					PIIMessage.Create(PIIType._Callee, this.CalleeInfo),
					PIIMessage.Create(PIIType._User, this.calleeId),
					PIIMessage.Create(PIIType._PhoneNumber, this.extnNumber)
				};
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "Changed CalleeInfo to:_Callee, CalleeId:_User, Extension:_PhoneNumber", new object[0]);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00012120 File Offset: 0x00010320
		internal CultureInfo Culture
		{
			get
			{
				CultureInfo result = CommonConstants.DefaultCulture;
				if (this.autoAttendantInfo != null)
				{
					if (this.autoAttendantCulture == null)
					{
						this.autoAttendantCulture = this.autoAttendantInfo.Language.Culture;
						List<CultureInfo> supportedPromptCultures = UmCultures.GetSupportedPromptCultures();
						if (!supportedPromptCultures.Contains(this.autoAttendantCulture) && this.DialPlan != null)
						{
							CultureInfo defaultCulture = Util.GetDefaultCulture(this.DialPlan);
							UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AALanguageNotFound, null, new object[]
							{
								this.autoAttendantInfo.Name,
								this.autoAttendantCulture.Name,
								defaultCulture.Name,
								this.DialPlan.Name
							});
							this.autoAttendantCulture = defaultCulture;
						}
					}
					result = this.autoAttendantCulture;
				}
				else if (this.CallerInfo != null && this.CallerInfo.TelephonyCulture != null)
				{
					result = this.CallerInfo.TelephonyCulture;
				}
				else if (this.dialPlanCulture != null)
				{
					result = this.dialPlanCulture;
				}
				else if (this.DialPlan != null)
				{
					this.dialPlanCulture = Util.GetDefaultCulture(this.DialPlan);
					result = this.dialPlanCulture;
				}
				return result;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00012238 File Offset: 0x00010438
		internal bool IsTestCall
		{
			get
			{
				return this.callIsTest;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00012240 File Offset: 0x00010440
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00012248 File Offset: 0x00010448
		internal bool HasVoicemailBeenSubmitted
		{
			get
			{
				return this.hasVoicemailBeenSubmitted;
			}
			set
			{
				this.hasVoicemailBeenSubmitted = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00012251 File Offset: 0x00010451
		internal OCFeature OCFeature
		{
			get
			{
				return this.officeCommunicatorFeature;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00012259 File Offset: 0x00010459
		internal UMAutoAttendant AutoAttendantInfo
		{
			get
			{
				return this.autoAttendantInfo;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00012261 File Offset: 0x00010461
		internal AutoAttendantSettings CurrentAutoAttendantSettings
		{
			get
			{
				return this.autoAttendantSettings;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00012269 File Offset: 0x00010469
		internal bool AutoAttendantBusinessHourCall
		{
			get
			{
				return this.businessHour;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00012271 File Offset: 0x00010471
		internal HolidaySchedule AutoAttendantHolidaySettings
		{
			get
			{
				return this.holidaySettings;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00012279 File Offset: 0x00010479
		internal ExDateTime CallReceivedTime
		{
			get
			{
				return this.callStartTime;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00012281 File Offset: 0x00010481
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00012289 File Offset: 0x00010489
		internal ExDateTime? ConnectionTime
		{
			get
			{
				return this.connectionTime;
			}
			set
			{
				this.connectionTime = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00012292 File Offset: 0x00010492
		internal UMSmartHost GatewayDetails
		{
			get
			{
				return this.gatewayDetails;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001229A File Offset: 0x0001049A
		internal IPAddress ImmediatePeer
		{
			get
			{
				return this.immediatePeer;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000122A2 File Offset: 0x000104A2
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x000122AA File Offset: 0x000104AA
		internal UMIPGateway GatewayConfig
		{
			get
			{
				return this.gatewayConfig;
			}
			set
			{
				this.gatewayConfig = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000122B3 File Offset: 0x000104B3
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x000122BB File Offset: 0x000104BB
		internal string RemoteFQDN { get; private set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000122C4 File Offset: 0x000104C4
		internal bool IsCallAnswerCallsCounterIncremented
		{
			get
			{
				return this.IsCounterIncremented(CallAnswerCounters.CallAnsweringCalls);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000122D1 File Offset: 0x000104D1
		internal bool IsDelayedCallsCounterIncremented
		{
			get
			{
				return this.IsCounterIncremented(GeneralCounters.DelayedCalls);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000122DE File Offset: 0x000104DE
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x000122E6 File Offset: 0x000104E6
		internal ReasonForCall ReasonForCall
		{
			get
			{
				return this.reasonForCall;
			}
			set
			{
				this.reasonForCall = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000122EF File Offset: 0x000104EF
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x000122F7 File Offset: 0x000104F7
		internal DropCallReason ReasonForDisconnect
		{
			get
			{
				return this.reasonForDisconnect;
			}
			set
			{
				this.reasonForDisconnect = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00012300 File Offset: 0x00010500
		internal string ReferredByHeader
		{
			get
			{
				return this.referredByHeader;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00012308 File Offset: 0x00010508
		internal bool CallIsOVATransferForUMSubscriber
		{
			get
			{
				return this.callIsOVATransferForUMSubscriber;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00012310 File Offset: 0x00010510
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00012318 File Offset: 0x00010518
		internal OfferResult OfferResult
		{
			get
			{
				return this.offerResult;
			}
			set
			{
				this.offerResult = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00012321 File Offset: 0x00010521
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0001232C File Offset: 0x0001052C
		internal CallType CallType
		{
			get
			{
				return this.callType;
			}
			set
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Setting CallType: Old {0}, New {1}.", new object[]
				{
					this.CallType,
					value
				});
				if (this.callType == null)
				{
					this.callLogHelper.CallsInProgressWhenStarted = (long)((int)GeneralCounters.CurrentCalls.RawValue);
				}
				this.callType = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0001238E File Offset: 0x0001058E
		internal PlatformSipUri FromUriOfCall
		{
			get
			{
				if (this.fromAddress == null)
				{
					return null;
				}
				return this.fromAddress.Uri;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000123A5 File Offset: 0x000105A5
		internal PlatformSipUri ToUriOfCall
		{
			get
			{
				if (this.toAddress == null)
				{
					return null;
				}
				return this.toAddress.Uri;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000123BC File Offset: 0x000105BC
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x000123C4 File Offset: 0x000105C4
		internal IPAACommonInterface LinkedManagerPointer
		{
			get
			{
				return this.linkedManagerPointer;
			}
			set
			{
				this.linkedManagerPointer = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x000123CD File Offset: 0x000105CD
		internal UMConfigCache UMConfigCache
		{
			get
			{
				return this.promptConfigCache;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000123D5 File Offset: 0x000105D5
		internal IDictionary<ExPerformanceCounter, long> IncrementedCounters
		{
			get
			{
				return this.incrementedCounters;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x000123DD File Offset: 0x000105DD
		internal CallLoggingHelper CallLoggingHelper
		{
			get
			{
				return this.callLogHelper;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000123E5 File Offset: 0x000105E5
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x000123ED File Offset: 0x000105ED
		public string UMPodRedirectTemplate { get; set; }

		// Token: 0x0600040D RID: 1037 RVA: 0x000123F8 File Offset: 0x000105F8
		internal static void UpdateCountersAndPercentages(bool successfulAttempt, ExPerformanceCounter successCounter, ExPerformanceCounter attemptCounter, ExPerformanceCounter percentageCounter, ExPerformanceCounter percentageCounterBase)
		{
			if (!UmServiceGlobals.ArePerfCountersEnabled)
			{
				return;
			}
			Util.IncrementCounter(attemptCounter);
			if (successfulAttempt)
			{
				Util.IncrementCounter(successCounter);
			}
			if (attemptCounter.RawValue > 0L && successCounter.RawValue > 0L)
			{
				Util.SetCounter(percentageCounter, successCounter.RawValue);
				Util.SetCounter(percentageCounterBase, attemptCounter.RawValue);
				return;
			}
			Util.SetCounter(percentageCounter, 0L);
			Util.SetCounter(percentageCounterBase, 1L);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001245C File Offset: 0x0001065C
		internal void PopulateCallContext(bool callIsOutbound, PlatformCallInfo callInfo)
		{
			if (callIsOutbound)
			{
				ExAssert.RetailAssert(this.GatewayConfig != null, "Gateway object was not set for an outbound call");
				ExAssert.RetailAssert(this.DialPlan != null, "DialPlan object was not set for an outbound call");
			}
			this.dialPlanCulture = null;
			this.callId = callInfo.CallId;
			this.CallInfo = callInfo;
			CallIdTracer.TracePfd(ExTraceGlobals.PFDUMCallAcceptanceTracer, this, "PFD UMC {0} - Attempt to Parse Information for Call.", new object[]
			{
				12282
			});
			this.RemoteFQDN = callInfo.RemoteMatchedFQDN;
			if (callIsOutbound)
			{
				this.PopulateTenantGuid(true);
				this.RoutingHelper = SipRoutingHelper.CreateForOutbound(this.DialPlan);
			}
			else
			{
				if (SipPeerManager.Instance.IsLocalDiagnosticCall(callInfo.RemotePeer, callInfo.RemoteHeaders))
				{
					this.callIsDiagnostic = true;
					this.callIsLocalDiagnostic = true;
					this.CallType = 7;
					this.TenantGuid = Guid.Empty;
				}
				else
				{
					this.PopulateTenantGuid(false);
				}
				this.RoutingHelper = SipRoutingHelper.Create(callInfo);
			}
			this.PopulateSIPHeaders(callIsOutbound, callInfo);
			if (this.FromUriOfCall == null)
			{
				throw new InvalidSIPHeaderException("INVITE", "from", string.Empty);
			}
			this.PopulateRemotePeer(callIsOutbound, callInfo.RemotePeer, callInfo.RemoteMatchedFQDN);
			if (this.CallInfo.DiversionInfo.Count > 0)
			{
				PlatformDiversionInfo platformDiversionInfo = this.CallInfo.DiversionInfo[0];
				this.extnNumber = platformDiversionInfo.OriginalCalledParty;
				this.originalCalledParty = platformDiversionInfo.OriginalCalledParty;
				this.diversionUserAtHost = platformDiversionInfo.UserAtHost;
				this.ReasonForCall = CallContext.GetReasonForCall(platformDiversionInfo);
			}
			this.Initialize(callInfo.RemoteHeaders);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000125E8 File Offset: 0x000107E8
		internal void OnSessionReceived(IList<PlatformSignalingHeader> headers)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext.OnSessionReceived. CallType: {0}.", new object[]
			{
				this.CallType
			});
			if (this.TryHandleTransferredCallFromAnotherServer())
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext.OnSessionReceived: Got transferred call from another server", new object[0]);
				OfferResult offerResult = this.offerResult;
				return;
			}
			if (this.offerResult == OfferResult.Redirect || this.IsDiagnosticCall || this.IsPlayOnPhoneCall || this.IsPlayOnPhonePAAGreetingCall)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext.OnSessionReceived: No additional steps required.", new object[0]);
				return;
			}
			if (this.IsAutoAttendantCall)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext.OnSessionReceived: AA call. No additional initialization required.", new object[0]);
				return;
			}
			if (this.CallInfo.DiversionInfo.Count > 0)
			{
				this.ProcessDiversionInformation();
				if (this.OfferResult == OfferResult.Redirect)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Redirect needed after processing diversion.", new object[0]);
					return;
				}
			}
			if (this.CallerInfo == null && !this.TryHandleCallWithCallerIdOfSubscriber())
			{
				return;
			}
			if (this.dialPlan.URIType == UMUriType.SipName && this.dialPlan.VoIPSecurity != UMVoIPSecurityType.Unsecured)
			{
				this.officeCommunicatorFeature.Parse(this, headers, this.localResourcePath);
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "{0}: URIType={1}, VoIPSecurity={2}: will not attempt to process OC features", new object[]
				{
					this.dialPlan.Name,
					this.dialPlan.URIType,
					this.dialPlan.VoIPSecurity
				});
			}
			if (this.CallType == null)
			{
				this.CallType = 1;
			}
			else if (this.CallType == 4 && this.UmSubscriberData != null)
			{
				this.UmSubscriberData.WaitForCompletion();
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Call Type: {0}.", new object[]
			{
				this.CallType
			});
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000127B8 File Offset: 0x000109B8
		internal IList<PlatformSignalingHeader> GetAcceptHeaders()
		{
			IList<PlatformSignalingHeader> list = null;
			if (this.dialPlan != null && this.dialPlan.URIType == UMUriType.SipName && !string.IsNullOrEmpty(this.Extension))
			{
				list = new List<PlatformSignalingHeader>();
				list.Add(Platform.Builder.CreateSignalingHeader("P-Asserted-Identity", string.Format(CultureInfo.InvariantCulture, "<sip:{0}>", new object[]
				{
					this.Extension
				})));
			}
			return list;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00012828 File Offset: 0x00010A28
		internal SetDiversionInfoResult SetDiversionInfo(PlatformDiversionInfo diversionInfo)
		{
			string b = null;
			string text = null;
			SetDiversionInfoResult setDiversionInfoResult = DiversionUtils.GetInitialDiversionInfo(diversionInfo, this.DialPlan, this.CallId, this.CallerId, out text, out b);
			if (setDiversionInfoResult != SetDiversionInfoResult.Invalid)
			{
				if (!string.Equals(this.CallerId.ToDial, b, StringComparison.OrdinalIgnoreCase))
				{
					this.extnNumber = text;
					this.diversionUserAtHost = diversionInfo.UserAtHost;
					this.ReasonForCall = CallContext.GetReasonForCall(diversionInfo);
					if (this.LookupAutoAttendantInDialPlan(text, true, this.dialPlan.Id))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Found valid AA.", new object[0]);
						return SetDiversionInfoResult.ObjectFound;
					}
					try
					{
						PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, text);
						CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "AA not found, try to find a user with _PhoneNumber", new object[0]);
						UMRecipient umrecipient = UMRecipient.Factory.FromExtension<UMRecipient>(text, this.dialPlan, null);
						if (umrecipient != null)
						{
							PIIMessage data2 = PIIMessage.Create(PIIType._User, umrecipient);
							CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data2, "Found user _User", new object[0]);
							UMSubscriber umsubscriber = umrecipient as UMSubscriber;
							if (umrecipient.RequiresRedirectForCallAnswering())
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data2, "User _User requires a redirect for C/A and the dialplan supports it", new object[0]);
								this.SetCallAnsweringCallType(umrecipient, false);
							}
							else if (umsubscriber != null)
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data2, "User _User is a valid user for call answering on this server", new object[0]);
								this.SetCallAnsweringCallType(umsubscriber, true);
							}
							else
							{
								CallIdTracer.TraceWarning(ExTraceGlobals.CallSessionTracer, this, data2, "User _User is NOT a valid user for call answering on this server", new object[0]);
								umrecipient.Dispose();
								umrecipient = null;
							}
						}
						return (umrecipient != null) ? SetDiversionInfoResult.ObjectFound : SetDiversionInfoResult.ObjectNotFound;
					}
					catch (LocalizedException ex)
					{
						CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, "Failed to find the callee : {0}.", new object[]
						{
							ex
						});
						return setDiversionInfoResult;
					}
				}
				setDiversionInfoResult = SetDiversionInfoResult.UserCallingItself;
				PIIMessage piimessage = PIIMessage.Create(PIIType._PhoneNumber, this.CallerId.ToDial);
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, piimessage, "Bypassing diversion because callerId and diversion are same. _PhoneNumber. .", new object[0]);
				if (this.dialPlan.URIType == UMUriType.TelExtn && text.Length != this.callerId.ToDial.Length)
				{
					PIIMessage[] data3 = new PIIMessage[]
					{
						piimessage,
						PIIMessage.Create(PIIType._PhoneNumber, text)
					};
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data3, "Repopulating the callerId _PhoneNumber1 with _PhoneNumer2 for telex dial plan.", new object[0]);
					PhoneNumber.TryParse(text, out this.callerId);
				}
			}
			return setDiversionInfoResult;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00012A78 File Offset: 0x00010C78
		internal void UpdateCallerInfo(InfoMessage.MessageReceivedEventArgs messageReceivedEventArgs)
		{
			if (this.GatewayConfig.DelayedSourcePartyInfoEnabled && this.DialPlan != null && this.DialPlan.URIType == UMUriType.TelExtn && messageReceivedEventArgs.Message.ContentType != null && messageReceivedEventArgs.Message.ContentType.Equals(Constants.ContentTypeSourceParty))
			{
				string text = (messageReceivedEventArgs.Message.Body != null) ? Encoding.UTF8.GetString(messageReceivedEventArgs.Message.Body).Trim() : null;
				if (!string.IsNullOrEmpty(text))
				{
					PIIMessage data = PIIMessage.Create(PIIType._Caller, text);
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "UpdateCallerInfo: Updating the calling party to _Caller.", new object[0]);
					if (!PhoneNumber.TryParse(text, out this.callerId))
					{
						this.callerId = PhoneNumber.Empty;
						return;
					}
					this.ConsumeUpdateForCallerInformation();
					return;
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "UpdateCallerInfo: Received INFO with no calling party. Body={0}.", new object[]
					{
						messageReceivedEventArgs.Message.Body
					});
				}
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00012B74 File Offset: 0x00010D74
		internal void ConsumeUpdateForCallerInformation()
		{
			UMRecipient umrecipient = this.ResolveCallerFromCallerId();
			PIIMessage data = PIIMessage.Create(PIIType._Callee, umrecipient);
			if (umrecipient == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ConsumeUpdateForCallerInformation: Calling party could not be resolved", new object[0]);
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "ConsumeUpdateForCallerInformation: Resolved Party To: _Callee.", new object[0]);
			UMSubscriber umsubscriber = umrecipient as UMSubscriber;
			if (umsubscriber != null)
			{
				this.CallerInfo = umsubscriber;
				return;
			}
			CallIdTracer.TraceWarning(ExTraceGlobals.CallSessionTracer, this, data, "ConsumeUpdateForCallerInformation: Resolved Party _Callee is not of type UMSubscriber", new object[0]);
			this.CallerInfo = null;
			if (umrecipient != null)
			{
				umrecipient.Dispose();
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012BFC File Offset: 0x00010DFC
		internal bool TryHandlePlayOnPhonePAAGreetingCall()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext::HandlePlayOnPhonePAAGreetingCall CallType={0}", new object[]
			{
				this.CallType
			});
			return this.IsPlayOnPhonePAAGreetingCall;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00012C38 File Offset: 0x00010E38
		internal bool HandleDirectAutoAttendantCallNoHuntGroup(string pilotNumber, PlatformSipUri requestUri, UMIPGateway gateway, bool gatewayInOnlyOneDialplan, ADObjectId gatewayDialPlanId)
		{
			UMAutoAttendant autoAttendant = AutoAttendantUtils.GetAutoAttendant(pilotNumber, requestUri, gateway, gatewayInOnlyOneDialplan, this.IsSecuredCall, gatewayDialPlanId, ADSystemConfigurationLookupFactory.CreateFromOrganizationId(gateway.OrganizationId));
			return this.CheckAndSetAA(autoAttendant, pilotNumber);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00012C6C File Offset: 0x00010E6C
		internal bool TryHandleTransferredCallFromAnotherServer()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "CallContext::TryHandleTransferredCallFromAnotherServer() header value = [{0}]", new object[]
			{
				this.ReferredByHeader ?? "<null>"
			});
			if (string.IsNullOrEmpty(this.ReferredByHeader))
			{
				return false;
			}
			UMRecipient caller = null;
			UserTransferWithContext.DeserializedReferredByHeader deserializedReferredByHeader = null;
			if (!UserTransferWithContext.TryParseReferredByHeader(this.ReferredByHeader, this.DialPlan, out caller, out deserializedReferredByHeader))
			{
				return false;
			}
			switch (deserializedReferredByHeader.TypeOfTransferredCall)
			{
			case 3:
				return this.HandleTransferredSubscriberAccessCall(caller);
			case 4:
				return this.HandleTransferredCallAnsweringCall(caller);
			default:
				return false;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00012CFC File Offset: 0x00010EFC
		internal bool TryHandleCallWithCallerIdOfSubscriber()
		{
			UMRecipient umrecipient = this.ResolveCallerFromCallerId();
			PIIMessage data = PIIMessage.Create(PIIType._Callee, umrecipient);
			if (this.CallType == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext::TryHandleCallWithCallerIdOfSubscriber(): CallType = None, Checking if we need to redirect the recipient", new object[0]);
				if (umrecipient == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext::TryHandleCallWithCallerIdOfSubscriber(): recipient is null", new object[0]);
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "CallContext::TryHandleCallWithCallerIdOfSubscriber(): Recipient=_Callee RequiresLegacySARedirect={0} ", new object[]
					{
						umrecipient.RequiresLegacyRedirectForSubscriberAccess
					});
					if (umrecipient.RequiresRedirectForSubscriberAccess())
					{
						this.OfferResult = OfferResult.Redirect;
						this.serverPicker = RedirectTargetChooserFactory.CreateFromRecipient(this, umrecipient);
						this.LegacySubscriber = umrecipient;
						CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "CallContext::TryHandleCallWithCallerIdOfSubscriber(): Recipient=_Callee will be redirected to an appropriate server", new object[0]);
						return false;
					}
				}
			}
			if (umrecipient != null)
			{
				UMSubscriber umsubscriber = umrecipient as UMSubscriber;
				if (umsubscriber != null)
				{
					if (!umsubscriber.IsLinkedToDialPlan(this.DialPlan))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "CallContext::TryHandleCallWithCallerIdOfSubscriber(): Subscriber=_Callee is not in the correct dialplan.", new object[0]);
						umsubscriber.Dispose();
					}
					else
					{
						PIIMessage[] data2 = new PIIMessage[]
						{
							PIIMessage.Create(PIIType._EmailAddress, umsubscriber.MailAddress),
							PIIMessage.Create(PIIType._UserDisplayName, umsubscriber.DisplayName),
							PIIMessage.Create(PIIType._PII, umsubscriber.ExchangeLegacyDN)
						};
						CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data2, "CallContext::TryHandleCallWithCallerIdOfSubscriber() Caller Info : _EmailAddress _UserDisplayName _PII.", new object[0]);
						this.CallerInfo = umsubscriber;
					}
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "CallContext::TryHandleCallWithCallerIdOfSubscriber(): Recipient=_Callee is not a subscriber", new object[0]);
					umrecipient.Dispose();
				}
			}
			return true;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00012E74 File Offset: 0x00011074
		internal void AsyncGetCallAnsweringData(bool evaluatePAA)
		{
			string diversion = this.originalCalledParty ?? this.Extension;
			this.UmSubscriberData = new NonBlockingCallAnsweringData(this.CalleeInfo, this.callId, this.CallerId, diversion, evaluatePAA);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012EB1 File Offset: 0x000110B1
		internal void SwitchToCallAnswering(UMRecipient user)
		{
			this.SetCallAnsweringCallType(user, true);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00012EBC File Offset: 0x000110BC
		internal bool SwitchToFallbackAutoAttendant()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ENTER: SwitchToFallbackAutoAttendant().", new object[0]);
			ADObjectId dtmffallbackAutoAttendant = this.AutoAttendantInfo.DTMFFallbackAutoAttendant;
			if (dtmffallbackAutoAttendant == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "DTMF FallbackAA was null on SpeechAA: DN=\"{0}\".", new object[]
				{
					this.AutoAttendantInfo.Id.DistinguishedName
				});
				return false;
			}
			return this.SwitchToAutoAttendant(dtmffallbackAutoAttendant, this.AutoAttendantInfo.OrganizationId);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00012F30 File Offset: 0x00011130
		internal bool SwitchToDefaultAutoAttendant()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ENTER: SwitchToDefaultAutoAttendant().", new object[0]);
			ADObjectId umautoAttendant = this.DialPlan.UMAutoAttendant;
			if (umautoAttendant == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Default AutoAttendant was null on DialPlan: DN=\"{0}\".", new object[]
				{
					this.DialPlan.Id.DistinguishedName
				});
				return false;
			}
			return this.SwitchToAutoAttendant(umautoAttendant, this.DialPlan.OrganizationId);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00012FA4 File Offset: 0x000111A4
		internal bool SwitchToAutoAttendant(ADObjectId autoAttendantIdentity, OrganizationId orgId)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ENTER: SwitchToAutoAttendant({0}).", new object[]
			{
				autoAttendantIdentity.DistinguishedName
			});
			UMAutoAttendant umautoAttendant = this.UMConfigCache.Find<UMAutoAttendant>(autoAttendantIdentity, orgId);
			if (umautoAttendant == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "FallbackAA \"{0}\" was was not found. It may have been deleted while the server was handling call.", new object[]
				{
					autoAttendantIdentity.DistinguishedName
				});
				return false;
			}
			if (StatusEnum.Disabled == umautoAttendant.Status)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "FallbackAA \"{0}\" was not Enabled by admin.", new object[]
				{
					autoAttendantIdentity.DistinguishedName
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallToUnusableAA, null, new object[]
				{
					umautoAttendant.Name,
					Strings.DisabledAA
				});
				return false;
			}
			LocalizedString localizedString;
			if (!AutoAttendantCore.IsRunnableAutoAttendant(umautoAttendant, out localizedString))
			{
				CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, "AutoAttendant with Name {0} in dialplan {1} is not Enabled, or does not have any features enabled.", new object[]
				{
					umautoAttendant.Name,
					umautoAttendant.UMDialPlan.Name
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallToUnusableAA, null, new object[]
				{
					umautoAttendant.Name,
					localizedString
				});
				return false;
			}
			this.LogCallData();
			this.SetCurrentAutoAttendant(umautoAttendant);
			return true;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000130E4 File Offset: 0x000112E4
		internal int CalcCallDuration()
		{
			if (this.connectionTime != null)
			{
				int num = (int)Math.Abs((ExDateTime.UtcNow - this.connectionTime.Value).TotalSeconds);
				return Math.Min(num, (this.DialPlan == null) ? num : (this.DialPlan.MaxCallDuration * 60));
			}
			return 0;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00013144 File Offset: 0x00011344
		internal void IncrementHungUpAfterDelayCounter()
		{
			switch (this.CallType)
			{
			case 3:
				this.IncrementCounter(SubscriberAccessCounters.CallsDisconnectedByCallersDuringUMAudioHourglass);
				return;
			case 4:
				this.IncrementCounter(CallAnswerCounters.CallsDisconnectedByCallersDuringUMAudioHourglass);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00013180 File Offset: 0x00011380
		internal void IncrementCounter(ExPerformanceCounter counter)
		{
			this.IncrementCounter(counter, 1L);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001318B File Offset: 0x0001138B
		internal void IncrementCounter(ExPerformanceCounter counter, long count)
		{
			if (this.IsDiagnosticCall)
			{
				return;
			}
			this.UpdateIncrementedCountersList(counter, count);
			Util.IncrementCounter(counter, count);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000131A5 File Offset: 0x000113A5
		internal void DecrementCounter(ExPerformanceCounter counter)
		{
			if (this.IsDiagnosticCall)
			{
				return;
			}
			this.UpdateIncrementedCountersList(counter, -1L);
			Util.DecrementCounter(counter);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000131BF File Offset: 0x000113BF
		internal void SetCounter(ExPerformanceCounter counter, long value)
		{
			if (this.IsDiagnosticCall)
			{
				return;
			}
			Util.SetCounter(counter, value);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000131D1 File Offset: 0x000113D1
		internal void TrackDirectoryAccessFailures(LocalizedException exception)
		{
			this.IncrementCounter(AvailabilityCounters.DirectoryAccessFailures);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000131DE File Offset: 0x000113DE
		internal void LogCallData()
		{
			this.callLogHelper.LogCallData();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000131EC File Offset: 0x000113EC
		internal void SetCurrentAutoAttendant(UMAutoAttendant aaconfig)
		{
			this.autoAttendantInfo = aaconfig;
			this.autoAttendantCulture = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "AutoAttendant Info : Name={0} DialPlan={1} TimeZone={2}.", new object[]
			{
				this.autoAttendantInfo.Name,
				this.autoAttendantInfo.UMDialPlan,
				this.autoAttendantInfo.TimeZone
			});
			if (string.IsNullOrEmpty(aaconfig.TimeZone))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "AutoAttendant Warning : The Unified Messaging auto attendant {0} has not been configured with a valid time zone.", new object[]
				{
					aaconfig.Name
				});
			}
			this.businessHour = true;
			this.autoAttendantSettings = aaconfig.GetCurrentSettings(out this.holidaySettings, ref this.businessHour);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Using AutoAttendantSettings: Name=\"{0}\" Dial-Plan: {1} Business-Hour: {2}.", new object[]
			{
				this.autoAttendantSettings.Parent.Name,
				this.autoAttendantSettings.Parent.UMDialPlan,
				this.businessHour
			});
			if (this.holidaySettings != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Using Holiday: Name=\"{0}\"", new object[]
				{
					this.holidaySettings.Name
				});
			}
			this.CallType = 2;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00013317 File Offset: 0x00011517
		internal void AddDiagnosticsTimer(Timer timer)
		{
			if (timer != null)
			{
				this.diagnosticsTimers.Add(timer);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00013330 File Offset: 0x00011530
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.IncrementCounter(GeneralCounters.TotalCalls);
				this.SetConnectionTimeCounters();
				CallRejectionCounterHelper.Instance.SetCounters(this.CallRejectionException, new Action<bool>(this.SetCallRejectionCounters), this.CallRejectionException != null, this.IsDiagnosticCall);
				this.LogCallData();
				if (this.CallerInfo != null)
				{
					this.CallerInfo.Dispose();
					this.CallerInfo = null;
				}
				if (this.CalleeInfo != null)
				{
					this.CalleeInfo.Dispose();
					this.CalleeInfo = null;
				}
				if (this.UmSubscriberData != null)
				{
					this.UmSubscriberData.Dispose();
					this.UmSubscriberData = null;
				}
				if (this.LegacySubscriber != null)
				{
					this.LegacySubscriber.Dispose();
					this.LegacySubscriber = null;
				}
				this.diagnosticsTimers.ForEach(delegate(Timer o)
				{
					o.Dispose();
				});
				this.diagnosticsTimers.Clear();
				this.CallType = 0;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001342A File Offset: 0x0001162A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CallContext>(this);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00013434 File Offset: 0x00011634
		private static ReasonForCall GetReasonForCall(PlatformDiversionInfo diversionInfo)
		{
			ReasonForCall result = ReasonForCall.None;
			switch (diversionInfo.RedirectReason)
			{
			case RedirectReason.UserBusy:
				result = ReasonForCall.DivertBusy;
				break;
			case RedirectReason.NoAnswer:
				result = ReasonForCall.DivertNoAnswer;
				break;
			case RedirectReason.Unconditional:
				result = ReasonForCall.DivertForward;
				break;
			case RedirectReason.Deflection:
				result = ReasonForCall.DivertForward;
				break;
			}
			return result;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00013474 File Offset: 0x00011674
		private void ProcessDiversionInformation()
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(1);
			SetDiversionInfoResult setDiversionInfoResult = SetDiversionInfoResult.Invalid;
			foreach (PlatformDiversionInfo platformDiversionInfo in this.CallInfo.DiversionInfo)
			{
				stringBuilder.Append(platformDiversionInfo.OriginalCalledParty).Append(" ");
				setDiversionInfoResult = this.SetDiversionInfo(platformDiversionInfo);
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "SetDiversionInfo returned:{0} - Iteration:{1}", new object[]
				{
					setDiversionInfoResult,
					num
				});
				if (SetDiversionInfoResult.ObjectFound == setDiversionInfoResult)
				{
					this.PrepareForCallAnsweringRedirectIfNecessary();
					break;
				}
				if (SetDiversionInfoResult.UserCallingItself == setDiversionInfoResult)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext: User calling itself.", new object[0]);
					break;
				}
				if (SetDiversionInfoResult.ObjectNotFound == setDiversionInfoResult)
				{
					if (++num == 6)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext: MaxNumOfDiversionLookups reached.", new object[0]);
						break;
					}
				}
				else
				{
					if (setDiversionInfoResult == SetDiversionInfoResult.Invalid)
					{
						throw CallRejectedException.Create(Strings.InvalidDiversionReceived(platformDiversionInfo.DiversionHeader), CallEndingReason.InvalidDiversionFormat, "Diversion number: {0}.", new object[]
						{
							platformDiversionInfo.OriginalCalledParty
						});
					}
					throw new NotSupportedException(setDiversionInfoResult.ToString());
				}
			}
			if (SetDiversionInfoResult.ObjectNotFound == setDiversionInfoResult)
			{
				this.divertedExtensionAllowVoiceMail = false;
				this.IncrementCounter(CallAnswerCounters.DivertedExtensionNotProvisioned);
				string text = stringBuilder.ToString().Trim();
				UmGlobals.ExEvent.LogEvent(this.DialPlan.OrganizationId, UMEventLogConstants.Tuple_DivertedExtensionNotProvisioned, text, CommonUtil.ToEventLogString(text), CommonUtil.ToEventLogString(this.dialPlan.Name), CommonUtil.ToEventLogString(this.callId), CommonUtil.ToEventLogString(this.gatewayDetails));
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00013624 File Offset: 0x00011824
		private void UpdateDisposableInstance<T>(ref T oldValue, T newValue, string propertyName) where T : DisposableBase
		{
			if (oldValue != null && !oldValue.IsDisposed && !object.ReferenceEquals(oldValue, newValue))
			{
				oldValue.Dispose();
			}
			oldValue = newValue;
			if (!string.IsNullOrEmpty(propertyName))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Changed {0} to: {1}", new object[]
				{
					propertyName,
					newValue
				});
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000136A4 File Offset: 0x000118A4
		private UMRecipient ResolveCallerFromCallerId()
		{
			UMRecipient umrecipient = null;
			try
			{
				umrecipient = SubscriberAccessUtils.ResolveCaller(this.CallerId, this.CalleeInfo, this.dialPlan);
			}
			finally
			{
				this.UpdateCallerResolutionCounters(umrecipient != null);
			}
			return umrecipient;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000136EC File Offset: 0x000118EC
		private void SetCallAnsweringCallType(UMRecipient user, bool getCallAnsweringData)
		{
			this.CallType = 4;
			PIIMessage[] data = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._EmailAddress, user.MailAddress),
				PIIMessage.Create(PIIType._UserDisplayName, user.DisplayName)
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "SetCallAnsweringCallType() Callee Info : _EmailAddress _UserDisplayName", new object[0]);
			this.CalleeInfo = user;
			if (getCallAnsweringData)
			{
				this.AsyncGetCallAnsweringData(true);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00013750 File Offset: 0x00011950
		private bool HandleTransferredSubscriberAccessCall(UMRecipient caller)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "CallContext::HandleTransferredSubscriberAccessCall caller type name {0}", new object[]
			{
				caller.GetType().Name
			});
			if (caller.RequiresRedirectForSubscriberAccess())
			{
				PIIMessage data = PIIMessage.Create(PIIType._Caller, caller);
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "CallContext::HandleTransferredSubscriberAccessCall Found caller=_Caller RequiresRedirectForSubscriberAccess={0}.", new object[]
				{
					caller.RequiresRedirectForSubscriberAccess()
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "CallContext::HandleTransferredSubscriberAccessCall call will be redirected", new object[0]);
				this.LegacySubscriber = caller;
				this.offerResult = OfferResult.Redirect;
				this.serverPicker = RedirectTargetChooserFactory.CreateFromRecipient(this, caller);
			}
			else
			{
				this.CallerInfo = (caller as UMSubscriber);
				this.callIsOVATransferForUMSubscriber = true;
				this.CallType = 1;
			}
			return true;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00013808 File Offset: 0x00011A08
		private bool HandleTransferredCallAnsweringCall(UMRecipient caller)
		{
			this.SetCallAnsweringCallType(caller, true);
			return true;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00013814 File Offset: 0x00011A14
		private void PopulateRemotePeer(bool callIsOutBound, IPAddress remoteEndp, string remoteMatchedFQDN)
		{
			if (callIsOutBound)
			{
				this.gatewayDetails = this.GatewayConfig.Address;
				this.securedCall = (this.DialPlan.VoIPSecurity != UMVoIPSecurityType.Unsecured);
				return;
			}
			this.immediatePeer = remoteEndp;
			if (string.IsNullOrEmpty(remoteMatchedFQDN))
			{
				this.securedCall = false;
				this.gatewayDetails = new UMSmartHost(this.immediatePeer.ToString());
				return;
			}
			this.securedCall = true;
			this.gatewayDetails = new UMSmartHost(remoteMatchedFQDN);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00013890 File Offset: 0x00011A90
		private void PopulateSIPHeaders(bool callIsOutbound, PlatformCallInfo callInfo)
		{
			this.referredByHeader = RouterUtils.GetReferredByHeader(callInfo.RemoteHeaders);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext.ReferredByHeader() Value = \"{0}\"", new object[]
			{
				this.referredByHeader ?? "<null>"
			});
			if (!callIsOutbound)
			{
				this.fromAddress = callInfo.CallingParty;
				this.requestUri = callInfo.RequestUri;
				this.toAddress = callInfo.CalledParty;
				this.localResourcePath = callInfo.RequestUri.FindParameter("local-resource-path");
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_InboundCallParams, null, new object[]
				{
					CommonUtil.ToEventLogString(callInfo.CallingParty.Uri),
					CommonUtil.ToEventLogString(callInfo.CalledParty.Uri),
					CommonUtil.ToEventLogString(RouterUtils.GetDiversionLogString(callInfo.DiversionInfo)),
					CommonUtil.ToEventLogString(this.ReferredByHeader),
					this.callId,
					callInfo.RemotePeer
				});
				this.ReasonForCall = ReasonForCall.Direct;
				return;
			}
			this.fromAddress = callInfo.CalledParty;
			this.toAddress = callInfo.CallingParty;
			this.requestUri = ((callInfo.CallingParty != null) ? callInfo.CallingParty.Uri : null);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_OutboundCallParams, null, new object[]
			{
				CommonUtil.ToEventLogString(callInfo.CallingParty),
				CommonUtil.ToEventLogString(callInfo.CalledParty.Uri),
				this.callId
			});
			this.ReasonForCall = ReasonForCall.Outbound;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00013A10 File Offset: 0x00011C10
		private void Initialize(IList<PlatformSignalingHeader> sipInviteHeaders)
		{
			PIIMessage[] data = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._Callee, this.FromUriOfCall),
				PIIMessage.Create(PIIType._PhoneNumber, this.extnNumber)
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "Callee Info : _Callee, _PhoneNumber.", new object[0]);
			if (Util.MaxCallLimitExceeded())
			{
				int num = (CommonConstants.MaxCallsAllowed != null) ? CommonConstants.MaxCallsAllowed.Value : -1;
				throw CallRejectedException.Create(Strings.MaxCallsLimitReached(num), CallEndingReason.MaxCallsReached, "Maximum configured value: {0}.", new object[]
				{
					num
				});
			}
			this.CheckifTestCall();
			this.IsTroubleshootingToolCall = this.CheckifUmTroubleshootingToolCall(sipInviteHeaders);
			this.IsActiveMonitoringCall = SipPeerManager.Instance.IsActiveMonitoringCall(sipInviteHeaders);
			if (this.TryHandleLocalDiagnosticCall())
			{
				return;
			}
			if (this.TryHandlePlayOnPhoneCall())
			{
				return;
			}
			if (this.TryHandleFindMeSubscriberCall())
			{
				return;
			}
			if (this.TryHandlePlayOnPhonePAAGreetingCall())
			{
				return;
			}
			if (this.TryHandleAccessProxyCall())
			{
				return;
			}
			PIIMessage[] data2 = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._Caller, this.FromUriOfCall),
				PIIMessage.Create(PIIType._Callee, this.requestUri)
			};
			CallIdTracer.TracePfd(ExTraceGlobals.PFDUMCallAcceptanceTracer, this, data2, "PFD UMC {0} - Initializing Call from Gateway: {1} Caller: _Caller Callee: _Callee.", new object[]
			{
				8698,
				this.gatewayDetails
			});
			if (!this.TryExecuteDefaultCallHandling())
			{
				return;
			}
			this.InitializeCallerAndCalleeIds();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013B6D File Offset: 0x00011D6D
		private void InitializeCallerAndCalleeIds()
		{
			this.InitializeCallerId(this.fromAddress, true);
			RouterUtils.ParseSipUri(this.requestUri, this.dialPlan, out this.calleeId);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00013B94 File Offset: 0x00011D94
		internal void InitializeCallerId(PlatformTelephonyAddress callerAddress, bool throwIfCallerIdInValid)
		{
			PIIMessage data = PIIMessage.Create(PIIType._Caller, callerAddress);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "InitializeCallerId : updating callerId to _Caller.", new object[0]);
			RouterUtils.ParseTelephonyAddress(callerAddress, this.dialPlan, throwIfCallerIdInValid, out this.callerId);
			this.callerIdDisplayName = this.GetCallerIdDisplayName(callerAddress);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013BE0 File Offset: 0x00011DE0
		private string GetCallerIdDisplayName(PlatformTelephonyAddress address)
		{
			if (address == null || string.IsNullOrEmpty(address.Name))
			{
				return null;
			}
			bool flag = UtilityMethods.IsAnonymousAddress(address);
			bool flag2 = PhoneNumber.IsValidPhoneNumber(address.Name);
			if (!flag && !flag2)
			{
				return address.Name;
			}
			return null;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013C20 File Offset: 0x00011E20
		private void CheckifTestCall()
		{
			if (GlobCfg.EnableRemoteGWAutomation)
			{
				this.callIsTest = true;
				return;
			}
			if (!string.IsNullOrEmpty(this.RemoteUserAgent))
			{
				string text = this.RemoteUserAgent.Replace(" ", string.Empty);
				string value = "Unified Messaging Test Client".Replace(" ", string.Empty);
				if (text.IndexOf(value, StringComparison.InvariantCulture) > 0)
				{
					this.callIsTest = true;
				}
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013C88 File Offset: 0x00011E88
		private bool CheckifUmTroubleshootingToolCall(IList<PlatformSignalingHeader> sipInviteHeaders)
		{
			foreach (PlatformSignalingHeader platformSignalingHeader in sipInviteHeaders)
			{
				if (platformSignalingHeader.Name.Equals("msexum-diagtool", StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013CE4 File Offset: 0x00011EE4
		private bool TryHandleAccessProxyCall()
		{
			bool result = false;
			if (this.RoutingHelper.SupportsMsOrganizationRouting)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "TryHandleAccessProxyCall: {0} supports ms-organization routing.", new object[]
				{
					this.RemoteFQDN
				});
				string diversionUri = this.diversionUserAtHost;
				SipRoutingHelper.Context routingContext = this.RoutingHelper.GetRoutingContext(this.ToUriOfCall.SimplifiedUri, this.FromUriOfCall.SimplifiedUri, diversionUri, this.RequestUriOfCall);
				if (routingContext.AutoAttendant != null && this.CheckAndSetAA(routingContext.AutoAttendant, this.requestUri.Host))
				{
					this.InitializeCallerAndCalleeIds();
				}
				else
				{
					if (routingContext.DialPlanId == null)
					{
						throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.InvalidRequest, null, new object[0]);
					}
					this.DialPlan = this.UMConfigCache.Find<UMDialPlan>(routingContext.DialPlanId, routingContext.OrgId);
					this.InitializeCallerAndCalleeIds();
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013DC4 File Offset: 0x00011FC4
		private bool TryExecuteDefaultCallHandling()
		{
			ExAssert.RetailAssert(this.gatewayConfig != null, "Gateway object was not set for an in bound call");
			string text = (!UtilityMethods.IsAnonymousNumber(this.RequestUriOfCall.User)) ? this.RequestUriOfCall.User : null;
			IADSystemConfigurationLookup adSession = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.gatewayConfig.OrganizationId, false);
			ADObjectId gatewayDialPlanId;
			bool gatewayInOnlyOneDialplan;
			UMHuntGroup huntGroup = HuntGroupUtils.GetHuntGroup(text, this.gatewayConfig, this.RequestUriOfCall, this.GatewayDetails, adSession, this.IsSecuredCall, out gatewayDialPlanId, out gatewayInOnlyOneDialplan);
			if (this.TryHandleDirectAutoAttendantCall(text, huntGroup, this.gatewayConfig, gatewayInOnlyOneDialplan, gatewayDialPlanId))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Found AA {0} that matched pilot# {1}.", new object[]
				{
					this.autoAttendantInfo.Id,
					text
				});
				return true;
			}
			if (huntGroup != null)
			{
				this.HandleSubscriberAccessCallWithHuntgroup(huntGroup);
				return true;
			}
			if (this.gatewayConfig.GlobalCallRoutingScheme == UMGlobalCallRoutingScheme.E164)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Global Gateway call ({0}), but no huntgroup, Virtual Number or AA found  for {1}.", new object[]
				{
					this.gatewayConfig.Address,
					text
				});
				throw CallRejectedException.Create(Strings.GlobalGatewayWithNoMatch(this.gatewayConfig.Address.ToString(), text), CallEndingReason.GlobalGatewayWithNoMatch, "UMIPGateway: {0}. Pilot number: {1}.", new object[]
				{
					this.gatewayConfig.Address.ToString(),
					text
				});
			}
			UMHuntGroup huntGroup2 = null;
			if (HuntGroupUtils.TryGetDefaultHuntGroup(this.gatewayConfig, text, out huntGroup2))
			{
				this.HandleSubscriberAccessCallWithHuntgroup(huntGroup2);
				return true;
			}
			throw CallRejectedException.Create(Strings.CallFromInvalidHuntGroup(text, this.gatewayDetails.ToString()), CallEndingReason.IncorrectHuntGroup, "Pilot number: {0}. UMIPGateway: {1}.", new object[]
			{
				text,
				this.gatewayDetails.ToString()
			});
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013F70 File Offset: 0x00012170
		private bool HandleSIPToHeaderLookups(CallContext.SIPToHeaderLookup lookupType, UMIPGateway gw)
		{
			string text = (!UtilityMethods.IsAnonymousNumber(this.ToUriOfCall.User)) ? this.ToUriOfCall.User : null;
			if (string.IsNullOrEmpty(text) || gw.GlobalCallRoutingScheme != UMGlobalCallRoutingScheme.E164)
			{
				return false;
			}
			if (lookupType == CallContext.SIPToHeaderLookup.DirectAA)
			{
				return this.TryFindAAWithNoDPKnowledge(text);
			}
			throw new ArgumentException("lookupType");
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013FC9 File Offset: 0x000121C9
		private void PrepareForCallAnsweringRedirectIfNecessary()
		{
			if (this.callType == 4 && this.CalleeInfo.RequiresRedirectForCallAnswering())
			{
				this.OfferResult = OfferResult.Redirect;
				this.serverPicker = RedirectTargetChooserFactory.CreateFromRecipient(this, this.CalleeInfo);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013FFA File Offset: 0x000121FA
		private bool TryHandleDirectAutoAttendantCall(string pilotNumber, UMHuntGroup huntGroup, UMIPGateway gateway, bool gatewayInOnlyOneDialplan, ADObjectId gatewayDialPlanId)
		{
			return this.HandleSIPToHeaderLookups(CallContext.SIPToHeaderLookup.DirectAA, gateway) || this.TryHandleDirectAACallWithDPKnowledge(pilotNumber, huntGroup, gateway, gatewayInOnlyOneDialplan, gatewayDialPlanId);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014018 File Offset: 0x00012218
		private bool TryFindAAWithNoDPKnowledge(string aaNumber)
		{
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromTenantGuid(this.TenantGuid);
			UMAutoAttendant autoAttendantWithNoDialplanInformation = iadsystemConfigurationLookup.GetAutoAttendantWithNoDialplanInformation(aaNumber);
			return autoAttendantWithNoDialplanInformation != null && this.CheckAndSetAA(autoAttendantWithNoDialplanInformation, aaNumber);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00014048 File Offset: 0x00012248
		private bool TryHandleDirectAACallWithDPKnowledge(string pilotNumber, UMHuntGroup huntGroup, UMIPGateway gateway, bool gatewayInOnlyOneDialplan, ADObjectId gatewayDialPlanId)
		{
			if (pilotNumber == null)
			{
				return false;
			}
			if (huntGroup == null)
			{
				return this.HandleDirectAutoAttendantCallNoHuntGroup(pilotNumber, this.RequestUriOfCall, gateway, gatewayInOnlyOneDialplan, gatewayDialPlanId);
			}
			UMAutoAttendant autoAttendant = AutoAttendantUtils.GetAutoAttendant(pilotNumber, huntGroup, this.RequestUriOfCall, this.IsSecuredCall, ADSystemConfigurationLookupFactory.CreateFromOrganizationId(gateway.OrganizationId));
			return this.CheckAndSetAA(autoAttendant, pilotNumber);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00014098 File Offset: 0x00012298
		private void HandleSubscriberAccessCallWithHuntgroup(UMHuntGroup huntGroup)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Finding dialplan with huntgroup {0}.", new object[]
			{
				huntGroup.Id
			});
			this.dialPlan = this.UMConfigCache.Find<UMDialPlan>(huntGroup.UMDialPlan, huntGroup.OrganizationId);
			if (this.dialPlan != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Found dialplan : {0}.", new object[]
				{
					this.dialPlan.Id
				});
				return;
			}
			CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, "Dial plan object not found {0}", new object[]
			{
				huntGroup.UMDialPlan
			});
			throw CallRejectedException.Create(Strings.DialPlanNotFound(huntGroup.UMDialPlan.DistinguishedName), CallEndingReason.DialPlanNotFound, "UM dial plan: {0}.", new object[]
			{
				huntGroup.UMDialPlan.DistinguishedName
			});
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00014168 File Offset: 0x00012368
		private bool TryHandleFindMeSubscriberCall()
		{
			if (this.IsFindMeSubscriberCall)
			{
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, this.CallerInfo.MailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "CallContext created for FindMeSubscriberCall caller _EmailAddress.", new object[0]);
				return true;
			}
			return false;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000141AA File Offset: 0x000123AA
		private bool TryHandleLocalDiagnosticCall()
		{
			return this.IsLocalDiagnosticCall;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000141B4 File Offset: 0x000123B4
		private bool TryHandlePlayOnPhoneCall()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext::HandlePlayOnPhoneCalll CallType={0}", new object[]
			{
				this.CallType
			});
			if (!this.IsPlayOnPhoneCall)
			{
				return false;
			}
			PlayOnPhoneAAGreetingRequest playOnPhoneAAGreetingRequest = this.webServiceRequest as PlayOnPhoneAAGreetingRequest;
			if (playOnPhoneAAGreetingRequest != null && this.UMConfigCache.Find<UMAutoAttendant>(playOnPhoneAAGreetingRequest.AutoAttendant.Id, playOnPhoneAAGreetingRequest.AutoAttendant.OrganizationId) == null)
			{
				throw new InvalidUMAutoAttendantException();
			}
			this.IncrementCounter(GeneralCounters.TotalPlayOnPhoneCalls);
			return true;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014238 File Offset: 0x00012438
		private bool LookupAutoAttendantInDialPlan(string pilotNumberOrName, bool numberIsPilot, ADObjectId dialPlanId)
		{
			UMAutoAttendant aa = AutoAttendantUtils.LookupAutoAttendantInDialPlan(pilotNumberOrName, numberIsPilot, dialPlanId, ADSystemConfigurationLookupFactory.CreateFromTenantGuid(this.TenantGuid));
			return this.CheckAndSetAA(aa, pilotNumberOrName);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00014264 File Offset: 0x00012464
		private bool CheckAndSetAA(UMAutoAttendant aa, string pilotNumberOrName)
		{
			if (aa == null)
			{
				return false;
			}
			aa = this.UMConfigCache.Find<UMAutoAttendant>(aa.Id, aa.OrganizationId);
			if (aa == null)
			{
				CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, "AutoAttendant with PilotNumberOrName '{0}' could not be loaded or does not have a valid configuration.", new object[]
				{
					pilotNumberOrName
				});
				return false;
			}
			if (!AutoAttendantUtils.IsAutoAttendantUsable(aa, pilotNumberOrName))
			{
				return false;
			}
			this.SetCurrentAutoAttendant(aa);
			this.dialPlan = this.UMConfigCache.Find<UMDialPlan>(aa.UMDialPlan, aa.OrganizationId);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "Found Enabled AutoAttendant - Name/Number = [{0}] DP = [{1}].", new object[]
			{
				pilotNumberOrName,
				aa.UMDialPlan.Name
			});
			return true;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001430C File Offset: 0x0001250C
		private void UpdateIncrementedCountersList(ExPerformanceCounter counter, long value)
		{
			if (this.incrementedCounters.ContainsKey(counter))
			{
				this.incrementedCounters[counter] = this.incrementedCounters[counter] + value;
			}
			else
			{
				this.incrementedCounters.Add(counter, value);
			}
			if (this.incrementedCounters[counter] < 0L)
			{
				CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, null, "Counter '{0}' was decremented more times than it was incremented. Stack trace: {1}.", new object[]
				{
					counter.CounterName,
					new StackTrace()
				});
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00014389 File Offset: 0x00012589
		private bool IsCounterIncremented(ExPerformanceCounter counter)
		{
			return this.incrementedCounters.ContainsKey(counter) && this.incrementedCounters[counter] > 0L;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000143AB File Offset: 0x000125AB
		private void SetCallRejectionCounters(bool isCallRejected)
		{
			if (isCallRejected)
			{
				this.IncrementCounter(AvailabilityCounters.UMWorkerProcessCallsRejected);
			}
			this.SetCounter(AvailabilityCounters.RecentUMWorkerProcessCallsRejected, (long)CallContext.recentPercentageRejectedCalls.Update(!isCallRejected));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000143D8 File Offset: 0x000125D8
		private void SetConnectionTimeCounters()
		{
			if (this.connectionTime != null)
			{
				int num = this.CalcCallDuration();
				long num2 = CallContext.averageCallDuration.Update((long)num);
				long num3 = CallContext.averageRecentCallDuration.Update((long)num);
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext.Dispose(). Call Duration={0} (seconds). New Average={1}. New Recent Average={2}", new object[]
				{
					num,
					num2,
					num3,
					num3
				});
				this.SetCounter(GeneralCounters.AverageCallDuration, num2);
				this.SetCounter(GeneralCounters.AverageRecentCallDuration, num3);
				if (this.CallerInfo != null && this.CallerInfo.IsAuthenticated)
				{
					this.SetCounter(SubscriberAccessCounters.AverageSubscriberCallDuration, num2);
					this.SetCounter(SubscriberAccessCounters.AverageRecentSubscriberCallDuration, num3);
					return;
				}
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext::SetConnectionTimeCounters()  Call is not connected, skipping call duration counter updates", new object[0]);
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000144B0 File Offset: 0x000126B0
		private void UpdateCallerResolutionCounters(bool callerLookupSuccessful)
		{
			if (7 == this.CallType)
			{
				return;
			}
			CallContext.UpdateCountersAndPercentages(callerLookupSuccessful, GeneralCounters.CallerResolutionsSucceeded, GeneralCounters.CallerResolutionsAttempted, GeneralCounters.PercentageSuccessfulCallerResolutions, GeneralCounters.PercentageSuccessfulCallerResolutions_Base);
			if (this.DialPlan != null && this.DialPlan.URIType == UMUriType.TelExtn && this.CallerId.UriType == UMUriType.TelExtn && this.CallerId.Number.Length == this.DialPlan.NumberOfDigitsInExtension)
			{
				CallContext.UpdateCountersAndPercentages(callerLookupSuccessful, GeneralCounters.ExtensionCallerResolutionsSucceeded, GeneralCounters.ExtensionCallerResolutionsAttempted, GeneralCounters.PercentageSuccessfulExtensionCallerResolutions, GeneralCounters.PercentageSuccessfulExtensionCallerResolutions_Base);
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001453C File Offset: 0x0001273C
		private void PopulateTenantGuid(bool callIsOutbound)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext.PopulateTenantGuid - callIsOutbound='{0}' IsLocalDiagnosticCall='{1}'", new object[]
			{
				callIsOutbound,
				this.IsLocalDiagnosticCall
			});
			ExAssert.RetailAssert(!this.IsLocalDiagnosticCall, "PopulateTenantGuid cannot be called for diagnostic calls.");
			this.TenantGuid = Guid.Empty;
			if (CommonConstants.UseDataCenterCallRouting)
			{
				if (callIsOutbound)
				{
					IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.DialPlan.OrganizationId);
					this.TenantGuid = iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId();
				}
				else
				{
					this.TenantGuid = Util.GetTenantGuid(this.CallInfo.RequestUri);
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "CallContext.PopulateTenantGuid - tenantGuid='{0}'", new object[]
			{
				this.TenantGuid
			});
		}

		// Token: 0x04000143 RID: 323
		private static Average averageCallDuration = new Average();

		// Token: 0x04000144 RID: 324
		private static MovingAverage averageRecentCallDuration = new MovingAverage(50);

		// Token: 0x04000145 RID: 325
		private static PercentageBooleanSlidingCounter recentPercentageRejectedCalls = PercentageBooleanSlidingCounter.CreateFailureCounter(1000, TimeSpan.FromHours(1.0));

		// Token: 0x04000146 RID: 326
		private CallLoggingHelper callLogHelper;

		// Token: 0x04000147 RID: 327
		private PhoneNumber callerId = PhoneNumber.Empty;

		// Token: 0x04000148 RID: 328
		private PhoneNumber calleeId = PhoneNumber.Empty;

		// Token: 0x04000149 RID: 329
		private string callerIdDisplayName;

		// Token: 0x0400014A RID: 330
		private string originalCalledParty;

		// Token: 0x0400014B RID: 331
		private string extnNumber;

		// Token: 0x0400014C RID: 332
		private string diversionUserAtHost;

		// Token: 0x0400014D RID: 333
		private UMSubscriber callerInfo;

		// Token: 0x0400014E RID: 334
		private PlatformTelephonyAddress fromAddress;

		// Token: 0x0400014F RID: 335
		private PlatformSipUri requestUri;

		// Token: 0x04000150 RID: 336
		private PlatformTelephonyAddress toAddress;

		// Token: 0x04000151 RID: 337
		private UMRecipient calleeInfo;

		// Token: 0x04000152 RID: 338
		private UMAutoAttendant autoAttendantInfo;

		// Token: 0x04000153 RID: 339
		private Dictionary<ExPerformanceCounter, long> incrementedCounters = new Dictionary<ExPerformanceCounter, long>();

		// Token: 0x04000154 RID: 340
		private bool callIsDiagnostic;

		// Token: 0x04000155 RID: 341
		private bool callIsLocalDiagnostic;

		// Token: 0x04000156 RID: 342
		private bool callIsTUIDiagnostic;

		// Token: 0x04000157 RID: 343
		private ExDateTime? connectionTime = null;

		// Token: 0x04000158 RID: 344
		private ExDateTime callStartTime = ExDateTime.UtcNow;

		// Token: 0x04000159 RID: 345
		private UMSmartHost gatewayDetails;

		// Token: 0x0400015A RID: 346
		private IPAddress immediatePeer;

		// Token: 0x0400015B RID: 347
		private bool securedCall;

		// Token: 0x0400015C RID: 348
		private UMDialPlan dialPlan;

		// Token: 0x0400015D RID: 349
		private CultureInfo dialPlanCulture;

		// Token: 0x0400015E RID: 350
		private CultureInfo autoAttendantCulture;

		// Token: 0x0400015F RID: 351
		private bool businessHour;

		// Token: 0x04000160 RID: 352
		private HolidaySchedule holidaySettings;

		// Token: 0x04000161 RID: 353
		private AutoAttendantSettings autoAttendantSettings;

		// Token: 0x04000162 RID: 354
		private CallType callType;

		// Token: 0x04000163 RID: 355
		private ReasonForCall reasonForCall;

		// Token: 0x04000164 RID: 356
		private DropCallReason reasonForDisconnect;

		// Token: 0x04000165 RID: 357
		private OfferResult offerResult;

		// Token: 0x04000166 RID: 358
		private string callId;

		// Token: 0x04000167 RID: 359
		private string localResourcePath;

		// Token: 0x04000168 RID: 360
		private bool callIsTest;

		// Token: 0x04000169 RID: 361
		private bool hasVoicemailBeenSubmitted;

		// Token: 0x0400016A RID: 362
		private PlayOnPhoneRequest webServiceRequest;

		// Token: 0x0400016B RID: 363
		private OCFeature officeCommunicatorFeature = new OCFeature();

		// Token: 0x0400016C RID: 364
		private IPAACommonInterface linkedManagerPointer;

		// Token: 0x0400016D RID: 365
		private bool faxToneReceived;

		// Token: 0x0400016E RID: 366
		private NonBlockingCallAnsweringData subscriberData;

		// Token: 0x0400016F RID: 367
		private string referredByHeader;

		// Token: 0x04000170 RID: 368
		private bool callIsOVATransferForUMSubscriber;

		// Token: 0x04000171 RID: 369
		private IRedirectTargetChooser serverPicker;

		// Token: 0x04000172 RID: 370
		private UMRecipient unsupportedSubscriber;

		// Token: 0x04000173 RID: 371
		private UMIPGateway gatewayConfig;

		// Token: 0x04000174 RID: 372
		private UMConfigCache promptConfigCache = new UMConfigCache();

		// Token: 0x04000175 RID: 373
		private bool divertedExtensionAllowVoiceMail = true;

		// Token: 0x04000176 RID: 374
		private List<Timer> diagnosticsTimers = new List<Timer>(2);

		// Token: 0x0200005E RID: 94
		private enum SIPToHeaderLookup
		{
			// Token: 0x04000181 RID: 385
			VirtualNumber,
			// Token: 0x04000182 RID: 386
			DirectAA
		}
	}
}
