using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000589 RID: 1417
	public abstract class ReportingTask<TIdentity> : GetTenantADObjectWithIdentityTaskBase<TIdentity, ADOrganizationalUnit> where TIdentity : IIdentityParameter, new()
	{
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000CAC18 File Offset: 0x000C8E18
		// (set) Token: 0x060031E8 RID: 12776 RVA: 0x000CAC39 File Offset: 0x000C8E39
		[Parameter(Mandatory = false)]
		public Fqdn ReportingServer
		{
			get
			{
				return (Fqdn)(base.Fields["ReportingServer"] ?? this.DefaultReportingServerName);
			}
			set
			{
				base.Fields["ReportingServer"] = value;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000CAC4C File Offset: 0x000C8E4C
		// (set) Token: 0x060031EA RID: 12778 RVA: 0x000CAC6C File Offset: 0x000C8E6C
		[Parameter(Mandatory = false)]
		public string ReportingDatabase
		{
			get
			{
				return (string)(base.Fields["ReportingDatabase"] ?? "pdm-TenantDS");
			}
			set
			{
				base.Fields["ReportingDatabase"] = value;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000CAC7F File Offset: 0x000C8E7F
		// (set) Token: 0x060031EC RID: 12780 RVA: 0x000CAC87 File Offset: 0x000C8E87
		public override TIdentity Identity
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

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000CAC90 File Offset: 0x000C8E90
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000CAC94 File Offset: 0x000C8E94
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 107, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\Tasks\\ReportingTask.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000CACD8 File Offset: 0x000C8ED8
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = new ExistsFilter(ExchangeConfigurationUnitSchema.OrganizationalUnitLink);
				QueryFilter result;
				if (base.InternalFilter != null)
				{
					result = new AndFilter(new QueryFilter[]
					{
						base.InternalFilter,
						queryFilter
					});
				}
				else
				{
					result = queryFilter;
				}
				return result;
			}
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000CAD1C File Offset: 0x000C8F1C
		protected string GetSqlConnectionString()
		{
			return string.Format("server={0};database={1};Integrated Security=SSPI", this.ReportingServer.ToString(), this.ReportingDatabase);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000CAD46 File Offset: 0x000C8F46
		protected void TraceInfo(string message)
		{
			ExTraceGlobals.TraceTracer.Information((long)this.GetHashCode(), message);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000CAD5A File Offset: 0x000C8F5A
		protected void TraceInfo(string format, params object[] args)
		{
			this.TraceInfo(string.Format(format, args));
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000CAD6C File Offset: 0x000C8F6C
		protected void ValidateSProcExists(string sprocName)
		{
			try
			{
				string sqlConnectionString = this.GetSqlConnectionString();
				using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
				{
					this.TraceInfo("Opening SQL connection: {0}", new object[]
					{
						sqlConnectionString
					});
					sqlConnection.Open();
					string text = "IF (EXISTS (SELECT name FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type = N'P'))\r\n                        SELECT 1\r\n                    ELSE \r\n                        SELECT 0;";
					text = string.Format(text, sprocName);
					SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
					sqlCommand.CommandType = CommandType.Text;
					this.TraceInfo("Executing SQL statement: {0}", new object[]
					{
						text
					});
					if (!Convert.ToBoolean(sqlCommand.ExecuteScalar()))
					{
						base.WriteError(new ReportsMPNotInstalledException(), (ErrorCategory)1001, null);
					}
					this.TraceInfo("Finished executing SQL statement: {0}", new object[]
					{
						text
					});
				}
			}
			catch (SqlException ex)
			{
				base.WriteError(new SqlReportingConnectionException(ex.Message), (ErrorCategory)1000, null);
			}
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000CAE60 File Offset: 0x000C9060
		protected ADObjectId ResolveTenantIdentity(string tenantNameFromReportingDB, Guid tenantGuidFromReportingDB, ADObjectId tenantId, ref Hashtable unresolvedTenants)
		{
			if (unresolvedTenants == null)
			{
				throw new ArgumentNullException("unresolvedTenants");
			}
			if (tenantId != null && tenantId.ObjectGuid == tenantGuidFromReportingDB)
			{
				return tenantId;
			}
			if (unresolvedTenants.Contains(tenantGuidFromReportingDB))
			{
				return null;
			}
			ADObjectId result;
			try
			{
				this.ConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(new OrganizationIdParameter(tenantGuidFromReportingDB.ToString()), this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(tenantNameFromReportingDB)), new LocalizedString?(Strings.ErrorOrganizationNotUnique(tenantNameFromReportingDB)));
				result = adorganizationalUnit.OrganizationId.OrganizationalUnit;
			}
			catch (ManagementObjectNotFoundException ex)
			{
				base.WriteWarning(ex.Message);
				unresolvedTenants.Add(tenantGuidFromReportingDB, null);
				result = null;
			}
			return result;
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000CAF28 File Offset: 0x000C9128
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (this.Identity != null)
			{
				IEnumerable<ADOrganizationalUnit> dataObjects = base.GetDataObjects(this.Identity);
				this.WriteResult<ADOrganizationalUnit>(dataObjects);
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400233D RID: 9021
		private const string DefaultReportingDatabaseName = "pdm-TenantDS";

		// Token: 0x0400233E RID: 9022
		private Fqdn DefaultReportingServerName = Fqdn.Parse("pdm-tenantds.exmgmt.local");
	}
}
