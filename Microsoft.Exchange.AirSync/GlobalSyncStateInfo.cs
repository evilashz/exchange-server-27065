using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000C3 RID: 195
	internal class GlobalSyncStateInfo : CustomSyncStateInfo
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0003E49B File Offset: 0x0003C69B
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0003E4A2 File Offset: 0x0003C6A2
		public override string UniqueName
		{
			get
			{
				return "Policy";
			}
			set
			{
				throw new InvalidOperationException("GlobalSyncStateInfo.UniqueName is not settable.");
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0003E4AE File Offset: 0x0003C6AE
		public override int Version
		{
			get
			{
				return 38;
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0003E4B4 File Offset: 0x0003C6B4
		public override void HandleSyncStateVersioning(SyncState syncState)
		{
			if (syncState == null)
			{
				throw new ArgumentNullException("syncState");
			}
			if (syncState.BackendVersion == null)
			{
				return;
			}
			bool flag = true;
			if (syncState.BackendVersion < 2 || syncState.BackendVersion > this.Version)
			{
				flag = false;
			}
			else if (syncState.BackendVersion.Value != this.Version)
			{
				string text = null;
				switch (syncState.BackendVersion.Value)
				{
				case 2:
					syncState["WipeConfirmationAddresses"] = null;
					break;
				case 3:
					break;
				case 4:
					goto IL_14F;
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
					goto IL_3B6;
				case 20:
					goto IL_16C;
				case 21:
					goto IL_184;
				case 22:
					goto IL_1A6;
				case 23:
					goto IL_1D2;
				case 24:
					goto IL_1EF;
				case 25:
					goto IL_1FA;
				case 26:
					goto IL_212;
				case 27:
					goto IL_259;
				case 28:
					goto IL_2D8;
				case 29:
					goto IL_2E4;
				case 30:
					goto IL_2EF;
				case 31:
					goto IL_2FB;
				case 32:
					goto IL_306;
				case 33:
					goto IL_312;
				case 34:
					goto IL_31E;
				case 35:
					goto IL_32A;
				case 36:
					goto IL_358;
				case 37:
					goto IL_39E;
				default:
					goto IL_3B6;
				}
				syncState[CustomStateDatumType.UserAgent] = null;
				IL_14F:
				syncState["LastAdUpdateTime"] = null;
				syncState["DeviceHealth"] = new Int32Data(0);
				IL_16C:
				text = syncState.GetData<StringData, string>("LastPolicyXML", null);
				syncState.Remove("LastPolicyXML");
				IL_184:
				syncState["ProvisionSupported"] = new BooleanData(syncState.GetData<UInt32Data, uint>("PolicyKeyOnDevice", 0U) != 0U);
				IL_1A6:
				syncState["LastPolicyXMLHash"] = ((text == null) ? null : new NullableData<Int32Data, int>(new int?(PolicyData.GetPolicyHashCode(text + true))));
				IL_1D2:
				syncState["DeviceEnableOutboundSMS"] = new BooleanData(false);
				syncState["DeviceMobileOperator"] = null;
				IL_1EF:
				syncState.Remove("LastAdUpdateTime");
				IL_1FA:
				syncState["ClientAlternateMailboxInformationVersion"] = null;
				syncState["DeviceUMRegisteredPhoneNumber"] = null;
				IL_212:
				syncState["HaveSentBoostrapMailForWM61"] = new BooleanData(false);
				if (syncState.BackendVersion.Value < 20)
				{
					syncState["SSUpgradeDateTime"] = new DateTimeData(ExDateTime.UtcNow);
				}
				else
				{
					syncState["SSUpgradeDateTime"] = null;
				}
				IL_259:
				syncState.Remove("DeviceHealth");
				syncState["DeviceAccessState"] = new Int32Data(0);
				syncState["DeviceAccessStateReason"] = new Int32Data(0);
				syncState["DevicePolicyApplied"] = null;
				syncState["DevicePolicyApplicationStatus"] = new Int32Data(0);
				syncState["LastDeviceWipeRequestor"] = null;
				syncState["DeviceActiveSyncVersion"] = null;
				syncState["ADDeviceInfoHash"] = null;
				syncState["DeviceInformationReceived"] = new BooleanData(false);
				IL_2D8:
				syncState["ADCreationTime"] = null;
				IL_2E4:
				syncState.Remove("DeviceUMRegisteredPhoneNumber");
				IL_2EF:
				syncState["NextTimeToClearMailboxLogs"] = null;
				IL_2FB:
				syncState.Remove("ClientAlternateMailboxInformationVersion");
				IL_306:
				syncState["DeviceADObjectId"] = null;
				IL_312:
				syncState["BootstrapMailForWM61TriggeredTime"] = null;
				IL_31E:
				syncState["UserADObjectId"] = null;
				IL_32A:
				syncState["ABQMailId"] = null;
				syncState["ABQMailState"] = new Int32Data(0);
				syncState["DeviceInformationPromoted"] = new BooleanData(false);
				IL_358:
				syncState["DevicePhoneNumberForSms"] = ((syncState["DevicePhoneNumber"] != null) ? new StringData(((StringData)syncState["DevicePhoneNumber"]).Data) : null);
				syncState["SmsSearchFolderCreated"] = new BooleanData(false);
				IL_39E:
				syncState["DeviceBehavior"] = new DeviceBehaviorData(new DeviceBehavior(true));
				goto IL_3B8;
				IL_3B6:
				flag = false;
			}
			IL_3B8:
			if (!flag)
			{
				syncState.HandleCorruptSyncState();
			}
		}

		// Token: 0x04000701 RID: 1793
		internal const string UniqueNameString = "Policy";

		// Token: 0x04000702 RID: 1794
		internal const int CurrentVersion = 38;

		// Token: 0x04000703 RID: 1795
		internal const int E14BaseVersion = 20;

		// Token: 0x020000C4 RID: 196
		internal struct PropertyNames
		{
			// Token: 0x04000704 RID: 1796
			internal const string DeviceFriendlyName = "DeviceFriendlyName";

			// Token: 0x04000705 RID: 1797
			internal const string DeviceImei = "DeviceIMEI";

			// Token: 0x04000706 RID: 1798
			internal const string DeviceModel = "DeviceModel";

			// Token: 0x04000707 RID: 1799
			internal const string DeviceOS = "DeviceOS";

			// Token: 0x04000708 RID: 1800
			internal const string DeviceOSLanguage = "DeviceOSLanguage";

			// Token: 0x04000709 RID: 1801
			internal const string DevicePhoneNumber = "DevicePhoneNumber";

			// Token: 0x0400070A RID: 1802
			internal const string DeviceUserAgent = "DeviceUserAgent";

			// Token: 0x0400070B RID: 1803
			internal const string LastPolicyTime = "LastPolicyTime";

			// Token: 0x0400070C RID: 1804
			internal const string LastPolicyXML = "LastPolicyXML";

			// Token: 0x0400070D RID: 1805
			internal const string LastPolicyXMLHash = "LastPolicyXMLHash";

			// Token: 0x0400070E RID: 1806
			internal const string NextTimeToClearMailboxLogs = "NextTimeToClearMailboxLogs";

			// Token: 0x0400070F RID: 1807
			internal const string PolicyKeyNeeded = "PolicyKeyNeeded";

			// Token: 0x04000710 RID: 1808
			internal const string PolicyKeyOnDevice = "PolicyKeyOnDevice";

			// Token: 0x04000711 RID: 1809
			internal const string PolicyKeyWaitingAck = "PolicyKeyWaitingAck";

			// Token: 0x04000712 RID: 1810
			internal const string RecoveryPassword = "RecoveryPassword";

			// Token: 0x04000713 RID: 1811
			internal const string WipeAckTime = "WipeAckTime";

			// Token: 0x04000714 RID: 1812
			internal const string WipeConfirmationAddresses = "WipeConfirmationAddresses";

			// Token: 0x04000715 RID: 1813
			internal const string WipeRequestTime = "WipeRequestTime";

			// Token: 0x04000716 RID: 1814
			internal const string WipeSendTime = "WipeSentTime";

			// Token: 0x04000717 RID: 1815
			internal const string DeviceHealth = "DeviceHealth";

			// Token: 0x04000718 RID: 1816
			internal const string ProvisionSupported = "ProvisionSupported";

			// Token: 0x04000719 RID: 1817
			internal const string DeviceEnableOutboundSMS = "DeviceEnableOutboundSMS";

			// Token: 0x0400071A RID: 1818
			internal const string DeviceMobileOperator = "DeviceMobileOperator";

			// Token: 0x0400071B RID: 1819
			internal const string SSUpgradeDateTime = "SSUpgradeDateTime";

			// Token: 0x0400071C RID: 1820
			internal const string HaveSentBoostrapMailForWM61 = "HaveSentBoostrapMailForWM61";

			// Token: 0x0400071D RID: 1821
			internal const string BootstrapMailForWM61TriggeredTime = "BootstrapMailForWM61TriggeredTime";

			// Token: 0x0400071E RID: 1822
			internal const string DeviceAccessState = "DeviceAccessState";

			// Token: 0x0400071F RID: 1823
			internal const string DeviceAccessStateReason = "DeviceAccessStateReason";

			// Token: 0x04000720 RID: 1824
			internal const string DeviceAccessControlRule = "DeviceAccessControlRule";

			// Token: 0x04000721 RID: 1825
			internal const string DevicePolicyApplied = "DevicePolicyApplied";

			// Token: 0x04000722 RID: 1826
			internal const string DevicePolicyApplicationStatus = "DevicePolicyApplicationStatus";

			// Token: 0x04000723 RID: 1827
			internal const string LastDeviceWipeRequestor = "LastDeviceWipeRequestor";

			// Token: 0x04000724 RID: 1828
			internal const string DeviceActiveSyncVersion = "DeviceActiveSyncVersion";

			// Token: 0x04000725 RID: 1829
			internal const string ADDeviceInfoHash = "ADDeviceInfoHash";

			// Token: 0x04000726 RID: 1830
			internal const string DeviceInformationReceived = "DeviceInformationReceived";

			// Token: 0x04000727 RID: 1831
			internal const string ADCreationTime = "ADCreationTime";

			// Token: 0x04000728 RID: 1832
			internal const string DeviceADObjectId = "DeviceADObjectId";

			// Token: 0x04000729 RID: 1833
			internal const string UserADObjectId = "UserADObjectId";

			// Token: 0x0400072A RID: 1834
			internal const string ABQMailId = "ABQMailId";

			// Token: 0x0400072B RID: 1835
			internal const string ABQMailState = "ABQMailState";

			// Token: 0x0400072C RID: 1836
			internal const string DeviceInformationPromoted = "DeviceInformationPromoted";

			// Token: 0x0400072D RID: 1837
			internal const string DevicePhoneNumberForSms = "DevicePhoneNumberForSms";

			// Token: 0x0400072E RID: 1838
			internal const string SmsSearchFolderCreated = "SmsSearchFolderCreated";

			// Token: 0x0400072F RID: 1839
			internal const string DeviceBehavior = "DeviceBehavior";
		}
	}
}
