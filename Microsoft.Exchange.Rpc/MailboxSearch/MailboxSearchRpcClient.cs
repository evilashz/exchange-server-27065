using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x0200028A RID: 650
	internal class MailboxSearchRpcClient : RpcClientBase
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x0002ABD4 File Offset: 0x00029FD4
		public MailboxSearchRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002ABE8 File Offset: 0x00029FE8
		[HandleProcessCorruptedStateExceptions]
		protected unsafe void Start(SearchId searchId, Guid ownerGuid, out SearchErrorInfo errorInfo)
		{
			__MIDL_IMailboxSearch_0001 _MIDL_IMailboxSearch_;
			<Module>.InitRpcSearchId(&_MIDL_IMailboxSearch_);
			<Module>.MToUSearchId(searchId, &_MIDL_IMailboxSearch_);
			__MIDL_IMailboxSearch_0003 _MIDL_IMailboxSearch_2;
			<Module>.InitRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			try
			{
				_GUID guid = <Module>.ToGUID(ref ownerGuid);
				int num = <Module>.cli_Start(base.BindingHandle, &_MIDL_IMailboxSearch_, guid, &_MIDL_IMailboxSearch_2);
				if (num < 0)
				{
					RpcExceptionHelper.ThrowRpcException(num, "cli_Start");
				}
				errorInfo = <Module>.UToMErrorInfo(&_MIDL_IMailboxSearch_2);
			}
			catch when (endfilter(true))
			{
				RpcExceptionHelper.ThrowRpcException(Marshal.GetExceptionCode(), "cli_Start");
			}
			finally
			{
				<Module>.FreeRpcSearchId(&_MIDL_IMailboxSearch_);
				<Module>.FreeRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002AC9C File Offset: 0x0002A09C
		[HandleProcessCorruptedStateExceptions]
		protected unsafe SearchStatus GetStatus(SearchId searchId, out SearchErrorInfo errorInfo)
		{
			__MIDL_IMailboxSearch_0001 _MIDL_IMailboxSearch_;
			<Module>.InitRpcSearchId(&_MIDL_IMailboxSearch_);
			<Module>.MToUSearchId(searchId, &_MIDL_IMailboxSearch_);
			__MIDL_IMailboxSearch_0002 _MIDL_IMailboxSearch_2;
			<Module>.InitRpcSearchStatus(&_MIDL_IMailboxSearch_2);
			__MIDL_IMailboxSearch_0003 _MIDL_IMailboxSearch_3;
			<Module>.InitRpcErrorInfo(&_MIDL_IMailboxSearch_3);
			try
			{
				int num = <Module>.cli_GetStatus(base.BindingHandle, &_MIDL_IMailboxSearch_, &_MIDL_IMailboxSearch_2, &_MIDL_IMailboxSearch_3);
				if (num < 0)
				{
					RpcExceptionHelper.ThrowRpcException(num, "cli_GetStatus");
				}
				errorInfo = <Module>.UToMErrorInfo(&_MIDL_IMailboxSearch_3);
				return <Module>.UToMSearchStatus(&_MIDL_IMailboxSearch_2);
			}
			catch when (endfilter(true))
			{
				RpcExceptionHelper.ThrowRpcException(Marshal.GetExceptionCode(), "cli_GetStatus");
			}
			finally
			{
				<Module>.FreeRpcSearchId(&_MIDL_IMailboxSearch_);
				<Module>.FreeRpcErrorInfo(&_MIDL_IMailboxSearch_3);
				<Module>.FreeRpcSearchStatus(&_MIDL_IMailboxSearch_2);
			}
			return null;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002AD64 File Offset: 0x0002A164
		[HandleProcessCorruptedStateExceptions]
		protected unsafe void Abort(SearchId searchId, Guid userGuid, out SearchErrorInfo errorInfo)
		{
			__MIDL_IMailboxSearch_0001 _MIDL_IMailboxSearch_;
			<Module>.InitRpcSearchId(&_MIDL_IMailboxSearch_);
			<Module>.MToUSearchId(searchId, &_MIDL_IMailboxSearch_);
			__MIDL_IMailboxSearch_0003 _MIDL_IMailboxSearch_2;
			<Module>.InitRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			try
			{
				_GUID guid = <Module>.ToGUID(ref userGuid);
				int num = <Module>.cli_Abort(base.BindingHandle, &_MIDL_IMailboxSearch_, guid, &_MIDL_IMailboxSearch_2);
				if (num < 0)
				{
					RpcExceptionHelper.ThrowRpcException(num, "cli_Abort");
				}
				errorInfo = <Module>.UToMErrorInfo(&_MIDL_IMailboxSearch_2);
			}
			catch when (endfilter(true))
			{
				RpcExceptionHelper.ThrowRpcException(Marshal.GetExceptionCode(), "cli_Abort");
			}
			finally
			{
				<Module>.FreeRpcSearchId(&_MIDL_IMailboxSearch_);
				<Module>.FreeRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002AE18 File Offset: 0x0002A218
		[HandleProcessCorruptedStateExceptions]
		protected unsafe void Remove(SearchId searchId, [MarshalAs(UnmanagedType.U1)] bool removeLogs, out SearchErrorInfo errorInfo)
		{
			__MIDL_IMailboxSearch_0001 _MIDL_IMailboxSearch_;
			<Module>.InitRpcSearchId(&_MIDL_IMailboxSearch_);
			<Module>.MToUSearchId(searchId, &_MIDL_IMailboxSearch_);
			__MIDL_IMailboxSearch_0003 _MIDL_IMailboxSearch_2;
			<Module>.InitRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			try
			{
				int num = removeLogs ? 1 : 0;
				int num2 = <Module>.cli_Remove(base.BindingHandle, &_MIDL_IMailboxSearch_, num, &_MIDL_IMailboxSearch_2);
				if (num2 < 0)
				{
					RpcExceptionHelper.ThrowRpcException(num2, "cli_Remove");
				}
				errorInfo = <Module>.UToMErrorInfo(&_MIDL_IMailboxSearch_2);
			}
			catch when (endfilter(true))
			{
				RpcExceptionHelper.ThrowRpcException(Marshal.GetExceptionCode(), "cli_Remove");
			}
			finally
			{
				<Module>.FreeRpcSearchId(&_MIDL_IMailboxSearch_);
				<Module>.FreeRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002AECC File Offset: 0x0002A2CC
		[HandleProcessCorruptedStateExceptions]
		protected unsafe void StartEx(SearchId searchId, string ownerId, out SearchErrorInfo errorInfo)
		{
			__MIDL_IMailboxSearch_0001 _MIDL_IMailboxSearch_;
			<Module>.InitRpcSearchId(&_MIDL_IMailboxSearch_);
			<Module>.MToUSearchId(searchId, &_MIDL_IMailboxSearch_);
			ushort* ptr = <Module>.StringToUnmanaged(ownerId);
			__MIDL_IMailboxSearch_0003 _MIDL_IMailboxSearch_2;
			<Module>.InitRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			try
			{
				int num = <Module>.cli_StartEx(base.BindingHandle, &_MIDL_IMailboxSearch_, ptr, &_MIDL_IMailboxSearch_2);
				if (num < 0)
				{
					RpcExceptionHelper.ThrowRpcException(num, "cli_StartEx");
				}
				errorInfo = <Module>.UToMErrorInfo(&_MIDL_IMailboxSearch_2);
			}
			catch when (endfilter(true))
			{
				RpcExceptionHelper.ThrowRpcException(Marshal.GetExceptionCode(), "cli_StartEx");
			}
			finally
			{
				<Module>.FreeRpcSearchId(&_MIDL_IMailboxSearch_);
				<Module>.FreeRpcErrorInfo(&_MIDL_IMailboxSearch_2);
				<Module>.FreeString(ptr);
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002AF84 File Offset: 0x0002A384
		[HandleProcessCorruptedStateExceptions]
		protected unsafe void AbortEx(SearchId searchId, string userId, out SearchErrorInfo errorInfo)
		{
			__MIDL_IMailboxSearch_0001 _MIDL_IMailboxSearch_;
			<Module>.InitRpcSearchId(&_MIDL_IMailboxSearch_);
			<Module>.MToUSearchId(searchId, &_MIDL_IMailboxSearch_);
			ushort* ptr = <Module>.StringToUnmanaged(userId);
			__MIDL_IMailboxSearch_0003 _MIDL_IMailboxSearch_2;
			<Module>.InitRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			try
			{
				int num = <Module>.cli_AbortEx(base.BindingHandle, &_MIDL_IMailboxSearch_, ptr, &_MIDL_IMailboxSearch_2);
				if (num < 0)
				{
					RpcExceptionHelper.ThrowRpcException(num, "cli_AbortEx");
				}
				errorInfo = <Module>.UToMErrorInfo(&_MIDL_IMailboxSearch_2);
			}
			catch when (endfilter(true))
			{
				RpcExceptionHelper.ThrowRpcException(Marshal.GetExceptionCode(), "cli_AbortEx");
			}
			finally
			{
				<Module>.FreeRpcSearchId(&_MIDL_IMailboxSearch_);
				<Module>.FreeRpcErrorInfo(&_MIDL_IMailboxSearch_2);
				<Module>.FreeString(ptr);
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002B03C File Offset: 0x0002A43C
		[HandleProcessCorruptedStateExceptions]
		protected unsafe void UpdateStatus(SearchId searchId, out SearchErrorInfo errorInfo)
		{
			__MIDL_IMailboxSearch_0001 _MIDL_IMailboxSearch_;
			<Module>.InitRpcSearchId(&_MIDL_IMailboxSearch_);
			<Module>.MToUSearchId(searchId, &_MIDL_IMailboxSearch_);
			__MIDL_IMailboxSearch_0003 _MIDL_IMailboxSearch_2;
			<Module>.InitRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			try
			{
				int num = <Module>.cli_UpdateStatus(base.BindingHandle, &_MIDL_IMailboxSearch_, &_MIDL_IMailboxSearch_2);
				if (num < 0)
				{
					RpcExceptionHelper.ThrowRpcException(num, "cli_UpdateStatus");
				}
				errorInfo = <Module>.UToMErrorInfo(&_MIDL_IMailboxSearch_2);
			}
			catch when (endfilter(true))
			{
				RpcExceptionHelper.ThrowRpcException(Marshal.GetExceptionCode(), "cli_UpdateStatus");
			}
			finally
			{
				<Module>.FreeRpcSearchId(&_MIDL_IMailboxSearch_);
				<Module>.FreeRpcErrorInfo(&_MIDL_IMailboxSearch_2);
			}
		}
	}
}
