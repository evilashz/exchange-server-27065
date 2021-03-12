using System;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006A7 RID: 1703
	internal class ReportContextFactory : IReportContextFactory
	{
		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x0010110A File Offset: 0x000FF30A
		// (set) Token: 0x06003C59 RID: 15449 RVA: 0x00101112 File Offset: 0x000FF312
		public Type ReportType { get; set; }

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x0010111B File Offset: 0x000FF31B
		// (set) Token: 0x06003C5B RID: 15451 RVA: 0x00101123 File Offset: 0x000FF323
		public string ViewName { get; set; }

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x0010112C File Offset: 0x000FF32C
		// (set) Token: 0x06003C5D RID: 15453 RVA: 0x00101134 File Offset: 0x000FF334
		public DataMartType DataMartType { get; set; }

		// Token: 0x06003C5E RID: 15454 RVA: 0x00101140 File Offset: 0x000FF340
		public IDbConnection CreateConnection(bool createBackupConnection = false)
		{
			string connectionString = DataMart.Instance.GetConnectionString(this.DataMartType, createBackupConnection);
			if (!string.IsNullOrEmpty(connectionString))
			{
				return new SqlConnection(connectionString);
			}
			return null;
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x00101174 File Offset: 0x000FF374
		public IReportContext CreateReportContext(IDbConnection connection)
		{
			ReportContext reportContext = new ReportContext(connection);
			reportContext.ChangeViewName(this.ReportType, this.ViewName);
			return reportContext;
		}
	}
}
