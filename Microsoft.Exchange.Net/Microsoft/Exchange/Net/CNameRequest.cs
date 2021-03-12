using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BDF RID: 3039
	internal sealed class CNameRequest : Request
	{
		// Token: 0x06004249 RID: 16969 RVA: 0x000B09D9 File Offset: 0x000AEBD9
		public CNameRequest(DnsServerList list, DnsQueryOptions flags, Dns dnsInstance, AsyncCallback requestCallback, object stateObject) : base(list, flags, dnsInstance, requestCallback, stateObject)
		{
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x0600424A RID: 16970 RVA: 0x000B09F3 File Offset: 0x000AEBF3
		// (set) Token: 0x0600424B RID: 16971 RVA: 0x000B09FB File Offset: 0x000AEBFB
		public int MaxCNameRecords
		{
			get
			{
				return this.maxCNameRecords;
			}
			set
			{
				this.maxCNameRecords = value;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x000B0A04 File Offset: 0x000AEC04
		protected override Request.Result InvalidDataResult
		{
			get
			{
				return CNameRequest.InvalidDataRequestResult;
			}
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x000B0A0B File Offset: 0x000AEC0B
		public IAsyncResult Resolve(string domainName)
		{
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "CNAME request for server {0}", domainName);
			this.queryFor = domainName;
			base.PostRequest(domainName, DnsRecordType.CNAME, null);
			return base.Callback;
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000B0A3C File Offset: 0x000AEC3C
		protected override bool ProcessData(DnsResult dnsResult, DnsAsyncRequest dnsAsyncRequest)
		{
			this.resolutionContext = new List<DnsCNameRecord>();
			DnsStatus dnsStatus = dnsResult.Status;
			DnsRecordList list = dnsResult.List;
			switch (dnsStatus)
			{
			case DnsStatus.Success:
			case DnsStatus.InfoTruncated:
				using (IEnumerator<DnsRecord> enumerator = list.EnumerateAnswers(DnsRecordType.CNAME).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DnsRecord dnsRecord = enumerator.Current;
						DnsCNameRecord dnsCNameRecord = (DnsCNameRecord)dnsRecord;
						if (string.IsNullOrEmpty(dnsCNameRecord.Name) || DnsNativeMethods.DnsNameCompare(dnsCNameRecord.Name, this.queryFor))
						{
							if (this.resolutionContext.Count < this.maxCNameRecords)
							{
								this.resolutionContext.Add(dnsCNameRecord);
							}
							else
							{
								this.truncation = true;
							}
						}
					}
					goto IL_BF;
				}
				break;
			case DnsStatus.InfoNoRecords:
			case DnsStatus.InfoDomainNonexistent:
			case DnsStatus.ErrorInvalidData:
			case DnsStatus.ErrorExcessiveData:
				goto IL_BF;
			}
			dnsStatus = DnsStatus.ErrorRetry;
			IL_BF:
			if (dnsStatus == DnsStatus.Success && this.resolutionContext.Count == 0)
			{
				dnsStatus = DnsStatus.InfoNoRecords;
			}
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "CNAME request complete [{0}].", Dns.DnsStatusToString(dnsStatus));
			DnsCNameRecord[] array = CNameRequest.EmptyCNameArray;
			if (dnsStatus == DnsStatus.Success || dnsStatus == DnsStatus.InfoTruncated)
			{
				array = this.resolutionContext.ToArray();
				if (this.truncation)
				{
					dnsStatus = DnsStatus.InfoTruncated;
				}
				else if (array.Length != 0)
				{
					dnsStatus = DnsStatus.Success;
				}
			}
			base.Callback.InvokeCallback(new Request.Result(dnsStatus, dnsResult.Server, array));
			return true;
		}

		// Token: 0x040038C9 RID: 14537
		public const int DefaultMaxCNameRecords = 256;

		// Token: 0x040038CA RID: 14538
		private const DnsRecordType RecordType = DnsRecordType.CNAME;

		// Token: 0x040038CB RID: 14539
		private static readonly DnsCNameRecord[] EmptyCNameArray = new DnsCNameRecord[0];

		// Token: 0x040038CC RID: 14540
		private static readonly Request.Result InvalidDataRequestResult = new Request.Result(DnsStatus.ErrorInvalidData, IPAddress.None, CNameRequest.EmptyCNameArray);

		// Token: 0x040038CD RID: 14541
		private string queryFor;

		// Token: 0x040038CE RID: 14542
		private List<DnsCNameRecord> resolutionContext;

		// Token: 0x040038CF RID: 14543
		private int maxCNameRecords = 256;

		// Token: 0x040038D0 RID: 14544
		private bool truncation;
	}
}
