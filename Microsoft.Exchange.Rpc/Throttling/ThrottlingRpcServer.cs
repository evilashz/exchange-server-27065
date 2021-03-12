using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Throttling
{
	// Token: 0x020003FA RID: 1018
	internal abstract class ThrottlingRpcServer : RpcServerBase
	{
		// Token: 0x0600116E RID: 4462
		[return: MarshalAs(UnmanagedType.U1)]
		public abstract bool ObtainSubmissionTokens(Guid mailboxGuid, int requestedTokenCount, int totalTokenCount, int submissionType);

		// Token: 0x0600116F RID: 4463
		public abstract byte[] ObtainTokens(byte[] inBlob);

		// Token: 0x06001170 RID: 4464 RVA: 0x00057558 File Offset: 0x00056958
		public ThrottlingRpcServer()
		{
		}

		// Token: 0x0400102C RID: 4140
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IThrottling_v1_0_s_ifspec;
	}
}
