﻿using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000006 RID: 6
	internal static class ServerLookup
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002600 File Offset: 0x00000800
		public static int? LookupVersion(string server)
		{
			int versionNumber;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.ServersCache.Enabled)
			{
				try
				{
					bool flag;
					MiniServer serverByFQDN = ServersCache.GetServerByFQDN(server, out flag);
					versionNumber = serverByFQDN.VersionNumber;
					goto IL_90;
				}
				catch (LocalServerNotFoundException)
				{
					return null;
				}
			}
			ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\Common\\Misc\\ServerLookup.cs", "LookupVersion", 45);
			if (!currentServiceTopology.TryGetServerVersion(server, out versionNumber, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\Common\\Misc\\ServerLookup.cs", "LookupVersion", 47))
			{
				return null;
			}
			IL_90:
			return new int?(versionNumber);
		}
	}
}
