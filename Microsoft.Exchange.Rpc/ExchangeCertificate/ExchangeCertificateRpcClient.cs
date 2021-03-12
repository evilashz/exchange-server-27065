using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Rpc.ExchangeCertificate
{
	// Token: 0x02000246 RID: 582
	internal class ExchangeCertificateRpcClient : RpcClientBase
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x00024644 File Offset: 0x00023A44
		public ExchangeCertificateRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00024658 File Offset: 0x00023A58
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] GetCertificate(int version, byte[] inBlob)
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
					int num2 = <Module>.cli_GetCertificate(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_GetCertificate");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_GetCertificate");
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

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002470C File Offset: 0x00023B0C
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] CreateCertificate(int version, byte[] inBlob)
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
					int num2 = <Module>.cli_CreateCertificate(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_CreateCertificate");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_CreateCertificate");
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

		// Token: 0x06000B56 RID: 2902 RVA: 0x000247C0 File Offset: 0x00023BC0
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] RemoveCertificate(int version, byte[] inBlob)
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
					int num2 = <Module>.cli_RemoveCertificate(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_RemoveCertificate");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_RemoveCertificate");
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

		// Token: 0x06000B57 RID: 2903 RVA: 0x00024928 File Offset: 0x00023D28
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] ExportCertificate(int version, byte[] inBlob, SecureString password)
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
					int num2 = <Module>.cli_ExportCertificate(base.BindingHandle, version, num, ptr, ptr3, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_ExportCertificate");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_ExportCertificate");
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

		// Token: 0x06000B58 RID: 2904 RVA: 0x00024A18 File Offset: 0x00023E18
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] ImportCertificate(int version, byte[] inBlob, SecureString password)
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
					int num2 = <Module>.cli_ImportCertificate(base.BindingHandle, version, num, ptr, ptr3, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_ImportCertificate");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_ImportCertificate");
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

		// Token: 0x06000B59 RID: 2905 RVA: 0x00024874 File Offset: 0x00023C74
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] EnableCertificate(int version, byte[] inBlob)
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
					int num2 = <Module>.cli_EnableCertificate(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
					if (num2 < 0)
					{
						RpcClientBase.ThrowRpcException(num2, "cli_EnableCertificate");
					}
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_EnableCertificate");
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
