using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x02000432 RID: 1074
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SimpleMiniClientAccessServerOrArrayLookup : IFindMiniClientAccessServerOrArray
	{
		// Token: 0x06003006 RID: 12294 RVA: 0x000C5115 File Offset: 0x000C3315
		public SimpleMiniClientAccessServerOrArrayLookup(ITopologyConfigurationSession adSession)
		{
			this.AdSession = adSession;
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x000C5124 File Offset: 0x000C3324
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ActiveManagerClientTracer;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000C512B File Offset: 0x000C332B
		// (set) Token: 0x06003009 RID: 12297 RVA: 0x000C5133 File Offset: 0x000C3333
		private ITopologyConfigurationSession AdSession
		{
			get
			{
				return this.adSession;
			}
			set
			{
				this.adSession = value;
				this.cdsAdSession = ADSessionFactory.CreateWrapper(this.adSession);
			}
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000C5150 File Offset: 0x000C3350
		public static IADMiniClientAccessServerOrArray FindMiniCasOrArrayWithClientAccess(IFindMiniClientAccessServerOrArray findMiniCasArray, ITopologyConfigurationSession adSession, ADObjectId siteId, ADObjectId preferredServerId)
		{
			ADObjectId adobjectId = SimpleMiniClientAccessServerOrArrayLookup.FindServerIdWithClientAccess(adSession, siteId, preferredServerId);
			if (adobjectId != null)
			{
				return findMiniCasArray.ReadMiniClientAccessServerOrArrayByObjectId(adobjectId);
			}
			return null;
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000C5172 File Offset: 0x000C3372
		public void Clear()
		{
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000C5174 File Offset: 0x000C3374
		public IADMiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayByFqdn(string serverFqdn)
		{
			return SimpleMiniClientAccessServerOrArrayLookup.FindMiniCasOrArrayByFqdn(this.cdsAdSession, serverFqdn);
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000C51A4 File Offset: 0x000C33A4
		internal static IADMiniClientAccessServerOrArray FindMiniCasOrArrayByFqdn(IADToplogyConfigurationSession cdsAdSession, string serverFqdn)
		{
			IADMiniClientAccessServerOrArray returnObj = null;
			Exception ex = ADUtils.RunADOperation(delegate()
			{
				returnObj = cdsAdSession.FindMiniClientAccessServerOrArrayByFqdn(serverFqdn);
			}, 2);
			if (ex != null)
			{
				SimpleMiniClientAccessServerOrArrayLookup.Tracer.TraceDebug<Exception>(0L, "SimpleMiniClientAccessServerOrArrayLookup.FindMiniCasOrArrayByFqdn got an exception: {0}", ex);
			}
			return returnObj;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000C51FA File Offset: 0x000C33FA
		public IADMiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayByLegdn(string serverLegdn)
		{
			return SimpleMiniClientAccessServerOrArrayLookup.FindMiniCasOrArrayByLegdn(this.cdsAdSession, serverLegdn);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000C5268 File Offset: 0x000C3468
		internal static IADMiniClientAccessServerOrArray FindMiniCasOrArrayByLegdn(IADToplogyConfigurationSession cdsAdSession, string serverLegdn)
		{
			IADMiniClientAccessServerOrArray result = null;
			Exception ex = ADUtils.RunADOperation(delegate()
			{
				bool flag = cdsAdSession.TryFindByExchangeLegacyDN(serverLegdn, out result);
				SimpleMiniClientAccessServerOrArrayLookup.Tracer.TraceDebug<string, bool>(0L, "TryFindByExchangeLegacyDN({0}) returned {1}.", serverLegdn, flag);
				if (!flag)
				{
					SimpleMiniClientAccessServerOrArrayLookup.Tracer.TraceDebug<string>(0L, "FindMiniCasOrArrayByLegdn: Could not find a MiniServer for the legdn extracted server '{0}'.", serverLegdn);
				}
			}, 2);
			if (ex != null)
			{
				SimpleMiniClientAccessServerOrArrayLookup.Tracer.TraceDebug<Exception>(0L, "SimpleMiniClientAccessServerOrArrayLookup.FindMiniCasOrArrayByLegdn got an exception: {0}", ex);
			}
			return result;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000C52E4 File Offset: 0x000C34E4
		public IADMiniClientAccessServerOrArray ReadMiniClientAccessServerOrArrayByObjectId(ADObjectId serverId)
		{
			IADMiniClientAccessServerOrArray result = null;
			Exception ex = ADUtils.RunADOperation(delegate()
			{
				result = this.cdsAdSession.ReadMiniClientAccessServerOrArray(serverId);
			}, 2);
			if (ex != null)
			{
				SimpleMiniClientAccessServerOrArrayLookup.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "SimpleMiniClientAccessServerOrArrayLookup.ReadMiniClientAccessServerOrArrayByObjectId got an exception: {0}", ex);
			}
			return result;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000C533F File Offset: 0x000C353F
		public IADMiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayWithClientAccess(ADObjectId siteId, ADObjectId preferredServerId)
		{
			return SimpleMiniClientAccessServerOrArrayLookup.FindMiniCasOrArrayWithClientAccess(this, this.AdSession, siteId, preferredServerId);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000C5350 File Offset: 0x000C3550
		private static ADObjectId FindServerIdWithClientAccess(ITopologyConfigurationSession adSession, ADObjectId mountedSiteId, ADObjectId preferredServerId)
		{
			ADObjectId adobjectId = null;
			List<ADObjectId> list = new List<ADObjectId>(8);
			foreach (KeyValuePair<Server, ExchangeRpcClientAccess> keyValuePair in ExchangeRpcClientAccess.GetServersWithRpcClientAccessEnabled(ExchangeRpcClientAccess.GetAllPossibleServers(adSession, mountedSiteId), ExchangeRpcClientAccess.GetAll(adSession)))
			{
				if ((keyValuePair.Value.Responsibility & RpcClientAccessResponsibility.Mailboxes) != RpcClientAccessResponsibility.None)
				{
					if (preferredServerId != null && preferredServerId.Equals(keyValuePair.Key.Id))
					{
						adobjectId = keyValuePair.Key.Id;
						break;
					}
					list.Add(keyValuePair.Key.Id);
				}
			}
			if (adobjectId == null && list.Count > 0)
			{
				adobjectId = list[0];
				if (list.Count > 1)
				{
					adobjectId = list[(Environment.TickCount & int.MaxValue) % list.Count];
				}
			}
			return adobjectId;
		}

		// Token: 0x04001A28 RID: 6696
		private ITopologyConfigurationSession adSession;

		// Token: 0x04001A29 RID: 6697
		private IADToplogyConfigurationSession cdsAdSession;
	}
}
