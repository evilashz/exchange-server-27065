using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006CD RID: 1741
	internal sealed class DnsTroubleshooter
	{
		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x0012ADCC File Offset: 0x00128FCC
		private static Dns Client
		{
			get
			{
				if (-1 == DnsTroubleshooter.lastRenewal || Globals.GetTickDifference(DnsTroubleshooter.lastRenewal, Environment.TickCount) > 60000UL)
				{
					ExTraceGlobals.DnsTroubleshooterTracer.TraceInformation<int>(0, 0L, "Updating server list {0}", Environment.TickCount);
					DnsTroubleshooter.sClient.InitializeServerList(DnsServerList.GetAdapterDnsServerList(Guid.Empty, false, false));
					Interlocked.Exchange(ref DnsTroubleshooter.lastRenewal, Environment.TickCount);
				}
				return DnsTroubleshooter.sClient;
			}
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x0012AE3C File Offset: 0x0012903C
		public static Task DiagnoseDnsProblemForDomainController(string serverFqdn)
		{
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			string addomainNameForServer = DnsTroubleshooter.GetADDomainNameForServer(serverFqdn);
			DnsTroubleshooter.DnsQueryContext context = new DnsTroubleshooter.DnsQueryContext(addomainNameForServer, serverFqdn);
			return DnsTroubleshooter.QuerySrvRecords(context).ContinueWith(new Action<Task<DnsTroubleshooter.SrvQueryResult>>(DnsTroubleshooter.CheckDnsResult), TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x0012AE84 File Offset: 0x00129084
		public static Task DiagnoseDnsProblemForDomain(string domainFqdn)
		{
			if (string.IsNullOrEmpty(domainFqdn))
			{
				throw new ArgumentNullException("domainFqdn");
			}
			DnsTroubleshooter.DnsQueryContext context = new DnsTroubleshooter.DnsQueryContext(domainFqdn, null);
			return DnsTroubleshooter.QuerySrvRecords(context).ContinueWith(new Action<Task<DnsTroubleshooter.SrvQueryResult>>(DnsTroubleshooter.CheckDnsResult), TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0012AEC4 File Offset: 0x001290C4
		private static string GetADDomainNameForServer(string serverFqdn)
		{
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			string text = null;
			try
			{
				text = NativeHelpers.GetPrimaryDomainInformationFromServer(serverFqdn);
			}
			catch (CannotGetDomainInfoException arg)
			{
				ExTraceGlobals.DnsTroubleshooterTracer.TraceError<string, CannotGetDomainInfoException>(0L, "{0} CannotGetDomainInfoException Error {1}", serverFqdn, arg);
			}
			catch (ADTransientException arg2)
			{
				ExTraceGlobals.DnsTroubleshooterTracer.TraceError<string, ADTransientException>(0L, "{0} ADTransientException Error {1}", serverFqdn, arg2);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = serverFqdn.Substring(serverFqdn.IndexOf(".") + 1);
			}
			return text;
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0012AF80 File Offset: 0x00129180
		private static Task<DnsTroubleshooter.SrvQueryResult> QuerySrvRecords(DnsTroubleshooter.DnsQueryContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("state");
			}
			context.SrvQuery = string.Format("_ldap._tcp.dc._msdcs.{0}", context.DomainFqdn);
			Dns client = DnsTroubleshooter.Client;
			return Task.Factory.FromAsync<string, DnsQueryOptions, DnsTroubleshooter.SrvQueryResult>(new Func<string, DnsQueryOptions, AsyncCallback, object, IAsyncResult>(client.BeginRetrieveSrvRecords), delegate(IAsyncResult x)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2244357437U);
				SrvRecord[] srvRecords;
				DnsStatus status = Dns.EndResolveToSrvRecords(x, out srvRecords);
				return new DnsTroubleshooter.SrvQueryResult(status, srvRecords);
			}, context.SrvQuery, DnsQueryOptions.UseTcpOnly | DnsQueryOptions.BypassCache, context, TaskCreationOptions.AttachedToParent);
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x0012AFF4 File Offset: 0x001291F4
		private static void CheckDnsResult(Task<DnsTroubleshooter.SrvQueryResult> task)
		{
			if (task == null)
			{
				throw new ArgumentException("task");
			}
			if (task.AsyncState == null)
			{
				throw new ArgumentException("Task.AsyncContext sholdn't be null");
			}
			DnsTroubleshooter.DnsQueryContext dnsQueryContext = task.AsyncState as DnsTroubleshooter.DnsQueryContext;
			if (dnsQueryContext == null)
			{
				throw new ArgumentException("Invalid async context");
			}
			ExTraceGlobals.DnsTroubleshooterTracer.TraceFunction<string, TaskStatus>(0L, "DiagnoseError - {0} Task Result {1}", dnsQueryContext.DomainFqdn, task.Status);
			if (task.Status != TaskStatus.RanToCompletion)
			{
				ExTraceGlobals.DnsTroubleshooterTracer.TraceError<string>(0L, "{0} Error Dns Query", dnsQueryContext.DomainFqdn);
				if (task.Exception != null)
				{
					Exception innerException = task.Exception.Flatten().InnerException;
					ExTraceGlobals.DnsTroubleshooterTracer.TraceError<string, Exception>(0L, "{0} Error Dns Query {1}", dnsQueryContext.DomainFqdn, innerException);
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DnsThroubleshooterError, dnsQueryContext.DomainFqdn, new object[]
					{
						dnsQueryContext.DomainFqdn,
						innerException.Message
					});
				}
				return;
			}
			DnsTroubleshooter.SrvQueryResult result = task.Result;
			DnsTroubleshooter.AnalyzeDnsResultAndLogEvent(dnsQueryContext, task.Result);
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0012B0EC File Offset: 0x001292EC
		private static void AnalyzeDnsResultAndLogEvent(DnsTroubleshooter.DnsQueryContext context, DnsTroubleshooter.SrvQueryResult result)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			if (ExTraceGlobals.DnsTroubleshooterTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (!result.SrvRecords.IsNullOrEmpty<SrvRecord>())
				{
					foreach (SrvRecord srvRecord in result.SrvRecords)
					{
						stringBuilder.Append(srvRecord.TargetHost);
						stringBuilder.Append(",");
					}
				}
				ExTraceGlobals.DnsTroubleshooterTracer.TraceDebug(0L, "Domain Fqdn {0}. Server Fqdn {1}. Query {2}. ResultStatus {3}. Result Records {4}", new object[]
				{
					context.DomainFqdn,
					context.ServerFqdn ?? "<NULL>",
					context.SrvQuery,
					result.Status,
					stringBuilder.ToString()
				});
			}
			DnsStatus status = result.Status;
			switch (status)
			{
			case DnsStatus.Success:
			{
				if (result.SrvRecords.IsNullOrEmpty<SrvRecord>())
				{
					ExTraceGlobals.DnsTroubleshooterTracer.TraceDebug<string, DnsStatus>(0L, "{0} - Status {1}. No records found", context.DomainFqdn, result.Status);
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DNS_OTHER, context.DomainFqdn, new object[]
					{
						result.Status,
						context.DomainFqdn,
						context.SrvQuery
					});
					return;
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				bool flag = false;
				foreach (SrvRecord srvRecord2 in result.SrvRecords)
				{
					stringBuilder2.AppendLine(srvRecord2.TargetHost);
					if (context.ServerFqdn != null && context.ServerFqdn.Equals(srvRecord2.TargetHost, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
					}
				}
				ExTraceGlobals.DnsTroubleshooterTracer.TraceDebug<string, DnsStatus, bool>(0L, "{0} - Status {1}. isHostAdvertisedInDns {2}", context.DomainFqdn, result.Status, flag);
				if (string.IsNullOrEmpty(context.ServerFqdn))
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DNS_NO_ERROR, context.DomainFqdn, new object[]
					{
						context.DomainFqdn,
						context.SrvQuery,
						stringBuilder2.ToString()
					});
					return;
				}
				if (flag)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DNS_NO_ERROR_DC_FOUND, context.ServerFqdn, new object[]
					{
						context.ServerFqdn,
						context.SrvQuery,
						stringBuilder2.ToString()
					});
					return;
				}
				string listOfZones = DnsTroubleshooter.GetListOfZones(context.DomainFqdn);
				string listOfClientDnsServerAddresses = DnsTroubleshooter.GetListOfClientDnsServerAddresses();
				ExTraceGlobals.DnsTroubleshooterTracer.TraceDebug(0L, "{0} - Status {1}. Zones {2}. Dns Servers {3}", new object[]
				{
					context.DomainFqdn,
					result.Status,
					listOfZones,
					listOfClientDnsServerAddresses
				});
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DNS_NO_ERROR_DC_NOT_FOUND, context.ServerFqdn, new object[]
				{
					context.ServerFqdn,
					context.DomainFqdn,
					context.SrvQuery,
					stringBuilder2.ToString(),
					listOfClientDnsServerAddresses,
					listOfZones
				});
				return;
			}
			case DnsStatus.InfoNoRecords:
				break;
			case DnsStatus.InfoDomainNonexistent:
			{
				string listOfZones = DnsTroubleshooter.GetListOfZones(context.DomainFqdn);
				string listOfClientDnsServerAddresses = DnsTroubleshooter.GetListOfClientDnsServerAddresses();
				ExTraceGlobals.DnsTroubleshooterTracer.TraceDebug(0L, "{0} - Status {1}. Zones {2}. Dns Servers {3}", new object[]
				{
					context.DomainFqdn,
					result.Status,
					listOfZones,
					listOfClientDnsServerAddresses
				});
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DNS_NAME_ERROR, context.DomainFqdn, new object[]
				{
					NativeMethods.HRESULT_FROM_WIN32(9003U).ToString("X"),
					context.DomainFqdn,
					context.SrvQuery,
					listOfClientDnsServerAddresses,
					listOfZones
				});
				return;
			}
			default:
				switch (status)
				{
				case DnsStatus.ErrorRetry:
				case DnsStatus.ErrorTimeout:
				{
					string listOfClientDnsServerAddresses = DnsTroubleshooter.GetListOfClientDnsServerAddresses();
					ExTraceGlobals.DnsTroubleshooterTracer.TraceDebug<string, DnsStatus, string>(0L, "{0} - Status {1}. Dns Servers {2}", context.DomainFqdn, result.Status, listOfClientDnsServerAddresses);
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DNS_TIMEOUT, context.DomainFqdn, new object[]
					{
						NativeMethods.HRESULT_FROM_WIN32(1460U).ToString("X"),
						context.DomainFqdn,
						context.SrvQuery,
						listOfClientDnsServerAddresses
					});
					return;
				}
				case DnsStatus.ServerFailure:
				{
					string listOfZones = DnsTroubleshooter.GetListOfZones(context.DomainFqdn);
					string listOfClientDnsServerAddresses = DnsTroubleshooter.GetListOfClientDnsServerAddresses();
					ExTraceGlobals.DnsTroubleshooterTracer.TraceDebug(0L, "{0} - Status {1}. Zones {2}. Dns Servers {3}", new object[]
					{
						context.DomainFqdn,
						result.Status,
						listOfZones,
						listOfClientDnsServerAddresses
					});
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DNS_DIAG_SERVER_FAILURE, context.DomainFqdn, new object[]
					{
						NativeMethods.HRESULT_FROM_WIN32(9002U).ToString("X"),
						context.DomainFqdn,
						context.SrvQuery,
						listOfClientDnsServerAddresses,
						listOfZones
					});
					return;
				}
				}
				break;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DNS_OTHER, context.DomainFqdn, new object[]
			{
				result.Status,
				context.DomainFqdn,
				context.SrvQuery
			});
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x0012B61C File Offset: 0x0012981C
		private static string GetListOfZones(string dnsDomainName)
		{
			if (string.IsNullOrEmpty(dnsDomainName))
			{
				throw new ArgumentNullException("dnsDomainName");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(dnsDomainName);
			for (int i = dnsDomainName.IndexOf("."); i > 0; i = dnsDomainName.IndexOf("."))
			{
				dnsDomainName = dnsDomainName.Substring(i + 1);
				stringBuilder.AppendLine(dnsDomainName);
			}
			stringBuilder.AppendLine(DirectoryStrings.RootZone);
			return stringBuilder.ToString();
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0012B694 File Offset: 0x00129894
		private static string GetListOfClientDnsServerAddresses()
		{
			IPAddress[] adapterDnsServerList = DnsServerList.GetAdapterDnsServerList(Guid.Empty, false, false);
			if (!adapterDnsServerList.IsNullOrEmpty<IPAddress>())
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (IPAddress ipaddress in adapterDnsServerList)
				{
					stringBuilder.AppendLine(ipaddress.ToString());
				}
				return stringBuilder.ToString();
			}
			return DirectoryStrings.NoAddresses;
		}

		// Token: 0x040036CE RID: 14030
		private const uint UnhandledDnsQueryException = 2244357437U;

		// Token: 0x040036CF RID: 14031
		private const string SrvQueryFormat = "_ldap._tcp.dc._msdcs.{0}";

		// Token: 0x040036D0 RID: 14032
		private const uint DnsCacheRefreshEveryNMilliseconds = 60000U;

		// Token: 0x040036D1 RID: 14033
		private static Dns sClient = new Dns();

		// Token: 0x040036D2 RID: 14034
		private static int lastRenewal = -1;

		// Token: 0x020006CE RID: 1742
		internal class SrvQueryResult
		{
			// Token: 0x060050B8 RID: 20664 RVA: 0x0012B70D File Offset: 0x0012990D
			public SrvQueryResult(DnsStatus status, SrvRecord[] srvRecords)
			{
				this.SrvRecords = srvRecords;
				this.Status = status;
			}

			// Token: 0x17001A76 RID: 6774
			// (get) Token: 0x060050B9 RID: 20665 RVA: 0x0012B723 File Offset: 0x00129923
			// (set) Token: 0x060050BA RID: 20666 RVA: 0x0012B72B File Offset: 0x0012992B
			public SrvRecord[] SrvRecords { get; private set; }

			// Token: 0x17001A77 RID: 6775
			// (get) Token: 0x060050BB RID: 20667 RVA: 0x0012B734 File Offset: 0x00129934
			// (set) Token: 0x060050BC RID: 20668 RVA: 0x0012B73C File Offset: 0x0012993C
			public DnsStatus Status { get; private set; }
		}

		// Token: 0x020006CF RID: 1743
		internal class DnsQueryContext
		{
			// Token: 0x060050BD RID: 20669 RVA: 0x0012B745 File Offset: 0x00129945
			public DnsQueryContext(string domainFqdn, string serverFqdn = null)
			{
				if (string.IsNullOrEmpty(domainFqdn))
				{
					throw new ArgumentNullException("domainFqdn");
				}
				this.DomainFqdn = domainFqdn;
				this.ServerFqdn = serverFqdn;
			}

			// Token: 0x17001A78 RID: 6776
			// (get) Token: 0x060050BE RID: 20670 RVA: 0x0012B76E File Offset: 0x0012996E
			// (set) Token: 0x060050BF RID: 20671 RVA: 0x0012B776 File Offset: 0x00129976
			public string DomainFqdn { get; private set; }

			// Token: 0x17001A79 RID: 6777
			// (get) Token: 0x060050C0 RID: 20672 RVA: 0x0012B77F File Offset: 0x0012997F
			// (set) Token: 0x060050C1 RID: 20673 RVA: 0x0012B787 File Offset: 0x00129987
			public string ServerFqdn { get; private set; }

			// Token: 0x17001A7A RID: 6778
			// (get) Token: 0x060050C2 RID: 20674 RVA: 0x0012B790 File Offset: 0x00129990
			// (set) Token: 0x060050C3 RID: 20675 RVA: 0x0012B798 File Offset: 0x00129998
			public string SrvQuery { get; set; }
		}
	}
}
