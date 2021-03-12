using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x02000437 RID: 1079
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SimpleMiniServerLookup : IFindMiniServer
	{
		// Token: 0x06003034 RID: 12340 RVA: 0x000C5CAD File Offset: 0x000C3EAD
		public SimpleMiniServerLookup(IADToplogyConfigurationSession adSession)
		{
			this.AdSession = adSession;
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x000C5CBC File Offset: 0x000C3EBC
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ActiveManagerClientTracer;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x000C5CC3 File Offset: 0x000C3EC3
		// (set) Token: 0x06003037 RID: 12343 RVA: 0x000C5CCB File Offset: 0x000C3ECB
		public IADToplogyConfigurationSession AdSession { get; set; }

		// Token: 0x06003038 RID: 12344 RVA: 0x000C5CD4 File Offset: 0x000C3ED4
		public void Clear()
		{
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000C5CD8 File Offset: 0x000C3ED8
		public IADServer FindMiniServerByFqdn(string serverFqdn)
		{
			string nodeNameFromFqdn = MachineName.GetNodeNameFromFqdn(serverFqdn);
			return this.FindMiniServerByShortName(nodeNameFromFqdn);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000C5CF4 File Offset: 0x000C3EF4
		public IADServer FindMiniServerByShortName(string shortName)
		{
			Exception ex;
			return this.FindMiniServerByShortNameEx(shortName, out ex);
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000C5D30 File Offset: 0x000C3F30
		public IADServer FindMiniServerByShortNameEx(string shortName, out Exception ex)
		{
			IADServer result = null;
			ex = ADUtils.RunADOperation(delegate()
			{
				result = this.AdSession.FindMiniServerByName(shortName);
			}, 2);
			if (ex != null)
			{
				SimpleMiniServerLookup.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "SimpleMiniServerLookup.FindMiniServerByFqdn got an exception: {0}", ex);
			}
			return result;
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000C5DB4 File Offset: 0x000C3FB4
		public IADServer ReadMiniServerByObjectId(ADObjectId serverId)
		{
			IADServer result = null;
			Exception ex = ADUtils.RunADOperation(delegate()
			{
				result = this.AdSession.ReadMiniServer(serverId);
			}, 2);
			if (ex != null)
			{
				SimpleMiniServerLookup.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "SimpleMiniServerLookup.ReadMiniServerByObjectId got an exception: {0}", ex);
			}
			return result;
		}
	}
}
