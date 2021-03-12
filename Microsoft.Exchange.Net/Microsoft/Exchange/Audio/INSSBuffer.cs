using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200062C RID: 1580
	[Guid("E1CD3524-03D7-11d2-9EED-006097D2D7CF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface INSSBuffer
	{
		// Token: 0x06001CB1 RID: 7345
		void GetLength(out uint pdwLength);

		// Token: 0x06001CB2 RID: 7346
		void SetLength([In] uint dwLength);

		// Token: 0x06001CB3 RID: 7347
		void GetMaxLength(out uint pdwLength);

		// Token: 0x06001CB4 RID: 7348
		void GetBuffer(out IntPtr ppdwBuffer);

		// Token: 0x06001CB5 RID: 7349
		void GetBufferAndLength(out IntPtr ppdwBuffer, out uint pdwLength);
	}
}
