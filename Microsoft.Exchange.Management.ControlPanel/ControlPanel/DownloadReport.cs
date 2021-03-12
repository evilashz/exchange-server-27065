using System;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000221 RID: 545
	[ClientScriptResource("DownloadReport", "Microsoft.Exchange.Management.ControlPanel.Client.Users.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class DownloadReport : EcpContentPage
	{
		// Token: 0x06002765 RID: 10085 RVA: 0x0007BC54 File Offset: 0x00079E54
		protected override void OnLoad(EventArgs e)
		{
			string value = this.Context.Request.QueryString["Identity"];
			if (string.IsNullOrEmpty(value))
			{
				throw new BadQueryParameterException("Identity");
			}
			if (string.IsNullOrEmpty(this.Context.Request.QueryString["Name"]))
			{
				throw new BadQueryParameterException("Name");
			}
			if (string.IsNullOrEmpty(this.Context.Request.QueryString["HandlerClass"]))
			{
				throw new BadQueryParameterException("HandlerClass");
			}
			Identity identity = Identity.FromIdParameter(value);
			WebServiceReference webServiceReference = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=MigrationReport");
			PowerShellResults<JsonDictionary<object>> powerShellResults = (PowerShellResults<JsonDictionary<object>>)webServiceReference.GetObject(identity);
			if (!powerShellResults.SucceededWithValue)
			{
				throw new BadQueryParameterException("Identity");
			}
			if ((MigrationType)powerShellResults.Output[0]["MigrationType"] == MigrationType.BulkProvisioning)
			{
				this.OverrideStringsForBulkProvisioning();
			}
			if (this.linkShowReport != null)
			{
				this.linkShowReport.NavigateUrl = this.Context.Request.RawUrl.Replace("DownloadReport.aspx?", "Download.aspx?");
			}
			base.OnLoad(e);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0007BD84 File Offset: 0x00079F84
		private void OverrideStringsForBulkProvisioning()
		{
			base.Title = Strings.DownloadErrorReportTitle;
			if (this.lblReportTitle != null)
			{
				this.lblReportTitle.Text = Strings.DownloadErrorReportTitle;
			}
			if (this.lblReportMsg != null)
			{
				this.lblReportMsg.Text = Strings.DownloadErrorReportText;
			}
		}

		// Token: 0x04001FFC RID: 8188
		protected Label lblReportTitle;

		// Token: 0x04001FFD RID: 8189
		protected Label lblReportMsg;

		// Token: 0x04001FFE RID: 8190
		protected HyperLink linkShowReport;
	}
}
