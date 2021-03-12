using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C6C RID: 3180
	internal sealed class SrvRequest : Request
	{
		// Token: 0x06004679 RID: 18041 RVA: 0x000BC9D4 File Offset: 0x000BABD4
		public SrvRequest(DnsServerList list, DnsQueryOptions flags, Dns dnsInstance, AsyncCallback requestCallback, object stateObject) : base(list, flags, dnsInstance, requestCallback, stateObject)
		{
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x000BC9E3 File Offset: 0x000BABE3
		protected override Request.Result InvalidDataResult
		{
			get
			{
				return SrvRequest.InvalidDataRequestResult;
			}
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x000BC9EA File Offset: 0x000BABEA
		public IAsyncResult Resolve(string query)
		{
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "SRV request for service {0}", query);
			this.queryFor = query;
			base.PostRequest(this.queryFor, DnsRecordType.SRV, null);
			return base.Callback;
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x000BCA20 File Offset: 0x000BAC20
		protected override bool ProcessData(DnsResult dnsResult, DnsAsyncRequest dnsAsyncRequest)
		{
			DnsStatus dnsStatus = dnsResult.Status;
			DnsRecordList list = dnsResult.List;
			List<SrvRecord> list2 = null;
			switch (dnsStatus)
			{
			case DnsStatus.Success:
			case DnsStatus.InfoTruncated:
				list2 = new List<SrvRecord>(list.Count);
				foreach (DnsRecord dnsRecord in list.EnumerateAnswers(DnsRecordType.SRV))
				{
					DnsSrvRecord dnsSrvRecord = (DnsSrvRecord)dnsRecord;
					if (!string.IsNullOrEmpty(dnsSrvRecord.NameTarget) && (string.IsNullOrEmpty(dnsSrvRecord.Name) || DnsNativeMethods.DnsNameCompare(dnsSrvRecord.Name, this.queryFor)))
					{
						SrvRecord item = new SrvRecord(dnsSrvRecord.Name, dnsSrvRecord.NameTarget, dnsSrvRecord.Priority, dnsSrvRecord.Weight, dnsSrvRecord.Port);
						list2.Add(item);
					}
				}
				if (list2.Count == 0 && dnsStatus != DnsStatus.InfoTruncated)
				{
					dnsStatus = DnsStatus.InfoNoRecords;
					goto IL_F5;
				}
				if (list2.Count > 0)
				{
					dnsStatus = DnsStatus.Success;
					goto IL_F5;
				}
				goto IL_F5;
			case DnsStatus.InfoNoRecords:
			case DnsStatus.InfoDomainNonexistent:
			case DnsStatus.ErrorInvalidData:
			case DnsStatus.ErrorExcessiveData:
				goto IL_F5;
			}
			dnsStatus = DnsStatus.ErrorRetry;
			IL_F5:
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "SRV request complete [{0}].", Dns.DnsStatusToString(dnsStatus));
			base.Callback.InvokeCallback(new Request.Result(dnsStatus, dnsResult.Server, (dnsStatus == DnsStatus.Success) ? list2.ToArray() : SrvRequest.EmptySrvRecordArray));
			return true;
		}

		// Token: 0x04003AC1 RID: 15041
		private const DnsRecordType RecordType = DnsRecordType.SRV;

		// Token: 0x04003AC2 RID: 15042
		private static readonly SrvRecord[] EmptySrvRecordArray = new SrvRecord[0];

		// Token: 0x04003AC3 RID: 15043
		private static readonly Request.Result InvalidDataRequestResult = new Request.Result(DnsStatus.ErrorInvalidData, IPAddress.None, SrvRequest.EmptySrvRecordArray);

		// Token: 0x04003AC4 RID: 15044
		private string queryFor;
	}
}
