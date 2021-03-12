using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BB6 RID: 2998
	internal abstract class Request : IDisposable
	{
		// Token: 0x0600402F RID: 16431 RVA: 0x000A9840 File Offset: 0x000A7A40
		protected Request(DnsServerList list, DnsQueryOptions flags, Dns dnsInstance, AsyncCallback requestCallback, object stateObject)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (dnsInstance == null)
			{
				throw new ArgumentNullException("dnsInstance");
			}
			this.resultCallback = new LazyAsyncResult(this, stateObject, requestCallback);
			this.dnsInstance = dnsInstance;
			this.serverList = list;
			this.queryOptions = flags;
			this.clients = new DnsClient[list.Count];
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x000A98C6 File Offset: 0x000A7AC6
		// (set) Token: 0x06004030 RID: 16432 RVA: 0x000A98BD File Offset: 0x000A7ABD
		public int MaxWireDataSize
		{
			get
			{
				return this.maxWireDataSize;
			}
			set
			{
				this.maxWireDataSize = value;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x000A98CE File Offset: 0x000A7ACE
		// (set) Token: 0x06004033 RID: 16435 RVA: 0x000A98D6 File Offset: 0x000A7AD6
		public DateTime RequestTimeout
		{
			get
			{
				return this.requestTimeout;
			}
			set
			{
				this.requestTimeout = value;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x000A98DF File Offset: 0x000A7ADF
		// (set) Token: 0x06004035 RID: 16437 RVA: 0x000A98E7 File Offset: 0x000A7AE7
		protected DnsQueryOptions Options
		{
			get
			{
				return this.queryOptions;
			}
			set
			{
				this.queryOptions = value;
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06004036 RID: 16438 RVA: 0x000A98F0 File Offset: 0x000A7AF0
		protected bool BypassCache
		{
			get
			{
				return (this.queryOptions & DnsQueryOptions.BypassCache) != DnsQueryOptions.None;
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x000A9900 File Offset: 0x000A7B00
		protected bool AcceptTruncatedResponse
		{
			get
			{
				return (this.queryOptions & DnsQueryOptions.AcceptTruncatedResponse) != DnsQueryOptions.None;
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x000A9910 File Offset: 0x000A7B10
		protected bool FailureTolerant
		{
			get
			{
				return (this.queryOptions & DnsQueryOptions.FailureTolerant) != DnsQueryOptions.None;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x000A9921 File Offset: 0x000A7B21
		protected DnsServerList ServerList
		{
			get
			{
				return this.serverList;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x000A9929 File Offset: 0x000A7B29
		protected LazyAsyncResult Callback
		{
			get
			{
				return this.resultCallback;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x000A9931 File Offset: 0x000A7B31
		protected Dns DnsInstance
		{
			get
			{
				return this.dnsInstance;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x000A9939 File Offset: 0x000A7B39
		internal int ClientCount
		{
			get
			{
				return this.clients.Length;
			}
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x000A9944 File Offset: 0x000A7B44
		protected static bool NextQueryType(AddressFamily requestedAddressFamily, int ipv4AddressCount, int ipv6AddressCount, DnsRecordType lastQueryType, out DnsRecordType nextQueryType)
		{
			nextQueryType = DnsRecordType.Unknown;
			switch (requestedAddressFamily)
			{
			case AddressFamily.Unspecified:
				if (lastQueryType == DnsRecordType.Unknown)
				{
					if (ipv6AddressCount == 0)
					{
						nextQueryType = DnsRecordType.AAAA;
						return true;
					}
					if (ipv4AddressCount == 0)
					{
						nextQueryType = DnsRecordType.A;
						return true;
					}
				}
				else if (lastQueryType == DnsRecordType.AAAA && ipv4AddressCount == 0)
				{
					nextQueryType = DnsRecordType.A;
					return true;
				}
				return false;
			case AddressFamily.Unix:
				break;
			case AddressFamily.InterNetwork:
				if (lastQueryType == DnsRecordType.Unknown && ipv4AddressCount == 0)
				{
					nextQueryType = DnsRecordType.A;
					return true;
				}
				return false;
			default:
				if (requestedAddressFamily == AddressFamily.InterNetworkV6)
				{
					if (lastQueryType == DnsRecordType.Unknown && ipv6AddressCount == 0)
					{
						nextQueryType = DnsRecordType.AAAA;
						return true;
					}
					return false;
				}
				break;
			}
			throw new NotSupportedException("Invalid address family " + requestedAddressFamily);
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x0600403E RID: 16446 RVA: 0x000A99C8 File Offset: 0x000A7BC8
		protected static ExEventLog EventLogger
		{
			get
			{
				if (Request.eventLogger == null)
				{
					Request.eventLogger = new ExEventLog(ExTraceGlobals.DNSTracer.Category, "MSExchange Common");
				}
				return Request.eventLogger;
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x0600403F RID: 16447
		protected abstract Request.Result InvalidDataResult { get; }

		// Token: 0x06004040 RID: 16448 RVA: 0x000A99EF File Offset: 0x000A7BEF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x000A9A00 File Offset: 0x000A7C00
		internal bool CloseClient(DnsClient clientToClose)
		{
			DnsClient dnsClient = Interlocked.CompareExchange<DnsClient>(ref this.clients[clientToClose.Id], null, clientToClose);
			if (clientToClose == dnsClient)
			{
				dnsClient.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x000A9A33 File Offset: 0x000A7C33
		internal void CloseSocketAndResendRequest(DnsClient clientToClose)
		{
			if (this.CloseClient(clientToClose))
			{
				this.SendToServer(clientToClose.Id);
			}
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x000A9A4C File Offset: 0x000A7C4C
		internal void CheckForTimeout()
		{
			ExTraceGlobals.DNSTracer.TraceDebug((long)this.GetHashCode(), "CheckForTimeout");
			DnsAsyncRequest dnsAsyncRequest = this.dnsAsyncRequest;
			if (dnsAsyncRequest == null)
			{
				ExTraceGlobals.DNSTracer.TraceDebug((long)this.GetHashCode(), "CheckForTimeout, no request");
				return;
			}
			if (dnsAsyncRequest.Query.Type == DnsRecordType.AAAA && this.serverList.Cache != null)
			{
				this.serverList.Cache.AddAaaaQueryTimeOut(dnsAsyncRequest.Query.Question);
			}
			if (DateTime.UtcNow > this.requestTimeout)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.TimeOutRequest), dnsAsyncRequest);
				return;
			}
			if (!this.ShouldRetrySubQuery(dnsAsyncRequest))
			{
				DnsLog.Log(this, "Subquery retry not required. {0}", new object[]
				{
					dnsAsyncRequest
				});
				return;
			}
			if (this.nextTimeout > DateTime.UtcNow)
			{
				DnsTimer.RegisterTimer(this, this.nextTimeout);
				ExTraceGlobals.DNSTracer.TraceDebug((long)this.GetHashCode(), "Timeout in future");
				return;
			}
			DnsLog.Log(this, "Retrying request due to timeout {0}", new object[]
			{
				this.dnsAsyncRequest
			});
			this.SendToNextServer();
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x000A9B65 File Offset: 0x000A7D65
		protected virtual bool ShouldRetrySubQuery(DnsAsyncRequest asyncRequest)
		{
			return true;
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x000A9B68 File Offset: 0x000A7D68
		protected static int FollowCNameChain(List<DnsCNameRecord> names, string key, List<DnsCNameRecord> list)
		{
			int num = 0;
			for (;;)
			{
				DnsCNameRecord dnsCNameRecord = new DnsCNameRecord(key);
				int num2 = names.BinarySearch(dnsCNameRecord, DnsCNameRecord.Comparer);
				if (num2 >= 0)
				{
					dnsCNameRecord = names[num2];
					if (num > names.Count || list.Count == Dns.MaxCnameRecords)
					{
						break;
					}
					list.Add(dnsCNameRecord);
					num++;
					key = dnsCNameRecord.Host;
				}
				if (num2 < 0)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x000A9BCC File Offset: 0x000A7DCC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				for (int i = 0; i < this.clients.Length; i++)
				{
					DnsClient dnsClient = Interlocked.Exchange<DnsClient>(ref this.clients[i], null);
					if (dnsClient != null)
					{
						dnsClient.Dispose();
					}
				}
			}
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x000A9C0B File Offset: 0x000A7E0B
		protected void PostRequest(string question, DnsRecordType dnsRecordType, object previousState = null)
		{
			question = Dns.TrimTrailingDot(question);
			if (!Dns.IsValidQuestion(question))
			{
				this.Callback.InvokeCallback(this.InvalidDataResult);
				return;
			}
			this.BeginResolve(question, dnsRecordType, new AsyncCallback(this.ResolveComplete), this, previousState);
		}

		// Token: 0x06004048 RID: 16456
		protected abstract bool ProcessData(DnsResult result, DnsAsyncRequest dnsAsyncRequest);

		// Token: 0x06004049 RID: 16457 RVA: 0x000A9C48 File Offset: 0x000A7E48
		protected IPAddress[] FindEntries(string key, AddressFamily type, bool staticOnly, out TimeSpan timeToLive)
		{
			List<IPAddress> list = new List<IPAddress>();
			timeToLive = TimeSpan.Zero;
			DnsRecordType type2;
			switch (type)
			{
			case AddressFamily.Unspecified:
			{
				TimeSpan timeSpan;
				IPAddress[] array = this.FindEntries(key, AddressFamily.InterNetwork, staticOnly, out timeSpan);
				if (array != null && timeSpan == TimeSpan.MaxValue)
				{
					staticOnly = true;
				}
				TimeSpan timeSpan2;
				IPAddress[] array2 = this.FindEntries(key, AddressFamily.InterNetworkV6, staticOnly, out timeSpan2);
				if (array != null && array2 != null)
				{
					if (timeSpan2 == TimeSpan.MaxValue && timeSpan != TimeSpan.MaxValue)
					{
						array = null;
						timeToLive = timeSpan2;
					}
					else
					{
						timeToLive = ((timeSpan < timeSpan2) ? timeSpan : timeSpan2);
					}
				}
				else if (array != null)
				{
					timeToLive = timeSpan;
				}
				else if (array2 != null)
				{
					timeToLive = timeSpan2;
				}
				if (array != null)
				{
					list.InsertRange(0, array);
				}
				if (array2 != null)
				{
					list.InsertRange(0, array2);
				}
				if (list.Count != 0)
				{
					return list.ToArray();
				}
				return null;
			}
			case AddressFamily.Unix:
				break;
			case AddressFamily.InterNetwork:
				type2 = DnsRecordType.A;
				goto IL_F8;
			default:
				if (type == AddressFamily.InterNetworkV6)
				{
					type2 = DnsRecordType.AAAA;
					goto IL_F8;
				}
				break;
			}
			return null;
			IL_F8:
			DnsResult dnsResult = this.FindInCache(new DnsQuery(type2, key));
			if (dnsResult == null || (staticOnly && !dnsResult.IsPermanentEntry) || dnsResult.HasExpired || dnsResult.Status != DnsStatus.Success)
			{
				return null;
			}
			DnsRecordList list2 = dnsResult.List;
			if (list2 == null)
			{
				return null;
			}
			if (dnsResult.IsPermanentEntry)
			{
				timeToLive = TimeSpan.MaxValue;
			}
			else
			{
				timeToLive = dnsResult.Expires - DateTime.UtcNow;
			}
			foreach (DnsRecord dnsRecord in list2.EnumerateAnswers(type2))
			{
				if (key.Equals(dnsRecord.Name, StringComparison.OrdinalIgnoreCase))
				{
					if (dnsRecord.RecordType == DnsRecordType.A)
					{
						list.Add(((DnsARecord)dnsRecord).IPAddress);
					}
					else
					{
						list.Add(((DnsAAAARecord)dnsRecord).IPAddress);
					}
				}
			}
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x000A9E50 File Offset: 0x000A8050
		protected DnsResult FindInCache(DnsQuery query)
		{
			return this.ServerList.Cache.Find(query);
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x000A9E64 File Offset: 0x000A8064
		protected static IPAddress[] FindLocalHostEntries(string key, AddressFamily type, out TimeSpan timeToLive)
		{
			timeToLive = TimeSpan.MaxValue;
			List<IPAddress> list = null;
			if (string.Equals(key, "localhost", StringComparison.OrdinalIgnoreCase))
			{
				list = new List<IPAddress>();
				if (type == AddressFamily.Unspecified || type == AddressFamily.InterNetwork)
				{
					list.Add(IPAddress.Loopback);
				}
				if (type == AddressFamily.Unspecified || type == AddressFamily.InterNetworkV6)
				{
					list.Add(IPAddress.IPv6Loopback);
				}
			}
			if (list != null && list.Count != 0)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x000A9ECC File Offset: 0x000A80CC
		private IAsyncResult BeginResolve(string question, DnsRecordType dnsRecordType, AsyncCallback callback, object state, object previousState)
		{
			if (string.IsNullOrEmpty(question))
			{
				throw new ArgumentNullException("question");
			}
			ushort queryIdentifier = this.GenerateRandomQueryIdentifier();
			DnsAsyncRequest dnsAsyncRequest = new DnsAsyncRequest(question, dnsRecordType, queryIdentifier, this.queryOptions, this, state, callback, previousState);
			ExTraceGlobals.DNSTracer.TraceDebug<DnsQuery, ushort>((long)dnsAsyncRequest.GetHashCode(), "BeginResolve, {0}, (query id:{1})", dnsAsyncRequest.Query, dnsAsyncRequest.QueryIdentifier);
			DnsAsyncRequest dnsAsyncRequest2 = Interlocked.CompareExchange<DnsAsyncRequest>(ref this.dnsAsyncRequest, dnsAsyncRequest, null);
			if (dnsAsyncRequest2 != null)
			{
				throw new InvalidOperationException(NetException.ResolveInProgress);
			}
			if (!dnsAsyncRequest.IsValid)
			{
				dnsAsyncRequest.InvokeCallback(new DnsResult(DnsStatus.ErrorInvalidData, IPAddress.None, DnsResult.ErrorTimeToLive));
				return dnsAsyncRequest;
			}
			if (!this.BypassCache)
			{
				this.cachedResult = this.FindInCache(dnsAsyncRequest.Query);
				if (this.cachedResult == null && dnsAsyncRequest.Query.Type != DnsRecordType.CNAME)
				{
					this.cachedResult = this.FindInCache(new DnsQuery(DnsRecordType.CNAME, dnsAsyncRequest.Query.Question));
				}
				if (this.cachedResult != null && !this.cachedResult.HasExpired)
				{
					DnsLog.Log(this, "Cachehit for {0}. Result = {1}", new object[]
					{
						question,
						this.cachedResult
					});
					dnsAsyncRequest.InvokeCallback(this.cachedResult);
					return dnsAsyncRequest;
				}
			}
			if (this.ServerList.Addresses.Count == 0)
			{
				dnsAsyncRequest.InvokeCallback(new DnsResult(DnsStatus.ErrorNoDns, IPAddress.None, DnsResult.DefaultTimeToLive));
				return dnsAsyncRequest;
			}
			this.queriesSent++;
			if (this.queriesSent == 1)
			{
				this.lastServerQueried = (int)queryIdentifier;
			}
			this.SendToNextServer();
			return dnsAsyncRequest;
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x000AA054 File Offset: 0x000A8254
		private void ResolveComplete(IAsyncResult asyncResult)
		{
			DnsAsyncRequest dnsAsyncRequest = (DnsAsyncRequest)asyncResult;
			DnsResult dnsResult = this.EndResolve(asyncResult);
			DnsLog.Log(this, "ResolveComplete. Result={0}; Request={1}", new object[]
			{
				dnsResult,
				dnsAsyncRequest
			});
			if (dnsAsyncRequest.IsValid && !this.BypassCache && !dnsResult.HasExpired && dnsResult.Status != DnsStatus.ErrorRetry && dnsResult.Status != DnsStatus.ErrorTimeout && dnsResult.Status != DnsStatus.InfoTruncated && dnsResult.Status != DnsStatus.ErrorSubQueryTimeout && dnsResult != this.cachedResult)
			{
				this.ServerList.Cache.Add(dnsAsyncRequest.Query, dnsResult);
			}
			this.cachedResult = null;
			this.ProcessData(dnsResult, dnsAsyncRequest);
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x000AA0F8 File Offset: 0x000A82F8
		private DnsResult EndResolve(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			DnsAsyncRequest dnsAsyncRequest = (DnsAsyncRequest)asyncResult;
			DnsAsyncRequest dnsAsyncRequest2 = Interlocked.CompareExchange<DnsAsyncRequest>(ref this.dnsAsyncRequest, null, dnsAsyncRequest);
			if (dnsAsyncRequest != dnsAsyncRequest2)
			{
				throw new InvalidOperationException(NetException.IAsyncResultMismatch);
			}
			if (dnsAsyncRequest.EndCalled)
			{
				throw new InvalidOperationException(NetException.EndAlreadyCalled);
			}
			dnsAsyncRequest.EndCalled = true;
			DnsResult dnsResult = (DnsResult)dnsAsyncRequest.Result;
			ExTraceGlobals.DNSTracer.TraceDebug<DnsQuery, ushort, DnsStatus>((long)dnsAsyncRequest.GetHashCode(), "EndResolve, {0}, (query id:{1}), status {2}", dnsAsyncRequest.Query, dnsAsyncRequest.QueryIdentifier, dnsResult.Status);
			return dnsResult;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x000AA190 File Offset: 0x000A8390
		private ushort GenerateRandomQueryIdentifier()
		{
			ushort num;
			do
			{
				lock (Request.rand)
				{
					num = (ushort)Request.rand.Next(0, 65535);
				}
			}
			while (this.usedQueryIds.Contains(num));
			this.usedQueryIds.Add(num);
			return num;
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x000AA1F8 File Offset: 0x000A83F8
		private void SendToNextServer()
		{
			for (int i = 0; i < this.clients.Length; i++)
			{
				int server = Interlocked.Increment(ref this.lastServerQueried) % this.clients.Length;
				if (this.SendToServer(server))
				{
					return;
				}
			}
			ExTraceGlobals.DNSTracer.TraceDebug((long)this.GetHashCode(), "SendToNextServer did not send request.");
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x000AA250 File Offset: 0x000A8450
		private bool SendToServer(int server)
		{
			this.SetNextTimeout();
			DnsAsyncRequest dnsAsyncRequest = this.dnsAsyncRequest;
			if (dnsAsyncRequest == null || !dnsAsyncRequest.IsValid)
			{
				return false;
			}
			DnsClient dnsClient = this.clients[server];
			if (dnsClient == null)
			{
				dnsClient = new DnsClient(server, this.serverList.Addresses[server]);
				DnsClient dnsClient2 = Interlocked.CompareExchange<DnsClient>(ref this.clients[server], dnsClient, null);
				if (dnsClient2 != null)
				{
					dnsClient.Dispose();
					return false;
				}
			}
			if (!dnsAsyncRequest.CanQueryClient(server))
			{
				return false;
			}
			ExTraceGlobals.DNSTracer.TraceDebug<int, DnsQuery, ushort>((long)dnsAsyncRequest.GetHashCode(), "SendToServer({0}), {1}, (query id:{2})", server, dnsAsyncRequest.Query, dnsAsyncRequest.QueryIdentifier);
			DnsLog.Log(this, "SendToServer {0}({1}), {2}, (query id:{3})", new object[]
			{
				this.serverList.Addresses[server],
				server,
				dnsAsyncRequest.Query,
				dnsAsyncRequest.QueryIdentifier
			});
			return dnsClient.Send(dnsAsyncRequest);
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x000AA33C File Offset: 0x000A853C
		private void SetNextTimeout()
		{
			DateTime dateTime = DateTime.UtcNow + this.DnsInstance.QueryRetryInterval;
			bool flag = this.nextTimeout <= dateTime;
			this.nextTimeout = dateTime;
			DnsAsyncRequest dnsAsyncRequest = this.dnsAsyncRequest;
			if (dnsAsyncRequest != null)
			{
				ExTraceGlobals.DNSTracer.TraceDebug((long)dnsAsyncRequest.GetHashCode(), "SetNextTimeout, {0}, (query id:{1}), Next timeout {2} register:{3} valid:{4}", new object[]
				{
					dnsAsyncRequest.Query,
					dnsAsyncRequest.QueryIdentifier,
					dateTime,
					flag,
					dnsAsyncRequest.IsValid
				});
			}
			if (flag)
			{
				DnsTimer.RegisterTimer(this, dateTime);
			}
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x000AA3DC File Offset: 0x000A85DC
		private void TimeOutRequest(object state)
		{
			DnsAsyncRequest dnsAsyncRequest = (DnsAsyncRequest)state;
			DnsLog.Log(this, "Query timedout. {0}", new object[]
			{
				dnsAsyncRequest
			});
			ExTraceGlobals.DNSTracer.TraceDebug<DnsQuery, ushort>((long)dnsAsyncRequest.GetHashCode(), "TimedOut, {0}, (query id:{1})", dnsAsyncRequest.Query, dnsAsyncRequest.QueryIdentifier);
			dnsAsyncRequest.InvokeCallback(new DnsResult(DnsStatus.ErrorTimeout, IPAddress.Any, DnsResult.ErrorTimeToLive));
		}

		// Token: 0x04003796 RID: 14230
		private static Random rand = new Random((int)DateTime.UtcNow.Ticks);

		// Token: 0x04003797 RID: 14231
		private int maxWireDataSize = 2048;

		// Token: 0x04003798 RID: 14232
		private LazyAsyncResult resultCallback;

		// Token: 0x04003799 RID: 14233
		private DnsResult cachedResult;

		// Token: 0x0400379A RID: 14234
		private DnsServerList serverList;

		// Token: 0x0400379B RID: 14235
		private DnsQueryOptions queryOptions;

		// Token: 0x0400379C RID: 14236
		private DnsClient[] clients;

		// Token: 0x0400379D RID: 14237
		private int lastServerQueried;

		// Token: 0x0400379E RID: 14238
		private DnsAsyncRequest dnsAsyncRequest;

		// Token: 0x0400379F RID: 14239
		private List<ushort> usedQueryIds = new List<ushort>(10);

		// Token: 0x040037A0 RID: 14240
		private DateTime requestTimeout;

		// Token: 0x040037A1 RID: 14241
		private DateTime nextTimeout;

		// Token: 0x040037A2 RID: 14242
		private int queriesSent;

		// Token: 0x040037A3 RID: 14243
		private Dns dnsInstance;

		// Token: 0x040037A4 RID: 14244
		private static ExEventLog eventLogger;

		// Token: 0x02000BB7 RID: 2999
		internal class Result
		{
			// Token: 0x06004055 RID: 16469 RVA: 0x000AA465 File Offset: 0x000A8665
			internal Result(DnsStatus status, IPAddress server, object data)
			{
				this.status = status;
				this.server = server;
				this.data = data;
			}

			// Token: 0x17000FC9 RID: 4041
			// (get) Token: 0x06004056 RID: 16470 RVA: 0x000AA482 File Offset: 0x000A8682
			internal DnsStatus Status
			{
				get
				{
					return this.status;
				}
			}

			// Token: 0x17000FCA RID: 4042
			// (get) Token: 0x06004057 RID: 16471 RVA: 0x000AA48A File Offset: 0x000A868A
			internal IPAddress Server
			{
				get
				{
					return this.server;
				}
			}

			// Token: 0x17000FCB RID: 4043
			// (get) Token: 0x06004058 RID: 16472 RVA: 0x000AA492 File Offset: 0x000A8692
			internal object Data
			{
				get
				{
					return this.data;
				}
			}

			// Token: 0x040037A5 RID: 14245
			private DnsStatus status;

			// Token: 0x040037A6 RID: 14246
			private IPAddress server;

			// Token: 0x040037A7 RID: 14247
			private object data;
		}

		// Token: 0x02000BB8 RID: 3000
		internal class HostAddressList : List<IPAddress>
		{
			// Token: 0x17000FCC RID: 4044
			// (get) Token: 0x06004059 RID: 16473 RVA: 0x000AA49A File Offset: 0x000A869A
			public int Ipv4AddressCount
			{
				get
				{
					return this.ipv4AddressCount;
				}
			}

			// Token: 0x17000FCD RID: 4045
			// (get) Token: 0x0600405A RID: 16474 RVA: 0x000AA4A2 File Offset: 0x000A86A2
			public int Ipv6AddressCount
			{
				get
				{
					return this.ipv6AddressCount;
				}
			}

			// Token: 0x0600405B RID: 16475 RVA: 0x000AA4AA File Offset: 0x000A86AA
			private void ProcessAddAddress(IPAddress address)
			{
				if (address.AddressFamily == AddressFamily.InterNetwork)
				{
					this.ipv4AddressCount++;
					return;
				}
				if (address.AddressFamily == AddressFamily.InterNetworkV6)
				{
					this.ipv6AddressCount++;
					return;
				}
				throw new NotSupportedException("Unexpected address type");
			}

			// Token: 0x0600405C RID: 16476 RVA: 0x000AA4E7 File Offset: 0x000A86E7
			public new void Insert(int index, IPAddress address)
			{
				base.Insert(index, address);
				this.ProcessAddAddress(address);
			}

			// Token: 0x0600405D RID: 16477 RVA: 0x000AA4F8 File Offset: 0x000A86F8
			public new void InsertRange(int index, IEnumerable<IPAddress> collection)
			{
				base.InsertRange(index, collection);
				foreach (IPAddress address in collection)
				{
					this.ProcessAddAddress(address);
				}
			}

			// Token: 0x0600405E RID: 16478 RVA: 0x000AA548 File Offset: 0x000A8748
			public new void AddRange(IEnumerable<IPAddress> collection)
			{
				base.AddRange(collection);
				foreach (IPAddress address in collection)
				{
					this.ProcessAddAddress(address);
				}
			}

			// Token: 0x0600405F RID: 16479 RVA: 0x000AA598 File Offset: 0x000A8798
			public new void Add(IPAddress address)
			{
				base.Add(address);
				this.ProcessAddAddress(address);
			}

			// Token: 0x040037A8 RID: 14248
			private int ipv4AddressCount;

			// Token: 0x040037A9 RID: 14249
			private int ipv6AddressCount;
		}
	}
}
