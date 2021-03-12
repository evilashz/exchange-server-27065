using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes
{
	// Token: 0x02000452 RID: 1106
	public class RwsSyntheticTenantMailboxUsageProbe : ProbeWorkItem
	{
		// Token: 0x06001C13 RID: 7187 RVA: 0x000A2850 File Offset: 0x000A0A50
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (DateTime.UtcNow < new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 23, 30, 0))
			{
				base.Result.StateAttribute11 = string.Format("Probe Start from Utc {0} (< 23:30:00 of that day), bypass data check", DateTime.UtcNow);
				return;
			}
			string account = base.Definition.Account;
			string arg = RwsCryptographyHelper.Decrypt(base.Definition.AccountPassword);
			string azureConnString = string.Format(base.Definition.Endpoint, account, arg);
			List<RwsSyntheticTenantMailboxUsageProbe.SyntheticTenant> list = null;
			try
			{
				list = this.GetSyntheticTenantFromAzure(azureConnString);
			}
			catch (Exception ex)
			{
				base.Result.StateAttribute13 = ex.ToString();
			}
			if (list == null || list.Count <= 0)
			{
				base.Result.StateAttribute12 = "No available synthetic tenant to verify, bypass the check ...";
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			List<RwsSyntheticTenantMailboxUsageProbe.SyntheticTenant> list2 = new List<RwsSyntheticTenantMailboxUsageProbe.SyntheticTenant>();
			foreach (RwsSyntheticTenantMailboxUsageProbe.SyntheticTenant syntheticTenant in list)
			{
				string empty = string.Empty;
				if (!this.VerifySyntheticTenantMailboxUsage(syntheticTenant, out empty))
				{
					stringBuilder.Append(empty);
					stringBuilder.Append("\r\n");
					list2.Add(syntheticTenant);
				}
			}
			if (list2.Count <= 0)
			{
				base.Result.StateAttribute11 = "Synthetic Tenant data check complete on MailboxUsage table. All data is good.";
				return;
			}
			base.Result.StateAttribute11 = "Synthetic Tenant data check complete on MailboxUsage table. Some data is wrong here";
			throw new Exception(stringBuilder.ToString());
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000A29EC File Offset: 0x000A0BEC
		private bool VerifySyntheticTenantMailboxUsage(RwsSyntheticTenantMailboxUsageProbe.SyntheticTenant tenant, out string errMsg)
		{
			errMsg = string.Empty;
			string connectionString = "Data Source=cdm-tenantds.exmgmt.local;Initial Catalog=CDM-TENANTDS;Integrated Security=SSPI;";
			string commandText = "select TenantGuid from [MailboxUsage] where [TenantGuid] = @TenantGuid AND [DateTime] = @DateTime";
			List<SqlParameter> list = new List<SqlParameter>();
			SqlParameter item = new SqlParameter("@TenantGuid", tenant.TenantGuid);
			SqlParameter item2 = new SqlParameter("@DateTime", DateTime.UtcNow.AddDays(-1.0).ToShortDateString());
			list.Add(item);
			list.Add(item2);
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = commandText;
					sqlCommand.CommandType = CommandType.Text;
					if (list != null)
					{
						foreach (SqlParameter value in list)
						{
							sqlCommand.Parameters.Add(value);
						}
					}
					sqlConnection.Open();
					using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (sqlDataReader.HasRows)
						{
							return true;
						}
					}
				}
			}
			errMsg = string.Format("Synthetic tenant [Guid = {0}] have no data available in MailboxUsage table", tenant.TenantGuid);
			return false;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000A2B54 File Offset: 0x000A0D54
		private List<RwsSyntheticTenantMailboxUsageProbe.SyntheticTenant> GetSyntheticTenantFromAzure(string azureConnString)
		{
			List<RwsSyntheticTenantMailboxUsageProbe.SyntheticTenant> list = new List<RwsSyntheticTenantMailboxUsageProbe.SyntheticTenant>();
			string commandText = "\r\n                            SELECT [TenantGuid]\r\n                                  ,[TenantName]\r\n                                  ,[Category]\r\n                                  ,[TimeRangeInDay]\r\n                                  ,[MeasurementPropertyBag]\r\n                              FROM [Database_CFRSyntheticTenantInfo]\r\n                              where Category = 'MailboxUsage';\r\n                            ";
			using (SqlConnection sqlConnection = new SqlConnection(azureConnString))
			{
				using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
				{
					sqlCommand.CommandText = commandText;
					sqlCommand.CommandType = CommandType.Text;
					sqlConnection.Open();
					using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (sqlDataReader.Read())
						{
							list.Add(new RwsSyntheticTenantMailboxUsageProbe.SyntheticTenant
							{
								TenantGuid = (sqlDataReader.IsDBNull(0) ? string.Empty : sqlDataReader.GetString(0)),
								TenantName = (sqlDataReader.IsDBNull(1) ? string.Empty : sqlDataReader.GetString(1)),
								Category = (sqlDataReader.IsDBNull(2) ? string.Empty : sqlDataReader.GetString(2)),
								TimeRangeInDay = (sqlDataReader.IsDBNull(3) ? 0 : sqlDataReader.GetInt32(3)),
								MeasurementPropertyBag = (sqlDataReader.IsDBNull(4) ? string.Empty : sqlDataReader.GetString(4))
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x02000453 RID: 1107
		internal struct SyntheticTenant
		{
			// Token: 0x04001335 RID: 4917
			internal string TenantGuid;

			// Token: 0x04001336 RID: 4918
			internal string TenantName;

			// Token: 0x04001337 RID: 4919
			internal string Category;

			// Token: 0x04001338 RID: 4920
			internal int TimeRangeInDay;

			// Token: 0x04001339 RID: 4921
			internal string MeasurementPropertyBag;
		}
	}
}
