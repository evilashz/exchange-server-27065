using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Common.HA
{
	// Token: 0x02000039 RID: 57
	internal static class ExDbFailureItemApi
	{
		// Token: 0x0600010E RID: 270
		[DllImport("ExDbFailureItemApi.dll", EntryPoint = "HaPublishDbFailureItem")]
		internal static extern int PublishFailureItem([In] ref ExDbFailureItemApi.HaDbFailureItem failureItem);

		// Token: 0x0600010F RID: 271
		[DllImport("ExDbFailureItemApi.dll", EntryPoint = "HaPublishDbFailureItemEx")]
		internal static extern int PublishFailureItemEx([In] bool isDebug, [In] ref ExDbFailureItemApi.HaDbFailureItem failureItem);

		// Token: 0x0400013D RID: 317
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x0200003A RID: 58
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct HaDbIoErrorInfo
		{
			// Token: 0x0400013E RID: 318
			internal int CbSize;

			// Token: 0x0400013F RID: 319
			internal IoErrorCategory Category;

			// Token: 0x04000140 RID: 320
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string FileName;

			// Token: 0x04000141 RID: 321
			internal long Offset;

			// Token: 0x04000142 RID: 322
			internal long Size;
		}

		// Token: 0x0200003B RID: 59
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct HaDbNotificationEventInfo
		{
			// Token: 0x04000143 RID: 323
			internal int CbSize;

			// Token: 0x04000144 RID: 324
			internal int EventId;

			// Token: 0x04000145 RID: 325
			internal int NumParameters;

			// Token: 0x04000146 RID: 326
			internal IntPtr Parameters;
		}

		// Token: 0x0200003C RID: 60
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct HaDbFailureItem
		{
			// Token: 0x04000147 RID: 327
			internal int CbSize;

			// Token: 0x04000148 RID: 328
			internal FailureNameSpace NameSpace;

			// Token: 0x04000149 RID: 329
			internal FailureTag Tag;

			// Token: 0x0400014A RID: 330
			internal Guid Guid;

			// Token: 0x0400014B RID: 331
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string InstanceName;

			// Token: 0x0400014C RID: 332
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string ComponentName;

			// Token: 0x0400014D RID: 333
			internal IntPtr IoError;

			// Token: 0x0400014E RID: 334
			internal IntPtr NotificationEventInfo;

			// Token: 0x0400014F RID: 335
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string Message;
		}
	}
}
