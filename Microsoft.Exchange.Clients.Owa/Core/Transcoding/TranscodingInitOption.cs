using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002EF RID: 751
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct TranscodingInitOption
	{
		// Token: 0x04001528 RID: 5416
		public int RowNumberPerExcelPage;

		// Token: 0x04001529 RID: 5417
		public int MaxOutputSize;

		// Token: 0x0400152A RID: 5418
		[MarshalAs(UnmanagedType.Bool)]
		public bool IsImageMode;

		// Token: 0x0400152B RID: 5419
		public HtmlFormat HtmlOutputFormat;
	}
}
