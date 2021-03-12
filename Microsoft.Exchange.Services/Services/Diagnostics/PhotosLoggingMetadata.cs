using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200004C RID: 76
	internal enum PhotosLoggingMetadata
	{
		// Token: 0x040003EB RID: 1003
		[DisplayName("GPP", "ISP")]
		IsSelfPhoto,
		// Token: 0x040003EC RID: 1004
		[DisplayName("GPP", "TgEm")]
		TargetEmailAddress,
		// Token: 0x040003ED RID: 1005
		[DisplayName("GPP", "PhSz")]
		PhotoSize,
		// Token: 0x040003EE RID: 1006
		[DisplayName("GPP", "SCH")]
		ServerCacheHit,
		// Token: 0x040003EF RID: 1007
		[DisplayName("GPP", "ADP")]
		ADHandlerProcessed,
		// Token: 0x040003F0 RID: 1008
		[DisplayName("GPP", "FSP")]
		FileSystemHandlerProcessed,
		// Token: 0x040003F1 RID: 1009
		[DisplayName("GPP", "MbxP")]
		MailboxHandlerProcessed,
		// Token: 0x040003F2 RID: 1010
		[DisplayName("GPP", "CaP")]
		CachingHandlerProcessed,
		// Token: 0x040003F3 RID: 1011
		[DisplayName("GPP", "HttpP")]
		HttpHandlerProcessed,
		// Token: 0x040003F4 RID: 1012
		[DisplayName("GPP", "PrvP")]
		PrivateHandlerProcessed,
		// Token: 0x040003F5 RID: 1013
		[DisplayName("GPP", "PrvSC")]
		PrivateHandlerServedContent,
		// Token: 0x040003F6 RID: 1014
		[DisplayName("GPP", "PrvSR")]
		PrivateHandlerServedRedirect,
		// Token: 0x040003F7 RID: 1015
		[DisplayName("GPP", "ADT")]
		ADHandlerTotalTime,
		// Token: 0x040003F8 RID: 1016
		[DisplayName("GPP", "ADC")]
		ADHandlerTotalLdapCount,
		// Token: 0x040003F9 RID: 1017
		[DisplayName("GPP", "ADL")]
		ADHandlerTotalLdapLatency,
		// Token: 0x040003FA RID: 1018
		[DisplayName("GPP", "ADRdPT")]
		ADHandlerReadPhotoTime,
		// Token: 0x040003FB RID: 1019
		[DisplayName("GPP", "ADRdPC")]
		ADHandlerReadPhotoLdapCount,
		// Token: 0x040003FC RID: 1020
		[DisplayName("GPP", "ADRdPL")]
		ADHandlerReadPhotoLdapLatency,
		// Token: 0x040003FD RID: 1021
		[DisplayName("GPP", "FST")]
		FileSystemHandlerTotalTime,
		// Token: 0x040003FE RID: 1022
		[DisplayName("GPP", "FSCpu")]
		FileSystemHandlerTotalCpuTime,
		// Token: 0x040003FF RID: 1023
		[DisplayName("GPP", "FSRdPT")]
		FileSystemHandlerReadPhotoTime,
		// Token: 0x04000400 RID: 1024
		[DisplayName("GPP", "FSRdPCpu")]
		FileSystemHandlerReadPhotoCpuTime,
		// Token: 0x04000401 RID: 1025
		[DisplayName("GPP", "FSRdThT")]
		FileSystemHandlerReadThumbprintTime,
		// Token: 0x04000402 RID: 1026
		[DisplayName("GPP", "FSRdThCpu")]
		FileSystemHandlerReadThumbprintCpuTime,
		// Token: 0x04000403 RID: 1027
		[DisplayName("GPP", "MbxT")]
		MailboxHandlerTotalTime,
		// Token: 0x04000404 RID: 1028
		[DisplayName("GPP", "MbxRpcC")]
		MailboxHandlerTotalStoreRpcCount,
		// Token: 0x04000405 RID: 1029
		[DisplayName("GPP", "MbxRpcL")]
		MailboxHandlerTotalStoreRpcLatency,
		// Token: 0x04000406 RID: 1030
		[DisplayName("GPP", "MbxOpMT")]
		MailboxHandlerOpenMailboxTime,
		// Token: 0x04000407 RID: 1031
		[DisplayName("GPP", "MbxOpMRpcC")]
		MailboxHandlerOpenMailboxStoreRpcCount,
		// Token: 0x04000408 RID: 1032
		[DisplayName("GPP", "MbxOpMRpcL")]
		MailboxHandlerOpenMailboxStoreRpcLatency,
		// Token: 0x04000409 RID: 1033
		[DisplayName("GPP", "MbxRdPT")]
		MailboxHandlerReadPhotoTime,
		// Token: 0x0400040A RID: 1034
		[DisplayName("GPP", "MbxRdPRpcC")]
		MailboxHandlerReadPhotoStoreRpcCount,
		// Token: 0x0400040B RID: 1035
		[DisplayName("GPP", "MbxRdPRpcL")]
		MailboxHandlerReadPhotoStoreRpcLatency,
		// Token: 0x0400040C RID: 1036
		[DisplayName("GPP", "MbxRFdPT")]
		MailboxPhotoReaderFindPhotoTime,
		// Token: 0x0400040D RID: 1037
		[DisplayName("GPP", "MbxRFdPRpcC")]
		MailboxPhotoReaderFindPhotoStoreRpcCount,
		// Token: 0x0400040E RID: 1038
		[DisplayName("GPP", "MbxRFdPRpcL")]
		MailboxPhotoReaderFindPhotoStoreRpcLatency,
		// Token: 0x0400040F RID: 1039
		[DisplayName("GPP", "MbxRBdPT")]
		MailboxPhotoReaderBindPhotoItemTime,
		// Token: 0x04000410 RID: 1040
		[DisplayName("GPP", "MbxRBdPRpcC")]
		MailboxPhotoReaderBindPhotoItemStoreRpcCount,
		// Token: 0x04000411 RID: 1041
		[DisplayName("GPP", "MbxRBdPRpcL")]
		MailboxPhotoReaderBindPhotoItemStoreRpcLatency,
		// Token: 0x04000412 RID: 1042
		[DisplayName("GPP", "MbxRRdST")]
		MailboxPhotoReaderReadStreamTime,
		// Token: 0x04000413 RID: 1043
		[DisplayName("GPP", "MbxRRdSRpcC")]
		MailboxPhotoReaderReadStreamStoreRpcCount,
		// Token: 0x04000414 RID: 1044
		[DisplayName("GPP", "MbxRRdSRpcL")]
		MailboxPhotoReaderReadStreamStoreRpcLatency,
		// Token: 0x04000415 RID: 1045
		[DisplayName("GPP", "CaT")]
		CachingHandlerTotalTime,
		// Token: 0x04000416 RID: 1046
		[DisplayName("GPP", "CaCpu")]
		CachingHandlerTotalCpuTime,
		// Token: 0x04000417 RID: 1047
		[DisplayName("GPP", "CaPT")]
		CachingHandlerCachePhotoTime,
		// Token: 0x04000418 RID: 1048
		[DisplayName("GPP", "CaPCpu")]
		CachingHandlerCachePhotoCpuTime,
		// Token: 0x04000419 RID: 1049
		[DisplayName("GPP", "CaNPT")]
		CachingHandlerCacheNegativePhotoTime,
		// Token: 0x0400041A RID: 1050
		[DisplayName("GPP", "CaNPCpu")]
		CachingHandlerCacheNegativePhotoCpuTime,
		// Token: 0x0400041B RID: 1051
		[DisplayName("GPP", "LdPT")]
		GetPersonFromPersonIdTime,
		// Token: 0x0400041C RID: 1052
		[DisplayName("GPP", "LdPRpcC")]
		GetPersonFromPersonIdStoreRpcCount,
		// Token: 0x0400041D RID: 1053
		[DisplayName("GPP", "LdPRpcL")]
		GetPersonFromPersonIdStoreRpcLatency,
		// Token: 0x0400041E RID: 1054
		[DisplayName("GPP", "FdEmADT")]
		FindEmailAddressFromADObjectIdTime,
		// Token: 0x0400041F RID: 1055
		[DisplayName("GPP", "FdEmADC")]
		FindEmailAddressFromADObjectIdLdapCount,
		// Token: 0x04000420 RID: 1056
		[DisplayName("GPP", "FdEmADL")]
		FindEmailAddressFromADObjectIdLdapLatency,
		// Token: 0x04000421 RID: 1057
		[DisplayName("GPP", "FdPT")]
		FindPersonIdByEmailAddressTime,
		// Token: 0x04000422 RID: 1058
		[DisplayName("GPP", "FdPRpcC")]
		FindPersonIdByEmailAddressStoreRpcCount,
		// Token: 0x04000423 RID: 1059
		[DisplayName("GPP", "FdPRpcL")]
		FindPersonIdByEmailAddressStoreRpcLatency,
		// Token: 0x04000424 RID: 1060
		[DisplayName("GPP", "LdPST")]
		GetPhotoStreamFromPersonTime,
		// Token: 0x04000425 RID: 1061
		[DisplayName("GPP", "LdPSRpcC")]
		GetPhotoStreamFromPersonStoreRpcCount,
		// Token: 0x04000426 RID: 1062
		[DisplayName("GPP", "LdPSRpcL")]
		GetPhotoStreamFromPersonStoreRpcLatency,
		// Token: 0x04000427 RID: 1063
		[DisplayName("GPP", "GPPT")]
		GetPersonaPhotoTotalTime,
		// Token: 0x04000428 RID: 1064
		[DisplayName("GPP", "GPPADC")]
		GetPersonaPhotoTotalLdapCount,
		// Token: 0x04000429 RID: 1065
		[DisplayName("GPP", "GPPADL")]
		GetPersonaPhotoTotalLdapLatency,
		// Token: 0x0400042A RID: 1066
		[DisplayName("GPP", "GPPRpcC")]
		GetPersonaPhotoTotalStoreRpcCount,
		// Token: 0x0400042B RID: 1067
		[DisplayName("GPP", "GPPRpcL")]
		GetPersonaPhotoTotalStoreRpcLatency,
		// Token: 0x0400042C RID: 1068
		[DisplayName("GPP", "GUPT")]
		GetUserPhotoTotalTime,
		// Token: 0x0400042D RID: 1069
		[DisplayName("GPP", "GUPADC")]
		GetUserPhotoTotalLdapCount,
		// Token: 0x0400042E RID: 1070
		[DisplayName("GPP", "GUPADL")]
		GetUserPhotoTotalLdapLatency,
		// Token: 0x0400042F RID: 1071
		[DisplayName("GPP", "GUPRpcC")]
		GetUserPhotoTotalStoreRpcCount,
		// Token: 0x04000430 RID: 1072
		[DisplayName("GPP", "GUPRpcL")]
		GetUserPhotoTotalStoreRpcLatency,
		// Token: 0x04000431 RID: 1073
		[DisplayName("GPP", "PxyT")]
		ProxyRequestTime,
		// Token: 0x04000432 RID: 1074
		[DisplayName("GPP", "QRsT")]
		GetUserPhotoQueryResolveTargetInDirectoryTime,
		// Token: 0x04000433 RID: 1075
		[DisplayName("GPP", "QRsADC")]
		GetUserPhotoQueryResolveTargetInDirectoryLdapCount,
		// Token: 0x04000434 RID: 1076
		[DisplayName("GPP", "QRsADL")]
		GetUserPhotoQueryResolveTargetInDirectoryLdapLatency,
		// Token: 0x04000435 RID: 1077
		[DisplayName("GPP", "LAzT")]
		LocalAuthorizationTime,
		// Token: 0x04000436 RID: 1078
		[DisplayName("GPP", "LAzADC")]
		LocalAuthorizationLdapCount,
		// Token: 0x04000437 RID: 1079
		[DisplayName("GPP", "LAzADL")]
		LocalAuthorizationLdapLatency,
		// Token: 0x04000438 RID: 1080
		[DisplayName("GPP", "GUPFail")]
		GetUserPhotoFailed,
		// Token: 0x04000439 RID: 1081
		[DisplayName("GPP", "GPPFail")]
		GetPersonaPhotoFailed,
		// Token: 0x0400043A RID: 1082
		[DisplayName("GPP", "QryCT")]
		GetUserPhotoQueryCreationTime,
		// Token: 0x0400043B RID: 1083
		[DisplayName("GPP", "QryCADC")]
		GetUserPhotoQueryCreationLdapCount,
		// Token: 0x0400043C RID: 1084
		[DisplayName("GPP", "QryCADL")]
		GetUserPhotoQueryCreationLdapLatency,
		// Token: 0x0400043D RID: 1085
		[DisplayName("GPP", "MRD")]
		MiscRoutingAndDiscovery,
		// Token: 0x0400043E RID: 1086
		[DisplayName("GPP", "CrtCliCtxT")]
		CreateClientContextTime,
		// Token: 0x0400043F RID: 1087
		[DisplayName("GPP", "CrtCliCtxADC")]
		CreateClientContextLdapCount,
		// Token: 0x04000440 RID: 1088
		[DisplayName("GPP", "CrtCliCtxADL")]
		CreateClientContextLdapLatency,
		// Token: 0x04000441 RID: 1089
		[DisplayName("GPP", "PxyRest")]
		RestProxy,
		// Token: 0x04000442 RID: 1090
		[DisplayName("GPP", "MbxCompEPT")]
		MailboxPhotoHandlerComputeTargetPrincipalTime,
		// Token: 0x04000443 RID: 1091
		[DisplayName("GPP", "MbxCompEPC")]
		MailboxPhotoHandlerComputeTargetPrincipalLdapCount,
		// Token: 0x04000444 RID: 1092
		[DisplayName("GPP", "MbxCompEPL")]
		MailboxPhotoHandlerComputeTargetPrincipalLdapLatency,
		// Token: 0x04000445 RID: 1093
		[DisplayName("GPP", "ADSer")]
		ADHandlerPhotoServed,
		// Token: 0x04000446 RID: 1094
		[DisplayName("GPP", "ADFSer")]
		ADFallbackPhotoServed,
		// Token: 0x04000447 RID: 1095
		[DisplayName("GPP", "FSSer")]
		FileSystemHandlerPhotoServed,
		// Token: 0x04000448 RID: 1096
		[DisplayName("GPP", "MbxSer")]
		MailboxHandlerPhotoServed,
		// Token: 0x04000449 RID: 1097
		[DisplayName("GPP", "ADHErr")]
		ADHandlerError,
		// Token: 0x0400044A RID: 1098
		[DisplayName("GPP", "FSHErr")]
		FileSystemHandlerError,
		// Token: 0x0400044B RID: 1099
		[DisplayName("GPP", "MbxHErr")]
		MailboxHandlerError,
		// Token: 0x0400044C RID: 1100
		[DisplayName("GPP", "ADPhAvail")]
		ADHandlerPhotoAvailabile,
		// Token: 0x0400044D RID: 1101
		[DisplayName("GPP", "FSPhAvail")]
		FileSystemHandlerPhotoAvailabile,
		// Token: 0x0400044E RID: 1102
		[DisplayName("GPP", "MbxPhAvail")]
		MailboxHandlerPhotoAvailabile,
		// Token: 0x0400044F RID: 1103
		[DisplayName("GPP", "HttpT")]
		HttpHandlerTotalTime,
		// Token: 0x04000450 RID: 1104
		[DisplayName("GPP", "HttpADC")]
		HttpHandlerTotalLdapCount,
		// Token: 0x04000451 RID: 1105
		[DisplayName("GPP", "HttpADL")]
		HttpHandlerTotalLdapLatency,
		// Token: 0x04000452 RID: 1106
		[DisplayName("GPP", "HttpSendNGetRespT")]
		HttpHandlerSendRequestAndGetResponseTime,
		// Token: 0x04000453 RID: 1107
		[DisplayName("GPP", "HttpSendNGetRespADC")]
		HttpHandlerSendRequestAndGetResponseLdapCount,
		// Token: 0x04000454 RID: 1108
		[DisplayName("GPP", "HttpSendNGetRespADL")]
		HttpHandlerSendRequestAndGetResponseLdapLatency,
		// Token: 0x04000455 RID: 1109
		[DisplayName("GPP", "HttpRdRespT")]
		HttpHandlerGetAndReadResponseStreamTime,
		// Token: 0x04000456 RID: 1110
		[DisplayName("GPP", "HttpLocSvcT")]
		HttpHandlerLocateServiceTime,
		// Token: 0x04000457 RID: 1111
		[DisplayName("GPP", "HttpLocSvcADC")]
		HttpHandlerLocateServiceLdapCount,
		// Token: 0x04000458 RID: 1112
		[DisplayName("GPP", "HttpLocSvcADL")]
		HttpHandlerLocateServiceLdapLatency,
		// Token: 0x04000459 RID: 1113
		[DisplayName("GPP", "RouterT")]
		RouterTotalTime,
		// Token: 0x0400045A RID: 1114
		[DisplayName("GPP", "RouterADC")]
		RouterTotalLdapCount,
		// Token: 0x0400045B RID: 1115
		[DisplayName("GPP", "RouterADL")]
		RouterTotalLdapLatency,
		// Token: 0x0400045C RID: 1116
		[DisplayName("GPP", "RouterLkTargetDirT")]
		RouterLookupTargetInDirectoryTime,
		// Token: 0x0400045D RID: 1117
		[DisplayName("GPP", "RouterLkTargetDirADC")]
		RouterLookupTargetInDirectoryLdapCount,
		// Token: 0x0400045E RID: 1118
		[DisplayName("GPP", "RouterLkTargetDirADL")]
		RouterLookupTargetInDirectoryLdapLatency,
		// Token: 0x0400045F RID: 1119
		[DisplayName("GPP", "RouterChkTargetMbxThisServerT")]
		RouterCheckTargetMailboxOnThisServerTime,
		// Token: 0x04000460 RID: 1120
		[DisplayName("GPP", "RouterChkTargetMbxThisServerADC")]
		RouterCheckTargetMailboxOnThisServerLdapCount,
		// Token: 0x04000461 RID: 1121
		[DisplayName("GPP", "RouterChkTargetMbxThisServerADL")]
		RouterCheckTargetMailboxOnThisServerLdapLatency,
		// Token: 0x04000462 RID: 1122
		[DisplayName("GPP", "ExeV1Impl")]
		ExecutedV1Implementation,
		// Token: 0x04000463 RID: 1123
		[DisplayName("GPP", "ExeV2Impl")]
		ExecutedV2Implementation,
		// Token: 0x04000464 RID: 1124
		[DisplayName("GPP", "Fallback")]
		FallbackToV1Implementation,
		// Token: 0x04000465 RID: 1125
		[DisplayName("GPP", "PrvT")]
		PrivateHandlerTotalTime,
		// Token: 0x04000466 RID: 1126
		[DisplayName("GPP", "PrvRpcC")]
		PrivateHandlerTotalStoreRpcCount,
		// Token: 0x04000467 RID: 1127
		[DisplayName("GPP", "PrvRpcL")]
		PrivateHandlerTotalStoreRpcLatency,
		// Token: 0x04000468 RID: 1128
		[DisplayName("GPP", "PrvRdPhT")]
		PrivateHandlerReadPhotoStreamTime,
		// Token: 0x04000469 RID: 1129
		[DisplayName("GPP", "PrvRdPhRpcC")]
		PrivateHandlerReadPhotoStreamStoreRpcCount,
		// Token: 0x0400046A RID: 1130
		[DisplayName("GPP", "PrvRdPhRpcL")]
		PrivateHandlerReadPhotoStreamStoreRpcLatency,
		// Token: 0x0400046B RID: 1131
		[DisplayName("GPP", "LocFrstPhSvcLocLocServerTime")]
		LocalForestPhotoServiceLocatorLocateServerTime,
		// Token: 0x0400046C RID: 1132
		[DisplayName("GPP", "LocFrstPhSvcLocLocServerADC")]
		LocalForestPhotoServiceLocatorLocateServerLdapCount,
		// Token: 0x0400046D RID: 1133
		[DisplayName("GPP", "LocFrstPhSvcLocLocServerADL")]
		LocalForestPhotoServiceLocatorLocateServerLdapLatency,
		// Token: 0x0400046E RID: 1134
		[DisplayName("GPP", "LocFrstPhSvcLocGetPhSvcUriTime")]
		LocalForestPhotoServiceLocatorGetPhotoServiceUriTime,
		// Token: 0x0400046F RID: 1135
		[DisplayName("GPP", "LocFrstPhSvcLocGetPhSvcUriADC")]
		LocalForestPhotoServiceLocatorGetPhotoServiceUriLdapCount,
		// Token: 0x04000470 RID: 1136
		[DisplayName("GPP", "LocFrstPhSvcLocGetPhSvcUriADL")]
		LocalForestPhotoServiceLocatorGetPhotoServiceUriLdapLatency,
		// Token: 0x04000471 RID: 1137
		[DisplayName("GPP", "WrgRtFallbackOtherSrv")]
		WrongRoutingDetectedThenFallbackToOtherServer,
		// Token: 0x04000472 RID: 1138
		[DisplayName("GPP", "PhRqHdler")]
		ServedByPhotoRequestHandler,
		// Token: 0x04000473 RID: 1139
		[DisplayName("GPP", "RFP")]
		RemoteForestHandlerProcessed,
		// Token: 0x04000474 RID: 1140
		RemoteForestHandlerServed,
		// Token: 0x04000475 RID: 1141
		[DisplayName("GPP", "RFT")]
		RemoteForestHandlerTotalTime,
		// Token: 0x04000476 RID: 1142
		[DisplayName("GPP", "RFADC")]
		RemoteForestHandlerTotalLdapCount,
		// Token: 0x04000477 RID: 1143
		[DisplayName("GPP", "RFADT")]
		RemoteForestHandlerTotalLdapLatency,
		// Token: 0x04000478 RID: 1144
		[DisplayName("GPP", "RFHErr")]
		RemoteForestHandlerError,
		// Token: 0x04000479 RID: 1145
		[DisplayName("GPP", "RCT")]
		ResponseContentType
	}
}
