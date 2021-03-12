using System;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200000A RID: 10
	public enum DBUTIL_OP
	{
		// Token: 0x04000029 RID: 41
		Consistency,
		// Token: 0x0400002A RID: 42
		DumpData,
		// Token: 0x0400002B RID: 43
		DumpMetaData,
		// Token: 0x0400002C RID: 44
		DumpPage,
		// Token: 0x0400002D RID: 45
		DumpNode,
		// Token: 0x0400002E RID: 46
		DumpSpace,
		// Token: 0x0400002F RID: 47
		SetHeaderState,
		// Token: 0x04000030 RID: 48
		DumpHeader,
		// Token: 0x04000031 RID: 49
		DumpLogfile,
		// Token: 0x04000032 RID: 50
		DumpLogfileTrackNode,
		// Token: 0x04000033 RID: 51
		DumpCheckpoint,
		// Token: 0x04000034 RID: 52
		EDBDump,
		// Token: 0x04000035 RID: 53
		EDBRepair,
		// Token: 0x04000036 RID: 54
		Munge,
		// Token: 0x04000037 RID: 55
		EDBScrub,
		// Token: 0x04000038 RID: 56
		[Obsolete]
		SLVMove,
		// Token: 0x04000039 RID: 57
		DBConvertRecords,
		// Token: 0x0400003A RID: 58
		DBDefragment,
		// Token: 0x0400003B RID: 59
		[Obsolete]
		DumpExchangeSLVInfo,
		// Token: 0x0400003C RID: 60
		DumpUnicodeFixupTable_ObsoleteAndUnused,
		// Token: 0x0400003D RID: 61
		DumpPageUsage,
		// Token: 0x0400003E RID: 62
		UpdateDBHeaderFromTrailer,
		// Token: 0x0400003F RID: 63
		ChecksumLogFromMemory
	}
}
