using System;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x02000108 RID: 264
	internal static class LoggerSettings
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001EA5F File Offset: 0x0001CC5F
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x0001EA66 File Offset: 0x0001CC66
		internal static bool IsInitialized { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001EA6E File Offset: 0x0001CC6E
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x0001EA75 File Offset: 0x0001CC75
		internal static bool LogEnabled { get; set; } = true;

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001EA7D File Offset: 0x0001CC7D
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0001EA84 File Offset: 0x0001CC84
		internal static bool IsPowerShellWebService { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001EA8C File Offset: 0x0001CC8C
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0001EA93 File Offset: 0x0001CC93
		internal static bool IsRemotePS { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001EA9B File Offset: 0x0001CC9B
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0001EAA2 File Offset: 0x0001CCA2
		internal static int ThresholdToLogActivityLatency { get; set; } = 1000;

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0001EAAA File Offset: 0x0001CCAA
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0001EAB1 File Offset: 0x0001CCB1
		internal static string LogSubFolderName { get; set; } = "Others";

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0001EAB9 File Offset: 0x0001CCB9
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		internal static TimeSpan LogFileAgeInDays { get; set; } = TimeSpan.FromDays(30.0);

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0001EACF File Offset: 0x0001CCCF
		internal static int MaxLogDirectorySizeInGB { get; set; } = 1;

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001EAD7 File Offset: 0x0001CCD7
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0001EADE File Offset: 0x0001CCDE
		internal static int MaxLogFileSizeInMB { get; set; } = 10;

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0001EAE6 File Offset: 0x0001CCE6
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0001EAED File Offset: 0x0001CCED
		internal static ExEventLog EventLog { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001EAF5 File Offset: 0x0001CCF5
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001EAFC File Offset: 0x0001CCFC
		internal static ExEventLog.EventTuple EventTuple { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0001EB04 File Offset: 0x0001CD04
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0001EB0B File Offset: 0x0001CD0B
		internal static string ProcessName { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001EB13 File Offset: 0x0001CD13
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0001EB1A File Offset: 0x0001CD1A
		internal static int ProcessId { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0001EB22 File Offset: 0x0001CD22
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0001EB29 File Offset: 0x0001CD29
		internal static int? MaxAppendableColumnLength { get; set; } = new int?(16384);

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001EB31 File Offset: 0x0001CD31
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0001EB38 File Offset: 0x0001CD38
		internal static int? ErrorMessageLengthThreshold { get; set; } = new int?(250);

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001EB40 File Offset: 0x0001CD40
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001EB47 File Offset: 0x0001CD47
		internal static bool ProcessExceptionMessage { get; set; } = true;

		// Token: 0x040004D1 RID: 1233
		internal const string DefaultLogSubFolderName = "Others";

		// Token: 0x040004D2 RID: 1234
		internal const string CustomLogFolderPathAppSettingsKey = "ConfigurationCoreLogger.LogFolder";
	}
}
