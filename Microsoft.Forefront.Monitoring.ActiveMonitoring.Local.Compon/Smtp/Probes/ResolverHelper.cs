using System;
using System.Net;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000240 RID: 576
	public class ResolverHelper
	{
		// Token: 0x06001360 RID: 4960 RVA: 0x000394E4 File Offset: 0x000376E4
		public static IPAddress ResolveEndPoint(string domain, DnsUtils.DnsRecordType queryType, DelTraceDebug traceDebug, bool simpleResolve)
		{
			if (queryType == DnsUtils.DnsRecordType.DnsTypeA)
			{
				return ResolverHelper.ResolveARecord(domain, traceDebug);
			}
			if (queryType == DnsUtils.DnsRecordType.DnsTypeMX)
			{
				return ResolverHelper.ResolveMxRecord(domain, traceDebug);
			}
			if (queryType != DnsUtils.DnsRecordType.DnsTypeAAAA)
			{
				throw new ArgumentException(string.Format("Unhandled RecordResolveType {0}", queryType));
			}
			return ResolverHelper.ResolveAAAARecord(domain, traceDebug, simpleResolve);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00039534 File Offset: 0x00037734
		private static IPAddress ResolveMxRecord(string domain, DelTraceDebug traceDebug)
		{
			int num;
			string[] mxrecords = DnsUtils.GetMXRecords(domain, out num);
			if (mxrecords == null || mxrecords.Length == 0)
			{
				throw new ResolverHelper.UnableToResolveException(domain, DnsUtils.DnsRecordType.DnsTypeMX, (DnsUtils.DnsResponseCode)num);
			}
			traceDebug("Resolved MxRecord for {0} to {1}", new object[]
			{
				domain,
				mxrecords[0]
			});
			string text = mxrecords[0];
			DnsUtils.DnsResponse arecord = DnsUtils.GetARecord(text);
			if (arecord.IPAddress == IPAddress.None)
			{
				throw new ResolverHelper.UnableToResolveException(text, DnsUtils.DnsRecordType.DnsTypeA, arecord.ReturnCode);
			}
			traceDebug("Resolved A for {0} to {1}", new object[]
			{
				text,
				arecord.IPAddress
			});
			return arecord.IPAddress;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000395D0 File Offset: 0x000377D0
		private static IPAddress ResolveARecord(string domain, DelTraceDebug traceDebug)
		{
			DnsUtils.DnsResponse arecord = DnsUtils.GetARecord(domain);
			if (arecord.IPAddress == IPAddress.None)
			{
				throw new ResolverHelper.UnableToResolveException(domain, DnsUtils.DnsRecordType.DnsTypeA, arecord.ReturnCode);
			}
			traceDebug("Resolved A for {0} to {1}", new object[]
			{
				domain,
				arecord.IPAddress
			});
			return arecord.IPAddress;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00039628 File Offset: 0x00037828
		private static IPAddress ResolveAAAARecord(string domain, DelTraceDebug traceDebug, bool simpleResolve)
		{
			int num;
			string[] mxrecords = DnsUtils.GetMXRecords(domain, out num);
			string text = domain;
			if (!simpleResolve)
			{
				if (mxrecords == null || mxrecords.Length == 0)
				{
					throw new ResolverHelper.UnableToResolveException(domain, DnsUtils.DnsRecordType.DnsTypeMX, (DnsUtils.DnsResponseCode)num);
				}
				traceDebug("Resolved MxRecord for {0} to {1}", new object[]
				{
					domain,
					mxrecords[0]
				});
				text = mxrecords[0];
			}
			DnsUtils.DnsResponse aaaarecord = DnsUtils.GetAAAARecord(text);
			if (aaaarecord.IPAddress == IPAddress.None)
			{
				throw new ResolverHelper.UnableToResolveException(text, DnsUtils.DnsRecordType.DnsTypeAAAA, aaaarecord.ReturnCode);
			}
			traceDebug("Resolved AAAA for {0} to {1}", new object[]
			{
				text,
				aaaarecord.IPAddress
			});
			return aaaarecord.IPAddress;
		}

		// Token: 0x02000241 RID: 577
		public class UnableToResolveException : Exception
		{
			// Token: 0x06001365 RID: 4965 RVA: 0x000396D0 File Offset: 0x000378D0
			public UnableToResolveException()
			{
			}

			// Token: 0x06001366 RID: 4966 RVA: 0x000396D8 File Offset: 0x000378D8
			public UnableToResolveException(string domain, DnsUtils.DnsRecordType queryType, DnsUtils.DnsResponseCode responseCode) : base(string.Format("Unable to resolve {0} records for {1} (ReturnCode = {2}).", queryType.ToString(), domain, responseCode.ToString()))
			{
				this.Domain = domain;
				this.QueryType = queryType;
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001367 RID: 4967 RVA: 0x0003970F File Offset: 0x0003790F
			// (set) Token: 0x06001368 RID: 4968 RVA: 0x00039717 File Offset: 0x00037917
			public string Domain { get; private set; }

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001369 RID: 4969 RVA: 0x00039720 File Offset: 0x00037920
			// (set) Token: 0x0600136A RID: 4970 RVA: 0x00039728 File Offset: 0x00037928
			public DnsUtils.DnsRecordType QueryType { get; private set; }
		}
	}
}
