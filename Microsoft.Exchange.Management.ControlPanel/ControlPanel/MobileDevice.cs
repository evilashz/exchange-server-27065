using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200049B RID: 1179
	[DataContract]
	public class MobileDevice : MobileDeviceRow
	{
		// Token: 0x06003A9C RID: 15004 RVA: 0x000B1C1F File Offset: 0x000AFE1F
		public MobileDevice(MobileDeviceConfiguration mobileDevice) : base(mobileDevice)
		{
		}

		// Token: 0x17002328 RID: 9000
		// (get) Token: 0x06003A9D RID: 15005 RVA: 0x000B1C28 File Offset: 0x000AFE28
		// (set) Token: 0x06003A9E RID: 15006 RVA: 0x000B1C34 File Offset: 0x000AFE34
		[DataMember]
		public string Caption
		{
			get
			{
				return OwaOptionStrings.MobileDeviceDetailTitle;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002329 RID: 9001
		// (get) Token: 0x06003A9F RID: 15007 RVA: 0x000B1C3C File Offset: 0x000AFE3C
		// (set) Token: 0x06003AA0 RID: 15008 RVA: 0x000B1C79 File Offset: 0x000AFE79
		[DataMember]
		public string FirstSyncTime
		{
			get
			{
				if (base.MobileDevice.FirstSyncTime != null)
				{
					return base.MobileDevice.FirstSyncTime.UtcToUserDateTimeString();
				}
				return OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700232A RID: 9002
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x000B1C80 File Offset: 0x000AFE80
		// (set) Token: 0x06003AA2 RID: 15010 RVA: 0x000B1CBD File Offset: 0x000AFEBD
		[DataMember]
		public string LastPolicyUpdateTime
		{
			get
			{
				if (base.MobileDevice.LastPolicyUpdateTime != null)
				{
					return base.MobileDevice.LastPolicyUpdateTime.UtcToUserDateTimeString();
				}
				return OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700232B RID: 9003
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x000B1CC4 File Offset: 0x000AFEC4
		// (set) Token: 0x06003AA4 RID: 15012 RVA: 0x000B1D01 File Offset: 0x000AFF01
		[DataMember]
		public string LastSuccessSync
		{
			get
			{
				if (base.MobileDevice.LastSuccessSync != null)
				{
					return base.MobileDevice.LastSuccessSync.UtcToUserDateTimeString();
				}
				return OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700232C RID: 9004
		// (get) Token: 0x06003AA5 RID: 15013 RVA: 0x000B1D08 File Offset: 0x000AFF08
		// (set) Token: 0x06003AA6 RID: 15014 RVA: 0x000B1D15 File Offset: 0x000AFF15
		[DataMember]
		public int NumberOfFoldersSynced
		{
			get
			{
				return base.MobileDevice.NumberOfFoldersSynced;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700232D RID: 9005
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x000B1D1C File Offset: 0x000AFF1C
		// (set) Token: 0x06003AA8 RID: 15016 RVA: 0x000B1D37 File Offset: 0x000AFF37
		[DataMember]
		public string DeviceMobileOperator
		{
			get
			{
				return base.MobileDevice.DeviceMobileOperator ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700232E RID: 9006
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x000B1D3E File Offset: 0x000AFF3E
		// (set) Token: 0x06003AAA RID: 15018 RVA: 0x000B1D6A File Offset: 0x000AFF6A
		[DataMember]
		public string DeviceAccessStateDescription
		{
			get
			{
				return LocalizedDescriptionAttribute.FromEnumForOwaOption(base.MobileDevice.DeviceAccessState.GetType(), base.MobileDevice.DeviceAccessState);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700232F RID: 9007
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x000B1D74 File Offset: 0x000AFF74
		// (set) Token: 0x06003AAC RID: 15020 RVA: 0x000B1DD1 File Offset: 0x000AFFD1
		[DataMember]
		public string DeviceAccessStateReasonDescription
		{
			get
			{
				string text = LocalizedDescriptionAttribute.FromEnumForOwaOption(base.MobileDevice.DeviceAccessStateReason.GetType(), base.MobileDevice.DeviceAccessStateReason);
				if (base.MobileDevice.DeviceAccessControlRule != null)
				{
					text = text + " - " + base.MobileDevice.DeviceAccessControlRule;
				}
				return text;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002330 RID: 9008
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x000B1DD8 File Offset: 0x000AFFD8
		// (set) Token: 0x06003AAE RID: 15022 RVA: 0x000B1E2C File Offset: 0x000B002C
		[DataMember]
		public string DeviceAccessStateReasonLink
		{
			get
			{
				switch (base.MobileDevice.DeviceAccessStateReason)
				{
				case DeviceAccessStateReason.Policy:
					return "http://go.microsoft.com/fwlink/p/?LinkId=262798";
				case DeviceAccessStateReason.UserAgentsChanges:
				case DeviceAccessStateReason.RecentCommands:
				case DeviceAccessStateReason.Watsons:
				case DeviceAccessStateReason.OutOfBudgets:
				case DeviceAccessStateReason.SyncCommands:
				case DeviceAccessStateReason.CommandFrequency:
					return "http://go.microsoft.com/fwlink/p/?LinkId=262797";
				}
				return "http://go.microsoft.com/fwlink/?LinkId=264210";
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002331 RID: 9009
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x000B1E34 File Offset: 0x000B0034
		// (set) Token: 0x06003AB0 RID: 15024 RVA: 0x000B1E9E File Offset: 0x000B009E
		[DataMember]
		public string DevicePolicyAppliedDescription
		{
			get
			{
				return ((base.MobileDevice.DevicePolicyApplied != null) ? base.MobileDevice.DevicePolicyApplied.ToString() : OwaOptionStrings.NotAvailable) + " - " + LocalizedDescriptionAttribute.FromEnumForOwaOption(base.MobileDevice.DevicePolicyApplicationStatus.GetType(), base.MobileDevice.DevicePolicyApplicationStatus);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002332 RID: 9010
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000B1EA5 File Offset: 0x000B00A5
		// (set) Token: 0x06003AB2 RID: 15026 RVA: 0x000B1EC0 File Offset: 0x000B00C0
		[DataMember]
		public string DeviceActiveSyncVersion
		{
			get
			{
				return base.MobileDevice.ClientVersion ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002333 RID: 9011
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x000B1EC7 File Offset: 0x000B00C7
		// (set) Token: 0x06003AB4 RID: 15028 RVA: 0x000B1EE2 File Offset: 0x000B00E2
		[DataMember]
		public string DeviceUserAgent
		{
			get
			{
				return base.MobileDevice.DeviceUserAgent ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002334 RID: 9012
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x000B1EEC File Offset: 0x000B00EC
		// (set) Token: 0x06003AB6 RID: 15030 RVA: 0x000B1F29 File Offset: 0x000B0129
		[DataMember]
		public string DeviceWipeSentTime
		{
			get
			{
				if (base.MobileDevice.DeviceWipeSentTime != null)
				{
					return base.MobileDevice.DeviceWipeSentTime.UtcToUserDateTimeString();
				}
				return OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002335 RID: 9013
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x000B1F30 File Offset: 0x000B0130
		// (set) Token: 0x06003AB8 RID: 15032 RVA: 0x000B1FD8 File Offset: 0x000B01D8
		[DataMember]
		public string IssuedCommandDescription
		{
			get
			{
				if (base.MobileDevice.DeviceWipeRequestTime != null || base.MobileDevice.DeviceWipeSentTime != null)
				{
					string str = (base.MobileDevice.DeviceWipeSentTime != null) ? OwaOptionStrings.PendingWipeCommandSentLabel : OwaOptionStrings.PendingWipeCommandIssuedLabel;
					DateTime? deviceWipeSentTime = base.MobileDevice.DeviceWipeSentTime;
					DateTime? dateTimeWithoutTimeZone = (deviceWipeSentTime != null) ? new DateTime?(deviceWipeSentTime.GetValueOrDefault()) : base.MobileDevice.DeviceWipeRequestTime;
					return str + " " + dateTimeWithoutTimeZone.UtcToUserDateTimeString();
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002336 RID: 9014
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x000B1FE0 File Offset: 0x000B01E0
		// (set) Token: 0x06003ABA RID: 15034 RVA: 0x000B201D File Offset: 0x000B021D
		[DataMember]
		public string DeviceWipeRequestTime
		{
			get
			{
				if (base.MobileDevice.DeviceWipeRequestTime != null)
				{
					return base.MobileDevice.DeviceWipeRequestTime.UtcToUserDateTimeString();
				}
				return OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002337 RID: 9015
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x000B2024 File Offset: 0x000B0224
		// (set) Token: 0x06003ABC RID: 15036 RVA: 0x000B2061 File Offset: 0x000B0261
		[DataMember]
		public string DeviceWipeAckTime
		{
			get
			{
				if (base.MobileDevice.DeviceWipeAckTime != null)
				{
					return base.MobileDevice.DeviceWipeAckTime.UtcToUserDateTimeString();
				}
				return OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002338 RID: 9016
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x000B2068 File Offset: 0x000B0268
		// (set) Token: 0x06003ABE RID: 15038 RVA: 0x000B2075 File Offset: 0x000B0275
		[DataMember]
		public uint? LastPingHeartbeat
		{
			get
			{
				return base.MobileDevice.LastPingHeartbeat;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002339 RID: 9017
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000B207C File Offset: 0x000B027C
		// (set) Token: 0x06003AC0 RID: 15040 RVA: 0x000B2097 File Offset: 0x000B0297
		[DataMember]
		public string DeviceImei
		{
			get
			{
				return base.MobileDevice.DeviceImei ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700233A RID: 9018
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000B209E File Offset: 0x000B029E
		// (set) Token: 0x06003AC2 RID: 15042 RVA: 0x000B20B9 File Offset: 0x000B02B9
		[DataMember]
		public string DeviceFriendlyName
		{
			get
			{
				return base.MobileDevice.DeviceFriendlyName ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700233B RID: 9019
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x000B20C0 File Offset: 0x000B02C0
		// (set) Token: 0x06003AC4 RID: 15044 RVA: 0x000B20DB File Offset: 0x000B02DB
		[DataMember]
		public string DeviceOS
		{
			get
			{
				return base.MobileDevice.DeviceOS ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700233C RID: 9020
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x000B20E2 File Offset: 0x000B02E2
		// (set) Token: 0x06003AC6 RID: 15046 RVA: 0x000B20FD File Offset: 0x000B02FD
		[DataMember]
		public string DeviceOSLanguage
		{
			get
			{
				return base.MobileDevice.DeviceOSLanguage ?? OwaOptionStrings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700233D RID: 9021
		// (get) Token: 0x06003AC7 RID: 15047 RVA: 0x000B2104 File Offset: 0x000B0304
		// (set) Token: 0x06003AC8 RID: 15048 RVA: 0x000B2111 File Offset: 0x000B0311
		[DataMember]
		public string StatusNote
		{
			get
			{
				return base.MobileDevice.StatusNote;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
