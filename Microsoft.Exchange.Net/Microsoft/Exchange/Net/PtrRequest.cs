using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C64 RID: 3172
	internal sealed class PtrRequest : Request
	{
		// Token: 0x06004646 RID: 17990 RVA: 0x000BC006 File Offset: 0x000BA206
		public PtrRequest(DnsServerList list, DnsQueryOptions flags, Dns dnsInstance, AsyncCallback requestCallback, object stateObject) : base(list, flags, dnsInstance, requestCallback, stateObject)
		{
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x000BC015 File Offset: 0x000BA215
		protected override Request.Result InvalidDataResult
		{
			get
			{
				return PtrRequest.InvalidDataRequestResult;
			}
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x000BC01C File Offset: 0x000BA21C
		public IAsyncResult Resolve(IPAddress address)
		{
			ExTraceGlobals.DNSTracer.Information<IPAddress>((long)this.GetHashCode(), "PTR request for address {0}", address);
			this.queryFor = PtrRequest.ConstructPTRQuery(address);
			base.PostRequest(this.queryFor, DnsRecordType.PTR, null);
			return base.Callback;
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x000BC058 File Offset: 0x000BA258
		protected override bool ProcessData(DnsResult dnsResult, DnsAsyncRequest dnsAsyncRequest)
		{
			DnsStatus dnsStatus = dnsResult.Status;
			DnsRecordList list = dnsResult.List;
			List<string> list2 = null;
			switch (dnsStatus)
			{
			case DnsStatus.Success:
			case DnsStatus.InfoTruncated:
				list2 = new List<string>(list.Count);
				foreach (DnsRecord dnsRecord in list.EnumerateAnswers(DnsRecordType.PTR))
				{
					DnsPtrRecord dnsPtrRecord = (DnsPtrRecord)dnsRecord;
					if (!string.IsNullOrEmpty(dnsPtrRecord.Host) && (string.IsNullOrEmpty(dnsPtrRecord.Name) || DnsNativeMethods.DnsNameCompare(dnsPtrRecord.Name, this.queryFor)))
					{
						list2.Add(dnsPtrRecord.Host);
					}
				}
				if (list2.Count == 0 && dnsStatus != DnsStatus.InfoTruncated)
				{
					dnsStatus = DnsStatus.InfoNoRecords;
					goto IL_D4;
				}
				if (list2.Count > 0)
				{
					dnsStatus = DnsStatus.Success;
					goto IL_D4;
				}
				goto IL_D4;
			case DnsStatus.InfoNoRecords:
			case DnsStatus.InfoDomainNonexistent:
			case DnsStatus.ErrorInvalidData:
			case DnsStatus.ErrorExcessiveData:
				goto IL_D4;
			}
			dnsStatus = DnsStatus.ErrorRetry;
			IL_D4:
			ExTraceGlobals.DNSTracer.Information<string>((long)this.GetHashCode(), "PTR request complete [{0}].", Dns.DnsStatusToString(dnsStatus));
			base.Callback.InvokeCallback(new Request.Result(dnsStatus, dnsResult.Server, (dnsStatus == DnsStatus.Success) ? list2.ToArray() : PtrRequest.EmptyStringArray));
			return true;
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000BC190 File Offset: 0x000BA390
		internal static string ConstructPTRQuery(IPAddress address)
		{
			string result = string.Empty;
			if (address.AddressFamily == AddressFamily.InterNetwork)
			{
				byte[] addressBytes = address.GetAddressBytes();
				StringBuilder stringBuilder = new StringBuilder(28);
				stringBuilder.Append(addressBytes[3]);
				stringBuilder.Append('.');
				stringBuilder.Append(addressBytes[2]);
				stringBuilder.Append('.');
				stringBuilder.Append(addressBytes[1]);
				stringBuilder.Append('.');
				stringBuilder.Append(addressBytes[0]);
				stringBuilder.Append(".in-addr.arpa");
				result = stringBuilder.ToString();
			}
			else
			{
				if (address.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new ArgumentException(NetException.InvalidIPType, "address");
				}
				byte[] addressBytes2 = address.GetAddressBytes();
				char[] array = new char[addressBytes2.Length * 4 + PtrRequest.ipv6tail.Length];
				int destinationIndex = 0;
				for (int i = addressBytes2.Length - 1; i >= 0; i--)
				{
					uint num = (uint)addressBytes2[i];
					array[destinationIndex++] = PtrRequest.hexidecimal[(int)((UIntPtr)(num & 15U))];
					array[destinationIndex++] = '.';
					array[destinationIndex++] = PtrRequest.hexidecimal[(int)((UIntPtr)(num >> 4 & 15U))];
					array[destinationIndex++] = '.';
				}
				Array.Copy(PtrRequest.ipv6tail, 0, array, destinationIndex, PtrRequest.ipv6tail.Length);
				result = new string(array);
			}
			return result;
		}

		// Token: 0x04003AA9 RID: 15017
		private const DnsRecordType RecordType = DnsRecordType.PTR;

		// Token: 0x04003AAA RID: 15018
		private static readonly char[] hexidecimal = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		// Token: 0x04003AAB RID: 15019
		private static readonly char[] ipv6tail = new char[]
		{
			'I',
			'P',
			'6',
			'.',
			'A',
			'R',
			'P',
			'A'
		};

		// Token: 0x04003AAC RID: 15020
		private static readonly string[] EmptyStringArray = new string[0];

		// Token: 0x04003AAD RID: 15021
		private static readonly Request.Result InvalidDataRequestResult = new Request.Result(DnsStatus.ErrorInvalidData, IPAddress.None, PtrRequest.EmptyStringArray);

		// Token: 0x04003AAE RID: 15022
		private string queryFor;
	}
}
