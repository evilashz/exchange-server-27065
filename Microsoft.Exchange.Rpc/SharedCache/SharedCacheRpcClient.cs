using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Rpc.SharedCache
{
	// Token: 0x020003EE RID: 1006
	internal class SharedCacheRpcClient : RpcClientBase
	{
		// Token: 0x06001142 RID: 4418 RVA: 0x000567CC File Offset: 0x00055BCC
		public SharedCacheRpcClient(string machineName, int timeoutMilliseconds) : base(machineName)
		{
			try
			{
				base.SetTimeOut(timeoutMilliseconds);
				int num = <Module>.RpcMgmtSetCancelTimeout(0);
				if (num != null)
				{
					RpcClientBase.ThrowRpcException(num, "RpcMgmtSetCancelTimeout");
				}
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00056824 File Offset: 0x00055C24
		[HandleProcessCorruptedStateExceptions]
		public unsafe CacheResponse Get(Guid guid, string key)
		{
			byte* ptr = null;
			int num = 0;
			ushort* ptr2 = <Module>.StringToUnmanaged(key);
			_GUID guid2 = <Module>.ToGUID(ref guid);
			try
			{
				int num2 = <Module>.cli_Get(base.BindingHandle, guid2, ptr2, &num, &ptr);
				if (num2 < 0)
				{
					RpcClientBase.ThrowRpcException(num2, "cli_Get");
				}
				if (num > 0 && ptr != null)
				{
					CacheResponse cacheResponse = SerializationServices.Deserialize<CacheResponse>(ptr, num);
					if (cacheResponse == null)
					{
						RpcClientBase.ThrowRpcException(-2147024883, "cli_Get");
					}
					return cacheResponse;
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_Get");
			}
			finally
			{
				<Module>.FreeString(ptr2);
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
			}
			return null;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00056904 File Offset: 0x00055D04
		[HandleProcessCorruptedStateExceptions]
		public unsafe CacheResponse Insert(Guid guid, string key, byte[] inBlob, long entryValidTime)
		{
			byte* ptr = null;
			int num = 0;
			ushort* ptr2 = <Module>.StringToUnmanaged(key);
			_GUID guid2 = <Module>.ToGUID(ref guid);
			byte* ptr3 = null;
			try
			{
				int num2 = 0;
				ptr3 = <Module>.MToUBytesClient(inBlob, &num2);
				int num3 = <Module>.cli_Insert(base.BindingHandle, guid2, ptr2, num2, ptr3, entryValidTime, &num, &ptr);
				if (num3 < 0)
				{
					RpcClientBase.ThrowRpcException(num3, "cli_Insert");
				}
				if (num > 0 && ptr != null)
				{
					CacheResponse cacheResponse = SerializationServices.Deserialize<CacheResponse>(ptr, num);
					if (cacheResponse == null)
					{
						RpcClientBase.ThrowRpcException(-2147024883, "cli_Insert");
					}
					return cacheResponse;
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_Insert");
			}
			finally
			{
				<Module>.FreeString(ptr2);
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr3 != null)
				{
					<Module>.MIDL_user_free((void*)ptr3);
				}
			}
			return null;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00056A04 File Offset: 0x00055E04
		[HandleProcessCorruptedStateExceptions]
		public unsafe CacheResponse Delete(Guid guid, string key)
		{
			byte* ptr = null;
			int num = 0;
			ushort* ptr2 = <Module>.StringToUnmanaged(key);
			_GUID guid2 = <Module>.ToGUID(ref guid);
			try
			{
				int num2 = <Module>.cli_Delete(base.BindingHandle, guid2, ptr2, &num, &ptr);
				if (num2 < 0)
				{
					RpcClientBase.ThrowRpcException(num2, "cli_Delete");
				}
				if (num > 0 && ptr != null)
				{
					CacheResponse cacheResponse = SerializationServices.Deserialize<CacheResponse>(ptr, num);
					if (cacheResponse == null)
					{
						RpcClientBase.ThrowRpcException(-2147024883, "cli_Delete");
					}
					return cacheResponse;
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_Delete");
			}
			finally
			{
				<Module>.FreeString(ptr2);
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
			}
			return null;
		}
	}
}
