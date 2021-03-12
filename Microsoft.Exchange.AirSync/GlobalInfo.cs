using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000BD RID: 189
	internal class GlobalInfo : SyncStateDataInfo, IGlobalInfo
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x0003A8FC File Offset: 0x00038AFC
		public GlobalInfo(CustomSyncState wrappedSyncState) : base(wrappedSyncState)
		{
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0003A908 File Offset: 0x00038B08
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0003A929 File Offset: 0x00038B29
		public int? LastPolicyXMLHash
		{
			get
			{
				return base.Fetch<NullableData<Int32Data, int>, int?>(CustomStateDatumType.LastPolicyXMLHash, null);
			}
			set
			{
				base.Assign<NullableData<Int32Data, int>, int?>(CustomStateDatumType.LastPolicyXMLHash, value);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0003A937 File Offset: 0x00038B37
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x0003A93F File Offset: 0x00038B3F
		public bool HasNewSyncData { get; private set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0003A948 File Offset: 0x00038B48
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0003A950 File Offset: 0x00038B50
		public bool HasNewAutdData { get; private set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0003A959 File Offset: 0x00038B59
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0003A966 File Offset: 0x00038B66
		public ExDateTime? NextTimeToClearMailboxLogs
		{
			get
			{
				return base.FetchDateTime(CustomStateDatumType.NextTimeToClearMailboxLogs);
			}
			set
			{
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.NextTimeToClearMailboxLogs, value);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0003A974 File Offset: 0x00038B74
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0003A982 File Offset: 0x00038B82
		public uint PolicyKeyNeeded
		{
			get
			{
				return base.Fetch<UInt32Data, uint>(CustomStateDatumType.PolicyKeyNeeded, 0U);
			}
			set
			{
				base.Assign<UInt32Data, uint>(CustomStateDatumType.PolicyKeyNeeded, value);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0003A990 File Offset: 0x00038B90
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x0003A99E File Offset: 0x00038B9E
		public uint PolicyKeyWaitingAck
		{
			get
			{
				return base.Fetch<UInt32Data, uint>(CustomStateDatumType.PolicyKeyWaitingAck, 0U);
			}
			set
			{
				base.Assign<UInt32Data, uint>(CustomStateDatumType.PolicyKeyWaitingAck, value);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0003A9AC File Offset: 0x00038BAC
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0003A9BA File Offset: 0x00038BBA
		public uint PolicyKeyOnDevice
		{
			get
			{
				return base.Fetch<UInt32Data, uint>(CustomStateDatumType.PolicyKeyOnDevice, 0U);
			}
			set
			{
				base.Assign<UInt32Data, uint>(CustomStateDatumType.PolicyKeyOnDevice, value);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0003A9C8 File Offset: 0x00038BC8
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0003A9D6 File Offset: 0x00038BD6
		public bool ProvisionSupported
		{
			get
			{
				return base.Fetch<BooleanData, bool>(CustomStateDatumType.ProvisionSupported, false);
			}
			set
			{
				base.Assign<BooleanData, bool>(CustomStateDatumType.ProvisionSupported, value);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0003A9E4 File Offset: 0x00038BE4
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0003A9F1 File Offset: 0x00038BF1
		public ExDateTime? LastPolicyTime
		{
			get
			{
				return base.FetchDateTime(CustomStateDatumType.LastPolicyTime);
			}
			set
			{
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.LastPolicyTime, value);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0003AA00 File Offset: 0x00038C00
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0003AA2F File Offset: 0x00038C2F
		public int[] ClientCategoryHashList
		{
			get
			{
				int[] result;
				if (base.TryGetProperty<int[]>(AirSyncStateSchema.ClientCategoryList, out result))
				{
					return result;
				}
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[GlobalInfo.ClientCategoryHashList] Failed to get data from GlobalInfo.  Returning null.");
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DeleteProperty(AirSyncStateSchema.ClientCategoryList);
				}
				else
				{
					base.SetProperty(AirSyncStateSchema.ClientCategoryList, value);
				}
				base.IsDirty = true;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0003AA5C File Offset: 0x00038C5C
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x0003AA8B File Offset: 0x00038C8B
		public string[] LastClientIdsSeen
		{
			get
			{
				string[] result;
				if (base.TryGetProperty<string[]>(AirSyncStateSchema.LastSeenClientIds, out result))
				{
					return result;
				}
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[GlobalInfo.LastClientIdsSeen] Failed to get data from GlobalInfo.  Returning null.");
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DeleteProperty(AirSyncStateSchema.LastSeenClientIds);
				}
				else
				{
					base.SetProperty(AirSyncStateSchema.LastSeenClientIds, value);
				}
				base.IsDirty = true;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0003AAB8 File Offset: 0x00038CB8
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x0003AAE7 File Offset: 0x00038CE7
		public string LastSyncUserAgent
		{
			get
			{
				string result;
				if (base.TryGetProperty<string>(AirSyncStateSchema.LastSyncUserAgent, out result))
				{
					return result;
				}
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[GlobalInfo.LastSyncUserAgent] Failed to get user agent from GlobalInfo.  Returning null.");
				return null;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.DeleteProperty(AirSyncStateSchema.LastSyncUserAgent);
				}
				else
				{
					base.SetProperty(AirSyncStateSchema.LastSyncUserAgent, value);
				}
				base.IsDirty = true;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0003AB14 File Offset: 0x00038D14
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0003AB50 File Offset: 0x00038D50
		public int? LastPingHeartbeat
		{
			get
			{
				int value;
				if (base.TryGetProperty<int>(AirSyncStateSchema.LastPingHeartbeatInterval, out value))
				{
					return new int?(value);
				}
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[GlobalInfo.LastPingHeartbeatInterval] Failed to get user agent from GlobalInfo.  Returning null.");
				return null;
			}
			set
			{
				if (value == null)
				{
					base.DeleteProperty(AirSyncStateSchema.LastPingHeartbeatInterval);
				}
				else
				{
					base.SetProperty(AirSyncStateSchema.LastPingHeartbeatInterval, value.Value);
				}
				base.IsDirty = true;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0003AB88 File Offset: 0x00038D88
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x0003ABD0 File Offset: 0x00038DD0
		public ExDateTime? LastSyncAttemptTime
		{
			get
			{
				if (this.HasNewSyncData)
				{
					ExDateTime value;
					if (base.TryGetProperty<ExDateTime>(AirSyncStateSchema.LastSyncAttemptTime, out value))
					{
						return new ExDateTime?(value);
					}
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[GlobalInfo.LastSyncAttemptTime] Tried using new sync data, but failed to get property from bag.  Will look in sync state (old data).");
				}
				return base.FetchDateTime(CustomStateDatumType.LastSyncAttemptTime);
			}
			set
			{
				if (this.WriteNewSyncData)
				{
					AirSyncDiagnostics.TraceDebug<bool>(ExTraceGlobals.RequestsTracer, this, "[GlobalInfo.LastSyncAttemptTime] Using new sync data - saving on first class property.  Has value? {0}", value != null);
					if (value != null)
					{
						base.SetProperty(AirSyncStateSchema.LastSyncAttemptTime, value.Value);
					}
					else
					{
						base.DeleteProperty(AirSyncStateSchema.LastSyncAttemptTime);
					}
					base.IsDirty = true;
					return;
				}
				base.DeleteProperty(AirSyncStateSchema.LastSyncAttemptTime);
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.LastSyncAttemptTime, value);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0003AC48 File Offset: 0x00038E48
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0003AC90 File Offset: 0x00038E90
		public ExDateTime? LastSyncSuccessTime
		{
			get
			{
				if (this.HasNewSyncData)
				{
					ExDateTime value;
					if (base.TryGetProperty<ExDateTime>(AirSyncStateSchema.LastSyncSuccessTime, out value))
					{
						return new ExDateTime?(value);
					}
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[GlobalInfo.LastSyncSuccessTime] Tried using new sync data, but failed to get property from bag.  Will look in sync state (old data).");
				}
				return base.FetchDateTime(CustomStateDatumType.LastSyncSuccessTime);
			}
			set
			{
				if (this.WriteNewSyncData)
				{
					AirSyncDiagnostics.TraceDebug<bool>(ExTraceGlobals.RequestsTracer, this, "[GlobalInfo.LastSyncSuccessTime] Using new sync data - saving on first class property.  Has value? {0}", value != null);
					if (value != null)
					{
						base.SetProperty(AirSyncStateSchema.LastSyncSuccessTime, value.Value);
					}
					else
					{
						base.DeleteProperty(AirSyncStateSchema.LastSyncSuccessTime);
					}
					base.IsDirty = true;
					return;
				}
				base.DeleteProperty(AirSyncStateSchema.LastSyncSuccessTime);
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.LastSyncSuccessTime, value);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0003AD08 File Offset: 0x00038F08
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0003AD15 File Offset: 0x00038F15
		public ExDateTime? RemoteWipeRequestedTime
		{
			get
			{
				return base.FetchDateTime(CustomStateDatumType.WipeRequestTime);
			}
			set
			{
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.WipeRequestTime, value);
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0003AD23 File Offset: 0x00038F23
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x0003AD30 File Offset: 0x00038F30
		public ExDateTime? RemoteWipeSentTime
		{
			get
			{
				return base.FetchDateTime(CustomStateDatumType.WipeSendTime);
			}
			set
			{
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.WipeSendTime, value);
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0003AD3E File Offset: 0x00038F3E
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0003AD4B File Offset: 0x00038F4B
		public ExDateTime? RemoteWipeAckTime
		{
			get
			{
				return base.FetchDateTime(CustomStateDatumType.WipeAckTime);
			}
			set
			{
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.WipeAckTime, value);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0003AD59 File Offset: 0x00038F59
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x0003AD67 File Offset: 0x00038F67
		public string DeviceModel
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.DeviceModel, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.DeviceModel, value);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0003AD75 File Offset: 0x00038F75
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0003AD83 File Offset: 0x00038F83
		public string DeviceImei
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.DeviceImei, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.DeviceImei, value);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0003AD91 File Offset: 0x00038F91
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0003AD9F File Offset: 0x00038F9F
		public string DeviceFriendlyName
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.DeviceFriendlyName, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.DeviceFriendlyName, value);
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0003ADAD File Offset: 0x00038FAD
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0003ADBB File Offset: 0x00038FBB
		public string DeviceOS
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.DeviceOS, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.DeviceOS, value);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0003ADC9 File Offset: 0x00038FC9
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0003ADD7 File Offset: 0x00038FD7
		public string DeviceOSLanguage
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.DeviceOSLanguage, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.DeviceOSLanguage, value);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0003ADE5 File Offset: 0x00038FE5
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0003ADF3 File Offset: 0x00038FF3
		public string DevicePhoneNumber
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.DevicePhoneNumber, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.DevicePhoneNumber, value);
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0003AE01 File Offset: 0x00039001
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0003AE0F File Offset: 0x0003900F
		public string UserAgent
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.UserAgent, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.UserAgent, value);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0003AE1D File Offset: 0x0003901D
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0003AE2B File Offset: 0x0003902B
		public bool DeviceEnableOutboundSMS
		{
			get
			{
				return base.Fetch<BooleanData, bool>(CustomStateDatumType.DeviceEnableOutboundSMS, false);
			}
			set
			{
				base.Assign<BooleanData, bool>(CustomStateDatumType.DeviceEnableOutboundSMS, value);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0003AE39 File Offset: 0x00039039
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0003AE47 File Offset: 0x00039047
		public string DeviceMobileOperator
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.DeviceMobileOperator, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.DeviceMobileOperator, value);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0003AE55 File Offset: 0x00039055
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0003AE63 File Offset: 0x00039063
		public string RecoveryPassword
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.RecoveryPassword, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.RecoveryPassword, value);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0003AE71 File Offset: 0x00039071
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x0003AE7F File Offset: 0x0003907F
		public DeviceAccessState DeviceAccessState
		{
			get
			{
				return (DeviceAccessState)base.Fetch<Int32Data, int>(CustomStateDatumType.DeviceAccessState, 0);
			}
			set
			{
				base.Assign<Int32Data, int>(CustomStateDatumType.DeviceAccessState, (int)value);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0003AE8D File Offset: 0x0003908D
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0003AE9B File Offset: 0x0003909B
		public DeviceAccessStateReason DeviceAccessStateReason
		{
			get
			{
				return (DeviceAccessStateReason)base.Fetch<Int32Data, int>(CustomStateDatumType.DeviceAccessStateReason, 0);
			}
			set
			{
				base.Assign<Int32Data, int>(CustomStateDatumType.DeviceAccessStateReason, (int)value);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0003AEA9 File Offset: 0x000390A9
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0003AEB7 File Offset: 0x000390B7
		public DevicePolicyApplicationStatus DevicePolicyApplicationStatus
		{
			get
			{
				return (DevicePolicyApplicationStatus)base.Fetch<Int32Data, int>(CustomStateDatumType.DevicePolicyApplicationStatus, 1);
			}
			set
			{
				base.Assign<Int32Data, int>(CustomStateDatumType.DevicePolicyApplicationStatus, (int)value);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0003AEC5 File Offset: 0x000390C5
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x0003AED3 File Offset: 0x000390D3
		public ADObjectId DevicePolicyApplied
		{
			get
			{
				return base.Fetch<ADObjectIdData, ADObjectId>(CustomStateDatumType.DevicePolicyApplied, null);
			}
			set
			{
				base.Assign<ADObjectIdData, ADObjectId>(CustomStateDatumType.DevicePolicyApplied, value);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0003AEE1 File Offset: 0x000390E1
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x0003AEEF File Offset: 0x000390EF
		public ADObjectId DeviceAccessControlRule
		{
			get
			{
				return base.Fetch<ADObjectIdData, ADObjectId>(CustomStateDatumType.DeviceAccessControlRule, null);
			}
			set
			{
				base.Assign<ADObjectIdData, ADObjectId>(CustomStateDatumType.DeviceAccessControlRule, value);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0003AEFD File Offset: 0x000390FD
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0003AF0B File Offset: 0x0003910B
		public string LastDeviceWipeRequestor
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.LastDeviceWipeRequestor, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.LastDeviceWipeRequestor, value);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0003AF19 File Offset: 0x00039119
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0003AF27 File Offset: 0x00039127
		public string DeviceActiveSyncVersion
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.DeviceActiveSyncVersion, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.DeviceActiveSyncVersion, value);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0003AF35 File Offset: 0x00039135
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0003AF43 File Offset: 0x00039143
		public string[] RemoteWipeConfirmationAddresses
		{
			get
			{
				return base.Fetch<ArrayData<StringData, string>, string[]>(CustomStateDatumType.WipeConfirmationAddresses, null);
			}
			set
			{
				base.Assign<ArrayData<StringData, string>, string[]>(CustomStateDatumType.WipeConfirmationAddresses, value);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0003AF54 File Offset: 0x00039154
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0003AF75 File Offset: 0x00039175
		public int? ADDeviceInfoHash
		{
			get
			{
				return base.Fetch<NullableData<Int32Data, int>, int?>(CustomStateDatumType.ADDeviceInfoHash, null);
			}
			set
			{
				base.Assign<NullableData<Int32Data, int>, int?>(CustomStateDatumType.ADDeviceInfoHash, value);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0003AF83 File Offset: 0x00039183
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0003AF91 File Offset: 0x00039191
		public bool HaveSentBoostrapMailForWM61
		{
			get
			{
				return base.Fetch<BooleanData, bool>(CustomStateDatumType.HaveSentBoostrapMailForWM61, false);
			}
			set
			{
				base.Assign<BooleanData, bool>(CustomStateDatumType.HaveSentBoostrapMailForWM61, value);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0003AF9F File Offset: 0x0003919F
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x0003AFAC File Offset: 0x000391AC
		public ExDateTime? BootstrapMailForWM61TriggeredTime
		{
			get
			{
				return base.FetchDateTime(CustomStateDatumType.BootstrapMailForWM61TriggeredTime);
			}
			set
			{
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.BootstrapMailForWM61TriggeredTime, value);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0003AFBA File Offset: 0x000391BA
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0003AFC8 File Offset: 0x000391C8
		public bool DeviceInformationReceived
		{
			get
			{
				return base.Fetch<BooleanData, bool>(CustomStateDatumType.DeviceInformationReceived, false);
			}
			set
			{
				base.Assign<BooleanData, bool>(CustomStateDatumType.DeviceInformationReceived, value);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0003AFD6 File Offset: 0x000391D6
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0003AFE3 File Offset: 0x000391E3
		public ExDateTime? SyncStateUpgradeTime
		{
			get
			{
				return base.FetchDateTime(CustomStateDatumType.SSUpgradeDateTime);
			}
			set
			{
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.SSUpgradeDateTime, value);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0003AFF1 File Offset: 0x000391F1
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0003AFFE File Offset: 0x000391FE
		public ExDateTime? ADCreationTime
		{
			get
			{
				return base.FetchDateTime(CustomStateDatumType.ADCreationTime);
			}
			set
			{
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.ADCreationTime, value);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0003B00C File Offset: 0x0003920C
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0003B01A File Offset: 0x0003921A
		public ADObjectId DeviceADObjectId
		{
			get
			{
				return base.Fetch<ADObjectIdData, ADObjectId>(CustomStateDatumType.DeviceADObjectId, null);
			}
			set
			{
				base.Assign<ADObjectIdData, ADObjectId>(CustomStateDatumType.DeviceADObjectId, value);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0003B028 File Offset: 0x00039228
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0003B036 File Offset: 0x00039236
		public ADObjectId UserADObjectId
		{
			get
			{
				return base.Fetch<ADObjectIdData, ADObjectId>(CustomStateDatumType.UserADObjectId, null);
			}
			set
			{
				base.Assign<ADObjectIdData, ADObjectId>(CustomStateDatumType.UserADObjectId, value);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0003B044 File Offset: 0x00039244
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0003B04C File Offset: 0x0003924C
		public bool IsSyncStateJustUpgraded { get; private set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0003B055 File Offset: 0x00039255
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0003B063 File Offset: 0x00039263
		public StoreObjectId ABQMailId
		{
			get
			{
				return base.Fetch<StoreObjectIdData, StoreObjectId>(CustomStateDatumType.ABQMailId, null);
			}
			set
			{
				base.Assign<StoreObjectIdData, StoreObjectId>(CustomStateDatumType.ABQMailId, value);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0003B071 File Offset: 0x00039271
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0003B07F File Offset: 0x0003927F
		public ABQMailState ABQMailState
		{
			get
			{
				return (ABQMailState)base.Fetch<Int32Data, int>(CustomStateDatumType.ABQMailState, 0);
			}
			set
			{
				base.Assign<Int32Data, int>(CustomStateDatumType.ABQMailState, (int)value);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0003B08D File Offset: 0x0003928D
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0003B09B File Offset: 0x0003929B
		public bool DeviceInformationPromoted
		{
			get
			{
				return base.Fetch<BooleanData, bool>(CustomStateDatumType.DeviceInformationPromoted, false);
			}
			set
			{
				base.Assign<BooleanData, bool>(CustomStateDatumType.DeviceInformationPromoted, value);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0003B0A9 File Offset: 0x000392A9
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0003B0B7 File Offset: 0x000392B7
		public string DevicePhoneNumberForSms
		{
			get
			{
				return base.Fetch<StringData, string>(CustomStateDatumType.DevicePhoneNumberForSms, null);
			}
			set
			{
				base.Assign<StringData, string>(CustomStateDatumType.DevicePhoneNumberForSms, value);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0003B0C5 File Offset: 0x000392C5
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0003B0D3 File Offset: 0x000392D3
		public bool SmsSearchFolderCreated
		{
			get
			{
				return base.Fetch<BooleanData, bool>(CustomStateDatumType.SmsSearchFolderCreated, false);
			}
			set
			{
				base.Assign<BooleanData, bool>(CustomStateDatumType.SmsSearchFolderCreated, value);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0003B0E4 File Offset: 0x000392E4
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0003B127 File Offset: 0x00039327
		public DeviceBehavior DeviceBehavior
		{
			get
			{
				DeviceBehavior deviceBehavior = base.Fetch<DeviceBehaviorData, DeviceBehavior>(CustomStateDatumType.DeviceBehavior, null);
				if (deviceBehavior == null || !deviceBehavior.Validate())
				{
					deviceBehavior = new DeviceBehavior(true);
					this.DeviceBehavior = deviceBehavior;
				}
				if (deviceBehavior.Owner == null)
				{
					deviceBehavior.Owner = this;
				}
				return deviceBehavior;
			}
			set
			{
				base.Assign<DeviceBehaviorData, DeviceBehavior>(CustomStateDatumType.DeviceBehavior, value);
				value.Owner = this;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0003B13C File Offset: 0x0003933C
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0003B144 File Offset: 0x00039344
		internal bool WriteNewSyncData { get; set; }

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0003B150 File Offset: 0x00039350
		public static GlobalInfo LoadFromMailbox(MailboxSession mailboxSession, SyncStateStorage syncStateStorage, ProtocolLogger protocolLogger)
		{
			bool flag;
			return GlobalInfo.LoadFromMailbox(mailboxSession, syncStateStorage, protocolLogger, out flag);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0003B168 File Offset: 0x00039368
		public static GlobalInfo LoadFromMailbox(MailboxSession mailboxSession, SyncStateStorage syncStateStorage, ProtocolLogger protocolLogger, out bool updateUserHasPartnership)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (syncStateStorage == null)
			{
				throw new ArgumentNullException("syncStateStorage");
			}
			updateUserHasPartnership = false;
			CustomSyncState customSyncState = null;
			GlobalInfo globalInfo = null;
			bool isDirty = false;
			bool hasNewSyncData = false;
			bool hasNewAutdData = false;
			bool isSyncStateJustUpgraded = false;
			bool flag = false;
			GlobalInfo result;
			try
			{
				GlobalSyncStateInfo syncStateInfo = new GlobalSyncStateInfo();
				customSyncState = syncStateStorage.GetCustomSyncState(syncStateInfo, GlobalInfo.ExtraGlobalInfoPropertiesToFetch);
				if (customSyncState == null)
				{
					isDirty = true;
					using (CustomSyncState customSyncState2 = syncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]))
					{
						if (customSyncState2 == null)
						{
							updateUserHasPartnership = true;
							syncStateStorage.DeleteAllSyncStates();
						}
					}
					customSyncState = syncStateStorage.CreateCustomSyncState(syncStateInfo);
					isDirty = true;
				}
				else
				{
					try
					{
						object obj = customSyncState.StoreObject.TryGetProperty(AirSyncStateSchema.LastSyncAttemptTime);
						hasNewSyncData = (obj != null && !(obj is PropertyError));
					}
					catch (Exception arg)
					{
						AirSyncDiagnostics.TraceError<Exception>(ExTraceGlobals.RequestsTracer, null, "[GlobalInfo.LoadFromMailbox] Encountered exception when checking for new syncStatus properties.  Exception: {0}", arg);
					}
					try
					{
						object obj2 = customSyncState.StoreObject.TryGetProperty(AirSyncStateSchema.LastPingHeartbeatInterval);
						hasNewAutdData = (obj2 != null && !(obj2 is PropertyError));
					}
					catch (Exception arg2)
					{
						AirSyncDiagnostics.TraceError<Exception>(ExTraceGlobals.RequestsTracer, null, "[GlobalInfo.LoadFromMailbox] Encountered exception when checking for new syncStatus properties.  Exception: {0}", arg2);
					}
					if (customSyncState.BackendVersion != null && customSyncState.BackendVersion.Value != customSyncState.Version)
					{
						isDirty = true;
						isSyncStateJustUpgraded = true;
						if (protocolLogger != null)
						{
							protocolLogger.SetValue(ProtocolLoggerData.Ssu, "2007");
						}
					}
				}
				globalInfo = new GlobalInfo(customSyncState);
				globalInfo.IsDirty = isDirty;
				globalInfo.HasNewSyncData = hasNewSyncData;
				globalInfo.HasNewAutdData = hasNewAutdData;
				globalInfo.IsSyncStateJustUpgraded = isSyncStateJustUpgraded;
				if (Command.CurrentCommand != null)
				{
					globalInfo.WriteNewSyncData = Command.CurrentCommand.User.Features.IsEnabled(EasFeature.SyncStatusOnGlobalInfo);
				}
				flag = true;
				result = globalInfo;
			}
			finally
			{
				if (!flag)
				{
					if (globalInfo != null)
					{
						globalInfo.Dispose();
					}
					else if (customSyncState != null)
					{
						customSyncState.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0003B38C File Offset: 0x0003958C
		internal static int ComputeADDeviceInfoHash(GlobalInfo globalInfo)
		{
			if (globalInfo == null)
			{
				return 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(globalInfo.DeviceFriendlyName);
			stringBuilder.Append(globalInfo.DeviceImei);
			stringBuilder.Append(globalInfo.DeviceOS);
			stringBuilder.Append(globalInfo.DeviceOSLanguage);
			stringBuilder.Append(DeviceInfo.ObfuscatePhoneNumber(globalInfo.DevicePhoneNumber));
			stringBuilder.Append(globalInfo.UserAgent);
			stringBuilder.Append(globalInfo.DeviceModel);
			stringBuilder.Append(globalInfo.DeviceMobileOperator);
			stringBuilder.Append(globalInfo.DeviceAccessState);
			stringBuilder.Append(globalInfo.DeviceAccessStateReason);
			stringBuilder.Append(globalInfo.DeviceActiveSyncVersion);
			stringBuilder.Append(globalInfo.DeviceAccessControlRule);
			return stringBuilder.ToString().GetHashCode();
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0003B45C File Offset: 0x0003965C
		internal static int ComputeADDeviceInfoHash(MobileDevice activeSyncDevice)
		{
			if (activeSyncDevice == null)
			{
				return 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(activeSyncDevice.FriendlyName);
			stringBuilder.Append(activeSyncDevice.DeviceImei);
			stringBuilder.Append(activeSyncDevice.DeviceOS);
			stringBuilder.Append(activeSyncDevice.DeviceOSLanguage);
			stringBuilder.Append(DeviceInfo.ObfuscatePhoneNumber(activeSyncDevice.DeviceTelephoneNumber));
			stringBuilder.Append(activeSyncDevice.DeviceUserAgent);
			stringBuilder.Append(activeSyncDevice.DeviceModel);
			stringBuilder.Append(activeSyncDevice.DeviceMobileOperator);
			stringBuilder.Append(activeSyncDevice.DeviceAccessState);
			stringBuilder.Append(activeSyncDevice.DeviceAccessStateReason);
			stringBuilder.Append(activeSyncDevice.ClientVersion);
			stringBuilder.Append(activeSyncDevice.DeviceAccessControlRule);
			return stringBuilder.ToString().GetHashCode();
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0003B52C File Offset: 0x0003972C
		internal static void CopyValuesFromGlobalInfo(MobileDevice mobileDevice, GlobalInfo globalInfo)
		{
			if (globalInfo != null)
			{
				mobileDevice.FriendlyName = globalInfo.DeviceFriendlyName;
				mobileDevice.DeviceImei = globalInfo.DeviceImei;
				mobileDevice.DeviceOS = globalInfo.DeviceOS;
				mobileDevice.DeviceOSLanguage = globalInfo.DeviceOSLanguage;
				mobileDevice.DeviceTelephoneNumber = DeviceInfo.ObfuscatePhoneNumber(globalInfo.DevicePhoneNumber);
				mobileDevice.DeviceUserAgent = globalInfo.UserAgent;
				mobileDevice.DeviceModel = globalInfo.DeviceModel;
				mobileDevice.DeviceMobileOperator = globalInfo.DeviceMobileOperator;
				mobileDevice.DeviceAccessState = globalInfo.DeviceAccessState;
				mobileDevice.DeviceAccessStateReason = globalInfo.DeviceAccessStateReason;
				mobileDevice.ClientVersion = globalInfo.DeviceActiveSyncVersion;
				mobileDevice.DeviceAccessControlRule = globalInfo.DeviceAccessControlRule;
			}
		}

		// Token: 0x0400063F RID: 1599
		public static readonly PropertyDefinition[] ExtraGlobalInfoPropertiesToFetch = new PropertyDefinition[]
		{
			AirSyncStateSchema.ClientCategoryList,
			AirSyncStateSchema.LastSeenClientIds,
			AirSyncStateSchema.LastSyncAttemptTime,
			AirSyncStateSchema.LastSyncSuccessTime,
			AirSyncStateSchema.LastSyncUserAgent,
			AirSyncStateSchema.LastPingHeartbeatInterval
		};
	}
}
