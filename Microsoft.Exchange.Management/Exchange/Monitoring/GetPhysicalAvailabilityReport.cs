using System;
using System.Data;
using System.Data.SqlClient;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000594 RID: 1428
	[Cmdlet("Get", "PhysicalAvailabilityReport", DefaultParameterSetName = "StartEndDateSet")]
	public sealed class GetPhysicalAvailabilityReport : GetAvailabilityReport<OrganizationIdParameter>
	{
		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000CC9B4 File Offset: 0x000CABB4
		// (set) Token: 0x06003248 RID: 12872 RVA: 0x000CC9CB File Offset: 0x000CABCB
		[Parameter(ParameterSetName = "ReportingPeriodSet", Mandatory = false, Position = 0, ValueFromPipeline = true)]
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "StartEndDateSet", Mandatory = false, Position = 0, ValueFromPipeline = true)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000CC9DE File Offset: 0x000CABDE
		// (set) Token: 0x0600324A RID: 12874 RVA: 0x000CC9F5 File Offset: 0x000CABF5
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "StartEndDateSet", Mandatory = false, Position = 0, ValueFromPipeline = true)]
		[Parameter(ParameterSetName = "ReportingPeriodSet", Mandatory = false, Position = 0, ValueFromPipeline = true)]
		public ServerIdParameter ExchangeServer
		{
			get
			{
				return (ServerIdParameter)base.Fields["ExchangeServer"];
			}
			set
			{
				base.Fields["ExchangeServer"] = value;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000CCA08 File Offset: 0x000CAC08
		// (set) Token: 0x0600324C RID: 12876 RVA: 0x000CCA10 File Offset: 0x000CAC10
		private new OrganizationIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000CCA1C File Offset: 0x000CAC1C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			base.ValidateSProcExists("[Exchange2010].[SLAReportDataGetByDatabaseOrExchangeServer]");
			if (this.Database != null && this.ExchangeServer == null)
			{
				this.database = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.Database, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.Database.ToString())));
			}
			else if (this.ExchangeServer != null && this.Database == null)
			{
				this.exchangeServer = (Server)base.GetDataObject<Server>(this.ExchangeServer, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.ExchangeServer.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.ExchangeServer.ToString())));
			}
			else if (this.ExchangeServer == null && this.Database == null)
			{
				this.database = null;
				this.exchangeServer = null;
			}
			else
			{
				base.WriteError(new ArgumentException(Strings.AmbiguousDatabaseAndExchangeServerParameters), (ErrorCategory)1000, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000CCB36 File Offset: 0x000CAD36
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.database = null;
			this.exchangeServer = null;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000CCB4C File Offset: 0x000CAD4C
		protected override void GetTenantAvailabilityReport(ADObjectId tenantId)
		{
			if (!base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				base.WriteError(new NotSupportedException(Strings.TenantExecutionNotSupported), (ErrorCategory)1000, null);
			}
			SqlDataReader sqlDataReader = null;
			string text = (tenantId != null) ? tenantId.Name : "<all>";
			base.TraceInfo("Getting physical availability report for tenant: {0}", new object[]
			{
				text
			});
			try
			{
				string sqlConnectionString = base.GetSqlConnectionString();
				using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
				{
					base.TraceInfo("Opening SQL connecttion: {0}", new object[]
					{
						sqlConnectionString
					});
					sqlConnection.Open();
					SqlCommand sqlCommand = new SqlCommand("[Exchange2010].[SLAReportDataGetByDatabaseOrExchangeServer]", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = (DateTime)this.utcStartDateTime;
					sqlCommand.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = (DateTime)this.utcEndDateTime;
					if (this.database != null && this.exchangeServer == null)
					{
						sqlCommand.Parameters.Add("@MdbName", SqlDbType.NVarChar).Value = this.database.Name;
					}
					else if (this.exchangeServer != null && this.database == null)
					{
						sqlCommand.Parameters.Add("@ExchangeServerFqdn", SqlDbType.NVarChar).Value = this.exchangeServer.Fqdn;
						sqlCommand.Parameters.Add("@ADSite", SqlDbType.NVarChar).Value = this.exchangeServer.ServerSite.Name;
					}
					base.TraceInfo("Executing stored procedure: {0}", new object[]
					{
						"[Exchange2010].[SLAReportDataGetByDatabaseOrExchangeServer]"
					});
					sqlDataReader = sqlCommand.ExecuteReader();
					base.TraceInfo("Processing physical availability data for tenant: {0}", new object[]
					{
						text
					});
					this.ProcessPhysicalAvailabilityReportData(sqlDataReader);
					base.TraceInfo("Finished processing physical availability data for tenant: {0}", new object[]
					{
						text
					});
				}
			}
			catch (SqlException ex)
			{
				base.WriteError(new SqlReportingConnectionException(ex.Message), (ErrorCategory)1000, null);
			}
			finally
			{
				if (sqlDataReader != null)
				{
					sqlDataReader.Close();
				}
			}
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000CCDB8 File Offset: 0x000CAFB8
		private void ProcessPhysicalAvailabilityReportData(SqlDataReader reader)
		{
			PhysicalAvailabilityReport physicalAvailabilityReport = new PhysicalAvailabilityReport();
			while (reader.Read())
			{
				string siteName = (string)reader["SiteName"];
				string text = (string)reader["Name"];
				ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, ((DateTime)reader["UtcDate"]).Date);
				double availabilityPercentage = Convert.ToDouble(reader["UpTime"]);
				double rawAvailabilityPercentage = Convert.ToDouble(reader["RawUpTime"]);
				physicalAvailabilityReport.SiteName = siteName;
				DailyPhysicalAvailability dailyPhysicalAvailability = new DailyPhysicalAvailability((DateTime)exDateTime);
				dailyPhysicalAvailability.AvailabilityPercentage = availabilityPercentage;
				dailyPhysicalAvailability.RawAvailabilityPercentage = rawAvailabilityPercentage;
				if (!physicalAvailabilityReport.DailyStatistics.Contains(dailyPhysicalAvailability))
				{
					physicalAvailabilityReport.DailyStatistics.Add(dailyPhysicalAvailability);
				}
			}
			if (physicalAvailabilityReport.DailyStatistics.Count > 0)
			{
				physicalAvailabilityReport.StartDate = (DateTime)this.utcStartDateTime;
				physicalAvailabilityReport.EndDate = (DateTime)this.utcEndDateTime;
				physicalAvailabilityReport.Database = ((this.database != null) ? this.database.Id : null);
				physicalAvailabilityReport.ExchangeServer = ((this.exchangeServer != null) ? this.exchangeServer.Id : null);
				physicalAvailabilityReport.DailyStatistics.Sort();
				double num = 0.0;
				double num2 = 0.0;
				int count = physicalAvailabilityReport.DailyStatistics.Count;
				foreach (DailyAvailability dailyAvailability in physicalAvailabilityReport.DailyStatistics)
				{
					DailyPhysicalAvailability dailyPhysicalAvailability2 = (DailyPhysicalAvailability)dailyAvailability;
					num += dailyPhysicalAvailability2.AvailabilityPercentage;
					num2 += dailyPhysicalAvailability2.RawAvailabilityPercentage;
				}
				num /= (double)count;
				num2 /= (double)count;
				physicalAvailabilityReport.AvailabilityPercentage = num;
				physicalAvailabilityReport.RawAvailabilityPercentage = num2;
				if (!base.DailyStatistics.IsPresent)
				{
					physicalAvailabilityReport.DailyStatistics = null;
				}
				this.WriteReport(physicalAvailabilityReport);
			}
		}

		// Token: 0x04002353 RID: 9043
		private const string CmdletNoun = "PhysicalAvailabilityReport";

		// Token: 0x04002354 RID: 9044
		private const string SPName = "[Exchange2010].[SLAReportDataGetByDatabaseOrExchangeServer]";

		// Token: 0x04002355 RID: 9045
		private MailboxDatabase database;

		// Token: 0x04002356 RID: 9046
		private Server exchangeServer;
	}
}
