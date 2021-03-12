using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002AB RID: 683
	public enum JET_param
	{
		// Token: 0x040007AA RID: 1962
		SystemPath,
		// Token: 0x040007AB RID: 1963
		TempPath,
		// Token: 0x040007AC RID: 1964
		LogFilePath,
		// Token: 0x040007AD RID: 1965
		BaseName,
		// Token: 0x040007AE RID: 1966
		EventSource,
		// Token: 0x040007AF RID: 1967
		MaxSessions,
		// Token: 0x040007B0 RID: 1968
		MaxOpenTables,
		// Token: 0x040007B1 RID: 1969
		MaxCursors = 8,
		// Token: 0x040007B2 RID: 1970
		MaxVerPages,
		// Token: 0x040007B3 RID: 1971
		MaxTemporaryTables,
		// Token: 0x040007B4 RID: 1972
		LogFileSize,
		// Token: 0x040007B5 RID: 1973
		LogBuffers,
		// Token: 0x040007B6 RID: 1974
		CircularLog = 17,
		// Token: 0x040007B7 RID: 1975
		DbExtensionSize,
		// Token: 0x040007B8 RID: 1976
		PageTempDBMin,
		// Token: 0x040007B9 RID: 1977
		CacheSizeMax = 23,
		// Token: 0x040007BA RID: 1978
		CheckpointDepthMax,
		// Token: 0x040007BB RID: 1979
		LrukCorrInterval,
		// Token: 0x040007BC RID: 1980
		LrukTimeout = 28,
		// Token: 0x040007BD RID: 1981
		OutstandingIOMax = 30,
		// Token: 0x040007BE RID: 1982
		StartFlushThreshold,
		// Token: 0x040007BF RID: 1983
		StopFlushThreshold,
		// Token: 0x040007C0 RID: 1984
		Recovery = 34,
		// Token: 0x040007C1 RID: 1985
		EnableOnlineDefrag,
		// Token: 0x040007C2 RID: 1986
		CacheSize = 41,
		// Token: 0x040007C3 RID: 1987
		EnableIndexChecking = 45,
		// Token: 0x040007C4 RID: 1988
		EventSourceKey = 49,
		// Token: 0x040007C5 RID: 1989
		NoInformationEvent,
		// Token: 0x040007C6 RID: 1990
		EventLoggingLevel,
		// Token: 0x040007C7 RID: 1991
		DeleteOutOfRangeLogs,
		// Token: 0x040007C8 RID: 1992
		EnableIndexCleanup = 54,
		// Token: 0x040007C9 RID: 1993
		CacheSizeMin = 60,
		// Token: 0x040007CA RID: 1994
		PreferredVerPages = 63,
		// Token: 0x040007CB RID: 1995
		DatabasePageSize,
		// Token: 0x040007CC RID: 1996
		ErrorToString = 70,
		// Token: 0x040007CD RID: 1997
		RuntimeCallback = 73,
		// Token: 0x040007CE RID: 1998
		CleanupMismatchedLogFiles = 77,
		// Token: 0x040007CF RID: 1999
		ExceptionAction = 98,
		// Token: 0x040007D0 RID: 2000
		CreatePathIfNotExist = 100,
		// Token: 0x040007D1 RID: 2001
		OneDatabasePerSession = 102,
		// Token: 0x040007D2 RID: 2002
		MaxInstances = 104,
		// Token: 0x040007D3 RID: 2003
		VersionStoreTaskQueueMax
	}
}
