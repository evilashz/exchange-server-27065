using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200000D RID: 13
	[Flags]
	internal enum OpenStoreFlag : long
	{
		// Token: 0x04000028 RID: 40
		None = 0L,
		// Token: 0x04000029 RID: 41
		UseAdminPrivilege = 1L,
		// Token: 0x0400002A RID: 42
		Public = 2L,
		// Token: 0x0400002B RID: 43
		HomeLogon = 4L,
		// Token: 0x0400002C RID: 44
		TakeOwnership = 8L,
		// Token: 0x0400002D RID: 45
		OverrideHomeMdb = 16L,
		// Token: 0x0400002E RID: 46
		Transport = 32L,
		// Token: 0x0400002F RID: 47
		RemoteTransport = 64L,
		// Token: 0x04000030 RID: 48
		InternetAnonymous = 128L,
		// Token: 0x04000031 RID: 49
		AlternateServer = 256L,
		// Token: 0x04000032 RID: 50
		IgnoreHomeMdb = 512L,
		// Token: 0x04000033 RID: 51
		NoMail = 1024L,
		// Token: 0x04000034 RID: 52
		OverrideLastModifier = 2048L,
		// Token: 0x04000035 RID: 53
		CallbackLogon = 4096L,
		// Token: 0x04000036 RID: 54
		Local = 8192L,
		// Token: 0x04000037 RID: 55
		FailIfNoMailbox = 16384L,
		// Token: 0x04000038 RID: 56
		CacheExchange = 32768L,
		// Token: 0x04000039 RID: 57
		CliWithNamedPropFix = 65536L,
		// Token: 0x0400003A RID: 58
		EnableLazyLogging = 131072L,
		// Token: 0x0400003B RID: 59
		CliWithReplidGuidMappingFix = 262144L,
		// Token: 0x0400003C RID: 60
		NoLocalization = 524288L,
		// Token: 0x0400003D RID: 61
		RestoreDatabase = 1048576L,
		// Token: 0x0400003E RID: 62
		XForestMove = 2097152L,
		// Token: 0x0400003F RID: 63
		MailboxGuid = 4194304L,
		// Token: 0x04000040 RID: 64
		CliWithPerMdbFix = 16777216L,
		// Token: 0x04000041 RID: 65
		DeliverNormalMessage = 33554432L,
		// Token: 0x04000042 RID: 66
		DeliverSpecialMessage = 67108864L,
		// Token: 0x04000043 RID: 67
		DeliverQuotaMessage = 134217728L,
		// Token: 0x04000044 RID: 68
		NoExtendedFlags = 268435456L,
		// Token: 0x04000045 RID: 69
		SupportsProgress = 536870912L,
		// Token: 0x04000046 RID: 70
		DisconnectedMailbox = 1073741824L,
		// Token: 0x04000047 RID: 71
		ApplicationIdOnly = 2147483648L,
		// Token: 0x04000048 RID: 72
		ShowAllFIDCs = 4294967296L,
		// Token: 0x04000049 RID: 73
		PublicFolderSubsystem = 8589934592L
	}
}
