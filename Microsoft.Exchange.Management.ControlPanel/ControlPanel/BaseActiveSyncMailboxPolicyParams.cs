using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F7 RID: 759
	[DataContract]
	public abstract class BaseActiveSyncMailboxPolicyParams : SetObjectProperties
	{
		// Token: 0x17001E5D RID: 7773
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x0008B8E7 File Offset: 0x00089AE7
		// (set) Token: 0x06002DAD RID: 11693 RVA: 0x0008B8F9 File Offset: 0x00089AF9
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}

		// Token: 0x17001E5E RID: 7774
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x0008B907 File Offset: 0x00089B07
		// (set) Token: 0x06002DAF RID: 11695 RVA: 0x0008B923 File Offset: 0x00089B23
		[DataMember]
		public bool IsDefault
		{
			get
			{
				return (bool)(base["IsDefault"] ?? false);
			}
			set
			{
				base["IsDefault"] = value;
			}
		}

		// Token: 0x17001E5F RID: 7775
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x0008B936 File Offset: 0x00089B36
		// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x0008B952 File Offset: 0x00089B52
		[DataMember]
		public bool AllowNonProvisionableDevices
		{
			get
			{
				return (bool)(base["AllowNonProvisionableDevices"] ?? false);
			}
			set
			{
				base["AllowNonProvisionableDevices"] = value;
			}
		}

		// Token: 0x17001E60 RID: 7776
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x0008B965 File Offset: 0x00089B65
		// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x0008B96D File Offset: 0x00089B6D
		[DataMember]
		public bool? IsDevicePolicyRefreshIntervalSet { get; set; }

		// Token: 0x17001E61 RID: 7777
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x0008B976 File Offset: 0x00089B76
		// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x0008B97E File Offset: 0x00089B7E
		[DataMember]
		public string DevicePolicyRefreshInterval { get; set; }

		// Token: 0x17001E62 RID: 7778
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x0008B987 File Offset: 0x00089B87
		// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x0008B98F File Offset: 0x00089B8F
		[DataMember]
		public bool? RequireDeviceEncryption { get; set; }

		// Token: 0x17001E63 RID: 7779
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x0008B998 File Offset: 0x00089B98
		// (set) Token: 0x06002DB9 RID: 11705 RVA: 0x0008B9A0 File Offset: 0x00089BA0
		[DataMember]
		public bool? RequireStorageCardEncryption { get; set; }

		// Token: 0x17001E64 RID: 7780
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x0008B9A9 File Offset: 0x00089BA9
		// (set) Token: 0x06002DBB RID: 11707 RVA: 0x0008B9BB File Offset: 0x00089BBB
		[DataMember]
		public bool? DevicePasswordEnabled
		{
			get
			{
				return (bool?)base["DevicePasswordEnabled"];
			}
			set
			{
				base["DevicePasswordEnabled"] = value;
			}
		}

		// Token: 0x17001E65 RID: 7781
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x0008B9CE File Offset: 0x00089BCE
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x0008B9D6 File Offset: 0x00089BD6
		[DataMember]
		public bool? AllowSimpleDevicePassword { get; set; }

		// Token: 0x17001E66 RID: 7782
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x0008B9DF File Offset: 0x00089BDF
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x0008B9E7 File Offset: 0x00089BE7
		[DataMember]
		public bool? AlphanumericDevicePasswordRequired { get; set; }

		// Token: 0x17001E67 RID: 7783
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x0008B9F0 File Offset: 0x00089BF0
		// (set) Token: 0x06002DC1 RID: 11713 RVA: 0x0008B9F8 File Offset: 0x00089BF8
		[DataMember]
		public bool? IsMinDevicePasswordLengthSet { get; set; }

		// Token: 0x17001E68 RID: 7784
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x0008BA01 File Offset: 0x00089C01
		// (set) Token: 0x06002DC3 RID: 11715 RVA: 0x0008BA09 File Offset: 0x00089C09
		[DataMember]
		public string MinDevicePasswordLength { get; set; }

		// Token: 0x17001E69 RID: 7785
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x0008BA12 File Offset: 0x00089C12
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x0008BA1A File Offset: 0x00089C1A
		[DataMember]
		public int? MinDevicePasswordComplexCharacters { get; set; }

		// Token: 0x17001E6A RID: 7786
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x0008BA23 File Offset: 0x00089C23
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x0008BA2B File Offset: 0x00089C2B
		[DataMember]
		public bool? IsMaxDevicePasswordFailedAttemptsSet { get; set; }

		// Token: 0x17001E6B RID: 7787
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x0008BA34 File Offset: 0x00089C34
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x0008BA3C File Offset: 0x00089C3C
		[DataMember]
		public string MaxDevicePasswordFailedAttempts { get; set; }

		// Token: 0x17001E6C RID: 7788
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x0008BA45 File Offset: 0x00089C45
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x0008BA4D File Offset: 0x00089C4D
		[DataMember]
		public bool? IsMaxInactivityTimeDeviceLockSet { get; set; }

		// Token: 0x17001E6D RID: 7789
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x0008BA56 File Offset: 0x00089C56
		// (set) Token: 0x06002DCD RID: 11725 RVA: 0x0008BA5E File Offset: 0x00089C5E
		[DataMember]
		public string MaxInactivityTimeDeviceLock { get; set; }

		// Token: 0x17001E6E RID: 7790
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x0008BA67 File Offset: 0x00089C67
		// (set) Token: 0x06002DCF RID: 11727 RVA: 0x0008BA6F File Offset: 0x00089C6F
		[DataMember]
		public bool? PasswordRecoveryEnabled { get; set; }

		// Token: 0x17001E6F RID: 7791
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x0008BA78 File Offset: 0x00089C78
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x0008BA80 File Offset: 0x00089C80
		[DataMember]
		public bool? IsDevicePasswordExpirationSet { get; set; }

		// Token: 0x17001E70 RID: 7792
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x0008BA89 File Offset: 0x00089C89
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x0008BA91 File Offset: 0x00089C91
		[DataMember]
		public string DevicePasswordExpiration { get; set; }

		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x0008BA9A File Offset: 0x00089C9A
		// (set) Token: 0x06002DD5 RID: 11733 RVA: 0x0008BAA2 File Offset: 0x00089CA2
		[DataMember]
		public int? DevicePasswordHistory { get; set; }

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x0008BAAB File Offset: 0x00089CAB
		// (set) Token: 0x06002DD7 RID: 11735 RVA: 0x0008BABD File Offset: 0x00089CBD
		[DataMember]
		public string MaxCalendarAgeFilter
		{
			get
			{
				return (string)base["MaxCalendarAgeFilter"];
			}
			set
			{
				base["MaxCalendarAgeFilter"] = value;
			}
		}

		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x0008BACB File Offset: 0x00089CCB
		// (set) Token: 0x06002DD9 RID: 11737 RVA: 0x0008BADD File Offset: 0x00089CDD
		[DataMember]
		public string MaxEmailAgeFilter
		{
			get
			{
				return (string)base["MaxEmailAgeFilter"];
			}
			set
			{
				base["MaxEmailAgeFilter"] = value;
			}
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x0008BAEB File Offset: 0x00089CEB
		// (set) Token: 0x06002DDB RID: 11739 RVA: 0x0008BAF3 File Offset: 0x00089CF3
		[DataMember]
		public bool? IsMaxEmailBodyTruncationSizeSet { get; set; }

		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x0008BAFC File Offset: 0x00089CFC
		// (set) Token: 0x06002DDD RID: 11741 RVA: 0x0008BB04 File Offset: 0x00089D04
		[DataMember]
		public string MaxEmailBodyTruncationSize { get; set; }

		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x0008BB0D File Offset: 0x00089D0D
		// (set) Token: 0x06002DDF RID: 11743 RVA: 0x0008BB15 File Offset: 0x00089D15
		[DataMember]
		public bool? IsMaxAttachmentSizeSet { get; set; }

		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x0008BB1E File Offset: 0x00089D1E
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x0008BB26 File Offset: 0x00089D26
		[DataMember]
		public string MaxAttachmentSize { get; set; }

		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x0008BB2F File Offset: 0x00089D2F
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x0008BB4B File Offset: 0x00089D4B
		[DataMember]
		public bool RequireManualSyncWhenRoaming
		{
			get
			{
				return (bool)(base["RequireManualSyncWhenRoaming"] ?? false);
			}
			set
			{
				base["RequireManualSyncWhenRoaming"] = value;
			}
		}

		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x0008BB5E File Offset: 0x00089D5E
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x0008BB7A File Offset: 0x00089D7A
		[DataMember]
		public bool AttachmentsEnabled
		{
			get
			{
				return (bool)(base["AttachmentsEnabled"] ?? false);
			}
			set
			{
				base["AttachmentsEnabled"] = value;
			}
		}

		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x0008BB8D File Offset: 0x00089D8D
		// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x0008BBA9 File Offset: 0x00089DA9
		[DataMember]
		public bool AllowHTMLEmail
		{
			get
			{
				return (bool)(base["AllowHTMLEmail"] ?? false);
			}
			set
			{
				base["AllowHTMLEmail"] = value;
			}
		}

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x0008BBBC File Offset: 0x00089DBC
		// (set) Token: 0x06002DE9 RID: 11753 RVA: 0x0008BBD8 File Offset: 0x00089DD8
		[DataMember]
		public bool AllowTextMessaging
		{
			get
			{
				return (bool)(base["AllowTextMessaging"] ?? false);
			}
			set
			{
				base["AllowTextMessaging"] = value;
			}
		}

		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x0008BBEB File Offset: 0x00089DEB
		// (set) Token: 0x06002DEB RID: 11755 RVA: 0x0008BC07 File Offset: 0x00089E07
		[DataMember]
		public bool AllowStorageCard
		{
			get
			{
				return (bool)(base["AllowStorageCard"] ?? false);
			}
			set
			{
				base["AllowStorageCard"] = value;
			}
		}

		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x0008BC1A File Offset: 0x00089E1A
		// (set) Token: 0x06002DED RID: 11757 RVA: 0x0008BC36 File Offset: 0x00089E36
		[DataMember]
		public bool AllowCamera
		{
			get
			{
				return (bool)(base["AllowCamera"] ?? false);
			}
			set
			{
				base["AllowCamera"] = value;
			}
		}

		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x0008BC49 File Offset: 0x00089E49
		// (set) Token: 0x06002DEF RID: 11759 RVA: 0x0008BC65 File Offset: 0x00089E65
		[DataMember]
		public bool AllowWiFi
		{
			get
			{
				return (bool)(base["AllowWiFi"] ?? false);
			}
			set
			{
				base["AllowWiFi"] = value;
			}
		}

		// Token: 0x17001E7F RID: 7807
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x0008BC78 File Offset: 0x00089E78
		// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x0008BC94 File Offset: 0x00089E94
		[DataMember]
		public bool AllowIrDA
		{
			get
			{
				return (bool)(base["AllowIrDA"] ?? false);
			}
			set
			{
				base["AllowIrDA"] = value;
			}
		}

		// Token: 0x17001E80 RID: 7808
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x0008BCA7 File Offset: 0x00089EA7
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x0008BCC3 File Offset: 0x00089EC3
		[DataMember]
		public bool AllowInternetSharing
		{
			get
			{
				return (bool)(base["AllowInternetSharing"] ?? false);
			}
			set
			{
				base["AllowInternetSharing"] = value;
			}
		}

		// Token: 0x17001E81 RID: 7809
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x0008BCD6 File Offset: 0x00089ED6
		// (set) Token: 0x06002DF5 RID: 11765 RVA: 0x0008BCF2 File Offset: 0x00089EF2
		[DataMember]
		public bool AllowBrowser
		{
			get
			{
				return (bool)(base["AllowBrowser"] ?? false);
			}
			set
			{
				base["AllowBrowser"] = value;
			}
		}

		// Token: 0x17001E82 RID: 7810
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x0008BD05 File Offset: 0x00089F05
		// (set) Token: 0x06002DF7 RID: 11767 RVA: 0x0008BD17 File Offset: 0x00089F17
		[DataMember]
		public string AllowBluetooth
		{
			get
			{
				return (string)base["AllowBluetooth"];
			}
			set
			{
				base["AllowBluetooth"] = value;
			}
		}

		// Token: 0x17001E83 RID: 7811
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x0008BD25 File Offset: 0x00089F25
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x0008BD2C File Offset: 0x00089F2C
		protected bool CheckAndParseParams<T>(bool isUnlimited, bool? isValueSet, bool? originalIsValueSet, string value, Func<string, T> convert, out object result) where T : struct, IComparable
		{
			bool result2 = false;
			result = null;
			if (isValueSet == false)
			{
				if (isUnlimited)
				{
					result = Unlimited<T>.UnlimitedString;
				}
				else
				{
					result = null;
				}
				result2 = true;
			}
			else if ((isValueSet == true || (isValueSet == null && originalIsValueSet == true)) && !string.IsNullOrEmpty(value))
			{
				try
				{
					if (isUnlimited)
					{
						result = new Unlimited<T>(convert(value));
					}
					else
					{
						result = convert(value);
					}
					result2 = true;
				}
				catch (Exception)
				{
					result2 = false;
				}
			}
			return result2;
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0008BE48 File Offset: 0x0008A048
		public virtual void ProcessPolicyParams(ActiveSyncMailboxPolicyObject originalPolicy)
		{
			bool flag = originalPolicy == null;
			bool flag2 = this.DevicePasswordEnabled ?? (!flag && originalPolicy.DevicePasswordEnabled);
			object value;
			if (this.CheckAndParseParams<EnhancedTimeSpan>(true, this.IsDevicePolicyRefreshIntervalSet, flag ? null : new bool?(originalPolicy.IsDevicePolicyRefreshIntervalSet), this.DevicePolicyRefreshInterval, (string x) => EnhancedTimeSpan.FromHours(double.Parse(x)), out value))
			{
				base["DevicePolicyRefreshInterval"] = value;
			}
			if (this.CheckAndParseParams<int>(true, this.IsMaxEmailBodyTruncationSizeSet, flag ? null : new bool?(originalPolicy.IsMaxEmailBodyTruncationSizeSet), this.MaxEmailBodyTruncationSize, (string x) => int.Parse(x), out value))
			{
				base["MaxEmailBodyTruncationSize"] = value;
			}
			if (this.CheckAndParseParams<ByteQuantifiedSize>(true, this.IsMaxAttachmentSizeSet, flag ? null : new bool?(originalPolicy.IsMaxAttachmentSizeSet), this.MaxAttachmentSize, (string x) => ByteQuantifiedSize.FromKB(ulong.Parse(x)), out value))
			{
				base["MaxAttachmentSize"] = value;
			}
			if (flag2)
			{
				if (this.AlphanumericDevicePasswordRequired == true || (!flag && this.AlphanumericDevicePasswordRequired == null && originalPolicy.AlphanumericDevicePasswordRequired))
				{
					int? minDevicePasswordComplexCharacters = this.MinDevicePasswordComplexCharacters;
					int? num = (minDevicePasswordComplexCharacters != null) ? new int?(minDevicePasswordComplexCharacters.GetValueOrDefault()) : ((flag || originalPolicy.AlphanumericDevicePasswordRequired) ? this.MinDevicePasswordComplexCharacters : new int?(3));
					if (num != null)
					{
						base["MinDevicePasswordComplexCharacters"] = num;
					}
				}
				if (this.CheckAndParseParams<int>(false, this.IsMinDevicePasswordLengthSet, flag ? null : new bool?(originalPolicy.IsMinDevicePasswordLengthSet), this.MinDevicePasswordLength ?? ((flag || originalPolicy.IsMinDevicePasswordLengthSet) ? this.MinDevicePasswordLength : "4"), (string x) => int.Parse(x), out value))
				{
					base["MinDevicePasswordLength"] = value;
				}
				if (this.CheckAndParseParams<int>(false, this.IsMinDevicePasswordLengthSet, flag ? null : new bool?(originalPolicy.IsMinDevicePasswordLengthSet), this.MinDevicePasswordLength ?? ((flag || originalPolicy.IsMinDevicePasswordLengthSet) ? this.MinDevicePasswordLength : "4"), (string x) => int.Parse(x), out value))
				{
					base["MinDevicePasswordLength"] = value;
				}
				if (this.CheckAndParseParams<int>(true, this.IsMaxDevicePasswordFailedAttemptsSet, flag ? null : new bool?(originalPolicy.IsMaxDevicePasswordFailedAttemptsSet), this.MaxDevicePasswordFailedAttempts ?? ((flag || originalPolicy.IsMaxDevicePasswordFailedAttemptsSet) ? this.MaxDevicePasswordFailedAttempts : "8"), (string x) => int.Parse(x), out value))
				{
					base["MaxDevicePasswordFailedAttempts"] = value;
				}
				if (this.CheckAndParseParams<EnhancedTimeSpan>(true, this.IsDevicePasswordExpirationSet, flag ? null : new bool?(originalPolicy.IsDevicePasswordExpirationSet), this.DevicePasswordExpiration ?? ((flag || originalPolicy.IsDevicePasswordExpirationSet) ? this.DevicePasswordExpiration : "90"), (string x) => EnhancedTimeSpan.FromDays(double.Parse(x)), out value))
				{
					base["DevicePasswordExpiration"] = value;
				}
				if (this.CheckAndParseParams<EnhancedTimeSpan>(true, this.IsMaxInactivityTimeDeviceLockSet, flag ? null : new bool?(originalPolicy.IsMaxInactivityTimeDeviceLockSet), this.MaxInactivityTimeDeviceLock ?? ((flag || originalPolicy.IsMaxInactivityTimeDeviceLockSet) ? this.MaxInactivityTimeDeviceLock : "15"), (string x) => EnhancedTimeSpan.FromMinutes(double.Parse(x)), out value))
				{
					base["MaxInactivityTimeDeviceLock"] = value;
				}
				if (this.RequireDeviceEncryption != null)
				{
					base["RequireDeviceEncryption"] = this.RequireDeviceEncryption;
				}
				if (this.RequireStorageCardEncryption != null)
				{
					base["RequireStorageCardEncryption"] = this.RequireStorageCardEncryption;
				}
				if (this.AllowSimpleDevicePassword != null)
				{
					base["AllowSimpleDevicePassword"] = this.AllowSimpleDevicePassword;
				}
				if (this.AlphanumericDevicePasswordRequired != null)
				{
					base["AlphanumericDevicePasswordRequired"] = this.AlphanumericDevicePasswordRequired;
				}
				if (this.PasswordRecoveryEnabled != null)
				{
					base["PasswordRecoveryEnabled"] = this.PasswordRecoveryEnabled;
				}
				if (this.DevicePasswordHistory != null)
				{
					base["DevicePasswordHistory"] = this.DevicePasswordHistory;
				}
			}
		}
	}
}
