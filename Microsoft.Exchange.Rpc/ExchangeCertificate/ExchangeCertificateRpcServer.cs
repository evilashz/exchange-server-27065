using System;
using System.Security;

namespace Microsoft.Exchange.Rpc.ExchangeCertificate
{
	// Token: 0x0200024B RID: 587
	internal abstract class ExchangeCertificateRpcServer : RpcServerBase
	{
		// Token: 0x06000B61 RID: 2913
		public abstract byte[] GetCertificate(int version, byte[] pInBytes);

		// Token: 0x06000B62 RID: 2914
		public abstract byte[] CreateCertificate(int version, byte[] pInBytes);

		// Token: 0x06000B63 RID: 2915
		public abstract byte[] RemoveCertificate(int version, byte[] pInBytes);

		// Token: 0x06000B64 RID: 2916
		public abstract byte[] ExportCertificate(int version, byte[] pInBytes, SecureString password);

		// Token: 0x06000B65 RID: 2917
		public abstract byte[] ImportCertificate(int version, byte[] pInBytes, SecureString password);

		// Token: 0x06000B66 RID: 2918
		public abstract byte[] EnableCertificate(int version, byte[] pInBytes);

		// Token: 0x06000B67 RID: 2919 RVA: 0x00025260 File Offset: 0x00024660
		public ExchangeCertificateRpcServer()
		{
		}

		// Token: 0x04000CB4 RID: 3252
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IExchangeCertificate_v1_0_s_ifspec;
	}
}
