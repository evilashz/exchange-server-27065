using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200062B RID: 1579
	[Guid("96406BDC-2B2B-11d3-B36B-00C04F6108FF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IWMStreamConfig
	{
		// Token: 0x06001CA6 RID: 7334
		void GetStreamType();

		// Token: 0x06001CA7 RID: 7335
		void GetStreamNumber();

		// Token: 0x06001CA8 RID: 7336
		void SetStreamNumber();

		// Token: 0x06001CA9 RID: 7337
		void GetStreamName();

		// Token: 0x06001CAA RID: 7338
		void SetStreamName();

		// Token: 0x06001CAB RID: 7339
		void GetConnectionName();

		// Token: 0x06001CAC RID: 7340
		void SetConnectionName();

		// Token: 0x06001CAD RID: 7341
		void GetBitrate();

		// Token: 0x06001CAE RID: 7342
		void SetBitrate([In] uint pdwBitrate);

		// Token: 0x06001CAF RID: 7343
		void GetBufferWindow();

		// Token: 0x06001CB0 RID: 7344
		void SetBufferWindow();
	}
}
