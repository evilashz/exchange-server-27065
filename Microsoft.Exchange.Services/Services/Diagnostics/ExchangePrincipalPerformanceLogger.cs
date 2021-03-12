using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ExchangePrincipalPerformanceLogger : IPerformanceDataLogger
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000A381 File Offset: 0x00008581
		internal ExchangePrincipalPerformanceLogger(RequestDetailsLogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this.logger = logger;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public void Log(string marker, string counter, TimeSpan dataPoint)
		{
			ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug<string, string, double>((long)this.GetHashCode(), "PerfLog: {0}.{1}={2}ms", marker, counter, dataPoint.TotalMilliseconds);
			if (dataPoint.TotalMilliseconds < 50.0)
			{
				return;
			}
			this.MapToMetadataAndLog(marker, counter, dataPoint.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A3FB File Offset: 0x000085FB
		public void Log(string marker, string counter, uint dataPoint)
		{
			ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug<string, string, uint>((long)this.GetHashCode(), "PerfLog: {0}.{1}={2}", marker, counter, dataPoint);
			if (dataPoint < 50U)
			{
				return;
			}
			this.MapToMetadataAndLog(marker, counter, dataPoint.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A430 File Offset: 0x00008630
		public void Log(string marker, string counter, string dataPoint)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A438 File Offset: 0x00008638
		private void MapToMetadataAndLog(string marker, string counter, string dataPoint)
		{
			string key;
			if (!ExchangePrincipalPerformanceLogger.MarkerAndCounterToMetadataMap.TryGetValue(Tuple.Create<string, string>(marker, counter), out key))
			{
				return;
			}
			if (this.logger != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendDetailedExchangePrincipalLatency(this.logger, key, dataPoint);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A470 File Offset: 0x00008670
		public void ReleaseReferenceToRequestDetailsLogger()
		{
			this.logger = null;
		}

		// Token: 0x04000226 RID: 550
		private const string ElapsedTime = "ElapsedTime";

		// Token: 0x04000227 RID: 551
		private const uint MinLatencyLogThresholdMilliseconds = 50U;

		// Token: 0x04000228 RID: 552
		private static readonly Dictionary<Tuple<string, string>, string> MarkerAndCounterToMetadataMap = new Dictionary<Tuple<string, string>, string>
		{
			{
				Tuple.Create<string, string>("UpdateDatabaseLocationInfo", "ElapsedTime"),
				"EPUpdateDbLocInfoLatency"
			},
			{
				Tuple.Create<string, string>("UpdateCrossPremiseStatus", "ElapsedTime"),
				"EPUpdateXPremiseStatusLatency"
			},
			{
				Tuple.Create<string, string>("UpdateCrossPremiseStatusFindByExchangeGuidIncludingAlternate", "ElapsedTime"),
				"EPUpdateXPremiseStatusFindByExchangeGuidIncludingAlternateLatency"
			},
			{
				Tuple.Create<string, string>("UpdateCrossPremiseStatusFindByLegacyExchangeDN", "ElapsedTime"),
				"EPUpdateXPremiseStatusFindByLegacyExchangeDNLatency"
			},
			{
				Tuple.Create<string, string>("UpdateCrossPremiseStatusRemoteMailbox", "ElapsedTime"),
				"EPUpdateXPremiseStatusRemoteMailboxLatency"
			},
			{
				Tuple.Create<string, string>("UpdateDelegationTokenRequest", "ElapsedTime"),
				"EPUpdateDelegationTokenRequestLatency"
			},
			{
				Tuple.Create<string, string>("GetUserSKUCapability", "ElapsedTime"),
				"EPGetUserSKUCapabilityLatency"
			},
			{
				Tuple.Create<string, string>("GetIsLicensingEnforcedInOrg", "ElapsedTime"),
				"EPGetIsLicensingEnforcedInOrgLatency"
			},
			{
				Tuple.Create<string, string>("GetServerForDatabaseGetServerInformationForDatabase", "ElapsedTime"),
				"EPUpdateDbLocInfoGetServerForDbGetServerInformationForDbLatency"
			},
			{
				Tuple.Create<string, string>("GetServerForDatabaseGetServerNameForDatabase", "ElapsedTime"),
				"EPUpdateDbLocInfoGetServerForDbGetServerNameForDbLatency"
			},
			{
				Tuple.Create<string, string>("GetServerInformationForDatabaseGetDatabaseByGuidEx", "ElapsedTime"),
				"EPUpdateDbLocInfoGetServerInformationForDbGetDbByGuidExLatency"
			},
			{
				Tuple.Create<string, string>("GetServerNameForDatabaseGetDatabaseByGuidEx", "ElapsedTime"),
				"EPUpdateDbLocInfoGetServerNameForDbGetDbByGuidExLatency"
			},
			{
				Tuple.Create<string, string>("GetServerNameForDatabaseLookupDatabaseAndPossiblyPopulateCache", "ElapsedTime"),
				"EPUpdateDbLocInfoGetServerNameForDbLookupDbAndPossiblyPopulateCacheLatency"
			},
			{
				Tuple.Create<string, string>("ADQuery", "ElapsedTime"),
				"EPADQueryLatency"
			}
		};

		// Token: 0x04000229 RID: 553
		private RequestDetailsLogger logger;
	}
}
