using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Rpc.Common;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x02000118 RID: 280
	internal class AmRpcClient : RpcClientBase
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x00002D84 File Offset: 0x00002184
		public AmRpcClient(string machineName, NetworkCredential networkCredential) : base(machineName, networkCredential)
		{
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00002D70 File Offset: 0x00002170
		public AmRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00002D9C File Offset: 0x0000219C
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo RpccGetServerForDatabase(Guid guid, ref AmDbStatusInfo2 dbInfo)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			_AmRpcDbStatusInfo2 amRpcDbStatusInfo;
			*(ref amRpcDbStatusInfo + 8) = 0;
			amRpcDbStatusInfo = 0L;
			*(ref amRpcDbStatusInfo + 16) = 0L;
			*(ref amRpcDbStatusInfo + 24) = 0L;
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_RpcsAmGetServerForDatabase(base.BindingHandle, guid2, &amRpcDbStatusInfo, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_RpcsAmGetServerForDatabase");
				}
				dbInfo = <Module>.Microsoft.Exchange.Rpc.ActiveManager.?A0x65e5aba0.UToMDbStatusInfo(&amRpcDbStatusInfo);
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_RpcsAmGetServerForDatabase");
			}
			finally
			{
				<Module>.Microsoft.Exchange.Rpc.ActiveManager.?A0x65e5aba0.FreeRpcDbStatusInfo(&amRpcDbStatusInfo);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00002E78 File Offset: 0x00002278
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo MountDatabase(Guid guid, int flags, int mountDialoverride)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmMountDatabase(base.BindingHandle, guid2, flags, mountDialoverride, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_MountDatabase");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_MountDatabase");
			}
			finally
			{
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00002FE0 File Offset: 0x000023E0
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo DismountDatabase(Guid guid, int flags)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmDismountDatabase(base.BindingHandle, guid2, flags, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_MountDatabase");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_MountDatabase");
			}
			finally
			{
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00003090 File Offset: 0x00002490
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo MoveDatabaseEx(Guid guid, int mountFlags, int dismountFlags, int mountDialOverride, string fromServer, string targetServer, int tryOtherHealthyServers, int skipValidationChecks, int actionCode)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			ushort* ptr = <Module>.StringToUnmanaged(fromServer);
			ushort* ptr2 = <Module>.StringToUnmanaged(targetServer);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmMoveDatabaseEx(base.BindingHandle, guid2, mountFlags, dismountFlags, mountDialOverride, ptr, ptr2, tryOtherHealthyServers, skipValidationChecks, actionCode, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_MoveDatabaseEx");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_MoveDatabaseEx");
			}
			finally
			{
				<Module>.FreeString(ptr);
				<Module>.FreeString(ptr2);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000316C File Offset: 0x0000256C
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo MoveDatabaseEx2(Guid guid, int mountFlags, int dismountFlags, int mountDialOverride, string fromServer, string targetServer, int tryOtherHealthyServers, int skipValidationChecks, int actionCode, ref AmDatabaseMoveResult databaseMoveResult)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			byte* ptr = null;
			int num2 = 0;
			databaseMoveResult = null;
			ushort* ptr2 = <Module>.StringToUnmanaged(fromServer);
			ushort* ptr3 = <Module>.StringToUnmanaged(targetServer);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmMoveDatabaseEx2(base.BindingHandle, guid2, mountFlags, dismountFlags, mountDialOverride, ptr2, ptr3, tryOtherHealthyServers, skipValidationChecks, actionCode, &num2, &ptr, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmMoveDatabaseEx2");
				}
				ExTraceGlobals.ActiveManagerTracer.TraceDebug<Guid, int>((long)this.GetHashCode(), "AmRpcClient::MoveDatabaseEx2() for guid {0} returned AmDatabaseMoveResult of {1} bytes.", guid, num2);
				if (num2 > 0 && ptr != null)
				{
					AmDatabaseMoveResult amDatabaseMoveResult = SerializationServices.Deserialize<AmDatabaseMoveResult>(ptr, num2);
					databaseMoveResult = amDatabaseMoveResult;
					if (amDatabaseMoveResult == null)
					{
						num = -2147024883;
						AmRpcException.ThrowRpcException(-2147024883, "cli_AmMoveDatabaseEx2");
					}
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				AmRpcException.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmMoveDatabaseEx2");
			}
			finally
			{
				<Module>.FreeString(ptr2);
				<Module>.FreeString(ptr3);
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000032B0 File Offset: 0x000026B0
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo MoveDatabaseEx3(Guid guid, int mountFlags, int dismountFlags, int mountDialOverride, string fromServer, string targetServer, int tryOtherHealthyServers, int skipValidationChecks, int actionCode, string moveComment, ref AmDatabaseMoveResult databaseMoveResult)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			byte* ptr = null;
			int num2 = 0;
			databaseMoveResult = null;
			ushort* ptr2 = <Module>.StringToUnmanaged(fromServer);
			ushort* ptr3 = <Module>.StringToUnmanaged(targetServer);
			ushort* ptr4 = <Module>.StringToUnmanaged(moveComment);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmMoveDatabaseEx3(base.BindingHandle, guid2, mountFlags, dismountFlags, mountDialOverride, ptr2, ptr3, tryOtherHealthyServers, skipValidationChecks, actionCode, ptr4, &num2, &ptr, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmMoveDatabaseEx3");
				}
				ExTraceGlobals.ActiveManagerTracer.TraceDebug<Guid, int>((long)this.GetHashCode(), "AmRpcClient::MoveDatabaseEx3() for guid {0} returned AmDatabaseMoveResult of {1} bytes.", guid, num2);
				if (num2 > 0 && ptr != null)
				{
					AmDatabaseMoveResult amDatabaseMoveResult = SerializationServices.Deserialize<AmDatabaseMoveResult>(ptr, num2);
					databaseMoveResult = amDatabaseMoveResult;
					if (amDatabaseMoveResult == null)
					{
						num = -2147024883;
						AmRpcException.ThrowRpcException(-2147024883, "cli_AmMoveDatabaseEx3");
					}
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				AmRpcException.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmMoveDatabaseEx3");
			}
			finally
			{
				<Module>.FreeString(ptr2);
				<Module>.FreeString(ptr3);
				<Module>.FreeString(ptr4);
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00003714 File Offset: 0x00002B14
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo AttemptCopyLastLogsDirect(Guid guid, int mountDialOverride, int numRetries, int e00timeoutMs, int networkIOtimeoutMs, int networkConnecttimeoutMs, string sourceServer, int actionCode, ref bool noLoss, ref bool mountAllowed)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			ushort* ptr = <Module>.StringToUnmanaged(sourceServer);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				int num2;
				int num3;
				num = <Module>.cli_AmAttemptCopyLastLogsDirect(base.BindingHandle, guid2, mountDialOverride, numRetries, e00timeoutMs, networkIOtimeoutMs, networkConnecttimeoutMs, ptr, actionCode, &num2, &num3, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmAttemptCopyLastLogs");
				}
				int num4 = (num2 != 0) ? 1 : 0;
				noLoss = (num4 != 0);
				int num5 = (num3 != 0) ? 1 : 0;
				mountAllowed = (num5 != 0);
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmAttemptCopyLastLogs");
			}
			finally
			{
				<Module>.FreeString(ptr);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00003800 File Offset: 0x00002C00
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo AttemptCopyLastLogsDirect2(Guid guid, int mountDialOverride, int numRetries, int e00timeoutMs, int networkIOtimeoutMs, int networkConnecttimeoutMs, string sourceServer, int actionCode, [MarshalAs(UnmanagedType.U1)] bool mountPending, string uniqueOperationId, int subactionAttemptNumber, ref AmAcllReturnStatus acllStatus)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			byte* ptr = null;
			int num2 = 0;
			acllStatus = new AmAcllReturnStatus();
			ushort* ptr2 = <Module>.StringToUnmanaged(sourceServer);
			ushort* ptr3 = <Module>.StringToUnmanaged(uniqueOperationId);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				int num3 = (!mountPending) ? 0 : 1;
				num = <Module>.cli_AmAttemptCopyLastLogsDirect2(base.BindingHandle, guid2, mountDialOverride, numRetries, e00timeoutMs, networkIOtimeoutMs, networkConnecttimeoutMs, ptr2, actionCode, num3, ptr3, subactionAttemptNumber, &num2, &ptr, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmAttemptCopyLastLogs2");
				}
				ExTraceGlobals.ActiveManagerTracer.TraceDebug<Guid, int>((long)this.GetHashCode(), "AmRpcClient::AttemptCopyLastLogsDirect2() for guid {0} returned AcllStatus of {1} bytes.", guid, num2);
				if (num2 > 0 && ptr != null)
				{
					AmAcllReturnStatus amAcllReturnStatus = SerializationServices.Deserialize<AmAcllReturnStatus>(ptr, num2);
					acllStatus = amAcllReturnStatus;
					if (amAcllReturnStatus == null)
					{
						num = -2147024883;
						AmRpcException.ThrowRpcException(-2147024883, "cli_AmAttemptCopyLastLogs2");
					}
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmAttemptCopyLastLogs2");
			}
			finally
			{
				<Module>.FreeString(ptr2);
				<Module>.FreeString(ptr3);
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00003954 File Offset: 0x00002D54
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo AttemptCopyLastLogsDirect3(Guid guid, int mountDialOverride, int numRetries, int e00timeoutMs, int networkIOtimeoutMs, int networkConnecttimeoutMs, string sourceServer, int actionCode, int skipValidationChecks, [MarshalAs(UnmanagedType.U1)] bool mountPending, string uniqueOperationId, int subactionAttemptNumber, ref AmAcllReturnStatus acllStatus)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			byte* ptr = null;
			int num2 = 0;
			acllStatus = new AmAcllReturnStatus();
			ushort* ptr2 = <Module>.StringToUnmanaged(sourceServer);
			ushort* ptr3 = <Module>.StringToUnmanaged(uniqueOperationId);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				int num3 = (!mountPending) ? 0 : 1;
				num = <Module>.cli_AmAttemptCopyLastLogsDirect3(base.BindingHandle, guid2, mountDialOverride, numRetries, e00timeoutMs, networkIOtimeoutMs, networkConnecttimeoutMs, ptr2, actionCode, skipValidationChecks, num3, ptr3, subactionAttemptNumber, &num2, &ptr, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmAttemptCopyLastLogs3");
				}
				ExTraceGlobals.ActiveManagerTracer.TraceDebug<Guid, int>((long)this.GetHashCode(), "AmRpcClient::AttemptCopyLastLogsDirect3() for guid {0} returned AcllStatus of {1} bytes.", guid, num2);
				if (num2 > 0 && ptr != null)
				{
					AmAcllReturnStatus amAcllReturnStatus = SerializationServices.Deserialize<AmAcllReturnStatus>(ptr, num2);
					acllStatus = amAcllReturnStatus;
					if (amAcllReturnStatus == null)
					{
						num = -2147024883;
						AmRpcException.ThrowRpcException(-2147024883, "cli_AmAttemptCopyLastLogs3");
					}
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmAttemptCopyLastLogs3");
			}
			finally
			{
				<Module>.FreeString(ptr2);
				<Module>.FreeString(ptr3);
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00003574 File Offset: 0x00002974
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo MountDatabaseDirectEx(Guid guid, AmMountArg arg)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			__MIDL_IActiveManagerRpc_0002 _MIDL_IActiveManagerRpc_;
			<Module>.Microsoft.Exchange.Rpc.ActiveManager.?A0x65e5aba0.MToUMountArg2(arg, &_MIDL_IActiveManagerRpc_);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmMountDatabaseDirectEx(base.BindingHandle, guid2, &_MIDL_IActiveManagerRpc_, 24, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmMountDatabaseDirectEx");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmMountDatabaseDirectEx");
			}
			finally
			{
				if (*(ref _MIDL_IActiveManagerRpc_ + 8) != 0L)
				{
					<Module>.FreeString(*(ref _MIDL_IActiveManagerRpc_ + 8));
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00003AAC File Offset: 0x00002EAC
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo DismountDatabaseDirect(Guid guid, AmDismountArg arg)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			__MIDL_IActiveManagerRpc_0004 flags;
			if (arg != null)
			{
				flags = arg.Flags;
				*(ref flags + 4) = arg.Reason;
			}
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmDismountDatabaseDirect(base.BindingHandle, guid2, &flags, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmDismountDatabaseDirect");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmDismountDatabaseDirect");
			}
			finally
			{
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000034AC File Offset: 0x000028AC
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo GetPrimaryActiveManager(ref AmPamInfo pamInfo)
		{
			int num = 0;
			__MIDL_IActiveManagerRpc_0001 _MIDL_IActiveManagerRpc_ = 0L;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				num = <Module>.cli_AmGetPrimaryActiveManager(base.BindingHandle, &_MIDL_IActiveManagerRpc_, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_GetPrimaryActiveManager");
				}
				pamInfo = <Module>.Microsoft.Exchange.Rpc.ActiveManager.?A0x65e5aba0.UToMPamInfo(&_MIDL_IActiveManagerRpc_);
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_GetPrimaryActiveManager");
			}
			finally
			{
				if (_MIDL_IActiveManagerRpc_ != null)
				{
					<Module>.FreeString(_MIDL_IActiveManagerRpc_);
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00003404 File Offset: 0x00002804
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo IsRunning()
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				num = <Module>.cli_AmIsRunning(base.BindingHandle, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_IsRunning in try");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				AmRpcException.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_IsRunning in except");
			}
			finally
			{
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00003B78 File Offset: 0x00002F78
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo ServerSwitchOver(string sourceServer)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			ushort* ptr = <Module>.StringToUnmanaged(sourceServer);
			try
			{
				num = <Module>.cli_AmServerSwitchOver(base.BindingHandle, ptr, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmServerSwitchOver");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmServerSwitchOver");
			}
			finally
			{
				<Module>.FreeString(ptr);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00003C2C File Offset: 0x0000302C
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo GetActiveManagerRole(ref AmRole amRole, ref string errorMessage)
		{
			int num = 0;
			int num2 = 1;
			ushort* pStr = null;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				num = <Module>.cli_AmGetActiveManagerRole(base.BindingHandle, &num2, &pStr, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmGetActiveManagerRole");
				}
				amRole = (AmRole)num2;
				errorMessage = <Module>.UToMString(pStr);
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmGetActiveManagerRole");
			}
			finally
			{
				<Module>.FreeString(pStr);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00003CEC File Offset: 0x000030EC
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo CheckThirdPartyListener(ref bool healthy, ref string errorMessage)
		{
			int num = 0;
			ushort* pStr = null;
			int num2 = 0;
			healthy = false;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				num = <Module>.cli_AmCheckThirdPartyListener(base.BindingHandle, &num2, &pStr, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmCheckThirdPartyListener");
				}
				int num3 = (num2 != 0) ? 1 : 0;
				healthy = (num3 != 0);
				errorMessage = <Module>.UToMString(pStr);
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmCheckThirdPartyListener");
			}
			finally
			{
				<Module>.FreeString(pStr);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00003DBC File Offset: 0x000031BC
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo ReportSystemEvent(int eventCode, string reportingServer)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			ushort* ptr = <Module>.StringToUnmanaged(reportingServer);
			try
			{
				num = <Module>.cli_AmReportSystemEvent(base.BindingHandle, eventCode, ptr, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmReportSystemEvent");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmReportSystemEvent");
			}
			finally
			{
				<Module>.FreeString(ptr);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00003F1C File Offset: 0x0000331C
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo RemountDatabase(Guid guid, int mountFlags, int mountDialOverride, string fromServer)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			ushort* ptr = <Module>.StringToUnmanaged(fromServer);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmRemountDatabase(base.BindingHandle, guid2, mountFlags, mountDialOverride, ptr, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_RemountDatabase");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_RemountDatabase");
			}
			finally
			{
				<Module>.FreeString(ptr);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00003FDC File Offset: 0x000033DC
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo ServerMoveAllDatabases(string sourceServer, string targetServer, int mountFlags, int dismountFlags, int mountDialOverride, int tryOtherHealthyServers, int skipValidationChecks, int actionCode, ref List<AmDatabaseMoveResult> databaseMoveResults)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			databaseMoveResults = null;
			ushort* ptr = <Module>.StringToUnmanaged(sourceServer);
			ushort* ptr2 = <Module>.StringToUnmanaged(targetServer);
			int num2 = 0;
			int num3 = 0;
			byte* ptr3 = null;
			try
			{
				num = <Module>.cli_AmServerMoveAllDatabases(base.BindingHandle, ptr, ptr2, mountFlags, dismountFlags, mountDialOverride, tryOtherHealthyServers, skipValidationChecks, actionCode, &num3, &ptr3, &num2, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmServerMoveAllDatabases");
				}
				ExTraceGlobals.ActiveManagerTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "AmRpcClient::ServerMoveAllDatabases() for source server '{0}', returned {1} AmDatabaseMoveResult objects for a total size of {2} bytes.", sourceServer, num2, num3);
				if (num3 > 0 && ptr3 != null)
				{
					List<AmDatabaseMoveResult> list = SerializationServices.Deserialize<List<AmDatabaseMoveResult>>(ptr3, num3);
					databaseMoveResults = list;
					if (list == null || list.Count != num2)
					{
						num = -2147024883;
						AmRpcException.ThrowRpcException(-2147024883, "cli_AmServerMoveAllDatabases");
					}
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmServerMoveAllDatabases");
			}
			finally
			{
				<Module>.FreeString(ptr);
				<Module>.FreeString(ptr2);
				if (ptr3 != null)
				{
					<Module>.MIDL_user_free((void*)ptr3);
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00004124 File Offset: 0x00003524
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo ServerMoveAllDatabases2(string sourceServer, string targetServer, int mountFlags, int dismountFlags, int mountDialOverride, int tryOtherHealthyServers, int skipValidationChecks, int actionCode, string moveComment, ref List<AmDatabaseMoveResult> databaseMoveResults)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			databaseMoveResults = null;
			ushort* ptr = <Module>.StringToUnmanaged(sourceServer);
			ushort* ptr2 = <Module>.StringToUnmanaged(targetServer);
			ushort* ptr3 = <Module>.StringToUnmanaged(moveComment);
			int num2 = 0;
			int num3 = 0;
			byte* ptr4 = null;
			try
			{
				num = <Module>.cli_AmServerMoveAllDatabases2(base.BindingHandle, ptr, ptr2, mountFlags, dismountFlags, mountDialOverride, tryOtherHealthyServers, skipValidationChecks, actionCode, ptr3, &num3, &ptr4, &num2, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmServerMoveAllDatabases2");
				}
				ExTraceGlobals.ActiveManagerTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "AmRpcClient::ServerMoveAllDatabases2() for source server '{0}', returned {1} AmDatabaseMoveResult objects for a total size of {2} bytes.", sourceServer, num2, num3);
				if (num3 > 0 && ptr4 != null)
				{
					List<AmDatabaseMoveResult> list = SerializationServices.Deserialize<List<AmDatabaseMoveResult>>(ptr4, num3);
					databaseMoveResults = list;
					if (list == null || list.Count != num2)
					{
						num = -2147024883;
						AmRpcException.ThrowRpcException(-2147024883, "cli_AmServerMoveAllDatabases2");
					}
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmServerMoveAllDatabases2");
			}
			finally
			{
				<Module>.FreeString(ptr);
				<Module>.FreeString(ptr2);
				<Module>.FreeString(ptr3);
				if (ptr4 != null)
				{
					<Module>.MIDL_user_free((void*)ptr4);
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000427C File Offset: 0x0000367C
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo ServerMoveAllDatabases3(string sourceServer, string targetServer, int mountFlags, int dismountFlags, int mountDialOverride, int tryOtherHealthyServers, int skipValidationChecks, int actionCode, string moveComment, string componentName, ref List<AmDatabaseMoveResult> databaseMoveResults)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			databaseMoveResults = null;
			ushort* ptr = <Module>.StringToUnmanaged(sourceServer);
			ushort* ptr2 = <Module>.StringToUnmanaged(targetServer);
			ushort* ptr3 = <Module>.StringToUnmanaged(moveComment);
			ushort* ptr4 = <Module>.StringToUnmanaged(componentName);
			int num2 = 0;
			int num3 = 0;
			byte* ptr5 = null;
			try
			{
				num = <Module>.cli_AmServerMoveAllDatabases3(base.BindingHandle, ptr, ptr2, mountFlags, dismountFlags, mountDialOverride, tryOtherHealthyServers, skipValidationChecks, actionCode, ptr3, ptr4, &num3, &ptr5, &num2, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmServerMoveAllDatabases3");
				}
				ExTraceGlobals.ActiveManagerTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "AmRpcClient::ServerMoveAllDatabases2() for source server '{0}', returned {1} AmDatabaseMoveResult objects for a total size of {2} bytes.", sourceServer, num2, num3);
				if (num3 > 0 && ptr5 != null)
				{
					List<AmDatabaseMoveResult> list = SerializationServices.Deserialize<List<AmDatabaseMoveResult>>(ptr5, num3);
					databaseMoveResults = list;
					if (list == null || list.Count != num2)
					{
						num = -2147024883;
						AmRpcException.ThrowRpcException(-2147024883, "cli_AmServerMoveAllDatabases3");
					}
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmServerMoveAllDatabases2");
			}
			finally
			{
				<Module>.FreeString(ptr);
				<Module>.FreeString(ptr2);
				<Module>.FreeString(ptr3);
				<Module>.FreeString(ptr4);
				if (ptr5 != null)
				{
					<Module>.MIDL_user_free((void*)ptr5);
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000043E8 File Offset: 0x000037E8
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo RpccGetAutomountConsensusState(ref int automountConsensusState)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				int num2;
				num = <Module>.cli_RpcsAmGetAutomountConsensusState(base.BindingHandle, &num2, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_RpcsAmGetAutomountConsensusState");
				}
				automountConsensusState = num2;
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_RpcsAmGetAutomountConsensusState");
			}
			finally
			{
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00004494 File Offset: 0x00003894
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo RpccSetAutomountConsensusState(int automountConsensusState)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				num = <Module>.cli_RpcsAmSetAutomountConsensusState(base.BindingHandle, automountConsensusState, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_RpcsAmSetAutomountConsensusState");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_RpcsAmSetAutomountConsensusState");
			}
			finally
			{
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00003E74 File Offset: 0x00003274
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo AmRefreshConfiguration(int refreshFlags, int maxSecondsToWait)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				num = <Module>.cli_AmRefreshConfiguration(base.BindingHandle, refreshFlags, maxSecondsToWait, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmRefreshConfiguration");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmRefreshConfiguration");
			}
			finally
			{
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00002F2C File Offset: 0x0000232C
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo MountDatabase3(Guid guid, int storeFlags, int amFlags, int mountDialoverride)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmMountDatabase3(base.BindingHandle, guid2, storeFlags, amFlags, mountDialoverride, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_MountDatabase3");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_MountDatabase3");
			}
			finally
			{
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00003644 File Offset: 0x00002A44
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo MountDatabaseDirect3(Guid guid, AmMountArg arg)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			__MIDL_IActiveManagerRpc_0003 _MIDL_IActiveManagerRpc_;
			<Module>.Microsoft.Exchange.Rpc.ActiveManager.?A0x65e5aba0.MToUMountArg3(arg, &_MIDL_IActiveManagerRpc_);
			try
			{
				_GUID guid2 = <Module>.ToGUID(ref guid);
				num = <Module>.cli_AmMountDatabaseDirect3(base.BindingHandle, guid2, &_MIDL_IActiveManagerRpc_, 24, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmMountDatabaseDirect3");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmMountDatabaseDirect3");
			}
			finally
			{
				if (*(ref _MIDL_IActiveManagerRpc_ + 8) != 0L)
				{
					<Module>.FreeString(*(ref _MIDL_IActiveManagerRpc_ + 8));
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000453C File Offset: 0x0000393C
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo ReportServiceKill(string serviceName, string serverName, string timeStampStrInUtc)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			ushort* ptr = <Module>.StringToUnmanaged(serviceName);
			ushort* ptr2 = <Module>.StringToUnmanaged(serverName);
			ushort* ptr3 = <Module>.StringToUnmanaged(timeStampStrInUtc);
			try
			{
				num = <Module>.cli_AmReportServiceKill(base.BindingHandle, ptr, ptr2, ptr3, &tagErrorExceptionInfo);
				if (num < 0)
				{
					AmRpcException.ThrowRpcException(num, "cli_AmReportServiceKill");
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmReportServiceKill");
			}
			finally
			{
				<Module>.FreeString(ptr);
				<Module>.FreeString(ptr2);
				<Module>.FreeString(ptr3);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00004610 File Offset: 0x00003A10
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo GetDeferredRecoveryEntries(ref List<AmDeferredRecoveryEntry> entries)
		{
			int num = 0;
			ExTraceGlobals.ActiveManagerTracer.TraceDebug(0L, "AmRpcClient::GetDeferredRecoveryEntries() entering");
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			entries = null;
			int num2 = 0;
			int num3 = 0;
			byte* ptr = null;
			try
			{
				num = <Module>.cli_AmGetDeferredRecoveryEntries(base.BindingHandle, &num3, &ptr, &num2, &tagErrorExceptionInfo);
				if (num < 0)
				{
					ExTraceGlobals.ActiveManagerTracer.TraceDebug<int>(0L, "AmRpcClient: cli_AmGetDeferredRecoveryEntries() failed with {0}", num);
					AmRpcException.ThrowRpcException(num, "cli_AmGetDeferredRecoveryEntries");
				}
				else
				{
					ExTraceGlobals.ActiveManagerTracer.TraceDebug<int, int>(0L, "AmRpcClient: cli_AmGetDeferredRecoveryEntries() success. (cbEntries: {0}, cEntriesCount: {1})", num3, num2);
				}
				if (num3 > 0 && ptr != null)
				{
					List<AmDeferredRecoveryEntry> list = SerializationServices.Deserialize<List<AmDeferredRecoveryEntry>>(ptr, num3);
					entries = list;
					string arg;
					if (list != null)
					{
						arg = list.Count.ToString();
					}
					else
					{
						arg = "<nullptr>";
					}
					ExTraceGlobals.ActiveManagerTracer.TraceDebug<string>(0L, "AmRpcClient::AmGetDeferredRecoveryEntries - Deserialize() finished. (count:{0})", arg);
					List<AmDeferredRecoveryEntry> list2 = entries;
					if (list2 == null || list2.Count != num2)
					{
						num = -2147024883;
						AmRpcException.ThrowRpcException(-2147024883, "cli_AmGetDeferredRecoveryEntries");
					}
				}
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_AmGetDeferredRecoveryEntries");
			}
			finally
			{
				ExTraceGlobals.ActiveManagerTracer.TraceDebug(0L, "AmRpcClient::AmGetDeferredRecoveryEntries - Before cleanup");
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
				ExTraceGlobals.ActiveManagerTracer.TraceDebug(0L, "AmRpcClient::AmGetDeferredRecoveryEntries - After cleanup");
			}
			return null;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00002858 File Offset: 0x00001C58
		[HandleProcessCorruptedStateExceptions]
		public unsafe RpcErrorExceptionInfo GenericRequest(RpcGenericRequestInfo requestInfo, out RpcGenericReplyInfo replyInfo)
		{
			int num = 0;
			tagErrorExceptionInfo tagErrorExceptionInfo;
			<Module>.InitErrorExceptionInfo(ref tagErrorExceptionInfo);
			tagGenericRequestInfo tagGenericRequestInfo;
			<Module>.Microsoft.Exchange.Rpc.Common.InitGenericRequestInfo(ref tagGenericRequestInfo);
			tagGenericReplyInfo tagGenericReplyInfo;
			<Module>.Microsoft.Exchange.Rpc.Common.InitGenericReplyInfo(ref tagGenericReplyInfo);
			try
			{
				num = <Module>.Microsoft.Exchange.Rpc.Common.MToUGenericRequestInfo(requestInfo, &tagGenericRequestInfo);
				if (num < 0)
				{
					RpcClientBase.ThrowRpcException(num, "GenericRequest.MToUGenericRequest");
				}
				num = <Module>.cli_AmGenericRequest(base.BindingHandle, &tagGenericRequestInfo, &tagGenericReplyInfo, &tagErrorExceptionInfo);
				if (num < 0)
				{
					RpcClientBase.ThrowRpcException(num, "cli_GenericRequest");
				}
				object[] args = new object[]
				{
					tagGenericReplyInfo,
					*(ref tagGenericReplyInfo + 4),
					*(ref tagGenericReplyInfo + 8),
					*(ref tagGenericReplyInfo + 12),
					*(ref tagGenericReplyInfo + 16)
				};
				ExTraceGlobals.ActiveManagerTracer.TraceDebug((long)this.GetHashCode(), "AmRpcClient::cli_GenericRequest() returned sver={0} cmd={1} majorver={2} minorver={3} dataSize={4}.", args);
				replyInfo = <Module>.Microsoft.Exchange.Rpc.Common.UToMGenericReplyInfo(ref tagGenericReplyInfo);
				return <Module>.UToMErrorExceptionInfo(ref tagErrorExceptionInfo);
			}
			catch when (endfilter(<Module>.DwExRpcExceptionFilter(Marshal.GetExceptionCode()) != null))
			{
				RpcClientBase.ThrowRpcException((num >= 0) ? Marshal.GetExceptionCode() : num, "cli_GenericRequest");
			}
			finally
			{
				<Module>.Microsoft.Exchange.Rpc.Common.FreeGenericRequestInfo(&tagGenericRequestInfo);
				<Module>.Microsoft.Exchange.Rpc.Common.FreeGenericReplyInfo(&tagGenericReplyInfo);
				<Module>.FreeErrorExceptionInfo(&tagErrorExceptionInfo);
			}
			return null;
		}
	}
}
