﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000035 RID: 53
	internal static class StoreCommon
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0000D07C File Offset: 0x0000B27C
		internal static List<Database> PopulateDatabasesFromServer(ActiveManager activeManager, Server server, bool includePassive)
		{
			if (activeManager == null)
			{
				throw new ArgumentNullException("activeManager");
			}
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			List<Database> list = new List<Database>();
			IEnumerable<Database> databases = server.GetDatabases();
			if (databases != null)
			{
				foreach (Database database in databases)
				{
					if (includePassive)
					{
						list.Add(database);
					}
					else
					{
						DatabaseLocationInfo serverForDatabase = activeManager.GetServerForDatabase(database.Guid);
						if ((serverForDatabase != null && serverForDatabase.ServerGuid == server.Guid) || (serverForDatabase == null && database.Server.ObjectGuid == server.Guid))
						{
							list.Add(database);
						}
					}
				}
			}
			return list;
		}
	}
}
