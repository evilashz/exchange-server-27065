using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C60 RID: 3168
	internal sealed class NsRequest : Request
	{
		// Token: 0x0600463B RID: 17979 RVA: 0x000BBA5D File Offset: 0x000B9C5D
		public NsRequest(DnsServerList list, DnsQueryOptions flags, Dns dnsInstance, AsyncCallback requestCallback, object stateObject) : base(list, flags, dnsInstance, requestCallback, stateObject)
		{
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x000BBA6C File Offset: 0x000B9C6C
		protected override Request.Result InvalidDataResult
		{
			get
			{
				return NsRequest.InvalidDataRequestResult;
			}
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x000BBA73 File Offset: 0x000B9C73
		public IAsyncResult Resolve(string domainName)
		{
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "NS request for server {0}", domainName);
			this.queryFor = domainName;
			base.PostRequest(domainName, DnsRecordType.NS, null);
			return base.Callback;
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x000BBAA4 File Offset: 0x000B9CA4
		protected override bool ProcessData(DnsResult dnsResult, DnsAsyncRequest dnsAsyncRequest)
		{
			List<DnsNsRecord> list = null;
			DnsStatus dnsStatus = dnsResult.Status;
			DnsRecordList list2 = dnsResult.List;
			switch (dnsStatus)
			{
			case DnsStatus.Success:
			case DnsStatus.InfoTruncated:
				list = new List<DnsNsRecord>(list2.Count);
				using (IEnumerator<DnsRecord> enumerator = list2.EnumerateAnswers(DnsRecordType.NS).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DnsRecord dnsRecord = enumerator.Current;
						DnsNsRecord dnsNsRecord = (DnsNsRecord)dnsRecord;
						if (string.IsNullOrEmpty(dnsNsRecord.Name) || DnsNativeMethods.DnsNameCompare(dnsNsRecord.Name, this.queryFor))
						{
							list.Add(dnsNsRecord);
						}
					}
					goto IL_A1;
				}
				break;
			case DnsStatus.InfoNoRecords:
			case DnsStatus.InfoDomainNonexistent:
			case DnsStatus.ErrorInvalidData:
			case DnsStatus.ErrorExcessiveData:
				goto IL_A1;
			}
			dnsStatus = DnsStatus.ErrorRetry;
			IL_A1:
			if (dnsStatus == DnsStatus.Success && list.Count == 0)
			{
				dnsStatus = DnsStatus.InfoNoRecords;
			}
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "NS request complete [{0}].", Dns.DnsStatusToString(dnsStatus));
			DnsNsRecord[] array = NsRequest.EmptyNsArray;
			if (dnsStatus == DnsStatus.Success || dnsStatus == DnsStatus.InfoTruncated)
			{
				array = list.ToArray();
				if (array.Length != 0)
				{
					dnsStatus = DnsStatus.Success;
				}
			}
			base.Callback.InvokeCallback(new Request.Result(dnsStatus, dnsResult.Server, array));
			return true;
		}

		// Token: 0x04003AA1 RID: 15009
		private const DnsRecordType RecordType = DnsRecordType.NS;

		// Token: 0x04003AA2 RID: 15010
		private static readonly DnsNsRecord[] EmptyNsArray = new DnsNsRecord[0];

		// Token: 0x04003AA3 RID: 15011
		private static readonly Request.Result InvalidDataRequestResult = new Request.Result(DnsStatus.ErrorInvalidData, IPAddress.None, NsRequest.EmptyNsArray);

		// Token: 0x04003AA4 RID: 15012
		private string queryFor;
	}
}
