using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002F2 RID: 754
	[Flags]
	internal enum OpenFlags : uint
	{
		// Token: 0x0400095D RID: 2397
		None = 0U,
		// Token: 0x0400095E RID: 2398
		UseAdminPrivilege = 1U,
		// Token: 0x0400095F RID: 2399
		Public = 2U,
		// Token: 0x04000960 RID: 2400
		HomeLogon = 4U,
		// Token: 0x04000961 RID: 2401
		TakeOwnership = 8U,
		// Token: 0x04000962 RID: 2402
		OverrideHomeMdb = 16U,
		// Token: 0x04000963 RID: 2403
		Transport = 32U,
		// Token: 0x04000964 RID: 2404
		RemoteTransport = 64U,
		// Token: 0x04000965 RID: 2405
		InternetAnonymous = 128U,
		// Token: 0x04000966 RID: 2406
		AlternateServer = 256U,
		// Token: 0x04000967 RID: 2407
		IgnoreHomeMdb = 512U,
		// Token: 0x04000968 RID: 2408
		NoMail = 1024U,
		// Token: 0x04000969 RID: 2409
		OverrideLastModifier = 2048U,
		// Token: 0x0400096A RID: 2410
		CallbackLogon = 4096U,
		// Token: 0x0400096B RID: 2411
		Local = 8192U,
		// Token: 0x0400096C RID: 2412
		FailIfNoMailbox = 16384U,
		// Token: 0x0400096D RID: 2413
		CacheExchange = 32768U,
		// Token: 0x0400096E RID: 2414
		CliWithNamedPropFix = 65536U,
		// Token: 0x0400096F RID: 2415
		EnableLazyLogging = 131072U,
		// Token: 0x04000970 RID: 2416
		CliWithReplidGuidMappingFix = 262144U,
		// Token: 0x04000971 RID: 2417
		NoLocalization = 524288U,
		// Token: 0x04000972 RID: 2418
		RestoreDatabase = 1048576U,
		// Token: 0x04000973 RID: 2419
		XForestMove = 2097152U,
		// Token: 0x04000974 RID: 2420
		MailboxGuid = 4194304U,
		// Token: 0x04000975 RID: 2421
		CliWithPerMdbFix = 16777216U,
		// Token: 0x04000976 RID: 2422
		DeliverNormalMessage = 33554432U,
		// Token: 0x04000977 RID: 2423
		DeliverSpecialMessage = 67108864U,
		// Token: 0x04000978 RID: 2424
		DeliverQuotaMessage = 134217728U,
		// Token: 0x04000979 RID: 2425
		SupportProgress = 536870912U
	}
}
