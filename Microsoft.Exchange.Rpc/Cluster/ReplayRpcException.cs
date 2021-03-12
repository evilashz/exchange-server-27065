using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000129 RID: 297
	internal class ReplayRpcException : RpcException
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x000082D4 File Offset: 0x000076D4
		public ReplayRpcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000082BC File Offset: 0x000076BC
		public ReplayRpcException(string message, int hr) : base(message, hr)
		{
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000082A8 File Offset: 0x000076A8
		public ReplayRpcException(string message) : base(message)
		{
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00005610 File Offset: 0x00004A10
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x00005624 File Offset: 0x00004A24
		public static bool DowngradeOOMErrors
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return ReplayRpcException.downgradeOOM;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				ReplayRpcException.downgradeOOM = value;
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000082EC File Offset: 0x000076EC
		public static void ThrowRpcException(int status, string routineName)
		{
			UncaughtClrOverRpcBarrierException.ThrowIfNecessary(status, routineName);
			if (status == 14 && ReplayRpcException.DowngradeOOMErrors)
			{
				throw new ReplayRpcException(string.Format("Error 0x{0:x} mapped to 0x{1:x} RPC_S_OUT_OF_RESOURCES from {2}", 14, 1721, routineName), 1721);
			}
			throw new ReplayRpcException(string.Format("Error 0x{0:x} ({2}) from {1}", status, routineName, new Win32Exception(status).Message), status);
		}

		// Token: 0x0400099B RID: 2459
		private static bool downgradeOOM = true;
	}
}
