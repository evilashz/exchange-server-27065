using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Exchange.Rpc.Throttling
{
	// Token: 0x020003F9 RID: 1017
	internal class ThrottlingRpcClient : RpcClientBase
	{
		// Token: 0x06001168 RID: 4456 RVA: 0x00057320 File Offset: 0x00056720
		public ThrottlingRpcClient(string machineName) : base(machineName)
		{
			try
			{
				this.m_refCount = 1;
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00057364 File Offset: 0x00056764
		[HandleProcessCorruptedStateExceptions]
		[return: MarshalAs(UnmanagedType.U1)]
		public bool ObtainSubmissionTokens(Guid mailboxGuid, int requestedTokenCount, int totalTokenCount, int submissionType)
		{
			int num = 1;
			try
			{
				_GUID guid = <Module>.ToGUID(ref mailboxGuid);
				num = <Module>.cli_ObtainSubmissionTokens(base.BindingHandle, guid, requestedTokenCount, totalTokenCount, submissionType);
				return ((num == 1) ? 1 : 0) != 0;
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "ObtainSubmissionTokens");
			}
			finally
			{
			}
			return ((num == 1) ? 1 : 0) != 0;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000573EC File Offset: 0x000567EC
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] ObtainTokens(byte[] inBytes)
		{
			byte[] result = null;
			IntPtr hglobal = IntPtr.Zero;
			byte* ptr = null;
			int cBytes = 0;
			try
			{
				bool flag = false;
				do
				{
					try
					{
						int num = 0;
						hglobal = <Module>.MToUBytes(inBytes, &num);
						<Module>.cli_ObtainTokens(base.BindingHandle, num, (byte*)hglobal.ToPointer(), &cBytes, &ptr);
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
							RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "ObtainTokens");
						}
					}
				}
				while (flag);
				result = <Module>.UToMBytes(cBytes, ptr);
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
			}
			return result;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000572EC File Offset: 0x000566EC
		public void AddRef()
		{
			Interlocked.Increment(ref this.m_refCount);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00057308 File Offset: 0x00056708
		public int RemoveRef()
		{
			return Interlocked.Decrement(ref this.m_refCount);
		}

		// Token: 0x0400102A RID: 4138
		private int m_refCount;

		// Token: 0x0400102B RID: 4139
		public static int RpcServerTooBusy = 1723;
	}
}
