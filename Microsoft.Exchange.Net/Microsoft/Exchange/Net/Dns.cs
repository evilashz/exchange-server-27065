using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BE8 RID: 3048
	internal class Dns
	{
		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x000B1050 File Offset: 0x000AF250
		// (set) Token: 0x06004283 RID: 17027 RVA: 0x000B1048 File Offset: 0x000AF248
		public static int MaxCnameRecords
		{
			get
			{
				return Dns.maxCnameRecords;
			}
			set
			{
				Dns.maxCnameRecords = value;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06004286 RID: 17030 RVA: 0x000B105F File Offset: 0x000AF25F
		// (set) Token: 0x06004285 RID: 17029 RVA: 0x000B1057 File Offset: 0x000AF257
		public static int MaxSubQueries
		{
			get
			{
				return Dns.maxSubQueries;
			}
			set
			{
				Dns.maxSubQueries = value;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x000B106F File Offset: 0x000AF26F
		// (set) Token: 0x06004287 RID: 17031 RVA: 0x000B1066 File Offset: 0x000AF266
		public int MaxDataPerRequest
		{
			get
			{
				return this.maxDataPerRequest;
			}
			set
			{
				this.maxDataPerRequest = value;
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x000B1077 File Offset: 0x000AF277
		// (set) Token: 0x0600428A RID: 17034 RVA: 0x000B107F File Offset: 0x000AF27F
		public DnsServerList ServerList
		{
			get
			{
				return this.serverList;
			}
			set
			{
				this.serverList = value;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x000B1088 File Offset: 0x000AF288
		// (set) Token: 0x0600428C RID: 17036 RVA: 0x000B1090 File Offset: 0x000AF290
		public IEnumerable<IPAddress> LocalIPAddresses
		{
			get
			{
				return this.localAddresses;
			}
			set
			{
				this.localAddresses = value;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x0600428D RID: 17037 RVA: 0x000B1099 File Offset: 0x000AF299
		// (set) Token: 0x0600428E RID: 17038 RVA: 0x000B10A1 File Offset: 0x000AF2A1
		public TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.timeout = value;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x000B10BD File Offset: 0x000AF2BD
		// (set) Token: 0x06004290 RID: 17040 RVA: 0x000B10C5 File Offset: 0x000AF2C5
		public TimeSpan QueryRetryInterval
		{
			get
			{
				return this.queryRetryInterval;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.queryRetryInterval = value;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x000B10E1 File Offset: 0x000AF2E1
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x000B10E9 File Offset: 0x000AF2E9
		public AddressFamily DefaultAddressFamily
		{
			get
			{
				return this.defaultAddressFamily;
			}
			set
			{
				if ((value == AddressFamily.InterNetwork || value == AddressFamily.Unspecified) && !Socket.OSSupportsIPv4)
				{
					throw new NotSupportedException("IPv4 not supported on this machine");
				}
				if ((value == AddressFamily.InterNetworkV6 || value == AddressFamily.Unspecified) && !Socket.OSSupportsIPv6)
				{
					throw new NotSupportedException("IPv6 not supported on this machine");
				}
				this.defaultAddressFamily = value;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06004293 RID: 17043 RVA: 0x000B1125 File Offset: 0x000AF325
		// (set) Token: 0x06004294 RID: 17044 RVA: 0x000B112D File Offset: 0x000AF32D
		public DnsQueryOptions Options
		{
			get
			{
				return this.queryOptions;
			}
			set
			{
				if ((value & ~(DnsQueryOptions.AcceptTruncatedResponse | DnsQueryOptions.UseTcpOnly | DnsQueryOptions.NoRecursion)) != DnsQueryOptions.None)
				{
					throw new ArgumentException(NetException.InvalidFlags((int)value), "value");
				}
				this.queryOptions = value;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x000B1152 File Offset: 0x000AF352
		public static ExEventLog EventLogger
		{
			get
			{
				if (Dns.eventLogger == null)
				{
					Dns.eventLogger = new ExEventLog(ExTraceGlobals.DNSTracer.Category, "MSExchange Common");
				}
				return Dns.eventLogger;
			}
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x000B117C File Offset: 0x000AF37C
		public static DnsStatus EndResolveToAddresses(IAsyncResult asyncResult, out IPAddress[] addresses)
		{
			IPAddress ipaddress;
			return Dns.EndResolveToAddresses(asyncResult, out addresses, out ipaddress);
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x000B1194 File Offset: 0x000AF394
		public static DnsStatus EndResolveToAddresses(IAsyncResult asyncResult, out IPAddress[] addresses, out IPAddress server)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			DnsStatus status;
			using (Request request = lazyAsyncResult.AsyncObject as Request)
			{
				if (request == null)
				{
					throw new ArgumentException(NetException.WrongType, "asyncResult");
				}
				Request.Result result = (Request.Result)lazyAsyncResult.Result;
				addresses = (IPAddress[])result.Data;
				server = result.Server;
				status = result.Status;
			}
			return status;
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x000B1220 File Offset: 0x000AF420
		public static DnsStatus EndResolveToMailServers(IAsyncResult asyncResult, out TargetHost[] hosts)
		{
			IPAddress ipaddress;
			return Dns.EndResolveToMailServers(asyncResult, out hosts, out ipaddress);
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x000B1238 File Offset: 0x000AF438
		public static DnsStatus EndResolveToMailServers(IAsyncResult asyncResult, out TargetHost[] hosts, out IPAddress server)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			DnsStatus status;
			using (MxRequest mxRequest = lazyAsyncResult.AsyncObject as MxRequest)
			{
				if (mxRequest == null)
				{
					throw new ArgumentException(NetException.WrongType, "asyncResult");
				}
				Request.Result result = (Request.Result)lazyAsyncResult.Result;
				hosts = (TargetHost[])result.Data;
				server = result.Server;
				status = result.Status;
			}
			return status;
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x000B12C4 File Offset: 0x000AF4C4
		public static DnsStatus EndRetrieveTextRecords(IAsyncResult asyncResult, out string[] text)
		{
			IPAddress ipaddress;
			return Dns.EndRetrieveTextRecords(asyncResult, out text, out ipaddress);
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x000B12DC File Offset: 0x000AF4DC
		public static DnsStatus EndRetrieveTextRecords(IAsyncResult asyncResult, out string[] text, out IPAddress server)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			DnsStatus status;
			using (TxtRequest txtRequest = lazyAsyncResult.AsyncObject as TxtRequest)
			{
				if (txtRequest == null)
				{
					throw new ArgumentException(NetException.WrongType, "asyncResult");
				}
				Request.Result result = (Request.Result)lazyAsyncResult.Result;
				text = (string[])result.Data;
				server = result.Server;
				status = result.Status;
			}
			return status;
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x000B1368 File Offset: 0x000AF568
		public static DnsStatus EndRetrieveCNameRecords(IAsyncResult asyncResult, out DnsCNameRecord[] records)
		{
			IPAddress ipaddress;
			return Dns.EndRetrieveCNameRecords(asyncResult, out records, out ipaddress);
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x000B1380 File Offset: 0x000AF580
		public static DnsStatus EndRetrieveCNameRecords(IAsyncResult asyncResult, out DnsCNameRecord[] records, out IPAddress server)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			DnsStatus status;
			using (CNameRequest cnameRequest = lazyAsyncResult.AsyncObject as CNameRequest)
			{
				if (cnameRequest == null)
				{
					throw new ArgumentException(NetException.WrongType, "asyncResult");
				}
				Request.Result result = (Request.Result)lazyAsyncResult.Result;
				records = (DnsCNameRecord[])result.Data;
				server = result.Server;
				status = result.Status;
			}
			return status;
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x000B140C File Offset: 0x000AF60C
		public static DnsStatus EndResolveToNames(IAsyncResult asyncResult, out string[] domains)
		{
			IPAddress ipaddress;
			return Dns.EndResolveToNames(asyncResult, out domains, out ipaddress);
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x000B1424 File Offset: 0x000AF624
		public static DnsStatus EndResolveToNames(IAsyncResult asyncResult, out string[] domains, out IPAddress server)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			DnsStatus status;
			using (PtrRequest ptrRequest = lazyAsyncResult.AsyncObject as PtrRequest)
			{
				if (ptrRequest == null)
				{
					throw new ArgumentException(NetException.WrongType, "asyncResult");
				}
				Request.Result result = (Request.Result)lazyAsyncResult.Result;
				domains = (string[])result.Data;
				server = result.Server;
				status = result.Status;
			}
			return status;
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x000B14B0 File Offset: 0x000AF6B0
		public static DnsStatus EndResolveToSrvRecords(IAsyncResult asyncResult, out SrvRecord[] srvRecords)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			DnsStatus result2;
			using (SrvRequest srvRequest = lazyAsyncResult.AsyncObject as SrvRequest)
			{
				if (srvRequest == null)
				{
					throw new ArgumentException(NetException.WrongType, "asyncResult");
				}
				Request.Result result = (Request.Result)lazyAsyncResult.Result;
				if (result != null)
				{
					srvRecords = (SrvRecord[])result.Data;
					result2 = result.Status;
				}
				else
				{
					srvRecords = null;
					result2 = DnsStatus.InfoNoRecords;
				}
			}
			return result2;
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x000B1540 File Offset: 0x000AF740
		public static DnsStatus EndRetrieveSoaRecords(IAsyncResult asyncResult, out SoaRecord[] records)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			DnsStatus status;
			using (SoaRequest soaRequest = lazyAsyncResult.AsyncObject as SoaRequest)
			{
				if (soaRequest == null)
				{
					throw new ArgumentException(NetException.WrongType, "asyncResult");
				}
				Request.Result result = (Request.Result)lazyAsyncResult.Result;
				records = (SoaRecord[])result.Data;
				status = result.Status;
			}
			return status;
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x000B15C4 File Offset: 0x000AF7C4
		public static DnsStatus EndRetrieveNsRecords(IAsyncResult asyncResult, out DnsNsRecord[] records)
		{
			IPAddress ipaddress;
			return Dns.EndRetrieveNsRecords(asyncResult, out records, out ipaddress);
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x000B15DC File Offset: 0x000AF7DC
		public static DnsStatus EndRetrieveNsRecords(IAsyncResult asyncResult, out DnsNsRecord[] records, out IPAddress server)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			DnsStatus status;
			using (NsRequest nsRequest = lazyAsyncResult.AsyncObject as NsRequest)
			{
				if (nsRequest == null)
				{
					throw new ArgumentException(NetException.WrongType, "asyncResult");
				}
				Request.Result result = (Request.Result)lazyAsyncResult.Result;
				records = (DnsNsRecord[])result.Data;
				server = result.Server;
				status = result.Status;
			}
			return status;
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x000B1668 File Offset: 0x000AF868
		public void AdapterServerList(Guid adapterGuid)
		{
			this.AdapterServerList(adapterGuid, false, false);
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x000B1674 File Offset: 0x000AF874
		public void AdapterServerList(Guid adapterGuid, bool excludeServersFromLoopbackAdapters, bool excludeIPv6SiteLocalAddresses)
		{
			IPAddress[] adapterDnsServerList = DnsServerList.GetAdapterDnsServerList(adapterGuid, excludeServersFromLoopbackAdapters, excludeIPv6SiteLocalAddresses);
			if (adapterDnsServerList == null || adapterDnsServerList.Length == 0)
			{
				Dns.EventLogger.LogEvent(CommonEventLogConstants.Tuple_NoAdapterDnsServerList, null, new object[]
				{
					adapterGuid
				});
				return;
			}
			this.InitializeServerList(adapterDnsServerList);
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x000B16BC File Offset: 0x000AF8BC
		public void InitializeFromMachineServerList()
		{
			IPAddress[] machineDnsServerList = DnsServerList.GetMachineDnsServerList();
			if (machineDnsServerList == null || machineDnsServerList.Length == 0)
			{
				Dns.EventLogger.LogEvent(CommonEventLogConstants.Tuple_NoMachineDnsServerList, null, new object[0]);
				return;
			}
			this.InitializeServerList(machineDnsServerList);
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x000B16F8 File Offset: 0x000AF8F8
		public void InitializeServerList(IPAddress[] addresses)
		{
			DnsServerList dnsServerList = this.serverList;
			if (dnsServerList == null || !dnsServerList.IsAddressListIdentical(addresses))
			{
				DnsServerList dnsServerList2 = new DnsServerList();
				dnsServerList2.Initialize(addresses);
				DnsServerList dnsServerList3 = Interlocked.CompareExchange<DnsServerList>(ref this.serverList, dnsServerList2, dnsServerList);
				if (dnsServerList3 == dnsServerList)
				{
					if (dnsServerList3 != null)
					{
						dnsServerList3.Dispose();
						return;
					}
				}
				else
				{
					dnsServerList2.Dispose();
				}
			}
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x000B1748 File Offset: 0x000AF948
		public IAsyncResult BeginResolveToAddresses(string domainName, AddressFamily type, AsyncCallback requestCallback, object state)
		{
			return this.BeginResolveToAddresses(domainName, type, this.ServerList, this.Options, requestCallback, state);
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x000B1761 File Offset: 0x000AF961
		public IAsyncResult BeginResolveToAddresses(string domainName, AddressFamily type, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			return this.BeginResolveToAddresses(domainName, type, this.ServerList, options, requestCallback, state);
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x000B1778 File Offset: 0x000AF978
		public IAsyncResult BeginResolveToAddresses(string domainName, AddressFamily type, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			if (string.IsNullOrEmpty(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			domainName = Dns.CleanseDomainName(domainName);
			if (!Dns.IsValidQuestion(domainName))
			{
				throw new ArgumentException(NetException.InvalidFQDN(domainName), "domainName");
			}
			if (type != AddressFamily.InterNetwork && type != AddressFamily.InterNetworkV6 && type != AddressFamily.Unspecified)
			{
				throw new ArgumentException(NetException.InvalidIPType, "type");
			}
			AddressRequest addressRequest = new AddressRequest(list, options, type, this, requestCallback, state);
			addressRequest.MaxWireDataSize = this.MaxDataPerRequest;
			addressRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = addressRequest.Resolve(domainName);
			}
			catch (Exception)
			{
				addressRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x000B1834 File Offset: 0x000AFA34
		public IAsyncResult BeginResolveToAddresses(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			if (string.IsNullOrEmpty(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			domainName = Dns.CleanseDomainName(domainName);
			if (!Dns.IsValidQuestion(domainName))
			{
				throw new ArgumentException(NetException.InvalidFQDN(domainName), "domainName");
			}
			AddressRequest addressRequest = new AddressRequest(list, options, this, requestCallback, state);
			addressRequest.MaxWireDataSize = this.MaxDataPerRequest;
			addressRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = addressRequest.Resolve(domainName);
			}
			catch (Exception)
			{
				addressRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x000B18D0 File Offset: 0x000AFAD0
		public IAsyncResult BeginResolveToMailServers(string domainName, AsyncCallback requestCallback, object state)
		{
			return this.BeginResolveToMailServers(domainName, this.ServerList, this.Options, requestCallback, state);
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x000B18E7 File Offset: 0x000AFAE7
		public IAsyncResult BeginResolveToMailServers(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			return this.BeginResolveToMailServers(domainName, this.ServerList, options, requestCallback, state);
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x000B18FC File Offset: 0x000AFAFC
		public IAsyncResult BeginResolveToMailServers(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			if (string.IsNullOrEmpty(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			domainName = Dns.CleanseDomainName(domainName);
			if (!Dns.IsValidQuestion(domainName))
			{
				throw new ArgumentException(NetException.InvalidFQDN(domainName), "domainName");
			}
			MxRequest mxRequest = new MxRequest(list, options, this.LocalIPAddresses, 0, this, requestCallback, state);
			DnsLog.Log(mxRequest, "ResolveMailServer for domain {0}. Servers={1}; options={2}; TimeOut={3}", new object[]
			{
				domainName,
				list,
				options,
				mxRequest.RequestTimeout
			});
			mxRequest.MaxWireDataSize = this.MaxDataPerRequest;
			mxRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = mxRequest.Resolve(domainName);
			}
			catch (Exception)
			{
				mxRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x000B19D0 File Offset: 0x000AFBD0
		public IAsyncResult BeginResolveToMailServers(string domainName, bool implicitSearch, AddressFamily type, AsyncCallback requestCallback, object state)
		{
			if (string.IsNullOrEmpty(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			domainName = Dns.CleanseDomainName(domainName);
			if (!Dns.IsValidQuestion(domainName))
			{
				throw new ArgumentException(NetException.InvalidFQDN(domainName), "domainName");
			}
			MxRequest mxRequest = new MxRequest(this.serverList, this.Options, this.LocalIPAddresses, 0, type, implicitSearch, this, requestCallback, state);
			mxRequest.MaxWireDataSize = this.MaxDataPerRequest;
			mxRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = mxRequest.Resolve(domainName);
			}
			catch (Exception)
			{
				mxRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x000B1A7C File Offset: 0x000AFC7C
		public IAsyncResult BeginRetrieveTextRecords(string domainName, AsyncCallback requestCallback, object state)
		{
			return this.BeginRetrieveTextRecords(domainName, this.ServerList, this.Options, requestCallback, state);
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x000B1A93 File Offset: 0x000AFC93
		public IAsyncResult BeginRetrieveTextRecords(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			return this.BeginRetrieveTextRecords(domainName, this.ServerList, options, requestCallback, state);
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x000B1AA8 File Offset: 0x000AFCA8
		public IAsyncResult BeginRetrieveTextRecords(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			if (string.IsNullOrEmpty(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			domainName = Dns.CleanseDomainName(domainName);
			if (!Dns.IsValidQuestion(domainName))
			{
				throw new ArgumentException(NetException.InvalidFQDN(domainName), "domainName");
			}
			TxtRequest txtRequest = new TxtRequest(list, options, this, requestCallback, state);
			txtRequest.MaxWireDataSize = this.MaxDataPerRequest;
			txtRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = txtRequest.Resolve(domainName);
			}
			catch (Exception)
			{
				txtRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x000B1B44 File Offset: 0x000AFD44
		public IAsyncResult BeginRetrieveSoaRecords(string domainName, AsyncCallback requestCallback, object state)
		{
			return this.BeginRetrieveSoaRecords(domainName, this.ServerList, this.Options, requestCallback, state);
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x000B1B5B File Offset: 0x000AFD5B
		public IAsyncResult BeginRetrieveSoaRecords(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			return this.BeginRetrieveSoaRecords(domainName, this.ServerList, options, requestCallback, state);
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x000B1B70 File Offset: 0x000AFD70
		public IAsyncResult BeginRetrieveSoaRecords(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			if (string.IsNullOrEmpty(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			domainName = Dns.CleanseDomainName(domainName);
			if (!Dns.IsValidQuestion(domainName))
			{
				throw new ArgumentException(NetException.InvalidFQDN(domainName), "domainName");
			}
			SoaRequest soaRequest = new SoaRequest(list, options, this, requestCallback, state);
			soaRequest.MaxWireDataSize = this.MaxDataPerRequest;
			soaRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = soaRequest.Resolve(domainName);
			}
			catch (Exception)
			{
				soaRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x000B1C0C File Offset: 0x000AFE0C
		public IAsyncResult BeginRetrieveCNameRecords(string domainName, AsyncCallback requestCallback, object state)
		{
			return this.BeginRetrieveCNameRecords(domainName, this.ServerList, this.Options, requestCallback, state);
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x000B1C23 File Offset: 0x000AFE23
		public IAsyncResult BeginRetrieveCNameRecords(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			return this.BeginRetrieveCNameRecords(domainName, this.ServerList, options, requestCallback, state);
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x000B1C38 File Offset: 0x000AFE38
		public IAsyncResult BeginRetrieveCNameRecords(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			if (string.IsNullOrEmpty(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			domainName = Dns.CleanseDomainName(domainName);
			if (!Dns.IsValidQuestion(domainName))
			{
				throw new ArgumentException(NetException.InvalidFQDN(domainName), "domainName");
			}
			CNameRequest cnameRequest = new CNameRequest(list, options, this, requestCallback, state);
			cnameRequest.MaxWireDataSize = this.MaxDataPerRequest;
			cnameRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = cnameRequest.Resolve(domainName);
			}
			catch (Exception)
			{
				cnameRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x000B1CD4 File Offset: 0x000AFED4
		public IAsyncResult BeginResolveToNames(IPAddress address, AsyncCallback requestCallback, object state)
		{
			return this.BeginResolveToNames(address, this.ServerList, this.Options, requestCallback, state);
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x000B1CEB File Offset: 0x000AFEEB
		public IAsyncResult BeginResolveToNames(IPAddress address, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			return this.BeginResolveToNames(address, this.ServerList, options, requestCallback, state);
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x000B1D00 File Offset: 0x000AFF00
		public IAsyncResult BeginResolveToNames(IPAddress address, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			PtrRequest ptrRequest = new PtrRequest(list, options, this, requestCallback, state);
			ptrRequest.MaxWireDataSize = this.MaxDataPerRequest;
			ptrRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = ptrRequest.Resolve(address);
			}
			catch (Exception)
			{
				ptrRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x000B1D60 File Offset: 0x000AFF60
		public IAsyncResult BeginRetrieveSrvRecords(string name, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			SrvRequest srvRequest = new SrvRequest(this.ServerList, options, this, requestCallback, state);
			srvRequest.MaxWireDataSize = this.MaxDataPerRequest;
			srvRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = srvRequest.Resolve(name);
			}
			catch (Exception)
			{
				srvRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x000B1DD8 File Offset: 0x000AFFD8
		public IAsyncResult BeginRetrieveNsRecords(string domainName, AsyncCallback requestCallback, object state)
		{
			return this.BeginRetrieveNsRecords(domainName, this.ServerList, this.Options, requestCallback, state);
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x000B1DEF File Offset: 0x000AFFEF
		public IAsyncResult BeginRetrieveNsRecords(string domainName, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			return this.BeginRetrieveNsRecords(domainName, this.ServerList, options, requestCallback, state);
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x000B1E04 File Offset: 0x000B0004
		public IAsyncResult BeginRetrieveNsRecords(string domainName, DnsServerList list, DnsQueryOptions options, AsyncCallback requestCallback, object state)
		{
			if (string.IsNullOrEmpty(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			domainName = Dns.CleanseDomainName(domainName);
			if (!Dns.IsValidQuestion(domainName))
			{
				throw new ArgumentException(NetException.InvalidFQDN(domainName), "domainName");
			}
			NsRequest nsRequest = new NsRequest(list, options, this, requestCallback, state);
			nsRequest.MaxWireDataSize = this.MaxDataPerRequest;
			nsRequest.RequestTimeout = DateTime.UtcNow + this.timeout;
			IAsyncResult result;
			try
			{
				result = nsRequest.Resolve(domainName);
			}
			catch (Exception)
			{
				nsRequest.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x000B1EA0 File Offset: 0x000B00A0
		internal static bool IsValidQuestion(string name)
		{
			return !string.IsNullOrEmpty(name) && name[0] != '*';
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x000B1EBA File Offset: 0x000B00BA
		internal static bool IsValidName(string name)
		{
			return Dns.ValidateName(DNSNameFormat.Domain, name) == DNSNameStatus.Valid;
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x000B1EC6 File Offset: 0x000B00C6
		internal static bool IsValidWildcardName(string name)
		{
			return Dns.ValidateName(DNSNameFormat.Wildcard, name) == DNSNameStatus.Valid;
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x000B1ED2 File Offset: 0x000B00D2
		internal static DNSNameStatus ValidateName(DNSNameFormat format, string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return (DNSNameStatus)DnsNativeMethods.ValidateName(name, (int)format);
			}
			return DNSNameStatus.InvalidName;
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x000B1EE8 File Offset: 0x000B00E8
		internal static string DnsStatusToString(DnsStatus key)
		{
			int num = (int)key;
			if (Dns.enumStatusString == null)
			{
				string[] names = Enum.GetNames(typeof(DnsStatus));
				Interlocked.Exchange<string[]>(ref Dns.enumStatusString, names);
			}
			if (num < 0 || num >= Dns.enumStatusString.Length)
			{
				return num.ToString("X8", NumberFormatInfo.InvariantInfo);
			}
			return Dns.enumStatusString[num];
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x000B1F41 File Offset: 0x000B0141
		internal static string TrimTrailingDot(string name)
		{
			if (string.IsNullOrEmpty(name) || name[name.Length - 1] != '.')
			{
				return name;
			}
			return name.Substring(0, name.Length - 1);
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x000B1F6E File Offset: 0x000B016E
		internal static string CleanseDomainName(string domainName)
		{
			domainName = Dns.TrimTrailingDot(domainName);
			return new IdnMapping().GetAscii(domainName);
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x000B1F84 File Offset: 0x000B0184
		private static AddressFamily GetDefaultAddressFamily()
		{
			AddressFamily result = AddressFamily.Unspecified;
			if (Socket.OSSupportsIPv4)
			{
				if (!Socket.OSSupportsIPv6)
				{
					result = AddressFamily.InterNetwork;
				}
			}
			else
			{
				if (!Socket.OSSupportsIPv6)
				{
					throw new NotSupportedException("Neither IPv4 nor IPv6 supported on this machine!");
				}
				result = AddressFamily.InterNetworkV6;
			}
			return result;
		}

		// Token: 0x040038F0 RID: 14576
		internal const int MaxTargetHosts = 100;

		// Token: 0x040038F1 RID: 14577
		private static int maxCnameRecords = 10;

		// Token: 0x040038F2 RID: 14578
		private static int maxSubQueries = 50;

		// Token: 0x040038F3 RID: 14579
		private int maxDataPerRequest = 8192;

		// Token: 0x040038F4 RID: 14580
		private DnsServerList serverList = new DnsServerList();

		// Token: 0x040038F5 RID: 14581
		private IEnumerable<IPAddress> localAddresses;

		// Token: 0x040038F6 RID: 14582
		private DnsQueryOptions queryOptions;

		// Token: 0x040038F7 RID: 14583
		private static string[] enumStatusString;

		// Token: 0x040038F8 RID: 14584
		private static ExEventLog eventLogger;

		// Token: 0x040038F9 RID: 14585
		private TimeSpan timeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x040038FA RID: 14586
		private TimeSpan queryRetryInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x040038FB RID: 14587
		private AddressFamily defaultAddressFamily = Dns.GetDefaultAddressFamily();
	}
}
