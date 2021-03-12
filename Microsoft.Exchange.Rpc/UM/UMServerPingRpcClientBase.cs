using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x020003FF RID: 1023
	internal class UMServerPingRpcClientBase : RpcClientBase
	{
		// Token: 0x0600117F RID: 4479 RVA: 0x00057D64 File Offset: 0x00057164
		public UMServerPingRpcClientBase(string machineName) : base(machineName)
		{
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00057D78 File Offset: 0x00057178
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual void Ping(Guid dialPlanGuid, ref bool availableToTakeCalls)
		{
			int num = 0;
			try
			{
				int num2;
				try
				{
					_GUID guid = <Module>.ToGUID(ref dialPlanGuid);
					num = <Module>.cli_Ping(base.BindingHandle, guid, &num2);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "Ping");
				}
				if (num < 0)
				{
					RpcClientBase.ThrowRpcException(num, "Ping");
				}
				int num3 = (num2 == 1) ? 1 : 0;
				availableToTakeCalls = (num3 != 0);
			}
			finally
			{
			}
		}
	}
}
