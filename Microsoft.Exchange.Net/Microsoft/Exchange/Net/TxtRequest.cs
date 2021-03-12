using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C9F RID: 3231
	internal sealed class TxtRequest : Request
	{
		// Token: 0x06004730 RID: 18224 RVA: 0x000BFAC5 File Offset: 0x000BDCC5
		public TxtRequest(DnsServerList list, DnsQueryOptions flags, Dns dnsInstance, AsyncCallback requestCallback, object stateObject) : base(list, flags, dnsInstance, requestCallback, stateObject)
		{
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06004731 RID: 18225 RVA: 0x000BFADF File Offset: 0x000BDCDF
		// (set) Token: 0x06004732 RID: 18226 RVA: 0x000BFAE7 File Offset: 0x000BDCE7
		public int MaxTextRecords
		{
			get
			{
				return this.maxTextRecords;
			}
			set
			{
				this.maxTextRecords = value;
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06004733 RID: 18227 RVA: 0x000BFAF0 File Offset: 0x000BDCF0
		private string NextLink
		{
			get
			{
				if (this.canonicalNames.Count != 0)
				{
					return this.canonicalNames[this.canonicalNames.Count - 1].Host;
				}
				return this.domain;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06004734 RID: 18228 RVA: 0x000BFB23 File Offset: 0x000BDD23
		protected override Request.Result InvalidDataResult
		{
			get
			{
				return TxtRequest.InvalidDataRequestResult;
			}
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x000BFB2C File Offset: 0x000BDD2C
		public IAsyncResult Resolve(string domainName)
		{
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "TXT request for server {0}", domainName);
			this.domain = domainName;
			this.queryFor = domainName;
			base.PostRequest(domainName, DnsRecordType.TXT, null);
			return base.Callback;
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x000BFB70 File Offset: 0x000BDD70
		protected override bool ProcessData(DnsResult dnsResult, DnsAsyncRequest dnsAsyncRequest)
		{
			this.resolutionContext = new List<string>();
			DnsStatus dnsStatus = dnsResult.Status;
			DnsRecordList list = dnsResult.List;
			switch (dnsStatus)
			{
			case DnsStatus.Success:
			case DnsStatus.InfoTruncated:
			{
				foreach (DnsRecord dnsRecord in list.EnumerateAnswers(DnsRecordType.TXT))
				{
					DnsTxtRecord dnsTxtRecord = (DnsTxtRecord)dnsRecord;
					if (string.IsNullOrEmpty(dnsTxtRecord.Name) || DnsNativeMethods.DnsNameCompare(dnsTxtRecord.Name, this.queryFor))
					{
						DnsStatus status = dnsTxtRecord.Status;
						if (status != DnsStatus.Success)
						{
							dnsStatus = status;
							break;
						}
						if (this.resolutionContext.Count < this.maxTextRecords)
						{
							string text = dnsTxtRecord.Text;
							this.resolutionContext.Add(text);
						}
						else
						{
							this.truncation = true;
						}
					}
				}
				if (this.resolutionContext.Count != 0)
				{
					goto IL_FF;
				}
				dnsStatus = this.FollowChain(list);
				DnsStatus dnsStatus2 = dnsStatus;
				if (dnsStatus2 != DnsStatus.Success && dnsStatus2 == DnsStatus.Pending)
				{
					return false;
				}
				goto IL_FF;
			}
			case DnsStatus.InfoNoRecords:
			case DnsStatus.InfoDomainNonexistent:
			case DnsStatus.ErrorInvalidData:
			case DnsStatus.ErrorExcessiveData:
				goto IL_FF;
			}
			dnsStatus = DnsStatus.ErrorRetry;
			IL_FF:
			if (dnsStatus == DnsStatus.Success && this.resolutionContext.Count == 0)
			{
				dnsStatus = DnsStatus.InfoNoRecords;
			}
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "TXT request complete [{0}].", Dns.DnsStatusToString(dnsStatus));
			string[] array = TxtRequest.EmptyStringArray;
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

		// Token: 0x06004737 RID: 18231 RVA: 0x000BFD04 File Offset: 0x000BDF04
		private DnsStatus FollowChain(DnsRecordList records)
		{
			List<DnsCNameRecord> list = records.ExtractRecords<DnsCNameRecord>(DnsRecordType.CNAME, DnsCNameRecord.Comparer);
			if (list.Count == 0)
			{
				return DnsStatus.InfoNoRecords;
			}
			if (this.canonicalNames == null)
			{
				this.canonicalNames = new List<DnsCNameRecord>();
			}
			int num = Request.FollowCNameChain(list, this.NextLink, this.canonicalNames);
			if (num == -1)
			{
				return DnsStatus.InfoNoRecords;
			}
			if (num == 0)
			{
				return DnsStatus.InfoNoRecords;
			}
			string nextLink = this.NextLink;
			foreach (DnsRecord dnsRecord in records.EnumerateAnswers(DnsRecordType.TXT))
			{
				DnsTxtRecord dnsTxtRecord = (DnsTxtRecord)dnsRecord;
				if (nextLink.Equals(dnsTxtRecord.Name, StringComparison.OrdinalIgnoreCase))
				{
					if (this.resolutionContext.Count < this.maxTextRecords)
					{
						this.resolutionContext.Add(dnsTxtRecord.Text);
					}
					else
					{
						this.truncation = true;
					}
				}
			}
			if (this.resolutionContext.Count > 0)
			{
				return DnsStatus.Success;
			}
			if (++this.subQueryCount >= Dns.MaxSubQueries)
			{
				return DnsStatus.InfoTruncated;
			}
			this.queryFor = nextLink;
			ExTraceGlobals.DNSTracer.Information<int, string>((long)this.GetHashCode(), "{0} Posting a query to find TXT records for {1}", this.subQueryCount, this.queryFor);
			base.PostRequest(this.queryFor, DnsRecordType.TXT, null);
			return DnsStatus.Pending;
		}

		// Token: 0x04003C3C RID: 15420
		public const int DefaultMaxTextRecords = 256;

		// Token: 0x04003C3D RID: 15421
		private const DnsRecordType RecordType = DnsRecordType.TXT;

		// Token: 0x04003C3E RID: 15422
		private static readonly string[] EmptyStringArray = new string[0];

		// Token: 0x04003C3F RID: 15423
		private static readonly Request.Result InvalidDataRequestResult = new Request.Result(DnsStatus.ErrorInvalidData, IPAddress.None, TxtRequest.EmptyStringArray);

		// Token: 0x04003C40 RID: 15424
		private string domain;

		// Token: 0x04003C41 RID: 15425
		private string queryFor;

		// Token: 0x04003C42 RID: 15426
		private List<string> resolutionContext;

		// Token: 0x04003C43 RID: 15427
		private int maxTextRecords = 256;

		// Token: 0x04003C44 RID: 15428
		private bool truncation;

		// Token: 0x04003C45 RID: 15429
		private List<DnsCNameRecord> canonicalNames;

		// Token: 0x04003C46 RID: 15430
		private int subQueryCount;
	}
}
