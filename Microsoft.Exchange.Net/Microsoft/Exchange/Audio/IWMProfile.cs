using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000629 RID: 1577
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("96406BDB-2B2B-11d3-B36B-00C04F6108FF")]
	[ComImport]
	internal interface IWMProfile
	{
		// Token: 0x06001C8E RID: 7310
		void GetVersion();

		// Token: 0x06001C8F RID: 7311
		void GetName();

		// Token: 0x06001C90 RID: 7312
		void SetName();

		// Token: 0x06001C91 RID: 7313
		void GetDescription();

		// Token: 0x06001C92 RID: 7314
		void SetDescription();

		// Token: 0x06001C93 RID: 7315
		void GetStreamCount();

		// Token: 0x06001C94 RID: 7316
		void GetStream([In] uint dwStreamIndex, [MarshalAs(UnmanagedType.Interface)] out IWMStreamConfig ppConfig);

		// Token: 0x06001C95 RID: 7317
		void GetStreamByNumber();

		// Token: 0x06001C96 RID: 7318
		void RemoveStream();

		// Token: 0x06001C97 RID: 7319
		void RemoveStreamByNumber();

		// Token: 0x06001C98 RID: 7320
		void AddStream();

		// Token: 0x06001C99 RID: 7321
		void ReconfigStream([MarshalAs(UnmanagedType.Interface)] [In] IWMStreamConfig pConfig);

		// Token: 0x06001C9A RID: 7322
		void CreateNewStream();

		// Token: 0x06001C9B RID: 7323
		void GetMutualExclusionCount();

		// Token: 0x06001C9C RID: 7324
		void GetMutualExclusion();

		// Token: 0x06001C9D RID: 7325
		void RemoveMutualExclusion();

		// Token: 0x06001C9E RID: 7326
		void AddMutualExclusion();

		// Token: 0x06001C9F RID: 7327
		void CreateNewMutualExclusion();
	}
}
