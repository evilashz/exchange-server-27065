using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Network;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network.Probes
{
	// Token: 0x0200022A RID: 554
	public class DnsServerForwardersProbe : ProbeWorkItem
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x00067400 File Offset: 0x00065600
		static DnsServerForwardersProbe()
		{
			bool flag = Environment.MachineName.Length >= 6 && Environment.MachineName.Substring(3, 3).ToUpperInvariant() == "MGT";
			if (flag)
			{
				DnsServerForwardersProbe.TargetDomainNames = DnsServerForwardersProbe.TargetDomainNamesForManagementForest;
				return;
			}
			DnsServerForwardersProbe.TargetDomainNames = DnsServerForwardersProbe.TargetDomainNamesForCapacityForest;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000674A4 File Offset: 0x000656A4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.TraceInformation("Begin DnsServerForwardersProbe.", new object[0]);
			this.dnsService = new WmiDnsClient(".");
			DnsServerForwardersProbe.ForwarderData[] array = this.DiscoverForwarders();
			if (array == null)
			{
				return;
			}
			this.TraceInformation("Test the health of each forwarder.", new object[0]);
			Parallel.ForEach<DnsServerForwardersProbe.ForwarderData>(array, new ParallelOptions
			{
				CancellationToken = cancellationToken
			}, delegate(DnsServerForwardersProbe.ForwarderData forwarder)
			{
				DnsServerForwardersProbe.TestForwarder(forwarder);
			});
			cancellationToken.ThrowIfCancellationRequested();
			Array.Sort<DnsServerForwardersProbe.ForwarderData>(array);
			this.TraceForwarders(array);
			this.AssessForwarders(array);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00067574 File Offset: 0x00065774
		private static void TestForwarder(DnsServerForwardersProbe.ForwarderData forwarder)
		{
			forwarder.IsUnhealthy = false;
			forwarder.SuccessCount = 0;
			Parallel.ForEach<string>(DnsServerForwardersProbe.TargetDomainNames, delegate(string domainName)
			{
				Win32DnsQueryResult<IPAddress> win32DnsQueryResult = Win32DnsQuery.ResolveRecordsA(domainName, forwarder.IPAddress);
				forwarder.TallyTestResult(win32DnsQueryResult.Success);
			});
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x000675D0 File Offset: 0x000657D0
		private void AssessForwarders(DnsServerForwardersProbe.ForwarderData[] forwarders)
		{
			this.TraceInformation("Assess the overall health of the forwarders.", new object[0]);
			if (forwarders.Length > 1 && forwarders[0].OriginalSequence != 0)
			{
				this.TraceInformation("The first forwarder is not the healthiest; reorder the list of forwarders.", new object[0]);
				this.SetForwarders(forwarders);
				string message = "Reordered forwarders: {0}.";
				object[] array = new object[1];
				array[0] = string.Join<IPAddress>(", ", from f in forwarders
				select f.IPAddress);
				this.TraceDebug(message, array);
			}
			int num = forwarders.Length;
			int num2 = forwarders.Count((DnsServerForwardersProbe.ForwarderData f) => f.IsUnhealthy);
			int num3 = num - num2;
			float num4 = (float)(num * 60) / 100f;
			if (num3 < 2)
			{
				this.TraceWarning("Fewer than {0} forwarders are healthy.", new object[]
				{
					2
				});
				this.SetRecoverySignal();
				return;
			}
			if ((float)num3 < num4)
			{
				this.TraceWarning("Fewer than {0}% of the forwarders are healthy.", new object[]
				{
					60
				});
				this.SetRecoverySignal();
				return;
			}
			this.TraceInformation("The number of healthy forwarders is sufficient.", new object[0]);
			this.ClearRecoverySignal();
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00067704 File Offset: 0x00065904
		private void ClearRecoverySignal()
		{
			FileInfo fileInfo = new FileInfo("D:\\NetworkMonitoring\\DnsServerForwardersProbe.signal");
			if (fileInfo.Exists)
			{
				try
				{
					this.TraceInformation("Prior persistent recovery signal exists; clearing it.", new object[0]);
					fileInfo.Delete();
				}
				catch (SystemException ex)
				{
					this.TraceWarning("Unable to clear persistent signal: " + ex.Message, new object[0]);
				}
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00067778 File Offset: 0x00065978
		private DnsServerForwardersProbe.ForwarderData[] DiscoverForwarders()
		{
			this.TraceInformation("Discover the configured forwarders.", new object[0]);
			IPAddress[] forwarders = this.dnsService.GetForwarders();
			if (forwarders == null || forwarders.Length == 0)
			{
				this.ReportFailure("No DNS forwarders are configured for this DNS server.");
				return null;
			}
			this.TraceDebug("Configured forwarders: {0}.", new object[]
			{
				string.Join<IPAddress>(", ", forwarders)
			});
			return forwarders.Select((IPAddress address, int index) => new DnsServerForwardersProbe.ForwarderData(address, index)).ToArray<DnsServerForwardersProbe.ForwarderData>();
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00067808 File Offset: 0x00065A08
		private void SetForwarders(DnsServerForwardersProbe.ForwarderData[] forwarders)
		{
			this.dnsService.SetForwarders((from f in forwarders
			select f.IPAddress).ToArray<IPAddress>());
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00067840 File Offset: 0x00065A40
		private void SetRecoverySignal()
		{
			this.TraceInformation("Set persisent recovery signal that forwarders need to be recalculated.", new object[0]);
			FileInfo fileInfo = new FileInfo("D:\\NetworkMonitoring\\DnsServerForwardersProbe.signal");
			if (fileInfo.Exists)
			{
				this.TraceDebug("Signal already exists with date {0} UTC; not changing it.", new object[]
				{
					fileInfo.LastWriteTimeUtc
				});
				return;
			}
			try
			{
				fileInfo.Directory.Create();
				using (FileStream fileStream = fileInfo.Create())
				{
					fileStream.Close();
				}
			}
			catch (SystemException ex)
			{
				this.TraceWarning("Unable to set persistent signal: " + ex.Message, new object[0]);
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000678F4 File Offset: 0x00065AF4
		private void ReportFailure(string stub)
		{
			this.TraceWarning(stub, new object[0]);
			throw new ApplicationException(stub);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0006790C File Offset: 0x00065B0C
		private void TraceDebug(string message, params object[] args)
		{
			if (args.Length == 0)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.NetworkTracer, base.TraceContext, message, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Probes\\DnsServerForwardersProbe.cs", 273);
				return;
			}
			WTFDiagnostics.TraceDebug(ExTraceGlobals.NetworkTracer, base.TraceContext, string.Format(message, args), null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Probes\\DnsServerForwardersProbe.cs", 277);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00067968 File Offset: 0x00065B68
		private void TraceForwarders(DnsServerForwardersProbe.ForwarderData[] forwarders)
		{
			this.TraceInformation("Forwarders data, in healthiest-first order:", new object[0]);
			this.TraceInformation(DnsServerForwardersProbe.ForwarderData.ToStringHeader, new object[0]);
			foreach (DnsServerForwardersProbe.ForwarderData forwarderData in forwarders)
			{
				this.TraceInformation(forwarderData.ToString(), new object[0]);
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x000679C0 File Offset: 0x00065BC0
		private void TraceInformation(string message, params object[] args)
		{
			if (args.Length == 0)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, base.TraceContext, message, null, "TraceInformation", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Probes\\DnsServerForwardersProbe.cs", 304);
				return;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, base.TraceContext, string.Format(message, args), null, "TraceInformation", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Probes\\DnsServerForwardersProbe.cs", 308);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00067A1C File Offset: 0x00065C1C
		private void TraceWarning(string message, params object[] args)
		{
			if (args.Length == 0)
			{
				WTFDiagnostics.TraceWarning(ExTraceGlobals.NetworkTracer, base.TraceContext, message, null, "TraceWarning", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Probes\\DnsServerForwardersProbe.cs", 321);
				return;
			}
			WTFDiagnostics.TraceWarning(ExTraceGlobals.NetworkTracer, base.TraceContext, string.Format(message, args), null, "TraceWarning", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Probes\\DnsServerForwardersProbe.cs", 325);
		}

		// Token: 0x04000BB4 RID: 2996
		private const int MinimumHealthyForwardersCount = 2;

		// Token: 0x04000BB5 RID: 2997
		private const int MinimumHealthyForwardersPercentage = 60;

		// Token: 0x04000BB6 RID: 2998
		private const string SignalFileName = "D:\\NetworkMonitoring\\DnsServerForwardersProbe.signal";

		// Token: 0x04000BB7 RID: 2999
		private static readonly string[] TargetDomainNames;

		// Token: 0x04000BB8 RID: 3000
		private static readonly string[] TargetDomainNamesForCapacityForest = new string[]
		{
			"brody.outlook.com.",
			"na01b.map.protection.outlook.com.",
			"www.bing.com."
		};

		// Token: 0x04000BB9 RID: 3001
		private static readonly string[] TargetDomainNamesForManagementForest = new string[]
		{
			"www.bing.com.",
			"www.yahoo.com.",
			"www.google.com."
		};

		// Token: 0x04000BBA RID: 3002
		private WmiDnsClient dnsService;

		// Token: 0x0200022B RID: 555
		private class ForwarderData : IComparable<DnsServerForwardersProbe.ForwarderData>
		{
			// Token: 0x06000F8F RID: 3983 RVA: 0x00067A7F File Offset: 0x00065C7F
			public ForwarderData(IPAddress ipAddress, int sequence)
			{
				this.IPAddress = ipAddress;
				this.OriginalSequence = sequence;
			}

			// Token: 0x170002FD RID: 765
			// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00067A98 File Offset: 0x00065C98
			public static string ToStringHeader
			{
				get
				{
					return string.Format("  {0,-15}  {1,11}  {2,12}  {3,11}", new object[]
					{
						"IPAddress",
						"OriginalSeq",
						"SuccessCount",
						"IsUnhealthy"
					});
				}
			}

			// Token: 0x170002FE RID: 766
			// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00067AD7 File Offset: 0x00065CD7
			// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00067ADF File Offset: 0x00065CDF
			public IPAddress IPAddress { get; private set; }

			// Token: 0x170002FF RID: 767
			// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00067AE8 File Offset: 0x00065CE8
			// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00067AF0 File Offset: 0x00065CF0
			public bool IsUnhealthy { get; set; }

			// Token: 0x17000300 RID: 768
			// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00067AF9 File Offset: 0x00065CF9
			// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00067B01 File Offset: 0x00065D01
			public int OriginalSequence { get; private set; }

			// Token: 0x17000301 RID: 769
			// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00067B0A File Offset: 0x00065D0A
			// (set) Token: 0x06000F98 RID: 3992 RVA: 0x00067B12 File Offset: 0x00065D12
			public int SuccessCount
			{
				get
				{
					return this.successCount;
				}
				set
				{
					this.successCount = value;
				}
			}

			// Token: 0x06000F99 RID: 3993 RVA: 0x00067B1B File Offset: 0x00065D1B
			public void TallyTestResult(bool result)
			{
				if (result)
				{
					Interlocked.Increment(ref this.successCount);
					return;
				}
				this.IsUnhealthy = true;
			}

			// Token: 0x06000F9A RID: 3994 RVA: 0x00067B34 File Offset: 0x00065D34
			public override string ToString()
			{
				return string.Format("  {0,-15}  {1,11}  {2,12}  {3,11}", new object[]
				{
					this.IPAddress,
					this.OriginalSequence,
					this.SuccessCount,
					this.IsUnhealthy
				});
			}

			// Token: 0x06000F9B RID: 3995 RVA: 0x00067B88 File Offset: 0x00065D88
			int IComparable<DnsServerForwardersProbe.ForwarderData>.CompareTo(DnsServerForwardersProbe.ForwarderData other)
			{
				if (other == null)
				{
					return 1;
				}
				if (object.ReferenceEquals(this, other))
				{
					return 0;
				}
				int num = other.SuccessCount - this.SuccessCount;
				if (num == 0)
				{
					num = this.OriginalSequence - other.OriginalSequence;
				}
				return num;
			}

			// Token: 0x04000BC0 RID: 3008
			private const string ToStringTemplate = "  {0,-15}  {1,11}  {2,12}  {3,11}";

			// Token: 0x04000BC1 RID: 3009
			private int successCount;
		}
	}
}
