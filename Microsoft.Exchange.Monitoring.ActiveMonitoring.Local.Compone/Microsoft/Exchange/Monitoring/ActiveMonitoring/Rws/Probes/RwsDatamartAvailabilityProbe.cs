using System;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes
{
	// Token: 0x0200045B RID: 1115
	public class RwsDatamartAvailabilityProbe : ProbeWorkItem
	{
		// Token: 0x06001C35 RID: 7221 RVA: 0x000A43E0 File Offset: 0x000A25E0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			string text = string.Empty;
			try
			{
				text = this.GetConnectionString();
				using (SqlConnection sqlConnection = new SqlConnection(text))
				{
					sqlConnection.Open();
					base.Result.StateAttribute21 = string.Format("Successfully connected to SQL Server {0}, Database {1}.", sqlConnection.DataSource, "CDM-TenantDS");
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("Exception when try to open connection to datamart. Exception: {0}. The connection string is {1}.", ex.Message, text), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityProbe.cs", 69);
				base.Result.StateAttribute21 = text;
				throw;
			}
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000A4490 File Offset: 0x000A2690
		private string GetConnectionString()
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
			string connectionString;
			try
			{
				sqlConnectionStringBuilder.DataSource = base.Definition.Endpoint;
				sqlConnectionStringBuilder.InitialCatalog = "CDM-TenantDS";
				sqlConnectionStringBuilder.ConnectTimeout = 30;
				sqlConnectionStringBuilder.IntegratedSecurity = true;
				connectionString = sqlConnectionStringBuilder.ConnectionString;
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, string.Format("Failed to set the connection string, exception: {0}, ConnectionString: {1}. ", ex.Message, sqlConnectionStringBuilder.ConnectionString), null, "GetConnectionString", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Rws\\RwsDatamartAvailabilityProbe.cs", 98);
				throw;
			}
			return connectionString;
		}

		// Token: 0x04001372 RID: 4978
		private const string ConnectionStringFormat = "Server={0};Database={1};Integrated Security=SSPI;Connection Timeout={2}";

		// Token: 0x04001373 RID: 4979
		private const string TenantsDatamartDatabaseName = "CDM-TenantDS";

		// Token: 0x04001374 RID: 4980
		private const int TenantsDatamartConnectionTimeoutSeconds = 30;
	}
}
