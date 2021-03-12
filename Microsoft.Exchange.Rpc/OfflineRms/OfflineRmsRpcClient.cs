using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.OfflineRms
{
	// Token: 0x02000368 RID: 872
	internal class OfflineRmsRpcClient : RpcClientBase
	{
		// Token: 0x06000FBD RID: 4029 RVA: 0x00045F38 File Offset: 0x00045338
		public OfflineRmsRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00045FF0 File Offset: 0x000453F0
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] AcquireTenantLicenses(int version, byte[] inputParameterBytes)
		{
			int num = 0;
			byte* ptr = <Module>.MToUBytesClient(inputParameterBytes, &num);
			int num2 = 0;
			byte* ptr2 = null;
			byte[] result = null;
			try
			{
				int num3 = <Module>.cli_AcquireTenantLicenses(base.BindingHandle, version, num, ptr, &num2, &ptr2);
				if (num2 > 0)
				{
					result = <Module>.UToMBytes(num2, ptr2);
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_AcquireTenantLicenses");
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

		// Token: 0x06000FBF RID: 4031 RVA: 0x00045F4C File Offset: 0x0004534C
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] AcquireUseLicenses(int version, byte[] inputParameterBytes)
		{
			int num = 0;
			byte* ptr = <Module>.MToUBytesClient(inputParameterBytes, &num);
			int num2 = 0;
			byte* ptr2 = null;
			byte[] result = null;
			try
			{
				int num3 = <Module>.cli_AcquireUseLicenses(base.BindingHandle, version, num, ptr, &num2, &ptr2);
				if (num2 > 0)
				{
					result = <Module>.UToMBytes(num2, ptr2);
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_AcquireTenantLicenses");
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

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00046094 File Offset: 0x00045494
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] GetTenantActiveCryptoMode(int version, byte[] inputParameterBytes)
		{
			int num = 0;
			byte* ptr = <Module>.MToUBytesClient(inputParameterBytes, &num);
			int num2 = 0;
			byte* ptr2 = null;
			byte[] result = null;
			try
			{
				int num3 = <Module>.cli_GetTenantActiveCryptoMode(base.BindingHandle, version, num, ptr, &num2, &ptr2);
				if (num2 > 0)
				{
					result = <Module>.UToMBytes(num2, ptr2);
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_GetTenantActiveCryptoMode");
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
