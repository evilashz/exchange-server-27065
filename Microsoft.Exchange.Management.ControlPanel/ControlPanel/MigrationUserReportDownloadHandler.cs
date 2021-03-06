using System;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000209 RID: 521
	public class MigrationUserReportDownloadHandler : IDownloadHandler
	{
		// Token: 0x060026C5 RID: 9925 RVA: 0x0007873C File Offset: 0x0007693C
		public PowerShellResults ProcessRequest(HttpContext context)
		{
			string text = context.Request.QueryString["identity"];
			if (string.IsNullOrEmpty(text))
			{
				throw new BadQueryParameterException("identity");
			}
			PowerShellResults<JsonDictionary<object>> powerShellResults = null;
			Stream outputStream = context.Response.OutputStream;
			try
			{
				context.Response.ContentType = "text/plain";
				context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode(Strings.MigrationUserReportFileName + "_" + text + ".txt")));
				context.Response.ContentEncoding = Encoding.UTF8;
				context.Response.Charset = "utf-8";
				outputStream.Write(new byte[]
				{
					239,
					187,
					191
				}, 0, 3);
				byte[] bytes = Encoding.UTF8.GetBytes(new string('​', 256).ToCharArray());
				outputStream.Write(bytes, 0, bytes.Length);
				IDDIService iddiservice = (IDDIService)new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=MigrationBatchUser&workflow=DownloadReport").ServiceInstance;
				powerShellResults = iddiservice.SingleObjectExecute(new Identity(text), null);
				if (context.Response.IsClientConnected)
				{
					if (powerShellResults.Succeeded)
					{
						this.OutputSucceedResults(outputStream, powerShellResults);
					}
					else
					{
						this.OutputFailedResults(outputStream, powerShellResults);
						powerShellResults.ErrorRecords = new ErrorRecord[0];
					}
				}
			}
			catch (IOException innerException)
			{
				throw new BadRequestException(innerException);
			}
			catch (HttpException innerException2)
			{
				throw new BadRequestException(innerException2);
			}
			finally
			{
				if (outputStream != null)
				{
					outputStream.Close();
				}
			}
			return powerShellResults;
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00078900 File Offset: 0x00076B00
		private void OutputSucceedResults(Stream outputStream, PowerShellResults<JsonDictionary<object>> results)
		{
			if (results.Output != null && results.Output.Length > 0)
			{
				JsonDictionary<object> jsonDictionary = results.Output[0];
				object obj = null;
				if (jsonDictionary.RawDictionary.TryGetValue("Report", out obj) && obj != null)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(((string)obj).ToCharArray());
					outputStream.Write(bytes, 0, bytes.Length);
				}
			}
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x00078964 File Offset: 0x00076B64
		private void OutputFailedResults(Stream outputStream, PowerShellResults<JsonDictionary<object>> results)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (results.ErrorRecords != null)
			{
				foreach (ErrorRecord errorRecord in results.ErrorRecords)
				{
					stringBuilder.Append(errorRecord.Message + Environment.NewLine);
				}
			}
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString().ToCharArray());
			outputStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x04001F85 RID: 8069
		private const string IdentityName = "identity";

		// Token: 0x04001F86 RID: 8070
		private const string Schema = "MigrationBatchUser";

		// Token: 0x04001F87 RID: 8071
		private const string Workflow = "DownloadReport";

		// Token: 0x04001F88 RID: 8072
		private const string ReportPropertyName = "Report";
	}
}
