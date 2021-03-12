using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000614 RID: 1556
	[ClientScriptResource("MessageBox", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class ModalDialog : ScriptControlBase
	{
		// Token: 0x06004541 RID: 17729 RVA: 0x000D1730 File Offset: 0x000CF930
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("Buttons", this.Buttons);
			if (this.PopupAtStartup)
			{
				descriptor.AddProperty("PopupAtStartup", true);
			}
			if (this.infos != null)
			{
				descriptor.AddProperty("Infos", this.infos);
			}
		}

		// Token: 0x170026B7 RID: 9911
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x000D178C File Offset: 0x000CF98C
		// (set) Token: 0x06004543 RID: 17731 RVA: 0x000D1794 File Offset: 0x000CF994
		[Browsable(false)]
		public bool PopupAtStartup
		{
			get
			{
				return this.popupAtStartup;
			}
			set
			{
				this.popupAtStartup = value;
			}
		}

		// Token: 0x170026B8 RID: 9912
		// (get) Token: 0x06004544 RID: 17732 RVA: 0x000D179D File Offset: 0x000CF99D
		// (set) Token: 0x06004545 RID: 17733 RVA: 0x000D17A5 File Offset: 0x000CF9A5
		[Browsable(false)]
		public MessageBoxButton Buttons
		{
			get
			{
				return this.buttons;
			}
			set
			{
				this.buttons = value;
			}
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x000D17AE File Offset: 0x000CF9AE
		public ModalDialog()
		{
			this.EnableViewState = false;
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x000D17C4 File Offset: 0x000CF9C4
		public static ModalDialog GetCurrent(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			return (ModalDialog)page.Items[typeof(ModalDialog)];
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x000D17F0 File Offset: 0x000CF9F0
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode)
			{
				ModalDialog current = ModalDialog.GetCurrent(this.Page);
				if (current != null)
				{
					throw new InvalidOperationException("Only one instance of ModalDialog is allowed in one page.");
				}
				this.Page.Items[typeof(ModalDialog)] = this;
			}
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x000D1844 File Offset: 0x000CFA44
		internal void ShowDialog(string title, string message, string details, ModalDialogType dialogType)
		{
			this.ThrowInvalidOperationExceptionIfShowDialogIsPending();
			InfoCore infoCore = new InfoCore
			{
				JsonTitle = title,
				Message = message,
				Details = details,
				MessageBoxType = dialogType
			};
			this.infos = new InfoCore[]
			{
				infoCore
			};
			this.PopupAtStartup = true;
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x000D1895 File Offset: 0x000CFA95
		internal void ShowDialog(InfoCore[] infos)
		{
			this.ThrowInvalidOperationExceptionIfShowDialogIsPending();
			this.infos = infos;
			this.PopupAtStartup = true;
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x000D18AC File Offset: 0x000CFAAC
		protected override void CreateChildControls()
		{
			Panel panel = new Panel();
			panel.ID = "frm";
			this.Controls.Add(panel);
			this.modalPopupExtender = new ModalPopupExtender();
			this.modalPopupExtender.ID = "modalpopupextenderDialog";
			this.modalPopupExtender.BackgroundCssClass = "ModalDlgBackground";
			this.modalPopupExtender.BehaviorID = "modalpopupextenderDialog";
			this.modalPopupExtender.PopupControlID = this.ClientID;
			this.modalPopupExtender.TargetControlID = "hiddenPanel";
			Panel panel2 = new Panel();
			panel2.ID = "hiddenPanel";
			panel2.Attributes.Add("style", "display:none;");
			this.Controls.Add(panel2);
			this.Controls.Add(this.modalPopupExtender);
		}

		// Token: 0x170026B9 RID: 9913
		// (get) Token: 0x0600454C RID: 17740 RVA: 0x000D1975 File Offset: 0x000CFB75
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x000D197C File Offset: 0x000CFB7C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			if (string.IsNullOrEmpty(cssClass))
			{
				this.CssClass = "ModalDlg";
			}
			else
			{
				this.CssClass += " ModalDlg";
			}
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
			writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none");
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x000D19D8 File Offset: 0x000CFBD8
		private void ThrowInvalidOperationExceptionIfShowDialogIsPending()
		{
			if (this.PopupAtStartup)
			{
				string message = string.Format("Cannot change ModalDialog.{0} property while a request to show dialog is pending.", new object[0]);
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x04002E6A RID: 11882
		private ModalPopupExtender modalPopupExtender;

		// Token: 0x04002E6B RID: 11883
		private InfoCore[] infos;

		// Token: 0x04002E6C RID: 11884
		private bool popupAtStartup;

		// Token: 0x04002E6D RID: 11885
		private MessageBoxButton buttons = MessageBoxButton.OK;
	}
}
