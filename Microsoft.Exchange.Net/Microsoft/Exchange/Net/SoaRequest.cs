using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C6A RID: 3178
	internal sealed class SoaRequest : Request
	{
		// Token: 0x0600466D RID: 18029 RVA: 0x000BC7C6 File Offset: 0x000BA9C6
		public SoaRequest(DnsServerList list, DnsQueryOptions flags, Dns dnsInstance, AsyncCallback requestCallback, object stateObject) : base(list, flags, dnsInstance, requestCallback, stateObject)
		{
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x000BC7D5 File Offset: 0x000BA9D5
		protected override Request.Result InvalidDataResult
		{
			get
			{
				return SoaRequest.InvalidDataRequestResult;
			}
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x000BC7DC File Offset: 0x000BA9DC
		public IAsyncResult Resolve(string query)
		{
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "Soa request for zone {0}", query);
			this.queryFor = query;
			base.PostRequest(this.queryFor, DnsRecordType.SOA, null);
			return base.Callback;
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x000BC810 File Offset: 0x000BAA10
		protected override bool ProcessData(DnsResult dnsResult, DnsAsyncRequest dnsAsyncRequest)
		{
			DnsStatus dnsStatus = dnsResult.Status;
			DnsRecordList list = dnsResult.List;
			List<SoaRecord> list2 = null;
			switch (dnsStatus)
			{
			case DnsStatus.Success:
			case DnsStatus.InfoTruncated:
				list2 = new List<SoaRecord>(list.Count);
				foreach (DnsRecord dnsRecord in list.EnumerateAnswers(DnsRecordType.SOA))
				{
					DnsSoaRecord dnsSoaRecord = (DnsSoaRecord)dnsRecord;
					if (!string.IsNullOrEmpty(dnsSoaRecord.PrimaryServer))
					{
						SoaRecord item = new SoaRecord(dnsSoaRecord.PrimaryServer, dnsSoaRecord.Administrator, dnsSoaRecord.SerialNumber, dnsSoaRecord.Refresh, dnsSoaRecord.Retry, dnsSoaRecord.Expire, dnsSoaRecord.DefaultTimeToLive);
						list2.Add(item);
					}
				}
				if (list2.Count == 0 && dnsStatus != DnsStatus.InfoTruncated)
				{
					dnsStatus = DnsStatus.InfoNoRecords;
					goto IL_E0;
				}
				if (list2.Count > 0)
				{
					dnsStatus = DnsStatus.Success;
					goto IL_E0;
				}
				goto IL_E0;
			case DnsStatus.InfoNoRecords:
			case DnsStatus.InfoDomainNonexistent:
			case DnsStatus.ErrorInvalidData:
			case DnsStatus.ErrorExcessiveData:
				goto IL_E0;
			}
			dnsStatus = DnsStatus.ErrorRetry;
			IL_E0:
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "Soa request complete [{0}].", Dns.DnsStatusToString(dnsStatus));
			base.Callback.InvokeCallback(new Request.Result(dnsStatus, dnsResult.Server, (dnsStatus == DnsStatus.Success) ? list2.ToArray() : SoaRequest.EmptySoaRecordArray));
			return true;
		}

		// Token: 0x04003AB8 RID: 15032
		private const DnsRecordType RecordType = DnsRecordType.SOA;

		// Token: 0x04003AB9 RID: 15033
		private static readonly SoaRecord[] EmptySoaRecordArray = new SoaRecord[0];

		// Token: 0x04003ABA RID: 15034
		private static readonly Request.Result InvalidDataRequestResult = new Request.Result(DnsStatus.ErrorInvalidData, IPAddress.None, SoaRequest.EmptySoaRecordArray);

		// Token: 0x04003ABB RID: 15035
		private string queryFor;
	}
}
