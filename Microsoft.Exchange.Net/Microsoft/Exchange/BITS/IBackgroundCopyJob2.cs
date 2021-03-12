using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000656 RID: 1622
	[Guid("54B50739-686F-45EB-9DFF-D6A9A0FAA9AF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IBackgroundCopyJob2 : IBackgroundCopyJob
	{
		// Token: 0x06001D8F RID: 7567
		void AddFileSet(uint cFileCount, [MarshalAs(UnmanagedType.LPArray)] BG_FILE_INFO[] pFileSet);

		// Token: 0x06001D90 RID: 7568
		void AddFile([MarshalAs(UnmanagedType.LPWStr)] string RemoteUrl, [MarshalAs(UnmanagedType.LPWStr)] string LocalName);

		// Token: 0x06001D91 RID: 7569
		void EnumFiles([MarshalAs(UnmanagedType.Interface)] out IEnumBackgroundCopyFiles pEnum);

		// Token: 0x06001D92 RID: 7570
		void Suspend();

		// Token: 0x06001D93 RID: 7571
		void Resume();

		// Token: 0x06001D94 RID: 7572
		void Cancel();

		// Token: 0x06001D95 RID: 7573
		void Complete();

		// Token: 0x06001D96 RID: 7574
		void GetId(out Guid pVal);

		// Token: 0x06001D97 RID: 7575
		void GetType(out BG_JOB_TYPE pVal);

		// Token: 0x06001D98 RID: 7576
		void GetProgress(out _BG_JOB_PROGRESS pVal);

		// Token: 0x06001D99 RID: 7577
		void GetTimes(out _BG_JOB_TIMES pVal);

		// Token: 0x06001D9A RID: 7578
		void GetState(out BG_JOB_STATE pVal);

		// Token: 0x06001D9B RID: 7579
		void GetError([MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyError ppError);

		// Token: 0x06001D9C RID: 7580
		void GetOwner([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

		// Token: 0x06001D9D RID: 7581
		void SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string Val);

		// Token: 0x06001D9E RID: 7582
		void GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

		// Token: 0x06001D9F RID: 7583
		void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string Val);

		// Token: 0x06001DA0 RID: 7584
		void GetDescription([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

		// Token: 0x06001DA1 RID: 7585
		void SetPriority(BG_JOB_PRIORITY Val);

		// Token: 0x06001DA2 RID: 7586
		void GetPriority(out BG_JOB_PRIORITY pVal);

		// Token: 0x06001DA3 RID: 7587
		void SetNotifyFlags([MarshalAs(UnmanagedType.U4)] BG_JOB_NOTIFICATION_TYPE Val);

		// Token: 0x06001DA4 RID: 7588
		void GetNotifyFlags(out uint pVal);

		// Token: 0x06001DA5 RID: 7589
		void SetNotifyInterface([MarshalAs(UnmanagedType.IUnknown)] object Val);

		// Token: 0x06001DA6 RID: 7590
		void GetNotifyInterface([MarshalAs(UnmanagedType.IUnknown)] out object pVal);

		// Token: 0x06001DA7 RID: 7591
		void SetMinimumRetryDelay(uint Seconds);

		// Token: 0x06001DA8 RID: 7592
		void GetMinimumRetryDelay(out uint Seconds);

		// Token: 0x06001DA9 RID: 7593
		void SetNoProgressTimeout(uint Seconds);

		// Token: 0x06001DAA RID: 7594
		void GetNoProgressTimeout(out uint Seconds);

		// Token: 0x06001DAB RID: 7595
		void GetErrorCount(out uint Errors);

		// Token: 0x06001DAC RID: 7596
		void SetProxySettings(BG_JOB_PROXY_USAGE ProxyUsage, [MarshalAs(UnmanagedType.LPWStr)] string ProxyList, [MarshalAs(UnmanagedType.LPWStr)] string ProxyBypassList);

		// Token: 0x06001DAD RID: 7597
		void GetProxySettings(out BG_JOB_PROXY_USAGE pProxyUsage, [MarshalAs(UnmanagedType.LPWStr)] out string pProxyList, [MarshalAs(UnmanagedType.LPWStr)] out string pProxyBypassList);

		// Token: 0x06001DAE RID: 7598
		void TakeOwnership();

		// Token: 0x06001DAF RID: 7599
		void SetNotifyCmdLine([MarshalAs(UnmanagedType.LPWStr)] [In] string Program, [MarshalAs(UnmanagedType.LPWStr)] [In] string Parameters);

		// Token: 0x06001DB0 RID: 7600
		void GetNotifyCmdLine([MarshalAs(UnmanagedType.LPWStr)] out string pProgram, [MarshalAs(UnmanagedType.LPWStr)] out string pParameters);

		// Token: 0x06001DB1 RID: 7601
		void GetReplyProgress(out _BG_JOB_REPLY_PROGRESS pProgress);

		// Token: 0x06001DB2 RID: 7602
		void GetReplyData([In] [Out] IntPtr ppBuffer, out ulong pLength);

		// Token: 0x06001DB3 RID: 7603
		void SetReplyFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string ReplyFileName);

		// Token: 0x06001DB4 RID: 7604
		void GetReplyFileName([MarshalAs(UnmanagedType.LPWStr)] out string pReplyFileName);

		// Token: 0x06001DB5 RID: 7605
		void SetCredentials([In] ref BG_AUTH_CREDENTIALS Credentials);

		// Token: 0x06001DB6 RID: 7606
		void RemoveCredentials(BG_AUTH_TARGET Target, BG_AUTH_SCHEME Scheme);
	}
}
