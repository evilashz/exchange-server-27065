using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000109 RID: 265
	internal class UncaughtClrOverRpcBarrierException : RpcException
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00002C08 File Offset: 0x00002008
		public UncaughtClrOverRpcBarrierException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00002BF0 File Offset: 0x00001FF0
		public UncaughtClrOverRpcBarrierException(string message, int hr) : base(message, hr)
		{
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00002BDC File Offset: 0x00001FDC
		public UncaughtClrOverRpcBarrierException(string message) : base(message)
		{
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00002C20 File Offset: 0x00002020
		public static void ThrowIfNecessary(int status, string routineName)
		{
			if (-532459699 == status)
			{
				throw new UncaughtClrOverRpcBarrierException(string.Format("Uncaught CLR exception on other side of '{0}'.", routineName));
			}
		}

		// Token: 0x04000939 RID: 2361
		private static uint ClrExceptionCode = 3762507597U;
	}
}
