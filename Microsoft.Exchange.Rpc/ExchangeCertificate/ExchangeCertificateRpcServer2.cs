using System;
using System.Security;

namespace Microsoft.Exchange.Rpc.ExchangeCertificate
{
	// Token: 0x0200024C RID: 588
	internal abstract class ExchangeCertificateRpcServer2 : RpcServerBase
	{
		// Token: 0x06000B69 RID: 2921
		public abstract byte[] GetCertificate2(int version, byte[] pInBytes);

		// Token: 0x06000B6A RID: 2922
		public abstract byte[] CreateCertificate2(int version, byte[] pInBytes);

		// Token: 0x06000B6B RID: 2923
		public abstract byte[] RemoveCertificate2(int version, byte[] pInBytes);

		// Token: 0x06000B6C RID: 2924
		public abstract byte[] ExportCertificate2(int version, byte[] pInBytes, SecureString password);

		// Token: 0x06000B6D RID: 2925
		public abstract byte[] ImportCertificate2(int version, byte[] pInBytes, SecureString password);

		// Token: 0x06000B6E RID: 2926
		public abstract byte[] EnableCertificate2(int version, byte[] pInBytes);

		// Token: 0x06000B6F RID: 2927 RVA: 0x00025508 File Offset: 0x00024908
		public ExchangeCertificateRpcServer2()
		{
		}

		// Token: 0x04000CB5 RID: 3253
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IExchangeCertificate2_v1_0_s_ifspec;
	}
}
