using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000049 RID: 73
	public enum OwaUserConfigurationLogMetadata
	{
		// Token: 0x04000347 RID: 839
		[DisplayName("OUC", "AGG")]
		AggregationStats,
		// Token: 0x04000348 RID: 840
		[DisplayName("OUC", "UC")]
		UserCulture,
		// Token: 0x04000349 RID: 841
		[DisplayName("OUC", "UOT")]
		UserOptionsLoadTime,
		// Token: 0x0400034A RID: 842
		[DisplayName("OUC", "UORC")]
		UserOptionsLoadRpcCount,
		// Token: 0x0400034B RID: 843
		[DisplayName("OUC", "UORL")]
		UserOptionsLoadRpcLatency,
		// Token: 0x0400034C RID: 844
		[DisplayName("OUC", "UORLS")]
		UserOptionsLoadRpcLatencyOnStore,
		// Token: 0x0400034D RID: 845
		[DisplayName("OUC", "UOCpu")]
		UserOptionsLoadCPUTime,
		// Token: 0x0400034E RID: 846
		[DisplayName("OUC", "WHT")]
		WorkingHoursTime,
		// Token: 0x0400034F RID: 847
		[DisplayName("OUC", "WHRC")]
		WorkingHoursRpcCount,
		// Token: 0x04000350 RID: 848
		[DisplayName("OUC", "WHRL")]
		WorkingHoursRpcLatency,
		// Token: 0x04000351 RID: 849
		[DisplayName("OUC", "WHRLS")]
		WorkingHoursRpcLatencyOnStore,
		// Token: 0x04000352 RID: 850
		[DisplayName("OUC", "WHCpu")]
		WorkingHoursCPUTime,
		// Token: 0x04000353 RID: 851
		[DisplayName("OUC", "RT")]
		ReminderTime,
		// Token: 0x04000354 RID: 852
		[DisplayName("OUC", "RRC")]
		ReminderRpcCount,
		// Token: 0x04000355 RID: 853
		[DisplayName("OUC", "RRL")]
		ReminderRpcLatency,
		// Token: 0x04000356 RID: 854
		[DisplayName("OUC", "RRLS")]
		ReminderRpcLatencyOnStore,
		// Token: 0x04000357 RID: 855
		[DisplayName("OUC", "RCpu")]
		ReminderCPUTime,
		// Token: 0x04000358 RID: 856
		[DisplayName("OUC", "SST")]
		SessionSettingsMiscTime,
		// Token: 0x04000359 RID: 857
		[DisplayName("OUC", "SSRC")]
		SessionSettingsMiscRpcCount,
		// Token: 0x0400035A RID: 858
		[DisplayName("OUC", "SSRL")]
		SessionSettingsMiscRpcLatency,
		// Token: 0x0400035B RID: 859
		[DisplayName("OUC", "SSRLS")]
		SessionSettingsMiscRpcLatencyOnStore,
		// Token: 0x0400035C RID: 860
		[DisplayName("OUC", "SSCpu")]
		SessionSettingsMiscCPUTime,
		// Token: 0x0400035D RID: 861
		[DisplayName("OUC", "SSmmsT")]
		SessionSettingsMessageSizeTime,
		// Token: 0x0400035E RID: 862
		[DisplayName("OUC", "SSmmsRC")]
		SessionSettingsMessageSizeRpcCount,
		// Token: 0x0400035F RID: 863
		[DisplayName("OUC", "SSmmsRL")]
		SessionSettingsMessageSizeRpcLatency,
		// Token: 0x04000360 RID: 864
		[DisplayName("OUC", "SSmmsRLS")]
		SessionSettingsMessageSizeRpcLatencyOnStore,
		// Token: 0x04000361 RID: 865
		[DisplayName("OUC", "SSmmsCpu")]
		SessionSettingsMessageSizeCPUTime,
		// Token: 0x04000362 RID: 866
		[DisplayName("OUC", "SSplT")]
		SessionSettingsIsPublicLogonTime,
		// Token: 0x04000363 RID: 867
		[DisplayName("OUC", "SSplRC")]
		SessionSettingsPublicLogonRpcCount,
		// Token: 0x04000364 RID: 868
		[DisplayName("OUC", "SSplRL")]
		SessionSettingsPublicLogonRpcLatency,
		// Token: 0x04000365 RID: 869
		[DisplayName("OUC", "SSplRLS")]
		SessionSettingsPublicLogonRpcLatencyOnStore,
		// Token: 0x04000366 RID: 870
		[DisplayName("OUC", "SSplCpu")]
		SessionSettingsPublicLogonCPUTime,
		// Token: 0x04000367 RID: 871
		[DisplayName("OUC", "TMT")]
		TeamMailboxTime,
		// Token: 0x04000368 RID: 872
		[DisplayName("OUC", "TMRC")]
		TeamMailboxRpcCount,
		// Token: 0x04000369 RID: 873
		[DisplayName("OUC", "TMRL")]
		TeamMailboxRpcLatency,
		// Token: 0x0400036A RID: 874
		[DisplayName("OUC", "TMRLS")]
		TeamMailboxRpcLatencyOnStore,
		// Token: 0x0400036B RID: 875
		[DisplayName("OUC", "TMCpu")]
		TeamMailboxCPUTime,
		// Token: 0x0400036C RID: 876
		[DisplayName("OUC", "MRT")]
		MiniRecipientTime,
		// Token: 0x0400036D RID: 877
		[DisplayName("OUC", "MRRC")]
		MiniRecipientRpcCount,
		// Token: 0x0400036E RID: 878
		[DisplayName("OUC", "MRRL")]
		MiniRecipientRpcLatency,
		// Token: 0x0400036F RID: 879
		[DisplayName("OUC", "MRRLS")]
		MiniRecipientRpcLatencyOnStore,
		// Token: 0x04000370 RID: 880
		[DisplayName("OUC", "MRCpu")]
		MiniRecipientCPUTime,
		// Token: 0x04000371 RID: 881
		[DisplayName("OUC", "DFT")]
		DefaultFolderTime,
		// Token: 0x04000372 RID: 882
		[DisplayName("OUC", "DFRC")]
		DefaultFolderRpcCount,
		// Token: 0x04000373 RID: 883
		[DisplayName("OUC", "DFRL")]
		DefaultFolderRpcLatency,
		// Token: 0x04000374 RID: 884
		[DisplayName("OUC", "DFRLS")]
		DefaultFolderRpcLatencyOnStore,
		// Token: 0x04000375 RID: 885
		[DisplayName("OUC", "DFCpu")]
		DefaultFolderCPUTime,
		// Token: 0x04000376 RID: 886
		[DisplayName("OUC", "UMT")]
		UMClientTime,
		// Token: 0x04000377 RID: 887
		[DisplayName("OUC", "UMRC")]
		UMClientRpcCount,
		// Token: 0x04000378 RID: 888
		[DisplayName("OUC", "UMRL")]
		UMClientRpcLatency,
		// Token: 0x04000379 RID: 889
		[DisplayName("OUC", "UMRLS")]
		UMClientRpcLatencyOnStore,
		// Token: 0x0400037A RID: 890
		[DisplayName("OUC", "UMCpu")]
		UMClientCPUTime,
		// Token: 0x0400037B RID: 891
		[DisplayName("OUC", "DCT")]
		IsDatacenterModeTime,
		// Token: 0x0400037C RID: 892
		[DisplayName("OUC", "DCRC")]
		IsDatacenterModeRpcCount,
		// Token: 0x0400037D RID: 893
		[DisplayName("OUC", "DCRL")]
		IsDatacenterModeRpcLatency,
		// Token: 0x0400037E RID: 894
		[DisplayName("OUC", "DCRLS")]
		IsDatacenterModeRpcLatencyOnStore,
		// Token: 0x0400037F RID: 895
		[DisplayName("OUC", "DCCpu")]
		IsDatacenterModeCPUTime,
		// Token: 0x04000380 RID: 896
		[DisplayName("OUC", "VST")]
		ViewStateTime,
		// Token: 0x04000381 RID: 897
		[DisplayName("OUC", "VSRC")]
		ViewStateRpcCount,
		// Token: 0x04000382 RID: 898
		[DisplayName("OUC", "VSRL")]
		ViewStateRpcLatency,
		// Token: 0x04000383 RID: 899
		[DisplayName("OUC", "VSRLS")]
		ViewStateRpcLatencyOnStore,
		// Token: 0x04000384 RID: 900
		[DisplayName("OUC", "VSCpu")]
		ViewStateCPUTime,
		// Token: 0x04000385 RID: 901
		[DisplayName("OUC", "MTT")]
		MailTipsTime,
		// Token: 0x04000386 RID: 902
		[DisplayName("OUC", "MTRC")]
		MailTipsRpcCount,
		// Token: 0x04000387 RID: 903
		[DisplayName("OUC", "MTRL")]
		MailTipsRpcLatency,
		// Token: 0x04000388 RID: 904
		[DisplayName("OUC", "MTRLS")]
		MailTipsRpcLatencyOnStore,
		// Token: 0x04000389 RID: 905
		[DisplayName("OUC", "MTCpu")]
		MailTipsCPUTime,
		// Token: 0x0400038A RID: 906
		[DisplayName("OUC", "RPT")]
		RetentionPolicyTime,
		// Token: 0x0400038B RID: 907
		[DisplayName("OUC", "RPRC")]
		RetentionPolicyRpcCount,
		// Token: 0x0400038C RID: 908
		[DisplayName("OUC", "RPRL")]
		RetentionPolicyRpcLatency,
		// Token: 0x0400038D RID: 909
		[DisplayName("OUC", "RPRLS")]
		RetentionPolicyRpcLatencyOnStore,
		// Token: 0x0400038E RID: 910
		[DisplayName("OUC", "RPCpu")]
		RetentionPolicyCPUTime,
		// Token: 0x0400038F RID: 911
		[DisplayName("OUC", "MCT")]
		MasterCategoryListTime,
		// Token: 0x04000390 RID: 912
		[DisplayName("OUC", "MCRC")]
		MasterCategoryListRpcCount,
		// Token: 0x04000391 RID: 913
		[DisplayName("OUC", "MCRL")]
		MasterCategoryListRpcLatency,
		// Token: 0x04000392 RID: 914
		[DisplayName("OUC", "MCRLS")]
		MasterCategoryListRpcLatencyOnStore,
		// Token: 0x04000393 RID: 915
		[DisplayName("OUC", "MCCpu")]
		MasterCategoryListCPUTime,
		// Token: 0x04000394 RID: 916
		[DisplayName("OUC", "MT")]
		MaxRecipientsTime,
		// Token: 0x04000395 RID: 917
		[DisplayName("OUC", "MRC")]
		MaxRecipientsRpcCount,
		// Token: 0x04000396 RID: 918
		[DisplayName("OUC", "MRL")]
		MaxRecipientsRpcLatency,
		// Token: 0x04000397 RID: 919
		[DisplayName("OUC", "MRLS")]
		MaxRecipientsRpcLatencyOnStore,
		// Token: 0x04000398 RID: 920
		[DisplayName("OUC", "MCpu")]
		MaxRecipientsCPUTime,
		// Token: 0x04000399 RID: 921
		[DisplayName("OUC", "PST")]
		PolicySettingsTime,
		// Token: 0x0400039A RID: 922
		[DisplayName("OUC", "PSRC")]
		PolicySettingsRpcCount,
		// Token: 0x0400039B RID: 923
		[DisplayName("OUC", "PSRL")]
		PolicySettingsRpcLatency,
		// Token: 0x0400039C RID: 924
		[DisplayName("OUC", "PSRLS")]
		PolicySettingsRpcLatencyOnStore,
		// Token: 0x0400039D RID: 925
		[DisplayName("OUC", "PSCpu")]
		PolicySettingsCPUTime,
		// Token: 0x0400039E RID: 926
		[DisplayName("OUC", "SEST")]
		SessionSettingsTime,
		// Token: 0x0400039F RID: 927
		[DisplayName("OUC", "SESRC")]
		SessionSettingsRpcCount,
		// Token: 0x040003A0 RID: 928
		[DisplayName("OUC", "SESRL")]
		SessionSettingsRpcLatency,
		// Token: 0x040003A1 RID: 929
		[DisplayName("OUC", "SESRLS")]
		SessionSettingsRpcLatencyOnStore,
		// Token: 0x040003A2 RID: 930
		[DisplayName("OUC", "SESCpu")]
		SessionSettingsCPUTime,
		// Token: 0x040003A3 RID: 931
		[DisplayName("OUC", "CCT")]
		ConfigContextTime,
		// Token: 0x040003A4 RID: 932
		[DisplayName("OUC", "CCRC")]
		ConfigContextRpcCount,
		// Token: 0x040003A5 RID: 933
		[DisplayName("OUC", "CCRL")]
		ConfigContextRpcLatency,
		// Token: 0x040003A6 RID: 934
		[DisplayName("OUC", "CCRLS")]
		ConfigContextRpcLatencyOnStore,
		// Token: 0x040003A7 RID: 935
		[DisplayName("OUC", "CCCpu")]
		ConfigContextCPUTime,
		// Token: 0x040003A8 RID: 936
		[DisplayName("OUC", "SEGT")]
		SegmentationSettingsTime,
		// Token: 0x040003A9 RID: 937
		[DisplayName("OUC", "SEGRC")]
		SegmentationSettingsRpcCount,
		// Token: 0x040003AA RID: 938
		[DisplayName("OUC", "SEGRL")]
		SegmentationSettingsRpcLatency,
		// Token: 0x040003AB RID: 939
		[DisplayName("OUC", "SEGRLS")]
		SegmentationSettingsRpcLatencyOnStore,
		// Token: 0x040003AC RID: 940
		[DisplayName("OUC", "SEGCpu")]
		SegmentationSettingsCPUTime,
		// Token: 0x040003AD RID: 941
		[DisplayName("OUC", "ACHT")]
		AttachmentPolicyTime,
		// Token: 0x040003AE RID: 942
		[DisplayName("OUC", "ACHRC")]
		AttachmentPolicyRpcCount,
		// Token: 0x040003AF RID: 943
		[DisplayName("OUC", "ACHRL")]
		AttachmentPolicyRpcLatency,
		// Token: 0x040003B0 RID: 944
		[DisplayName("OUC", "ACHRLS")]
		AttachmentPolicyRpcLatencyOnStore,
		// Token: 0x040003B1 RID: 945
		[DisplayName("OUC", "ACHCpu")]
		AttachmentPolicyCPUTime,
		// Token: 0x040003B2 RID: 946
		[DisplayName("OUC", "PWT")]
		PlacesWeatherTime,
		// Token: 0x040003B3 RID: 947
		[DisplayName("OUC", "PWRC")]
		PlacesWeatherRpcCount,
		// Token: 0x040003B4 RID: 948
		[DisplayName("OUC", "PWRL")]
		PlacesWeatherRpcLatency,
		// Token: 0x040003B5 RID: 949
		[DisplayName("OUC", "PWRLS")]
		PlacesWeatherRpcLatencyOnStore,
		// Token: 0x040003B6 RID: 950
		[DisplayName("OUC", "PWCpu")]
		PlacesWeatherCPUTime,
		// Token: 0x040003B7 RID: 951
		[DisplayName("OUC", "GRP")]
		GlobalReadingPanePosition,
		// Token: 0x040003B8 RID: 952
		[DisplayName("OUC", "CSO")]
		ConversationSortOrder,
		// Token: 0x040003B9 RID: 953
		[DisplayName("OUC", "HDI")]
		HideDeletedItems,
		// Token: 0x040003BA RID: 954
		[DisplayName("OUC", "FFTC")]
		IsFavoritesFolderTreeCollapsed,
		// Token: 0x040003BB RID: 955
		[DisplayName("OUC", "MRTC")]
		IsMailRootFolderTreeCollapsed,
		// Token: 0x040003BC RID: 956
		[DisplayName("OUC", "MFPE")]
		MailFolderPaneExpanded,
		// Token: 0x040003BD RID: 957
		[DisplayName("OUC", "PTLV")]
		ShowPreviewTextInListView,
		// Token: 0x040003BE RID: 958
		[DisplayName("OUC", "SSTLV")]
		ShowSenderOnTopInListView,
		// Token: 0x040003BF RID: 959
		[DisplayName("OUC", "RPFL")]
		ShowReadingPaneOnFirstLoad,
		// Token: 0x040003C0 RID: 960
		[DisplayName("OUC", "NPVO")]
		NavigationPaneViewOption,
		// Token: 0x040003C1 RID: 961
		[DisplayName("OUC", "EDI")]
		EmptyDeletedItemsOnLogoff,
		// Token: 0x040003C2 RID: 962
		[DisplayName("OUC", "IOA")]
		IsOptimizedForAccessibility,
		// Token: 0x040003C3 RID: 963
		[DisplayName("OUC", "PIKFC")]
		IsPeopleIKnowFolderTreeCollapsed,
		// Token: 0x040003C4 RID: 964
		[DisplayName("OUC", "MRDT")]
		MarkAsReadDelaytime,
		// Token: 0x040003C5 RID: 965
		[DisplayName("OUC", "NS")]
		NextSelection,
		// Token: 0x040003C6 RID: 966
		[DisplayName("OUC", "PMR")]
		PreviewMarkAsRead,
		// Token: 0x040003C7 RID: 967
		[DisplayName("OUC", "PNC")]
		PrimaryNavigationCollapsed,
		// Token: 0x040003C8 RID: 968
		[DisplayName("OUC", "RJS")]
		ReportJunkSelected,
		// Token: 0x040003C9 RID: 969
		[DisplayName("OUC", "SIUE")]
		ShowInferenceUiElements,
		// Token: 0x040003CA RID: 970
		[DisplayName("OUC", "STIL")]
		ShowTreeInListView,
		// Token: 0x040003CB RID: 971
		[DisplayName("OUC", "PIKU")]
		PeopleIKnowUse,
		// Token: 0x040003CC RID: 972
		[DisplayName("OUC", "SS")]
		SearchScope,
		// Token: 0x040003CD RID: 973
		[DisplayName("OUC", "GFVS")]
		GlobalFolderViewState,
		// Token: 0x040003CE RID: 974
		[DisplayName("OUC", "AFI")]
		ArchiveFolderId,
		// Token: 0x040003CF RID: 975
		[DisplayName("OUC", "RTIC")]
		IsRenewTimeIndexComplete,
		// Token: 0x040003D0 RID: 976
		[DisplayName("OUC", "DPFT")]
		DefaultPublicFolderMailboxTime,
		// Token: 0x040003D1 RID: 977
		[DisplayName("OUC", "DPFRC")]
		DefaultPublicFolderMailboxRpcCount,
		// Token: 0x040003D2 RID: 978
		[DisplayName("OUC", "DPFRL")]
		DefaultPublicFolderMailboxRpcLatency,
		// Token: 0x040003D3 RID: 979
		[DisplayName("OUC", "DPFRLS")]
		DefaultPublicFolderMailboxRpcLatencyOnStore,
		// Token: 0x040003D4 RID: 980
		[DisplayName("OUC", "DPFCpu")]
		DefaultPublicFolderMailboxCPUTime,
		// Token: 0x040003D5 RID: 981
		[DisplayName("OUC", "DPFE")]
		DefaultPublicFolderMailboxError
	}
}
