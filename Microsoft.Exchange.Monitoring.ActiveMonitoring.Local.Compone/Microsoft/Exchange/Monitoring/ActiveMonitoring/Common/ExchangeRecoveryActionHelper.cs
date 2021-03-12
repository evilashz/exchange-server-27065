using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000111 RID: 273
	internal class ExchangeRecoveryActionHelper
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x00031014 File Offset: 0x0002F214
		internal static string[] GetLocalDagMemberServers(out bool isLocalServerInDag, out Exception adException)
		{
			isLocalServerInDag = false;
			adException = null;
			List<string> list = new List<string>();
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 51, "GetLocalDagMemberServers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\RecoveryAction\\ExchangeRecoveryActionHelper.cs");
				if (topologyConfigurationSession != null)
				{
					Server server = topologyConfigurationSession.FindLocalServer();
					if (server != null)
					{
						ADObjectId databaseAvailabilityGroup = server.DatabaseAvailabilityGroup;
						if (databaseAvailabilityGroup != null)
						{
							isLocalServerInDag = true;
							DatabaseAvailabilityGroup databaseAvailabilityGroup2 = topologyConfigurationSession.Read<DatabaseAvailabilityGroup>(databaseAvailabilityGroup);
							if (databaseAvailabilityGroup2 != null)
							{
								foreach (ADObjectId entryId in databaseAvailabilityGroup2.Servers)
								{
									Server server2 = topologyConfigurationSession.Read<Server>(entryId);
									list.Add(server2.Fqdn);
								}
							}
						}
					}
				}
			}
			catch (ADTransientException ex)
			{
				adException = ex;
			}
			catch (ADExternalException ex2)
			{
				adException = ex2;
			}
			catch (ADOperationException ex3)
			{
				adException = ex3;
			}
			return list.ToArray();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00031110 File Offset: 0x0002F310
		internal static string[] GetLocalDagMemberServersWithValidation()
		{
			Exception ex = null;
			bool flag = false;
			string[] localDagMemberServers = ExchangeRecoveryActionHelper.GetLocalDagMemberServers(out flag, out ex);
			if (ex != null)
			{
				throw new InvalidOperationException("Attempt to get local dag servers failed due to AD failures", ex);
			}
			if (!flag)
			{
				throw new InvalidOperationException("Local server is not in a DAG");
			}
			if (localDagMemberServers == null || localDagMemberServers.Length == 0)
			{
				throw new InvalidOperationException("ServersInGroup is empty");
			}
			return localDagMemberServers;
		}
	}
}
