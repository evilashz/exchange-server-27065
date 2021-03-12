using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000226 RID: 550
	public class MigrationReportHandler : IDownloadHandler
	{
		// Token: 0x0600277A RID: 10106 RVA: 0x0007C2DC File Offset: 0x0007A4DC
		public PowerShellResults ProcessRequest(HttpContext context)
		{
			Identity identity = Identity.FromIdParameter(context.Request.QueryString["Identity"]);
			string text = context.Request.QueryString["Name"];
			if (identity == null || string.IsNullOrEmpty(identity.RawIdentity))
			{
				throw new BadQueryParameterException("Identity");
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new BadQueryParameterException("Name");
			}
			context.Response.ContentType = "text/csv";
			context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode(text)));
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["CsvStream"] = context.Response.OutputStream;
			DDIParameters properties = new DDIParameters
			{
				Parameters = new JsonDictionary<object>(dictionary)
			};
			IDDIService iddiservice = (IDDIService)new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=MigrationReport&workflow=ExportMigrationReport").ServiceInstance;
			PowerShellResults powerShellResults = iddiservice.SingleObjectExecute(identity, properties);
			if (this.IsValidUserError(powerShellResults))
			{
				powerShellResults = new PowerShellResults();
			}
			return powerShellResults;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0007C3EF File Offset: 0x0007A5EF
		private bool IsValidUserError(PowerShellResults results)
		{
			return !results.Succeeded && results.ErrorRecords[0].Exception is HttpException;
		}
	}
}
