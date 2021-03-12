using System;

namespace Microsoft.Exchange.Rpc.OfflineRms
{
	// Token: 0x02000369 RID: 873
	internal abstract class OfflineRmsRpcServer : RpcServerBase
	{
		// Token: 0x06000FC1 RID: 4033
		public abstract byte[] AcquireTenantLicenses(int version, byte[] inputParameterBytes);

		// Token: 0x06000FC2 RID: 4034
		public abstract byte[] AcquireUseLicenses(int version, byte[] inputParameterBytes);

		// Token: 0x06000FC3 RID: 4035
		public abstract byte[] GetTenantActiveCryptoMode(int version, byte[] inputParameterBytes);

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00046208 File Offset: 0x00045608
		public OfflineRmsRpcServer()
		{
		}

		// Token: 0x04000F4D RID: 3917
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IOfflineRms_v1_0_s_ifspec;
	}
}
