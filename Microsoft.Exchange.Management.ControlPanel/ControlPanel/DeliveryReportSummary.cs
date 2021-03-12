using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200029B RID: 667
	[ToolboxData("<{0}:DeliveryReportSummary runat=server></{0}:DeliveryReportSummary>")]
	[ClientScriptResource("DeliveryReportSummary", "Microsoft.Exchange.Management.ControlPanel.Client.DeliveryReports.js")]
	public class DeliveryReportSummary : ScriptControlBase, INamingContainer
	{
		// Token: 0x06002B48 RID: 11080 RVA: 0x00087358 File Offset: 0x00085558
		public DeliveryReportSummary() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x00087364 File Offset: 0x00085564
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("ListView", this.ListViewID, this);
			if (this.RecipientCounts != null)
			{
				descriptor.AddProperty("RecipientCounts", this.RecipientCounts);
			}
			descriptor.AddElementProperty("RecipientsStatusLinksCell", this.recipientsStatusLinksCell.ClientID, this);
			descriptor.AddElementProperty("LargeRecipientsTextRow", this.largeRecipientsTextRow.ClientID, this);
			descriptor.AddComponentProperty("ListViewDataSource", this.ListViewDataSourceID, this);
			descriptor.AddProperty("IsOWA", this.IsOWA);
			descriptor.AddProperty("DeliveryReportUrl", this.DeliveryReportUrl);
			descriptor.AddComponentProperty("NewMailMessageWebServiceMethod", this.newMailMessageWSMethod.ClientID, this);
			descriptor.AddComponentProperty("DeliveryStatusDataSourceRefreshMethod", this.DeliveryStatusDataSourceRefreshMethod.ClientID, this);
		}

		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x00087438 File Offset: 0x00085638
		// (set) Token: 0x06002B4B RID: 11083 RVA: 0x00087440 File Offset: 0x00085640
		public string ListViewID { get; set; }

		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x00087449 File Offset: 0x00085649
		// (set) Token: 0x06002B4D RID: 11085 RVA: 0x00087451 File Offset: 0x00085651
		public RecipientCounts RecipientCounts { get; set; }

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x0008745A File Offset: 0x0008565A
		// (set) Token: 0x06002B4F RID: 11087 RVA: 0x00087462 File Offset: 0x00085662
		public string ListViewDataSourceID { get; set; }

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x0008746B File Offset: 0x0008566B
		// (set) Token: 0x06002B51 RID: 11089 RVA: 0x00087473 File Offset: 0x00085673
		public bool IsOWA { get; set; }

		// Token: 0x17001D65 RID: 7525
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x0008747C File Offset: 0x0008567C
		// (set) Token: 0x06002B53 RID: 11091 RVA: 0x00087484 File Offset: 0x00085684
		public string DeliveryReportUrl { get; set; }

		// Token: 0x17001D66 RID: 7526
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x0008748D File Offset: 0x0008568D
		// (set) Token: 0x06002B55 RID: 11093 RVA: 0x00087495 File Offset: 0x00085695
		public WebServiceReference MailMessageServiceUrl { get; set; }

		// Token: 0x17001D67 RID: 7527
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x0008749E File Offset: 0x0008569E
		// (set) Token: 0x06002B57 RID: 11095 RVA: 0x000874A6 File Offset: 0x000856A6
		public WebServiceMethod DeliveryStatusDataSourceRefreshMethod { get; internal set; }

		// Token: 0x06002B58 RID: 11096 RVA: 0x000874B0 File Offset: 0x000856B0
		protected override void CreateChildControls()
		{
			this.summaryTable = new Table();
			this.summaryTable.ID = "summaryTable";
			TableRow tableRow = new TableRow();
			EncodingLabel encodingLabel = new EncodingLabel();
			encodingLabel.Text = OwaOptionStrings.SummaryToDate;
			this.recipientsStatusLinksCell = new TableCell();
			this.recipientsStatusLinksCell.Controls.Add(encodingLabel);
			this.recipientsStatusLinksCell.ID = "recipientStatusLinks";
			tableRow.Cells.Add(this.recipientsStatusLinksCell);
			this.largeRecipientsTextRow = new TableRow();
			this.largeRecipientsTextRow.ID = "largeRecipTxtRow";
			TableCell tableCell = new TableCell();
			EncodingLabel encodingLabel2 = new EncodingLabel();
			encodingLabel2.Text = OwaOptionStrings.LargeRecipientList(30);
			tableCell.Controls.Add(encodingLabel2);
			this.largeRecipientsTextRow.Cells.Add(tableCell);
			this.summaryTable.Rows.Add(tableRow);
			this.summaryTable.Rows.Add(this.largeRecipientsTextRow);
			this.Controls.Add(this.summaryTable);
			this.AddNewMailMessageWebServiceMthod();
			base.CreateChildControls();
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000875D0 File Offset: 0x000857D0
		private void AddNewMailMessageWebServiceMthod()
		{
			this.newMailMessageWSMethod = new WebServiceMethod();
			this.newMailMessageWSMethod.ServiceUrl = this.MailMessageServiceUrl;
			this.newMailMessageWSMethod.Method = "NewObject";
			this.newMailMessageWSMethod.ID = "NewMailMessage";
			this.newMailMessageWSMethod.ParameterNames = WebServiceParameterNames.NewObject;
			this.Controls.Add(this.newMailMessageWSMethod);
		}

		// Token: 0x0400218C RID: 8588
		private TableRow largeRecipientsTextRow;

		// Token: 0x0400218D RID: 8589
		private TableCell recipientsStatusLinksCell;

		// Token: 0x0400218E RID: 8590
		private WebServiceMethod newMailMessageWSMethod;

		// Token: 0x0400218F RID: 8591
		private Table summaryTable;
	}
}
