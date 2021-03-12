using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001FE RID: 510
	public class ExportCsvHandler : IDownloadHandler
	{
		// Token: 0x0600268D RID: 9869 RVA: 0x00077AFA File Offset: 0x00075CFA
		static ExportCsvHandler()
		{
			ExportCsvHandler.GetListExportCsvDefaultResultSize = ConfigUtil.ReadInt("GetListExportCsvDefaultResultSize", 20000);
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00077B2C File Offset: 0x00075D2C
		public PowerShellResults ProcessRequest(HttpContext context)
		{
			string text = context.Request.QueryString["schema"];
			string text2 = context.Request.Form["workflowOutput"];
			string text3 = context.Request.Form["titlesCSV"];
			string text4 = context.Request.Form["PropertyList"];
			string filter = context.Request.Form["filter"];
			if (string.IsNullOrEmpty(text2))
			{
				throw new BadQueryParameterException("workflowOutput");
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new BadQueryParameterException("schema");
			}
			if (string.IsNullOrEmpty(text3))
			{
				throw new BadQueryParameterException("titlesCSV");
			}
			if (string.IsNullOrEmpty(text4))
			{
				throw new BadQueryParameterException("PropertyList");
			}
			string[] columnList = text2.Split(new char[]
			{
				','
			});
			Stream outputStream = context.Response.OutputStream;
			PowerShellResults<JsonDictionary<object>> powerShellResults;
			try
			{
				context.Response.ContentType = "text/csv";
				context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode(Strings.ExportCsvFileName + ".csv")));
				context.Response.ContentEncoding = Encoding.UTF8;
				context.Response.Charset = "utf-8";
				outputStream.Write(new byte[]
				{
					239,
					187,
					191
				}, 0, 3);
				byte[] bytes = Encoding.UTF8.GetBytes(new StringBuilder(text3.PadRight(256)).AppendLine().ToString());
				outputStream.Write(bytes, 0, bytes.Length);
				powerShellResults = this.GetPowerShellResult(text2, text4, text, filter);
				if (context.Response.IsClientConnected && powerShellResults.Succeeded && powerShellResults.Output.Length > 0)
				{
					JsonDictionary<object>[] output = powerShellResults.Output;
					this.WriteOnePageToFile(outputStream, output, columnList);
				}
			}
			catch (IOException exception)
			{
				powerShellResults = new PowerShellResults<JsonDictionary<object>>();
				powerShellResults.ErrorRecords = new ErrorRecord[]
				{
					new ErrorRecord(exception)
				};
			}
			catch (HttpException exception2)
			{
				powerShellResults = new PowerShellResults<JsonDictionary<object>>();
				powerShellResults.ErrorRecords = new ErrorRecord[]
				{
					new ErrorRecord(exception2)
				};
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

		// Token: 0x0600268F RID: 9871 RVA: 0x00077DBC File Offset: 0x00075FBC
		private void WriteOnePageToFile(Stream outputStream, JsonDictionary<object>[] output, string[] columnList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (output != null && output.Length > 0)
			{
				for (int i = 0; i < output.Length; i++)
				{
					Dictionary<string, object> row = output[i];
					this.GetOneRowResult(stringBuilder, row, columnList);
				}
			}
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			outputStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00077E18 File Offset: 0x00076018
		private void GetOneRowResult(StringBuilder sb, Dictionary<string, object> row, string[] columnList)
		{
			int num = columnList.Length;
			for (int i = 0; i < columnList.Length; i++)
			{
				object obj = row[columnList[i]];
				string text = (obj == null) ? string.Empty : obj.ToString();
				if (text != null)
				{
					if (text.IndexOfAny(ExportCsvHandler.SpecialChars) < 0)
					{
						sb.Append(text);
					}
					else
					{
						sb.Append("\"");
						sb.Append(text.Replace("\"", "\"\""));
						sb.Append("\"");
					}
				}
				if (--num > 0)
				{
					sb.Append(",");
				}
			}
			sb.AppendLine(string.Empty);
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x00077EC0 File Offset: 0x000760C0
		private PowerShellResults<JsonDictionary<object>> GetPowerShellResult(string outputColumn, string properties, string schema, string filter)
		{
			IDDIService iddiservice = (IDDIService)new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=" + schema).ServiceInstance;
			DDIParameters ddiparameters = new DDIParameters();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["workflowOutput"] = outputColumn;
			dictionary["PropertyList"] = properties;
			if (!string.IsNullOrEmpty(filter))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary2 = javaScriptSerializer.Deserialize<Dictionary<string, object>>(filter);
				foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			ddiparameters.Parameters = new JsonDictionary<object>(dictionary);
			return iddiservice.GetList(ddiparameters, null);
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x00077F90 File Offset: 0x00076190
		public static bool IsExportCsv
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				return httpContext != null && httpContext.Request.QueryString["handlerClass"] == "ExportCsvHandler";
			}
		}

		// Token: 0x04001F74 RID: 8052
		private const string TitlesCSV = "titlesCSV";

		// Token: 0x04001F75 RID: 8053
		private const string Schema = "schema";

		// Token: 0x04001F76 RID: 8054
		private const string PropertyList = "PropertyList";

		// Token: 0x04001F77 RID: 8055
		private const string Filter = "filter";

		// Token: 0x04001F78 RID: 8056
		private const string SearchText = "SearchText";

		// Token: 0x04001F79 RID: 8057
		public static readonly int GetListExportCsvDefaultResultSize;

		// Token: 0x04001F7A RID: 8058
		private static readonly char[] SpecialChars = new char[]
		{
			',',
			'"',
			'\r',
			'\t',
			'\n'
		};
	}
}
