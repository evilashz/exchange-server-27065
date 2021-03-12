using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000130 RID: 304
	public class ADUtils
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x000343B4 File Offset: 0x000325B4
		internal static string[] GetSameRoleServersInSameSite(ServerRole serverRole)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 36, "GetSameRoleServersInSameSite", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\ADUtils.cs");
			if (topologyConfigurationSession != null)
			{
				ADSite localSite = topologyConfigurationSession.GetLocalSite();
				if (localSite != null)
				{
					IEnumerable<string> source = from server in topologyConfigurationSession.FindPaged<Server>(null, QueryScope.SubTree, QueryFilter.AndTogether(new QueryFilter[]
					{
						new BitMaskAndFilter(ServerSchema.CurrentServerRole, (ulong)serverRole),
						new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, localSite.Id)
					}), null, ADGenericPagedReader<Server>.DefaultPageSize)
					where server.IsE15OrLater
					select server into x
					select x.Fqdn;
					return source.ToArray<string>();
				}
			}
			throw new ApplicationException("Couldn't create ADSession or couldn't get the local ADSite.");
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00034485 File Offset: 0x00032685
		internal static string[] GetCentralAdminSvrsInSameSite()
		{
			return ADUtils.GetSameRoleServersInSameSite(ServerRole.CentralAdmin);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00034491 File Offset: 0x00032691
		internal static string[] GetDomainControllersInSameSite()
		{
			return ADUtils.GetSameRoleServersInSameSite(ServerRole.DomainController);
		}
	}
}
