using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000031 RID: 49
	internal static class Util
	{
		// Token: 0x02000032 RID: 50
		public static class PerformanceCounters
		{
			// Token: 0x060000D0 RID: 208 RVA: 0x00005008 File Offset: 0x00003208
			public static void MessageValidatedWithResult(SenderIdResult result)
			{
				if (result == null)
				{
					throw new ArgumentNullException("result");
				}
				SenderIdPerfCounters.TotalMessagesValidated.Increment();
				switch (result.Status)
				{
				case SenderIdStatus.Pass:
					SenderIdPerfCounters.TotalMessagesWithPassResult.Increment();
					return;
				case SenderIdStatus.Neutral:
					SenderIdPerfCounters.TotalMessagesWithNeutralResult.Increment();
					return;
				case SenderIdStatus.SoftFail:
					SenderIdPerfCounters.TotalMessagesWithSoftFailResult.Increment();
					return;
				case SenderIdStatus.Fail:
					switch (result.FailReason)
					{
					case SenderIdFailReason.NotPermitted:
						SenderIdPerfCounters.TotalMessagesWithFailNotPermittedResult.Increment();
						return;
					case SenderIdFailReason.MalformedDomain:
						SenderIdPerfCounters.TotalMessagesWithFailMalformedDomainResult.Increment();
						return;
					case SenderIdFailReason.DomainDoesNotExist:
						SenderIdPerfCounters.TotalMessagesWithFailNonExistentDomainResult.Increment();
						return;
					default:
						throw new InvalidOperationException("Invalid FailReason");
					}
					break;
				case SenderIdStatus.None:
					SenderIdPerfCounters.TotalMessagesWithNoneResult.Increment();
					return;
				case SenderIdStatus.TempError:
					SenderIdPerfCounters.TotalMessagesWithTempErrorResult.Increment();
					return;
				case SenderIdStatus.PermError:
					SenderIdPerfCounters.TotalMessagesWithPermErrorResult.Increment();
					return;
				default:
					return;
				}
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x000050ED File Offset: 0x000032ED
			public static void DnsQueryPerformed()
			{
				SenderIdPerfCounters.TotalDnsQueries.Increment();
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x000050FA File Offset: 0x000032FA
			public static void NoPRA()
			{
				SenderIdPerfCounters.TotalMessagesWithNoPRA.Increment();
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00005107 File Offset: 0x00003307
			public static void MissingOriginatingIP()
			{
				SenderIdPerfCounters.TotalMessagesMissingOriginatingIP.Increment();
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00005114 File Offset: 0x00003314
			public static void MessageBypassedValidation()
			{
				SenderIdPerfCounters.TotalMessagesThatBypassedValidation.Increment();
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00005124 File Offset: 0x00003324
			public static void RemoveCounters()
			{
				SenderIdPerfCounters.TotalMessagesValidated.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithPassResult.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithNeutralResult.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithSoftFailResult.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithFailNotPermittedResult.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithFailMalformedDomainResult.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithFailNonExistentDomainResult.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithNoneResult.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithTempErrorResult.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithPermErrorResult.RawValue = 0L;
				SenderIdPerfCounters.TotalDnsQueries.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesWithNoPRA.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesMissingOriginatingIP.RawValue = 0L;
				SenderIdPerfCounters.TotalMessagesThatBypassedValidation.RawValue = 0L;
			}
		}

		// Token: 0x02000033 RID: 51
		public static class AsyncDns
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x000051D9 File Offset: 0x000033D9
			public static void SetDns(Dns dnsObject)
			{
				if (dnsObject == null)
				{
					throw new ArgumentNullException("dnsObject");
				}
				Util.AsyncDns.dns = dnsObject;
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x000051EF File Offset: 0x000033EF
			public static bool IsAcceptable(DnsStatus status)
			{
				return status == DnsStatus.Success || status == DnsStatus.InfoDomainNonexistent || status == DnsStatus.InfoNoRecords || status == DnsStatus.InfoTruncated;
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x00005202 File Offset: 0x00003402
			public static bool IsDnsServerListEmpty()
			{
				return Util.AsyncDns.dns.ServerList == null || Util.AsyncDns.dns.ServerList.Count == 0;
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x00005224 File Offset: 0x00003424
			public static IAsyncResult BeginTxtRecordQuery(string domain, AsyncCallback asyncCallback, object asyncState)
			{
				Util.PerformanceCounters.DnsQueryPerformed();
				return Util.AsyncDns.dns.BeginRetrieveTextRecords(domain, asyncCallback, asyncState);
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00005238 File Offset: 0x00003438
			public static DnsStatus EndTxtRecordQuery(IAsyncResult ar, out string[] text)
			{
				return Dns.EndRetrieveTextRecords(ar, out text);
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00005241 File Offset: 0x00003441
			public static IAsyncResult BeginARecordQuery(string domain, AddressFamily addressFamily, AsyncCallback asyncCallback, object asyncState)
			{
				Util.PerformanceCounters.DnsQueryPerformed();
				return Util.AsyncDns.dns.BeginResolveToAddresses(domain, addressFamily, asyncCallback, asyncState);
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00005256 File Offset: 0x00003456
			public static DnsStatus EndARecordQuery(IAsyncResult ar, out IPAddress[] addresses)
			{
				return Dns.EndResolveToAddresses(ar, out addresses);
			}

			// Token: 0x060000DD RID: 221 RVA: 0x0000525F File Offset: 0x0000345F
			public static IAsyncResult BeginMxRecordQuery(string domain, AddressFamily addressFamily, AsyncCallback asyncCallback, object asyncState)
			{
				Util.PerformanceCounters.DnsQueryPerformed();
				return Util.AsyncDns.dns.BeginResolveToMailServers(domain, false, addressFamily, asyncCallback, asyncState);
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00005275 File Offset: 0x00003475
			public static DnsStatus EndMxRecordQuery(IAsyncResult ar, out TargetHost[] hosts)
			{
				return Dns.EndResolveToMailServers(ar, out hosts);
			}

			// Token: 0x060000DF RID: 223 RVA: 0x0000527E File Offset: 0x0000347E
			public static IAsyncResult BeginPtrRecordQuery(IPAddress ipAddress, AsyncCallback asyncCallback, object asyncState)
			{
				Util.PerformanceCounters.DnsQueryPerformed();
				return Util.AsyncDns.dns.BeginResolveToNames(ipAddress, asyncCallback, asyncState);
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x00005292 File Offset: 0x00003492
			public static DnsStatus EndPtrRecordQuery(IAsyncResult ar, out string[] domains)
			{
				return Dns.EndResolveToNames(ar, out domains);
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x0000529C File Offset: 0x0000349C
			public static bool IsValidName(string name)
			{
				if (string.IsNullOrEmpty(name) || !Dns.IsValidQuestion(name))
				{
					return false;
				}
				DNSNameStatus dnsnameStatus = Dns.ValidateName(DNSNameFormat.Domain, name);
				return (dnsnameStatus == DNSNameStatus.Valid || dnsnameStatus == DNSNameStatus.NonRFCName) && !string.Equals(name.Trim(), ".", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000084 RID: 132
			private static Dns dns = TransportFacades.Dns;
		}
	}
}
