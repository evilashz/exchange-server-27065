using System;
using System.Configuration;
using System.Drawing;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200067B RID: 1659
	public class ViewO365ReportCommand : WizardCommand
	{
		// Token: 0x060047CD RID: 18381 RVA: 0x000DA999 File Offset: 0x000D8B99
		public ViewO365ReportCommand() : this(string.Empty, CommandSprite.SpriteId.NONE)
		{
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x000DA9A8 File Offset: 0x000D8BA8
		public ViewO365ReportCommand(string text, CommandSprite.SpriteId imageID) : base(text, imageID)
		{
			this.DialogSize = ViewO365ReportCommand.DefaultDialogSize;
			this.OnClientClick = "ViewO365ReportCommandHandler";
			base.ImageAltText = Strings.ViewDetailsCommandText;
			this.BypassUrlCheck = true;
			base.Visible = !string.IsNullOrEmpty(ViewO365ReportCommand.o365Url);
			this.ServiceId = "Exchange";
		}

		// Token: 0x1700278B RID: 10123
		// (get) Token: 0x060047CF RID: 18383 RVA: 0x000DAA08 File Offset: 0x000D8C08
		// (set) Token: 0x060047D0 RID: 18384 RVA: 0x000DAA30 File Offset: 0x000D8C30
		public override string NavigateUrl
		{
			get
			{
				return ViewO365ReportCommand.o365Url + string.Format("Reports/ReportDetails.aspx?ServiceId={0}&CategoryId={1}&ReportId={2}", this.ServiceId, this.CategoryId, this.ReportId);
			}
			set
			{
				base.NavigateUrl = string.Empty;
			}
		}

		// Token: 0x1700278C RID: 10124
		// (get) Token: 0x060047D1 RID: 18385 RVA: 0x000DAA3D File Offset: 0x000D8C3D
		// (set) Token: 0x060047D2 RID: 18386 RVA: 0x000DAA45 File Offset: 0x000D8C45
		public string ReportId { get; set; }

		// Token: 0x1700278D RID: 10125
		// (get) Token: 0x060047D3 RID: 18387 RVA: 0x000DAA4E File Offset: 0x000D8C4E
		// (set) Token: 0x060047D4 RID: 18388 RVA: 0x000DAA56 File Offset: 0x000D8C56
		public string CategoryId { get; set; }

		// Token: 0x1700278E RID: 10126
		// (get) Token: 0x060047D5 RID: 18389 RVA: 0x000DAA5F File Offset: 0x000D8C5F
		// (set) Token: 0x060047D6 RID: 18390 RVA: 0x000DAA67 File Offset: 0x000D8C67
		public string ServiceId { get; set; }

		// Token: 0x0400303C RID: 12348
		private const int DefaultViewDetailWidth = 800;

		// Token: 0x0400303D RID: 12349
		private const int DefaultViewDetailHeight = 600;

		// Token: 0x0400303E RID: 12350
		private static readonly Size DefaultDialogSize = new Size(800, 600);

		// Token: 0x0400303F RID: 12351
		private static readonly string o365Url = ConfigurationManager.AppSettings["O365Url"];
	}
}
