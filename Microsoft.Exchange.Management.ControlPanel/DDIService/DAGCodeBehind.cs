using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020000FA RID: 250
	public class DAGCodeBehind
	{
		// Token: 0x06001EDE RID: 7902 RVA: 0x0005C858 File Offset: 0x0005AA58
		public static void GetDAGPostAction(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			IEnumerable<ADObjectId> enumerable = (IEnumerable<ADObjectId>)store.GetValue("DatabaseAvailabilityGroup", "Servers");
			List<ServerResolverRow> list = (enumerable != null) ? ServerResolver.Instance.ResolveObjects(enumerable).ToList<ServerResolverRow>() : null;
			IEnumerable<ADObjectId> enumerable2 = (IEnumerable<ADObjectId>)store.GetValue("DatabaseAvailabilityGroup", "OperationalServers");
			if (list != null && enumerable2 != null)
			{
				foreach (ADObjectId identity in enumerable2)
				{
					Identity operationalServerIdentity = identity.ToIdentity();
					list.ForEach(delegate(ServerResolverRow resolvedServer)
					{
						if (resolvedServer.Identity == operationalServerIdentity)
						{
							resolvedServer.OperationalState = Strings.Yes;
						}
					});
				}
			}
			dataTable.Rows[0]["ServersWithOperationalState"] = list;
			dataTable.Rows[0]["WitnessServer"] = store.GetValue("DatabaseAvailabilityGroup", "WitnessServer");
			dataTable.Rows[0]["WitnessDirectory"] = store.GetValue("DatabaseAvailabilityGroup", "WitnessDirectory");
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0005C974 File Offset: 0x0005AB74
		public static void GetDAGListPostAction(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["WitnessServer"] = dataRow["WitnessServerValue"];
				dataRow["WitnessDirectory"] = dataRow["WitnessDirectoryValue"];
			}
		}
	}
}
