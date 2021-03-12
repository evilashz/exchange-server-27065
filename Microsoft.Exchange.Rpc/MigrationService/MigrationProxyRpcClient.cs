using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Rpc.Nspi;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x020002A4 RID: 676
	internal class MigrationProxyRpcClient : RpcClientBase, IMigrationProxyRpc
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002E2B0 File Offset: 0x0002D6B0
		public MigrationProxyRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002E2C4 File Offset: 0x0002D6C4
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual int NspiQueryRows(int version, byte[] inBlob, out byte[] outBlob, out SafeRpcMemoryHandle rowsetHandle)
		{
			int result = -2147467259;
			_SRowSet_r* ptr = null;
			byte* ptr2 = null;
			byte* ptr3 = null;
			int cBytes = 0;
			int num = 0;
			outBlob = null;
			rowsetHandle = null;
			try
			{
				bool flag = false;
				do
				{
					try
					{
						ptr2 = <Module>.MToUBytesClient(inBlob, &num);
						result = <Module>.cli_ProxyNspiQueryRows(base.BindingHandle, version, num, ptr2, &cBytes, &ptr3, &ptr);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode && !flag)
						{
							flag = true;
						}
						else
						{
							RpcClientBase.ThrowRpcException(exceptionCode, "ProxyNspiQueryRows");
						}
					}
				}
				while (flag);
				outBlob = <Module>.UToMBytes(cBytes, ptr3);
				if (ptr != null)
				{
					IntPtr handle = new IntPtr((void*)ptr);
					rowsetHandle = new SafeSRowSetHandle(handle);
				}
			}
			finally
			{
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
				if (ptr3 != null)
				{
					<Module>.MIDL_user_free((void*)ptr3);
				}
			}
			return result;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002E3AC File Offset: 0x0002D7AC
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual int NspiGetRecipient(int version, byte[] inBlob, out byte[] outBlob, out SafeRpcMemoryHandle rowsetHandle)
		{
			int result = -2147467259;
			_SRowSet_r* ptr = null;
			byte* ptr2 = null;
			byte* ptr3 = null;
			int cBytes = 0;
			int num = 0;
			outBlob = null;
			rowsetHandle = null;
			try
			{
				bool flag = false;
				do
				{
					try
					{
						ptr2 = <Module>.MToUBytesClient(inBlob, &num);
						result = <Module>.cli_ProxyNspiGetRecipient(base.BindingHandle, version, num, ptr2, &cBytes, &ptr3, &ptr);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode && !flag)
						{
							flag = true;
						}
						else
						{
							RpcClientBase.ThrowRpcException(exceptionCode, "ProxyNspiGetRecipient");
						}
					}
				}
				while (flag);
				outBlob = <Module>.UToMBytes(cBytes, ptr3);
				if (ptr != null)
				{
					IntPtr handle = new IntPtr((void*)ptr);
					rowsetHandle = new SafeSRowSetHandle(handle);
				}
			}
			finally
			{
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
				if (ptr3 != null)
				{
					<Module>.MIDL_user_free((void*)ptr3);
				}
			}
			return result;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002E494 File Offset: 0x0002D894
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual int NspiSetRecipient(int version, byte[] inBlob, out byte[] outBlob)
		{
			int result = -2147467259;
			byte* ptr = null;
			byte* ptr2 = null;
			int cBytes = 0;
			int num = 0;
			outBlob = null;
			try
			{
				bool flag = false;
				do
				{
					try
					{
						ptr = <Module>.MToUBytesClient(inBlob, &num);
						result = <Module>.cli_ProxyNspiSetRecipient(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode && !flag)
						{
							flag = true;
						}
						else
						{
							RpcClientBase.ThrowRpcException(exceptionCode, "ProxyNspiSetRecipient");
						}
					}
				}
				while (flag);
				outBlob = <Module>.UToMBytes(cBytes, ptr2);
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

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002E55C File Offset: 0x0002D95C
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual int NspiGetGroupMembers(int version, byte[] inBlob, out byte[] outBlob, out SafeRpcMemoryHandle rowsetHandle)
		{
			int result = -2147467259;
			_SRowSet_r* ptr = null;
			byte* ptr2 = null;
			byte* ptr3 = null;
			int cBytes = 0;
			int num = 0;
			outBlob = null;
			rowsetHandle = null;
			try
			{
				bool flag = false;
				do
				{
					try
					{
						ptr2 = <Module>.MToUBytesClient(inBlob, &num);
						result = <Module>.cli_ProxyNspiGetGroupMembers(base.BindingHandle, version, num, ptr2, &cBytes, &ptr3, &ptr);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode && !flag)
						{
							flag = true;
						}
						else
						{
							RpcClientBase.ThrowRpcException(exceptionCode, "ProxyNspiGetGroupMembers");
						}
					}
				}
				while (flag);
				outBlob = <Module>.UToMBytes(cBytes, ptr3);
				if (ptr != null)
				{
					IntPtr handle = new IntPtr((void*)ptr);
					rowsetHandle = new SafeSRowSetHandle(handle);
				}
			}
			finally
			{
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
				if (ptr3 != null)
				{
					<Module>.MIDL_user_free((void*)ptr3);
				}
			}
			return result;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002E644 File Offset: 0x0002DA44
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual int NspiRfrGetNewDSA(int version, byte[] inBlob, out byte[] outBlob)
		{
			int result = -2147467259;
			byte* ptr = null;
			byte* ptr2 = null;
			int cBytes = 0;
			int num = 0;
			outBlob = null;
			try
			{
				bool flag = false;
				do
				{
					try
					{
						ptr = <Module>.MToUBytesClient(inBlob, &num);
						result = <Module>.cli_ProxyNspiRfrGetNewDSA(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode && !flag)
						{
							flag = true;
						}
						else
						{
							RpcClientBase.ThrowRpcException(exceptionCode, "ProxyNspiRfrGetNewDSA");
						}
					}
				}
				while (flag);
				outBlob = <Module>.UToMBytes(cBytes, ptr2);
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

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002E70C File Offset: 0x0002DB0C
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual void AutodiscoverGetUserSettings(int version, byte[] inBlob, out byte[] outBlob)
		{
			byte* ptr = null;
			byte* ptr2 = null;
			int cBytes = 0;
			int num = 0;
			outBlob = null;
			try
			{
				bool flag = false;
				do
				{
					try
					{
						ptr = <Module>.MToUBytesClient(inBlob, &num);
						<Module>.cli_ProxyAutodiscoverGetUserSettings(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode && !flag)
						{
							flag = true;
						}
						else
						{
							RpcClientBase.ThrowRpcException(exceptionCode, "ProxyAutodiscoverGetUserSettings");
						}
					}
				}
				while (flag);
				outBlob = <Module>.UToMBytes(cBytes, ptr2);
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
		}

		// Token: 0x04000D5B RID: 3419
		public const int RpcErrorServerTooBusy = 1723;
	}
}
