using System;

namespace Microsoft.Exchange.Rpc.JobQueue
{
	// Token: 0x02000278 RID: 632
	internal abstract class JobQueueRpcServer : RpcServerBase
	{
		// Token: 0x06000BD1 RID: 3025
		public abstract byte[] EnqueueRequest(int version, int type, byte[] inputParameterBytes);

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000298AC File Offset: 0x00028CAC
		public JobQueueRpcServer()
		{
		}

		// Token: 0x04000CF7 RID: 3319
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IJobQueue_v1_0_s_ifspec;
	}
}
