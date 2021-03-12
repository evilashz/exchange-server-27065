using System;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x02000259 RID: 601
	internal abstract class ExchangeRpcServerBase : RpcServerBase
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x000272C8 File Offset: 0x000266C8
		public virtual IProxyServer GetProxyServer()
		{
			return null;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00027F64 File Offset: 0x00027364
		public ExchangeRpcServerBase()
		{
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000272D8 File Offset: 0x000266D8
		// Note: this type is marked as 'beforefieldinit'.
		static ExchangeRpcServerBase()
		{
			ExchangeRpcServerBase.maxBuildMajor = short.MaxValue;
			ExchangeRpcServerBase.criticalBuildMajor = 0;
			ExchangeRpcServerBase.maxProductMinor = 255;
			ExchangeRpcServerBase.criticalProductMinor = 0;
			ExchangeRpcServerBase.maxProductMajor = 127;
			ExchangeRpcServerBase.criticalProductMajor = 6;
		}

		// Token: 0x04000CCC RID: 3276
		public static short criticalProductMajor = 6;

		// Token: 0x04000CCD RID: 3277
		public static short maxProductMajor = 127;

		// Token: 0x04000CCE RID: 3278
		public static short criticalProductMinor = 0;

		// Token: 0x04000CCF RID: 3279
		public static short maxProductMinor = 255;

		// Token: 0x04000CD0 RID: 3280
		public static short criticalBuildMajor = 0;

		// Token: 0x04000CD1 RID: 3281
		public static short maxBuildMajor = 32767;

		// Token: 0x04000CD2 RID: 3282
		public static short criticalBuildMinor = 0;
	}
}
