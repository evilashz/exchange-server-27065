using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.SharedCache;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.SharedCache;

namespace Microsoft.Exchange.SharedCache.Client
{
	// Token: 0x02000007 RID: 7
	public class SharedCacheClient
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021EB File Offset: 0x000003EB
		public SharedCacheClient(Guid cacheGuid, string clientName, int timeoutMilliseconds) : this(cacheGuid, clientName, timeoutMilliseconds, false, null)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021F8 File Offset: 0x000003F8
		public SharedCacheClient(Guid cacheGuid, string clientName, int timeoutMilliseconds, bool throwRpcExceptions) : this(cacheGuid, clientName, timeoutMilliseconds, throwRpcExceptions, null)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002206 File Offset: 0x00000406
		public SharedCacheClient(Guid cacheGuid, string clientName, int timeoutMilliseconds, IConcurrencyGuard concurrencyGuard) : this(cacheGuid, clientName, timeoutMilliseconds, false, concurrencyGuard)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002214 File Offset: 0x00000414
		public SharedCacheClient(Guid cacheGuid, string clientName, int timeoutMilliseconds, bool throwRpcExceptions, IConcurrencyGuard concurrencyGuard) : this(cacheGuid, clientName, throwRpcExceptions, concurrencyGuard, new SharedCacheRpcClientImpl("localhost", timeoutMilliseconds))
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000222D File Offset: 0x0000042D
		internal SharedCacheClient(Guid cacheGuid, string clientName, bool throwRpcExceptions, IConcurrencyGuard concurrencyGuard, ISharedCacheRpcClient rpcClient)
		{
			this.cacheGuid = cacheGuid;
			this.clientName = clientName;
			this.rpcCacheClient = rpcClient;
			this.throwRpcExceptions = throwRpcExceptions;
			this.guard = concurrencyGuard;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000225A File Offset: 0x0000045A
		public bool ThrowRpcExceptions
		{
			get
			{
				return this.throwRpcExceptions;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002262 File Offset: 0x00000462
		public bool TryGet<T>(string key, out T value) where T : ISharedCacheEntry, new()
		{
			return this.TryGet<T>(key, Guid.NewGuid(), out value);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002274 File Offset: 0x00000474
		public bool TryGet<T>(string key, Guid requestCorrelationId, out T value) where T : ISharedCacheEntry, new()
		{
			string text;
			return this.TryGet<T>(key, requestCorrelationId, out value, out text);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000228C File Offset: 0x0000048C
		public bool TryGet<T>(string key, out T value, out string diagnosticsInformation) where T : ISharedCacheEntry, new()
		{
			return this.TryGet<T>(key, Guid.NewGuid(), out value, out diagnosticsInformation);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000229C File Offset: 0x0000049C
		public bool TryGet<T>(string key, Guid requestCorrelationId, out T value, out string diagnosticsInformation) where T : ISharedCacheEntry, new()
		{
			value = default(T);
			byte[] serializedBytes;
			bool flag = this.TryGet(key, requestCorrelationId, out serializedBytes, out diagnosticsInformation);
			if (flag)
			{
				value = SerializationHelper.Deserialize<T>(serializedBytes);
			}
			return flag;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022CD File Offset: 0x000004CD
		public bool TryGet(string key, out byte[] value)
		{
			return this.TryGet(key, Guid.NewGuid(), out value);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022DC File Offset: 0x000004DC
		public bool TryGet(string key, Guid requestCorrelationId, out byte[] value)
		{
			string text;
			return this.TryGet(key, requestCorrelationId, out value, out text);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool TryGet(string key, out byte[] value, out string diagnosticsInformation)
		{
			return this.TryGet(key, Guid.NewGuid(), out value, out diagnosticsInformation);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002330 File Offset: 0x00000530
		public bool TryGet(string key, Guid requestCorrelationId, out byte[] value, out string diagnosticsInformation)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("key", key);
			string transactionId = RpcHelper.CreateTransactionString(this.clientName, "Get", requestCorrelationId, key);
			SharedCacheClient.RpcAction actionToExecute = () => this.rpcCacheClient.Get(this.cacheGuid, key);
			CacheResponse response = this.Execute(transactionId, actionToExecute);
			RpcHelper.SetCommonOutParameters(response, out value, out diagnosticsInformation);
			return RpcHelper.ValidateValuedBasedResponse(response);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000239E File Offset: 0x0000059E
		public bool TryInsert(string key, ISharedCacheEntry value, DateTime valueTimestamp)
		{
			return this.TryInsert(key, value, valueTimestamp, Guid.NewGuid());
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023B0 File Offset: 0x000005B0
		public bool TryInsert(string key, ISharedCacheEntry value, DateTime valueTimestamp, Guid requestCorrelationId)
		{
			string text;
			return this.TryInsert(key, value, valueTimestamp, requestCorrelationId, out text);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023CA File Offset: 0x000005CA
		public bool TryInsert(string key, ISharedCacheEntry value, DateTime valueTimestamp, out string diagnosticsInformation)
		{
			return this.TryInsert(key, value, valueTimestamp, Guid.NewGuid(), out diagnosticsInformation);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023DC File Offset: 0x000005DC
		public bool TryInsert(string key, ISharedCacheEntry value, DateTime valueTimestamp, Guid requestCorrelationId, out string diagnosticsInformation)
		{
			ArgumentValidator.ThrowIfNull("value", value);
			byte[] value2 = SerializationHelper.Serialize(value);
			return this.TryInsert(key, value2, valueTimestamp, requestCorrelationId, out diagnosticsInformation);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002408 File Offset: 0x00000608
		public bool TryInsert(string key, byte[] value, DateTime valueTimestamp)
		{
			return this.TryInsert(key, value, valueTimestamp, Guid.NewGuid());
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002418 File Offset: 0x00000618
		public bool TryInsert(string key, byte[] value, DateTime valueTimestamp, Guid requestCorrelationId)
		{
			string text;
			return this.TryInsert(key, value, valueTimestamp, requestCorrelationId, out text);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002432 File Offset: 0x00000632
		public bool TryInsert(string key, byte[] value, DateTime valueTimestamp, out string diagnosticsInformation)
		{
			return this.TryInsert(key, value, valueTimestamp, Guid.NewGuid(), out diagnosticsInformation);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002480 File Offset: 0x00000680
		public bool TryInsert(string key, byte[] value, DateTime valueTimestamp, Guid requestCorrelationId, out string diagnosticsInformation)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("key", key);
			ArgumentValidator.ThrowIfNull("value", value);
			string transactionId = RpcHelper.CreateTransactionString(this.clientName, "Insert", requestCorrelationId, key);
			SharedCacheClient.RpcAction actionToExecute = () => this.rpcCacheClient.Insert(this.cacheGuid, key, value, valueTimestamp.ToBinary());
			CacheResponse cacheResponse = this.Execute(transactionId, actionToExecute);
			byte[] array;
			RpcHelper.SetCommonOutParameters(cacheResponse, out array, out diagnosticsInformation);
			return cacheResponse.ResponseCode == ResponseCode.OK;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000251C File Offset: 0x0000071C
		public bool TryRemove<T>(string key, Guid requestCorrelationId, out T value) where T : ISharedCacheEntry, new()
		{
			value = default(T);
			byte[] serializedBytes;
			bool flag = this.TryRemove(key, requestCorrelationId, out serializedBytes);
			if (flag)
			{
				value = SerializationHelper.Deserialize<T>(serializedBytes);
			}
			return flag;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000254C File Offset: 0x0000074C
		public bool TryRemove(string key)
		{
			byte[] array;
			return this.TryRemove(key, out array);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002562 File Offset: 0x00000762
		public bool TryRemove(string key, out byte[] value)
		{
			return this.TryRemove(key, Guid.NewGuid(), out value);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002571 File Offset: 0x00000771
		public bool TryRemove(string key, out byte[] value, out string diagnosticsInformation)
		{
			return this.TryRemove(key, Guid.NewGuid(), out value, out diagnosticsInformation);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002584 File Offset: 0x00000784
		public bool TryRemove(string key, Guid requestCorrelationId)
		{
			byte[] array;
			string text;
			return this.TryRemove(key, requestCorrelationId, out array, out text);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025A0 File Offset: 0x000007A0
		public bool TryRemove(string key, Guid requestCorrelationId, out byte[] value)
		{
			string text;
			return this.TryRemove(key, requestCorrelationId, out value, out text);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025B8 File Offset: 0x000007B8
		public bool TryRemove(string key, Guid requestCorrelationId, out string diagnosticsInformation)
		{
			byte[] array;
			return this.TryRemove(key, requestCorrelationId, out array, out diagnosticsInformation);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025FC File Offset: 0x000007FC
		public bool TryRemove(string key, Guid requestCorrelationId, out byte[] value, out string diagnosticsInformation)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("key", key);
			string transactionId = RpcHelper.CreateTransactionString(this.clientName, "Delete", requestCorrelationId, key);
			SharedCacheClient.RpcAction actionToExecute = () => this.rpcCacheClient.Delete(this.cacheGuid, key);
			CacheResponse response = this.Execute(transactionId, actionToExecute);
			RpcHelper.SetCommonOutParameters(response, out value, out diagnosticsInformation);
			return RpcHelper.ValidateValuedBasedResponse(response);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000266C File Offset: 0x0000086C
		private CacheResponse Execute(string transactionId, SharedCacheClient.RpcAction actionToExecute)
		{
			CacheResponse cacheResponse;
			try
			{
				ExTraceGlobals.ClientTracer.TraceDebug((long)this.GetHashCode(), "[SharedCacheClient:Execute] >> " + transactionId);
				if (this.guard != null)
				{
					this.guard.Increment(null);
				}
				cacheResponse = actionToExecute();
				if (cacheResponse == null)
				{
					if (this.ThrowRpcExceptions)
					{
						throw new CacheClientException("Cache action completed but response is null.");
					}
					cacheResponse = CacheResponse.Create(ResponseCode.RpcError);
					cacheResponse.AppendDiagInfo("Error", "Null response");
				}
				else
				{
					ExTraceGlobals.ClientTracer.TraceDebug((long)this.GetHashCode(), "[SharedCacheClient:Execute] << " + transactionId + " " + cacheResponse.ToString());
					switch (cacheResponse.ResponseCode)
					{
					case ResponseCode.InternalServerError:
						if (this.ThrowRpcExceptions)
						{
							throw new CacheClientException("Internal Server Error: " + cacheResponse.ToString());
						}
						break;
					case ResponseCode.CacheGuidNotFound:
						throw new ArgumentException("Cache GUID " + this.cacheGuid + " was not found registered with the shared cache service.");
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ClientTracer.TraceError((long)this.GetHashCode(), "[SharedCacheClient:Execute] << " + transactionId + " " + ex.ToString());
				RpcException ex2 = ex as RpcException;
				MaxConcurrencyReachedException ex3 = ex as MaxConcurrencyReachedException;
				if (this.ThrowRpcExceptions)
				{
					throw new CacheClientException("Unhandled Exception", ex);
				}
				if (ex3 != null)
				{
					cacheResponse = CacheResponse.Create(ResponseCode.TooManyOutstandingRequests);
					cacheResponse.AppendDiagInfo("Too many outstanding requests", ex3.Message);
				}
				else if (ex2 != null && ex2.ErrorCode == 1753)
				{
					cacheResponse = CacheResponse.Create(ResponseCode.RpcError);
					cacheResponse.AppendDiagInfo("Service Unavailable", "(0x" + ex2.ErrorCode.ToString("x") + ")");
				}
				else
				{
					cacheResponse = CacheResponse.Create(ResponseCode.RpcError);
					cacheResponse.AppendDiagInfo("Exception", ex.ToString());
				}
			}
			finally
			{
				if (this.guard != null)
				{
					this.guard.Decrement(null);
				}
			}
			return cacheResponse;
		}

		// Token: 0x04000001 RID: 1
		private const int RpcErrorNoEndpointAvailable = 1753;

		// Token: 0x04000002 RID: 2
		private readonly Guid cacheGuid;

		// Token: 0x04000003 RID: 3
		private readonly string clientName;

		// Token: 0x04000004 RID: 4
		private readonly ISharedCacheRpcClient rpcCacheClient;

		// Token: 0x04000005 RID: 5
		private readonly bool throwRpcExceptions;

		// Token: 0x04000006 RID: 6
		private IConcurrencyGuard guard;

		// Token: 0x02000008 RID: 8
		// (Invoke) Token: 0x0600002D RID: 45
		private delegate CacheResponse RpcAction();
	}
}
