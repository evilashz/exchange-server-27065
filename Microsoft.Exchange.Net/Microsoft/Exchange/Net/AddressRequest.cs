using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BB9 RID: 3001
	internal sealed class AddressRequest : Request
	{
		// Token: 0x06004061 RID: 16481 RVA: 0x000AA5B0 File Offset: 0x000A87B0
		public AddressRequest(DnsServerList list, DnsQueryOptions flags, AddressFamily requestedAddressFamily, Dns dnsInstance, AsyncCallback requestCallback, object stateObject) : base(list, flags, dnsInstance, requestCallback, stateObject)
		{
			this.requestedAddressFamily = requestedAddressFamily;
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000AA5D2 File Offset: 0x000A87D2
		public AddressRequest(DnsServerList list, DnsQueryOptions flags, Dns dnsInstance, AsyncCallback requestCallback, object stateObject) : base(list, flags, dnsInstance, requestCallback, stateObject)
		{
			this.requestedAddressFamily = dnsInstance.DefaultAddressFamily;
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x000AA5F8 File Offset: 0x000A87F8
		private bool SetNextQueryType()
		{
			if (this.staticRecords)
			{
				return false;
			}
			DnsRecordType dnsRecordType;
			bool flag = Request.NextQueryType(this.requestedAddressFamily, this.addressList.Ipv4AddressCount, this.addressList.Ipv6AddressCount, this.currentType, out dnsRecordType);
			if (flag)
			{
				this.currentType = dnsRecordType;
			}
			return flag;
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x000AA644 File Offset: 0x000A8844
		private string NextLink
		{
			get
			{
				if (this.canonicalNames != null && this.canonicalNames.Count != 0)
				{
					return this.canonicalNames[this.canonicalNames.Count - 1].Host;
				}
				return this.domain;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x000AA67F File Offset: 0x000A887F
		protected override Request.Result InvalidDataResult
		{
			get
			{
				return AddressRequest.InvalidDataRequestResult;
			}
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000AA688 File Offset: 0x000A8888
		private int CacheOrInternalLookup(string name)
		{
			TimeSpan zero = TimeSpan.Zero;
			IPAddress[] array = null;
			if (!base.BypassCache)
			{
				array = base.FindEntries(name, this.requestedAddressFamily, false, out zero);
			}
			if (array == null)
			{
				array = Request.FindLocalHostEntries(name, this.requestedAddressFamily, out zero);
			}
			if (array != null)
			{
				this.addressList.AddRange(array);
				if (zero == TimeSpan.MaxValue)
				{
					this.staticRecords = true;
				}
				return array.Length;
			}
			return 0;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000AA6F0 File Offset: 0x000A88F0
		public IAsyncResult Resolve(string domainName)
		{
			ExTraceGlobals.DNSTracer.Information<string, DnsRecordType>((long)this.GetHashCode(), "Address request for server {0}, record type {1}", domainName, this.currentType);
			this.domain = domainName;
			this.queryFor = domainName;
			if (this.CacheOrInternalLookup(this.queryFor) > 0)
			{
				ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "Found cached IPAddresses for {0}", this.queryFor);
			}
			if (!this.SetNextQueryType())
			{
				base.Callback.InvokeCallback(new Request.Result(DnsStatus.Success, IPAddress.None, this.addressList.ToArray()));
			}
			else
			{
				base.PostRequest(domainName, this.currentType, null);
			}
			return base.Callback;
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000AA798 File Offset: 0x000A8998
		protected override bool ProcessData(DnsResult dnsResult, DnsAsyncRequest dnsAsyncRequest)
		{
			DnsStatus dnsStatus = dnsResult.Status;
			DnsRecordList list = dnsResult.List;
			bool flag = false;
			switch (dnsStatus)
			{
			case DnsStatus.Success:
			case DnsStatus.InfoTruncated:
				foreach (DnsRecord dnsRecord in list.EnumerateAnswers(this.currentType))
				{
					DnsAddressRecord dnsAddressRecord = (DnsAddressRecord)dnsRecord;
					if (this.queryFor.Equals(dnsAddressRecord.Name, StringComparison.OrdinalIgnoreCase))
					{
						this.addressList.Add(dnsAddressRecord.IPAddress);
					}
				}
				if (this.addressList.Count == 0)
				{
					dnsStatus = this.FollowChain(list);
					DnsStatus dnsStatus2 = dnsStatus;
					if (dnsStatus2 != DnsStatus.Success && dnsStatus2 == DnsStatus.Pending)
					{
						return false;
					}
				}
				if (dnsStatus == DnsStatus.Success)
				{
					flag = this.SetNextQueryType();
					goto IL_E1;
				}
				goto IL_E1;
			case DnsStatus.InfoNoRecords:
			case DnsStatus.ErrorInvalidData:
			case DnsStatus.ErrorExcessiveData:
				flag = this.SetNextQueryType();
				goto IL_E1;
			case DnsStatus.InfoDomainNonexistent:
				goto IL_E1;
			}
			base.Callback.ErrorCode = (int)dnsStatus;
			dnsStatus = DnsStatus.ErrorRetry;
			IL_E1:
			if (flag)
			{
				if (++this.subQueryCount < Dns.MaxSubQueries)
				{
					ExTraceGlobals.DNSTracer.Information<int, string>((long)this.GetHashCode(), "{0} Posting a query to find Alias for {1}", this.subQueryCount, this.queryFor);
					base.PostRequest(this.queryFor, this.currentType, null);
					return false;
				}
				if (this.addressList.Count > 0)
				{
					dnsStatus = DnsStatus.Success;
				}
				else
				{
					dnsStatus = DnsStatus.InfoTruncated;
				}
			}
			if (dnsStatus != DnsStatus.Success && this.addressList.Count > 0)
			{
				ExTraceGlobals.DNSTracer.Information((long)this.GetHashCode(), "Overwrite status with DnsStatus.Success because we have valid address in the list already.");
				dnsStatus = DnsStatus.Success;
			}
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "Address request complete [{0}].", Dns.DnsStatusToString(dnsStatus));
			base.Callback.InvokeCallback(new Request.Result(dnsStatus, dnsResult.Server, (dnsStatus == DnsStatus.Success) ? this.addressList.ToArray() : AddressRequest.EmptyIPAddressArray));
			return true;
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x000AA974 File Offset: 0x000A8B74
		private DnsStatus FollowChain(DnsRecordList records)
		{
			List<DnsCNameRecord> list = records.ExtractRecords<DnsCNameRecord>(DnsRecordType.CNAME, DnsCNameRecord.Comparer);
			if (list.Count == 0)
			{
				ExTraceGlobals.DNSTracer.Information((long)this.GetHashCode(), "No CNAME records found in response");
				return DnsStatus.InfoNoRecords;
			}
			if (this.canonicalNames == null)
			{
				this.canonicalNames = new List<DnsCNameRecord>();
			}
			int num = Request.FollowCNameChain(list, this.NextLink, this.canonicalNames);
			if (num == -1)
			{
				ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "CNAME chain too long. End of chain is {0}", this.NextLink);
				return DnsStatus.InfoNoRecords;
			}
			if (num == 0)
			{
				ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "No CNAME link found for {0}", this.NextLink);
				return DnsStatus.InfoNoRecords;
			}
			string nextLink = this.NextLink;
			ExTraceGlobals.DNSTracer.Information<string, string>((long)this.GetHashCode(), "CNAME chain for {0} leads to {1}", this.queryFor, nextLink);
			if (this.CacheOrInternalLookup(nextLink) > 0)
			{
				ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "Found cached IPAddresses for {0}", nextLink);
			}
			if (this.addressList.Count > 0)
			{
				return DnsStatus.Success;
			}
			foreach (DnsRecord dnsRecord in records.EnumerateAnswers(this.currentType))
			{
				DnsAddressRecord dnsAddressRecord = (DnsAddressRecord)dnsRecord;
				if (nextLink.Equals(dnsAddressRecord.Name, StringComparison.OrdinalIgnoreCase))
				{
					this.addressList.Add(dnsAddressRecord.IPAddress);
				}
			}
			if (this.addressList.Count > 0)
			{
				ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "Found IPAddresses for {0}", nextLink);
				return DnsStatus.Success;
			}
			if (++this.subQueryCount >= Dns.MaxSubQueries)
			{
				return DnsStatus.InfoTruncated;
			}
			this.queryFor = nextLink;
			ExTraceGlobals.DNSTracer.Information<int, string, DnsRecordType>((long)this.GetHashCode(), "{0} Posting a query to find Address records for {1}, record type {2}", this.subQueryCount, this.queryFor, this.currentType);
			base.PostRequest(this.queryFor, this.currentType, null);
			return DnsStatus.Pending;
		}

		// Token: 0x040037AA RID: 14250
		private static readonly IPAddress[] EmptyIPAddressArray = new IPAddress[0];

		// Token: 0x040037AB RID: 14251
		private static readonly Request.Result InvalidDataRequestResult = new Request.Result(DnsStatus.ErrorInvalidData, IPAddress.None, AddressRequest.EmptyIPAddressArray);

		// Token: 0x040037AC RID: 14252
		private string domain;

		// Token: 0x040037AD RID: 14253
		private string queryFor;

		// Token: 0x040037AE RID: 14254
		private List<DnsCNameRecord> canonicalNames;

		// Token: 0x040037AF RID: 14255
		private int subQueryCount;

		// Token: 0x040037B0 RID: 14256
		private AddressFamily requestedAddressFamily;

		// Token: 0x040037B1 RID: 14257
		private DnsRecordType currentType;

		// Token: 0x040037B2 RID: 14258
		private Request.HostAddressList addressList = new Request.HostAddressList();

		// Token: 0x040037B3 RID: 14259
		private bool staticRecords;
	}
}
