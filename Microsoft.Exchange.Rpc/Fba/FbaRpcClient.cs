using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Fba
{
	// Token: 0x02000270 RID: 624
	internal class FbaRpcClient : RpcClientBase
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x000289DC File Offset: 0x00027DDC
		public FbaRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc)
		{
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000289C8 File Offset: 0x00027DC8
		public FbaRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000289F4 File Offset: 0x00027DF4
		[HandleProcessCorruptedStateExceptions]
		public unsafe int EncryptUserToken(uint dwFlags, string pszVersion, string pszUserPrincipalName, out byte[] pbTokenData)
		{
			byte* ptr = null;
			uint num = 0;
			int result = 0;
			if (null == pszVersion)
			{
				pszVersion = string.Empty;
			}
			if (null == pszUserPrincipalName)
			{
				pszUserPrincipalName = string.Empty;
			}
			SafeMarshalHGlobalHandle safeMarshalHGlobalHandle = new SafeMarshalHGlobalHandle(Marshal.StringToHGlobalAnsi(pszVersion));
			SafeMarshalHGlobalHandle safeMarshalHGlobalHandle2 = new SafeMarshalHGlobalHandle(Marshal.StringToHGlobalAnsi(pszUserPrincipalName));
			base.ResetRetryCounter();
			for (;;)
			{
				try
				{
					IntPtr intPtr = safeMarshalHGlobalHandle2.DangerousGetHandle();
					IntPtr intPtr2 = safeMarshalHGlobalHandle.DangerousGetHandle();
					result = <Module>.cli_EncryptUserToken(base.BindingHandle, dwFlags, (sbyte*)intPtr2.ToPointer(), (sbyte*)intPtr.ToPointer(), &ptr, &num);
				}
				catch when (endfilter(true))
				{
					int exceptionCode = Marshal.GetExceptionCode();
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.CallCancelled | RpcRetryType.ServerBusy | RpcRetryType.ServerUnavailable) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_EncryptUserToken");
				}
				finally
				{
					if (null != ptr)
					{
						pbTokenData = new byte[num];
						IntPtr source = new IntPtr((void*)ptr);
						Marshal.Copy(source, pbTokenData, 0, num);
						<Module>.MIDL_user_free((void*)ptr);
					}
				}
				break;
			}
			if (safeMarshalHGlobalHandle != null)
			{
				((IDisposable)safeMarshalHGlobalHandle).Dispose();
			}
			if (safeMarshalHGlobalHandle2 != null)
			{
				((IDisposable)safeMarshalHGlobalHandle2).Dispose();
			}
			return result;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00028B14 File Offset: 0x00027F14
		[HandleProcessCorruptedStateExceptions]
		public unsafe int DecryptUserToken(uint dwFlags, byte[] pbTokenData, out string pszUserPrincipalName, out byte[] pbTokenDataNew)
		{
			int result = 0;
			sbyte* ptr = null;
			uint num = 0;
			byte* ptr2 = null;
			uint num2 = 0;
			int num3 = 0;
			byte* ptr3 = <Module>.MToUBytesClient(pbTokenData, &num3);
			uint num4 = num3;
			base.ResetRetryCounter();
			for (;;)
			{
				try
				{
					result = <Module>.cli_DecryptUserToken(base.BindingHandle, dwFlags, ptr3, num4, &ptr, &num, &ptr2, &num2);
				}
				catch when (endfilter(true))
				{
					int exceptionCode = Marshal.GetExceptionCode();
					if (base.RetryRpcCall(exceptionCode, RpcRetryType.CallCancelled | RpcRetryType.ServerBusy | RpcRetryType.ServerUnavailable) != 0)
					{
						continue;
					}
					<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "cli_DecryptUserToken");
				}
				finally
				{
					if (null != ptr2)
					{
						pbTokenDataNew = new byte[num2];
						IntPtr source = new IntPtr((void*)ptr2);
						Marshal.Copy(source, pbTokenDataNew, 0, num2);
						<Module>.MIDL_user_free((void*)ptr2);
					}
					if (null != ptr)
					{
						pszUserPrincipalName = new string((sbyte*)ptr);
						<Module>.MIDL_user_free((void*)ptr);
					}
				}
				break;
			}
			return result;
		}

		// Token: 0x04000CE9 RID: 3305
		public static uint UpdateToken = 1U;

		// Token: 0x04000CEA RID: 3306
		public static string Version_1_0 = "1.0";
	}
}
