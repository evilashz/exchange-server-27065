using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001DB RID: 475
	internal class EmsmdbConstants
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x0001B720 File Offset: 0x0001AB20
		// Note: this type is marked as 'beforefieldinit'.
		static EmsmdbConstants()
		{
			int num = EmsmdbConstants.MaxRopBufferSize / 2;
			EmsmdbConstants.MinFastTransferChainSize = EmsmdbConstants.ExtendedBufferHeaderSize + num;
			EmsmdbConstants.MaxMapiHttpChainedPayloadSize = 268288;
			EmsmdbConstants.MaxMapiHttpChainedOutlookPayloadSize = 104448;
		}

		// Token: 0x04000B89 RID: 2953
		public static readonly int ExtendedBufferHeaderSize = 8;

		// Token: 0x04000B8A RID: 2954
		public static readonly int MaxAuxBufferSize = 4096;

		// Token: 0x04000B8B RID: 2955
		public static readonly int MaxExtendedAuxBufferSize = EmsmdbConstants.MaxAuxBufferSize + EmsmdbConstants.ExtendedBufferHeaderSize;

		// Token: 0x04000B8C RID: 2956
		public static readonly int MaxRopBufferSize = 32767;

		// Token: 0x04000B8D RID: 2957
		public static readonly int MaxExtendedRopBufferSize = EmsmdbConstants.MaxRopBufferSize + EmsmdbConstants.ExtendedBufferHeaderSize;

		// Token: 0x04000B8E RID: 2958
		public static readonly int MaxOutlookChainedExtendedRopBufferSize = EmsmdbConstants.ExtendedBufferHeaderSize + 98304;

		// Token: 0x04000B8F RID: 2959
		public static readonly int MaxChainedExtendedRopBufferSize = 262144;

		// Token: 0x04000B90 RID: 2960
		public static readonly int MinCompressionSize = 1025;

		// Token: 0x04000B91 RID: 2961
		public static readonly int MaxChainBuffers = 96;

		// Token: 0x04000B92 RID: 2962
		public static readonly int MinChainSize = 8192;

		// Token: 0x04000B93 RID: 2963
		public static readonly int MinQueryRowsChainSize = EmsmdbConstants.MaxRopBufferSize + EmsmdbConstants.ExtendedBufferHeaderSize;

		// Token: 0x04000B94 RID: 2964
		public static readonly int MinFastTransferChainSize;

		// Token: 0x04000B95 RID: 2965
		public static readonly int MaxMapiHttpChainedPayloadSize;

		// Token: 0x04000B96 RID: 2966
		public static readonly int MaxMapiHttpChainedOutlookPayloadSize;
	}
}
