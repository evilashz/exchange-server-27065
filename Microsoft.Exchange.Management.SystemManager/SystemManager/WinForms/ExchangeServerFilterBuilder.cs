using System;
using System.Data;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200025E RID: 606
	internal class ExchangeServerFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A76 RID: 6774 RVA: 0x00074E24 File Offset: 0x00073024
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			long num = 8L;
			ServerRole role = ServerRole.Cafe | ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport | ServerRole.Edge | ServerRole.ProvisionedServer | ServerRole.FrontendTransport;
			bool flag = false;
			bool flag2 = false;
			ADObjectId[] array = null;
			ADObjectId adobjectId = null;
			string text = null;
			if (!DBNull.Value.Equals(row["MinVersion"]))
			{
				num = (long)row["MinVersion"];
			}
			if (row.Table.Columns.Contains("ExactVersion") && !DBNull.Value.Equals(row["ExactVersion"]))
			{
				text = (string)row["ExactVersion"];
			}
			if (!DBNull.Value.Equals(row["DesiredServerRoleBitMask"]))
			{
				role = (ServerRole)row["DesiredServerRoleBitMask"];
			}
			if (!DBNull.Value.Equals(row["IncludeLegacyServer"]))
			{
				flag = (bool)row["IncludeLegacyServer"];
			}
			if (row.Table.Columns.Contains("OnlyBackendServer") && !DBNull.Value.Equals(row["OnlyBackendServer"]))
			{
				flag2 = (bool)row["OnlyBackendServer"];
			}
			if (row.Table.Columns.Contains("ExcludeServers") && !DBNull.Value.Equals(row["ExcludeServers"]))
			{
				array = (ADObjectId[])row["ExcludeServers"];
			}
			if (row.Table.Columns.Contains("ServersDag") && !DBNull.Value.Equals(row["ServersDag"]))
			{
				adobjectId = (ADObjectId)row["ServersDag"];
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" | Filter-ExchangeServer -minVersion {0} ", num);
			string text2 = this.BuildServerRole(role);
			if (!string.IsNullOrEmpty(text2))
			{
				stringBuilder.AppendFormat(" -serverRoles {0} ", text2);
			}
			if (flag)
			{
				stringBuilder.Append(" -includeLegacyServer");
			}
			if (flag2)
			{
				stringBuilder.Append(" -backendServerOnly");
			}
			if (array != null && array.Length > 0)
			{
				stringBuilder.AppendFormat(" -excludedServers {0} ", this.BuildExcludeServersFilter(array));
			}
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat(" -exactVersion '{0}' ", text);
			}
			if (adobjectId != null)
			{
				stringBuilder.AppendFormat(" | Filter-PropertyEqualTo -Property 'DatabaseAvailabilityGroup' -Value '{0}'", adobjectId);
			}
			filter = stringBuilder.ToString();
			parameterList = null;
			preArgs = null;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00075088 File Offset: 0x00073288
		private string BuildExcludeServersFilter(ADObjectId[] servers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < servers.Length; i++)
			{
				stringBuilder.AppendFormat((i == 0) ? " '{0}'" : ",'{0}'", servers[i].ToQuotationEscapedString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000750D0 File Offset: 0x000732D0
		private string BuildServerRole(ServerRole role)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (role != (ServerRole.Cafe | ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport | ServerRole.Edge | ServerRole.ProvisionedServer | ServerRole.FrontendTransport))
			{
				int num = 0;
				foreach (ServerRole serverRole in ExchangeServerFilterBuilder.e12ServerRoles)
				{
					if (ExchangeServerFilterBuilder.IsBitOn(role, serverRole))
					{
						if (0 < num++)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.AppendFormat("'{0}'", serverRole.ToQuotationEscapedString());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00075144 File Offset: 0x00073344
		private static bool IsBitOn(ServerRole a, ServerRole b)
		{
			return (a & b) == b;
		}

		// Token: 0x040009D3 RID: 2515
		private const ServerRole allServerRoleBits = ServerRole.Cafe | ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport | ServerRole.Edge | ServerRole.ProvisionedServer | ServerRole.FrontendTransport;

		// Token: 0x040009D4 RID: 2516
		private static ServerRole[] e12ServerRoles = new ServerRole[]
		{
			ServerRole.HubTransport,
			ServerRole.ClientAccess,
			ServerRole.Edge,
			ServerRole.Mailbox,
			ServerRole.UnifiedMessaging,
			ServerRole.ProvisionedServer
		};
	}
}
