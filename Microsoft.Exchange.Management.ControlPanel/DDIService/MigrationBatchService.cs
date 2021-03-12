using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000227 RID: 551
	public class MigrationBatchService
	{
		// Token: 0x0600277D RID: 10109 RVA: 0x0007C418 File Offset: 0x0007A618
		public static void FetchMigrationEndpoints(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			PowerShellResults<PSObject> powerShellResults = (PowerShellResults<PSObject>)results[0];
			List<MigrationEndpointObject> list = new List<MigrationEndpointObject>(powerShellResults.HasValue ? powerShellResults.Output.Length : 0);
			if (powerShellResults.Succeeded)
			{
				foreach (PSObject psobject in powerShellResults.Output)
				{
					list.Add(new MigrationEndpointObject((MigrationEndpoint)psobject.BaseObject));
				}
			}
			dataTable.Rows[0]["_AllMigrationEndpoints"] = list;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0007C4B0 File Offset: 0x0007A6B0
		public static void FetchAcceptedDomains(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			PowerShellResults<PSObject> powerShellResults = (PowerShellResults<PSObject>)results[0];
			if (!powerShellResults.Succeeded)
			{
				return;
			}
			bool flag = false;
			if (!DBNull.Value.Equals(dataTable.Rows[0]["IsDedicated"]))
			{
				flag = (bool)dataTable.Rows[0]["IsDedicated"];
			}
			if (flag)
			{
				dataTable.Rows[0]["_AcceptedDomains"] = MigrationBatchService.FilterAcceptedDomains(powerShellResults.Output, (Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain domain) => domain.DomainType == AcceptedDomainType.Authoritative);
				return;
			}
			bool flag2 = false;
			if (!DBNull.Value.Equals(dataTable.Rows[0]["IsMultiTenant"]))
			{
				flag2 = (bool)dataTable.Rows[0]["IsMultiTenant"];
			}
			if (flag2)
			{
				string[] array = MigrationBatchService.FilterAcceptedDomains(powerShellResults.Output, (Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain domain) => domain.IsCoexistenceDomain);
				if (array.Length > 0)
				{
					dataTable.Rows[0]["TargetDeliveryDomain"] = array[0];
					dataTable.Rows[0]["_AcceptedDomains"] = array;
				}
				return;
			}
			dataTable.Rows[0]["_AcceptedDomains"] = MigrationBatchService.FilterAcceptedDomains(powerShellResults.Output, (Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain domain) => true);
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x0007C640 File Offset: 0x0007A840
		public static void FetchRelayDomains(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			PowerShellResults<PSObject> powerShellResults = (PowerShellResults<PSObject>)results[0];
			if (powerShellResults.Succeeded)
			{
				dataTable.Rows[0]["_AcceptedDomains"] = MigrationBatchService.FilterAcceptedDomains(powerShellResults.Output, (Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain domain) => domain.DomainType != AcceptedDomainType.Authoritative);
			}
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0007C69C File Offset: 0x0007A89C
		private static string[] FilterAcceptedDomains(PSObject[] acceptedDomainObjects, Func<Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain, bool> domainFilter)
		{
			List<string> list = new List<string>();
			foreach (PSObject psobject in acceptedDomainObjects)
			{
				Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain acceptedDomain = (Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain)psobject.BaseObject;
				if (!acceptedDomain.DomainName.IsStar)
				{
					string domain = acceptedDomain.DomainName.Domain;
					if (!list.Contains(domain, StringComparer.OrdinalIgnoreCase) && domainFilter(acceptedDomain))
					{
						list.Add(domain);
					}
				}
			}
			return list.ToArray();
		}
	}
}
