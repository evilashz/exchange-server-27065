using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BEF RID: 3055
	internal class DnsAsyncRequest : LazyAsyncResult
	{
		// Token: 0x060042DD RID: 17117 RVA: 0x000B21CC File Offset: 0x000B03CC
		internal DnsAsyncRequest(string question, DnsRecordType dnsRecordType, ushort queryIdentifier, DnsQueryOptions queryOptions, object workerObject, object callerState, AsyncCallback callback, object previousRequestState) : base(workerObject, callerState, callback)
		{
			this.query = new DnsQuery(dnsRecordType, question);
			this.queryIdentifier = queryIdentifier;
			this.queryOptions = queryOptions;
			int recursionDesired = ((queryOptions & DnsQueryOptions.NoRecursion) != DnsQueryOptions.None) ? 0 : 1;
			this.buffer = DnsNativeMethods.DnsQuestionToBuffer(false, question, dnsRecordType, queryIdentifier, recursionDesired);
			Request request = (Request)workerObject;
			this.clientStatus = new int[request.ClientCount];
			this.previousRequestState = previousRequestState;
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x000B223D File Offset: 0x000B043D
		internal bool IsValid
		{
			get
			{
				return this.buffer != null;
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x000B224B File Offset: 0x000B044B
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x060042E0 RID: 17120 RVA: 0x000B2253 File Offset: 0x000B0453
		internal DnsQuery Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x000B225B File Offset: 0x000B045B
		internal ushort QueryIdentifier
		{
			get
			{
				return this.queryIdentifier;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x060042E2 RID: 17122 RVA: 0x000B2263 File Offset: 0x000B0463
		internal DnsQueryOptions DnsQueryOptions
		{
			get
			{
				return this.queryOptions;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x000B226B File Offset: 0x000B046B
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x000B227B File Offset: 0x000B047B
		internal bool UseTcpOnly
		{
			get
			{
				return (this.queryOptions & DnsQueryOptions.UseTcpOnly) != DnsQueryOptions.None;
			}
			set
			{
				if (value)
				{
					this.queryOptions |= DnsQueryOptions.UseTcpOnly;
					return;
				}
				this.queryOptions &= ~DnsQueryOptions.UseTcpOnly;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x000B229E File Offset: 0x000B049E
		internal bool AcceptTruncatedResponse
		{
			get
			{
				return (this.queryOptions & DnsQueryOptions.AcceptTruncatedResponse) != DnsQueryOptions.None;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x000B22B0 File Offset: 0x000B04B0
		internal bool ExceedsDnsErrorLimit
		{
			get
			{
				for (int i = 0; i < this.clientStatus.Length; i++)
				{
					if (this.clientStatus[i] == 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x000B22DD File Offset: 0x000B04DD
		// (set) Token: 0x060042E8 RID: 17128 RVA: 0x000B22E5 File Offset: 0x000B04E5
		internal int RetryCount
		{
			get
			{
				return this.retryCount;
			}
			set
			{
				this.retryCount = value;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x060042E9 RID: 17129 RVA: 0x000B22EE File Offset: 0x000B04EE
		public object PreviousRequestState
		{
			get
			{
				return this.previousRequestState;
			}
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x000B22F6 File Offset: 0x000B04F6
		internal bool CanQueryClient(int clientId)
		{
			if (clientId < 0 || clientId >= this.clientStatus.Length)
			{
				throw new ArgumentOutOfRangeException("clientId", "Dns client index out of range");
			}
			return this.clientStatus[clientId] == 0;
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x000B2322 File Offset: 0x000B0522
		internal void SetClientError(int clientId)
		{
			if (clientId < 0 || clientId >= this.clientStatus.Length)
			{
				throw new ArgumentOutOfRangeException("clientId", "Dns client index out of range");
			}
			this.clientStatus[clientId]++;
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x000B235C File Offset: 0x000B055C
		public override string ToString()
		{
			return string.Format("id={0}; query={1}; retryCount={2}; Options={3}", new object[]
			{
				this.queryIdentifier,
				this.query,
				this.retryCount,
				this.DnsQueryOptions
			});
		}

		// Token: 0x04003903 RID: 14595
		private byte[] buffer;

		// Token: 0x04003904 RID: 14596
		private ushort queryIdentifier;

		// Token: 0x04003905 RID: 14597
		private DnsQueryOptions queryOptions;

		// Token: 0x04003906 RID: 14598
		private readonly DnsQuery query;

		// Token: 0x04003907 RID: 14599
		private int[] clientStatus;

		// Token: 0x04003908 RID: 14600
		private int retryCount;

		// Token: 0x04003909 RID: 14601
		private object previousRequestState;
	}
}
