using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public sealed class DeviceConfigurationRule : DeviceRuleBase
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0001F12C File Offset: 0x0001D32C
		public DeviceConfigurationRule()
		{
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001F134 File Offset: 0x0001D334
		public DeviceConfigurationRule(RuleStorage ruleStorage) : base(ruleStorage)
		{
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001F13D File Offset: 0x0001D33D
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0001F145 File Offset: 0x0001D345
		public bool? PasswordRequired { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001F14E File Offset: 0x0001D34E
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x0001F156 File Offset: 0x0001D356
		public bool? PhoneMemoryEncrypted { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001F15F File Offset: 0x0001D35F
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0001F167 File Offset: 0x0001D367
		public TimeSpan? PasswordTimeout { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001F170 File Offset: 0x0001D370
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0001F178 File Offset: 0x0001D378
		public int? PasswordMinimumLength { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001F181 File Offset: 0x0001D381
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0001F189 File Offset: 0x0001D389
		public int? PasswordHistoryCount { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001F192 File Offset: 0x0001D392
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0001F19A File Offset: 0x0001D39A
		public int? PasswordExpirationDays { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001F1A3 File Offset: 0x0001D3A3
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0001F1AB File Offset: 0x0001D3AB
		public int? MaxPasswordAttemptsBeforeWipe { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001F1B4 File Offset: 0x0001D3B4
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x0001F1BC File Offset: 0x0001D3BC
		public int? PasswordMinComplexChars { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001F1C5 File Offset: 0x0001D3C5
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x0001F1CD File Offset: 0x0001D3CD
		public bool? AllowSimplePassword { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001F1D6 File Offset: 0x0001D3D6
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x0001F1DE File Offset: 0x0001D3DE
		public bool? EnableRemovableStorage { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001F1E7 File Offset: 0x0001D3E7
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0001F1EF File Offset: 0x0001D3EF
		public bool? CameraEnabled { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0001F200 File Offset: 0x0001D400
		public bool? BluetoothEnabled { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001F209 File Offset: 0x0001D409
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x0001F211 File Offset: 0x0001D411
		public bool? ForceEncryptedBackup { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0001F21A File Offset: 0x0001D41A
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0001F222 File Offset: 0x0001D422
		public bool? AllowiCloudDocSync { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001F22B File Offset: 0x0001D42B
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0001F233 File Offset: 0x0001D433
		public bool? AllowiCloudPhotoSync { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001F23C File Offset: 0x0001D43C
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0001F244 File Offset: 0x0001D444
		public bool? AllowiCloudBackup { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001F24D File Offset: 0x0001D44D
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0001F255 File Offset: 0x0001D455
		public RatingRegionEntry? RegionRatings { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0001F25E File Offset: 0x0001D45E
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0001F266 File Offset: 0x0001D466
		public RatingMovieEntry? MoviesRating { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001F26F File Offset: 0x0001D46F
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x0001F277 File Offset: 0x0001D477
		public RatingTvShowEntry? TVShowsRating { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001F280 File Offset: 0x0001D480
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x0001F288 File Offset: 0x0001D488
		public RatingAppsEntry? AppsRating { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001F291 File Offset: 0x0001D491
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0001F299 File Offset: 0x0001D499
		public bool? AllowVoiceDialing { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001F2A2 File Offset: 0x0001D4A2
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0001F2AA File Offset: 0x0001D4AA
		public bool? AllowVoiceAssistant { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001F2B3 File Offset: 0x0001D4B3
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0001F2BB File Offset: 0x0001D4BB
		public bool? AllowAssistantWhileLocked { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0001F2C4 File Offset: 0x0001D4C4
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0001F2CC File Offset: 0x0001D4CC
		public bool? AllowScreenshot { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0001F2D5 File Offset: 0x0001D4D5
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0001F2DD File Offset: 0x0001D4DD
		public bool? AllowVideoConferencing { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001F2E6 File Offset: 0x0001D4E6
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0001F2EE File Offset: 0x0001D4EE
		public bool? AllowPassbookWhileLocked { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001F2F7 File Offset: 0x0001D4F7
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0001F2FF File Offset: 0x0001D4FF
		public bool? AllowDiagnosticSubmission { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0001F308 File Offset: 0x0001D508
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0001F310 File Offset: 0x0001D510
		public bool? AllowConvenienceLogon { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001F319 File Offset: 0x0001D519
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001F321 File Offset: 0x0001D521
		public TimeSpan? MaxPasswordGracePeriod { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0001F32A File Offset: 0x0001D52A
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0001F332 File Offset: 0x0001D532
		public int? PasswordQuality { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001F33B File Offset: 0x0001D53B
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0001F343 File Offset: 0x0001D543
		public bool? AllowAppStore { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0001F34C File Offset: 0x0001D54C
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0001F354 File Offset: 0x0001D554
		public bool? ForceAppStorePassword { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001F35D File Offset: 0x0001D55D
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0001F365 File Offset: 0x0001D565
		public bool? SystemSecurityTLS { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001F36E File Offset: 0x0001D56E
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001F376 File Offset: 0x0001D576
		public UserAccountControlStatusEntry? UserAccountControlStatus { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001F37F File Offset: 0x0001D57F
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001F387 File Offset: 0x0001D587
		public FirewallStatusEntry? FirewallStatus { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001F390 File Offset: 0x0001D590
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0001F398 File Offset: 0x0001D598
		public AutoUpdateStatusEntry? AutoUpdateStatus { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001F3A1 File Offset: 0x0001D5A1
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001F3A9 File Offset: 0x0001D5A9
		public string AntiVirusStatus { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0001F3B2 File Offset: 0x0001D5B2
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0001F3BA File Offset: 0x0001D5BA
		public bool? AntiVirusSignatureStatus { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001F3C3 File Offset: 0x0001D5C3
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0001F3CB File Offset: 0x0001D5CB
		public bool? SmartScreenEnabled { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001F3D4 File Offset: 0x0001D5D4
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0001F3DC File Offset: 0x0001D5DC
		public string WorkFoldersSyncUrl { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001F3E5 File Offset: 0x0001D5E5
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0001F3ED File Offset: 0x0001D5ED
		public string PasswordComplexity { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001F3F6 File Offset: 0x0001D5F6
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001F3FE File Offset: 0x0001D5FE
		public bool? WLANEnabled { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001F407 File Offset: 0x0001D607
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x0001F40F File Offset: 0x0001D60F
		public string AccountName { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001F418 File Offset: 0x0001D618
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0001F420 File Offset: 0x0001D620
		public string AccountUserName { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001F429 File Offset: 0x0001D629
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0001F431 File Offset: 0x0001D631
		public string ExchangeActiveSyncHost { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001F43A File Offset: 0x0001D63A
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0001F442 File Offset: 0x0001D642
		public string EmailAddress { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001F44B File Offset: 0x0001D64B
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0001F453 File Offset: 0x0001D653
		public bool? UseSSL { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001F45C File Offset: 0x0001D65C
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0001F464 File Offset: 0x0001D664
		public bool? AllowMove { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001F46D File Offset: 0x0001D66D
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0001F475 File Offset: 0x0001D675
		public bool? AllowRecentAddressSyncing { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0001F47E File Offset: 0x0001D67E
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0001F486 File Offset: 0x0001D686
		public long? DaysToSync { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001F48F File Offset: 0x0001D68F
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0001F497 File Offset: 0x0001D697
		public long? ContentType { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		public bool? UseSMIME { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001F4B1 File Offset: 0x0001D6B1
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0001F4B9 File Offset: 0x0001D6B9
		public long? SyncSchedule { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001F4C2 File Offset: 0x0001D6C2
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0001F4CA File Offset: 0x0001D6CA
		public bool? UseOnlyInEmail { get; set; }

		// Token: 0x060007FE RID: 2046 RVA: 0x0001F4D4 File Offset: 0x0001D6D4
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
			if (this.PasswordRequired != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_Required, typeof(string).ToString()), new List<string>
				{
					this.PasswordRequired.ToString()
				}));
			}
			if (this.PhoneMemoryEncrypted != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Encryption_PhoneMemoryEncrypted, typeof(string).ToString()), new List<string>
				{
					this.PhoneMemoryEncrypted.ToString()
				}));
			}
			if (this.PasswordTimeout != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_Timeout, typeof(string).ToString()), new List<string>
				{
					this.PasswordTimeout.GetValueOrDefault().Subtract(TimeSpan.FromSeconds((double)this.PasswordTimeout.GetValueOrDefault().Seconds)).TotalMinutes.ToString()
				}));
			}
			if (this.PasswordMinimumLength != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_MinimumLength, typeof(string).ToString()), new List<string>
				{
					this.PasswordMinimumLength.ToString()
				}));
			}
			if (this.PasswordHistoryCount != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_History, typeof(string).ToString()), new List<string>
				{
					this.PasswordHistoryCount.ToString()
				}));
			}
			if (this.PasswordExpirationDays != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_Expiration, typeof(string).ToString()), new List<string>
				{
					this.PasswordExpirationDays.ToString()
				}));
			}
			if (this.MaxPasswordAttemptsBeforeWipe != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_MaxAttemptsBeforeWipe, typeof(string).ToString()), new List<string>
				{
					this.MaxPasswordAttemptsBeforeWipe.ToString()
				}));
			}
			if (this.PasswordMinComplexChars != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_MinComplexChars, typeof(string).ToString()), new List<string>
				{
					this.PasswordMinComplexChars.ToString()
				}));
			}
			if (this.AllowSimplePassword != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_AllowSimplePassword, typeof(string).ToString()), new List<string>
				{
					this.AllowSimplePassword.ToString()
				}));
			}
			if (this.EnableRemovableStorage != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Security_EnableRemovableStorage, typeof(string).ToString()), new List<string>
				{
					this.EnableRemovableStorage.ToString()
				}));
			}
			if (this.CameraEnabled != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Security_CameraEnabled, typeof(string).ToString()), new List<string>
				{
					this.CameraEnabled.ToString()
				}));
			}
			if (this.BluetoothEnabled != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Security_BluetoothEnabled, typeof(string).ToString()), new List<string>
				{
					this.BluetoothEnabled.ToString()
				}));
			}
			if (this.ForceEncryptedBackup != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Cloud_ForceEncryptedBackup, typeof(string).ToString()), new List<string>
				{
					this.ForceEncryptedBackup.ToString()
				}));
			}
			if (this.AllowiCloudDocSync != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Cloud_AllowiCloudDocSync, typeof(string).ToString()), new List<string>
				{
					this.AllowiCloudDocSync.ToString()
				}));
			}
			if (this.AllowiCloudPhotoSync != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Cloud_AllowiCloudPhotoSync, typeof(string).ToString()), new List<string>
				{
					this.AllowiCloudPhotoSync.ToString()
				}));
			}
			if (this.AllowiCloudBackup != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Cloud_AllowiCloudBackup, typeof(string).ToString()), new List<string>
				{
					this.AllowiCloudBackup.ToString()
				}));
			}
			if (this.RegionRatings != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_RatingsRegion, typeof(string).ToString()), new List<string>
				{
					this.RegionRatings.ToString()
				}));
			}
			if (this.MoviesRating != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_RatingMovies, typeof(string).ToString()), new List<string>
				{
					((int)this.MoviesRating.Value).ToString()
				}));
			}
			if (this.TVShowsRating != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_RatingTVShows, typeof(string).ToString()), new List<string>
				{
					((int)this.TVShowsRating.Value).ToString()
				}));
			}
			if (this.AppsRating != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_RatingApps, typeof(string).ToString()), new List<string>
				{
					((int)this.AppsRating.Value).ToString()
				}));
			}
			if (this.AllowVoiceDialing != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_AllowVoiceDialing, typeof(string).ToString()), new List<string>
				{
					this.AllowVoiceDialing.ToString()
				}));
			}
			if (this.AllowVoiceAssistant != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_AllowVoiceAssistant, typeof(string).ToString()), new List<string>
				{
					this.AllowVoiceAssistant.ToString()
				}));
			}
			if (this.AllowAssistantWhileLocked != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_AllowAssistantWhileLocked, typeof(string).ToString()), new List<string>
				{
					this.AllowAssistantWhileLocked.ToString()
				}));
			}
			if (this.AllowScreenshot != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_AllowScreenshot, typeof(string).ToString()), new List<string>
				{
					this.AllowScreenshot.ToString()
				}));
			}
			if (this.AllowVideoConferencing != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_AllowVideoConferencing, typeof(string).ToString()), new List<string>
				{
					this.AllowVideoConferencing.ToString()
				}));
			}
			if (this.AllowPassbookWhileLocked != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_AllowPassbookWhileLocked, typeof(string).ToString()), new List<string>
				{
					this.AllowPassbookWhileLocked.ToString()
				}));
			}
			if (this.AllowDiagnosticSubmission != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_AllowDiagnosticSubmission, typeof(string).ToString()), new List<string>
				{
					this.AllowDiagnosticSubmission.ToString()
				}));
			}
			if (this.AllowConvenienceLogon != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_AllowConvenienceLogon, typeof(string).ToString()), new List<string>
				{
					this.AllowConvenienceLogon.ToString()
				}));
			}
			if (this.MaxPasswordGracePeriod != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_MaxGracePeriod, typeof(string).ToString()), new List<string>
				{
					this.MaxPasswordGracePeriod.GetValueOrDefault().Subtract(TimeSpan.FromSeconds((double)this.MaxPasswordGracePeriod.GetValueOrDefault().Seconds)).TotalMinutes.ToString()
				}));
			}
			if (this.PasswordQuality != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_PasswordQuality, typeof(string).ToString()), new List<string>
				{
					this.PasswordQuality.ToString()
				}));
			}
			if (this.AllowAppStore != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_AllowAppStore, typeof(string).ToString()), new List<string>
				{
					this.AllowAppStore.ToString()
				}));
			}
			if (this.ForceAppStorePassword != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Restrictions_ForceAppStorePassword, typeof(string).ToString()), new List<string>
				{
					this.ForceAppStorePassword.ToString()
				}));
			}
			if (this.SystemSecurityTLS != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_SystemSecurity_TLS, typeof(string).ToString()), new List<string>
				{
					this.SystemSecurityTLS.ToString()
				}));
			}
			if (this.UserAccountControlStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_SystemSecurity_UserAccountControlStatus, typeof(string).ToString()), new List<string>
				{
					((int)this.UserAccountControlStatus.Value).ToString()
				}));
			}
			if (this.FirewallStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_SystemSecurity_FirewallStatus, typeof(string).ToString()), new List<string>
				{
					((int)this.FirewallStatus.Value).ToString()
				}));
			}
			if (this.AutoUpdateStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_SystemSecurity_AutoUpdateStatus, typeof(string).ToString()), new List<string>
				{
					((int)this.AutoUpdateStatus.Value).ToString()
				}));
			}
			if (this.AntiVirusStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_SystemSecurity_AntiVirusStatus, typeof(string).ToString()), new List<string>
				{
					this.AntiVirusStatus.ToString()
				}));
			}
			if (this.AntiVirusSignatureStatus != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_SystemSecurity_AntiVirusSignatureStatus, typeof(string).ToString()), new List<string>
				{
					this.AntiVirusSignatureStatus.ToString()
				}));
			}
			if (this.SmartScreenEnabled != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_InternetExplorer_SmartScreenEnabled, typeof(string).ToString()), new List<string>
				{
					this.SmartScreenEnabled.ToString()
				}));
			}
			if (this.WorkFoldersSyncUrl != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_WorkFolders_SyncUrl, typeof(string).ToString()), new List<string>
				{
					this.WorkFoldersSyncUrl.ToString()
				}));
			}
			if (this.PasswordComplexity != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Password_Type, typeof(string).ToString()), new List<string>
				{
					this.PasswordComplexity.ToString()
				}));
			}
			if (this.WLANEnabled != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Device_Wireless_WLANEnabled, typeof(string).ToString()), new List<string>
				{
					this.WLANEnabled.ToString()
				}));
			}
			if (this.AccountName != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_AccountName, typeof(string).ToString()), new List<string>
				{
					this.AccountName.ToString()
				}));
			}
			if (this.AccountUserName != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_UserName, typeof(string).ToString()), new List<string>
				{
					this.AccountUserName.ToString()
				}));
			}
			if (this.ExchangeActiveSyncHost != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_Host, typeof(string).ToString()), new List<string>
				{
					this.ExchangeActiveSyncHost.ToString()
				}));
			}
			if (this.EmailAddress != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_EmailAddress, typeof(string).ToString()), new List<string>
				{
					this.EmailAddress.ToString()
				}));
			}
			if (this.UseSSL != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_UseSSL, typeof(string).ToString()), new List<string>
				{
					this.UseSSL.ToString()
				}));
			}
			if (this.AllowMove != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_PreventAppSheet, typeof(string).ToString()), new List<string>
				{
					this.AllowMove.ToString()
				}));
			}
			if (this.AllowRecentAddressSyncing != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_DisableMailRecentsSyncing, typeof(string).ToString()), new List<string>
				{
					this.AllowRecentAddressSyncing.ToString()
				}));
			}
			if (this.DaysToSync != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_MailNumberOfPastDaysToSync, typeof(string).ToString()), new List<string>
				{
					this.DaysToSync.ToString()
				}));
			}
			if (this.ContentType != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_ContentTypeToSync, typeof(string).ToString()), new List<string>
				{
					this.ContentType.ToString()
				}));
			}
			if (this.UseSMIME != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_UseSMIME, typeof(string).ToString()), new List<string>
				{
					this.UseSMIME.ToString()
				}));
			}
			if (this.SyncSchedule != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_SyncSchedule, typeof(string).ToString()), new List<string>
				{
					this.SyncSchedule.ToString()
				}));
			}
			if (this.UseOnlyInEmail != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceConfigurationRule.Eas_PreventMove, typeof(string).ToString()), new List<string>
				{
					this.UseOnlyInEmail.ToString()
				}));
			}
			return list;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002085C File Offset: 0x0001EA5C
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
							if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_Required))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.PasswordRequired = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Encryption_PhoneMemoryEncrypted))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.PhoneMemoryEncrypted = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_Timeout))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordTimeout = new TimeSpan?(TimeSpan.FromMinutes((double)num));
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_MinimumLength))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordMinimumLength = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_History))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordHistoryCount = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_Expiration))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordExpirationDays = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_MaxAttemptsBeforeWipe))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.MaxPasswordAttemptsBeforeWipe = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_MinComplexChars))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordMinComplexChars = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_AllowSimplePassword))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowSimplePassword = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Security_EnableRemovableStorage))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.EnableRemovableStorage = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Security_CameraEnabled))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.CameraEnabled = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Security_BluetoothEnabled))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.BluetoothEnabled = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Cloud_ForceEncryptedBackup))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.ForceEncryptedBackup = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Cloud_AllowiCloudDocSync))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowiCloudDocSync = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Cloud_AllowiCloudPhotoSync))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowiCloudPhotoSync = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Cloud_AllowiCloudBackup))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowiCloudBackup = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_RatingsRegion))
							{
								RatingRegionEntry value2;
								if (Enum.TryParse<RatingRegionEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value2))
								{
									this.RegionRatings = new RatingRegionEntry?(value2);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_RatingMovies))
							{
								RatingMovieEntry value3;
								if (Enum.TryParse<RatingMovieEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value3))
								{
									this.MoviesRating = new RatingMovieEntry?(value3);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_RatingTVShows))
							{
								RatingTvShowEntry value4;
								if (Enum.TryParse<RatingTvShowEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value4))
								{
									this.TVShowsRating = new RatingTvShowEntry?(value4);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_RatingApps))
							{
								RatingAppsEntry value5;
								if (Enum.TryParse<RatingAppsEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value5))
								{
									this.AppsRating = new RatingAppsEntry?(value5);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_AllowVoiceDialing))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowVoiceDialing = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_AllowVoiceAssistant))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowVoiceAssistant = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_AllowAssistantWhileLocked))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowAssistantWhileLocked = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_AllowScreenshot))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowScreenshot = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_AllowVideoConferencing))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowVideoConferencing = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_AllowPassbookWhileLocked))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowPassbookWhileLocked = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_AllowDiagnosticSubmission))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowDiagnosticSubmission = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_AllowConvenienceLogon))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowConvenienceLogon = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_MaxGracePeriod))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.MaxPasswordGracePeriod = new TimeSpan?(TimeSpan.FromMinutes((double)num));
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_PasswordQuality))
							{
								int num;
								if (int.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out num))
								{
									this.PasswordQuality = new int?(num);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_AllowAppStore))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowAppStore = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Restrictions_ForceAppStorePassword))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.ForceAppStorePassword = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_SystemSecurity_TLS))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.SystemSecurityTLS = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_SystemSecurity_UserAccountControlStatus))
							{
								UserAccountControlStatusEntry value6;
								if (Enum.TryParse<UserAccountControlStatusEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value6))
								{
									this.UserAccountControlStatus = new UserAccountControlStatusEntry?(value6);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_SystemSecurity_FirewallStatus))
							{
								FirewallStatusEntry value7;
								if (Enum.TryParse<FirewallStatusEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value7))
								{
									this.FirewallStatus = new FirewallStatusEntry?(value7);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_SystemSecurity_AutoUpdateStatus))
							{
								AutoUpdateStatusEntry value8;
								if (Enum.TryParse<AutoUpdateStatusEntry>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value8))
								{
									this.AutoUpdateStatus = new AutoUpdateStatusEntry?(value8);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_SystemSecurity_AntiVirusStatus))
							{
								this.AntiVirusStatus = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_SystemSecurity_AntiVirusSignatureStatus))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AntiVirusSignatureStatus = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_InternetExplorer_SmartScreenEnabled))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.SmartScreenEnabled = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_WorkFolders_SyncUrl))
							{
								this.WorkFoldersSyncUrl = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Password_Type))
							{
								this.PasswordComplexity = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Device_Wireless_WLANEnabled))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.WLANEnabled = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_AccountName))
							{
								this.AccountName = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_UserName))
							{
								this.AccountUserName = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_Host))
							{
								this.ExchangeActiveSyncHost = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_EmailAddress))
							{
								this.EmailAddress = nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>();
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_UseSSL))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.UseSSL = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_PreventAppSheet))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowMove = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_DisableMailRecentsSyncing))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.AllowRecentAddressSyncing = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_MailNumberOfPastDaysToSync))
							{
								long value9;
								if (long.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value9))
								{
									this.DaysToSync = new long?(value9);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_ContentTypeToSync))
							{
								long value9;
								if (long.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value9))
								{
									this.ContentType = new long?(value9);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_UseSMIME))
							{
								if (bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.UseSMIME = new bool?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_SyncSchedule))
							{
								long value9;
								if (long.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value9))
								{
									this.SyncSchedule = new long?(value9);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceConfigurationRule.Eas_PreventMove) && bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
							{
								this.UseOnlyInEmail = new bool?(value);
							}
						}
					}
				}
			}
		}

		// Token: 0x040002C0 RID: 704
		public static string Device_Password_Required = "Device_Password_Required";

		// Token: 0x040002C1 RID: 705
		public static string Device_Encryption_PhoneMemoryEncrypted = "Device_Encryption_PhoneMemoryEncrypted";

		// Token: 0x040002C2 RID: 706
		public static string Device_Password_Timeout = "Device_Password_Timeout";

		// Token: 0x040002C3 RID: 707
		public static string Device_Password_MinimumLength = "Device_Password_MinimumLength";

		// Token: 0x040002C4 RID: 708
		public static string Device_Password_History = "Device_Password_History";

		// Token: 0x040002C5 RID: 709
		public static string Device_Password_Expiration = "Device_Password_Expiration";

		// Token: 0x040002C6 RID: 710
		public static string Device_Password_MaxAttemptsBeforeWipe = "Device_Password_MaxAttemptsBeforeWipe";

		// Token: 0x040002C7 RID: 711
		public static string Device_Password_MinComplexChars = "Device_Password_MinComplexChars";

		// Token: 0x040002C8 RID: 712
		public static string Device_Password_AllowSimplePassword = "Device_Password_AllowSimplePassword";

		// Token: 0x040002C9 RID: 713
		public static string Device_Security_EnableRemovableStorage = "Device_Security_EnableRemovableStorage";

		// Token: 0x040002CA RID: 714
		public static string Device_Security_CameraEnabled = "Device_Security_CameraEnabled";

		// Token: 0x040002CB RID: 715
		public static string Device_Security_BluetoothEnabled = "Device_Security_BluetoothEnabled";

		// Token: 0x040002CC RID: 716
		public static string Device_Cloud_ForceEncryptedBackup = "Device_Cloud_ForceEncryptedBackup";

		// Token: 0x040002CD RID: 717
		public static string Device_Cloud_AllowiCloudDocSync = "Device_Cloud_AllowiCloudDocSync";

		// Token: 0x040002CE RID: 718
		public static string Device_Cloud_AllowiCloudPhotoSync = "Device_Cloud_AllowiCloudPhotoSync";

		// Token: 0x040002CF RID: 719
		public static string Device_Cloud_AllowiCloudBackup = "Device_Cloud_AllowiCloudBackup";

		// Token: 0x040002D0 RID: 720
		public static string Device_Restrictions_RatingsRegion = "Device_Restrictions_RatingsRegion";

		// Token: 0x040002D1 RID: 721
		public static string Device_Restrictions_RatingMovies = "Device_Restrictions_RatingMovies";

		// Token: 0x040002D2 RID: 722
		public static string Device_Restrictions_RatingTVShows = "Device_Restrictions_RatingTVShows";

		// Token: 0x040002D3 RID: 723
		public static string Device_Restrictions_RatingApps = "Device_Restrictions_RatingApps";

		// Token: 0x040002D4 RID: 724
		public static string Device_Restrictions_AllowVoiceDialing = "Device_Restrictions_AllowVoiceDialing";

		// Token: 0x040002D5 RID: 725
		public static string Device_Restrictions_AllowVoiceAssistant = "Device_Restrictions_AllowVoiceAssistant";

		// Token: 0x040002D6 RID: 726
		public static string Device_Restrictions_AllowAssistantWhileLocked = "Device_Restrictions_AllowAssistantWhileLocked";

		// Token: 0x040002D7 RID: 727
		public static string Device_Restrictions_AllowScreenshot = "Device_Restrictions_AllowScreenshot";

		// Token: 0x040002D8 RID: 728
		public static string Device_Restrictions_AllowVideoConferencing = "Device_Restrictions_AllowVideoConferencing";

		// Token: 0x040002D9 RID: 729
		public static string Device_Restrictions_AllowPassbookWhileLocked = "Device_Restrictions_AllowPassbookWhileLocked";

		// Token: 0x040002DA RID: 730
		public static string Device_Restrictions_AllowDiagnosticSubmission = "Device_Restrictions_AllowDiagnosticSubmission";

		// Token: 0x040002DB RID: 731
		public static string Device_Password_AllowConvenienceLogon = "Device_Password_AllowConvenienceLogon";

		// Token: 0x040002DC RID: 732
		public static string Device_Password_MaxGracePeriod = "Device_Password_MaxGracePeriod";

		// Token: 0x040002DD RID: 733
		public static string Device_Password_PasswordQuality = "Device_Password_PasswordQuality";

		// Token: 0x040002DE RID: 734
		public static string Device_Restrictions_AllowAppStore = "Device_Restrictions_AllowAppStore";

		// Token: 0x040002DF RID: 735
		public static string Device_Restrictions_ForceAppStorePassword = "Device_Restrictions_ForceAppStorePassword";

		// Token: 0x040002E0 RID: 736
		public static string Device_SystemSecurity_TLS = "Device_SystemSecurity_TLS";

		// Token: 0x040002E1 RID: 737
		public static string Device_SystemSecurity_UserAccountControlStatus = "Device_SystemSecurity_UserAccountControlStatus";

		// Token: 0x040002E2 RID: 738
		public static string Device_SystemSecurity_FirewallStatus = "Device_SystemSecurity_FirewallStatus";

		// Token: 0x040002E3 RID: 739
		public static string Device_SystemSecurity_AutoUpdateStatus = "Device_SystemSecurity_AutoUpdateStatus";

		// Token: 0x040002E4 RID: 740
		public static string Device_SystemSecurity_AntiVirusStatus = "Device_SystemSecurity_AntiVirusStatus";

		// Token: 0x040002E5 RID: 741
		public static string Device_SystemSecurity_AntiVirusSignatureStatus = "Device_SystemSecurity_AntiVirusSignatureStatus";

		// Token: 0x040002E6 RID: 742
		public static string Device_InternetExplorer_SmartScreenEnabled = "Device_InternetExplorer_SmartScreenEnabled";

		// Token: 0x040002E7 RID: 743
		public static string Device_WorkFolders_SyncUrl = "Device_WorkFolders_SyncUrl";

		// Token: 0x040002E8 RID: 744
		public static string Device_Password_Type = "Device_Password_Type";

		// Token: 0x040002E9 RID: 745
		public static string Device_Wireless_WLANEnabled = "Device_Wireless_WLANEnabled";

		// Token: 0x040002EA RID: 746
		public static string Eas_AccountName = "Eas_AccountName";

		// Token: 0x040002EB RID: 747
		public static string Eas_UserName = "Eas_UserName";

		// Token: 0x040002EC RID: 748
		public static string Eas_Host = "Eas_Host";

		// Token: 0x040002ED RID: 749
		public static string Eas_EmailAddress = "Eas_EmailAddress";

		// Token: 0x040002EE RID: 750
		public static string Eas_UseSSL = "Eas_UseSSL";

		// Token: 0x040002EF RID: 751
		public static string Eas_PreventAppSheet = "Eas_PreventAppSheet";

		// Token: 0x040002F0 RID: 752
		public static string Eas_DisableMailRecentsSyncing = "Eas_DisableMailRecentsSyncing";

		// Token: 0x040002F1 RID: 753
		public static string Eas_MailNumberOfPastDaysToSync = "Eas_MailNumberOfPastDaysToSync";

		// Token: 0x040002F2 RID: 754
		public static string Eas_ContentTypeToSync = "Eas_ContentTypeToSync";

		// Token: 0x040002F3 RID: 755
		public static string Eas_UseSMIME = "Eas_UseSMIME";

		// Token: 0x040002F4 RID: 756
		public static string Eas_SyncSchedule = "Eas_SyncSchedule";

		// Token: 0x040002F5 RID: 757
		public static string Eas_PreventMove = "Eas_PreventMove";
	}
}
