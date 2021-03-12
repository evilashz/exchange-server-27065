using System;

namespace Microsoft.Exchange.Rpc.IPFilter
{
	// Token: 0x02000276 RID: 630
	internal abstract class IPFilterRpcServer : RpcServerBase
	{
		// Token: 0x06000BCA RID: 3018
		public abstract int Add(IPFilterRange element);

		// Token: 0x06000BCB RID: 3019
		public abstract int Remove(int identity, int filter);

		// Token: 0x06000BCC RID: 3020
		public abstract IPFilterRange[] GetItems(int startIdentity, int flags, ulong highBytes, ulong lowBytes, int count);

		// Token: 0x06000BCD RID: 3021 RVA: 0x000296AC File Offset: 0x00028AAC
		public IPFilterRpcServer()
		{
		}

		// Token: 0x04000CF6 RID: 3318
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IAdminIPFilters_v1_0_s_ifspec;
	}
}
