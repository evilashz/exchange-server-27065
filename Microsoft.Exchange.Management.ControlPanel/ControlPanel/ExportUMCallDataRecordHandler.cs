using System;
using System.Globalization;
using System.Management.Automation;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A5 RID: 1189
	internal class ExportUMCallDataRecordHandler : DataSourceService, IDownloadHandler
	{
		// Token: 0x06003B06 RID: 15110 RVA: 0x000B28F4 File Offset: 0x000B0AF4
		public PowerShellResults ProcessRequest(HttpContext context)
		{
			ExDateTime date;
			if (!ExDateTime.TryParseExact(context.Request.QueryString["Date"], "d", Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out date))
			{
				throw new BadRequestException(new Exception("ExportUMCallHandler got a request with Date not specified in query param or Date is invalid."));
			}
			ExportUMCallDataRecordParameters exportUMCallDataRecordParameters = new ExportUMCallDataRecordParameters();
			exportUMCallDataRecordParameters.Date = date;
			exportUMCallDataRecordParameters.UMDialPlan = context.Request.QueryString["UMDialPlanID"];
			exportUMCallDataRecordParameters.UMIPGateway = context.Request.QueryString["UMIPGatewayID"];
			exportUMCallDataRecordParameters.ClientStream = context.Response.OutputStream;
			context.Response.ContentType = "text/csv";
			context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", this.ConstructFilename(exportUMCallDataRecordParameters)));
			PowerShellResults powerShellResults = this.ExportObject(exportUMCallDataRecordParameters);
			if (this.IsValidUserError(powerShellResults))
			{
				powerShellResults = new PowerShellResults();
			}
			return powerShellResults;
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000B29DC File Offset: 0x000B0BDC
		private bool IsValidUserError(PowerShellResults results)
		{
			return !results.Succeeded && results.ErrorRecords[0].Exception is HttpException;
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000B2A00 File Offset: 0x000B0C00
		[PrincipalPermission(SecurityAction.Demand, Role = "Export-UMCallDataRecord?Date&ClientStream@R:Organization")]
		private PowerShellResults ExportObject(ExportUMCallDataRecordParameters parameters)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Export-UMCallDataRecord");
			psCommand.AddParameters(parameters);
			return base.Invoke(psCommand);
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x000B2A2C File Offset: 0x000B0C2C
		private string ConstructFilename(ExportUMCallDataRecordParameters parameters)
		{
			StringBuilder stringBuilder = new StringBuilder("UM_CDR");
			stringBuilder.Append("_");
			stringBuilder.Append(parameters.Date.ToString("yyyy-MM-dd"));
			stringBuilder.Append(".csv");
			return stringBuilder.ToString();
		}

		// Token: 0x04002743 RID: 10051
		private const string ShortDateTimeFormat = "d";

		// Token: 0x04002744 RID: 10052
		private const string DateFormatForFileName = "yyyy-MM-dd";

		// Token: 0x04002745 RID: 10053
		private const string Noun = "UMCallDataRecord";

		// Token: 0x04002746 RID: 10054
		private const string FilenamePrefix = "UM_CDR";

		// Token: 0x04002747 RID: 10055
		private const string CSVExtension = ".csv";

		// Token: 0x04002748 RID: 10056
		public const string ExportCmdlet = "Export-UMCallDataRecord";

		// Token: 0x04002749 RID: 10057
		public const string ReadScope = "@R:Organization";

		// Token: 0x0400274A RID: 10058
		private const string ExportObjectRole = "Export-UMCallDataRecord?Date&ClientStream@R:Organization";
	}
}
