using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Assistants
{
	// Token: 0x020001C2 RID: 450
	internal class AssistantsRpcClient : RpcClientBase
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x00015FFC File Offset: 0x000153FC
		public AssistantsRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00016010 File Offset: 0x00015410
		[HandleProcessCorruptedStateExceptions]
		public unsafe void Start(string assistantName, ValueType mailboxGuid, ValueType mdbGuid)
		{
			IntPtr hglobal = 0;
			int num = 0;
			try
			{
				try
				{
					hglobal = Marshal.StringToHGlobalAnsi(assistantName);
					sbyte* ptr = (sbyte*)hglobal.ToPointer();
					_GUID guid = <Module>.Microsoft.Exchange.Rpc.?A0x40f01437.GUIDFromGuid(mdbGuid);
					_GUID guid2 = <Module>.Microsoft.Exchange.Rpc.?A0x40f01437.GUIDFromGuid(mailboxGuid);
					num = <Module>.cli_RunNowHR(base.BindingHandle, (sbyte*)ptr, guid2, guid);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "Start");
				}
				if (num < 0)
				{
					RpcClientBase.ThrowRpcException(num, "Start");
				}
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000160BC File Offset: 0x000154BC
		[HandleProcessCorruptedStateExceptions]
		public unsafe void Stop(string assistantName)
		{
			IntPtr hglobal = 0;
			int num = 0;
			try
			{
				try
				{
					hglobal = Marshal.StringToHGlobalAnsi(assistantName);
					sbyte* ptr = (sbyte*)hglobal.ToPointer();
					num = <Module>.cli_HaltHR(base.BindingHandle, (sbyte*)ptr);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "Stop");
				}
				if (num < 0)
				{
					RpcClientBase.ThrowRpcException(num, "Stop");
				}
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00016154 File Offset: 0x00015554
		[HandleProcessCorruptedStateExceptions]
		public unsafe void StartWithParams(string assistantName, ValueType mailboxGuid, ValueType mdbGuid, string parameters)
		{
			IntPtr hglobal = 0;
			IntPtr hglobal2 = 0;
			int num = 0;
			try
			{
				try
				{
					hglobal = Marshal.StringToHGlobalAnsi(assistantName);
					hglobal2 = Marshal.StringToHGlobalAnsi(parameters);
					sbyte* ptr = (sbyte*)hglobal.ToPointer();
					sbyte* ptr2 = (sbyte*)hglobal2.ToPointer();
					_GUID guid = <Module>.Microsoft.Exchange.Rpc.?A0x40f01437.GUIDFromGuid(mdbGuid);
					_GUID guid2 = <Module>.Microsoft.Exchange.Rpc.?A0x40f01437.GUIDFromGuid(mailboxGuid);
					num = <Module>.cli_RunNowWithParamsHR(base.BindingHandle, (sbyte*)ptr, guid2, guid, (sbyte*)ptr2);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "StartWithParams");
				}
				if (num < 0)
				{
					RpcClientBase.ThrowRpcException(num, "StartWithParams");
				}
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
				Marshal.FreeHGlobal(hglobal2);
			}
		}
	}
}
