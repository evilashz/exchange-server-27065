using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000655 RID: 1621
	[Guid("37668D37-507E-4160-9316-26306D150B12")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IBackgroundCopyJob
	{
		// Token: 0x06001D6F RID: 7535
		void AddFileSet(uint cFileCount, [MarshalAs(UnmanagedType.LPArray)] BG_FILE_INFO[] pFileSet);

		// Token: 0x06001D70 RID: 7536
		void AddFile([MarshalAs(UnmanagedType.LPWStr)] string RemoteUrl, [MarshalAs(UnmanagedType.LPWStr)] string LocalName);

		// Token: 0x06001D71 RID: 7537
		void EnumFiles([MarshalAs(UnmanagedType.Interface)] out IEnumBackgroundCopyFiles pEnum);

		// Token: 0x06001D72 RID: 7538
		void Suspend();

		// Token: 0x06001D73 RID: 7539
		void Resume();

		// Token: 0x06001D74 RID: 7540
		void Cancel();

		// Token: 0x06001D75 RID: 7541
		void Complete();

		// Token: 0x06001D76 RID: 7542
		void GetId(out Guid pVal);

		// Token: 0x06001D77 RID: 7543
		void GetType(out BG_JOB_TYPE pVal);

		// Token: 0x06001D78 RID: 7544
		void GetProgress(out _BG_JOB_PROGRESS pVal);

		// Token: 0x06001D79 RID: 7545
		void GetTimes(out _BG_JOB_TIMES pVal);

		// Token: 0x06001D7A RID: 7546
		void GetState(out BG_JOB_STATE pVal);

		// Token: 0x06001D7B RID: 7547
		void GetError([MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyError ppError);

		// Token: 0x06001D7C RID: 7548
		void GetOwner([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

		// Token: 0x06001D7D RID: 7549
		void SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string Val);

		// Token: 0x06001D7E RID: 7550
		void GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

		// Token: 0x06001D7F RID: 7551
		void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string Val);

		// Token: 0x06001D80 RID: 7552
		void GetDescription([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

		// Token: 0x06001D81 RID: 7553
		void SetPriority(BG_JOB_PRIORITY Val);

		// Token: 0x06001D82 RID: 7554
		void GetPriority(out BG_JOB_PRIORITY pVal);

		// Token: 0x06001D83 RID: 7555
		void SetNotifyFlags(uint Val);

		// Token: 0x06001D84 RID: 7556
		void GetNotifyFlags(out uint pVal);

		// Token: 0x06001D85 RID: 7557
		void SetNotifyInterface([MarshalAs(UnmanagedType.IUnknown)] object Val);

		// Token: 0x06001D86 RID: 7558
		void GetNotifyInterface([MarshalAs(UnmanagedType.IUnknown)] out object pVal);

		// Token: 0x06001D87 RID: 7559
		void SetMinimumRetryDelay(uint Seconds);

		// Token: 0x06001D88 RID: 7560
		void GetMinimumRetryDelay(out uint Seconds);

		// Token: 0x06001D89 RID: 7561
		void SetNoProgressTimeout(uint Seconds);

		// Token: 0x06001D8A RID: 7562
		void GetNoProgressTimeout(out uint Seconds);

		// Token: 0x06001D8B RID: 7563
		void GetErrorCount(out uint Errors);

		// Token: 0x06001D8C RID: 7564
		void SetProxySettings(BG_JOB_PROXY_USAGE ProxyUsage, [MarshalAs(UnmanagedType.LPWStr)] string ProxyList, [MarshalAs(UnmanagedType.LPWStr)] string ProxyBypassList);

		// Token: 0x06001D8D RID: 7565
		void GetProxySettings(out BG_JOB_PROXY_USAGE pProxyUsage, [MarshalAs(UnmanagedType.LPWStr)] out string pProxyList, [MarshalAs(UnmanagedType.LPWStr)] out string pProxyBypassList);

		// Token: 0x06001D8E RID: 7566
		void TakeOwnership();
	}
}
