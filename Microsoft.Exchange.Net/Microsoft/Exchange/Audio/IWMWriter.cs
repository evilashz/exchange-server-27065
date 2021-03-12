using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000628 RID: 1576
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("96406BD4-2B2B-11d3-B36B-00C04F6108FF")]
	[ComImport]
	internal interface IWMWriter
	{
		// Token: 0x06001C81 RID: 7297
		void SetProfileByID();

		// Token: 0x06001C82 RID: 7298
		void SetProfile([MarshalAs(UnmanagedType.Interface)] [In] IWMProfile pProfile);

		// Token: 0x06001C83 RID: 7299
		void SetOutputFilename([MarshalAs(UnmanagedType.LPWStr)] [In] string pwszFilename);

		// Token: 0x06001C84 RID: 7300
		void GetInputCount(out uint pcInputs);

		// Token: 0x06001C85 RID: 7301
		void GetInputProps([In] uint dwInputNum, [MarshalAs(UnmanagedType.Interface)] out IWMInputMediaProps ppInput);

		// Token: 0x06001C86 RID: 7302
		void SetInputProps([In] uint dwInputNum, [MarshalAs(UnmanagedType.Interface)] [In] IWMInputMediaProps pInput);

		// Token: 0x06001C87 RID: 7303
		void GetInputFormatCount();

		// Token: 0x06001C88 RID: 7304
		void GetInputFormat();

		// Token: 0x06001C89 RID: 7305
		void BeginWriting();

		// Token: 0x06001C8A RID: 7306
		void EndWriting();

		// Token: 0x06001C8B RID: 7307
		void AllocateSample([In] uint dwSampleSize, [MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppSample);

		// Token: 0x06001C8C RID: 7308
		void WriteSample([In] uint dwInputNum, [In] ulong cnsSampleTime, [In] uint dwFlags, [MarshalAs(UnmanagedType.Interface)] [In] INSSBuffer pSample);

		// Token: 0x06001C8D RID: 7309
		void Flush();
	}
}
