using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000627 RID: 1575
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9397F121-7705-4dc9-B049-98B698188414")]
	[ComImport]
	internal interface IWMSyncReader
	{
		// Token: 0x06001C6C RID: 7276
		void Open([MarshalAs(UnmanagedType.LPWStr)] [In] string pwszFilename);

		// Token: 0x06001C6D RID: 7277
		void Close();

		// Token: 0x06001C6E RID: 7278
		void SetRange();

		// Token: 0x06001C6F RID: 7279
		void SetRangeByFrame();

		// Token: 0x06001C70 RID: 7280
		void GetNextSample([In] ushort wStreamNum, out INSSBuffer ppSample, out ulong pcnsSampleTime, out ulong pcnsDuration, out uint pdwFlags, out uint pdwOutputNum, out ushort pwStreamNum);

		// Token: 0x06001C71 RID: 7281
		void SetStreamsSelected();

		// Token: 0x06001C72 RID: 7282
		void GetStreamSelected();

		// Token: 0x06001C73 RID: 7283
		void SetReadStreamSamples();

		// Token: 0x06001C74 RID: 7284
		void GetReadStreamSamples();

		// Token: 0x06001C75 RID: 7285
		void GetOutputSetting();

		// Token: 0x06001C76 RID: 7286
		void SetOutputSetting();

		// Token: 0x06001C77 RID: 7287
		void GetOutputCount(out uint pcOutputs);

		// Token: 0x06001C78 RID: 7288
		void GetOutputProps();

		// Token: 0x06001C79 RID: 7289
		void SetOutputProps();

		// Token: 0x06001C7A RID: 7290
		void GetOutputFormatCount();

		// Token: 0x06001C7B RID: 7291
		void GetOutputFormat([In] uint dwOutputNum, [In] uint dwFormatNum, [MarshalAs(UnmanagedType.Interface)] out IWMOutputMediaProps ppProps);

		// Token: 0x06001C7C RID: 7292
		void GetOutputNumberForStream();

		// Token: 0x06001C7D RID: 7293
		void GetStreamNumberForOutput();

		// Token: 0x06001C7E RID: 7294
		void GetMaxOutputSampleSize();

		// Token: 0x06001C7F RID: 7295
		void GetMaxStreamSampleSize();

		// Token: 0x06001C80 RID: 7296
		void OpenStream();
	}
}
