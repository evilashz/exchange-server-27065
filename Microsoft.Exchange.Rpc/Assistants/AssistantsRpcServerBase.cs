using System;

namespace Microsoft.Exchange.Rpc.Assistants
{
	// Token: 0x020001C3 RID: 451
	internal abstract class AssistantsRpcServerBase : RpcServerBase
	{
		// Token: 0x0600098E RID: 2446
		public abstract void RunNow(string assistantName, ValueType mailboxGuid, ValueType mdbGuid);

		// Token: 0x0600098F RID: 2447
		public abstract void Halt(string assistantName);

		// Token: 0x06000990 RID: 2448
		public abstract int RunNowWithParamsHR(string assistantName, ValueType mailboxGuid, ValueType mdbGuid, string parameters);

		// Token: 0x06000991 RID: 2449
		public abstract int RunNowHR(string assistantName, ValueType mailboxGuid, ValueType mdbGuid);

		// Token: 0x06000992 RID: 2450
		public abstract int HaltHR(string assistantName);

		// Token: 0x06000993 RID: 2451 RVA: 0x00016220 File Offset: 0x00015620
		public static void StopServer()
		{
			RpcServerBase.StopServer(AssistantsRpcServerBase.RpcIntfHandle);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000162E8 File Offset: 0x000156E8
		public AssistantsRpcServerBase()
		{
		}

		// Token: 0x04000B6C RID: 2924
		internal static IntPtr RpcIntfHandle = (IntPtr)<Module>.AssistantsRpc_v1_0_s_ifspec;
	}
}
