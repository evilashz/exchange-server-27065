using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000599 RID: 1433
	[Cmdlet("Get", "ServiceStatus", DefaultParameterSetName = "Identity")]
	public sealed class GetServiceStatus : ReportingTask<OrganizationIdParameter>
	{
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000CDBB4 File Offset: 0x000CBDB4
		// (set) Token: 0x06003265 RID: 12901 RVA: 0x000CDBD6 File Offset: 0x000CBDD6
		[Parameter(Mandatory = false)]
		public uint MaintenanceWindowDays
		{
			get
			{
				return (uint)(base.Fields["MaintenanceWindowDays"] ?? 14U);
			}
			set
			{
				base.Fields["MaintenanceWindowDays"] = value;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x000CDBEE File Offset: 0x000CBDEE
		// (set) Token: 0x06003267 RID: 12903 RVA: 0x000CDC05 File Offset: 0x000CBE05
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

		// Token: 0x06003268 RID: 12904 RVA: 0x000CDC18 File Offset: 0x000CBE18
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			base.ValidateSProcExists("[Exchange2010].[ServiceStatusCurrentStatus]");
			TaskLogger.LogExit();
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000CDC38 File Offset: 0x000CBE38
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			if (dataObject != null)
			{
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)dataObject;
				if (adorganizationalUnit.OrganizationId.OrganizationalUnit != null && adorganizationalUnit.OrganizationId.ConfigurationUnit != null)
				{
					this.GetTenantServiceStatus(adorganizationalUnit.OrganizationId.OrganizationalUnit);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000CDC84 File Offset: 0x000CBE84
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
					this.GetTenantServiceStatus(null);
				}
				else
				{
					if (base.CurrentOrganizationId.OrganizationalUnit != null && base.CurrentOrganizationId.ConfigurationUnit != null)
					{
						this.GetTenantServiceStatus(base.CurrentOrganizationId.OrganizationalUnit);
					}
					base.WriteResult<T>(dataObjects);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000CDCFC File Offset: 0x000CBEFC
		private void GetTenantServiceStatus(ADObjectId tenantId)
		{
			SqlDataReader sqlDataReader = null;
			string text = (tenantId != null) ? tenantId.Name : "<all>";
			base.TraceInfo("Getting service status for tenant: {0}", new object[]
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
					SqlCommand sqlCommand = new SqlCommand("[Exchange2010].[ServiceStatusCurrentStatus]", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					if (tenantId != null)
					{
						sqlCommand.Parameters.Add("@TenantGuid", SqlDbType.UniqueIdentifier).Value = tenantId.ObjectGuid;
					}
					base.TraceInfo("Executing stored procedure: {0}", new object[]
					{
						"[Exchange2010].[ServiceStatusCurrentStatus]"
					});
					sqlDataReader = sqlCommand.ExecuteReader();
					base.TraceInfo("Processing current service status data for tenant: {0}", new object[]
					{
						text
					});
					this.ProcessCurrentServiceStatusData(tenantId, sqlDataReader);
					base.TraceInfo("Finished processing current service status data for tenant: {0}", new object[]
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

		// Token: 0x0600326C RID: 12908 RVA: 0x000CDE64 File Offset: 0x000CC064
		private void ProcessCurrentServiceStatusData(ADObjectId tenantId, SqlDataReader reader)
		{
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			Hashtable hashtable3 = new Hashtable();
			while (reader.Read())
			{
				string text = (string)reader["TenantName"];
				Guid guid = (Guid)reader["TenantGuid"];
				bool flag = (int)reader["IsOrganizationConfig"] == 1;
				string entityName = (string)reader["ManagedEntityFullName"];
				ExDateTime problemTime = new ExDateTime(ExTimeZone.UtcTimeZone, (DateTime)reader["DateTime"]);
				int impactedUserCount = (int)reader["AllMailboxCount"];
				ServiceStatus serviceStatus;
				List<string> value;
				if (hashtable.Contains(guid))
				{
					serviceStatus = (ServiceStatus)hashtable[guid];
					value = (List<string>)hashtable2[guid];
				}
				else
				{
					serviceStatus = new ServiceStatus();
					ADObjectId adobjectId;
					if (flag)
					{
						adobjectId = base.RootOrgContainerId;
					}
					else
					{
						adobjectId = base.ResolveTenantIdentity(text, guid, tenantId, ref hashtable3);
					}
					if (adobjectId == null)
					{
						continue;
					}
					serviceStatus.Identity = adobjectId;
					hashtable.Add(guid, serviceStatus);
					value = new List<string>();
					hashtable2.Add(guid, value);
				}
				if (this.TryMapEntityToTenantServiceStatus(text, entityName, problemTime, impactedUserCount, ref serviceStatus, ref value))
				{
					serviceStatus.MaintenanceWindowDays = this.MaintenanceWindowDays;
				}
			}
			foreach (object obj in hashtable.Values)
			{
				ServiceStatus dataObject = (ServiceStatus)obj;
				base.WriteResult(dataObject);
			}
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000CE0D0 File Offset: 0x000CC2D0
		private bool TryMapEntityToTenantServiceStatus(string tenantName, string entityName, ExDateTime problemTime, int impactedUserCount, ref ServiceStatus tenantServiceStatus, ref List<string> tenantEntityList)
		{
			if (string.IsNullOrEmpty(entityName))
			{
				this.WriteWarning(Strings.NoEntityLinkedToTenantInReportingDB(tenantName));
				return false;
			}
			string adSite = string.Empty;
			string text = entityName.Remove(entityName.IndexOf(":"));
			string key;
			if ((key = text) != null)
			{
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6002eda-1 == null)
				{
					<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6002eda-1 = new Dictionary<string, int>(7)
					{
						{
							"Microsoft.Exchange.2010.Mailbox.DatabaseService",
							0
						},
						{
							"Microsoft.Exchange.2010.ClientAccessActiveSyncService",
							1
						},
						{
							"Microsoft.Exchange.2010.ClientAccessOutlookWebAccessService",
							2
						},
						{
							"Microsoft.Exchange.2010.ClientAccessImap4Service",
							3
						},
						{
							"Microsoft.Exchange.2010.ClientAccessPop3Service",
							4
						},
						{
							"Microsoft.Exchange.2010.ClientAccessWebServicesService",
							5
						},
						{
							"Microsoft.Exchange.2010.HubTransportService",
							6
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6002eda-1.TryGetValue(key, out num))
				{
					Status tenantStatus;
					switch (num)
					{
					case 0:
						tenantStatus = this.GetTenantStatus(tenantServiceStatus, StatusType.MailboxDatabaseOffline);
						if (!tenantEntityList.Contains(entityName))
						{
							tenantEntityList.Add(entityName);
							tenantStatus.ImpactedUserCount += impactedUserCount;
						}
						tenantStatus.StatusMessage = Strings.MailboxDatabaseCasImap4CasPop3CasWebServicesServiceProblemMessage(tenantStatus.ImpactedUserCount);
						break;
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
						tenantStatus = this.GetTenantStatus(tenantServiceStatus, StatusType.ClientAccessOffline);
						adSite = this.GetADSiteName(entityName);
						if (!tenantEntityList.Exists((string name) => (name.StartsWith("Microsoft.Exchange.2010.ClientAccessActiveSyncService:") && name.EndsWith(adSite)) || (name.StartsWith("Microsoft.Exchange.2010.ClientAccessOutlookWebAccessService:") && name.EndsWith(adSite)) || (name.StartsWith("Microsoft.Exchange.2010.ClientAccessImap4Service:") && name.EndsWith(adSite)) || (name.StartsWith("Microsoft.Exchange.2010.ClientAccessPop3Service:") && name.EndsWith(adSite)) || (name.StartsWith("Microsoft.Exchange.2010.ClientAccessWebServicesService:") && name.EndsWith(adSite))))
						{
							tenantStatus.ImpactedUserCount += impactedUserCount;
						}
						if (!tenantEntityList.Contains(entityName))
						{
							tenantEntityList.Add(entityName);
						}
						if (text == "Microsoft.Exchange.2010.ClientAccessActiveSyncService")
						{
							tenantStatus.StatusMessage = Strings.CasActiveSyncServiceProblemMessage(tenantStatus.ImpactedUserCount);
						}
						else
						{
							tenantStatus.StatusMessage = Strings.MailboxDatabaseCasImap4CasPop3CasWebServicesServiceProblemMessage(tenantStatus.ImpactedUserCount);
						}
						break;
					case 6:
						tenantStatus = this.GetTenantStatus(tenantServiceStatus, StatusType.TransportOffline);
						adSite = this.GetADSiteName(entityName);
						if (!tenantEntityList.Exists((string name) => name.StartsWith("Microsoft.Exchange.2010.HubTransportService:") && name.EndsWith(adSite)))
						{
							tenantStatus.ImpactedUserCount += impactedUserCount;
						}
						if (!tenantEntityList.Contains(entityName))
						{
							tenantEntityList.Add(entityName);
						}
						tenantStatus.StatusMessage = Strings.TransportServiceProblemMessage(tenantStatus.ImpactedUserCount);
						break;
					default:
						goto IL_240;
					}
					if (tenantStatus.StartDateTime == (DateTime)ExDateTime.MinValue || (DateTime)problemTime < tenantStatus.StartDateTime)
					{
						tenantStatus.StartDateTime = (DateTime)problemTime;
					}
					return true;
				}
			}
			IL_240:
			this.WriteWarning(Strings.UnknownEntityLinkedToTenantInReportingDB(tenantName, entityName));
			return false;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000CE364 File Offset: 0x000CC564
		private string GetADSiteName(string entityName)
		{
			string result = string.Empty;
			int num = entityName.LastIndexOf(" - ");
			if (num > -1 && num + 3 < entityName.Length)
			{
				result = entityName.Substring(num + 3);
			}
			return result;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000CE3B8 File Offset: 0x000CC5B8
		private Status GetTenantStatus(ServiceStatus tenantServiceStatus, StatusType statusType)
		{
			Status status = tenantServiceStatus.StatusList.Find((Status s) => s.StatusType == statusType);
			if (status == null)
			{
				status = new Status(statusType);
				if (!tenantServiceStatus.StatusList.Contains(status))
				{
					tenantServiceStatus.StatusList.Add(status);
				}
			}
			return status;
		}

		// Token: 0x0400235C RID: 9052
		private const string CmdletNoun = "ServiceStatus";

		// Token: 0x0400235D RID: 9053
		private const string SPName = "[Exchange2010].[ServiceStatusCurrentStatus]";

		// Token: 0x0400235E RID: 9054
		private const uint DefaultMaintenanceWindowDays = 14U;
	}
}
