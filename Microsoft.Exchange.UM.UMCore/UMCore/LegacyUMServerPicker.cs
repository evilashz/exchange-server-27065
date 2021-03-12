using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000167 RID: 359
	internal class LegacyUMServerPicker : ServerPickerBase<InternalExchangeServer, ADObjectId>
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
		public LegacyUMServerPicker()
		{
			this.version = VersionEnum.E12Legacy;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002CC04 File Offset: 0x0002AE04
		private LegacyUMServerPicker(VersionEnum version) : base(LegacyUMServerPicker.UMDefaultRetryInterval, LegacyUMServerPicker.UMDefaultRefreshInterval, LegacyUMServerPicker.UMDefaultRefreshIntervalOnFailure, ExTraceGlobals.UtilTracer)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this.GetHashCode(), "LegacyUMServerPicker for version : {0}", new object[]
			{
				version
			});
			this.version = version;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002CC60 File Offset: 0x0002AE60
		public static LegacyUMServerPicker GetInstance(VersionEnum version)
		{
			switch (version)
			{
			case VersionEnum.E12Legacy:
				if (LegacyUMServerPicker.e12staticInstance == null)
				{
					lock (LegacyUMServerPicker.lockObj)
					{
						if (LegacyUMServerPicker.e12staticInstance == null)
						{
							LegacyUMServerPicker.e12staticInstance = new LegacyUMServerPicker(VersionEnum.E12Legacy);
						}
					}
				}
				return LegacyUMServerPicker.e12staticInstance;
			case VersionEnum.E14Legacy:
				if (LegacyUMServerPicker.e14staticInstance == null)
				{
					lock (LegacyUMServerPicker.lockObj)
					{
						if (LegacyUMServerPicker.e14staticInstance == null)
						{
							LegacyUMServerPicker.e14staticInstance = new LegacyUMServerPicker(VersionEnum.E14Legacy);
						}
					}
				}
				return LegacyUMServerPicker.e14staticInstance;
			default:
				throw new ArgumentException(version.ToString());
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002CD28 File Offset: 0x0002AF28
		protected override List<InternalExchangeServer> LoadConfiguration()
		{
			List<InternalExchangeServer> list = new List<InternalExchangeServer>(10);
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
			IEnumerable<Server> enabledExchangeServers = adtopologyLookup.GetEnabledExchangeServers(this.version, ServerRole.UnifiedMessaging);
			foreach (Server server in enabledExchangeServers)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this.GetHashCode(), "LegacyUMServerPicker::LoadConfiguration() Adding legacy Server={0} Serial={1} UMRole={2} Status={3}", new object[]
				{
					server.Fqdn,
					server.SerialNumber,
					server.IsUnifiedMessagingServer,
					server.Status
				});
				ExAssert.RetailAssert(server.Status == ServerStatus.Enabled, "Server {0} is enabled", new object[]
				{
					server.Fqdn
				});
				list.Add(new InternalExchangeServer(server));
			}
			return list;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002CE1C File Offset: 0x0002B01C
		protected override bool IsHealthy(ADObjectId dialPlanId, InternalExchangeServer server)
		{
			return Util.PingServerWithRpc(server.Server, dialPlanId.ObjectGuid, (this.version == VersionEnum.E12Legacy) ? LegacyUMServerPicker.FakeE12Server : LegacyUMServerPicker.FakeE14Server);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002CE44 File Offset: 0x0002B044
		protected override bool IsValid(ADObjectId dialPlanId, InternalExchangeServer candidate)
		{
			foreach (ADObjectId adobjectId in candidate.Server.DialPlans)
			{
				if (adobjectId.ObjectGuid.Equals(dialPlanId.ObjectGuid))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000965 RID: 2405
		internal static readonly TimeSpan UMDefaultRetryInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000966 RID: 2406
		internal static readonly TimeSpan UMDefaultRefreshInterval = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000967 RID: 2407
		internal static readonly TimeSpan UMDefaultRefreshIntervalOnFailure = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000968 RID: 2408
		private static readonly string FakeE12Server = "FakeE12UmServer";

		// Token: 0x04000969 RID: 2409
		private static readonly string FakeE14Server = "FakeE14UmServer";

		// Token: 0x0400096A RID: 2410
		private static LegacyUMServerPicker e12staticInstance;

		// Token: 0x0400096B RID: 2411
		private static LegacyUMServerPicker e14staticInstance;

		// Token: 0x0400096C RID: 2412
		private static object lockObj = new object();

		// Token: 0x0400096D RID: 2413
		private VersionEnum version;
	}
}
