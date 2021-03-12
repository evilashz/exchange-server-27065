using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A3C RID: 2620
	internal enum Counter
	{
		// Token: 0x04004CEF RID: 19695
		CacheObj,
		// Token: 0x04004CF0 RID: 19696
		CacheHitsRate = 2,
		// Token: 0x04004CF1 RID: 19697
		CacheMissesRate = 4,
		// Token: 0x04004CF2 RID: 19698
		CacheExpiriesUserRate = 6,
		// Token: 0x04004CF3 RID: 19699
		CacheExpiriesConfigRate = 8,
		// Token: 0x04004CF4 RID: 19700
		CacheInsertsUserRate = 10,
		// Token: 0x04004CF5 RID: 19701
		CacheInsertsConfigRate = 12,
		// Token: 0x04004CF6 RID: 19702
		CacheLdapSearchesRate = 14,
		// Token: 0x04004CF7 RID: 19703
		CacheAsyncNotifies = 16,
		// Token: 0x04004CF8 RID: 19704
		CacheAsyncReads = 18,
		// Token: 0x04004CF9 RID: 19705
		CacheAsyncSearches = 20,
		// Token: 0x04004CFA RID: 19706
		CacheTotalUserEntries = 22,
		// Token: 0x04004CFB RID: 19707
		CacheTotalConfigEntries = 24,
		// Token: 0x04004CFC RID: 19708
		CacheUserDnEntries = 26,
		// Token: 0x04004CFD RID: 19709
		CacheConfigDnEntries = 28,
		// Token: 0x04004CFE RID: 19710
		CacheUserSrchEntries = 30,
		// Token: 0x04004CFF RID: 19711
		CacheConfigSrchEntries = 32,
		// Token: 0x04004D00 RID: 19712
		CacheUserNfdnEntries = 34,
		// Token: 0x04004D01 RID: 19713
		CacheConfigNfdnEntries = 36,
		// Token: 0x04004D02 RID: 19714
		CacheUserNfguidEntries = 38,
		// Token: 0x04004D03 RID: 19715
		CacheConfigNfguidEntries = 40,
		// Token: 0x04004D04 RID: 19716
		CacheSizeTotalUserEntries = 42,
		// Token: 0x04004D05 RID: 19717
		CacheSizeTotalConfigEntries = 44,
		// Token: 0x04004D06 RID: 19718
		CacheSizeUserDnEntries = 46,
		// Token: 0x04004D07 RID: 19719
		CacheSizeConfigDnEntries = 48,
		// Token: 0x04004D08 RID: 19720
		CacheSizeUserSrchEntries = 50,
		// Token: 0x04004D09 RID: 19721
		CacheSizeConfigSrchEntries = 52,
		// Token: 0x04004D0A RID: 19722
		CacheSizeUserNfdnEntries = 54,
		// Token: 0x04004D0B RID: 19723
		CacheSizeConfigNfdnEntries = 56,
		// Token: 0x04004D0C RID: 19724
		CacheSizeUserNfguidEntries = 58,
		// Token: 0x04004D0D RID: 19725
		CacheSizeConfigNfguidEntries = 60,
		// Token: 0x04004D0E RID: 19726
		CacheHitsTotal = 62,
		// Token: 0x04004D0F RID: 19727
		CacheMissesTotal = 64,
		// Token: 0x04004D10 RID: 19728
		CacheExpiriesUserTotal = 66,
		// Token: 0x04004D11 RID: 19729
		CacheExpiriesConfigTotal = 68,
		// Token: 0x04004D12 RID: 19730
		CacheInsertsUserTotal = 70,
		// Token: 0x04004D13 RID: 19731
		CacheInsertsConfigTotal = 72,
		// Token: 0x04004D14 RID: 19732
		CacheLdapSearchesTotal = 74,
		// Token: 0x04004D15 RID: 19733
		ProcessObj = 76,
		// Token: 0x04004D16 RID: 19734
		ProcessProcessid = 78,
		// Token: 0x04004D17 RID: 19735
		ProcessRateRead = 80,
		// Token: 0x04004D18 RID: 19736
		ProcessRateSearch = 82,
		// Token: 0x04004D19 RID: 19737
		ProcessRateWrite = 84,
		// Token: 0x04004D1A RID: 19738
		ProcessRatePaged = 86,
		// Token: 0x04004D1B RID: 19739
		ProcessRateVlv = 88,
		// Token: 0x04004D1C RID: 19740
		ProcessRateNotFoundConfigReads = 90,
		// Token: 0x04004D1D RID: 19741
		ProcessRateLongRunningOperations = 92,
		// Token: 0x04004D1E RID: 19742
		ProcessRateTimeouts = 94,
		// Token: 0x04004D1F RID: 19743
		ProcessRateNotificationsReceived = 96,
		// Token: 0x04004D20 RID: 19744
		ProcessRateNotificationsReported = 98,
		// Token: 0x04004D21 RID: 19745
		ProcessRateCriticalValidationFailures = 100,
		// Token: 0x04004D22 RID: 19746
		ProcessRateNonCriticalValidationFailures = 102,
		// Token: 0x04004D23 RID: 19747
		ProcessRateIgnoredValidationFailures = 104,
		// Token: 0x04004D24 RID: 19748
		ProcessOpenConnectionsDC = 106,
		// Token: 0x04004D25 RID: 19749
		ProcessOpenConnectionsGC = 108,
		// Token: 0x04004D26 RID: 19750
		ProcessOutstandingRequests = 110,
		// Token: 0x04004D27 RID: 19751
		ProcessTopologyVersion = 112,
		// Token: 0x04004D28 RID: 19752
		ProcessTimeRead = 114,
		// Token: 0x04004D29 RID: 19753
		ProcessTimeReadBase = 116,
		// Token: 0x04004D2A RID: 19754
		ProcessTimeSearch = 118,
		// Token: 0x04004D2B RID: 19755
		ProcessTimeSearchBase = 120,
		// Token: 0x04004D2C RID: 19756
		ProcessTimeSearchNinetiethPercentile = 122,
		// Token: 0x04004D2D RID: 19757
		ProcessTimeSearchNinetiethPercentileBase = 124,
		// Token: 0x04004D2E RID: 19758
		ProcessTimeSearchNinetyFifthPercentile = 126,
		// Token: 0x04004D2F RID: 19759
		ProcessTimeSearchNinetyFifthPercentileBase = 128,
		// Token: 0x04004D30 RID: 19760
		ProcessTimeSearchNinetyNinethPercentile = 130,
		// Token: 0x04004D31 RID: 19761
		ProcessTimeSearchNinetyNinethPercentileBase = 132,
		// Token: 0x04004D32 RID: 19762
		ProcessTimeSearchOnDC = 134,
		// Token: 0x04004D33 RID: 19763
		ProcessTimeSearchOnDCBase = 136,
		// Token: 0x04004D34 RID: 19764
		ProcessCostSearch = 138,
		// Token: 0x04004D35 RID: 19765
		ProcessCostSearchBase = 140,
		// Token: 0x04004D36 RID: 19766
		ProcessTimeWrite = 142,
		// Token: 0x04004D37 RID: 19767
		ProcessTimeWriteBase = 144,
		// Token: 0x04004D38 RID: 19768
		DCObj = 146,
		// Token: 0x04004D39 RID: 19769
		DCRateRead = 148,
		// Token: 0x04004D3A RID: 19770
		DCRateSearch = 150,
		// Token: 0x04004D3B RID: 19771
		DCRateTimeouts = 152,
		// Token: 0x04004D3C RID: 19772
		DCRateTimelimitExceeded = 154,
		// Token: 0x04004D3D RID: 19773
		DCRateFatalErrors = 156,
		// Token: 0x04004D3E RID: 19774
		DCRateDisconnects = 158,
		// Token: 0x04004D3F RID: 19775
		DCRateSearchFailures = 160,
		// Token: 0x04004D40 RID: 19776
		DCRateModificationError = 162,
		// Token: 0x04004D41 RID: 19777
		DCRateBindFailures = 164,
		// Token: 0x04004D42 RID: 19778
		DCRateLongRunningOperations = 166,
		// Token: 0x04004D43 RID: 19779
		DCRatePaged = 168,
		// Token: 0x04004D44 RID: 19780
		DCRateVlv = 170,
		// Token: 0x04004D45 RID: 19781
		DCOutstandingRequests = 172,
		// Token: 0x04004D46 RID: 19782
		DCTimeNetlogon = 174,
		// Token: 0x04004D47 RID: 19783
		DCTimeGethostbyname = 176,
		// Token: 0x04004D48 RID: 19784
		DCAvgTimeKerberos = 178,
		// Token: 0x04004D49 RID: 19785
		DCAvgTimeConnection = 180,
		// Token: 0x04004D4A RID: 19786
		DCLocalSite = 182,
		// Token: 0x04004D4B RID: 19787
		DCStateReachability = 184,
		// Token: 0x04004D4C RID: 19788
		DCStateSynchronized = 186,
		// Token: 0x04004D4D RID: 19789
		DCStateGCCapable = 188,
		// Token: 0x04004D4E RID: 19790
		DCStateIsPdc = 190,
		// Token: 0x04004D4F RID: 19791
		DCStateSaclRight = 192,
		// Token: 0x04004D50 RID: 19792
		DCStateCriticalData = 194,
		// Token: 0x04004D51 RID: 19793
		DCStateNetlogon = 196,
		// Token: 0x04004D52 RID: 19794
		DCStateOsversion = 198,
		// Token: 0x04004D53 RID: 19795
		DCTimeRead = 200,
		// Token: 0x04004D54 RID: 19796
		DCTimeReadBase = 202,
		// Token: 0x04004D55 RID: 19797
		DCTimeSearch = 204,
		// Token: 0x04004D56 RID: 19798
		DCTimeSearchBase = 206,
		// Token: 0x04004D57 RID: 19799
		LocalDCObj = 208,
		// Token: 0x04004D58 RID: 19800
		LocalDCRateRead = 210,
		// Token: 0x04004D59 RID: 19801
		LocalDCRateSearch = 212,
		// Token: 0x04004D5A RID: 19802
		LocalDCRateTimeouts = 214,
		// Token: 0x04004D5B RID: 19803
		LocalDCRateTimelimitExceeded = 216,
		// Token: 0x04004D5C RID: 19804
		LocalDCRateFatalErrors = 218,
		// Token: 0x04004D5D RID: 19805
		LocalDCRateDisconnects = 220,
		// Token: 0x04004D5E RID: 19806
		LocalDCRateSearchFailures = 222,
		// Token: 0x04004D5F RID: 19807
		LocalDCRateModificationError = 224,
		// Token: 0x04004D60 RID: 19808
		LocalDCRateBindFailures = 226,
		// Token: 0x04004D61 RID: 19809
		LocalDCRateLongRunningOperations = 228,
		// Token: 0x04004D62 RID: 19810
		LocalDCRatePaged = 230,
		// Token: 0x04004D63 RID: 19811
		LocalDCRateVlv = 232,
		// Token: 0x04004D64 RID: 19812
		LocalDCOutstandingRequests = 234,
		// Token: 0x04004D65 RID: 19813
		LocalDCTimeNetlogon = 236,
		// Token: 0x04004D66 RID: 19814
		LocalDCTimeGethostbyname = 238,
		// Token: 0x04004D67 RID: 19815
		LocalDCAvgTimeKerberos = 240,
		// Token: 0x04004D68 RID: 19816
		LocalDCAvgTimeConnection = 242,
		// Token: 0x04004D69 RID: 19817
		LocalDCStateReachability = 244,
		// Token: 0x04004D6A RID: 19818
		LocalDCStateSynchronized = 246,
		// Token: 0x04004D6B RID: 19819
		LocalDCStateGCCapable = 248,
		// Token: 0x04004D6C RID: 19820
		LocalDCStateIsPdc = 250,
		// Token: 0x04004D6D RID: 19821
		LocalDCStateSaclRight = 252,
		// Token: 0x04004D6E RID: 19822
		LocalDCStateCriticalData = 254,
		// Token: 0x04004D6F RID: 19823
		LocalDCStateNetlogon = 256,
		// Token: 0x04004D70 RID: 19824
		LocalDCStateOsversion = 258,
		// Token: 0x04004D71 RID: 19825
		LocalDCTimeRead = 260,
		// Token: 0x04004D72 RID: 19826
		LocalDCTimeReadBase = 262,
		// Token: 0x04004D73 RID: 19827
		LocalDCTimeSearch = 264,
		// Token: 0x04004D74 RID: 19828
		LocalDCTimeSearchBase = 266,
		// Token: 0x04004D75 RID: 19829
		GlobalObj = 268,
		// Token: 0x04004D76 RID: 19830
		GlobalTimeDiscovery = 270,
		// Token: 0x04004D77 RID: 19831
		GlobalTimeDnsquery = 272,
		// Token: 0x04004D78 RID: 19832
		GlobalDCInSite = 274,
		// Token: 0x04004D79 RID: 19833
		GlobalGCInSite = 276,
		// Token: 0x04004D7A RID: 19834
		GlobalDCOutOfSite = 278,
		// Token: 0x04004D7B RID: 19835
		GlobalGCOutOfSite = 280,
		// Token: 0x04004D7C RID: 19836
		CacheShortObjEnd = 60,
		// Token: 0x04004D7D RID: 19837
		CacheShortObjNum = 30,
		// Token: 0x04004D7E RID: 19838
		CacheLongObjEnd = 74,
		// Token: 0x04004D7F RID: 19839
		CacheLongObjNum = 7,
		// Token: 0x04004D80 RID: 19840
		CacheObjNum = 37,
		// Token: 0x04004D81 RID: 19841
		ProcessShortObjEnd = 112,
		// Token: 0x04004D82 RID: 19842
		ProcessShortObjNum = 18,
		// Token: 0x04004D83 RID: 19843
		ProcessSearchObjEnd = 144,
		// Token: 0x04004D84 RID: 19844
		ProcessSearchObjNum = 8,
		// Token: 0x04004D85 RID: 19845
		ProcessObjNum = 34,
		// Token: 0x04004D86 RID: 19846
		DCShortObjEnd = 198,
		// Token: 0x04004D87 RID: 19847
		DCShortObjNum = 26,
		// Token: 0x04004D88 RID: 19848
		LocalDCShortObjNum = 25,
		// Token: 0x04004D89 RID: 19849
		DCSearchObjEnd = 206,
		// Token: 0x04004D8A RID: 19850
		DCSearchObjNum = 2,
		// Token: 0x04004D8B RID: 19851
		DCObjNum = 30,
		// Token: 0x04004D8C RID: 19852
		LocalDCObjNum = 29,
		// Token: 0x04004D8D RID: 19853
		GlobalObjEnd = 280,
		// Token: 0x04004D8E RID: 19854
		GlobalObjNum = 6
	}
}
