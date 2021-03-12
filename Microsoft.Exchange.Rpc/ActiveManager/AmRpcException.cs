using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x0200010C RID: 268
	internal class AmRpcException : RpcException
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x00002C74 File Offset: 0x00002074
		public AmRpcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00002C5C File Offset: 0x0000205C
		public AmRpcException(string message, int hr) : base(message, hr)
		{
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00002C48 File Offset: 0x00002048
		public AmRpcException(string message) : base(message)
		{
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00001B40 File Offset: 0x00000F40
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00001B54 File Offset: 0x00000F54
		public static bool DowngradeOOMErrors
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return AmRpcException.downgradeOOM;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				AmRpcException.downgradeOOM = value;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00002C8C File Offset: 0x0000208C
		public static void ThrowRpcException(int status, string routineName)
		{
			UncaughtClrOverRpcBarrierException.ThrowIfNecessary(status, routineName);
			if (status == 14 && AmRpcException.DowngradeOOMErrors)
			{
				throw new AmRpcException(string.Format("Error 0x{0:x} mapped to 0x{1:x} RPC_S_OUT_OF_RESOURCES from {2}", 14, 1721, routineName), 1721);
			}
			throw new AmRpcException(string.Format("Error 0x{0:x} ({2}) from {1}", status, routineName, new Win32Exception(status).Message), status);
		}

		// Token: 0x04000940 RID: 2368
		private static bool downgradeOOM = true;
	}
}
