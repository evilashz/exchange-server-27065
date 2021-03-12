using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000033 RID: 51
	internal enum CodeLid
	{
		// Token: 0x04000350 RID: 848
		None,
		// Token: 0x04000351 RID: 849
		RemoteEventsBeg = 1494,
		// Token: 0x04000352 RID: 850
		RemoteRequestId = 2006,
		// Token: 0x04000353 RID: 851
		RemoteCtxOverflow = 1238,
		// Token: 0x04000354 RID: 852
		RemoteEventsEnd = 1750,
		// Token: 0x04000355 RID: 853
		EcDoConnect_RpcCalled = 21017,
		// Token: 0x04000356 RID: 854
		EcDoConnect_RpcReturned = 29209,
		// Token: 0x04000357 RID: 855
		EcDoConnect_RpcException = 19961,
		// Token: 0x04000358 RID: 856
		EcDoConnectEx_RpcCalled = 23065,
		// Token: 0x04000359 RID: 857
		EcDoConnectEx_RpcReturned = 17913,
		// Token: 0x0400035A RID: 858
		EcDoConnectEx_RpcException = 28153,
		// Token: 0x0400035B RID: 859
		EcDoRpc_RpcCalled = 31257,
		// Token: 0x0400035C RID: 860
		EcDoRpc_RpcReturned = 16921,
		// Token: 0x0400035D RID: 861
		EcDoRpc_RpcException = 24057,
		// Token: 0x0400035E RID: 862
		EcDoRpcExt2_RpcCalled = 18969,
		// Token: 0x0400035F RID: 863
		EcDoRpcExt2_RpcReturned = 27161,
		// Token: 0x04000360 RID: 864
		EcDoRpcExt2_RpcException = 32249,
		// Token: 0x04000361 RID: 865
		EMSMDB_EcDoDisconnect_RpcCalled = 38951,
		// Token: 0x04000362 RID: 866
		EMSMDB_EcDoDisconnect_RpcReturned = 55335,
		// Token: 0x04000363 RID: 867
		EMSMDB_EcDoDisconnect_RpcException = 43047,
		// Token: 0x04000364 RID: 868
		EMSMDB_EcDoConnectEx_RpcCalled = 59431,
		// Token: 0x04000365 RID: 869
		EMSMDB_EcDoConnectEx_RpcReturned = 34855,
		// Token: 0x04000366 RID: 870
		EMSMDB_EcDoConnectEx_RpcException = 51239,
		// Token: 0x04000367 RID: 871
		EMSMDB_EcDoRpcExt2_RpcCalled = 45095,
		// Token: 0x04000368 RID: 872
		EMSMDB_EcDoRpcExt2_RpcReturned = 61479,
		// Token: 0x04000369 RID: 873
		EMSMDB_EcDoRpcExt2_RpcException = 36903,
		// Token: 0x0400036A RID: 874
		EMSMDB_EcDoAsyncConnectEx_RpcCalled = 35879,
		// Token: 0x0400036B RID: 875
		EMSMDB_EcDoAsyncConnectEx_RpcReturned = 52263,
		// Token: 0x0400036C RID: 876
		EMSMDB_EcDoAsyncConnectEx_RpcException = 46119,
		// Token: 0x0400036D RID: 877
		EMSMDB_EcDoAsyncWaitEx_RpcCalled = 62503,
		// Token: 0x0400036E RID: 878
		EMSMDB_EcDoAsyncWaitEx_RpcReturned = 37927,
		// Token: 0x0400036F RID: 879
		EMSMDB_EcDoAsyncWaitEx_RpcException = 54311,
		// Token: 0x04000370 RID: 880
		EMSMDBMT_EcDoDisconnect_RpcCalled = 40999,
		// Token: 0x04000371 RID: 881
		EMSMDBMT_EcDoDisconnect_RpcReturned = 57383,
		// Token: 0x04000372 RID: 882
		EMSMDBMT_EcDoDisconnect_RpcException = 32807,
		// Token: 0x04000373 RID: 883
		EMSMDBMT_EcDoConnectEx_RpcCalled = 49191,
		// Token: 0x04000374 RID: 884
		EMSMDBMT_EcDoConnectEx_RpcReturned = 48679,
		// Token: 0x04000375 RID: 885
		EMSMDBMT_EcDoConnectEx_RpcException = 65063,
		// Token: 0x04000376 RID: 886
		EMSMDBMT_EcDoRpcExt2_RpcCalled = 40487,
		// Token: 0x04000377 RID: 887
		EMSMDBMT_EcDoRpcExt2_RpcReturned = 56871,
		// Token: 0x04000378 RID: 888
		EMSMDBMT_EcDoRpcExt2_RpcException = 44583,
		// Token: 0x04000379 RID: 889
		EMSMDBMT_EcDoAsyncConnectEx_RpcCalled = 42023,
		// Token: 0x0400037A RID: 890
		EMSMDBMT_EcDoAsyncConnectEx_RpcReturned = 58407,
		// Token: 0x0400037B RID: 891
		EMSMDBMT_EcDoAsyncConnectEx_RpcException = 33831,
		// Token: 0x0400037C RID: 892
		EMSMDBMT_EcDoAsyncWaitEx_RpcCalled = 50215,
		// Token: 0x0400037D RID: 893
		EMSMDBMT_EcDoAsyncWaitEx_RpcReturned = 47143,
		// Token: 0x0400037E RID: 894
		EMSMDBMT_EcDoAsyncWaitEx_RpcException = 63527,
		// Token: 0x0400037F RID: 895
		EMSMDBPOOL_EcPoolWaitForNotificationsAsync_RpcCalled = 36391,
		// Token: 0x04000380 RID: 896
		EMSMDBPOOL_EcPoolWaitForNotificationsAsync_RpcReturned = 52775,
		// Token: 0x04000381 RID: 897
		EMSMDBPOOL_EcPoolWaitForNotificationsAsync_RpcException = 46631,
		// Token: 0x04000382 RID: 898
		EMSMDBPOOL_EcPoolConnect_RpcCalled = 38439,
		// Token: 0x04000383 RID: 899
		EMSMDBPOOL_EcPoolConnect_RpcReturned = 54823,
		// Token: 0x04000384 RID: 900
		EMSMDBPOOL_EcPoolConnect_RpcException = 42535,
		// Token: 0x04000385 RID: 901
		EMSMDBPOOL_EcPoolCloseSession_RpcCalled = 58919,
		// Token: 0x04000386 RID: 902
		EMSMDBPOOL_EcPoolCloseSession_RpcReturned = 34343,
		// Token: 0x04000387 RID: 903
		EMSMDBPOOL_EcPoolCloseSession_RpcException = 50727,
		// Token: 0x04000388 RID: 904
		EMSMDBPOOL_EcPoolCreateSession_RpcCalled = 47655,
		// Token: 0x04000389 RID: 905
		EMSMDBPOOL_EcPoolCreateSession_RpcReturned = 64039,
		// Token: 0x0400038A RID: 906
		EMSMDBPOOL_EcPoolCreateSession_RpcException = 39463,
		// Token: 0x0400038B RID: 907
		EMSMDBPOOL_EcPoolSessionDoRpc_RpcCalled = 55847,
		// Token: 0x0400038C RID: 908
		EMSMDBPOOL_EcPoolSessionDoRpc_RpcReturned = 43559,
		// Token: 0x0400038D RID: 909
		EMSMDBPOOL_EcPoolSessionDoRpc_RpcException = 59943,
		// Token: 0x0400038E RID: 910
		ResponseParseStart = 23226,
		// Token: 0x0400038F RID: 911
		ResponseRop = 27962,
		// Token: 0x04000390 RID: 912
		ResponseRopError = 17082,
		// Token: 0x04000391 RID: 913
		ResponseParseDone = 31418,
		// Token: 0x04000392 RID: 914
		ResponseParseFailure = 21817,
		// Token: 0x04000393 RID: 915
		RequestRop = 26426,
		// Token: 0x04000394 RID: 916
		EcPoolEnter_DeadPool = 39793,
		// Token: 0x04000395 RID: 917
		EcCreatePool_DeadPool = 50551,
		// Token: 0x04000396 RID: 918
		EcConnectToServerPool_DeadPool = 60017,
		// Token: 0x04000397 RID: 919
		EEInfo_EnumerationFailure = 14232,
		// Token: 0x04000398 RID: 920
		EEInfo_NextRecordFailure = 12184,
		// Token: 0x04000399 RID: 921
		EEInfo_ComputerName = 16280,
		// Token: 0x0400039A RID: 922
		EEInfo_PID = 8600,
		// Token: 0x0400039B RID: 923
		EEInfo_GenerationTime = 12696,
		// Token: 0x0400039C RID: 924
		EEInfo_GeneratingComponent = 10648,
		// Token: 0x0400039D RID: 925
		EEInfo_Status = 14744,
		// Token: 0x0400039E RID: 926
		EEInfo_DetectionLocation = 9624,
		// Token: 0x0400039F RID: 927
		EEInfo_Flags = 13720,
		// Token: 0x040003A0 RID: 928
		EEInfo_NumberOfParameters = 11672,
		// Token: 0x040003A1 RID: 929
		EEInfo_Parameter_Ansi = 15768,
		// Token: 0x040003A2 RID: 930
		EEInfo_Parameter_Unicode = 8856,
		// Token: 0x040003A3 RID: 931
		EEInfo_Parameter_Long = 12952,
		// Token: 0x040003A4 RID: 932
		EEInfo_Parameter_Short = 10904,
		// Token: 0x040003A5 RID: 933
		EEInfo_Parameter_Pointer = 15000,
		// Token: 0x040003A6 RID: 934
		EEInfo_Parameter_Truncated = 9880,
		// Token: 0x040003A7 RID: 935
		EEInfo_Parameter_Binary = 13976,
		// Token: 0x040003A8 RID: 936
		EEInfo_Parameter_Unknown = 11928,
		// Token: 0x040003A9 RID: 937
		ServerVersion_EcDoConnectEx = 51056,
		// Token: 0x040003AA RID: 938
		ClientVersion_EcDoConnectEx = 50544,
		// Token: 0x040003AB RID: 939
		ServerVersion_EcDoRpcExt2 = 50032,
		// Token: 0x040003AC RID: 940
		ClientVersion_EcDoRpcExt2 = 52176
	}
}
