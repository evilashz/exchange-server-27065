using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Exchange.Sqm
{
	// Token: 0x02000004 RID: 4
	internal static class SqmLibWrap
	{
		// Token: 0x06000001 RID: 1
		[DllImport("sqmapi.dll")]
		public static extern void SqmAddToAverage(uint hSession, uint dwId, uint dwVal);

		// Token: 0x06000002 RID: 2
		[DllImport("sqmapi.dll")]
		public static extern bool SqmAddToStreamDWord(uint hSession, uint dwId, uint cTuple, uint dwVal);

		// Token: 0x06000003 RID: 3
		[DllImport("sqmapi.dll")]
		public static extern bool SqmAddToStreamString(uint hSession, uint dwId, uint cTuple, [MarshalAs(UnmanagedType.LPWStr)] string pwszVal);

		// Token: 0x06000004 RID: 4
		[DllImport("sqmapi.dll")]
		public static extern void SqmClearFlags(uint hSession, ref uint dwFlags);

		// Token: 0x06000005 RID: 5
		[DllImport("sqmapi.dll")]
		public static extern bool SqmCreateNewId(out Guid pGuid);

		// Token: 0x06000006 RID: 6
		[DllImport("sqmapi.dll")]
		public static extern void SqmEndSession(uint hSession, [MarshalAs(UnmanagedType.LPWStr)] string pszPattern, uint dwMaxFilesToQueue, uint dwFlags);

		// Token: 0x06000007 RID: 7
		[DllImport("sqmapi.dll")]
		public static extern bool SqmGetEnabled(uint hSession);

		// Token: 0x06000008 RID: 8
		[DllImport("sqmapi.dll")]
		public static extern void SqmGetFlags(uint hSession, out uint dwFlags);

		// Token: 0x06000009 RID: 9
		[DllImport("sqmapi.dll")]
		public static extern void SqmGetMachineId(uint hSession, out Guid guid);

		// Token: 0x0600000A RID: 10
		[DllImport("sqmapi.dll")]
		public static extern uint SqmGetSession([MarshalAs(UnmanagedType.LPWStr)] string pszSessionIdentifier, uint cbMaxSessionSize, uint dwFlags);

		// Token: 0x0600000B RID: 11
		[DllImport("sqmapi.dll")]
		public static extern System.Runtime.InteropServices.ComTypes.FILETIME SqmGetSessionStartTime(uint hSession);

		// Token: 0x0600000C RID: 12
		[DllImport("sqmapi.dll")]
		public static extern void SqmGetUserId(uint hSession, out Guid guid);

		// Token: 0x0600000D RID: 13
		[DllImport("sqmapi.dll")]
		public static extern void SqmIncrement(uint hSession, uint dwId, uint dwInc);

		// Token: 0x0600000E RID: 14
		[DllImport("sqmapi.dll")]
		public static extern bool SqmIsWindowsOptedIn();

		// Token: 0x0600000F RID: 15
		[DllImport("sqmapi.dll")]
		public static extern bool SqmReadSharedMachineId(out Guid pGuid);

		// Token: 0x06000010 RID: 16
		[DllImport("sqmapi.dll")]
		public static extern bool SqmReadSharedUserId(out Guid guid);

		// Token: 0x06000011 RID: 17
		[DllImport("sqmapi.dll")]
		public static extern void SqmSet(uint hSession, uint dwId, uint dwVal);

		// Token: 0x06000012 RID: 18
		[DllImport("sqmapi.dll")]
		public static extern bool SqmSetAppId(uint hSession, uint dwAppId);

		// Token: 0x06000013 RID: 19
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetAppVersion(uint hSession, uint dwVersionHigh, uint dwVersionLow);

		// Token: 0x06000014 RID: 20
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetBits(uint hSession, uint dwId, uint dwOrBits);

		// Token: 0x06000015 RID: 21
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetBool(uint hSession, uint dwId, uint dwVal);

		// Token: 0x06000016 RID: 22
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetCurrentTimeAsUploadTime([MarshalAs(UnmanagedType.LPWStr)] string pszSqmFileName);

		// Token: 0x06000017 RID: 23
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetEnabled(uint hSession, bool fEnabled);

		// Token: 0x06000018 RID: 24
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetFlags(uint hSession, uint dwFlags);

		// Token: 0x06000019 RID: 25
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetIfMax(uint hSession, uint dwId, uint dwOrBits);

		// Token: 0x0600001A RID: 26
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetIfMin(uint hSession, uint dwId, uint dwOrBits);

		// Token: 0x0600001B RID: 27
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetMachineId(uint hSession, ref Guid pGuid);

		// Token: 0x0600001C RID: 28
		[DllImport("sqmapi.dll")]
		public static extern bool SqmSetString(uint hSession, uint dwId, [MarshalAs(UnmanagedType.LPWStr)] string pwszVal);

		// Token: 0x0600001D RID: 29
		[DllImport("sqmapi.dll")]
		public static extern void SqmSetUserId(uint hSession, ref Guid pGuid);

		// Token: 0x0600001E RID: 30
		[DllImport("sqmapi.dll")]
		public static extern void SqmStartSession(uint hSession);

		// Token: 0x0600001F RID: 31
		[DllImport("sqmapi.dll", SetLastError = true)]
		public static extern uint SqmStartUpload([MarshalAs(UnmanagedType.LPWStr)] string szPattern, [MarshalAs(UnmanagedType.LPWStr)] string szUrl, [MarshalAs(UnmanagedType.LPWStr)] string szSecureUrl, uint dwFlags, SqmLibWrap.SqmUploadCallback pfnCallback);

		// Token: 0x06000020 RID: 32
		[DllImport("sqmapi.dll")]
		public static extern void SqmTimerAccumulate(uint hSession, uint dwId);

		// Token: 0x06000021 RID: 33
		[DllImport("sqmapi.dll")]
		public static extern void SqmTimerAddToAverage(uint hSession, uint dwId);

		// Token: 0x06000022 RID: 34
		[DllImport("sqmapi.dll")]
		public static extern void SqmTimerRecord(uint hSession, uint dwId);

		// Token: 0x06000023 RID: 35
		[DllImport("sqmapi.dll")]
		public static extern void SqmTimerStart(uint hSession, uint dwId);

		// Token: 0x06000024 RID: 36
		[DllImport("sqmapi.dll")]
		public static extern bool SqmWaitForUploadComplete(uint dwTimeoutMilliseconds, uint dwFlags);

		// Token: 0x06000025 RID: 37
		[DllImport("sqmapi.dll")]
		public static extern bool SqmWriteSharedMachineId(ref Guid pGuid);

		// Token: 0x06000026 RID: 38
		[DllImport("sqmapi.dll")]
		public static extern bool SqmWriteSharedUserId(ref Guid pGuid);

		// Token: 0x0400018A RID: 394
		public const uint SqmSessionCreateNew = 1U;

		// Token: 0x0400018B RID: 395
		public const uint SqmWaitForUploadCurrentFileInQueue = 1U;

		// Token: 0x0400018C RID: 396
		public const uint SqmWaitForUploadAllFilesInQueue = 2U;

		// Token: 0x0400018D RID: 397
		public const uint SqmUploadAllFiles = 2U;

		// Token: 0x0400018E RID: 398
		public const uint SqmIgnoreWindowsOptin = 4U;

		// Token: 0x0400018F RID: 399
		public const uint SqmOverwriteOldestFile = 2U;

		// Token: 0x04000190 RID: 400
		public const uint SqmReleaseSession = 8U;

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x06000028 RID: 40
		public delegate bool SqmUploadCallback(uint hr, [MarshalAs(UnmanagedType.LPWStr)] string filePath, uint dwHttpResponse);
	}
}
