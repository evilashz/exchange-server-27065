using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.JobQueue
{
	// Token: 0x02000277 RID: 631
	internal class JobQueueRpcClient : RpcClientBase
	{
		// Token: 0x06000BCF RID: 3023 RVA: 0x00029798 File Offset: 0x00028B98
		public JobQueueRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000297AC File Offset: 0x00028BAC
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] EnqueueRequest(int version, int type, byte[] inputParameterBytes)
		{
			int num = 0;
			byte* ptr = <Module>.MToUBytesClient(inputParameterBytes, &num);
			int num2 = 0;
			byte* ptr2 = null;
			byte[] result = null;
			try
			{
				int num3 = <Module>.cli_EnqueueRequest(base.BindingHandle, version, type, num, ptr, &num2, &ptr2);
				if (num2 > 0)
				{
					result = <Module>.UToMBytes(num2, ptr2);
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_EnqueueRequest");
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
			return result;
		}
	}
}
