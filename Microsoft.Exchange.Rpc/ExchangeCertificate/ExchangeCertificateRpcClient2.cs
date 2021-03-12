using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Rpc.ExchangeCertificate
{
	// Token: 0x0200024A RID: 586
	internal class ExchangeCertificateRpcClient2 : RpcClientBase
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x00024B08 File Offset: 0x00023F08
		public ExchangeCertificateRpcClient2(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00024B1C File Offset: 0x00023F1C
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] GetCertificate2(int version, byte[] inBlob)
		{
			byte[] result = null;
			byte* ptr = null;
			byte* ptr2 = null;
			int cBytes = 0;
			try
			{
				try
				{
					int num = 0;
					ptr = <Module>.MToUBytesClient(inBlob, &num);
					int num2 = <Module>.cli_GetCertificate2(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_GetCertificate2");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_GetCertificate2");
				}
				result = <Module>.UToMBytes(cBytes, ptr2);
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

		// Token: 0x06000B5C RID: 2908 RVA: 0x00024BD0 File Offset: 0x00023FD0
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] CreateCertificate2(int version, byte[] inBlob)
		{
			byte[] result = null;
			byte* ptr = null;
			byte* ptr2 = null;
			int cBytes = 0;
			try
			{
				try
				{
					int num = 0;
					ptr = <Module>.MToUBytesClient(inBlob, &num);
					int num2 = <Module>.cli_CreateCertificate2(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_CreateCertificate2");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_CreateCertificate2");
				}
				result = <Module>.UToMBytes(cBytes, ptr2);
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

		// Token: 0x06000B5D RID: 2909 RVA: 0x00024C84 File Offset: 0x00024084
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] RemoveCertificate2(int version, byte[] inBlob)
		{
			byte[] result = null;
			byte* ptr = null;
			byte* ptr2 = null;
			int cBytes = 0;
			try
			{
				try
				{
					int num = 0;
					ptr = <Module>.MToUBytesClient(inBlob, &num);
					int num2 = <Module>.cli_RemoveCertificate2(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_RemoveCertificate2");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_RemoveCertificate2");
				}
				result = <Module>.UToMBytes(cBytes, ptr2);
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

		// Token: 0x06000B5E RID: 2910 RVA: 0x00024DEC File Offset: 0x000241EC
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] ExportCertificate2(int version, byte[] inBlob, SecureString password)
		{
			byte[] result = null;
			byte* ptr = null;
			IntPtr intPtr = IntPtr.Zero;
			byte* ptr2 = null;
			int cBytes = 0;
			try
			{
				try
				{
					int num = 0;
					ptr = <Module>.MToUBytesClient(inBlob, &num);
					ushort* ptr3;
					if (password != null)
					{
						intPtr = Marshal.SecureStringToBSTR(password);
						ptr3 = (ushort*)intPtr.ToPointer();
					}
					else
					{
						ptr3 = (ushort*)(&<Module>.??_C@_11LOCGONAA@?$AA?$AA@);
					}
					int num2 = <Module>.cli_ExportCertificate2(base.BindingHandle, version, num, ptr, ptr3, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_ExportCertificate2");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_ExportCertificate2");
				}
				result = <Module>.UToMBytes(cBytes, ptr2);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(intPtr);
				}
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

		// Token: 0x06000B5F RID: 2911 RVA: 0x00024EDC File Offset: 0x000242DC
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] ImportCertificate2(int version, byte[] inBlob, SecureString password)
		{
			byte[] result = null;
			byte* ptr = null;
			IntPtr intPtr = IntPtr.Zero;
			byte* ptr2 = null;
			int cBytes = 0;
			try
			{
				try
				{
					int num = 0;
					ptr = <Module>.MToUBytesClient(inBlob, &num);
					ushort* ptr3;
					if (password != null)
					{
						intPtr = Marshal.SecureStringToBSTR(password);
						ptr3 = (ushort*)intPtr.ToPointer();
					}
					else
					{
						ptr3 = (ushort*)(&<Module>.??_C@_11LOCGONAA@?$AA?$AA@);
					}
					int num2 = <Module>.cli_ImportCertificate2(base.BindingHandle, version, num, ptr, ptr3, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_ImportCertificate2");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_ImportCertificate2");
				}
				result = <Module>.UToMBytes(cBytes, ptr2);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(intPtr);
				}
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

		// Token: 0x06000B60 RID: 2912 RVA: 0x00024D38 File Offset: 0x00024138
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] EnableCertificate2(int version, byte[] inBlob)
		{
			byte[] result = null;
			byte* ptr = null;
			byte* ptr2 = null;
			int cBytes = 0;
			try
			{
				try
				{
					int num = 0;
					ptr = <Module>.MToUBytesClient(inBlob, &num);
					int num2 = <Module>.cli_EnableCertificate2(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_EnableCertificate2");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_EnableCertificate2");
				}
				result = <Module>.UToMBytes(cBytes, ptr2);
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
