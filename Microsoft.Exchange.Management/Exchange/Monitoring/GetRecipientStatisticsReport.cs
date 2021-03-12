using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000595 RID: 1429
	[Cmdlet("Get", "RecipientStatisticsReport", DefaultParameterSetName = "Identity")]
	public sealed class GetRecipientStatisticsReport : ReportingTask<OrganizationIdParameter>
	{
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000CCFC4 File Offset: 0x000CB1C4
		// (set) Token: 0x06003253 RID: 12883 RVA: 0x000CCFDB File Offset: 0x000CB1DB
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override OrganizationIdParameter Identity
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000CCFEE File Offset: 0x000CB1EE
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			base.ValidateSProcExists("[Exchange2010].[RecipientStatisticsReport]");
			TaskLogger.LogExit();
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000CD00C File Offset: 0x000CB20C
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			if (dataObject != null)
			{
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)dataObject;
				if (adorganizationalUnit.OrganizationId.OrganizationalUnit != null && adorganizationalUnit.OrganizationId.ConfigurationUnit != null)
				{
					this.GetTenantRecipientStatistics(adorganizationalUnit.OrganizationId.OrganizationalUnit);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000CD058 File Offset: 0x000CB258
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			TaskLogger.LogEnter();
			if (dataObjects != null)
			{
				if (this.Identity != null)
				{
					base.WriteResult<T>(dataObjects);
				}
				else if (base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					this.GetTenantRecipientStatistics(base.RootOrgContainerId);
				}
				else if (base.CurrentOrganizationId.OrganizationalUnit != null && base.CurrentOrganizationId.ConfigurationUnit != null)
				{
					this.GetTenantRecipientStatistics(base.CurrentOrganizationId.OrganizationalUnit);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000CD0D0 File Offset: 0x000CB2D0
		private void GetTenantRecipientStatistics(ADObjectId organizationId)
		{
			SqlDataReader sqlDataReader = null;
			base.TraceInfo("Getting service status for tenant: {0}", new object[]
			{
				organizationId.Name
			});
			Guid guid = organizationId.Equals(base.RootOrgContainerId) ? Guid.Empty : organizationId.ObjectGuid;
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
					SqlCommand sqlCommand = new SqlCommand("[Exchange2010].[RecipientStatisticsReport]", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add("@TenantGuid", SqlDbType.UniqueIdentifier).Value = guid;
					base.TraceInfo("Executing stored procedure: {0}", new object[]
					{
						"[Exchange2010].[RecipientStatisticsReport]"
					});
					sqlDataReader = sqlCommand.ExecuteReader();
					base.TraceInfo("Processing Recipient Statistics data for tenant: {0}", new object[]
					{
						organizationId.Name
					});
					this.ProcessTenantRecipientStatistics(organizationId, sqlDataReader);
					base.TraceInfo("Finished Recipient Statistics data for tenant: {0}", new object[]
					{
						organizationId.Name
					});
				}
			}
			catch (SqlException ex)
			{
				base.WriteError(new SqlReportingConnectionException(ex.Message), (ErrorCategory)1000, null);
			}
			catch (InvalidOperationException ex2)
			{
				base.WriteError(new SqlReportingConnectionException(ex2.Message), (ErrorCategory)1001, null);
			}
			finally
			{
				if (sqlDataReader != null)
				{
					sqlDataReader.Close();
				}
			}
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000CD2A0 File Offset: 0x000CB4A0
		private void ProcessTenantRecipientStatistics(ADObjectId tenantId, SqlDataReader reader)
		{
			if (reader.Read())
			{
				base.WriteResult(new RecipientStatisticsReport
				{
					Identity = tenantId,
					TotalNumberOfMailboxes = uint.Parse(reader["AllMailboxCount"].ToString()),
					TotalNumberOfActiveMailboxes = uint.Parse(reader["ActiveMailboxCount"].ToString()),
					NumberOfContacts = uint.Parse(reader["TenantContactCount"].ToString()),
					NumberOfDistributionLists = uint.Parse(reader["TenantDLCount"].ToString()),
					LastUpdated = (DateTime)new ExDateTime(ExTimeZone.UtcTimeZone, (DateTime)reader["DateTime"])
				});
			}
		}

		// Token: 0x04002357 RID: 9047
		private const string CmdletNoun = "RecipientStatisticsReport";

		// Token: 0x04002358 RID: 9048
		private const string RecipientStatisticsSPName = "[Exchange2010].[RecipientStatisticsReport]";
	}
}
