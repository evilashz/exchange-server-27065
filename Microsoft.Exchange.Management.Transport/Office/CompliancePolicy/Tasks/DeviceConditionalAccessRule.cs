using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	public sealed class DeviceConditionalAccessRule : DeviceRuleBase
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x00021A39 File Offset: 0x0001FC39
		public DeviceConditionalAccessRule()
		{
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00021A41 File Offset: 0x0001FC41
		public DeviceConditionalAccessRule(RuleStorage ruleStorage) : base(ruleStorage)
		{
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00021A4A File Offset: 0x0001FC4A
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00021A52 File Offset: 0x0001FC52
		public bool? AllowJailbroken { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00021A5B File Offset: 0x0001FC5B
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x00021A63 File Offset: 0x0001FC63
		public bool? PasswordRequired { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00021A6C File Offset: 0x0001FC6C
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00021A74 File Offset: 0x0001FC74
		public bool? PhoneMemoryEncrypted { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00021A7D File Offset: 0x0001FC7D
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x00021A85 File Offset: 0x0001FC85
		public TimeSpan? PasswordTimeout { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00021A8E File Offset: 0x0001FC8E
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x00021A96 File Offset: 0x0001FC96
		public int? PasswordMinimumLength { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00021A9F File Offset: 0x0001FC9F
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x00021AA7 File Offset: 0x0001FCA7
		public int? PasswordHistoryCount { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00021AB0 File Offset: 0x0001FCB0
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00021AB8 File Offset: 0x0001FCB8
		public int? PasswordExpirationDays { get; set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00021AC1 File Offset: 0x0001FCC1
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x00021AC9 File Offset: 0x0001FCC9
		public int? PasswordMinComplexChars { get; set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00021AD2 File Offset: 0x0001FCD2
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x00021ADA File Offset: 0x0001FCDA
		public bool? AllowSimplePassword { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00021AE3 File Offset: 0x0001FCE3
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x00021AEB File Offset: 0x0001FCEB
		public int? PasswordQuality { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00021AF4 File Offset: 0x0001FCF4
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x00021AFC File Offset: 0x0001FCFC
		public int? MaxPasswordAttemptsBeforeWipe { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00021B05 File Offset: 0x0001FD05
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x00021B0D File Offset: 0x0001FD0D
		public bool? EnableRemovableStorage { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00021B16 File Offset: 0x0001FD16
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00021B1E File Offset: 0x0001FD1E
		public bool? CameraEnabled { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00021B27 File Offset: 0x0001FD27
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00021B2F File Offset: 0x0001FD2F
		public bool? BluetoothEnabled { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00021B38 File Offset: 0x0001FD38
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00021B40 File Offset: 0x0001FD40
		public bool? ForceEncryptedBackup { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00021B49 File Offset: 0x0001FD49
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00021B51 File Offset: 0x0001FD51
		public bool? AllowiCloudDocSync { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00021B5A File Offset: 0x0001FD5A
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00021B62 File Offset: 0x0001FD62
		public bool? AllowiCloudPhotoSync { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00021B6B File Offset: 0x0001FD6B
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x00021B73 File Offset: 0x0001FD73
		public bool? AllowiCloudBackup { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00021B7C File Offset: 0x0001FD7C
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00021B84 File Offset: 0x0001FD84
		public CARatingRegionEntry? RegionRatings { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00021B8D File Offset: 0x0001FD8D
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00021B95 File Offset: 0x0001FD95
		public CARatingMovieEntry? MoviesRating { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00021B9E File Offset: 0x0001FD9E
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00021BA6 File Offset: 0x0001FDA6
		public CARatingTvShowEntry? TVShowsRating { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00021BAF File Offset: 0x0001FDAF
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00021BB7 File Offset: 0x0001FDB7
		public CARatingAppsEntry? AppsRating { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00021BC0 File Offset: 0x0001FDC0
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00021BC8 File Offset: 0x0001FDC8
		public bool? AllowVoiceDialing { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00021BD1 File Offset: 0x0001FDD1
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00021BD9 File Offset: 0x0001FDD9
		public bool? AllowVoiceAssistant { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00021BE2 File Offset: 0x0001FDE2
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x00021BEA File Offset: 0x0001FDEA
		public bool? AllowAssistantWhileLocked { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00021BF3 File Offset: 0x0001FDF3
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x00021BFB File Offset: 0x0001FDFB
		public bool? AllowScreenshot { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00021C04 File Offset: 0x0001FE04
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x00021C0C File Offset: 0x0001FE0C
		public bool? AllowVideoConferencing { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00021C15 File Offset: 0x0001FE15
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00021C1D File Offset: 0x0001FE1D
		public bool? AllowPassbookWhileLocked { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00021C26 File Offset: 0x0001FE26
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x00021C2E File Offset: 0x0001FE2E
		public bool? AllowDiagnosticSubmission { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00021C37 File Offset: 0x0001FE37
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00021C3F File Offset: 0x0001FE3F
		public bool? AllowConvenienceLogon { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00021C48 File Offset: 0x0001FE48
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x00021C50 File Offset: 0x0001FE50
		public TimeSpan? MaxPasswordGracePeriod { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00021C59 File Offset: 0x0001FE59
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00021C61 File Offset: 0x0001FE61
		public bool? AllowAppStore { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00021C6A File Offset: 0x0001FE6A
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x00021C72 File Offset: 0x0001FE72
		public bool? ForceAppStorePassword { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00021C7B File Offset: 0x0001FE7B
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x00021C83 File Offset: 0x0001FE83
		public bool? SystemSecurityTLS { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00021C8C File Offset: 0x0001FE8C
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x00021C94 File Offset: 0x0001FE94
		public CAUserAccountControlStatusEntry? UserAccountControlStatus { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00021C9D File Offset: 0x0001FE9D
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x00021CA5 File Offset: 0x0001FEA5
		public CAFirewallStatusEntry? FirewallStatus { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00021CAE File Offset: 0x0001FEAE
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x00021CB6 File Offset: 0x0001FEB6
		public CAAutoUpdateStatusEntry? AutoUpdateStatus { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00021CBF File Offset: 0x0001FEBF
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00021CC7 File Offset: 0x0001FEC7
		public string AntiVirusStatus { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00021CD0 File Offset: 0x0001FED0
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x00021CD8 File Offset: 0x0001FED8
		public bool? AntiVirusSignatureStatus { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00021CE1 File Offset: 0x0001FEE1
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x00021CE9 File Offset: 0x0001FEE9
		public bool? SmartScreenEnabled { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00021CF2 File Offset: 0x0001FEF2
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00021CFA File Offset: 0x0001FEFA
		public string WorkFoldersSyncUrl { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00021D03 File Offset: 0x0001FF03
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00021D0B File Offset: 0x0001FF0B
		public string PasswordComplexity { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00021D14 File Offset: 0x0001FF14
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x00021D1C File Offset: 0x0001FF1C
		public bool? WLANEnabled { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00021D25 File Offset: 0x0001FF25
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x00021D2D File Offset: 0x0001FF2D
		public string AccountName { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00021D36 File Offset: 0x0001FF36
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x00021D3E File Offset: 0x0001FF3E
		public string AccountUserName { get; set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00021D47 File Offset: 0x0001FF47
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x00021D4F File Offset: 0x0001FF4F
		public string ExchangeActiveSyncHost { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00021D58 File Offset: 0x0001FF58
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x00021D60 File Offset: 0x0001FF60
		public string EmailAddress { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00021D69 File Offset: 0x0001FF69
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00021D71 File Offset: 0x0001FF71
		public bool? UseSSL { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00021D7A File Offset: 0x0001FF7A
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00021D82 File Offset: 0x0001FF82
		public bool? AllowMove { get; set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00021D8B File Offset: 0x0001FF8B
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00021D93 File Offset: 0x0001FF93
		public bool? AllowRecentAddressSyncing { get; set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00021D9C File Offset: 0x0001FF9C
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00021DA4 File Offset: 0x0001FFA4
		public long? DaysToSync { get; set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00021DAD File Offset: 0x0001FFAD
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x00021DB5 File Offset: 0x0001FFB5
		public long? ContentType { get; set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00021DBE File Offset: 0x0001FFBE
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x00021DC6 File Offset: 0x0001FFC6
		public bool? UseSMIME { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00021DCF File Offset: 0x0001FFCF
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x00021DD7 File Offset: 0x0001FFD7
		public long? SyncSchedule { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00021DE0 File Offset: 0x0001FFE0
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00021DE8 File Offset: 0x0001FFE8
		public bool? UseOnlyInEmail { get; set; }

		// Token: 0x06000871 RID: 2161 RVA: 0x00021DF4 File Offset: 0x0001FFF4
		protected override IEnumerable<Condition> GetTaskConditions()
		{
			List<Condition> list = new List<Condition>();
			if (base.TargetGroups != null)
			{
				List<string> list2 = new List<string>();
				foreach (Guid guid in base.TargetGroups)
				{
					list2.Add(guid.ToString());
				}
				list.Add(new IsPredicate(Property.CreateProperty("isMemberOf", typeof(Guid).ToString()), list2));
			}
			if (this.AllowJailbroken != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Security_Jailbroken, typeof(string).ToString()), new List<string>
				{
					this.AllowJailbroken.ToString()
				}));
			}
			if (this.PasswordRequired != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_Required, typeof(string).ToString()), new List<string>
				{
					this.PasswordRequired.ToString()
				}));
			}
			if (this.PhoneMemoryEncrypted != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Encryption_PhoneMemoryEncrypted, typeof(string).ToString()), new List<string>
				{
					this.PhoneMemoryEncrypted.ToString()
				}));
			}
			if (this.PasswordTimeout != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_Timeout, typeof(string).ToString()), new List<string>
				{
					this.PasswordTimeout.GetValueOrDefault().Subtract(TimeSpan.FromSeconds((double)this.PasswordTimeout.GetValueOrDefault().Seconds)).TotalMinutes.ToString()
				}));
			}
			if (this.PasswordMinimumLength != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_MinimumLength, typeof(string).ToString()), new List<string>
				{
					this.PasswordMinimumLength.ToString()
				}));
			}
			if (this.PasswordHistoryCount != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_History, typeof(string).ToString()), new List<string>
				{
					this.PasswordHistoryCount.ToString()
				}));
			}
			if (this.PasswordExpirationDays != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_Expiration, typeof(string).ToString()), new List<string>
				{
					this.PasswordExpirationDays.ToString()
				}));
			}
			if (this.PasswordMinComplexChars != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_MinComplexChars, typeof(string).ToString()), new List<string>
				{
					this.PasswordMinComplexChars.ToString()
				}));
			}
			if (this.AllowSimplePassword != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_AllowSimplePassword, typeof(string).ToString()), new List<string>
				{
					this.AllowSimplePassword.ToString()
				}));
			}
			if (this.PasswordQuality != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_PasswordQuality, typeof(string).ToString()), new List<string>
				{
					this.PasswordQuality.ToString()
				}));
			}
			if (this.MaxPasswordAttemptsBeforeWipe != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_MaxAttemptsBeforeWipe, typeof(string).ToString()), new List<string>
				{
					this.MaxPasswordAttemptsBeforeWipe.ToString()
				}));
			}
			if (this.EnableRemovableStorage != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Security_EnableRemovableStorage, typeof(string).ToString()), new List<string>
				{
					this.EnableRemovableStorage.ToString()
				}));
			}
			if (this.CameraEnabled != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Security_CameraEnabled, typeof(string).ToString()), new List<string>
				{
					this.CameraEnabled.ToString()
				}));
			}
			if (this.BluetoothEnabled != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Security_BluetoothEnabled, typeof(string).ToString()), new List<string>
				{
					this.BluetoothEnabled.ToString()
				}));
			}
			if (this.ForceEncryptedBackup != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Cloud_ForceEncryptedBackup, typeof(string).ToString()), new List<string>
				{
					this.ForceEncryptedBackup.ToString()
				}));
			}
			if (this.AllowiCloudDocSync != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Cloud_AllowiCloudDocSync, typeof(string).ToString()), new List<string>
				{
					this.AllowiCloudDocSync.ToString()
				}));
			}
			if (this.AllowiCloudPhotoSync != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Cloud_AllowiCloudPhotoSync, typeof(string).ToString()), new List<string>
				{
					this.AllowiCloudPhotoSync.ToString()
				}));
			}
			if (this.AllowiCloudBackup != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Cloud_AllowiCloudBackup, typeof(string).ToString()), new List<string>
				{
					this.AllowiCloudBackup.ToString()
				}));
			}
			if (this.RegionRatings != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_RatingsRegion, typeof(string).ToString()), new List<string>
				{
					this.RegionRatings.ToString()
				}));
			}
			if (this.MoviesRating != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_RatingMovies, typeof(string).ToString()), new List<string>
				{
					((int)this.MoviesRating.Value).ToString()
				}));
			}
			if (this.TVShowsRating != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_RatingTVShows, typeof(string).ToString()), new List<string>
				{
					((int)this.TVShowsRating.Value).ToString()
				}));
			}
			if (this.AppsRating != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_RatingApps, typeof(string).ToString()), new List<string>
				{
					((int)this.AppsRating.Value).ToString()
				}));
			}
			if (this.AllowVoiceDialing != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_AllowVoiceDialing, typeof(string).ToString()), new List<string>
				{
					this.AllowVoiceDialing.ToString()
				}));
			}
			if (this.AllowVoiceAssistant != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_AllowVoiceAssistant, typeof(string).ToString()), new List<string>
				{
					this.AllowVoiceAssistant.ToString()
				}));
			}
			if (this.AllowAssistantWhileLocked != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_AllowAssistantWhileLocked, typeof(string).ToString()), new List<string>
				{
					this.AllowAssistantWhileLocked.ToString()
				}));
			}
			if (this.AllowScreenshot != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_AllowScreenshot, typeof(string).ToString()), new List<string>
				{
					this.AllowScreenshot.ToString()
				}));
			}
			if (this.AllowVideoConferencing != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_AllowVideoConferencing, typeof(string).ToString()), new List<string>
				{
					this.AllowVideoConferencing.ToString()
				}));
			}
			if (this.AllowPassbookWhileLocked != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_AllowPassbookWhileLocked, typeof(string).ToString()), new List<string>
				{
					this.AllowPassbookWhileLocked.ToString()
				}));
			}
			if (this.AllowDiagnosticSubmission != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_AllowDiagnosticSubmission, typeof(string).ToString()), new List<string>
				{
					this.AllowDiagnosticSubmission.ToString()
				}));
			}
			if (this.AllowConvenienceLogon != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_AllowConvenienceLogon, typeof(string).ToString()), new List<string>
				{
					this.AllowConvenienceLogon.ToString()
				}));
			}
			if (this.MaxPasswordGracePeriod != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_MaxGracePeriod, typeof(string).ToString()), new List<string>
				{
					this.MaxPasswordGracePeriod.GetValueOrDefault().Subtract(TimeSpan.FromSeconds((double)this.MaxPasswordGracePeriod.GetValueOrDefault().Seconds)).TotalMinutes.ToString()
				}));
			}
			if (this.AllowAppStore != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_AllowAppStore, typeof(string).ToString()), new List<string>
				{
					this.AllowAppStore.ToString()
				}));
			}
			if (this.ForceAppStorePassword != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Restrictions_ForceAppStorePassword, typeof(string).ToString()), new List<string>
				{
					this.ForceAppStorePassword.ToString()
				}));
			}
			if (this.SystemSecurityTLS != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_SystemSecurity_TLS, typeof(string).ToString()), new List<string>
				{
					this.SystemSecurityTLS.ToString()
				}));
			}
			if (this.UserAccountControlStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_SystemSecurity_UserAccountControlStatus, typeof(string).ToString()), new List<string>
				{
					((int)this.UserAccountControlStatus.Value).ToString()
				}));
			}
			if (this.FirewallStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_SystemSecurity_FirewallStatus, typeof(string).ToString()), new List<string>
				{
					((int)this.FirewallStatus.Value).ToString()
				}));
			}
			if (this.AutoUpdateStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_SystemSecurity_AutoUpdateStatus, typeof(string).ToString()), new List<string>
				{
					((int)this.AutoUpdateStatus.Value).ToString()
				}));
			}
			if (this.AntiVirusStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_SystemSecurity_AntiVirusStatus, typeof(string).ToString()), new List<string>
				{
					this.AntiVirusStatus.ToString()
				}));
			}
			if (this.AntiVirusSignatureStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_SystemSecurity_AntiVirusSignatureStatus, typeof(string).ToString()), new List<string>
				{
					this.AntiVirusSignatureStatus.ToString()
				}));
			}
			if (this.SmartScreenEnabled != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_InternetExplorer_SmartScreenEnabled, typeof(string).ToString()), new List<string>
				{
					this.SmartScreenEnabled.ToString()
				}));
			}
			if (this.WorkFoldersSyncUrl != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_WorkFolders_SyncUrl, typeof(string).ToString()), new List<string>
				{
					this.WorkFoldersSyncUrl.ToString()
				}));
			}
			if (this.PasswordComplexity != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Password_Type, typeof(string).ToString()), new List<string>
				{
					this.PasswordComplexity.ToString()
				}));
			}
			if (this.WLANEnabled != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Device_Wireless_WLANEnabled, typeof(string).ToString()), new List<string>
				{
					this.WLANEnabled.ToString()
				}));
			}
			if (this.AccountName != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_AccountName, typeof(string).ToString()), new List<string>
				{
					this.AccountName.ToString()
				}));
			}
			if (this.AccountUserName != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_UserName, typeof(string).ToString()), new List<string>
				{
					this.AccountUserName.ToString()
				}));
			}
			if (this.ExchangeActiveSyncHost != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_Host, typeof(string).ToString()), new List<string>
				{
					this.ExchangeActiveSyncHost.ToString()
				}));
			}
			if (this.EmailAddress != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_EmailAddress, typeof(string).ToString()), new List<string>
				{
					this.EmailAddress.ToString()
				}));
			}
			if (this.UseSSL != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_UseSSL, typeof(string).ToString()), new List<string>
				{
					this.UseSSL.ToString()
				}));
			}
			if (this.AllowMove != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_PreventAppSheet, typeof(string).ToString()), new List<string>
				{
					this.AllowMove.ToString()
				}));
			}
			if (this.AllowRecentAddressSyncing != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_DisableMailRecentsSyncing, typeof(string).ToString()), new List<string>
				{
					this.AllowRecentAddressSyncing.ToString()
				}));
			}
			if (this.DaysToSync != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_MailNumberOfPastDaysToSync, typeof(string).ToString()), new List<string>
				{
					this.DaysToSync.ToString()
				}));
			}
			if (this.ContentType != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_ContentTypeToSync, typeof(string).ToString()), new List<string>
				{
					this.ContentType.ToString()
				}));
			}
			if (this.UseSMIME != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_UseSMIME, typeof(string).ToString()), new List<string>
				{
					this.UseSMIME.ToString()
				}));
			}
			if (this.SyncSchedule != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_SyncSchedule, typeof(string).ToString()), new List<string>
				{
					this.SyncSchedule.ToString()
				}));
			}
			if (this.UseOnlyInEmail != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConditionalAccessRule.Eas_PreventMove, typeof(string).ToString()), new List<string>
				{
					this.UseOnlyInEmail.ToString()
				}));
			}
			return list;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000231D4 File Offset: 0x000213D4
		protected override void SetTaskConditions(IEnumerable<Condition> conditions)
		{
			foreach (Condition condition in conditions)
			{
				if (condition.GetType() == typeof(NameValuesPairConfigurationPredicate) || condition.GetType() == typeof(IsPredicate))
				{
					IsPredicate isPredicate = condition as IsPredicate;
					if (isPredicate != null)
					{
						MultiValuedProperty<Guid> multiValuedProperty = new MultiValuedProperty<Guid>();
						if (isPredicate.Property.Name.Equals("isMemberOf"))
						{
							if (isPredicate.Value.ParsedValue is Guid)
							{
								multiValuedProperty.Add(isPredicate.Value.ParsedValue);
							}
							if (isPredicate.Value.ParsedValue is List<Guid>)
							{
								foreach (string item in ((List<string>)isPredicate.Value.ParsedValue))
								{
									multiValuedProperty.Add(item);
								}
							}
							base.TargetGroups = multiValuedProperty;
						}
					}
					else
					{
						NameValuesPairConfigurationPredicate nameValuesPairConfigurationPredicate = condition as NameValuesPairConfigurationPredicate;
						if (nameValuesPairConfigurationPredicate != null)
						{
							bool value;
							if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Security_Jailbroken))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowJailbroken = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_Required))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.PasswordRequired = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Encryption_PhoneMemoryEncrypted))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.PhoneMemoryEncrypted = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_Timeout))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordTimeout = new TimeSpan?(TimeSpan.FromMinutes((double)num));
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_MinimumLength))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordMinimumLength = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_History))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordHistoryCount = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_Expiration))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordExpirationDays = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_MinComplexChars))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordMinComplexChars = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_AllowSimplePassword))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowSimplePassword = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_PasswordQuality))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordQuality = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_MaxAttemptsBeforeWipe))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.MaxPasswordAttemptsBeforeWipe = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Security_EnableRemovableStorage))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.EnableRemovableStorage = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Security_CameraEnabled))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.CameraEnabled = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Security_BluetoothEnabled))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.BluetoothEnabled = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Cloud_ForceEncryptedBackup))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.ForceEncryptedBackup = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Cloud_AllowiCloudDocSync))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowiCloudDocSync = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Cloud_AllowiCloudPhotoSync))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowiCloudPhotoSync = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Cloud_AllowiCloudBackup))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowiCloudBackup = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_RatingsRegion))
							{
								CARatingRegionEntry value2;
								if (Enum.TryParse<CARatingRegionEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value2))
								{
									this.RegionRatings = new CARatingRegionEntry?(value2);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_RatingMovies))
							{
								CARatingMovieEntry value3;
								if (Enum.TryParse<CARatingMovieEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value3))
								{
									this.MoviesRating = new CARatingMovieEntry?(value3);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_RatingTVShows))
							{
								CARatingTvShowEntry value4;
								if (Enum.TryParse<CARatingTvShowEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value4))
								{
									this.TVShowsRating = new CARatingTvShowEntry?(value4);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_RatingApps))
							{
								CARatingAppsEntry value5;
								if (Enum.TryParse<CARatingAppsEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value5))
								{
									this.AppsRating = new CARatingAppsEntry?(value5);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_AllowVoiceDialing))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowVoiceDialing = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_AllowVoiceAssistant))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowVoiceAssistant = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_AllowAssistantWhileLocked))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowAssistantWhileLocked = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_AllowScreenshot))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowScreenshot = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_AllowVideoConferencing))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowVideoConferencing = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_AllowPassbookWhileLocked))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowPassbookWhileLocked = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_AllowDiagnosticSubmission))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowDiagnosticSubmission = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_AllowConvenienceLogon))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowConvenienceLogon = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_MaxGracePeriod))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.MaxPasswordGracePeriod = new TimeSpan?(TimeSpan.FromMinutes((double)num));
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_AllowAppStore))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowAppStore = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Restrictions_ForceAppStorePassword))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.ForceAppStorePassword = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_SystemSecurity_TLS))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.SystemSecurityTLS = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_SystemSecurity_UserAccountControlStatus))
							{
								CAUserAccountControlStatusEntry value6;
								if (Enum.TryParse<CAUserAccountControlStatusEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value6))
								{
									this.UserAccountControlStatus = new CAUserAccountControlStatusEntry?(value6);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_SystemSecurity_FirewallStatus))
							{
								CAFirewallStatusEntry value7;
								if (Enum.TryParse<CAFirewallStatusEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value7))
								{
									this.FirewallStatus = new CAFirewallStatusEntry?(value7);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_SystemSecurity_AutoUpdateStatus))
							{
								CAAutoUpdateStatusEntry value8;
								if (Enum.TryParse<CAAutoUpdateStatusEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value8))
								{
									this.AutoUpdateStatus = new CAAutoUpdateStatusEntry?(value8);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_SystemSecurity_AntiVirusStatus))
							{
								this.AntiVirusStatus = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_SystemSecurity_AntiVirusSignatureStatus))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AntiVirusSignatureStatus = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_InternetExplorer_SmartScreenEnabled))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.SmartScreenEnabled = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_WorkFolders_SyncUrl))
							{
								this.WorkFoldersSyncUrl = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Password_Type))
							{
								this.PasswordComplexity = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Device_Wireless_WLANEnabled))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.WLANEnabled = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_AccountName))
							{
								this.AccountName = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_UserName))
							{
								this.AccountUserName = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_Host))
							{
								this.ExchangeActiveSyncHost = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_EmailAddress))
							{
								this.EmailAddress = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_UseSSL))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.UseSSL = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_PreventAppSheet))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowMove = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_DisableMailRecentsSyncing))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowRecentAddressSyncing = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_MailNumberOfPastDaysToSync))
							{
								long value9;
								if (long.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value9))
								{
									this.DaysToSync = new long?(value9);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_ContentTypeToSync))
							{
								long value9;
								if (long.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value9))
								{
									this.ContentType = new long?(value9);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_UseSMIME))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.UseSMIME = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_SyncSchedule))
							{
								long value9;
								if (long.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value9))
								{
									this.SyncSchedule = new long?(value9);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConditionalAccessRule.Eas_PreventMove) && bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
							{
								this.UseOnlyInEmail = new bool?(value);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400032C RID: 812
		public static string Device_Security_Jailbroken = "Device_Security_Jailbroken";

		// Token: 0x0400032D RID: 813
		public static string Device_Password_Required = "Device_Password_Required";

		// Token: 0x0400032E RID: 814
		public static string Device_Encryption_PhoneMemoryEncrypted = "Device_Encryption_PhoneMemoryEncrypted";

		// Token: 0x0400032F RID: 815
		public static string Device_Password_Timeout = "Device_Password_Timeout";

		// Token: 0x04000330 RID: 816
		public static string Device_Password_MinimumLength = "Device_Password_MinimumLength";

		// Token: 0x04000331 RID: 817
		public static string Device_Password_History = "Device_Password_History";

		// Token: 0x04000332 RID: 818
		public static string Device_Password_Expiration = "Device_Password_Expiration";

		// Token: 0x04000333 RID: 819
		public static string Device_Password_MinComplexChars = "Device_Password_MinComplexChars";

		// Token: 0x04000334 RID: 820
		public static string Device_Password_AllowSimplePassword = "Device_Password_AllowSimplePassword";

		// Token: 0x04000335 RID: 821
		public static string Device_Password_PasswordQuality = "Device_Password_PasswordQuality";

		// Token: 0x04000336 RID: 822
		public static string Device_Password_MaxAttemptsBeforeWipe = "Device_Password_MaxAttemptsBeforeWipe";

		// Token: 0x04000337 RID: 823
		public static string Device_Security_EnableRemovableStorage = "Device_Security_EnableRemovableStorage";

		// Token: 0x04000338 RID: 824
		public static string Device_Security_CameraEnabled = "Device_Security_CameraEnabled";

		// Token: 0x04000339 RID: 825
		public static string Device_Security_BluetoothEnabled = "Device_Security_BluetoothEnabled";

		// Token: 0x0400033A RID: 826
		public static string Device_Cloud_ForceEncryptedBackup = "Device_Cloud_ForceEncryptedBackup";

		// Token: 0x0400033B RID: 827
		public static string Device_Cloud_AllowiCloudDocSync = "Device_Cloud_AllowiCloudDocSync";

		// Token: 0x0400033C RID: 828
		public static string Device_Cloud_AllowiCloudPhotoSync = "Device_Cloud_AllowiCloudPhotoSync";

		// Token: 0x0400033D RID: 829
		public static string Device_Cloud_AllowiCloudBackup = "Device_Cloud_AllowiCloudBackup";

		// Token: 0x0400033E RID: 830
		public static string Device_Restrictions_RatingsRegion = "Device_Restrictions_RatingsRegion";

		// Token: 0x0400033F RID: 831
		public static string Device_Restrictions_RatingMovies = "Device_Restrictions_RatingMovies";

		// Token: 0x04000340 RID: 832
		public static string Device_Restrictions_RatingTVShows = "Device_Restrictions_RatingTVShows";

		// Token: 0x04000341 RID: 833
		public static string Device_Restrictions_RatingApps = "Device_Restrictions_RatingApps";

		// Token: 0x04000342 RID: 834
		public static string Device_Restrictions_AllowVoiceDialing = "Device_Restrictions_AllowVoiceDialing";

		// Token: 0x04000343 RID: 835
		public static string Device_Restrictions_AllowVoiceAssistant = "Device_Restrictions_AllowVoiceAssistant";

		// Token: 0x04000344 RID: 836
		public static string Device_Restrictions_AllowAssistantWhileLocked = "Device_Restrictions_AllowAssistantWhileLocked";

		// Token: 0x04000345 RID: 837
		public static string Device_Restrictions_AllowScreenshot = "Device_Restrictions_AllowScreenshot";

		// Token: 0x04000346 RID: 838
		public static string Device_Restrictions_AllowVideoConferencing = "Device_Restrictions_AllowVideoConferencing";

		// Token: 0x04000347 RID: 839
		public static string Device_Restrictions_AllowPassbookWhileLocked = "Device_Restrictions_AllowPassbookWhileLocked";

		// Token: 0x04000348 RID: 840
		public static string Device_Restrictions_AllowDiagnosticSubmission = "Device_Restrictions_AllowDiagnosticSubmission";

		// Token: 0x04000349 RID: 841
		public static string Device_Password_AllowConvenienceLogon = "Device_Password_AllowConvenienceLogon";

		// Token: 0x0400034A RID: 842
		public static string Device_Password_MaxGracePeriod = "Device_Password_MaxGracePeriod";

		// Token: 0x0400034B RID: 843
		public static string Device_Restrictions_AllowAppStore = "Device_Restrictions_AllowAppStore";

		// Token: 0x0400034C RID: 844
		public static string Device_Restrictions_ForceAppStorePassword = "Device_Restrictions_ForceAppStorePassword";

		// Token: 0x0400034D RID: 845
		public static string Device_SystemSecurity_TLS = "Device_SystemSecurity_TLS";

		// Token: 0x0400034E RID: 846
		public static string Device_SystemSecurity_UserAccountControlStatus = "Device_SystemSecurity_UserAccountControlStatus";

		// Token: 0x0400034F RID: 847
		public static string Device_SystemSecurity_FirewallStatus = "Device_SystemSecurity_FirewallStatus";

		// Token: 0x04000350 RID: 848
		public static string Device_SystemSecurity_AutoUpdateStatus = "Device_SystemSecurity_AutoUpdateStatus";

		// Token: 0x04000351 RID: 849
		public static string Device_SystemSecurity_AntiVirusStatus = "Device_SystemSecurity_AntiVirusStatus";

		// Token: 0x04000352 RID: 850
		public static string Device_SystemSecurity_AntiVirusSignatureStatus = "Device_SystemSecurity_AntiVirusSignatureStatus";

		// Token: 0x04000353 RID: 851
		public static string Device_InternetExplorer_SmartScreenEnabled = "Device_InternetExplorer_SmartScreenEnabled";

		// Token: 0x04000354 RID: 852
		public static string Device_WorkFolders_SyncUrl = "Device_WorkFolders_SyncUrl";

		// Token: 0x04000355 RID: 853
		public static string Device_Password_Type = "Device_Password_Type";

		// Token: 0x04000356 RID: 854
		public static string Device_Wireless_WLANEnabled = "Device_Wireless_WLANEnabled";

		// Token: 0x04000357 RID: 855
		public static string Eas_AccountName = "Eas_AccountName";

		// Token: 0x04000358 RID: 856
		public static string Eas_UserName = "Eas_UserName";

		// Token: 0x04000359 RID: 857
		public static string Eas_Host = "Eas_Host";

		// Token: 0x0400035A RID: 858
		public static string Eas_EmailAddress = "Eas_EmailAddress";

		// Token: 0x0400035B RID: 859
		public static string Eas_UseSSL = "Eas_UseSSL";

		// Token: 0x0400035C RID: 860
		public static string Eas_PreventAppSheet = "Eas_PreventAppSheet";

		// Token: 0x0400035D RID: 861
		public static string Eas_DisableMailRecentsSyncing = "Eas_DisableMailRecentsSyncing";

		// Token: 0x0400035E RID: 862
		public static string Eas_MailNumberOfPastDaysToSync = "Eas_MailNumberOfPastDaysToSync";

		// Token: 0x0400035F RID: 863
		public static string Eas_ContentTypeToSync = "Eas_ContentTypeToSync";

		// Token: 0x04000360 RID: 864
		public static string Eas_UseSMIME = "Eas_UseSMIME";

		// Token: 0x04000361 RID: 865
		public static string Eas_SyncSchedule = "Eas_SyncSchedule";

		// Token: 0x04000362 RID: 866
		public static string Eas_PreventMove = "Eas_PreventMove";
	}
}
