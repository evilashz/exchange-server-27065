using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x02000400 RID: 1024
	internal class UMVersionedRpcClientBase : RpcClientBase
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x00057E04 File Offset: 0x00057204
		protected UMVersionedRpcClientBase(string serverFqdn) : base(serverFqdn)
		{
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00057854 File Offset: 0x00056C54
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00057E18 File Offset: 0x00057218
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual byte[] ExecuteRequest(byte[] request)
		{
			int num = 0;
			int cBytes = 0;
			byte* ptr = null;
			byte* ptr2 = null;
			byte[] result = null;
			try
			{
				int num2;
				try
				{
					num2 = <Module>.MToUBytes(request, &num, &ptr);
					if (num2 >= 0)
					{
						num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void*,System.Int32,System.Byte*,System.Int32*,System.Byte**), base.BindingHandle, num, ptr, ref cBytes, ref ptr2, this.executeRequestDelegate);
						if (num2 >= 0 && ptr2 != null)
						{
							result = <Module>.UToMBytes(cBytes, ptr2);
						}
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), this.operationName);
				}
				if (num2 < 0)
				{
					RpcClientBase.ThrowRpcException(num2, this.operationName);
				}
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

		// Token: 0x0400102F RID: 4143
		protected string operationName;

		// Token: 0x04001030 RID: 4144
		protected method executeRequestDelegate;
	}
}
